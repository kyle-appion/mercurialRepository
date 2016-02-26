namespace ION.Droid.Activity {
  
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Util;
  using ION.Droid.Widgets.Adapters.Navigation;

  /// <summary>
  /// The activity that is responsible for launching the ION app state.
  /// </summary>
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_logo_appiondefault")]
	public class MainActivity : Activity { 
   
    /// <summary>
    /// The minimum time to wait for the ION context to init.
    /// </summary>
    private static readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(5);

    // Overridden from Activity
		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
      SetContentView(Resource.Layout.activity_main);
      Log.printer = new LogPrinter();
      if (AppState.context == null) {
        StartService(new Intent(this, typeof(AndroidION)));
      }
		}

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();

      Task.Factory.StartNew(InitApplication);
    }

    /// <summary>
    /// Initializes the application context.
    /// </summary>
    private async Task InitApplication() {
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
        string msg = "Failed to start ION. Please contact Appion for assistance.";
        Log.E(this, msg);
        var adb = new AlertDialog.Builder(this);
        adb.SetTitle("Failed to start ION");
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
