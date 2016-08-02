namespace ION.Droid.Widgets.Adapters.DataLogging {

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Report.DataLogs;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Widgets.RecyclerViews;

  public class GraphOverviewAdapter : IONRecyclerViewAdapter {
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

    public GraphOverviewAdapter(IION ion) {
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
        case EViewType.Log:
          return new OverviewViewHolder(li.Inflate(Resource.Layout.list_item_data_log_overview, parent, false));
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
        case EViewType.Log:
          ((OverviewViewHolder)holder).BindTo(ion, record as OverviewRecord);
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
        records.Add(new OverviewRecord(l));
      }

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
      Log,
    }
  }

  public class OverviewRecord : GraphOverviewAdapter.IRecord {
    public GraphOverviewAdapter.EViewType viewType {
      get {
        return GraphOverviewAdapter.EViewType.Log;
      }
    }

    public DeviceSensorLogs logs;

    public OverviewRecord(DeviceSensorLogs logs) {
      this.logs = logs;
    }
  }

  public class OverviewViewHolder : RecyclerView.ViewHolder { 
    private TextView serial;
    private TextView lowest;
    private TextView highest;
    private TextView total;

    private OverviewRecord record;

    public OverviewViewHolder(View view) : base(view) {
      serial = view.FindViewById<TextView>(Resource.Id.device_serial_number);
      lowest = view.FindViewById<TextView>(Resource.Id.report_data_logging_lowest_measurement);
      highest = view.FindViewById<TextView>(Resource.Id.report_data_logging_highest_measurement);
      total = view.FindViewById<TextView>(Resource.Id.report_data_logging_measurement_count);
    }

    public void BindTo(IION ion, OverviewRecord record) {
      this.record = record;

      var ussn = ion.database.Table<DeviceRow>().Where(dr => dr.serialNumber == record.logs.deviceSerialNumber).First().serialNumber;
      var sn = SerialNumberExtensions.ParseSerialNumber(ussn);

      serial.Text = sn.ToString();

      var gauge = ion.deviceManager[sn] as GaugeDevice;
      var sensor = gauge.sensors[record.logs.index];
      var u = ion.defaultUnits.DefaultUnitFor(sensor.type);
      var l = double.MaxValue;
      var h = double.MinValue;

      foreach (var log in record.logs.logs) {
        if (log.measurement < l) {
          l = log.measurement;
        }

        if (log.measurement > h) {
          h = log.measurement;
        }
      }

      lowest.Text = "" + u.standardUnit.OfScalar(l).ConvertTo(u);
      highest.Text = "" + u.standardUnit.OfScalar(h).ConvertTo(u);
      total.Text = "" + record.logs.logs.Length;
    }
  }
}

