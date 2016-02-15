using System;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public partial class SessionCell : UITableViewCell {
    
    public int SID;
    public DateTime start;
    public DateTime finish;
    public UILabel sessionInfo;

    public SessionCell(IntPtr handle ) {
      
    }

    public SessionCell(){
    
    }

    public void makeCellData(int session, DateTime begin, DateTime end, UITableView tableView, nfloat cellHeight){
      SID = session;
      start = begin;
      finish = end;

      sessionInfo = new UILabel(new CGRect(0,0,tableView.Bounds.Width, cellHeight));
      sessionInfo.AdjustsFontSizeToFitWidth = true;
      sessionInfo.TextAlignment = UITextAlignment.Left;
      sessionInfo.Layer.BorderColor = UIColor.Black.CGColor;
      sessionInfo.Layer.BorderWidth = 1f;
      sessionInfo.Text = start.ToLongDateString() + " " + start.ToLongTimeString() + " - " + finish.ToLongTimeString();

      this.AddSubview(sessionInfo);
    }
      
  }
}

