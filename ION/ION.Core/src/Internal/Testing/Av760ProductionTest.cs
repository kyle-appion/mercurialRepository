namespace ION.Core.Internal.Testing {

  using System;
  using System.Collections.Generic;
	using System.Reflection;
  using System.Threading.Tasks;

  using ION.Core.Devices;
	using ION.Core.Internal;
  using ION.Core.Measure;
  using ION.Core.Sensors;
	using ION.Core.StateMachine;
  using ION.Core.Util;

  public class Av760ProductionTest {

		private static readonly Unit DEGREE = Units.Angle.DEGREE;
		private const float ERROR_TOLERANCE = 0.025f;

    public delegate void OnTestEvent(Av760ProductionTest test);

    /// <summary>
    /// The event handler that is notified when the test yields a target point that was met by the test rig.
    /// </summary>
		public event OnTestEvent onTestEvent;

    /// <summary>
    /// </summary>
    public List<GaugeDeviceSensor> sensors {
      get {
        return new List<GaugeDeviceSensor>(__sensors);
      }
      private set {
        __sensors = new List<GaugeDeviceSensor>(value);
      }
    } List<GaugeDeviceSensor> __sensors = new List<GaugeDeviceSensor>();
    /// <summary>
    /// The test rig device.
    /// </summary>
    public BluefruitDevice bluefruit { get; private set; }
    /// <summary>
    /// The procedure that is used for the test.
    /// </summary>
    public TestProcedure test { get; private set; }
		/// <summary>
		/// Whether or not the test is currently running.
		/// </summary>
		public bool isTesting { get; internal set; }
		/// <summary>
		/// Whether or not the test is done.
		/// </summary>
		/// <value>The is done.</value>
		public bool isDone { get; internal set; }
		/// <summary>
		/// The results of the test.s
		/// </summary>
		public Dictionary<GaugeDeviceSensor, int[]> results = new Dictionary<GaugeDeviceSensor, int[]>();
		/// <summary>
		/// The current index within the test.
		/// </summary>
		private int testIndex;

		public StateEngine sm { get; private set; }

    public Av760ProductionTest(TestProcedure procedure, BluefruitDevice bf, List<GaugeDeviceSensor> gaugeSensors) {
      if (ESensorType.Vacuum != procedure.sensorType) {
        throw new Exception("Cannot create Av760ProductionTest with " + procedure.sensorType + " sensor type test");
      }

      this.test = procedure;
			for (int i = 0; i < gaugeSensors.Count; i++) {
				results[gaugeSensors[i]] = new int[test.count];
			}
      bluefruit = bf;
      sensors = gaugeSensors;

			bluefruit.onDeviceEvent += OnBluefruitDeviceEvent;

			sm = new StateEngine.Builder()
			                    .AddTransition(State_Reset,				ERetCode.Ok, State_Initialize)

							            .AddTransition(State_Initialize, 	ERetCode.Ok, 			State_Prepare)
							            .AddTransition(State_Initialize, 	ERetCode.Repeat, 	State_Initialize)
							            .AddTransition(State_Initialize,	ERetCode.Error, 	State_Error)

							            .AddTransition(State_Prepare,			ERetCode.Ok,			State_Test)
							            .AddTransition(State_Prepare,			ERetCode.Repeat,	State_Prepare)
							            .AddTransition(State_Prepare,			ERetCode.Error,		State_Error)

							            .AddTransition(State_Test,				ERetCode.Ok,			State_FinishTest)
							            .AddTransition(State_Test,				ERetCode.Repeat,	State_Test)
							            .AddTransition(State_Test,				ERetCode.Failure,	State_Initialize)
							            .AddTransition(State_Test,				ERetCode.Error,		State_Error)

							            .AddTransition(State_FinishTest,	ERetCode.Ok,			State_Initialize)
							            .AddTransition(State_Error,				ERetCode.Repeat,	State_Error)
							            .Build(State_Initialize);
			sm.onStateChanged += OnStateChanged;

			isTesting = false;
    }

		public void StartTesting() {
			isTesting = true;
			isDone = false;
			sm.Advance();
		}

		/// <summary>
		/// Callend when the device event has a new device event.
		/// </summary>
		/// <returns>The device event.</returns>
		/// <param name="">.</param>
		private void OnBluefruitDeviceEvent(DeviceEvent e) {
			if (isTesting) {
				sm.Advance();
			}
		}

		/// <summary>
		/// Called when the state engine's state changes.
		/// </summary>
		/// <returns>The state changed.</returns>
		/// <param name="engine">Engine.</param>
		/// <param name="prevState">Previous state.</param>
		/// <param name="newState">New state.</param>
		private void OnStateChanged(StateEngine engine, StateFunction prevState, StateFunction newState) {
			Log.D(this, "Engine state change from: " + prevState.GetMethodInfo().Name + " to " + newState.GetMethodInfo().Name);
			this.NotifyTestEvent();
		}

		private bool SendCommand(Scalar targetInputAngle, ESpeed inputSpeed, Scalar targetExhaustAngle, ESpeed exhaustSpeed) {
			var packet = bluefruit.blueProtocol.CreateCommandPacket(targetInputAngle, inputSpeed, targetExhaustAngle, exhaustSpeed);
			return bluefruit.connection.Write(packet);
		}

		/// <summary>
		/// Ensures that the rig is in a valid state before performing a test.
		/// </summary>
		/// <returns>The rig validity.</returns>
		private bool EnsureRigValidity() {
			return bluefruit.isConnected;
		}

		private ERetCode State_Reset(StateEngine engine) {
			// TODO ahodder@appioninc.com: This is blocking with no real display or indication. Baaaaaaaad
			var bp = bluefruit.blueProtocol;
			bluefruit.connection.Write(bp.CreateResetPacket());

			Task.Delay(500).Wait();

			while (bluefruit.isResetting) {
				Task.Delay(10).Wait();
			}

			return ERetCode.Ok;
		}

		/// <summary>
		/// Performs the initialization state for the test. This is done by closing the input stepper and opening the
		/// exhaust stepper.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="engine">Engine.</param>
		private ERetCode State_Initialize(StateEngine engine) {
			if (!EnsureRigValidity()) {
				return ERetCode.Error;
			} else if (bluefruit.inputStepper.atEnd && bluefruit.exhaustStepper.atHome) {
				return ERetCode.Ok;
			} else {
				SendCommand(DEGREE.OfScalar(95), ESpeed.Fast, DEGREE.OfScalar(0), ESpeed.Fast);
				return ERetCode.Repeat;
			}
		}

		/// <summary>
		/// Performs the reseting state for the test. This is done by closing the exhaust stepper.
		/// </summary>
		/// <returns>The reseting.</returns>
		/// <param name="engine">Engine.</param>
		private ERetCode State_Prepare(StateEngine engine) {
			if (!EnsureRigValidity()) {
				return ERetCode.Error;
			} else if (bluefruit.inputStepper.atEnd && bluefruit.exhaustStepper.atEnd) {
				return ERetCode.Ok;
			} else {
				SendCommand(DEGREE.OfScalar(95), ESpeed.Fast, DEGREE.OfScalar(95), ESpeed.Slowest);
				return ERetCode.Repeat;
			}
		}

		// The testing state is where all of the testing happens for the test rig. Note:
		// this step does not actually perform any IO operations with the stepper. It simply
		// detmerines whether or not stepper io is necessary.
		private ERetCode State_Test(StateEngine engine) {
			if (!EnsureRigValidity()) {
				return ERetCode.Error;
			}

			if (testIndex < test.count) {
				var meas = bluefruit.currentVrcMeasurement;
				var target = test[testIndex].target.ConvertTo(Units.Vacuum.MICRON).amount;
				var avg = (bluefruit.lastVrcMeasurement + target) / 2;
				var error = Math.Abs((target - avg) / (float)target);

				if (meas <= target) {
					if (meas < target && error > ERROR_TOLERANCE) {
						Log.D(this, "Failed to hit target: " + target + " with an error of: " + error);
						return ERetCode.Failure;
					} else {
						// We successfully found a point. Save it.
						for (int i = 0; i < sensors.Count; i++) {
							var m = sensors[i].measurement.ConvertTo(Units.Vacuum.MICRON).amount;
							Log.D(this, "Storing value: " + m);
              results[sensors[i]][testIndex] = (int)m;
						}
						NotifyTestEvent();
						testIndex++;
						return ERetCode.Repeat;
					}
				} // The meas is > than target
				SendCommand(DEGREE.OfScalar(0), ESpeed.Turtle, DEGREE.OfScalar(90), ESpeed.Fastest);
				return ERetCode.Repeat;
			} else {
				// We are done testing.
				return ERetCode.Ok;
			}
		}

		/// <summary>
		/// States the finish test.
		/// </summary>
		/// <returns>The finish test.</returns>
		/// <param name="engine">Engine.</param>
		private ERetCode State_FinishTest(StateEngine engine) {
			isTesting = false;
			isDone = true;
			return ERetCode.Ok;
		}

		/// <summary>
		/// States the error.
		/// </summary>
		/// <returns>The error.</returns>
		/// <param name="engine">Engine.</param>
		private ERetCode State_Error(StateEngine engine) {
			return ERetCode.Repeat;
		}

		private void NotifyTestEvent() {
			if (onTestEvent != null) {
				onTestEvent(this);
			}
		}
  }
}

