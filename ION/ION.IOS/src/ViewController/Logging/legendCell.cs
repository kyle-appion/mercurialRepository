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
		public int measurementCount;
		public double highestMeasurement;
		public double lowestMeasurement;

		public void setupTable(UIView parentView,deviceReadings data)
		{
			measurementCount = 0;
			highestMeasurement = -9999;
			lowestMeasurement = 9999999;

			header = new UILabel (new CGRect (0,0,.98 * parentView.Bounds.Width,.055 * parentView.Bounds.Height));
			header.BackgroundColor = UIColor.Black;
			header.TextColor = UIColor.White;
			header.TextAlignment = UITextAlignment.Center;
			header.AdjustsFontSizeToFitWidth = true;
			header.Text = data.name;

			information = new UILabel (new CGRect (0,.055 * parentView.Bounds.Height,.98 * parentView.Bounds.Width,.111 * parentView.Bounds.Height));
			information.Layer.BorderWidth = 1f;
			information.Lines = 0;
			information.AdjustsFontSizeToFitWidth = true;

			foreach (var measurement in data.readings) {
				if (measurement > highestMeasurement) {
					highestMeasurement = measurement;
				}
				if (measurement < lowestMeasurement) {
					lowestMeasurement = measurement;
				}
				measurementCount++;
			}
			information.Text = "Lowest Measurement: " + lowestMeasurement + "\nHighest Measurement: " + highestMeasurement + "\n" + "Number of measurements: " + measurementCount;

			this.AddSubview (header);
			this.AddSubview (information);
		}
	}
}

