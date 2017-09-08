namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;
	using System.IO;

	using Stateless;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.Sensors;

	using TestBench.Droid.Devices;

	/// <summary>
	/// The pressure rig controller is responsible for for controling the pressure rig thoughout a test procedure.
	/// </summary>
	public class PressureRigController : ITest {
		private static readonly Scalar APPROXIMATION_AMOUNT = Units.Pressure.PSIG.OfScalar(5);


		// Implemented from ITest
		public event OnTestEvent onTestEvent;

		// Implemented from ITest
		public EDeviceModel deviceModel { get; private set; }
		// Implemented from ITest
		public ESensorType sensorType { get; private set; }
		// Implemented from ITest
		public TestParameters parameters { get; private set; }
		// Implemented from ITest
		public int currentTargetPointIndex { get; set; }
		// Implemented from ITest
		public IRig rig { get { return pressureRig; } }
		// Implemented from ITest
		public Dictionary<ISerialNumber, IConnection> connections { get; private set; }
		// Implemented from ITest
		public TestResults testResults { get; }
		// Implemented from ITest
		public bool isTesting { get; private set; }
		// Implemented from ITest
		public string testName { get; private set; }

		/// <summary>
		/// The rig that we are controlling.
		/// </summary>
		private PressureRigDevice pressureRig;
		/// <summary>
		/// The state machine that is used to walk through a test.
		/// </summary>
		private StateMachine<EState, ETrigger> sm;


		public PressureRigController(PressureRigDevice pressureRig) {
			this.pressureRig = pressureRig;
			this.pressureRig.onDeviceEvent += OnDeviceEvent;
		}

		// Implemented from ITest
		public void StartTest() {
		}

		public void StopTest() {
		}

		public string GetState() {
			return "";
		}

		public bool Export(Stream stream) {
			return false;
		}

		public StateMachine<EState, ETrigger> BuildStateMachine() {
			var ret = new StateMachine<EState, ETrigger>(EState.Off);

			// Register out unhandled trigger handler.
			ret.OnUnhandledTrigger(OnHandleUnhandledTrigger);
			ret.Configure(EState.Off)
			   .OnEntry(OnTestOff)
			   .Permit(ETrigger.Start, EState.Initialize);
			// Configure the initialization state.
			ret.Configure(EState.Initialize)
			   .OnEntry(OnInitializeRig)
			   .Permit(ETrigger.Success, EState.Idle)
			   .Permit(ETrigger.Failure, EState.Off);
			// Configure the idle state.
			ret.Configure(EState.Idle)
			   .Permit(ETrigger.Start, EState.VerifyTest);
			// Configure the Verify Test state
			ret.Configure(EState.VerifyTest)
			   .Permit(ETrigger.Success, EState.Pressurize)
			   .Permit(ETrigger.Failure, EState.Idle);
			// Configure the Pressurize state
			ret.Configure(EState.Pressurize)
			   .OnEntry(OnEnterPressure)
			   .OnExit(OnExitPressure)
			   .Permit(ETrigger.NearbyTarget, EState.VerifyTargetPoint)
			   .Permit(ETrigger.Success, EState.VerifyTargetPoint)
			   .Permit(ETrigger.Stop, EState.CancelOperation);
			// Configure the VerifyTargetPoint state
			ret.Configure(EState.VerifyTargetPoint)
			   .OnEntry(OnVerifyTargetPoint)
			   .Permit(ETrigger.Success, EState.Advance);
			ret.Configure(EState.Advance)
			   .OnEntry(OnAdvance)
			   .Permit(ETrigger.Success, EState.Pressurize)
			   .Permit(ETrigger.Stop, EState.Complete);
			// Configure the Complete state
			ret.Configure(EState.Complete)
			   .Permit(ETrigger.Success, EState.Idle);


			return ret;
		}

		private void OnDeviceEvent(DeviceEvent de) {
			switch (de.type) {
				case DeviceEvent.EType.NewData:
					DoDeviceUpdate();
					break;
			}
		}

		/// <summary>
		/// Queries whether or not the pressure rig's pressure is near the current target point.
		/// </summary>
		/// <returns><c>true</c>, if near target point was ised, <c>false</c> otherwise.</returns>
		private bool IsNearTargetPoint() {
			var p = pressureRig.pressure;
			var tp = parameters.targetPoints[currentTargetPointIndex].measurement.ConvertTo(p.unit);
			return p.amount > (tp.amount - APPROXIMATION_AMOUNT.ConvertTo(p.unit).amount);
		}

		private void DoDeviceUpdate() {
			if (sm.IsInState(EState.Pressurize)) {
				if (IsNearTargetPoint()) {
					sm.Fire(ETrigger.Success);
				}
			}
		}

		private void GotoTargetPoint(Scalar targetPoint) {
			var startime = DateTime.Now;
			var isNarrowing = false;
		}


		/*
		 * STATE ENTRY AND EXIT DEFINITIONS
		 */

		/// <summary>
		/// Cleans up the test's internal state.
		/// </summary>
		/// <returns>The test off.</returns>
		private void OnTestOff() {

		}

		/// <summary>
		/// Initializes the rig. This involved connecting to it if necessary and ensuring that the rig has properly
		/// initialized itself.
		/// </summary>
		private void OnInitializeRig() {
			var timeout = TimeSpan.FromMilliseconds(1000 * 15);

			if (!pressureRig.isConnected) {
				var start = DateTime.Now;
				while (!pressureRig.isConnected) {
					if (DateTime.Now - start > timeout) {
						break;
					}
				}
			}

			if (!pressureRig.isConnected) {
				sm.Fire(ETrigger.Failure);
			} else {
				sm.Fire(ETrigger.Success);
			}
		}

		private void OnEnterPressure() {
			pressureRig.connection.Write(pressureRig.pressureRigProtocol.CreateCommand(ECommand.G5On));
		}

		private void OnExitPressure()  {
			pressureRig.connection.Write(pressureRig.pressureRigProtocol.CreateCommand(ECommand.G5Off));
		}

		/// <summary>
		/// Resolves the pressure controller receiving a new pressure measurement from the pressure rig.
		/// </summary>
		private void OnRigPressureChanged() {
			if (sm.State == EState.Pressurize) {
				if (pressureRig.pressure > Units.Pressure.PSIG.OfScalar(5).amount) {
					sm.Fire(ETrigger.Success);
				}
			}
		}

		private void OnVerifyTargetPoint() {
			// Hold the Stop the G5 and track the pressure change.
			var currentTargetPressure = parameters.targetPoints[currentTargetPointIndex++].measurement;
			if (currentTargetPointIndex > parameters.targetPoints.Count) {
				// The test is complete.
				sm.Fire(ETrigger.Success);
			}
		}

		private void OnAdvance() {
			currentTargetPointIndex += 1;
			if (currentTargetPointIndex >= parameters.targetPoints.Count) {
				sm.Fire(ETrigger.Stop);
			} else {
				sm.Fire(ETrigger.Success);
			}
		}

		/// <summary>
		/// Handles the unhandled trigger.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="trigger">Trigger.</param>
		private void OnHandleUnhandledTrigger(EState state, ETrigger trigger) {
			Log.E(this, "Received unhandled trigger {" + trigger + "} for state: " + state);
			if (ETrigger.ConnectionLost == trigger) {
				Log.E(this, "The connection was lost. Cleaning up the test.");
			}
		}


		/// <summary>
		/// The enumeration of the states that the pressure rig can be in.
		/// </summary>
		public enum EState {
			/// <summary>
			/// The state that is used to show that the rig is not properly initialized.
			/// </summary>
			Off,
			/// <summary>
			/// The state that is used to prepare the rig hardware for a test. This is where the rig will perform any
			/// necessary preprocessing and initialization of internal hardware and state. Once the initialization is complete,
			/// a result trigger will be posted to the state machine.
			/// Action:
			/// 	InitializeRig
			/// </summary>
			Initialize,
			/// <summary>
			/// The state that is used to indicate that the rig hardware is idle.
			/// Actions:
			/// 	None
			/// </summary>
			Idle,
			/// <summary>
			/// The state that is performed once the state machine received the start test trigger. This state is used to
			/// validate the content of the test and ensure that it is acceptable for continued operation.
			/// Action:
			/// 	ValidateTest
			/// </summary>
			VerifyTest,
			/// <summary>
			/// The state that approached the next target point in the test.
			/// Action:
			/// 	PressurizeRigToCurrentTargetPoint
			/// </summary>
			Pressurize,
			/// <summary>
			/// The state that is used to verify that the reached target point is valid within the test parameters.
			/// Action:
			/// 	VerifyTargetPoint
			/// </summary>
			VerifyTargetPoint,
			/// <summary>
			/// The state where the state machine determines whether it needs to keep excuting or complete the test.
			/// Action:
			/// 	AdvanceTest
			/// </summary>
			Advance,
			/// <summary>
			/// The state that is used when the test is complete and needs to wrap up.
			/// Action:
			/// 	CompleteTest
			/// </summary>
			Complete,
			/// <summary>
			/// The state that is used when the test is cancel mid execution.
			/// </summary>
			CancelOperation,
		}

		public enum ETrigger {
			/// <summary>
			/// The trigger is used to start a test.
			/// </summary>
			Start,
			/// <summary>
			/// The trigger that is used to stop a test. Should be allowed to be used everywhere.
			/// </summary>
			Stop,
			/// <summary>
			/// The trigger used to indicate that a general state has been completed. 
			/// </summary>
			Success,
			/// <summary>
			/// The trigger that is used to indicate that the test lost connection to the pressure rig.
			/// </summary>
			ConnectionLost,
			/// <summary>
			/// The trigger used to indicate that a state failed its execution.
			/// </summary>
			Failure,
			/// <summary>
			/// The trigger that is used to indicate that the state is neary the target pressure.
			/// </summary>
			NearbyTarget,
		}
	}
}
