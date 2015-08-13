using System;
using System.IO;

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
using ION.IOS.ViewController.Calculators;
using ION.IOS.ViewController.Navigation;
using ION.IOS.ViewController.Main;

namespace ION.IOS.App {
  // The UIApplicationDelegate for the application. This class is responsible for launching the
  // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
  [Register("AppDelegate")]
  public class AppDelegate : UIApplicationDelegate {
    private static UIStoryboard STORYBOARD = UIStoryboard.FromName("ApplicationStoryboard", null);

    public override UIWindow Window { get; set; }

    /// <summary>
    /// The current ion context for the application.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }

    /// <summary>
    /// The instance of the device manager view controller that will allow the user to select
    /// device for other controller's.
    /// </summary>
    public DeviceManagerViewController deviceManagerViewController { get; private set; }
    /// <summary>
    /// The instance of the workbench view controller that will display a workbench to the
    /// user.
    /// </summary>
    public WorkbenchViewController workbenchViewController { get; private set; }
    /// <summary>
    /// The instance of an analyzer view controller that will display an analyzer to the user.
    /// </summary>
    public AnalyzerViewController analyzerViewController { get; private set; }
    /// <summary>
    /// The instance of a pressure temperature view controller that will display the pressure
    /// temperature calculator to the user.
    /// </summary>
    public PressureTemperatureViewController pressureTemperatureViewController { get; private set; }
    /// <summary>
    /// The instance of a superheat subcool view controller that will display the superheat/
    /// subcool calcultor to the user.
    /// </summary>
    public SuperheatSubcoolViewController superheatSubCoolViewController { get; private set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
      // Initialize the application state.

//      application.SetStatusBarHidden(true, UIStatusBarAnimation.None);

      BaseION bi = new BaseION();
      ion = AppState.context = bi;
      bi.fileManager = new IosFileManager();
      bi.deviceManager = new IOSDeviceManager(ion);
      bi.fluidManager = new BaseFluidManager(ion);
      var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      bi.database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, bi);

      bi.deviceManager.Init();

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
    }

    public override void WillEnterForeground(UIApplication application) {
      // Called as part of the transiton from background to active state.
      // Here you can undo many of the changes made on entering the background.
    }

    public override void OnActivated(UIApplication application) {
      // Restart any tasks that were paused (or not yet started) while the application was inactive. 
      // If the application was previously in the background, optionally refresh the user interface.
    }

    public override void WillTerminate(UIApplication application) {
      // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
    }
  }
}


