namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml;
	using System.Xml.Linq;

	public class AndroidResourceExporter {

		private const string EL_STRING = "string";
		private const string EL_NAME = "name";

		public static void ExportAndroidStrings(FileInfo[] files) {

			var strings = new Dictionary<string, string>();

			foreach (var fi in files) {
				ReadStringFile(fi, strings);
			}

			ExportStringsToCsv(strings, File.Open("AndroidLocalization.csv", FileMode.Create));
		}

		/// <summary>
		/// Reads the android file into the strings dictionary.
		/// </summary>
		/// <param name="fi">Fi.</param>
		/// <param name="strings">Strings.</param>
		private static void ReadStringFile(FileInfo fi, Dictionary<string, string> strings) {
			var stream = File.Open(fi.FullName, FileMode.Open);
			var root = XElement.Load(XmlReader.Create(stream));

			foreach (var element in root.Elements()) {
				if (EL_STRING.Equals(element.Name.LocalName)) {
					var tu = ParseString(element);
					strings[tu.Item1] = tu.Item2;
				}
			}
		}

		/// <summary>
		/// Parses a single string element.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="e">E.</param>
		private static Tuple<string, string> ParseString(XElement e) {
			return new Tuple<string, string>(e.Attribute(EL_NAME).Value, e.Value);
		}

		/// <summary>
		/// Exports the strings.
		/// </summary>
		/// <param name="strings">Strings.</param>
		private static void ExportStringsToCsv(Dictionary<string, string> strings, Stream outStream) {
			var csv = new Csv();

			foreach (var key in strings.Keys) {
				var row = new Csv.Row();
				csv.AddRow(row);

				row.AddCol(key);
				row.AddCol(strings[key]);
			}

			csv.Export(outStream);
		}
	}
}
