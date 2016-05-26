﻿namespace ION.Core.Report.DataLogs {

  using System;

  /// <summary>
  /// The class that holds the results of a session.
  /// </summary>
  public class SessionResults {
    /// <summary>
    /// Whether or not this object represents the complete results of the session.
    /// </summary>
    /// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
    public bool complete { get; internal set; }
    public int sessionId { get; internal set; }
    /// <summary>
    /// The array of device sensor logs that make up the session.
    /// </summary>
    /// <value>The start time.</value>
    public DeviceSensorLogs[] deviceSensorLogs { get; internal set; }
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
    /// Returns a subset of this session result that will fit within the given date range.
    /// </summary>
    /// <returns>The set.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    public SessionResults SubSet(DateTime startTime, DateTime endTime) {
      var dsl = new DeviceSensorLogs[deviceSensorLogs.Length];

      for (int i = 0; i < deviceSensorLogs.Length; i++) {
        dsl[i] = deviceSensorLogs[i].SubSet(startTime, endTime);
      }

      return new SessionResults() {
        complete = this.complete,
        deviceSensorLogs = dsl,
        startTime = this.startTime,
        endTime = this.endTime,
      };
    }
  }
}
