using System;

namespace ION.Core.Util {
  /// <summary>
  /// A class that will provide some extension for byte interactions.
  /// </summary>
  public static class ByteExtensions {
    /// <summary>
    /// Converts a byte array into a user friendly hex string.
    /// Stolen from Stackoverflow because it is adorable:
    /// http://stackoverflow.com/a/14333437/480691
    /// </summary>
    /// <returns>The byte string.</returns>
    /// <param name="bytes">Bytes.</param>
    public static string ToByteString(this byte[] bytes) {
			if (bytes == null) {
				return "";
			}

      char[] c = new char[bytes.Length * 5];
      int b;
      for (int i = 0; i < bytes.Length; i++) {
        c[i * 5] = '0';
        c[i * 5 + 1] = 'x';
        b = bytes[i] >> 4;
        c[i * 5 + 2] = (char)(55 + b + (((b-10)>>31)&-7));
        b = bytes[i] & 0xF;
        c[i * 5 + 3] = (char)(55 + b + (((b-10)>>31)&-7));
        c[i * 5 + 4] = ' ';
      }
      return new string(c);
    }
  }
}

