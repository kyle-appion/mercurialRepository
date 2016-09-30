namespace ION.Core.Report.DataLogs {

  using System;

	using ION.Core.Util;

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
    /// The date sorted array of the actual logs.
    /// </summary>
    /// <value>The logs.</value>
    public SensorLog[] logs { get; internal set; }
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

		public DeviceSensorLogs(string serialNumber, int index, DateTime start, DateTime end) {
			this.deviceSerialNumber = serialNumber;
			this.index = index;
			this.logs = new SensorLog[0];
			this.start = start;
			this.end = end;
		}

		public DeviceSensorLogs(string serialNumber, int index, SensorLog[] logs) {
			this.deviceSerialNumber = serialNumber;
			this.index = index;
			this.logs = logs;
			this.start = logs[0].recordedDate;
			this.end = logs[logs.Length - 1].recordedDate;
		}

    /// <summary>
    /// Subsets this data set.
    /// </summary>
    /// <returns>The set.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    public DeviceSensorLogs SubSet(DateTime startTime, DateTime endTime) {
			var st = startTime;
			var et = endTime;

			if (st < start) {
				st = start;
			}

			if (et > end) {
				et = end;
			}

      var l = logs.Length;
			var low = 0;
//      var low = FindTime(st, 0, l - 1);
      var high = FindTime(et, 0, l - 1);

			if (low < 0 || high < 0) {
				Log.E(this, "StartTime: " + st + " EndTime: " + et);
				Log.E(this, "Cannot subset: start time is greater than or equal to end time");
				return new DeviceSensorLogs(deviceSerialNumber, index, st, et);
			}

      var sl = new SensorLog[high - low];
			if (sl.Length > 0) {
      	Array.ConstrainedCopy(logs, low, sl, 0, high - low);
				return new DeviceSensorLogs(deviceSerialNumber, index, sl);
			} else {
				return new DeviceSensorLogs(deviceSerialNumber, index, startTime, endTime);				
			}
    }

		public int CompareTo(DeviceSensorLogs logs) {
			var ret = deviceSerialNumber.CompareTo(logs.deviceSerialNumber);

			if (ret == 0) {
				start.CompareTo(logs.start);
			}

			return ret;
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
			if (startInclusive >= endInclusive || logs.Length == 0) {
        return endInclusive;
      }

			if (logs[startInclusive].recordedDate == timeToFind) {
				return startInclusive;
			} else if (logs[endInclusive].recordedDate == timeToFind) {
				return endInclusive;
			} else {
				var i = (startInclusive + endInclusive) / 2;
				Log.D(this, "timeToFind: " + timeToFind + " i: " + i + " startInclusive: " + startInclusive + " endInclusive: " + endInclusive);
	      if (logs[i].recordedDate == timeToFind) {
	        return i;
	      }

				var cmp = timeToFind.CompareTo(logs[i].recordedDate);
	      if (cmp >= 0) {
	        return FindTime(timeToFind, i + 1, endInclusive);
	      } else {
	        return FindTime(timeToFind, startInclusive, i);
	      }
			}
    }
  }
}

