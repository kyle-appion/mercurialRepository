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
    public UILabel sessionInfo;
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

      sessionInfo = new UILabel(new CGRect(0,0,tableView.Bounds.Width, cellHeight));
      sessionInfo.AdjustsFontSizeToFitWidth = true;
      sessionInfo.TextAlignment = UITextAlignment.Left;
      sessionInfo.Layer.BorderColor = UIColor.Black.CGColor;
      sessionInfo.Layer.BorderWidth = 1f;
      sessionInfo.Text = start.ToShortDateString() + " " + formatTime + " " + duration.ToString("0.0") + " min";
      sessionInfo.Font = UIFont.SystemFontOfSize(20);

      this.AddSubview(sessionInfo);
    }
      
  }
}

