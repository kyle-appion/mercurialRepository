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
    DATE = "date".FromResources(),
    DELETE_QUESTION = "delete_question".FromResources(),
    ENTER_NAME = "enter_name".FromResources(),
    HELP = "help".FromResources(),
    OK = "ok".FromResources(),
    OK_SAVE = "ok_save".FromResources(),
    RENAME = "rename".FromResources(),
    PLEASE_WAIT = "please_wait".FromResources(),
    SAVE = "save".FromResources(),
    SAVING = "saving".FromResources(),
    SETTINGS = "settings".FromResources(),
    UNKNOWN = "unknown"
    ;

    public static class Alarms {
      public static readonly string
      SELF = "alarms".FromResources(),
      LOW_ALARM = "alarms_low".FromResources(),
      LOW_ALARM_FIRED = "alarms_low_fired".FromResources(),
      HIGH_ALARM = "alarms_high".FromResources(),
      HIGH_ALARM_FIRED = "alarms_high_fired".FromResources(),

      REENABLE = "alarms_reenable".FromResources()
      ;
    } // End Strings.Alarms

    public static class Analyzer {
      public static readonly string
      SELF = "analyzer".FromResources(),
      ACTION = "Complete Action?".FromResources(),
      ADDTEMP = "Do you want to add this Temperature sensor to the current Pressure sensor?".FromResources(),
      ADDPRESS = "Do you want to add this Pressure sensor to the current Temperature sensor?".FromResources(),
      CANTMOVE = "Cannot Move Sensor".FromResources(),
      CANTADD = "Cannot add as secondary sensor".FromResources(),
      NOSPACE = "Not enough space".FromResources(),
      SAMESIDE = "Secondary sensor must be on the same side as the primary sensor".FromResources(),
      HIGHLOST = "Completing this action will cause the High side subview arrangement to be lost.".FromResources(),
      LOWLOST = "Completing this action will cause the Low side subview arrangement to be lost.".FromResources(),
      SUBVIEW = "Choose a subview".FromResources(),
      ADDSUBVIEW = "Add Subview".FromResources(),
      REMOVESENSOR = "Remove Sensor".FromResources(),
      HIGHUNDEFINED = "High Viewer Not Defined".FromResources(),
      LOWUNDEFINED = "Low Viewer Not Defined".FromResources(),
      ACTIONS = "Actions".FromResources(),
      CLOSE = "Close".FromResources(),
      DISPLAYLINK = "Display Link:".FromResources(),
      OKDONE = "OK-Done".FromResources(),
      OKSAVE = "OK - Save".FromResources(),
      DEVICETYPE = "Device Type".FromResources(),
      CREATEMANUAL = "Create Manual Entry".FromResources(),
      PRESSURE = "Pressure".FromResources(),
      TEMPERATURE = "Temperature".FromResources(),
      VACUUM = "Vacuum".FromResources(),
      ENTERNAME = "Enter Name".FromResources(),
      UNITPICKER = "Unit Picker".FromResources(),
      CHOOSEUNIT = "Choose Unit".FromResources(),
      DEVICEMANAGER = "Device Manager".FromResources(),
      DEVICEACTIONS = "Device Actions".FromResources(),
      ADDFROM = "Add From...".FromResources(),
      CHOOSEDEVICE = "Choose Device Type".FromResources(),
      RENAME = "Rename".FromResources(),
      ALARMS = "Alarms".FromResources(),
      UNSPECIFIED = "Unspecified".FromResources(),
      LOWSIDE = "Low Side".FromResources(),
      HIGHSIDE = "High Side".FromResources(),
      DISCONNECTED = "Disconnected".FromResources(),
      LHTABLE = "'s Subviews".FromResources(),
      SC = "S/C".FromResources(),
      SH = "S/H".FromResources(),
      HOLD = "hold".FromResources(),
      SETUP = "Setup".FromResources(),
      STABLE = "Stable".FromResources()
      ;
    } // End Strings.Analyzer

    public static class Device {
      public static readonly string
      AVAILABLE = "device_available".FromResources(),
      CONNECTED = "device_connected".FromResources(),
      CONNECT_ALL = "device_connect_all".FromResources(),
      DISCONNECT = "device_disconnect".FromResources(),
      DISCONNECT_ALL = "device_disconnect_all".FromResources(),
      DISCONNECTED = "device_disconnected".FromResources(),
      FORGET = "device_forget".FromResources(),
      FORGET_ALL = "device_forget_all".FromResources(),
      FORGET_DESC = "device_forget_desc".FromResources(),
      FORGET_NAME = "device_forget_name".FromResources(),
      FORGET_NAME_WHERE = "device_forget_name_where".FromResources(),
      IN_WORKBENCH = "device_in_workbench".FromResources(),
      LONG_RANGE = "device_long_range".FromResources(),
      NAME = "device_name".FromResources(),
      NEW_DEVICES = "device_new_devices".FromResources(),
      REALLY_FORGET = "device_really_forget".FromResources(),
      RECONNECT = "device_reconnect".FromResources(),
      SERIAL = "device_serial".FromResources(),
      SERIAL_NUMBER = "device_serial_number".FromResources(),
      TYPE = "device_type".FromResources()
      ;

      public static class Manager {
        public static readonly string
        SELF = "device_manager".FromResources(),
        AVAILABLE_ACTIONS = "device_manager_available_actions".FromResources(),
        BROADCASTING_ACTIONS = "device_manager_broadcasting_actions".FromResources(),
        CONNECTED_ACTIONS = "device_manager_connected_actions".FromResources(),
        DISCONNECTED_ACTIONS = "device_manager_disconnected_actions".FromResources(),
        EMPTY = "device_manager_empty".FromResources(),
        NEW_ACTIONS = "device_manager_new_actions".FromResources(),
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
        PT300 = "device_model_pt300".FromResources(),
        PT500 = "device_model_pt500".FromResources(),
        PT800 = "device_model_pt800".FromResources(),
        UNKNOWN = "device_model_unknown".FromResources()
        ;
      } // End Strings.Device.Model
    } // End Strings.Device

    public static class Errors {
      public static readonly string
      CANNOT_SEND_FEEBACK = "errors_cannot_send_feedback".FromResources(),
      FAILED_TO_SEND_FEEDBACK = "errors_failed_to_send_feedback".FromResources(),
      PRESSURE_INPUT_PARSE_ERROR = "errors_pressure_input_parse_error".FromResources(),
      SCAN_INIT_FAIL = "errors_scan_init_fail".FromResources(),
      SCREENSHOT = "errors_screenshot".FromResources(),
      SCREENSHOT_MISSING_TITLE = "errors_screenshot_missing_title".FromResources(),
      TEMPERATURE_INPUT_PARSE_ERROR = "errors_temperature_input_parse_error".FromResources()
      ;
    }

    public static class Fluid {
      public static readonly string
      OUT_OF_RANGE = "fluid_out_of_range".FromResources(),
      PT = "fluid_pt".FromResources(),
      PT_CALCULATOR = "fluid_pt_calculator".FromResources(),
      PT_CHART = "fluid_pt_chart".FromResources(),
      PT_CHART_BUB = "fluid_pt_chart_bub".FromResources(),
      PT_CHART_DEW = "fluid_pt_chart_dew".FromResources(),
      SATURATED = "fluid_saturated".FromResources(),
      SC = "fluid_sc".FromResources(),
      SH = "fluid_sh".FromResources(),
      SHSC = "fluid_shsc".FromResources(),
      STATE_HELP = "fluid_state_help".FromResources(),
      SUBCOOL = "fluid_subcool".FromResources(),
      SUBCOOL_ABRV = "fluid_subcool_abrv".FromResources(),
      SUPERHEAT = "fluid_superheat".FromResources(),
      SUPERHEAT_ABRV = "fluid_superheat_abrv".FromResources(),
      SUPERHEAT_SUBCOOL = "fluid_superheat_subcool".FromResources()
      ;
      public static class Manager {
        public static readonly string
        SELF = "fluid_manager".FromResources()
        ;
      } // End Strings.Fluid.Manager
    } // End Strings.Fluid

    public static class Help {
      public static readonly string
      ABOUT = "help_about".FromResources(),
      SEND_FEEDBACK = "help_send_feedback".FromResources(),
      SENT_FEEDBACK = "help_sent_feedback".FromResources(),
      VERSION = "help_version".FromResources()
      ;
    } // End Strings.Help

    public static class Measure {
      public static readonly string
      PER_MINUTE = "measure_per_minute".FromResources(),
      PICK_UNIT = "measure_pick_unit".FromResources()
      ;
    } // End Strings.Measure

    public static class Navigation {
      public static readonly string
      CALCULATORS = "navigation_calculators".FromResources(),
      CONFIGURATION = "navigation_configuration".FromResources(),
      MAIN = "navigation_main".FromResources()
      ;
    } // End Strings.Navigation

    public static class Report {
      public static readonly string
      CALIBRATION_CERTIFICATES = "report_calibration_certificates".FromResources(),
      CITY = "report_city".FromResources(),
      DOWNLOADING_CERTIFICATES = "report_downloading_certificates".FromResources(),
      DOWNLOADING_CERTIFICATES_FAILURES = "report_downloading_certificates_failures".FromResources(),
      FAILED_TO_DOWNLOAD = "report_failed_to_download".FromResources(),
      LOGGING = "report_create".FromResources(),
      MANAGER = "report_job".FromResources(),
      TITLE = "report_title".FromResources(),
      NOTES = "report_notes".FromResources(),
      REPORTS = "Reports".FromResources(),
      SCREENSHOT = "report_screenshot".FromResources(),
      SCREENSHOT_ARCHIVE = "report_screenshot_archive".FromResources(),
      SCREENSHOT_TITLE = "report_screenshot_title".FromResources(),
      STATE = "report_state".FromResources(),
      ZIP = "report_zip".FromResources()
      ;
    }

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
      ADD_ALL_TO_WORKBENCH = "workbench_add_all".FromResources(),
      REMOVE = "workbench_remove".FromResources(),
      SELECT_VIEWER_ACTION = "workbench_select_viewer_action".FromResources()
      ;

      public static class Viewer {
        public static readonly string
        ADD = "workbench_subview_add".FromResources(),
        ALT = "workbench_subview_alt".FromResources(),
        ALT_DESC = "workbench_subview_alt_desc".FromResources(),
        HOLD = "workbench_subview_hold".FromResources(),
        HOLD_DESC = "workbench_subview_hold_desc".FromResources(),
        MAX = "workbench_subview_max".FromResources(),
        MAX_DESC = "workbench_subview_max_desc".FromResources(),
        MIN = "workbench_subview_min".FromResources(),
        MIN_DESC = "workbench_subview_min_desc".FromResources(),
        ROC = "workbench_subview_roc".FromResources(),
        ROC_DESC = "workbench_subview_roc_desc".FromResources(),
        ROC_STABLE = "workbench_subview_roc_stable".FromResources(),
        PT_CHART_DESC = "workbench_subview_pt_chart_desc".FromResources(),
        SC = "workbench_subview_sc".FromResources(),
        SH = "workbench_subview_sh_setup".FromResources(),
        SHSC_DESC = "workbench_subview_shsc_desc".FromResources(),
        SHSC_SETUP = "workbench_subview_shsc_setup".FromResources(),
        TIMER = "workbench_subview_timer".FromResources(),
        TIMER_DESC = "workbench_subview_timer_desc".FromResources(),
        SECONDARY = "workbench_subview_linked_desc".FromResources()
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

