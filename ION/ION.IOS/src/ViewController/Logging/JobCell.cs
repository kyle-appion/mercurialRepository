using System;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public partial class JobCell : UITableViewCell{

    public int JID;
    public string Name;
    public UILabel jobInfo;

    public JobCell(IntPtr handle ) {

    }

    public JobCell(){

    }

    public void makeCellData(int job, string jName, UITableView tableView, nfloat cellHeight){
      JID = job;
      Name = jName;
      Console.WriteLine("Width of the job table: " + tableView.Bounds);
      jobInfo = new UILabel(new CGRect(0,0,tableView.Bounds.Width, cellHeight));
      jobInfo.AdjustsFontSizeToFitWidth = true;
      jobInfo.TextAlignment = UITextAlignment.Center;
      jobInfo.Layer.BorderColor = UIColor.Black.CGColor;
      jobInfo.Layer.BorderWidth = 1f;
      jobInfo.Text = Name;

      this.AddSubview(jobInfo);
    }

  }
}

