namespace ION.IOS {

	using UIKit;

	using Appion.Commons.Util;

	using ION.IOS.Util;
	using System;
	using System.Threading.Tasks;
	using System.Diagnostics;

	public class Application {
    // This is the main entry point of the application.
    static void Main(string[] args) {
      var l = new IosLogger();
      Log.logger = l;
      // if you want to use a different Application Delegate class from "AppDelegate"
      // you can specify it here.
      try {
       UIApplication.Main(args, null, "AppDelegate");
      } catch (Exception e) {
        Log.E("Main.cs", "Some nasty shit just happend", e);
      }
       
			 AppDomain.CurrentDomain.UnhandledException += (o, e) => 
			{ 
			    #if DEBUG
			        Console.WriteLine(e);
			        //Debugger.Break();
			    #endif
			};
			
			TaskScheduler.UnobservedTaskException += (o, e) => 
			{
			    #if DEBUG
			        Console.WriteLine(e);
			        //Debugger.Break();
			    #endif
			};      
       
    }
  }
}
