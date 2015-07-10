using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.Droid.Devices;
using ION.Droid.Dialog;
using ION.Droid.Util;
using ION.Droid.Widgets;

namespace ION.Droid.Activities {
  /// <summary>
  /// This activity is the primary activity that is used to interface directly
  /// with the "low level" device hardware. More preceisely, this activity is
  /// used to add/modify ION devices and allow them to be returned to various
  /// other primary and ansiliary activities.
  /// </summary>
  [Activity(Label = "DeviceManagerActivity")]
  public class DeviceManagerActivity : Activity, View.IOnClickListener, Handler.ICallback {
    private static readonly string TAG = "DeviceManagerActivity";

    private static readonly DeviceGroup CONNECTED = new DeviceGroup(Resource.String.connected, Resource.Color.green);
    private static readonly DeviceGroup LONG_RANGE = new DeviceGroup(Resource.String.long_range_mode, Resource.Color.light_blue);
    private static readonly DeviceGroup NEW_DEVICES_FOUND = new DeviceGroup(Resource.String.device_manager_new_devices_found, Resource.Color.light_gray);
    private static readonly DeviceGroup AVAILABLE = new DeviceGroup(Resource.String.available, Resource.Color.yellow);
    private static readonly DeviceGroup DISCONNECTED = new DeviceGroup(Resource.String.disconnected, Resource.Color.yellow);

    /// <summary>
    /// The collection of all the groups of devices that the device manager will
    /// display to the user.
    /// </summary>
    private static readonly DeviceGroup[] GROUPS = new DeviceGroup[] {
      CONNECTED,
      LONG_RANGE,
      NEW_DEVICES_FOUND,
      AVAILABLE,
      DISCONNECTED,
    };

    /// <summary>
    /// The refresh adapter message.
    /// </summary>
    private const int MSG_REFRESH = 1;
    /// <summary>
    /// The update scan state message.
    /// </summary>
    private const int MSG_INVALIDATE_MENU = 2;

