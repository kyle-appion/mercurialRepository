namespace ION.Droid.Util {

  using System;

  public static class DateTimeExtensions {
    /// <summary>
    /// Converts the date time to a full time stamp string.
    /// </summary>
    /// <returns>The full short string.</returns>
    /// <param name="dateTime">Date time.</param>
    public static string ToFullShortString(this DateTime dateTime) {
      return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
    }
  }
}

