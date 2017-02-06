namespace TestBench.Droid {
  
  using System.Collections.Generic;
	using System.IO;

	using ION.Core.Devices;
	using ION.Core.Sensors;

  public delegate void OnTestEvent(ITest test, TestEvent te);

  public class TestEvent {
    public EType type;
		public string message;


    public TestEvent(EType type) {
      this.type = type;
    }

		public TestEvent(EType type, string message) {
			this.type = type;
			this.message = message;
		}

		public override string ToString() {
			return string.Format("[TestEvent{Type:" + type + " Message: " + message + "}]");
		}

    public enum EType {
			/// <summary>
			/// The test was started.
			/// </summary>
			TestStarted,
			/// <summary>
			/// The test was requested to stop.
			/// </summary>
			TestStopped,
			/// <summary>
			/// The test was cancelled.
			/// </summary>
			TestCancelled,
			/// <summary>
			/// The test's rig received hit a target point.
			/// </summary>
      NewTestData,
			/// <summary>
			/// The Test's rig posted new state.
			/// </summary>
			NewTestState,
			/// <summary>
			/// The test is now complete. Similar to cancelled in that the test is now not longer running.
			/// </summary>
			TestComplete,
    }
  }

  public interface ITest {

    /// <summary>
    /// The handler that is used to notify the world that it has changed.
    /// </summary>
    event OnTestEvent onTestEvent;

		/// <summary>
		/// The device model that the test was designed for.
		/// </summary>
		/// <value>The device model.</value>
		EDeviceModel deviceModel { get; }
    /// <summary>
    /// The sensor type within the device that the test is qualifying.
    /// </summary>
    /// <value>The parameters.</value>
    ESensorType sensorType { get; }
    /// <summary>
    /// The parameters of the test.
    /// </summary>
    TestParameters parameters { get; }
    /// <summary>
    /// The current target point that is being worked on in the test.
    /// </summary>
    /// <value>The index of the current target point.</value>
    int currentTargetPointIndex { get; set; }
    /// <summary>
    /// The controller that will control and manager communications with the test rig.
    /// </summary>
    /// <value>The rig.</value>
    IRig rig { get;}
    /// <summary>
    /// The list of gauges that are known by the application.
    /// </summary>
    Dictionary<ISerialNumber, IConnection> connections { get; }
    /// <summary>
    /// The lookup table of the results of the test.
    /// </summary>
    /// <value>The test results.</value>
    TestResults testResults { get; }
		/// <summary>
		/// Whether or not the test is currently running.
		/// </summary>
		/// <value><c>true</c> if is running; otherwise, <c>false</c>.</value>
		bool isTesting { get; }

		string testName { get; }

    void StartTest();
    void StopTest();

    string GetState();

		bool Export(Stream stream);
  }
}

