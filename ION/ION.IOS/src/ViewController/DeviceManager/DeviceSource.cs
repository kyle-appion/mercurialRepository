using System;
using System.Collections.Generic;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.UI;

namespace ION.IOS.ViewController.DeviceManager {
  /// <description>
  /// This source is used to provide device cells to the device manager view controller's
  /// UI table. However, because the table view really only work well with a section/row
  /// source relationship, and this source wishes three levels of nesting (DeviceCategory,
  /// Device, DeviceSensors), we had to create a way to allow the extra layer of content
  /// nesting.
  /// 
  /// To accomplish this, we simply use the first item of each category list as the 
  /// category cell descriptor.
  /// </description>
  /// <summary>
  /// 
  /// </summary>
  public class DeviceSource : UITableViewSource {
    private const string CELL_HEADER = "cellHeader";
    private const string CELL_DEVICE = "cellDevice";
    private const string CELL_SENSOR = "cellSensor";
    private const string CELL_SERIAL_NUMBER = "cellSerialNumber";

    /// <summary>
    /// The delegate that is called when a sensor's add button is clicked.
    /// </summary>
    public delegate bool OnSensorAddClicked(GaugeDeviceSensor sensor, NSIndexPath indexPath);

    public delegate bool OnDeviceLongClicked(IDevice device, NSIndexPath indexPath);
    /// <summary>
    /// The action that will be called on a sensor's add button click.
    /// </summary>
    /// <value>The on sensor add clicked.</value>
    public OnSensorAddClicked onSensorAddClicked { get; set; }

    /// <summary>
    /// The filter that will only display matching sensors.
    /// </summary>
    public IFilter<Sensor> sensorFilter {
      get {
        return __sensorFilter;
      }
      set {
        if (value == null) {
          value = new YesFilter<Sensor>();
        }

        __sensorFilter = value;
      }
    } IFilter<Sensor> __sensorFilter = new YesFilter<Sensor>();

    /// <summary>
    /// The maximum number of devices that can be expanded.
    /// </summary>
    /// <value>The max expanded.</value>
    public int maxExpanded {
      get {
        return __maxExpanded;
      }
      set {
        __maxExpanded = value;
        if (__maxExpanded < 1) {
          __maxExpanded = 1;
        }
        EnsureExpansionCount();
      }
    } int __maxExpanded = 1;

    /// <summary>
    /// The ION context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The table view that this source is providing cells to.
    /// </summary>
    /// <value>The table.</value>
    private UITableView table { get; set; }
    /// <summary>
    /// The items that the source will provide cells for.
    /// </summary>
    private List<Item> __items = new List<Item>();
    /// <summary>
    /// The queue of items that have been expanded.
    /// </summary>
    private List<IDevice> __expansionHistory = new List<IDevice>();


    public DeviceSource(IION ion, UITableView table) {
      this.ion = ion;
      this.table = table;
    }

    // Overriddenfrom UITableViewsource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          // Remove the device from the device manager
          var index = (int)indexPath.Section;
          var item = __items[index];
          if (item is DeviceItem) {
            var di = (DeviceItem)item;
            ion.deviceManager.DeleteDevice(di.device.serialNumber);
          }

