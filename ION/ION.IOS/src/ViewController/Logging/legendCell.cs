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
      Console.WriteLine("Measurement set is of type: " + deviceData.type);
      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");

      if (deviceData.type.Equals("Temperature")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
      } else if (deviceData.type.Equals("Vacuum")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
      }

      Console.WriteLine("Using the standard unit of : " + defaultUnit);


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
      Console.WriteLine("lowest value: " + lowestMeasurement);
      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));
      Console.WriteLine("looked up unit: " + lookup.ToString());
      var standardUnit = lookup.standardUnit;
      Console.WriteLine("standardUnit is : " + standardUnit.ToString());
      var workingValue = standardUnit.OfScalar(highestMeasurement);
      Console.WriteLine("working value is: " + workingValue.amount);
      var finalHighest = workingValue.ConvertTo(lookup);
      Console.WriteLine("final highest is: " + finalHighest.amount);
      workingValue = standardUnit.OfScalar(lowestMeasurement);
      var finalLowest = workingValue.ConvertTo(lookup);
      Console.WriteLine("final lowest is: " + finalLowest.amount);
      workingValue = standardUnit.OfScalar((totalValue/totalMeasurements));
      var finalAverage = workingValue.ConvertTo(lookup);
      Console.WriteLine("final average is: " + finalAverage.amount);


      information.Text = "Lowest Measurement: " + finalLowest.amount.ToString("N") + " " + lookup.ToString() + "\nHighest Measurement: " + finalHighest.amount.ToString("N") + " " + lookup.ToString() + "\n" + "Average measurement: " + finalAverage.amount.ToString("N") + " " + lookup.ToString();

			this.AddSubview (header);
			this.AddSubview (information);
		}
	}
}

