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
  public static class Strings {
    public static readonly String
    ACTIONS = "actions".FromResources(),
    CANCEL = "cancel".FromResources(),
    HELP = "help".FromResources(),
    OK = "ok".FromResources(),
    RENAME = "rename".FromResources(),
    UNKNOWN = "unknown"
    ;

    public static class Device {
      public static readonly string
      AVAILABLE = "device_available".FromResources(),
      CONNECTED = "device_connected".FromResources(),
      DISCONNECT = "device_disconnect".FromResources(),
      DISCONNECTED = "device_disconnected".FromResources(),
      LONG_RANGE = "device_long_range".FromResources(),
      NAME = "device_name".FromResources(),
      NEW_DEVICES = "device_new_devices".FromResources(),
      RECONNECT = "device_reconnect".FromResources(),
      SERIAL_NUMBER = "device_serial_number".FromResources(),
      TYPE = "device_type".FromResources()
      ;

      public static class Manager {
        public static readonly string
        SELF = "device_manager".FromResources(),
        SCAN = "device_manager_scan".FromResources(),
        SCANNING = "device_manager_scanning".FromResources()
        ;
      } // End Strings.Device.Manager

      public static class Model {
        public static readonly string
        _3XTM = "device_model_3xtm".FromResources(),
        AV760 = "device_model_av760".FromResources(),
        HT = "device_model_ht".FromResources(),
        P300 = "device_model_p300".FromResources(),
        P500 = "device_model_p500".FromResources(),
        P800 = "device_model_p800".FromResources(),
        PT800 = "device_model_pt800".FromResources(),
        UNKNOWN = "device_model_unknown".FromResources()
        ;
      } // End Strings.Device.Model
    } // End Strings.Device

    public static class Errors {
      public static readonly string
      SCAN_INIT_FAIL = "errors_scan_init_fail".FromResources()
      ;
    }

    public static class Fluid {
      public static readonly string
      PT_CALCULATOR = "fluid_pt_calculator".FromResources(),
      PT_CHART = "fluid_pt_chart".FromResources(),
      STATE_HELP = "fluid_state_help".FromResources(),
      SUPERHEAT_SUBCOOL = "fluid_superheat_subcool".FromResources()
      ;
      public static class Manager {
        public static readonly string
        SELF = "fluid_manager".FromResources()
        ;
      } // End Strings.Fluid.Manager
    } // End Strings.Fluid

    public static class Measure {
      public static readonly string
      PICK_UNIT = "measure_pick_unit".FromResources()
      ;
    } // End Strings.Measure

    public static class Navigation {
      public static readonly string
      CALCULATORS = "navigation_calculators".FromResources(),
      EXIT = "navigation_exit".FromResources(),
      MAIN = "navigation_main".FromResources()
      ;
    } // End Strings.Navigation

    public static class Sensor {
      public static class Type {
        public static readonly string
        LENGTH = "sensor_type_length".FromResources(),
        HUMIDITY = "sensor_type_humidity".FromResources(),
        MASS = "sensor_type_mass".FromResources(),
        PRESSURE = "sensor_type_pressure".FromResources(),
        TEMPERATURE = "sensor_type_temperature".FromResources(),
        VACUUM = "sensor_type_vacuum".FromResources(),
        UNKNOWN = "sensor_type_unknown".FromResources()
        ;
      } // End Strings.Sensor.Type
    } // End Strings.Sensor

    public static class Workbench {
      public static readonly string
      SELF = "workbench".FromResources(),
      REMOVE = "workbench_remove".FromResources(),
      SELECT_VIEWER_ACTION = "workbench_select_viewer_action".FromResources()
      ;

      public static class Viewer {
        public static readonly string
        ADD = "workbench_viewer_add".FromResources(),
        ALT = "workbench_viewer_alt".FromResources(),
        ALT_DESC = "workbench_viewer_alt_desc".FromResources(),
        HOLD = "workbench_viewer_hold".FromResources(),
        HOLD_DESC = "workbench_viewer_hold_desc".FromResources(),
        MAX = "workbench_viewer_max".FromResources(),
        MAX_DESC = "workbench_viewer_max_desc".FromResources(),
        MIN = "workbench_viewer_min".FromResources(),
        MIN_DESC = "workbench_viewer_min_desc".FromResources()
        ;
      }
    } // End Strings.Workbench
  } // End Strings

  /// <summary>
  /// This class provides extensions for the string class that allow
  /// us to easily access iOS locized strings using a string itself
  /// as the key.
  /// </summary>
  public static class IOSStrings {

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

