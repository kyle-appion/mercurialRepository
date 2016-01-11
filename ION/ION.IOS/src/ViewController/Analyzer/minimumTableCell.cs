using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Analyzer {
  public partial class minimumTableCell : UITableViewCell {
    UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    UILabel cellReading = new UILabel(new CGRect(30,30,119,30));
    UIButton cellButton = new UIButton(new CGRect(0,30,30,30));

    public minimumTableCell (IntPtr handle) {
    }

    public void makeEvents(lowHighSensor lhSensor){
      cellHeader.Text = "MIN";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      cellReading = lhSensor.minReading;
      cellReading.Text = 0.00 + " " + lhSensor.currentSensor.unit.ToString();
      cellReading.TextAlignment = UITextAlignment.Right;
      cellReading.Font = UIFont.FromName("Helvetica", 14f);
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.Layer.BorderColor = UIColor.Black.CGColor;
      cellReading.Layer.BorderWidth = 1f;

      cellButton.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
      cellButton.Layer.BorderColor = UIColor.Black.CGColor;
      cellButton.Layer.BorderWidth = 1f;


      cellButton.TouchUpInside += delegate {
        cellReading.Text = 0.00 + " " + lhSensor.currentSensor.unit.ToString();
        lhSensor.min = 0.00;
        lhSensor.minType = lhSensor.currentSensor.unit.ToString();
      };


      this.AddSubview(cellHeader);
      this.AddSubview(cellReading);
      this.AddSubview(cellButton);
    }


  }
}