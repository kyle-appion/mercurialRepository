namespace ION.IOS.IO {
  
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.IO;

  public class IosFileManager : IFileManager {

    // Overridden from IFileManager
    public async Task<InitializationResult> InitAsync() {
      return new InitializationResult() { success = true };
    }

    // Overridden from IFileManager
    public void Dispose() {
    }

    // Overridden from IFileManager
    public IFolder GetAssetDirectory() {
      var cd = System.IO.Directory.GetCurrentDirectory();   
      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(cd));
//      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
    }

    // Overridden from IFileManager
    public IFolder GetApplicationInternalDirectory() {
      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
    }

    // Overridden from IFileManager
    public IFolder GetApplicationExternalDirectory() {
      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
    }

    // Overridden from IFileManager
    public IFile CreateTemporaryFile(string name, EFileAccessResponse accessResponse = EFileAccessResponse.ReplaceIfExists) {
      var tempFileName = System.IO.Path.GetTempPath() + name;
      return new IONFile(new FileInfo(tempFileName));
    }
  }
}

