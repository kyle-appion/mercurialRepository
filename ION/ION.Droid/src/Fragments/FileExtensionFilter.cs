namespace ION.Droid.Fragments {

	using System.Collections.Generic;

	using ION.Core.IO;

	public class FileExtensionFilter {
		/// <summary>
		/// Whether or not the extensions that are present in the filter are to be included or excluded.
		/// </summary>
		/// <value><c>true</c> if inslusive; otherwise, <c>false</c>.</value>
		public bool inclusive { get; private set; }

		/// <summary>
		/// The list of extensions that are to be under consideration of the filter.
		/// </summary>
		private HashSet<string> extensions = new HashSet<string>();

		public FileExtensionFilter(bool inclusive) {
			this.inclusive = inclusive;
		}

		public FileExtensionFilter(bool inclusive, params string[] ext) {
			this.inclusive = inclusive;
			if (ext != null) {
				foreach (var e in ext) {
					extensions.Add(e.ToLower());
				}
			}
		}

		/// <summary>
		/// Queries whether or not the file matches the filter.
		/// </summary>
		/// <param name="file">File.</param>
		public bool Matches(IFile file) {
			if (extensions.Contains(file.extension.ToLower())) {
				return inclusive;
			} else {
				return !inclusive;
			}
		}

		/// <summary>
		/// Adds the extension to the filter.
		/// </summary>
		/// <param name="ext">Ext.</param>
		public void AddExtension(string ext) {
			extensions.Add(ext.ToLower());
		}

		/// <summary>
		/// Removes the extension from the filter.
		/// </summary>
		/// <param name="ext">Ext.</param>
		public void RemoveExtension(string ext) {
			extensions.Remove(ext);
		}
	}
}

