namespace ION.Core.Location {

  using System;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Measure;

  public delegate void OnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation);

  /// <summary>
  /// A manager that will abstract a platforms location interfaces.
  /// </summary>
  public interface ILocationManager : IIONManager {
    /// <summary>
    /// The event that will be notified when the location manager's location changes.
    /// </summary>
    event OnLocationChanged onLocationChanged;

    /// <summary>
    /// Whether or not the location manager is enabled (in a working state).
    /// </summary>
    /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
    bool isEnabled { get; }

    /// <summary>
    /// Whether or not the location manager should track locations.
    /// </summary>
    /// <value><c>true</c> if allow location tracking; otherwise, <c>false</c>.</value>
    bool allowLocationTracking { get; set; }

    /// <summary>
    /// Whether or not the location manager get get the altitude of the user.
    /// </summary>
    /// <value><c>true</c> if can get altitude; otherwise, <c>false</c>.</value>
//    bool canGetAltitude { get; set; }

    /// <summary>
    /// The last location known by the location manager.
    /// </summary>
    /// <value>The last know location.</value>
    ILocation lastKnownLocation { get; }

    /// <summary>
    /// Whether or not the location manager is polling locations.
    /// </summary>
    bool isPolling { get; }

    /// <summary>
    /// Attempts to start automatic location polling.
    /// </summary>
    /// <returns><c>true</c>, if automatic location polling was started, <c>false</c> otherwise.</returns>
    bool StartAutomaticLocationPolling();

    /// <summary>
    /// Stops location polling.
    /// </summary>
    void StopAutomaticLocationPolling();

    /// <summary>
    /// Queries the address of the given location.
    /// </summary>
    /// <returns>The address from location async.</returns>
    /// <param name="location">Location.</param>
    Task<Address> GetAddressFromLocationAsync(ILocation location);
  }

  /// <summary>
  /// A simple entity encapsulating a location.
  /// </summary>
  public interface ILocation {
    /// <summary>
    /// Wether or not the location is valid.
    /// </summary>
    /// <value><c>true</c> if is valid; otherwise, <c>false</c>.</value>
    bool isValid { get; }
    /// <summary>
    /// The altitude of the location.
    /// </summary>
    /// <value>The altitude.</value>
    Scalar altitude { get; }
    /// <summary>
    /// The longitude of the location.
    /// </summary>
    /// <value>The longitude.</value>
    Scalar longitude { get; }
    /// <summary>
    /// The latitude of the location.
    /// </summary>
    /// <value>The latitude.</value>
    Scalar latitude { get; }
  }
}

