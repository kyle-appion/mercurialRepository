using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class ChooseReporting : UIView {

    public UIView reportType;
    public UIButton savedReports;
    public UIButton newReport;
    public UILabel step1;
    public UITapGestureRecognizer resize;
    
    public ChooseReporting(UIView mainView) {
      reportType = new UIView(new CGRect(.01 * mainView.Bounds.Width, 0, .98 * mainView.Bounds.Width, .55 * mainView.Bounds.Height));
      reportType.BackgroundColor = UIColor.White;
      reportType.Layer.BorderColor = UIColor.Black.CGColor;
      reportType.Layer.BorderWidth = 1f;
      reportType.Layer.CornerRadius = 8;

      savedReports = new UIButton(new CGRect(.25 * reportType.Bounds.Width,.2 * mainView.Bounds.Height,.5 * reportType.Bounds.Width, .12 * mainView.Bounds.Height));
      savedReports.SetTitle("Saved Reports", UIControlState.Normal);
      savedReports.BackgroundColor = UIColor.LightGray;
      savedReports.SetTitleColor(UIColor.White, UIControlState.Normal);
      savedReports.Layer.CornerRadius = 8;

      newReport = new UIButton(new CGRect(.25 * reportType.Bounds.Width,.35 * mainView.Bounds.Height,.5 * reportType.Bounds.Width, .12 * mainView.Bounds.Height));
      newReport.SetTitle("New Report", UIControlState.Normal);
      newReport.BackgroundColor = UIColor.LightGray;
      newReport.SetTitleColor(UIColor.White, UIControlState.Normal);
      newReport.Layer.CornerRadius = 8;

      step1 = new UILabel(new CGRect(0, .13 * reportType.Bounds.Height,reportType.Bounds.Width, .07 * mainView.Bounds.Height));
      step1.Text = "Step 1";
      step1.TextColor = UIColor.Black;
      step1.TextAlignment = UITextAlignment.Center;
      step1.AdjustsFontSizeToFitWidth = true;
      step1.Hidden = true;

      savedReports.TouchUpInside += (sender, e) => {savedReports.BackgroundColor = UIColor.LightGray;};
      savedReports.TouchDown += (sender, e) => {savedReports.BackgroundColor = UIColor.Blue;};
      savedReports.TouchUpOutside += (sender, e) => {savedReports.BackgroundColor = UIColor.LightGray;};

      newReport.TouchUpInside += (sender, e) => {newReport.BackgroundColor = UIColor.LightGray;};
      newReport.TouchDown += (sender, e) => {newReport.BackgroundColor = UIColor.Blue;};
      newReport.TouchUpOutside += (sender, e) => {newReport.BackgroundColor = UIColor.LightGray;};

      reportType.AddSubview(savedReports);
      reportType.AddSubview(newReport);
      reportType.AddSubview(step1);
      reportType.BringSubviewToFront(step1);
    }

  }
}

