namespace ION.Droid.IO {

  using ION.Core.IO;

  /// <summary>
  /// This class continas extension methods for files.
  /// </summary>
  public static class FileExtensions {
		public const string EXT_EXCEL = ".xlsx";
    public const string EXT_CSV = ".csv";
    public const string EXT_PDF = ".pdf";

    /// <summary>
    /// Queries the icon for the given file.
    /// </summary>
    /// <returns>The icon.</returns>
    /// <param name="file">File.</param>
    public static int GetIcon(this IFile file) {
			switch (file.extension.ToLower()) {
        case EXT_CSV:
				case EXT_EXCEL:
				  return Resource.Drawable.ic_spreadsheet;
        case EXT_PDF:
          return Resource.Drawable.ic_pdf;
        default:
          return Resource.Drawable.ic_logo_appion_a;
      }
    }

    /// <summary>
    /// Queries the icon for the given folder.
    /// </summary>
    /// <returns>The icon.</returns>
    /// <param name="folder">Folder.</param>
    public static int GetIcon(this IFolder folder) {
      return Resource.Drawable.ic_folder;
    }
  }
}

