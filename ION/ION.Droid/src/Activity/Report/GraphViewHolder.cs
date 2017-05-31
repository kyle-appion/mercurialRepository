namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;

	using ION.Droid.Sensors;
	using ION.Droid.Util;
	using ION.Droid.Widgets.RecyclerViews;

	public class GraphRecord : RecordAdapter.Record<GaugeDeviceSensor> {
		public override int viewType { get { return (int)EViewType.Graph; } }

		public DateIndexLookup dil { get; private set; }
		public PointSeries[] pointSeries { get; private set; }

		public double min { get; private set; }
		public double max { get; private set; }

		public LineSeries[] series { get; private set; }
		public bool isChecked { get; set; }
		
		public GraphRecord(GaugeDeviceSensor sensor, DateIndexLookup dil, PointSeries[] pointSeries) : base(sensor) {
			this.dil = dil;
			this.pointSeries = pointSeries;
			isChecked = true;

			var list = new List<LineSeries>();

			foreach (var s in pointSeries) {
				var lineSeries = new LineSeries {
					StrokeThickness = 1,
					MarkerType = MarkerType.Circle,
					MarkerSize = 0,
					MarkerStroke = OxyColors.Transparent,
					MarkerStrokeThickness = 0,
				};

				min = double.MaxValue;
				max = double.MinValue;

				for (var i = 0; i < s.length; i++) {
					var meas = s.measurements[i];
					lineSeries.Points.Add(new DataPoint(dil.IndexOfDate(s.times[i]), meas));
					if (min > meas) {
						min = meas;
					}

					if (max < meas) {
						max = meas;
					}
				}

				var span = sensor.maxMeasurement.ConvertTo(sensor.unit.standardUnit).amount * 0.05;
				min -= span;
				max += span;

				Log.D(this, "Series has: " + lineSeries.Points.Count + " points");

				list.Add(lineSeries);
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

	public class GraphViewHolder : RecordAdapter.RecordViewHolder<GraphRecord> {
		public PlotView plot;
		public TextView title;
    public View checkContainer;
		public CheckBox check;

		public GraphViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
			plot = ItemView.FindViewById<PlotView>(Resource.Id.graph);
			title = ItemView.FindViewById<TextView>(Resource.Id.name);
      checkContainer = ItemView.FindViewById(Resource.Id.view);
			check = ItemView.FindViewById<CheckBox>(Resource.Id.check);

			check.CheckedChange += (sender, e) => {
				if (record != null) {
					record.isChecked = e.IsChecked;
				}
			};
		}

		public override void Invalidate() {
			var ion = AppState.context;
			var serial = record.data.device.serialNumber;

			check.Checked = record.isChecked;

			var sensor = record.data;
			var u = sensor.unit.standardUnit;

			foreach (var series in record.series) {
				var c = sensor.GetChartColor(ItemView.Context);

				series.Color = OxyColor.FromUInt32((uint)c.ToArgb());
			}

			var model = new PlotModel() {
				Title = serial.ToString(),
				Subtitle = sensor.type.GetTypeString(),
				Padding = new OxyThickness(0),
			};

			var indices = record.dil.dateSpan;

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

			var standardUnit = record.data.unit.standardUnit;
			var yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = record.min,//t.sensor.minMeasurement.ConvertTo(standardUnit).amount,
				Maximum = record.max,//t.sensor.maxMeasurement.ConvertTo(standardUnit).amount,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3, 
				MaximumPadding = 3,
			};

			var plots = new List<LineSeries>();

			foreach (var s in record.series) {
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

