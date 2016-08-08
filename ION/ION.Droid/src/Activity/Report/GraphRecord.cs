namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Measure;
	using ION.Core.Report.DataLogs;
	using ION.Core.Util;

	using ION.Droid.Sensors;
	using ION.Droid.Util;
	using ION.Droid.Widgets.RecyclerViews;

	public class GraphRecord : SwipableRecyclerViewAdapter.IRecord {
		public int viewType { get { return (int)EViewType.Graph; } }

		public GaugeDeviceSensor sensor { get; private set; }
		public DateIndexLookup dil { get; private set; }
		public PointSeries[] pointSeries { get; private set; }

		public LineSeries[] series { get; private set; }
		public bool isChecked { get; set; }
		
		public GraphRecord(GaugeDeviceSensor sensor, DateIndexLookup dil, PointSeries[] pointSeries) {
			this.sensor = sensor;
			this.dil = dil;
			this.pointSeries = pointSeries;
			isChecked = false;

			var list = new List<LineSeries>();

			foreach (var s in pointSeries) {
				var series = new LineSeries {
					StrokeThickness = 1,
					MarkerType = MarkerType.Circle,
					MarkerSize = 0,
					MarkerStroke = OxyColors.Transparent,
					MarkerStrokeThickness = 0,
				};

				for (var i = 0; i < s.length; i++) {
					series.Points.Add(new DataPoint(dil.IndexOfDate(s.times[i]), s.measurements[i]));
				}

				Log.D(this, "Series has: " + series.Points.Count + " points");

				list.Add(series);
			}

			series = list.ToArray();
		}

		public class PointSeries {
			public DeviceSensorLogs logs;
			/// <summary>
			/// The times that the measurements are mapped to.
			/// </summary>
			public DateTime[] times;
			public double[] measurements;

			public int length {
				get {
					return Math.Min(times.Length, measurements.Length);
				}
			}
				
			public PointSeries(DeviceSensorLogs logs) {
				this.logs = logs;

				var len = logs.logs.Length;
				times = new DateTime[len];
				measurements = new double[len];

				for (int i = 0; i < len; i++) {
					var sl = logs.logs[i];
					times[i] = sl.recordedDate;
					measurements[i] = sl.measurement;
				}
			}
		}
	}

	public class GraphViewHolder : SwipableViewHolder<GraphRecord> {
		private PlotView plot;
		private TextView title;
		private CheckBox check;

		public GraphViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
			plot = view.FindViewById<PlotView>(Resource.Id.graph);
			title = view.FindViewById<TextView>(Resource.Id.name);
			check = view.FindViewById<CheckBox>(Resource.Id.check);
		}

		public override void OnBindTo() {
			var ion = AppState.context;
			var serial = t.sensor.device.serialNumber;

			check.Checked = t.isChecked;

			var sensor = t.sensor;
			var u = sensor.unit.standardUnit;

			foreach (var series in t.series) {
				var c = t.sensor.GetChartColor(view.Context);

				series.Color = OxyColor.FromUInt32((uint)c.ToArgb());
			}

			var model = new PlotModel() {
				Title = serial.ToString(),
				Subtitle = sensor.type.GetTypeString(),
				Padding = new OxyThickness(0),
			};

			var indices = t.dil.dateSpan;

			var xAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = indices - 1,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			var standardUnit = t.sensor.unit.standardUnit;
			var yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = t.sensor.minMeasurement.ConvertTo(standardUnit).amount,
				Maximum = t.sensor.maxMeasurement.ConvertTo(standardUnit).amount,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3, 
				MaximumPadding = 3,
			};

			var plots = new List<LineSeries>();

			foreach (var s in t.series) {
				plots.Add(s);
			}

			model.Axes.Add(xAxis);
			model.Axes.Add(yAxis);
			foreach (var series in plots) {
				model.Series.Add(series);
			}
			model.Background = OxyColors.Transparent;
			model.DefaultFontSize = 0;
			model.PlotAreaBorderThickness = new OxyThickness(1, 1, 1, 1);

			plot.Model = model;
		}
	}
}

