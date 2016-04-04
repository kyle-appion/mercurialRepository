using System;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager  {
  
  public class CreateJobView {
    public UIView cjView;
    public UILabel nameHeader;
    public UITextField jobName;
    public UIButton createJobButton;
    public UIButton showPdf;

    public CreateJobView(UIView parentView) {
      cjView = new UIView(new CGRect(.01 * parentView.Bounds.Width,50, .98 * parentView.Bounds.Width, .8 * parentView.Bounds.Height));
      cjView.BackgroundColor = UIColor.White;

      nameHeader = new UILabel(new CGRect(.1 * cjView.Bounds.Width, 0,.8 * cjView.Bounds.Width,.1 * cjView.Bounds.Height));
      nameHeader.Text = "Enter a name for the job";
      nameHeader.AdjustsFontSizeToFitWidth = true;
      nameHeader.TextAlignment = UITextAlignment.Center;

      jobName = new UITextField(new CGRect(.1 * cjView.Bounds.Width, .1 * cjView.Bounds.Height, .8 * cjView.Bounds.Width, .1 * cjView.Bounds.Height));
      jobName.Layer.BorderWidth = 1f;
      jobName.UserInteractionEnabled = true;
      jobName.ShouldReturn += (jobName) => {
        jobName.ResignFirstResponder();
        return true;
      };

      createJobButton = new UIButton(new CGRect(.25 * cjView.Bounds.Width, .88 * cjView.Bounds.Height, .5 * cjView.Bounds.Width, .1 * cjView.Bounds.Height));
      createJobButton.Layer.CornerRadius = 8;
      createJobButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      createJobButton.Layer.BorderWidth = 2f;
      createJobButton.SetTitle("Create Job", UIControlState.Normal);
      createJobButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

      createJobButton.TouchUpInside += (sender, e) => {
        Console.WriteLine("Creating Job");
        UIApplication.SharedApplication.IdleTimerDisabled = false;

      };

      showPdf = new UIButton(new CGRect(.25 * cjView.Bounds.Width, .6 * cjView.Bounds.Height, .5 * cjView.Bounds.Width, .1 * cjView.Bounds.Height));
      showPdf.Layer.CornerRadius = 8;
      showPdf.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      showPdf.Layer.BorderWidth = 2f;
      showPdf.SetTitle("Show pdf", UIControlState.Normal);
      showPdf.SetTitleColor(UIColor.Black, UIControlState.Normal);

      showPdf.TouchUpInside += (sender, e) => {

      };

      cjView.AddSubview(nameHeader);
      cjView.AddSubview(jobName);
      cjView.AddSubview(createJobButton);
    }

  }
}

