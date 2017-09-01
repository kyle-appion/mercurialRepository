using System;
using Foundation;
using CoreGraphics;
using UIKit;
using ION.Core.Devices;
using ION.Core.App;
using ION.IOS.Devices;
using ION.Core.Sensors;
using ION.IOS.ViewController.DeviceGrid;

namespace ION.IOS.ViewController {

  public class SensorStatusPopup {
		/// <summary>
		/// The delegate that is used to pass a sensor back from the device manager.
		/// </summary>
		public delegate void OnSensorReturn(GaugeDeviceSensor sensor);

		/// <summary>
		/// The action that will be fired when the user selects a sensor for returning 
		/// within the device manager.
		/// </summary>
		public OnSensorReturn onSensorReturnDelegate { get; set; }

    public UIView popupView;
    public UILabel nameLabel, typeLabel1, typeLabel2, measurementLabel1, measurementLabel2, unitLabel1, unitLabel2, displayOnLabel, moreInfoLabel, divider1, divider2, divider3, divider4;
    public UIButton settingsButton, connectButton, linkToggleButton, addWorkbench1, addWorkbench2, addAnalyzer1, addAnalyzer2;
    public UIImageView settingsImage, bluetoothImage, deviceImage, batteryImage;
    public GaugeDeviceSensor sensor;
    public bool shouldOpen = true;
    IION ion;
    public DeviceGridViewController gridVC;
    public int analyzerSlot;
    public bool fromWorkbench;

