namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using Appion.Commons.Measure;
  using Foundation;
  using UIKit;
  using CoreGraphics;

  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.IOS.Util;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.Xamarin.iOS;
  using ION.Core.Devices;

  public class RateOfChangeRecord : SensorPropertyRecord {
    public override WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.RateOfChange;
      }
    }

    public RateOfChangeSensorProperty roc { get; set; }


    public RateOfChangeRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
    }
  }

	public partial class RateOfChangeSensorPropertyCell : UITableViewCell {
  
		public PlotView plotView;
		public LinearAxis BAX;
    public LinearAxis LAX;
		public LinearAxis RAX;
    
    public LineSeries primarySeries;
    public LineSeries secondarySeries;
    public OxyColor primaryColor;
    public OxyColor secondaryColor;

    public UILabel TLMeasurement;
    public UILabel BLMeasurement;
    public UILabel TRMeasurement;
    public UILabel BRMeasurement;

    public bool isConnected = false;
    		
    private RateOfChangeRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
        }

        __record = value;

        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
          OnSensorPropertyChanged(__record.sensorProperty);
        }
      }
    } RateOfChangeRecord __record;

    private bool isUpdating { get; set; }

    /*
      Put the legend in a table(bordered box) FINISHED
      heading for the top of the graph Live Trending FINISHED
      Description of trending rate above legend 10x/second 1 second 10 seconds 30 seconds FINISHED
      Option to set trending rate as above options FINISHED
    */

		public RateOfChangeSensorPropertyCell (IntPtr handle) : base (handle) {
		}

    public async void UpdateTo(RateOfChangeRecord record) {
      await Task.Delay(TimeSpan.FromMilliseconds(200));

    	this.Layer.BorderWidth = 1f;  
      this.record = record;
      labelTitle.Text = Strings.Workbench.Viewer.ROC;
      buttonIcon.Layer.BorderWidth = 1f;
      
      ///SETUP THE TRENDING GRAPH
      if(plotView == null){
  			plotView = new PlotView(new CGRect(0,.5 * viewBackground.Bounds.Height, viewBackground.Bounds.Width, .5 * viewBackground.Bounds.Height)){
  				Model = CreatePlotModel(),
          BackgroundColor = UIColor.Clear,
  			};
        
        TLMeasurement = new UILabel(new CGRect(10,0,.5 * plotView.Bounds.Width,20));
        TLMeasurement.AdjustsFontSizeToFitWidth = true;
        TLMeasurement.TextAlignment = UITextAlignment.Left;
        TLMeasurement.BackgroundColor = UIColor.Clear;
        
        BLMeasurement = new UILabel(new CGRect(10,30,.5 * plotView.Bounds.Width,20));
        BLMeasurement.AdjustsFontSizeToFitWidth = true;
        BLMeasurement.TextAlignment = UITextAlignment.Left;
        BLMeasurement.BackgroundColor = UIColor.Clear;
        
        TRMeasurement = new UILabel(new CGRect(.5 * plotView.Bounds.Width,0,.5 * plotView.Bounds.Width - 10,20));
        TRMeasurement.AdjustsFontSizeToFitWidth = true;
        TRMeasurement.TextAlignment = UITextAlignment.Right;
        TRMeasurement.BackgroundColor = UIColor.Clear;
        
        BRMeasurement = new UILabel(new CGRect(.5 * plotView.Bounds.Width,30,.5 * plotView.Bounds.Width - 10,20));
        BRMeasurement.AdjustsFontSizeToFitWidth = true;
        BRMeasurement.TextAlignment = UITextAlignment.Right;
        BRMeasurement.BackgroundColor = UIColor.Clear;
        
        //plotView.AddSubview(TLMeasurement);
        //plotView.AddSubview(BLMeasurement);
        //plotView.AddSubview(TRMeasurement);
        //plotView.AddSubview(BRMeasurement);

        plotView.Layer.BorderWidth = 1f;
        plotView.UserInteractionEnabled = false;
        viewBackground.AddSubview(plotView);
      }
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
      if (!isConnected) {
        updateCellGraph();
      }
    }

    private async void DoUpdateCell() {
      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;

      var sp = record.sensorProperty as RateOfChangeSensorProperty;
      var rocScalar = sp.GetPrimaryAverageRateOfChange();
      var abs = Math.Abs(rocScalar.magnitude);
      var range = (sp.sensor.maxMeasurement.ConvertTo(rocScalar.unit) - sp.sensor.minMeasurement.ConvertTo(rocScalar.unit)) / 10;

      if (abs > range.magnitude) {
        labelMeasurement.Text = ">" + SensorUtils.ToFormattedString(sp.sensor.type, range) + Strings.Measure.PER_MINUTE;
      } else {
				labelMeasurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, rocScalar.unit.OfScalar(abs)) + Strings.Measure.PER_MINUTE;
      }

      if (rocScalar.magnitude == 0) {
        buttonIcon.Hidden = true;
        labelMeasurement.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        buttonIcon.Hidden = false;
        if (rocScalar < 0) {
          buttonIcon.SetImage(UIImage.FromBundle("ic_arrow_trend_down"), UIControlState.Normal);
        } else {
          buttonIcon.SetImage(UIImage.FromBundle("ic_arrow_trend_up"), UIControlState.Normal);
        }

        await Task.Delay(100);
        DoUpdateCell();
      }
    }

    public async void updateCellGraph(){

      if(plotView == null){
        return;
      }

      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
      isConnected = device.isConnected;

      if (device == null || device.isConnected) {
          InvalidatePrimary();
          if(record.manifold.secondarySensor != null){
            InvalidateSecondary();
          }
          InvalidateTime();
  
        InvokeOnMainThread ( () => {    
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
      } 

      await Task.Delay(TimeSpan.FromMilliseconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")));
      if (isConnected) {
        updateCellGraph();
      }
    }
    
    private void InvalidateTime() {
      var axis = BAX;
			var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

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
			axis.PlotModel.PlotMargins = new OxyThickness(-7, -7, -7, 3);

			axis.MinorStep = axis.MajorStep / 5;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
    }

    private void InvalidatePrimary() {
			var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			if (roc == null || TLMeasurement == null || BLMeasurement == null) {
        return;
      }
      
      var minMax = roc.GetPrimaryMinMax();
      var rangeAmount = roc.manifold.primarySensor.maxMeasurement.ConvertTo(roc.manifold.primarySensor.unit.standardUnit).amount * .05;
      var sensorRange = new Scalar(roc.manifold.primarySensor.unit.standardUnit,rangeAmount);
      var sensorUnit = roc.manifold.primarySensor.unit;
      var sensorMin = roc.manifold.primarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

      UpdateAxis(LAX, minMax.min, minMax.max, sensorRange,sensorUnit,TRMeasurement,BRMeasurement,sensorMin);
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

    private void InvalidateSecondary() {
			var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
			if (record.manifold.secondarySensor == null) {  
        return;
      }

      if (roc == null || TRMeasurement == null || BRMeasurement == null) {
        return;
      }

      var minMax = roc.GetSecondaryMinMax();
      var rangeAmount = roc.manifold.secondarySensor.maxMeasurement.ConvertTo(roc.manifold.secondarySensor.unit.standardUnit).amount * .05;
      var sensorRange = new Scalar(roc.manifold.secondarySensor.unit.standardUnit,rangeAmount);
      var sensorUnit = roc.manifold.secondarySensor.unit;
      var sensorMin = roc.manifold.secondarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

      UpdateAxis(RAX, minMax.min, minMax.max, sensorRange,sensorUnit,TRMeasurement,BRMeasurement,sensorMin);

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
      if(max.amount < sensorMin.amount){
        return;
      }
      if(min.amount - (range.amount / 2) < sensorMin.amount){
        axis.Minimum = sensorMin.amount;
        bottomLabel.Text = sensorMin.ConvertTo(u).amount + u.ToString();
      } else {
        axis.Minimum = min.amount - (range.amount / 2);
        bottomLabel.Text = (min.ConvertTo(u).amount - (range.ConvertTo(u).amount / 2)) + u.ToString();
      }

      if(max.amount + (range.amount / 2) < sensorMin.amount + range.amount){
        axis.Maximum = sensorMin.amount + range.amount;
        topLabel.Text = SensorUtils.ToFormattedString(range.ConvertTo(u),true);
      } else {
        axis.Maximum = max.amount + (range.amount / 2);
        topLabel.Text = SensorUtils.ToFormattedString(new Scalar(u,(max.ConvertTo(u).amount + (range.ConvertTo(u).amount / 2))),true);
      }

      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
    }

    /// <summary>
    /// Sets up the graph for live trending
    /// </summary>
    /// <returns>The plot model.</returns>
    public PlotModel CreatePlotModel(){
			var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      var model = new PlotModel();

      primaryColor = OxyColors.Blue;
      secondaryColor = OxyColors.Red;
      
      if(roc.manifold.primarySensor.type == ESensorType.Temperature){
        primaryColor = OxyColors.Red;
        secondaryColor = OxyColors.Blue;
      } else if (roc.manifold.primarySensor.type == ESensorType.Vacuum){
        primaryColor = OxyColors.Maroon;
      }
      Console.WriteLine("Creating roc plotmodel for " + roc.manifold.primarySensor.type+ " sensor with primary color " + primaryColor + " and secondary color " + secondaryColor);

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
        FontSize = 10,
        TextColor = OxyColors.Black,
        AxislineThickness = 0,
        AxislineStyle = LineStyle.None,
        MajorGridlineStyle = LineStyle.None,
        MinorGridlineStyle = LineStyle.None,
        AxisTickToLabelDistance = 0.0,
        TickStyle = TickStyle.None,
      };

      var baseUnit = record.manifold.primarySensor.unit.standardUnit;
      LAX = new LinearAxis() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "first",
        LabelFormatter = (arg) => {
          var u = record.manifold.primarySensor.unit;
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
		    TickStyle = TickStyle.None,
	    };
      
      RAX = new LinearAxis() {
        Position = AxisPosition.Right,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "second",
        LabelFormatter = (arg) => {
          if (record.manifold.secondarySensor != null) {
            var u = record.manifold.secondarySensor.unit;
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
		    TickStyle = TickStyle.None,
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
      
      model.PlotMargins = new OxyThickness(-7,-7,-7,-5);
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
	}
}
