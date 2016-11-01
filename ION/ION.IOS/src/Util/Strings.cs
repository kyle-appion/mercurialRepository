﻿/// <summary>
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
    CLOSE = "close".FromResources(),
		OKDONE = "ok_done".FromResources(),
		RETURNSETTINGS = "return_settings".FromResources(),   
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
      ACTION = "complete_action".FromResources(),
      ADDFROM = "add_from".FromResources(),
      ADDPRESS = "add_pressure".FromResources(),
      ADDSECONDARY = "add_secondary_2sarg".FromResources(),
      ADDSUBVIEW = "add_subview".FromResources(),
      ADDTEMP = "add_temp".FromResources(),
      ANALYZERREMOTEVIEW = "analyzer_remote_view".FromResources(),
      ANALYZERREMOTEEDIT = "analyzer_remote_edit".FromResources(),
      CANTMOVE = "cant_move".FromResources(),
      CANTADD = "cant_add".FromResources(),
      CHOOSEDEVICE = "choose_type".FromResources(),
      CHOOSEUNIT = "choose_unit".FromResources(),
      CREATEMANUAL = "create_manual".FromResources(),
      DEVICEACTIONS = "device_actions".FromResources(),
      DISPLAYLINK = "display_link".FromResources(),
      ENTERNAME = "enter_name".FromResources(),
      HIGHLOST = "high_lost".FromResources(),
      HIGHSIDE = "high_side".FromResources(),
      HIGHUNDEFINED = "high_undefined".FromResources(),
      HOLD = "analyzer_hold".FromResources(),
      LHTABLE = "lh_subviews".FromResources(),
      LOWLOST = "low_lost".FromResources(),
      LOWSIDE = "low_side".FromResources(),
      LOWUNDEFINED = "low_undefined".FromResources(),
      MISSINGVALUE = "missing_value".FromResources(),
      NOSPACE = "no_space".FromResources(),
      OPTIONS = "analyzer_options".FromResources(),
      SETUPPRESSURE = "setup_pressure".FromResources(),
      RECORDINGSTARTED = "recording_started".FromResources(),
      RECORDINGSTOPPED = "recording_stopped".FromResources(),
      REMOVESENSOR = "remove_sensor".FromResources(),
      REMOVESETUP = "remove_setup".FromResources(),
      SAMESIDE = "same_side".FromResources(),
      SC = "analyzer_subcool".FromResources(),
      SETUP = "analyzer_setup".FromResources(),
      SH = "analyzer_superheat".FromResources(),
      STABLE = "analyzer_stable".FromResources(),
      SUBVIEW = "choose_subview".FromResources(),
      UNITPICKER = "unit_picker".FromResources(),
      UNSPECIFIED = "unspecified".FromResources(),
      VALIDMEASUREMENT = "valid_measurement".FromResources()
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

		public static class Job{
		public static readonly string
      CUSTOMERNUMBER = "job_customer_number".FromResources(),
			DISPATCHNUMBER = "job_dispatch_number".FromResources(),
			JOBINFO = "job_info".FromResources(),
			JOBNAME = "job_name".FromResources(),
			MULTIPLEJOBS = "job_multiple_jobs".FromResources(),
      PONUMBER = "job_po_number".FromResources()
			;
			public static class Manager{
				public static readonly string
				SELF = "job_manager".FromResources()
				;
			}
		}
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
      SELF = "report".FromResources(),
      CALIBRATION_CERTIFICATES = "report_calibration_certificates".FromResources(),
      CITY = "report_city".FromResources(),
      CHOOSEFORMAT = "report_choose_format".FromResources(),
      CREATESPREADSHEET = "report_create_spreadsheet".FromResources(),
      CREATEPDF = "report_create_pdf".FromResources(),
      DATALOGGED = "report_data_logged".FromResources(),
      DEVICEINFO = "report_device_info".FromResources(),
      DOWNLOADING_CERTIFICATES = "report_downloading_certificates".FromResources(),
      DOWNLOADING_CERTIFICATES_FAILURES = "report_downloading_certificates_failures".FromResources(),
      FAILED_TO_DOWNLOAD = "report_failed_to_download".FromResources(),
      FINISH = "report_finish".FromResources(),
			GRAPHSELECTION = "report_graph_selection".FromResources(),
      LOGGING = "report_create".FromResources(),
      LOGGINGINTERVAL = "report_logging_interval".FromResources(),
      LOWINTERVAL = "report_low_interval".FromResources(),
      MANAGER = "report_job".FromResources(),
      TITLE = "report_title".FromResources(),
      NOTES = "report_notes".FromResources(),
      NODATA = "report_no_data".FromResources(),
      PLEASEWAIT = "report_please_wait".FromResources(),
      REPORTS = "reports".FromResources(),
      RETURNSESSIONS = "report_view_sessions".FromResources(),
      SCREENSHOT = "report_screenshot".FromResources(),
      SCREENSHOT_ARCHIVE = "report_screenshot_archive".FromResources(),
      SCREENSHOT_TITLE = "report_screenshot_title".FromResources(),
      SESSIONSELECTION = "report_session_selection".FromResources(),
      START = "report_start".FromResources(),
      STATE = "report_state".FromResources(),
      TOASTSPREADSHEET = "report_toast_spreadsheet".FromResources(),
      TOASTPDF = "report_toast_pdf".FromResources(),
      UNASSIGNED = "report_session_unassigned".FromResources(),
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
      ADD = "workbench_add".FromResources(),
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
    public static class AccessManager{
			public static readonly string
			SELF = "access_manager".FromResources()
			;
		} // End Strings.AccessManager
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

