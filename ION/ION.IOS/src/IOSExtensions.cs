namespace ION.IOS {

	using System;

	using Foundation;

	public static class IOSExtensions {
		/// <summary>
		/// Converts a NSDate to a date time.
		/// </summary>
		/// <returns>The ate to date time.</returns>
		/// <param name="date">Date.</param>
		public static DateTime NSDateToDateTime(this NSDate date) {
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
			return reference.AddSeconds(date.SecondsSinceReferenceDate);
		}

		/// <summary>
		/// Converts a date time to a NSDate.
		/// </summary>
		/// <returns>The time to NSD ate.</returns>
		/// <param name="date">Date.</param>
		public static NSDate DateTimeToNSDate(this DateTime date) {
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2003, 1, 1, 0, 0, 0));
			return NSDate.FromTimeIntervalSinceReferenceDate((date - reference).TotalSeconds);
		}
	}
}

