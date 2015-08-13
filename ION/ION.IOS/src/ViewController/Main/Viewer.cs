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

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      viewBackground.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (onViewerClicked != null) {
          onViewerClicked();
        }

      }));
    }

    // Overridden from UITableViewCell
    public override void RemoveFromSuperview() {
      base.RemoveFromSuperview();

      manifold = null;
    }

    private void UpdateToGaugeDeviceSensor(GaugeDeviceSensor sensor) {
      // Set header content
      labelHeader.Text = sensor.device.serialNumber.deviceModel.GetTypeString() + ": " + sensor.device.name;

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
      labelHeader.Text = sensor.sensorType.GetTypeString() + ": " + sensor.name;

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
