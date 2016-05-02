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

      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
 
      if (deviceData.type.Equals("Temperature")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
        Console.WriteLine("Changed to temperature default unit: " + defaultUnit);
        if (defaultUnit == null) {
          defaultUnit = "18";
          NSUserDefaults.StandardUserDefaults.SetInt(18, "settings_units_default_temperature");
        }
      } else if (deviceData.type.Equals("Vacuum")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
        Console.WriteLine("Changed to vacuum default unit: " + defaultUnit);
      }

      foreach (var device in allData) {        
        if (device.name.Equals(deviceData.name) && device.type.Equals(deviceData.type)) {
          foreach (var reading in device.readings) {
            if (reading < lowestMeasurement) {
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

      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));

      var standardUnit = lookup.standardUnit;

      var workingValue = standardUnit.OfScalar(highestMeasurement);

      var finalHighest = workingValue.ConvertTo(lookup);

      workingValue = standardUnit.OfScalar(lowestMeasurement);
      var finalLowest = workingValue.ConvertTo(lookup);

      workingValue = standardUnit.OfScalar((totalValue/totalMeasurements));
      var finalAverage = workingValue.ConvertTo(lookup);

      information.Text = "Lowest Measurement: " + finalLowest.amount.ToString("N") + " " + lookup.ToString() + "\nHighest Measurement: " + finalHighest.amount.ToString("N") + " " + lookup.ToString() + "\n" + "Average measurement: " + finalAverage.amount.ToString("N") + " " + lookup.ToString();

			this.AddSubview (header);
			this.AddSubview (information);
		}
	}
}

