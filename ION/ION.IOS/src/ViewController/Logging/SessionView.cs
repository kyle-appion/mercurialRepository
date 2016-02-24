using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class SessionView {
    
    public SessionView(UIView mainView) {
      sView = new UIView(new CGRect(.01 * mainView.Bounds.Width, .2 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .75 * mainView.Bounds.Height));
      //sView = new UIView(new CGRect(.01 * mainView.Bounds.Width, .2 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .1 * mainView.Bounds.Height));
      sView.Layer.CornerRadius = 8;
      sView.BackgroundColor = UIColor.White;
      sView.Layer.BorderColor = UIColor.Black.CGColor;
      sView.Layer.BorderWidth = 1f;

      expanded = true;
      sView.Hidden = true;   

      activityLoadingSessions = new UIActivityIndicatorView();
    }

    public UIView sView;
    public UIActivityIndicatorView activityLoadingSessions;
    public bool expanded;
  }
}

