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
    public Task<long> GetSizeAsync() {
      return Task.Run(() =>  __nativeFile.Length);
    }

    // Overridden from IFile
    public Task<Stream> OpenForReadingAsync() {
      return Task.Run(() => (Stream)__nativeFile.OpenRead());
    }

    // Overridden from IFile
    public Task<Stream> OpenForWritingAsync(bool append = false) {
      return Task.Run(() => {
        if (append) {
          return (Stream)__nativeFile.Open(FileMode.Append);
        } else {
          return (Stream)__nativeFile.OpenWrite();
        }
      });
    }

    // Overridden from IFile
    public Task<bool> ExistsAsync() {
      return Task.Run(() => {
        return __nativeFile.Exists;
      });
    }

    // Overridden from IFile
    public Task<bool> CreateAsync() {
      return Task.Run(() => {
        __nativeFile.Create();
        return __nativeFile.Exists;
      });
    }

    // Overridden from IFile
    public Task DeleteAsync() {
      return Task.Run(() => __nativeFile.Delete());
    }
  }
}

