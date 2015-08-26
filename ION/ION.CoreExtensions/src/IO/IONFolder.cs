using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
  public class IONFolder  : IFolder {
    // Overridden from IFile
    public string fullPath { 
      get {
        return __nativeFolder.FullName;
      }
    }
    // Overridden from IFile
    public string name {
      get {
        return __nativeFolder.Name;
      }
    }

    /// <summary>
    /// The native folder.
    /// </summary>
    private DirectoryInfo __nativeFolder;

    public IONFolder(DirectoryInfo folder) {
      __nativeFolder = folder;
    }

    // Overridden from IFolder
    public bool ContainsFolder(string name) {
      foreach (DirectoryInfo dir in __nativeFolder.GetDirectories()) {
        if (dir.Name.Equals(name)) {
          return true;
        }
      }

      return false;
    }

    // Overridden from IFolder
    public bool ContainsFile(string name) {
      foreach (FileInfo file in __nativeFolder.GetFiles()) {
        if (file.Name.Equals(name)) {
          return true;
        }
      }

      return false;
    }

    // Overridden from IFolder
    public IFolder GetFolder(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing) {
      var dir = System.IO.Path.Combine(__nativeFolder.FullName, name);

      if (Directory.Exists(dir)) {
        if (EFileAccessResponse.FailIfExists == accessResponse) {
          throw new IOException("Cannot get folder " + dir + ": folder already exists.");
        } else {
          return (IFolder) new IONFolder(new DirectoryInfo(dir));
        }
      } else {
        if (EFileAccessResponse.FailIfMissing == accessResponse) {
          throw new FileNotFoundException("Cannot get folder " + dir + ": doesn't exist.");
        } else {
          return (IFolder)new IONFolder(Directory.CreateDirectory(dir));
        }
      }
    }

    // Overridden from IFolder
    public IFile GetFile(string name, EFileAccessResponse accessReponse = EFileAccessResponse.FailIfMissing) {
      var file = System.IO.Path.Combine(__nativeFolder.FullName, name);

      if (File.Exists(file)) {
        if (EFileAccessResponse.FailIfExists == accessReponse) {
          throw new IOException("Cannot get File " + file + ": file already exists.");
        } else {
          if (EFileAccessResponse.ReplaceIfExists == accessReponse) {
            File.Create(file).Close();
          }
          return (IFile) new IONFile(new FileInfo(file));
        }
      } else {
        if (EFileAccessResponse.FailIfMissing == accessReponse) {
          throw new FileNotFoundException("Cannot get file " + file + ": doesn't exist.");
        } else {
          if (EFileAccessResponse.CreateIfMissing == accessReponse) {
            File.Create(file).Close();
          }
          return (IFile) new IONFile(new FileInfo(file));
        }
      }
    }

    // Overridden from IFolder
    public List<IFolder> GetFolderList() {
      var ret = new List<IFolder>();

      foreach (DirectoryInfo dir in __nativeFolder.GetDirectories()) {
        ret.Add(new IONFolder(dir));
      }

      return ret;
    }

    // Overridden from IFolder
    public List<IFile> GetFileList() {
      var ret = new List<IFile>();

      foreach (FileInfo file in __nativeFolder.GetFiles()) {
        ret.Add(new IONFile(file));
      }

      return ret;
    }

    // Overridden from IFolder
    public bool Exists() {
      return __nativeFolder.Exists;
    }

    // Overridden from IFolder
    public bool Create() {
      __nativeFolder.Create();
      return __nativeFolder.Exists;
    }

    // Overridden from IFolder
    public void Delete() {
      __nativeFolder.Delete();
    }
  }
}

