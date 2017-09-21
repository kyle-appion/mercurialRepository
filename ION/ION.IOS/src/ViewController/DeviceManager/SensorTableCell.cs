namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using UIKit;

	using Appion.Commons.Util;

  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.IOS.Sensors;
  using ION.IOS.UI;

  public class SensorRecord : IRecord {
    // Overridden from IDeviceTableRecord
    public EViewType viewType {
      get {
        return EViewType.Sensor;
      }
    }

    public bool isExpandable { 
      get {
        return false;
      }
    }
    public bool isExpanded { get; set; }

    public GaugeDeviceSensor sensor { get; set; }

    public SensorRecord(GaugeDeviceSensor sensor) {
      this.sensor = sensor;
    }
  }

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
    private SensorRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.sensor.onSensorEvent -= OnSensorUpdated;
        }

        __record = value;

        if (__record != null) {
          __record.sensor.onSensorEvent += OnSensorUpdated;
        }
      }
    } SensorRecord __record;

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
      record = null;
    }

    /// <summary>
    /// Updates the cell to the display the given sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="addClickedResponder">Add clicked responder.</param>
    public void UpdateTo(SensorRecord record, Action addClickedResponder = null) {
      this.record = record;

      onAddClicked = addClickedResponder;

      OnSensorUpdated(new SensorEvent(SensorEvent.EType.Invalidated,record.sensor));
    }

    private void OnSensorUpdated(SensorEvent sensorEvent) {
      labelType.Text = sensorEvent.sensor.type.GetTypeString();
      labelMeasurement.Text = sensorEvent.sensor.ToFormattedString(true);

      if (sensorEvent.sensor is GaugeDeviceSensor) {
        var ds = sensorEvent.sensor as GaugeDeviceSensor;
        if (!ds.device.isConnected || ds.removed) {
          labelMeasurement.Text = "---";
        }
      }
    }
	}
}
