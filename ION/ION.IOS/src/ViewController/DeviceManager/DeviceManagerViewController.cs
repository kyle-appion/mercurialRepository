using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Util;
using ION.IOS.ViewController;

namespace ION.IOS.ViewController.DeviceManager {
	public partial class DeviceManagerViewController : UIViewController {

    /// <summary>
    /// The ion context for this view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device source for our device table.
    /// </summary>
    /// <value>The device source.</value>
    private DeviceSource deviceSource { get; set; }

		public DeviceManagerViewController (IntPtr handle) : base (handle) {
      ion = AppState.context;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      buttonScan.TouchUpInside += (object sender, EventArgs e) => {
        ion.deviceManager.DoActiveScan();
      };

      tableDeviceList.Source = deviceSource = new DeviceSource(tableDeviceList, ion.deviceManager.devices);

      ion.deviceManager.onDeviceFound += HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged += HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged += HandleDeviceStateChanged;
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      base.ViewDidUnload();

      ion.deviceManager.onDeviceFound -= HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged -= HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged -= HandleDeviceStateChanged;
    }

    /// <summary>
    /// The callback used by the device manager when a new device is found.
    /// </summary>
    /// <param name="dm">Dm.</param>
    /// <param name="device">Device.</param>
    private void HandleDeviceFound(IDeviceManager dm, IDevice device) {
      Log.D(this, "Handling device found!");
      deviceSource.SetDevices(ion.deviceManager.devices);
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void HandleDeviceManagerStateChanged(IDeviceManager dm, EDeviceManagerState state) {
      Log.D(this, "Handling device manager state changed!");
    }

    /// <summary>
    /// The callbacks used by the device manager when a device's state changes.
    /// </summary>
    /// <param name="device">Device.</param>
    private void HandleDeviceStateChanged(IDevice device) {
      Log.D(this, "Handling device state changed!");
      Task.Factory.StartNew(() => {
        deviceSource.SetDevices(ion.deviceManager.devices);
      }, Task.Factory.CancellationToken, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }
	} // End DeviceManagerViewController

  /// <summary>
  /// The table source that will provide the cell views for the device
  /// manager's table view 
  /// </summary>
  internal class DeviceSource : UITableViewSource {
    private static readonly DeviceGroup
      CONNECTED = new DeviceGroup(Strings.Device.CONNECTED.FromResources(), Colors.GREEN),
      LONG_RANGE = new DeviceGroup(Strings.Device.LONG_RANGE.FromResources(), Colors.LIGHT_BLUE),
      NEW_DEVICES = new DeviceGroup(Strings.Device.NEW_DEVICES.FromResources(), Colors.LIGHT_GRAY),
      AVAILABLE = new DeviceGroup(Strings.Device.AVAILABLE.FromResources(), Colors.YELLOW),
      DISCONNECTED = new DeviceGroup(Strings.Device.DISCONNECTED.FromResources(), Colors.RED);

    /// <summary>
    /// Collection of all the groups that can exist in the device source as a section.
    /// </summary>
    private static readonly DeviceGroup[] GROUPS = new DeviceGroup[] {
      CONNECTED,
      LONG_RANGE,
      NEW_DEVICES,
      AVAILABLE,
      DISCONNECTED,
    };

    /// <summary>
    /// The key that is used to fetch a section nib height.
    /// </summary>
    private const string CELL_SECTION = "cellSection";
    /// <summary>
    /// The key that is used to fetch a device cell nib.
    /// </summary>
    private const string CELL_DEVICE = "cellDevice";
    /// <summary>
    /// The key that is used to fetcht the height of the sensor nib.
    /// </summary>
    private const string CELL_SENSOR = "cellSensor";

    /// <summary>
    /// The indexer that will retrieve an IDevice from the source
    /// at the given index.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public IDevice this[NSIndexPath indexPath] {
      get {
        return __items[__sections[indexPath.Section]][indexPath.Row];
      }
    }

    /// <summary>
    /// The table view that this entity is a source to.
    /// </summary>
    /// <value>The table view.</value>
    private UITableView tableView { get; set; }
    /// <summary>
    /// The sections of devices within the source.
    /// </summary>
    /// <value>The sections.</value>
    private List<DeviceGroup> __sections;
    /// <summary>
    /// The mapping of section to content.
    /// </summary>
    private  Dictionary<DeviceGroup, List<IDevice>> __items;
    /// <summary>
    /// A lookup table for the heights of all the cells.
    /// </summary>
    private Dictionary<string, nfloat> __cellHeights = new Dictionary<string, nfloat>();

    /*
    /// <summary>
    /// The states.
    /// </summary>
    private List<EDeviceState> __states = new List<EDeviceState>();
    /// <summary>
    /// The dictionary that is holding the content of the table source.
    /// </summary>
    private Dictionary<EDeviceState, List<IDevice>> __content = new Dictionary<EDeviceState, IDevice>();
    */

    public DeviceSource(UITableView tableView, List<IDevice> devices) {
      this.tableView = tableView;


      __cellHeights[CELL_SECTION] = tableView.DequeueReusableCell(CELL_SECTION).Frame.Size.Height;
      __cellHeights[CELL_DEVICE] = tableView.DequeueReusableCell(CELL_DEVICE).Frame.Size.Height;
      __cellHeights[CELL_SENSOR] = tableView.DequeueReusableCell(CELL_SENSOR).Frame.Size.Height;

      __sections = new List<DeviceGroup>(GROUPS);
      __items = new Dictionary<DeviceGroup, List<IDevice>>();
      foreach (DeviceGroup group in __sections) {
        __items.Add(group, new List<IDevice>());
      }

      SetDevices(devices);
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return __sections.Count;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return (nint)__items[__sections[(int)section]].Count;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      IDevice device = this[indexPath];

      nfloat ret = __cellHeights[CELL_DEVICE];

      if (true) { // Is expanded
        if (device is GaugeDevice) {
          nfloat sh = __cellHeights[CELL_SENSOR] * (((GaugeDevice)device).sensorCount - 1);
          if (sh < 0) {
            sh = 0;
          }

          ret += sh;
        }
      }

      return ret;
    }


    // Overridden from UITableViewSource
    public UIView GetViewForFooter(UITableView tableView, int section) {
      var sectionCell = (SectionCell)tableView.DequeueReusableCell(CELL_DEVICE);

      var deviceGroup = __sections[section];

      sectionCell.labelTitle.Text = deviceGroup.title;
      sectionCell.labelCounter.Text = __items[deviceGroup].Count + "";

//      sectionCell.Apply((int)RowsInSection(tableView, section), __sections[section].title, () => {
//        Log.D(this, "Section " + section + " clicked");
//      });
      return sectionCell;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      return __cellHeights[CELL_SECTION];
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      // TODO ahodder@appioninc.com: Erm... This should be fixed at some point
      if (!(this[indexPath] is GaugeDevice)) {
        throw new Exception("Cannot get set for device of type " + this[indexPath].GetType().Name);
      }

      Log.D(this, "updating cell");

      GaugeDevice gauge = (GaugeDevice)this[indexPath];

      // Gets the next cell or creates one from the registered nib.
      DeviceCell cell = (DeviceCell)tableView.DequeueReusableCell(CELL_DEVICE);

      UpdateDeviceCellToDevice(tableView, cell, true, gauge);

      return cell;
    }

    /// <summary>
    /// Sets the content of the table source.
    /// </summary>
    /// <param name="devices">Devices.</param>
    public void SetDevices(List<IDevice> devices) {
      foreach (DeviceGroup group in __sections) {
        __items[group].Clear();
      }

      foreach (IDevice device in devices) {
        DeviceGroup group = GetDeviceGroupForDevice(device);
        __items[group].Add(device);
      }

      tableView.ReloadData();
    }

    /// <summary>
    /// Updates the given DeviceManagerDeviceCell's content to that of the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    private void UpdateDeviceCellToDevice(UITableView tableView, DeviceCell deviceCell, bool expanded, GaugeDevice device) {
      deviceCell.iconDevice.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      deviceCell.labelDeviceType.Text = device.serialNumber.deviceModel.GetTypeString();
      deviceCell.labelDeviceName.Text = device.name;

      deviceCell.buttonDeviceToggleConnect.TouchUpInside += (object obj, EventArgs args) => {
        // TODO ahodder@appioninc.com: memory leak
        if (EConnectionState.Disconnected == device.connection.connectionState) {
          device.connection.Connect();
        } else {
          device.connection.Disconnect();
        }
      };

      deviceCell.activityDeviceConnection.Hidden = !(EConnectionState.Connecting == device.connection.connectionState);

      // Create a gesture that will allow the view to toggle its "expansion state"
      UITapGestureRecognizer tapper = new UITapGestureRecognizer(() => {
        deviceCell.viewContainerExpanded.Hidden = !deviceCell.viewContainerExpanded.Hidden;
        var hidden = deviceCell.viewContainerExpanded.Hidden;
        if (hidden) {
          ExpandDeviceCell(deviceCell.viewContainerExpanded);
        } else {
          CollapseDeviceCell(deviceCell.viewContainerExpanded);
        }

//        deviceCell.viewContainerExpanded.Hidden = !hidden;
      });
      // TODO ahodder@appioninc.com: memory leak
      deviceCell.viewDeviceContent.AddGestureRecognizer(tapper);

      if (expanded) {
        deviceCell.labelDeviceSerialNumber.Text = device.serialNumber.ToString();

        switch (device.type) {
          case EDeviceType.Gauge: {
            UpdateDeviceCellSensors(deviceCell.viewDeviceSensorContainer, (GaugeDevice)device);
            break;
          }
          default: {
            Log.E(this, "Failed to update device content");
            break;
          }
        }
      }
    }

