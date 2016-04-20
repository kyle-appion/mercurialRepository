namespace ION.IOS.ViewController.DeviceManager {
  
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using SWTableViewCell;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.IOS.UI;
  using ION.IOS.Util;

  public class DeviceTableSource : UITableViewSource, IReleasable {
    /// <summary>
    /// The delegate that is called when a sensor's add button is clicked.
    /// </summary>
    public delegate bool OnSensorAddClicked(GaugeDeviceSensor sensor, NSIndexPath indexPath);

    private const string CELL_HEADER = "cellHeader";
    private const string CELL_DEVICE = "cellDevice";
    private const string CELL_SENSOR = "cellSensor";
    private const string CELL_SERIAL_NUMBER = "cellSerialNumber";
    private const string CELL_SPACE = "cellSpace";

    /// <summary>
    /// The action that will be called on a sensor's add button click.
    /// </summary>
    /// <value>The on sensor add clicked.</value>
    public OnSensorAddClicked onSensorAddClicked { get; set; }

    /// <summary>
    /// The ION context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device manager view controller that owns this source.
    /// </summary>
    /// <value>The vc.</value>
    private DeviceManagerViewController vc { get; set; }
    /// <summary>
    /// The table view that this source is working with.
    /// </summary>
    /// <value>The table.</value>
    private UITableView table { get; set; }
    /// <summary>
    /// The filter that will prevent some sensors from being shown.
    /// </summary>
    /// <value>The sensor filter.</value>
    private IFilter<Sensor> sensorFilter { get; set; }
    /// <summary>
    /// The list of sections in the view.
    /// </summary>
    private List<Section> sections = new List<Section>();
    /// <summary>
    /// The current expanded device.
    /// </summary>
    /// <value>The expanded device.</value>
    private IDevice expandedDevice { get; set; }
   

    public DeviceTableSource(IION ion, DeviceManagerViewController vc, UITableView table) {
      this.ion = ion;
      this.vc = vc;
      this.table = table;

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
    }

    // Overridden from UITableViewSource
    public void Release() {
      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
    }

    // Overridden from UITableViewSource
    public override void CellDisplayingEnded(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
      var r = cell as IReleasable;
      if (r != null) {
        r.Release();
      }
    }

    // Overridden from UITableViewSource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      var record = sections[(int)indexPath.Section].records[(int)indexPath.Row];

      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          if (record is DeviceRecord) {
            RequestDeleteDeviceVerification(((DeviceRecord)record).device);
          }
          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath path) {
      return sections[(int)path.Section].records[(int)path.Row] is DeviceRecord;
    }

    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return sections.Count;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint sectionIndex) {
      var section = sections[(int)sectionIndex];
      return section.records.Count;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      var section = sections[(int)indexPath.Section];
      var record = section.records[(int)indexPath.Row];

      if (record is DeviceRecord) {
        var r = record as DeviceRecord;

        if (r.expanded) {
          DoCollapse(indexPath);
        } else {
          DoExpand(indexPath);
          if (expandedDevice != null && expandedDevice != r.device) {
            var ip = IndexPathOfDevice(expandedDevice);
            if (ip != null) {
              DoCollapse(ip);
            }
          }
          expandedDevice = r.device;
        }
      }
    }

    // Overriddn from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      return 48;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      var section = sections[indexPath.Section];
      var record = section.records[indexPath.Row];

      if (record is SerialNumberRecord) {
        return 21;
      } else if (record is DeviceRecord) {
        return 48;
      } else if (record is SensorRecord) {
        return 48;
      } else if (record is SpaceRecord) {
        return 5;
      } else {
        throw new Exception("Cannot get row height for: " + record);
      }
    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint sectionIndex) {
      var section = sections[(int)sectionIndex];

      var cell = tableView.DequeueReusableCell(CELL_HEADER) as DeviceSectionHeaderTableCell;
      var w = tableView.Frame.Size.Width;
      var f = cell.Frame;
      f.Width = w;
      cell.Frame = f;
      cell.UpdateTo(section);

      var ret = new UIView();
      ret.AddSubview(cell);

      return ret;
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var section = sections[indexPath.Section];
      var record = section.records[indexPath.Row];

      if (record is DeviceRecord) {
        var r = record as DeviceRecord;

        var cell = tableView.DequeueReusableCell(CELL_DEVICE) as DeviceTableCell;
        cell.UpdateTo(ion, r);
        cell.BackgroundColor = UIColor.Clear;

        return cell;
      } else if (record is SerialNumberRecord) {
        var r = record as SerialNumberRecord;

        var cell = tableView.DequeueReusableCell(CELL_SERIAL_NUMBER) as SerialNumberTableCell;
        cell.UpdateTo(r);

        return cell;
      } else if (record is SensorRecord) {
        var r = record as SensorRecord;

        var cell = tableView.DequeueReusableCell(CELL_SENSOR) as SensorTableCell;
        cell.UpdateTo(r, () => {
          if (onSensorAddClicked != null) {
            if (!ion.deviceManager.IsDeviceKnown(r.sensor.device)) {
              r.sensor.device.connection.ConnectAsync();
            }
            onSensorAddClicked(r.sensor, indexPath);
          }
        });

        return cell;
      } else if (record is SpaceRecord) {
        var cell = tableView.DequeueReusableCell(CELL_SPACE);

        cell.BackgroundColor = UIColor.Clear;

        return cell;
      } else {
        throw new Exception("Cannot get cell for record: " + record);
      }
    }

    /// <summary>
    /// Rebuilds the content for this source.
    /// </summary>
    public void RefreshContent() {
      var dict = new Dictionary<EDeviceState, Section>();
      dict.Add(EDeviceState.Connected, new Section(EDeviceState.Connected, Strings.Device.CONNECTED.FromResources(), Colors.GREEN));
      dict.Add(EDeviceState.Broadcasting, new Section(EDeviceState.Broadcasting, Strings.Device.LONG_RANGE.FromResources(), Colors.LIGHT_BLUE));
      dict.Add(EDeviceState.New, new Section(EDeviceState.New, Strings.Device.NEW_DEVICES.FromResources(), Colors.LIGHT_GRAY));
      dict.Add(EDeviceState.Available, new Section(EDeviceState.Available, Strings.Device.AVAILABLE.FromResources(), Colors.YELLOW));
      dict.Add(EDeviceState.Disconnected, new Section(EDeviceState.Disconnected, Strings.Device.DISCONNECTED.FromResources(), Colors.RED));

      foreach (var device in ion.deviceManager.devices) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          if (sensorFilter == null || gd.HasSensorMatchingFilter(sensorFilter)) {
            var r = new DeviceRecord(gd, expandedDevice == device);
            var state = GetStateFor(device);
            var section = dict[state];
            section.records.Add(r);

            if (r.expanded) {
              section.records.Add(new SerialNumberRecord(gd.serialNumber));
              foreach (var sensor in ((GaugeDevice)device).sensors) {
                if (sensorFilter.Matches(sensor)) {
                  section.records.Add(new SensorRecord(sensor));
                }
              }
            }

            section.records.Add(new SpaceRecord());
          }
        }
      }

      sections.Clear();

      foreach (EDeviceState state in Enum.GetValues(typeof(EDeviceState))) {
        var section = dict[state];
        if (section.records.Count > 0) {
          sections.Add(section);
        }
      }

      vc.labelEmpty.Hidden = sections.Count != 0;
      table.ReloadData();
    }

    /// <summary>
    /// Builds the NSIndexPath for the given device. If the device is not in the source, then the 
    /// returned path will be null.
    /// </summary>
    /// <returns>The path of device.</returns>
    /// <param name="device">Device.</param>
    public NSIndexPath IndexPathOfDevice(IDevice device) {
      for  (int s = 0; s < sections.Count; s++) {
        var section = sections[s];
        for (int r = 0; r < section.records.Count; r++) {
          var dr = section.records[r] as DeviceRecord;
          if (dr != null && dr.device == device) {
            return NSIndexPath.FromRowSection((nint)r, (nint)s);
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Sets the filter that will filter out all devices that do not match.
    /// </summary>
    /// <param name="sensorFilter">Sensor filter.</param>
    public void SetSensorFilter(IFilter<Sensor> sensorFilter) {
      this.sensorFilter = sensorFilter;
      RefreshContent();
    }

    private void DoExpand(NSIndexPath indexPath, bool animate = true) {
      var section = sections[(int)indexPath.Section];
      var record = section.records[(int)indexPath.Row] as DeviceRecord;
      var gd = record.device as GaugeDevice;

      if (record.expanded) {
        return;
      }

      record.expanded = true;

      var start = (int)indexPath.Row;

      var index = start;

      section.records.Insert(++index, new SerialNumberRecord(gd.serialNumber));
      foreach (var sensor in gd.sensors) {
        if (sensorFilter.Matches(sensor)) {
          section.records.Insert(++index, new SensorRecord(sensor));
        }
      }

      if (animate) {
        table.InsertRows(ToNSIndexPath(Arrays.Range(start + 1, index), (int)indexPath.Section), UITableViewRowAnimation.Top);
      }
    }

    private void DoCollapse(NSIndexPath indexPath, bool animate = true) {
      var section = sections[(int)indexPath.Section];
      var record = section.records[(int)indexPath.Row] as DeviceRecord;
      var gd = record.device as GaugeDevice;

      if (!record.expanded) {
        return;
      }

      record.expanded = false;

      var start = (int)indexPath.Row;

      var count = gd.GetSensorsMatchingFilter(sensorFilter).Count + 1;
      section.records.RemoveRange(start + 1, count);

      if (animate) {
        table.DeleteRows(ToNSIndexPath(Arrays.Range(start + 1, start + count), (int)indexPath.Section), UITableViewRowAnimation.Top);
      }
    }

    /// <summary>
    /// Converts the given ints to a NSIndexPath array.
    /// </summary>
    /// <returns>The NS index path.</returns>
    /// <param name="ints">Ints.</param>
    private NSIndexPath[] ToNSIndexPath(int[] ints, int section = 0) {
      var ret = new NSIndexPath[ints.Length];

      for (int i = 0; i < ret.Length; i++) {
        ret[i] = NSIndexPath.FromRowSection((nint)ints[i], (nint)section);
      }

      return ret;
    }

    /// <summary>
    /// Called when the device manager raised a new device manager event.
    /// </summary>
    /// <param name="e">E.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      if (DeviceManagerEvent.EType.DeviceEvent == e.type) {
        OnDeviceEvent(e.deviceEvent);
      }
    }

    /// <summary>
    /// Called when a device event is received. This is generally called from
    /// OnDeviceManagerEvent.
    /// </summary>
    /// <param name="e">E.</param>
    private void OnDeviceEvent(DeviceEvent e) {
      var device = e.device;
      switch (e.type) {
        case DeviceEvent.EType.NameChanged:
          table.ReloadRows(new NSIndexPath[] { IndexPathOfDevice(device) }, UITableViewRowAnimation.Top);
          break;
          //          case DeviceEvent.EType.NewData:
          //            table.ReloadRows(new NSIndexPath[] { IndexPathOfDevice(device) }, UITableViewRowAnimation.Top);
          //            break;
        default:
          // Found, ConnectionChange and Deleted
          // TODO ahodder@appioninc.com: Make more pretty
          RefreshContent();
          break;
      }
    }

    /// <summary>
    /// Queries the device state of the given device.
    /// </summary>
    /// <returns>The state for.</returns>
    /// <param name="device">Device.</param>
    private EDeviceState GetStateFor(IDevice device) {
      var c = device.connection;
      if (EConnectionState.Connected == c.connectionState) {
        return EDeviceState.Connected;
      } else if (EConnectionState.Broadcasting == c.connectionState) {
        return EDeviceState.Broadcasting;
      } else if (!ion.deviceManager.IsDeviceKnown(device) && device.isNearby) {
        return EDeviceState.New;
      } else if (ion.deviceManager.IsDeviceKnown(device) && device.isNearby) {
        return EDeviceState.Available;
      } else {
        return EDeviceState.Disconnected;
      }
    }

    private string GetForgetHeader(IDevice device) {
      if (ion.currentWorkbench.ContainsDevice(device)) {
        return String.Format(Strings.Device.FORGET_NAME_WHERE, device.name, Strings.Device.IN_WORKBENCH);
      } else {
        return String.Format(Strings.Device.FORGET, device.name);
      }
    }

    /// <summary>
    /// Requests whether or not the user is really sure they want to delete the device.
    /// </summary>
    /// <param name="device">Device.</param>
    private void RequestDeleteDeviceVerification(IDevice device) {
      var dialog = UIAlertController.Create(GetForgetHeader(device), Strings.Device.FORGET_DESC, UIAlertControllerStyle.Alert);

      dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));
      dialog.AddAction(UIAlertAction.Create(Strings.Device.FORGET, UIAlertActionStyle.Default, (action) => {
        ion.deviceManager.DeleteDevice(device.serialNumber);
      }));

      dialog.Show();
    }

    /// <summary>
    /// The enumeration of the types of views that are in the table source.
    /// </summary>
    public enum EViewType {
      Space,
      Device,
      Sensor,
      SerialNumber,
    }

    /// <summary>
    /// Compares two device records by serial number.
    /// </summary>
    private class Comperer : IComparer<DeviceRecord> {
      // Overridden from IComparer
      public int Compare(DeviceRecord first, DeviceRecord second) {
        return first.device.serialNumber.CompareTo(second.device.serialNumber);
      }
    }
  }

  public interface IDeviceTableRecord {
    DeviceTableSource.EViewType viewType { get; }
  }

  public class SpaceRecord : IDeviceTableRecord {
    public DeviceTableSource.EViewType viewType { get { return DeviceTableSource.EViewType.Space; } }
  }

  /// <summary>
  /// The enumeration of the possible states that a device can be in.
  /// </summary>
  public enum EDeviceState {
    Connected,
    Broadcasting,
    New,
    Available,
    Disconnected,
  }

  /// <summary>
  /// The object that represents as section.
  /// </summary>
  public class Section {
    /// <summary>
    /// The state that all the sections devices are in.
    /// </summary>
    public EDeviceState state;
    /// <summary>
    /// The name of the section.
    /// </summary>
    public string name;
    /// <summary>
    /// The color of the section.
    /// </summary>
    public CGColor color;
    /// <summary>
    /// The list of records that are in the section.
    /// </summary>
    public List<IDeviceTableRecord> records = new List<IDeviceTableRecord>();

    public Section(EDeviceState state, string name, CGColor color) {
      this.state = state;
      this.name = name;
      this.color = color;
    }
  }
}

