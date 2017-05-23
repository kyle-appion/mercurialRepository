namespace ION.Droid.Fragments._Workbench {

	using System;
  using System.Collections.Generic;

	using Android.OS;
	using Android.Widget;
  using Android.Views;

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

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;

	public class ROCSensorPropertyRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
		public ROCSensorPropertyRecord(Manifold manifold, RateOfChangeSensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.ROC) {
		}
	}

	public class ROCSensorPropertyViewHolder : SensorPropertyViewHolder<RateOfChangeSensorProperty> {
		private BitmapCache cache;
		private PlotView plot;
		private TextView title;
		private ImageView icon;
		private TextView measurement;
		private TextView unit;

		private PlotModel model;
		private LinearAxis xAxis;
    private MinMaxLineSeries primaryAxis;
    private MinMaxLineSeries secondaryAxis;

		private LineSeries primarySeries;
		private LineSeries secondarySeries;

    /// <summary>
    /// Used to measure the labels for the graph so that the labels can be lain out correctly.
    /// </summary>
    private CanvasRenderContext rc;

		private Handler handler;
		private bool isRunning;

		public ROCSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_graph_large) {
			this.cache = cache;
			plot = this.foreground.FindViewById<PlotView>(Resource.Id.graph);
			handler = new Handler(HandleMessage);
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);

      var dm = recyclerView.Context.Resources.DisplayMetrics;
      var scale = dm.Density;
      rc = new CanvasRenderContext(scale, dm.ScaledDensity);
		}

		public override void Bind() {
			base.Bind();
			isRunning = true;

      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      // Initialize the plot
      model = new PlotModel();

      xAxis = new LinearAxis() {
        Position = AxisPosition.Bottom,
        Minimum = 0,
        Maximum = roc.window.TotalMilliseconds,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 0,
        MaximumPadding = 0,
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
        AxisTickToLabelDistance = 0,
      };
 //     xAxis.AxisTickToLabelDistance = -1000; //-(MeasureTextHeight(xAxis) + xAxis.majorTickSize + 5);
			//       axis.AxisTickToLabelDistance = -(MeasureTextWidth(axis) + axis.MajorTickSize + 5);

			var baseUnit = record.manifold.primarySensor.unit.standardUnit;
      primaryAxis = new MinMaxLineSeries() {
        Position = AxisPosition.Left,
        Minimum = 0,
        Maximum = 100,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        Key = "first",
/*
        LabelFormatter = (arg) => {
          var u = record.manifold.primarySensor.unit;
          var p = SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
          return p;
        },
*/
        Font = model.DefaultFont,
        FontSize = 7,
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
/*  
        LabelFormatter = (arg) => {
          if (record.manifold.secondarySensor != null) {
            var u = record.manifold.secondarySensor.unit;
            return SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
          } else {
            return "";
          }
        },
*/
        Font = model.DefaultFont,
        FontSize = 7,
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

      model.PlotMargins = new OxyThickness(-7, -7, -7, 2);
      model.PlotType = PlotType.XY;
      model.Axes.Add(xAxis);
      model.Axes.Add(primaryAxis);
      model.Axes.Add(secondaryAxis);
      model.Series.Add(primarySeries);
      model.Series.Add(secondarySeries);
      model.PlotAreaBorderThickness = new OxyThickness(0);
      model.PlotAreaBorderColor = OxyColors.Transparent;
      plot.Model = model;

			handler.SendEmptyMessageDelayed(0, 500);
		}

		public override void Unbind() {
			base.Unbind();
			isRunning = false;
			handler.RemoveCallbacksAndMessages(null);
		}

		public override void Invalidate() {
			base.Invalidate();

			var c = ItemView.Context;

			title.Text = record.sp.GetLocalizedStringAbreviation(c);

			var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
			if (device != null && device.isConnected) {
        plot.Visibility = ViewStates.Visible;

				InvalidatePrimary();
				InvalidateSecondary();
        InvalidateTime();

				plot.InvalidatePlot();
				model.InvalidatePlot(true);
			} else {
//				measurement.SetText(Resource.String.na);
				measurement.Text = "";
				plot.Visibility = ViewStates.Invisible;
			}
		}

    private void InvalidateTime() {
      var axis = xAxis;
      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

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
      axis.TickStyle = TickStyle.None;
      axis.AxislineThickness = 1;
    }

    private void InvalidatePrimary() {
      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null) {
        return;
      }

      var averageChange = roc.GetPrimaryAverageRateOfChange();
      var c = title.Context;

      var amount = Math.Abs(averageChange.magnitude);
      if (amount == 0) {
        measurement.Text = c.GetString(Resource.String.stable);
        unit.Visibility = ViewStates.Invisible;
      } else {
        var dmax = record.sp.sensor.maxMeasurement.amount / 10;
        if (amount > dmax) {
          measurement.Text = "> " + SensorUtils.ToFormattedString(averageChange.unit.OfScalar(dmax));
        } else {
          measurement.Text = SensorUtils.ToFormattedString(averageChange.unit.OfScalar(amount));
        }
        unit.Visibility = ViewStates.Visible;
        unit.Text = c.GetString(Resource.String.time_minute_abrv);
      }

      var dir = Math.Sign(averageChange.magnitude);
      if (averageChange.magnitude == 0) {
        icon.Visibility = ViewStates.Invisible;
      } else if (dir == 1) {
        icon.Visibility = ViewStates.Visible;
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trendup));
      } else {
        icon.Visibility = ViewStates.Visible;
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trenddown));
      }

      var minMax = roc.GetPrimaryMinMax();

      UpdateAxis(primaryAxis, minMax.min, minMax.max, record.manifold.primarySensor.unit, 1, 5);

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

      UpdateAxis(secondaryAxis, minMax.min, minMax.max, record.manifold.secondarySensor.unit, 1, 5);

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
      axis.TickStyle = TickStyle.None;
//      axis.MajorTickSize = 10;
//      axis.TicklineColor = OxyColors.Black;

//      axis.MinimumPadding = 0.25;
//      axis.MaximumPadding = 0.25;
//      axis.AxislineStyle = LineStyle.Solid;
//      axis.AxislineThickness = 1;
//      axis.AxisTickToLabelDistance = -(MeasureTextWidth(axis) + axis.MajorTickSize + 5);
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

		private void HandleMessage(Message msg) {
			Invalidate();
			if (isRunning) {
				handler.SendEmptyMessageDelayed(0, (long)record.sp.interval.TotalMilliseconds);
			}
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
