namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Bluetooth;
  using Android.Content;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Filters;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Connections;
  using ION.Droid.Sensors;
  using ION.Droid.Views;
  using ION.Droid.Widgets.Adapters;

  /// <summary>
  /// The enum that will provide the filters the will only show the toggled devices.
  /// </summary>
  [Flags]
  public enum EDeviceFilter {
    All = 0,
    Pressure = 1 << 0,
    Temperature = 1 << 1,
  }

  [Activity(Label="@string/device_manager", Icon="@drawable/ic_nav_devmanager", Theme="@style/AppTheme")]
  public class DeviceManagerActivity : IONActivity {
    /// <summary>
    /// The extra key that is used to pull a masked Filter enum which is used to filter only the given devices.
    /// </summary>
    public const string EXTRA_DEVICE_FILTER = "ION.Droid.extra.DEVICE_FILTER";
    /// <summary>
    /// The extra key that is used to pull sensors from the result intent returned
    /// by the activity upon finish.
    /// </summary>
    public const string EXTRA_SENSOR = "ion.droid.activity.extra.device_manager.SENSOR";

    /// <summary>
    /// The default time that the activity will be scanning for.
    /// </summary>
    private const long DEFAULT_SCAN_TIME = 5000;

    /// <summary>
    /// The view that will display all of the devices for the activity.
    /// </summary>
    /// <value>The list.</value>
    private RecyclerView list { get; set; }
    /// <summary>
    /// The view that we will show when the device manager has no device content.
    /// </summary>
    /// <value>The empty.</value>
    private View empty { get; set; }
    /// <summary>
    /// The adapter that will organize the devices into a friendly and easy to understand
    /// list layout. (HA! Friendly. Right.)
    /// </summary>
    /// <value>The adapter.</value>
    private DeviceRecycleAdapter adapter { get; set; }
    /// <summary>
    /// The filter for the activity.
    /// </summary>
    private EDeviceFilter filter;
    /// <summary>
    /// The connection helper that the application was using before the activity started.
    /// </summary>
    private IConnectionHelper previousHelper;

    // Overridden from IONActivity
    protected override void OnCreate(Bundle state) {
      RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      base.OnCreate(state);

      SetContentView(Resource.Layout.activity_device_manager);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);

      list = FindViewById<RecyclerView>(Resource.Id.list);
      empty = FindViewById<TextView>(Resource.Id.view);
      empty.Visibility = ViewStates.Gone;

      list.SetLayoutManager(new LinearLayoutManager(this));

      if (Intent.HasExtra(EXTRA_DEVICE_FILTER)) {
        filter = (EDeviceFilter)Intent.GetIntExtra(EXTRA_DEVICE_FILTER, 0);
      } else {
        filter = EDeviceFilter.All;
      }

      adapter = new DeviceRecycleAdapter(this);
      adapter.deviceFilter = BuildDeviceFilter(filter);
      adapter.sensorFilter = BuildSensorFilter(filter);
      adapter.onSensorReturnClicked += OnSensorReturnClicked;
      adapter.onDatasetChanged += (adapter) => {
        if (adapter.ItemCount > 0) {
          empty.Visibility = ViewStates.Gone;
          list.Visibility = ViewStates.Visible;
        } else {
          empty.Visibility = ViewStates.Visible;
          list.Visibility = ViewStates.Gone;
        }
//        OnAdapterRefreshed();
      };

      list.SetAdapter(adapter);
    }

    /// <summary>
    /// Raises the resume event.
    /// </summary>
    protected override void OnResume() {
      base.OnResume();

      previousHelper = ion.deviceManager.connectionHelper;
      var bm = (BluetoothManager)GetSystemService(BluetoothService);
      ion.deviceManager.connectionHelper = new ConnectionHelperCollection(new LeConnectionHelper(this, bm), new ClassicConnectionHelper(this, bm));

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

      InvalidateOptionsMenu();
      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_devmanager, Resource.Color.gray));

      RefreshAdapter();
      var connectionHelper = ion.deviceManager.connectionHelper;

      if (connectionHelper.isEnabled) {
        ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
      } else {
        ShowBluetoothOffDialog();
      }
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();

      ion.deviceManager.connectionHelper.Dispose();
      ion.deviceManager.connectionHelper = previousHelper;

      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
      ion.deviceManager.connectionHelper.Stop();
    }

    // Overridden from Activity
    public override bool OnCreateOptionsMenu(Android.Views.IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.scan, menu);

      return true;
    }

    // Overridden from Activity
    public override bool OnPrepareOptionsMenu(Android.Views.IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var scan = menu.FindItem(Resource.Id.scan);

      var scanView = (TextView)scan.ActionView;
      scanView.SetOnClickListener(new ViewClickAction((view) => {
        var dm = ion.deviceManager;

        if (dm.connectionHelper.isEnabled) {
          if (dm.connectionHelper.isScanning) {
            dm.connectionHelper.Stop();
          } else {
  //          var options = new ScanRepeatOptions(ScanRepeatOptions.REPEAT_FOREVER, TimeSpan.FromMilliseconds(10000));
  //          dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME), options);
            dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
          }
        } else {
          ShowBluetoothOffDialog();
        }
      }));

      if (ion.deviceManager.connectionHelper.isScanning) {
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
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        case Resource.Id.scan:
          var dm = ion.deviceManager;

          if (dm.connectionHelper.isScanning) {
            dm.connectionHelper.Stop();
          } else {
            dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
          }

          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Called when the adapter's content is refreshed.
    /// </summary>
    private void OnAdapterRefreshed() {
      if (adapter.ItemCount <= 0) {
        empty.Visibility = ViewStates.Visible;
        list.Visibility = ViewStates.Gone;
      } else {
        empty.Visibility = ViewStates.Gone;
        list.Visibility = ViewStates.Visible;
      }
    }

    /// <summary>
    /// Repopulates the device manager adapter.
    /// </summary>
    private void RefreshAdapter() {
      try {
        adapter.SetDevices(ion.deviceManager.devices);
        adapter.NotifyDataSetChanged();
      } catch (Exception e) {
        ION.Core.Util.Log.D(this, "asdfasdf", e);
      }
    }

    /// <summary>
    /// Called when the user clicks the return sensor in the adapter.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="position">Position.</param>
    private void OnSensorReturnClicked(GaugeDeviceSensor sensor, int position) {
      var ret = new Intent();

      ret.PutExtra(EXTRA_SENSOR, new GaugeDeviceSensorParcelable(sensor));

      SetResult(Result.Ok, ret);
      Finish();
    }

    /// <summary>
    /// The event handler method that wil resolve the device manager events that come in.
    /// </summary>
    /// <param name="e">E.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      switch (e.type) {
        case DeviceManagerEvent.EType.DeviceEvent:
          OnDeviceEvent(e.deviceEvent);
          break;
        default:
          InvalidateOptionsMenu();
          break;
      }
    }

    /// <summary>
    /// Called when the device manager posts a new device event.
    /// </summary>
    /// <param name="deviceEvent">Device event.</param>
    private void OnDeviceEvent(DeviceEvent deviceEvent) {
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
        default:
          break;
      }
    }

    /// <summary>
    /// Builds a filter that will restrict the devices that are shown by the adapter.
    /// </summary>
    /// <returns>The device filter.</returns>
    private IFilter<IDevice> BuildDeviceFilter(EDeviceFilter filter) {
      if (EDeviceFilter.All == filter) {
        return new YesFilter<IDevice>();
      }

      var filters = new List<IFilter<IDevice>>();

      for (int i = 0; i < 32; i++) {
        var flag = (EDeviceFilter)(1 << i);

        if ((filter & flag) != flag) {
          continue;
        }

        switch (flag) {
          case EDeviceFilter.Pressure:
            filters.Add(new HasSensorOfTypeFilter(ESensorType.Pressure));
            break;

          case EDeviceFilter.Temperature:
            filters.Add(new HasSensorOfTypeFilter(ESensorType.Temperature));
            break;
        }
      }

      return new AndFilterCollection<IDevice>(filters.ToArray());
    }

    /// <summary>
    /// Builds a filter that will restrict the sensors that are shown by the adapter.
    /// </summary>
    /// <returns>The sensor filter.</returns>
    /// <param name="filter">Filter.</param>
    private IFilter<Sensor> BuildSensorFilter(EDeviceFilter filter) {
      if (EDeviceFilter.All == filter) {
        return new YesFilter<Sensor>();
      }

      var filters = new List<IFilter<Sensor>>();

      for (int i = 0; i < 32; i++) {
        var flag = (EDeviceFilter)(1 << i);

        if ((filter & flag) != flag) {
          continue;
        }

        switch (flag) {
          case EDeviceFilter.Pressure:
            filters.Add(new SensorOfTypeFilter(ESensorType.Pressure));
            break;

          case EDeviceFilter.Temperature:
            filters.Add(new SensorOfTypeFilter(ESensorType.Temperature));
            break;
        }
      }

      return new AndFilterCollection<Sensor>(filters.ToArray());
    }
  }
}

