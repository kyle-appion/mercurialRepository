namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Threading.Tasks;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
	using ION.Core.IO.Exporter;
	using ION.Core.Sensors;

	public class AV760Test : ITest {
		private const float ERROR_TOLERANCE = 0.05f;

		/// <summary>
		/// The header for the csv document.
		/// </summary>
		private const string DOCUMENT_HEADER = "AV760: NIST Tracable Vacuum Gauge Calibration Certificate";

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
		public IRig rig { get { return __rig; } }
		// Used by rig;
		private VacuumRig __rig;
		// Implemented from ITest
		public Dictionary<ISerialNumber, IConnection> connections { get; private set; }
		// Implementes from ITest
		public TestResults testResults { get; private set; }
		public string testName { get { return "AV760 Test"; } }

		public bool isTesting { get; private set; }
		public bool isDone { get; private set; }
		public DateTime startTime { get; private set; }

		private TestEvent lastEvent;

		public AV760Test(TestParameters parameters, VacuumRig rig, List<IConnection> connections) {
			this.parameters = parameters;
			__rig = rig;
			this.connections = new Dictionary<ISerialNumber, IConnection>();
			rig.onConnectionStateChanged += OnRigConnectionStateChanged;

			foreach (var connection in connections) {
				this.connections[connection.serialNumber] = connection;
			}

			InvalidateTestResults();
		}

		private void InvalidateTestResults() {
			testResults = new TestResults();
			testResults.errorBand = parameters.errorBand;
			foreach (var connection in connections.Values) {
				var results = new Dictionary<TestParameters.TargetPoint, TestResults.Result>();
				testResults.resultsTable[connection.serialNumber] = results;
				foreach (var tp in parameters.targetPoints) {
					results[tp] = null;
				}
			}
		}

		// Implemented from ITest
		public void StartTest() {
			isTesting = true;
			isDone = false;
			startTime = DateTime.Now;
			currentTargetPointIndex = 0;

			__rig.onNewVrcReading += HandleNewVrcReading;
			rig.onConnectionStateChanged += OnRigConnectionStateChanged;
			foreach (var c in connections.Values) {
				c.onNewPacket += HandleNewGaugePacket;
				c.onConnectionStateChanged += OnConnectionStateChanged;
			}

			__rig.WriteCommand(VacuumRig.EVrcRigCommand.Test);
			NotifyTestEvent(TestEvent.EType.TestStarted);
			InvalidateTestResults();
			NotifyTestEvent(TestEvent.EType.NewTestData);
		}

		// Implemented from ITest
		public void StopTest() {
			DoFinishTest();
			NotifyTestEvent(TestEvent.EType.TestStopped);
		}

		// Implemented from ITest
		public string GetState() {
			var sb = new StringBuilder();
			sb.Append("<b>VRC: </b>")
			  .Append(__rig.vrcMeasurement.ToString())
			  .Append("<br><b>Angle: </b>")
			  .Append(__rig.rigAngle)
			  .Append("<br><b>CPI: </b>")
			  .Append(currentTargetPointIndex + 1)
			  .Append(" / ")
			  .Append(parameters.targetPoints.Count)
			  .Append("<br><b>Last Event: </b>")
			  .Append(lastEvent?.type)
			  .Append("<br><b>Test Complete: </b>")
			  .Append(isDone)
			  .Append("<br><b>Ellapsed Time: </b>")
			  .Append(DateTime.Now - startTime);
			return sb.ToString();
		}



		/// <summary>
		/// Cancels the test in progress.
		/// </summary>
		private void CancelTest(string message) {
			DoFinishTest();
			NotifyTestEvent(TestEvent.EType.TestCancelled, message);
		}

		/// <summary>
		/// Similar to Cancel test except notifies of a complete event instead of a cancelled event.
		/// </summary>
		private void CompleteTest() {
			DoFinishTest();
			isDone = true;
			testResults.testDuration = DateTime.Now - startTime;
			NotifyTestEvent(TestEvent.EType.TestComplete);
		}

		private void DoFinishTest() {
			rig.onConnectionStateChanged -= OnRigConnectionStateChanged;
			__rig.onNewVrcReading -= HandleNewVrcReading;
			foreach (var c in connections.Values) {
				c.onConnectionStateChanged -= OnConnectionStateChanged;
				c.onNewPacket -= HandleNewGaugePacket;
			}

			currentTargetPointIndex = 0;
			__rig.WriteCommand(VacuumRig.EVrcRigCommand.Reset);
			isDone = false;
			isTesting = false;
		}

		private void HandleNewVrcReading(VacuumRig controller, Scalar lastMeas, Scalar newMeas) {
			Log.D(this, "New Vrc reading: " + lastMeas + ", " + newMeas);
			// We got a new VRC reading from the rig. We need to try and record all of the sensor values if testing or resolve
			// and possible state changes.

/*
			if (!isTesting) {
				// We are not currently testing, and can disregard any new values.
				return;
			}
*/

			var tp = parameters.targetPoints[currentTargetPointIndex];

			if (tp.measurement.amount > newMeas.amount) {
				// We have a new data point for the target point. Record it.
				foreach (var connection in connections.Values) {
					RecordConnectionMeasurement(connection, currentTargetPointIndex);
				}
				// The new measurement from the rig passed the target point. We can now focus on the next target point.
				currentTargetPointIndex++;
				NotifyTestEvent(TestEvent.EType.NewTestData);
			}

			NotifyTestEvent(TestEvent.EType.NewTestState);

			// Check to make sure that the test is not complete.
			if (currentTargetPointIndex >= parameters.targetPoints.Count) {
				// The test is complete.
				CompleteTest();
				return;
			}
		}

		private void HandleNewGaugePacket(IConnection connection, GaugePacket packet) {
/*
			if (!isTesting) {
				// We are not currently testing, and can disregard any new values.
				return;
			}
*/
			RecordConnectionMeasurement(connection, currentTargetPointIndex);
		}

		private void RecordConnectionMeasurement(IConnection connection, int cpi) {
			var tp = parameters.targetPoints[cpi];
			var results = testResults.resultsTable[connection.serialNumber];
			var result = new TestResults.Result(connection.lastPacket[0].reading, tp.measurement);

			lock (testResults) {
				TestResults.Result old;

				if (results.TryGetValue(tp, out old)) {
					if (old != null && old.IsBetterThan(result)) {
						result = old;
					}
				}

				testResults.resultsTable[connection.serialNumber][tp] = result;
			}
		}

		private void OnRigConnectionStateChanged(IRig rig) {
			lock (this) {
				if (isTesting) {
					CancelTest("Rig disconnected resulting in the test be scrubbed");
				}
			}
		}

		private void OnConnectionStateChanged(IConnection connection) {
			Task.Factory.StartNew(() => {
				connection.Connect();
				var start = DateTime.Now;
				while (DateTime.Now - start < TimeSpan.FromSeconds(5)) {
					if (connection.isConnected) {
						break;
					}
				}

				if (!connection.isConnected) {
					lock (this) {
						if (isTesting) {
							CancelTest("One or more connection have disconnected in the middle of the test resulting in the entire test being scrubbed");
						}
					}
				}
			});
		}

		private void NotifyTestEvent(TestEvent.EType type, string message=null) {
			if (onTestEvent != null) {
				onTestEvent(this, new TestEvent(type, message));
			}
		}

		// Implemented from ITest
		public bool Export(Stream stream) {
			var targetPoints = parameters.targetPoints;
			targetPoints.Sort();
			targetPoints.Reverse();
			var resultsTable = testResults.resultsTable;

			var csv = new Csv();

			csv.AddRow(new Csv.Row().AddCol(DOCUMENT_HEADER));
			csv.AddRow(new Csv.Row().AddCol("Test Time:").AddCol(testResults.testDuration.ToString()));

			var header = new Csv.Row();
			csv.AddRow(header);
			header.AddCol("Part Number")
			      .AddCol("Serial Number")
			      .AddCol("Test Date")
			      .AddCol("Instrument Model")
			      .AddCol("Instrument Serial Number")
			      .AddCol("Accuracy")
			      .AddCol("Calibration Date")
			      .AddCol("Certified By")
			      .AddCol("Temperature ºC")
			      .AddCol("%RH")
			      .AddCol("Error Band");

			foreach (var tp in targetPoints) {
				header.AddCol("Calibration Standard")
				      .AddCol("Appion Device")
				      .AddCol("Units")
				      .AddCol("Min (-" + testResults.errorBand.ToString("P1") + ")")
				      .AddCol("Max (+" + testResults.errorBand.ToString("P1") + ")");
			}

			header.AddCol("Failures")
			      .AddCol("Error(avg)")
			      .AddCol("Error(max)");


			foreach (var sn in resultsTable.Keys) {
				var results = resultsTable[sn];

				var row = new Csv.Row();
				csv.AddRow(row);

				row.AddCol(sn.deviceModel.GetUnlocalizedPartNumber())
				   .AddCol(sn.ToString())
				   .AddCol(DateTime.Now.ToShortDateString())
				   .AddCol(testResults.instrumentModel)
				   .AddCol(testResults.instrumentSerialNumber)
				   .AddCol(testResults.instrumentAccuracy.ToString("P1"))
				   .AddCol(testResults.instrumentCalibrationDate.ToShortDateString())
				   .AddCol(testResults.certifiedBy)
				   .AddCol(testResults.temperature.amount.ToString("N1"))
				   .AddCol(testResults.humidity.amount.ToString("N1"))
				   .AddCol(testResults.errorBand.ToString("P1"));

				double avg = 0, max = 0;
				int failures = 0;

				foreach (var tp in targetPoints) {
					var eb = testResults.errorBand;
					var result = results[tp];
					if (result == null) {
						row.AddCol("N/A")
						   .AddCol("N/A")
						   .AddCol("N/A")
						   .AddCol("N/A")
						   .AddCol("N/A");
					} else {
						var rmeas = result.result;
						row.AddCol(tp.measurement.amount.ToString("#"))
						   .AddCol(rmeas.amount.ToString("#"))
						   .AddCol(tp.measurement.unit.ToString())
						   .AddCol((tp.measurement.amount - (tp.measurement.amount * eb)).ToString("#"))
						   .AddCol((tp.measurement.amount + (tp.measurement.amount * eb)).ToString("#"));

						TestParameters.Grade grade;
						if (parameters.FindGradeForMeasurement(tp, rmeas, out grade)) {
							if (!grade.passable) {
								failures++;
							}
						}

						avg += result.error;

						if (result.error > max) {
							max = result.error;
						}
					}
				}

				row.AddCol(failures.ToString())
				   .AddCol((avg / targetPoints.Count).ToString("P1"))
				   .AddCol(max.ToString("P1"));
			}

			var ret = csv.Export(stream);

			return ret;
		}
	}
}
