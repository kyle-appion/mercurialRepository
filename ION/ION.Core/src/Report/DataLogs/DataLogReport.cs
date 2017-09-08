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
    /// The date that the report was created.
    /// </summary>
    /// <value>The created.</value>
    public DateTime created { get; set; }
    /// <summary>
    /// The localization object for the report.
    /// </summary>
    /// <value>The localization.</value>
    public ILocalization localization { get; set; }
    /// <summary>
    /// The bytes that make up the appion logo.
    /// </summary>
    /// <value>The appion logo png.</value>
    public IonImage appionLogoPng { get; set; }
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
    /// The DateIndexLookup that is used to match sensor measurements to indices.
    /// </summary>
    /// <value>The dil.</value>
//    public DateIndexLookup dil { get; private set; }
    /// <summary>
    /// The dictionary of sensors and their coresponding data log results.
    /// </summary>
    /// <value>The data log results.</value>
    public Dictionary<GaugeDeviceSensor, SensorDataLogResults> dataLogResults { get; private set; }

    /// <summary>
    /// The devices that produced the session results.
    /// </summary>
    public HashSet<IDevice> devices {
      get {
        var ret = new HashSet<IDevice>();
        if (dataLogResults != null) {
          foreach (var sensor in dataLogResults.Keys) {
            ret.Add(sensor.device);
          }
        }
        return ret;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ION.Core.Report.DataLogs.DataLogReport"/> class.
    /// </summary>
    /// <param name="local">Local.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="jobs">Jobs.</param>
    /// <param name="devices">Devices.</param>
    public DataLogReport(ILocalization local, HashSet<JobRow> jobs,/* DateIndexLookup dil, */Dictionary<GaugeDeviceSensor, SensorDataLogResults> dataLogResults) {
      this.localization = local;
			this.jobs = jobs;
      this.dataLogResults = dataLogResults;

      graphImages = new Dictionary<GaugeDeviceSensor, IonImage>();

      var sd = new DateTime(9999, 1, 1);
      var ed = new DateTime(1, 1, 1);
      foreach (var sdlr in dataLogResults.Values) {
        if (sdlr.startDate < sd) {
          sd = sdlr.startDate;
        }
        if (sdlr.endDate > ed) {
          ed = sdlr.endDate;
        }
      }

      // Set defaults
      reportName = "Unnamed Report";
      created = DateTime.Now;
		}

    /// <summary>
    /// Sets graph data for the given sensor. If the image data is null, then existing graph data will be cleared.
    /// </summary>
    /// <returns><c>true</c>, if sensor data graph was added, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    /// <param name="image">Image.</param>
    public bool SetSensorDataGraph(GaugeDeviceSensor sensor, IonImage image = null) {
      if (!dataLogResults.ContainsKey(sensor)) {
        return false;
      }

      graphImages[sensor] = image;
      return true;
    }
    
    /// <summary>
    /// Builds a dictionary mapping session ids to start/end date tuples.
    /// </summary>
    /// <returns>The session start ends.</returns>
    public Dictionary<int, Tuple<DateTime, DateTime>> GatherSessionStartEnds() {
      var ret = new Dictionary<int, Tuple<DateTime, DateTime>>();
    
      foreach (var sdlr in dataLogResults.Values) {
        foreach (var sid in sdlr.sessionIds) {
          var dlms = sdlr[sid];
          
          if (dlms.Count <= 0) {
            continue;
          }
          
          if (ret.ContainsKey(sid)) {
            var other = ret[sid];
            DateTime st = other.Item1;
            DateTime et = other.Item2;
            var changed = false;
            
            if (dlms[0].recordedDate < st) {
              st = dlms[0].recordedDate;
              changed = true;
            }
            
            if (dlms[dlms.Count].recordedDate > et) {
              et = dlms[dlms.Count - 1].recordedDate;
              changed = true;
            }
            
            if (changed) {
              ret[sid] = new Tuple<DateTime, DateTime>(st, et);
            }
          } else {
            ret[sid] = new Tuple<DateTime, DateTime>(dlms[0].recordedDate, dlms[dlms.Count - 1].recordedDate);
          }
        }
      }
      
      return ret;
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
