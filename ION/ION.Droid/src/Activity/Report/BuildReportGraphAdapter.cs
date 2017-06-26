namespace ION.Droid.Activity.Report {

  using System;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Xamarin.Android;

  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Report.DataLogs;
  using ION.Core.Sensors;

  using ION.Droid.Sensors;
	using ION.Droid.Widgets.RecyclerViews;

	public class BuildReportGraphAdapter : RecordAdapter {


    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return new ReportGraphViewHolder(parent);
    }
  }

  public class ReportGraphRecord : RecordAdapter.Record<GaugeDeviceSensor> {
    public override int viewType { get { return 1; } }

    public SensorReportEncapsulation encap;
    public bool isChecked { get; set; }

    public ReportGraphRecord(SensorReportEncapsulation encap) : base(encap.sensor) {
      this.encap = encap;
      isChecked = true;
    }
  }

  public class ReportGraphViewHolder : RecordAdapter.RecordViewHolder<ReportGraphRecord> {
    public View contentContainer;
    public PlotView plot;
    public TextView title;
    public View checkContainer;
    public CheckBox check;

    private PlotModel model;
    private LinearAxis xAxis;
    private LinearAxis yAxis;

    public ReportGraphViewHolder(ViewGroup parent) : base(parent, Resource.Layout.activity_build_report_list_item_graph) {
      contentContainer = ItemView.FindViewById(Resource.Id.content);
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

		public override void Bind() {
			base.Bind();

			model = new PlotModel() {
				Padding = new OxyThickness(0),
			};

			xAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			model.Axes.Add(xAxis);
			model.Axes.Add(yAxis);

			model.Background = OxyColors.Transparent;
			model.DefaultFontSize = 0;
			//      model.PlotAreaBorderThickness = new OxyThickness(1, 1, 1, 1);

			plot.Model = model;
		}

		public override void Invalidate() {
			var ion = AppState.context;
			var serial = record.data.device.serialNumber;
			var indices = record.encap.dil.dateSpan;
			var sensor = record.data;
			var series = record.encap.CreateSeries();

			model.Title = serial.ToString();
			model.Subtitle = sensor.type.GetTypeString();

			xAxis.Minimum = 0;
			xAxis.Maximum = indices - 1;

			yAxis.Minimum = record.encap.min;
			yAxis.Maximum = record.encap.max;

			check.Checked = record.isChecked;

			model.Series.Clear();
			foreach (var s in series) {
				var c = sensor.GetChartColor(ItemView.Context);
				s.Color = OxyColor.FromUInt32((uint)c.ToArgb());
				model.Series.Add(s);
			}
			model.InvalidatePlot(true);
		}
  }
}
