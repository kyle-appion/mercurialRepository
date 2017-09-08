namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml;
	using System.Xml.Linq;

	public class ResourceCompiler : ConsoleOperator {
		private const string SECTION_DEFINITION = "@SECTION";
		private const string EL_RESOURCES = "resources";
		private const string EL_STRING = "string";
		private const string ATT_NAME = "name";
		private const string IOS_COMMENT_TOKEN = "//";

		private FileInfo currentFile;
		private int currentLine;
		private int currentColumn;

		private StringResources.Section currentSection;

		private HashSet<string> sectionNames = new HashSet<string>();
		private Dictionary<string, StringResources.Section> androidSections = new Dictionary<string, StringResources.Section>();
		private Dictionary<string, StringResources.Section> iosSections = new Dictionary<string, StringResources.Section>();

		/// <summary>
		/// The current context that the resource compiler is running in.
		/// </summary>
		/// <param name="context">Context.</param>
		private Context context;

		public ResourceCompiler(Context context) {
			this.context = context;
		}

		public void AddAndroidResourcesFromPath(FileInfo fi) {
			Console.WriteLine("Resolving resources from: " + fi.FullName);
			currentFile = fi;

			var stream = File.Open(fi.FullName, FileMode.Open);
			var settings = new XmlReaderSettings();
			settings.IgnoreComments = false;
			using (var reader = XmlReader.Create(stream, settings)) {
				var doc = XDocument.Load(reader);

				foreach (var node in doc.DescendantNodes()) {
					var info = node as IXmlLineInfo;
					if (info != null && info.HasLineInfo()) {
						currentLine = info.LineNumber;
						currentColumn = info.LinePosition;
					}

					switch (node.NodeType) {
						case XmlNodeType.Comment:
							var comment = node as XComment;
							if (comment.Value.Trim().StartsWith(SECTION_DEFINITION)) {
								var sectionName = ParseSectionCommandLabel(comment.Value.Substring(SECTION_DEFINITION.Length));
								sectionNames.Add(sectionName);
								currentSection = new StringResources.Section() { name = sectionName };
								androidSections[sectionName] = currentSection;
							}
							break;
						case XmlNodeType.Element:
							var e = node as XElement;
							if (EL_STRING.Equals(e.Name.ToString())) {
								if (currentSection == null) {
									Error("Cannot compile string: string does not belong to a section");
								}
								var key = e.Attribute(ATT_NAME).Value.ToLower();
								InsertKeyValueIntoSection(currentSection, key, e.Value);
							}
							break;
					}
				}
			}
		}

		public void AddIosResourcesFromPath(FileInfo fi) {
			Console.WriteLine("Resolving resources from: " + fi.FullName);
			currentFile = fi;

			var stream = File.Open(fi.FullName, FileMode.Open);

			using (var r = new StreamReader(stream)) {
				string line = null;

				while ((line = r.ReadLine()) != null) {
					currentLine++;
					line = line.Trim();
					if (string.IsNullOrEmpty(line)) {
						continue;
					}

					if (line.StartsWith(IOS_COMMENT_TOKEN)) {
						line = line.Substring(2).Trim();
						if (line.StartsWith(SECTION_DEFINITION)) {
							var sectionName = ParseSectionCommandLabel(line);
							currentSection = new StringResources.Section() { name = sectionName };
							sectionNames.Add(sectionName);
							iosSections[sectionName] = currentSection;
							Console.WriteLine("EnteredSection: " + sectionName);
						}
					} else {
						// Deal with the string.
						line = line.Replace("\"", "");
						line = line.Replace(";", "");

						var parts = line.Split(new char[] { '=' });
						var key = parts[0].Trim().ToLower();
						var value = parts[1].Trim();

						InsertKeyValueIntoSection(currentSection, key, value);
					}
				}
			}
		}

		/// <summary>
		/// Exports the resources to the given excel file.
		/// </summary>
		/// <param name="fi">Fi.</param>
		public void ExportToExcel(FileInfo fi) {
			var sections = VerifyResourceIntegrity();
		}

		/// <summary>
		/// Inserts the value into the section at the given key.
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		private void InsertKeyValueIntoSection(StringResources.Section section, string key, string value) {
			if (section.ContainsKey(key)) {
				NL(2);
				// We have to handle a key collision.
				var options = new string[] { section.resources[key], value };
				WL("File => " + currentFile.FullName + " line: " + currentLine + " column: " + currentColumn);
				NL();
				WL("ResourceCompiler has detected a duplicate key in section: " + section.name);
				var msg = "Please select which value you would prefer for key { " + key + " in section '" + section.name + "'}";
				var selection = context.BlockForUserInput(msg, options);
				value = options[selection];
			}

			section.resources[key] = value;
			WL("Key: " + key + " value: " + value);
		}

		/// <summary>
		/// Ensures that both platforms are using the same resources. If a collision occurs, then we will ask the user for
		/// clarification.
		/// </summary>
		private List<StringResources.Section> VerifyResourceIntegrity() {
			var ret = new List<StringResources.Section>();

			foreach (var name in sectionNames) {
				var sections = new List<StringResources.Section>();

				if (androidSections.ContainsKey(name)) {
					sections.Add(androidSections[name]);
				}

				if (iosSections.ContainsKey(name)) {
					sections.Add(iosSections[name]);
				}

				ret.Add(CombineSections(sections.ToArray()));
			}

			return ret;
		}

		/// <summary>
		/// Compares the content of all of the sections to ensure that the resources are used on all platforms.
		/// </summary>
		/// <param name="sections">Sections.</param>
		private StringResources.Section CombineSections(StringResources.Section[] sections) {
			Clear();

			if (sections == null || sections.Length <= 0) {
				Error("Found and empty section when merging sections");
			}

			var ret = new StringResources.Section() { name = sections[0].name };

			var keys = new HashSet<string>();
			foreach (var section in sections) {
				// Generate a union of all the keys in the sections.
				foreach (var key in section.resources.Keys) {
					keys.Add(key);
				}
			}

			foreach (var key in keys) {
				Clear();
				var options = new List<string>();
				var ib = new InputBuilder();

				var i = 0;
				foreach (var section in sections) {
					if (section.ContainsKey(key)) {
						options.Add(section.resources[key]);
						ib.AddAction(i++ + "", section.resources[key], () => {
							ret.resources[key] = section.resources[key];
						});
					}
				}

				var similar = true;
				for (int j = 1; j < options.Count; j++) {
					if (!sections[0].ContainsKey(key) || !sections[j].ContainsKey(key) ||
					    (!sections[0].resources[key].Equals(sections[j].resources[key]))) {
						similar = false;
						break;
					}
				}

				if (!similar) {
					// We need to ask the user which option to use.
					// We need to build the actions list.
					ib.AddAction(i++ + "", "Use keep both (do this only when there is a distinction between platform strings)", () => {
						WL("Failed to keep both");
					});

					WL("Found multiple values for key [" + key + "]");
					context.RequestListItemSelection("Please select the value that the key should be.", ib);
				}
			}

			return ret;
		}




	}
}
