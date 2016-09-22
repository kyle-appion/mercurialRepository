namespace ION.Droid.Activity.DeviceManager {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Filters;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.Droid.Sensors;
  using ION.Droid.Views;

  /// <summary>
  /// The enum that will provide the filters the will only show the toggled devices.
  /// </summary>
  [Flags]
  public enum EDeviceFilter {
    All = 0,
    Pressure = 1 << 0,
    Temperature = 1 << 1,
  }

  [Activity(Label="@string/device_manager", Icon="@drawable/ic_nav_devmanager", Theme="@style/AppTheme", ScreenOrientation=ScreenOrientation.Portrait)]
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
    private const long DEFAULT_SCAN_TIME = 7500;

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
    private DeviceManagerRecycleAdapter adapter;
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

      adapter = new DeviceManagerRecycleAdapter(ion, list);
      adapter.deviceFilter = BuildDeviceFilter(filter);
      adapter.sensorFilter = BuildSensorFilter(filter);
      if (Intent.ActionPick.Equals(Intent.Action)) {
        adapter.onSensorReturnClicked = OnSensorReturnClicked;
      }

      empty.Visibility = ViewStates.Gone;
      list.Visibility = ViewStates.Visible;

      adapter.onDatasetChanged += (adapter) => {
        if (adapter.ItemCount > 0) {
          empty.Visibility = ViewStates.Gone;
          list.Visibility = ViewStates.Visible;
        } else {
          empty.Visibility = ViewStates.Visible;
          list.Visibility = ViewStates.Gone;
        }
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

			ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

			InvalidateOptionsMenu();
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_devmanager, Resource.Color.gray));

			var connectionHelper = ion.deviceManager.connectionHelper;

			if (bm.Adapter.IsEnabled) {
				if (!ion.deviceManager.connectionHelper.isScanning) {
					ion.deviceManager.connectionHelper.StartScan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
				}
			} else {
				ShowBluetoothOffDialog();
			}

      adapter.Reload();
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();

      ion.deviceManager.connectionHelper.Dispose();
      ion.deviceManager.connectionHelper = previousHelper;

      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
      ion.deviceManager.connectionHelper.StopScan();
    }

    /// <Docs>Perform any final cleanup before an activity is destroyed.</Docs>
    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    protected override void OnDestroy() {
      base.OnDestroy();

      ion.deviceManager.ForgetFoundDevices();
      adapter.Release();
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

      var scanView = (TextView)scan.ActionView;
      scanView.SetOnClickListener(new ViewClickAction((view) => {
				var manager = (BluetoothManager)GetSystemService(BluetoothService);

        var dm = ion.deviceManager;

				if (manager.Adapter.IsEnabled) {
          if (dm.connectionHelper.isScanning) {
            dm.connectionHelper.StopScan();
          } else {
            dm.connectionHelper.StartScan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
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
/*
        case Resource.Id.scan:
          var dm = ion.deviceManager;

          if (dm.connectionHelper.isScanning) {
            dm.connectionHelper.StopScan();
          } else {
            dm.connectionHelper.StartScan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
          }
*/
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Called when the user clicks the return sensor in the adapter.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorReturnClicked(Sensor sensor) {
      var ret = new Intent();

      ret.PutExtra(EXTRA_SENSOR, sensor.ToParcelable());

      SetResult(Result.Ok, ret);
      Finish();
    }

    /// <summary>
    /// The event handler method that wil resolve the device manager events that come in.
    /// </summary>
    /// <param name="e">E.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      if (e.type == DeviceManagerEvent.EType.ScanStarted || e.type == DeviceManagerEvent.EType.ScanStopped) {
        InvalidateOptionsMenu();
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
    /// <param name="deviceFilter">Filter.</param>
		private IFilter<Sensor> BuildSensorFilter(EDeviceFilter deviceFilter) {
      if (EDeviceFilter.All == deviceFilter) {
        return new YesFilter<Sensor>();
      }

      var filters = new List<IFilter<Sensor>>();

      for (int i = 0; i < 32; i++) {
        var flag = (EDeviceFilter)(1 << i);

        if ((deviceFilter & flag) != flag) {
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

