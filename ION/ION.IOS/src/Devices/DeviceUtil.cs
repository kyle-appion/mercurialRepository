namespace ION.IOS.Devices {

  using System;

  using UIKit;

  using ION.Core.Devices;
  using ION.Core.Util;

  using ION.IOS.Util;

  /// <summary>
  /// The iOS native device model extensions.
  /// </summary>
  public static class DeviceModelExtensions {
    /// <summary>
    /// Queries the localized string name of the given device model.
    /// </summary>
    /// <returns>The type string.</returns>
    /// <param name="deviceModel">Device model.</param>
    public static string GetTypeString(this EDeviceModel deviceModel) {
      switch (deviceModel) {
        case EDeviceModel.AV760:
          return Strings.Device.Model.AV760.FromResources();
        case EDeviceModel.HT:
          return Strings.Device.Model.HT.FromResources();
        case EDeviceModel.P300:
          return Strings.Device.Model.P300.FromResources(); 
        case EDeviceModel.P500:
          return Strings.Device.Model.P500.FromResources();
        case EDeviceModel.P800:
          return Strings.Device.Model.P800.FromResources();
        case EDeviceModel.PT800:
          return Strings.Device.Model.PT800.FromResources();
        case EDeviceModel._3XTM:
          return Strings.Device.Model._3XTM.FromResources();
        default:
          return Strings.Device.Model.UNKNOWN.FromResources();
      }
    }
  } // End DeviceModelExtensions

  public static partial class DeviceUtil {
    /// <summary>
    /// Constructs a UIImage for the given device model. By default the
    /// method will cache the UIImage as to not rape the HDD, however,
    /// you can opt out if you are a crazy person.
    /// </summary>
    /// <returns>The user interface image from device model.</returns>
    /// <param name="deviceModel">Device model.</param>
    public static UIImage GetUIImageFromDeviceModel(this EDeviceModel deviceModel, bool enforeCaching = true) {
      switch (deviceModel) {
        case EDeviceModel.P300:
          return UIImage.FromBundle("ic_render_gauge_p300");
        case EDeviceModel.P500:
          return UIImage.FromBundle("ic_render_gauge_p500");
        case EDeviceModel.P800:
          return UIImage.FromBundle("ic_render_gauge_p800");
        case EDeviceModel.AV760:
          return UIImage.FromBundle("ic_render_gauge_av760");
        default:
          return UIImage.FromBundle("ic_missing.png");
      }
    }
  } // End DeviceUtil
}
