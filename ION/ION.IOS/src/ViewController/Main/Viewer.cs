using System;

using Foundation;
using UIKit;

using ION.Core.Connections;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;

using ION.IOS.Devices;
using ION.IOS.UI;
using ION.IOS.Util;
using ION.IOS.Sensors;

namespace ION.IOS.ViewController.Main {
	public partial class Viewer : UITableViewCell {

    /// <summary>
    /// The action that is called when the viewer is clicked.
    /// </summary>
    public Action onViewerClicked;
    /// <summary>
    /// The manifold that is used used to provide content for the view.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold {
      get {
        return __manifold;
      }
      set {
        if (__manifold != null) {
          __manifold.onManifoldChanged -= OnManifoldChanged;
        }

        __manifold = value;

        if (__manifold != null) {
          __manifold.onManifoldChanged += OnManifoldChanged;
          OnManifoldChanged(__manifold);
        }
      }
    } Manifold __manifold;

		public Viewer (IntPtr handle) : base (handle) {
      // Nope
		}

    /// <summary>
    /// The old battery level to prevent from reloading the battery image.
    /// </summary>
    /// <value>The old batterty.</value>
    private int oldBattery { get; set; }

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      viewBackground.Layer.CornerRadius = 10f;

      viewBackground.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (onViewerClicked != null) {
          onViewerClicked();
        }
      }));
    }

    // Overridden from UITableViewCell
    public override void PrepareForReuse() {
      base.PrepareForReuse();

      manifold = null;
    }

    private void UpdateToGaugeDeviceSensor(GaugeDeviceSensor sensor) {
      // Set header content
      labelHeader.Text = sensor.device.serialNumber.deviceModel.GetTypeString() + ": " + sensor.device.name;

      if (sensor.device.isConnected) {
        imageBattery.Hidden = false;
        imageBattery.TintColor = new UIColor(Colors.BLACK);
        if (oldBattery != sensor.device.battery) {
          if (sensor.device.battery >= 100) {
            imageBattery.Image = UIImage.FromBundle("img_battery_100");
          } else if (sensor.device.battery >= 75) {
            imageBattery.Image = UIImage.FromBundle("img_battery_75");
          } else if (sensor.device.battery >= 50) {
            imageBattery.Image = UIImage.FromBundle("img_battery_50");
          } else if (sensor.device.battery >= 25) {
            imageBattery.TintColor = new UIColor(Colors.RED);
            imageBattery.Image = UIImage.FromBundle("img_battery_25");
          } else {
            imageBattery.TintColor = new UIColor(Colors.RED);
            imageBattery.Image = UIImage.FromBundle("img_battery_0");
          }
          oldBattery = sensor.device.battery;
        }

        // TODO ahodder@appioninc.com: Prevent from constantly loading these images.
        buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_green_background_bordered").AsNinePatch(), UIControlState.Normal);
        buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
      } else {
        imageBattery.Hidden = true;

        buttonConnection.SetBackgroundImage(UIImage.FromBundle("np_red_background_bordered").AsNinePatch(), UIControlState.Normal);
        buttonConnection.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
      }

      // Set primary content
      imageSensor.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
      var measurement = sensor.measurement;
      labelMeasurement.Text = measurement.amount + "";
      if (sensor.device.isConnected) {
        labelMeasurement.TextColor = new UIColor(Colors.BLACK);
        labelUnit.TextColor = new UIColor(Colors.BLACK);
      } else {
        labelMeasurement.TextColor = new UIColor(Colors.LIGHT_GRAY);
        labelUnit.TextColor = new UIColor(Colors.LIGHT_GRAY);
      }

      // Set footer content
      if (/*sensor has an alarm*/false) {
        labelAlarm.TextColor = new UIColor(Colors.BLACK);
        imageAlarmIcon.Hidden = false;
        if (/*sensor alarm is active (on)*/false) {
          // TODO ahodder@appioninc.com: Apply black color mask to the icon
        } else {
          // TODO ahodder@appioninc.com: Apply gray color mask to the icon
        }
      } else {
        labelAlarm.TextColor = new UIColor(Colors.LIGHT_GRAY);
        imageAlarmIcon.Hidden = true;
      }
      labelConnectionStatus.Hidden = sensor.device.isConnected;
      labelUnit.Text = measurement.unit.ToString();

      // Set subview content
      labelSerialNumber.Text = sensor.device.serialNumber.ToString();
    }

    private void UpdateToSensor(Sensor sensor) {
      // Set header content      
      labelHeader.Text = sensor.type.GetTypeString() + ": " + sensor.name;

      // Set primary content
      var measurement = sensor.measurement;
      labelMeasurement.Text = measurement.amount + "";
      labelMeasurement.TextColor = new UIColor(Colors.BLACK);
      labelUnit.Text = measurement.unit.ToString();
      labelMeasurement.TextColor = new UIColor(Colors.BLACK);

      // Set footer content
      if (/*sensor has an alarm*/false) {
        labelAlarm.TextColor = new UIColor(Colors.BLACK);
        imageAlarmIcon.Hidden = false;
        if (/*sensor alarm is active (on)*/false) {
          // TODO ahodder@appioninc.com: Apply black color mask to the icon
        } else {
          // TODO ahodder@appioninc.com: Apply gray color mask to the icon
        }
      } else {
        labelAlarm.TextColor = new UIColor(Colors.LIGHT_GRAY);
        imageAlarmIcon.Hidden = true;
      }
      labelConnectionStatus.Hidden = true;
      labelUnit.Text = measurement.unit.ToString();

      // Set subview content
      labelSerialNumber.Text = sensor.name;
    }

    /// <summary>
    /// Called when the viewer's manifold changes.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldChanged(Manifold manifold) {
      var sensor = manifold.primarySensor as GaugeDeviceSensor;
      if (sensor != null) {
        UpdateToGaugeDeviceSensor(sensor);
      } else {
        UpdateToSensor(sensor);
      }
    }
	}
}
