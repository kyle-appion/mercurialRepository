namespace TestBench.Droid.Activity {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Graphics;
	using Android.OS;
	using Android.Support.Design.Widget;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Devices;

	using TestBench.Droid.Bluetooth;

	[Activity(Label="Test Builder", /*MainLauncher=true, */Icon="@mipmap/icon")]
	public class HomeActivity : BaseActivity {

		private TextInputEditText technicianEntry;
		private Spinner testTypeSpinner;
		private CheckBox calibrate;
		private CheckBox test;

		private SwipeRefreshLayout swiper;
		private RecyclerView list;
		private View empty;


		private Handler handler;
		private BtScanner scanner;
		private Adapter<EDeviceModel> testTypeAdapter;


		// Overridden from Activity
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_home);

			technicianEntry = FindViewById<TextInputEditText>(Resource.Id.name);
			testTypeSpinner = FindViewById<Spinner>(Resource.Id.testType);
			calibrate = FindViewById<CheckBox>(Resource.Id.calibrate);
			test = FindViewById<CheckBox>(Resource.Id.test);

			testTypeAdapter  = new Adapter<EDeviceModel>(new EDeviceModel[] {
				EDeviceModel.AV760,
				EDeviceModel.P300,
				EDeviceModel.P500,
				EDeviceModel.P800,
				EDeviceModel.PT500,
				EDeviceModel.PT800,
			});

			testTypeSpinner.Adapter = testTypeAdapter;

			testTypeSpinner.ItemSelected += (sender, e) => {
				switch (testTypeAdapter[e.Position]) {
					case EDeviceModel.AV760:
						UpdateForVacuum();
						break;
					case EDeviceModel.P300: // Fallthrough
					case EDeviceModel.PT300:
						UpdateForPressure();
						break;
					case EDeviceModel.P500: // Fallthrough
					case EDeviceModel.PT500:
						UpdateForPressure();
						break;
					case EDeviceModel.P800: // Fallthrough
					case EDeviceModel.PT800:
						UpdateForPressure();
						break;
				}
			};

			testTypeSpinner.SetSelection(0);

			UpdateFromSettings();

			scanner = new BtScanner(this);
		}

		protected override void OnResume() {
			base.OnResume();
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
			if (scanner.isScanning) {
				item.SetTitle("Scanning...");
			} else {
				item.SetTitle("Scan");
			}

			return true;
		}

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Resource.Id.scan:
					if (scanner.isScanning) {
						scanner.StopScan();
					} else {
						scanner.StartScan();
					}
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		private void UpdateFromSettings() {
			this.technicianEntry.Text = prefs.certifiedBy;
		}

		private void UpdateForVacuum() {
			calibrate.Checked = false;
			calibrate.Enabled = false;
			calibrate.Visibility = ViewStates.Gone;
		}

		private void UpdateForPressure() {
			calibrate.Enabled = true;
			calibrate.Visibility = ViewStates.Visible;
		}



		/// <summary>
		/// The adapter that will display the supported tests.
		/// </summary>
		private class Adapter<T> : BaseAdapter {
			public override int Count {
				get {
					return items.Count;
				}
			}

			public T this[int index] {
				get {
					return items[index];
				}
			}

			private List<T> items = new List<T>();

			public Adapter(IEnumerable<T> supportedTestTypes) {
				items = new List<T>(supportedTestTypes);
			}

			public override Java.Lang.Object GetItem(int position) {
				throw new NotImplementedException();
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
}
