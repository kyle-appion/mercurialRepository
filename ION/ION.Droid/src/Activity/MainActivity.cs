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
    private static readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(3);

    // Overridden from Activity
		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
      SetContentView(Resource.Layout.activity_main);
      Log.printer = new LogPrinter();
		}

    // Overridden from Activity
    protected override async void OnResume() {
      base.OnResume();

      if (!(await InitApplication())) {
        string msg = "Failed to initialize ION. Please contact Appion for assistance.";
        Log.E(this, msg);
        Toast.MakeText(this, msg, ToastLength.Short);
        Finish();
      } else {
        StartActivity(typeof(HomeActivity));
        Finish();
      }
    }

    /// <summary>
    /// Initializes the application context.
    /// </summary>
    private async Task<bool> InitApplication() {
      var ion = AppState.context as AndroidION;

      if (ion == null) {
        try {
          AppState.context = ion = new AndroidION(ApplicationContext);

          var start = DateTime.Now;

          if (await ion.Init()) {
            // Wait for minimum time.
            var d = DateTime.Now - start;

            if (d < WAIT_TIME) {
              await Task.Delay(WAIT_TIME - d);
            }

            AppState.context = ion;
            return true;
          } else {
            // TODO ahodder@appioninc.com: display error message dialog at this point
            return false;
          }
        } catch (Exception e) {
          Log.E(this, "Failed to init ion", e);
          return false;
        }
      } else {
        Log.D(this, "The ION context is already alive");
        return true;
      }
    }
	}
}


