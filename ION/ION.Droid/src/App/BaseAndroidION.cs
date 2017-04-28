namespace ION.Droid.App {

	using System;
	using System.Threading.Tasks;

	using Android.OS;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Alarms;
	using ION.Core.Content;
	using ION.Core.Content.Parsers;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Report.DataLogs;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Preferences;

	/// <summary>
	/// The base class for an ION instance on the Android platform.
	/// </summary>
	public abstract class BaseAndroidION : IION {
		/// <summary>
		/// The file name for the primary workbench.
		/// </summary>
		private const string PRIMARY_WORKBENCH_FILENAME = "primaryWorkbench.workbench";
		/// <summary>
		/// The file name for the primary analyzer.
		/// </summary>
		private const string PRIMARY_ANALYZER_FILENAME = "primaryAnalyzer.analyzer";

		/// <summary>
		/// The file name for the primary workbench.
		/// </summary>
		public const string FILE_WORKBENCH = "primaryWorkbench.workbench";
		/// <summary>
		/// The file name for the primary workbench.
		/// </summary>
		public const string FILE_ANALYZER = "primaryAnalyzer.analyzer";

		public const string FOLDER_REPORTS = "reports";
		public const string FOLDER_SCREENSHOTS = "screenshots";
		public const string FOLDER_CERTIFICATES = "certificates";
		public const string FOLDER_DATALOGS = "dataLogs";

		// Implemented from IION
		public event OnWorkbenchChanged onWorkbenchChanged;
		// Implemented from IION
		public event OnAnalyzerChanged onAnalyzerChanged;

		// Implemented from IION
		public string name { get { return context.GetString(Resource.String.app_name); } }
		// Implemented from IION
		public string version { get { return context.PackageManager.GetPackageInfo(context.PackageName, Android.Content.PM.PackageInfoFlags.MetaData).VersionName; } }
    // Implemented from IION
    public Guid applicationIdentifier { get { return preferences.appId; } }

		// Implemented From IION
		public IONDatabase database { get; protected set; }
		// Implemented From IION
		public IFileManager fileManager { get; protected set; }
		// Implemented From IION
		public IDeviceManager deviceManager { get; protected set; }
		// Implemented From IION
		public IAlarmManager alarmManager { get; protected set; }
		// Implemented From IION
		public IFluidManager fluidManager { get; protected set; }
		// Implemented From IION
		public ILocationManager locationManager { get; protected set; }
		// Implemented From IION
		public DataLogManager dataLogManager { get; protected set; }

		// Implemented From IION
		public virtual Analyzer currentAnalyzer { 
			get {
				return __analyzer;
			}
			set {
				__analyzer = value;
				if (onAnalyzerChanged != null) {
					onAnalyzerChanged(__analyzer);
				}
			}
		} Analyzer __analyzer;
		// Implemented From IION
		public virtual Workbench currentWorkbench {
			get {
				return __workbench;
			}
			set {
				__workbench = value;
				if (onWorkbenchChanged != null) {
					onWorkbenchChanged(__workbench);
				}
			}
		} Workbench __workbench;
		// Implemented from IION
		public IUnits defaultUnits { get { return preferences.units; } }

		// Implemented From IION
		public IFolder screenshotReportFolder { get; protected set; }
		// Implemented from IION
		public IFolder calibrationCertificateFolder { get; protected set; } 
		// Implemented from 
		public IFolder dataLogReportFolder { get; protected set; }

		/// <summary>
		/// The backing android context.
		/// </summary>
		public AppService context;
		/// <summary>
		/// The user's application preferences.
		/// </summary>
		/// <value>The preferences.</value>
		public AppPrefs preferences { get { return AppPrefs.Get(context); } }

		// Implemented from IION
		public IONPortalService portal { get; set; }

    // Implemented for IION
    public IPlatformInfo localDevice { get; set; }
    // Implemented for IION
    public IPlatformInfo remoteDevice { get; set; }


		/// <summary>
		/// The handler that actions are posted to the main thread with.
		/// </summary>
		protected Handler handler { get; private set; }

    /// <summary>
    /// The object that we will lock to for multithreading purposes.
    /// </summary>
    private object locker = new object();

		public BaseAndroidION(AppService context) {
			this.context = context;
			handler = new Handler(Looper.MainLooper);
		}

		/// <summary>
		/// Initializes the ION instance.
		/// </summary>
		/// <returns>The init.</returns>
		public async Task<bool> InitAsync() {
			try {
				var _ = preferences.appVersion; // Sets the current application version.

				if (!OnPreInit()) {
					return false;
				}

				await VerifyManagerOrThrow(fileManager);
				await VerifyManagerOrThrow(database);
				await VerifyManagerOrThrow(deviceManager);
				await VerifyManagerOrThrow(fluidManager);
				await VerifyManagerOrThrow(locationManager);
				await VerifyManagerOrThrow(dataLogManager);
				await VerifyManagerOrThrow(alarmManager);

				screenshotReportFolder = fileManager.GetApplicationExternalDirectory()
				                                    .GetFolder(FOLDER_REPORTS, EFileAccessResponse.CreateIfMissing)
				                                    .GetFolder(FOLDER_SCREENSHOTS, EFileAccessResponse.CreateIfMissing);

				calibrationCertificateFolder = fileManager.GetApplicationExternalDirectory()
				                                          .GetFolder(FOLDER_REPORTS, EFileAccessResponse.CreateIfMissing)
				                                          .GetFolder(FOLDER_CERTIFICATES, EFileAccessResponse.CreateIfMissing);

				dataLogReportFolder = fileManager.GetApplicationExternalDirectory()
				                                 .GetFolder(FOLDER_REPORTS, EFileAccessResponse.CreateIfMissing)
				                                 .GetFolder(FOLDER_DATALOGS, EFileAccessResponse.CreateIfMissing);

				try {
					Workbench w = await LoadWorkbenchAsync();
					currentWorkbench = w;
				} catch (Exception e) {
					Log.E(this, "Failed to load workbench", e);
					currentWorkbench = new Workbench(this);
				}

				try {
					Analyzer a = await LoadAnalyzerAsync();
					currentAnalyzer = a;
				} catch (Exception e) {
					Log.E(this, "Failed to load analyzer", e);
					currentAnalyzer = new Analyzer(this);
				}

				if (currentWorkbench == null) {
					currentWorkbench = new Workbench(this);
				}

				if (currentAnalyzer == null) {
					currentAnalyzer = new Analyzer(this);
				}

        // Start post initialization
        database.PostInit();
        deviceManager.PostInit();
        fluidManager.PostInit();
        locationManager.PostInit();
        dataLogManager.PostInit();
        alarmManager.PostInit();
				if (!OnPostInit()) {
					return false;
				}

				deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to init ION", e);
				return false;
			}
		}

		// Implemented from IION
		public virtual void Dispose() {
			try {
				SaveWorkbenchAsync().Wait();
				SaveAnalyzerAsync().Wait();

				alarmManager.Dispose();
				locationManager.Dispose();
				dataLogManager.Dispose();
				deviceManager.Dispose();
				fileManager.Dispose();
				database.Dispose();
			} catch (Exception e) {
				Log.E(this, "Failed to dispose BaseION", e);
			}
		}

		// Implemented from IION
		public void PostToMain(Action action) {
			handler.Post(action);
		}

		// Implemented from IION
		public void PostToMainDelayed(Action action, TimeSpan delay) {
			handler.PostDelayed(action, (long)delay.TotalMilliseconds);
		}

		// Implemented from IION
		public virtual Task SaveWorkbenchAsync() {
			return Task.Factory.StartNew(() => {
				lock (locker) {
					var internalDirectory = fileManager.GetApplicationInternalDirectory();
					var file = internalDirectory.GetFile(PRIMARY_WORKBENCH_FILENAME, EFileAccessResponse.CreateIfMissing);
					var parser = new WorkbenchParser();
					try {
						using (var s = file.OpenForWriting()) {
							parser.WriteToStream(this, currentWorkbench, s);
						}
					} catch (Exception e) {
						Log.E(this, "Failed to write workbench to file", e);
					}
				}
			});
		}

		// Implemented from IION
		public virtual Task<Workbench> LoadWorkbenchAsync() {
			var internalDir = fileManager.GetApplicationInternalDirectory();
			if (internalDir.ContainsFile(FILE_WORKBENCH)) {
				var file = internalDir.GetFile(FILE_WORKBENCH);
				return LoadWorkbenchAsync(file);
			} else {
				return Task.FromResult(new Workbench(this));
			}
		}

		// Implemented from IION
		public virtual Task<Workbench> LoadWorkbenchAsync(IFile file) {
			return Task.Factory.StartNew(() => {
        lock (locker) {
  				try {
  					var parser = new WorkbenchParser();
  					using (var s = file.OpenForReading()) {
  						return parser.ReadFromStream(this, s);
  					}
  				} catch (Exception e) {
  					Log.E(this, "Failed to load workbench. Defaulting to a new one.", e);
  					return new Workbench(this);
  				}
        }
			});
		}

		// Implemented from IION
		public virtual Task SaveAnalyzerAsync() {
			return Task.Factory.StartNew(() => {
				lock (locker) {
					var internalDirectory = fileManager.GetApplicationInternalDirectory();
					var file = internalDirectory.GetFile(PRIMARY_ANALYZER_FILENAME, EFileAccessResponse.CreateIfMissing);
					var parser = new AnalyzerParser();
					try {
						using (var s = file.OpenForWriting()) {
							parser.WriteToStream(this, currentAnalyzer, s);
						}
					} catch (Exception e) {
						Log.E(this, "Failed to write analyzer to file", e);
					}
				}
			});
		}

		// Implemented from IION
		public virtual Task<Analyzer> LoadAnalyzerAsync() {
			var internalDir = fileManager.GetApplicationInternalDirectory();
			if (internalDir.ContainsFile(FILE_ANALYZER)) {
				var file = internalDir.GetFile(FILE_ANALYZER);
				return LoadAnalyzerAsync(file);
			} else {
				return Task.FromResult(new Analyzer(this));
			}
		}

		// Implemented from IION
		public virtual Task<Analyzer> LoadAnalyzerAsync(IFile file) {
			return Task.Factory.StartNew(() => {
        lock (locker) {
  				try {
  					var parser = new AnalyzerParser();
  					using (var s = file.OpenForReading()) {
  						return parser.ReadFromStream(this, s);
  					}
  				} catch (Exception e) {
  					Log.E(this, "Failed to load analyzer. Defaulting to a new one.", e);
  					return new Analyzer(this);
  				}
        }
			});
		}

		// Implemented from IION
		public IAppDump CreateApplicationDump() {
			return new BaseAppDump(this, new AndroidPlatformInfo(context));
		}

    // Implemented for IION
    public IPlatformInfo GetPlatformInformation() {
      return new AndroidPlatformInfo(context);
    }

    // Implemented for IION
    public void SetRemotePlatformInformation(sessionStateInfo pi) {
    }

		// TODO ahodder@appioninc.com: Implement? See kyle@appioninc.com for more info
		public Task setRemoteDeviceManager() {
			return null;
		}

		// TODO ahodder@appioninc.com: Implement? See kyle@appioninc.com for more info
		public Task setOriginalDeviceManager() {
			return null;
		}

		/// <summary>
		/// Override this methos to initialize entities that have no dependencies.
		/// </summary>
		/// <returns>The pre init async.</returns>
		protected virtual bool OnPreInit() {
			return true;
		}

		/// <summary>
		/// Override this method to initialize entities that depend on other data structures.
		/// </summary>
		/// <returns>The post init async.</returns>
		protected virtual bool OnPostInit() {
			return true;
		}

		private async Task VerifyManagerOrThrow(IManager manager) {
			if (manager == null) {
				throw new Exception("Cannot verify manager: null");
			}

			var name = manager.GetType().Name;

			var result = await manager.InitAsync();
			if (result == null) {
				throw new Exception("Manager {" + name + "} verification fail: failed to return result");
			} else {
				Log.D(this, "Initialization of {" + name + "}: " + result.success);
				if (!result.success) {
					throw new Exception("Manager {" + name + "} verification fail: init failed\n" + result.errorMessage);
				}
			}
		}

		/// <summary>
		/// Called when a device manager throws an event.
		/// </summary>
		/// <param name="dm">Dm.</param>
		/// <param name="dme">Dme.</param>
		private void OnDeviceManagerEvent(DeviceManagerEvent dme) {
			if (DeviceManagerEvent.EType.DeviceEvent == dme.type) {
				if (DeviceEvent.EType.ConnectionChange == dme.deviceEvent.type) {
					context.UpdateNotification();
				}
			}
		}
	}
}
