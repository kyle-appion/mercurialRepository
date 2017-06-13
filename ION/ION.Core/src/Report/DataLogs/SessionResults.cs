namespace ION.Core.Report.DataLogs {

  using System;
	using System.Collections.Generic;

  using ION.Core.Devices;

  /// <summary>
  /// The class that holds the results of a session.
  /// </summary>
  public class SessionResults : IComparable<SessionResults> {
    /// <summary>
    /// Whether or not this object represents the complete results of the session.
    /// </summary>
    /// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
    public bool complete { get; internal set; }
		/// <summary>
		/// The session id that the session results represents.
		/// </summary>
		/// <value>The session identifier.</value>
    public int sessionId { get; internal set; }
    /// <summary>
    /// The array of device sensor logs that make up the session.
    /// </summary>
    /// <value>The start time.</value>
    public List<DeviceSensorLogs> deviceSensorLogs { get; internal set; }
    /// <summary>
    /// The time that the session started.
    /// </summary>
    /// <value>The start time.</value>
    public DateTime startTime { get; internal set; }
    /// <summary>
    /// The time that the session ended.
    /// </summary>
    /// <value>The end time.</value>
    public DateTime endTime { get; internal set; }
		/// <summary>
		/// Whether or not the session results is empty.
		/// </summary>
		/// <value>The is empty.</value>
		public bool isEmpty {
			get {
				foreach (var dsl in deviceSensorLogs) {
					if (dsl.logs.Count > 0) {
						return false;
					}
				}

				return true;
			}
		}

    /// <summary>
    /// Queries the device sensor logs for the given sensor. If the sensor is not present in the session results, then
    /// we will return null.
    /// </summary>
    /// <returns>The device sensor logs for.</returns>
    /// <param name="sensor">Sensor.</param>
    public DeviceSensorLogs GetDeviceSensorLogsFor(GaugeDeviceSensor sensor) {
      foreach (var dsl in deviceSensorLogs) {
        if (dsl.deviceSerialNumber.Equals(sensor.device.serialNumber.ToString()) && dsl.index == sensor.index) {
          return dsl;
        }
      }
      return null;
    }

    /// <summary>
    /// Returns a subset of this session result that will fit within the given date range.
    /// </summary>
    /// <returns>The set.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    public SessionResults SubSet(DateTime startTime, DateTime endTime) {
			var dsl = new List<DeviceSensorLogs>(deviceSensorLogs);

      for (int i = 0; i < deviceSensorLogs.Count; i++) {
        dsl[i] = deviceSensorLogs[i].SubSet(startTime, endTime);
      }

      return new SessionResults() {
        complete = false,
				sessionId = this.sessionId,
        deviceSensorLogs = dsl,
        startTime = startTime,
        endTime = endTime,
      };
    }

    // Implemented for IComparable
    public int CompareTo(SessionResults sr) {
      return startTime.CompareTo(sr.startTime);
    }
  }
}

