namespace ION.Droid.Activity {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;

  using ION.Droid.Sensors;
  using ION.Droid.Views;
  using ION.Droid.Widgets.Adapters;

  [Activity(Label="!!!", Icon="@drawable/ic_nav_devmanager", Theme="@style/AppTheme")]
  public class DeviceManagerActivity : IONActivity {


    /// <summary>
    /// The extra key that is used to pull ESensorTypes (as an array of ints)
    /// from the starting intent.
    /// </summary>
    public const string EXTRA_SENSOR_TYPES = "ion.droid.activity.extra.device_manager.TYPES";
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
    /// The current ion instance.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The view that will display all of the devices for the activity.
    /// </summary>
    /// <value>The list.</value>
    private RecyclerView list { get; set; }
    /// <summary>
    /// The adapter that will organize the devices into a friendly and easy to understand
    /// list layout. (HA! Friendly. Right.)
    /// </summary>
    /// <value>The adapter.</value>
    private DeviceRecycleAdapter adapter { get; set; }

    // Overridden from IONActivity
    protected override void OnCreate(Bundle state) {
      RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      base.OnCreate(state);

      SetContentView(Resource.Layout.activity_device_manager);

      ion = AppState.context;

      list = FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(this));

      adapter = new DeviceRecycleAdapter(Resources);
      adapter.SetDevices(ion.deviceManager.devices);
      adapter.onSensorReturnClicked += OnSensorReturnClicked;

      list.SetAdapter(adapter);
    }

    protected override void OnResume() {
      base.OnResume();

      ion.deviceManager.onDeviceEvent += OnDeviceEvent;
      ion.deviceManager.onDeviceManagerStatesChanged += OnDeviceManagerStateChanged;

      InvalidateOptionsMenu();
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();

      ion.deviceManager.onDeviceEvent -= OnDeviceEvent;
      ion.deviceManager.onDeviceManagerStatesChanged -= OnDeviceManagerStateChanged;
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
        if (dm.connectionHelper.isScanning) {
          dm.connectionHelper.Stop();
        } else {
          var options = new ScanRepeatOptions(ScanRepeatOptions.REPEAT_FOREVER, TimeSpan.FromMilliseconds(10000));
          dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME), options);
//          dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
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
            var options = new ScanRepeatOptions(ScanRepeatOptions.REPEAT_FOREVER, TimeSpan.FromMilliseconds(5000));
            dm.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME), options);
          }

          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Repopulates the device manager adapter.
    /// </summary>
    private void RefreshAdapter() {
      ion.PostToMain(() => {
        try {
          adapter.SetDevices(ion.deviceManager.devices);
          adapter.NotifyDataSetChanged();
        } catch (Exception e) {
          ION.Core.Util.Log.D(this, "asdfasdf", e);
        }
      });
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
//        case DeviceEvent.EType.NameChanged:
//        case DeviceEvent.EType.NewData
        default:
          break;
      }
    }

    /// <summary>
    /// Called when the device manager's state changes.
    /// </summary>
    /// <param name="deviceManager">Device manager.</param>
    private void OnDeviceManagerStateChanged(IDeviceManager deviceManager) {
      InvalidateOptionsMenu();
    }
  }
}

