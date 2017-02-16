namespace ION.Core.Report.DataLogs {

	using System;
  using System.Collections.Generic;
	using System.IO;

	using Appion.Commons.Util;

	using ION.Core.App;
  using ION.Core.Database;
	using ION.Core.Devices;

	/// <summary>
	/// A small data object that holds all of the data that is used when generating a data log report.
	/// </summary>
  public class DataLogReport {

		/// <summary>
		/// The date that the report was started.
		/// </summary>
		/// <value>The jobs.</value>
		public DateTime start { get; private set; }
		/// <summary>
		/// The date that the report was ended.
		/// </summary>
		/// <value>The end.</value>
		public DateTime end { get; private set; }
		/// <summary>
		/// The jobs that are attached to the report.
		/// </summary>
		public List<JobRow> jobs { get; private set; }
		/// <summary>
		/// The devices that produced the session results.
		/// </summary>
		public List<IDevice> devices { get; private set; }
    /// <summary>
    /// The session results that are making up the current report.
    /// </summary>
    /// <value>The results.</value>
		public List<SessionResults> sessionResults { get; private set; }
		/// <summary>
		/// The Dictionary that maps sensors to their epxorted graph png image. 
		/// </summary>
		public Dictionary<GaugeDeviceSensor, Stream> graphImages { get; set; }


		/// <summary>
		/// Creates a new DataLogReport based on the given jobs and session results.
		/// </summary>
		/// <param name="jobs">Jobs.</param>
		/// <param name="sessionResults">Session results.</param>
		public DataLogReport(DateTime start, DateTime end, List<JobRow> jobs, List<IDevice> devices, List<SessionResults> sessionResults) {
			this.start = start;
			this.end = end;
			this.jobs = jobs;
			this.devices = devices;
			this.sessionResults = sessionResults;
		}

		public static DataLogReport BuildFromSessionResults(IION ion, DateTime start, DateTime end, List<SessionResults> sessionResults) {
			var jobs = new List<JobRow>();

			var deviceSet = new HashSet<IDevice>();
			foreach (var results in sessionResults) {
				foreach (var dsl in results.deviceSensorLogs) {
					if (SerialNumberExtensions.IsValidSerialNumber(dsl.deviceSerialNumber)) {
						deviceSet.Add(ion.deviceManager[SerialNumberExtensions.ParseSerialNumber(dsl.deviceSerialNumber)]);
					}
				}

				var row = ion.database.Table<SessionRow>().Where(sr => sr.SID == results.sessionId).FirstOrDefault();
				if (row != null) {
					var job = ion.database.Table<JobRow>().Where(jr => jr.JID == row.frn_JID).FirstOrDefault();
					if (job != null) {
						jobs.Add(job);
					}
				} else {
					Log.E(typeof(DataLogReport).Name, "Failed to query a session for for export");
				}
			}

			return new DataLogReport(start, end, jobs, new List<IDevice>(deviceSet), sessionResults);
		}
  }
}

