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
		public UILabel includeLabel;
		public UIButton includeButton;
		public UIView buttonImage;

		public void setupTable(UITableView tableView,deviceReadings deviceData,List<deviceReadings> allData)
		{
			double highestMeasurement = -9999;
			double lowestMeasurement = 9999999;
			var combineName = deviceData.serialNumber + "/" + deviceData.sensorIndex;

			var cellHeight = .25 * tableView.Bounds.Height;

			header = new UILabel (new CGRect (0,0,.8 * tableView.Bounds.Width, .2 * cellHeight));
			header.BackgroundColor = UIColor.Black;
			header.TextColor = UIColor.White;
      header.TextAlignment = UITextAlignment.Left;
			header.AdjustsFontSizeToFitWidth = true;
      header.Text = deviceData.serialNumber + "    " + deviceData.type;

			if (deviceData.type.Equals("Temperature")) {
				header.BackgroundColor = UIColor.FromRGB(247, 148, 29);
				header.TextColor = UIColor.Black;
			} else if (deviceData.type.Equals("Vacuum")) {
				header.BackgroundColor = UIColor.FromRGB(123, 38, 34);
				header.TextColor = UIColor.White;
			} else {
				header.BackgroundColor = UIColor.FromRGB(46, 49, 146);
				header.TextColor = UIColor.White;
			}

			includeLabel = new UILabel(new CGRect(.8 * tableView.Bounds.Width, 0, .2 * tableView.Bounds.Width, .2 * cellHeight));
			includeLabel.TextAlignment = UITextAlignment.Center;
			includeLabel.Text = Util.Strings.INCLUDE;
			includeLabel.AdjustsFontSizeToFitWidth = true;
			includeLabel.Layer.BorderWidth = 1f;
			includeLabel.BackgroundColor = UIColor.Black;
			includeLabel.TextColor = UIColor.White;

			includeButton = new UIButton(new CGRect(.799 * tableView.Bounds.Width, 0, .2 * tableView.Bounds.Width, cellHeight));
			includeButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			includeButton.Layer.BorderWidth = 1f;

			buttonImage = new UIView(new CGRect(.8 * tableView.Bounds.Width, .25 * includeButton.Bounds.Height, includeButton.Bounds.Width, .75 * includeButton.Bounds.Height));
			buttonImage.BackgroundColor = UIColor.Yellow;
			buttonImage.UserInteractionEnabled = true;

			var image = new UIImageView(new CGRect(.25 * buttonImage.Bounds.Width, .25 * buttonImage.Bounds.Height, .5 * buttonImage.Bounds.Width, .5 * buttonImage.Bounds.Width));
			image.Layer.CornerRadius = 12;
			image.BackgroundColor = UIColor.White;
			if (ChosenDates.includeList.Contains(combineName)) {
				image.Image = UIImage.FromBundle("ic_checkbox");
			} else {
				image.Image = UIImage.FromBundle("ic_unchecked");
			}

			buttonImage.AddSubview(image);

			includeButton.TouchUpInside += (sender, e) => {
				if (ChosenDates.includeList.Contains(combineName)) {
					ChosenDates.includeList.Remove(combineName);
					image.Image = UIImage.FromBundle("ic_unchecked");
				} else {
					ChosenDates.includeList.Add(combineName);
					image.Image = UIImage.FromBundle("ic_checkbox");
				}
			};

			information = new UILabel (new CGRect (0,.2 * cellHeight,.8 * tableView.Bounds.Width, .8 * cellHeight));
			information.Layer.BorderWidth = 1f;
			information.Lines = 0;
			information.AdjustsFontSizeToFitWidth = true;

      var totalMeasurements = 0;
      double totalValue = 0;

      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
 
      if (deviceData.type.Equals("Temperature")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
        if (defaultUnit == null) {
          defaultUnit = "18";
          NSUserDefaults.StandardUserDefaults.SetInt(18, "settings_units_default_temperature");
        }
      } else if (deviceData.type.Equals("Vacuum")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
      }
      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));
      var standardUnit = lookup.standardUnit;
      
      foreach (var device in allData) {
        if (device.serialNumber.Equals(deviceData.serialNumber) && device.sensorIndex.Equals(deviceData.sensorIndex)) {
          for(int i = 0;i < device.readings.Count; i++){
      			var baseValue = standardUnit.OfScalar(device.readings[i]);
      			var coverted = baseValue.ConvertTo(lookup);
          	if(device.readings[i] < lowestMeasurement){
							lowestMeasurement = device.readings[i];				
						}
          	if(device.readings[i] > highestMeasurement){
							highestMeasurement = device.readings[i];
						}
						totalValue += device.readings[i];					
            totalMeasurements++;
          }
        }
      }

      var workingValue = standardUnit.OfScalar(highestMeasurement);

      var finalHighest = workingValue.ConvertTo(lookup);

      workingValue = standardUnit.OfScalar(lowestMeasurement);
      var finalLowest = workingValue.ConvertTo(lookup);

      workingValue = standardUnit.OfScalar((totalValue/totalMeasurements));
      var finalAverage = workingValue.ConvertTo(lookup);

      information.Text = Util.Strings.Measure.LOWESTMEASUREMENT+": " + finalLowest.amount.ToString("N") + " " + lookup.ToString() + "\n"+Util.Strings.Measure.HIGHESTMEASUREMENT + ": " + finalHighest.amount.ToString("N") + " " + lookup.ToString() + "\n" + Util.Strings.Measure.AVERAGEMEASUREMENT + ": " + finalAverage.amount.ToString("N") + " " + lookup.ToString();

			this.AddSubview (header);
			this.AddSubview (information);

			this.AddSubview(includeLabel);
			this.AddSubview(buttonImage);
			this.AddSubview(includeButton);
			this.BringSubviewToFront(includeButton);
		}
	}
}

