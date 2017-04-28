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
    
    public UILabel TLMeasurement;
    public UILabel BLMeasurement;
    public UILabel TRMeasurement;
    public UILabel BRMeasurement;
    
    public GaugeDeviceSensor gaugeSensor;
    
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

		public RateOfChangeSensorPropertyCell (IntPtr handle) : base (handle) {
		}

    public async void UpdateTo(RateOfChangeRecord record) {
      await Task.Delay(TimeSpan.FromMilliseconds(200));
      gaugeSensor = record.manifold.primarySensor  as GaugeDeviceSensor;
      
    	this.Layer.BorderWidth = 1f;  
      this.record = record;
      labelTitle.Text = Strings.Workbench.Viewer.ROC;
      buttonIcon.Layer.BorderWidth = 1f;
      
      ///SETUP THE TRENDING GRAPH
      if(plotView == null){      
  			plotView = new PlotView(new CGRect(0,35, viewBackground.Bounds.Width, 85)){
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
        
        plotView.AddSubview(TLMeasurement);
        plotView.AddSubview(BLMeasurement);
        plotView.AddSubview(TRMeasurement);
        plotView.AddSubview(BRMeasurement);

        plotView.Layer.BorderWidth = 1f;
        plotView.UserInteractionEnabled = false;
        viewBackground.AddSubview(plotView);
        updateCellGraph();
      }
      //if(TLMeasurement == null){
      //  TLMeasurement = new UILabel(new CGRect(10,0,.5 * plotView.Bounds.Width,20));
      //}
      //if(BLMeasurement == null){
      //  BLMeasurement = new UILabel(new CGRect(10,0,.5 * plotView.Bounds.Width,20));
      //}
      //if(TRMeasurement == null){
      //  TRMeasurement = new UILabel(new CGRect(10,0,.5 * plotView.Bounds.Width,20));
      //}
      //if(BRMeasurement == null){
      //  BRMeasurement = new UILabel(new CGRect(10,0,.5 * plotView.Bounds.Width,20));
      //}
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
    }

    private async void DoUpdateCell() {
      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
      
      if(device != null && device.isConnected && isConnected == false){
        isConnected = true;
        updateCellGraph();
      }
      
      var sp = record.sensorProperty as RateOfChangeSensorProperty;
			var roc = sp.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
			var abs = Math.Abs(roc.amount);
      var range = (sp.sensor.maxMeasurement - sp.sensor.minMeasurement) / 10;

			if (abs > range.magnitude) {
        labelMeasurement.Text = ">" + SensorUtils.ToFormattedString(sp.sensor.type, range) + Strings.Measure.PER_MINUTE;
      } else {
				labelMeasurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, roc.unit.OfScalar(abs)) + Strings.Measure.PER_MINUTE;
      }

			if (roc.amount == 0) {
        buttonIcon.Hidden = true;
        labelMeasurement.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        buttonIcon.Hidden = false;
        if (roc < 0) {
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
      if (device == null || device.isConnected) {
          InvalidatePrimary();
          InvalidateSecondary();
          InvalidateTime();
  
        InvokeOnMainThread ( () => {    
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
      } else {
        isConnected = false;
        plotView.Model.PlotMargins = new OxyThickness(0,double.NaN,0,0);
        return;
      }
      await Task.Delay(TimeSpan.FromMilliseconds(100));
      updateCellGraph();   
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
      TLMeasurement.Text = SensorUtils.ToFormattedString(minMax.max.ConvertTo(roc.manifold.primarySensor.unit),true);
      BLMeasurement.Text = SensorUtils.ToFormattedString(minMax.min.ConvertTo(roc.manifold.primarySensor.unit),true);
      
      UpdateAxis(LAX, minMax.min, minMax.max, record.manifold.primarySensor.unit, 1, 5);      
      
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
      if (record.manifold.secondarySensor == null) {  
        return;
      }

      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null || TRMeasurement == null || BRMeasurement == null) {
        return;
      }

      var minMax = roc.GetSecondaryMinMax();
      
      if(roc.manifold.secondarySensor != null){
        TRMeasurement.Text = SensorUtils.ToFormattedString(minMax.max.ConvertTo(roc.manifold.secondarySensor.unit),true);
        BRMeasurement.Text = SensorUtils.ToFormattedString(minMax.min.ConvertTo(roc.manifold.secondarySensor.unit),true);
      }
      
      UpdateAxis(RAX, minMax.min, minMax.max, record.manifold.secondarySensor.unit, 1, 5);

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
    /// Updates the axis to the given state.
    /// </summary>
    /// <param name="axis">Axis.</param>
    private void UpdateAxis(LinearAxis axis, Scalar min, Scalar max, Unit u, int major = 3, int minor = 5) {
      var su = u.standardUnit;
      var diff = (max - min).ConvertTo(su).magnitude;

      if (Math.Abs(diff)/major > 1) {
        axis.Minimum = min.ConvertTo(su).amount;
        axis.Maximum = max.ConvertTo(su).amount;
      } else {
        var one = u.OfScalar(1);
        axis.Minimum = (min - one).ConvertTo(su).magnitude;
        axis.Maximum = (max + one).ConvertTo(su).magnitude;
        diff = u.OfScalar(3).ConvertTo(su).amount;
      }

      var padding = diff * 0.1;
      axis.Minimum -= padding;
      axis.Maximum += padding;

      var mod = diff / major;
      var tmod = mod / (double)minor;
      
      if(tmod == 0){
        tmod = 1;
      }
      if(mod == 0){
        mod = 1;
      }
      
      axis.MajorStep = mod;
      axis.MajorTickSize = 10;
      axis.TicklineColor = OxyColors.Black;

      axis.MinorStep = tmod;
      axis.MinorTickSize = 5;
      axis.TicklineColor = OxyColors.Black;

      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
        plotView.Model.PlotMargins = new OxyThickness(0,double.NaN, 0, double.NaN);
      
    }

    /// <summary>
    /// Measures the largest text size for the axis.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="axis">Axis.</param>
    //private double MeasureTextWidth(LinearAxis axis) {
      //IList<double> majorLabelValues = new List<double>();
      //IList<double> majorTickValues = new List<double>();
      //IList<double> minorTickValues = new List<double>();

      //if (axis.ActualMinorStep == 0 || axis.ActualMajorStep == 0) {
      //  return 0;
      //}

      //axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);

      //double bestWidth = 0;
      //foreach (var tickLabel in majorLabelValues) {
      //  var size = rc.MeasureText(axis.LabelFormatter(tickLabel), axis.Font, axis.FontSize, axis.FontWeight);        
      //  if (size.Width > bestWidth) {
      //    bestWidth = size.Width;
      //  }
      //}

      //return bestWidth;
    //}
   
    public PlotModel CreatePlotModel(){
    	Console.WriteLine("Creating roc plotmodel");
      var model = new PlotModel();
      
      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      
      BAX = new LinearAxis() {
        Position = AxisPosition.Bottom,
        Minimum = 0,
        Maximum = roc.window.TotalMilliseconds,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 3,
        MaximumPadding = 3,
        Key = "time",
        LabelFormatter = (arg) => {
          var d = DateTime.FromFileTime((long)arg);
          return d.Hour.ToString("00") + ":" + d.Minute.ToString("00") + ":" + d.Second.ToString("00");
        },
        Font = model.DefaultFont,
        FontSize = 15,
        TextColor = OxyColors.Black,
        AxislineThickness = 0,
        AxislineStyle = LineStyle.None,
        MajorGridlineStyle = LineStyle.None,
        MinorGridlineStyle = LineStyle.None,
      };

      var baseUnit = record.manifold.primarySensor.unit.standardUnit;
      LAX = new LinearAxis() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = true,
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
      };

      primarySeries = new LineSeries() {
        StrokeThickness = 1,
        Color = OxyColors.Red,
        MarkerType = MarkerType.None,
        MarkerSize = 0,
        MarkerStroke = OxyColors.Transparent,
        MarkerStrokeThickness = 0,
        YAxisKey = "first",
      };

      secondarySeries = new LineSeries() {
        StrokeThickness = 1,
        Color = OxyColors.Blue,
        MarkerType = MarkerType.None,
        MarkerSize = 0,
        MarkerStroke = OxyColors.Transparent,
        MarkerStrokeThickness = 0,
        YAxisKey = "second",
      };
      
      model.PlotMargins = new OxyThickness(0,double.NaN,0,0);

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
