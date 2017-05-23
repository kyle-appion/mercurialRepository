namespace ION.Droid.Preferences {
  
  using System;

  using Android.Content;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Sensors;

  /// <summary>
  /// A simple class that is used to access the android applications preferences.
  /// </summary>
  public class AppPrefs : Java.Lang.Object, IIONPreferences, ISharedPreferencesOnSharedPreferenceChangeListener {
		/// <summary>
		/// The name of the general ion preferences.
		/// </summary>
		public const string PREFERENCES_GENERAL = "ion.preferences";

		private static AppPrefs PREFS;
		public static AppPrefs Get(Context context) {
			if (PREFS == null) {
				PREFS = new AppPrefs(context, context.GetSharedPreferences(PREFERENCES_GENERAL, FileCreationMode.Private));
			}

			return PREFS;
		}

    public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (onPreferencesChanged != null) {
        onPreferencesChanged();
      }
    }

    // Implemented for IIONPreferences
    public event Action onPreferencesChanged;

    // Implemented for IIONPreferences
    public string lastKnownAppVersion {
      get {
        return prefs.GetString(context.GetString(Resource.String.pkey_app_version), "0.0.0");
      }

      set {
        var e = prefs.Edit();
        e.PutString(context.GetString(Resource.String.pkey_app_version), value);
        e.Commit();
      }
    }

    // Implemented for IIONPreferences
    public Guid appId {
      get {
        lock (PREFS) {
          var key = context.GetString(Resource.String.pkey_id);
          var id = prefs.GetString(key, null);

          if (id == null) {
            var guid = Guid.NewGuid();
            id = guid.ToString();
            var e = prefs.Edit();
            e.PutString(key, id);
            e.Commit();
            return guid;
          } else {
            try {
              return Guid.Parse(id);
            } catch (Exception e) {
              Log.E(this, "Failed to parse Guid.", e);
              return Guid.Empty;
            }
          }
        }
      }
    }

    // Implemented for IIONPreferences
    public bool allowAppAnalytics {
      get {
        return prefs.GetBoolean(context.GetString(Resource.String.pkey_app_analytics), true);
      }
      set {
        var e = prefs.Edit();
        e.PutBoolean(context.GetString(Resource.String.pkey_app_analytics), value);
        e.Commit();
      }
    }

    /// <summary>
    /// Queries whether or not this is the application's first launch.
    /// </summary>
    /// <value><c>true</c> if first launch; otherwise, <c>false</c>.</value>
    public bool firstLaunch {
      get {
        var ret = prefs.GetBoolean(context.GetString(Resource.String.pkey_first_launch), true);        

        if (ret) {
          var e = prefs.Edit();
          e.PutBoolean(context.GetString(Resource.String.pkey_first_launch), false);
          e.Commit();
        }

        return ret;
      }
    }

    /// <summary>
    /// Queries whether or not the application should show the what's new dialog.
    /// </summary>
    /// <value><c>true</c> if show whats new; otherwise, <c>false</c>.</value>
    public bool showWhatsNew {
      get {
        return prefs.GetBoolean(context.GetString(Resource.String.pkey_whats_new), true);
      }

      set {
        var e = prefs.Edit();
        e.PutBoolean(context.GetString(Resource.String.pkey_whats_new), value);
        e.Commit();
      }
    }

    /// <summary>
    /// Queries whether or not the application should show the tutorial walkthrough.
    /// </summary>
    /// <value><c>true</c> if show tutorial; otherwise, <c>false</c>.</value>
    public bool showTutorial {
      get {
        return prefs.GetBoolean(context.GetString(Resource.String.pkey_help_walkthrough), true);
      }
      set {
        var e = prefs.Edit();
        e.PutBoolean(context.GetString(Resource.String.pkey_help_walkthrough), value);
        e.Commit();
      }
    }

    /// <summary>
    /// Queries whether or not the wake lock is set in preferences.
    /// </summary>
    /// <value><c>true</c> if is wake locked; otherwise, <c>false</c>.</value>
    public bool isWakeLocked {
      get {
        return prefs.GetBoolean(context.GetString(Resource.String.pkey_wake_lock), true);
      }
      set {
        var e = prefs.Edit();
        e.PutBoolean(context.GetString(Resource.String.pkey_wake_lock), value);
        e.Commit();
      }
    }


    public DevicePreferences _device { get; private set; }
    // Implemented for IIONPreferences
    public IDevicePreferences device { get { return _device; } }

    public AlarmPreferences _alarm { get; private set; }
    // Implemented for IIONPreferences
    public IAlarmPreferences alarm { get { return _alarm; } }

    public LocationPreferences _location { get; private set; }
    // Implemented for IIONPreferences
    public ILocationPreferences location { get { return _location; } }

    public UnitPreferences _units { get; private set; }
    // Implemented for IIONPreferences
    public IUnitPreferences units { get { return _units; } }

    public ReportPreferences _report { get; private set; }
    // Implemented for IIONPreferences
    public IReportPreferences report { get { return _report; } }

    public PortalPreferences _portal { get; private set; }
    // Implemented for IIONPreferences
    public IPortalPreferences portal { get { return _portal; } }

    /// <summary>
    /// The android context that is used to get the preferences.
    /// </summary>
    /// <value>The ion.</value>
		public Context context { get; private set; }
    /// <summary>
    /// The application's preferences.
    /// </summary>
    /// <value>The prefs.</value>
    public ISharedPreferences prefs { get; set; }

		public AppPrefs(Context context, ISharedPreferences prefs) {
      this.context = context;
      this.prefs = prefs;
      _device = new DevicePreferences(this);
      _alarm = new AlarmPreferences(this);
      _location = new LocationPreferences(this);
      _units = new UnitPreferences(this);
      _report =  new ReportPreferences(this);
      _portal = new PortalPreferences(this);

      prefs.RegisterOnSharedPreferenceChangeListener(this);
    }
  }

  /// <summary>
  /// A simple wrapper that will provide convenience methods for preferences.
  /// </summary>
  public class BasePreferences {
    /// <summary>
    /// The application's preferences.
    /// </summary>
    /// <value>The prefs.</value>
    protected AppPrefs appPrefs { get; private set; }
    /// <summary>
    /// The ion instance as a context.
    /// </summary>
    /// <value>The ion.</value>
    protected Context context { get { return appPrefs.context; } }
    /// <summary>
    /// The preferences provided by the app prefs.
    /// </summary>
    /// <value>The prefs.</value>
    protected ISharedPreferences prefs { get { return appPrefs.prefs; } }

		protected BasePreferences(AppPrefs prefs) {
      appPrefs = prefs;
    }

		/// <summary>
		/// Queries the preference string with the given key. If the preference doesn't exist or can't be retrieved, we will
		/// return fallback.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="key">Key.</param>
		/// <param name="fallback">Fallback.</param>
		public string GetString(int key, string fallback) {
			return prefs.GetString(context.GetString(key), fallback);
		}

		/// <summary>
		/// Queries a boolean from a string preference. If the preference doesn't exist or can't be retrieved, we will
		/// return fallback.
		/// </summary>
		/// <returns>The int from string.</returns>
		/// <param name="key">Key.</param>
		/// <param name="fallback">Fallback.</param>
		public bool GetBool(int key, bool fallback) {
			return prefs.GetBoolean(context.GetString(key), fallback);
		}

    /// <summary>
    /// Queries a float from preferences.
    /// </summary>
    /// <returns>The float.</returns>
    /// <param name="key">Key.</param>
    /// <param name="fallback">Fallback.</param>
    public float GetFloat(int key, float fallback) {
      return prefs.GetFloat(context.GetString(key), fallback);
    }

		/// <summary>
		/// Queries an integer from a string preference. If the preference doesn't exist or can't be retrieved, we will
		/// return fallback.
		/// </summary>
		/// <returns>The int from string.</returns>
		/// <param name="key">Key.</param>
		/// <param name="fallback">Fallback.</param>
		public int GetIntFromString(int key, int fallback) {
			var str = GetString(key, fallback + "");

			try {
				return int.Parse(str);
			} catch (Exception e) {
				Log.E(this, "Failed to retrieve int from string preference: " + context.GetString(key), e);
	      return fallback;
			}
		}

		/// <summary>
		/// Puts the string into the preferences.
		/// </summary>
		/// <returns><c>true</c>, if string was put, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public bool PutString(int key, string value) {
			var e = prefs.Edit();
			e.PutString(context.GetString(key), value);
			return e.Commit();
		}

		/// <summary>
		/// Puts the bool into the preferences.
		/// </summary>
		/// <returns><c>true</c>, if string was put, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public bool PutBool(int key, bool value) {
			var e = prefs.Edit();
			e.PutBoolean(context.GetString(key), value);
			return e.Commit();
		}

		/// <summary>
		/// Puts the int into the preferences.
		/// </summary>
		/// <returns><c>true</c>, if string was put, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public bool PutInt(int key, int value) {
			var e = prefs.Edit();
			e.PutInt(context.GetString(key), value);
			return e.Commit();
		}

		/// <summary>
		/// Puts the float into the preferences.
		/// </summary>
		/// <returns><c>true</c>, if string was put, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public bool PutFloat(int key, float value) {
			var e = prefs.Edit();
			e.PutFloat(context.GetString(key), value);
			return e.Commit();
		}
  }

  public class DevicePreferences : BasePreferences, IDevicePreferences {
    // Implemented for IIONPreferences
    public bool allowDeviceAutoConnect {
      get {
        return GetBool(Resource.String.pkey_device_auto_connect, true);
      }
      set {
        PutBool(Resource.String.pkey_device_auto_connect, value);
      }
    }

    // Implemented for IIONPreferences
    public bool allowLongRangeMode {
      get {
        return GetBool(Resource.String.pkey_device_long_range, false);
      }

      set {
        PutBool(Resource.String.pkey_device_long_range, value);
      }
    }

    // Implemented for IIONPreferences
    public TimeSpan rateOfChangeWindow {
      get {
        return TimeSpan.FromMilliseconds(trendInterval.TotalMilliseconds * 300);
      }
    }

    // Implemented for IDevicePreferences
    public TimeSpan trendInterval {
      get {
        return TimeSpan.FromMilliseconds(GetIntFromString(Resource.String.pkey_device_trend_interval, 1000));
      }
      set {
        PutInt(Resource.String.pkey_device_trend_interval, (int)value.TotalMilliseconds);
      }
    }

    public DevicePreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's alarm preferences.
  /// </summary>
  public class AlarmPreferences : BasePreferences, IAlarmPreferences {
    /// <summary>
    /// Queries whether or not the user will allow the application to vibrate for alarms.
    /// </summary>
    /// <value><c>true</c> if allows vibrate; otherwise, <c>false</c>.</value>
    public bool allowsVibrate {
      get {
        return GetBool(Resource.String.pkey_alarm_vibrate, true);
      }
    }

    /// <summary>
    /// Queries whether or not the user will allow the application to play sound for alarms.
    /// </summary>
    /// <value><c>true</c> if allows sounds; otherwise, <c>false</c>.</value>
    public bool allowsSounds {
      get {
        return GetBool(Resource.String.pkey_alarm_sound, true);
      }
    }

		public AlarmPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  public class LocationPreferences : BasePreferences, ILocationPreferences {
    /// <summary>
    /// Queries whether or not the user will allow the application to use the device's GPS.
    /// </summary>
    /// <value><c>true</c> if allows gps; otherwise, <c>false</c>.</value>
    public bool allowsGps {
      get {
        return GetBool(Resource.String.pkey_location_gps, false);
      }
      set {
        PutBool(Resource.String.pkey_location_gps, value);
      }
    }

    // Implemented for ILocationPreferences
    public Scalar customElevation {
      get {
        var raw = GetFloat(Resource.String.pkey_location_elevation, 0.0f);
        return Units.Length.METER.OfScalar(raw).ConvertTo(appPrefs.units.length);
      }

      set {
        var raw = value.ConvertTo(Units.Length.METER);
        PutFloat(Resource.String.pkey_location_elevation, (float)raw.amount);
      }
    }

		/// <summary>
		/// Whether or not the application should ask for the user's location permission.
		/// </summary>
		/// <value><c>true</c> if ask for permissions; otherwise, <c>false</c>.</value>
		public bool askForPermissions {
			get {
				return GetBool(Resource.String.pkey_location_ask_for_permissions, true);
			}
			set {
				PutBool(Resource.String.pkey_location_ask_for_permissions, value);
			}
		}

    public LocationPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to store the local application's network preferences.
  /// </summary>
  public class NetworkPreferences : BasePreferences {

    /// <summary>
    /// The last date that we checked the rss feed.
    /// </summary>
    /// <value>The last rss check date.</value>
    public DateTime lastRssCheckDate {
      get {
        var usDateString = GetString(Resource.String.pkey_network_last_rss_check_date, null);
        if (usDateString == null) {
          return new DateTime(1, 1, 1);
        } else {
          try {
            return DateTime.Parse(usDateString);
          } catch (Exception e) {
            var key = context.GetString(Resource.String.pkey_network_last_rss_check_date);
            Log.E(this, "Failed to DateTime parse preference: " + key + " {" + usDateString + "}", e);
            return new DateTime(1, 1, 1);
          }
        }
      }
      set {
        PutString(Resource.String.pkey_network_last_rss_check_date, value.ToLongDateString());
      }
    }

    public NetworkPreferences(AppPrefs prefs) : base(prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's default unit preferences.
  /// </summary>
  public class UnitPreferences : BasePreferences, IUnitPreferences { 
    // Overridden from IUnits
    public Unit length {
      get {
        return AssertUnitGet(Resource.String.pkey_unit_length, Units.Length.FOOT);
      }
      set {
        AssertUnitSet(Resource.String.pkey_unit_length, Quantity.Length, value);
      }
    }

    // Overridden from IUnits
    public Unit pressure {
      get {
        return AssertUnitGet(Resource.String.pkey_unit_pressure, Units.Pressure.PSIG);
      }
      set {
        AssertUnitSet(Resource.String.pkey_unit_pressure, Quantity.Pressure, value);
      }
    }

    // Overridden from IUnits
    public Unit temperature {
      get {
        return AssertUnitGet(Resource.String.pkey_unit_temperature, Units.Temperature.FAHRENHEIT);
      }
      set {
        AssertUnitSet(Resource.String.pkey_unit_temperature, Quantity.Temperature, value);
      }
    }

    // Overridden from IUnits
    public Unit vacuum {
      get {
        return AssertUnitGet(Resource.String.pkey_unit_vacuum, Units.Vacuum.MICRON);
      }
      set {
        AssertUnitSet(Resource.String.pkey_unit_vacuum, Quantity.Vacuum, value);
      }
    }

		// Overridden from IUnits
		public Unit weight {
			get {
				return AssertUnitGet(Resource.String.pkey_unit_weight, Units.Weight.POUND_FORCE);
			}
			set {
				AssertUnitSet(Resource.String.pkey_unit_weight, Quantity.Force, value);
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
    private Unit AssertUnitGet(int preferenceKey, Unit backup) {
      var key = context.GetString(preferenceKey);

      try {
        var ret = UnitLookup.GetUnit(int.Parse(prefs.GetString(key, null)));  
        return ret;
      } catch (Exception e) {
        Log.E(this, "Failed to retrieve unit for key: " + key, e);
        AssertUnitSet(preferenceKey, backup.quantity, backup);
        return backup;
      }
    }

    /// <summary>
    /// Safely attempts to set the unit for the given key.
    /// </summary>
    /// <returns>The unit set.</returns>
    private void AssertUnitSet(int preferenceKey, Quantity quantity, Unit unit) {
      var key = context.GetString(preferenceKey);
      try {
        if (quantity != unit.quantity) {
          throw new ArgumentException("Unit: " + unit + " is not compatible with quantity + " + quantity);
        }

        var e = prefs.Edit();

        e.PutString(key, UnitLookup.GetCode(unit) + "");

        e.Commit();
      } catch (Exception e) {
        Log.E(this, "Failed to set unit " + unit + " for key: " + key, e);
      }
    }
  }

	public class ReportPreferences : BasePreferences, IReportPreferences {
    // Implemented for IReportPreferences
		public TimeSpan dataLoggingInterval {
			get {
				return TimeSpan.FromSeconds(GetIntFromString(Resource.String.pkey_reporting_data_log_interval, 60));
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

	public class PortalPreferences : BasePreferences, IPortalPreferences {
    // Implemented for IPortalPreferences
		public bool rememberMe {
			get {
				return GetBool(Resource.String.pkey_portal_rememberme, false);
			}
			set {
				PutBool(Resource.String.pkey_portal_rememberme, value);
			}
		}

    // Implemented for IPortalPreferences
		public string username {
			get {
				return GetString(Resource.String.pkey_portal_username, "");
			}

			set {
				PutString(Resource.String.pkey_portal_username, value);
			}
		}

    // Implemented for IPortalPreferences
		public string password {
			get {
				return GetString(Resource.String.pkey_portal_password, "");
			}

			set {
				PutString(Resource.String.pkey_portal_password, value);
			}
		}

    public PortalPreferences(AppPrefs prefs) : base(prefs) {
		}
	}
}

