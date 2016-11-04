namespace BluetoothTesting {

	using Android.App;
	using Android.Bluetooth;
	using Android.Content;
	using Android.Content.PM;
	using Android.Support.V4.App;
	using Android.Support.V4.Content;
	using Android.Widget;
	using Android.OS;

	[Activity(Label = "BluetoothTesting", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, ActivityCompat.IOnRequestPermissionsResultCallback {
		/// <summary>
		/// The request code that is used to request the location permissions from the user.
		/// </summary>
		public const int REQUEST_LOCATION_PERMISSIONS = 1;

		/// <summary>
		/// The request code that is used when the app requests to enable bluetooth.
		/// </summary>
		public const int REQUEST_BLUETOOTH_ENABLE = -1;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Text = "Click to start bluetooth test activity";

			button.Click += (sender, e) => {
				AttemptToLaunchBluetoothActivity();
			};
		}

		public void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
			switch (requestCode) {
				case REQUEST_LOCATION_PERMISSIONS:
					AttemptToLaunchBluetoothActivity();
					break;
			}
		}

		private void AttemptToLaunchBluetoothActivity() {
			var manager = GetSystemService(BluetoothService) as BluetoothManager;
			var adapter = manager.Adapter;

			if (Permission.Granted != ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessCoarseLocation)) {
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
				StartActivity(new Intent(this, typeof(BluetoothScanActivity)));
			}
		}

		private void RequestLocationPermission() {
			ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.AccessCoarseLocation }, REQUEST_LOCATION_PERMISSIONS);
		}
	}
}

