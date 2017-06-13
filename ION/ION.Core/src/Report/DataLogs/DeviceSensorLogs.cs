namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;

	using Appion.Commons.Util;

  /// <summary>
  /// The class that will hold all of the sensor logs for a particular device's sensor.
  /// </summary>
  public class DeviceSensorLogs : IComparable<DeviceSensorLogs> {
    /// <summary>
    /// The serial number of the device who owns the device sensor.
    /// </summary>
    /// <value>The device identifier.</value>
    public string deviceSerialNumber { get; internal set; }
    /// <summary>
    /// The index of the device sensor.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
		/// <summary>
		/// The date time of the first sensor log.
		/// </summary>
		/// <value>The start.</value>
		public DateTime start { get; private set; }
		/// <summary>
		/// The date time of the last sensor log.
		/// </summary>
		/// <value>The end.</value>
		public DateTime end { get; private set; }
		/// <summary>
		/// The date sorted array of the actual logs.
		/// </summary>
		/// <value>The logs.</value>
		public List<SensorLog> logs { get; internal set; }

    /// <summary>
    /// Creates an empty device sensor logs.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="index">Index.</param>
    private DeviceSensorLogs(string serialNumber, int index, DateTime startDate, DateTime endDate) {
			this.deviceSerialNumber = serialNumber;
			this.index = index;
			this.logs = new List<SensorLog>();
      this.start = startDate;
      this.end = endDate;
    }

    /// <summary>
    /// Creates a new device sensor logs.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="index">Index.</param>
    /// <param name="logs">Logs.</param>
    public DeviceSensorLogs(string serialNumber, int index, IEnumerable<SensorLog> logs) {
			this.deviceSerialNumber = serialNumber;
			this.index = index;
      this.logs = new List<SensorLog>(logs);
      this.logs.Sort();
			this.start = this.logs[0].recordedDate.ToLocalTime();
			this.end = this.logs[this.logs.Count - 1].recordedDate.ToLocalTime();
		}

    /// <summary>
    /// Subsets this data set.
    /// </summary>
    /// <returns>The set.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    public DeviceSensorLogs SubSet(DateTime startTime, DateTime endTime) {
			if (startTime < start) {
				startTime = start;
			}

			if (endTime > end) {
				endTime = end;
			}

      var cnt = logs.Count;

      var low = FindTime(startTime, 0, cnt - 1);
      var high = FindTime(endTime, 0, cnt - 1);

			if (high <= low) {
        return new DeviceSensorLogs(deviceSerialNumber, index, startTime, endTime);
			}
      cnt = high - low + 1; // +1 for inclusive subtraction
			if (cnt > 0) {
        var subset = logs.GetRange(low, cnt);
				return new DeviceSensorLogs(deviceSerialNumber, index, subset);
			} else {
				return new DeviceSensorLogs(deviceSerialNumber, index, startTime, endTime);				
			}
    }

    /// <summary>
    /// Binary searches the sensor log array looking for the time to find. If the time is not found in the array, we 
		/// simply return -1.
    /// </summary>
    /// <returns>The time.</returns>
    /// <param name="timeToFind">Time to find.</param>
    /// <param name="index">Index.</param>
    /// <param name="startInclusive">Start inclusive.</param>
		/// <param name="endInclusive">End inclusive.</param>
		private int FindTime(DateTime timeToFind, int startInclusive, int endInclusive) {
			if (startInclusive >= endInclusive || logs.Count == 0) {
        return endInclusive;
      }
      var dStartTime = Math.Abs((timeToFind - logs[startInclusive].recordedDate).TotalSeconds);
			var dEndTime = Math.Abs((timeToFind - logs[endInclusive].recordedDate).TotalSeconds);

      if (dStartTime < 1) {
				return startInclusive;
			} else if (dEndTime < 1) {
				return endInclusive;
			} else {
				var i = (startInclusive + endInclusive) / 2;
	      if (logs[i].recordedDate == timeToFind) {
	        return i;
	      }

        var cmp = (int)(timeToFind - logs[i].recordedDate).TotalSeconds;
	      if (cmp >= 0) {
	        return FindTime(timeToFind, i + 1, endInclusive);
	      } else {
	        return FindTime(timeToFind, startInclusive, i);
	      }
			}
    }

		public int CompareTo(DeviceSensorLogs logs) {
			var ret = deviceSerialNumber.CompareTo(logs.deviceSerialNumber);

			if (ret == 0) {
				start.CompareTo(logs.start);
			}

			return ret;
		}
  }
}

