namespace ION.Core.App {

  using System;

  using Appion.Commons.Measure;

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
    /// The window (length of time) that the rate of change will record data points for.
    /// </summary>
    /// <value>The rate of change window.</value>
    TimeSpan rateOfChangeWindow { get; }
    /// <summary>
    /// The interval between rate of change data logs.
    /// Note: The total number of points is calculated by window / interval, that is to say, that interval more than
    /// window, affects that total memory used by the rate of change component.
    /// </summary>
    /// <value>The rate of change interval.</value>
    TimeSpan rateOfChangeInterval { get; set; }
  }

  /// <summary>
  /// The interface that describes the default units for the application.
  /// </summary>
  public interface IUnitPreferences { 
    Unit length { get; set; }
    Unit pressure { get; set; }
    Unit temperature { get; set; }
    Unit vacuum { get; set; }

    Unit DefaultUnitFor(ESensorType sensorType);
  } // End IUnits

  public interface IAlarmPreferences {
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
}
