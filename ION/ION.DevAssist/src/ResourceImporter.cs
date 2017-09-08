namespace ION.DevAssist {

	using System;
	using System.IO;

	public abstract class ResourceImporter {
		private const string SECTION_DEFINITION = "@SECTION";

		protected ConsoleOperator console;
		protected FileInfo currentFile;
		protected int currentLine;
		protected int currentColumn;

		public ResourceImporter(ConsoleOperator console) {
			this.console = console;
		}

		/// <summary>
		/// Imports the resources from the given file into the string resources.
		/// </summary>
		/// <param name="resources">Resources.</param>
		/// <param name="fi">Fi.</param>
		public abstract void Import(StringResources resources, FileInfo fi);

		/// <summary>
		/// Queries whether or not the given string is a section definition.
		/// </summary>
		/// <returns><c>true</c>, if section definition was ised, <c>false</c> otherwise.</returns>
		/// <param name="str">String.</param>
		public bool IsSectionDefinition(string str) {
			return str.StartsWith(SECTION_DEFINITION);
		}

		/// <summary>
		/// Parses the command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="value">Value.</param>
		public string ParseSectionCommandLabel(string value) {
			value = value.Substring(SECTION_DEFINITION.Length);
			var oparen = value.IndexOf('(');
			var cparen = value.IndexOf(')');

			if (oparen < 0) {
				Error("Section definition is missing opening parent", currentFile, currentLine, currentColumn);
			} else if (cparen < 0) {
				Error("Section definition is missing close parent", currentFile, currentLine, currentColumn);
			}

			return value.Substring(oparen + 1, Math.Max(cparen - (oparen + 1), 0));
		}

		/// <summary>
		/// Crashes the program
		/// </summary>
		/// <param name="message">Message.</param>
		protected void Error(string message) {
			Error(message, currentFile, currentLine, currentColumn);
		}

		/// <summary>
		/// Crashes the program.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="fi">Fi.</param>
		/// <param name="line">Line.</param>
		/// <param name="column">Column.</param>
		protected void Error(string message, FileInfo fi, int line, int column) {
			console.WL("Resource Compiler Error @" + fi.FullName + "{" + line + ":" + column + "}");
			console.Quit(message, 8); // Format error
		}
	}
}
