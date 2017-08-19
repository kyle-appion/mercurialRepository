using System;
using Foundation;
using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using ION.Core.App;
using ION.Core.Devices;

namespace ION.IOS.ViewController.DeviceGrid {
  public partial class DeviceGridViewController : BaseIONViewController {
    List<string> collectionItems;
    UITapGestureRecognizer gridTapped;
		SensorStatusPopup selectedSensor;
    IION ion;
    bool updatingGrid = false;

		private List<GaugeDeviceSensor> connectedSensors = new List<GaugeDeviceSensor>();
		private List<GaugeDeviceSensor> disconnectedSensors = new List<GaugeDeviceSensor>();

		public DeviceGridViewController(IntPtr handle) : base(handle) {

			ion = AppState.context;   
		}

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("CarbonBackground"));

      InitNavigationBar("ic_job_settings", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
			AutomaticallyAdjustsScrollViewInsets = false;
			var conCount = 1;
			var disconCount = 1;
      foreach(var device in ion.deviceManager.knownDevices){
        var holder = device as GaugeDevice;
        if(holder.connection.connectionState == Core.Connections.EConnectionState.Connected || holder.connection.connectionState == Core.Connections.EConnectionState.Broadcasting){
					////IF THE NUMBER OF SENSORS BEING ADDED WONT BE GREATER THAN THE ROW SIZE JUST ADD THEM---->
					if (holder.sensorCount + conCount <= 4) {
						foreach (var sensor in holder.sensors) {
							connectedSensors.Add(sensor);
							conCount++;
						}
						////CHECK IF ROW START NEEDS TO BE STARTED OVER
						if (conCount == 4) {
							conCount = 1;
						}
					} else {
						while (conCount < 4) {
							connectedSensors.Add(null);
							conCount++;
						}

						foreach (var sensor in holder.sensors) {
							connectedSensors.Add(sensor);
							conCount++;
						}
						if (conCount == 4) {
							conCount = 1;
						}
					}
        } else {
					if (holder.sensorCount + disconCount <= 4) {
						foreach (var sensor in holder.sensors) {
							disconnectedSensors.Add(sensor);
							disconCount++;
						}
						////CHECK IF ROW START NEEDS TO BE STARTED OVER
						if (disconCount == 4) {
							disconCount = 1;
						}
					} else {
						while (disconCount < 4) {
							disconnectedSensors.Add(null);
							disconCount++;
						}

						foreach (var sensor in holder.sensors) {
							disconnectedSensors.Add(sensor);
							disconCount++;
						}
						if (disconCount == 4) {
							disconCount = 1;
						}
					}
        }
      }
      setupGridView();
    }

    public async void setupGridView(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      selectedSensor = new SensorStatusPopup(View);
      View.AddSubview(selectedSensor.popupView);

			gridTapped = new UITapGestureRecognizer(() => {
					selectedSensor.popupView.Hidden = true;
          selectedSensor.shouldOpen = false;
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
      if (DeviceManagerEvent.EType.DeviceEvent == dme.type && updatingGrid == false) {
				if (DeviceEvent.EType.ConnectionChange == dme.deviceEvent.type) {
          updatingGrid = true;
          Console.WriteLine("Device connection change occurred");   
					connectedSensors.Clear();
					disconnectedSensors.Clear();
					var conCount = 1;
					var disconCount = 1;
					foreach (var device in ion.deviceManager.knownDevices) {
						var holder = device as GaugeDevice;
            if (holder.connection.connectionState == Core.Connections.EConnectionState.Connected || holder.connection.connectionState == Core.Connections.EConnectionState.Broadcasting) {
              ////IF THE NUMBER OF SENSORS BEING ADDED WONT BE GREATER THAN THE ROW SIZE JUST ADD THEM---->
              if (holder.sensorCount + conCount <= 4){
                foreach (var sensor in holder.sensors) {
                  connectedSensors.Add(sensor);
                  conCount++;
                }
                ////CHECK IF ROW START NEEDS TO BE STARTED OVER
                if(conCount == 4){
                  conCount = 1;
                }
              } else {
                while(conCount < 4){
                  connectedSensors.Add(null);
                  conCount++;
                }

								foreach (var sensor in holder.sensors) {
									connectedSensors.Add(sensor);
									conCount++;
								}
								if (conCount == 4) {
									conCount = 1;
								}
              }
						} else {
							if (holder.sensorCount + disconCount <= 4) {
								foreach (var sensor in holder.sensors) {
									disconnectedSensors.Add(sensor);
									disconCount++;
								}
								////CHECK IF ROW START NEEDS TO BE STARTED OVER
								if (disconCount == 4) {
									disconCount = 1;
								}
							} else {
								while (disconCount < 4) {
									disconnectedSensors.Add(null);
									disconCount++;
								}

								foreach (var sensor in holder.sensors) {
									disconnectedSensors.Add(sensor);
									disconCount++;
								}
								if (disconCount == 4) {
									disconCount = 1;
								}
							}
						}
					}

					gridView.ReloadData();
          updatingGrid = false;
				}
			}
		}

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }

}

