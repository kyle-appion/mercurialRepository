using System;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
  /// <summary>
  /// A contract for a cross platform file.
  /// </summary>
  public interface IFile {
    /// <summary>
    /// Queries the path of the file. The local name of the path is, coincidently,
    /// the name of the file.
    /// </summary>
    string fullPath { get; }
    /// <summary>
    /// The name of the file.
    /// </summary>
    /// <value>The name.</value>
    string name { get; }

    /// <summary>
    /// Queries the size of the file. If the size could not be determined, then
    /// we will return -1;
    /// </summary>
    long GetSize();
    /// <summary>
    /// Opens the file for reading.
    /// </summary>
    /// <returns></returns>
    Stream OpenForReading();
    /// <summary>
    /// Opens the file for writing.
    /// </summary>
    /// <returns></returns>
    Stream OpenForWriting(bool append = false);
    /// <summary>
    /// Queries whether or not the file exists.
    /// </summary>
    /// <returns>The async.</returns>
    bool Exists();
    /// <summary>
    /// Creates the file if it doesn't already exist. If it does exist, nothing
    /// will happen.
    /// </summary>
    /// <returns>True if the file was created (or existed), false otherwise.</returns>
    bool Create();
    /// <summary>
    /// Deletes the file.
    /// </summary>
    void Delete();
  }
}
