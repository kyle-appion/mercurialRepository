namespace ION.Droid.App {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.Preferences;

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

  using ION.Droid.Connections;
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

    // Overridden from IION
    public IFolder calibrationCertificateFolder {
      get {
        throw new Exception("Not implemented");
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

      var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      managers.Add(database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, this));
      managers.Add(fileManager = new AndroidFileManager(context));
      managers.Add(deviceManager = new BaseDeviceManager(this, new LeConnectionHelper(this, (BluetoothManager)context.GetSystemService(Context.BluetoothService))));
      managers.Add(locationManager = new AndroidLocationManager(this));
      managers.Add(alarmManager = new BaseAlarmManager(this));

      managers.Add(fluidManager = new BaseFluidManager(this));

      defaultUnits = new AndroidDefaultUnits(context, preferences);
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
          Log.E(this, "" + res.errorMessage);
          return false;
        }
      }

      currentWorkbench = new Workbench(this);

      return true;
    }

    /// <summary>
    /// Handles a message that was received from the android message pump.
    /// </summary>
    /// <param name="msg">Message.</param>
    private void OnHandleMessage(Android.OS.Message msg) {
    }
  }

  /// <summary>
  /// The class that will manager the ION default unit settings.
  /// </summary>
  internal class AndroidDefaultUnits : IUnits {
    // Overridden from IUnits
    public Unit length {
      get {
        return AssertUnitGet(Resource.String.preferences_units_length, Units.Length.FOOT);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_length, Quantity.Length, Units.Length.FOOT);
      }
    }

    // Overridden from IUnits
    public Unit pressure {
      get {
        return AssertUnitGet(Resource.String.preferences_units_pressure, Units.Pressure.PSIG);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_pressure, Quantity.Pressure, Units.Pressure.PSIG);
      }
    }

    // Overridden from IUnits
    public Unit temperature {
      get {
        return AssertUnitGet(Resource.String.preferences_units_temperature, Units.Temperature.FAHRENHEIT);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_temperature, Quantity.Temperature, Units.Temperature.FAHRENHEIT);
      }
    }

    // Overridden from IUnits
    public Unit vacuum {
      get {
        return AssertUnitGet(Resource.String.preferences_units_vacuum, Units.Vacuum.MICRON);
      }
      set {
        AssertUnitSet(Resource.String.preferences_units_vacuum, Quantity.Vacuum, Units.Vacuum.MICRON);
      }
    }

    /// <summary>
    /// The application's context.
    /// </summary>
    /// <value>The context.</value>
    private Context context { get; set; }

    /// <summary>
    /// The application's preferences.
    /// </summary>
    /// <value>The prefs.</value>
    private ISharedPreferences prefs { get; set; }

    public AndroidDefaultUnits(Context context, ISharedPreferences prefs) {
      this.context = context;
      this.prefs = prefs;
    }

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

    /// <summary>
    /// Safely gets the unit for the given key. If the desired unit could not be
    /// fetched, we will return the backup.
    /// </summary>
    /// <returns>The unit get.</returns>
    /// <param name="preferenceKey">Preference key.</param>
    /// <param name="backup">Backup.</param>
    private Unit AssertUnitGet(int preferenceKey, Unit backup) {
      var key = context.GetString(preferenceKey);

      try {
        return UnitLookup.GetUnit(int.Parse(prefs.GetString(key, null)));  
      } catch (Exception e) {
        Log.E(this, "Failed to retrieve unit for key: " + key);
        AssertUnitSet(preferenceKey, backup.quantity, backup);
        return backup;
      }
    }

    /// <summary>
    /// Safely attempts to set the unit for the given key.
    /// </summary>
    /// <returns>The unit set.</returns>
    /// <param name="prefernceKey">Prefernce key.</param>
    private void AssertUnitSet(int preferenceKey, Quantity quantity, Unit unit) {
      var key = context.GetString(preferenceKey);
      try {
        if (quantity != unit.quantity) {
          throw new ArgumentException("Unit: " + unit + " is not compatible with quantity + " + quantity);
        }

        var e = prefs.Edit();

        e.PutString(key, UnitLookup.GetCode(unit) + "");

        e.Commit();
      } catch (Exception e) {
        Log.E(this, "Failed to set unit " + unit + " for key: " + key, e);
      }
    }
  }
}

