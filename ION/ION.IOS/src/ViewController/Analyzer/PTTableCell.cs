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
  public partial class PTTableCell : UITableViewCell {
    //private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    private UILabel cellHeader;
    //private UILabel fluidType = new UILabel(new CGRect(0,30,100,30));
    private UILabel fluidType;
    //private UILabel tempReading = new UILabel(new CGRect(100,30,49,30));
    private UILabel tempReading;


    public PTTableCell(IntPtr handle) {
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0,0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      fluidType = new UILabel(new CGRect(0, .5 * lhSensor.cellHeight, .5 * tableRect.Width, .5 * lhSensor.cellHeight));
      //tempReading = new UILabel(new CGRect(.5 * tableRect.Width, .5 * lhSensor.cellHeight, .5 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader.Text = "PTdew";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      fluidType = lhSensor.ptFluidType;
      if (lhSensor.manifold != null && lhSensor.manifold.ptChart != null) {
        var name = lhSensor.manifold.ptChart.fluid.name;
        fluidType.Text = name;
        fluidType.BackgroundColor = CGExtensions.FromARGB8888(lhSensor.ion.fluidManager.GetFluidColor(name));
      } else {
        fluidType.Text = "----";
        fluidType.BackgroundColor = UIColor.White;
      }
      fluidType.TextAlignment = UITextAlignment.Center;
      fluidType.Font = UIFont.FromName("Helvetica", 18f);
      fluidType.AdjustsFontSizeToFitWidth = true;
      fluidType.Layer.BorderColor = UIColor.Black.CGColor;
      fluidType.Layer.BorderWidth = 1f;

      tempReading = lhSensor.ptReading;
      if (lhSensor.manifold.secondarySensor != null) {
        if(lhSensor.manifold.primarySensor.type == ESensorType.Pressure){

        } else {

        }
      } else {
        tempReading.Text = "Setup";
      }
      tempReading.AdjustsFontSizeToFitWidth = true;
      tempReading.TextAlignment = UITextAlignment.Center;
      tempReading.Layer.BorderColor = UIColor.Black.CGColor;
      tempReading.Layer.BorderWidth = 1f;

      this.AddSubview(cellHeader);
      this.AddSubview(lhSensor.ptFluidType);
      this.AddSubview(lhSensor.ptReading);
      this.AddSubview(lhSensor.changePTFluid);
    }
  }
}

