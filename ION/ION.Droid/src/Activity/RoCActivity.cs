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

  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ROCFlags = ION.Core.Sensors.Properties.RateOfChangeSensorProperty.EFlags;

  using ION.Droid.Content;
  using ION.Droid.Devices;

  [Activity(Label = "US RoC", ScreenOrientation = ScreenOrientation.Portrait)]
  public class RoCActivity : IONActivity {
    /// <summary>
    /// The extra that pulls a ManifoldParcelable from the launching intent.
    /// </summary>
    public const string EXTRA_MANIFOLD = "ION.Droid.Activity.Extra.MANIFOLD";

    private PlotView plot;
    private Manifold manifold;
    private RateOfChangeSensorProperty roc;

    private View content1;
    private View content2;

    private ImageView icon1;
    private ImageView icon2;

    private CheckBox check1;
    private CheckBox check2;

    private PlotModel model;
    private LinearAxis xAxis;
    private LinearAxis primaryAxis;
    private LinearAxis secondaryAxis;

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

      content1 = FindViewById(Resource.Id._1);
      content2 = FindViewById(Resource.Id._2);

      icon1 = content1.FindViewById<ImageView>(Resource.Id.icon);
      icon2 = content2.FindViewById<ImageView>(Resource.Id.icon);

      check1 = content1.FindViewById<CheckBox>(Resource.Id.checkbox);
      check2 = content2.FindViewById<CheckBox>(Resource.Id.checkbox);

      LoadManifold();
      InitViews();

      check1.CheckedChange += (obj, args) => {
        primarySeries.IsVisible = roc.ToggleFlags(ROCFlags.ShowPrimary);
      };

      check2.CheckedChange += (obj, args) => {
        secondarySeries.IsVisible = roc.ToggleFlags(ROCFlags.ShowSecondary);
      };

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

      var points = roc.primarySensorPoints;
      var startTime = points[0];
      var endTime = points[points.Length - 1].date;

      axis.Minimum = (startTime.date - roc.window).ToFileTime() - 1000000;
      axis.Maximum = startTime.date.ToFileTime() + 1000000;

      axis.MajorStep = (long)((roc.window.TotalMilliseconds * 1e4) / 2);
      axis.MinorStep = axis.MajorStep / 5;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
    }

    private void InvalidatePrimary() {
      var averageChange = roc.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(2), TimeSpan.FromMinutes(1));

      var minMax = roc.GetPrimaryMinMax();

      UpdateAxis(primaryAxis, minMax.min, minMax.max, manifold.primarySensor.unit);

      primarySeries.Points.Clear();
      var primaryBuffer = roc.primarySensorPoints;
      foreach (var pp in primaryBuffer) {
        var t = pp.date.ToFileTime();
        primarySeries.Points.Add(new DataPoint(t, pp.measurement));
      }
    }

    private void InvalidateSecondary() {
      if (manifold.secondarySensor == null) {
        return;
      }

      var minMax = roc.GetSecondaryMinMax();

      UpdateAxis(secondaryAxis, minMax.min, minMax.max, manifold.secondarySensor.unit);

      secondarySeries.Points.Clear();
      var secondaryBuffer = roc.secondarySensorPoints;
      foreach (var pp in secondaryBuffer) {
        var t = pp.date.ToFileTime();
        secondarySeries.Points.Add(new DataPoint(t, pp.measurement));
      }
    }

    /// <summary>
    /// Updates the axis to the given state.
    /// </summary>
    /// <param name="axis">Axis.</param>
    private void UpdateAxis(LinearAxis axis, Scalar min, Scalar max, Unit u, int major = 3, int minor = 5) {
      var su = u.standardUnit;
      var diff = (max - min).ConvertTo(su).magnitude;

      if (diff != 0) {
        axis.Minimum = min.ConvertTo(su).amount;
        axis.Maximum = max.ConvertTo(su).amount;
      } else {
        var one = u.OfScalar(1);
        axis.Minimum = (min - one).ConvertTo(su).magnitude;
        axis.Maximum = (max + one).ConvertTo(su).magnitude;
        diff = u.OfScalar(3).ConvertTo(su).amount;
      }

      var mod = (int)diff / major;
      var tmod = mod / (double)minor;

      axis.MajorStep = mod;
      axis.MinimumMajorStep = mod;
      axis.MajorTickSize = 10;
      axis.TicklineColor = OxyColors.Black;

      axis.MinorStep = tmod;
      axis.MinimumMinorStep = tmod;
      axis.MinorTickSize = 5;
      axis.MinorTicklineColor = OxyColors.Black;

      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
      axis.AxisTickToLabelDistance = -(MeasureText(axis) + axis.MajorTickSize + 5);
    }

    /// <summary>
    /// Measures the largest text size for the axis.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="axis">Axis.</param>
    private double MeasureText(LinearAxis axis) {
      IList<double> majorLabelValues = new List<double>();
      IList<double> majorTickValues = new List<double>();
      IList<double> minorTickValues = new List<double>();
      axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);

      double bestWidth = 0;
      foreach (var label in majorLabelValues) {
        var size = rc.MeasureText(axis.LabelFormatter(label), axis.Font, axis.FontSize, axis.FontWeight);
        if (size.Width > bestWidth) {
          bestWidth = size.Width;
        }
      }

      return bestWidth;
    }

    private void OnHandleMessage(Message message) {
      Invalidate();
      handler.SendEmptyMessageDelayed(0, 100);
    }

    private void InitViews() {
      primarySeries.IsVisible = check1.Checked = roc.HasFlag(ROCFlags.ShowPrimary);
      secondarySeries.IsVisible = check2.Checked = roc.HasFlag(ROCFlags.ShowSecondary);

      check1.Text = manifold.primarySensor.type.GetSensorTypeName();

      if (manifold.secondarySensor != null) {
        check2.Text = manifold.secondarySensor.type.GetSensorTypeName();
      }
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

      roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      if (roc == null) {
        Error("How did you get here if the manifold doesn't have a RateOfChangeSensorProperty");
        Finish();
        return;
      }

      // Initialize the plot
      model = new PlotModel() {
        Padding = new OxyThickness(5),
      };

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
      };

      var baseUnit = manifold.primarySensor.unit.standardUnit;
      primaryAxis = new LinearAxis() {
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
      };

      secondaryAxis = new LinearAxis() {
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

      //      model.Title = "RoC";
      model.PlotType = PlotType.XY;
      model.Axes.Add(xAxis);
      model.Axes.Add(primaryAxis);
      model.Axes.Add(secondaryAxis);
      model.Series.Add(primarySeries);
      model.Series.Add(secondarySeries);
      //			model.DefaultFontSize = 0;
      //			model.PlotAreaBorderThickness = new OxyThickness(1, 1, 1, 1);
      plot.Model = model;
    }
  }
}
