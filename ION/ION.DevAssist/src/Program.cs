namespace ION.DevAssist {

	using System;
	using System.IO;

	class MainClass {
		public static void Main(string[] args) {

			foreach (var arg in args) {
				Console.Write(arg + ", ");
			}

			Console.WriteLine("Current directory: " + Directory.GetCurrentDirectory());

			var androidFiles = new FileInfo[] {
				new FileInfo("ION.Droid/Resources/values/Strings.xml"),
				new FileInfo("ION.Droid/Resources/values/Preference_Values.xml"),
			};

			var iosFiles = new FileInfo[] {
				new FileInfo("ION.IOS/Resources/en.lproj/Localizable.strings"),
			};

			AndroidResourceExporter.ExportAndroidStrings(androidFiles);
			IosResourceExporter.ExportIosStrings(iosFiles);
		}
	}
}
