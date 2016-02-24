using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class JobView : UIView {
    
    public JobView(UIView mainView) {
      jView = new UIView(new CGRect(.01 * mainView.Bounds.Width, .2 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .75 * mainView.Bounds.Height));
      //jView = new UIView(new CGRect(.01 * mainView.Bounds.Width, .2 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .1 * mainView.Bounds.Height));
      jView.Layer.CornerRadius = 8;
      jView.BackgroundColor = UIColor.White;
      jView.Layer.BorderColor = UIColor.Black.CGColor;
      jView.Layer.BorderWidth = 1f;

      expanded = true;

      createJob = new UIButton(new CGRect(0,0,jView.Bounds.Width, .07 * mainView.Bounds.Height));
      createJob.SetTitle("Create New Job", UIControlState.Normal);
      createJob.SetTitleColor(UIColor.Black, UIControlState.Normal);
      createJob.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      createJob.Layer.BorderColor = UIColor.Black.CGColor;
      createJob.Layer.BorderWidth = 2f;

      jView.AddSubview(createJob);

      activityLoadingJobs = new UIActivityIndicatorView();

      createJob.TouchUpInside += (sender, e) => {createJob.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      createJob.TouchDown += (sender, e) => {createJob.BackgroundColor = UIColor.Blue;};
      createJob.TouchUpOutside += (sender, e) => {createJob.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
    }

    public UIView jView;
    public UIButton createJob;
    public UIActivityIndicatorView activityLoadingJobs;
    public bool expanded;
  }
}