namespace ION.Droid.Util {

  using System;

	using Android.Content;

  public static class DateTimeExtensions {
    /// <summary>
    /// Converts the date time to a full time stamp string.
    /// </summary>
    /// <returns>The full short string.</returns>
    /// <param name="dateTime">Date time.</param>
    public static string ToFullShortString(this DateTime dateTime) {
      return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
    }

		public static string ToFriendlyString(this TimeSpan timeSpan, Context context) {
      var r = context.Resources;
			if (timeSpan.TotalHours > 24) {
        return timeSpan.TotalDays.ToString("#.#") + " " + r.GetQuantityString(Resource.Plurals.time_days, (int)timeSpan.TotalDays);
			} else if (timeSpan.TotalMinutes > 60) {
        return timeSpan.TotalHours.ToString("#.#") + " " + r.GetQuantityString(Resource.Plurals.time_hours, (int)timeSpan.TotalHours);
			} else if (timeSpan.TotalSeconds > 60) {
        return timeSpan.TotalMinutes.ToString("#.#") + " " + r.GetQuantityString(Resource.Plurals.time_mins, (int)timeSpan.TotalMinutes);
			} else {
        return timeSpan.TotalSeconds.ToString("#.#") + " " + r.GetQuantityString(Resource.Plurals.time_secs, (int)timeSpan.TotalSeconds);
			}
		}
  }
}