    /// <summary>
    /// </summary>
    /// <param name="tableView"></param>
    /// <param name="container"></param>
    /// <param name="device"></param>
    private void UpdateDeviceCellSensors(UIView container, GaugeDevice gauge) {
      UIView[] subviews = container.Subviews;

      // Ensure we have the proper number of views in the container
      if (subviews.Length >= gauge.sensorCount) {
        for (int i = subviews.Length - 1; i >= gauge.sensorCount; i--) {
          subviews[i].RemoveFromSuperview();
        }
      }

      while (container.Subviews.Length < gauge.sensorCount) {
        UIView view = tableView.DequeueReusableCell(CELL_SENSOR);
        container.AddSubview(view);
      }

      for (int i = 0; i < container.Subviews.Length; i++) {
        UpdateSensorCellToSensor((SensorCell)container.Subviews[i], gauge[i]);
      }
    }

    /// <summary>
    /// Updates a gauge device sensor view to the given sensor.
    /// </summary>
    /// <param name="view"></param>
    /// <param name="sensor"></param>
    private void UpdateSensorCellToSensor(SensorCell view, Sensor sensor) {
      view.labelSensorMeasurement.Text = sensor.sensorType.GetTypeString();
      view.labelSensorMeasurement.Text = sensor.measurement.ToString();

      sensor.readingChanged += (Sensor obj, Scalar reading) => {
        // TODO ahodder@appioninc.com: Memory leak
        view.labelSensorMeasurement.Text = reading.ToString();
      };
    }

