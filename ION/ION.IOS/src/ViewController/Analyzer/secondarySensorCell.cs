using System;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer {
  
  public partial class secondarySensorCell : UITableViewCell  {
    //private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    UILabel cellHeader;
    //UILabel fluidType = new UILabel(new CGRect(0,30,100,30));
    UILabel secondaryReading;

    public secondarySensorCell(IntPtr handle) {
      
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0, 0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      //secondaryReading = new UILabel(new CGRect(0, .5 * lhSensor.cellHeight, tableRect.Width, .5 * lhSensor.cellHeight));
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

      if (lhSensor.currentSensor != null && lhSensor.manifold.secondarySensor != null) {
        if (lhSensor.currentSensor != lhSensor.manifold.primarySensor) {
          secondaryReading.Text = lhSensor.manifold.primarySensor.measurement.amount.ToString("N") + " " + lhSensor.manifold.primarySensor.unit;
        } else if (lhSensor.currentSensor == lhSensor.manifold.primarySensor) {
          secondaryReading.Text = lhSensor.manifold.secondarySensor.measurement.amount.ToString("N") + " " + lhSensor.manifold.secondarySensor.unit;
        } else {
          secondaryReading.Text = Util.Strings.Device.NOTLINKED;
        }
      } else if (lhSensor.manualSensor != null && lhSensor.manifold.secondarySensor != null) {
        if(lhSensor.manualSensor.type.Equals(ESensorType.Pressure)){
          secondaryReading.Text = lhSensor.manifold.secondarySensor.measurement.amount.ToString("N") + " " + lhSensor.manifold.secondarySensor.unit;
        } else {
          secondaryReading.Text = lhSensor.manifold.primarySensor.measurement.amount.ToString("N") + " " + lhSensor.manifold.primarySensor.unit;
        }
      } else {
        secondaryReading.Text = Util.Strings.Device.NOTLINKED;      
      }
      this.AddSubview(cellHeader);
      this.AddSubview(secondaryReading);
    }
  }
}

