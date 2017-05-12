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
    
    public List<TrendPoint> primaryPlots;
    public List<TrendPoint> secondaryPlots;
    
    public double primaryMin;
    public double primaryMax;
    public double secondaryMin;
    public double secondaryMax;  
    
    public RateOfChangeRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
    }
    /// <summary>
    /// Sets the minimum and the maximum for a plot series
    /// </summary>
    /// <returns>not a thing</returns>
    public void setMinMax(List<TrendPoint> plotList, bool primary = false){
      if(roc == null){
        return;
      }
      var startSmall = plotList[0].measurement;
      var startHigh = plotList[0].measurement;
      
      for(int i = 1; i < plotList.Count; i++){
        if(startSmall > plotList[i].measurement){
          startSmall = plotList[i].measurement;
        }
        
        if(startHigh < plotList[i].measurement){
          startHigh = plotList[i].measurement;
        }
      }
      
      if(primary){
        if(startSmall < (roc.manifold.primarySensor.maxMeasurement.ConvertTo(roc.manifold.primarySensor.unit).amount * .05)){
          startSmall = roc.manifold.primarySensor.minMeasurement.ConvertTo(roc.manifold.primarySensor.unit).amount;
        }
        primaryMin = startSmall;
        if(startHigh < (roc.manifold.primarySensor.maxMeasurement.ConvertTo(roc.manifold.primarySensor.unit).amount *  .05)){
          startHigh = roc.manifold.primarySensor.maxMeasurement.ConvertTo(roc.manifold.primarySensor.unit).amount * .05;
        }
        primaryMax = startHigh;
      } else {
        if(startSmall < (roc.manifold.secondarySensor.maxMeasurement.ConvertTo(roc.manifold.secondarySensor.unit).amount * .05)){
          startSmall = roc.manifold.secondarySensor.minMeasurement.ConvertTo(roc.manifold.secondarySensor.unit).amount;
        }
        secondaryMin = startSmall;
        if(startHigh < (roc.manifold.secondarySensor.maxMeasurement.ConvertTo(roc.manifold.secondarySensor.unit).amount  * .05)){
          startHigh = roc.manifold.secondarySensor.maxMeasurement.ConvertTo(roc.manifold.secondarySensor.unit).amount * .05;
        }        
        secondaryMax = startHigh;
      }      
    }
    
    /// <summary>
    /// A structure representing the x,y points on the graph.
    /// </summary>
    public struct TrendPoint {
      /// <summary>
      /// The time that the measurement was made.
      /// </summary>
      public DateTime date { get; private set; }
      /// <summary>
      /// The measurement of a point on the graph. The measurement's unit is the base unit for the sensor.
      /// </summary>
      public double measurement { get; private set; }

      public TrendPoint(double measurement) {
        date = DateTime.Now;
        this.measurement = measurement;
      }
      
      // Overridden from object
      public override string ToString() {
        return string.Format("[TrendPoint: date={0}, measurement={1}]", date, measurement);
      }
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
      gaugeSensor = record.manifold.primarySensor  as GaugeDeviceSensor;
      
    	this.Layer.BorderWidth = 1f;  
      this.record = record;
      labelTitle.Text = Strings.Workbench.Viewer.ROC;
      buttonIcon.Layer.BorderWidth = 1f;
      
      ///SETUP THE TRENDING GRAPH
      if(plotView == null){
        record.primaryPlots = new List<RateOfChangeRecord.TrendPoint>();      
        record.secondaryPlots = new List<RateOfChangeRecord.TrendPoint>();
    
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
        TrendingIntervalLog();
      }
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
      updateCellGraph();
    }

    private async void DoUpdateCell() {
      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
      
      if(device != null && device.isConnected && isConnected == false){
        isConnected = true;
      }
      
      var sp = record.sensorProperty as RateOfChangeSensorProperty;
			var rocScalar = sp.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
			var abs = Math.Abs(rocScalar.amount);
      var range = (sp.sensor.maxMeasurement.ConvertTo(rocScalar.unit) - sp.sensor.minMeasurement.ConvertTo(rocScalar.unit)) / 10;

			if (abs > range.magnitude) {
        labelMeasurement.Text = ">" + SensorUtils.ToFormattedString(sp.sensor.type, range) + Strings.Measure.PER_MINUTE;
      } else {
				labelMeasurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, rocScalar.unit.OfScalar(abs)) + Strings.Measure.PER_MINUTE;
      }

			if (rocScalar.amount == 0) {
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
    /// <summary>
    /// Records the intervals for slower interval selections
    /// </summary>
    public async void TrendingIntervalLog(){

      if(record.primaryPlots.Count >= 300){
        record.primaryPlots.RemoveAt(0);
      }
      var addPrimary = record.manifold.primarySensor.measurement.amount;      
      record.primaryPlots.Add(new RateOfChangeRecord.TrendPoint(addPrimary));
      ////UPDATE THE MINIMUM AND MAXIMUM READING FOR A SENSOR
      record.setMinMax(record.primaryPlots, true);
      if(record.manifold.secondarySensor != null){
        ////TRIM THE SECONDARY LIST TO HAVE NO MORE THAN 300 POINTS
        if(record.secondaryPlots.Count >= 300){
          record.secondaryPlots.RemoveAt(0);
        }      
        ////RECORD THE LATEST SECONDARY MEASUREMENT
        var addSecondary = record.manifold.secondarySensor.measurement.amount;
        record.secondaryPlots.Add(new RateOfChangeRecord.TrendPoint(addSecondary));
        record.setMinMax(record.secondaryPlots);
      }
      await Task.Delay(TimeSpan.FromMilliseconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")));
      TrendingIntervalLog();
    }
    
    public async void updateCellGraph(){
      record.roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      if(plotView == null){
        return;
      }
     
      var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
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
      } else {
        isConnected = false;
        plotView.Model.PlotMargins = new OxyThickness(0,double.NaN,0,0);
        return;
      }
      await Task.Delay(TimeSpan.FromMilliseconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")));
      updateCellGraph();   
    }
    
    private void InvalidateTime() {
      var axis = BAX;

      if (record.roc == null) {
        return;
      }
      ////IF INTERVAL IS 10x PER SECOND USE THE RATE OF CHANGE SENSOR TO ALLOW FOR SPEED OF RECORDING
      if(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 100){
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

      } 
      ////USE THE MANUALLY RECORDED INTERVAL READINGS
      else {
        var points = record.primaryPlots;
  
        if (points.Count <= 0) {
          Console.WriteLine("Failed to invalidate time: points.count was " + points.Count);
          //return;
        } else {
          var startTime = points[0];
          var endTime = points[points.Count - 1].date;
          
          axis.Minimum = endTime.AddMilliseconds(-300 * NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")).ToFileTime() - 1000000;
          axis.Maximum = endTime.ToFileTime() - 1000000;
 
          axis.MajorStep = (long)((300 * NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") * 1e4) / 2);
        }
      }
      axis.MinorStep = axis.MajorStep / 5;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
    }

    private void InvalidatePrimary() {

      if (record.roc == null || TLMeasurement == null || BLMeasurement == null) {
        return;
      }

      
      if(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 100){
        var minMax = record.roc.GetPrimaryMinMax();
        var rangeAmount = record.roc.manifold.primarySensor.maxMeasurement.ConvertTo(record.roc.manifold.primarySensor.unit.standardUnit).amount * .05;
        var sensorRange = new Scalar(record.roc.manifold.primarySensor.unit.standardUnit,rangeAmount);
        var sensorUnit = record.roc.manifold.primarySensor.unit;
        var sensorMin = record.roc.manifold.primarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);
        
        UpdateAxis(LAX, minMax.min, minMax.max, sensorRange,sensorUnit,TRMeasurement,BRMeasurement,sensorMin);
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
      } else {
        var primaryBuffer = record.primaryPlots;
        var l = primaryBuffer.Count;
        
        var minMax = record.roc.GetPrimaryMinMax();
        var rangeAmount = record.roc.manifold.primarySensor.maxMeasurement.ConvertTo(record.roc.manifold.primarySensor.unit.standardUnit).amount * .05;
        var sensorRange = new Scalar(record.roc.manifold.primarySensor.unit.standardUnit,rangeAmount);
        var sensorMin = record.roc.manifold.primarySensor.minMeasurement;
        var sensorUnit = record.roc.manifold.primarySensor.unit;
        
        UpdateAxis(LAX, minMax.min, minMax.max,sensorRange, sensorUnit,TLMeasurement,BLMeasurement, sensorMin);       
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
    }

    private void InvalidateSecondary() {
      if (record.manifold.secondarySensor == null) {  
        return;
      }

      if (record.roc == null || TRMeasurement == null || BRMeasurement == null) {
        return;
      }

      if(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval") == 100){
        var minMax = record.roc.GetSecondaryMinMax();
        var rangeAmount = record.roc.manifold.secondarySensor.maxMeasurement.ConvertTo(record.roc.manifold.secondarySensor.unit.standardUnit).amount * .05;
        var sensorRange = new Scalar(record.roc.manifold.secondarySensor.unit.standardUnit,rangeAmount);
        var sensorUnit = record.roc.manifold.secondarySensor.unit;
        var sensorMin = record.roc.manifold.secondarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);
        
        UpdateAxis(RAX, minMax.min, minMax.max, sensorRange,sensorUnit,TRMeasurement,BRMeasurement,sensorMin);
  
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
      } else {
        var rangeAmount = record.roc.manifold.secondarySensor.maxMeasurement.amount * .05;
        var sensorRange = new Scalar(record.roc.manifold.secondarySensor.unit,rangeAmount);
        var sensorMin = record.roc.manifold.secondarySensor.minMeasurement;
        
        UpdateAxis(RAX, record.secondaryMin, record.secondaryMax, sensorRange,record.roc.manifold.secondarySensor.unit,TRMeasurement,BRMeasurement,sensorMin);
  
        var secondaryBuffer = record.secondaryPlots;
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
    }
 
    /// <summary>
    /// Updates the axis for non RoC based measurements.
    /// </summary>
    /// <param name="axis">Axis.</param>
    private void UpdateAxis(LinearAxis axis, double min, double max, Scalar range, Unit u, UILabel topLabel, UILabel bottomLabel, Scalar sensorMin) {
      if(max < sensorMin.amount){
        return;
      }
      if((min - (range.amount / 2)) < sensorMin.ConvertTo(u).amount){
        axis.Minimum = sensorMin.ConvertTo(u).amount;
        bottomLabel.Text = SensorUtils.ToFormattedString(sensorMin.ConvertTo(u),true);
      } else {
        axis.Minimum = min - (range.amount / 2);
        var minScalar = new Scalar(u,(min - (range.amount / 2)));
        bottomLabel.Text = SensorUtils.ToFormattedString(minScalar,true);
      }
      
      if(max <= (sensorMin.ConvertTo(u).amount + range.amount)){
        axis.Maximum = sensorMin.ConvertTo(u).amount + range.amount;
        var maxScalar = new Scalar(u,(max));
        topLabel.Text = SensorUtils.ToFormattedString(maxScalar.ConvertTo(u),true);       
      } else {
        axis.Maximum = max + (range.amount / 2);
        var maxScalar = new Scalar(u,(max + (range.amount / 2)));
        topLabel.Text = SensorUtils.ToFormattedString(maxScalar,true);        
      }      
           
      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
      plotView.Model.PlotMargins = new OxyThickness(0,double.NaN, 0, double.NaN);      
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
      
      if(min.unit == Units.Temperature.KELVIN){
        Console.WriteLine("Min axis: " + axis.Minimum);
        Console.WriteLine("Max axis: " + axis.Minimum);
        
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
    public PlotModel CreatePlotModel(){      
      record.roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      
      record.primaryPlots.Add(new RateOfChangeRecord.TrendPoint(record.manifold.primarySensor.measurement.amount));
      record.primaryMin = record.primaryPlots[0].measurement;    
      record.primaryMax = record.primaryPlots[0].measurement;
    
      if(record.manifold.secondarySensor != null){
        record.secondaryPlots.Add(new RateOfChangeRecord.TrendPoint(record.manifold.secondarySensor.measurement.amount));
        record.secondaryMin = record.secondaryPlots[0].measurement;    
        record.secondaryMax = record.secondaryPlots[0].measurement;
      }
      
      var model = new PlotModel();      

      primaryColor = OxyColors.Blue;
      secondaryColor = OxyColors.Red;
      
      if(record.roc.manifold.primarySensor.type == ESensorType.Temperature){
        primaryColor = OxyColors.Red;
        secondaryColor = OxyColors.Blue;
      } else if (record.roc.manifold.primarySensor.type == ESensorType.Vacuum){
        primaryColor = OxyColors.Maroon;
      }
      Console.WriteLine("Creating roc plotmodel for " + record.roc.manifold.primarySensor.type+ " sensor with primary color " + primaryColor + " and secondary color " + secondaryColor);
      
      BAX = new LinearAxis() {
        Position = AxisPosition.Bottom,
        Minimum = record.primaryPlots[0].date.ToFileTime() - 100000,
        Maximum = record.primaryPlots[0].date.ToFileTime() - 100000,
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
        FontSize = 14,
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
