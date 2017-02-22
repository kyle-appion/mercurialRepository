namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.Support.V7.Widget;
	using Android.Views;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;

	using ION.Droid.Widgets.RecyclerViews;

	public class GraphRecordAdapter : RecordAdapter {
		
		public DateIndexLookup dil;
		private List<SessionResults> sessionResults;

		public GraphRecordAdapter() {
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			switch ((EViewType)viewType) {
				case EViewType.Graph:
					return new GraphViewHolder(parent, Resource.Layout.list_item_data_log_graph);
				default:
					throw new Exception("Cannot create view for: " + (EViewType)viewType);
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			if (holder is RecordViewHolder) {
				((RecordViewHolder)holder).data = records[position] as Record;
			}
		}

		public List<SessionResults> GatherSelectedLogs(float leftPercent, float rightPercent) {
			var ret = new List<SessionResults>();

			var startTime = FindDateTimeFromSelection(leftPercent);
			var endTime = FindDateTimeFromSelection(rightPercent);

			foreach (var sr in sessionResults) {
				var nsr = sr.SubSet(startTime, endTime);
				if (!nsr.isEmpty) {
					var ion = AppState.context;
					var sensors = GetCheckedSensors();

					for (int i = sr.deviceSensorLogs.Count - 1; i >= 0; i--) {
						var dsl = sr.deviceSensorLogs[i];
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

						if (!sensors.Contains(sensor)) {
							sr.deviceSensorLogs.RemoveAt(i);
						}
					}

					if (sr.deviceSensorLogs.Count > 0) {
						ret.Add(sr);
					}
				}
			}

			return ret;
		}

		/// <summary>
		/// Queries the index of the given date.
		/// </summary>
		/// <returns>The of date time.</returns>
		/// <param name="date">Date.</param>
		public int IndexOfDateTime(DateTime date) {
			return dil.IndexOfDate(date);
		}

		/// <summary>
		/// Finds the date time
		/// </summary>
		/// <returns>The date time from selection.</returns>
		/// <param name="percent">Percent.</param>
		public DateTime FindDateTimeFromSelection(float percent) {
			var index = (int)(percent * (dil.dateSpan - 1));
			if (index >= dil.dateSpan) {
				return DateTime.FromFileTime(0);
			} else {
				var ret = dil.DateFromIndex(index);
				return ret;
			}
		}

		/// <summary>
		/// Queries the percent within the date spand that the given date is
		/// </summary>
		/// <returns>The percent from date time.</returns>
		/// <param name="date">Date.</param>
		public float FindPercentFromDateTime(DateTime date) {
			var i = dil.IndexOfDate(date);
			return i / (float)dil.dateSpan; 
		}

		/// <summary>
		/// Queries a list of dates within the DateIndexLookup that are before the given date.
		/// </summary>
		/// <returns>The dates preceding.</returns>
		public List<DateTime> GetDatesInRange(DateTime first, DateTime last) {
			var fi = dil.IndexOfDate(first);
			var li = dil.IndexOfDate(last);

			return GetDatesInRange(fi, li);
		}

		/// <summary>
		/// Queries the dates that are inclussively between startIndex and endIndex.
		/// </summary>
		/// <returns>The dates in range.</returns>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		public List<DateTime> GetDatesInRange(int startIndex, int endIndex) {
			var ret = new List<DateTime>();

			for (int i = startIndex; i <= endIndex; i++) {
				ret.Add(dil.DateFromIndex(i));
			}

			return ret;
		}

		public List<Sensor> GetCheckedSensors() {
			var ret = new List<Sensor>();

			foreach (var record in records) {
				var gr = record as GraphRecord;
				if (gr != null && gr.isChecked) {
					ret.Add(gr.data);
				}
			}

			return ret;
		}

		public void SetRecords(IION ion, List<SessionResults> sessionResults) {
			// Because we want to pad out empty time spans, and show dates in a non-linear fashion, we must map the dates to
			// in index. For simplicity, we will map the dates to their index within the date lookup table.
			var dateLookupTable = new List<DateTime>();
			var map = new Dictionary<GaugeDeviceSensor, List<GraphRecord.PointSeries>>();

			this.sessionResults = sessionResults;

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

					List<GraphRecord.PointSeries> list;

					map.TryGetValue(sensor, out list);
					if (list == null) {
						list = new List<GraphRecord.PointSeries>();
						map[sensor] = list;
					}

					// Allocate the record contents.
					list.Add(new GraphRecord.PointSeries(dsl));
				}
			}

			records.Clear();

			dil = new DateIndexLookup(dateLookupTable);

			foreach (var sensor in map.Keys) {
				records.Add(new GraphRecord(sensor, dil, map[sensor].ToArray()));
			}

			NotifyDataSetChanged();
		}
	}
}

