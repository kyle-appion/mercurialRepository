using System;
using Foundation;
using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using ION.Core.App;
using ION.Core.Devices;
using ION.Core.Devices.Sorters;
using ION.IOS.ViewController.Workbench;
using ION.IOS.ViewController.Analyzer;
using ION.IOS.Util;
using ION.Core.Connections;
using ION.Core.Devices.Protocols;

namespace ION.IOS.ViewController.DeviceGrid {
  public partial class DeviceGridViewController : BaseIONViewController {
		/// <summary>
		/// The delegate that is used to pass a sensor back from the device manager.
		/// </summary>
		public delegate void OnSensorReturn(GaugeDeviceSensor sensor);

		/// <summary>
		/// The action that will be fired when the user selects a sensor for returning 
		/// within the device manager.
		/// </summary>
		public OnSensorReturn onSensorReturnDelegate { get; set; }

    UITapGestureRecognizer gridTapped;
		SensorStatusPopup selectedSensor;
    IION ion;
    bool updatingGrid = false;
		public int fromAnalyzer = -1;
    public bool fromAnalyzerLH = false;
    public bool fromWorkbench = false;

		private List<GaugeDeviceSensor> connectedSensors = new List<GaugeDeviceSensor>();
		private List<GaugeDeviceSensor> disconnectedSensors = new List<GaugeDeviceSensor>();
		private SortedSet<GaugeDevice> knownGauges = new SortedSet<GaugeDevice>();

		public DeviceGridViewController(IntPtr handle) : base(handle) {

			ion = AppState.context;   
		}

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("CarbonBackground"));

      if (fromAnalyzer == -1 && !fromWorkbench && !fromAnalyzerLH) {
        InitNavigationBar("ic_job_settings", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      } else {
				InitNavigationBar("ic_nav_device_manager", true);
			}
			NavigationItem.Title = Strings.Device.Grid.SELF;
			AutomaticallyAdjustsScrollViewInsets = false;

			foreach (var device in ion.deviceManager.devices) {
				var holder = device as GaugeDevice;
				if (holder.connection.connectionState == Core.Connections.EConnectionState.Connected || holder.connection.connectionState == Core.Connections.EConnectionState.Broadcasting) {
					foreach (var sensor in holder.sensors) {
						connectedSensors.Add(sensor);
					}
				} else {
          if (holder.isNearby) {
						foreach (var sensor in holder.sensors) {
              if (!connectedSensors.Contains(sensor)) {
                connectedSensors.Add(sensor);
              }
						}
          } else {
            foreach (var sensor in holder.sensors) {
              disconnectedSensors.Add(sensor);
            }
          }
				}
			}

      foreach (var device in ion.deviceManager.devices){
				var holder = device as GaugeDevice;
        if(holder.isNearby == true){
					foreach (var sensor in holder.sensors) {
            if(!connectedSensors.Contains(sensor)){
							connectedSensors.Add(sensor);
						}
					}
        } else {
					foreach (var sensor in holder.sensors) {
						if (!disconnectedSensors.Contains(sensor)) {
							disconnectedSensors.Add(sensor);
						}
					}
        }
      }

			connectedSensors.Sort(new GeneralSensorSorter());
			disconnectedSensors.Sort(new GeneralSensorSorter());

      spaceSensors();
      setupGridView();
    }

    public async void setupGridView(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      selectedSensor = new SensorStatusPopup(View);
      selectedSensor.gridVC = this;
			selectedSensor.analyzerSlot = fromAnalyzer;
      selectedSensor.fromWorkbench = fromWorkbench;

      View.AddSubview(selectedSensor.popupView);

			gridTapped = new UITapGestureRecognizer(() => {
					selectedSensor.popupView.Hidden = true;
          selectedSensor.shouldOpen = false;
				  selectedSensor.sensor.onSensorEvent -= selectedSensor.updateSensor;
			});

      gridTapped.CancelsTouchesInView = false;
      gridTapped.ShouldReceiveTouch += (recognizer, touch) => !(selectedSensor.popupView.Hidden);

      gridView.AddGestureRecognizer(gridTapped);
			gridView.RegisterClassForCell(typeof(GridCellConnected), GridCellConnected.CellID);
			gridView.RegisterClassForCell(typeof(GridCellDisconnected), GridCellDisconnected.CellID);
			gridView.RegisterClassForSupplementaryView(typeof(Header), UICollectionElementKindSection.Header, "sectionHeader");

			gridView.DataSource = new DeviceGridSource(connectedSensors,disconnectedSensors);
      gridView.Delegate = new DeviceGridFlowDelegate(selectedSensor);
			gridView.ReloadData();
      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
    }

