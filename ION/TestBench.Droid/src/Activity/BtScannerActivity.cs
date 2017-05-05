namespace TestBench.Droid.Activity {

	using System.Collections.Generic;

	using Android.App;
	using Android.Graphics;
	using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Devices;

	using TestBench.Droid.Bluetooth;

	public class BtScannerActivity : Activity, SwipeRefreshLayout.IOnRefreshListener {

		private HashSet<EDeviceModel> allowedDeviceModels;

		private RecyclerView list;

		private Adapter adapter;
		private BtScanner scanner;

		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			SetContentView(Resource.Layout.activity_btscanner);

			list = FindViewById<RecyclerView>(Resource.Id.list);
			var empty = FindViewById(Resource.Id.empty);

			list.SetAdapter(adapter = new Adapter(list, empty));
		}

		// Implemented from SwipeRefreshLayout.IOnRefreshListener
		public void OnRefresh() {
			StartScan();
		}

		public void SetAllowedDeviceModels(IEnumerable<EDeviceModel> models) {
			allowedDeviceModels = new HashSet<EDeviceModel>(models);
		}

		private void StartScan() {
			scanner.StartScan();
		}

		private void StopScan() {
			scanner.StopScan();
		}


		internal delegate void OnCheckChanged(HashSet<BtScanner.IONBtDevice> devices);
		internal class Adapter : RecyclerView.Adapter {
			public event OnCheckChanged onCheckChanged;

			public override int ItemCount {
				get {
					return devices.Count;
				}
			}

			private RecyclerView list;
			private View emptyView;
			private List<BtScanner.IONBtDevice> devices = new List<BtScanner.IONBtDevice>();
			public HashSet<BtScanner.IONBtDevice> checkedDevices = new HashSet<BtScanner.IONBtDevice>();

			public Adapter(RecyclerView list, View emptyView) {
				this.list = list;
				this.emptyView = emptyView;
				list.Visibility = ViewStates.Gone;
				emptyView.Visibility =  ViewStates.Visible;
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var ret = new ViewHolder(parent);

				ret.ItemView.Click += (sender, e) => {
					if (ret.device == null) {
						return;
					}

					if (checkedDevices.Contains(ret.device)) {
						checkedDevices.Remove(ret.device);
						ret.isChecked = false;
					} else {
						checkedDevices.Add(ret.device);
						ret.isChecked = true;
					}

					if (onCheckChanged != null) {
						onCheckChanged(checkedDevices);
					}
				};

				return ret;
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
				((ViewHolder)holder).Bind(devices[position]);
			}

			public override long GetItemId(int position) {
				return position;
			}

			public void SetConnections(IEnumerable<BtScanner.IONBtDevice> device) {
				this.devices.Clear();
				this.devices.AddRange(device);
				NotifyDataSetChanged();

				if (this.devices.Count <= 0) {
					list.Visibility = ViewStates.Gone;
					emptyView.Visibility =  ViewStates.Visible;
				} else {
					list.Visibility = ViewStates.Visible;
					emptyView.Visibility =  ViewStates.Gone;
				}
			}

			public void AddDevice(BtScanner.IONBtDevice device) {
				if (devices.Contains(device)) {
					return;
				}
				var pos = devices.Count;
				devices.Add(device);
				NotifyItemInserted(pos);
				list.Visibility = ViewStates.Visible;
				emptyView.Visibility =  ViewStates.Gone;
			}

			public void Clear() {
				checkedDevices.Clear();
				devices.Clear();
				NotifyDataSetChanged();
			}

			public HashSet<BtScanner.IONBtDevice> GetCheckedDevices() {
				return checkedDevices;
			}
		}

		internal class ViewHolder : RecyclerView.ViewHolder {
			public BtScanner.IONBtDevice device;
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

			public void Bind(BtScanner.IONBtDevice device) {
				this.device = device;
				serialNumber.Text = device.serialNumber.ToString();
			}
		}
	}
}
