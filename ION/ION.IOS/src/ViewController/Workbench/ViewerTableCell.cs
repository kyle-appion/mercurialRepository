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
			this.BackgroundColor = UIColor.Clear;
      this.ion = ion;
      this.manifold = manifold;
      this.Layer.BorderWidth = 1f;
      labelMeasurement.Font = UIFont.FromName("DroidSans-Bold", 36f);

      labelMeasurement.AdjustsFontSizeToFitWidth = true;
      batteryImage.Layer.BorderWidth = 1f;
      buttonConnection.Layer.CornerRadius = 5f;
     
      buttonConnection.TouchUpInside -= changeConnectionStatus;
      buttonConnection.TouchUpInside += changeConnectionStatus;
    }

    public void UpdateFromGaugeSensor(GaugeDeviceSensor sensor) {
      if(labelHeader == null){
        return;
      }
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
        UpdateBatteryIcon(device.battery);

        buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
        buttonConnection.BackgroundColor = UIColor.Green;

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

        buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
			  buttonConnection.BackgroundColor = UIColor.Red;

				labelMeasurement.TextColor = new UIColor(Colors.LIGHT_GRAY);
        labelUnit.TextColor = new UIColor(Colors.LIGHT_GRAY);
      }

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
      if (lastBatteryLevel == percent && percent != -1 && lastBatteryLevel != -1) {
        batteryImage.Hidden = false;
				batteryImage.Image = UIImage.FromBundle("img_battery_0");
				return;
      }

      batteryImage.Hidden = false;
      batteryImage.TintColor = new UIColor(Colors.BLACK);

      if (percent >= 100) {
        batteryImage.Image = UIImage.FromBundle("img_battery_100");
      } else if (percent >= 75) {
				batteryImage.Image = UIImage.FromBundle("img_battery_75");
			} else if (percent >= 50) {
				batteryImage.Image = UIImage.FromBundle("img_battery_50");
			} else if (percent >= 25) {
				batteryImage.Image = UIImage.FromBundle("img_battery_25");
				//batteryImage.TintColor = new UIColor(Colors.RED);
      } else if (percent >= 0) {
				batteryImage.Image = UIImage.FromBundle("img_battery_0");
				//batteryImage.TintColor = new UIColor(Colors.RED);
      } else {
				batteryImage.Hidden = true;
			}

      lastBatteryLevel = percent;
    }

   public void changeConnectionStatus(object sender, EventArgs eww){
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
