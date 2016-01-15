using System;
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
    //private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
   private UILabel cellHeader;
    //private UILabel cellReading = new UILabel(new CGRect(0,30,149,30));
   private UILabel cellReading;
    //private UIButton cellButton = new UIButton(new CGRect(0,30,149,30));
   private UIButton cellButton;
    private Sensor manSensor;

    public altTableCell(IntPtr handle) {
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0, 0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      //cellReading = new UILabel(new CGRect(0, .5 * lhSensor.cellHeight, tableRect.Width, .5 * lhSensor.cellHeight));
      cellButton = new UIButton(new CGRect(0, .5 * lhSensor.cellHeight, tableRect.Width, .5 * lhSensor.cellHeight));


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
        Console.WriteLine("the manual gt: " + lhSensor.manualGType);
        if (lhSensor.manualGType == "Pressure") {
          manSensor = new Sensor(ESensorType.Pressure);
          manSensor.measurement = manSensor.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        } else if (lhSensor.manualGType == "Temperature") {
          manSensor = new Sensor(ESensorType.Temperature, lhSensor.alt.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text)));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        } else if (lhSensor.manualGType == "Vacuum"){
          manSensor = new Sensor(ESensorType.Vacuum, lhSensor.alt.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text)));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        }
        lhSensor.alt.unit = UnitLookup.GetUnit(manSensor.type, lhSensor.altUnits[0].Replace("/", "").ToLower());
        cellReading.Text = SensorUtils.ToFormattedString(lhSensor.alt.sensor.type, lhSensor.alt.modifiedMeasurement, true);
      } else {
        lhSensor.alt = new AlternateUnitSensorProperty(lhSensor.currentSensor as Sensor);
        if (lhSensor.altUnit != null) {
          lhSensor.alt.unit = lhSensor.altUnit;
        } else {
          lhSensor.alt.unit = UnitLookup.GetUnit(lhSensor.currentSensor.type, lhSensor.altUnits[0].Replace("/", "").ToLower());
        }
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
            if(lhSensor.isManual){
              lhSensor.altUnit = UnitLookup.GetUnit(lhSensor.alt.sensor.type ,unit.Replace("/","").ToLower());
              lhSensor.alt.unit = UnitLookup.GetUnit(lhSensor.alt.sensor.type ,unit.Replace("/","").ToLower());
              lhSensor.altReading.Text = SensorUtils.ToFormattedString(lhSensor.alt.sensor.type, lhSensor.alt.modifiedMeasurement, true);
            } else {
              lhSensor.altUnit = UnitLookup.GetUnit(lhSensor.currentSensor.type ,unit.Replace("/","").ToLower());
              lhSensor.alt.unit = UnitLookup.GetUnit(lhSensor.currentSensor.type ,unit.Replace("/","").ToLower());
              lhSensor.altReading.Text = SensorUtils.ToFormattedString(lhSensor.alt.sensor.type, lhSensor.alt.modifiedMeasurement, true);
            }
          }));
        }

        altUnit.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController (altUnit, true, null);
      };

      this.AddSubview(cellHeader);
      this.AddSubview(cellButton);
      this.AddSubview(cellReading);
    }
  }
}

