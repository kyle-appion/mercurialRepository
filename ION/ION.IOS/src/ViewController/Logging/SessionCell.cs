using System;
using System.Linq;
using System.Windows;
using UIKit;
using CoreGraphics;

using ION.Core.App;
using ION.Core.Database;

using SQLite;


namespace ION.IOS.ViewController.Logging {
  public partial class SessionCell : UITableViewCell {
    
    public int SID;
    public DateTime start;
    public DateTime finish;
    public UILabel sessionDate;
    public UILabel sessionTime;
    public UILabel sessionLength;
    public IION ion;

    public SessionCell(IntPtr handle ) {
      
    }

    public SessionCell(){
      
    }

    public void makeCellData(int session, DateTime begin, DateTime end, UITableView tableView, nfloat cellHeight){
      SID = session;
      start = begin.ToLocalTime();
      finish = end.ToLocalTime();
      ion = AppState.context;

      var deviceAmount = ion.database.Table<SensorMeasurementRow>()
        .Where(smr => smr.frn_SID == SID)
        .Select(smr => smr.serialNumber).Distinct()
        .Count();
		
      var duration = finish.Subtract(start).TotalMinutes;

      var formatTime = "";

      if(start.TimeOfDay.Equals("PM")){
        start.AddHours(12);
        //formatTime = start.AddHours(12).Hour + ":" + start.Minute + ":" + start.Second;
        formatTime = start.AddHours(12).Hour + ":";

        if (start.Minute < 10)
          formatTime += "0" + start.Minute + ":";
        else
          formatTime += start.Minute + ":";
        
        formatTime += start.Second;
      }else{
        //formatTime = start.Hour + ":" + start.Minute + ":" + start.Second;
        formatTime = start.Hour + ":";

        if (start.Minute < 10)
          formatTime += "0" + start.Minute + ":";
        else
          formatTime += start.Minute + ":";
        
        formatTime += start.Second;
      }

      sessionDate = new UILabel(new CGRect(0,0,tableView.Bounds.Width, cellHeight));
      sessionDate.AdjustsFontSizeToFitWidth = true;
      sessionDate.TextAlignment = UITextAlignment.Left;
      sessionDate.Layer.BorderColor = UIColor.Black.CGColor;
      sessionDate.Layer.BorderWidth = 1f;
      sessionDate.Text = Util.Strings.Job.STARTDATE + " : " + start.ToShortDateString()+" "+formatTime+"\n " + Util.Strings.Job.DURATION + ":    " + duration.ToString("0.0") + " min\n " + Util.Strings.DEVICES + ":     " + deviceAmount;
      sessionDate.Font = UIFont.SystemFontOfSize(20);
      sessionDate.Lines = 0;

      //sessionDate = new UILabel(new CGRect(0,0,.3 * tableView.Bounds.Width, cellHeight));
      //sessionDate.AdjustsFontSizeToFitWidth = true;
      //sessionDate.TextAlignment = UITextAlignment.Center;
      //sessionDate.Layer.BorderColor = UIColor.Black.CGColor;
      //sessionDate.Layer.BorderWidth = 1f;
      //sessionDate.Text = start.ToShortDateString();
      //sessionDate.Font = UIFont.SystemFontOfSize(20);

      //sessionTime = new UILabel(new CGRect(.3 * tableView.Bounds.Width,0,.3 * tableView.Bounds.Width, cellHeight));
      //sessionTime.AdjustsFontSizeToFitWidth = true;
      //sessionTime.TextAlignment = UITextAlignment.Center;
      //sessionTime.Layer.BorderColor = UIColor.Black.CGColor;
      //sessionTime.Layer.BorderWidth = 1f;
      //sessionTime.Text = formatTime;
      //sessionTime.Font = UIFont.SystemFontOfSize(20);
      
      //sessionLength = new UILabel(new CGRect(.6 * tableView.Bounds.Width,0,.3 * tableView.Bounds.Width, cellHeight));
      //sessionLength.AdjustsFontSizeToFitWidth = true;
      //sessionLength.TextAlignment = UITextAlignment.Center;
      //sessionLength.Text = duration.ToString("0.0") + " min";
      //sessionLength.Font = UIFont.SystemFontOfSize(20);      

      this.AddSubview(sessionDate);
      //this.AddSubview(sessionTime);
      //this.AddSubview(sessionLength);
      this.Layer.BorderWidth = 1f;
    }
      
  }
}