    public SensorStatusPopup(UIView gridView) {
      ion = AppState.context;
      popupView = new UIView(new CGRect(.05 * gridView.Bounds.Width,.2 * gridView.Bounds.Height, .9 * gridView.Bounds.Width,.36 * gridView.Bounds.Height));
      popupView.Layer.CornerRadius = 5;
			popupView.Layer.BorderWidth = 2f;
      popupView.BackgroundColor = UIColor.White;
      popupView.ClipsToBounds = true;

      settingsImage = new UIImageView(new CGRect(.02 * popupView.Bounds.Width, .03 * popupView.Bounds.Width, .08 * popupView.Bounds.Width, .1 * popupView.Bounds.Height));
      settingsImage.Image = UIImage.FromBundle("ic_settings");

      nameLabel = new UILabel(new CGRect(.11 * popupView.Bounds.Width,0,.6 * popupView.Bounds.Width, .18 * popupView.Bounds.Height));
      nameLabel.Font = UIFont.BoldSystemFontOfSize(18f);
			nameLabel.AdjustsFontSizeToFitWidth = true;
      nameLabel.TextAlignment = UITextAlignment.Left;

      settingsButton = new UIButton(new CGRect(0,0,.7 * popupView.Bounds.Width, .18 * popupView.Bounds.Height));
      settingsButton.TouchUpInside += showSensorSettings;
      settingsButton.BackgroundColor = UIColor.Clear;

      connectButton = new UIButton(new CGRect(.8 * popupView.Bounds.Width, 0, .2 * popupView.Bounds.Width, .18 * popupView.Bounds.Height));
			connectButton.TouchUpInside += connectSensor;
			connectButton.BackgroundColor = UIColor.Clear;

      bluetoothImage = new UIImageView(new CGRect(.89 * popupView.Bounds.Width, .02 * popupView.Bounds.Height, .09 * popupView.Bounds.Width, .14 * popupView.Bounds.Height));
      bluetoothImage.Layer.CornerRadius = 5;
      bluetoothImage.Layer.BorderWidth = 1f;

      divider1 = new UILabel(new CGRect(.01 * popupView.Bounds.Width, .18 * popupView.Bounds.Height, .98 * popupView.Bounds.Width, 2));
      divider1.BackgroundColor = UIColor.Black; 

      deviceImage = new UIImageView(new CGRect(-.02 * popupView.Bounds.Width,.23 * popupView.Bounds.Height, .18 * popupView.Bounds.Width, .25 * popupView.Bounds.Height));

      batteryImage = new UIImageView(new CGRect(.18 * popupView.Bounds.Width, .23 * popupView.Bounds.Height, .05 * popupView.Bounds.Width, .2 * popupView.Bounds.Height));

      typeLabel1 = new UILabel(new CGRect(.31 * popupView.Bounds.Width, .2 * popupView.Bounds.Height, .26 * popupView.Bounds.Width, .1 * popupView.Bounds.Height));
      typeLabel1.TextAlignment = UITextAlignment.Right;
      typeLabel1.AdjustsFontSizeToFitWidth = true;
			typeLabel1.Font = UIFont.BoldSystemFontOfSize(18);

      measurementLabel1 = new UILabel(new CGRect(.31 * popupView.Bounds.Width, .3 * popupView.Bounds.Height, .26 * popupView.Bounds.Width, .13 * popupView.Bounds.Height));
      measurementLabel1.TextAlignment = UITextAlignment.Right;
			measurementLabel1.Font = UIFont.BoldSystemFontOfSize(38);
			measurementLabel1.AdjustsFontSizeToFitWidth = true;

      unitLabel1 = new UILabel(new CGRect(.31 * popupView.Bounds.Width, .43 * popupView.Bounds.Height, .26 * popupView.Bounds.Width, .1 * popupView.Bounds.Height));
      unitLabel1.TextAlignment = UITextAlignment.Right;
			unitLabel1.Font = UIFont.BoldSystemFontOfSize(18);

      divider2 = new UILabel(new CGRect(.60 * popupView.Bounds.Width, .18 * popupView.Bounds.Height, 2, .35 * popupView.Bounds.Height));
      divider2.BackgroundColor = UIColor.Black;

      typeLabel2 = new UILabel(new CGRect(.66 * popupView.Bounds.Width, .2 * popupView.Bounds.Height, .31 * popupView.Bounds.Width, .1 * popupView.Bounds.Height));
      typeLabel2.TextAlignment = UITextAlignment.Right;
			typeLabel2.AdjustsFontSizeToFitWidth = true;
			typeLabel2.Font = UIFont.BoldSystemFontOfSize(18);
      //typeLabel2.Text = "Superheat";

      measurementLabel2 = new UILabel(new CGRect(.63 * popupView.Bounds.Width, .3 * popupView.Bounds.Height, .34 * popupView.Bounds.Width, .13 * popupView.Bounds.Height));
      measurementLabel2.TextAlignment = UITextAlignment.Right;
			measurementLabel2.Font = UIFont.BoldSystemFontOfSize(38);
			measurementLabel2.AdjustsFontSizeToFitWidth = true;

      unitLabel2 = new UILabel(new CGRect(.66 * popupView.Bounds.Width, .43 * popupView.Bounds.Height, .31 * popupView.Bounds.Width, .1 * popupView.Bounds.Height));
      unitLabel2.TextAlignment = UITextAlignment.Right;
			unitLabel2.Font = UIFont.BoldSystemFontOfSize(18);

      divider3 = new UILabel(new CGRect(.01 * popupView.Bounds.Width, .53 * popupView.Bounds.Height, .98 * popupView.Bounds.Width, 2));
      divider3.BackgroundColor = UIColor.Black;

      displayOnLabel = new UILabel(new CGRect(.01 * popupView.Bounds.Width,.56 * popupView.Bounds.Height, .23 * popupView.Bounds.Width, .2 * popupView.Bounds.Height));
      displayOnLabel.Lines = 2;
      displayOnLabel.LineBreakMode = UILineBreakMode.WordWrap;
      displayOnLabel.TextAlignment = UITextAlignment.Center;
      displayOnLabel.AdjustsFontSizeToFitWidth = true;
      if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone){
				displayOnLabel.Font = UIFont.BoldSystemFontOfSize(18f);
			} else {
				displayOnLabel.Font = UIFont.BoldSystemFontOfSize(30f);
			}
      displayOnLabel.Text = "Display\nOn";

