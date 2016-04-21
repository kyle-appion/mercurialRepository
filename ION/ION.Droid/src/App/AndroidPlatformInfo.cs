namespace ION.Droid.App {

  using System;

  using Android.OS;

  using ION.Core.App;

  public class AndroidPlatformInfo : IPlatformInfo {
    public string manufacturer { get; set; }
    public string deviceName { get; set; }
    public string model { get; set; }
    public string version { get; set; }
    public string api { get; set; }
    public string chipset { get; set; }

    public AndroidPlatformInfo(AndroidION ion) {
      manufacturer = Build.Manufacturer;
      deviceName = Build.Product;
      model = Build.Model;
      version = Build.VERSION.Release;
      api = Build.VERSION.SdkInt + "";
      chipset = Build.Board;
    }
  }
}