    /// <summary>
    /// Expands the device cell's content view.
    /// </summary>
    private void ExpandDeviceCell(UIView container) {
      // TODO ahodder@appioninc.com: Implement ExpandDeviceCell
    }

    /// <summary>
    /// Collapses the device cell's content view.
    /// </summary>
    /// <param name="container">Container.</param>
    private void CollapseDeviceCell(UIView container) {
      // TODO ahodder@appioninc.com: Implement CollapseDeviceCell
    }

    /// <summary>
    /// Maps the given device to a device group.
    /// </summary>
    /// <returns>The device group for device.</returns>
    /// <param name="device">Device.</param>
    private DeviceGroup GetDeviceGroupForDevice(IDevice device) {
      if (!device.isKnown && device.isNearby) {
        return NEW_DEVICES;
      } else if (device.isKnown && device.isNearby) {
        return AVAILABLE;
      } else if (EConnectionState.Connected == device.connection.connectionState) {
        return CONNECTED;
      } else if (EConnectionState.Broadcasting == device.connection.connectionState) {
        return LONG_RANGE;
      } else {
        return DISCONNECTED;
      }
    }

    /// <summary>
    /// The struct that will represent a section within the device source.
    /// </summary>
    internal struct DeviceGroup {
      public string title { get; private set; }
      public CGColor color { get; private set; }

      public DeviceGroup(string title, CGColor color) : this() {
        this.title = title;
        this.color = color;
      }
    } // End DeviceGroup
  } // End DeviceSource
}
