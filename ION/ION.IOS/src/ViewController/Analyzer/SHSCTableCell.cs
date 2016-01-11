using System;
using Foundation;
using UIKit;
using CoreGraphics;
using ION.Core.Content;
using ION.Core.Fluids;
using ION.IOS.Util;
using ION.Core.App;
using ION.Core.Measure;


namespace ION.IOS.ViewController.Analyzer {
  public partial class SHSCTableCell : UITableViewCell {
    private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    private UILabel fluidType = new UILabel(new CGRect(0,30,100,30));
    private UILabel tempReading = new UILabel(new CGRect(100,30,49,30));

    public SHSCTableCell(IntPtr handle) {
      
    }

    public void makeEvents(lowHighSensor Sensor){

      cellHeader.Text = "S/H";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      fluidType = Sensor.shFluidType;
      fluidType.Text = "----";
      fluidType.TextAlignment = UITextAlignment.Center;
      fluidType.BackgroundColor = UIColor.White;
      fluidType.Font = UIFont.FromName("Helvetica", 18f);
      fluidType.AdjustsFontSizeToFitWidth = true;
      fluidType.Layer.BorderColor = UIColor.Black.CGColor;
      fluidType.Layer.BorderWidth = 1f;

      tempReading = Sensor.shReading;
      tempReading.Text = "Setup";
      tempReading.AdjustsFontSizeToFitWidth = true;
      tempReading.TextAlignment = UITextAlignment.Center;

      this.AddSubview(cellHeader);
      this.AddSubview(fluidType);
      this.AddSubview(tempReading);
      this.AddSubview(Sensor.changeFluid);
    }

  }
}

