using System;
using System.Threading.Tasks;

using ION.Core.App;

namespace ION.Core.IO {
  /// <summary>
  /// The contract that allows the core to interact with the platform storage.
  /// </summary>
  public interface IFileManager : IManager {
    /// <summary>
    /// Queries the directory that the application's resources/assets are held.
    /// </summary>
    /// <returns>The application directory async.</returns>
    IFolder GetAssetDirectory();
    /// <summary>
    /// Queries the directory that allows core to interact with files that are
    /// saved to the application's local storage. ie. for android, this will 
    /// be the application's private storage. For a desktop, this would be
    /// roaming.
    /// </summary>
    /// <returns>The application directory.</returns>
    IFolder GetApplicationInternalDirectory();
    /// <summary>
    /// Queries the directory that allows the application interact with files
    /// that are located "far" away from the application. ie. for android this
    /// would be the SDCard. For a desktop, this can be any directory that is
    /// not the System directory.
    /// </summary>
    /// <returns>The external directory.</returns>
    IFolder GetApplicationExternalDirectory();
    /// <summary>
    /// Creates a temporary file.
    /// </summary>
    /// <returns>The temporary file.</returns>
    /// <param name="name">Name.</param>
    /// <param name="accessReponse">Access reponse.</param>
    IFile CreateTemporaryFile(string name, EFileAccessResponse accessReponse = EFileAccessResponse.ReplaceIfExists);
  }
}

