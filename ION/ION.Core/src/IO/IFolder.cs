using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ION.Core.IO {
/*
  /// <summary>
  /// The delegate that is called when a folder event is triggered.
  /// </summary>
  public delegate void OnFolderEvent(IFolder folder, FolderEvent folderEvent);

  /// <summary>
  /// The event that is passed on a triggered folder event.
  /// </summary>
  public class FolderEvent {
    /// <summary>
    /// The type of event.
    /// </summary>
    /// <value>The type.</value>
    public EFolderEventType type { get; protected set; }
    /// <summary>
    /// Whether or not the "file" that triggered the event is a file or folder.
    /// </summary>
    /// <value><c>true</c> if is file event; otherwise, <c>false</c>.</value>
    public bool isFileEvent { get; protected set; }
    /// <summary>
    /// The file affected. This will be null if the event is a folder event.
    /// </summary>
    /// <value>The file.</value>
    public IFile file { get; protected set; }
    /// <summary>
    /// The folder affected. This will be null if the event is a file event.
    /// </summary>
    /// <value>The folder.</value>
    public IFolder folder { get; protected set; }
  }

  /// <summary>
  /// The enumeration of the types of events that may occur.
  /// </summary>
  public enum EFolderEventType {
    Added,
    Changed,
    Moved,
    Renamed,
  }
*/


  /// <summary>
  /// The contract for folder/directory storage interaction.
  /// </summary>
  public interface IFolder {
    /// <summary>
    /// The event that notifies attached delegate of a folder event.
    /// </summary>
//    event OnFolderEvent onFolderEvent;

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
    bool ContainsFolder(string name);
    /// <summary>
    /// Queries whether or not the folder contains a file witht the given local name.
    /// </summary>
    /// <returns>The file async.</returns>
    /// <param name="name">Name.</param>
    bool ContainsFile(string name);
    /// <summary>
    /// Gets a subfolder within this directory. The access response describes 
    /// the action taken for acquiring the folder.
    /// +
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="accessResponse"></param>
    IFolder GetFolder(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing);
    /// <summary>
    /// Creates a new file within this folder. The access response describes
    /// the action taken for acquiring the file.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="collisionResponse"></param>
    /// <returns></returns>
    IFile GetFile(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing);
    /// <summary>
    /// Queries a list of all the folders (not files) that are in the current directory.
    /// </summary>
    /// <returns></returns>
    List<IFolder> GetFolderList();
    /// <summary>
    /// Queries a list of all the files (not folders) that are in the current directory.
    /// </summary>
    /// <returns>The file list.</returns>
    List<IFile> GetFileList();
    /// <summary>
    /// Queries whether or not the folder exists.
    /// </summary>
    bool Exists();
    /// <summary>
    /// Creates this IFolder if it does not already exist.
    /// </summary>
    bool Create();
    /// <summary>
    /// Deletes the folder and all of its content. Note: THIS IS DANGEROUS!
    /// </summary>
    void Delete();
  }

  public enum EFileAccessResponse {
    CreateIfMissing,
    FailIfMissing,

    ReplaceIfExists,
    FailIfExists,
  }
}

