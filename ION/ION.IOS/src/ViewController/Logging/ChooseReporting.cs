using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class ChooseReporting : UIView {
    public UIView typeView;

    public UIButton savedReports;
    public UIButton newReport;

		public UILabel newHighlight;
		public UILabel savedHighlight;

    public ChooseReporting(UIView containerView) {
      typeView = new UIView(new CGRect(0,0,containerView.Bounds.Width,55));

      newReport = new UIButton(new CGRect(0,0,.5 * typeView.Bounds.Width, 40));
      newReport.Enabled = false;
      newReport.SetTitle(Util.Strings.Report.NEWREPORT, UIControlState.Normal);
      newReport.BackgroundColor = UIColor.White;
      newReport.SetTitleColor(UIColor.Black, UIControlState.Normal);

      newHighlight = new UILabel(new CGRect(0,40,.5 * typeView.Bounds.Width,5));
      newHighlight.BackgroundColor = UIColor.FromRGB(255, 215, 101);

      savedReports = new UIButton(new CGRect(.5 * typeView.Bounds.Width,0,.5 * typeView.Bounds.Width, 40));
      savedReports.SetTitle(Util.Strings.Report.SAVEDREPORT, UIControlState.Normal);
      savedReports.BackgroundColor = UIColor.LightGray;
      savedReports.SetTitleColor(UIColor.Black, UIControlState.Normal);

      savedHighlight = new UILabel(new CGRect(.5 * typeView.Bounds.Width, 40, .5 * typeView.Bounds.Width, 5));
			savedHighlight.BackgroundColor = UIColor.Black;

      savedReports.TouchUpInside += (sender, e) => {
        newReport.Enabled = true; 
        savedReports.Enabled = false; 
        savedReports.BackgroundColor = UIColor.White; 
        savedHighlight.BackgroundColor = UIColor.FromRGB(255, 215, 101); 
        newReport.BackgroundColor = UIColor.LightGray; 
        newHighlight.BackgroundColor = UIColor.Black; 
      };

      newReport.TouchUpInside += (sender, e) => {
        savedReports.Enabled = true; 
        newReport.Enabled = false; 
        newReport.BackgroundColor = UIColor.White;
		    newHighlight.BackgroundColor = UIColor.FromRGB(255, 215, 101);
		    savedReports.BackgroundColor = UIColor.LightGray;
		    savedHighlight.BackgroundColor = UIColor.Black;
	    };

      typeView.AddSubview(savedReports);
      typeView.AddSubview(savedHighlight);
      typeView.AddSubview(newReport);
			typeView.AddSubview(newHighlight);
		}
  }
}

