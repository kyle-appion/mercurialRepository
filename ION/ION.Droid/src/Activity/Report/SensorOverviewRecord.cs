namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.Views;
	using Android.Widget;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Measure;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;

	using ION.Droid.Sensors;
	using ION.Droid.Widgets.RecyclerViews;

	public class SensorOverviewRecord : SwipableRecyclerViewAdapter.IRecord {
		public int viewType { get { return 0; } }

		public GaugeDeviceSensor sensor { get; private set; }
		public IEnumerable<DeviceSensorLogs> logs { get; private set; }

		public Scalar lowest { get; private set; }
		public Scalar highest { get; private set; }
		public Scalar average { get; private set; }

		public SensorOverviewRecord(IION ion, GaugeDeviceSensor sensor, IEnumerable<DeviceSensorLogs> logs) {
			this.sensor = sensor;
			this.logs = logs;

			var cnt = 0;
			var tot = 0.0;

			var h = double.MinValue;
			var l = double.MaxValue;

			foreach (var log in logs) {
				foreach (var sl in log.logs) {
					if (sl.measurement > h) {
						h = sl.measurement;
					}

					if (sl.measurement < l) {
						l = sl.measurement;
					}

					tot += sl.measurement;
					cnt++;
				}
			}
			var avg = tot / cnt;


			highest = sensor.unit.standardUnit.OfScalar(h).ConvertTo(ion.defaultUnits.DefaultUnitFor(sensor.type));
			lowest = sensor.unit.standardUnit.OfScalar(l).ConvertTo(ion.defaultUnits.DefaultUnitFor(sensor.type));
			average = sensor.unit.standardUnit.OfScalar(avg).ConvertTo(ion.defaultUnits.DefaultUnitFor(sensor.type));
		}
	}

	public class OverviewViewHolder : SwipableViewHolder<SensorOverviewRecord> {
		private TextView header;
		private TextView lowest;
		private TextView highest;
		private TextView average;

		public OverviewViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_data_log_overview) {
			header = view.FindViewById<TextView>(Resource.Id.device_serial_number);
			lowest = view.FindViewById<TextView>(Resource.Id.lowest);
			highest = view.FindViewById<TextView>(Resource.Id.highest);
			average = view.FindViewById<TextView>(Resource.Id.average);
		}

		public override void OnBindTo() {
			header.Text = t.sensor.device.serialNumber + " (" + t.sensor.type.GetTypeString() + ")";
			lowest.Text = SensorUtils.ToFormattedString(t.sensor.type, t.lowest);
			highest.Text = SensorUtils.ToFormattedString(t.sensor.type, t.highest);
			average.Text = SensorUtils.ToFormattedString(t.sensor.type, t.average);
		}
	}
}

