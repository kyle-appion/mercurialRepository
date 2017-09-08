using System;
using System.Linq;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Logging
{
  public class LegendView : UIView
  {
    public UIView lView;

		public UILabel header;
		public UILabel actionHeader;
    public UILabel blueLabel;
    public UILabel redLabel;
    public UILabel burgundyLabel;

    public UIImageView bluePlot;
    public UIImageView redPlot;
    public UIImageView burgundyPlot;

    public UIButton pressureUnits;
    public UIButton temperatureUnits;
		public UIButton vacuumUnits;
		public UIButton closeButton;

    public List<deviceReadings> selectedData;

    public UIViewController parentVC;

    public LegendView (UIView mainView,List<deviceReadings> pressuresTemperatures, UIViewController mainVC)
    {
      selectedData = pressuresTemperatures;
      parentVC = mainVC;

      lView = new UIView (new CGRect (.05 * mainView.Bounds.Width,.1 * mainView.Bounds.Height,.9 * mainView.Bounds.Width,.4 * mainView.Bounds.Height));
      lView.BackgroundColor = UIColor.White;
      lView.Layer.BorderColor = UIColor.Black.CGColor;
      lView.Layer.BorderWidth = 1f;
      lView.Hidden = true;

      createLegend ();
    }

    public void createLegend(){
    	var defaultPressure = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
    	var defaultVacuum = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
    	var defaultTemperature = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");

      header = new UILabel (new CGRect (0,0,lView.Bounds.Width,.15 * lView.Bounds.Height));
      header.Layer.CornerRadius = 8;
      header.TextAlignment = UITextAlignment.Left;
      header.Text = "  Report Settings";
      header.Font = UIFont.BoldSystemFontOfSize(20);
      header.BackgroundColor = UIColor.FromRGB(0, 174, 239);

      closeButton = new UIButton(new CGRect(.9 * header.Bounds.Width,.05 * header.Bounds.Height, .9 * header.Bounds.Height, .9 * header.Bounds.Height));
      closeButton.SetImage(UIImage.FromBundle("img_button_blackclosex"), UIControlState.Normal);
      closeButton.BackgroundColor = UIColor.FromRGB(0, 174, 239);
      closeButton.TouchUpInside += (sender, e) => {
        Console.WriteLine("Closing legend view");
        lView.Hidden = true;
      };

      actionHeader = new UILabel(new CGRect(0,.15 * lView.Bounds.Height,lView.Bounds.Width,.15 * lView.Bounds.Height));
      actionHeader.TextAlignment = UITextAlignment.Center;
      actionHeader.Text = "Measurement Units";

			blueLabel = new UILabel(new CGRect(.05 * lView.Bounds.Width, .3 * lView.Bounds.Height, .35 * lView.Bounds.Width, .2 * lView.Bounds.Height));
			blueLabel.AdjustsFontSizeToFitWidth = true;
			blueLabel.Text = Util.Strings.Sensor.Type.PRESSURE;
			blueLabel.TextAlignment = UITextAlignment.Left;

      bluePlot = new UIImageView(new CGRect(.4 * lView.Bounds.Width,.3 * lView.Bounds.Height,.29 * lView.Bounds.Width, .2 * lView.Bounds.Height));
      bluePlot.Image = UIImage.FromBundle("img_blue_plot");
      
      pressureUnits = new UIButton(new CGRect(.7 * lView.Bounds.Width, .3 * lView.Bounds.Height,.25 * lView.Bounds.Width, .2 * lView.Bounds.Height));
      pressureUnits.SetTitle(ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultPressure)).ToString(),UIControlState.Normal);
      pressureUnits.SetTitleColor(UIColor.Black,UIControlState.Normal);
			pressureUnits.Layer.BorderWidth = 1f;
			pressureUnits.Layer.CornerRadius = 5f;
      pressureUnits.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			redLabel = new UILabel(new CGRect(.05 * lView.Bounds.Width, .5 * lView.Bounds.Height, .35 * lView.Bounds.Width, .2 * lView.Bounds.Height));
			redLabel.AdjustsFontSizeToFitWidth = true;
			redLabel.Text = Util.Strings.Sensor.Type.TEMPERATURE;
			redLabel.TextAlignment = UITextAlignment.Left;

			redPlot = new UIImageView(new CGRect(.4 * lView.Bounds.Width, .5 * lView.Bounds.Height, .29 * lView.Bounds.Width, .2 * lView.Bounds.Height));
			redPlot.Image = UIImage.FromBundle("img_red_plot");

			temperatureUnits = new UIButton(new CGRect(.7 * lView.Bounds.Width, .5 * lView.Bounds.Height, .25 * lView.Bounds.Width, .2 * lView.Bounds.Height));
			temperatureUnits.SetTitle(ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultTemperature)).ToString(), UIControlState.Normal);
			temperatureUnits.SetTitleColor(UIColor.Black, UIControlState.Normal);
			temperatureUnits.Layer.BorderWidth = 1f;
			temperatureUnits.Layer.CornerRadius = 5f;
			temperatureUnits.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			burgundyLabel = new UILabel(new CGRect(.05 * lView.Bounds.Width, .7 * lView.Bounds.Height, .35 * lView.Bounds.Width, .2 * lView.Bounds.Height));
			burgundyLabel.AdjustsFontSizeToFitWidth = true;
			burgundyLabel.Text = Util.Strings.Sensor.Type.VACUUM;
			burgundyLabel.TextAlignment = UITextAlignment.Left;

      burgundyPlot = new UIImageView(new CGRect(.4 * lView.Bounds.Width,.7 * lView.Bounds.Height,.29 * lView.Bounds.Width, .2 * lView.Bounds.Height));
      burgundyPlot.Image = UIImage.FromBundle("img_burgundy_plot");

      vacuumUnits = new UIButton(new CGRect(.7 * lView.Bounds.Width, .7 * lView.Bounds.Height,.25 * lView.Bounds.Width, .2 * lView.Bounds.Height));
      vacuumUnits.SetTitle(ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultVacuum)).ToString(),UIControlState.Normal);
      vacuumUnits.SetTitleColor(UIColor.Black,UIControlState.Normal);
      vacuumUnits.Layer.BorderWidth = 1f;
			vacuumUnits.Layer.CornerRadius = 5f;
			vacuumUnits.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			lView.AddSubview(header);
			lView.AddSubview(closeButton);
      lView.BringSubviewToFront(closeButton);
			lView.AddSubview (actionHeader);
      lView.AddSubview (bluePlot);
      lView.AddSubview (redPlot);
      lView.AddSubview (burgundyPlot);
      lView.AddSubview (blueLabel);
      lView.AddSubview (redLabel);
      lView.AddSubview (burgundyLabel);
      lView.AddSubview (pressureUnits);
      lView.AddSubview (temperatureUnits);
      lView.AddSubview (vacuumUnits);
    }
  }
}

