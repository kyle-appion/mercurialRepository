namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;
	using System.IO;

	public class Context : ConsoleOperator {
		private static readonly ConsoleKey[] ESCAPE_CODES = { ConsoleKey.Escape, ConsoleKey.Q };
		/// <summary>
		/// The collection of all of the hardcoded android string files. 
		/// </summary>
		private static readonly FileInfo[] ANDROID_STRING_FILES = {
			new FileInfo("ION.Droid/Resources/values/Strings.xml"),
//			new FileInfo("ION.Droid/Resources/values/DELETE_ME.xml"),
		};

		private static readonly FileInfo[] IOS_STRING_FILES = {
			new FileInfo("ION.IOS/Resources/en.lproj/Localizable.strings"),
		};

		public static void Main(string[] args) {
			foreach (var arg in args) {
				Console.Write(arg + ", ");
			}
			var mc = new Context();
			mc.Compile();
		}

		private ResourceCompiler resourceCompiler;

		private Context() {
			resourceCompiler = new ResourceCompiler(this);
		}

		/// <summary>
		/// Compiles Android and iOS ensuring continuty and ease of translation.
		/// </summary>
		private void Compile() {
			Console.WriteLine("Current directory: " + Directory.GetCurrentDirectory());

			foreach (var fi in ANDROID_STRING_FILES) {
				resourceCompiler.AddAndroidResourcesFromPath(fi);
			}

			foreach (var fi in IOS_STRING_FILES) {
				resourceCompiler.AddIosResourcesFromPath(fi);
			}

//			AndroidResourceExporter.ExportAndroidStrings(androidFiles);
//			IosResourceExporter.ExportIosStrings(iosFiles);

			var excel = new FileInfo("LocalizedStrings.xls");
			resourceCompiler.ExportToExcel(excel);
		}

		/// <summary>
		/// Blocks the program until the user provides input. Returns the index of the option that was selected.
		/// </summary>
		/// <returns>The for user input.</returns>
		/// <param name="message">Message.</param>
		/// <param name="options">Options.</param>
		public int BlockForUserInput(string message, string[] options) {
			while (true) {
				Console.WriteLine(message);
				Console.WriteLine("");
				for (int i = 0; i < options.Length; i++) {
					Console.Write("[" + i + "]\t");
					Console.WriteLine(options[i]);
				}

				var key = Console.ReadKey();
				if (key == null) {
					continue;
				}

				if (IsKeyAnEscapeCode(key.Key)) {
					Quit("Terminated program by user request", -128); // User canceled error
				} 

				var val = key.KeyChar - '0';
				if (val < 0 || val >= options.Length) {
					Console.WriteLine("\nPlease enter a valid option");
				} else {
					BS();
					return val;
				}
			}
		}

		/// <summary>
		/// Queries whether or not the given key is an escape key.
		/// </summary>
		/// <param name="key">Key.</param>
		public bool IsKeyAnEscapeCode(ConsoleKey key) {
			foreach (var l in ESCAPE_CODES) {
				if (key == l) {
					return true;
				}
			}

			return false;
		}
	}

	public class InputBuilder {
		public Dictionary<string, Tuple<string, Action>> actions = new Dictionary<string, Tuple<string, Action>>();

		public bool AddAction(string key, string message, Action action) {
			if (!actions.ContainsKey(key)) {
				actions.Add(key, new Tuple<string, Action>(message, action));
        return true;
      } else {
				return false;
			}
		}

		public Tuple<string, Action> GetAction(string key) {
			return actions[key];
		}
	}
}