      addWorkbench1 = new UIButton(new CGRect(.25 * popupView.Bounds.Width, .56 * popupView.Bounds.Height, .15 * popupView.Bounds.Width, .21 * popupView.Bounds.Height));
			addWorkbench1.SetImage(UIImage.FromBundle("ic_device_add_to_workbench"), UIControlState.Normal);
			addWorkbench1.BackgroundColor = UIColor.FromRGB(255, 200, 46);
      addWorkbench1.Layer.BorderWidth = 1f;
      addWorkbench1.TouchUpInside += addToWorkbench;

      addAnalyzer1 = new UIButton(new CGRect(.4 * popupView.Bounds.Width, .56 * popupView.Bounds.Height, .15 * popupView.Bounds.Width, .21 * popupView.Bounds.Height));
      addAnalyzer1.SetImage(UIImage.FromBundle("ic_device_add_to_analyzer"),UIControlState.Normal);
      addAnalyzer1.BackgroundColor = UIColor.FromRGB(255, 200, 46);
			addAnalyzer1.Layer.BorderWidth = 1f;
			addAnalyzer1.TouchUpInside += addToAnalyzer;

      addWorkbench2 = new UIButton(new CGRect(.64 * popupView.Bounds.Width, .56 * popupView.Bounds.Height, .15 * popupView.Bounds.Width, .21 * popupView.Bounds.Height));
      addWorkbench2.SetImage(UIImage.FromBundle("ic_device_add_to_workbench"), UIControlState.Normal);
      addWorkbench2.BackgroundColor = UIColor.FromRGB(255, 200, 46);
			addWorkbench2.Layer.BorderWidth = 1f;
			addWorkbench2.TouchUpInside += addToWorkbench2;

			addAnalyzer2 = new UIButton(new CGRect(.79 * popupView.Bounds.Width, .56 * popupView.Bounds.Height, .15 * popupView.Bounds.Width, .21 * popupView.Bounds.Height));
			addAnalyzer2.SetImage(UIImage.FromBundle("ic_device_add_to_analyzer"), UIControlState.Normal);
			addAnalyzer2.BackgroundColor = UIColor.FromRGB(255, 200, 46);
			addAnalyzer2.Layer.BorderWidth = 1f;
			addAnalyzer2.TouchUpInside += addToAnalyzer2; 

      divider4 = new UILabel(new CGRect(.01 * popupView.Bounds.Width, .79 * popupView.Bounds.Height, .98 * popupView.Bounds.Width, 2));
      divider4.BackgroundColor = UIColor.Black;

