namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;
	using System.IO;

	public class IosResourceExporter {

		public static void ExportIosStrings(FileInfo[] files) {
			var strings = new Dictionary<string, string>();

			foreach (var fi in files) {
				ReadStringFile(fi, strings);
			}

			ExportStringsToCsv(strings, File.Open("IonLocalization.csv", FileMode.Create));
		}

		private static void ReadStringFile(FileInfo fi, Dictionary<string, string> strings) {
			var stream = File.Open(fi.FullName, FileMode.Open);

			using (var r = new StreamReader(stream)) {
				string line = null;
				var inComment = false;

				while ((line = r.ReadLine()) != null) {
					Console.WriteLine("Resolving line: " + line);
					if (string.IsNullOrEmpty(line)) {
						continue;
					}

					// Resolve any block comments.
					if (inComment) {
						if (line.Contains("*/")) {
							inComment = false;
						}
						continue;
					} else {
						if (line.Contains("/*")) {
							if (!line.Contains("*/")) {
								inComment = true;
							}
							continue;
						}
					}

					// Deal with the string.
					line = line.Replace("\"", "");
					line = line.Replace(";", "");

					var parts = line.Split(new char[] { '=' });

					strings[parts[0].Trim()] = parts[1].Trim();
				}
			}
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
