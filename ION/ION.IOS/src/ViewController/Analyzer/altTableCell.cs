﻿using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;


namespace ION.IOS.ViewController.Analyzer {
  
  public partial class altTableCell : UITableViewCell  {
   private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
   private UILabel cellReading = new UILabel(new CGRect(0,30,149,30));
   private UIButton cellButton = new UIButton(new CGRect(0,30,149,30));

    public altTableCell(IntPtr handle) {
    }

    public void makeEvents(lowHighSensor lhSensor){
      cellHeader.Text = "ALT";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      cellReading = lhSensor.altReading;
      cellReading.TextAlignment = UITextAlignment.Right;

      cellButton.BackgroundColor = UIColor.Clear;
      cellButton.Layer.BorderColor = UIColor.Black.CGColor;
      cellButton.Layer.BorderWidth = 1f;

      if (lhSensor.isManual) {
        lhSensor.alt = new AlternateUnitSensorProperty();
        if (lhSensor.manualGType == "Pressure") {
          Console.WriteLine("Pressure Manual");
          lhSensor.alt.unit = Units.Pressure.PSIG;
        } else if (lhSensor.manualGType == "Temperature") {
          Console.WriteLine("Temp Manual");
          lhSensor.alt.unit = Units.Temperature.FAHRENHEIT;
        } else {
          Console.WriteLine("Vacuum Manual");
          lhSensor.alt.unit = Units.Vacuum.PASCAL;
        }
      } else {
        lhSensor.alt = new AlternateUnitSensorProperty(lhSensor.currentSensor as Sensor);
        lhSensor.alt.unit = UnitLookup.GetUnit(lhSensor.currentSensor.type, lhSensor.altUnits[0].Replace("/", "").ToLower());
      }
      cellButton.TouchUpInside += delegate {
        var window = UIApplication.SharedApplication.KeyWindow;
        var vc = window.RootViewController;
        while (vc.PresentedViewController != null) {
          vc = vc.PresentedViewController;
        }

        UIAlertController altUnit = UIAlertController.Create ("Choose Unit", "", UIAlertControllerStyle.Alert);

        foreach(String unit in lhSensor.altUnits){
          altUnit.AddAction (UIAlertAction.Create(unit ,UIAlertActionStyle.Default, (action) => {
            lhSensor.alt.unit = UnitLookup.GetUnit(lhSensor.currentSensor.type ,unit.Replace("/","").ToLower());
            lhSensor.altReading.Text = SensorUtils.ToFormattedString(lhSensor.alt.sensor.type, lhSensor.alt.modifiedMeasurement, true);
          }));
        }

        altUnit.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {
          
        }));

        altUnit.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController (altUnit, true, null);
      };

      this.AddSubview(cellHeader);
      this.AddSubview(cellButton);
      this.AddSubview(cellReading);
    }
  }
}
