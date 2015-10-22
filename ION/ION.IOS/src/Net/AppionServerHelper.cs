namespace ION.IOS.Net {
  
  using System;
  using System.IO;
  using System.Reflection;
  using System.Runtime.InteropServices;

  using Foundation;
  using UIKit;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using ION.Core.IO;

  public partial class AppionServerHelper {
    public partial class Email { 
      public const string APPION_SUPPORT = "ahodder@appioninc.com";//"appsupport@appiontools.com";

      /// <summary>
      /// Creates a json file describing the current platform.
      /// </summary>
      /// <returns>The platform description file.</returns>
      /// <param name="fm">Fm.</param>
      /// <exception cref="IOException">If the file could not be created.</exception>
      public static IFile CreatePlatformDescriptionFile(IFileManager fm) {
        var ass = Assembly.GetExecutingAssembly();
        var obj = new JObject();

        obj["host_manufacturer"] = "Apple Inc.";
        obj["host_device"] = UIDevice.CurrentDevice.SystemName;
        obj["host_version"] = UIDevice.CurrentDevice.SystemVersion;
        obj["ion_version"] = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

        ION.Core.Util.Log.D("AppionServiceHelper.Email", obj.ToString());

        var tempFile = fm.CreateTemporaryFile("Platform Description");
        using (var w = new StreamWriter(tempFile.OpenForWriting())) {
          w.Write(obj.ToString());
        }

        return tempFile;
      }
    }
  }
}

