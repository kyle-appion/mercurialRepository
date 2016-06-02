namespace ION.Droid.Widgets.Adapters.DeviceManager {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Content.Res;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Devices.Filters;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Dialog;
  using ION.Droid.Devices;
  using ION.Droid.Sensors;
  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  /// <summary>
  /// The delegate that will be notified when a sensor is clicked.
  /// </summary>
  public delegate void OnSensorReturnClicked(Sensor sensor);
  /// <summary>
  /// The adapter that will list and present ION devices to the user.
  /// </summary>
  public class DeviceManagerRecycleAdapter : SwipableRecyclerViewAdapter, IReleasable {
    /// <summary>
    /// The action that will be called on a sensor's add button click event.
    /// </summary>
    public OnSensorReturnClicked onSensorReturnClicked;

    /// <summary>
    /// The filter that will filter out devices that should not be shown in the adapter.
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
    } IFilter<Sensor> __sensorFilter;

    public IFilter<IDevice> deviceFilter {
      get {
        return __deviceFilter;
      }
      set {
        if (value == null) {
          value = new YesFilter<IDevice>();
        }
        __deviceFilter = value;
      }
    } IFilter<IDevice> __deviceFilter;

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
    /// The current ion that is being used by the adapter.
    /// </summary>
    private IION ion;
    /// <summary>
    /// The view that the adapter is providing content to.
    /// </summary>
    private RecyclerView listView;
    /// <summary>
    /// The bitmap cache for the adapter that will cache fequently used bitmaps.
    /// </summary>
    private BitmapCache cache;

    public DeviceManagerRecycleAdapter(IION ion, RecyclerView listView) {
      this.ion = ion;
      this.listView = listView;
      this.cache = new BitmapCache(listView.Resources);

      var connected = new Section(EDeviceState.Connected, Resource.String.connected, Resource.Color.green);
      connected.actions = BuildBatchOptionsDialog(EActions.DisconnectAll | EActions.ForgetAll | EActions.AddAllToWorkbench,
        Resource.String.device_manager_batch_connected_actions, connected);
      allSections.Add(EDeviceState.Connected, connected);

      var broadcasting = new Section(EDeviceState.Broadcasting, Resource.String.long_range_mode, Resource.Color.light_blue);
      broadcasting.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.AddAllToWorkbench,
        Resource.String.device_manager_batch_long_range_actions, broadcasting);
      allSections.Add(EDeviceState.Broadcasting, broadcasting);

      var @new = new Section(EDeviceState.New, Resource.String._new, Resource.Color.light_gray);
      @new.actions = BuildBatchOptionsDialog(EActions.ConnectAll, Resource.String.device_manager_batch_new_device_actions, @new);
      allSections.Add(EDeviceState.New, @new);

      var avail = new Section(EDeviceState.Available, Resource.String.available, Resource.Color.yellow);
      @avail.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.ForgetAll,
        Resource.String.device_manager_batch_available_actions, avail);
      allSections.Add(EDeviceState.Available, avail);

      var disconnected = new Section(EDeviceState.Disconnected, Resource.String.disconnected, Resource.Color.red);
      disconnected.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.ForgetAll,
          Resource.String.device_manager_batch_disconnected_actions, disconnected);
      allSections.Add(EDeviceState.Disconnected, disconnected);

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
    }

    /// <summary>
    /// Raises the view recycled event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    public override void OnViewRecycled(Java.Lang.Object holder) {
      var dh = holder as DMViewHolder;
      if (dh != null) {
        dh.Unbind();
      }
    }

    /// <summary>
    /// Queries whether or not the given view holder is swipeable.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="index">Index.</param>
    /// <param name="record">Record.</param>
    public override bool IsViewHolderSwipable(IRecord record, SwipableViewHolder viewHolder, int index) {
      return record is DeviceRecord;
    }

    /// <summary>
    /// Queries the action that is triggered when the swipe revealed button is clicked.
    /// </summary>
    /// <returns>The view holder swipe action.</returns>
    /// <param name="index">Index.</param>
    public override Action GetViewHolderSwipeAction(int index) {
      var record = records[index];

      if (record is DeviceRecord) {
        return () => {
          ShowRequestForgetDevices(new IDevice[] { (record as DeviceRecord).device });
        };
      } else {
        return null;
      }
    }

    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return records[position].viewType;
    }

    /// <summary>
    /// Called when the adapter needs a new view holder.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
      switch ((EViewType)viewType) {
        case EViewType.Section:
          return new DeviceSectionViewHolder(parent);
        case EViewType.IDevice:
          return new DeviceViewHolder(parent, cache);
        case EViewType.SerialNumber:
          throw new Exception("Failed to build serial number thingie");
        case EViewType.Space:
          return new SpaceViewHolder(parent);
        case EViewType.Sensor:
          return new SensorViewHolder(parent, ion, cache);
        default:
          throw new Exception("Cannot create viewholder for view type: " + viewType);
      }
    }

    /// <summary>
    /// Binds the given record to the view holder.
    /// </summary>
    /// <param name="record">Record.</param>
    /// <param name="vh">Vh.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(IRecord record, SwipableViewHolder vh, int position) {
      var dm = vh as DMViewHolder;
      if (dm != null) {
        dm.clickAction = null;
      }
      switch ((EViewType)GetItemViewType(position)) {
        case EViewType.IDevice:
          var dr = record as DeviceRecord;
          (vh as DeviceViewHolder)?.BindTo(dr);
          dm.clickAction = () => {
            ToggleRecord(IndexOfDevice(dr.device));
          };
          break;
        case EViewType.Section:
          (vh as DeviceSectionViewHolder)?.BindTo(record as DeviceSectionRecord);
          break;
        case EViewType.Sensor:
          (vh as SensorViewHolder)?.BindTo(record as SensorRecord, onSensorReturnClicked);
          break;
        case EViewType.SerialNumber:
//          (vh as SerialNumberViewHolder)?.BindTo(allRecords[position] as SerialNumberRecord);
          break;
      }
    }

    /// <summary>
    /// Release this instance.
    /// </summary>
    public void Release() {
      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
    }

    /// <summary>
    /// Reloads the content of the adapter. Note: this is a full reload that does not perform pretty animations.
    /// </summary>
    public void Reload() {
      records.Clear();
      shownSections.Clear();

      foreach (var device in ion.deviceManager.devices) {
        var state = device.GetDeviceState();
        var section = allSections[state];
        if (AllowsDevice(device)) {
          deviceToSection[device] = section;
          AddDeviceToSection(section, device, false);
          if (device == expandedDevice) {
            ExpandRecord(IndexOfDevice(device), false);
          }
        }
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Adds the given device to the given section and performs an add animation.
    /// </summary>
    /// <param name="section">Section.</param>
    /// <param name="device">Device.</param>
    private void AddDeviceToSection(Section section, IDevice device, bool animate = true) {
      if (section.HasDevice(device) || !AllowsDevice(device)) {
        return;
      }

      section.devices.Add(device);
      section.devices.Sort(new ION.Core.Devices.Sorters.DeviceSerialNumberSorter());

      var sectionIndex = IndexOfSection(section);

      if (!shownSections.Contains(section)) {
        shownSections.Add(section);
        shownSections.Sort(new SectionSorter());
        sectionIndex = FindInsertionIndexForSection(section);
        records.Insert(sectionIndex, new DeviceSectionRecord(section));
        if (animate) {
          NotifyItemInserted(sectionIndex);
        }
      }

      var deviceIndex = FindInsertionIndexForDevice(section, device);
      var record = CreateRecordFor(device);
      if (deviceIndex >= records.Count) {
        records.Add(record);
        records.Add(new SpaceRecord());        
      } else {
        records.Insert(deviceIndex, new SpaceRecord());
        records.Insert(deviceIndex, record);
      }

      if (animate) {
        NotifyItemRangeInserted(deviceIndex, 2);
      }

      NotifyItemChanged(sectionIndex);
    }

    /// <summary>
    /// Removes the given device from the given section and performs an add animation.
    /// </summary>
    /// <param name="section">Section.</param>
    /// <param name="device">Device.</param>
    private void RemoveDeviceFromSection(Section section, IDevice device, bool animate = true) {
      if (!section.HasDevice(device)) {
        return;
      }

      var sectionIndex = IndexOfSection(section);
      var deviceIndex = IndexOfDevice(device);
      var record = RecordForDevice(device);
      var size = SizeOfRecord(record);
      section.devices.Remove(device);

      Log.D(this, "Removing record of size: " + size);
      records.RemoveRange(deviceIndex, size);
      if (animate) {
        NotifyItemRangeRemoved(deviceIndex, size);
      }

      if (shownSections.Contains(section) && section.devices.Count <= 0) {
        shownSections.Remove(section);
        records.RemoveAt(sectionIndex);
        if (animate) {
          NotifyItemRemoved(sectionIndex);
        }
      } else {
        NotifyItemChanged(sectionIndex);
      }

      if (device == expandedDevice) {
        expandedDevice = null;
      }
    }

    /// <summary>
    /// Toggles whether or not the given record is expanded or not.
    /// </summary>
    /// <param name="index">Index.</param>
    private void ToggleRecord(int index) {
      var dr = records[index] as DeviceRecord;
      if (dr != null) {
        if (dr.device == expandedDevice) {
          CollapseRecord(index, true);
        } else {
          ExpandRecord(index, true);
        }
      }
    }

    /// <summary>
    /// Expands the record at the given index, if it is expandable.
    /// </summary>
    /// <param name="index">Index.</param>
    private void ExpandRecord(int index, bool animate = true) {
      var dr = records[index] as DeviceRecord;

      if (!IsRecordExpandable(dr) || dr == null || dr.device == expandedDevice) {
        return;
      }

      var added = 0;

      if (dr.device is GaugeDevice) {
        var gd = dr.device as GaugeDevice;
        var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
        for (int i = sensors.Count; i > 0; i--) {
          records.Insert(index + 1, new SensorRecord(sensors[i - 1]));
        }
        added = sensors.Count;
      }

      if (animate && added > 0) {
        NotifyItemRangeInserted(index + 1, added);
      }

      if (expandedDevice != null && expandedDevice != dr.device) {
        CollapseRecord(IndexOfDevice(expandedDevice));
      }

      expandedDevice = dr.device;
    }

    /// <summary>
    /// Collapses the record at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    private void CollapseRecord(int index, bool animate = true) {
      var dr = records[index] as DeviceRecord;

      if (!IsRecordExpandable(dr) || dr == null) {
        return;
      }

      var section = deviceToSection[dr.device];
      var removed = 0;

      if (dr.device is GaugeDevice) {
        var gd = dr.device as GaugeDevice;
        var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
        records.RemoveRange(index + 1, sensors.Count);
        removed = sensors.Count;
      }

      if (animate && removed > 0) {
        NotifyItemRangeRemoved(index + 1, removed);
      }

      expandedDevice = null;
    }

    /// <summary>
    /// Queries whether or not the record is expandable.
    /// </summary>
    /// <returns><c>true</c> if this instance is record expandable the specified record; otherwise, <c>false</c>.</returns>
    /// <param name="record">Record.</param>
    private bool IsRecordExpandable(IRecord record) {
      var dr = record as DeviceRecord;
      return dr != null && dr.device is GaugeDevice;
    }

    /// <summary>
    /// Queries the size of the given record. Because records can expand, allowing for "child" records, this method is
    /// used to query how many records are associated to the given record. 1 means the record is only responsible for
    /// itself. Most records will have a size of 2.
    /// </summary>
    /// <returns>The of record.</returns>
    /// <param name="record">Record.</param>
    private int SizeOfRecord(IRecord record) {
      var dr = record as DeviceRecord;
      if (dr != null) {
        var gd = dr.device as GaugeDevice;
        if (gd != null && gd == expandedDevice) {
          return 2 + gd.GetSensorsMatchingFilter(sensorFilter).Count;
        } else {
          return 2;
        }
      } else {
        return 2;
      }
    }

    /// <summary>
    /// Find the record insertion index for the given device within the given section.
    /// </summary>
    /// <returns>The insertion index for device.</returns>
    /// <param name="section">Section.</param>
    /// <param name="device">Device.</param>
    private int FindInsertionIndexForDevice(Section section, IDevice device) {
      var ret = IndexOfSection(section) + 1;

      foreach (var d in section.devices) {
        if (d == device) {
          return ret;
        } else {
          ret += SizeOfRecord(RecordForDevice(d));
        }
      }

      return -1;
    }

    /// <summary>
    /// Find the record insertion index for the given section.
    /// </summary>
    /// <returns>The insertion index for section.</returns>
    /// <param name="section">Section.</param>
    private int FindInsertionIndexForSection(Section section) {
      var name = listView.Context.GetString(section.name);
      var ret = 0;

      var index = shownSections.IndexOf(section);
      for (int i = 0; i < index; i++) {
        var size = SizeOfSection(shownSections[i]);
        ret += size;
      }

      return ret;
    }

    /// <summary>
    /// Queries the size of the section (inclusive of its own record).
    /// </summary>
    /// <returns>The of section.</returns>
    /// <param name="section">Section.</param>
    private int SizeOfSection(Section section) {
      var ret = 1;

      var sectionIndex = IndexOfSection(section);
      foreach (var device in section.devices) {
        ret += SizeOfRecord(RecordForDevice(device));
      }

      return ret;
    }

    /// <summary>
    /// Creates a record for the given device.
    /// </summary>
    /// <returns>The record for.</returns>
    /// <param name="device">Device.</param>
    private SwipableRecyclerViewAdapter.IRecord CreateRecordFor(IDevice device) {
      return new DeviceRecord(device);
    }

    /// <summary>
    /// Queries whether or not the source will allow the device to be used.
    /// </summary>
    /// <returns><c>true</c>, if device was allowsed, <c>false</c> otherwise.</returns>
    /// <param name="device">Device.</param>
    private bool AllowsDevice(IDevice device) {
      if (deviceFilter.Matches(device)) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          return gd.GetSensorsMatchingFilter(sensorFilter).Count > 0;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }

    /// <summary>
    /// Builds an options dialog for a device state category.
    /// </summary>
    /// <param name="actions">Actions.</param>
    /// <param name="title">Title.</param>
    /// <param name="devices">Devices.</param>
    private Action BuildBatchOptionsDialog(EActions actions, int title, Section section) {
      return () => {
        var ldb = new ListDialogBuilder(listView.Context);
        ldb.SetTitle(title);

        for (int i = 1; i <= 32; i++) {
          if ((i & (int)actions) == i) {
            switch ((EActions)i) {
              case EActions.ConnectAll:
                ldb.AddItem(Resource.String.connect_all, () => {
                  foreach (var device in section.devices) {
                    device.connection.ConnectAsync();
                  }
                });
                break;

              case EActions.DisconnectAll:
                ldb.AddItem(Resource.String.disconnect_all, () => {
                  foreach (var device in section.devices) {
                    device.connection.Disconnect();
                  }
                });
                break;

              case EActions.ForgetAll:
                ldb.AddItem(Resource.String.forget_all, () => {
                  ShowRequestForgetDevices(section.devices);
                });
                break;

              case EActions.AddAllToWorkbench:
                ldb.AddItem(Resource.String.device_manager_add_all_to_workbench, () => {
                  foreach (var device in section.devices) {
                    var gd = device as GaugeDevice;
                    if (gd != null) {
                      foreach (var sensor in gd.sensors) {
                        ion.currentWorkbench.AddSensor(sensor);
                      }
                    }
                  }
                });
                break;
            }
          }
        }

        ldb.Show();
      };
    }

    /// <summary>
    /// Shows a dialog that will confirm with the user the intention to delete a collection of devices fromt the
    /// application.
    /// </summary>
    /// <param name="devices">Devices.</param>
    private void ShowRequestForgetDevices(IEnumerable<IDevice> devices) {
      var adb = new IONAlertDialog(listView.Context);

      adb.SetTitle(Resource.String.devices_forget);
      adb.SetMessage(Resource.String.devices_forget_warning);

      adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
      });

      adb.SetPositiveButton(Resource.String.forget, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
        ForgetDevices(devices);
      });
    }

    /// <summary>
    /// Deleted the devices from the app.
    /// </summary>
    /// <param name="devices">Devices.</param>
    private void ForgetDevices(IEnumerable<IDevice> devices) {
      foreach (var d in devices) {
        var index = IndexOfDevice(d);
        int count = 2;

        if (expandedDevice == d) {
          var gd = d as GaugeDevice;
          if (gd != null) {
            count += gd.sensorCount;
          }
        }

        records.RemoveRange(index, count);
        NotifyItemRangeRemoved(index, count);
        ion.deviceManager.DeleteDevice(d.serialNumber);
      }
    }

    /// <summary>
    /// Queries the record that is represented by the given device.
    /// </summary>
    /// <returns>The for device.</returns>
    /// <param name="device">Device.</param>
    private IRecord RecordForDevice(IDevice device) {
      var index = IndexOfDevice(device);
      if (index >= 0) {
        return records[index];
      } else {
        return null;
      }
    }

    /// <summary>
    /// Queries the index of the device. If the device is not in the adapter, -1 will be returned.
    /// </summary>
    /// <returns>The of device.</returns>
    /// <param name="device">Device.</param>
    private int IndexOfDevice(IDevice device) {
      for (int i = 0; i < records.Count; i++) {
        var record = records[i] as DeviceRecord;

        if (record != null && record.device.Equals(device)) {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Queries the index of the section. If the section is not in the adapter, -1 will be returned.
    /// </summary>
    /// <returns>The of section.</returns>
    /// <param name="section">Section.</param>
    private int IndexOfSection(Section section) {
      for (int i = 0; i < records.Count; i++) {
        var record = records[i] as DeviceSectionRecord;
        if (record != null && record.section.Equals(section)) {
          return i;
        }
      }

      return -1;
    }

    private void OnDeviceManagerEvent(DeviceManagerEvent de) {
      lock (this) {
        if (DeviceManagerEvent.EType.DeviceEvent == de.type) {
          var et = de.deviceEvent.type;
          if (DeviceEvent.EType.ConnectionChange != et && DeviceEvent.EType.Deleted != et && DeviceEvent.EType.Found != et) {
            return;
          }

          var device = de.deviceEvent.device;
          var index = IndexOfDevice(device);
          var state = device.GetDeviceState();
          var section = allSections[state];

          if (deviceToSection.ContainsKey(device)) {
            var oldSection = deviceToSection[device];

            if (oldSection != section) {
              RemoveDeviceFromSection(oldSection, device);
            }
          }

          if (!section.HasDevice(device) && AllowsDevice(device)) {
            deviceToSection[device] = section;
            AddDeviceToSection(section, device);
            if (state == EDeviceState.Connected) {
              ExpandRecord(IndexOfDevice(device));
            }
          }
        }
      }
    }

    [Flags]
    private enum EActions {
      ConnectAll        = 1 << 0,
      DisconnectAll     = 1 << 1,
      ForgetAll         = 1 << 2,
      AddAllToWorkbench = 1 << 3,
    }
  }
/*
  /// <summary>
  /// A simple record that is present in the DeviceManager recycler adapter.
  /// </summary>
  public interface IDeviceRecord : SwipableRecyclerViewAdapter.IRecord {
    IDevice device { get; }
    /// <summary>
    /// Gets a value indicating whether this <see cref="ION.Droid.Widget.Adapters.DeviceManager.IDMRecord"/> is expandable.
    /// </summary>
    /// <value><c>true</c> if is expandable; otherwise, <c>false</c>.</value>
    bool isExpandable { get; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ION.Droid.Widget.Adapters.DeviceManager.IDMRecord"/> is expanded.
    /// </summary>
    /// <value><c>true</c> if is expanded; otherwise, <c>false</c>.</value>
    bool isExpanded { get; set; }
  }
*/

  /// <summary>
  /// A simple view holder definition that is applied to all of the view holders in the adapter.
  /// </summary>
  public abstract class DMViewHolder : SwipableViewHolder {
    /// <summary>
    /// The action that is fired when the view holder is clicked.
    /// </summary>
    public Action clickAction;

    public DMViewHolder(ViewGroup parent, int layoutResource) : base(parent, layoutResource) {
      view.SetOnClickListener(new ViewClickAction((v) => {
        if (clickAction != null) {
          clickAction();
        }
      }));
    }

    /// <summary>
    /// Unbinds the record from anything that it has attached to.
    /// </summary>
    public abstract void Unbind();
  }

  /// <summary>
  /// A record that is used to provide empty space.
  /// </summary>
  public class SpaceRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get { return (int)EViewType.Space; } }
    public bool isExpandable {
      get {
        return false;
      }
    }
    public bool isExpanded { get; set; }
  }

  public class SpaceViewHolder : SwipableViewHolder {
    public SpaceViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_space, false) {
    }
  }

  /// <summary>
  /// A simple enumeration of the type of views that are present in the adapter.
  /// </summary>
  public enum EViewType {
    Section,
    IDevice,
    SerialNumber,
    Sensor,
    Space,
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
  /// The sorter that will sort sections based on their EDeviceState.
  /// </summary>
  internal class SectionSorter : IComparer<Section> {
    public int Compare(Section s1, Section s2) {
      return s1.state.CompareTo(s2.state);
    }
  }

  /// <summary>
  /// A section is a collection of devices that are to be displayed in the list view.
  /// </summary>
  public class Section {
    /// <summary>
    /// The state that all the sections devices are in.
    /// </summary>
    public EDeviceState state;
    /// <summary>
    /// The name of the section.
    /// </summary>
    public int name;
    /// <summary>
    /// The color of the section.
    /// </summary>
    public int color;
    /// <summary>
    /// The list of devices that are present in the section. This is used simply to maintain a sorted devices list.
    /// </summary>
    public List<IDevice> devices = new List<IDevice>();
    /// <summary>
    /// The actions that are allowed for the section.
    /// </summary>
    public Action actions;

    public Section(EDeviceState state, int name, int color) {
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
