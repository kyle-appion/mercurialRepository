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
    
    public LineSeries primarySeries;
    public LineSeries secondarySeries;
    public GaugeDeviceSensor gaugeSensor;
    
    public bool actualSecondary = true;
    private bool isUpdating { get; set; }
  
    public RateofChangeSettingsViewController(IntPtr handle) : base (handle) {
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      setupSettings();
    }
    
    public async void setupSettings(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
        graphView.Layer.BorderWidth = 1f;
        
        plotView = new PlotView(new CGRect(0,0, graphView.Bounds.Width, graphView.Bounds.Height)){
          Model = CreatePlotModel(),
          BackgroundColor = UIColor.Clear,
        };    
        
        shscLabel.AdjustsFontSizeToFitWidth = true;
        secondaryToggle.On = actualSecondary;        
        secondaryToggle.ValueChanged += toggleSwitched;
        
        graphView.AddSubview(plotView);
        updateCellGraph();
    }
 
    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      var device = (initialRecord.manifold.primarySensor as GaugeDeviceSensor)?.device;
      
      if(device != null && device.isConnected){
        updateCellGraph();
      }
    }
 
    public async void updateCellGraph(){
      if(plotView == null){
        return;
      }           
      var device = (initialRecord.manifold.primarySensor as GaugeDeviceSensor)?.device;
      if (device == null || device.isConnected) {
          InvalidatePrimary();
          InvalidateSecondary();
          InvalidateTime();
  
        InvokeOnMainThread ( () => {    
          plotView.InvalidatePlot();
          plotView.Model.InvalidatePlot(true);
        });
      } else {
        plotView.Model.PlotMargins = new OxyThickness(0,double.NaN,0,0);
        return;
      }
      await Task.Delay(TimeSpan.FromMilliseconds(100));
      updateCellGraph();   
    }
  
    public void toggleSwitched(object sender, EventArgs e){    
      actualSecondary = secondaryToggle.On;
      
      Console.WriteLine("toggled switch to state: " + actualSecondary);
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

    private void InvalidatePrimary() {
      var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if(roc == null){
         return;
      }
      //if (roc == null || TLMeasurement == null || BLMeasurement == null) {
      //  return;
      //}

      var minMax = roc.GetPrimaryMinMax();
      //TLMeasurement.Text = SensorUtils.ToFormattedString(minMax.max.ConvertTo(roc.manifold.primarySensor.unit),true);
      //BLMeasurement.Text = SensorUtils.ToFormattedString(minMax.min.ConvertTo(roc.manifold.primarySensor.unit),true);
      
      UpdateAxis(LAX, minMax.min, minMax.max, initialRecord.manifold.primarySensor.unit, 1, 5);      
      
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
      if (initialRecord.manifold.secondarySensor == null) {  
        return;
      }

      var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      if(roc == null){
        return;
      }
      //if (roc == null || TRMeasurement == null || BRMeasurement == null) {
      //  return;
      //}

      var minMax = roc.GetSecondaryMinMax();
      
      //if(roc.manifold.secondarySensor != null){
      //  TRMeasurement.Text = SensorUtils.ToFormattedString(minMax.max.ConvertTo(roc.manifold.secondarySensor.unit),true);
      //  BRMeasurement.Text = SensorUtils.ToFormattedString(minMax.min.ConvertTo(roc.manifold.secondarySensor.unit),true);
      //}
      
      UpdateAxis(RAX, minMax.min, minMax.max, initialRecord.manifold.secondarySensor.unit, 1, 5);

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

      /////FIND START POINT IN PRIMARY POINTS THAT THE SECONDARY HAS STARTED FROM
      var secondStartDate = secondaryBuffer[0].date;
      var primaryIndex = 0;

      while(secondStartDate < roc.primarySensorPoints[primaryIndex].date){
        primaryIndex++;
      }
      for (int i = 0; i < secondaryBuffer.Count; i++) {
        var p = secondaryBuffer[i];
        //var s = roc.primarySensorPoints[primaryIndex];
        
        //if(secondaryToggle.On){
          
        //  secondarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), roc.manifold.ptChart.CalculateSystemTemperatureDelta(new Scalar(roc.manifold.primarySensor.unit.standardUnit,s.measurement),new Scalar(roc.manifold.secondarySensor.unit.standardUnit,p.measurement)).magnitude);
        //  Console.WriteLine("SHSC " +i+" data point X: " + secondarySeries.Points[i].X + " Y: " + secondarySeries.Points[i].Y);
        //  primaryIndex++;
        //} else {
          secondarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), p.measurement);
        //}
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
    
    public PlotModel CreatePlotModel(){    
      Console.WriteLine("Creating rocvc plotmodel");
      var model = new PlotModel();
      
      var roc = initialRecord.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      
      var primaryColor = OxyColors.Blue;
      var secondaryColor = OxyColors.Red;
      
      if(roc.manifold.primarySensor.type == ESensorType.Temperature){
        primaryColor = OxyColors.Red;
        secondaryColor = OxyColors.Blue;
      } else if (roc.manifold.primarySensor.type == ESensorType.Vacuum){
        primaryColor = OxyColors.Maroon;
      }
      
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

      var baseUnit = initialRecord.manifold.primarySensor.unit.standardUnit;
      LAX = new LinearAxis() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "first",
        LabelFormatter = (arg) => {
          var u = initialRecord.manifold.primarySensor.unit;
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
          if (initialRecord.manifold.secondarySensor != null) {
            var u = initialRecord.manifold.secondarySensor.unit;
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
        //Color = OxyColors.Red,
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

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}

