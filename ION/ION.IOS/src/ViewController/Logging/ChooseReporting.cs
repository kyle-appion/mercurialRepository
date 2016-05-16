﻿using System;
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
      reportType = new UIView(new CGRect(.01 * mainView.Bounds.Width, 20, .98 * mainView.Bounds.Width, .15 * mainView.Bounds.Height));
      reportType.BackgroundColor = UIColor.Clear;
      reportType.Layer.BorderColor = UIColor.Black.CGColor;
      reportType.Layer.CornerRadius = 8;

      newReport = new UIButton(new CGRect(0,.3 * reportType.Bounds.Height,.5 * reportType.Bounds.Width, .5 * reportType.Bounds.Height));
      newReport.Enabled = false;
      newReport.SetTitle("New Report", UIControlState.Normal);
      newReport.BackgroundColor = UIColor.FromRGB(95,212,48);
      newReport.SetTitleColor(UIColor.Black, UIControlState.Normal);
      newReport.Layer.CornerRadius = 8;
      newReport.Layer.BorderWidth = 2f;

      savedReports = new UIButton(new CGRect(.5 * reportType.Bounds.Width,.3 * reportType.Bounds.Height,.5 * reportType.Bounds.Width, .5 * reportType.Bounds.Height));
      savedReports.SetTitle("Saved Reports", UIControlState.Normal);
      savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      savedReports.SetTitleColor(UIColor.Black, UIControlState.Normal);
      savedReports.Layer.CornerRadius = 8;
      savedReports.Layer.BorderWidth = 2f;

      savedReports.TouchUpInside += (sender, e) => {newReport.Enabled = true; savedReports.Enabled = false; savedReports.BackgroundColor = UIColor.FromRGB(95,212,48);};
      savedReports.TouchDown += (sender, e) => {newReport.Enabled = false; savedReports.BackgroundColor = UIColor.FromRGB(95,212,48);};
      savedReports.TouchUpOutside += (sender, e) => {
        if(!savedReports.Enabled){          
          savedReports.BackgroundColor = UIColor.FromRGB(95,212,48);
          newReport.Enabled = true; 
        } else {
          savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        }
      };

      newReport.TouchUpInside += (sender, e) => {savedReports.Enabled = true; newReport.Enabled = false; newReport.BackgroundColor = UIColor.FromRGB(95,212,48);};
      newReport.TouchDown += (sender, e) => {savedReports.Enabled = false; newReport.BackgroundColor = UIColor.FromRGB(95,212,48);};
      newReport.TouchUpOutside += (sender, e) => {

        if(!newReport.Enabled){
          newReport.BackgroundColor = UIColor.FromRGB(95,212,48);
          savedReports.Enabled = true;
        } else {
          newReport.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        }         
      };

      reportType.AddSubview(savedReports);
      reportType.AddSubview(newReport);
    }
  }
}

