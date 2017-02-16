using ION.Droid.Preferences;
namespace ION.Droid.Activity {
  
  using System;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Support.V4.Content;

	using Appion.Commons.Util;

  using ION.Core.App;

  using ION.Droid.App;
  using ION.Droid.Dialog;
	using ION.Droid.Util;

  /// <summary>
  /// The activity that is responsible for launching the ION app state.
  /// </summary>
  [Activity(Label = "@string/app_name", MainLauncher=true, Icon="@drawable/ic_logo_appiondefault", ScreenOrientation=ScreenOrientation.Portrait)]
	public class MainActivity : IONActivity {    

    /// <summary>
    /// The minimum time to wait for the ION context to init.
    /// </summary>
    private static readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(5);     

    // Overridden from Activity
		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
      SetContentView(Resource.Layout.activity_main);
      Log.printer = new LogPrinter();

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				EnsureBluetoothPermissions();
			} else {
				Task.Factory.StartNew(InitApplication);
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, String[] permissions, Permission[] grantResults) {
			switch (requestCode) {
				case REQUEST_LOCATION_PERMISSIONS: {
						if (grantResults[0] == Permission.Granted) {
							EnsureBluetoothPermissions();
						} else {
							ShowMissingPermissionsDialog(GetString(Resource.String.location));
						}
				} break;
				default:
					base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
					break;
			}
		}

    /// <summary>
    /// Checks that the application has all of the permissions inline to actually start up.
    /// </summary>
    /// <returns>The permissions.</returns>
		private void EnsureBluetoothPermissions() {
      if (Permission.Granted != ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation)) {
        var adb = new IONAlertDialog(this);
        adb.SetTitle(Resource.String.alert);
				adb.SetCancelable(false);
        adb.SetMessage(Resource.String.error_start_up_request_location_for_bluetooth);
        adb.SetPositiveButton(Resource.String.allow, (sender, e) => {
          var d = sender as Dialog;
          d.Dismiss();
          ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION_PERMISSIONS);
        });
        adb.SetNegativeButton(Resource.String.deny, (sender, e) => {
          var d = sender as Dialog;
          d.Dismiss();
          ShowMissingPermissionsDialog(GetString(Resource.String.location));
        });

        adb.Show();
      } else {
        // We are all good and have all of our permissions. Start the application.
        Task.Factory.StartNew(InitApplication);
      }
    }

    /// <summary>
    /// Shows a dialog to the user explaining that the application cannot start due to missing permissions.
    /// </summary>
    /// <param name="permissions">A comma separated string of permissions</param>
    /// <returns>The missing permissions dialog.</returns>
    private void ShowMissingPermissionsDialog(string permissions) {
      var adb = new IONAlertDialog(this);
      adb.SetTitle(Resource.String.error_start_up_fail);
      adb.SetMessage(string.Format(GetString(Resource.String.error_start_up_failed_to_acquire_permissions), permissions));
      adb.SetCancelable(false);
      adb.SetNegativeButton(Resource.String.close, (sender, e) => {
        var d = sender as Dialog;
        d.Dismiss();
        Finish();
      });
      adb.Show();
    }

    /// <summary>
    /// Initializes the application context.
    /// </summary>
    private async Task InitApplication() {
      if (AppState.context == null) {
        StartService(new Intent(this, typeof(AndroidION)));
      }

      var success = false;

      var start = DateTime.Now;
      while (AppState.context == null || !(AppState.context as AndroidION).initialized) {
        if (DateTime.Now - start > WAIT_TIME) {
          success = false;
          break;
        }

        await Task.Delay(50);
      }

      success = true;

      if (!success) {
				string msg = GetString(Resource.String.app_error_failed_to_start_msg);
        Log.E(this, msg);
        var adb = new AlertDialog.Builder(this);
				adb.SetTitle(Resource.String.app_error_failed_to_start);
        adb.SetMessage(msg);

        adb.SetNegativeButton(Android.Resource.String.Cancel, (obj, args) => {
          Finish();
        });

        adb.Show();
      } else {
        StartActivity(typeof(HomeActivity));
        Finish();
      }
    }
	}
}
