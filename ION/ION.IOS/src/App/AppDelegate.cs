 namespace ION.IOS.App {

 	using System;

	using Foundation;
	using UIKit;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Net;

	using ION.IOS.ViewController.Walkthrough;
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
/*
      var nb = UINavigationBar.Appearance;
      nb.BarTintColor = new UIColor(Colors.LIGHT_GRAY);
      nb.TintColor = new UIColor(Colors.BLACK);
      nb.SetTitleTextAttributes(new UITextAttributes() {
        TextColor = new UIColor(Colors.BLACK),
      });
*/


      AppState.context = ion = new IosION();
      try {
        ion.Init().Wait();
      } catch (Exception e) {
        Log.E(this, "Failed to initialize ion.", e);
        Environment.Exit(1);
      }
      ion.webServices = new WebPayload();
      // create a new window instance based on the screen size
      Window = new UIWindow(UIScreen.MainScreen.Bounds);

      Window.RootViewController = STORYBOARD.InstantiateInitialViewController();

      // make the window visible
      Window.MakeKeyAndVisible();
			NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
      
      var preWalkthrough = NSUserDefaults.StandardUserDefaults.StringForKey("walkthrough_complete");
      
      if(string.IsNullOrEmpty(preWalkthrough)){
    	  var ionvc = (IONPrimaryScreenController)Window.RootViewController;
    	  var appdnavigation = ionvc.navigation;
  
	      var ret = (WalkthroughScreenshotViewController)STORYBOARD.InstantiateViewController(BaseIONViewController.VC_WALKTHROUGH_MENU);
	      ret.initial = true;
  	  	
       	var nvc = ((UINavigationController)appdnavigation.CurrentViewController).VisibleViewController;
      	nvc.NavigationController.PushViewController(ret, true);   	  	
				NSUserDefaults.StandardUserDefaults.SetString("1","walkthrough_complete");

			} else {
				Console.WriteLine("Checking app version for popup");
	      //*********CHECK APP VERSION!!!!!
				//var record = KeychainAccess.ValueForKey("lastUsedVersion");
				var record = NSUserDefaults.StandardUserDefaults.StringForKey("lastUsedVersion");
				
				if(!string.IsNullOrEmpty(record)){
					var currentVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
				 	var latestVersion = record;
					
				 	if(!currentVersion.Equals(latestVersion)){
						var window = UIApplication.SharedApplication.KeyWindow;
					    var vc = window.RootViewController;
					    while (vc.PresentedViewController != null) {
					      vc = vc.PresentedViewController;
					    }
					    var updateView = new WhatsNewView(vc.View,vc);			    
					    vc.View.AddSubview(updateView.infoView);
						//KeychainAccess.SetValueForKey(NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(),"lastUsedVersion");
						NSUserDefaults.StandardUserDefaults.SetString(NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(), "lastUsedVersion");
					}
					////////USE THIS TO RESET VERSION NUMBER FOR TESTING WHAT'S NEW POPUP/////////
					//else{
					//	NSUserDefaults.StandardUserDefaults.SetString("1.0","lastUsedVersion");
					//}
				} else {
					var window = UIApplication.SharedApplication.KeyWindow;
		    	var vc = window.RootViewController;
		    	while (vc.PresentedViewController != null) {
		     	 vc = vc.PresentedViewController;
		    	}
		    	var updateView = new WhatsNewView(vc.View,vc);			    
		    	vc.View.AddSubview(updateView.infoView);
					//KeychainAccess.SetValueForKey(NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(),"lastUsedVersion");
					NSUserDefaults.StandardUserDefaults.SetString(NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(), "lastUsedVersion");
				}			
			}
			
			/*********************CHECK REPORTING DEFAULTS***************************/
			var defaultSpreadsheet = NSUserDefaults.StandardUserDefaults.StringForKey("user_spreadsheet_default");
			if(string.IsNullOrEmpty(defaultSpreadsheet)){
				NSUserDefaults.StandardUserDefaults.SetString("0","user_spreadsheet_default");
				Console.WriteLine("Set blank user pdf setting to " + NSUserDefaults.StandardUserDefaults.StringForKey("user_spreadsheet_default"));
			}
			var defaultData = NSUserDefaults.StandardUserDefaults.StringForKey("user_data_default");
			if(string.IsNullOrEmpty(defaultData)){
				NSUserDefaults.StandardUserDefaults.SetString("0","user_data_default");
				Console.WriteLine("Set blank user raw data setting to " + NSUserDefaults.StandardUserDefaults.StringForKey("user_data_default"));
			}
			var defaultPDF = NSUserDefaults.StandardUserDefaults.StringForKey("user_pdf_default");
			if(string.IsNullOrEmpty(defaultPDF)){
				NSUserDefaults.StandardUserDefaults.SetString("0","user_pdf_default");
				Console.WriteLine("Set blank user pdf setting to " + NSUserDefaults.StandardUserDefaults.StringForKey("user_spreadsheet_default"));
			}
      //************************************************************************
      /*********************CHECK TRENDING DEFAULTS***************************/
      var defaultTrending = NSUserDefaults.StandardUserDefaults.StringForKey("settings_default_trending_interval");
      if(string.IsNullOrEmpty(defaultTrending)){
        NSUserDefaults.StandardUserDefaults.SetInt(100,"settings_default_trending_interval");
        Console.WriteLine("No trending default was set so it is now");        
      }
      //************************************************************************
			//**********CHECK RSS FEED UPDATES!!!!!!!!
			
        var feedPull = new ViewController.RssFeed.RssFeedCheck();
        feedPull.BeginReadXMLStreamSingle();
		  
			//************************************************************************
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
			var ion = AppState.context as IosION;

      if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval") == 1) {
        if (intervalWarning == true) {
          UIAlertView loggingWarning = new UIAlertView(Util.Strings.Report.LOGGINGINTERVAL, Util.Strings.Report.LOWINTERVAL, null,Util.Strings.CLOSE, Util.Strings.RETURNSETTINGS);
          loggingWarning.Clicked += (sender, e) => {
            if(e.ButtonIndex.Equals(1)){
              UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
            }
          };
          loggingWarning.Show();
        }
      } 

      if (ion.settings.leaveScreenOn) {
        UIApplication.SharedApplication.IdleTimerDisabled = true;
      }
      if (ion.settings._location.allowsGps) {
      	Console.WriteLine("Came back and using geolocation");
        ion.locationManager.StartAutomaticLocationPolling();
      }else {
      	Console.WriteLine("Came back and not using geolocation");
        ion.locationManager.StopAutomaticLocationPolling();
      }
      
        string lastDate = NSUserDefaults.StandardUserDefaults.StringForKey("rssCheck");
        
        if (!string.IsNullOrEmpty(lastDate) && DateTime.Parse(lastDate).ToLocalTime().AddHours(24) <= DateTime.Now.ToLocalTime()){
            var feedPull = new ViewController.RssFeed.RssFeedCheck();
            feedPull.BeginReadXMLStreamSingle();
        }      
    }

    public override void OnActivated(UIApplication application) {
      // Restart any tasks that were paused (or not yet started) while the application was inactive. 
      // If the application was previously in the background, optionally refresh the user interface.
    }


    public override void WillTerminate(UIApplication application) {
      // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
      try {
        if(ion.dataLogManager.isRecording){
          var done = ion.dataLogManager.StopRecording().Result;
        }
        if(KeychainAccess.ValueForKey("stayLogged") == "no"){
					KeychainAccess.SetValueForKey(null,"userID");
					KeychainAccess.SetValueForKey(null,"userName");
					KeychainAccess.SetValueForKey(null,"userPword");
				}

        if (Reachability.LocalWifiConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork) {
          try {
            Log.UploadLogs();
          } catch (Exception ee) {
            Log.E(this, "Failed to upload logs", ee);
          }
        }


  			var userID = KeychainAccess.ValueForKey("userID");
			  ion.webServices.updateOnlineStatus("0",userID);
        ion.SaveWorkbenchAsync().Wait();
        ion.Dispose();
      } catch (Exception e) {
        Log.E(this, "Failed to terminate ion instance", e);
      }
    }
  }
}


