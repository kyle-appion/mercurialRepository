using System;
using CoreGraphics;
using UIKit;

using OxyPlot;
using OxyPlot.Xamarin.iOS;

using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors.Properties;
using System.Threading.Tasks;
using OxyPlot.Axes;
using OxyPlot.Series;
using Appion.Commons.Measure;
using ION.Core.Sensors;
using System.Collections.Generic;
using Foundation;

namespace ION.IOS.ViewController.Workbench {
  
  public partial class RateofChangeSettingsViewController : BaseIONViewController {
    public RateOfChangeRecord initialRecord {
      get {
        return __initialRecord;
      }
      set {
        if (__initialRecord != null) {
          __initialRecord.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
        }

        __initialRecord = value;

        if (__initialRecord != null) {
          __initialRecord.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
        }
      }
    } RateOfChangeRecord __initialRecord;
    
    public PlotView plotView;
    public LinearAxis BAX;
    public LinearAxis LAX;
    public LinearAxis RAX;

		public OxyColor primaryColor;
		public OxyColor secondaryColor;

    public LineSeries primarySeries;
    public LineSeries secondarySeries;
    public GaugeDeviceSensor gaugeSensor;
        
    public bool actualSecondary = true;
    private bool isUpdating { get; set; } = false;
  
    public RateofChangeSettingsViewController(IntPtr handle) : base (handle) {
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      
      if(plotView == null){
        Console.WriteLine("Creating the plotview");
        setupSettings();
      }
    }
    
