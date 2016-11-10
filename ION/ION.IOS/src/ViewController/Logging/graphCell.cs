using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

namespace ION.IOS.ViewController.Logging
{
	public partial class graphCell : UITableViewCell
	{
		public PlotView plotView;
		public deviceReadings cellData;
    public List<deviceReadings> allData;
		public LinearAxis LAX;
		public LinearAxis RAX;
		//public LinearAxis BAX;
		public CategoryAxis BAX;
		public UILabel deviceName;
		public UILabel includeLabel;
		public UIButton includeButton;
		public UITableView graphTable;
    public UIView buttonImage;

		public graphCell(IntPtr handle) {
		
		}

    public void setupGraph(deviceReadings startData, List<deviceReadings> totalData, double cellWidth, double cellHeight, double trackerHeight, UIView parentView, UITableView tableView)
		{
      cellData = startData;
 
      var combineName = cellData.serialNumber + "/" + cellData.sensorIndex;
      //Console.WriteLine("Looking at device " + combineName);
      allData = totalData;

			graphTable = tableView;
      this.BackgroundColor = UIColor.Clear;
			this.Layer.BorderWidth = 1f;
			 
			plotView = new PlotView(new CGRect(0,0, .8 * cellWidth, cellHeight)){
				Model = CreatePlotModel(trackerHeight, parentView),
        BackgroundColor = UIColor.Clear,
			};
			
			deviceName = new UILabel (new CGRect (0,.92 * cellHeight,.3 * cellWidth,.25 * cellHeight));
      deviceName.Text = cellData.serialNumber;
			deviceName.AdjustsFontSizeToFitWidth = true;
			deviceName.TextAlignment = UITextAlignment.Center;

			includeButton = new UIButton (new CGRect (.799 * cellWidth,0,.2 * cellWidth, 1.17 * cellHeight));
			includeButton.SetTitleColor (UIColor.White, UIControlState.Normal);
      includeButton.Layer.BorderWidth = 1f;

      includeLabel = new UILabel (new CGRect (.8 * cellWidth,0,includeButton.Bounds.Width,.25 * includeButton.Bounds.Height));
      includeLabel.TextAlignment = UITextAlignment.Center;
      includeLabel.Text = Util.Strings.INCLUDED;
      includeLabel.AdjustsFontSizeToFitWidth = true;
      includeLabel.Layer.BorderWidth = 1f;
      includeLabel.BackgroundColor = UIColor.Black;
      includeLabel.TextColor = UIColor.White;

      buttonImage = new UIView(new CGRect(.8 * cellWidth,.25 * includeButton.Bounds.Height, includeButton.Bounds.Width,.75 * includeButton.Bounds.Height));
      buttonImage.BackgroundColor = UIColor.Yellow;
      buttonImage.UserInteractionEnabled = true;
      var image = new UIImageView(new CGRect(.25 * buttonImage.Bounds.Width, .25 * buttonImage.Bounds.Height,.5 * buttonImage.Bounds.Width, .5 * buttonImage.Bounds.Width));
      image.Layer.CornerRadius = 12;
      image.BackgroundColor = UIColor.White;
      if (ChosenDates.includeList.Contains(combineName)) {
        image.Image = UIImage.FromBundle("ic_checkbox");
      } else {
        image.Image = UIImage.FromBundle("ic_unchecked");
      }
      buttonImage.AddSubview(image);

			includeButton.TouchUpInside += (sender, e) => {        
        if (ChosenDates.includeList.Contains (combineName)) {
          ChosenDates.includeList.Remove (combineName);
          image.Image = UIImage.FromBundle ("ic_unchecked");
				} else {
          ChosenDates.includeList.Add (combineName);
          image.Image = UIImage.FromBundle ("ic_checkbox");
				}
			};

			this.AddSubview (plotView);
			this.AddSubview (deviceName);			
			this.AddSubview (includeLabel);
      this.AddSubview (buttonImage);
			this.AddSubview (includeButton);
      this.BringSubviewToFront(includeButton);
      this.BringSubviewToFront (deviceName);
		}

		/// <summary>
		/// Setup the axes and setup the plot points for each
		/// series(recording session) for the device/sensor
		/// </summary>
		/// <returns>The plot model.</returns>	
		public PlotModel CreatePlotModel(double trackerHeight, UIView parentView) {
			var lowValue = 9999999.9;
			var highValue = -9999.9;
			bool lowestMarked = false;
			bool highestMarked = false;

      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
      if (cellData.type.Equals("Temperature")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
      } else if (cellData.type.Equals("Vacuum")) {
        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
      }
      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));
      
