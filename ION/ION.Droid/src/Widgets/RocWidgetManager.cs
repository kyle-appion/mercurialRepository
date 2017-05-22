namespace ION.Droid.Widgets {

	using System;
	using System.Collections.Generic;

  using Android.Content;
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

	using ION.Droid.Sensors.Properties;
	using ION.Droid.Util;

	public class RocWidgetManager {

    public Manifold manifold { get; private set; }
    public RateOfChangeSensorProperty roc { get { return manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>(); } }

    private PlotView plot;
    private Context context { get { return plot.Context; } }
    private bool showLabels;

    private PlotModel model;
    private LinearAxis xAxis;
    private MinMaxLineAxis primaryAxis;
    private MinMaxLineAxis secondaryAxis;

    private LineSeries primarySeries;
    private LineSeries secondarySeries;

		/// <summary>
		/// Used to measure the labels for the graph so that the labels can be lain out correctly.
		/// </summary>
		private CanvasRenderContext rc;

    public RocWidgetManager(Manifold manifold, PlotView plot, bool showLabels) {
      this.manifold = manifold;
      this.plot = plot;
      this.showLabels = showLabels;

			var dm = context.Resources.DisplayMetrics;
			var scale = dm.Density;
			rc = new CanvasRenderContext(scale, dm.ScaledDensity);
    }

    public void Initialize() {
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
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0,
        AxisTitleDistance = 0,
			};

			var baseUnit = manifold.primarySensor.unit.standardUnit;
			primaryAxis = new MinMaxLineAxis() {
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "first",
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
			};

			secondaryAxis = new MinMaxLineAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "second",
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
			};

      if (showLabels) {
				primaryAxis.LabelFormatter = (arg) => {
					var u = manifold.primarySensor.unit;
					var p = SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
					return p;
				};

        secondaryAxis.LabelFormatter = (arg) => {
          if (manifold.secondarySensor != null) {
            var u = manifold.secondarySensor.unit;
            return SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
          } else {
            return "";
          }
        };
        xAxis.FontSize = 15;
        primaryAxis.FontSize = 15;
        secondaryAxis.FontSize = 15;
			} else {
        xAxis.FontSize = 10;
      }

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

      // model.PlotMargins = new OxyThickness(0, -5, 0, 2 + (showLabels ? xAxis.FontSize : 10) - 10);
      model.PlotMargins = new OxyThickness(0, -7, 0, 2 + (showLabels ? xAxis.FontSize : 10) - 10);
			model.PlotType = PlotType.XY;
			model.Axes.Add(xAxis);
			model.Axes.Add(primaryAxis);
			model.Axes.Add(secondaryAxis);
			model.Series.Add(primarySeries);
			model.Series.Add(secondarySeries);
      model.PlotAreaBorderThickness = new OxyThickness(1);
      model.PlotAreaBorderColor = OxyColors.Black;
			plot.Model = model;
    }

    public void Invalidate() {
			var device = (manifold.primarySensor as GaugeDeviceSensor)?.device;
			if (device != null && device.isConnected) {
				InvalidatePrimary();
				InvalidateSecondary();
				InvalidateTime();

				var height = MeasureTextHeight(xAxis);
//				model.PlotMargins = new OxyThickness(double.NaN, double.NaN, -height, double.NaN);

				model.InvalidatePlot(true);
				plot.InvalidatePlot();
			} else {
				plot.Visibility = ViewStates.Invisible;
			}
    }

    private void InvalidateTime() {
      var axis = xAxis;

      if (roc == null) {
        return;
      }

      var points = roc.primarySensorPoints;

      if (points.Count <= 0) {
        Log.D(this, "Failed to invalidate: points.count was " + points.Count);
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
			var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			primaryAxis.IsAxisVisible = showLabels;
			if (roc == null) {
				return;
			}

			var averageChange = roc.GetPrimaryAverageRateOfChange();
			var minMax = roc.GetPrimaryMinMax();

			UpdateAxis(primaryAxis, manifold.primarySensor, minMax.min, minMax.max, manifold.primarySensor.unit, 1, 5);

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
			secondaryAxis.IsAxisVisible = showLabels;
			if (manifold.secondarySensor == null) {
				return;
			}

			var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

			if (roc == null) {
				return;
			}

			var minMax = roc.GetSecondaryMinMax();

			UpdateAxis(secondaryAxis, manifold.primarySensor, minMax.min, minMax.max, manifold.secondarySensor.unit, 1, 5);

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
		private void UpdateAxis(MinMaxLineAxis axis, Sensor sensor, Scalar min, Scalar max, Unit u, int major = 2, int minor = 5) {
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

      var determineDifference = DetermineMinimumBounds(sensor);

			minMag = u.OfScalar(minMag).ConvertTo(su).amount;
			maxMag = u.OfScalar(maxMag).ConvertTo(su).amount;
			var diff = (maxMag - minMag);

			if (minMag < sensor.minMeasurement.ConvertTo(su).amount) {
				minMag = sensor.minMeasurement.ConvertTo(su).amount;
			}

			var range = sensor.range.ConvertTo(su) * 0.05;
			if (diff < range.magnitude) {
				diff = range.magnitude;
				maxMag = minMag + diff;
			}

			var mod = diff / major;

			axis.SetMinMax(minMag, maxMag);

			axis.Minimum = minMag;
			axis.Maximum = maxMag;

			var padding = diff * 0.20;

			axis.MajorStep = mod;
			axis.MinimumMajorStep = mod;

      axis.Minimum -= padding;
			axis.Maximum += padding;
			axis.MajorTickSize = 10;
      axis.TicklineColor = OxyColors.Black;

      axis.MinimumPadding = 0.25;
      axis.MaximumPadding = 0.25;
      axis.AxislineStyle = LineStyle.Solid;
      axis.AxislineThickness = 1;
      axis.AxisTickToLabelDistance = -(MeasureTextWidth(axis) + axis.MajorTickSize + 5);
		}

    private ScalarSpan DetermineMinimumBounds(Sensor sensor) {
      if (sensor.type == ESensorType.Vacuum) {
        if (sensor.measurement <= Units.Vacuum.MICRON.OfScalar(1000)) {
          return Units.Vacuum.MICRON.OfSpan(50);
        } else if (sensor.measurement <= Units.Vacuum.MICRON.OfScalar(15000)) {
          return Units.Vacuum.MICRON.OfSpan(500);
        } else {
          return Units.Vacuum.MICRON.OfSpan(10000);
        }
      } else {
        return sensor.unit.OfSpan(sensor.range.magnitude * 0.5);
      }
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


		internal class MinMaxLineAxis : LinearAxis {
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
}
