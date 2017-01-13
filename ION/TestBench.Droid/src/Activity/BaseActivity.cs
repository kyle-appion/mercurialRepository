namespace TestBench.Droid.Activity {

	using Android.App;
	using Android.Bluetooth;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V4.App;
	using Android.Support.V4.Content;
	using Android.Widget;

	[Activity(Label = "Activity")]
	public class BaseActivity : Activity, IServiceConnection {
		public const int REQUEST_LOCATION_PERMISSIONS = 1;
		public const int REQUEST_BLUETOOTH_ENABLE = 2;

		public Prefs prefs { get { return Prefs.Get(this); } }

		protected AppService service { get; set; }

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			StartService(new Intent(this, typeof(AppService)));
		}

		protected override void OnResume() {
			base.OnResume();
			AttemptToBindAppService();
		}

		protected override void OnPause() {
			base.OnPause();
			if (service != null) {
				service.onScanStateChanged -= OnScanStateChanged;
				service.onConnectionFound -= OnConnectionFound;
				UnbindService(this);
			}
		}

		public void OnServiceConnected(ComponentName name, IBinder binder) {
			service = ((AppService.Binder)binder).service;
			service.onScanStateChanged += OnScanStateChanged;
			service.onConnectionFound += OnConnectionFound;
			service.onRigFound += OnRigFound;
			OnServiceBound();
		}

		public void OnServiceDisconnected(ComponentName name) {
			Toast.MakeText(this, "Service has disconnected", ToastLength.Short).Show();
		}

		public virtual void OnServiceBound() {
		}

		public virtual void OnScanStateChanged(AppService service) {
		}

		public virtual void OnConnectionFound(IConnection connection) {
		}

		public virtual void OnRigFound(IRig rig) {
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
			switch (requestCode) {
				case REQUEST_LOCATION_PERMISSIONS:
					AttemptToBindAppService();
					break;
			}
		}

		private void AttemptToBindAppService() {
			var manager = GetSystemService(BluetoothService) as BluetoothManager;
			var adapter = manager.Adapter;

			if (Permission.Granted != ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation)) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Feed me permissions!");
				adb.SetMessage("We need location permissions to work!");
				adb.SetNegativeButton("Become Usless", (sender, e) => {
					Toast.MakeText(this, "Good luck making me do anything for you, then", ToastLength.Long).Show();
				});
				adb.SetPositiveButton("Give Perms", (sender, e) => {
					RequestLocationPermission();
				});
				adb.Show();
			} else if (!adapter.IsEnabled) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Feed me blueteeth!");
				adb.SetMessage("We need blueteeth to make bluetooth do anything.");
				adb.SetNegativeButton("Starve", (sender, e) => {
					Toast.MakeText(this, "Fine. Sit here", ToastLength.Long).Show();
				});
				adb.SetPositiveButton("Feed", (sender, e) => {;
					var intent = new Intent(BluetoothAdapter.ActionRequestEnable);
					StartActivityForResult(intent, REQUEST_BLUETOOTH_ENABLE);
				});
				adb.Show();
			} else {
				BindService(new Intent(this, typeof(AppService)), this, Bind.AutoCreate);
			}
		}

		private void RequestLocationPermission() {
			ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION_PERMISSIONS);
		}
	}
}
