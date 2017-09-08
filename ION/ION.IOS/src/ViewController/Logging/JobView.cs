using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class JobView : UIView {
    
    public JobView(nfloat startY, nfloat width, nfloat height) {
      jView = new UIView(new CGRect(0, startY, width, height));   
      jView.Layer.CornerRadius = 8;
      jView.BackgroundColor = UIColor.White;
      jView.Layer.BorderColor = UIColor.Black.CGColor;
      jView.Layer.BorderWidth = 1f;

      expanded = true;

      activityLoadingJobs = new UIActivityIndicatorView();

      selectedIndex = 9999;
    }

    public UIView jView;
    public UIActivityIndicatorView activityLoadingJobs;
    public List<JobData> allJobs;
    public bool expanded;
    public int selectedIndex;
  }
}