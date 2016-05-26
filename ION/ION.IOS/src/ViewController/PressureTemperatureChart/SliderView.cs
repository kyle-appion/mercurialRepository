using System;
using System.Threading.Tasks;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Foundation; 

using ION.Core.Fluids;
using ION.Core.Measure;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class SliderView {
    public PTSlideView measurementView;
    public UILabel pUnitLabel;
    public UILabel tUnitLabel;

    public SliderView(UIScrollView scrollView,PTChart ptChart, Unit pressureUnit, Unit temperatureUnit) {

      measurementView = new PTSlideView(ptChart, scrollView, pressureUnit, temperatureUnit);
      measurementView.BackgroundColor = UIColor.White;
      measurementView.Layer.CornerRadius = 5;

      pUnitLabel = new UILabel(new CGRect(324.8,0,40,.5 * measurementView.Bounds.Height));
      pUnitLabel.AdjustsFontSizeToFitWidth = true;

      tUnitLabel = new UILabel(new CGRect(324.8,.5 * measurementView.Bounds.Height,40,.5 * measurementView.Bounds.Height));
      tUnitLabel.AdjustsFontSizeToFitWidth = true;

      measurementView.AddSubview(pUnitLabel);
      measurementView.AddSubview(tUnitLabel);

      RedrawMeasurements(ptChart);
    }

    public void RedrawMeasurements(PTChart ptChart){
      measurementView.SetNeedsDisplay();
    }
  }
}

