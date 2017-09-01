namespace ION.IOS.ViewController.Workbench {

	using System;

	using UIKit;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Connections;
	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Sensors;

	using ION.IOS.Devices;
	using ION.IOS.Sensors;
	using ION.IOS.UI;
	using ION.IOS.Util;
  using CoreGraphics;

  public partial class ViewerTableCell : UITableViewCell, IReleasable {
    private IION ion { get; set; }
    /// <summary>
    /// The manifold that we are representing.
    /// </summary>
    /// <value>The manifold.</value>
    private Manifold manifold {
      get {
        return __manifold;
      }
      set {
        if (__manifold != null) {
          __manifold.onManifoldChanged -= OnManifoldUpdated;
        }

        __manifold = value;

        if (__manifold != null) {
          __manifold.onManifoldChanged += OnManifoldUpdated;
          OnManifoldUpdated(__manifold);
        }
      }
    } Manifold __manifold;

    private int lastBatteryLevel { get; set; }

		public ViewerTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from IReleasable
    public void Release() {
      manifold = null;
      lastBatteryLevel = -1;
//      lastConnectionState = EConnectionState.Resolving; // Not the best, but a resolution should be over quick.
    }

    public void UpdateTo(IION ion, Manifold manifold) {
			this.BackgroundColor = UIColor.White;
      this.ion = ion;
      this.manifold = manifold;
     
      buttonConnection.TouchUpInside -= changeConnectionStatus;
      buttonConnection.TouchUpInside += changeConnectionStatus;
    }

    public void UpdateFromGaugeSensor(GaugeDeviceSensor sensor) {
      if(labelHeader == null){
        return;
      }
      Console.WriteLine("Update cell bounds: " + this.Bounds);
      var device = sensor.device;
      var state = device.connection.connectionState;


      labelHeader.Text = device.serialNumber.deviceModel.GetTypeString() + ": " + sensor.name;
      imageSensorIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      labelMeasurement.Text = sensor.ToFormattedString();
      labelUnit.Text = sensor.unit.ToString();
      activityConnectStatus.Hidden = EConnectionState.Resolving != sensor.device.connection.connectionState;
      buttonConnection.Hidden = false;

      UpdateAlarm(sensor);

      if (device.isConnected) {
        Console.WriteLine("Device is connected so updating battery to " + device.battery);
        UpdateBatteryIcon(device.battery);

//        if (lastConnectionState != state) {
          buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_green_background_bordered").AsNinePatch(), UIControlState.Normal);
          buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
//        }

        labelMeasurement.TextColor = new UIColor(Colors.BLACK);
        labelUnit.TextColor = new UIColor(Colors.BLACK);
        labelConnectionStatus.Text = "Connected";
        labelConnectionStatus.TextColor = UIColor.Green;
      } else {
        if (EConnectionState.Broadcasting == device.connection.connectionState) {
          UpdateBatteryIcon(device.battery);
					labelConnectionStatus.Text = "Long Range Mode";
					labelConnectionStatus.TextColor = UIColor.Blue;
				} else {
					labelConnectionStatus.Text = "Disconnected";
					labelConnectionStatus.TextColor = UIColor.Red;
					UpdateBatteryIcon(-1);
        }

//        if (lastConnectionState != state) {
          buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_red_background_bordered").AsNinePatch(), UIControlState.Normal);
          buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
//        }

        labelMeasurement.TextColor = new UIColor(Colors.LIGHT_GRAY);
        labelUnit.TextColor = new UIColor(Colors.LIGHT_GRAY);
      }

//      lastConnectionState = state;
    }

    private void OnManifoldUpdated(Manifold manifold) {
      if (manifold.primarySensor is GaugeDeviceSensor) {
        UpdateFromGaugeSensor(manifold.primarySensor as GaugeDeviceSensor);
      } else {
        UpdateFromSensor(manifold.primarySensor);
      }
    }

    private void UpdateFromSensor(Sensor sensor) {
      labelHeader.Text = sensor.type.GetTypeString() + ": " + sensor.name;
      labelMeasurement.Text = sensor.ToFormattedString();
      labelUnit.Text = sensor.unit.ToString();

      activityConnectStatus.Hidden = true;
      UpdateBatteryIcon(-1);
      UpdateAlarm(sensor);

      buttonConnection.Hidden = true;
      labelMeasurement.TextColor = new UIColor(Colors.BLACK);
      labelUnit.TextColor = new UIColor(Colors.BLACK);
    }

    /// <summary>
    /// Updates the battery icon. -1 will hide the image.
    /// </summary>
    /// <param name="percent">Percent.</param>
    private void UpdateBatteryIcon(int percent) {
      Console.WriteLine("Updating battery with " + percent);  
      if (lastBatteryLevel == percent && percent != -1 && lastBatteryLevel != -1) {
        return;
      }

      imageBattery.Hidden = false;
      imageBattery.TintColor = new UIColor(Colors.BLACK);

      if (percent >= 100) {
        Console.WriteLine("Setting battery level to 100");
        imageBattery.Image = UIImage.FromBundle("img_battery_100");
      } else if (percent >= 75) {
				Console.WriteLine("Setting battery level to 75");
				imageBattery.Image = UIImage.FromBundle("img_battery_75");
      } else if (percent >= 50) {
				Console.WriteLine("Setting battery level to 50");
				imageBattery.Image = UIImage.FromBundle("img_battery_50");
      } else if (percent >= 25) {
				Console.WriteLine("Setting battery level to 25");
				imageBattery.Image = UIImage.FromBundle("img_battery_25");
        imageBattery.TintColor = new UIColor(Colors.RED);
      } else if (percent >= 0) {
				Console.WriteLine("Setting battery level to 0");
				imageBattery.Image = UIImage.FromBundle("img_battery_0");
        imageBattery.TintColor = new UIColor(Colors.RED);
      } else {
        Console.WriteLine("Hiding battery");
				imageBattery.Hidden = true;
			}

      lastBatteryLevel = percent;
    }

   public void changeConnectionStatus(object sender, EventArgs eww){
      Console.WriteLine("conDis button clicked");
      var gaugeSensor = manifold.primarySensor as GaugeDeviceSensor;
	    activityConnectStatus.StartAnimating();
      if (gaugeSensor.device.isConnected) {
        gaugeSensor.device.connection.Disconnect();
      } else {
        gaugeSensor.device.connection.Connect();
      }
      activityConnectStatus.StopAnimating();
    }

    private void UpdateAlarm(Sensor sensor) {
      if (ion.alarmManager.HostHasEnabledAlarms(sensor)) {
        imageAlarmIcon.Hidden = false;
      } else {
        imageAlarmIcon.Hidden = true;
      }
    }
	}
}
