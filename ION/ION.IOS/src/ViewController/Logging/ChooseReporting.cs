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
      reportType = new UIView(new CGRect(.01 * mainView.Bounds.Width, .25 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .55 * mainView.Bounds.Height));
      reportType.BackgroundColor = UIColor.White;
      reportType.Layer.BorderColor = UIColor.Black.CGColor;
      reportType.Layer.BorderWidth = 1f;
      reportType.Layer.CornerRadius = 8;

      savedReports = new UIButton(new CGRect(.25 * reportType.Bounds.Width,.25 * reportType.Bounds.Height,.5 * reportType.Bounds.Width, .25 * reportType.Bounds.Height));
      savedReports.SetTitle("Saved Reports", UIControlState.Normal);
      savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      savedReports.SetTitleColor(UIColor.Black, UIControlState.Normal);
      savedReports.Layer.CornerRadius = 8;
      savedReports.Layer.BorderWidth = 2f;

      newReport = new UIButton(new CGRect(.25 * reportType.Bounds.Width,.55 * reportType.Bounds.Height,.5 * reportType.Bounds.Width, .25 * reportType.Bounds.Height));
      newReport.SetTitle("New Report", UIControlState.Normal);
      newReport.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      newReport.SetTitleColor(UIColor.Black, UIControlState.Normal);
      newReport.Layer.CornerRadius = 8;
      newReport.Layer.BorderWidth = 2f;

      step1 = new UILabel(new CGRect(0, .13 * reportType.Bounds.Height,reportType.Bounds.Width, .07 * mainView.Bounds.Height));
      step1.Text = "Step 1";
      step1.TextColor = UIColor.Black;
      step1.TextAlignment = UITextAlignment.Center;
      step1.AdjustsFontSizeToFitWidth = true;
      step1.Hidden = true;

      savedReports.TouchUpInside += (sender, e) => {newReport.Enabled = true; savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      savedReports.TouchDown += (sender, e) => {newReport.Enabled = false; savedReports.BackgroundColor = UIColor.Blue;};
      savedReports.TouchUpOutside += (sender, e) => {newReport.Enabled = true; savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

      newReport.TouchUpInside += (sender, e) => {savedReports.Enabled = true; newReport.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      newReport.TouchDown += (sender, e) => {savedReports.Enabled = false; newReport.BackgroundColor = UIColor.Blue;};
      newReport.TouchUpOutside += (sender, e) => {savedReports.Enabled = true; newReport.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

      reportType.AddSubview(savedReports);
      reportType.AddSubview(newReport);
      reportType.AddSubview(step1);
      reportType.BringSubviewToFront(step1);
    }

  }
}

