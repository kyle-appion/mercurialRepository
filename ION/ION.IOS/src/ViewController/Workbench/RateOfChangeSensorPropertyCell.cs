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
  using ION.Core.App;

  public class RateOfChangeRecord : SensorPropertyRecord {
    public override WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.RateOfChange;
      }
    }

    public RateOfChangeSensorProperty roc { get; set; }

		public RateOfChangeRecord(Sensor sensor, ISensorProperty sensorProperty) : base(sensor, sensorProperty) {
			roc = sensor.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
    }
  }

  public partial class RateOfChangeSensorPropertyCell : UITableViewCell {

    public PlotView plotView {
      get {
        return __plotView;
      }
     set {
        if(__plotView != null){
          __plotView = null;
        }
        __plotView = value;
        if(__plotView != null && isConnected){
					updateCellGraph();
				}
      }
    } PlotView __plotView;

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
    public IION ion;

    public bool isConnected = false;
    		
    public RateOfChangeRecord record {
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
      ion = AppState.context;
      await Task.Delay(TimeSpan.FromMilliseconds(200));
      isConnected = (bool)(record.sensor as GaugeDeviceSensor)?.device.isConnected;
			primaryColor = OxyColors.Blue;
			secondaryColor = OxyColors.Red;

			if (record.roc.sensor.type == ESensorType.Temperature) {
				primaryColor = OxyColors.Red;
				secondaryColor = OxyColors.Blue;
			} else if (record.roc.sensor.type == ESensorType.Vacuum) {
				primaryColor = OxyColors.Maroon;
			} 

    	this.Layer.BorderWidth = 1f;
      this.record = record;
      labelTitle.Text = Strings.Workbench.Viewer.TREND;
      buttonIcon.Layer.BorderWidth = 1f;
			labelMeasurement.Hidden = true;

			///SETUP THE TRENDING GRAPH
			if(plotView == null){
        plotView = new PlotView(new CGRect(0,.5 * viewBackground.Bounds.Height, viewBackground.Bounds.Width, .5 * viewBackground.Bounds.Height)){
          Model = CreatePlotModel(),
          BackgroundColor = UIColor.Clear,
        };

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
        if (plotView != null) {
          plotView.Hidden = true;
          labelMeasurement.Hidden = true;
        }
        updateCellGraph();
      } 
    }


    private async void DoUpdateCell() {
      var sp = record.sensorProperty as RateOfChangeSensorProperty;
      var rocScalar = sp.GetPrimaryAverageRateOfChange();
      var abs = Math.Abs(rocScalar.magnitude);
      var range = (sp.sensor.maxMeasurement.ConvertTo(rocScalar.unit) - sp.sensor.minMeasurement.ConvertTo(rocScalar.unit)) / 10;

      if (abs > range.magnitude) {
        labelMeasurement.Text = ">" + SensorUtils.ToFormattedString(sp.sensor.type, range) + " " + rocScalar.unit.ToString() + Strings.Measure.PER_MINUTE;
      } else {
        labelMeasurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, rocScalar.unit.OfScalar(abs)) + " " + rocScalar.unit.ToString() + Strings.Measure.PER_MINUTE;
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

			var device = (record.sensor as GaugeDeviceSensor)?.device;
      isConnected = device.isConnected;

      if (device == null || device.isConnected) {
        InvalidatePrimary();
				if(record.sensor.linkedSensor != null && !(record.sensor.linkedSensor is ManualSensor)){
          InvalidateSecondary();
        }
        InvalidateTime();

        InvokeOnMainThread ( () => {    
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
      } 

      await Task.Delay(TimeSpan.FromMilliseconds(ion.preferences.device.trendInterval.TotalMilliseconds));
      if (isConnected) {
        labelMeasurement.Hidden = false;
		    plotView.Hidden = false;
        updateCellGraph();
      } else {
				labelMeasurement.Hidden = true;
				plotView.Hidden = true;
      }
    }

    private void InvalidateTime() {
      var axis = BAX;

			if (record.roc == null) {
        return;
      }

      var points = record.roc.primarySensorPoints;

      if (points.Count <= 0) {
        Console.WriteLine("Failed to invalidate time: points.count was " + points.Count);
        return;
      }
      var startTime = points[0];
      var endTime = points[points.Count - 1].date;

      axis.Minimum = (startTime.date - record.roc.window).ToFileTime() - 1000000;
      axis.Maximum = startTime.date.ToFileTime() + 1000000;

      axis.MajorStep = (long)((record.roc.window.TotalMilliseconds * 1e4) / 2);
			axis.PlotModel.PlotMargins = new OxyThickness(-7, -7, -7, 3);

      axis.MinorStep = axis.MajorStep / 5;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
    }

    private void InvalidatePrimary() {
      var holderRoc = record.roc;

	    if (record.roc == null) {
        return;
      }
      
      var minMax = record.roc.GetPrimaryMinMax();
			//var rangeAmount = record.roc.manifold.primarySensor.maxMeasurement.ConvertTo(record.roc.manifold.primarySensor.unit.standardUnit).amount * .05;
			var rangeAmount = record.roc.sensor.maxMeasurement.ConvertTo(record.roc.sensor.unit.standardUnit).amount * .05;
			//////VACUUM READINGS WILL HAVE 3 TIERS
			/// ATM-> 15K microns(2000 Pa) = 10,000 BUFFER
			/// 15K -> 1K microns(134 Pa) = 500 BUFFER
			/// 1K -> 1 micron = 50 BUFFER
			//if (record.manifold.primarySensor.type == ESensorType.Vacuum){
			if(record.sensor.type == ESensorType.Vacuum){
        if(minMax.max >= 2000){
					rangeAmount = 2666.45;
        } else if (minMax.max >= 134){
          rangeAmount = 133.32; 
        } else {   
          rangeAmount = 7.0;
        }
      }
			//var sensorRange = new Scalar(record.roc.manifold.primarySensor.unit.standardUnit, rangeAmount);
			var sensorRange = new Scalar(record.roc.sensor.unit.standardUnit, rangeAmount);
			//var sensorUnit = record.roc.manifold.primarySensor.unit;
			var sensorUnit = record.roc.sensor.unit;
			//var sensorMin = record.roc.manifold.primarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);
			var sensorMin = record.roc.sensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			//UpdateAxis(LAX, minMax.min, minMax.max, sensorRange, sensorUnit, TLMeasurement, BLMeasurement, sensorMin);
			UpdateAxis(LAX, minMax.min, minMax.max, sensorRange,sensorUnit,sensorMin);
      var primaryBuffer = record.roc.primarySensorPoints;
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
			//if (record.manifold.secondarySensor == null) {
			if (record.sensor.linkedSensor == null) {
        return;
      }

      if (record.roc == null) {
        return;
      }

      var minMax = record.roc.GetSecondaryMinMax();

			//var rangeAmount = record.roc.manifold.secondarySensor.maxMeasurement.ConvertTo(record.roc.manifold.secondarySensor.unit.standardUnit).amount * .05;
			var rangeAmount = record.roc.sensor.linkedSensor.maxMeasurement.ConvertTo(record.roc.sensor.linkedSensor.unit.standardUnit).amount * .05;
			//var sensorRange = new Scalar(record.roc.manifold.secondarySensor.unit.standardUnit, rangeAmount);
			var sensorRange = new Scalar(record.roc.sensor.linkedSensor.unit.standardUnit,rangeAmount);
			//var sensorUnit = record.roc.manifold.secondarySensor.unit;
			var sensorUnit = record.roc.sensor.linkedSensor.unit;
			//var sensorMin = record.roc.manifold.secondarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);
			var sensorMin = record.roc.sensor.linkedSensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			//UpdateAxis(RAX, minMax.min, minMax.max, sensorRange, sensorUnit, TRMeasurement, BRMeasurement, sensorMin);
			UpdateAxis(RAX, minMax.min, minMax.max, sensorRange,sensorUnit,sensorMin);

      var secondaryBuffer = record.roc.secondarySensorPoints;
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
    private void UpdateAxis(LinearAxis axis, Scalar min, Scalar max, Scalar range, Unit u, Scalar sensorMin) {
      if(max.amount < sensorMin.amount){
        return;
      }
      if(min.amount - (range.amount / 2) < sensorMin.amount){
	      axis.Minimum = sensorMin.amount;
			  //bottomLabel.Text = SensorUtils.ToFormattedString(sensorMin.ConvertTo(u), true);
		  } else {
	      axis.Minimum = min.amount - (range.amount / 2);
			  //var diffScalar = new Scalar(u.standardUnit, (min.amount - (range.amount / 2)));
			  //bottomLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
      }
      if(max.amount + (range.amount / 2) < sensorMin.amount + range.amount){
        axis.Maximum = sensorMin.amount + range.amount;
			  //var diffScalar = new Scalar(u.standardUnit, sensorMin.amount + range.amount);
			  //topLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
      } else {
        axis.Maximum = max.amount + (range.amount / 2);
			  //var diffScalar = new Scalar(u.standardUnit, (max.amount + (range.amount / 2)));
			  //topLabel.Text = SensorUtils.ToFormattedString(diffScalar.ConvertTo(u), true);
      }
		  primarySeries.Color = primaryColor;
		  secondarySeries.Color = secondaryColor;

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
      var model = new PlotModel();

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

			//var baseUnit = record.manifold.primarySensor.unit.standardUnit;
			var baseUnit = record.sensor.unit.standardUnit;
      LAX = new LinearAxis() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "first",
        LabelFormatter = (arg) => {
			    //var u = record.manifold.primarySensor.unit;
			    var u = record.sensor.unit;
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
			    //if (record.manifold.secondarySensor != null){
				  if (record.sensor.linkedSensor != null) {
    				//var u = record.manifold.secondarySensor.unit;
    				var u = record.sensor.linkedSensor.unit;
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
      model.Background = OxyColors.Transparent;

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