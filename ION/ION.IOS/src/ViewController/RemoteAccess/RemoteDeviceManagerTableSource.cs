namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;
  using System.Collections.Generic;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  //  using SWTableViewCell;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Internal;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.DeviceManager;

  public class RemoteDeviceManagerTableSource : UITableViewSource, IReleasable {
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
    /// <param name="sensor">Sensor.</param>
    /// <param name="indexPath">Index path.</param>
    public OnSensorAddClicked onSensorAddClicked;

    public IFilter<Sensor> sensorFilter {
      get {
        return __sensorFilter;
      }

      set {
        __sensorFilter = value;
        foreach (var section in allSections.Values) {
          section.sensorFilter = value;
        }
        Reload();
      }
    } private IFilter<Sensor> __sensorFilter;

    /// <summary>
    /// The mapping of devices to the section that they are currently present in.
    /// </summary>
    private Dictionary<IDevice, Section> deviceToSection = new Dictionary<IDevice, Section>();
    /// <summary>
    /// The lookup table for the sections.
    /// </summary>
    private Dictionary<EDeviceState, Section> allSections = new Dictionary<EDeviceState, Section>();
    /// <summary>
    /// The list of the shown sections.
    /// </summary>
    private List<Section> shownSections = new List<Section>();
    /// <summary>
    /// The current expanded device.
    /// </summary>
    private IDevice expandedDevice;

    /// <summary>
    /// The app context that the source is using.
    /// </summary>
    /// <value>The ion.</value>
    internal IION ion { get; private set; }
    /// <summary>
    /// The view controller that has presented this source.
    /// </summary>
    /// <value>The vc.</value>
    internal RemoteDeviceManagerViewController vc { get; private set; }
    /// <summary>
    /// The table view that the source belongs to.
    /// </summary>
    internal UITableView tableView { get; private set; }

    public RemoteDeviceManagerTableSource(IION ion, RemoteDeviceManagerViewController vc, UITableView tableView) {
      this.ion = ion;
      this.vc = vc;
      this.tableView = tableView;
      var connected = new Section(EDeviceState.Connected, Strings.Device.CONNECTED.FromResources(), Colors.GREEN);
      connected.actions = BuildBatchOptionsDialog(Actions.AddAllToWorkbench,Strings.Device.Manager.AVAILABLE_ACTIONS, connected);
      //connected.actions = BuildBatchOptionsDialog(Actions.DisconnectAll | Actions.ForgetAll | Actions.AddAllToWorkbench,
      //  Strings.Device.Manager.CONNECTED_ACTIONS, connected);
      //allSections.Add(EDeviceState.Connected, connected);

      //var broadcasting = new Section(EDeviceState.Broadcasting, Strings.Device.LONG_RANGE.FromResources(), Colors.LIGHT_BLUE);
      //broadcasting.actions = BuildBatchOptionsDialog(Actions.ConnectAll | Actions.AddAllToWorkbench,
      //  Strings.Device.Manager.BROADCASTING_ACTIONS, broadcasting);
      //allSections.Add(EDeviceState.Broadcasting, broadcasting);

      //var @new = new Section(EDeviceState.New, Strings.Device.NEW_DEVICES.FromResources(), Colors.LIGHT_GRAY);
      //@new.actions = BuildBatchOptionsDialog(Actions.ConnectAll, Strings.Device.Manager.NEW_ACTIONS, @new);
      //allSections.Add(EDeviceState.New, @new);

      var avail = new Section(EDeviceState.Available, Strings.Device.AVAILABLE.FromResources(), Colors.YELLOW);
      @avail.actions = BuildBatchOptionsDialog(Actions.ConnectAll | Actions.ForgetAll,
        Strings.Device.Manager.AVAILABLE_ACTIONS, avail);
      allSections.Add(EDeviceState.Available, avail);

      //var disconnected = new Section(EDeviceState.Disconnected, Strings.Device.DISCONNECTED.FromResources(), Colors.RED);
      //disconnected.actions = BuildBatchOptionsDialog(Actions.ConnectAll | Actions.ForgetAll,
      //  Strings.Device.Manager.DISCONNECTED_ACTIONS, disconnected);
      //allSections.Add(EDeviceState.Disconnected, disconnected);

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
    }

    /// <summary>
    /// Release this instance.
    /// </summary>
    public void Release() {
      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
    }

    /// <summary>
    /// Cells the displaying ended.
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="cell">Cell.</param>
    /// <param name="indexPath">Index path.</param>
    public override void CellDisplayingEnded(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
      var r = cell as IReleasable;
      if (r != null) {
        r.Release();
      }
    }

    /// <summary>
    /// Commits the editing style.
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="editingStyle">Editing style.</param>
    /// <param name="indexPath">Index path.</param>
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      var record = shownSections[(int)indexPath.Section].records[(int)indexPath.Row];

      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          if (record is DeviceRecord) {
            RequestDeleteDeviceVerification(((DeviceRecord)record).device);
          }
          break;
      }
    }

    /// <summary>
    /// Determines whether this instance can edit row the specified tableView indexPath.
    /// </summary>
    /// <returns><c>true</c> if this instance can edit row the specified tableView indexPath; otherwise, <c>false</c>.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return shownSections[(int)indexPath.Section].records[indexPath.Row] is DeviceRecord;
    }

    /// <summary>
    /// Titles for delete confirmation.
    /// </summary>
    /// <returns>The for delete confirmation.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    /// <summary>
    /// Numbers the of sections.
    /// </summary>
    /// <returns>The of sections.</returns>
    /// <param name="tableView">Table view.</param>
    public override nint NumberOfSections(UITableView tableView) {
      var section = shownSections.Count;
      return section;
    }

    /// <summary>
    /// Rowses the in section.
    /// </summary>
    /// <returns>The in section.</returns>
    /// <param name="tableview">Tableview.</param>
    /// <param name="section">Section.</param>
    public override nint RowsInSection(UITableView tableview, nint section) {
      var rows = shownSections[(int)section].records.Count;
      return rows;
    }

    /// <summary>
    /// Rows the selected.
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      var section = shownSections[(int)indexPath.Section];
      var record = section.records[(int)indexPath.Row];

      if (!record.isExpandable) {
        return;
      }

      if (record is DeviceRecord) {
        var r = record as DeviceRecord;

        if (r.isExpanded) {
          DoCollapse(indexPath);    
        } else {
          DoExpand(indexPath);
        }
      }
    }

    private void DoExpand(NSIndexPath indexPath) {
      var r = shownSections[indexPath.Section].records[indexPath.Row];
      var dr = r as DeviceRecord;
      if (dr == null) {
        return;
      }
      var section = deviceToSection[dr.device];
      var index = indexPath.Row;
      var added = section.ExpandRecord(index);
      index += 1;
      if (added > 0) {
        tableView.InsertRows(ToNSIndexPath(Arrays.Range(index, index + added - 1), indexPath.Section), UITableViewRowAnimation.Right);
      }

      if (expandedDevice != null && expandedDevice != dr.device) {
        var s = deviceToSection[expandedDevice];
        var sectionIndex = shownSections.IndexOf(s);
        var i = s.IndexOfRecord(expandedDevice);
        var removed = s.CollapseRecord(i);
        if (removed > 0 && shownSections.Contains(s)) {
          i += 1;
          tableView.DeleteRows(ToNSIndexPath(Arrays.Range(i, i + removed - 1), sectionIndex), UITableViewRowAnimation.Left);
        }
      }

      expandedDevice = dr.device;
    }

    private void DoCollapse(NSIndexPath indexPath) {
      var r = shownSections[indexPath.Section].records[indexPath.Row];
      var dr = r as DeviceRecord;
      if (dr == null) {
        return;
      }
      var section = deviceToSection[dr.device];
      var index = indexPath.Row;
      var removed = section.CollapseRecord(index);
      index += 1;
      if (removed > 0) {
        tableView.DeleteRows(ToNSIndexPath(Arrays.Range(index, index + removed - 1), indexPath.Section), UITableViewRowAnimation.Left);
      }
    }

    /// <summary>
    /// Gets the height for header.
    /// </summary>
    /// <returns>The height for header.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="section">Section.</param>
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      return 48;
    }

    /// <summary>
    /// Gets the height for row.
    /// </summary>
    /// <returns>The height for row.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      var record = shownSections[indexPath.Section].records[indexPath.Row];

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

    /// <summary>
    /// Gets the view for header.
    /// </summary>
    /// <returns>The view for header.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="section">Section.</param>
    public override UIView GetViewForHeader(UITableView tableView, nint sectionIndex) {
      var section = shownSections[(int)sectionIndex];
      var cell = tableView.DequeueReusableCell(CELL_HEADER) as RemoteDeviceSectionHeaderTableCell;
      section.cell = cell;

      var w = tableView.Frame.Size.Width;
      var f = cell.Frame;
      f.Width = w;
      cell.Frame = f;

      cell.UpdateTo(section, section.actions);

      var ret = new UIView();
      ret.AddSubview(cell);

      return ret;
    }

    /// <summary>
    /// Gets the cell.
    /// </summary>
    /// <returns>The cell.</returns>
    /// <param name="tableView">Table view.</param>
    /// <param name="indexPath">Index path.</param>
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var record = shownSections[indexPath.Section].records[indexPath.Row];

      if (record is DeviceRecord) {
        var r = record as DeviceRecord;

        var cell = tableView.DequeueReusableCell(CELL_DEVICE) as RemoteDeviceTableCell;
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

        var cell = tableView.DequeueReusableCell(CELL_SENSOR) as RemoteSensorTableCell;
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
    /// Reloads the content of the source. Note: this is a full reload that does not perform an pretty animations.
    /// </summary>
    public void Reload() {
      shownSections.Clear();

      foreach (var device in ion.deviceManager.devices) {
        //var state = device.GetDeviceState();

        //var section = allSections[state];
        var section = allSections[EDeviceState.Connected];
        if (AllowsDevice(device)) {
          var i = 0;
          section.AddDevice(device, out i);
          deviceToSection[device] = section;
        }
      }

      foreach (var section in allSections.Values) {
        if (section.records.Count > 0) {
          shownSections.Add(section);
        }
      }

      tableView.ReloadData();
      Invalidate();
    }

    /// <summary>
    /// Resolves any needed device manager events.
    /// </summary>
    /// <param name="de">De.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent de) {
      lock (this) {
        if (DeviceManagerEvent.EType.DeviceEvent == de.type) {
          var eventType = de.deviceEvent.type;
          if (DeviceEvent.EType.ConnectionChange != eventType && DeviceEvent.EType.Deleted != eventType && DeviceEvent.EType.Found != eventType) {
            return;
          }

          var device = de.deviceEvent.device;
          var state = device.GetDeviceState();
          var section = allSections[state];

          if (deviceToSection.ContainsKey(device)) {
            var oldSection = deviceToSection[device];
            if (oldSection != section) {
              int recordIndex = -1;
              int numberRemoved = -1;
              if (oldSection.RemoveDevice(device, out recordIndex, out numberRemoved) && shownSections.Contains(oldSection)) {
                if (oldSection.devices.Count <= 0) {
                  var index = shownSections.IndexOf(oldSection);
                  shownSections.Remove(oldSection);
                  AnimateSectionRemoval(index);
                } else {
                  AnimateRemoveDevice(NSIndexPath.FromRowSection(recordIndex, shownSections.IndexOf(oldSection)), numberRemoved);
                }
              }
            }
          }

          if (!section.HasDevice(device)) {
            var sectionIndex = -1;

            if (!shownSections.Contains(section)) {
              shownSections.Add(section);
              shownSections.Sort(new SectionSorter());
              sectionIndex = shownSections.IndexOf(section);
              AnimateSectionAdd(sectionIndex);
            } else {
              sectionIndex = shownSections.IndexOf(section);
            }

            int index = -1;
            if (section.AddDevice(device, out index)) {
              AnimateAddDevice(NSIndexPath.FromRowSection(index, sectionIndex));
            }

            deviceToSection[device] = section;
            if (EDeviceState.Connected == state) {
              DoExpand(NSIndexPath.FromRowSection(index, shownSections.IndexOf(section)));
            }
          }

          Invalidate();
        }
      }
    }

    /// <summary>
    /// Queries whether or not the source will allow the device to be used.
    /// </summary>
    /// <returns><c>true</c>, if device was allowed, <c>false</c> otherwise.</returns>
    /// <param name="device">Device.</param>
    private bool AllowsDevice(IDevice device) {
      if (sensorFilter != null) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          return gd.GetSensorsMatchingFilter(sensorFilter).Count > 0;
        } else {
          return false;
        }
      } else {
        return true;
      }
    }

    /// <summary>
    /// Does a general purpose view controller invalidation.
    /// </summary>
    private void Invalidate() {
      vc.labelEmpty.Hidden = shownSections.Count != 0;
    }

    /// <summary>
    /// Animates the insertion of a section.
    /// </summary>
    /// <param name="section">Section.</param>
    private void AnimateSectionAdd(int index) {
      tableView.InsertSections(NSIndexSet.FromIndex((nint)index), UITableViewRowAnimation.Right);
    }

    /// <summary>
    /// Animates the removal of a section.
    /// </summary>
    /// <param name="section">Section.</param>
    private void AnimateSectionRemoval(int index) {
      tableView.DeleteSections(NSIndexSet.FromIndex((nint)index), UITableViewRowAnimation.Left);
    }

    /// <summary>
    /// Animates the addition of a new device to a section.
    /// </summary>
    /// <param name="section">Section.</param>
    /// <param name="device">Device.</param>
    private void AnimateAddDevice(NSIndexPath index) {
      tableView.InsertRows(ToNSIndexPath(Arrays.Range(index.Row, index.Row + 1), index.Section), UITableViewRowAnimation.Right);
    }

    /// <summary>
    /// Animates the removal of a device from a section.
    /// </summary>
    /// <param name="section">Section.</param>
    /// <param name="device">Device.</param>
    private void AnimateRemoveDevice(NSIndexPath index, int numberRemoved) {
      var start = index.Row;
      var end = index.Row + numberRemoved - 1;
      tableView.DeleteRows(ToNSIndexPath(Arrays.Range(index.Row, index.Row + numberRemoved - 1), index.Section), UITableViewRowAnimation.Left);
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
    /// Build the batch options dialog for the given section.
    /// </summary>
    /// <param name="actions">Actions.</param>
    /// <param name="title">Title.</param>
    /// <param name="section">Section.</param> 
    private Action BuildBatchOptionsDialog (Actions actions, string title, Section section) {
      return () => {
        var dialog = UIAlertController.Create(title, "", UIAlertControllerStyle.Alert);

        foreach (var s in shownSections) {
          Log.D(this, "Section name: " + s.name);
        }

        for (int i = 1; i <= 32; i++) {
          if ((i & (int)actions) == i) {
            switch ((Actions)i) {
              case Actions.ConnectAll:
                dialog.AddAction(UIAlertAction.Create(Strings.Device.CONNECT_ALL, UIAlertActionStyle.Default, (action) => {
                  foreach (var device in section.devices) {
                    device.connection.ConnectAsync();
                  }
                }));
                break;
              case Actions.DisconnectAll:
                dialog.AddAction(UIAlertAction.Create(Strings.Device.DISCONNECT_ALL, UIAlertActionStyle.Default, (action) => {
                  foreach (var device in section.devices) {
                    device.connection.Disconnect();
                  }
                }));
                break;
              case Actions.ForgetAll:
                dialog.AddAction(UIAlertAction.Create(Strings.Device.FORGET_ALL, UIAlertActionStyle.Default, (action) => {
                  foreach (var device in section.devices) {
                    ion.deviceManager.DeleteDevice(device.serialNumber);
                  }
                }));
                break;
              case Actions.AddAllToWorkbench:
                dialog.AddAction(UIAlertAction.Create(Strings.Workbench.ADD_ALL_TO_WORKBENCH, UIAlertActionStyle.Default, (action) => {
                  foreach (var device in section.devices) {
                    var gd = device as GaugeDevice;
                    if (gd != null) {
                      foreach (var sensor in gd.sensors) {
                        ion.currentWorkbench.AddSensor(sensor);
                      }
                    }
                  }
                }));
                break;
            }
          }
        }

        dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

        dialog.Show();
      };
    }

    /// <summary>
    /// Builds the header that is used to forget devices.
    /// </summary>
    /// <returns>The forget header.</returns>
    /// <param name="device">Device.</param>
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

    [Flags]
    private enum Actions {
      ConnectAll          = 1 << 0,
      DisconnectAll       = 1 << 1,
      ForgetAll           = 1 << 2,
      AddAllToWorkbench   = 1 << 3,
    }
  }

  /// <summary>
  /// The sorter that will sort sections based on their EDeviceState.
  /// </summary>
  internal class SectionSorter : IComparer<Section> {
    public int Compare(Section s1, Section s2) {
      return s1.state.CompareTo(s2.state);
    }
  }

  /// <summary>
  /// The enumeration of the type of records that are present in the device manager.
  /// </summary>
  public enum EViewType {
    Device,
    SerialNumber,
    Sensor,
    Space,
  }

  /// <summary>
  /// The interface that will represent a record that is held in the table source.
  /// </summary>
  public interface IRecord {
    /// <summary>
    /// The view type of the record.
    /// </summary>
    /// <value>The type of the view.</value>
    EViewType viewType { get; }
    /// <summary>
    /// Gets a value indicating whether this <see cref="ION.IOS.ViewController.DeviceManager.IRecord"/> is expandable.
    /// </summary>
    /// <value><c>true</c> if expandable; otherwise, <c>false</c>.</value>
    bool isExpandable { get; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ION.IOS.ViewController.DeviceManager.IRecord"/> is expanded.
    /// </summary>
    /// <value><c>true</c> if is expanded; otherwise, <c>false</c>.</value>
    bool isExpanded { get; set; }
  }

  public class SpaceRecord : IRecord {
    public EViewType viewType { get { return EViewType.Space; } }
    public bool isExpandable {
      get {
        return false;
      }
    }
    public bool isExpanded { get; set; }
  }

  /// <summary>
  /// An enumeration of the current state of a device.
  /// </summary>
  public enum EDeviceState {
    Connected,
    Broadcasting,
    New,
    Available,
    Disconnected,
  }


  /// <summary>
  /// A section is the collection of devices that are to be displayed in the table.
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
    /// The sensor filter that is applied to the devices within the section.
    /// </summary>
    public IFilter<Sensor> sensorFilter;
    /// <summary>
    /// The list of devices that are present in the section. This is used simply to maintain a sorted devices list.
    /// </summary>
    public List<IDevice> devices = new List<IDevice>();
    /// <summary>
    /// The list of records that are in the section.
    /// </summary>
    public List<IRecord> records = new List<IRecord>();

    public RemoteDeviceSectionHeaderTableCell cell;
    public Action actions;

    public Section(EDeviceState state, string name, CGColor color) {
      this.state = state;
      this.name = name;
      this.color = color;
    }

    /// <summary>
    /// Queries whether or not the table manager source has the given device.
    /// </summary>
    /// <returns><c>true</c> if this instance has device the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    public bool HasDevice(IDevice device) {
      return devices.Contains(device);
    }

    /// <summary>
    /// Adds the device to the section. The index will be set to current index of the device record in the section. This
    /// method will ALWAYS add two records: one for the device itself, and one space record.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="device">Device.</param>
    /// <param name="index">Index.</param>
    public bool AddDevice(IDevice device, out int index) {
      //      Log.D(this, "Adding device: " + device + "{" + ((GaugeDevice)device).serialNumber + "}");
      if (HasDevice(device)) {
        index = -1;
        Log.E(this, "Not adding device: the device {" + device.serialNumber + "} is already present in the section {" + state + "}.");
        return false;
      }

      devices.Add(device);
      devices.Sort(new ION.Core.Devices.Sorters.DeviceSerialNumberSorter());
      var deviceIndex = devices.IndexOf(device);

      if (deviceIndex > 0) {
        var recordIndex = IndexOfRecord(devices[deviceIndex - 1]);

        if (recordIndex < 0) {
          Log.E(this, "Not adding device: Failed to add device {" + device.serialNumber + "} to section {" + state + "}!");
          index = -1;
          return false;
        }

        index = recordIndex + SizeOfRecord(records[recordIndex], sensorFilter);

        records.Insert(index, new SpaceRecord());
        records.Insert(index, CreateRecordFor(device));
      } else {
        index = 0;
        records.Insert(0, new SpaceRecord());
        records.Insert(0, CreateRecordFor(device));
      }

      if (cell != null) {
        cell.UpdateTo(this, actions);
      }


      return true;
    }

    /// <summary>
    /// Removes the device and all records associated with it. This includes the space and any subrecords such as
    /// sensor records if the device is expanded.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="device">Device.</param>
    /// <param name="index">Index.</param>
    public bool RemoveDevice(IDevice device, out int index, out int removed) {
      var ri = IndexOfRecord(device);

      if (ri < 0) {
        index = -1;
        removed = 0;
        return false;
      }

      var record = records[ri];
      var size = SizeOfRecord(record, sensorFilter);
      devices.Remove(device);
      records.RemoveRange(ri, size);

      index = ri;
      removed = size;

      if (cell != null) {
        cell.UpdateTo(this, actions);
      }

      return true;
    }

    /// <summary>
    /// Queries the index of the device's record.
    /// </summary>
    /// <returns>The of device.</returns>
    /// <param name="device">Device.</param>
    public int IndexOfRecord(IDevice device) {
      var i = 0;

      foreach (var record in records) {
        var dr = record as DeviceRecord;
        if (dr != null && dr.device.Equals(device)) {
          return i;
        }
        i++;
      }

      return -1;
    }

    /// <summary>
    /// Expands the given device if it is expandable. Returns the number of records that we added.
    /// </summary>
    /// <param name="device">Device.</param>
    public int ExpandRecord(int recordIndex) {
      var record = records[recordIndex];

      if (!record.isExpandable || record.isExpanded) {
        return 0;
      }

      if (record is DeviceRecord) {
        var dr = record as DeviceRecord;
        if (dr.device is GaugeDevice) {
          var gd = dr.device as GaugeDevice;
          var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
          for (int i = sensors.Count; i > 0; i--) {
            records.Insert(recordIndex + 1, new SensorRecord(sensors[i - 1] as GaugeDeviceSensor));
          }
          record.isExpanded = true;
          return sensors.Count;
        }          
      }

      return 0;
    }

    /// <summary>
    /// Collapses the given device if it is expandable.
    /// </summary>
    /// <param name="device">Device.</param>
    public int CollapseRecord(int recordIndex) {
      var record = records[recordIndex];

      if (!record.isExpandable || !record.isExpanded) {
        return 0;
      }

      if (record is DeviceRecord) { 
        var dr = record as DeviceRecord;
        if (dr.device is GaugeDevice) {
          var gd = dr.device as GaugeDevice;
          var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
          records.RemoveRange(recordIndex + 1, sensors.Count);
          record.isExpanded = false;
          return sensors.Count;
        }
      }

      return 0;
    }

    /// <summary>
    /// Queries the size of the record (includive of the record itself, all subrecord {sensors etc...} and spaces).
    /// </summary>
    /// <returns>The of record.</returns>
    /// <param name="record">Record.</param>
    public int SizeOfRecord(IRecord record, IFilter<Sensor> filter) {
      var dr = record as DeviceRecord;
      if (dr != null) {
        var gd = dr.device as GaugeDevice;
        if (record.isExpanded && gd != null) {
          return 2 + gd.GetSensorsMatchingFilter(filter).Count;
        } else {
          return 2;
        }
      } else {
        return 2;
      }
    }

    /// <summary>
    /// Creates a record for the given device.
    /// </summary>
    /// <returns>The record for.</returns>
    /// <param name="device">Device.</param>
    private IRecord CreateRecordFor(IDevice device) {
      if (device is GaugeDevice) {
        return new DeviceRecord(device);
      } else {
        return new DeviceRecord(device);
      }
    }
  }

  /// <summary>
  /// The collection of extensions that are used by the app.
  /// </summary>
  internal static class DeviceExtensions {
    /// <summary>
    /// A convenience method that will get the device state of the given device.
    /// </summary>
    /// <returns>The device state.</returns>
    /// <param name="device">Device.</param>
    public static EDeviceState GetDeviceState(this IDevice device) {
      var ion = AppState.context;
      var connectionState = device.connection.connectionState;

      if (EConnectionState.Connected == connectionState) {
        return EDeviceState.Connected;
      } else if (EConnectionState.Broadcasting == connectionState) {
        return EDeviceState.Broadcasting;
      } else if (!ion.deviceManager.IsDeviceKnown(device)) {
        return EDeviceState.New;
      } else if ((ion.deviceManager.IsDeviceKnown(device) && device.isNearby) || EConnectionState.Connecting == connectionState) {
        return EDeviceState.Available;
      } else {
        return EDeviceState.Disconnected;
      }
    }
  }
}
