namespace ION.Droid.Preferences {
  
  using System;

  using Android.Content;

  using ION.Core.App;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.App;

  /// <summary>
  /// A simple class that is used to access the android applications preferences.
  /// </summary>
  public class AppPrefs {

    /// <summary>
    /// The alarm preferences.
    /// </summary>
    /// <value>The alarm.</value>
    public AlarmPreferences alarm { get; private set; }
    /// <summary>
    /// The unit preferences.
    /// </summary>
    /// <value>The units.</value>
    public UnitPreferences units { get; private set; }

    /// <summary>
    /// The ion instance as a context.
    /// </summary>
    /// <value>The ion.</value>
    public AndroidION ion { get; private set; }
    /// <summary>
    /// The application's preferences.
    /// </summary>
    /// <value>The prefs.</value>
    public ISharedPreferences prefs { get; set; }

    public AppPrefs(AndroidION ion, ISharedPreferences prefs) {
      alarm = new AlarmPreferences(ion, prefs);
      units = new UnitPreferences(ion, prefs);
    }
  }

  /// <summary>
  /// A simple wrapper that will provide convenience methods for preferences.
  /// </summary>
  public class BasePreferences {
    /// <summary>
    /// The ion instance as a context.
    /// </summary>
    /// <value>The ion.</value>
    public AndroidION ion { get; private set; }
    /// <summary>
    /// The application's preferences.
    /// </summary>
    /// <value>The prefs.</value>
    public ISharedPreferences prefs { get; set; }

    protected BasePreferences(AndroidION ion, ISharedPreferences prefs) {
      this.ion = ion;
      this.prefs = prefs;
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's alarm preferences.
  /// </summary>
  public class AlarmPreferences : BasePreferences {
    /// <summary>
    /// Queries whether or not the user will allow the application to vibrate for alarms.
    /// </summary>
    /// <value><c>true</c> if allows vibrate; otherwise, <c>false</c>.</value>
    public bool allowsVibrate {
      get {
        return prefs.GetBoolean(ion.GetString(Resource.String.pkey_alarm_vibrate), true);
      }
    }

    /// <summary>
    /// Queries whether or not the user will allow the application to play sound for alarms.
    /// </summary>
    /// <value><c>true</c> if allows sounds; otherwise, <c>false</c>.</value>
    public bool allowsSounds {
      get {
        return prefs.GetBoolean(ion.GetString(Resource.String.pkey_alarm_sound), true);
      }
    }

    public AlarmPreferences(AndroidION ion, ISharedPreferences prefs) : base(ion, prefs) {
    }
  }

  /// <summary>
  /// The preferences that are used to query the application's default unit preferences.
  /// </summary>
  public class UnitPreferences : BasePreferences, IUnits { 
    // Overridden from IUnits
    public Unit length {
      get {
        return AssertUnitGet(Resource.String.preferences_units_length, Units.Length.FOOT);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_length, Quantity.Length, Units.Length.FOOT);
      }
    }

    // Overridden from IUnits
    public Unit pressure {
      get {
        return AssertUnitGet(Resource.String.preferences_units_pressure, Units.Pressure.PSIG);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_pressure, Quantity.Pressure, Units.Pressure.PSIG);
      }
    }

    // Overridden from IUnits
    public Unit temperature {
      get {
        return AssertUnitGet(Resource.String.preferences_units_temperature, Units.Temperature.FAHRENHEIT);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_temperature, Quantity.Temperature, Units.Temperature.FAHRENHEIT);
      }
    }

    // Overridden from IUnits
    public Unit vacuum {
      get {
        return AssertUnitGet(Resource.String.preferences_units_vacuum, Units.Vacuum.MICRON);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_vacuum, Quantity.Vacuum, Units.Vacuum.MICRON);
      }
    }

    public UnitPreferences(AndroidION ion, ISharedPreferences prefs) : base(ion, prefs) {
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
      var key = ion.GetString(preferenceKey);

      try {
        return UnitLookup.GetUnit(int.Parse(prefs.GetString(key, null)));  
      } catch (Exception e) {
        Log.E(this, "Failed to retrieve unit for key: " + key);
        AssertUnitSet(preferenceKey, backup.quantity, backup);
        return backup;
      }
    }

    /// <summary>
    /// Safely attempts to set the unit for the given key.
    /// </summary>
    /// <returns>The unit set.</returns>
    /// <param name="prefernceKey">Prefernce key.</param>
    private void AssertUnitSet(int preferenceKey, Quantity quantity, Unit unit) {
      var key = ion.GetString(preferenceKey);
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
}

