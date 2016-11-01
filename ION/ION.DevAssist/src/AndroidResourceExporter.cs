namespace ION.DevAssist {

	using System;
	using System.IO;
	using System.Xml;
	using System.Xml.Linq;

	public class AndroidResourceExporter {

		public static void ExportAndroidStrings(FileInfo[] files) {
			foreach (var fi in files) {
				ExportFile(fi);
			}
		}
	}
}
