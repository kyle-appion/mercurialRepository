using System;
using System.Threading.Tasks;

using ION.Core.IO;

namespace ION.IOS.IO {
  public class IosFileManager : IFileManager {
    // Overridden from IFileManager
    public Task<IFolder> GetAssetDirectoryAsync() {
      return Task.Run(() => {
        var cd = System.IO.Directory.GetCurrentDirectory();

        return (IFolder)new IONFolder(new System.IO.DirectoryInfo(cd));//System.IO.Path.Combine(cd, "Resources")));
      });
    }

    // Overridden from IFileManager
    public Task<IFolder> GetApplicationInternalDirectoryAsync() {
      return Task.Run(() => {
        return (IFolder)new IONFolder(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
      });
    }

    // Overridden from IFileManager
    public Task<IFolder> GetApplicationExternalDirectoryAsync() {
      return Task.Run(() => {
        return (IFolder)new IONFolder(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
      });
    }
  }
}

