namespace TestBench.Droid {

	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Graphics;
	using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Math;

	using ION.Core.Devices;

	[Activity(Label = "Gauge Scanner", MainLauncher = true, Icon = "@mipmap/icon")]
	public class GaugeScanActivity : BaseActivity, SwipeRefreshLayout.IOnRefreshListener {
		private SwipeRefreshLayout swiper;
		private RecyclerView list;
		private DeviceAdapter deviceAdapter;
		private Spinner spinner;
		private SpinnerAdapter spinnerAdapter;
		private TextView rigState;
		private View startTest;
		private EDeviceModel deviceFilter;
		private Handler handler;

		private IRig rig {
			get {
				return __rig;
			}
			set {
				__rig = value;
				UpdateRigDisplay();
			}
		} IRig __rig;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			handler = new Handler();

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_device_selector);
			swiper = FindViewById<SwipeRefreshLayout>(Resource.Id.swiper);
			list = FindViewById<RecyclerView>(Resource.Id.list);
			list.SetLayoutManager(new LinearLayoutManager(this));
			list.SetAdapter(deviceAdapter = new DeviceAdapter(list, FindViewById(Resource.Id.empty)));
			swiper.SetOnRefreshListener(this);
			startTest = FindViewById(Resource.Id.startTest);
			startTest.Click += async (sender, e) => {
				StartTestActivity(deviceAdapter.GetCheckedConnections());
			};

			startTest.Visibility = ViewStates.Gone;
			deviceAdapter.onCheckChanged += (connections) => {
				UpdateRigDisplay();
			};

			spinner = FindViewById<Spinner>(Resource.Id.spinner);
			spinner.Adapter = spinnerAdapter = new SpinnerAdapter(new EDeviceModel[] {
				EDeviceModel.AV760,
				EDeviceModel.P800,
				EDeviceModel.P500,
			});

			spinner.SetSelection(0);
			deviceFilter = EDeviceModel.AV760;

			FindViewById(Resource.Id.clear).Click += (sender, e) => {
				Clear();
			};

			rigState = FindViewById<TextView>(Resource.Id.rigState);
			rig = null;
			UpdateRigDisplay();
		}

		protected override void OnResume() {
			base.OnResume();
			InvalidateProgress();
			Clear();
		}

		protected override void OnPause() {
			base.OnPause();
			if (service != null) {
				service.scanDelegate.StopScan();
			}
			if (rig != null) {
				rig.onConnectionStateChanged -= OnRigConnectionStateChanged;
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			MenuInflater.Inflate(Resource.Menu.scan, menu);

			return true;
		}

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			base.OnPrepareOptionsMenu(menu);

			var item = menu.FindItem(Android.Resource.Id.Button1);
			if (service == null) {
				item.SetTitle("Please Wait...");
			} else {
				if (service.isScanning) {
					item.SetTitle("Scanning...");
				} else {
					item.SetTitle("Scan");
				}
			}

			return true;
		}

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Button1:
					Appion.Commons.Util.Log.D(this, "Printing Matrix");
					var matrix = new Appion.Commons.Math.Matrix(new double[3, 4] {
						{  1, 2, -1,  -4 },
						{  2, 3, -1, -11 },
						{ -2, 0, -3,  22 },
					});
					Appion.Commons.Util.Log.D(this, "Pre-rr\n" + matrix.ToString());
					matrix.Echelonize();
					Appion.Commons.Util.Log.D(this, "Post-rr\n" + matrix.ToString());
