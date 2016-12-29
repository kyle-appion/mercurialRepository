namespace ION.IOS.UI {

	using System;

	using UIKit;

	using Appion.Commons.Util;

  /// <summary>
  /// Extensions to the UIAlertController
  /// </summary>
  public static class UIAlertControllerExtensions {
    public static bool Show(this UIAlertController alert, bool animated = true, Action completion = null) {
      var rootVC = UIApplication.SharedApplication.KeyWindow.RootViewController;
      if (rootVC != null) {
        PresentViewController(alert, rootVC, animated, completion);
        return true;
      } else {
        Log.E(alert, "Failed to show UIAlertController: root view controller was null.");
        return false;
      }


      /*
      var window = new UIWindow(UIScreen.MainScreen.Bounds);
      window.RootViewController = new UIViewController();
      window.WindowLevel = UIWindowLevel.Alert + 1;
      window.MakeKeyAndVisible();
      window.RootViewController.PresentViewController(alert, true, null);
      */
    }

    /// <summary>
    /// Presents the showMe view controller to the root of the given controller..
    /// </summary>
    /// <param name="showMe">Show me.</param>
    /// <param name="controller">Controller.</param>
    /// <param name="animated">If set to <c>true</c> animated.</param>
    /// <param name="completion">Completion.</param>
    private static void PresentViewController(UIAlertController showMe, UIViewController controller, bool animated, Action completion) {
      if (controller is UINavigationController) {
        var vc = controller as UINavigationController;
        PresentViewController(showMe, vc.VisibleViewController, animated, completion);
      } else if (controller is UITabBarController) {
        var vc = controller as UITabBarController;
        PresentViewController(showMe, vc.SelectedViewController, animated, completion);
      } else {
        controller.PresentViewController(showMe, animated, completion);
      }
    }

    /// <summary>
    /// Dismisses the alert controller from the screen.
    /// </summary>
    /// <param name="alert">Alert.</param>
    public static void Dismiss(this UIAlertController alert) {
      alert.RemoveFromParentViewController();
    }
  }
}

