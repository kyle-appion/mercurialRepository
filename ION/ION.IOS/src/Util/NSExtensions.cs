using System;

using Foundation;

namespace ION.IOS.Util {
  /// <summary>
  /// Extensions for the NS classes. This functions are reimplementations of the
  /// functions that  xamarin conveniently left out of their APIs.
  /// </summary>
  public static class NSExtensions {
    /// <summary>
    /// Subarrays the given array with the given range.
    /// </summary>
    /// <returns>The with range.</returns>
    /// <param name="array">Array.</param>
    /// <param name="range">Range.</param>
    public static NSArray SubarrayWithRange(this NSArray array, NSRange range) {
      var ret = new object[range.Length];

      for (nint i = range.Location, j = 0; j < range.Length; i++, j++) {
        ret[j] = array.GetItem<NSObject>((nuint)i);
      }

      return NSArray.FromObjects(ret);
    }
  }
}

