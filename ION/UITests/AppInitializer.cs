using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ION.UITests {
	public class AppInitializer {
		public static IApp StartApp (Platform platform) {
			if (platform == Platform.Android) {
				return ConfigureApp.Android
/*
					                 .DeviceSerial("09069baf")
					                 .ApkFile("")
*/
					                 .StartApp();
			} else {
				return ConfigureApp.iOS.StartApp ();
			}
		}
	}
}

