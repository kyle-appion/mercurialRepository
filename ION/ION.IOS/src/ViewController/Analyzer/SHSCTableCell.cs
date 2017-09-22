using System;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.Fluids;
using ION.Core.Sensors;
using ION.IOS.Util;
using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS.ViewController.Analyzer {
  public partial class SHSCTableCell : UITableViewCell {
    public UILabel cellHeader;
    private UILabel fluidType;
    private UILabel tempReading;
    public IosION ion;

    public SHSCTableCell(IntPtr handle) {
      ion = AppState.context as IosION;
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0,0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader = lhSensor.shFluidState;
      //lhSensor.shFluidState = cellHeader;

      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      fluidType = lhSensor.shFluidType;

      fluidType.Text = ion.fluidManager.lastUsedFluid.name;
      fluidType.BackgroundColor = CGExtensions.FromARGB8888(lhSensor.ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
      if(lhSensor.currentSensor.fluidState.Equals(Fluid.EState.Dew)){
        cellHeader.Text = Util.Strings.Analyzer.SC;
      } else if (lhSensor.currentSensor.fluidState.Equals(Fluid.EState.Bubble)){
        cellHeader.Text = Util.Strings.Analyzer.SH;
      }

      fluidType.TextAlignment = UITextAlignment.Center;
      fluidType.Font = UIFont.FromName("Helvetica", 18f);
      fluidType.AdjustsFontSizeToFitWidth = true;
      fluidType.Layer.BorderColor = UIColor.Black.CGColor;
      fluidType.Layer.BorderWidth = 1f;

      tempReading = lhSensor.shReading;
      if (lhSensor.currentSensor.linkedSensor != null) {
				var stateCheck = new ScalarSpan();
        if(lhSensor.currentSensor.type == ESensorType.Pressure){
          var calculation = ion.fluidManager.lastUsedFluid.CalculateTemperatureDelta(lhSensor.currentSensor.fluidState,lhSensor.currentSensor.measurement, lhSensor.currentSensor.linkedSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
	        stateCheck = calculation;  
					if (!ion.fluidManager.lastUsedFluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
          cellHeader.Text = lhSensor.shFluidState.Text;
					tempReading.Text = calculation.magnitude.ToString("N")+ " " + calculation.unit.ToString();
        } else {
          var calculation = ion.fluidManager.lastUsedFluid.CalculateTemperatureDelta(lhSensor.currentSensor.fluidState, lhSensor.currentSensor.linkedSensor.measurement, lhSensor.currentSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
          stateCheck = calculation;  
					if (!ion.fluidManager.lastUsedFluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
          cellHeader.Text = lhSensor.shFluidState.Text;
					tempReading.Text = calculation.magnitude.ToString("N")+ " " + calculation.unit.ToString();
        }
       	
        var ptAmount = stateCheck.magnitude;
        if (!ion.fluidManager.lastUsedFluid.mixture){
          if (ptAmount < 0) {
            lhSensor.shFluidState.Text = Util.Strings.Analyzer.SC;
          } else {
            lhSensor.shFluidState.Text = Util.Strings.Analyzer.SH;
          }
        } else if (lhSensor.currentSensor.fluidState.Equals(Fluid.EState.Bubble)) {
          lhSensor.shFluidState.Text = Util.Strings.Analyzer.SC;
        } else if (lhSensor.currentSensor.fluidState.Equals(Fluid.EState.Dew)) {
          lhSensor.shFluidState.Text = Util.Strings.Analyzer.SH;
        }  
      } else {
        tempReading.Text = Util.Strings.Analyzer.SETUP;
      }

      tempReading.AdjustsFontSizeToFitWidth = true;
      tempReading.TextAlignment = UITextAlignment.Center;

      this.AddSubview(cellHeader);
      this.AddSubview(lhSensor.shFluidType);
      this.AddSubview(tempReading);
      this.AddSubview(lhSensor.changeFluid);
    }
  }
}

