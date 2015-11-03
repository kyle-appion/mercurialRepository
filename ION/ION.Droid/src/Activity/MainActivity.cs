namespace ION.Droid.Activity {
  
  using System;
  using System.Threading.Tasks;

  using Android.App;
  using Android.OS;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Util;


	[Activity (Label = "ION.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity { 
   
    /// <summary>
    /// The minimum time to wait for the ION context to init.
    /// </summary>
    private static readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(3);

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
      SetContentView(Resource.Layout.activity_main);
		}

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();

      InitApplication();
    }

    /// <summary>
    /// Initializes the application context.
    /// </summary>
    private async void InitApplication() {
      var ion = new AndroidION(ApplicationContext);
      Log.D(this, "Starting init");

      var start = DateTime.Now;

      if (await ion.Init()) {
        Log.D(this, "Finishing init");
        // Wait for minimum time.
        var d = DateTime.Now - start;
        Log.D(this, "Delta: " + d);

        if (d < WAIT_TIME) {
          Log.D(this, "Waiting for : " + (WAIT_TIME - d));
          await Task.Delay(WAIT_TIME - d);
        }

        Log.D(this, "Starting next activity");
        AppState.context = ion;
        StartActivity(typeof(DeviceManagerActivity));
      } else {
        // TODO ahodder@appioninc.com: display error message dialog at this point
        Toast.MakeText(this, "Failed to initialize ION. Please contact Appion for assistance.", ToastLength.Short);
      }
      Finish();
    }
	}
}


