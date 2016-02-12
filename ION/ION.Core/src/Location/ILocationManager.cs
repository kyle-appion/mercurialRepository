﻿using System;

using ION.Core.App;
using ION.Core.Measure;

namespace ION.Core.Location {
  public delegate void OnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation);

  public interface ILocationManager : IIONManager {
    /// <summary>
    /// The event that will be notified when the location manager's location changes.
    /// </summary>
    event OnLocationChanged onLocationChanged;

    /// <summary>
    /// Whether or not the location manager should track locations.
    /// </summary>
    /// <value><c>true</c> if allow location tracking; otherwise, <c>false</c>.</value>
    bool allowLocationTracking { get; set; }

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

