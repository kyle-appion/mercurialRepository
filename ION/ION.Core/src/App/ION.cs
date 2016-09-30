namespace ION.Core.App {

	using System;
	using System.Threading.Tasks;

	using Newtonsoft.Json.Linq;

	using ION.Core.Alarms;
	using ION.Core.Content;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Measure;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;
	using ION.Core.Util;

	/// <summary>
	/// A utility class that will retain the ION context. We do this because
	/// interfaces can't have static properties. 
	/// </summary>
	public static class AppState {
    /// <summary>
    /// The current app state. If the current app state has not been set,
    /// then we will throw an exception when the property is fetched.
    /// </summary>
    /// <value>The App.</value>
    public static IION context { get; set; }
      /*
      get {
        if (__context == null) {
          string msg = "Critical failure: Application attempted to retrieve ION context, yet the application was not running.";
          Log.C("AppState.ION", msg);
          throw new Exception(msg);
        }
        return __context;
      }
      set {
        __context = value;
      }
    } private static IION __context;
    */
  } // End ION

	public delegate void OnWorkbenchChanged(Workbench workbench);

  /// <summary>
  /// The interface that describes an ION application context.
  /// </summary>
  public interface IION : IDisposable {
		/// <summary>
		/// The event that is notified when the ion's workbench changes.
		/// </summary>
		event OnWorkbenchChanged onWorkbenchChanged;
    /// <summary>
    /// Queries the build name of the ion instance. (ie. ION HVAC/r for android of ION Viewer for iOS)
    /// </summary>
    /// <value>The name.</value>
    string name { get; }
    /// <summary>
    /// Queries the full version of the ion instance.
    /// </summary>
    /// <value>The version.</value>
    string version { get; }

    /// <summary>
    /// The database that will store all of the application data.
    /// </summary>
    /// <value>The database.</value>
    IONDatabase database { get;}

    /// <summary>
    /// The FileSystem that will allow the ion context to access the native
    /// platforms files.
    /// </summary>
    IFileManager fileManager { get; }
    /// <summary>
    /// Queries the device manager for the ION instance.
    /// </summary>
    /// <value>The device manager.</value>
    IDeviceManager deviceManager { get; }
    /// <summary>
    /// Queries the alarm manager.
    /// </summary>
    /// <value>The alarm manager.</value>
    IAlarmManager alarmManager { get; }
    /// <summary>
    /// Queries the fluid manager that is responsible for acquiring and
    /// maintaining the applications fluids.
    /// </summary>
    IFluidManager fluidManager { get; }
    /// <summary>
    /// Queries the location manager that is responsbile for ascertaining the user's altitude.
    /// </summary>
    /// <value>The location manager.</value>
    ILocationManager locationManager { get; }
    /// <summary>
    /// Queries the data log manager that is responsible for storing sensor data.
    /// </summary>
    /// <value>The data log manager.</value>
    DataLogManager dataLogManager { get; }
		
		
    /// <summary>
    /// The current primary analyzer for the ion context.
    /// </summary>
    /// <value>The current analyzer.</value>
    Analyzer currentAnalyzer { get; set; }
    /// <summary>
    /// The current primary workbench for the ION context.
    /// </summary>
    /// <value>The current workbench.</value>
    Workbench currentWorkbench { get; set; }

    /// <summary>
    /// The default units for the ION instance.
    /// </summary>
    /// <value>The default units.</value>
    IUnits defaultUnits { get; }

    /// <summary>
    /// Queries the screenshot report folder.
    /// </summary>
    /// <returns>The screenshot report folder.</returns>
    /// <exception cref="IOException">If the folder could not be retrieved.</exception>
    IFolder screenshotReportFolder { get; }

    /// <summary>
    /// Queries the folder where calibration certificates are placed.
    /// </summary>
    /// <value>The calibraaction certificate folder.</value>
    IFolder calibrationCertificateFolder { get; }

    /// <summary>
    /// Posts the action to the main message pump for execution on the main thread.
    /// </summary>
    /// <param name="action">Action.</param>
    void PostToMain(Action action);
    /// <summary>
    /// Posts an action to the main message pump for execution on the main thread after
    /// a given delay.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="delay">Delay.</param>
    void PostToMainDelayed(Action action, TimeSpan delay);

    Task SaveWorkbenchAsync();

    Task<Workbench> LoadWorkbenchAsync(IFile file);

    /// <summary>
    /// Creates a new application dump object.
    /// </summary>
    /// <returns>The application dump.</returns>
    IAppDump CreateApplicationDump();
    
    Task setRemoteDeviceManager();
    Task setOriginalDeviceManager();
  } // End IION

  /// <summary>
  /// The interface that describes the default units for the application.
  /// </summary>
  public interface IUnits { 
    Unit length { get; set; }
    Unit pressure { get; set; }
    Unit temperature { get; set; }
    Unit vacuum { get; set; }

    Unit DefaultUnitFor(ESensorType sensorType);
  } // End IUnits
}

