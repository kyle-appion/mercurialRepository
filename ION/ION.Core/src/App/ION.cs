using System;

using ION.Core.Devices;
using ION.Core.Util;

namespace ION.Core.App {
  /// <summary>
  /// A utility class that will retain the ION context. We do this because
  /// interfaces can't have static properties. 
  /// </summary>
  public sealed class AppState {
    /// <summary>
    /// The current app state. If the current app state has not been set,
    /// then we will throw an exception when the property is fetched.
    /// </summary>
    /// <value>The App.</value>
    public static IION APP {
      get {
        if (__APP == null) {
          string msg = "Critical failure: Application attempted to retrieve ION context, yet the application was not running.";
          Log.C("AppState.ION", msg);
          throw new Exception(msg);
        }
        return __APP;
      }
      set {
        __APP = value;
      }
    } private static IION __APP;
  } // End AppState
  /// <summary>
  /// The interface that describes an ION application context.
  /// </summary>
  public interface IION : IDisposable {
    /// <summary>
    /// Queries the device manager for the ION instance.
    /// </summary>
    /// <value>The device manager.</value>
    IDeviceManager deviceManager { get; }
  } // End IION

  public class BaseION : IION {
    // Overridden from IION
    public IDeviceManager deviceManager { get; set; }

    // Overridden from IION
    public void Dispose() {
      if (deviceManager != null) {
        deviceManager.Dispose();
      }
    }
  } // End BaseION
}

