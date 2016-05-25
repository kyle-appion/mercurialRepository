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
    public UILabel temperatureLabels;

    public SliderView(UIScrollView scrollView,PTChart ptChart, Unit pressureUnit, Unit temperatureUnit) {

      temperatureLabels = new UILabel(new CGRect(.5 * scrollView.Bounds.Width,.75 * scrollView.Bounds.Height,(2 * scrollView.Bounds.Width),.25 * scrollView.Bounds.Height));
      temperatureLabels.AdjustsFontSizeToFitWidth = true;
      temperatureLabels.TextColor = UIColor.Black;

      measurementView = new PTSlideView(ptChart, scrollView,temperatureLabels, pressureUnit, temperatureUnit);
      measurementView.BackgroundColor = UIColor.White;
      measurementView.Layer.CornerRadius = 5;
      measurementView.AddSubview(temperatureLabels);

      RedrawMeasurements(ptChart);
    }

    public void RedrawMeasurements(PTChart ptChart){
      measurementView.SetNeedsDisplay();
    }
  }
}

