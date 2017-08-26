using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using System.IO;
using ION.Core.App;
using ION.Core.Devices;
using System.Threading.Tasks;
using Foundation;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Logging {
  public partial class GraphingViewController : BaseIONViewController {
		public ChooseData checkData;
		public ChooseGraphing graphingSection;
		public UIActivityIndicatorView activityLoadingGraphs;
		private IION ion;
		public ObservableCollection<int> selectedSessions;

		public GraphingViewController(IntPtr handle) : base(handle) {

		}

    public override void ViewDidLoad() {
      base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			InitNavigationBar("ic_graph_menu", true);
			if(checkData == null){
        NavigationController.PopViewController(true);
      }
      ion = AppState.context;

      setupGraphing();
		}

    public async void setupGraphing(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
			graphingSection = new ChooseGraphing(containerView, checkData);

			containerView.AddSubview(graphingSection.graphingType);

			if (activityLoadingGraphs != null) {
				activityLoadingGraphs = null;
			}

			activityLoadingGraphs = new UIActivityIndicatorView(new CGRect(0, 0, containerView.Bounds.Width, containerView.Bounds.Height));
			activityLoadingGraphs.Alpha = .4f;
			activityLoadingGraphs.Layer.CornerRadius = 8;
			activityLoadingGraphs.BackgroundColor = UIColor.DarkGray;

			containerView.AddSubview(activityLoadingGraphs);
			containerView.BringSubviewToFront(activityLoadingGraphs);

			activityLoadingGraphs.StartAnimating();

			ChosenDates.includeList = new List<string>();
			ChosenDates.allTimes = new Dictionary<string, int>();
			ChosenDates.allIndexes = new Dictionary<int, string>();
			var paramList = new List<string>();

			foreach (var num in checkData.selectedSessions) {
				paramList.Add('"' + num.ToString() + '"');
			}

			var graphResult = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd, frn_JID FROM SessionRow WHERE SID in (" + string.Join(",", paramList.ToArray()) + ") ORDER BY SID");

			var tempResults = new List<deviceReadings>();
			var holderList = new List<string>();
			var sessionBreaks = new string[graphResult.Count];

			ChosenDates.breakPoints = graphResult.Count;
			for (int s = 0; s < graphResult.Count; s++) {
				//Console.WriteLine("Going through session " + graphResult[s].SID);
				var deviceCount = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT DISTINCT serialNumber, sensorIndex FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY serialNumber ASC", graphResult[s].SID);
				//Console.WriteLine("Grabbed " + deviceCount.Count + " device results");

				for (int m = 0; m < deviceCount.Count; m++) {
					var activeDevice = new deviceReadings();
					activeDevice.times = new List<DateTime>();
					activeDevice.readings = new List<double>();
					activeDevice.SID = graphResult[s].SID;
					activeDevice.frnJID = graphResult[s].frn_JID;
					activeDevice.serialNumber = deviceCount[m].serialNumber;
					activeDevice.sensorIndex = deviceCount[m].sensorIndex;

					var measurementCount = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT * FROM SensorMeasurementRow WHERE serialNumber = ? AND frn_SID = ? AND sensorIndex = ? ORDER BY MID ASC", activeDevice.serialNumber, graphResult[s].SID, deviceCount[m].sensorIndex);
					//Console.WriteLine("Using sensor index: " + measurementCount[0].sensorIndex + " for device: " + measurementCount[0].serialNumber);
					var df = ion.deviceManager.deviceFactory;
					var tempD = df.GetDeviceDefinition(SerialNumberExtensions.ParseSerialNumber(activeDevice.serialNumber)) as GaugeDeviceDefinition;
					activeDevice.type = tempD.sensorDefinitions[measurementCount[0].sensorIndex].sensorType.ToString();

					foreach (var meas in measurementCount) {
						activeDevice.times.Add(meas.recordedDate.ToLocalTime());
						if (!holderList.Contains(meas.recordedDate.ToLocalTime().ToString())) {
							holderList.Add(meas.recordedDate.ToLocalTime().ToString());
						}
						var measurement = Convert.ToDouble(meas.measurement);
						activeDevice.readings.Add(measurement);
					}

					tempResults.Add(activeDevice);
					//Console.WriteLine("Added package for serial " + activeDevice.serialNumber);
				}
				if (holderList.Count > 0) {
					sessionBreaks[s] = holderList[holderList.Count - 1];
					//Console.WriteLine("Added a session break @ " + holderList[holderList.Count - 1]);
				}
			}
			holderList.Sort((x, y) => DateTime.Parse(x).CompareTo(DateTime.Parse(y)));

			ChosenDates.extraPlots = holderList.Count;

			ChosenDates.extraPlots = (ChosenDates.extraPlots + (int)(ChosenDates.extraPlots * .05)) - holderList.Count;

			if (ChosenDates.extraPlots == 0) {
				ChosenDates.extraPlots = 1;
			}

			var indexes = 0;
			var breakPoint = 0;

			foreach (var time in holderList) {
				//Console.WriteLine("Adding index " +indexes);
				ChosenDates.allTimes.Add(time, indexes);
				ChosenDates.allIndexes.Add(indexes, time);
				if (breakPoint < sessionBreaks.Length && sessionBreaks[breakPoint].Equals(time)) {
					//Console.WriteLine("hit a breakpoint");
					indexes = indexes + ChosenDates.extraPlots;
					breakPoint = breakPoint + 1;

				} else {
					indexes = indexes + 1;
				}
			}

				graphingSection.graphingView = new GraphingView(graphingSection.graphingType, this.root, tempResults, selectedSessions);
				graphingSection.legendView = new LegendView(graphingSection.graphingType, tempResults, this);
				graphingSection.graphingType.AddSubview(graphingSection.graphingView.gView);
			  graphingSection.graphingType.AddSubview(graphingSection.legendView.lView);
			  graphingSection.graphingType.BringSubviewToFront(graphingSection.legendView.lView);

				if (holderList.Count > 0) {
					containerView.AddSubview(graphingSection.graphingView.resetButton);
					containerView.AddSubview(graphingSection.graphingView.exportGraph);
				}
			  activityLoadingGraphs.StopAnimating();
			  graphingSection.graphingView.graphTab.TouchUpInside += showGraphTable;
			  graphingSection.graphingView.numericTab.TouchUpInside += showNumericTable;
        graphingSection.graphingView.menuButton.TouchUpInside += showLegendView;

			  graphingSection.legendView.pressureUnits.TouchUpInside += changePressureDefault;
			  graphingSection.legendView.temperatureUnits.TouchUpInside += changeTemperatureDefault;
			  graphingSection.legendView.vacuumUnits.TouchUpInside += changeVacuumDefault;
		}

    public void showGraphTable(object sender, EventArgs e){
			graphingSection.graphingView.graphTab.BackgroundColor = UIColor.White;
      graphingSection.graphingView.graphTabHighlight.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			graphingSection.graphingView.numericTab.BackgroundColor = UIColor.LightGray;
			graphingSection.graphingView.numericTabHighlight.BackgroundColor = UIColor.Black;

			graphingSection.graphingView.extraInfoTable.Hidden = true;
			graphingSection.graphingView.graphTable.Hidden = false;
    }

		public void showNumericTable(object sender, EventArgs e) {
			graphingSection.graphingView.graphTab.BackgroundColor = UIColor.LightGray;
			graphingSection.graphingView.graphTabHighlight.BackgroundColor = UIColor.Black;

			graphingSection.graphingView.numericTab.BackgroundColor = UIColor.White;
			graphingSection.graphingView.numericTabHighlight.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			graphingSection.graphingView.graphTable.Hidden = true;
			graphingSection.graphingView.extraInfoTable.Hidden = false;
		}

		public void showLegendView(object sender, EventArgs e) {
      Console.WriteLine("Opening the legend view");
      graphingSection.legendView.lView.Hidden = false;
		}

		public void changePressureDefault(object sender, EventArgs e) {
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}

			var unitList = ION.Core.Sensors.SensorUtils.DEFAULT_PRESSURE_UNITS;
			var dialog = UIAlertController.Create(Util.Strings.Analyzer.CHOOSEUNIT, null, UIAlertControllerStyle.Alert);
			foreach (var unit in unitList) {
				dialog.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (action) => {
					NSUserDefaults.StandardUserDefaults.SetString(ION.Core.Sensors.UnitLookup.GetCode(unit).ToString(), "settings_units_default_pressure");
					graphingSection.legendView.pressureUnits.SetTitle(unit.ToString(), UIControlState.Normal);
					graphingSection.graphingView.graphTable.ReloadData();
					graphingSection.graphingView.extraInfoTable.ReloadData();
				}));
			}
			dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));
			vc.PresentViewController(dialog, true, null);
		}

		public void changeVacuumDefault(object sender, EventArgs e) {
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}

			var unitList = ION.Core.Sensors.SensorUtils.DEFAULT_VACUUM_UNITS;
			var dialog = UIAlertController.Create(Util.Strings.Analyzer.CHOOSEUNIT, null, UIAlertControllerStyle.Alert);
			foreach (var unit in unitList) {
				dialog.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (action) => {
					NSUserDefaults.StandardUserDefaults.SetString(ION.Core.Sensors.UnitLookup.GetCode(unit).ToString(), "settings_units_default_vacuum");
					graphingSection.legendView.vacuumUnits.SetTitle(unit.ToString(), UIControlState.Normal);
					graphingSection.graphingView.graphTable.ReloadData();
					graphingSection.graphingView.extraInfoTable.ReloadData();
				}));
			}
			dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));
			vc.PresentViewController(dialog, true, null);
		}

		public void changeTemperatureDefault(object sender, EventArgs e) {
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}

			var unitList = ION.Core.Sensors.SensorUtils.DEFAULT_TEMPERATURE_UNITS;
			var dialog = UIAlertController.Create(Util.Strings.Analyzer.CHOOSEUNIT, null, UIAlertControllerStyle.Alert);
			foreach (var unit in unitList) {
				dialog.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (action) => {
					NSUserDefaults.StandardUserDefaults.SetString(ION.Core.Sensors.UnitLookup.GetCode(unit).ToString(), "settings_units_default_temperature");
					graphingSection.legendView.temperatureUnits.SetTitle(unit.ToString(), UIControlState.Normal);
					graphingSection.graphingView.graphTable.ReloadData();
					graphingSection.graphingView.extraInfoTable.ReloadData();
				}));
			}
			dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));
			vc.PresentViewController(dialog, true, null);
		}

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}

