using ION.Droid.Activity.Report;
namespace ION.Droid {

	using System;
	using System.Collections.Generic;

	using Android.Views;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;
	using ION.Core.Util;

	using ION.Droid.Widgets.RecyclerViews;

	public class OverviewAdapter : SwipableRecyclerViewAdapter {

		public OverviewAdapter() {
		}

		public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
			return new OverviewViewHolder(parent);
		}

		public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
			return false;
		}

		public override Action GetViewHolderSwipeAction(int index) {
			return null;
		}

		public void SetLogs(IION ion, List<SessionResults> sessionResults) {
			// Because we want to pad out empty time spans, and show dates in a non-linear fashion, we must map the dates to
			// in index. For simplicity, we will map the dates to their index within the date lookup table.
			var dateLookupTable = new List<DateTime>();
			var map = new Dictionary<GaugeDeviceSensor, List<DeviceSensorLogs>>();

			foreach (var sr in sessionResults) {
				foreach (var dsl in sr.deviceSensorLogs) {
					// Find the gauge device sensor.
					if (!SerialNumberExtensions.IsValidSerialNumber(dsl.deviceSerialNumber)) {
						Log.E(this, "Failed to parse serial number: " + dsl.deviceSerialNumber);
						continue;
					}

					var sn = SerialNumberExtensions.ParseSerialNumber(dsl.deviceSerialNumber);
					var device = ion.deviceManager[sn] as GaugeDevice;
					if (device == null) {
						Log.E(this, "Failed to find gauge device: " + sn);
						continue;
					}
					var sensor = device[dsl.index];

					// Gather the dates and measurements.
					for (int i = 0; i < dsl.logs.Length; i++) {
						dateLookupTable.Add(dsl.logs[i].recordedDate);
					}

					List<DeviceSensorLogs> list;

					map.TryGetValue(sensor, out list);
					if (list == null) {
						list = new List<DeviceSensorLogs>();
						map[sensor] = list;
					}

					// Allocate the record contents.
					list.Add(dsl);
				}
			}

			records.Clear();

			foreach (var sensor in map.Keys) {
				records.Add(new SensorOverviewRecord(ion, sensor, map[sensor]));
			}

			NotifyDataSetChanged();
		}
	}
}

