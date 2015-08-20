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
using ION.IOS.ViewController.Main;
using ION.IOS.ViewController.Calculators;

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

      navigation.NavigationRoot = new RootElement("Navigation Menu") {
        new Section ("MAIN") {
//          new StringElement ("Analyzer"),
//          new StringElement ("Device Manager"),
          new StringElement ("Workbench"),
        },
        new Section ("CALCULATORS") {
          new StringElement("P/T Chart"),
          new StringElement("Superheat/Subcool"),
        },
        /*
        // TODO ahodder@appioninc.com: Localize these strings
        new Section("MAIN") {
          from vc in mainNavigation select new UIViewElement(vc.Title, vc.View, false) as Element
        },
        new Section("CALCUATORS") {
          from vc in calculatorNavigation select new UIViewElement(vc.Title, vc.View, false) as Element
        },
        */
        /*
        new Section("REPORTS") {
          from vc in reportNavigation select new UIViewElement(vc.Title, vc.View, false) as Element
        },
        new Section("CONFIGURATION") {
          from vc in configurationNavigation select new UIViewElement(vc.Title, vc.View, false) as Element
        },
        new Section("EXIT") {
          from vc in exitNavigation select new UIViewElement(vc.Title, vc.View, false) as Element
        }
        */
      };
      /*
      //navigation.ViewControllers = mainNavigation.Concat(calculatorNavigation, reportNavigation, configurationNavigation, exitNavigation);
      navigation.ViewControllers = new UIViewController[] {
//        new UINavigationController ((AnalyzerViewController)this.Storyboard.InstantiateViewController("analyzerViewController")),
//        new UINavigationController ((DeviceManagerViewController)this.Storyboard.InstantiateViewController("deviceManagerViewController")),
        new UINavigationController ((WorkbenchViewController)this.Storyboard.InstantiateViewController("workbenchViewController")),
        new UINavigationController ((PressureTemperatureViewController)this.Storyboard.InstantiateViewController("pressureTemperatureViewController")),
        new UINavigationController ((SuperheatSubcoolViewController)this.Storyboard.InstantiateViewController("superheatSubcoolViewController")),
      };
      */
      navigation.ViewControllers = BuildViewControllers();
      // Create the menu
    }

    /// <summary>
    /// Constructs and initialized the view controllers that are used in the application.
    /// </summary>
    /// <returns>The view controllers.</returns>
    private UIViewController[] BuildViewControllers() {
      var ret = new UINavigationController[] {
        new UINavigationController(InflateViewController<WorkbenchViewController>("workbenchViewController")),
        new UINavigationController(InflateViewController<PressureTemperatureViewController>("pressureTemperatureViewController")),
        new UINavigationController(InflateViewController<SuperheatSubcoolViewController>("superheatSubcoolViewController")),
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