    public async void setupSettings(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      
      graphView.Layer.BorderWidth = 1f;
      legendView.Layer.BorderWidth = 1f;
      
      plotView = new PlotView(new CGRect(0,0, graphView.Bounds.Width, graphView.Bounds.Height)){
        Model = CreatePlotModel(),
        BackgroundColor = UIColor.Clear,
      };

      trendGraphHeader.Layer.BorderWidth = 1f;
      ////FOUR CORNER MIN AND MAX MEASUREMENT LABELS
      TLMeasurement.AdjustsFontSizeToFitWidth = true;
      BLMeasurement.AdjustsFontSizeToFitWidth = true;
      TRMeasurement.AdjustsFontSizeToFitWidth = true;
      BRMeasurement.AdjustsFontSizeToFitWidth = true;
      
      ////SHOWS THE UPDATE INTERVAL A USER HAS CREATED
      var tinfoText = "";
      if(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 100){
        tinfoText += "10x /second";
      } else if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 1000){
        tinfoText += "1 second";
      } else if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 10000){
        tinfoText += "10 seconds";
      } else if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 30000){
        tinfoText += "30 seconds";
      }
      trendInfoHeader.Text = "Update Interval: ";
      trendInfoHeader.Font = UIFont.BoldSystemFontOfSize(20);
      trendInfoInterval.Text = tinfoText;
      
      trendIntervalSettings.Text = "This interval can be changed in the App Settings";
      trendIntervalSettings.Font = UIFont.ItalicSystemFontOfSize(14);
      trendIntervalSettings.AdjustsFontSizeToFitWidth = true;
      
      /////SAYS WHAT KIND OF SENSOR AND SERIAL NUMBER IS BEING GRAPHED
      primaryLegendLabel.Text = initialRecord.manifold.primarySensor.type + " " + initialRecord.manifold.primarySensor.name;
      primaryLegendLabel.AdjustsFontSizeToFitWidth = true;
      linkedLegendLabel.AdjustsFontSizeToFitWidth = true;
      
      switch(initialRecord.manifold.primarySensor.type){
        case ESensorType.Pressure:
          primaryLegendImage.Image = UIImage.FromBundle("img_blue_plot");
          break;
          
        case ESensorType.Temperature:
          primaryLegendImage.Image = UIImage.FromBundle("img_red_plot");          
          break;
          
        case ESensorType.Vacuum:
          primaryLegendImage.Image = UIImage.FromBundle("img_burgundy_plot");          
          break;        
      }
      /////SETUP LINKED SENSOR LEGEND INFORMATION
      if(initialRecord.manifold.secondarySensor != null && !(initialRecord.manifold.secondarySensor is ManualSensor)){
        linkedLegendLabel.Text = initialRecord.manifold.secondarySensor.type + " " + initialRecord.manifold.secondarySensor.name;
        switch(initialRecord.manifold.secondarySensor.type){
          case ESensorType.Pressure:
            linkedLegendImage.Image = UIImage.FromBundle("img_blue_plot");
            break;
            
          case ESensorType.Temperature:
            linkedLegendImage.Image = UIImage.FromBundle("img_red_plot");          
            break;
            
          case ESensorType.Vacuum:
            linkedLegendImage.Image = UIImage.FromBundle("img_burgundy_plot");          
            break;        
        }          
        
        
        linkedLegendLabel.Hidden = false;
        linkedLegendImage.Hidden = false; 
      } else {
        linkedLegendLabel.Hidden = true;
        linkedLegendImage.Hidden = true;
      }
      
      graphView.AddSubview(plotView);
      updateCellGraph();
    }
 
    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      var device = (initialRecord.manifold.primarySensor as GaugeDeviceSensor)?.device;
      
      if(device != null && device.isConnected && !isUpdating){
				Console.WriteLine("Sensor change and graph was not updating so starting roc settings update started");
				updateCellGraph();
      }
    }
 
    public async void updateCellGraph(){
      if(plotView == null){
        return;
      }           
      var device = (initialRecord.manifold.primarySensor as GaugeDeviceSensor)?.device;
      isUpdating = device.isConnected;

      if (device == null || device.isConnected) {
          InvalidatePrimary();
          if (initialRecord.manifold.secondarySensor != null && !(initialRecord.manifold.secondarySensor is ManualSensor)) {
            InvalidateSecondary();
          }
          InvalidateTime();
  
        InvokeOnMainThread ( () => {    
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
      } else {
        plotView.Model.PlotMargins = new OxyThickness(0,double.NaN,0,0);
        return;
      }
			await Task.Delay(TimeSpan.FromMilliseconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")));
			if (isUpdating) {
        updateCellGraph();
      }
    }

		private void InvalidateTime() {
			var axis = BAX;
			var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			if (roc == null) {
				return;
			}

			var points = roc.primarySensorPoints;

			if (points.Count <= 0) {
				Console.WriteLine("Failed to invalidate time: points.count was " + points.Count);
				return;
			}
			var startTime = points[0];
			var endTime = points[points.Count - 1].date;

			axis.Minimum = (startTime.date - roc.window).ToFileTime() - 1000000;
			axis.Maximum = startTime.date.ToFileTime() + 1000000;

			axis.MajorStep = (long)((roc.window.TotalMilliseconds * 1e4) / 2);

			axis.MinorStep = axis.MajorStep / 5;
			axis.AxislineStyle = LineStyle.Solid;
			axis.AxislineThickness = 1;
		}

    /// <summary>
    /// Updates the left axis
    /// </summary>
		private void InvalidatePrimary() {
			var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			if (roc == null || TLMeasurement == null || BLMeasurement == null) {
				return;
			}

			var minMax = roc.GetPrimaryMinMax();
			var rangeAmount = roc.manifold.primarySensor.maxMeasurement.ConvertTo(roc.manifold.primarySensor.unit.standardUnit).amount * .05;
			var sensorUnit = roc.manifold.primarySensor.unit;
			var sensorMin = roc.manifold.primarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			//////VACUUM READINGS WILL HAVE 3 TIERS
			/// ATM-> 15K microns(2000 Pa) = 10,000 BUFFER
			/// 15K -> 1K microns(134 Pa) = 500 BUFFER
			/// 1K -> 1 micron = 50 BUFFER
			if (initialRecord.manifold.primarySensor.type == ESensorType.Vacuum) {
				if (minMax.max >= 2000) {
					rangeAmount = 2666.45;
				} else if (minMax.max >= 134) {
					rangeAmount = 133.32;
				} else {
					rangeAmount = 7.0;
				}
			}
			var sensorRange = new Scalar(roc.manifold.primarySensor.unit.standardUnit, rangeAmount);

			UpdateAxis(LAX, minMax.min, minMax.max, sensorRange, sensorUnit, TLMeasurement, BLMeasurement, sensorMin);
			var primaryBuffer = roc.primarySensorPoints;
			var l = primaryBuffer.Count;
			// Resize the points list
			// Trim down to size
			while (primarySeries.Points.Count > l) {
				primarySeries.Points.RemoveAt(primarySeries.Points.Count - 1);
			}
			// Add any missing items
			while (primarySeries.Points.Count < l) {
				primarySeries.Points.Add(new DataPoint());
			}

			for (int i = 0; i < primaryBuffer.Count; i++) {
				var p = primaryBuffer[i];
				primarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), p.measurement);
			}
		}
    /// <summary>
    /// Updates the right axis
    /// </summary>
		private void InvalidateSecondary() {
			var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			if (roc.manifold.secondarySensor == null) {
				return;
			}

			if (roc == null || TRMeasurement == null || BRMeasurement == null) {
				return;
			}

			var minMax = roc.GetSecondaryMinMax();
			var rangeAmount = roc.manifold.secondarySensor.maxMeasurement.ConvertTo(roc.manifold.secondarySensor.unit.standardUnit).amount * .05;
			var sensorRange = new Scalar(roc.manifold.secondarySensor.unit.standardUnit, rangeAmount);
			var sensorUnit = roc.manifold.secondarySensor.unit;
			var sensorMin = roc.manifold.secondarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			UpdateAxis(RAX, minMax.min, minMax.max, sensorRange, sensorUnit, TRMeasurement, BRMeasurement, sensorMin);

			var secondaryBuffer = roc.secondarySensorPoints;
			var l = secondaryBuffer.Count;
			// Resize the points list
			// Trim down to size
			while (secondarySeries.Points.Count > l) {
				secondarySeries.Points.RemoveAt(secondarySeries.Points.Count - 1);
			}
			// Add any missing items
			while (secondarySeries.Points.Count < l) {
				secondarySeries.Points.Add(new DataPoint());
			}

			for (int i = 0; i < secondaryBuffer.Count; i++) {
				var p = secondaryBuffer[i];
				secondarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), p.measurement);
			}
		}

		/// <summary>
		/// Updates the axis to the given state for RoC based measurements.
		/// </summary>
		/// <param name="axis">Axis.</param>
		private void UpdateAxis(LinearAxis axis, Scalar min, Scalar max, Scalar range, Unit u, UILabel topLabel, UILabel bottomLabel, Scalar sensorMin) {
			if (max.amount < sensorMin.amount) {
				return;
			}
			if (min.amount - (range.amount / 2) < sensorMin.amount) {
				axis.Minimum = sensorMin.amount;
				bottomLabel.Text = SensorUtils.ToFormattedString(sensorMin.ConvertTo(u), true);
			} else {
				axis.Minimum = min.amount - (range.amount / 2);
				var diffScalar = new Scalar(u.standardUnit, (min.amount - (range.amount / 2)));
				bottomLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
			}

			if (max.amount + (range.amount / 2) < sensorMin.amount + range.amount) {
				axis.Maximum = sensorMin.amount + range.amount;
        var diffScalar = new Scalar(u.standardUnit, sensorMin.amount + range.amount);
				topLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
			} else {
				axis.Maximum = max.amount + (range.amount / 2);
				var diffScalar = new Scalar(u.standardUnit, (max.amount + (range.amount / 2)));
				topLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
			}

			axis.MinimumPadding = 0.25;
			axis.MaximumPadding = 0.25;
			axis.AxislineStyle = LineStyle.Solid;
			axis.AxislineThickness = 1;
			plotView.Model.PlotMargins = new OxyThickness(0,double.NaN, 0, double.NaN);
		}

		/// <summary>
		/// Sets up the graph for live trending
		/// </summary>
		/// <returns>The plot model.</returns>
		public PlotModel CreatePlotModel() {
			var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			var model = new PlotModel();

			primaryColor = OxyColors.Blue;
			secondaryColor = OxyColors.Red;

			if (roc.manifold.primarySensor.type == ESensorType.Temperature) {
				primaryColor = OxyColors.Red;
				secondaryColor = OxyColors.Blue;
			} else if (roc.manifold.primarySensor.type == ESensorType.Vacuum) {
				primaryColor = OxyColors.Maroon;
			}

			BAX = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = DateTime.Now.AddSeconds(-30).ToFileTime() - 100000,
				Maximum = DateTime.Now.ToFileTime() - 100000,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "time",
				LabelFormatter = (arg) => {
					var d = DateTime.FromFileTime((long)arg);
					return d.Hour.ToString("00") + ":" + d.Minute.ToString("00") + ":" + d.Second.ToString("00");

				},
				Font = model.DefaultFont,
				FontSize = 13,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,
			};

			var baseUnit = roc.manifold.primarySensor.unit.standardUnit;
			LAX = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "first",
				LabelFormatter = (arg) => {
					var u = roc.manifold.primarySensor.unit;
					var p = SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
					return p;
				},
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,
			};

			RAX = new LinearAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "second",
				LabelFormatter = (arg) => {
					if (roc.manifold.secondarySensor != null) {
						var u = roc.manifold.secondarySensor.unit;
						return SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
					} else {
						return "";
					}
				},
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,

			};

			primarySeries = new LineSeries() {
				StrokeThickness = 1,
				Color = primaryColor,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "first",
			};

			secondarySeries = new LineSeries() {
				StrokeThickness = 1,
				Color = secondaryColor,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "second",
			};
      model.Background = OxyColors.Transparent;

			model.PlotMargins = new OxyThickness(0, double.NaN, 0, 0);
			model.TitlePadding = 0.0;

			model.PlotType = PlotType.XY;
			model.Axes.Add(BAX);
			model.Axes.Add(LAX);
			model.Axes.Add(RAX);
			model.Series.Add(primarySeries);
			model.Series.Add(secondarySeries);

			model.PlotAreaBorderThickness = new OxyThickness(0);
			model.PlotAreaBorderColor = OxyColors.Transparent;

			return model;
		}

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}

