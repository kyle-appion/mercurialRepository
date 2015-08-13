using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;


using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Devices;
using ION.IOS.UI;
using ION.IOS.Util;
using ION.IOS.ViewController;

namespace ION.IOS.ViewController.Main {
  /*
  /// <summary>
  /// The table source that will provide the cell views for the device
  /// manager's table view 
  /// </summary>
  public class DeviceManagerSource : UITableViewSource {
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
//    private const string CELL_SENSOR = "cellSensor";
    private const string VIEW_SENSOR = "sensorView";

    /// <summary>
    /// The indexer that will retrieve an IDevice from the source
    /// at the given index.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public DeviceItem this[NSIndexPath indexPath] {
      get {
        return __items[__sections[indexPath.Section]][indexPath.Row];
      }
    }

    /// <summary>
    /// The ion context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
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
    private  Dictionary<DeviceGroup, List<DeviceItem>> __items;
    /// <summary>
    /// A lookup table for the heights of all the cells.
    /// </summary>
    private Dictionary<string, nfloat> __cellHeights = new Dictionary<string, nfloat>();


    public DeviceManagerSource(IION ion, UITableView tableView, List<IDevice> devices) {
      this.ion = ion;
      this.tableView = tableView;

      __cellHeights[CELL_SECTION] = tableView.DequeueReusableCell(CELL_SECTION).Frame.Size.Height;
      __cellHeights[CELL_DEVICE] = (nint)83;//tableView.DequeueReusableCell(CELL_DEVICE).Frame.Size.Height;
      __cellHeights[VIEW_SENSOR] = (nint)68;//tableView.DequeueReusableCell(CELL_SENSOR).Frame.Size.Height;

      __sections = new List<DeviceGroup>(GROUPS);
      __items = new Dictionary<DeviceGroup, List<DeviceItem>>();
      foreach (DeviceGroup group in __sections) {
        __items.Add(group, new List<DeviceItem>());
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
      DeviceItem item = this[indexPath];

      nfloat ret = __cellHeights[CELL_DEVICE];

      if (item.expanded) { // Is expanded
        if (item.device is GaugeDevice) {
          nfloat sh = __cellHeights[VIEW_SENSOR] * (((GaugeDevice)item.device).sensorCount);
          if (sh < 0) {
            sh = 0;
          }

          ret += sh;
        }
      }

      return ret;
    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      var sectionCell = (SectionCell)tableView.DequeueReusableCell(CELL_SECTION);

      var deviceGroup = __sections[(int)section];

      sectionCell.labelTitle.Text = deviceGroup.title;
      sectionCell.cellBackground.BackgroundColor = new UIColor(deviceGroup.color);
      sectionCell.labelCounter.Text = __items[deviceGroup].Count + "";

      return sectionCell;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      var ret = __cellHeights[CELL_SECTION];
      return ret;
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      DeviceItem item = this[indexPath];

      // Gets the next cell or creates one from the registered nib.
      DeviceCell cell = (DeviceCell)tableView.DequeueReusableCell(CELL_DEVICE);

      UpdateDeviceCellToDevice(tableView, indexPath, cell, item);

      return cell;
    }

    /// <summary>
    /// Reloads the given cell.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public void ReloadCell(NSIndexPath indexPath) {
      tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
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
        DeviceItem item = new DeviceItem(device);

        if (item.device is GaugeDevice) {
          __items[group].Add(item);
        } else {
          Log.E(this, "Unknown device " + item + ": not adding to source.");
        }
      }

      tableView.ReloadData();
    }

    /// <summary>
    /// Updates the given DeviceManagerDeviceCell's content to that of the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    private void UpdateDeviceCellToDevice(UITableView tableView, NSIndexPath indexPath, DeviceCell deviceCell, DeviceItem item) {
      var device = item.device as GaugeDevice;
      if (device == null) {
        // TODO ahodder@appioninc.com: resolve unknown device
        Log.E(this, "Failed to create view for non-gauge device: " + item.device);
      }
      // Attach listeners
      deviceCell.connectClicked = () => {
        if (EConnectionState.Disconnected == device.connection.connectionState) {
          ion.deviceManager.ConnectDeviceAsync(device);
          ion.currentWorkbench.Add(new ION.Core.Content.Manifold(device[0]));
        } else {
          device.connection.Disconnect();
        }
        ReloadCell(indexPath);
      };

      deviceCell.collapseToggleClicked = () => {
        item.expanded = !item.expanded;
        tableView.ReloadData();
      };

      if (EConnectionState.Connecting == item.device.connection.connectionState ||
        EConnectionState.Resolving == item.device.connection.connectionState) {
        deviceCell.activityDeviceConnection.Hidden = false;
      } else {
        deviceCell.activityDeviceConnection.Hidden = true;
      }

      deviceCell.viewContainerExpanded.Hidden = !item.expanded;
      deviceCell.iconDevice.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      deviceCell.labelDeviceType.Text = device.serialNumber.deviceModel.GetTypeString();
      deviceCell.labelDeviceName.Text = device.name;
      deviceCell.activityDeviceConnection.Hidden = !(EConnectionState.Connecting == device.connection.connectionState);

      deviceCell.labelDeviceSerialNumber.Text = device.serialNumber.ToString();

      switch (device.type) {
        case EDeviceType.Gauge: {
            UpdateGaugeDeviceCellSensors(deviceCell.viewDeviceSensorContainer, (GaugeDevice)device);
            break;
          }
        default: {
            Log.E(this, "Failed to update device content");
            break;
          }
      }
    }

    /// <summary>
    /// </summary>
    /// <param name="tableView"></param>
    /// <param name="container"></param>
    /// <param name="device"></param>
    private void UpdateGaugeDeviceCellSensors(UIView container, GaugeDevice gauge) {
      UIView[] subviews = container.Subviews;

      // Ensure we have the proper number of views in the container
      if (subviews.Length >= gauge.sensorCount) {
        for (int i = subviews.Length - 1; i >= gauge.sensorCount; i--) {
          var view = subviews[i] as SensorCell;
          view.RemoveFromSuperview();
        }
      }

      while (container.Subviews.Length < gauge.sensorCount) {
        UIView view = SensorView.NIB.Instantiate(tableView, null)[0] as UIView;
        container.AddSubview(view);
      }

      var frame = container.Frame;
      var newHeight = (nint)48 * container.Subviews.Length;
      container.Frame = new CGRect(frame.Left, frame.Top, frame.Width, newHeight);

      for (int i = 0; i < gauge.sensorCount; i++) {
        UpdateSensorCellToSensor((SensorView)container.Subviews[i], gauge[i++]);
      }
    }

    /// <summary>
    /// Updates a gauge device sensor view to the given sensor.
    /// </summary>
    /// <param name="view"></param>
    /// <param name="sensor"></param>
    private void UpdateSensorCellToSensor(SensorView view, Sensor sensor) {
      view.labelSensorType.Text = sensor.sensorType.GetTypeString();
      view.labelSensorMeasurement.Text = sensor.measurement.ToString();
      view.Update(sensor);

      var f = view.buttonWorkbench.Frame;
      //      Log.D(this, "Workbench's Frame {l:" + f.Left + ", t:" + f.Top + ", r:" + f.Right + ", b:" + f.Bottom + "}");
      view.onAddToWorkbenchClicked = () => {
        Toast.New(tableView, "Add to workbench");
      };

      f = view.buttonAnalyzer.Frame;
      //      Log.D(this, "Analyzer's Frame {l:" + f.Left + ", t:" + f.Top + ", r:" + f.Right + ", b:" + f.Bottom + "}");
      view.onAddToAnalyzerClicked = () => {
        Toast.New(tableView, "Add to analyzer");
      };
    }

    /// <summary>
    /// Maps the given device to a device group.
    /// </summary>
    /// <returns>The device group for device.</returns>
    /// <param name="device">Device.</param>
    private DeviceGroup GetDeviceGroupForDevice(IDevice device) {
      if (EConnectionState.Connected == device.connection.connectionState) {
        return CONNECTED;
      } else if (!device.isKnown && device.isNearby) {
        return NEW_DEVICES;
      } else if (device.isKnown && device.isNearby) {
        return AVAILABLE;
      } else if (EConnectionState.Broadcasting == device.connection.connectionState) {
        return LONG_RANGE;
      } else {
        return DISCONNECTED;
      }
    }
  } // End DeviceManagerSource

  /// <summary>
  /// The struct that will represent a section within the device source.
  /// </summary>
  public class DeviceGroup {
    public string title { get; private set; }
    public CGColor color { get; private set; }

    public DeviceGroup(string title, CGColor color) {
      this.title = title;
      this.color = color;
    }
  } // End DeviceGroup

  /// <summary>
  /// The struct that will represent a device with a device group.
  /// </summary>
  public class DeviceItem {
    public IDevice device { get; private set; }
    public bool expanded { get; set; }
    public int expandedHeight { get; set; }

    public DeviceItem(IDevice device) {
      this.device = device;
    }
  }
  */
}

