 namespace ION.IOS.App {

  using System;
  using System.IO;
  using System.Threading.Tasks;

  using Foundation;
  using UIKit;

  using SQLite.Net;
  using SQLite.Net.Interop;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Fluids;
  using ION.Core.Util;

  using ION.IOS.Devices;
  using ION.IOS.IO;
  using ION.IOS.Util;
  using ION.IOS.ViewController;

  // The UIApplicationDelegate for the application. This class is responsible for launching the
  // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
  [Register("AppDelegate")]
  public class AppDelegate : UIApplicationDelegate {
    private static UIStoryboard STORYBOARD = UIStoryboard.FromName("Storyboard", null);

    public override UIWindow Window { get; set; }

    /// <summary>
    /// The current ion context for the application.
    /// </summary>
    /// <value>The ion.</value>
    public IosION ion { get; private set; }

    public bool intervalWarning = false;

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
      // Initialize the application state.
      // Set Navigation Bar preferences
      var nb = UINavigationBar.Appearance;
      nb.BarTintColor = new UIColor(Colors.LIGHT_GRAY);
      nb.TintColor = new UIColor(Colors.BLACK);
      nb.SetTitleTextAttributes(new UITextAttributes() {
        TextColor = new UIColor(Colors.BLACK),
      });

      AppState.context = ion = new IosION();
      try {
        ion.Init().Wait();
      } catch (Exception e) {
        Log.E(this, "Failed to initialize ion.", e);
        Environment.Exit(1);
      }

      var list = new System.Collections.Generic.List<ION.Core.Devices.ISerialNumber>();
      foreach (var device in ion.deviceManager.devices) {
        list.Add(device.serialNumber);
      }
//      new ION.IOS.Net.RequestCalibrationCertificatesTask(ion, list.ToArray()).Request();
      ion.settings.screen.leaveOn = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_screen_leave_on");
      ion.settings.location.useGeoLocation = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_location_use_geolocation");
      ion.settings.alarm.haptic = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_haptic");
      ion.settings.alarm.sound = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_sound");

      if (ion.settings.screen.leaveOn) {        
        UIApplication.SharedApplication.IdleTimerDisabled = true;
      }
      if (ion.settings.location.useGeoLocation) {
        ion.locationManager.StartAutomaticLocationPolling();
      } else {
        ion.locationManager.StopAutomaticLocationPolling();
      }

      if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval") <= 0) {
        NSUserDefaults.StandardUserDefaults.SetInt(30, "settings_default_logging_interval");
      }

      // create a new window instance based on the screen size
      Window = new UIWindow(UIScreen.MainScreen.Bounds);

      Window.RootViewController = STORYBOARD.InstantiateInitialViewController();

      // make the window visible
      Window.MakeKeyAndVisible();

      return true;
    }

    public override void OnResignActivation(UIApplication application) {
      // Invoked when the application is about to move from active to inactive state.
      // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
      // or when the user quits the application and it begins the transition to the background state.
      // Games should use this method to pause the game.

    }

    public override void DidEnterBackground(UIApplication application) {
      // Use this method to release shared resources, save user data, invalidate timers and store the application state.
      // If your application supports background exection this method is called instead of WillTerminate when the user quits.
      UIApplication.SharedApplication.IdleTimerDisabled = false;

      ion.locationManager.StopAutomaticLocationPolling();

      if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval") == 1) {
        intervalWarning = false;
      } else {
        intervalWarning = true;
      }
    }

    public override void WillEnterForeground(UIApplication application) {
      // Called as part of the transiton from background to active state.
      // Here you can undo many of the changes made on entering the background.
      ion.settings.screen.leaveOn = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_screen_leave_on");
      ion.settings.location.useGeoLocation = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_location_use_geolocation");
      ion.settings.alarm.haptic = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_haptic");
      ion.settings.alarm.sound = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_sound");

      if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval") == 1) {
        if (intervalWarning == true) {
          UIAlertView loggingWarning = new UIAlertView("Logging Interval", "Using a 1 second logging interval uses much more disk space and is not recommended", null,"Close","Return to Settings");
          loggingWarning.Clicked += (sender, e) => {
            if(e.ButtonIndex.Equals(1)){
              Console.WriteLine("Return to settings");
              UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
            }
          };
          loggingWarning.Show();
        } 
      } 

      if (ion.settings.screen.leaveOn) {
        UIApplication.SharedApplication.IdleTimerDisabled = true;
      }
      if (ion.settings.location.useGeoLocation) {
        ion.locationManager.StartAutomaticLocationPolling();
      }else {
        ion.locationManager.StopAutomaticLocationPolling();
      }
    }

    public override void OnActivated(UIApplication application) {
      // Restart any tasks that were paused (or not yet started) while the application was inactive. 
      // If the application was previously in the background, optionally refresh the user interface.
    }

    public override void WillTerminate(UIApplication application) {
      try {
        if(ion.dataLogManager.isRecording){
          var done = ion.dataLogManager.StopRecording().Result;
        }
        ion.SaveWorkbenchAsync().Wait();
        ion.Dispose();
      } catch (Exception e) {
        Log.E(this, "Failed to terminate ion instance", e);
      }
      // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
    }
  }
}


