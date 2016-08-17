namespace ION.IOS.ViewController.GaugeTesting {

	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	using CoreGraphics;
	using Foundation;
	using UIKit;

	using DSoft.Themes.Grid;
	using DSoft.Datatypes.Enums;
	using DSoft.Datatypes.Grid.Data;
	using DSoft.Datatypes.Types;
	using DSoft.Datatypes.Formatters;
	using DSoft.UI.Grid;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Internal;
	using ION.Core.Internal.Testing;
	using ION.Core.IO;
	using ION.Core.Measure;
	using ION.Core.Sensors;
	using ION.Core.Util;

	using ION.IOS.UI;

	public partial class InternalGaugeTestingViewController : BaseIONViewController {

		//    private TestProcedure test;
		/// <summary>
		/// The table of data that we are displaying.
		/// </summary>
		private TableDataSet table;

		private BluefruitDevice bluefruit;
		private TestProcedure testProcedure;
		private Av760ProductionTest test;

		/// <summary>
		/// The list of the sensors that are held in the table.
		/// </summary>
		private List<GaugeDeviceSensor> sensors = new List<GaugeDeviceSensor>();

		public InternalGaugeTestingViewController (IntPtr handle) : base (handle) {
		}

		/// <summary>
		/// Updates the content of the table.
		/// </summary>
		/// <returns>The content.</returns>
		private void UpdateTableContent() {
			if (test != null) {
				for (int col = 1; col <= test.test.count; col++) {
					for (int row = 1; row <= test.sensors.Count; row++) {
						var c = col - 1;
						var r = row - 1;
						var tp = test.test.targetPoints[c];
						var val = test.results[test.sensors[r]][c];
						if (val == 0) {
							table.SetValue(r, "" + tp.target.amount, "N/A");
						} else {
							table.SetValue(r, "" + tp.target.amount, val.ToString("#"));
						} 
					}
				}

				gridView.ReloadData();
			}
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad() {
			base.ViewDidLoad();

			try {
				var dm = AppState.context.deviceManager;
				var bf = dm.GetAllDevicesOfType(EDeviceType.InternalInterface);
				var deviceSensors = dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum);

				foreach (var sensor in dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum)) {
					if (sensor.device.isConnected) {
						sensors.Add(sensor);
					}
				}

				testProcedure = XmlTestProcedureParser.DoParse(EmbeddedResource.Load("Av760TestingProcedure.xml"));

				bluefruit = bf[0] as BluefruitDevice;

				Title = "Gauge Test Bench";

				var tp = testProcedure.targetPoints;
				var ch = new string[tp.Count + 1];
				ch[0] = "Serial";
				for (int i = 0; i < tp.Count; i++) {
					ch[i + 1] = tp[i].target.amount + "";
				}

				var gds = dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum);
				var serials = new string[gds.Count];

				for (int i = 0; i < gds.Count; i++) {
					serials[i] = gds[i].device.serialNumber.ToString();
				}

				var content = new string[ch.Length, serials.Length];

				for (int r = 0; r < serials.Length; r++) {
					content[0, r] = serials[r];
				}

				table = new TableDataSet("Test Table", ch, serials, content);
			} catch (Exception e) {
				Log.E(this, "Whoops", e);
				Toast.New(View, "Failed to load InternalTestingViewController. Please tell Kyle to stop breaking things");
			}

			gridView.DataSource = table;
			textViewStatus.Text = "Test not started. Press the 'Test' button.";
		}

		public override void ViewDidAppear(bool animated) {
			base.ViewDidAppear(animated);


			ShowStartTestingViews();
		}

		public override void ViewDidDisappear(bool animated) {
			base.ViewDidDisappear(animated);
		}

		public override void ViewDidUnload() {
			base.ViewDidUnload();
		}

		private bool Validate() {
			if (bluefruit == null) {
				Toast.New(View, "Cannot do testing: Please connect to the test rig.");
				return false;
			} else {
				return true;
			}
		}

		/// <summary>
		/// Starts the test procedure.
		/// </summary>
		/// <returns>The test.</returns>
		private void StartTest() {
			if (!Validate()) {
				return;
			}

			if (bluefruit != null) {
				bluefruit.onDeviceEvent += OnDeviceEvent;
			}

			test = new Av760ProductionTest(testProcedure, bluefruit, sensors);
			test.onTestEvent += OnTestEvent;
			test.StartTesting();

			UpdateTableContent();
		}

		private void StopTest() {
			if (test != null) {
				if (bluefruit != null) {
					bluefruit.onDeviceEvent -= OnDeviceEvent;
				}
			}
			test = null;
		}

		private void ClearActionSpace() {
			foreach (var view in viewActionSpace.Subviews) {
				view.RemoveFromSuperview();
			}
		}

		private void ShowStartTestingViews() {
			ClearActionSpace();
			var start = new UIButton(new CGRect(0, 0, viewActionSpace.Frame.Width, viewActionSpace.Frame.Height));
			start.BackgroundColor = new UIColor(0, 1, 0, 1);
			start.SetTitleColor(new UIColor(0, 0, 0, 1), UIControlState.Normal);
			start.SetTitle("Start Test", UIControlState.Normal);
			viewActionSpace.AddSubview(start);
			start.TouchUpInside += (sender, e) => {
				StartTest();
				ShowStopTestingViews();
			};
		}

		private void ShowStopTestingViews() {
			ClearActionSpace();
			var start = new UIButton(new CGRect(0, 0, viewActionSpace.Frame.Width, viewActionSpace.Frame.Height));
			start.BackgroundColor = new UIColor(1, 0, 0, 1);
			start.SetTitleColor(new UIColor(0, 0, 0, 1), UIControlState.Normal);
			start.SetTitle("Cancel Test", UIControlState.Normal);
			viewActionSpace.AddSubview(start);
			start.TouchUpInside += (sender, e) => {
				StopTest();
				ShowStartTestingViews();
			};
		}

		private void ShowEndTestingViews() {
			ClearActionSpace();
			var f = viewActionSpace.Frame;
			var restart = new UIButton(new CGRect(0, 0, f.Width / 2, f.Height));
			restart.SetTitle("Restart Test", UIControlState.Normal);
			restart.SetTitleColor(new UIColor(0, 0, 0, 1), UIControlState.Normal);
			restart.BackgroundColor = new UIColor(0, 1, 0, 1);
			restart.TouchUpInside += (sender, e) => {
				ShowStartTestingViews();
			};
			viewActionSpace.AddSubview(restart);

			var export = new UIButton(new CGRect(f.Width / 2, 0, f.Width / 2, f.Height));
			export.SetTitle("Export Results", UIControlState.Normal);
			export.SetTitleColor(new UIColor(0, 0, 0, 1), UIControlState.Normal);
			export.BackgroundColor = new UIColor(0.15f, .15f, 1, 1);
			export.TouchUpInside += (sender, e) => {
				var vc = InflateViewController<SaveInternalTestViewController>(BaseIONViewController.VC_SAVE_INTERNAL_TEST);
				var ci = new CalibrationInstrument();
				ci.accuracy = "1.0%";
				ci.lastCalibrationDate = new DateTime(2016, 04, 16);
				ci.model = "VRC 902";
				ci.serialNumber = "130717150";
				vc.testResults = new TestResults(test.test, test.results, ci, "A random tester");
				NavigationController.PushViewController(vc, true);
			};
			viewActionSpace.AddSubview(export);
		}

		private void OnTestEvent(Av760ProductionTest test) {
			AppState.context.PostToMain(() => {
				if (test.isDone) {
					ShowEndTestingViews();
				} else if (test.isTesting) {
					UpdateTableContent();
				}
			});
		}

		private void OnDeviceEvent(DeviceEvent e) {
			if (test == null) {
				return;
			}
			AppState.context.PostToMain(() => {
				if (test == null) {
					return;
				}
				var sb = new StringBuilder();
				sb.Append(string.Format("{0,15}:  {1}\n", "VRC", test.bluefruit.currentVrcMeasurement));
				sb.Append(string.Format("{0,15}:  {1}\n", "State", test.sm.currentState.Method.Name));

				sb.Append(string.Format("{0,15}:  {1}\n", "∠ InputTarget", test.bluefruit.inputStepper.targetAngle.ConvertTo(Units.Angle.DEGREE)));
				sb.Append(string.Format("{0,15}:  {1}\n", "∠ InputCurrent", test.bluefruit.inputStepper.currentAngle.ConvertTo(Units.Angle.DEGREE)));
				sb.Append(string.Format("{0,15}:  {1}\n", "Input RPS", test.bluefruit.inputStepper.rps));
				sb.Append(string.Format("{0,15}:  {1}\n", "Input Home", test.bluefruit.inputStepper.atHome));
				sb.Append(string.Format("{0,15}:  {1}\n", "Input End", test.bluefruit.inputStepper.atEnd));



				sb.Append(string.Format("{0,15}:  {1}\n", "∠ ExhaustTarget", test.bluefruit.exhaustStepper.targetAngle.ConvertTo(Units.Angle.DEGREE)));
				sb.Append(string.Format("{0,15}:  {1}\n", "∠ ExhaustTarget", test.bluefruit.exhaustStepper.currentAngle.ConvertTo(Units.Angle.DEGREE)));
				sb.Append(string.Format("{0,15}:  {1}\n", "Exhaust RPS", test.bluefruit.exhaustStepper.rps));
				sb.Append(string.Format("{0,15}:  {1}\n", "Exhaust Home", test.bluefruit.exhaustStepper.atHome));
				sb.Append(string.Format("{0,15}:  {1}\n", "Exhaust End", test.bluefruit.exhaustStepper.atEnd));

				if (textViewStatus != null) {
					textViewStatus.Text = sb.ToString();
				}
			});
		}
	}
}