    /// <summary>
    /// Queries the index of the group.
    /// </summary>
    /// <param name="group"></param>
    /// <returns>The index of the group or -1 if the group is not in the device
    /// manager's collection.</returns>
    internal static int IndexOf(DeviceGroup group) {
      for (int i = 0; i < GROUPS.Length; i++) {
        if (GROUPS[i].Equals(group)) {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// The device manager that we are "displaying".
    /// </summary>
    private IDeviceManager deviceManager { get; set; }
    /// <summary>
    /// This ListView will display all of the devices from DeviceManager.
    /// </summary>
    private ExpandableListView list { get; set; }
    /// <summary>
    /// The adapter that is providing device views to the list view.
    /// </summary>
    private DeviceAdapter adapter { get; set; }
    /// <summary>
    /// The handler that will update the adapter.
    /// </summary>
    private Handler handler { get; set; }
    /// <summary>
    /// The most recent historic content of the adapter.
    /// </summary>
    private Dictionary<DeviceGroup, List<IDevice>> oldContent { get; set; }
    /// <summary>
    /// The delegate that is listening for newly found devices.
    /// </summary>
    private OnDeviceFound onDeviceFound;
    /// <summary>
    /// The delegate that is used to updated device rows when their state changes.
    /// </summary>
    private OnDeviceStateChanged onDeviceStateChanged;
    /// <summary>
    /// The delegate that is listening for device manager state changes.
    /// </summary>
    private OnDeviceManagerStateChanged onDeviceManagerStateChanged;

    // Overridden from Activity
    protected override void OnCreate(Bundle bundle) {
      Log.printer = new LogPrinter();
      RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      base.OnCreate(bundle);

      onDeviceFound = (IDeviceManager deviceManager, IDevice device) => {
        Log.D(TAG, "Found new device: " + device);
        handler.SendEmptyMessage(MSG_REFRESH);
      };

      onDeviceStateChanged = (IDevice device) => {
        Log.D(this, "Device: " + device.serialNumber + " changed state.");
        DeviceGroup group = adapter.GetGroupFor(device);
        if (adapter.ContainsGroup(group)) {
          adapter.PostUpdate(group, device);
        }
      };

      onDeviceManagerStateChanged = (IDeviceManager deviceManager, EDeviceManagerState state) => {
        Log.D(TAG, "Device manager state changed to: " + state);
        handler.SendEmptyMessage(MSG_REFRESH);
        handler.SendEmptyMessage(MSG_INVALIDATE_MENU);
      };

      SetContentView(Resource.Layout.activity_device_manager);

      deviceManager = new AndroidDeviceManager(this);
      handler = new Handler(this);

      list = FindViewById<ExpandableListView>(Android.Resource.Id.List);
      list.SetGroupIndicator(null);
      list.EmptyView = FindViewById(Android.Resource.Id.Empty);

      adapter = new DeviceAdapter(list);
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();

      deviceManager.onDeviceFound += onDeviceFound;
      deviceManager.onDeviceStateChanged += onDeviceStateChanged;
      deviceManager.onDeviceManagerStateChanged += onDeviceManagerStateChanged;

      deviceManager.DoActiveScan();

      adapter.expansionLimit = 1;

      adapter.onGroup = (DeviceGroup group) => {
        if (CONNECTED.Equals(group)) {
          ShowConnectedContextMenu();
        } else if (LONG_RANGE.Equals(group)) {
          ShowLongRangeContextMenu();
        } else if (NEW_DEVICES_FOUND.Equals(group)) {
          ShowNewDevicesContextMenu();
        } else if (AVAILABLE.Equals(group)) {
          ShowAvailableContextMenu();
        } else if (DISCONNECTED.Equals(group)) {
          ShowDisconnectedContextMenu();
        } else {
          // TODO ahodder@appioninc.com: handler this better
          Log.E(this, "Cannot show context menu: invalid group");
        }
      };

      if (Intent.ActionPick.Equals(Intent.Action)) {
        adapter.onAdd = (Sensor sensor) => {
          // TODO ahodder@appioninc.com: Implement this
          Log.D(this, "Return a sensor to the caller");
        };
      } else {
        adapter.onAddWorkbench = (Sensor sensor) => {
          Log.D(this, "Add sensor to workbench");
        };

        adapter.onAddAnalyzer = (Sensor sensor) => {
          Log.D(this, "Add sensor to analyzer");
        };
      }

      RefreshAdapter();
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();

      deviceManager.onDeviceFound -= onDeviceFound;
      deviceManager.onDeviceStateChanged -= onDeviceStateChanged;
      deviceManager.onDeviceManagerStateChanged -= onDeviceManagerStateChanged;

      deviceManager.StopActiveScan();
    }

    // Overridden from Activity
    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.scan, menu);

      return true;
    }

    // Overridden from Activity
    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var scan = menu.FindItem(Resource.Id.scan);

      TextView scanView = (TextView)scan.ActionView;
      scanView.SetOnClickListener(this);

      if (EDeviceManagerState.ActiveScanning == deviceManager.state) {
        scanView.SetText(Resource.String.scanning);
        SetProgressBarIndeterminateVisibility(true);
      } else {
        scanView.SetText(Resource.String.scan);
        SetProgressBarIndeterminateVisibility(false);
      }

      return true;
    }

