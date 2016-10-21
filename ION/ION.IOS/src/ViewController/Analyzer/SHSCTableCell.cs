using System;
using Foundation;
using UIKit;
using CoreGraphics;
using ION.Core.Content;
using ION.Core.Fluids;
using ION.Core.Sensors;
using ION.IOS.Util;
using ION.Core.App;
using ION.Core.Measure;


namespace ION.IOS.ViewController.Analyzer {
  public partial class SHSCTableCell : UITableViewCell {
    public UILabel cellHeader;
    private UILabel fluidType;
    private UILabel tempReading;

    public SHSCTableCell(IntPtr handle) {
      
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      if (lhSensor.manifold.ptChart.fluid != AppState.context.fluidManager.lastUsedFluid) {
        lhSensor.manifold.ptChart = PTChart.New(AppState.context, lhSensor.manifold.ptChart.state);
      }

      cellHeader = new UILabel(new CGRect(0,0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader = lhSensor.shFluidState;
      //lhSensor.shFluidState = cellHeader;

      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      fluidType = lhSensor.shFluidType;
      if (lhSensor.manifold != null && lhSensor.manifold.ptChart != null) {
        var name = lhSensor.manifold.ptChart.fluid.name;
        fluidType.Text = name;
        fluidType.BackgroundColor = CGExtensions.FromARGB8888(lhSensor.ion.fluidManager.GetFluidColor(name));
        if(lhSensor.manifold.ptChart.state.Equals(Fluid.EState.Dew)){
          cellHeader.Text = Util.Strings.Analyzer.SC;
        } else if (lhSensor.manifold.ptChart.state.Equals(Fluid.EState.Bubble)){
          cellHeader.Text = Util.Strings.Analyzer.SH;
        }
      } else {
        fluidType.Text = "----";
        fluidType.BackgroundColor = UIColor.White;
      }
      fluidType.TextAlignment = UITextAlignment.Center;
      fluidType.Font = UIFont.FromName("Helvetica", 18f);
      fluidType.AdjustsFontSizeToFitWidth = true;
      fluidType.Layer.BorderColor = UIColor.Black.CGColor;
      fluidType.Layer.BorderWidth = 1f;

      tempReading = lhSensor.shReading;
      if (lhSensor.manifold.secondarySensor != null) {
				var stateCheck = new ScalarSpan();
        if(lhSensor.manifold.primarySensor.type == ESensorType.Pressure){
          var calculation = lhSensor.manifold.ptChart.CalculateSystemTemperatureDelta(lhSensor.manifold.primarySensor.measurement, lhSensor.manifold.secondarySensor.measurement, lhSensor.manifold.primarySensor.isRelative);
	        stateCheck = calculation;  
					if (!lhSensor.manifold.ptChart.fluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
          cellHeader.Text = lhSensor.shFluidState.Text;
					tempReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        } else {
          var calculation = lhSensor.manifold.ptChart.CalculateSystemTemperatureDelta(lhSensor.manifold.secondarySensor.measurement, lhSensor.manifold.primarySensor.measurement, lhSensor.manifold.secondarySensor.isRelative);
          stateCheck = calculation;  
					if (!lhSensor.manifold.ptChart.fluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
          cellHeader.Text = lhSensor.shFluidState.Text;
					tempReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        }
       	
        var ptAmount = stateCheck.magnitude;
        if (!lhSensor.manifold.ptChart.fluid.mixture){
          if (ptAmount < 0) {
            lhSensor.shFluidState.Text = Util.Strings.Analyzer.SC;
          } else {
            lhSensor.shFluidState.Text = Util.Strings.Analyzer.SH;
          }
        } else if (lhSensor.manifold.ptChart.state.Equals(Fluid.EState.Bubble)) {
          lhSensor.shFluidState.Text = Util.Strings.Analyzer.SC;
        } else if (lhSensor.manifold.ptChart.state.Equals(Fluid.EState.Dew)) {
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

