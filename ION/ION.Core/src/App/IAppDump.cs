namespace ION.Core.App {

  using System;
  using System.Collections.Generic;

  /// <summary>
  /// A contract interface that is used to provide the foundation for an application dump.
  /// </summary>
  public interface IAppDump {
    string appVersion { get; }
    IDeviceInfo deviceInfo { get; }
    IPlatformInfo platformInfo { get; }
  }

  /// <summary>
  /// A contract interface that is used to provide the foundation for the application device info dump.
  /// </summary>
  public interface IDeviceInfo {
    string[] deviceSerials { get; }
  }

  /// <summary>
  /// A contract interface that is used to provide the foundation for the platform's info dump.
  /// </summary>
  public interface IPlatformInfo {
    string manufacturer { get; }
    string deviceName { get; }
    string model { get; }
    string version { get; }
    string api { get; }
    string chipset { get; }
    string wifiConnected  { get; }
    int batteryPercentage { get; }
    double freeMemory { get; }
  }

  /// <summary>
  /// A simple implementation of an app dump.
  /// </summary>
  public class BaseAppDump : IAppDump {
    public string appVersion { get; set; }
    public IDeviceInfo deviceInfo { get; set; }
    public IPlatformInfo platformInfo { get; set; }

    public BaseAppDump(IION ion, IPlatformInfo pi) {
      appVersion = ion.version;
      deviceInfo = new BaseDeviceInfo(ion);
      platformInfo = pi;
    }
  }

  /// <summary>
  /// A simple implementation of an device info dump.
  /// </summary>
  public class BaseDeviceInfo : IDeviceInfo {
    public string[] deviceSerials { get; set; }

    public BaseDeviceInfo(IION ion) {
      var list = new List<string>();

      foreach (var d in ion.deviceManager.knownDevices) {
        list.Add(d.serialNumber.ToString());
      }

      deviceSerials = list.ToArray();
    }
  }
}