    // Overridden from Activity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.scan: {
          if (EDeviceManagerState.ActiveScanning == deviceManager.state) {
            deviceManager.StopActiveScan();
          } else {
            deviceManager.DoActiveScan();
          }
          return true;
        }
        default: {
          return base.OnMenuItemSelected(featureId, item);
        }
      }
    }

    // Overridden from View.IOnClickListener
    public void OnClick(View view) {
      // NOTE ahodder@appioninc.com: This is only meant for the scan button.
      Log.D(this, "Clicky clicky");
      if (EDeviceManagerState.ActiveScanning == deviceManager.state) {
        deviceManager.StopActiveScan();
      } else { 
        deviceManager.DoActiveScan();
      }
    }

    // Overridden from Handler.ICallback
    public bool HandleMessage(Message msg) {
      switch (msg.What) {
        case MSG_REFRESH: {
          RefreshAdapter();
          return true;
        }
        case MSG_INVALIDATE_MENU: {
          InvalidateOptionsMenu();
          return true;
        }
        default: {
          return false;
        }
      }
    }

    /// <summary>
    /// Updates the content of the adapter based on the content within the
    /// backing device manager.
    /// </summary>
    private void RefreshAdapter() {
      Log.D(this, "Refreshing adapter.");
      Dictionary<DeviceGroup, List<IDevice>> newContent = new Dictionary<DeviceGroup, List<IDevice>>();

      foreach (DeviceGroup group in GROUPS) {
        newContent.Add(group, new List<IDevice>());
      }

      foreach (IDevice device in deviceManager.devices) {
        Log.D(this, "Working with device " + device.serialNumber);
        newContent[GetGroupForDevice(device)].Add(device);
      }

      foreach (DeviceGroup group in GROUPS) {
        List<IDevice> devices = newContent[group];

        if (devices.Count <= 0) {
          adapter.Remove(group);
        } else {
          int i = 0;
          // Find the index within the adapter where the group belongs
          for (; i < adapter.GroupCount; i++) {
            DeviceGroup g = adapter[i];
            if (IndexOf(group) < IndexOf(g)) {
              break;
            }
          }

          // Ensure that the group is in the adapter and expanded
          if (!adapter.ContainsGroup(group)) {
            adapter.Add(group, i);
//            list.ExpandGroup(i);
          }

          adapter.SetRetainOrder(group, devices);

          if (oldContent != null) {
            List<IDevice> oldDevices = oldContent[group];

            if (devices.Count == 1 && oldDevices.Count == 0 && CONNECTED.Equals(group)) {
              // TODO: ahodder@appioninc.com: Expand the single item
              Log.D(this, "Expand child");
            }
          }
        }
      }

      Log.D(this, "adapter should have " + deviceManager.devices.Count + " items. Has " + adapter.GroupCount + " groups");
      oldContent = newContent;
      adapter.NotifyDataSetChanged();
    }

    /// <summary>
    /// Queries which group a device belongs to.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    private DeviceGroup GetGroupForDevice(IDevice device) {
      // Just ick
      if (!device.isKnown && device.isNearby) {
        return NEW_DEVICES_FOUND;
      } else if (device.isKnown && device.isNearby) {
        return AVAILABLE;
      }

      switch (device.connection.connectionState) {
        case EConnectionState.Connected: {
          return CONNECTED;
        }
        case EConnectionState.Broadcasting: {
          return LONG_RANGE;
        }
        default: {
          return DISCONNECTED;
        }
      }
    }

    /// <summary>
    /// Shows the connect group's context menu.
    /// </summary>
    /// <param name="group"></param>
    private void ShowConnectedContextMenu() {
      ListDialogBuilder ldb = new ListDialogBuilder(this);
      ldb.onOptionSelected = (int id) => {
        switch (id) {
          case Resource.String.disconnect_all: {
            foreach (IDevice device in deviceManager.devices) {
              device.connection.Disconnect();
            }
            break;
          }
          case Resource.String.device_manager_add_all_to_workbench: {
            foreach (IDevice device in deviceManager.knownDevices) {
              if (device.isConnected) {
                // TODO ahodder@appioninc.com: Implement add all to workbench
                Log.D(this, "Adding device to workbench!");
              }
            }
            break;
          }
          default: {
            break;
          }
        }
      };

      ldb.SetTitle(Resource.String.device_manager_batch_connected_actions);
      ldb.Show();
    }

    /// <summary>
    /// Shows the long range group's context menu.
    /// </summary>
    private void ShowLongRangeContextMenu() {
      ListDialogBuilder ldb = new ListDialogBuilder(this);
      ldb.onOptionSelected = (int id) => {
      };

      ldb.SetTitle(Resource.String.device_manager_batch_long_range_actions);
      ldb.Show();
    }

    /// <summary>
    /// Shows the new device group context menu.
    /// </summary>
    private void ShowNewDevicesContextMenu() {
      ListDialogBuilder ldb = new ListDialogBuilder(this);
      ldb.onOptionSelected = (int id) => {
      };

      ldb.SetTitle(Resource.String.device_manager_batch_new_device_actions);
      ldb.Show();
    }

    /// <summary>
    /// Shows the available device group context menu.
    /// </summary>
    private void ShowAvailableContextMenu() {
      ListDialogBuilder ldb = new ListDialogBuilder(this);
      ldb.onOptionSelected = (int id) => {
      };

      ldb.SetTitle(Resource.String.device_manager_batch_available_actions);
      ldb.Show();
    }

    /// <summary>
    /// Shows the disconnected device group context menu.
    /// </summary>
    private void ShowDisconnectedContextMenu() {
      ListDialogBuilder ldb = new ListDialogBuilder(this);
      ldb.onOptionSelected = (int id) => {
      };

      ldb.SetTitle(Resource.String.device_manager_batch_disconnected_actions);
      ldb.Show();
    }
  } // End DeviceManagerActiity

  internal struct DeviceGroup {
    public int stringRes { get; private set; }
    public int colorRes { get; private set; }

    public DeviceGroup(int stringRes, int colorRes) : this() {
      this.stringRes = stringRes;
      this.colorRes = colorRes;
    }

    /// <summary>
    /// Queries the index of this group within the device manager activity.
    /// </summary>
    /// <returns></returns>
    public int IndexOf() {
      return DeviceManagerActivity.IndexOf(this);
    }
  } // End DeviceGroup

  /// <summary>
  /// The adapter that will provide the content for the device manager.
  /// </summary>
  internal class DeviceAdapter : ExpandableContentELVAdapter<DeviceGroup, IDevice> {
    /// <summary>
    /// A delegate that will be called when a group's options view is clicked.
    /// </summary>
    /// <param name="group"></param>
    internal delegate void OnGroupOptionsClicked(DeviceGroup group);
    /// <summary>
    /// A delegate that will be called when a sensor's "add" button is clicked.
    /// </summary>
    /// <param name="sensor"></param>
    internal delegate void OnAddClicked(Sensor sensor);
    /// <summary>
    /// A delegate that will be called when a sensor's "add to workbench" button
    /// is clicked.
    /// </summary>
    /// <param name="sensor"></param>
    internal delegate void OnAddWorkbenchClicked(Sensor sensor);
    /// <summary>
    /// A delegate that will be called when a sensor's "add to analyzer" button
    /// is clicked.
    /// </summary>
    /// <param name="sensor"></param>
    internal delegate void OnAddAnalyzerClicked(Sensor sensor);

    /// <summary>
    /// The callback that will be notified when a group's options buttin is
    /// clicked.
    /// </summary>
    internal OnGroupOptionsClicked onGroup { get; set; }
    /// <summary>
    /// The callback that will be notified when the add button is clicked.
    /// </summary>
    internal OnAddClicked onAdd { get; set; }
    /// <summary>
    /// The callback that will be notified when the add to workbench button
    /// is clicked.
    /// </summary>
    internal OnAddWorkbenchClicked onAddWorkbench { get; set; }
    /// <summary>
    /// The callback that will be notified when the add to analyzer button is
    /// clicked.
    /// </summary>
    internal OnAddAnalyzerClicked onAddAnalyzer { get; set; }
    /// <summary>
    /// The cache that will help us out with all the bitmaps that we are using.
    /// </summary>
    private BitmapCache cache { get; set; }

    public DeviceAdapter(ExpandableListView list) :
      base(list, Resource.Layout.list_item_device_manager_device, Resource.Layout.list_item_device_manager_sensor_content) {
      cache = BitmapCache.Get(context.Resources);
    }

    // Overridden from ExpandableContentELVAdapter
    public override View GetGroupView(int groupPosition, bool isExpanded, View convert, ViewGroup mommy) {
      GroupViewHolder vh;

      if (convert == null) {
        convert = LayoutInflater.From(context).Inflate(Resource.Layout.list_item_device_manager_group, mommy, false);
        convert.Tag = vh = new GroupViewHolder();

        vh.counter = convert.FindViewById<TextView>(Resource.Id.counter);
        vh.title = convert.FindViewById<TextView>(Resource.Id.title);
        vh.options = convert.FindViewById(Resource.Id.icon);
      } else {
        vh = (GroupViewHolder)convert.Tag;
      }

      DeviceGroup group = this[groupPosition];

      Resources res = context.Resources;

      vh.group = group;
      vh.counter.Text = "" + GetChildrenCount(groupPosition);
      vh.counter.SetBackgroundColor(res.GetColor(group.colorRes));
      vh.title.Text = res.GetString(group.stringRes);
      vh.options.SetOnClickListener(vh);

      return convert;
    }

    // Overridden from ExpandableContentELVAdapter
    public override View GetTitleView(int groupPosition, int childPosition, View convert, ViewGroup mommy) {
      DeviceViewHolder vh = (DeviceViewHolder)convert.Tag;

      if (vh == null) {
        convert.Tag = vh = new DeviceViewHolder();
        vh.deviceIcon = convert.FindViewById<ImageView>(Resource.Id.icon);
        vh.deviceType = convert.FindViewById<TextView>(Resource.Id.type);
        vh.name = convert.FindViewById<TextView>(Resource.Id.name);
        vh.expansionArrow = convert.FindViewById<ImageView>(Resource.Id.arrow);
        vh.connectionStatus = convert.FindViewById<ImageView>(Resource.Id.status);
        vh.connect = convert.FindViewById(Resource.Id.connect);
        vh.connecting = convert.FindViewById(Resource.Id.loading);
      }

      DeviceGroup group = this[groupPosition];
      IDevice device = this[groupPosition, childPosition];

      vh.group = group;
      vh.device = device;
      vh.deviceIcon.SetImageBitmap(cache.GetBitmap(DeviceUtils.GetDeviceIcon(device)));
      vh.deviceType.SetText(DeviceUtils.GetDeviceProductName(device));
      vh.name.Text = device.name;

      if (IsExpanded(groupPosition, childPosition)) {
        vh.expansionArrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_upblack));
      } else {
        vh.expansionArrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_downblack));
      }

      vh.connectionStatus.Visibility = ViewStates.Visible;
      vh.connecting.Visibility = ViewStates.Gone;
      switch (device.connection.connectionState) {
        case EConnectionState.Connected: {
          vh.connectionStatus.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_paired));
          break;
        }
        case EConnectionState.Connecting: {
          vh.connectionStatus.Visibility = ViewStates.Gone;
          vh.connecting.Visibility = ViewStates.Visible;
          break;
        }
        case EConnectionState.Broadcasting: {
          vh.connectionStatus.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_broadcast));
          break;
        }
        default: {
          vh.connectionStatus.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_disconnected));
          break;
        }
      }

      vh.connect.SetOnClickListener(vh);

      return convert;
    }

    // Overridden from ExpandableContentELVAdapter
    public override View GetContentView(int groupPosition, int childPosition, View convert, ViewGroup mommy) {
      SensorContentViewHolder vh = (SensorContentViewHolder)convert.Tag;

      if (vh == null) {
        convert.Tag = vh = new SensorContentViewHolder();

        vh.serial = convert.FindViewById<TextView>(Resource.Id.device_serial_number);
        vh.sensorLayout = convert.FindViewById<LinearLayout>(Resource.Id.content);
      }

      DeviceGroup group = this[groupPosition];
      IDevice device = this[groupPosition, childPosition];

      vh.serial.Text = device.serialNumber.ToString();

      switch (device.type) {
        case EDeviceType.Gauge: {
          GaugeDevice gauge = (GaugeDevice)device;

          if (vh.sensorLayout.ChildCount != gauge.sensorCount) {
            // Ensure we don't have too many views.
            for (int i = vh.sensorLayout.ChildCount - 1; i >= gauge.sensorCount; i--) {
              vh.sensorLayout.RemoveViewAt(i);
            }
          }

          for (int i = 0; i < gauge.sensorCount; i++) {
            View view = vh.sensorLayout.GetChildAt(i);
            bool createdView = view == null;

            view = GetSensorView(groupPosition, childPosition, gauge[i], view, vh.sensorLayout);

            if (createdView) {
              vh.sensorLayout.AddView(view);
            }
          }
          break;
        }
        default: {
          break;
        }
      }

      return convert;
    }

    /// <summary>
    /// The view inflation method for the sensors within the device content row.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <param name="sensor"></param>
    /// <param name="convert"></param>
    /// <param name="mommy"></param>
    /// <returns></returns>
    private View GetSensorView(int groupPosition, int childPosition, Sensor sensor, View convert, ViewGroup mommy) {
      SensorViewHolder vh;

      if (convert == null) {
        convert = LayoutInflater.From(context).Inflate(Resource.Layout.list_item_device_manager_sensor, mommy, false);
        convert.Tag = vh = new SensorViewHolder();

        vh.adapter = this;
        vh.sensorIcon = convert.FindViewById<ImageView>(Resource.Id.icon);
        vh.sensorType = convert.FindViewById<TextView>(Resource.Id.type);
        vh.sensorReading = convert.FindViewById<TextView>(Resource.Id.measurement);
        vh.addToWorkbench = convert.FindViewById<ImageButton>(Resource.Id.add_to_workbench);
        vh.addToAnalyzer = convert.FindViewById<ImageButton>(Resource.Id.add_to_analyzer);
        vh.add = convert.FindViewById<ImageButton>(Resource.Id.add);
      } else {
        vh = (SensorViewHolder)convert.Tag;
      }

      vh.groupPosition = groupPosition;
      vh.childPosition = childPosition;
      vh.sensor = sensor;

      vh.sensorType.SetText(DeviceUtils.GetSensorTypeName(sensor.sensorType));
      // TODO ahodder@appioninc.com: Format the scalar
      vh.sensorReading.Text = sensor.measurement.ToString();

      // TODO: ahodder@appioninc.com: Bind to the ion context.
      if (onAdd != null) { // activity is in request mode
        vh.add.Visibility = ViewStates.Visible;
      } else {
        vh.add.Visibility = ViewStates.Gone;
      }

      if (onAddWorkbench != null) {
        if (false) { // sensor is in workbench
          vh.addToWorkbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_workbench));
          vh.addToWorkbench.SetBackgroundResource(Resource.Drawable.np_bordered_transparent_square);
        } else {
          vh.addToWorkbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add_to_workbench));
          vh.addToWorkbench.SetBackgroundResource(Resource.Drawable.img_button_up_gold);
        }
      }

      if (onAddAnalyzer != null) {
        if (false) { // sensor is in analyzer
          vh.addToAnalyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_analyzer));
          vh.addToAnalyzer.SetBackgroundResource(Resource.Drawable.np_bordered_transparent_square);
        } else {
          vh.addToAnalyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add_to_analyzer));
          vh.addToAnalyzer.SetBackgroundResource(Resource.Drawable.img_button_up_gold);
        }
      }

      return convert;
    }

    internal class GroupViewHolder : Java.Lang.Object, View.IOnClickListener {
      public DeviceAdapter adapter { get; set; }
      public DeviceGroup group { get; set; }

      public TextView counter { get; set; }
      public TextView title { get; set; }
      public View options { get; set; }

      // Overridden from View.IOnClickListener
      public void OnClick(View view) {
        adapter.onGroup(group);
      }
    } // End DeviceAdapter.GroupViewHolder

    internal class DeviceViewHolder : Java.Lang.Object, View.IOnClickListener {
      public DeviceGroup group { get; set; }
      public IDevice device { get; set; }

      public ImageView deviceIcon { get; set; }
      public TextView deviceType { get; set; }
      public TextView name { get; set; }
      public ImageView expansionArrow { get; set; }
      public ImageView connectionStatus { get; set; }
      public View connect { get; set; }
      public View connecting { get; set; }

      // Overridden from View.IOnClickListener
      public void OnClick(View view) {
        if (device.isConnected) {
          device.connection.Disconnect();
        } else {
          device.connection.Connect();
        }
      }
    } // End DeviceAdapter.DeviceViewHolder

    internal class SensorContentViewHolder : Java.Lang.Object {
      public TextView serial { get; set; }
      public LinearLayout sensorLayout { get; set; }
    } // End SensorContentViewHolder

    internal class SensorViewHolder : Java.Lang.Object {
      public DeviceAdapter adapter { get; set; }
      public int groupPosition { get; set; }
      public int childPosition { get; set; }
      public Sensor sensor { get; set; }

      public ImageView sensorIcon { get; set; }
      public TextView sensorType { get; set; }
      public TextView sensorReading { get; set; }
      public ImageButton addToWorkbench { get; set; }
      public ImageButton addToAnalyzer { get; set; }
      public ImageButton add { get; set; }
    } // End DeviceAdapter.SensorViewHolder
  } // End DeviceAdapter
}