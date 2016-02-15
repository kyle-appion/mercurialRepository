using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;
using System.IO;

namespace ION.IOS.ViewController.Logging {
  
  public class AssociateJob{

    public UIView chooseJob;
    public UILabel jobHeader;
    public UITableView jobList;
    public UIButton closeButton;
    public UIButton okButton;

    public int frnSID;
    
    public AssociateJob(UIView mainView, nfloat cellHeight) {
      chooseJob = new UIView(new CGRect(.01 * mainView.Bounds.Width, .2 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, 4 * cellHeight));
      chooseJob.Layer.BorderColor = UIColor.Black.CGColor;
      chooseJob.Layer.BorderWidth = 1f;

      jobHeader = new UILabel(new CGRect(0,0,chooseJob.Bounds.Width, cellHeight));
      jobHeader.TextAlignment = UITextAlignment.Center;
      jobHeader.Text = "Choose a job below for this session";
      jobHeader.BackgroundColor = UIColor.FromRGB(9,221,255);
      jobHeader.AdjustsFontSizeToFitWidth = true;

      jobList = new UITableView(new CGRect(0,cellHeight, chooseJob.Bounds.Width, 2 * cellHeight));
      jobList.SeparatorStyle = UITableViewCellSeparatorStyle.None;

      closeButton = new UIButton(new CGRect(0, 3 * cellHeight, .5 * chooseJob.Bounds.Width, cellHeight));
      closeButton.BackgroundColor = UIColor.LightGray;
      closeButton.SetTitle(Util.Strings.CANCEL, UIControlState.Normal);
      closeButton.Layer.BorderColor = UIColor.Black.CGColor;
      closeButton.Layer.BorderWidth = 1f;

      okButton = new UIButton(new CGRect(.5 * chooseJob.Bounds.Width, 3 * cellHeight, .5 * chooseJob.Bounds.Width, cellHeight));
      okButton.BackgroundColor = UIColor.LightGray;
      okButton.SetTitle(Util.Strings.OK, UIControlState.Normal);
      okButton.Layer.BorderColor = UIColor.Black.CGColor;
      okButton.Layer.BorderWidth = 1f;

      ///user feedback for cancel button press
      closeButton.TouchUpInside += (sender, e) => {closeButton.BackgroundColor = UIColor.LightGray; chooseJob.Hidden = true;};
      closeButton.TouchDown += (sender, e) => {closeButton.BackgroundColor = UIColor.Blue;};
      closeButton.TouchUpOutside += (sender, e) => {closeButton.BackgroundColor = UIColor.LightGray;};

      ///user feedback for cancel button press
      okButton.TouchUpInside += touchUpOkButton;
      okButton.TouchDown += (sender, e) => {okButton.BackgroundColor = UIColor.Blue;};
      okButton.TouchUpOutside += (sender, e) => {okButton.BackgroundColor = UIColor.LightGray;};

      chooseJob.AddSubview(jobHeader);
      chooseJob.AddSubview(jobList);
      chooseJob.AddSubview(closeButton);
      chooseJob.AddSubview(okButton);
      chooseJob.Hidden = true;
    }

    public void touchUpOkButton (object sender, EventArgs e){
      okButton.BackgroundColor = UIColor.LightGray;
      chooseJob.Hidden = true;
      Console.WriteLine("Associating job to session: " + frnSID);
    }
  }
}

