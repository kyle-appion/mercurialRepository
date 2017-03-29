namespace ION.Droid.Fragments._Workbench {

	using Android.OS;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using ION.Core.Content;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Widgets.RecyclerViews;

	public class GraphSensorPropertyRecord : SensorPropertyRecord<GraphSensorProperty> {
		public GraphSensorPropertyRecord(Manifold manifold, GraphSensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.Graph) {
		}
	}


	public class GraphSensorPropertyViewHolder : SensorPropertyViewHolder<GraphSensorProperty> {
		private PlotView plot;
		private PlotModel model;
		private LineSeries mainSeries;

		private Handler handler;
		private bool isRunning;

		public GraphSensorPropertyViewHolder(SwipeRecyclerView recyclerView) : base(recyclerView, Resource.Layout.subview_graph_large) {
			plot = this.foreground.FindViewById<PlotView>(Resource.Id.graph);
			handler = new Handler(HandleMessage);
		}

		public override void Bind() {
			base.Bind();
			isRunning = true;

			model = new PlotModel() {
				Padding = new OxyThickness(3),
			};

			var xAxis = new LinearAxis() {
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
			var yAxis = new LinearAxis() {
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

			mainSeries.Points.Clear();
			var buffer = record.sp.points;
			foreach (var pp in buffer) {
				var t = record.sp.window - (buffer[0].date - pp.date);
				mainSeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
			Appion.Commons.Util.Log.D(this, "" + (record.sp.window - (buffer[0].date - buffer[buffer.Length - 1].date)));
			plot.InvalidatePlot();
		}

		private void HandleMessage(Message msg) {
			Invalidate();
			if (isRunning) {
				handler.SendEmptyMessageDelayed(0, (long)record.sp.interval.TotalMilliseconds);
			}
		}
	}
}
