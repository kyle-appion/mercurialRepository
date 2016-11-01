namespace ION.Droid.Report {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using ION.Core.Report.DataLogs;

	/// <summary>
	/// An object that will organize the data log reports into a table to be exported.
	/// </summary>
	public class DataLogReportTable {

		public string[] header { get; private set; }
		public DateTime[] dates { get; private set; } 
		public string[,] content { get; private set; }

		private DataLogReportTable(string[] header, DateTime[] dates, string[,] content) {
		}




		public static Task<DataLogReportTable> Create(Dictionary<string, List<DeviceSensorLogs>> logs) {
			return Task.Factory.StartNew(() => {
				var dl = new List<DateTime>();

				foreach (var key in logs.Keys) {
					foreach (var dsl in logs[key]) {
						foreach (var sl in dsl.logs) {
							if (!dl.Contains(sl.recordedDate)) {
							}
						}
					}
				}

				return new DataLogReportTable(null, null, null);
			});
		}
/*

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
*/

	}
}

