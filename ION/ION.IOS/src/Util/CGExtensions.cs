using System;

using Foundation;
using UIKit;

using ION.Core.Util;

namespace ION.IOS.Util {
  public static class CGExtensions {
    /// <summary>
    /// Creates a UIColor from an ARGB8888 32-bit integer.
    /// </summary>
    /// <returns>The argument b8888.</returns>
    /// <param name="argb">ARGB.</param>
    public static UIColor FromARGB8888(int argb) {
      var alpha = (argb >> 24) & 0xff;
      var red = (argb >> 16) & 0xff;
      var green = (argb >> 8) & 0xff;
      var blue = (argb >> 0) & 0xff;

      return UIColor.FromRGBA(red, green, blue, alpha);
    }
  }
}

