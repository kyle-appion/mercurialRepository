using System;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.Fluids;
using ION.IOS.Util;
using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS.ViewController.Analyzer {
  public partial class PTTableCell : UITableViewCell {
    private UILabel cellHeader;
    private UILabel fluidType;
    private UILabel tempReading;
    public IosION ion;


    public PTTableCell(IntPtr handle) {
    	ion = AppState.context as IosION;
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      if (!(ion is RemoteIosION) && lhSensor.manifold.ptChart.fluid != AppState.context.fluidManager.lastUsedFluid) {
        lhSensor.manifold.ptChart = PTChart.New(AppState.context, lhSensor.manifold.ptChart.state);
      }

      cellHeader = new UILabel(new CGRect(0,0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      fluidType = new UILabel(new CGRect(0, .5 * lhSensor.cellHeight, .5 * tableRect.Width, .5 * lhSensor.cellHeight));


      cellHeader = lhSensor.ptFluidState;
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

