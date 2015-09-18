using System;


using Foundation;

namespace ION.IOS.App {
  public class AppSettings {
    const string PREFERENCES_PATH = "Settings.bundle/Root.plist";
    const string KEY_PREFERENCE_SPECIFIERS = "PreferenceSpecifiers";
    const string KEY_KEY = "Key";
    const string KEY_DEFAULT_VALUE = "DefaultValue";

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

    public readonly Location location = new Location();
    public readonly DefaultUnits units = new DefaultUnits();
    public readonly Alarm alarm = new Alarm();

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

      /*
      var tmp = new NSDictionary(NSBundle.MainBundle.PathForResource(bundlePath, null));
      settingsBundle = new NSDictionary();

      var preferences = tmp[KEY_PREFERENCE_SPECIFIERS] as NSArray;

      foreach (var item in NSArray.FromArray<NSDictionary>(preferences)) {
        var key = (NSString)item[KEY_KEY];
        if (key == null) {
          continue;
        }

        settingsBundle[key] = item;
      }
      */
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
        ION.Core.Util.Log.D(this, "Resolving key " + key);

        switch (key.ToString()) {
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
        KEY_HAPTIC, alarm.haptic
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
    public int length { get; set; }
    public int pressure { get; set; }
    public int temperature { get; set; }
    public int vacuum { get; set; }
  }

  public class Alarm {
    public bool sound { get; set; }
    public bool haptic { get; set; }
  }
}
