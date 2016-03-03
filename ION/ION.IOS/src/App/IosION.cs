namespace ION.IOS.App {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using CoreFoundation;
  using Foundation;

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
  using ION.Core.Measure;
  using ION.Core.Pdf;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.IOS.Alarms.Alerts;
  using ION.IOS.IO;
  using ION.IOS.Location;
  using ION.IOS.Devices;

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

    // Overridden from IION
    public string name { get { return GetDisplayName(); } }
    // Overridden from IION
    public string version { get { return GetVersion(); } }

    // Overridden from IION
    public IONDatabase database { get; set; }
    // Overridden from IION
    public ION.Core.IO.IFileManager fileManager { get; set; }
    // Overridden from IION
    public IDeviceManager deviceManager { get; set; }
    // Overridden from IION
    public IAlarmManager alarmManager { get; set; }
    // Overridden from IION
    public IFluidManager fluidManager { get; set; }
    // Overridden from IION
    public Workbench currentWorkbench { get; set; }
    // Overridden from IION
    public ILocationManager locationManager { get; set; }

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
    /// The list of managers that are present in the ion context.
    /// </summary>
    private readonly List<IIONManager> managers = new List<IIONManager>();
    /// <summary>
    /// The native ios application settings dictionary.
    /// </summary>
//    private NSDictionary settings { get; set; }

    public IosION() {
      settings = new AppSettings();
      // Load up the application settings
//      defaultUnits = new IosUnits();
      // Order matters - Manager's with no dependencies should come first such
      // that later manager's may depend on them.
      var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      managers.Add(database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this));
      managers.Add(fileManager = new IosFileManager());
      managers.Add(locationManager = new IosLocationManager(this));
      managers.Add(deviceManager = new BaseDeviceManager(this, new LeConnectionHelper(new CBCentralManager(DispatchQueue.CurrentQueue))));
      managers.Add(alarmManager = new BaseAlarmManager(this));
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, new PopupWindowAlarmAlert(alarm));
      };
      managers.Add(fluidManager = new BaseFluidManager(this));
    }

    // Overridden from IION
    public void Dispose() {
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
      return new BaseAppDump(this, new IOSPlatforInfo());
    }

    /// <summary>
    /// Initializes the ION instance.
    /// </summary>
    public async Task Init() {
      var settings = new AppSettings();
      defaultUnits = new IosUnits(settings);


      try {
        Log.D(this, "Starting ION init");
        foreach (var im in managers) {
          Log.D(this, "Initializing " + im.GetType().Name);
          await im.InitAsync();
        }

        var internalDir = fileManager.GetApplicationInternalDirectory();
        if (internalDir.ContainsFile(FILE_WORKBENCH)) {
          var workbenchFile = internalDir.GetFile(FILE_WORKBENCH);
          currentWorkbench = await LoadWorkbenchAsync(workbenchFile);
        } else {
          currentWorkbench = new Workbench(this);
        }

        Log.D(this, "Ending ION init");
      } catch (Exception e) {
        Log.E(this, "Failed to init ION", e);
      }
    }

    private async Task<Workbench> LoadWorkbenchAsync(IFile file) {
      lock (this) {
        var wp = new WorkbenchParser();
        try {
          using (var stream = file.OpenForReading()) {
            return wp.ReadFromStream(this, stream);
          }
        } catch (Exception e) {
          Log.E(this, "Failed to load workbench. Creating a new one.", e);
          return new Workbench(this);
        }
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
