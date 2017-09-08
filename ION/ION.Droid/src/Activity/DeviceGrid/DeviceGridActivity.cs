namespace ION.Droid.Activity.Grid {

  using System;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using Appion.Commons.Util;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  using ION.Droid.Connections;
  using ION.Droid.Devices;
	using ION.Droid.Dialog;
  using ION.Droid.Views;

  [Activity(Label="@string/grid_device", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class DeviceGridActivity : IONActivity, SwipeRefreshLayout.IOnRefreshListener {
    /// <summary>
    /// The number of columns that are present in the grid.
    /// </summary>
    private const int COL_SIZE = 4;

		private SwipeRefreshLayout _swiper;
    private View _availableHeader;
    private View _disconnectedHeader;
    private RecyclerView _availableList;
    private RecyclerView _disconnectedList;
    private DeviceGridAdapter _availableAdapter;
    private DeviceGridAdapter _disconnectedAdapter;

    private Handler _handler;
    private bool _paused;

    private CombinedScanReceiver _receiver;

    /// <summary>
    /// Whether or not the activity should filter out the disconnected devices.
    /// </summary>
    private bool isFiltering {
      get { return _isFiltering; }
      set {
        _isFiltering = value;
        if (value) {
          _availableHeader.Visibility = ViewStates.Invisible;
          _disconnectedHeader.Visibility = ViewStates.Invisible;
          _disconnectedList.Visibility = ViewStates.Gone;
        } else {
          _availableHeader.Visibility = ViewStates.Visible;
          _disconnectedHeader.Visibility = ViewStates.Visible;
          _disconnectedList.Visibility = ViewStates.Visible;
        }
        
        InvalidateOptionsMenu();
      }
    }

    private bool _isFiltering;

    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);
      SetContentView(Resource.Layout.activity_device_grid);
      
      ActionBar.SetIcon(Resource.Drawable.ic_nav_devmanager.AsResourceDrawable(this, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      _swiper = FindViewById<SwipeRefreshLayout>(Resource.Id.swiper);
      _availableHeader = FindViewById(Resource.Id.content);
      _disconnectedHeader = FindViewById(Resource.Id.content2);
      _availableList = FindViewById<RecyclerView>(Resource.Id.connected);
			_disconnectedList = FindViewById<RecyclerView>(Resource.Id.disconnected);

      _handler = new Handler();

			_swiper.SetOnRefreshListener(this);
      _availableList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      _availableAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return gd.isConnected || gd.isNearby;
      });
      _availableList.SetAdapter(_availableAdapter);
      _availableAdapter.onSensorClicked = OnSensorClicked;

      _disconnectedList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      _disconnectedAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return gd.connection.connectionState == EConnectionState.Disconnected && !gd.isNearby;
      });
      _disconnectedList.SetAdapter(_disconnectedAdapter);
			_disconnectedAdapter.onSensorClicked = OnSensorClicked;

      _receiver = new CombinedScanReceiver(this, ion.deviceManager);
    }

    protected override void OnResume() {
      base.OnResume();
      _paused = false;

      ion.deviceManager.connectionManager.onScanStateChanged += OnScanStateChanged;
      ion.deviceManager.connectionManager.StartScan();
    }

    protected override void OnPause() {
      base.OnPause();
			_paused = true;


			ion.deviceManager.connectionManager.onScanStateChanged -= OnScanStateChanged;
      ion.deviceManager.ForgetFoundDevices();
			ion.deviceManager.connectionManager.StopScan();
		}

    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.help, menu);
      MenuInflater.Inflate(Resource.Menu.folder, menu);
      MenuInflater.Inflate(Resource.Menu.scan, menu);

      return true;
    }

    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var scan = menu.FindItem(Resource.Id.scan);
      var scanView = (TextView)scan.ActionView;
      scanView.SetOnClickListener(new ViewClickAction((view) => {
        if (_receiver.isScanning) {
          StopScan();
        } else {
          StartScan();
        }
      }));

      if (_receiver.isScanning) {
        scanView.SetText(Resource.String.scanning);
      } else {
        scanView.SetText(Resource.String.scan);
      }

      var mi = menu.FindItem(Resource.Id.filter);
      if (isFiltering) {
        mi.Icon.SetTint(Resource.Color.green.AsResourceColor(this).ToArgb());
      } else {
        mi.Icon.SetTint(Resource.Color.red.AsResourceColor(this).ToArgb());
      }

      var hi = menu.FindItem(Resource.Id.help);
      hi.Icon.SetTint(Resource.Color.light_blue.AsResourceColor(this).ToArgb());

      return true;
    }

    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
          
        case Resource.Id.filter:
          isFiltering = !isFiltering;
          return true;
          
        case Resource.Id.help:
          ShowHelpDialog();
          return true;
          
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    public void OnRefresh() {
      if (!_receiver.isScanning) {
        StartScan();
			}

      _swiper.Refreshing = _receiver.isScanning;
    }

    /// <summary>
    /// Starts the classic scan, stop the ble scan.
    /// </summary>
    private void StartScan() {
			_receiver.StartScan();
			_handler.Post(() => {
				if (!_paused) {
					ion.deviceManager.connectionManager.StopScan();
				}
			});
    }

    /// <summary>
    /// Stops the classic scan, starts the ble scan/
    /// </summary>
    private void StopScan() {
			_receiver.StopScan();
			_handler.Post(() => {
				if (!_paused) {
					ion.deviceManager.connectionManager.StartScan();
				}
			});
    }

    private void OnSensorClicked(GaugeDeviceSensor sensor, int position) {
      ShowGaugeDialog(sensor);
    }

    private void ShowGaugeDialog(GaugeDeviceSensor sensor) {
      var ft = FragmentManager.BeginTransaction();
      var prev = FragmentManager.FindFragmentByTag("dialog");
      if (prev != null) {
        ft.Remove(prev);
      }
      ft.AddToBackStack(null);
      var frag = new GaugeDeviceControlDialogFragment(sensor.device);
      frag.Show(ft, "dialog");
    }

    private void ShowHelpDialog() {
      var adb = new IONAlertDialog(this);
      adb.SetTitle(Resource.String.help);
      adb.SetMessage(Resource.String.grid_help);
      adb.SetNegativeButton(Resource.String.close, (sender, args) => { });
      adb.Show();
    }

    private void OnScanStateChanged(IConnectionManager cm) {
      InvalidateOptionsMenu();
      _swiper.Refreshing = _receiver.isScanning;
    }

    private class CombinedScanReceiver : BroadcastReceiver {
      public bool isScanning { get; private set; }

      private IDeviceManager _deviceManager;
      private Context _context;
      private BluetoothManager _bm;
      private readonly object locker = new object();

      public CombinedScanReceiver(Context context, IDeviceManager deviceManager) {
        _context = context;
        _deviceManager = deviceManager;
			  _bm = _context.GetSystemService(Context.BluetoothService) as BluetoothManager;
			}

      public override void OnReceive(Context context, Intent intent) {
        switch (intent.Action) {
          case BluetoothAdapter.ActionDiscoveryStarted:
            isScanning = true;
            break;

          case BluetoothAdapter.ActionDiscoveryFinished:
            isScanning = false;
            break;

          case BluetoothDevice.ActionFound:
            ResolveFoundDevice(intent);
            break;
        }
      }

      public void StartScan() {
        _bm.Adapter.StartDiscovery();
      }

      public void StopScan() {
        _bm.Adapter.CancelDiscovery();
      }

      private void ResolveFoundDevice(Intent intent) {
        var device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;

        if (device != null) {
          switch (device.Type) {
            case BluetoothDeviceType.Classic:
              VerifyClassicDevice(device);
              break;
            case BluetoothDeviceType.Le:
            case BluetoothDeviceType.Dual:
              try {
                _deviceManager.CreateBleDeviceOrThrow(device, null);
              } catch (Exception e) {
                Log.E(this, "Failed to create ble device in classic scan mode", e);
              }
              break;
          }
        }
      }

			/// <summary>
			/// Attempts to resolve the device if it is an appion device.
			/// </summary>
			/// <param name="device">Device.</param>
			private void VerifyClassicDevice(BluetoothDevice device) {
				lock (locker) {
					if (!_deviceManager.HasDeviceForAddress(device.Address)) {
						if (Protocol.APPION_CLASSIC_DEVICE_NAME.Equals(device.Name)) {
							Task.Factory.StartNew(async () => {
								var sn = await ClassicConnection.ResolveSerialNumber(device);
								if (sn != null) {
                  _deviceManager.CreateDevice(sn, device.Address, EProtocolVersion.Classic);
								}
							});
						}
					}
				}
			}
    }
  }
}
