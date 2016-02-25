using System;

using CoreGraphics;

using ION.Core.Util;

namespace ION.IOS.Util {
  /// <summary>
  /// A resource class that will hold common colors for the iOS application.
  /// </summary>
  public static class Colors {
    public static CGColor
    BLACK = FromInt(0xff000000),
      BLUE = FromInt(0xff0000ff),
      GOLD = FromInt(0xfffdd351),
      GRAY = FromInt(0xff777777),
      GREEN = FromInt(0xff39b54a),
      LIGHT_BLUE = FromInt(0xff00beef),
      LIGHT_GRAY = FromInt(0xffbdbdbd),
      RED = FromInt(0xffff0000),
      YELLOW = FromInt(0xffffff00),
      WHITE = FromInt(0xfffffff);

    /// <summary>
    /// Returns a CGColor from a packed argb color integer.
    /// </summary>
    /// <returns>The int.</returns>
    /// <param name="argb">ARGB.</param>
    public static CGColor FromInt(uint argb) {
      var alpha = (argb & 0xff000000) >> 24;
      var red = (argb & 0x00ff0000) >> 16;
      var green = (argb & 0x0000ff00) >> 8;
      var blue = (argb & 0x000000ff);

      var ret = new CGColor(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);
      return ret;
    }
  }
}

