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

	public partial class ViewerTableCell : UITableViewCell, IReleasable {

    /// <summary>
    /// The ion context necessary for connecting the primary sensor if it is
    /// attached to a gauge device.
    /// </summary>
    /// <value>The ion.</value>
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
    /// <summary>
    /// The action that is fired when the background clicked.
    /// </summary>
    /// <value>The on background clicked.</value>
    private EventHandler<Manifold> onBackgroundClicked { get; set; }

    /// <summary>
    /// The last known battery level.
    /// </summary>
    /// <value>The last battery level.</value>
    private int lastBatteryLevel { get; set; }
    /// <summary>
    /// The last known connection state.
    /// </summary>
    /// <value>The last state of the connection.</value>
//    private EConnectionState lastConnectionState { get; set; }

		public ViewerTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      viewBackground.Layer.CornerRadius = 5;

      var tapper = new UITapGestureRecognizer(() => {
        if (onBackgroundClicked != null) {
          onBackgroundClicked(this, manifold);
        }
      });
      AddGestureRecognizer(tapper);
			lastBatteryLevel = -1;
    }

    // Overridden from UITableViewCell
    public override void PrepareForReuse() {
      base.PrepareForReuse();

      Release();
    }

    // Overridden from UITableViewCell
    public override void RemoveFromSuperview() {
      base.RemoveFromSuperview();

      Release();
    }

    // Overridden from IReleasable
    public void Release() {
      manifold = null;
      lastBatteryLevel = -1;
//      lastConnectionState = EConnectionState.Resolving; // Not the best, but a resolution should be over quick.
    }

    public void UpdateTo(IION ion, Manifold manifold, EventHandler<Manifold> backgroundClicked = null) {
      this.ion = ion;
      this.manifold = manifold;
      this.onBackgroundClicked = backgroundClicked;
      this.labelLinked.Hidden = !(manifold.secondarySensor is GaugeDeviceSensor);
    }

    public void UpdateFromGaugeSensor(GaugeDeviceSensor sensor) {
      var device = sensor.device;
      var state = device.connection.connectionState;

      labelHeader.Text = device.serialNumber.deviceModel.GetTypeString() + ": " + sensor.name;
      imageSensorIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      labelMeasurement.Text = sensor.ToFormattedString();
      labelUnit.Text = sensor.unit.ToString();
      labelSerialNumber.Text = device.serialNumber.ToString();
      activityConnectStatus.Hidden = EConnectionState.Resolving != sensor.device.connection.connectionState;
      buttonConnection.Hidden = false;
			buttonConnection.TouchUpInside += delegate {
        var gaugeSensor = manifold.primarySensor as GaugeDeviceSensor;
        activityConnectStatus.StartAnimating();
        if(gaugeSensor.device.isConnected){
          gaugeSensor.device.connection.Disconnect();          
        } else {
          gaugeSensor.device.connection.Connect();
        }
			  activityConnectStatus.StopAnimating();
			};

      UpdateAlarm(sensor);

      if (device.isConnected) {
        UpdateBatteryIcon(device.battery);

//        if (lastConnectionState != state) {
          buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_green_background_bordered").AsNinePatch(), UIControlState.Normal);
          buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
//        }

        labelMeasurement.TextColor = new UIColor(Colors.BLACK);
        labelUnit.TextColor = new UIColor(Colors.BLACK);
        labelConnectionStatus.Hidden = true;
      } else {
        if (EConnectionState.Broadcasting == device.connection.connectionState) {
          UpdateBatteryIcon(device.battery);
        } else {
          UpdateBatteryIcon(-1);
        }

//        if (lastConnectionState != state) {
          buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_red_background_bordered").AsNinePatch(), UIControlState.Normal);
          buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
//        }

        labelMeasurement.TextColor = new UIColor(Colors.LIGHT_GRAY);
        labelUnit.TextColor = new UIColor(Colors.LIGHT_GRAY);
        labelConnectionStatus.Hidden = true;
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
      if (lastBatteryLevel == percent && percent != -1 && lastBatteryLevel != -1) {
        return;
      }

      imageBattery.Hidden = false;
      imageBattery.TintColor = new UIColor(Colors.BLACK);

      if (percent >= 100) {
        imageBattery.Image = UIImage.FromBundle("img_battery_100");
      } else if (percent >= 75) {
        imageBattery.Image = UIImage.FromBundle("img_battery_75");
      } else if (percent >= 50) {
        imageBattery.Image = UIImage.FromBundle("img_battery_50");
      } else if (percent >= 25) {
        imageBattery.Image = UIImage.FromBundle("img_battery_25");
        imageBattery.TintColor = new UIColor(Colors.RED);
      } else if (percent >= 0) {
        imageBattery.Image = UIImage.FromBundle("img_battery_0");
        imageBattery.TintColor = new UIColor(Colors.RED);
      } else {
        imageBattery.Hidden = true;
      }

      lastBatteryLevel = percent;
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
