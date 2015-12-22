namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;

  using ION.Droid.Widgets;
  using ION.Droid.Widgets.Adapters;

  [Activity(Label = "DeviceManagerActivity2", Icon="@drawable/ic_nav_devmanager", Theme="@style/AppTheme")]      
  public class DeviceManagerActivity2 : IONActivity {

    /// <summary>
    /// The extra key that is used to pull ESensorTypes (as an array of Ints) from
    /// the starting intent.
    /// </summary>
    public const string EXTRA_SENSOR_TYPES = "ion.droid.activity.extra.device_manager.TYPES";
    /// <summary>
    /// The extra key that is used to pull sensor's from a result intent in the
    /// form of a SensorParcelable.
    /// </summary>
    public const string EXTRA_SENSOR = "ion.droid.activity.extra.device_manager.SENSOR";

    /// <summary>
    /// The default time that the activity will scan for new devices.
    /// </summary>
    private const long DEFAULT_SCAN_TIME = 5000;

    /// <summary>
    /// The list that will provide views for the list view.
    /// </summary>
    /// <value>The list.</value>
    private ListView list { get; set; }
    /// <summary>
    /// The adapter that will provide view for the list view.
    /// </summary>
    /// <value>The adapter.</value>
    private DeviceManagerAdapter adapter { get; set; }
    /// <summary>
    /// The ion context for the activity.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }

    // Overridden from Activity
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_device_manager_2);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_devmanager, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.Title = GetString(Resource.String.device_manager);

      list = FindViewById<ListView>(Android.Resource.Id.List);
      list.EmptyView = FindViewById(Android.Resource.Id.Empty);

      ion = AppState.context;

      // Create your application here
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();

      ion.deviceManager.onDeviceEvent += OnDeviceEvent;
      ion.deviceManager.onDeviceManagerStatesChanged += OnDeviceManagerStateChanged;
      ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
    }



    /// <summary>
    /// Refreshes the contents of the adapter.
    /// </summary>
    private void RefreshAdapter() {
      var items = new List<IItem>();

      var connected = new List<IDevice>();
      var broadcasting = new List<IDevice>();
      var newDevices = new List<IDevice>();
      var available = new List<IDevice>();
      var disconnected = new List<IDevice>();

      foreach (var d in ion.deviceManager.devices) {
        var state = d.connection.connectionState;
        if (d.isConnected) {
          connected.Add(d);
        } else if (EConnectionState.Broadcasting == state) {
          broadcasting.Add(d);
        } else if (ion.deviceManager.IsDeviceKnown(d)) {
          available.Add(d);
        } else if (ion.deviceManager.IsDeviceKnown(d)) {
          newDevices.Add(d);
        } else {
          if (EConnectionState.Connecting == state || EConnectionState.Resolving == state) {
            available.Add(d);
          }
          disconnected.Add(d);
        }
      }

      // Adds all of the items into the actual adapter source

      // Adds all of the connected to the list
      if (connected.Count > 0) {
        items.Add(new GroupItem(GetString(Resource.String.connected), GetColor(Resource.Color.green), connected.Count));
        foreach (var device in connected) {
          items.Add(new DeviceItem(device));
        }
      }

      // Adds all of the broadcasting devices to the list
      if (broadcasting.Count > 0) {
        items.Add(new GroupItem(GetString(Resource.String.long_range_mode), GetColor(Resource.Color.light_blue), broadcasting.Count));
        foreach (var device in broadcasting) {
          items.Add(new DeviceItem(device));
        }
      }

      // Add all of the new devices to the list.
      if (newDevices.Count > 0) {
        items.Add(new GroupItem(GetString(Resource.String.device_manager_new_devices_found), GetColor(Resource.Color.gray), newDevices.Count));
        foreach (var device in newDevices) {
          items.Add(new DeviceItem(device));
        }
      }

      // Add all of the available devices to the list
      if (available.Count > 0) {
        items.Add(new GroupItem(GetString(Resource.String.available), GetColor(Resource.Color.yellow), available.Count));
        foreach (var device in available) {
          items.Add(new DeviceItem(device));
        }
      }

      // Add all of the disconnected devices
      if (disconnected.Count > 0) {
        items.Add(new GroupItem(GetString(Resource.String.disconnected), GetColor(Resource.Color.red), disconnected.Count));
        foreach (var device in disconnected) {
          items.Add(new DeviceItem(device));
        }
      }

      list.Adapter = adapter = new DeviceManagerAdapter(this, items);
    }

    /// <summary>
    /// Called when a device event is received by the activity.
    /// </summary>
    /// <param name="deviceEvent">Device event.</param>
    private async void OnDeviceEvent(DeviceEvent deviceEvent) {
      var device = deviceEvent.device;

      switch (deviceEvent.type) {
        case DeviceEvent.EType.Found:
          RefreshAdapter();
          break;
        case DeviceEvent.EType.Deleted:
          RefreshAdapter();
          break;
        case DeviceEvent.EType.ConnectionChange:
          RefreshAdapter();
          break;
        case DeviceEvent.EType.NameChanged:
          list.RefreshRow(adapter.IndexOf(device));
          break;
        case DeviceEvent.EType.NewData:
          list.RefreshRow(adapter.IndexOf(device));
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// Called when the device manager's state changes.
    /// </summary>
    /// <param name="deviceManager">Device manager.</param>
    private async void OnDeviceManagerStateChanged(IDeviceManager deviceManager) {
      InvalidateOptionsMenu();
    }
  }
}

