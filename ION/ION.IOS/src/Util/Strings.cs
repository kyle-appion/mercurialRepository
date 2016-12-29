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
    APPFEEDBACK = "app_feedback".FromResources(),
    CANCEL = "cancel".FromResources(),
    CLOSE = "close".FromResources(),
    DATE = "date".FromResources(),
    DELETE_QUESTION = "delete_question".FromResources(),
    DESELECTALL = "deselect_all".FromResources(),
    DEVICES = "devices".FromResources(),
    DONE = "done".FromResources(),
    ENTER_NAME = "enter_name".FromResources(),
    HELP = "help".FromResources(),
    INCLUDED = "included".FromResources(),
    LINKED = "linked".FromResources(),
    NAME = "name".FromResources(),
    NEXT = "next".FromResources(),
    NOTES = "notes".FromResources(),
    OK = "ok".FromResources(),
		OKDONE = "ok_done".FromResources(),
    OK_SAVE = "ok_save".FromResources(),
    PLEASE_WAIT = "please_wait".FromResources(),
    PREVIOUS = "previous".FromResources(),
    RENAME = "rename".FromResources(),
    REQUIRED = "required".FromResources(),
    RESET = "reset".FromResources(),
		RETURNSETTINGS = "return_settings".FromResources(),   
    SAVE = "save".FromResources(),
    SAVING = "saving".FromResources(),
    SELECTALL = "select_all".FromResources(),
    SESSIONS = "sessions".FromResources(),
    SETTINGS = "settings".FromResources(),
    SPREADSHEET = "spreadsheet".FromResources(),
    TIME = "time".FromResources(),
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
      ENTERMEASUREMENT = "enter_measurement".FromResources(),
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
      PTDESC = "analyzer_pt_desc".FromResources(),
      SETUPPRESSURE = "setup_pressure".FromResources(),
      RECORDINGSTARTED = "recording_started".FromResources(),
      RECORDINGSTOPPED = "recording_stopped".FromResources(),
      REMOVESENSOR = "remove_sensor".FromResources(),
      REMOVESETUP = "remove_setup".FromResources(),
      SAMESIDE = "same_side".FromResources(),
      SHSCDESC = "analyzer_shsc_desc".FromResources(),
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
      CERTDATE = "device_cert_date".FromResources(),
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
      NOTLINKED = "device_not_linked".FromResources(),
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
    } //End String.Errors

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
      SLIDERHEADING = "fluid_slider_heading".FromResources(),
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

		public static class Job{
		public static readonly string
			ADDNOTES = "job_add_notes".FromResources(),
			ADDSELECTED = "job_add_selected".FromResources(),
			AVAILABLESESSIONS = "job_available_sessions".FromResources(),
			CONFIRMREMOVAL = "job_confirm_removal".FromResources(),
			CURRENTSESSIONS = "job_current_sessions".FromResources(),
      CUSTOMERNUMBER = "job_customer_number".FromResources(),
      CUSTOMERSIGN = "job_customer_sign".FromResources(),
			DISPATCHNUMBER = "job_dispatch_number".FromResources(),
			DISPATCHSIGN = "job_dispatch_sign".FromResources(),
			DURATION = "job_duration".FromResources(),
			EDITSESSIONS = "job_edit_sessions".FromResources(),
			EDIT = "job_edit".FromResources(),
			JOBADDRESS= "job_address".FromResources(),
			JOBEXISTS = "job_exists".FromResources(),
			JOBINFO = "job_info".FromResources(),
			JOBLOCATION = "job_location".FromResources(),
			JOBNAME = "job_name".FromResources(),
			JOBOPTIONS = "job_options".FromResources(),
			JOBSETTINGS = "job_settings".FromResources(),
			MULTIPLEJOBS = "job_multiple".FromResources(),
			NONEAVAILABLE = "job_none_available".FromResources(),
			NOSESSIONS = "job_no_session".FromResources(),
			NOTESSAVED = "job_notes_saved".FromResources(),
      POFULL = "job_po_full".FromResources(),
      PONUMBER = "job_po_number".FromResources(),
      POSIGN = "job_po_sign".FromResources(),
      REDSESSIONS = "job_red_sessions".FromResources(),
      REMOVEDIALOGUE = "job_remove_dialogue".FromResources(),
      REMOVESELECTED = "job_remove_selected".FromResources(),
      REMOVINGSESSIONS = "job_sessions_removing".FromResources(),
      SAVENOTES = "job_save_notes".FromResources(),
      SAVEDSUCCESS = "job_saved_success".FromResources(),
      STARTDATE = "job_start_date".FromResources(),
      SYSTEMTYPE = "job_system".FromResources(),
      TECHNAME = "job_tech".FromResources(),
      UPDATED = "job_updated".FromResources()
			;
			public static class Manager{
				public static readonly string
				SELF = "job_manager".FromResources()
				;
			} //End Strings.Manager
		} //End Strings.Job
				
    public static class Measure {
      public static readonly string
      AVERAGE = "measure_average".FromResources(),
      LOWESTMEASUREMENT = "measure_lowest".FromResources(),
      HIGHESTMEASUREMENT = "measure_highest".FromResources(),
      AVERAGEMEASUREMENT = "measure_avg".FromResources(),
      MAXIMUM = "measure_maximum".FromResources(),
      MINIMUM = "measure_minimum".FromResources(),
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
      ADDRESS = "report_address".FromResources(),
			BYDATE = "report_by_date".FromResources(),
			BYJOB = "report_by_job".FromResources(),
      CALIBRATION_CERTIFICATES = "report_calibration_certificates".FromResources(),
      CITY = "report_city".FromResources(),
      CHOOSEEND = "report_choose_end".FromResources(),
      CHOOSEFORMAT = "report_choose_format".FromResources(),
      CHOOSESTART = "report_choose_start".FromResources(),
      CREATED ="report_created".FromResources(),
      CREATESPREADSHEET = "report_create_spreadsheet".FromResources(),
      CREATEPDF = "report_create_pdf".FromResources(),
      DATALOGGED = "report_data_logged".FromResources(),
      DELETEMESSAGE = "report_delete_message".FromResources(),
      DELETESESSION = "report_delete_session".FromResources(),
      DETAILED = "report_detailed".FromResources(),
      DEVICEINFO = "report_device_info".FromResources(),
      DEVICESUSED = "report_devices_used".FromResources(),
      DEVICEMODEL = "report_device_model".FromResources(),
      DOWNLOADING_CERTIFICATES = "report_downloading_certificates".FromResources(),
      DOWNLOADING_CERTIFICATES_FAILURES = "report_downloading_certificates_failures".FromResources(),
      EXPORT = "report_export".FromResources(),
      FAILED_TO_DOWNLOAD = "report_failed_to_download".FromResources(),
      FINISH = "report_finish".FromResources(),
      GRAPHINFO = "report_graph_info".FromResources(),
			GRAPHSELECTION = "report_graph_selection".FromResources(),
      LOGGING = "report_create".FromResources(),
      LOGGINGINTERVAL = "report_logging_interval".FromResources(),
      LOWINTERVAL = "report_low_interval".FromResources(),
      MANAGER = "report_job".FromResources(),
      NEWREPORT = "report_new".FromResources(),
      TITLE = "report_title".FromResources(),
      NODATA = "report_no_data".FromResources(),
			NOJOBS = "logging_no_jobs".FromResources(),
      NOTES = "report_notes".FromResources(),
      ONEPAGE = "report_one_page".FromResources(),
      PLEASEWAIT = "report_please_wait".FromResources(),
      RAWDATA = "report_raw_data".FromResources(),
      REPORTS = "reports".FromResources(),
      REPORTDATES = "report_dates".FromResources(),
      RETURNSESSIONS = "report_view_sessions".FromResources(),
      SAVEDREPORT = "report_saved".FromResources(),
      SCREENSHOT = "report_screenshot".FromResources(),
      SCREENSHOT_ARCHIVE = "report_screenshot_archive".FromResources(),
      SCREENSHOT_TITLE = "report_screenshot_title".FromResources(),
      SELECTION = "report_selection".FromResources(),
      SELECTEND = "report_select_end".FromResources(),
      SELECTSTART = "report_select_start".FromResources(),
      SESSIONSELECTION = "report_session_selection".FromResources(),
      START = "report_start".FromResources(),
      STATE = "report_state".FromResources(),
      TOASTSPREADSHEET = "report_toast_spreadsheet".FromResources(),
      TOASTPDF = "report_toast_pdf".FromResources(),
      UNASSIGNED = "report_session_unassigned".FromResources(),
      ZIP = "report_zip".FromResources()
      ;
    } //End Strings.Report

    public static class Sensor {
      public static class Type {
        public static readonly string
        LENGTH = "sensor_type_length".FromResources(),
        HUMIDITY = "sensor_type_humidity".FromResources(),
				WEIGHT = "sensor_type_weight".FromResources(),
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
    
    public static class Walkthrough{
			public static readonly string 
			INTRODUCTORY = "walkthrough_introductory".FromResources(),
			INTR1 = "walkthrough_1_1".FromResources(),
			INTR2 = "walkthrough_1_2".FromResources(),
			INTR3 = "walkthrough_1_3".FromResources(),
			INTR4 = "walkthrough_1_4".FromResources(),
			INTR5 = "walkthrough_1_5".FromResources(),
			INTR6 = "walkthrough_1_6".FromResources(),
			INTR7 = "walkthrough_1_7".FromResources(),
			INTR8 = "walkthrough_1_8".FromResources()
			;
		} //End Strings.Walkthrough
    
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