/*
					if (service == null) {
						Toast.MakeText(this, "Please wait for the service to connect...", ToastLength.Long).Show();
					} else {
						if (service.isScanning) {
							service.StopScan();
						} else {
							service.StartScan();
							if (rig != null && !rig.isConnected) {
								rig.Connect();
							}
						}
					}
*/
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		public override void OnServiceBound() {
			base.OnServiceBound();
			InvalidateProgress();

			spinner.ItemSelected += (sender, e) => {
				SetDeviceFilter((EDeviceModel)spinnerAdapter[e.Position]);
			};
			Clear();
			spinner.SetSelection(0);
		}

		public override void OnScanStateChanged(AppService service) {
			base.OnScanStateChanged(service);
			InvalidateProgress();
		}

		public override void OnConnectionFound(IConnection connection) {
			base.OnConnectionFound(connection);
			if (connection.serialNumber.deviceModel == spinnerAdapter[spinner.SelectedItemPosition]) {
				deviceAdapter.AddConnection(connection);
			}
		}

		public override void OnRigFound(IRig newRig) {
			base.OnRigFound(newRig);

			if (rig == null) {
				if (newRig.rigType == deviceFilter.AsRigType()) {
					rig = newRig;
				} // Else the rig doen't match and we should ignore it
			} else {
				// Check if the old rig doesn't match the device filter.
				if (rig.rigType != deviceFilter.AsRigType()) {
					rig = null;
					if (newRig.rigType == deviceFilter.AsRigType()) {
						rig = newRig;
					} 
				} // Else we already have a rig of the proper type.
			}
		}

		public void OnRefresh() {
			if (service != null && !service.isScanning) {
				service.StartScan();
			}
		}

		public void SetDeviceFilter(EDeviceModel deviceModel) {
			deviceFilter = deviceModel;
			deviceAdapter.SetConnections(service.GetConnectionsOfModel(deviceModel));
			if (rig != null && deviceModel.AsRigType() != rig.rigType) {
				rig.Disconnect();
				rig = null;
			}
		}

		private void Clear() {
			if (service != null) {
				if (rig != null) {
					rig.Disconnect();
					rig = null;
				}
				deviceAdapter.Clear();
				UpdateRigDisplay();
			}
		}

		private void StartTestActivity(IEnumerable<IConnection> connections) {
			if (service.currentTest != null) {
				service.currentTest.StopTest();
				service.currentTest = null;
			}

			var serials = new List<string>();
			foreach (var connection in connections) {
				serials.Add(connection.serialNumber.rawSerial);
			}

			var type = spinnerAdapter[spinner.SelectedItemPosition];

			var i = new Intent(this, typeof(TestActivity));
			i.PutExtra(TestActivity.EXTRA_SERIALS, serials.ToArray());
			i.PutExtra(TestActivity.EXTRA_TEST_TYPE, (int)type);
			i.PutExtra(TestActivity.EXTRA_RIG_ADDRESS, rig.address);
			StartActivity(i);
		}

		private void OnRigConnectionStateChanged(IRig rig) {
			handler.Post(UpdateRigDisplay);
		}

		private void UpdateRigDisplay() {
			if (rig == null) {
				rigState.Text = "Scan to find Rig";
				rigState.SetBackgroundColor(Android.Graphics.Color.Red);
			} else {
				rigState.Text = "Rig Found"; 
				rigState.SetBackgroundColor(Android.Graphics.Color.Green);
			}

			if (deviceAdapter.checkedConnections.Count <= 0 || rig == null) {
				startTest.Visibility = ViewStates.Gone;
			} else {
				startTest.Visibility = ViewStates.Visible;
			}
		}

		private void InvalidateProgress() {
			if (service != null) {
				swiper.Refreshing = service.isScanning;
				InvalidateOptionsMenu();
			} else {
				swiper.Refreshing = false;
			}
		}
	}

	internal delegate void OnCheckChanged(HashSet<IConnection> connections);
	internal class DeviceAdapter : RecyclerView.Adapter {
		public event OnCheckChanged onCheckChanged;

		public override int ItemCount {
			get {
				return connections.Count;
			}
		}

		private RecyclerView list;
		private View emptyView;
		private List<IConnection> connections = new List<IConnection>();
		public HashSet<IConnection> checkedConnections = new HashSet<IConnection>();

		public DeviceAdapter(RecyclerView list, View emptyView) {
			this.list = list;
			this.emptyView = emptyView;
			list.Visibility = ViewStates.Gone;
			emptyView.Visibility =  ViewStates.Visible;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var ret = new ViewHolder(parent);

			ret.ItemView.Click += (sender, e) => {
				if (ret.connection == null) {
					return;
				}

				if (checkedConnections.Contains(ret.connection)) {
					checkedConnections.Remove(ret.connection);
					ret.isChecked = false;
				} else {
					checkedConnections.Add(ret.connection);
					ret.isChecked = true;
				}

				if (onCheckChanged != null) {
					onCheckChanged(checkedConnections);
				}
			};

			return ret;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			((ViewHolder)holder).Bind(connections[position]);
		}

		public override long GetItemId(int position) {
			return 0;
		}

		public void SetConnections(IEnumerable<IConnection> connections) {
			this.connections.Clear();
			this.connections.AddRange(connections);
			NotifyDataSetChanged();

			if (this.connections.Count <= 0) {
				list.Visibility = ViewStates.Gone;
				emptyView.Visibility =  ViewStates.Visible;
			} else {
				list.Visibility = ViewStates.Visible;
				emptyView.Visibility =  ViewStates.Gone;
			}
		}

		public void AddConnection(IConnection connection) {
			if (connections.Contains(connection)) {
				return;
			}
			var pos = connections.Count;
			connections.Add(connection);
			NotifyItemInserted(pos);
			list.Visibility = ViewStates.Visible;
			emptyView.Visibility =  ViewStates.Gone;
		}

		public void Clear() {
			checkedConnections.Clear();
			connections.Clear();
			NotifyDataSetChanged();
		}

		public HashSet<IConnection> GetCheckedConnections() {
			return checkedConnections;
		}
	}

	internal class ViewHolder : RecyclerView.ViewHolder {
		public IConnection connection;
		public bool isChecked {
			get {
				return _isChecked;
			}
			set {
				_isChecked = value;
				if (value) {
					ItemView.SetBackgroundColor(Color.LightGray);
				} else {
					ItemView.SetBackgroundColor(Color.White);
				}
			}
		} bool _isChecked;

		private TextView serialNumber;

		public ViewHolder(ViewGroup parent) : base(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_content, parent, false)) {
			serialNumber = ItemView.FindViewById<TextView>(Resource.Id.content);
			isChecked = false;
		}

		public void Bind(IConnection connection) {
			this.connection = connection;
			serialNumber.Text = connection.serialNumber.ToString();
		}
	}

	internal class SpinnerAdapter : BaseAdapter {
		public override int Count {
			get {
				return supportedDeviceModels.Count;
			}
		}

		public EDeviceModel this[int index] {
			get {
				return supportedDeviceModels[index];
			}
		}

		private List<EDeviceModel> supportedDeviceModels;

		public SpinnerAdapter(params EDeviceModel[] supportedDeviceModels) {
			this.supportedDeviceModels = new List<EDeviceModel>(supportedDeviceModels);
		}

		public override Java.Lang.Object GetItem(int position) {
			throw new System.NotImplementedException("We are not in java land and cannot cast out C# objects to java.");
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override View GetView(int position, View convert, ViewGroup parent) {
			if (convert == null) {
				convert = LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.list_item_content, parent, false);
			}

			var text = convert.FindViewById<TextView>(Resource.Id.content);

			text.Text = this[position].ToString();

			return convert;
		}
	}
}

