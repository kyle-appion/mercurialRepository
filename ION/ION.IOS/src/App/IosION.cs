namespace ION.IOS.App {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using CoreFoundation;
	using Foundation;
	using UIKit;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Alarms;
	using ION.Core.Alarms.Alerts;
	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Content.Parsers;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;

	using ION.IOS.Alarms.Alerts;
	using ION.IOS.IO;
	using ION.IOS.Location;
	using ION.IOS.Connections;
	using ION.Core.Net;

	/// <summary>
	/// The iOS ION implementation.
	/// </summary>
	public class IosION : IION {
    /// <summary>
    /// The file name for the primary workbench.
    /// </summary>
    public const string FILE_WORKBENCH = "primaryWorkbench.workbench";

    private static string GetDisplayName() {
      var info = NSBundle.MainBundle.InfoDictionary;
      var appDisplayName = info.ObjectForKey(new NSString("CFBundleDisplayName"));
      return ((NSString)appDisplayName).ToString();
    }

    private static string GetVersion() {
      var info = NSBundle.MainBundle.InfoDictionary;
      var version = info.ObjectForKey(new NSString("CFBundleVersion"));
      return ((NSString)version).ToString();
    }

		/// <summary>
		/// Occurs when on workbench changed.
		/// </summary>
		public event OnWorkbenchChanged onWorkbenchChanged;

		public event OnAnalyzerChanged onAnalyzerChanged;

    // Overridden from IION
    public string name { get { return GetDisplayName(); } }
    // Overridden from IION
    public string version { get { return GetVersion(); } }

    // Overridden from IION
    public IONDatabase database {
			get {
				return __database;
			}
			set {
				if (__database != null) {
					managers.Remove(__database);
				}

				__database = value;

				if (__database != null) {
					managers.Add(__database);
				}
			}
		} private IONDatabase __database;
    // Overridden from IION
    public IFileManager fileManager {
			get {
				return __fileManager;
			}
			set {
				if (__fileManager != null) {
					managers.Remove(__fileManager);
				}

				__fileManager = value;

				if (__fileManager != null) {
					managers.Add(__fileManager);
				}
			}
		} private IFileManager __fileManager;
    // Overridden from IION
    public IDeviceManager deviceManager {
			get {
				return __deviceManager;
			}
			set {
				if (__deviceManager != null) {
					managers.Remove(__deviceManager);
				}

				__deviceManager = value;

				if (__deviceManager != null) {
					managers.Add(__deviceManager);
				}
			}
		} private IDeviceManager __deviceManager;
    // Overridden from IION
    public IAlarmManager alarmManager {
			get {
				return __alarmManager;
			}
			set {
				if (__alarmManager != null) {
					managers.Remove(__alarmManager);
				}

				__alarmManager = value;

				if (__alarmManager != null) {
					managers.Add(__alarmManager);
				}
			}
		} private IAlarmManager __alarmManager;
    // Overridden from IION
    public IFluidManager fluidManager {
			get {
				return __fluidManager;
			}
			set {
				if (__fluidManager != null) {
					managers.Remove(__fluidManager);
				}

				__fluidManager = value;

				if (__fluidManager != null) {
					managers.Add(__fluidManager);
				}
			}
		} private IFluidManager __fluidManager;
    // Overridden from IION
    public ILocationManager locationManager {
			get {
				return __locationManager;
			}
			set {
				if (__locationManager != null) {
					managers.Remove(__locationManager);
				}

				__locationManager = value;

				if (__locationManager != null) {
					managers.Add(__locationManager);
				}
			}
		} private ILocationManager __locationManager;
    /// <summary>
    /// Queries the data log manager that is responsible for storing sensor data.
    /// </summary>
    /// <value>The data log manager.</value>
    public DataLogManager dataLogManager {
			get {
				return __dataLogManager;
			}
			set {
				if (__dataLogManager != null) {
					managers.Remove(__dataLogManager);
				}

				__dataLogManager = value;

				if (__dataLogManager != null) {
					managers.Add(__dataLogManager);
				}
			}
		} private DataLogManager __dataLogManager;
    /// <summary>
    /// The current primary analyzer for the ion context.
    /// </summary>
    /// <value>The current analyzer.</value>
    public Analyzer currentAnalyzer { get; set; }
    // Overridden from IION
    public Workbench currentWorkbench {
			get {
				return __workbench;
			}
			set {
				if (value == null) {
					value = new Workbench(this);
				}

				__workbench = value;

        if (onWorkbenchChanged != null) {
				  onWorkbenchChanged(value);
        }
			}
		} Workbench __workbench;

    // Overridden from IION
    public IUnits defaultUnits { get; private set; }

    // Overridden from IION
    public IFolder screenshotReportFolder {
      get {
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("screenshots", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }

    // Overridden from IION
    public IFolder calibrationCertificateFolder {
      get {
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("calibrationCertificates", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }

    /// <summary>
    /// The ios application settings.
    /// </summary>
    /// <value>The settings.</value>
    public AppSettings settings { get; private set; }
		/// <summary>
		/// Handles all the Amazon Web Service calls
		/// </summary>
		/// <value>The web services.</value>
		public WebPayload webServices { get; set; }
		
		///HOLDS THE INFORMATION FOR A DEVICE	
		public IPlatformInfo localDevice {get; set;}
		
		public IPlatformInfo remoteDevice {get; set;}
    /// <summary>
    /// The list of managers that are present in the ion context.
    /// </summary>
    private readonly List<IManager> managers = new List<IManager>();

    public IosION() {
      var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");

      database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this);
      fileManager = new IosFileManager();
      locationManager = new IosLocationManager(this);
      deviceManager = new BaseDeviceManager(this, new IonCBCentralManagerDelegate()); 
      alarmManager = new BaseAlarmManager(this);
      dataLogManager = new DataLogManager(this);
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, new PopupWindowAlarmAlert(alarm), new VibrateAlarmAlert(alarm, this), new SoundAlarmAlert(alarm, this));
      };
      fluidManager = new BaseFluidManager(this);
      currentAnalyzer = new Analyzer(this);
    }

    // Overridden from IION
    public void Dispose() {
			managers.Reverse();

      foreach (var m in managers) {
        try {
          m.Dispose();
        } catch (Exception e) {
          Log.E(this, "Failed to dispose of IIONManager: " + m.GetType().Name, e);
        }
      }
    }

    // Overridden from IION
    public void PostToMain(Action action) {
      NSOperationQueue.MainQueue.AddOperation(() => {
        try {
          action();
        } catch (Exception e) {
          Log.E(this, "Failed to execute action on main thread", e);
        }
      });
    }

    // Overridden from IION
    public void PostToMainDelayed(Action action, TimeSpan delay) {
      DispatchQueue.MainQueue.DispatchAfter(new DispatchTime(DispatchTime.Now, delay), action);
    }

    // Overridden from IION
    public Task SaveWorkbenchAsync() {
      return Task.Factory.StartNew(() => {
        lock (this) {
          Log.D(this, "Saving workbench");
          var internalDir = fileManager.GetApplicationInternalDirectory();
          var file = internalDir.GetFile(FILE_WORKBENCH, EFileAccessResponse.CreateIfMissing);
          var wp = new WorkbenchParser();
          try {
            using (var stream = file.OpenForWriting()) {
              wp.WriteToStream(this, currentWorkbench, stream);
            }
            Log.D(this, "Workbench saved!");
          } catch (Exception e) {
            Log.E(this, "Failed to write workbench to file", e);
          }
        }
      });
    }

    /// <summary>
    /// Creates a new application dump object.
    /// </summary>
    /// <returns>The application dump.</returns>
    public IAppDump CreateApplicationDump() {
      return new BaseAppDump(this, new IOSPlatformInfo());
    }
    
    /// <summary>
    /// Gets the platform information.
    /// </summary>
    /// <returns>The platform information.</returns>
    public IPlatformInfo GetPlatformInformation(){
    	localDevice = new IOSPlatformInfo();
			return new IOSPlatformInfo();
		}
		
		public void SetRemotePlatformInformation(Core.App.sessionStateInfo remoteStatus){
			if(remoteDevice == null){
				remoteDevice = new IOSPlatformInfo();
			}
			remoteDevice.batteryPercentage = remoteStatus.batteryLevel;
			remoteDevice.wifiConnected = remoteStatus.wifiStatus;
			remoteDevice.freeMemory = remoteStatus.remainingMemory;
			remoteDevice.loggingStatus = remoteStatus.isRecording;
		}

    /// <summary>
    /// Initializes the ION instance.
    /// </summary>
    public async Task Init() {
      settings = new AppSettings();
      defaultUnits = new IosUnits(settings);


      try {
				Log.D(this, "Starting ION init");
				foreach (var im in managers) {
					Log.D(this, "Initializing " + im.GetType().Name);
					await im.InitAsync();
				}
				Log.D(this, "Finished loading the things");
				InitSettings();

				var internalDir = fileManager.GetApplicationInternalDirectory();
				if (internalDir.ContainsFile(FILE_WORKBENCH)) {
					var workbenchFile = internalDir.GetFile(FILE_WORKBENCH);
					currentWorkbench = await LoadWorkbenchAsync(workbenchFile);
				} else {
					currentWorkbench = new Workbench(this);
				}
				currentWorkbench.onWorkbenchEvent += onWorkbenchEvent;

        foreach (var im in managers) {
          im.PostInit();
        }

				Log.D(this, "Ending ION init");
			} catch (Exception e) {
        Log.E(this, "Failed to init ION", e);
      }
    }

		public Task<Workbench> LoadWorkbenchAsync() {
			return null;
		}

		public Task<Analyzer> LoadAnalyzerAsync() {
			return null;
		}

		public Task<Workbench> LoadWorkbenchAsync(IFile file) {
			Log.D(this, "load workbenchasync");
      var wp = new WorkbenchParser();
      try {
        using (var stream = file.OpenForReading()) {
					return Task.FromResult(wp.ReadFromStream(this, stream));
        }
      } catch (Exception e) {
        Log.E(this, "Failed to load workbench. Creating a new one.", e);
				return Task.FromResult(new Workbench(this));
      }
    }

		public Task<Analyzer> LoadAnalyzerAsync(IFile file) {
			return null;
		}

		public Task SaveAnalyzerAsync() {
			return null;
		}
    
    public void onWorkbenchEvent(WorkbenchEvent e){
			if(e.type != WorkbenchEvent.EType.ManifoldEvent || (e.manifoldEvent.type != ManifoldEvent.EType.Invalidated)){
				Log.D(this,"Workbench event saving workbench layout");
				SaveWorkbenchAsync();
			}
		}
    
    /// <summary>
    /// Creates a Device Manager that will deal only with the remote devices another user has uploaded
    /// </summary>
    /// <returns>The remote device manager.</returns>
    public Task setRemoteDeviceManager(){
    	Log.D(this, "Setting remote Device Manager");
      return Task.Factory.StartNew(() => {
      	///create a new analyzer manager for the remote session chosen and store the original analyzer for restoring
      	var remoteAnalyzer = new Analyzer(this);
      	remoteAnalyzer.storedAnalyzer = currentAnalyzer;
      	currentAnalyzer = remoteAnalyzer;
      	
      	///create a new workbench manager for the remote session chosen and store the original workbench for restoring
      	//var remoteWorkbench = new Workbench(this);
      	//remoteWorkbench.storedWorkbench = currentWorkbench;
      	//currentWorkbench = remoteWorkbench;
      	
      	///create a new remotebasedevicemanager for the remote session chosen and store the original basedevicemanager for restoring
      	var remoteDManager = new RemoteBaseDeviceManager(this, NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser"));
				remoteDManager.InitAsync().Wait();
				remoteDManager.storedDeviceManager = (BaseDeviceManager)deviceManager;
				deviceManager = remoteDManager;
				deviceManager.connectionManager = remoteDManager.storedDeviceManager.connectionManager;
      });
		}

    /// <summary>
    /// Creates a Device Manager that will deal only with the remote devices another user has uploaded
    /// </summary>
    /// <returns>The remote device manager.</returns>
    public Task setOriginalDeviceManager(){
      return Task.Factory.StartNew(() => {
      	///restore analyzer to the user's analyzer after ending remote viewing
      	var remoteAnalyzer = currentAnalyzer;
      	currentAnalyzer = remoteAnalyzer.storedAnalyzer;
      	///restore workbench to the user's workbench after ending remote viewing
      	//var remoteWorkbench = currentWorkbench;
      	//currentWorkbench = remoteWorkbench.storedWorkbench;
      	///restore basedevicemanager to user's basedevicemanager after ending remote viewing
      	var remoteDManager = (RemoteBaseDeviceManager)deviceManager;
				deviceManager = remoteDManager.storedDeviceManager;
      });
		}


		public void InitSettings() {
			settings.screen.leaveOn = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_screen_leave_on");
			settings.location.useGeoLocation = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_location_use_geolocation");
			settings.alarm.haptic = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_haptic");
			settings.alarm.sound = NSUserDefaults.StandardUserDefaults.BoolForKey("settings_alarm_sound");

			if (NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval") <= 0) {
				NSUserDefaults.StandardUserDefaults.SetInt(30, "settings_default_logging_interval");
			}

			if (settings.screen.leaveOn) {
				UIApplication.SharedApplication.IdleTimerDisabled = true;
			}

			if (settings.location.useGeoLocation) {
				locationManager.StartAutomaticLocationPolling();
			} else {
				locationManager.StopAutomaticLocationPolling();
			}
		}
  } // End IosION

  internal class IosUnits : IUnits {
    // Overridden from IUnits
    public Unit length {
      get {
        return UnitLookup.GetUnit(settings.units.length);
      }
      set {
      }
    }
    // Overridden from IUnits
    public Unit pressure {
      get {
        return UnitLookup.GetUnit(settings.units.pressure);
      }
      set {
      }
    }
    // Overridden from IUnits
    public Unit temperature {
      get {
        return UnitLookup.GetUnit(settings.units.temperature);
      }
      set {
      }
    }
    // Overridden from IUnits
    public Unit vacuum {
      get {
        return UnitLookup.GetUnit(settings.units.vacuum);
      }
      set {
      }
    }

    private AppSettings settings { get; set; }

    public IosUnits(AppSettings settings) {
      this.settings = settings;
    }

    // Overridden from IUnits
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
  }
}
