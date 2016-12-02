using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;
using System.Collections;
using ION.Core.Measure;
using ION.Core.Sensors;

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
      var buffer = 0.0;
      
      if(cellData.type.Equals("Temperature")){
        color = OxyColors.Red;
        buffer = getUnitBuffer(ESensorType.Temperature,lookup);
      } else if (cellData.type.Equals("Vacuum")){
        color = OxyColors.Maroon;
        buffer = getUnitBuffer(ESensorType.Vacuum,lookup);
      } else {
				buffer = getUnitBuffer(ESensorType.Pressure,lookup);
			}
			
			var plotModel = new PlotModel();

			plotModel.Background = OxyColors.Transparent;
			plotModel.PlotAreaBackground = OxyColors.White;
			plotModel.DefaultFontSize = 0;
			plotModel.PlotAreaBorderThickness = new OxyThickness(0,0,0,0);
			plotModel.PlotMargins = new OxyThickness(-5,-5,-5,-5);

			/// The bottom axis of the graph will be index based. Each measurement is one "tick"
      /// Corresponsding date indexes will provide the plot points

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
			 
			string[] timeArray = new string[ChosenDates.allTimes[ChosenDates.latest.ToString()] + 1];
			
			foreach(var time in ChosenDates.allIndexes){
				var formatTime = DateTime.Parse(time.Value).ToString("H:mm:ss");
				timeArray[time.Key] = formatTime;
				//Console.WriteLine("Set time: " + formatTime + " at index " + time.Key);
			}
			
			///FILL IN EMPTY SPACES FOR SESSION PADDED INDEXES
			for(int a = 0; a < timeArray.Length; a++){
				if(timeArray[a] == null){
					//Console.WriteLine("time array had a null spot at index " + a + " setting it to previous time of " + timeArray[a-1]);
					timeArray[a] = timeArray[a-1];
					//Console.WriteLine("Null Time is now: " + timeArray[a]);					
				}
			}
			foreach(var entry in timeArray){
				BAX.ActualLabels.Add(entry);
				
			}
			///YOU WOULD OBVIOUSLY WANT TICK MARKS TO BE CENTERED AS DEFAULT, SO WHY IS THE DEFAULT FALSE OXYPLOT!?!?!
			BAX.IsTickCentered = true;
			/// left axis of the graph that adds a pad on the top and bottom to the lowest and highest value 
			/// this will be used for pressure measurements
			var measurementRange = Math.Ceiling(highValue + buffer) - Math.Floor(lowValue - buffer);
      var majorStep = Math.Ceiling(measurementRange / 5);
			//Console.WriteLine("Looking at high value " + Math.Ceiling(highValue + buffer) + " and low value " + Math.Floor(lowValue - buffer) + " to get a range of " + measurementRange + " that gives a major step of " + majorStep);
       
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
				MajorStep = majorStep,
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
          }
          plotModel.Series.Add (series);
        }
      }

			plotModel.IsLegendVisible = false;

			return plotModel;
		}
		/// <summary>
		/// RETURNS THE BUFFER VALUE FOR THE GRAPH BASED ON THE UNIT A USER HAS CHOSEN
		/// </summary>
		/// <returns>THE GRAPH TOP AND BOTTOM BUFFER</returns>
		/// <param name="type">TYPE OF SENSOR</param>
		/// <param name="gaugeUnit">SPECIFIC UNIT CHOSEN BY USER FOR THE SENSOR TYPE</param>
		public double getUnitBuffer(ESensorType type, Unit gaugeUnit){
			switch(type){
				case ESensorType.Pressure:
					double psig = 10;
					var pressureBuffer = new ScalarSpan(Units.Pressure.PSIG,psig);
					var pressureConvert = pressureBuffer.ConvertTo(gaugeUnit);
					return pressureConvert.magnitude;
				case ESensorType.Temperature:
					double fahrenheit = 10;
					var temperatureBuffer = new ScalarSpan(Units.Temperature.FAHRENHEIT,fahrenheit);
					var temperatureConvert = temperatureBuffer.ConvertTo(gaugeUnit);
					return temperatureConvert.magnitude;
				case ESensorType.Vacuum:
					double micron = 2000;
					var vacuumBuffer = new ScalarSpan(Units.Vacuum.MICRON,micron);
					var vacuumConvert = vacuumBuffer.ConvertTo(gaugeUnit);
					return vacuumConvert.magnitude;
				default:
					var defaultBuffer = new ScalarSpan(Units.Pressure.PSIG,10);
					var defaultConvert = defaultBuffer.ConvertTo(gaugeUnit);
					return defaultConvert.magnitude;				
			}
		}
	}
}

