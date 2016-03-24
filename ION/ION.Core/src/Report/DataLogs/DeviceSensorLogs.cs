namespace ION.Core.Report.DataLogs {

  using System;

  /// <summary>
  /// The class that will hold all of the sensor logs for a particular device's sensor.
  /// </summary>
  public class DeviceSensorLogs {
    /// <summary>
    /// The id of the device who owns the device sensor.
    /// </summary>
    /// <value>The device identifier.</value>
    public int deviceId { get; internal set; }
    /// <summary>
    /// The index of the device sensor.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
    /// <summary>
    /// The unit code that all of the senor log measurements will be in.
    /// </summary>
    /// <value>The unit code.</value>
    public int unitCode { get; internal set; }
    /// <summary>
    /// The date sorted array of the actual logs.
    /// </summary>
    /// <value>The logs.</value>
    public SensorLog[] logs { get; internal set; }
    /// <summary>
    /// The date time of the first sensor log.
    /// </summary>
    /// <value>The start.</value>
    public DateTime start { 
      get {
        return logs[0].recordedDate;
      }
    }
    /// <summary>
    /// The date time of the last sensor log.
    /// </summary>
    /// <value>The end.</value>
    public DateTime end {
      get {
        return logs[logs.Length - 1].recordedDate;
      }
    }

    /// <summary>
    /// Subsets this data set.
    /// </summary>
    /// <returns>The set.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    public DeviceSensorLogs SubSet(DateTime startTime, DateTime endTime) {
      var l = logs.Length;
      var low = FindTime(startTime, 0, l);
      var high = FindTime(endTime, 0, l);

      if (low >= high) {
        throw new Exception("Cannot subset: start time is greater than or equal to end time");
      }

      var sl = new SensorLog[high - low];
      Array.ConstrainedCopy(logs, low, sl, 0, high - low);

      return new DeviceSensorLogs() {
        deviceId = this.deviceId,
        index = this.index,
        unitCode = this.unitCode,
        logs = sl,
      };
    }

    /// <summary>
    /// Binary searches the sensor log array looking for the time to find. If the time is not found in the array, we 
    /// will return the natural index where it should be. This is down as we are not directly affecting the array.
    /// </summary>
    /// <returns>The time.</returns>
    /// <param name="timeToFind">Time to find.</param>
    /// <param name="index">Index.</param>
    /// <param name="startInclusive">Start inclusive.</param>
    /// <param name="endExclusive">End exclusive.</param>
    private int FindTime(DateTime timeToFind, int startInclusive, int endExclusive) {
      if (startInclusive >= endExclusive) {
        return startInclusive;
      }

      var i = startInclusive + endExclusive;
      if (logs[i].recordedDate == timeToFind) {
        return i;
      }

      var cmp = logs[i].recordedDate.CompareTo(timeToFind);
      if (cmp > 0) {
        return FindTime(timeToFind, i, endExclusive);
      } else {
        return FindTime(timeToFind, startInclusive, i);
      }
    }
  }
}

