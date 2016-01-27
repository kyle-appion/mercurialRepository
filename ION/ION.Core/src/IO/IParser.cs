using System;
using System.IO;

using ION.Core.App;

namespace ION.Core.IO {
  /// <summary>
  /// A common contract for parsers.
  /// </summary>
  public interface IParser<T> {
    /// <summary>
    /// The version of the parser.
    /// </summary>
    /// <value>The version.</value>
    int version { get; }

    /// <summary>
    /// Writes the item to the given stream. If the item cannot be written, we will
    /// throw an IOException.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <param name="stream">Stream.</param>
    void WriteToStream(IION ion, T item, Stream stream);

    /// <summary>
    /// Reads an item from the given stream. If the item cannot not be read, we will
    /// throw an IOException.
    /// </summary>
    /// <returns>The from stream.</returns>
    /// <param name="stream">Stream.</param>
    T ReadFromStream(IION ion, Stream stream);
  }
}

