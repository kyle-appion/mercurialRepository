namespace ION.IOS.App {

	using Foundation;

  public class AppSettings {
    const string PREFERENCES_PATH = "Settings.bundle/Root.plist";
    const string KEY_PREFERENCE_SPECIFIERS = "PreferenceSpecifiers";
    const string KEY_KEY = "Key";
    const string KEY_DEFAULT_VALUE = "DefaultValue";

    // App
    const string KEY_ANALYTICS = "settings_app_analytics";

    // Location
    const string KEY_USE_GEO_LOCATION = "settings_location_use_geolocation";

    // Default Unit Keys
    const string KEY_LENGTH = "settings_units_default_length";
    const string KEY_PRESSURE = "settings_units_default_pressure";
    const string KEY_TEMPERATURE = "settings_units_default_temperature";
    const string KEY_VACUUM = "settings_units_default_vacuum";

    // Alarm
    const string KEY_SOUND = "settings_alarm_sound";
    const string KEY_HAPTIC = "settings_alarm_haptic";

    // Screen Settings
    const string KEY_WAKE_LOCK = "settings_screen_leave_on";

    public readonly App app = new App();
    public readonly Location location = new Location();
    public readonly DefaultUnits units = new DefaultUnits();
    public readonly Alarm alarm = new Alarm();
    public readonly ScreenSettings screen = new ScreenSettings();

    private NSDictionary settingsBundle { get; set; }

    public AppSettings() {
      // So. This is a doozy. Ios, in all of its infinite wisdom, doesn't have a
      // method that will validate the existance of settings. So, we instead have
      // to poll a "test value" and assert that it is not "a default value" (ie.
      // 0 for an int, null for a string, false for a bool). If this assertion
      // fails, then we will need to load the defaults in the most heinous way
      // I have ever seen anything loaded.
      var testValue = NSUserDefaults.StandardUserDefaults.IntForKey(KEY_PRESSURE);
      if (testValue == 0) {
        // We need to load defaults
        LoadDefaults();
      }
      Initialize();
    }

    /// <summary>
    /// Loads the default settings.
    /// </summary>
    private void LoadDefaults() {
      var settings = new NSDictionary(NSBundle.MainBundle.PathForResource(PREFERENCES_PATH, null));

      var specifierArray = settings[KEY_PREFERENCE_SPECIFIERS] as NSArray;
      foreach (var item in NSArray.FromArray<NSDictionary>(specifierArray)) {
        var key = (NSString)item[KEY_KEY];
        if (key == null) {
          continue;
        }

        var val = item[KEY_DEFAULT_VALUE];

        switch (key.ToString()) { 
          // App
          case KEY_ANALYTICS:
            app.analytics = ((NSNumber)val).BoolValue;
            break;

          // Location
          case KEY_USE_GEO_LOCATION:
            location.useGeoLocation = ((NSNumber)val).BoolValue;
            break;

          // Default Units
          case KEY_LENGTH:
            units.length = ((NSNumber)val).Int32Value;
            break;
          case KEY_PRESSURE:
            units.pressure = ((NSNumber)val).Int32Value;
            break;
          case KEY_TEMPERATURE:
            units.temperature = ((NSNumber)val).Int32Value;
            break;
          case KEY_VACUUM:
            units.vacuum = ((NSNumber)val).Int32Value;
            break;

          // Alarm
          case KEY_SOUND:
            alarm.sound = ((NSNumber)val).BoolValue;
            break;
          case KEY_HAPTIC:
            alarm.haptic = ((NSNumber)val).BoolValue;
            break;
          case KEY_WAKE_LOCK:
            screen.leaveOn = ((NSNumber)val).BoolValue;
            break;
        }
      } // End foreach

      var defaults = new NSDictionary(
      // Location
        KEY_USE_GEO_LOCATION, location.useGeoLocation,

      // Default Unit Keys
        KEY_LENGTH, units.length,
        KEY_PRESSURE, units.pressure,
        KEY_TEMPERATURE, units.temperature,
        KEY_VACUUM, units.vacuum,

      // Alarm
        KEY_SOUND, alarm.sound,
        KEY_HAPTIC, alarm.haptic,

      // Screen
        KEY_WAKE_LOCK, screen.leaveOn
      );

      NSUserDefaults.StandardUserDefaults.RegisterDefaults(defaults);
      NSUserDefaults.StandardUserDefaults.Synchronize();
    }

    /// <summary>
    /// Initializes the preferences.
    /// </summary>
    private void Initialize() {
      units.length = Int(KEY_LENGTH);
      units.pressure = Int(KEY_PRESSURE);
      units.temperature = Int(KEY_TEMPERATURE);
      units.vacuum = Int(KEY_VACUUM);
      location.useGeoLocation = Bool(KEY_USE_GEO_LOCATION);
    }

    private static int Int(string key) {
      return (int)NSUserDefaults.StandardUserDefaults.IntForKey(key);
    }

    private static bool Bool(string key) {
      return NSUserDefaults.StandardUserDefaults.BoolForKey(key);
    }

    private static string String(string key) {
      return NSUserDefaults.StandardUserDefaults.StringForKey(key);
    }
  }

  public class Location {
    public bool useGeoLocation { get; set; }
  }

  public class DefaultUnits {
    public int length {
			get {
				return __length;
			}
			set {
				if (value == 0) {
					value = 21;
				}

				__length = value;
			}
		} int __length = 21;

    public int pressure {
			get {
				return __pressure;
			}
			set {
				if (value == 0) {
					value = 7;
				}

				__pressure = value;
			}
		} int __pressure = 7;

    public int temperature {
			get {
				return __temperature;
			}
			set {
				if (value  == 0) {
					value = 18;
				}

				__temperature = value;
			} 
		} int __temperature = 18;


    public int vacuum {
			get {
				return __vacuum;
			}
			set {
				if (value == 0) {
					value = 39;
				}

				__vacuum = value;
			}
		} int __vacuum = 39;
  }

  public class App {
    public bool analytics { get; set; }
  }

  public class Alarm {
    public bool sound { get; set; }
    public bool haptic { get; set; }
  }

  public class ScreenSettings{
    public bool leaveOn {get; set;}
  }
}