		private void OnDeviceManagerEvent(DeviceManagerEvent dme) {
      if (dme != null && dme.type == DeviceManagerEvent.EType.ScanStarted)
      {
        if (updatingGrid == false)
        {
          updatingGrid = true;
          connectedSensors.Clear();
          disconnectedSensors.Clear();

          foreach (var device in ion.deviceManager.devices)
          {
            var holder = device as GaugeDevice;
            if (holder.connection.connectionState == Core.Connections.EConnectionState.Connected || holder.connection.connectionState == Core.Connections.EConnectionState.Broadcasting)
            {
              foreach (var sensor in holder.sensors)
              {
                connectedSensors.Add(sensor);
              }
            }
            else
            {
              if (holder.isNearby == true)
              {
                foreach (var sensor in holder.sensors)
                {
                  if (!connectedSensors.Contains(sensor))
                  {
                    connectedSensors.Add(sensor);
                  }
                }
              }
              else
              {
                foreach (var sensor in holder.sensors)
                {
                  disconnectedSensors.Add(sensor);
                }
              }
            }
          }

          connectedSensors.Sort(new GeneralSensorSorter());
          disconnectedSensors.Sort(new GeneralSensorSorter());
          spaceSensors();
        }
      }
		}

    public void spaceSensors(){
			var conCount = 0;
			var disconCount = 0;

			for (int c = 0; c < connectedSensors.Count;) {
				if (connectedSensors[c].device.sensorCount + conCount <= 4) {
					conCount += connectedSensors[c].device.sensorCount;
					c += connectedSensors[c].device.sensorCount;
				} else {
					while (conCount < 4) {
						connectedSensors.Insert(c++, null);
						conCount++;
					}
				}
				if (conCount >= 4) {
					conCount = 0;
				}
			}

			for (int d = 0; d < disconnectedSensors.Count;) {
				if (disconnectedSensors[d].device.sensorCount + disconCount <= 4) {
					disconCount += disconnectedSensors[d].device.sensorCount;
					d += disconnectedSensors[d].device.sensorCount;
				} else {
					while (disconCount < 4) {
						disconnectedSensors.Insert(d++, null);
						disconCount++;
					}
				}
				if (disconCount >= 4) {
					disconCount = 0;
				}
			}

			gridView.ReloadData();
			updatingGrid = false;
    }

    public override void ViewDidAppear(bool animated) {
      StartScan();
		}

    public override void ViewWillDisappear(bool animated) {
      fromAnalyzer = -1;
			fromWorkbench = false;
      StopScan();
		}

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }   

    public void inflateWorkbench(){
			selectedSensor.sensor.onSensorEvent -= selectedSensor.updateSensor;

			var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController as IONPrimaryScreenController;
      if (fromWorkbench) {
				fromAnalyzer = -1;
				fromWorkbench = false;
        onSensorReturnDelegate(null);
				NavigationController.PopViewController(true);
			} else {
				//TODO This is wholely dependent upon the flyoutnavigation controller library being used for the menu. Any updates will also affect how the grid view navigates for its sensor
				onSensorReturnDelegate(null);
				NavigationController.PopViewController(true);
				vc.navigation.GetType().InvokeMember("NavigationItemSelected", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static, Type.DefaultBinder, vc.navigation, new object[] { 0 });
			}
    }

		public void inflateAnalyzer(GaugeDeviceSensor sensor = null) {
			selectedSensor.sensor.onSensorEvent -= selectedSensor.updateSensor;

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController as IONPrimaryScreenController;
      if (fromAnalyzer == -1 && fromAnalyzerLH == false) {
				//TODO This is wholely dependent upon the flyoutnavigation controller library being used for the menu. Any updates will also affect how the grid view navigates for its sensor
				NavigationController.PopViewController(true);
				vc.navigation.GetType().InvokeMember("NavigationItemSelected", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static, Type.DefaultBinder, vc.navigation, new object[] { 1 });
      } else {
				fromAnalyzer = -1;
				fromAnalyzerLH = false;
				fromWorkbench = false;
        NavigationController.PopViewController(true);
      }
    }

		private void StartScan() {
      ion.deviceManager.connectionManager.StartScan();
		}

		private void StopScan() {
			ion.deviceManager.connectionManager.StopScan();
		}

  }
}

