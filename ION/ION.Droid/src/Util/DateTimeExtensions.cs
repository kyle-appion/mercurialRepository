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
			if (timeSpan.TotalHours > 24) {
				return timeSpan.TotalDays.ToString("#.#") + " " + context.GetString(Resource.String.time_days_abrv);
			} else if (timeSpan.TotalMinutes > 60) {
				return timeSpan.TotalHours.ToString("#.#") + " " + context.GetString(Resource.String.time_hours_abrv);
			} else if (timeSpan.TotalSeconds > 60) {
				return timeSpan.TotalMinutes.ToString("#.#") + " " + context.GetString(Resource.String.time_minutes_abrv);
			} else {
				return timeSpan.TotalSeconds.ToString("#.#") + " " + context.GetString(Resource.String.time_seconds_abrv);
			}
		}
  }
}

