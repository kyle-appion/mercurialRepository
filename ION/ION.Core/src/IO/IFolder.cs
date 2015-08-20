using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
  /// <summary>
  /// The contract for folder/directory storage interaction.
  /// </summary>
  public interface IFolder {
    /// <summary>
    /// The path to the folder.
    /// </summary>
    string fullPath { get; }
    /// <summary>
    /// The name of the folder.
    /// </summary>
    /// <value>The name.</value>
    string name { get; }

    /// <summary>
    /// Queries whether or not the folder contains a folder with the given local name.
    /// </summary>
    /// <returns>The folder async.</returns>
    /// <param name="name">Name.</param>
    Task<bool> ContainsFolderAsync(string name);
    /// <summary>
    /// Queries whether or not the folder contains a file witht the given local name.
    /// </summary>
    /// <returns>The file async.</returns>
    /// <param name="name">Name.</param>
    Task<bool> ContainsFileAsync(string name);
    /// <summary>
    /// Gets a subfolder within this directory. The access response describes 
    /// the action taken for acquiring the folder.
    /// +
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="accessResponse"></param>
    Task<IFolder> GetFolderAsync(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing);
    /// <summary>
    /// Creates a new file within this folder. The access response describes
    /// the action taken for acquiring the file.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="collisionResponse"></param>
    /// <returns></returns>
    Task<IFile> GetFileAsync(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing);
    /// <summary>
    /// Queries a list of all the folders (not files) that are in the current directory.
    /// </summary>
    /// <returns></returns>
    Task<List<IFolder>> GetFolderListAsync();
    /// <summary>
    /// Queries a list of all the files (not folders) that are in the current directory.
    /// </summary>
    /// <returns>The file list async.</returns>
    Task<List<IFile>> GetFileListAsync();
    /// <summary>
    /// Queries whether or not the folder exists.
    /// </summary>
    /// <returns>The async.</returns>
    Task<bool> ExistsAsync();
    /// <summary>
    /// Creates this IFolder if it does not already exist.
    /// </summary>
    /// <returns>The async.</returns>
    Task<bool> CreateAsync();
    /// <summary>
    /// Deletes the folder and all of its content. Note: THIS IS DANGEROUS!
    /// </summary>
    Task DeleteAsync();
  }

  public enum EFileAccessResponse {
    CreateIfMissing,
    FailIfMissing,

    ReplaceIfExists,
    FailIfExists,
  }
}

