using System;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.Util;

using ION.IOS.Util;

namespace ION.IOS.ViewController {
  public class BaseIONViewController : UIViewController {

    public const string VC_DEVICE_MANAGER = "viewControllerDeviceManager";
    public const string VC_FLUID_MANAGER = "viewControllerFluidManager";
    public const string VC_PT_CHART = "viewControllerPTChart";
    public const string VC_SENSOR_ALARMS = "viewControllerSensorAlarms";
    public const string VC_SETTINGS = "viewControllerSettings";
    public const string VC_SUPERHEAT_SUBCOOL = "viewControllerSuperheatSubcool";
    public const string VC_WORKBENCH = "viewControllerWorkbench";

    /// <summary>
    /// The action that is called when the back button is clicked in the navigation
    /// bar.
    /// </summary>
    public Action backAction { get; set; }
    /// <summary>
    /// The primary root view controller for the application.
    /// </summary>
    /// <param name="handle">Handle.</param>
    public IONPrimaryScreenController root { get; set; }

    /// <summary>
    /// Whether or not the viewcontroller was created as a popover or a primary screen.
    /// </summary>
    /// <value><c>true</c> if is pop over; otherwise, <c>false</c>.</value>
    private bool isPopOver { get; set; }

    public BaseIONViewController(IntPtr handle) : base(handle) {
      // Nope
    }

    // Overridden from ViewController
    public override void ViewDidAppear(bool animated) {
      base.ViewDidAppear(animated);
    }

    // Overridden from ViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewDidDisappear(animated);
    }

    /// <summary>
    /// The base method for initializing the view controller's back button.
    /// </summary>
    /// <param name="iconName">Icon name.</param>
    /// <param name="popViewController">If true, then when the back button is pressed, we
    /// will pop the view controller instead of calling the backAction</param>
    protected virtual void InitNavigationBar(string iconName, bool popViewController=false) {
      if (NavigationItem == null) {
        Log.D(this, "Failed to initialize navigation bar: null");
        return;
      }

      /*
//      var bar = NavigationController.NavigationBar;
      var bar = UINavigationBar.Appearance;
      bar.BackIndicatorImage = UIImage.FromBundle(iconName);
      bar.BackIndicatorTransitionMaskImage = bar.BackIndicatorImage;
      bar.TintColor = new UIColor(Colors.BLACK);
      */


      isPopOver = popViewController;

      var leftContainer = new UIView();
      leftContainer.Frame = new CGRect(0, 0, 50, 40);
      leftContainer.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (popViewController) {
          NavigationController.PopViewController(true);
        } else {
          if (backAction != null) {
            backAction();
          }
        }
      }));

      var icon = new UIImageView(UIImage.FromBundle(iconName));
      icon.Frame = new CGRect(0, 0, 40, 40);
      icon.ContentMode = UIViewContentMode.ScaleAspectFit;

      leftContainer.AddSubview(icon);
      var left = new UIBarButtonItem(leftContainer);
      left.Style = UIBarButtonItemStyle.Bordered;

      NavigationItem.LeftBarButtonItem = left;
      /*
      if (popViewController) {
        NavigationController.InteractivePopGestureRecognizer.Delegate = this;// new UIGestureRecognizerDelegate();
      }
      */
    }

    /// <summary>
    /// Inflates an ION view controller from the storyboard.
    /// </summary>
    /// <returns>The view controller.</returns>
    /// <param name="key">Key.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T InflateViewController<T>(string key) where T : BaseIONViewController {
      var ret = (T)Storyboard.InstantiateViewController(key);
      ret.root = root;
      return ret;
    }
  }
}

