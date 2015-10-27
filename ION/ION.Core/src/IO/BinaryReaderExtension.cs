namespace ION.Core.IO {

  using System;
  using System.IO;

  /// <summary>
  /// Some convenience extensions for BinaryReader, such as the greatly forgotten
  /// big-endian byte order preference.
  /// </summary>
  public static class BinaryReaderExtension {
    /// <summary>
    /// Reverse the specified bytes.
    /// </summary>
    /// <param name="bytes">Bytes.</param>
    public static byte[] Reverse(this byte[] bytes) {
      Array.Reverse(bytes);
      return bytes;
    }

    public static byte[] ReadBytesRequired(this System.IO.BinaryReader reader, int byteCount) {
      var ret = reader.ReadBytes(byteCount);

      if (ret.Length != byteCount) {
        throw new EndOfStreamException("Cannot read " + byteCount + " bytes from stream: stream ended unexpectedly");
      }

      return ret;
    }

    /// <summary>
    /// Reads a big engian int 16.
    /// </summary>
    /// <returns>The int16.</returns>
    /// <param name="reader">Reader.</param>
    public static short ReadInt16BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(short));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToInt16(bytes, 0);
    }

    /// <summary>
    /// Reads a big engian int 32.
    /// </summary>
    /// <returns>The int32 B.</returns>
    /// <param name="reader">Reader.</param>
    public static int ReadInt32BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(int));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// Reads a big engian int 64.
    /// </summary>
    /// <returns>The int64.</returns>
    /// <param name="reader">Reader.</param>
    public static long ReadInt64BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(long));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToInt64(bytes, 0);
    }

    /// <summary>
    /// Reads a big engian uint 16.
    /// </summary>
    /// <returns>The uint16.</returns>
    /// <param name="reader">Reader.</param>
    public static ushort ReadUInt16BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(ushort));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToUInt16(bytes, 0);
    }

    /// <summary>
    /// Reads a big engian uint 32.
    /// </summary>
    /// <returns>The uint32 B.</returns>
    /// <param name="reader">Reader.</param>
    public static uint ReadUInt32BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(uint));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// Reads a big engian uint 64.
    /// </summary>
    /// <returns>The uint64.</returns>
    /// <param name="reader">Reader.</param>
    public static ulong ReadUInt64BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(ulong));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToUInt64(bytes, 0);
    }

    /// <summary>
    /// Reads a big endian float32.
    /// </summary>
    /// <returns>The float32.</returns>
    /// <param name="reader">Reader.</param>
    public static float ReadFloat32BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(float));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToSingle(bytes, 0);
    }

    /// <summary>
    /// Reads a big endian double from the reader.
    /// </summary>
    /// <returns>The float64.</returns>
    /// <param name="reader">Reader.</param>
    public static double ReadFloat64BE(this BinaryReader reader) {
      var bytes = reader.ReadBytesRequired(sizeof(double));
      if (BitConverter.IsLittleEndian) {
        bytes = bytes.Reverse();
      }
      return BitConverter.ToDouble(bytes, 0);
    }
  }
}

