using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Analyzer {
  public partial class holdTableCell : UITableViewCell {
    UILabel cellHeader;
    UILabel cellReading;
    UIButton cellButton;
  
    public holdTableCell(IntPtr handle) {
      
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      //UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
      cellHeader = new UILabel(new CGRect(0,0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      //UILabel cellReading = new UILabel(new CGRect(30,30,121,30));
      cellReading = new UILabel(new CGRect(.2 * tableRect.Width, .5 * lhSensor.cellHeight, .8 * tableRect.Width, .5 * lhSensor.cellHeight));
      //UIButton cellButton = new UIButton(new CGRect(0,30,30,30));
      cellButton = new UIButton(new CGRect(0, .5 * lhSensor.cellHeight, .2 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader.Text = "HOLD";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      if (lhSensor.holdType != "hold") {
        Console.WriteLine("hold label wasn't empty");
        cellReading.Text = lhSensor.holdValue.ToString("N") + " " + lhSensor.holdType + " ";
      } else {
        Console.WriteLine("hold label was empty");
        cellReading.Text = lhSensor.holdValue.ToString("N");
      }
      Console.WriteLine("The hold type is " +lhSensor.holdType +" currently");
      cellReading.TextAlignment = UITextAlignment.Right;
      cellReading.Font = UIFont.FromName("Helvetica", 14f);
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.Layer.BorderColor = UIColor.Black.CGColor;
      cellReading.Layer.BorderWidth = 1f;

      cellButton.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
      cellButton.Layer.BorderColor = UIColor.Black.CGColor;
      cellButton.Layer.BorderWidth = 1f;

      cellButton.TouchUpInside += delegate {
        if(lhSensor.isManual){
          lhSensor.holdValue = Convert.ToDouble(lhSensor.LabelMiddle.Text);
          lhSensor.holdType = lhSensor.LabelBottom.Text;
          cellReading.Text = lhSensor.LabelMiddle.Text + " " + lhSensor.LabelBottom.Text + " ";
        } else {
          lhSensor.holdValue = Convert.ToDouble(lhSensor.LabelMiddle.Text);
          lhSensor.holdType = lhSensor.LabelBottom.Text;
          cellReading.Text = lhSensor.holdValue.ToString("N") + " " + lhSensor.holdType + " ";
        }
      };

      this.AddSubview(cellHeader);
      this.AddSubview(cellReading);
      this.AddSubview(cellButton);
    }


  }
}