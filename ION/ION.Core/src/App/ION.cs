using System;

using ION.Core.Content;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.App {
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
    public static IION context {
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
  } // End ION

  /// <summary>
  /// The interface that describes an ION application context.
  /// </summary>
  public interface IION : IDisposable {
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
    /// Queries the fluid manager that is responsible for acquiring and
    /// maintaining the applications fluids.
    /// </summary>
    IFluidManager fluidManager { get; }
    /// <summary>
    /// The current primary workbench for the ION context.
    /// </summary>
    /// <value>The current workbench.</value>
    Workbench currentWorkbench { get; }

    /// <summary>
    /// Posts the action to the main message pump for execution on the main thread.
    /// </summary>
    /// <param name="action">Action.</param>
    void PostToMain(Action action);
  } // End IION
}

