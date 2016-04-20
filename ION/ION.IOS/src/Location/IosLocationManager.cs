namespace ION.IOS.Location {

  using System;
  using System.Threading.Tasks;

  using CoreLocation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Measure;
  using ION.Core.Location;
  using ION.Core.Util;

  using ION.IOS.App;


  /// <summary>
  /// The entity that will manage the user's location such that we can determine his
  /// elevation for use in PT calculations.
  /// </summary>
  public class IosLocationManager : ILocationManager {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;

    // Overridden from ILocationManager
    public bool allowLocationTracking { 
      get {
        return ion.settings.location.useGeoLocation;
      }
      set {
        ion.settings.location.useGeoLocation = value;
      }
    }

    // Overridden from ILocationManager
    public ILocation lastKnownLocation {
      get {
        return __lastKnownLocation;
      }
      private set {
        var old = __lastKnownLocation;
        __lastKnownLocation = value;
        if (onLocationChanged != null) {
          onLocationChanged(this, old, value);
        }
      }
    } ILocation __lastKnownLocation;
    // Overridden from ILocationManager
    public bool isPolling { get; private set; }

    /// <summary>
    /// The ion owning this manager.
    /// </summary>
    /// <value>The ion.</value>
    private IosION ion { get; set; }
    /// <summary>
    /// The native location manager.
    /// </summary>
    /// <value>The location manager.</value>
    private CLLocationManager native { get; set; }

    public IosLocationManager(IosION ion) {
      this.ion = ion;
      lastKnownLocation = new IosLocation();
    }

    // Overridden from ILocationManager
    public async Task<InitializationResult> InitAsync() {
      native = new CLLocationManager();

      if (ion.settings.location.useGeoLocation) {
        // We should assert that the application will always be compiled to 8.0
//      if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
        // But just in case, be safe.
//        native.RequestWhenInUseAuthorization();
        native.RequestAlwaysAuthorization();
//      }

//      if (ion.settings.location.useGeoLocation) {
        StartAutomaticLocationPolling();
      }
//      }
      return new InitializationResult() { success = true };
    }

    // Overridden frm ILocationManager
    public void Dispose() {
      StopAutomaticLocationPolling();
    }

    // Overridden from ILocationManager
    public bool StartAutomaticLocationPolling() {
      Log.D(this, "Starting location polling");
      if (CLLocationManager.LocationServicesEnabled) {
        native.DesiredAccuracy = 100;// Desired accuracy in meters.
      }

      native.LocationsUpdated += ResolveLocationChange;
      native.StartMonitoringSignificantLocationChanges();
      isPolling = true;
      return true;
    }

    // Overridden from ILocationManager
    public void StopAutomaticLocationPolling() {
      native.LocationsUpdated -= ResolveLocationChange;
      isPolling = false;
    }

    /// <summary>
    /// Gets the addres from location async.
    /// </summary>
    /// <returns>The addres from location async.</returns>
    /// <param name="location">Location.</param>
    public Task<Address> GetAddressFromLocationAsync(ILocation location) {
      // TODO ahodder@appioninc.com: Implement get address
      return Task.FromResult(default(Address));
    }

    /// <summary>
    /// Called when we receive new location data.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void ResolveLocationChange(object sender, CLLocationsUpdatedEventArgs e) {
      var len = e.Locations.Length;
      if (len > 0) {
        lastKnownLocation = new IosLocation(e.Locations[len - 1]);
        Log.D(this, "New " + lastKnownLocation);
      }
    }
  }

  internal class IosLocation : ILocation {
    public bool isValid { get; }
    // Overridden from ILocation
    public Scalar altitude { get; }
    // Overridden from ILocation
    public Scalar longitude { get; }
    // Overridden from ILocation
    public Scalar latitude { get; }

    public IosLocation() {
      isValid = false;
      altitude = Units.Length.METER.OfScalar(0);
      latitude = Units.Angle.DEGREE.OfScalar(0);
      longitude = Units.Angle.DEGREE.OfScalar(0);
    }

    public IosLocation(CLLocation location) {
      isValid = true;
      altitude = Units.Length.METER.OfScalar(location.Altitude);
      latitude = Units.Angle.DEGREE.OfScalar(location.Coordinate.Latitude);
      longitude = Units.Angle.DEGREE.OfScalar(location.Coordinate.Longitude);
    }

    // Overridden from Object
    public override string ToString() {
      return "Location {altitude: " + altitude + ", longitude: " + longitude + ", latitude: " + latitude + "}";
    }
  }
}

