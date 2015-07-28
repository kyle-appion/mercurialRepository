using System;

using UIKit;

using ION.Core.Devices;
using ION.Core.Util;

using ION.IOS.Util;

/// <summary>
/// NOTE:
/// 
/// THIS CLASS IS DEDICATED TO PROVIDED IOS EXLCUSIVE EXTENSIONS TO THE DEVICE UTIL
/// CLASS IN THE CORE LIBRARY. AS SUCH, LEAVE THE FUCKING NAMESPACE ALONE!
/// </summary>
namespace ION.Core.Devices {
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
        case EDeviceModel.AV760: {
          return Strings.Device.Model.AV760.FromResources();
        }
        case EDeviceModel.HT: {
          return Strings.Device.Model.HT.FromResources();
        }
        case EDeviceModel.P300: {
          return Strings.Device.Model.P300.FromResources(); 
        }
        case EDeviceModel.P500: {
          return Strings.Device.Model.P500.FromResources();
        }
        case EDeviceModel.P800: {
          return Strings.Device.Model.P800.FromResources();
        }
        default:{
          return Strings.Device.Model.UNKNOWN.FromResources();
        }
      }
    }
  } // End DeviceModelExtensions

  public partial class DeviceUtil {
    /// <summary>
    /// Constructs a UIImage for the given device model. By default the
    /// method will cache the UIImage as to not rape the HDD, however,
    /// you can opt out if you are a crazy person.
    /// </summary>
    /// <returns>The user interface image from device model.</returns>
    /// <param name="deviceModel">Device model.</param>
    public static UIImage GetUIImageFromDeviceModel(EDeviceModel deviceModel, bool enforeCaching = true) {
      // TODO ahodder@appioninc.com: Implement caching
      switch (deviceModel) {
        case EDeviceModel.P300: {
          return UIImage.FromBundle("Icons/ic_render_gaugep300.png");
        }
        case EDeviceModel.P500: {
          return UIImage.FromBundle("Icons/ic_render_gaugep500.png");
        }
        case EDeviceModel.P800: {
          return UIImage.FromBundle("Icons/ic_render_gaugep800.png");
        }
        case EDeviceModel.AV760: {
          return UIImage.FromBundle("Icons/ic_render_gaugeav760.png");
        }
        default: {
          return UIImage.FromBundle("Icons/ic_logo_appiondefault.png");
        }
      }
    }
  } // End DeviceUtil
}
  
