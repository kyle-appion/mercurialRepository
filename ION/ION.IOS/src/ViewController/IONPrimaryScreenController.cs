using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using MonoTouch.Dialog;

using FlyoutNavigation;

using ION.Core.App;
using ION.Core.Util;

using ION.IOS.App;
using ION.IOS.UI;
using ION.IOS.Util;
using ION.IOS.ViewController.Analyzer;
using ION.IOS.ViewController.PressureTemperatureChart;
using ION.IOS.ViewController.Settings;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.Workbench;

namespace ION.IOS.ViewController {
	public partial class IONPrimaryScreenController : UIViewController {
    /// <summary>
    /// The view controller that will manage the navigation items for the screen.
    /// </summary>
    /// <value>The navigation.</value>
    public FlyoutNavigationController navigation { get; private set; }


		public IONPrimaryScreenController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      // Create the navigation drawer
      navigation = new FlyoutNavigationController();
      navigation.View.Frame = UIScreen.MainScreen.Bounds;
//      navigation.NavigationController.View.BackgroundColor = new UIColor(Colors.BLACK);
      View.AddSubview(navigation.View);

      navigation.NavigationRoot = new RootElement("BS Navigation Menu") {
        new Section (Strings.Navigation.MAIN.ToUpper()) {
//          new ImageStringElement(Strings.Analyzer.SELF, UIImage.FromBundle("ic_nav_analyzer")),
          new ImageStringElement(Strings.Workbench.SELF, UIImage.FromBundle("ic_nav_workbench")),
        },
        new Section (Strings.Navigation.CALCULATORS.ToUpper()) {
          new ImageStringElement(Strings.Fluid.PT_CHART, UIImage.FromBundle("ic_nav_pt_chart")),
          new ImageStringElement(Strings.Fluid.SUPERHEAT_SUBCOOL, UIImage.FromBundle("ic_nav_superheat_subcool")),
        },
        new Section (Strings.Navigation.CONFIGURATION.ToUpper()) {
          new ImageStringElement(Strings.SETTINGS, OnNavSettingsClicked, UIImage.FromBundle("ic_settings")),
//          new ImageStringElement(Strings.HELP, null),
        },
      };
      navigation.ViewControllers = BuildViewControllers();
      // Create the menu
    }

    /// <summary>
    /// Opens the application's settings.
    /// </summary>
    private void OnNavSettingsClicked() {
      UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
    }

    private void OnNavHelpClicked() {
      // TODO Do Helpful things
    }

    /// <summary>
    /// Constructs and initialized the view controllers that are used in the application.
    /// </summary>
    /// <returns>The view controllers.</returns>
    private UIViewController[] BuildViewControllers() {
      var ret = new UINavigationController[] {
//        new UINavigationController(InflateViewController<AnalyzerViewController>(BaseIONViewController.VC_ANALYZER)),
        new UINavigationController(InflateViewController<WorkbenchViewController>(BaseIONViewController.VC_WORKBENCH)),
        new UINavigationController(InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART)),
        new UINavigationController(InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL)),
        null, // Settings navigation
//        null, // Help Navigation
      };

      return ret;
    }

    /// <summary>
    /// Inflates an ION view controller from the storyboard.
    /// </summary>
    /// <returns>The view controller.</returns>
    /// <param name="key">Key.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    private T InflateViewController<T>(string key) where T : BaseIONViewController {
      var ret = (T)Storyboard.InstantiateViewController(key);
      ret.root = this;
      return ret;
    }
	}
}
