using System;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer {
  
  public partial class secondarySensorCell : UITableViewCell  {
    UILabel cellHeader;
    UILabel secondaryReading;

    public secondarySensorCell(IntPtr handle) {
      
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0, 0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      secondaryReading = new UILabel(new CGRect(0, .5 * lhSensor.cellHeight, tableRect.Width, .5 * lhSensor.cellHeight));
			
			if(lhSensor.currentSensor != null && lhSensor.currentSensor.type == ESensorType.Temperature){
				cellHeader.Text = Util.Strings.LINKED + " PRESS";
			} else if(lhSensor.currentSensor != null){
				cellHeader.Text = Util.Strings.LINKED + " TEMP";
			} else if (lhSensor.manualSensor != null && lhSensor.manualSensor.type == ESensorType.Pressure){
				cellHeader.Text = Util.Strings.LINKED + " TEMP";
			} else {
				cellHeader.Text = Util.Strings.LINKED + " PRESS";
			}
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      secondaryReading = lhSensor.secondaryReading;
      secondaryReading.AdjustsFontSizeToFitWidth = true;
      secondaryReading.TextAlignment = UITextAlignment.Center;

      if (lhSensor.currentSensor != null && lhSensor.currentSensor.linkedSensor != null) {
        secondaryReading.Text = lhSensor.currentSensor.linkedSensor.measurement.amount.ToString("N") + " " + lhSensor.currentSensor.linkedSensor.unit;
      } else if (lhSensor.manualSensor != null && lhSensor.manualSensor.linkedSensor != null) {
        secondaryReading.Text = lhSensor.manualSensor.linkedSensor.measurement.amount.ToString("N") + " " + lhSensor.manualSensor.linkedSensor.unit;
      } else {
        secondaryReading.Text = Util.Strings.Device.NOTLINKED;      
      }
      this.AddSubview(cellHeader);
      this.AddSubview(secondaryReading);
    }
  }
}

