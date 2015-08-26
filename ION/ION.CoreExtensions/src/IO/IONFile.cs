using System;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
  public class IONFile : IFile {
    // Overridden from IFile
    public string fullPath { 
      get {
        return __nativeFile.FullName;
      }
    }
    // Overridden from IFile
    public string name {
      get {
        return __nativeFile.Name;
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

