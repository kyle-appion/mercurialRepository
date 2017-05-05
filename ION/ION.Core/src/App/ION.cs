namespace ION.Core.App {

	using System;
	using System.Threading.Tasks;

  using Newtonsoft.Json;

	using ION.Core.Alarms;
	using ION.Core.Content;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Report.DataLogs;

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
  } // End ION

	public delegate void OnWorkbenchChanged(Workbench workbench);
	public delegate void OnAnalyzerChanged(Analyzer analyzer);
  public delegate void RemotePlatformChanged(IPlatformInfo platformInfo);
	public delegate void OnIonStateChanged(IonState.EType eventType);

  /// <summary>
  /// The interface that describes an ION application context.
  /// </summary>
  public interface IION : IDisposable {
		/// <summary>
		/// The event that is notified when the ion's workbench changes.
		/// </summary>
		event OnWorkbenchChanged onWorkbenchChanged;
		/// <summary>
		/// The event that is notified when the ion's analyzer changes.
		/// </summary>
		event OnAnalyzerChanged onAnalyzerChanged;
		/// <summary>
		/// The event that is notified when remote viewing platform information is updated
		/// </summary>
		event RemotePlatformChanged remotePlatformChanged;    
    /// <summary>
    /// Occurs when on ion state changed. This can be workbench, analyzer, remote, etc... changes.
    /// Basically anything that deals with core updates/changes
    /// </summary>
    event OnIonStateChanged onIonStateChanged;		
    /// <summary>
    /// Queries the build name of the ion instance. (ie. ION HVAC/r for android of ION Viewer for iOS)
    /// </summary>
    /// <value>The name.</value>
    string name { get; }
    /// <summary>
    /// The current application version.
    /// </summary>
    /// <value>The version.</value>
    string version { get; } 
    /// <summary>
    /// Queries the user and common app preferences for the ION application.
    /// </summary>
    /// <value>The preferences.</value>
    IIONPreferences preferences { get; }

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

		/// <summary>
		/// Saves the current workbench to the primary workbench save location.
		/// </summary>
		/// <returns>The workbench async.</returns>
    Task SaveWorkbenchAsync();
		/// <summary>
		/// Saves the current analyzer to the primary analyzer save location.
		/// </summary>
		/// <returns>The workbench async.</returns>
		Task SaveAnalyzerAsync();
		/// <summary>
		/// Loads the current workbench from the primary workbench save location.
		/// </summary>
		/// <returns>The workbench async.</returns>
		Task<Workbench> LoadWorkbenchAsync();
		/// <summary>
		/// Loads the current analyzer from the primary analyzer save location.
		/// </summary>
		/// <returns>The workbench async.</returns>
		Task<Analyzer> LoadAnalyzerAsync();

    Task<Workbench> LoadWorkbenchAsync(IFile file);
		Task<Analyzer> LoadAnalyzerAsync(IFile file);

    /// <summary>
    /// Creates a new application dump object.
    /// </summary>
    /// <returns>The application dump.</returns>
    IAppDump CreateApplicationDump();
    
    /// <summary>
    /// Gets the platform specific info.
    /// </summary>
    /// <returns>The platform info.</returns>
    IPlatformInfo GetPlatformInformation();
    /// <summary>
    /// Sets the platform information for a remote device.
    /// </summary>
    /// <returns>The remote platform information.</returns>
    void SetRemotePlatformInformation(sessionStateInfo remoteState);
    /// <summary>
    /// Gets or sets the local device platform information.
    /// </summary>
    /// <value>The local device.</value>
    IPlatformInfo localDevice {get; set;}
    /// <summary>
    /// Gets or sets the remote device platform information.
    /// </summary>
    /// <value>The remote device.</value>
    IPlatformInfo remoteDevice {get; set;}   
    
    Task setRemoteDeviceManager();
    Task setOriginalDeviceManager();
    
    
    void NotifyStateChanged(IonState.EType eventType);
  } // End IION

  	[Preserve(AllMembers = true)]
	public class sessionStateInfo {
		public sessionStateInfo(){}
		[JsonProperty("log")]
		public int isRecording { get; set; }
		[JsonProperty("battery")]
		public int batteryLevel { get; set; }
		[JsonProperty("wifi")]
		public int wifiStatus { get; set; }
		[JsonProperty("memory")]
		public long remainingMemory { get; set; }	
	}
}

