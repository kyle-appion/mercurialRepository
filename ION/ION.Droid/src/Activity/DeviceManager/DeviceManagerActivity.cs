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

	using Appion.Commons.Util;

  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Filters;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;

  using ION.Droid.Connections;
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
		/// The state flag that indicates that the activity has requested bluetooth permissions.
		/// </summary>
		private const int STATE_REQUESTED_BLUETOOTH_ON = 1 << 0;
		/// <summary>
		/// The state flags that indicates that the activity has requested that the gps be turned on.
		/// </summary>
		private const int STATE_REQUESTED_LOCATION_ON = 1 << 1;
		/// <summary>
		/// The state flag that indicates that the activity has requested fine location permissions.
		/// </summary>
		private const int STATE_REQUESTED_LOCATION_PERM = 1 << 2;

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
		/// Holds the current state of permissions requests. This is used to prevent the activity from requesting permissions
		/// everytime the activity gains focus.
		/// </summary>
		private int permissionStates;
    /// <summary>
    /// The scanner that will search for classic devices.
    /// </summary>
    private AndroidConnectionManager connectionManager;
    /// <summary>
    /// The handler that we will post scan actions to.
    /// </summary>
    private Handler handler = new Handler();

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

      adapter = new DeviceManagerRecycleAdapter(ion, cache);
      adapter.deviceFilter = BuildDeviceFilter(filter);
      adapter.sensorFilter = BuildSensorFilter(filter);
      if (Intent.ActionPick.Equals(Intent.Action)) {
        adapter.onSensorReturnClicked = OnSensorReturnClicked;
      }

      empty.Visibility = ViewStates.Gone;
      list.Visibility = ViewStates.Visible;
			adapter.emptyView = empty;

      list.SetAdapter(adapter);
      connectionManager = ion.deviceManager.connectionManager as AndroidConnectionManager;
    }

    // Overridden from Activity
    protected override void OnStart() {
      base.OnStart();
      connectionManager.StartScan();
      handler.PostDelayed(() => connectionManager.StopScan(), 10000);
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();

			ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

			InvalidateOptionsMenu();
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_devmanager, Resource.Color.gray));

      adapter.Reload();
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();

			ion.deviceManager.ForgetFoundDevices();

      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
      ion.deviceManager.connectionManager.StopScan();
      handler.RemoveCallbacksAndMessages(null);
    }

    // Overridden from Activity
    protected override void OnStop() {
      base.OnStop();
      ion.deviceManager.connectionManager.StopScan();
    }

    // Overridden from Activity
    protected override void OnDestroy() {
      base.OnDestroy();

      ion.deviceManager.ForgetFoundDevices();
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
				ToggleScanning();
      }));

      if (ion.deviceManager.connectionManager.isScanning) {
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
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

		/// <summary>
		/// Toggles whether or not the activity should perform a scan operation.
		/// </summary>
		private void ToggleScanning() {
/*
      if (connectionManager.isScanning) {
        connectionManager.StopScan();
        handler.RemoveCallbacksAndMessages(null);
      } else {
        connectionManager.StartScan();
        handler.PostDelayed(() => connectionManager.StopScan(), (long)TimeSpan.FromSeconds(12).TotalMilliseconds);
      }
*/
      if (connectionManager.isScanning) {
        handler.RemoveCallbacksAndMessages(null);
        connectionManager.StopScan();
      } else {
        ClearPermissionStates();
        if (CheckPermissionsAndStates()) {
          if (!connectionManager.StartScan()) {
            Error(GetString(Resource.String.bluetooth_error_scan_failed));
          } else {
            handler.PostDelayed(() => {
              connectionManager.StopScan();
              handler.PostDelayed(() => {
                connectionManager.StartClassicScan();
                handler.PostDelayed(() => connectionManager.StopScan(), 12000);
              }, 500);
            }, 8000);
          }
        }
      }
		}

		/// <summary>
		/// Checks the application's permission state to see if we have all of the necessary permissions to operate at full
		/// efficiency. If all of the the necessary permisions are present, then we will return true. Otherwise, we will
		/// return false.
		/// </summary>
		private bool CheckPermissionsAndStates() {
			var bm = (BluetoothManager)GetSystemService(BluetoothService);

			// Check bluetooth state
			if (!isBluetoothOn && (permissionStates & STATE_REQUESTED_BLUETOOTH_ON) == 0) {
				RequestBluetoothAdapterOn();
				permissionStates |= STATE_REQUESTED_BLUETOOTH_ON;
				return false;
			} else if ((int)Android.OS.Build.VERSION.SdkInt >= 23) {
				// TODO ahodder@appioninc.com: Insert non hardcoded value
				// At the time of writing, xamarin was THREE full versions of build codes behind, so we needed to hard code this

				// Check location permissions
				if (!isLocationOn && (permissionStates & STATE_REQUESTED_LOCATION_ON) == 0) {
					RequestLocationServicesEnabled();
					permissionStates |= STATE_REQUESTED_LOCATION_ON;
					return false;
				}

				// Check location enabled state
        if (!hasFineLocationPerms && (permissionStates & STATE_REQUESTED_LOCATION_PERM) == 0) {
					RequestFineLocationPermission();
					permissionStates |= STATE_REQUESTED_LOCATION_PERM;
					return false;
				}

				// Nothing needed handling.
				return true;
			} else {
				// Nothing needed handling.
				return true;
			}
		}

		private void ClearPermissionStates() {
			permissionStates = 0;
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

