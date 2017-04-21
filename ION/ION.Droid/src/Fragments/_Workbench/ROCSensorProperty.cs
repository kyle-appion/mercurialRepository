
namespace ION.Droid.Fragments._Workbench {

	using System;

	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using Appion.Commons.Measure;

	using ION.Core.Content;
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
		private LinearAxis primaryAxis;
		private LinearAxis secondaryAxis;

		private LineSeries primarySensorSeries;
		private LineSeries secondarySensorSeries;


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
		}

		public override void Bind() {
			base.Bind();
			isRunning = true;

			model = new PlotModel() {
				Padding = new OxyThickness(3),
			};

			xAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = record.sp.window.TotalMilliseconds,

				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			var baseUnit = record.manifold.primarySensor.unit.standardUnit;
			primaryAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "first",
			};

			secondaryAxis = new LinearAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "second",
			};

			primarySensorSeries = new LineSeries() {
				StrokeThickness = 1,
				MarkerType = MarkerType.Circle,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "first",
			};

			secondarySensorSeries = new LineSeries() {
				StrokeThickness = 1,
				MarkerType = MarkerType.Circle,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "second",
			};

			model.Axes.Add(xAxis);
			model.Axes.Add(primaryAxis);
			model.Axes.Add(secondaryAxis);
			model.Series.Add(primarySensorSeries);
			model.Series.Add(secondarySensorSeries);
			model.DefaultFontSize = 0;
			model.PlotAreaBorderThickness = new OxyThickness(1, 1, 1, 1);
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

			InvalidatePrimary();
			InvalidateSecondary();

			plot.InvalidatePlot();
			model.InvalidatePlot(true);
		}

		private void InvalidatePrimary() {
			var c = title.Context;
			var roc = record.sp.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(2), TimeSpan.FromMinutes(1));
			var amount = Math.Abs(roc.amount);
			if (amount == 0) {
				measurement.Text = c.GetString(Resource.String.stable);
				unit.Visibility = ViewStates.Invisible;
			} else {
				var dmax = record.sp.sensor.maxMeasurement.amount / 10;
				if (amount > dmax) {
					measurement.Text = "> " + SensorUtils.ToFormattedString(roc.unit.OfScalar(dmax));
				} else {
					measurement.Text = SensorUtils.ToFormattedString(roc.unit.OfScalar(amount));
				}
				unit.Visibility = ViewStates.Visible;
				unit.Text = c.GetString(Resource.String.time_minute_abrv);
			}

			var dir = Math.Sign(roc.amount);
			if (roc.amount == 0) {
				icon.Visibility = ViewStates.Invisible;
			} else if (dir == 1) {
				icon.Visibility = ViewStates.Visible;
				icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trendup));
			} else {
				icon.Visibility = ViewStates.Visible;
				icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trenddown));
			}


			var primaryMinMax = record.sp.GetPrimaryMinMax();
			double diff = primaryMinMax.diff / 10;
			if (diff == 0) {
				diff = 1;
			}
			primaryAxis.Minimum = primaryMinMax.min.amount - diff;
			primaryAxis.Maximum = primaryMinMax.max.amount + diff;

			primarySensorSeries.Points.Clear();
			var primaryBuffer = record.sp.primarySensorPoints;
			foreach (var pp in primaryBuffer) {
				var t = record.sp.window - (primaryBuffer[0].date - pp.date);
				primarySensorSeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void InvalidateSecondary() {
			if (record.manifold.secondarySensor == null) {
				return;
			}

			var minMax = record.sp.GetSecondaryMinMax();
			double diff = minMax.diff / 10;
			if (diff == 0) {
				diff = 1;
			}

			secondaryAxis.Minimum = minMax.min.amount - diff;
			secondaryAxis.Maximum = minMax.max.amount + diff;

			secondarySensorSeries.Points.Clear();
			var secondaryBuffer = record.sp.secondarySensorPoints;
			foreach (var pp in secondaryBuffer) {
				var t = record.sp.window - (secondaryBuffer[0].date - pp.date);
				secondarySensorSeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void HandleMessage(Message msg) {
			Invalidate();
			if (isRunning) {
				handler.SendEmptyMessageDelayed(0, (long)record.sp.interval.TotalMilliseconds);
			}
		}
	}
}
