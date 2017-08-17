namespace ION.Core.App {

  using System;
  using System.Threading.Tasks;

  using Appion.Commons.Measure;

  using ION.Core.Fluids;
  using ION.Core.Sensors;

  public interface IIONPreferences {
		/// <summary>
		/// The event that is notified when the backing preferences changed. Because preferences are different on various
		/// platforms, we cannot really adequately provide a way to say what preference changed.
		/// </summary>
		event Action onPreferencesChanged;
    /// <summary>
    /// Queries the last known app version.
    /// </summary>
    /// <value>The app version.</value>
    string lastKnownAppVersion { get; set; }
    /// <summary>
    /// Queries the unique identifier for this installation of the app. This is used to identiy the device for various
    /// portal and networking actions.
    /// </summary>
    /// <value>The app identifier.</value>
    Guid appId { get; }
    /// <summary>
    /// Queries whether or not the user allows app analytics.
    /// </summary>
    /// <value><c>true</c> if allow app analytics; otherwise, <c>false</c>.</value>
    bool allowAppAnalytics { get; set; }

    /// <summary>
    /// Queries the user's device preferences.
    /// </summary>
    /// <value>The device.</value>
    IDevicePreferences device { get; }
    /// <summary>
    /// Queries the alarm preferences.
    /// </summary>
    /// <value>The alarm.</value>
    IAlarmPreferences alarm { get; }
    /// <summary>
    /// Queries the fluid preferences.
    /// </summary>
    /// <value>The fluid.</value>
    IFluidPreferences fluid { get; }
    /// <summary>
    /// Queries the location preferences.
    /// </summary>
    /// <value>The location.</value>
    ILocationPreferences location { get; }
    /// <summary>
    /// Queries the unit preferences.
    /// </summary>
    /// <value>The units.</value>
    IUnitPreferences units { get; }
    /// <summary>
    /// Queries the report preferences.
    /// </summary>
    /// <value>The report.</value>
    IReportPreferences report { get; }
    /// <summary>
    /// Queries the portal preferences.
    /// </summary>
    /// <value>The portal.</value>
    IPortalPreferences portal { get; }

  }

  /// <summary>
  /// The user's device preferences.
  /// </summary>
  public interface IDevicePreferences {
    /// <summary>
    /// Queries whether or not it is the user's preference to allow devices to auto reconnect.
    /// </summary>
    /// <value><c>true</c> if allow device auto connect; otherwise, <c>false</c>.</value>
    bool allowDeviceAutoConnect { get; set; }
    /// <summary>
    /// Queries whether or not the user is allowing long range mode.
    /// </summary>
    /// <value><c>true</c> if allow long range mode; otherwise, <c>false</c>.</value>
    bool allowLongRangeMode { get; set; }
    /// <summary>
    /// The interval between rate of change data logs.
    /// Note: The total number of points is calculated by window / interval, that is to say, that interval more than
    /// window, affects that total memory used by the rate of change component.
    /// </summary>
    /// <value>The rate of change interval.</value>
    TimeSpan trendInterval { get; set; }
  }

  /// <summary>
  /// The interface that describes the default units for the application.
  /// </summary>
  public interface IUnitPreferences { 
    Unit length { get; set; }
    Unit pressure { get; set; }
    Unit temperature { get; set; }
    Unit vacuum { get; set; }
  } // End IUnits

  public interface IAlarmPreferences {
  }

  public interface IFluidPreferences {
    string preferredFluid { get; set; }
    string[] favorites { get; set; }
  }

  public interface ILocationPreferences {
    Scalar customElevation { get; set; }
  }

  public interface IReportPreferences {
    TimeSpan dataLoggingInterval { get; }
  }

  public interface IPortalPreferences {
    bool rememberMe { get; set; }
    string username { get; set; }
    string password { get; set; }
  }

  /// <summary>
  /// A class that provides extension methods to the IUnitPreferences interface.
  /// </summary>
  public static class IUnitPreferenceExtensions {
    /// <summary>
    /// Resolves the default unit for the given sensor type.
    /// </summary>
    /// <returns>The unit for.</returns>
    /// <param name="prefs">Prefs.</param>
    /// <param name="sensorType">Sensor type.</param>
		public static Unit DefaultUnitFor(this IUnitPreferences prefs, ESensorType sensorType) {
			switch (sensorType) {
				case ESensorType.Length:
				return prefs.length;
				case ESensorType.Pressure:
				return prefs.pressure;
				case ESensorType.Temperature:
				return prefs.temperature;
				case ESensorType.Vacuum:
				return prefs.vacuum;
				default:
				return sensorType.GetDefaultUnit();
			}
		}

    /// <summary>
    /// Converts the scalar to the preferred unit for the quantity backing the scalar.
    /// </summary>
    /// <returns>The default unit.</returns>
    /// <param name="prefs">Prefs.</param>
    /// <param name="scalar">Scalar.</param>
		public static Scalar ToDefaultUnit(this IUnitPreferences prefs, Scalar scalar) {
			var unit = prefs.DefaultUnitFor(scalar.unit.quantity.AsSensorType());
			return scalar.ConvertTo(unit);
		}
  }
}
