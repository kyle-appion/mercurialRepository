namespace ION.Droid.App {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  using Android.Content;
  using Android.Content.PM;

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
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Devices;
  using ION.Droid.Location;

  public class AndroidION : IION {
    /// <summary>
    /// The name of the general ion preferences.
    /// </summary>
    public const string PREFERENCES_GENERAL = "ion.preferences";

    // Overridden from IION
    public string name { get { return context.PackageName; } }
    // Overridden from IION
    public string version { get { return context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.MetaData).VersionName; } }

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

    /// <summary>
    /// The android context that binds the ion context to the android platform.
    /// </summary>
    /// <value>The context.</value>
    public Context context { get; private set; }

    /// <summary>
    /// The general preferences for the app.
    /// </summary>
    /// <value>The preferences.</value>
    public ISharedPreferences preferences { 
      get {
        return context.GetSharedPreferences(PREFERENCES_GENERAL, FileCreationMode.Private);
      }
    }

    /// <summary>
    /// The android specfic message pump.
    /// </summary>
    /// <value>The handler.</value>
    private Android.OS.Handler handler { get; set; }
    /// <summary>
    /// The whole aggragation of the managers present within the ion context.
    /// </summary>
    private readonly List<IIONManager> managers = new List<IIONManager>();


    public AndroidION(Context context) {
      this.context = context;
      this.handler = new Android.OS.Handler(OnHandleMessage);

      try {
      var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      managers.Add(database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, this));
      managers.Add(fileManager = new AndroidFileManager(context));
      managers.Add(deviceManager = new BaseDeviceManager(this, new AndroidLeConnectionHelper(this, Android.Bluetooth.BluetoothAdapter.DefaultAdapter)));
      managers.Add(locationManager = new AndroidLocationManager(this));
      managers.Add(alarmManager = new BaseAlarmManager(this));

      managers.Add(fluidManager = new BaseFluidManager(this, fileManager.GetAssetDirectory().GetFolder("fluids")));
      } catch (Exception e) {
        Log.E(this, "", e);
      }
    }

    public void Dispose() {
      foreach (var m in managers) {
        try {
          m.Dispose();
        } catch (Exception e) {
          Log.E(this, "Failed to dipose of IIONManager: " + m.GetType().Name, e); 
        }
      }
    }

    // Overridden from IION
    public void PostToMain(Action action) {
      handler.Post(action);
    }

    // Overridden from IION
    public void PostToMainDelayed(Action action, TimeSpan delay) {
      handler.PostDelayed(action, (long)delay.TotalMilliseconds);
    }

    // Overridden from IION
    public Task SaveWorkbenchAsync() {
      return Task.Factory.StartNew(() => {
      });
    }

    /// <summary>
    /// Initializes the ion instance.
    /// </summary>
    public async Task<bool> Init() {
      foreach (var m in managers) {
        var res = await m.InitAsync();
        if (!res.success) {
          Log.E(this, "Failed to init manager: " + m);
          Log.E(this, res.errorMessage);
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Handles a message that was received from the android message pump.
    /// </summary>
    /// <param name="msg">Message.</param>
    private void OnHandleMessage(Android.OS.Message msg) {
    }
  }
}

