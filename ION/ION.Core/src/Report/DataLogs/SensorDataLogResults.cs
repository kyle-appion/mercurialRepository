namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;

  using Appion.Commons.Measure;

  using ION.Core.Database;
  using ION.Core.Devices;

  /// <summary>
  /// A simple structure that will map a number of sessions that a GaugeDeviceSensor was present in to the
  /// DataLogMeasurements stored for each of those sessions.
  /// </summary>
	public class SensorDataLogResults : IComparable<SensorDataLogResults> {

    /// <summary>
    /// The inflated sensor who owns the sensor measurement rows.
    /// </summary>
    /// <value>The sensor.</value>
    public GaugeDeviceSensor sensor { get; private set; }
		/// <summary>
		/// The enumeration of the sessions ids that are present in the SensorDataLogResults.
		/// </summary>
		/// <value>The sessions identifiers.</value>
		public IEnumerable<int> sessionIds { get { return _measurementData.Keys; } }
    /// <summary>
    /// The earliest date present in the data log results.
    /// </summary>
    /// <value>The start date.</value>
    public DateTime startDate { get; private set; }
    /// <summary>
    /// The latest date present in the date log results.
    /// </summary>
    /// <value>The end date.</value>
    public DateTime endDate { get; private set; }
    /// <summary>
    /// The minimum measurement of the results.
    /// </summary>
    /// <value>The minimum.</value>
    public Scalar minimum { get; private set; }
		/// <summary>
		/// The maximum measurement of the results.
		/// </summary>
		/// <value>The minimum.</value>
		public Scalar maximum { get; private set; }
    /// <summary>
    /// The average measurement of the results.
    /// </summary>
    /// <value>The average.</value>
    public Scalar average { get; private set; }

		/// <summary>
		/// Queries the DataLogMeasurements for the given session id. If the SensorDataLogResults does not contain
		/// the measurements for the given session id, we will retun false.
		/// </summary>
		/// <param name="sessionId">Session identifier.</param>
		public List<DataLogMeasurement> this[int sessionId] {
      get {
        if (_measurementData.ContainsKey(sessionId)) {
          return _measurementData[sessionId];
        } else {
          return null;
        }
      }
    }

    /// <summary>
    /// A dictionary mapping from a session id to the complete list of all sensor measurement rows for this GaugeDeviceSensor.
    /// </summary>
    private Dictionary<int, List<DataLogMeasurement>> _measurementData;

    public SensorDataLogResults(GaugeDeviceSensor sensor, Dictionary<int, List<DataLogMeasurement>> measurementData) {
      this.sensor = sensor;
      _measurementData = measurementData;

      var sd = new DateTime(9999, 1, 1);
      var ed = new DateTime(1, 1, 1);
      var su = sensor.unit.standardUnit;
      var min = su.OfScalar(double.MaxValue);
      var max = su.OfScalar(double.MinValue);
      var avg = 0.0;
      var cnt = 0;
      
      
      foreach (var id in sessionIds) {
        foreach (var dlm in this[id]) {
          if (dlm.recordedDate < sd) {
            sd = dlm.recordedDate;
          }
          if (dlm.recordedDate > ed) {
            ed = dlm.recordedDate;
          }
          if (dlm.measurement < min) {
            min = dlm.measurement;
          }
          if (dlm.measurement > max) {
            max = dlm.measurement;
          }
          avg += dlm.measurement.ConvertTo(su).amount;
          cnt++;
        }
      }

      startDate = sd;
      endDate = endDate;
      minimum = min;
      maximum = max;
      average = su.OfScalar(avg / cnt);
    }

    /// <summary>
    /// Returns a subset of this result object such that all items within the results are bounded by the given dates.
    /// </summary>
    /// <returns>The by dates.</returns>
    /// <param name="startDate">Start date.</param>
    /// <param name="endDate">End date.</param>
    public SensorDataLogResults SubsetByDates(DateTime startDate, DateTime endDate) {
      var dict = new Dictionary<int, List<DataLogMeasurement>>();

      foreach (var id in sessionIds) {
        var list = new List<DataLogMeasurement>();
        foreach (var dlm in this[id]) {
          if (dlm.recordedDate.BoundedBy(startDate, endDate)) {
            list.Add(dlm);
          }
        }
        if (list.Count > 0) {
          list.Sort();
          dict[id] = list;
        }
      }

      return new SensorDataLogResults(sensor, dict);
    }
    
    public int CompareTo(SensorDataLogResults results) {
      return startDate.CompareTo(results.startDate);
    }
  }
}
