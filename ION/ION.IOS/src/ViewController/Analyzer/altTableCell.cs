using System;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;
using ION.Core.App;

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
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.TextAlignment = UITextAlignment.Right;

      cellButton.BackgroundColor = UIColor.Clear;
      cellButton.Layer.BorderColor = UIColor.Black.CGColor;

      if (lhSensor.isManual) {

        if (lhSensor.manifold.primarySensor.type == ESensorType.Pressure) {
          manSensor = new ManualSensor(ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0));
          manSensor.unit = AnalyserUtilities.getManualUnit(ESensorType.Pressure, lhSensor.LabelBottom.Text.ToLower());
          manSensor.measurement = manSensor.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        } else if (lhSensor.manifold.primarySensor.type == ESensorType.Temperature) {
          manSensor = new ManualSensor(ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0));
          manSensor.unit = AnalyserUtilities.getManualUnit(ESensorType.Temperature, lhSensor.LabelBottom.Text.ToLower());
          manSensor.measurement = manSensor.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        } else if (lhSensor.manifold.primarySensor.type == ESensorType.Vacuum){
          manSensor = new ManualSensor(ESensorType.Vacuum, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Vacuum).OfScalar(0.0));
          manSensor.unit = AnalyserUtilities.getManualUnit(ESensorType.Vacuum, lhSensor.LabelBottom.Text.ToLower());
          manSensor.measurement = manSensor.unit.OfScalar(Convert.ToDouble(lhSensor.LabelMiddle.Text));
          lhSensor.alt = new AlternateUnitSensorProperty(manSensor);
        }
        if (lhSensor.altUnit != null) {
          lhSensor.alt.unit = lhSensor.altUnit;
        } else {
          lhSensor.alt.unit = manSensor.unit;
        }
        cellReading.Text = SensorUtils.ToFormattedString(lhSensor.alt.sensor.type, lhSensor.alt.modifiedMeasurement, true);
      } else {       

        lhSensor.alt = new AlternateUnitSensorProperty(lhSensor.currentSensor as Sensor);
        if (lhSensor.altUnit != null) {
          lhSensor.alt.unit = lhSensor.altUnit;
        } else {
          if (lhSensor.currentSensor.type == ESensorType.Pressure) {
            lhSensor.alt.unit = AnalyserUtilities.getManualUnit(lhSensor.currentSensor.type, lhSensor.altUnits[0].Replace("/", "").ToLower());
          } else if (lhSensor.currentSensor.type == ESensorType.Temperature) {
            lhSensor.alt.unit = AnalyserUtilities.getManualUnit(lhSensor.currentSensor.type, lhSensor.tempUnits[0].Replace("/", "").ToLower());
          } else if (lhSensor.currentSensor.type == ESensorType.Vacuum) {
            lhSensor.alt.unit = AnalyserUtilities.getManualUnit(lhSensor.currentSensor.type, lhSensor.vacUnits[0].Replace("/", "").ToLower());
          }
        }
      }
      cellButton.TouchUpInside += delegate {
        var window = UIApplication.SharedApplication.KeyWindow;
        var vc = window.RootViewController;
        while (vc.PresentedViewController != null) {
          vc = vc.PresentedViewController;
        }

        UIAlertController altUnit = UIAlertController.Create (Util.Strings.Analyzer.CHOOSEUNIT, "", UIAlertControllerStyle.Alert);

        if(lhSensor.alt.sensor.type== ESensorType.Pressure){
          foreach(String unit in lhSensor.altUnits) {
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
        } else if (lhSensor.alt.sensor.type == ESensorType.Temperature) {
          foreach(String unit in lhSensor.tempUnits) {
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
        } else if (lhSensor.alt.sensor.type == ESensorType.Vacuum) {
          foreach(String unit in lhSensor.vacUnits){
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
        }

        altUnit.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController (altUnit, true, null);
      };

      this.AddSubview(cellHeader);
      this.AddSubview(cellButton);
      this.AddSubview(cellReading);
    }
  }
}

