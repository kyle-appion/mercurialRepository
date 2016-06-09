using System;
using System.Threading.Tasks;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Foundation; 

using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class SliderView {
    public TSlideView temperatureView;
    public PSlideView pressureView;
    public UILabel tUnitLabel;
    public UILabel pUnitLabel;
    public UIScrollView temperatureScroller;
    public UIScrollView pressureScroller;

    public SliderView(UIView View,PTChart ptChart, Unit pressureUnit, Unit temperatureUnit, Sensor temperatureSensor) {
      pressureScroller = new UIScrollView(new CGRect(.025 * View.Bounds.Width,.5 * View.Bounds.Height, .95 * View.Bounds.Width, 64));
      pressureScroller.ShowsHorizontalScrollIndicator = false;
      pressureScroller.ContentSize = new CGSize(4377.6,pressureScroller.Bounds.Height);
      pressureScroller.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
      pressureScroller.Layer.CornerRadius = 5;
      pressureScroller.Layer.BorderWidth = 1f;
      pressureScroller.BackgroundColor = UIColor.White;

      temperatureScroller = new UIScrollView(new CGRect(.025 * View.Bounds.Width,.5 * View.Bounds.Height + 64, .95 * View.Bounds.Width, 64));
      temperatureScroller.ShowsHorizontalScrollIndicator = false;
      temperatureScroller.ContentSize = new CGSize(2188.8,temperatureScroller.Bounds.Height);
      temperatureScroller.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
      temperatureScroller.Layer.CornerRadius = 5;
      temperatureScroller.Layer.BorderWidth = 1f;
      temperatureScroller.BackgroundColor = UIColor.White;

      pressureView = new PSlideView(ptChart, pressureScroller, pressureUnit, temperatureUnit, temperatureSensor);
      pressureView.BackgroundColor = UIColor.White;
      pressureView.Layer.CornerRadius = 5;

      temperatureView = new TSlideView(ptChart, temperatureScroller, pressureUnit, temperatureUnit, temperatureSensor);
      temperatureView.BackgroundColor = UIColor.White;
      temperatureView.Layer.CornerRadius = 5;

      pUnitLabel = new UILabel(new CGRect(pressureView.startGap - 40,0,40,pressureView.Bounds.Height));
      pUnitLabel.AdjustsFontSizeToFitWidth = true;
      pUnitLabel.Text = pressureUnit.ToString();

      tUnitLabel = new UILabel(new CGRect(temperatureView.startGap - 40,0,40,temperatureView.Bounds.Height));
      tUnitLabel.AdjustsFontSizeToFitWidth = true;
      tUnitLabel.Text = temperatureUnit.ToString();

      pressureView.AddSubview(pUnitLabel);
      temperatureView.AddSubview(tUnitLabel);

      temperatureScroller.AddSubview(temperatureView);
      pressureScroller.AddSubview(pressureView);

      RedrawMeasurements(ptChart);
    }

    public void RedrawMeasurements(PTChart ptChart){
      temperatureView.SetNeedsDisplay();
    }
  }
}

