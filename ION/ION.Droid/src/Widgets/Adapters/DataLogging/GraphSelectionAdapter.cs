namespace ION.Droid.Widgets.Adapters.DataLogging {

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.Xamarin.Android;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Report.DataLogs;
  using ION.Core.Util;

  using ION.Droid.Widgets.Adapters;

  /// <summary>
  /// The adapter that will list session graphs.
  /// </summary>
  public class GraphSelectionAdapter : IONRecyclerViewAdapter {
    /// <summary>
    /// Gets the item count.
    /// </summary>
    /// <value>The item count.</value>
    public override int ItemCount {
      get {
        return records.Count;
      }
    }

    private IION ion;

    /// <summary>
    /// The records that are present in the adapter.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();

    public GraphSelectionAdapter(IION ion) {
      this.ion = ion;
    }

    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Graph:
          return new GraphViewHolder(li.Inflate(Resource.Layout.list_item_data_log, parent, false));
        default:
          throw new Exception("Cannot create view holder for: " + ((EViewType)viewType));
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var record = records[position];

      switch (record.viewType) {
        case EViewType.Graph:
          ((GraphViewHolder)holder).BindTo(ion, record as GraphRecord);
          return;
      }
    }

    /// <summary>
    /// Sets the logs that the adapter will display in the list.
    /// </summary>
    /// <param name="logs">Logs.</param>
    public void SetLogs(IEnumerable<DeviceSensorLogs> logs) {
      records.Clear();

      foreach (var l in logs) {
        records.Add(new GraphRecord(l, false));
      }

      Log.D(this, "graph adapter has: " + this.ItemCount + " items");
      NotifyDataSetChanged();
    }

    /// <summary>
    /// The abstract data object that will represent an entity displayed by the adapter.
    /// </summary>
    public interface IRecord {
      EViewType viewType { get; }
    }

    /// <summary>
    /// An enumeration that will enumerate the type of views that the adapter will show.
    /// </summary>
    public enum EViewType {
      Graph,
    }
  }

  public class GraphRecord : GraphSelectionAdapter.IRecord {
    public GraphSelectionAdapter.EViewType viewType {
      get {
        return GraphSelectionAdapter.EViewType.Graph;
      }
    }

    public DeviceSensorLogs logs;
    public bool isChecked;

    public GraphRecord(DeviceSensorLogs logs, bool isChecked) {
      this.logs = logs;
      this.isChecked = isChecked;
    }
  }

  public class GraphViewHolder : RecyclerView.ViewHolder { 
    private PlotView plot;
    private TextView title;
    private CheckBox checkBox;
    private TextView serialNumber;

    private GraphRecord record;

    public GraphViewHolder(View view) : base(view) {
      plot = view.FindViewById<PlotView>(Resource.Id.content);
      title = view.FindViewById<TextView>(Resource.Id.title);
      checkBox = view.FindViewById<CheckBox>(Resource.Id.check);
      serialNumber = view.FindViewById<TextView>(Resource.Id.device_serial_number);

      checkBox.CheckedChange += (obj, e) => {
        if (record != null) {
          record.isChecked = e.IsChecked;
        }
      };
    }

    public void BindTo(IION ion, GraphRecord record) {
      this.record = record;

      var sn = ion.database.Table<DeviceRow>().Where(dr => dr.id == record.logs.deviceId).First().serialNumber;
      var serial = SerialNumberExtensions.ParseSerialNumber(sn);
      var device = ion.deviceManager[serial] as GaugeDevice;
      var sensor = device[record.logs.index];
      var u = sensor.unit.standardUnit;

      checkBox.Checked = record.isChecked;
      serialNumber.Text = sn;

      var model = new PlotModel();
      model.Background = OxyColors.Transparent;

      var xAxis = new DateTimeAxis() {
        Position = AxisPosition.Bottom,
        Minimum = DateTimeAxis.ToDouble(record.logs.start),
        AbsoluteMinimum = DateTimeAxis.ToDouble(record.logs.start),
        Maximum = DateTimeAxis.ToDouble(record.logs.end),
        AbsoluteMaximum = DateTimeAxis.ToDouble(record.logs.end),
        IsZoomEnabled = false,
        IsPanEnabled = false,
        IsAxisVisible = false,
        MinimumPadding = 0, 
        MaximumPadding = 0,
      };

      var yAxis = new DateTimeAxis() {
        Position = AxisPosition.Left,
        Minimum = sensor.minMeasurement.ConvertTo(u).amount,
        Maximum = sensor.maxMeasurement.ConvertTo(u).amount,
      };
    }
  }
}

