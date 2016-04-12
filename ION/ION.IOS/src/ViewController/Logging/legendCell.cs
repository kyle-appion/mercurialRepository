using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging
{
	public class legendCell : UITableViewCell
	{

		public legendCell(IntPtr handle) {

		}

		public UILabel header;
		public UILabel information;

		public void setupTable(UIView parentView,deviceReadings deviceData,List<deviceReadings> allData)
		{
			double highestMeasurement = -9999;
			double lowestMeasurement = 9999999;

			header = new UILabel (new CGRect (0,0,.98 * parentView.Bounds.Width,.055 * parentView.Bounds.Height));
			header.BackgroundColor = UIColor.Black;
			header.TextColor = UIColor.White;
			header.TextAlignment = UITextAlignment.Center;
			header.AdjustsFontSizeToFitWidth = true;
      header.Text = deviceData.name;

			information = new UILabel (new CGRect (0,.055 * parentView.Bounds.Height,.98 * parentView.Bounds.Width,.111 * parentView.Bounds.Height));
			information.Layer.BorderWidth = 1f;
			information.Lines = 0;
			information.AdjustsFontSizeToFitWidth = true;

      var totalMeasurements = 0;
      double totalValue = 0;
      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_default_units_pressure");

      if (deviceData.type.Equals("Temperature")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_default_units_temperature");
      } else if (deviceData.type.Equals("Vacuum")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_default_units_vacuum");
      }

      foreach (var device in allData) {
        if (device.name.Equals(deviceData.name) && device.type.Equals(deviceData.type)) {
          foreach (var reading in device.readings) {
            if (reading < highestMeasurement) {
              lowestMeasurement = reading;
            }
            if (reading > highestMeasurement) {
              highestMeasurement = reading;
            } 
            totalValue += reading;
            totalMeasurements++;
          }
        }
      }
        
      information.Text = "Lowest Measurement: " + lowestMeasurement.ToString("N") + "\nHighest Measurement: " + highestMeasurement.ToString("N") + "\n" + "Average measurement: " + (totalValue/totalMeasurements).ToString("N");

			this.AddSubview (header);
			this.AddSubview (information);
		}
	}
}

