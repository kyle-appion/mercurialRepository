﻿namespace ION.IOS.App {

  using System;

  using Foundation;

  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Sensors;

  public class AppPrefs : BasePreferences, IIONPreferences {

    private static AppPrefs SINGLETON;
    public static AppPrefs Get() {
      if (SINGLETON == null) {
        SINGLETON = new AppPrefs();
      }

      return SINGLETON;
    }

    private const string PREFERENCES_PATH = "Settings.bundle/Root.plist";
    private const string KEY_PREFERENCES_SPECIFIERS = "PreferencesSpecifiers";
    private const string KEY = "Key";
    private const string KEY_DEFAULT_VALUE = "DefaultValue";


    // App
    private const string KEY_ANALYTICS = "settings_app_analytics";
    private const string KEY_KEEP_SCREEN_ON = "settings_screen_leave_on";



    // Implemented for IIONPreferences
    public string lastKnownAppVersion { get; set; }
    // Implemented for IIONPreferences
    public Guid appId { get { return Guid.Empty; } }
    // Implemented for IIONPreferences
    public bool allowAppAnalytics { 
      get {
        return GetBool(KEY_ANALYTICS);
      }
      set {
        PutBool(KEY_ANALYTICS, value);
      }
    }

    // Implemented for IPreferences
    public DevicePreferences _device { get; private set; }
    // Implemented for IPreferences
    public IDevicePreferences device { get { return _device; } }

    public AlarmPreferences _alarm { get; private set; }
    // Implemented for IPreferences
    public IAlarmPreferences alarm { get { return _alarm; } }

    public LocationPreferences _location { get; private set; }
    // Implemented for IPreferences
    public ILocationPreferences location { get { return _location; } }

    public UnitPreferences _units { get; private set; }
    // Implemented for IPreferences
    public IUnitPreferences units { get { return _units; } }

    public ReportPreferences _report { get; private set; }
    // Implemented for IPreferences
    public IReportPreferences report { get { return _report; } }

    public PortalPreferences _portal { get; private set; }
    // Implemented for IPreferences
    public IPortalPreferences portal { get { return _portal; } }


    /// <summary>
    /// Whether or not the user wants the screen to be left on.
    /// </summary>
    public bool leaveScreenOn {
      get {
        return GetBool(KEY_KEEP_SCREEN_ON);
      }
      set {
        PutBool(KEY_KEEP_SCREEN_ON, value);
      }
    }



    private AppPrefs() {
      _device = new DevicePreferences(this);
      _alarm = new AlarmPreferences(this);
      _location = new LocationPreferences(this);
      _units = new UnitPreferences(this);
      _report =  new ReportPreferences(this);
      _portal = new PortalPreferences(this);
    }
  }

  public class BasePreferences {

    /// <summary>
    /// Queries the integer setting with the given key.
    /// </summary>
    /// <returns>The int.</returns>
    /// <param name="key">Key.</param>
    public int GetInt(string key) {
      return (int)NSUserDefaults.StandardUserDefaults.IntForKey(key);
    }

    /// <summary>
    /// Sets the value for the given setting.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="setting">Setting.</param>
    public void PutInt(string key, int setting) {
      NSUserDefaults.StandardUserDefaults.SetInt(setting, key);
    }

    /// <summary>
    /// Queries the bool setting with the given key.
    /// </summary>
    /// <returns>The bool.</returns>
    /// <param name="key">Key.</param>
    public bool GetBool(string key) {
      return NSUserDefaults.StandardUserDefaults.BoolForKey(key);
    }

    /// <summary>
    /// Sets the value for the given setting.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="setting">Setting.</param>
    public void PutBool(string key, bool setting) {
      NSUserDefaults.StandardUserDefaults.SetBool(setting, key);
    }

    /// <summary>
    /// Queries the float setting with the given key.
    /// </summary>
    /// <returns>The float.</returns>
    /// <param name="key">Key.</param>
    public float GetFloat(string key) {
      return NSUserDefaults.StandardUserDefaults.FloatForKey(key);
    }

    /// <summary>
    /// Sets the value for the given setting.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="setting">Setting.</param>
    public void PutFloat(string key, float setting) {
      NSUserDefaults.StandardUserDefaults.SetFloat(setting, key);
    }

    /// <summary>
    /// Queries the string setting with the given key.
    /// </summary>
    /// <returns>The string.</returns>
    /// <param name="key">Key.</param>
    public string GetString(string key) {
      return NSUserDefaults.StandardUserDefaults.StringForKey(key);
    }

    /// <summary>
    /// Sets the value for the given setting.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="setting">Setting.</param>
    public void PutString(string key, string setting) {
      NSUserDefaults.StandardUserDefaults.SetString(setting, key);
    }

    /// <summary>
    /// Queries the double setting with the given key.
    /// </summary>
    /// <returns>The double.</returns>
    /// <param name="key">Key.</param>
    public double GetDouble(string key) {
      return NSUserDefaults.StandardUserDefaults.DoubleForKey(key);
    }

    /// <summary>
    /// Sets the value for the given setting.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="setting">Setting.</param>
    public void PutDouble(string key, double setting) {
      NSUserDefaults.StandardUserDefaults.SetDouble(setting, key);
    }
  }

  public class DerivedPreferences : BasePreferences {
    protected AppPrefs prefs { get; private set; }

    public DerivedPreferences(AppPrefs prefs) {
      this.prefs = prefs;
    }
  }

  public class DevicePreferences : DerivedPreferences, IDevicePreferences {
    private const string KEY_AUTO_CONNECT = "settings_device_auto_connect";
    private const string KEY_LONG_RANGE = "settings_device_long_range";

    // Implemented for IPreferences
    public bool allowDeviceAutoConnect {
      get {
        return GetBool(KEY_AUTO_CONNECT);
      }
      set {
        PutBool(KEY_LONG_RANGE, value);
      }
    }

    // Implemented for IPreferences
    public bool allowLongRangeMode {
      get {
        return false;
//        return GetBool(KEY_LONG_RANGE);
      }

      set {
//        PutBool(KEY_LONG_RANGE, value);
      }
    }

    public DevicePreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's alarm preferences.
  /// </summary>
  public class AlarmPreferences : DerivedPreferences, IAlarmPreferences {
    private const string KEY_SOUND = "settings_alarm_sound";
    private const string KEY_HAPTIC = "settings_alarm_haptic";

    /// <summary>
    /// Queries whether or not the user will allow the application to vibrate for alarms.
    /// </summary>
    /// <value><c>true</c> if allows vibrate; otherwise, <c>false</c>.</value>
    public bool allowsVibrate {
      get {
        return GetBool(KEY_HAPTIC);
      }
      set {
        PutBool(KEY_HAPTIC, value);
      }
    }

    /// <summary>
    /// Queries whether or not the user will allow the application to play sound for alarms.
    /// </summary>
    /// <value><c>true</c> if allows sounds; otherwise, <c>false</c>.</value>
    public bool allowsSounds {
      get {
        return GetBool(KEY_SOUND);
      }
      set {
        PutBool(KEY_SOUND, value);
      }
    }

    public AlarmPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  public class LocationPreferences : DerivedPreferences, ILocationPreferences {
    private const string KEY_USE_GEO_LOCATION = "settings_location_use_geolocation";
    private const string KEY_CUSTOM_ELEVATION = "settings_location_custom_elevation";

    /// <summary>
    /// Queries whether or not the user will allow the application to use the device's GPS.
    /// </summary>
    /// <value><c>true</c> if allows gps; otherwise, <c>false</c>.</value>
    public bool allowsGps {
      get {
        return GetBool(KEY_USE_GEO_LOCATION);
      }
      set {
        PutBool(KEY_USE_GEO_LOCATION, value);
      }
    }

    // Implemented for ILocationPreferences
    public Scalar customElevation {
      get {
        var raw = GetFloat(KEY_CUSTOM_ELEVATION);
        return Units.Length.METER.OfScalar(raw).ConvertTo(prefs.units.length);
      }

      set {
        var raw = value.ConvertTo(Units.Length.METER);
        PutFloat(KEY_CUSTOM_ELEVATION, (float)raw.amount);
      }
    }

    public LocationPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's default unit preferences.
  /// </summary>
  public class UnitPreferences : DerivedPreferences, IUnitPreferences { 
    private const string KEY_LENGTH = "settings_units_default_length";
    private const string KEY_PRESSURE = "settings_units_default_pressure";
    private const string KEY_TEMPERATURE = "settings_units_default_temperature";
    private const string KEY_VACUUM = "settings_units_default_vacuum";
    private const string KEY_WEIGHT = "settings_units_default_weight";

    // Overridden from IUnits
    public Unit length {
      get {
        return AssertUnitGet(KEY_LENGTH, Units.Length.FOOT);
      }
      set {
        AssertUnitSet(KEY_LENGTH, Quantity.Length, value);
      }
    }

    // Overridden from IUnits
    public Unit pressure {
      get {
        return AssertUnitGet(KEY_PRESSURE, Units.Pressure.PSIG);
      }
      set {
        AssertUnitSet(KEY_PRESSURE, Quantity.Pressure, value);
      }
    }

    // Overridden from IUnits
    public Unit temperature {
      get {
        return AssertUnitGet(KEY_TEMPERATURE, Units.Temperature.FAHRENHEIT);
      }
      set {
        AssertUnitSet(KEY_TEMPERATURE, Quantity.Temperature, value);
      }
    }

    // Overridden from IUnits
    public Unit vacuum {
      get {
        return AssertUnitGet(KEY_VACUUM, Units.Vacuum.MICRON);
      }
      set {
        AssertUnitSet(KEY_VACUUM, Quantity.Vacuum, value);
      }
    }

    // Overridden from IUnits
    public Unit weight {
      get {
        return AssertUnitGet(KEY_WEIGHT, Units.Weight.POUND_FORCE);
      }
      set {
        AssertUnitSet(KEY_WEIGHT, Quantity.Force, value);
      }
    }

    public UnitPreferences(AppPrefs prefs) : base(prefs) {
    }

    public Unit DefaultUnitFor(ESensorType sensorType) {
      switch (sensorType) {
        case ESensorType.Length:
        return length;
        case ESensorType.Pressure:
        return pressure;
        case ESensorType.Temperature:
        return temperature;
        case ESensorType.Vacuum:
        return vacuum;
        default:
        return sensorType.GetDefaultUnit();
      }
    }

    /// <summary>
    /// Safely gets the unit for the given key. If the desired unit could not be
    /// fetched, we will return the backup.
    /// </summary>
    /// <returns>The unit get.</returns>
    /// <param name="preferenceKey">Preference key.</param>
    /// <param name="backup">Backup.</param>
    private Unit AssertUnitGet(string preferenceKey, Unit backup) {
      try {
        var ret = UnitLookup.GetUnit(GetInt(preferenceKey));  
        return ret;
      } catch (Exception e) {
        Log.E(this, "Failed to retrieve unit for key: " + preferenceKey, e);
        AssertUnitSet(preferenceKey, backup.quantity, backup);
        return backup;
      }
    }

    /// <summary>
    /// Safely attempts to set the unit for the given key.
    /// </summary>
    /// <returns>The unit set.</returns>
    private void AssertUnitSet(string preferenceKey, Quantity quantity, Unit unit) {
      try {
        if (quantity != unit.quantity) {
          throw new ArgumentException("Unit: " + unit + " is not compatible with quantity + " + quantity);
        }

        PutInt(preferenceKey, UnitLookup.GetCode(unit));
      } catch (Exception e) {
        Log.E(this, "Failed to set unit " + unit + " for key: " + preferenceKey, e);
      }
    }
  }

  public class ReportPreferences : DerivedPreferences, IReportPreferences {
    // Implemented for IReportPreferences
    public TimeSpan dataLoggingInterval {
      get {
//        return TimeSpan.FromSeconds(GetIntFromString(Resource.String.pkey_reporting_data_log_interval, 60));
        return TimeSpan.FromSeconds(60);
      }

/*
      set {
        SetStringFromInt((int)value.TotalMilliseconds);
      }
*/
    }

    public ReportPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  public class PortalPreferences : DerivedPreferences, IPortalPreferences {
    // Implemented for IPortalPreferences
    public bool rememberMe {
      get {
//        return GetBool(Key_, false);
        return false;
      }
      set {
//      PutBool(Resource.String.pkey_portal_rememberme, value);
      }
    }

    // Implemented for IPortalPreferences
    public string username {
      get {
//        return GetString(Resource.String.pkey_portal_username, "");
        return "";
      }

      set {
//        PutString(Resource.String.pkey_portal_username, value);
      }
    }

    // Implemented for IPortalPreferences
    public string password {
      get {
//        return GetString(Resource.String.pkey_portal_password, "");
        return "";
      }

      set {
//        PutString(Resource.String.pkey_portal_password, value);
      }
    }

    public PortalPreferences(AppPrefs prefs) : base(prefs) {
    }
  }
}
