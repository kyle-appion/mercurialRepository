namespace ION.Droid.App {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Preferences;
  using Android.Support.V4.App;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Content;
  using ION.Core.Content.Parsers;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.Location;
  using ION.Core.Measure;
  using ION.Core.Report.DataLogs;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Alarms.Alerts;
  using ION.Droid.Activity;
  using ION.Droid.Connections;
  using ION.Droid.Devices;
  using ION.Droid.Location;
  using ION.Droid.Preferences;

  [Service]
  public class AndroidION : Service, IION {
    /// <summary>
    /// The name of the general ion preferences.
    /// </summary>
    public const string PREFERENCES_GENERAL = "ion.preferences";
    /// <summary>
    /// The file name for the primary workbench.
    /// </summary>
    public const string FILE_WORKBENCH = "primaryWorkbench.workbench";
    /// <summary>
    /// The id of that application's notification.
    /// </summary>
    private const int NOTIFICATION_APP_ID = 1;

		/// <summary>
		/// Occurs when the workbench is changed.
		/// </summary>
		public event OnWorkbenchChanged onWorkbenchChanged;

    /// <summary>
    /// Queries the build name of the ion instance. (ie. ION HVAC/r for android of ION Viewer for iOS)
    /// </summary>
    /// <value>The name.</value>
    public string name { get { return PackageName; } }
    /// <summary>
    /// Queries the full version of the ion instance.
    /// </summary>
    /// <value>The version.</value>
    public string version { get { return PackageManager.GetPackageInfo(PackageName, PackageInfoFlags.MetaData).VersionName; } }

    /// <summary>
    /// The database that will store all of the application data.
    /// </summary>
    /// <value>The database.</value>
    public IONDatabase database { get; set; }
    /// <summary>
    /// The FileSystem that will allow the ion context to access the native
    /// platforms files.
    /// </summary>
    /// <value>The file manager.</value>
    public ION.Core.IO.IFileManager fileManager { get; set; }
    /// <summary>
    /// Queries the device manager for the ION instance.
    /// </summary>
    /// <value>The device manager.</value>
    public IDeviceManager deviceManager { get; set; }
    /// <summary>
    /// Queries the alarm manager.
    /// </summary>
    /// <value>The alarm manager.</value>
    public IAlarmManager alarmManager { get; set; }
    /// <summary>
    /// Queries the fluid manager that is responsible for acquiring and
    /// maintaining the applications fluids.
    /// </summary>
    /// <value>The fluid manager.</value>
    public IFluidManager fluidManager { get; set; }
    /// <summary>
    /// Queries the location manager that is responsbile for ascertaining the user's altitude.
    /// </summary>
    /// <value>The location manager.</value>
    public ILocationManager locationManager { get; set; }
    /// <summary>
    /// Queries the data log manager that is responsible for storing sensor data.
    /// </summary>
    /// <value>The data log manager.</value>
    public DataLogManager dataLogManager { get; set; }

    /// <summary>
    /// Gets or sets the current analyzer.
    /// </summary>
    /// <value>The current analyzer.</value>
    public Analyzer currentAnalyzer { get; set; }
    /// <summary>
    /// The current primary workbench for the ION context.
    /// </summary>
    /// <value>The current workbench.</value>
    public Workbench currentWorkbench {
			get {
				return __workbench;
			}
			set {
				__workbench = value;
				if (onWorkbenchChanged != null) {
					onWorkbenchChanged(value);
				}
			}
		} Workbench __workbench;

    /// <summary>
    /// The default units for the ION instance.
    /// </summary>
    /// <value>The default units.</value>
    public IUnits defaultUnits {
      get {
        return preferences.units;
      }
    }
    /// <summary>
    /// Queries the screenshot report folder.
    /// </summary>
    /// <returns>The screenshot report folder.</returns>
    /// <exception cref="IOException">If the folder could not be retrieved.</exception>
    /// <value>The screenshot report folder.</value>
    public IFolder screenshotReportFolder {
      get { 
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("screenshots", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }

    /// <summary>
    /// Queries the folder where calibration certificates are placed.
    /// </summary>
    /// <value>The calibraaction certificate folder.</value>
    public IFolder calibrationCertificateFolder {
      get {
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("certificates", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }

    /// <summary>
    /// The wrapped preferences for the application.
    /// </summary>
    /// <value>The preferences.</value>
    public AppPrefs preferences { get; private set; }

    /// <summary>
    /// Whether or not the context is initialized.
    /// </summary>
    /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
    public bool initialized { get; private set; }

    /// <summary>
    /// The android specfic message pump.
    /// </summary>
    /// <value>The handler.</value>
    private Android.OS.Handler handler { get; set; }

    /// <summary>
    /// The whole aggragation of the managers present within the ion context.
    /// </summary>
    private readonly List<IIONManager> managers = new List<IIONManager>();

    /// <Docs>Called by the system when the service is first created.</Docs>
    /// <para tool="javadoc-to-mdoc">Called by the system when the service is first created. Do not call this method directly.</para>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the create event.
    /// </summary>
    public override async void OnCreate() {
      base.OnCreate();

      initialized = false;

      Log.D(this, "Creating the AndroidION");
      if (AppState.context != null) {
        Log.D(this, "A previous service was discovered to be running. Killing it");
        AppState.context.Dispose();
        AppState.context = null;
      }

      AppState.context = this;
      this.handler = new Android.OS.Handler();
      preferences = new AppPrefs(this, GetSharedPreferences(AndroidION.PREFERENCES_GENERAL, FileCreationMode.Private));
      var discard = preferences.appVersion; // Sets the current application version.
      var bluetoothManager = (BluetoothManager)GetSystemService(Context.BluetoothService);

      var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ION.database");
      managers.Add(database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, this));
      managers.Add(fileManager = new AndroidFileManager(this));
      managers.Add(deviceManager = new BaseDeviceManager(this, 
        new AndroidConnectionFactory(this, bluetoothManager),
        new LeConnectionHelper(bluetoothManager)));
      managers.Add(locationManager = new AndroidLocationManager(this));
      managers.Add(alarmManager = new BaseAlarmManager(this));
      managers.Add(dataLogManager = new DataLogManager(this));
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, 
          new PopupActivityAlert(alarm, this),
          new ToneAlarmAlert(alarm, this),
          new VibrateAlarmAlert(alarm, this)
        );
      };

      managers.Add(fluidManager = new BaseFluidManager(this));

      foreach (var m in managers) {
        var res = await m.InitAsync();
        if (!res.success) {
          Log.E(this, "Failed to init manager: " + m);
          Log.E(this, "" + res.errorMessage);
          StopSelf();
          return;
        }
      }

/*
#if DEBUG
      if (preferences.firstLaunch) {
        Log.D(this, "Creating debug data logs.");
        CreateDebugDataLogs(3);
      }
#endif
*/

      deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

      try {
        Workbench w = null;
        var internalDir = fileManager.GetApplicationInternalDirectory();
        if (internalDir.ContainsFile(FILE_WORKBENCH)) {
          var file = internalDir.GetFile(FILE_WORKBENCH);
          w = await LoadWorkbenchAsync(file);
        } else {
          w = new Workbench(this);
        }
        currentWorkbench = w;
      } catch (Exception e) {
        Log.E(this, "Failed to load workbench", e);
        currentWorkbench = new Workbench(this);
      }

      if (currentWorkbench == null) {
        currentWorkbench = new Workbench(this);
      }

      if (currentAnalyzer == null) {
        currentAnalyzer = new Analyzer(this);
      }

      // TODO Save/load analyzer.

      UpdateNotification();

      initialized = true;
    }

    /// <Docs>Called by the system to notify a Service that it is no longer used and is being removed.</Docs>
    /// <para tool="javadoc-to-mdoc">Called by the system to notify a Service that it is no longer used and is being removed. The
    ///  service should clean up any resources it holds (threads, registered
    ///  receivers, etc) at this point. Upon return, there will be no more calls
    ///  in to this Service object and it is effectively dead. Do not call this method directly.</para>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    public override void OnDestroy() {
      base.OnDestroy();

      initialized = false;

      AppState.context = null;

      Log.D(this, "Destroying ION service");

      foreach (var m in managers) {
        try {
          m.Dispose();
        } catch (Exception e) {
          Log.E(this, "Failed to dipose of IIONManager: " + m.GetType().Name, e); 
        }
      }

      var nm = GetSystemService(NotificationService) as NotificationManager;
      nm.Cancel(NOTIFICATION_APP_ID);
    }

    /// <summary>
    /// Raises the start command event.
    /// </summary>
    /// <param name="intent">Intent.</param>
    /// <param name="flags">Flags.</param>
    /// <param name="startId">Start identifier.</param>
    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
      base.OnStartCommand(intent, flags, startId);

      return StartCommandResult.Sticky;
    }

    /// <summary>
    /// Raises the bind event.
    /// </summary>
    /// <param name="intent">Intent.</param>
    public override IBinder OnBind(Intent intent) {
      return new IONBinder(this);
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Droid.App.AndroidION"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Droid.App.AndroidION"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.Droid.App.AndroidION"/> in an unusable state. After
    /// calling <see cref="Dispose"/>, you must release all references to the <see cref="ION.Droid.App.AndroidION"/> so
    /// the garbage collector can reclaim the memory that the <see cref="ION.Droid.App.AndroidION"/> was occupying.</remarks>
    public new void Dispose() {
      StopSelf();
    }

    /// <summary>
    /// Posts the action to the main message pump for execution on the main thread.
    /// </summary>
    /// <param name="action">Action.</param>
    public void PostToMain(Action action) {
      handler.Post(action);
    }

    /// <summary>
    /// Posts an action to the main message pump for execution on the main thread after
    /// a given delay.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="delay">Delay.</param>
    public void PostToMainDelayed(Action action, TimeSpan delay) {
      handler.PostDelayed(action, (long)delay.TotalMilliseconds);
    }

    /// <summary>
    /// Saves the workbench async.
    /// </summary>
    /// <returns>The workbench async.</returns>
    public Task SaveWorkbenchAsync() {
      return Task.Factory.StartNew(() => {
        lock (this) {
          var internalDir = fileManager.GetApplicationInternalDirectory();
          var file = internalDir.GetFile(FILE_WORKBENCH, EFileAccessResponse.CreateIfMissing);
          var wp = new WorkbenchParser();
          try {
            using (var s = file.OpenForWriting()) {
              wp.WriteToStream(this, currentWorkbench, s);
            }
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
      return new BaseAppDump(this, new AndroidPlatformInfo(this));
    }

    /// <summary>
    /// Loads a workbench from the given file.
    /// </summary>
    /// <returns>The workbench async.</returns>
    /// <param name="file">File.</param>
    public Task<Workbench> LoadWorkbenchAsync(IFile file) {
      lock (this) {
        var wp = new WorkbenchParser();
        try {
          using (var s = file.OpenForReading()) {
            return Task.FromResult(wp.ReadFromStream(this, s));
          }
        } catch (Exception e) {
          Log.E(this, "Failed to load workbench. Defaulting to a new one.", e);
          return Task.FromResult(new Workbench(this));
        }
      }
    }

    /// <summary>
    /// Updates (creating if necessary the application's notification.
    /// </summary>
    private void UpdateNotification() {
      var i = new Intent(this, typeof(HomeActivity));
      var pi = PendingIntent.GetActivity(this, 0, i, PendingIntentFlags.UpdateCurrent);


      var connected = 0;
      foreach (var d in deviceManager.devices) {
        if (d.isConnected) {
          connected++;
        }
      }
      var total = deviceManager.devices.Count;

      var note = new NotificationCompat.Builder(this)
        .SetSmallIcon(Resource.Drawable.ic_logo_appiondefault_small)
        .SetContentTitle(GetString(Resource.String.app_name))
        .SetContentText(string.Format(GetString(Resource.String.devices_connected_2arg), connected, total))
        .SetContentIntent(pi)
        .SetOngoing(true);

      var nm = GetSystemService(NotificationService) as NotificationManager;
      nm.Notify(NOTIFICATION_APP_ID, note.Build());
    }

    /// <summary>
    /// Called when a device manager throws an event.
    /// </summary>
    /// <param name="dm">Dm.</param>
    /// <param name="dme">Dme.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent dme) {
      if (DeviceManagerEvent.EType.DeviceEvent == dme.type) {
        if (DeviceEvent.EType.ConnectionChange == dme.deviceEvent.type) {
          UpdateNotification();
        }
      }
    }

    /// <summary>
    /// The binder that will retrieve the ion from bind calls.
    /// </summary>
    public class IONBinder : Binder {
      public AndroidION ion { get; private set; }

      public IONBinder(AndroidION ion) {
        this.ion = ion;
      }
    }
  }
}

