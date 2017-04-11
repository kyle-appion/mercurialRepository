namespace ION.CoreExtensions.IO {

  using System;
  using System.IO;

  using ION.Core.IO;

  public static class IONFolderExtensions {
    /// <summary>
    /// Copies the given folder and its contents to dest path.
    /// </summary>
    /// <param name="dir">Dir.</param>
    /// <param name="destPath">Destination path.</param>
    /// <param name="copySubDirs">If set to <c>true</c> copy sub dirs.</param>
    public static void CopyToOrThrow(this IFolder dir, string destPath, bool copySubDirs) {
      CopyDirectoryOrThrow(dir.fullPath, destPath, copySubDirs);
    }

    /// <summary>
    /// Copies the directory at source path to dest path.
    /// </summary>
    /// <param name="sourcePath">Source path.</param>
    /// <param name="destPath">Destination path.</param>
    /// <param name="copySubDirs">If set to <c>true</c> copy sub dirs.</param>
    public static void CopyDirectoryOrThrow(string sourcePath, string destPath, bool copySubDirs) {
      var dir = new DirectoryInfo(sourcePath);

      if (!dir.Exists) {
        throw new DirectoryNotFoundException(string.Format("Source directory[{0}] does not exist/could not be found. Copy not performed", sourcePath));
      }

      var dirs = dir.GetDirectories();

      // Ensure that the target directory exists
      if (!Directory.Exists(destPath)) {
        Directory.CreateDirectory(destPath);
      }

      // Copy the files to the new directory
      var files = dir.GetFiles();
      foreach (var file in files) {
        var tmpPath = Path.Combine(destPath, file.Name);
        file.CopyTo(tmpPath);
      }

      // Copy the subdirectories
      if (copySubDirs) {
        foreach (var sdir in dirs) {
          var tmpPath = Path.Combine(destPath, sdir.Name);
          CopyDirectoryOrThrow(sdir.FullName, tmpPath, copySubDirs);
        }
      }
    }
  }
}
