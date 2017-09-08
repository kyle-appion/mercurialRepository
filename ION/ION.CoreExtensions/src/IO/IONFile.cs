using System;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
  public class IONFile : IFile {
    /// <summary>
    /// Queries the path of the file. The local name of the path is, coincidently,
    /// the name of the file.
    /// </summary>
    /// <value>The full path.</value>
    public string fullPath { 
      get {
        return __nativeFile.FullName;
      }
    }

    /// <summary>
    /// The name of the file.
    /// </summary>
    /// <value>The name.</value>
    public string name {
      get {
        return __nativeFile.Name;
      }
    }

    /// <summary>
    /// Queries the file extension for the file. This is done by taking the last string section separated by a period.
    /// If the file does not have an extension, then we will return "".
    /// </summary>
    /// <value>The extension.</value>
    public string extension {
      get {
        var ret = __nativeFile.Extension;

        if (ret == null) {
          ret = "";
        }

        return ret;
      }
    }

    /// <summary>
    /// The native file object.
    /// </summary>
    private FileInfo __nativeFile;
    
    public IONFile(FileInfo file) {
      __nativeFile = file;
    }

    // Overridden from IFile
    public long GetSize() {
      return __nativeFile.Length;
    }

    // Overridden from IFile
    public Stream OpenForReading() {
      return __nativeFile.OpenRead();
    }

    // Overridden from IFile
    public Stream OpenForWriting(bool append = false) {
      if (__nativeFile.Exists) {
        if (append) {
          return __nativeFile.Open(FileMode.Append);
        } else {
          return __nativeFile.OpenWrite();
        }
      } else {
        return __nativeFile.Create();
      }
    }

    // Overridden from IFile
    public bool Exists() {
      return __nativeFile.Exists;
    }

    // Overridden from IFile
    public bool Create() {
      __nativeFile.Create();
      return __nativeFile.Exists;
    }

    // Overridden from IFile
    public void Delete() {
      __nativeFile.Delete();
    }
  }
}

