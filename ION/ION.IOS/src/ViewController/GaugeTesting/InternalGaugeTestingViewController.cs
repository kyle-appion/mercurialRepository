namespace ION.IOS.ViewController.GaugeTesting {

	using System;
	using System.Collections.Generic;
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
	using ION.Core.Sensors;
	using ION.Core.Util;

	public partial class InternalGaugeTestingViewController : BaseIONViewController {

		//    private TestProcedure test;
		/// <summary>
		/// The table of data that we are displaying.
		/// </summary>
		private TableDataSet table;

		private Av760ProductionTest test;
		private Thread thread;

		/// <summary>
		/// The list of the sensors that are held in the table.
		/// </summary>
		private List<GaugeDeviceSensor> sensors = new List<GaugeDeviceSensor>();

		public InternalGaugeTestingViewController (IntPtr handle) : base (handle) {
			var dm = AppState.context.deviceManager;
			var bf = dm.GetAllDevicesOfType(EDeviceType.InternalInterface);
			var deviceSensors = dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum);

			foreach (var sensor in dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum)) {
				if (sensor.device.isConnected) {
					sensors.Add(sensor);
				}
			}

			if (bf == null) {
				throw new Exception("You need a valid bluefruit connected to visit this view controller.");
			}

			test = new Av760ProductionTest(XmlTestProcedureParser.DoParse(EmbeddedResource.Load("Av760TestingProcedure.xml")), bf[0] as BluefruitDevice, sensors);
			test.onTargetPointMet += OnTargetPointMet;
			var scanButton = new UIButton(new CGRect(0, 0, 45, 30));
			scanButton.SetTitle("Test", UIControlState.Normal);
			scanButton.TouchUpInside += Do;

			Title = "Gauge Test Bench";
			var b = new UIBarButtonItem(scanButton);
			NavigationItem.RightBarButtonItem = b;

			var tp = test.procedure.targetPoints;
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

			for (int c = 1; c < ch.Length; c++) {
				for (int r = 0; r < serials.Length; r++) {
					content[c,r] = "[" + c + "," + r + "]";
				}
			}

			table = new TableDataSet("Test Table", ch, serials, content);
		}

		/// <summary>
		/// Called when the test procedure meets a new test point.
		/// </summary>
		private void OnTargetPointMet(TestProcedure.TargetPoint tp) {
			for (int i = 0; i < sensors.Count; i++) {
				var sensor = sensors[i];
				table.SetValue(i, tp.target.amount + "", sensor.measurement.amount + "");
			}
		}

		private void Do(object sender, EventArgs e) {
			var view = new LoadingOverlay(View.Bounds, "Testing");
			View.Add(view);
			Task.Factory.StartNew(test.Run)
			    .ContinueWith((t) => {
				AppState.context.PostToMain(() => {
					view.Hide();
					Log.D(this, "Hiding overlay");
				});
			});
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			gridView.DataSource = table;
		}
	}

	public class LoadingOverlay : UIView {
		private UIActivityIndicatorView activitySpinner;
		private UILabel loadingLabel;

		public LoadingOverlay(CGRect frame, string message) : base(frame) {
			// configurable bits
			BackgroundColor = UIColor.Black;
			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.All;

			nfloat labelHeight = 22;
			nfloat labelWidth = Frame.Width - 20;

			// derive the center x and y
			nfloat centerX = Frame.Width / 2;
			nfloat centerY = Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new CGRect (
				centerX - (activitySpinner.Frame.Width / 2),
				centerY - activitySpinner.Frame.Height - 20,
				activitySpinner.Frame.Width,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();

			// create and configure the "Loading Data" label
			loadingLabel = new UILabel(new CGRect (
				centerX - (labelWidth / 2),
				centerY + 20 ,
				labelWidth ,
				labelHeight
			));
			loadingLabel.BackgroundColor = UIColor.Clear;
			loadingLabel.TextColor = UIColor.White;
			loadingLabel.Text = message;
			loadingLabel.TextAlignment = UITextAlignment.Center;
			loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
			AddSubview (loadingLabel);
		}

		/// <summary>
		/// Hide this instance.
		/// </summary>
		public void Hide() {
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); }
			);
		}
	}
}
