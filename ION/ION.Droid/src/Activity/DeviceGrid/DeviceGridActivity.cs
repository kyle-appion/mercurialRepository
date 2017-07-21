namespace ION.Droid.Activity.Grid {

  using System;

  using Android.App;
  using Android.Content.PM;
  using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Connections;
  using ION.Core.Devices;

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

    private IConnectionManager _connectionManager;

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

			_swiper.SetOnRefreshListener(this);
      _availableList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      _availableAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return gd.isConnected || gd.isNearby;
      });
      _availableList.SetAdapter(_availableAdapter);
      _availableAdapter.onSensorClicked = OnSensorClicked;

      _disconnectedList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      _disconnectedAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return !gd.isConnected && !gd.isNearby;
      });
      _disconnectedList.SetAdapter(_disconnectedAdapter);
			_availableAdapter.onSensorClicked = OnSensorClicked;

			_connectionManager = ion.deviceManager.connectionManager;
    }

    protected override void OnResume() {
      base.OnResume();

      _connectionManager.onScanStateChanged += OnScanStateChanged;
    }

    protected override void OnPause() {
      base.OnPause();

      _connectionManager.onScanStateChanged -= OnScanStateChanged;
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
        if (_connectionManager.isScanning) {
          _connectionManager.StopScan();
        } else {
          _connectionManager.StartScan();
        }
      }));

      if (_connectionManager.isScanning) {
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
			if (!_connectionManager.isScanning) {
				_connectionManager.StartScan();
			}

			_swiper.Refreshing = _connectionManager.isScanning;
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
      _swiper.Refreshing = _connectionManager.isScanning;
    }
  }
}
