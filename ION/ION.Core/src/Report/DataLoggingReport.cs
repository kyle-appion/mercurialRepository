namespace ION.Core.Report {

  using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
  using ION.Core.IO;

  /// <summary>
  /// A Data Logging report is the simplest report that ION offers. Simply, it is
  /// a PNG with meta data that the user my provide to describe what the PNG is
  /// descibing.
  /// </summary>
  public class DataLoggingReport {

		public static async Task Create(IION ion, DateTime start, DateTime end, List<int> sessionIds) {
			var masterTimes = new HashSet<DateTime>();

			var dataSets = new List<DataLogDataSet>();
			foreach (var id in sessionIds) {
				var session = ion.database.QueryForAsync<SessionRow>(id).Result;
				var rows = ion.database.Table<SensorMeasurementRow>().Where(smr => smr.frn_SID == id &&
				                                                            smr.recordedDate >= start &&
				                                                            smr.recordedDate <= end).OrderBy(smr => smr.recordedDate);
				var dict = new Dictionary<DateTime, double>();
				foreach (var smr in rows) {
					dict[smr.recordedDate] = smr.measurement;
				}

				var dlds = new DataLogDataSet() {
					
					jobId = session.frn_JID,
					sessionId = id,
					readings = dict,
				};
				
				dataSets.Add(dlds);
      }
		}

		/// <summary>
		/// The master list of times that are applied to the report.
		/// </summary>
		private List<DateTime> masterTimesList;

		public class DataLogDataSet {
			public string name;
			public int jobId;
			public int sessionId;
			public int sensorId;
			public GaugeDeviceSensor sensor;
			public Dictionary<DateTime, double> readings;
		}
  }
}

