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



// Security bytes: [52, 50, 53, 49, 53, 57, 50, 56, 49, 54, 54, 52, 51, 48, 48, 56]