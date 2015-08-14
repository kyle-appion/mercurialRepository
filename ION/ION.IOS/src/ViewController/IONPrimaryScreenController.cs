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
    protected FlyoutNavigationController navigation { get; private set; }


    /// <summary>
    /// The main navigation items. These lead to primary screen controllers. [Except for
    /// the device manager, but that screen is so critical to the application that it
    /// belongs in this category.]
    /// </summary>
    private UIViewController[] mainNavigation { get; set; }

    /// <summary>
    /// The calculator navigation items. These lead to the calculator tools that will aid
    /// the technician in calculating properties of a job or work effort.
    /// </summary>
    private UIViewController[] calculatorNavigation { get; set; }

    /// <summary>
    /// The navigation items that will let the user view reports abouts jobs and/or other
    /// work that has been saved by the application.
    /// </summary>
    private UIViewController[] reportNavigation { get; set; }

    /// <summary>
    /// The navigation items that will be used to allow the user to configure the application
    /// or get help in how to use some of its features.
    /// </summary>
    private UIViewController[] configurationNavigation { get; set; }

    /// <summary>
    /// The navigation items that will be used to allow the user to exit the application.
    /// </summary>
    private UIViewController[] exitNavigation { get; set; }

		public IONPrimaryScreenController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitControllers();

      // Create the navigation drawer
      navigation = new FlyoutNavigationController();
      navigation.View.Frame = UIScreen.MainScreen.Bounds;
//      navigation.NavigationController.View.BackgroundColor = new UIColor(Colors.BLACK);
      View.AddSubview(navigation.View);

      navigation.NavigationRoot = new RootElement("Navigation Menu") {
        new Section ("MAIN") {
          new StringElement ("Analyzer"),
          new StringElement ("Device Manager"),
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

      //navigation.ViewControllers = mainNavigation.Concat(calculatorNavigation, reportNavigation, configurationNavigation, exitNavigation);
      navigation.ViewControllers = new UIViewController[] {
        new UINavigationController ((AnalyzerViewController)this.Storyboard.InstantiateViewController("analyzerViewController")),
        new UINavigationController ((DeviceManagerViewController)this.Storyboard.InstantiateViewController("deviceManagerViewController")),
        new UINavigationController ((WorkbenchViewController)this.Storyboard.InstantiateViewController("workbenchViewController")),
        new UINavigationController ((PressureTemperatureViewController)this.Storyboard.InstantiateViewController("pressureTemperatureViewController")),
        new UINavigationController ((SuperheatSubcoolViewController)this.Storyboard.InstantiateViewController("superheatSubcoolViewController")),
      };
      // Create the menu
    }

    private void InitControllers() {
      InitMainControllers();
      InitCalculatorControllers();
    }

    private void InitMainControllers() {
      var app = UIApplication.SharedApplication.Delegate as AppDelegate;
      mainNavigation = new UIViewController[] {
        app.analyzerViewController,
        app.deviceManagerViewController,
        app.workbenchViewController,
      };
    }

    private void InitCalculatorControllers() {
      var app = UIApplication.SharedApplication.Delegate as AppDelegate;
      calculatorNavigation = new UIViewController[] {
        app.pressureTemperatureViewController,
        app.superheatSubCoolViewController,
      };
    }
	}
}
