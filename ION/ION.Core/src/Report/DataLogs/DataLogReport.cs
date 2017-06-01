namespace ION.Core.Report.DataLogs {

	using System;
  using System.Collections.Generic;
  using System.IO;

	using Appion.Commons.Util;

	using ION.Core.App;
  using ION.Core.Database;
	using ION.Core.Devices;
  using ION.Core.UI;

	/// <summary>
	/// A small data object that holds all of the data that is used when generating a data log report.
  /// Note: A DataLogReport requires one contract class that will bind the host platform to the report itself. This
  /// contract is the ILocationzation class. It defines some strings and other things that are needed for the report
  /// to be generated cleanly.
	/// </summary>
  public class DataLogReport {
    /// <summary>
    /// The name for the data log report.
    /// </summary>
    /// <value>The name of the report.</value>
    public string reportName { get; set; }
    /// <summary>
    /// The localization object for the report.
    /// </summary>
    /// <value>The localization.</value>
    public ILocalization localization { get; set; }
    /// <summary>
    /// The bytes that make up the appion logo.
    /// </summary>
    /// <value>The appion logo png.</value>
    public byte[] appionLogoPng { get; set; }
		/// <summary>
		/// The Dictionary that maps sensors to their exported graph png image. 
		/// </summary>
		public Dictionary<GaugeDeviceSensor, IonImage> graphImages { get; set; }
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
		public HashSet<JobRow> jobs { get; private set; }
		/// <summary>
		/// The devices that produced the session results.
		/// </summary>
		public List<IDevice> devices { get; private set; }
		/// <summary>
		/// The sensors that were used to make up the report.
		/// </summary>
		/// <value>The sensors.</value>
		public HashSet<GaugeDeviceSensor> sensors { get; private set; }
    /// <summary>
    /// The sorted by start date session results that are making up the current report.
    /// </summary>
    /// <value>The results.</value>
		public List<SessionResults> sessionResults { get; private set; }

		/// <summary>
		/// Creates a new DataLogReport based on the given jobs and session results.
		/// </summary>
		/// <param name="jobs">Jobs.</param>
		/// <param name="sessionResults">Session results.</param>
		private DataLogReport(ILocalization local, DateTime start, DateTime end, HashSet<JobRow> jobs, List<IDevice> devices, List<SessionResults> sessionResults) {
      this.localization = local;
			this.start = start;
			this.end = end;
			this.jobs = jobs;
			this.devices = devices;
			this.sessionResults = sessionResults;

      graphImages = new Dictionary<GaugeDeviceSensor, IonImage>();
		}

    /// <summary>
    /// Creates a new DataLogReport.
    /// </summary>
    /// <returns>The from session results.</returns>
    /// <param name="ion">Ion.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="sessionResults">Session results.</param>
    public static DataLogReport BuildFromSessionResults(IION ion, ILocalization local, DateTime start, DateTime end, List<SessionResults> sessionResults) {
      sessionResults.Sort();

			var jobs = new HashSet<JobRow>();
			var deviceSet = new List<IDevice>();
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

      var ret = new DataLogReport(local, start, end, jobs, deviceSet, sessionResults);

      ret.sensors = GetUsedSensors(ion, sessionResults);

      return ret;
		}

		/// <summary>
		/// Builds a collection of all of the GaugeDeviceSensors that are apart of the session results.
		/// </summary>
		/// <returns>The used sensors.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="sessionResults">Session results.</param>
		private static HashSet<GaugeDeviceSensor> GetUsedSensors(IION ion, IEnumerable<SessionResults> sessionResults) {
			var sensors = new HashSet<GaugeDeviceSensor>();

			foreach (var session in sessionResults) {
				foreach (var dsl in session.deviceSensorLogs) {
					var sn = dsl.deviceSerialNumber.ParseSerialNumber();
					var device = ion.deviceManager[sn] as GaugeDevice;
					if (device != null) {
						sensors.Add(device[dsl.index]);
					}
				}
			}

			return sensors;
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
      /// <summary>
      /// Queries the stream for the font that is used for the data log report.
      /// </summary>
      /// <returns>The font stream.</returns>
      Stream GetFontStream();
    }
  }
}

