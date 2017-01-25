namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

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


		public PressureRigController() {
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

		private async Task GotoTargetPoint(Scalar targetPoint) {
			var startime = DateTime.Now;
			var isNarrowing = false;
			/*
			while (isNarrowing) {
				if (rig.pressure < (targetPoint - slop)) {
					await Task.Delay(100);
					continue;
				} else if (rig.pressure > targetPoint + slop) {
					await QuickReleasePressure();
				} else if (
			}
			*/
		}

		public StateMachine<EState, ETrigger> BuildStateMachine() {
			var ret = new StateMachine<EState, ETrigger>(EState.Idle);

			// Register out unhandled trigger handler.
			ret.OnUnhandledTrigger(HandleUnhandledTrigger);
			// Configure the initialization state.
			ret.Configure(EState.Initialize)
			   .OnEntry(InitializeRig)
			   .Permit(ETrigger.Success, EState.Idle);
			// Configure the idle state.
			ret.Configure(EState.Idle)
			   .Permit(ETrigger.Start, EState.VerifyTest);
			// Configure the Verify Test state
			ret.Configure(EState.VerifyTest)
			   .Permit(ETrigger.Success, EState.Pressurize)
			   .Permit(ETrigger.Failure, EState.Idle);
			// Configure the Pressurize state
			ret.Configure(EState.Pressurize)
			   .InternalTransition(ETrigger.OverPressure, t => DoReleivePressure())
			   .Permit(ETrigger.Success, EState.VerifyTargetPoint)
			   .Permit(ETrigger.Stop, EState.CancelOperation);
			// Configure the VerifyTargetPoint state
			ret.Configure(EState.VerifyTargetPoint)
			   .Permit(ETrigger.Success, EState.Complete)
			   .Permit(ETrigger.OverPressure, EState.ReleiveTargetPoint)
			   .Permit(ETrigger.UnderPressure, EState.Pressurize);
			// Configure the ReleiveTargetPoint state
			ret.Configure(EState.ReleiveTargetPoint)
			   .Permit(ETrigger.Success, EState.Pressurize)
			   .Permit(ETrigger.Stop, EState.CancelOperation);
			// Configure the Complete state
			ret.Configure(EState.Complete)
			   .Permit(ETrigger.Success, EState.Idle);


			return ret;
		}

		private void BastardedConceptVersion() {
			// while we are actively connected to the pressure rig
			while (pressureRig.isConnected) {
				switch (state) {
					case IDLE:
						DoNothing.EXE();
						break;
					case TESTING:
						var tp = GetCurrentTargetPoint();
						if (pressureRig.
						
				}
			}
		}

		/// <summary>
		/// Commits the initialization command the rig hardware and fires a result trigger to the state machine.
		/// </summary>
		private void InitializeRig() {
		}

		/// <summary>
		/// Write the releive pressure command to the remote rig.
		/// </summary>
		private void DoReleivePressure() {
		}

		/// <summary>
		/// Handles the unhandled trigger.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="trigger">Trigger.</param>
		private void HandleUnhandledTrigger(EState state, ETrigger trigger) {
			Log.E(this, "Received unhandled trigger {" + trigger + "} for state: " + state);
		}


		/// <summary>
		/// The enumeration of the states that the pressure rig can be in.
		/// </summary>
		public enum EState {
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
			/// The state that is used if a TargetPoint was not valid in the last state approach.
			/// Action:
			/// 	ReleiveRigPressure
			/// </summary>
			ReleiveTargetPoint,
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
			/// The trigger used to indicate that a state failed its execution.
			/// </summary>
			Failure,
			/// <summary>
			/// The trigger that is used to indicate that the state was over pressure.
			/// </summary>
			OverPressure,
			/// <summary>
			/// The trigger that is used to indicate that the state was under pressure.
			/// </summary>
			UnderPressure,
		}
	}
}
