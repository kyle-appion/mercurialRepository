namespace ION.IOS.App {

  using System;

  using UIKit;
  using Foundation;

  using ION.Core.App;

  public class IOSPlatformInfo : IPlatformInfo {
    public string manufacturer { get; set; }
    public string deviceName { get; set; }
    public string model { get; set; }
    public string version { get; set; }
    public string api { get; set; }
    public string chipset { get; set; }

    public IOSPlatformInfo() {
      manufacturer = "Apple Inc.";
      deviceName = UIDevice.CurrentDevice.Model;
      model = UIDevice.CurrentDevice.LocalizedModel;
      version = UIDevice.CurrentDevice.SystemVersion;
      api = UIDevice.CurrentDevice.SystemName;
      chipset = "N/A";
    }
  }
}

