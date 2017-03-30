namespace ION.Droid.Fragments._Analyzer {

	using System;

	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;

	public class ROCSensorPropertyRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
		public ROCSensorPropertyRecord(Manifold manifold, RateOfChangeSensorProperty sp) : base(manifold, sp, SubviewAdapter.EViewType.ROC) {
		}
	}


	public class ROCSensorPropertyViewHolder : SensorPropertyViewHolder<RateOfChangeSensorProperty> {
		private BitmapCache cache;
		private PlotView plot;
		private LineSeries mainSeries;
		private TextView title;
		private ImageView icon;
		private TextView measurement;
		private TextView unit;

		private PlotModel model;
		private LinearAxis xAxis;
		private LinearAxis yAxis;

		private Handler handler;
		private bool isRunning;

		public ROCSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_graph_small) {
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
			yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = record.manifold.primarySensor.minMeasurement.ConvertTo(baseUnit).amount,
				Maximum = record.manifold.primarySensor.maxMeasurement.ConvertTo(baseUnit).amount,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
			};

			mainSeries = new LineSeries() {
				StrokeThickness = 1,
				MarkerType = MarkerType.Circle,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
			};

			model.Axes.Add(xAxis);
			model.Axes.Add(yAxis);
			model.Series.Add(mainSeries);
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

			var roc = record.sp.GetAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
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


			var minMax = FindGraphRange();
			double diff = (minMax.Item2 - minMax.Item1) / 10;
			if (diff == 0) {
				diff = 1;
			}
			yAxis.Minimum = minMax.Item1 - diff;
			yAxis.Maximum = minMax.Item2 + diff;


			mainSeries.Points.Clear();
			var buffer = record.sp.points;
			foreach (var pp in buffer) {
				var t = record.sp.window - (buffer[0].date - pp.date);
				mainSeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
			Appion.Commons.Util.Log.D(this, "" + (record.sp.window - (buffer[0].date - buffer[buffer.Length - 1].date)));
			plot.InvalidatePlot();
			model.ResetAllAxes();
		}

		/// <summary>
		/// Returns the minimum and maximum values for the rate of change graph.
		/// </summary>
		/// <returns>The tuple contains the min and max as items 0, 1 respectively.</returns>
		private Tuple<double, double> FindGraphRange() {
			var points = record.sp.points;
			double min = double.MaxValue, max = double.MinValue;
			foreach (var dp in points) {
				if (dp.measurement < min) {
					min = dp.measurement;
				}
				if (dp.measurement > max) {
					max = dp.measurement;
				}
			}
			return new Tuple<double, double>(min, max);
		}

		private void HandleMessage(Message msg) {
			Invalidate();
			if (isRunning) {
				handler.SendEmptyMessageDelayed(0, (long)record.sp.interval.TotalMilliseconds);
			}
		}
	}
}
