using System;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.Util;

namespace ION.IOS.ViewController {
  public class BaseIONViewController : UIViewController {

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

    public BaseIONViewController(IntPtr handle) : base(handle) {
      // Nope
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
      /*
      leftContainer.AddConstraint(NSLayoutConstraint.Create(leftContainer, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, 
        icon, NSLayoutAttribute.Trailing, (nfloat)1, (nfloat)0));
      */
      
      var left = new UIBarButtonItem(leftContainer);
      left.Style = UIBarButtonItemStyle.Bordered;

      NavigationItem.LeftBarButtonItem = left;
    }
  }
}

