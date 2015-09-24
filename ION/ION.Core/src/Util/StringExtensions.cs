using System;

namespace ION.Core.Util {
  public static class StringExtensions {
    /// <summary>
    /// Converts the given string to comprised byte array.
    /// </summary>
    /// <returns>The bytes.</returns>
    /// <param name="str">String.</param>
    public static byte[] ToBytes(this string str) {
      return System.Text.Encoding.UTF8.GetBytes(str);
    }
  }
}

