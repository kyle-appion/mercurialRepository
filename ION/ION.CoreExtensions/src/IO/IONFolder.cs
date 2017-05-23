namespace ION.Core.IO {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Security.Permissions;
  using System.Threading.Tasks;


  public class IONFolder  : IFolder {
    // Overridden from IFolder
    public string fullPath { 
      get {
        return nativeFolder.FullName;
      }
    }
    // Overridden from IFolder
    public string name {
      get {
        return nativeFolder.Name;
      }
    }

    /// <summary>
    /// The native folder.
    /// </summary>
    private DirectoryInfo nativeFolder { get; set; }

    public IONFolder(DirectoryInfo folder) {
      nativeFolder = folder;
    }

    // Overridden from IFolder
    public bool ContainsFolder(string name) {
      foreach (DirectoryInfo dir in nativeFolder.GetDirectories()) {
        if (dir.Name.Equals(name)) {
          return true;
        }
      }

      return false;
    }

    // Overridden from IFolder
    public bool ContainsFile(string name) {
      foreach (FileInfo file in nativeFolder.GetFiles()) {
        if (file.Name.Equals(name)) {
          return true;
        }
      }

      return false;
    }

    // Overridden from IFolder
    public IFolder GetFolder(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing) {
      var dir = System.IO.Path.Combine(nativeFolder.FullName, name);

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
      var file = System.IO.Path.Combine(nativeFolder.FullName, name);

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

      foreach (DirectoryInfo dir in nativeFolder.GetDirectories()) {
        ret.Add(new IONFolder(dir));
      }

      return ret;
    }

    // Overridden from IFolder
    public List<IFile> GetFileList() {
      var ret = new List<IFile>();

      foreach (FileInfo file in nativeFolder.GetFiles()) {
        ret.Add(new IONFile(file));
      }

      return ret;
    }

    // Overridden from IFolder
    public bool Exists() {
      return nativeFolder.Exists;
    }

    // Overridden from IFolder
    public bool Create() {
      nativeFolder.Create();
      return nativeFolder.Exists;
    }

    // Overridden from IFolder
    public void Delete() {
      nativeFolder.Delete();
    }
  }
}

