namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.Xamarin.Android;

  using Appion.Commons.Math;
  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ROCFlags = ION.Core.Sensors.Properties.RateOfChangeSensorProperty.EFlags;

  using ION.Droid.Content;
  using ION.Droid.Devices;
  using ION.Droid.Util;
  using ION.Droid.Sensors.Properties;
  using ION.Droid.Widgets.RecyclerViews;

  [Activity(Label = "@string/live_trending", ScreenOrientation = ScreenOrientation.Portrait)]
  public class RoCActivity : IONActivity {
    /// <summary>
    /// The extra that pulls a ManifoldParcelable from the launching intent.
    /// </summary>
    public const string EXTRA_MANIFOLD = "ION.Droid.Activity.Extra.MANIFOLD";

    private PlotView plot;
    private Manifold manifold;
    private RateOfChangeSensorProperty roc;

    private TextView title;

    private View content1;
    private View content2;

    private TextView text1;
    private TextView text2;

    private ImageView icon1;
    private ImageView icon2;

    private PlotModel model;
    private LinearAxis xAxis;
    private MinMaxLineSeries primaryAxis;
    private MinMaxLineSeries secondaryAxis;

    private LineSeries primarySeries;
    private LineSeries secondarySeries;

    private CanvasRenderContext rc;

    private Handler handler;

    // Overridden from IONActivity
    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);

      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_roc);

      plot = FindViewById<PlotView>(Resource.Id.graph);

      title = FindViewById<TextView>(Resource.Id.title);

      content1 = FindViewById(Resource.Id._1);
      content2 = FindViewById(Resource.Id._2);

			text1 = content1.FindViewById<TextView>(Resource.Id.text);
			text2 = content2.FindViewById<TextView>(Resource.Id.text);

			icon1 = content1.FindViewById<ImageView>(Resource.Id.icon);
      icon2 = content2.FindViewById<ImageView>(Resource.Id.icon);

      LoadManifold();

      var dm = Resources.DisplayMetrics;
      var scale = dm.Density;
      rc = new CanvasRenderContext(scale, dm.ScaledDensity);

      handler = new Handler(OnHandleMessage);
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();
      handler.SendEmptyMessageDelayed(0, 100);
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();
      handler.RemoveCallbacksAndMessages(null);
    }

    // Overridden from Activity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    private void Invalidate() {
      var device = (manifold.primarySensor as GaugeDeviceSensor)?.device;
      if (device == null || device.isConnected) {
        InvalidatePrimary();
        InvalidateSecondary();
        InvalidateTime();

        plot.InvalidatePlot();
        model.InvalidatePlot(true);
      }
    }

    private void InvalidateTime() {
      var axis = xAxis;
      var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null) {
        return;
      }

      var points = roc.primarySensorPoints;

      if (points.Count <= 0) {
        Log.D(this, "Failed to invalidate time: points.count was " + points.Count);
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
      var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null) {
        return;
      }

      var minMax = roc.GetPrimaryMinMax();

      UpdateAxis(primaryAxis, minMax.min, minMax.max, manifold.primarySensor.unit, 1, 5);

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
      if (manifold.secondarySensor == null) {
        return;
      }

      var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null) {
        return;
      }

      var minMax = roc.GetSecondaryMinMax();

      UpdateAxis(secondaryAxis, minMax.min, minMax.max, manifold.secondarySensor.unit, 1, 5);

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
    private void UpdateAxis(MinMaxLineSeries axis, Scalar min, Scalar max, Unit u, int major = 2, int minor = 5) {
      var su = u.standardUnit;

      var minMag = min.ConvertTo(u).amount;
      var maxMag = max.ConvertTo(u).amount;

      minMag = minMag.TruncateToSignifiantDigits(2);
      maxMag = maxMag.RoundToSignificantDigits(2);


      if (maxMag - minMag == 0) {
        if (minMag == 0) {
          minMag -= u.OfScalar(1).amount;
          maxMag += u.OfScalar(1).amount;
        } else {
          var del = Math.Pow(10, Math.Floor(Math.Log10(minMag)) - 1);
          minMag -= del;
          maxMag += del;
        }
      }

      minMag = u.OfScalar(minMag).ConvertTo(su).amount;
      maxMag = u.OfScalar(maxMag).ConvertTo(su).amount;
      var diff = (maxMag - minMag);
      var mod = diff / major;

      axis.SetMinMax(minMag, maxMag);

      axis.Minimum = minMag;
      axis.Maximum = maxMag;

      var padding = diff * 0.20;
      axis.Minimum -= padding;
      axis.Maximum += padding;

      axis.MajorStep = mod;
      axis.MinimumMajorStep = mod;
      axis.MajorTickSize = 10;
      axis.TicklineColor = OxyColors.Black;
/*
      axis.MinorStep = tmod;
      axis.MinimumMinorStep = tmod;
      axis.MinorTickSize = 5;
      axis.MinorTicklineColor = OxyColors.Black;
*/

      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
      axis.AxisTickToLabelDistance = -(MeasureTextWidth(axis) + axis.MajorTickSize + 5);
    }

    /// <summary>
    /// Measures the largest text size for the axis.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="axis">Axis.</param>
    private double MeasureTextWidth(LinearAxis axis) {
      IList<double> majorLabelValues = new List<double>();
      IList<double> majorTickValues = new List<double>();
      IList<double> minorTickValues = new List<double>();

      if (axis.ActualMinorStep == 0 || axis.ActualMajorStep == 0) {
        return 0;
      }

      try {
        axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);

        double bestWidth = 0;
        foreach (var label in majorLabelValues) {
          var size = rc.MeasureText(axis.LabelFormatter(label), axis.Font, axis.FontSize, axis.FontWeight);
          if (size.Width > bestWidth) {
            bestWidth = size.Width;
          }
        }

        return bestWidth;
      } catch (Exception e) {
        Log.E(this, "Failing to measure text width", e);
        return 0;
      }
    }

    /// <summary>
    /// Measures the height of the text.
    /// </summary>
    /// <returns>The text height.</returns>
    /// <param name="axis">Axis.</param>
    private double MeasureTextHeight(LinearAxis axis) {
      IList<double> majorLabelValues = new List<double>();
      IList<double> majorTickValues = new List<double>();
      IList<double> minorTickValues = new List<double>();

      if (axis.ActualMinorStep == 0 || axis.ActualMajorStep == 0) {
        return 0;
      }

      axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);

      double bestHeight = 0;
      foreach (var label in majorLabelValues) {
        var size = rc.MeasureText(axis.LabelFormatter(label), axis.Font, axis.FontSize, axis.FontWeight);
        if (size.Width > bestHeight) {
          bestHeight = size.Height;
        }
      }

      return bestHeight;
    }

    private void OnHandleMessage(Message message) {
      Invalidate();
      handler.SendEmptyMessageDelayed(0, 100);
    }

    private void LoadManifold() {
      var mp = Intent.GetParcelableExtra(EXTRA_MANIFOLD) as ManifoldParcelable;
      if (mp == null) {
        // TODO ahodder@appioninc.com: Localize
        Error("US Manifold was not passed to activity");
        Finish();
        return;
      }

      manifold = mp.Get(ion);
      if (manifold == null) {
        // TODO ahodder@appioninc.com: Localize
        Error("US Manifold did not load from parcelable");
        Finish();
        return;
      }

      // Primary sensor
      var ps = manifold.primarySensor;
      text1.Text = ps.type.GetSensorTypeName() + " " + ps.name;

      // Secondary sensor
      if (manifold.secondarySensor != null) {
        var ss = manifold.secondarySensor;
        text2.Text = ss.type.GetSensorTypeName() + " " + ss.name;
      } else {
        content2.Visibility = ViewStates.Gone;
      }

      // Title
      var values = Resources.GetStringArray(Resource.Array.preferences_device_trend_interval_values);
      var entries = Resources.GetStringArray(Resource.Array.preferences_device_trend_interval_entries);
      var interval = ion.preferences.device.rateOfChangeInterval;

      var index = -1;
      for (int i = 0; i < values.Length; i++) {
        if (values[i].Equals((int)interval.TotalMilliseconds + "")) {
          index = i;
        }        
      }

      if (index == -1) {
        Log.E(this, "Failed to find text for interval: " + interval);
      } else {
        title.Text = string.Format(GetString(Resource.String.trend_update_1arg), entries[index]);
      }

      roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      if (roc == null) {
        Error("How did you get here if the manifold doesn't have a RateOfChangeSensorProperty");
        Finish();
        return;
      }

      // Initialize the plot
      model = new PlotModel();

      xAxis = new LinearAxis() {
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

      var baseUnit = manifold.primarySensor.unit.standardUnit;
      primaryAxis = new MinMaxLineSeries() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "first",
        LabelFormatter = (arg) => {
          var u = manifold.primarySensor.unit;
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

      secondaryAxis = new MinMaxLineSeries() {
        Position = AxisPosition.Right,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "second",
        LabelFormatter = (arg) => {
          if (manifold.secondarySensor != null) {
            var u = manifold.secondarySensor.unit;
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
      model.Axes.Add(xAxis);
      model.Axes.Add(primaryAxis);
      model.Axes.Add(secondaryAxis);
      model.Series.Add(primarySeries);
      model.Series.Add(secondarySeries);
      model.PlotAreaBorderThickness = new OxyThickness(0);
      model.PlotAreaBorderColor = OxyColors.Transparent;
      plot.Model = model;
    }
  }

  internal class MinMaxLineSeries : LinearAxis {
    private double min;
    private double max;

    public void SetMinMax(double min, double max) {
      this.min = min;
      this.max = max;
    }

    protected override IList<double> CreateTickValues(double from, double to, double step, int maxTicks) {
      var list = new List<double>();
      list.Add(min);
      list.Add(max);
      return list;
    }
  }
}
