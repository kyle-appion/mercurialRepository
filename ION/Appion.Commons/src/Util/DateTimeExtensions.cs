namespace Appion.Commons.Util {
  
  using System;

  /// <summary>
  /// Some usful extensions to the date time class.
  /// </summary>
  public static class DateTimeExtensions {
    /// <summary>
    /// Converts the date time to milliseconds since 1 Janurary 1970.
    /// </summary>
    /// <returns>The UTC milliseconds.</returns>
    /// <param name="time">Time.</param>
    public static long ToUTCMilliseconds(this DateTime dateTime) {
      return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
  }
}