      moreInfoLabel = new UILabel(new CGRect(.02  * popupView.Bounds.Width, .79 * popupView.Bounds.Height, .96 * popupView.Bounds.Width, .2 * popupView.Bounds.Height));
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				moreInfoLabel.Font = UIFont.SystemFontOfSize(18f);
			} else {
				moreInfoLabel.Font = UIFont.SystemFontOfSize(25f);
			}
			moreInfoLabel.AdjustsFontSizeToFitWidth = true;
			moreInfoLabel.Lines = 2;
      moreInfoLabel.LineBreakMode = UILineBreakMode.WordWrap;
			NSError error = null;
			var htmlString = new NSAttributedString(
			NSData.FromString("<div style=\"font-size: 150%\">For added functionality, add this device<br>to <b>System Analyzer</b> or <b>Workbench</b>.</div>"),
			new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8 },
			ref error
			);

      moreInfoLabel.AttributedText = htmlString;
          
			popupView.AddSubview(settingsImage);
			popupView.AddSubview(nameLabel);
			popupView.AddSubview(settingsButton);
			popupView.AddSubview(bluetoothImage);
			popupView.AddSubview(connectButton);
			popupView.AddSubview(divider1);
			popupView.AddSubview(deviceImage);
			popupView.AddSubview(batteryImage);
			popupView.AddSubview(typeLabel1);
			popupView.AddSubview(measurementLabel1);
			popupView.AddSubview(unitLabel1);
			popupView.AddSubview(divider2);
			popupView.AddSubview(typeLabel2);
			popupView.AddSubview(measurementLabel2);
			popupView.AddSubview(unitLabel2);
			popupView.AddSubview(divider3);
			popupView.AddSubview(displayOnLabel);
			popupView.AddSubview(addWorkbench1);
			popupView.AddSubview(addAnalyzer1);
			popupView.AddSubview(addWorkbench2);
			popupView.AddSubview(addAnalyzer2);
			popupView.AddSubview(divider4);
			popupView.AddSubview(moreInfoLabel);
      popupView.Hidden = true;
    }

    public void showSensorSettings (object sender, EventArgs e){
      Console.WriteLine("Show the settings for sensor");
    }

		public void connectSensor(object sender, EventArgs e) {
      if(sensor.device.isConnected){
        sensor.device.connection.Disconnect();
      } else {
        sensor.device.connection.Connect();
      }
		}

		public void addToWorkbench(object sender, EventArgs e) {
      //Console.WriteLine("Add sensor " + sensor.device.sensors[0].name +" "+sensor.device.sensors[0].type.ToString()+" to workbench" );
      popupView.Hidden = true;
      if (ion.currentWorkbench.IndexOf(sensor.device.sensors[0]) == -1) {
        if (fromWorkbench){
					gridVC.inflateWorkbench(sensor.device.sensors[0]);
				} else {
					ion.currentWorkbench.AddSensor(sensor.device.sensors[0]);
					gridVC.inflateWorkbench();
				}
			} else {
				Console.WriteLine("Manifold is already on the Workbench");
			}
		}

		public void addToWorkbench2(object sender, EventArgs e) {
      if(sensor.device.sensorCount == 1){
				popupView.Hidden = true;
				if (ion.currentWorkbench.IndexOf(sensor.device.sensors[0]) == -1) {
					if (fromWorkbench) {
						gridVC.inflateWorkbench(sensor.device.sensors[0]);
					} else {
						ion.currentWorkbench.AddSensor(sensor.device.sensors[0]);
						gridVC.inflateWorkbench();
					}
				} else {
					Console.WriteLine("Pressure Manifold is already on the Workbench");
				}
			} else {
				popupView.Hidden = true;
				if (ion.currentWorkbench.IndexOf(sensor.device.sensors[1]) == -1) {
					if (fromWorkbench) {
						gridVC.inflateWorkbench(sensor.device.sensors[1]);
					} else {
						ion.currentWorkbench.AddSensor(sensor.device.sensors[1]);
						gridVC.inflateWorkbench();
					}
        } else {
					Console.WriteLine("Temperature Manifold is already on the Workbench");
				}
			}
		}

		public void addToAnalyzer(object sender, EventArgs e) {
			popupView.Hidden = true;
      if(!ion.currentAnalyzer.sensorList.Contains(sensor.device.sensors[0])){
        if(analyzerSlot == -1){
          Console.WriteLine("No specific analyzer slot set");
					updateAnalyzer(sensor.device.sensors[0]);
					gridVC.inflateAnalyzer();
				} else {
          sensor.device.sensors[0].analyzerSlot = analyzerSlot;
          ion.currentAnalyzer.sensorList.Add(sensor.device.sensors[0]);
					gridVC.inflateAnalyzer(sensor.device.sensors[0]);
				}
      } else {

				Console.WriteLine("Sensor is already on the Analyzer");
			}
		}

		public void addToAnalyzer2(object sender, EventArgs e) {
			if (sensor.device.sensorCount == 1) {
				popupView.Hidden = true;
				if (!ion.currentAnalyzer.sensorList.Contains(sensor.device.sensors[0])) {
					if (analyzerSlot == -1) {
						Console.WriteLine("No specific analyzer2 slot set");
						updateAnalyzer(sensor.device.sensors[0]);
						gridVC.inflateAnalyzer();
					} else {
						Console.WriteLine("Requested slot is available so adding sensor to that slot");
						sensor.device.sensors[0].analyzerSlot = analyzerSlot;
						ion.currentAnalyzer.sensorList.Add(sensor.device.sensors[0]);
						gridVC.inflateAnalyzer(sensor.device.sensors[0]);
					}
				} else {
					Console.WriteLine("Pressure sensor is already on the Analyzer");
				}
			} else {
				popupView.Hidden = true;
				if (!ion.currentAnalyzer.sensorList.Contains(sensor.device.sensors[1])) {
					if (analyzerSlot == -1) {
						Console.WriteLine("No specific analyzer2 slot set");
						updateAnalyzer(sensor.device.sensors[1]);
						gridVC.inflateAnalyzer();
					} else {
						Console.WriteLine("Requested slot is available so adding sensor to that slot");
						sensor.device.sensors[1].analyzerSlot = analyzerSlot;
						ion.currentAnalyzer.sensorList.Add(sensor.device.sensors[1]);
						gridVC.inflateAnalyzer(sensor.device.sensors[1]);
					}
				} else {
					Console.WriteLine("Temperature sensor is already on the Analyzer");
				}
			}
		}

    public void updatePopup(GaugeDeviceSensor passedSensor){
      if(sensor != null){
				sensor.onSensorStateChangedEvent -= updateSensor;
			}
      Console.WriteLine("Analyzer slot index: " + analyzerSlot);
      sensor = passedSensor;
			sensor.onSensorStateChangedEvent += updateSensor;
			nameLabel.Text = sensor.device.serialNumber.deviceModel.GetTypeString() + ":" + sensor.device.serialNumber.rawSerial.ToUpper();
			deviceImage.Image = Devices.DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);

      if (sensor.device.sensorCount == 1) {
        typeLabel1.Hidden = true;
        measurementLabel1.Hidden = true;
        unitLabel1.Hidden = true;
        divider2.Hidden = true;
        addWorkbench1.Hidden = true;
        addAnalyzer1.Hidden = true;
      } else {
        typeLabel1.Hidden = false;
        measurementLabel1.Hidden = false;
        unitLabel1.Hidden = false;
        divider2.Hidden = false;
        addWorkbench1.Hidden = false;
        addAnalyzer1.Hidden = false;
      }
      sensor.NotifySensorStateChanged();
		}

    public void updateSensor(Sensor sensor){
      var updateSensor = sensor as GaugeDeviceSensor;
			if (updateSensor.device.sensorCount == 1) {
				typeLabel2.Text = sensor.type.ToString();

				if (updateSensor.device.isConnected) {
					bluetoothImage.Image = UIImage.FromBundle("ic_bluetooth_connected");
					bluetoothImage.BackgroundColor = UIColor.Green;
					measurementLabel2.Text = sensor.measurement.amount.ToString();
					unitLabel2.Text = sensor.measurement.unit.ToString();

				} else {
					bluetoothImage.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
					bluetoothImage.BackgroundColor = UIColor.Red;
					measurementLabel2.Text = "----";
					unitLabel2.Text = "";
				}

			} else {
				typeLabel1.Text = updateSensor.device.sensors[0].type.ToString();
				typeLabel2.Text = updateSensor.device.sensors[1].type.ToString();

				if (updateSensor.device.isConnected) {
					bluetoothImage.Image = UIImage.FromBundle("ic_bluetooth_connected");
					bluetoothImage.BackgroundColor = UIColor.Green;

					measurementLabel1.Text = updateSensor.device.sensors[0].measurement.amount.ToString();
					unitLabel1.Text = updateSensor.device.sensors[0].unit.ToString();

					measurementLabel2.Text = updateSensor.device.sensors[1].measurement.amount.ToString();
					unitLabel2.Text = updateSensor.device.sensors[1].unit.ToString();
				} else {
					bluetoothImage.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
					bluetoothImage.BackgroundColor = UIColor.Red;
					measurementLabel2.Text = "----";
					unitLabel2.Text = "";

					measurementLabel1.Text = "----";
					unitLabel1.Text = "";
				}
			}

			batteryImage.Image = UIImage.FromBundle("img_battery_vert_100");
			if (updateSensor.device.battery > 75) {
				batteryImage.Image = UIImage.FromBundle("img_battery_vert_100");
			} else if (updateSensor.device.battery > 50) {
				batteryImage.Image = UIImage.FromBundle("img_battery_vert_75");
			} else if (updateSensor.device.battery > 25) {
				batteryImage.Image = UIImage.FromBundle("img_battery_vert_50");
			} else if (updateSensor.device.battery > 0) {
				batteryImage.Image = UIImage.FromBundle("img_battery_vert_25");
			} else {
				batteryImage.Image = UIImage.FromBundle("img_battery_vert_0");
			}
    }

    public void updateAnalyzer(GaugeDeviceSensor sensor){

				var deviceType = sensor.device.serialNumber.deviceModel;
        int index;
				if (deviceType == EDeviceModel.P500 || deviceType == EDeviceModel.PT500) {
          index = analyzerOpen(Core.Content.Analyzer.ESide.Low);
          if(index != -1){
            sensor.analyzerSlot = index;
            ion.currentAnalyzer.sensorList.Add(sensor);
          }

          ////CONSOLIDATED DATA STRUCTURE METHOD OF ADDING TO ANALYZER
          //if(ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.Low,sensor)){
            
          //} else if (ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.High,sensor)){
            
          //} else {
            
          //}
				} else if (deviceType == EDeviceModel.P800 || deviceType == EDeviceModel.PT800) {
					index = analyzerOpen(Core.Content.Analyzer.ESide.High);
					if (index != -1) {
						sensor.analyzerSlot = index;
						ion.currentAnalyzer.sensorList.Add(sensor);
					}

					////CONSOLIDATED DATA STRUCTURE METHOD OF ADDING TO ANALYZER
					//if (ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.High, sensor)) {

					//} else if (ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.Low, sensor)) {

					//} else {

					//}
				} else {
					index = analyzerOpen(Core.Content.Analyzer.ESide.Low);
					if (analyzerOpen(Core.Content.Analyzer.ESide.Low) != -1) {
						sensor.analyzerSlot = index;
						ion.currentAnalyzer.sensorList.Add(sensor);
					}

					////CONSOLIDATED DATA STRUCTURE METHOD OF ADDING TO ANALYZER
					//if (ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.Low, sensor)) {

					//} else if (ion.currentAnalyzer.AddSensorToSide(Core.Content.Analyzer.ESide.High, sensor)) {

					//} else {

					//}
				}

    }

    public int analyzerOpen(Core.Content.Analyzer.ESide side){
			bool[] slots = new bool[8] { true, true, true, true,true, true, true, true };

			foreach (var item in ion.currentAnalyzer.sensorList) {
				if (item.analyzerSlot == 0) {
					slots[0] = false;
				} else if (item.analyzerSlot == 1) {
					slots[1] = false;
				} else if (item.analyzerSlot == 2) {
					slots[2] = false;
				} else if (item.analyzerSlot == 3) {
					slots[3] = false;
				} else if (item.analyzerSlot == 4) {
					slots[4] = false;
				} else if (item.analyzerSlot == 5) {
					slots[5] = false;
				} else if (item.analyzerSlot == 6) {
					slots[6] = false;
				} else if (item.analyzerSlot == 7) {
					slots[7] = false;
				}
			}
			if (side == Core.Content.Analyzer.ESide.Low) {
        for (int i = 0; i < 8; i++){
          if(slots[i] == true){
						return i;
          }
        }
			} else {
				for (int i = 4; i < 8; i++) {
					if (slots[i] == true) {
						return i;
					}
				}
				for (int i = 0; i < 4; i++) {
					if (slots[i] == true) {
						return i;
					}
				}
			}

      return -1;
    }
  }
}
