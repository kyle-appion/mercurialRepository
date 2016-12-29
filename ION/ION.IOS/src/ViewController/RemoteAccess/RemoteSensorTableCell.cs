namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;
	using CoreGraphics;
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

	public partial class RemoteSensorTableCell : UITableViewCell, IReleasable {
		/// <summary>
		///  cell height is 48 for sensor table cell
		/// </summary>
		public UIView viewBackground;
		public UIButton buttonAdd;
		public UILabel labelType;
		public UILabel labelMeasurement;
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
          __record.sensor.onSensorStateChangedEvent -= OnSensorUpdated;
        }

        __record = value;

        if (__record != null) {
          __record.sensor.onSensorStateChangedEvent += OnSensorUpdated;
        }
      }
    } SensorRecord __record;

		public RemoteSensorTableCell (IntPtr handle) : base (handle) {
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
    public void UpdateTo(SensorRecord record, double cellWidth, Action addClickedResponder = null) {
      this.record = record;
      var cellHeight = 48;
      this.BackgroundColor = UIColor.Clear;

      viewBackground = new UIView(new CGRect(0,0,cellWidth,cellHeight));
      viewBackground.BackgroundColor = UIColor.Clear;
      
      buttonAdd = new UIButton(new CGRect(cellWidth - cellHeight,0,cellHeight,cellHeight));
      labelType = new UILabel(new CGRect(cellHeight,0,cellWidth - cellHeight,.5 * cellHeight));
      labelType.BackgroundColor = UIColor.White;
      labelMeasurement = new UILabel(new CGRect(cellHeight,.5 * cellHeight, cellWidth - cellHeight, .5 * cellHeight));
      labelMeasurement.BackgroundColor = UIColor.White;
      

      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);
      buttonAdd.SetImage(UIImage.FromBundle("ic_device_add"), UIControlState.Normal);
      buttonAdd.TouchUpInside += (object sender, EventArgs e) => {
        if (onAddClicked != null) {
          onAddClicked();
        }
      };
      onAddClicked = addClickedResponder;

      OnSensorUpdated(record.sensor);      

      viewBackground.Add(labelType);
      viewBackground.Add(labelMeasurement);
      viewBackground.Add(buttonAdd);
      this.AddSubview(viewBackground);
    }

    private void OnSensorUpdated(Sensor sensor) {
      labelType.Text = sensor.type.GetTypeString();
      labelMeasurement.Text = sensor.ToFormattedString(true);

      if (sensor is GaugeDeviceSensor) {
        var ds = sensor as GaugeDeviceSensor;
        //labelMeasurement.Text = ds.measurement.ToString();
        if (!ds.device.isConnected || ds.removed) {
          labelMeasurement.Text = "---";
        }
      }
    }
	}
}
