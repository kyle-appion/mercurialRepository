/// <summary>
/// This file provides utils and extensions of iOS native strings.
/// </summary>


using System;

using Foundation;

namespace ION.IOS.Util {
  /// <summary>
  /// A utility class that will provide all of the keys used for string
  /// lookups. Essentially, we are trying to aileive the fact that Android's
  /// resource management is miles ahead of iOS and iOS can't really, truly
  /// safely access resource strings.
  /// </summary>
  public class Strings {
    public class Device : Strings {
      public const string
      NAME = "device_name",
      SERIAL_NUMBER = "device_serial_number",
      TYPE = "device_type"
      ;

      public class Model : Device {
        public const string
        _3XTM = "device_model_3xtm",
        AV760 = "device_model_av760",
        HT = "device_model_ht",
        P300 = "device_model_p300",
        P500 = "device_model_p500",
        P800 = "device_model_p800",
        UNKNOWN = "device_model_unknown"
        ;
      } // End Strings.Device.Model
    } // End Strings.Device
  } // End Strings

  /// <summary>
  /// This class provides extensions for the string class that allow
  /// us to easily access iOS locized strings using a string itself
  /// as the key.
  /// </summary>
  public static class IOSStringss {

    /// <summary>
    /// Uses the string as a key input to retrieve a localized string
    /// from resources.
    /// </summary>
    /// <returns>The resources.</returns>
    /// <param name="key">Key.</param>
    public static string FromResources(this string key) {
      return NSBundle.MainBundle.LocalizedString(key, "");
    }
  }
}

