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
    /// The name for the data log report.
    /// </summary>
    /// <value>The name of the report.</value>
    public string reportName { get; private set; }
    /// <summary>
    /// The localization object for the report.
    /// </summary>
    /// <value>The localization.</value>
    public ILocalization localization { get; private set; }
    /// <summary>
    /// The bytes that make up the appion logo.
    /// </summary>
    /// <value>The appion logo png.</value>
    public byte[] appionLogoPng { get; private set; }
		/// <summary>
		/// The Dictionary that maps sensors to their exported graph png image. 
		/// </summary>
		public Dictionary<GaugeDeviceSensor, byte[]> graphImages { get; set; }
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
		/// The sensors that were used to make up the report.
		/// </summary>
		/// <value>The sensors.</value>
		public List<GaugeDeviceSensor> sensors { get; private set; }
    /// <summary>
    /// The session results that are making up the current report.
    /// </summary>
    /// <value>The results.</value>
		public List<SessionResults> sessionResults { get; private set; }

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
			var sensorSet = new HashSet<GaugeDeviceSensor>();
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

    /// <summary>
    /// The localization object that provides localized strings for the report.
    /// </summary>
    public interface ILocalization {
      /// <summary>
      /// The localized string for "SerialNumber" used in section headers.
      /// </summary>
      /// <value>The serial number.</value>
      string serialNumber { get; }
      /// <summary>
      /// The localized string for "Name" used for device names in section headers.
      /// </summary>
      /// <value>The name.</value>
      string name { get; }
      /// <summary>
      /// The localized string for "Certificated Date" used for the last time a device was certified in section headers.
      /// </summary>
      /// <value>The certification date.</value>
      string certificationDate { get; }
      /// <summary>
      /// The localized string for "Device Model" used in section headers.
      /// </summary>
      /// <value>The device model.</value>
      string deviceModel { get; }
      /// <summary>
      /// The localized string for "Report Created" used in section headers.
      /// </summary>
      /// <value>The report created.</value>
      string reportCreated { get; }
      /// <summary>
      /// The localized string for "Report Dates" used in section headers.
      /// </summary>
      /// <value>The report dates.</value>
      string reportDates { get; }
      /// <summary>
      /// The localized string for "Minimum" used in section headers.
      /// </summary>
      /// <value>The minimum.</value>
      string minimum { get; }
      /// <summary>
      /// The localized string for "Maximum" used in section headers.
      /// </summary>
      /// <value>The maximum.</value>
      string maximum { get; }
      /// <summary>
      /// The localized string for "Average" used in section headers.
      /// </summary>
      /// <value>The average.</value>
      string average { get; }

      /// <summary>
      /// Queries the type safe string for the given device model;
      /// </summary>
      /// <returns>The device model string.</returns>
      /// <param name="deviceModel">Device model.</param>
      string GetDeviceModelString(EDeviceModel deviceModel);
      /// <summary>
      /// Queries the type safe string for the given sensor type.
      /// </summary>
      /// <returns>The sensor type string.</returns>
      /// <param name="sensorType">Sensor type.</param>
      string GetSensorTypeString(Sensors.ESensorType sensorType);
    }
  }
}

