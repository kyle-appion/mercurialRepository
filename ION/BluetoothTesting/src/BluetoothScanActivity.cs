namespace BluetoothTesting {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.V4.Content;
	using Android.Support.V7.Widget;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	[Activity(Label = "BluetoothScanActivity", Theme="@style/AppTheme")]
	public class BluetoothScanActivity : Activity, IServiceConnection {

		private const long SCAN_TIME = 1000 * 10;

		private BackendBluetoothService service;
		private Handler handler;

		private RecyclerView list;
		private Adapter adapter;

		protected override void OnCreate(Bundle savedInstanceState) {
			RequestWindowFeature(WindowFeatures.IndeterminateProgress);
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_bluetooth_scan);
			StartService(new Intent(this, typeof(BackendBluetoothService)));
			BindService(new Intent(this, typeof(BackendBluetoothService)), this, Bind.AutoCreate);


			handler = new Handler();

			list = FindViewById<RecyclerView>(Resource.Id.list);
			list.SetAdapter(adapter = new Adapter());
			adapter.onRowClicked += OnRowClicked;
		}

		protected override void OnResume() {
			base.OnResume();
			InvalidateProgress();
		}

		protected override void OnPause() {
			base.OnPause();
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			MenuInflater.Inflate(Resource.Menu.scan, menu);

			return true;
		}

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			base.OnPrepareOptionsMenu(menu);

			var item = menu.FindItem(Resource.Id.scan);
			if (service == null) {
				item.SetTitle("Please Wait...");
			} else {
				if (service.isScanning) {
					item.SetTitle("Scanning");
				} else {
					item.SetTitle("Scan");
				}
			}

			return true;
		}

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Resource.Id.scan:
					ToggleScanning();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		public void OnServiceConnected(ComponentName name, IBinder binder) {
			service = ((BackendBluetoothService.Binder)binder).service;
			service.onScanStarted += OnScanStarted;
			service.onScanStopped += OnScanStopped;
			service.onDeviceFound += OnDeviceFound;
			service.onConnectionChanged += OnConnectionChanged;
			InvalidateProgress();
		}

		public void OnServiceDisconnected(ComponentName name) {
			Toast.MakeText(this, "Service has disconnected", ToastLength.Short).Show();
		}

		private void OnScanStarted(BackendBluetoothService service) {
			Toast.MakeText(this, "Starting scan", ToastLength.Short).Show();
			InvalidateProgress();
		}

		private void OnScanStopped(BackendBluetoothService service) {
			Toast.MakeText(this, "Stopping scan", ToastLength.Short).Show();
			InvalidateProgress();
		}

		private void OnDeviceFound(BackendBluetoothService service, BluetoothConnection connection) {
			handler.Post(() => {
				D("Found new device: " + connection.device.Name);
				adapter.AddConnection(connection);
			});
		}

		private void OnRowClicked(int position, BluetoothConnection connection) {
			if (connection.connectionState == ProfileState.Disconnected) {
				connection.Connect();
			} else {
				connection.Disconnect();
			}
		}

		private void OnConnectionChanged(BluetoothConnection connection) {
			handler.Post(() => {
				adapter.UpdateConnection(connection);
			});
		}

		private void InvalidateProgress() {
			if (service != null) {
				InvalidateOptionsMenu();
				SetProgressBarIndeterminateVisibility(service.isScanning);
			} else {
				SetProgressBarIndeterminateVisibility(true);
			}
		}

		private void ToggleScanning() {
			if (!service.isScanning) {
				StartScan();
				handler.PostDelayed(() => {
					StopScan();
				}, SCAN_TIME);
			} else {
				StopScan();
			}
		}

		private void StartScan() {
			service.StartScan();
		}

		private void StopScan() {
			service.StopScan();
			handler.RemoveCallbacksAndMessages(null);
		}

		public static void D(string msg) {
			Log.Debug(typeof(BluetoothScanActivity).Name, msg);
		}

		private class Adapter : RecyclerView.Adapter {
			public event Action<int, BluetoothConnection> onRowClicked;

			public List<BluetoothConnection> list = new List<BluetoothConnection>();

			public override int ItemCount {
				get {
					return list.Count;
				}
			}

			/// <summary>
			/// Raises the attached to recycler view event.
			/// </summary>
			/// <param name="recyclerView">Recycler view.</param>
			public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
				base.OnAttachedToRecyclerView(recyclerView);
				if (recyclerView.GetLayoutManager() == null) {
					recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
				}
			}

			public override int GetItemViewType(int position) {
				return 0;
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_device, parent, false) as ViewGroup;
				return new DeviceViewHolder(view);
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
				holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
					ClickRow(position);

				}));
				((DeviceViewHolder)holder).Bind(list[position]);
			}

			public void AddConnection(BluetoothConnection connection) {
				if (!list.Contains(connection)) {
					var index = list.Count;
					BluetoothScanActivity.D("There are " + index + " connections now");
					list.Add(connection);
			  	NotifyItemInserted(index);
//					NotifyDataSetChanged();
				}
			}

			public void UpdateConnection(BluetoothConnection connection) {
				var index = list.IndexOf(connection);
				NotifyItemChanged(index);
			}

			private void ClickRow(int position) {
				if (onRowClicked != null) {
					onRowClicked(position, list[position]);
				}
			}
		}

		private class DeviceViewHolder : RecyclerView.ViewHolder {
			public BluetoothConnection connection;

			private TextView name, address, state;

			public DeviceViewHolder(ViewGroup parent) : base(parent) {
				name = parent.FindViewById<TextView>(Resource.Id.name);
				address = parent.FindViewById<TextView>(Resource.Id.address);
				state = parent.FindViewById<TextView>(Resource.Id.state);
			}

			public void Bind(BluetoothConnection connection) {
				this.connection = connection;
				var device = connection.device;

				name.Text = device.Name;
				address.Text = device.Address;
				state.Text = connection.connectionState.ToString();
			}
		}
	}
}
