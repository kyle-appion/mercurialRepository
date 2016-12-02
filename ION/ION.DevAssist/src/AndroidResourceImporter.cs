namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml;
	using System.Xml.Linq;

	public class AndroidResourceImporter : ResourceImporter {
		private const string EL_RESOURCES = "resources";
		private const string EL_STRING = "string";
		private const string ATT_NAME = "name";

		private FileInfo currentFile;
		private int currentLine;
		private int currentColumn;

		public AndroidResourceImporter(ConsoleOperator console) : base(console) {
		}

		public override void Import(StringResources resources, FileInfo fi) {
			console.WL("Resolving resources from: " + fi.FullName);

			var stream = File.Open(fi.FullName, FileMode.Open);
			var settings = new XmlReaderSettings();
			settings.IgnoreComments = false;
			using (var reader = XmlReader.Create(stream, settings)) {
				var doc = XDocument.Load(reader);

				var currentSection = "";
				foreach (var node in doc.DescendantNodes()) {
					var info = node as IXmlLineInfo;
					if (info != null && info.HasLineInfo()) {
						currentLine = info.LineNumber;
						currentColumn = info.LinePosition;
					}

					switch (node.NodeType) {
						case XmlNodeType.Comment:
							var comment = (node as XComment).Value.Trim();
							if (IsSectionDefinition(comment)) {
								currentSection = ParseSectionCommandLabel(comment);
							}
							break;
						case XmlNodeType.Element:
							var e = node as XElement;
							if (EL_STRING.Equals(e.Name.ToString())) {
								if (currentSection == null) {
									Error("Cannot compile string: string does not belong to a section");
								}
								var key = e.Attribute(ATT_NAME).Value.ToLower();
								InsertKeyValueIntoSection(resources, currentSection, key, e.Value);
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// Inserts the key value into the given string resources.
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		private void InsertKeyValueIntoSection(StringResources res, string section, string key, string value) {
			if (res.HasKey(section, key)) {
				// The same resource file has duplicate key. Select which key you want.
				var ib = new InputBuilder();
				ib.AddAction(key, value, () => {
					res.AddValue(section, key, value);
				});

				ib.AddAction(key, res[section].resources[key], () => {
					// No action required.
				});

				console.NL(2);
				console.WL("File => " + currentFile.FullName + " line: " + currentLine + " column: " + currentColumn);
				console.NL();
				console.WL("ResourceCompiler has detected a duplicate key in section: " + section);
				console.RequestListItemSelection("Please select which value you would prefer for key {" + key + " in section '" + section + "'}", ib);
			} else {
				res.AddValue(section, key, value);
			}
		}
	}
}
