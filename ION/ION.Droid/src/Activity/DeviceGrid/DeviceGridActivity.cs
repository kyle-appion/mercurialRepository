namespace ION.Droid.Activity.Grid {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  using ION.Droid.Views;

  [Activity(Label="US Grid", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class DeviceGridActivity : IONActivity, SwipeRefreshLayout.IOnRefreshListener {
    /// <summary>
    /// The number of columns that are present in the grid.
    /// </summary>
    private const int COL_SIZE = 4;

		private SwipeRefreshLayout swiper;
    private RecyclerView availableList;
    private RecyclerView disconnectedList;
    private DeviceGridAdapter availableAdapter;
    private DeviceGridAdapter disconnectedAdapter;

    private IConnectionManager connectionManager;

    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);
      SetContentView(Resource.Layout.activity_device_grid);

      swiper = FindViewById<SwipeRefreshLayout>(Resource.Id.swiper);
      availableList = FindViewById<RecyclerView>(Resource.Id.connected);
			disconnectedList = FindViewById<RecyclerView>(Resource.Id.disconnected);

			swiper.SetOnRefreshListener(this);
      availableList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      availableAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return gd.isConnected || gd.isNearby;
      });
      availableList.SetAdapter(availableAdapter);
      availableAdapter.onSensorClicked = OnSensorClicked;

      disconnectedList.SetLayoutManager(new GridLayoutManager(this, COL_SIZE));
      disconnectedAdapter = new DeviceGridAdapter(ion, COL_SIZE, (gd) => {
        return !gd.isConnected && !gd.isNearby;
      });
      disconnectedList.SetAdapter(disconnectedAdapter);
			availableAdapter.onSensorClicked = OnSensorClicked;

			connectionManager = ion.deviceManager.connectionManager;
    }

    protected override void OnResume() {
      base.OnResume();

      connectionManager.onScanStateChanged += OnScanStateChanged;
    }

    protected override void OnPause() {
      base.OnPause();

      connectionManager.onScanStateChanged -= OnScanStateChanged;
    }

    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.scan, menu);

      return true;
    }

    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var scan = menu.FindItem(Resource.Id.scan);
      var scanView = (TextView)scan.ActionView;
      scanView.SetOnClickListener(new ViewClickAction((view) => {
        if (connectionManager.isScanning) {
          connectionManager.StopScan();
        } else {
          connectionManager.StartScan();
        }
      }));

      if (connectionManager.isScanning) {
        scanView.SetText(Resource.String.scanning);
      } else {
        scanView.SetText(Resource.String.scan);
      }

      return true;
    }

    public void OnRefresh() {
			if (!connectionManager.isScanning) {
				connectionManager.StartScan();
			}

			swiper.Refreshing = connectionManager.isScanning;
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

    private void OnScanStateChanged(IConnectionManager cm) {
      InvalidateOptionsMenu();
      swiper.Refreshing = connectionManager.isScanning;
    }
  }
}
