namespace ION.TestFixtures.App {

  using System;
  using System.Threading.Tasks;

  using NUnit.Framework;

  using ION.Core.Alarms;
  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.Location;

  using ION.TestFixtures.Connections;

  public class MockION : IION {
    /// <summary>
    /// Queries the build name of the ion instance. (ie. ION HVAC/r for android of ION Viewer for iOS)
    /// </summary>
    /// <value>The name.</value>
    public string name {
      get {
        return "MockION";
      }
    }
    /// <summary>
    /// Queries the full version of the ion instance.
    /// </summary>
    /// <value>The version.</value>
    public string version {
      get {
        return "0.0.0";
      }
    }
    /// <summary>
    /// The database that will store all of the application data.
    /// </summary>
    /// <value>The database.</value>
    public IONDatabase database { get; private set; }
    /// <summary>
    /// The FileSystem that will allow the ion context to access the native
    /// platforms files.
    /// </summary>
    /// <value>The file manager.</value>
    public IFileManager fileManager { get; private set; }
    /// <summary>
    /// Queries the device manager for the ION instance.
    /// </summary>
    /// <value>The device manager.</value>
    public IDeviceManager deviceManager { get; private set; }
    /// <summary>
    /// Queries the manager that will resolve alarms for the application.
    /// </summary>
    /// <value>The alarm manager.</value>
    public IAlarmManager alarmManager { get; private set; }
    /// <summary>
    /// Queries the fluid manager that is responsible for acquiring and
    /// maintaining the applications fluids.
    /// </summary>
    /// <value>The fluid manager.</value>
    public IFluidManager fluidManager { get; private set; }

    /// <summary>
    /// The current primary workbench for the ION context.
    /// </summary>
    /// <value>The current workbench.</value>
    public Workbench currentWorkbench { get; private set; }
    /// <summary>
    /// Queries the screenshot report folder.
    /// </summary>
    /// <returns>The screenshot report folder.</returns>
    /// <exception cref="IOException">If the folder could not be retrieved.</exception>
    /// <value>The screenshot report folder.</value>
    public ILocationManager locationManager { get; private set; }
    /// <summary>
    /// The default units for the ION instance.
    /// </summary>
    /// <value>The default units.</value>
    public IUnits defaultUnits { get; private set; }


    /// <summary>
    /// Queries the screenshot report folder.
    /// </summary>
    /// <returns>The screenshot report folder.</returns>
    /// <exception cref="IOException">If the folder could not be retrieved.</exception>
    /// <value>The screenshot report folder.</value>
    public IFolder screenshotReportFolder {
      get {
        return null;
      }
    }
    /// <summary>
    /// Queries the folder where calibration certificates are placed.
    /// </summary>
    /// <value>The calibraaction certificate folder.</value>
    public IFolder calibrationCertificateFolder {
      get {
        return null;
      }
    }

    public MockION() {
      deviceManager = new BaseDeviceManager(this, new MockConnectionHelper());
      fluidManager = new BaseFluidManager(this);
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.TestFixtures.App.MockION"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.TestFixtures.App.MockION"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.TestFixtures.App.MockION"/> in an unusable state. After
    /// calling <see cref="Dispose"/>, you must release all references to the <see cref="ION.TestFixtures.App.MockION"/>
    /// so the garbage collector can reclaim the memory that the <see cref="ION.TestFixtures.App.MockION"/> was occupying.</remarks>
    public void Dispose() {
    }

    /// <summary>
    /// Posts the action to the main message pump for execution on the main thread.
    /// </summary>
    /// <param name="action">Action.</param>
    public void PostToMain(Action action) {
      action();
    }
    /// <summary>
    /// Posts an action to the main message pump for execution on the main thread after
    /// a given delay.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="delay">Delay.</param>
    public void PostToMainDelayed(Action action, TimeSpan delay) {
      action();
    }

    /// <summary>
    /// Saves the workbench async.
    /// </summary>
    /// <returns>The workbench async.</returns>
    public Task SaveWorkbenchAsync() {
      return null;
    }

    public void CreateApplicationDump(){
      
    }
  }
}

