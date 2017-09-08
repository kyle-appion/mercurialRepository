using System;

using ION.Core.App;

namespace ION.Core.IO {
  public interface ISerializer<T> {
    /// <summary>
    /// Serializes the given item to a byte array.
    /// </summary>
    /// <param name="t">T.</param>
    byte[] Serialize(IION ion, T t);

    /// <summary>
    /// Deserializes the given item from a byte array.
    /// </summary>
    /// <param name="bytes">Bytes.</param>
    T Deserialize(IION ion, byte[] bytes);
  }
}

