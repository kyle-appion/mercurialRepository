namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Measure;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;

	using ION.Droid.Sensors;
	using ION.Droid.Widgets.RecyclerViews;

	public class SensorOverviewRecord : RecordAdapter.Record<GaugeDeviceSensor> {
		public override int viewType { get { return 0; } }

    public SensorReportEncapsulation encap { get; private set; }

		public Scalar lowest { get; private set; }
		public Scalar highest { get; private set; }
		public Scalar average { get; private set; }

    public SensorOverviewRecord(SensorReportEncapsulation encap) : base(encap.sensor) {
			this.encap = encap;

			var cnt = 0;
			var tot = 0.0;

			var h = double.MinValue;
			var l = double.MaxValue;

      foreach (var sr in encap.pointSeries) {
        foreach (var meas in sr.measurements) {
          if (meas > h) {
            h = meas;
          }

          if (meas < l) {
            l = meas;  
          }

          tot += meas;
          cnt++;
        }
			}

			var avg = tot / cnt;
      var sensor = encap.sensor;
      highest = sensor.unit.standardUnit.OfScalar(h);
      lowest = sensor.unit.standardUnit.OfScalar(l);
      average = sensor.unit.standardUnit.OfScalar(avg);
		}
	}

	public class OverviewViewHolder : RecordAdapter.RecordViewHolder<SensorOverviewRecord> {
		private TextView header;
		private TextView lowest;
		private TextView highest;
		private TextView average;

		public OverviewViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_data_log_overview) {
			header = ItemView.FindViewById<TextView>(Resource.Id.device_serial_number);
			lowest = ItemView.FindViewById<TextView>(Resource.Id.lowest);
			highest = ItemView.FindViewById<TextView>(Resource.Id.highest);
			average = ItemView.FindViewById<TextView>(Resource.Id.average);
		}

		public override void Invalidate() {
			header.Text = record.data.device.serialNumber + " (" + record.data.type.GetTypeString() + ")";

      var ion = AppState.context;
      var u = ion.preferences.units.DefaultUnitFor(record.encap.sensor.type);
      lowest.Text = SensorUtils.ToFormattedString(record.lowest.ConvertTo(u));
      highest.Text = SensorUtils.ToFormattedString(record.highest.ConvertTo(u));
      average.Text = SensorUtils.ToFormattedString(record.average.ConvertTo(u));
		}
	}
}

