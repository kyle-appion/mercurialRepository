﻿namespace ION.IOS {

	using UIKit;

	using Appion.Commons.Util;

	using ION.IOS.Util;

  public class Application {
    // This is the main entry point of the application.
    static void Main(string[] args) {
      Log.printer = new LogPrinter();
      // if you want to use a different Application Delegate class from "AppDelegate"
      // you can specify it here.
       UIApplication.Main(args, null, "AppDelegate");
    }
  }
}
