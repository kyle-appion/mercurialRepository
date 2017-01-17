namespace ION.Droid {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;

  using Android.Content;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.IO;

  public class AndroidFileManager : IFileManager {

		public bool isInitialized { get { return true; } }
    /// <summary>
    /// The context that we will pull assets from.
    /// </summary>
    /// <value>The context.</value>
    private Context context { get; set; }

    public AndroidFileManager(Context context) {
      this.context = context;
    }

    // Overridden from IFileManager
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() { success = true });
    }

    // Overridden from IFileManager
    public void Dispose() {
    }

    // Overridden from IFileManager
    public IFolder GetAssetDirectory() {
      return new AndroidAssetFolder(context, "");
    }

    // Overridden from IFileManager
    public IFolder GetApplicationInternalDirectory() {
      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(context.GetFileStreamPath("").AbsolutePath));
    }

    // Overridden from IFileManager
    public IFolder GetApplicationExternalDirectory() {
      return (IFolder)new IONFolder(new System.IO.DirectoryInfo(context.GetExternalFilesDir("").AbsolutePath));
    }

    // Overridden from IFileManager
    public IFile CreateTemporaryFile(string name, EFileAccessResponse accessResponse = EFileAccessResponse.ReplaceIfExists) {
      var jf = Java.IO.File.CreateTempFile(name, "", new Java.IO.File(GetApplicationExternalDirectory().fullPath));
      return new IONFile(new FileInfo(jf.AbsolutePath));
/*
      var tempFileName = System.IO.Path.GetTempPath() + name;
      return new IONFile(new FileInfo(tempFileName));
*/
    }
  }

  internal class AndroidAssetFolder : IFolder {
    // Overridden from IFolder
    public string fullPath { get; set; }
    // Overridden from IFolder
    public string name { 
      get {
        var parts = fullPath.Split("/".ToCharArray());
        if (parts.Length <= 1) {
          return "";
        } else {
          return parts[parts.Length - 1];
        }
      }
    }

    /// <summary>
    /// The context to pull assets from.
    /// </summary>
    /// <value>The context.</value>
    private Context context { get; set; }
    /// <summary>
    /// The list of subfolders that are within this folder.
    /// </summary>
    /// <value>The subfolders.</value>
    private List<AndroidAssetFolder> subfolders { get; set; }
    /// <summary>
    /// The list of files within this folder.
    /// </summary>
    /// <value>The files.</value>
    private List<AndroidAssetFile> files { get; set; }

    private AndroidAssetFolder lastSearchedFolder { get; set; }
    private AndroidAssetFile lastSearchedFile { get; set; }

    public AndroidAssetFolder(Context context, string fullPath) {
      this.context = context;
      this.fullPath = fullPath;
    }

    public AndroidAssetFolder(Context context, AndroidAssetFolder parent, string name) {
      this.context = context;
      if (parent.fullPath.Equals("")) {
        this.fullPath = name;
      } else {
        this.fullPath = parent.fullPath + "/" + name;
      }
    }

    /// <summary>
    /// Not supported by android.
    /// </summary>
    /// <returns>The folder async.</returns>
    /// <param name="name">Name.</param>
    // Overridden from IFolder
    public bool ContainsFolder(string name) {
      throw new IOException("Not supported");
    }

    // Overridden from IFile
    public bool ContainsFile(string name) {
      foreach (var file in GetFileList()) {
        if (file.name.Equals(name)) {
          lastSearchedFile = file as AndroidAssetFile;
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Gets the folder with the given name. If the folder doesn't exist, we will throw an exception.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="accessResponse"></param>
    /// <returns>The folder.</returns>
    /// <param name="accessReponse">Access reponse.</param>
    // Overridden from IFolder
    public IFolder GetFolder(string name, EFileAccessResponse accessReponse = EFileAccessResponse.FailIfMissing) {
      return new AndroidAssetFolder(context, this, name);
    }

    /// <summary>
    /// Gets the file with the given name. If the file doesn't exists, we will throw an exception.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="collisionResponse"></param>
    /// <returns></returns>
    /// <param name="accessResponse">Access response.</param>
    // Overridden from IFolder
    public IFile GetFile(string name, EFileAccessResponse accessResponse = EFileAccessResponse.FailIfMissing) {
      if (name.Equals(lastSearchedFile?.name)) {
        return lastSearchedFile;
      }

      foreach (var file in GetFileList()) {
        if (file.name.Equals(name)) {
          lastSearchedFile = file as AndroidAssetFile;
          return file;
        }
      }

      throw new IOException("Cannot find file " + fullPath + "/" + name);
    }

    /// <summary>
    /// Not supported by android.
    /// </summary>
    /// <returns></returns>
    // Overridden from IFolder
    public List<IFolder> GetFolderList() {
      throw new IOException("Not supported");
    }

    // Overridden from IFile
    public List<IFile> GetFileList() {
      var ret = new List<IFile>();

      foreach (var path in context.Assets.List(fullPath)) {
        ret.Add(new AndroidAssetFile(context, this, path));
      }

      return ret;
    }

    // Overridden from IFolder.
    public bool Exists() {
			return new Java.IO.File(fullPath).Exists();
    }

    /// <summary>
    /// Not supported
    /// </summary>
    // Overridden from IFolder
    public bool Create() {
      throw new IOException("Not supported");
    }

    /// <summary>
    /// Not supported
    /// </summary>
    // Overridden from IFolder
    public void Delete() {
      throw new IOException("Not supported");
    }
  }

  internal class AndroidAssetFile : IFile {
    // Overridden from IFile
    public string fullPath { 
      get {
        return folder.fullPath + "/" + name;
      }
    }
    // Overridden from IFile
    public string name { get; set; }

    public string extension {
      get {
        var parts = name.Split(new char[] { '.' });

        if (parts.Length > 0) {
          return "." + parts[parts.Length - 1];
        } else {
          return "";
        }
      }
    }

    /// <summary>
    /// The context to pull assets from.
    /// </summary>
    /// <value>The context.</value>
    private Context context { get; set; }
    /// <summary>
    /// The folder that the file resides in.
    /// </summary>
    /// <value>The folder.</value>
    private IFolder folder { get; set; }

    public AndroidAssetFile(Context context, AndroidAssetFolder folder, string name) {
      this.context = context;
      this.folder = folder;
      this.name = name;
    }

    // Overridden from IFile
    public long GetSize() {
      return -1;
    }

    // Overridden from IFile
    public Stream OpenForReading() {
      return context.Assets.Open(fullPath);
    }

    /// <summary>
    /// Not supported
    /// </summary>
    /// <returns>The for writing.</returns>
    // Overridden from IFile
    public Stream OpenForWriting(bool append = false) {
      throw new IOException("Cannot open AndroidAssetFile for writing");
    }

    // Overridden from IFile
    public bool Exists() {
      return folder.ContainsFile(name);
    }

    /// <summary>
    /// Not supported
    /// </summary>
    /// <returns>True if the file was created (or existed), false otherwise.</returns>
    // Overridden form IFile
    public bool Create() {
      throw new IOException("Cannot create AndroidAssetFile");
    }

    /// <summary>
    /// Not supported
    /// </summary>
    // Overridden from IFile
    public void Delete() {
      throw new IOException("Cannot delege AndroidAssetFile");
    }
  }
}