          // Remove the device from the source
          __items.RemoveAt(index);
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Left);
//          tableView.ReloadData();
          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return false;
    }

    // Overridden from UITableViewSource
    public override void CellDisplayingEnded(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
      var releasable = cell as IReleasable;
      if (releasable != null) {
        releasable.Release();
      }
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return __items.Count;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      var item = __items[(int)section];
      switch (item.type) {
        case SectionType.Device:
          var deviceItem = item as DeviceItem;
          if (IsDeviceExpanded(deviceItem.device)) {
            var count = deviceItem.sensors.Count;
            if (count > 0) {
              return count + 1;
            } else {
              return 0;
            }
          } else {
            return 0;
          }
          /*
          if (IsDeviceExpanded(deviceItem.device)) {
            var gaugeDevice = deviceItem.device as GaugeDevice;
            return gaugeDevice.sensorCount + 1;
          } else {
            return 0;
          }
          */
        default:
          return 0;
      }
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      // TODO ahodder@appioninc.com: Make more abstract
      var item = __items[(int)section];
      switch (item.type) {
        case SectionType.Device:
          return 48;
        default:
          return 48;
      }
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      // TODO ahodder@appioninc.com: Make more abstract
      if (indexPath.Row == 0) {
        return 21;
      } else {
        return 48;
      }
    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      var item = __items[(int)section];

      switch (item.type) {
        case SectionType.Device:
          return GetDeviceCell(tableView, section, item as DeviceItem);
        case SectionType.Header:
          return GetHeaderCell(tableView, section, item as HeaderItem);
        default:
          throw new Exception("Cannot GetCell: " + item.type + " was not expected and does not have a view.");
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var deviceItem = __items[(int)indexPath.Section] as DeviceItem;

      if (indexPath.Row == 0) {
        // This is the serial number display row.
        var cell = tableView.DequeueReusableCell(CELL_SERIAL_NUMBER) as SerialNumberTableCell;

        cell.UpdateTo(deviceItem.device);

        return cell;
      } else {
        // Use this instead of indexPath.Row as the cells are offset by one to accomodate serial number
        var row = (int)indexPath.Row - 1;
        if (!(deviceItem.device is GaugeDevice)) {
          throw new Exception("Cannot get cell: expected gauge device, received " + deviceItem.device.GetType().Name);
        }

        var gaugeDevice = deviceItem.device as GaugeDevice;
        var cell = tableView.DequeueReusableCell(CELL_SENSOR) as SensorTableCell;
        var sensor = gaugeDevice[row];

        cell.UpdateTo(sensor, () => {
          if (onSensorAddClicked != null) {
            if (!ion.deviceManager.IsDeviceKnown(gaugeDevice)) {
//              ion.deviceManager.ConnectDeviceAsync(gaugeDevice);
              gaugeDevice.connection.Connect();
            }
            onSensorAddClicked(sensor, indexPath);
          }
        });

        return cell;
      }
    }

    /// <summary>
    /// Sets the device content for this source. This will cause a data reload
    /// for the source.
    /// </summary>
    /// <param name="groups">Groups.</param>
    public void SetContent(List<DeviceGroup> groups) {
      __items = new List<Item>();

      foreach (DeviceGroup group in groups) {
        __items.Add(new HeaderItem(group));
        foreach (IDevice device in group.devices) {
          var item = new DeviceItem(device);
          if (item.device is GaugeDevice) {
            var gauge = (GaugeDevice)item.device;
            foreach (var sensor in gauge.sensors) {
              if (sensorFilter.Matches(sensor)) {
                item.sensors.Add(sensor);
              }
            }
          }
          __items.Add(item);
        }
      }

      table.ReloadData();
    }

    /// <summary>
    /// Queries the index path of the given device.
    /// </summary>
    /// <returns>The index path of device or null if the device is not in the source.</returns>
    /// <param name="device">Device.</param>
    public NSIndexPath GetIndexPathOfDevice(IDevice device) {
      for (int i = 0; i < __items.Count; i++) {
        var deviceItem = __items[i] as DeviceItem;
        if (deviceItem == null) {
          continue;
        }
        if (deviceItem.device.Equals(device)) {
          return NSIndexPath.FromIndex((nuint)i);
        }
      }

      return null;
    }

    /// <summary>
    /// Queries whether or not the given device is expanded.
    /// </summary>
    /// <returns><c>true</c> if this instance is device expanded the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    public bool IsDeviceExpanded(IDevice device) {
      return __expansionHistory.Contains(device);
    }

    /// <summary>
    /// Expands the given device. If the number of expanded devices
    /// exceeds the max number of expanded items, then we will collpase the oldest
    /// expanded items. If the given index path does not point to a valid device item,
    /// then the expand will silently fail.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public void ExpandDevice(IDevice device) {
      var deviceItem = __items[(int)GetIndexPathOfDevice(device).Section] as DeviceItem;
      if (deviceItem == null || IsDeviceExpanded(device)) {
        return;
      }
      __expansionHistory.Add(device);
      EnsureExpansionCount();
      table.ReloadData();
    }

    /// <summary>
    /// Collapses the given device. If the given index path does not
    /// point to a valid device item, then the expand will silently fail.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public void CollapseDevice(IDevice device) {
      var deviceItem = __items[(int)GetIndexPathOfDevice(device).Section] as DeviceItem;
      if (deviceItem == null || !IsDeviceExpanded(device)) {
        return;
      }

      __expansionHistory.Remove(deviceItem.device);
      table.ReloadData();
    }

    /// <summary>
    /// Toggles whether or not the given index path is expanded. If the given index path
    /// does not point to a valid device item, then the toggle will silently fail.
    /// </summary>
    /// <param name="indexPath">Index path.</param>
    public void ToggleDevice(NSIndexPath indexPath) {
      var deviceItem = __items[(int)indexPath.Section] as DeviceItem;
      if (deviceItem == null) {
        return;
      }
      if (IsDeviceExpanded(deviceItem.device)) {
        CollapseDevice(deviceItem.device);
      } else {
        ExpandDevice(deviceItem.device);
      }
    }

    /// <summary>
    /// Toggles the expansion state of the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    public void ToggleDevice(IDevice device) {
      ToggleDevice(GetIndexPathOfDevice(device));
    }

    /// <summary>
    /// If we have too many devices expanded, we will start collapsing the oldest expanded
    /// items.
    /// </summary>
    private void EnsureExpansionCount() {
      while (maxExpanded < __expansionHistory.Count) {
        var device = __expansionHistory[0];
        __expansionHistory.RemoveAt(0);
        CollapseDevice(device);
      }
    }

    /// <summary>
    /// Gets the DeviceItem's cell view (constructing one if necessary).
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    /// <param name="item">Item.</param>
    private UITableViewCell GetDeviceCell(UITableView tableView, nint section, DeviceItem item) {
      var cell = tableView.DequeueReusableCell(CELL_DEVICE) as DeviceTableCell;

      cell.UpdateTo(ion, item.device, () => {
        ToggleDevice(NSIndexPath.FromRowSection(0, section));
      });

      cell.onBackgroundLongClicked = () => {
        Log.D(this, "Long clicky");
      };

      return cell;
    }

    /// <summary>
    /// Gets the HeaderItem's cell view (constructing one if necessary).
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    /// <param name="item">Item.</param>
    private UITableViewCell GetHeaderCell(UITableView tableView, nint section, HeaderItem item) {
      var cell = tableView.DequeueReusableCell(CELL_HEADER) as DeviceSectionHeaderTableCell;

      cell.UpdateTo(item.group);

      return cell;
    }
  } // End DeviceSource

  /// <summary>
  /// The class that represents a grouping of devices.
  /// </summary>
  public class DeviceGroup {
    public string title { get; private set; }
    public CGColor color { get; private set; }
    public List<IDevice> devices { get; private set; }

    public DeviceGroup(string title, CGColor color) {
      this.title = title;
      this.color = color;
      this.devices = new List<IDevice>();
    }
  } // End DeviceGroup

  /// <summary>
  /// The enumeration of what type item is provided to a section cell.
  /// </summary>
  internal enum SectionType {
    Device,
    Header,
  } // End SectionType

  /// <summary>
  /// The items contract. All section items must implement this to properly be used by
  /// the table view source.
  /// </summary>
  internal interface Item {
    SectionType type { get; }
  } // End Item

  internal class DeviceItem : Item {
    public SectionType type { get { return SectionType.Device; } }
    public IDevice device { get; private set; }
    public List<GaugeDeviceSensor> sensors = new List<GaugeDeviceSensor>();

    public DeviceItem(IDevice device) {
      this.device = device;
    }
  } // End DeviceItem

  internal class HeaderItem : Item {
    public SectionType type { get { return SectionType.Header; } }
    public DeviceGroup group { get; private set; }

    public HeaderItem(DeviceGroup group) {
      this.group = group;
    }
  } // End HeaderItem
}

