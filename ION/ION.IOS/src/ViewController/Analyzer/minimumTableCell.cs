﻿using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Analyzer {
  public partial class minimumTableCell : UITableViewCell {
    UILabel cellHeader;
    UILabel cellReading;
    UIButton cellButton;

    public minimumTableCell (IntPtr handle) {
    
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      //UILabel cellHeader = new UILabel(new CGRect(0,0,150, 30));
      cellHeader = new UILabel(new CGRect(0,0, 1.006 * lhSensor.snapArea.Bounds.Width, .5 * lhSensor.cellHeight));
      //UILabel cellReading = new UILabel(new CGRect(30,30,121,30));
      //cellReading = new UILabel(new CGRect(.201 * tableRect.Width, .5 * lhSensor.cellHeight , .812 * tableRect.Width, .5 * lhSensor.cellHeight ));
      //UIButton cellButton = new UIButton(new CGRect(0,30,30,30));
      cellButton = new UIButton(new CGRect(0, .5 * lhSensor.cellHeight, .201 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader.Text = "MIN";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      cellReading = lhSensor.minReading;
      if (lhSensor.minType.Equals("hold") && lhSensor.currentSensor != null) {
        lhSensor.min = lhSensor.currentSensor.measurement.amount;
        lhSensor.minType = lhSensor.LabelBottom.Text;
      }

      if (lhSensor.isManual) {
        var amount = Convert.ToDecimal(lhSensor.LabelMiddle.Text);
        cellReading.Text = amount.ToString("N") + " " + lhSensor.LabelBottom.Text + " ";
      } else {        
        cellReading.Text = lhSensor.min.ToString("N") + " " + lhSensor.minType + " ";
      }
      
      cellReading.TextAlignment = UITextAlignment.Right;
      cellReading.Font = UIFont.FromName("Helvetica", 18f);
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.Layer.BorderColor = UIColor.Black.CGColor;

      cellButton.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
      cellButton.Layer.BorderColor = UIColor.Black.CGColor;
      cellButton.Layer.BorderWidth = 1f;

      cellButton.TouchUpInside += delegate {
        lhSensor.min = Convert.ToDouble(lhSensor.LabelMiddle.Text);
        lhSensor.minType = lhSensor.LabelBottom.Text;         
        cellReading.Text =  lhSensor.min.ToString("N")+ " " + lhSensor.minType + " ";     
      };

      this.AddSubview(cellHeader);
      this.AddSubview(cellReading);
      this.AddSubview(cellButton);
    }


  }
}