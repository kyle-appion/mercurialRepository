// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Sensors;
using ION.IOS.UI;

namespace ION.IOS.ViewController.DeviceManager {
	public partial class SensorTableCell : UITableViewCell, IReleasable {

    /// <summary>
    /// The action that is called when the add button is clicked.
    /// </summary>
    /// <value>The on add clicked.</value>
    public Action onAddClicked { get; set; }

    /// <summary>
    /// The sensor that this cell is displaying.
    /// </summary>
    /// <value>The sensor.</value>
    private Sensor sensor {
      get {
        return __sensor;
      }
      set {
        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent -= OnSensorUpdated;
        }

        __sensor = value;

        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent += OnSensorUpdated;
        }
      }
    } Sensor __sensor;

		public SensorTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);
      buttonAdd.TouchUpInside += (object sender, EventArgs e) => {
        if (onAddClicked != null) {
          onAddClicked();
        }
      };
    }

    // Overridden from IReleasable
    public void Release() {
      sensor = null;
    }

    /// <summary>
    /// Updates the cell to the display the given sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="addClickedResponder">Add clicked responder.</param>
    public void UpdateTo(Sensor sensor, Action addClickedResponder = null) {
      this.sensor = sensor;

      sensor.onSensorStateChangedEvent += OnSensorUpdated;

      onAddClicked = addClickedResponder;

      OnSensorUpdated(sensor);
    }

    private void OnSensorUpdated(Sensor sensor) {
      labelType.Text = sensor.type.GetTypeString();
      labelMeasurement.Text = sensor.ToFormattedString(true);

      if (sensor is GaugeDeviceSensor) {
        var ds = sensor as GaugeDeviceSensor;
        if (!ds.device.isConnected) {
          labelMeasurement.Text = "---";
        }
      }
    }
	}
}