      var standardUnit = lookup.standardUnit;
      foreach (var device in allData) {
        if (device.serialNumber.Equals(cellData.serialNumber) && device.type.Equals(cellData.type)) {
          foreach (var reading in device.readings) {
            if (reading < lowValue) {
              lowValue = reading;
            }
            if (reading > highValue) {
              highValue = reading;
            } 
          }
        }
      }
			var baseLow = standardUnit.OfScalar(lowValue);
			lowValue = baseLow.ConvertTo(lookup).amount; 
			var baseHigh = standardUnit.OfScalar(highValue);
			highValue = baseHigh.ConvertTo(lookup).amount;
			
      var color = OxyColors.Blue;
      var buffer = 5;
      if(cellData.type.Equals("Temperature")){
        color = OxyColors.Red;
        buffer = 3;
      } else if (cellData.type.Equals("Vacuum")){
        color = OxyColors.Maroon;
        buffer = 2000;
      }
			var plotModel = new PlotModel();

			plotModel.Background = OxyColors.Transparent;
			plotModel.PlotAreaBackground = OxyColors.White;
			plotModel.DefaultFontSize = 0;
			plotModel.PlotAreaBorderThickness = new OxyThickness(0,0,0,0);
			plotModel.PlotMargins = new OxyThickness(-5,-5,-5,-5);

			/// The bottom axis of the graph will be index based. Each measurement is one "tick"
      /// Corresponsding date indexes will provide the plot points
      //BAX = new LinearAxis {
      //  Position = AxisPosition.Bottom,
      //  Maximum = ChosenDates.allTimes[ChosenDates.latest.ToString()],
      //  AbsoluteMaximum = ChosenDates.allTimes[ChosenDates.latest.ToString()],
      //  Minimum = 0,
      //  AbsoluteMinimum = -1,
      //  IntervalLength = 1,
      //  IsAxisVisible = false,
      //  IsZoomEnabled = false,
      //  IsPanEnabled = false,
      //  MinimumPadding = 0,
      //  MaximumPadding = 0,
      //  AxislineThickness = 0,
      //};
      
      BAX = new CategoryAxis {
        Position = AxisPosition.Bottom,
        Maximum = ChosenDates.allTimes[ChosenDates.latest.ToString()],
        AbsoluteMaximum = ChosenDates.allTimes[ChosenDates.latest.ToString()],
        Minimum = 0,
        AbsoluteMinimum = -1,
        IntervalLength = 1,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 0,
        MaximumPadding = 0,
        AxislineThickness = 0,
        Angle = 90,
        Title = "Time",
      };

			foreach(var time in ChosenDates.allIndexes){
				var formatTime = DateTime.Parse(time.Value).ToString("H:mm:ss");
				BAX.ActualLabels.Add(formatTime);
			}
			/// left axis of the graph that adds a pad on the top and bottom to the lowest and highest value 
			/// this will be used for pressure measurements
			LAX = new LinearAxis {
				Position = AxisPosition.Left,
        Maximum = highValue + buffer,
				AbsoluteMaximum = highValue + buffer,
        Minimum = lowValue - buffer,
        AbsoluteMinimum = lowValue - buffer,
				Key = "left",
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 0,
				MaximumPadding = 0,
				AxislineThickness = 0,
				Title = cellData.type+"("+lookup+")",
			};

			plotModel.Axes.Add (BAX);
			plotModel.Axes.Add (LAX);

      
      foreach(var device in allData){
        if (device.serialNumber.Equals(cellData.serialNumber) && device.type.Equals(cellData.type)) {
        	var markSize = 0.0;
          var series = new LineSeries {
            MarkerType = MarkerType.Circle,
            MarkerSize = markSize,
            MarkerStroke = color,
            MarkerFill = color,
            LineStyle = LineStyle.Solid,
            Color = color,
          };

          for(int i = 0; i < device.times.Count; i++) {
      			var baseValue = standardUnit.OfScalar(device.readings[i]);
      			var measurement = baseValue.ConvertTo(lookup).amount;
      			
            var index = ChosenDates.allTimes[device.times[i].ToString()];

            series.Points.Add(new DataPoint(index,measurement));
            series.MarkerSize = 0;
          }
          plotModel.Series.Add (series);
        }
      }

			plotModel.IsLegendVisible = false;

			return plotModel;
		}
	}
}

