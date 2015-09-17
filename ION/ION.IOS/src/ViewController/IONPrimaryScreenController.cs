using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using MonoTouch.Dialog;

using FlyoutNavigation;

using ION.Core.Util;

using ION.IOS.App;
using ION.IOS.Util;
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
        new Section (Strings.Navigation.MAIN) {
//          new StringElement ("Analyzer"),
//          new StringElement ("Device Manager"),
          new StringElement (Strings.Workbench.SELF),
        },
        new Section (Strings.Navigation.CALCULATORS) {
          new StringElement(Strings.Fluid.PT_CHART),
          new StringElement(Strings.Fluid.SUPERHEAT_SUBCOOL),
        },
/*
        new Section (Strings.Navigation.CONFIGURATION) {
          new StringElement(Strings.SETTINGS),
          new StringElement(Strings.HELP),
        }
*/
      };
      navigation.ViewControllers = BuildViewControllers();
      // Create the menu
    }

    /// <summary>
    /// Constructs and initialized the view controllers that are used in the application.
    /// </summary>
    /// <returns>The view controllers.</returns>
    private UIViewController[] BuildViewControllers() {
      var ret = new UINavigationController[] {
        new UINavigationController(InflateViewController<WorkbenchViewController>(BaseIONViewController.VC_WORKBENCH)),
        new UINavigationController(InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART)),
        new UINavigationController(InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL)),
//        new UINavigationController(InflateViewController<SettingsViewController>(BaseIONViewController.VC_SETTINGS)),
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
