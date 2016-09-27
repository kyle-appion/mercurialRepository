using System;
using UIKit;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class AssociatedSessionCell : UITableViewCell {
    public UILabel sessionInfo;

    public AssociatedSessionCell(IntPtr handle) {
      
    }

    public AssociatedSessionCell() {
      
    }

    public void SetupCell(ION.IOS.ViewController.Logging.SessionData data, double cellHeight, double cellWidth){

      var duration = data.finish.Subtract(data.start).TotalMinutes;

      var formatTime = "";

      if(data.start.TimeOfDay.Equals("PM")){
        data.start.AddHours(12);
        //formatTime = start.AddHours(12).Hour + ":" + start.Minute + ":" + start.Second;
        formatTime = data.start.AddHours(12).Hour + ":";

        if (data.start.Minute < 10)
          formatTime += "0" + data.start.Minute + ":";
        else
          formatTime += data.start.Minute + ":";

        formatTime += data.start.Second;
      }else{
        //formatTime = start.Hour + ":" + start.Minute + ":" + start.Second;
        formatTime = data.start.Hour + ":";

        if (data.start.Minute < 10)
          formatTime += "0" + data.start.Minute + ":";
        else
          formatTime += data.start.Minute + ":";

        formatTime += data.start.Second;
      }

      sessionInfo = new UILabel(new CGRect(0,0,cellWidth,cellHeight));
      sessionInfo.Layer.BorderWidth = .5f;
      sessionInfo.AdjustsFontSizeToFitWidth = true;
      sessionInfo.TextAlignment = UITextAlignment.Left;
      sessionInfo.Font = UIFont.SystemFontOfSize(20);
      sessionInfo.Text = data.start.ToShortDateString() + " " + formatTime + " " + duration.ToString("0.0") + " min";

      this.Layer.ShadowColor = UIColor.Black.CGColor;
      this.Layer.ShadowOpacity = .1f;
      this.Layer.ShadowRadius = .3f;
      this.Layer.ShadowOffset = new CGSize(0f, 1f);
      this.AddSubview(sessionInfo);
    }

  }
}

