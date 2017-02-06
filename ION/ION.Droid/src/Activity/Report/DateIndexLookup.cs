namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Appion.Commons.Util;

	using ION.Droid.Util;

	public class DateIndexLookup {
		/// <summary>
		/// The number of indeces that match one-to-one to the dates present in the lookup.
		/// </summary>
		/// <value>The date span.</value>
		public int dateSpan {
			get {
				return dates.Count;
			}
		}

		/// <summary>
		/// The list of sorted dates that are present in the lookup.
		/// </summary>
		private List<DateTime> dates;
		/// <summary>
		/// The dictionary that maps dates to their index.
		/// </summary>
		public Dictionary<DateTime, int> lookup;

		public DateIndexLookup(List<DateTime> dates) {
			this.dates = dates;
			dates.Sort();

			lookup = new Dictionary<DateTime, int>();

			for (var i = 0; i < dates.Count; i++) {
				lookup[dates[i]] = i;
				Log.D(this, "Date is: " + dates[i].ToFullShortString());
			}
		}

		/// <summary>
		/// Returns the index of the given date or -1 if the date is not present in the lookup.
		/// </summary>
		/// <returns>The of date.</returns>
		/// <param name="time">Time.</param>
		public int IndexOfDate(DateTime time) {
			var ret = -1;

			lookup.TryGetValue(time, out ret);

			return ret;
		}

		/// <summary>
		/// Returns the DateTime from the given index.
		/// </summary>
		/// <returns>The from index.</returns>
		/// <param name="index">Index.</param>
		public DateTime DateFromIndex(int index) {
			return dates[index];
		}

		public List<DateTime> Subset(int startIndex, int count) {
			var ret = new List<DateTime>();

			var cnt = Math.Min(dates.Count, startIndex + count);

			for (int i = startIndex; i < cnt; i++) {
				ret.Add(dates[i]);
			}

			return ret;
		}
	}
}

