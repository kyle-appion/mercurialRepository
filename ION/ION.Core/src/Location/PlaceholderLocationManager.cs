namespace ION.Core.Location {

  using System;
  using System.Threading.Tasks;

  using ION.Core.App;

  /// <summary>
  /// This location manager is a location manager is primarily used when a platform's real location manager fails to
  /// resolve. This will allow the rest of the application to always depend on a location manager, even if it is not
  /// doing anything.
  /// </summary>
  public class PlaceholderLocationManager : ILocationManager {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;

    /// <summary>
    /// Whether or not the location manager should track locations.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool allowLocationTracking { get; set; }

    /// <summary>
    /// The last location known by the location manager.
    /// </summary>
    /// <value>The last know location.</value>
    public ILocation lastKnownLocation { get; private set; }
    /// <summary>
    /// Whether or not the location manager is polling locations.
    /// </summary>
    /// <value><c>true</c> if is polling; otherwise, <c>false</c>.</value>
    public bool isPolling { get; private set; }

    public PlaceholderLocationManager() {
      allowLocationTracking = false;
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() {
        success = true,
      });
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Location.PlaceholderLocationManager"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Core.Location.PlaceholderLocationManager"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Core.Location.PlaceholderLocationManager"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Core.Location.PlaceholderLocationManager"/> so the garbage collector can reclaim the memory that
    /// the <see cref="ION.Core.Location.PlaceholderLocationManager"/> was occupying.</remarks>
    public void Dispose() {
    }

    /// <summary>
    /// Attempts to start automatic location polling.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public bool StartAutomaticLocationPolling() {
      return false;
    }

    /// <summary>
    /// Stops the automatic polling.
    /// </summary>
    public void StopAutomaticLocationPolling() {
    }
  }
}

