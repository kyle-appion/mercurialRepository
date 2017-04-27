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

        plotView.Layer.BorderWidth = 1f;
        viewBackground.AddSubview(plotView);
        updateCellGraph();
      }
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
    }

    private async void DoUpdateCell() {
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
      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
      if (device == null || device.isConnected) {
          InvalidatePrimary();
          InvalidateSecondary();
          InvalidateTime();
  
        InvokeOnMainThread ( () => {     
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
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

      if (roc == null) {
        return;
      }

      var minMax = roc.GetPrimaryMinMax();

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

      if (roc == null) {
        return;
      }

      var minMax = roc.GetSecondaryMinMax();

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
      //axis.AxisTickToLabelDistance = -(MeasureTextWidth(axis) + axis.MajorTickSize + 5);
    }

    /// <summary>
    /// Measures the largest text size for the axis.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="axis">Axis.</param>
    //private double MeasureTextWidth(LinearAxis axis) {
    //  IList<double> majorLabelValues = new List<double>();
    //  IList<double> majorTickValues = new List<double>();
    //  IList<double> minorTickValues = new List<double>();

    //  if (axis.ActualMinorStep == 0 || axis.ActualMajorStep == 0) {
    //    return 0;
    //  }

    //  axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);

    //  double bestWidth = 0;
    //  foreach (var label in majorLabelValues) {
    //    var size = rc.MeasureText(axis.LabelFormatter(label), axis.Font, axis.FontSize, axis.FontWeight);
    //    if (size.Width > bestWidth) {
    //      bestWidth = size.Width;
    //    }
    //  }

    //  return bestWidth;
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
