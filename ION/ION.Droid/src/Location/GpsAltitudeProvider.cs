namespace ION.Droid.Location {

  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Android.Content;
  using Android.Locations;
  using Android.OS;

  using ION.Core.Util;

  /// <summary>
  /// An object that describes an event within the altitude manager.
  /// </summary>
  public class AltitudeEvent {
    public EType type { get; internal set; }
    public Location location { get; internal set; }

    public AltitudeEvent(EType type, Location location = null) {
      this.type = type;
      this.location = location;
    }

    /// <summary>
    /// The enumeration of the altitude event types.
    /// </summary>
    public enum EType {
      NewLocation,
      ProviderChange,
    }
  }

  /// <summary>
  /// The class is used to bypass the inability of the GoogleApiClient to return a location with an altitude.
  /// </summary>
  public class GpsAltitudeProvider : Java.Lang.Object, ILocationListener, IDisposable {
    /// <summary>
    /// The delegate that is used to register to the OnAltitudeEvent event.
    /// </summary>
    public delegate void OnAltitudeEvent(GpsAltitudeProvider pam, AltitudeEvent ae);
    /// <summary>
    /// The event that is notified when the altitude manager throws a new event.
    /// </summary>
    public event OnAltitudeEvent onAltitudeEvent;

    /// <summary>
    /// Queries whether or not the altitude manager is enabled.
    /// </summary>
    /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
    public bool isEnabled {
      get {
        return lm.GetProvider(LocationManager.GpsProvider) != null && lm.IsProviderEnabled(LocationManager.GpsProvider);
      }
    }
    /// <summary>
    /// The last known location that the manager received from the android location manager. May be null.
    /// </summary>
    /// <value>The last known location.</value>
    public Location lastKnownLocation { get; internal set; }
    /// <summary>
    /// The location manager that we are subscribed to to received new locations with altitudes.
    /// </summary>
    private LocationManager lm;
    /// <summary>
    /// The last time that a location was received by the manager.
    /// </summary>
    private DateTime lastLocationReceivedTime;

    private bool disposed;

    public GpsAltitudeProvider(LocationManager lm) {
      this.lm = lm;
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Droid.Location.GpsAltitudeProvider"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Droid.Location.GpsAltitudeProvider"/>.
    /// The <see cref="Dispose"/> method leaves the <see cref="ION.Droid.Location.GpsAltitudeProvider"/> in an unusable
    /// state. After calling <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Droid.Location.GpsAltitudeProvider"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.Droid.Location.GpsAltitudeProvider"/> was occupying.</remarks>
    public void Dispose() {
      lm.RemoveUpdates(this);
      disposed = true;

    }

    /// <Docs>The new location, as a Location object.</Docs>
    /// <remarks>Called when the location has changed.</remarks>
    /// <summary>
    /// Raises the location changed event.
    /// </summary>
    /// <param name="location">Location.</param>
    public void OnLocationChanged(Location location) {
       Log.D(this, "GpsAltitudeProvider says that it got a new location: " + location);
      lastKnownLocation = location;
      lastLocationReceivedTime = DateTime.Now;
      NotifyEvent(new AltitudeEvent(AltitudeEvent.EType.NewLocation, location));
    }

    /// <Docs>the name of the location provider associated with this
    ///  update.</Docs>
    /// <summary>
    /// Raises the status changed event.
    /// </summary>
    /// <param name="provider">Provider.</param>
    /// <param name="status">Status.</param>
    /// <param name="extras">Extras.</param>
    public void OnStatusChanged(string provider, Availability status, Bundle extras) {
    }

    /// <Docs>the name of the location provider associated with this
    ///  update.</Docs>
    /// <remarks>Called when the provider is enabled by the user.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the provider enabled event.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public void OnProviderEnabled(string provider) {
      NotifyEvent(new AltitudeEvent(AltitudeEvent.EType.ProviderChange));
    }

    /// <Docs>the name of the location provider associated with this
    ///  update.</Docs>
    /// <remarks>Called when the provider is disabled by the user. If requestLocationUpdates
    ///  is called on an already disabled provider, this method is called
    ///  immediately.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// <param name="provider">Provider.</param>
    public void OnProviderDisabled(string provider) {
      NotifyEvent(new AltitudeEvent(AltitudeEvent.EType.ProviderChange));
    }

    /// <summary>
    /// Start the location gathering.
    /// </summary>
    public void StartUpdates() {
      lm.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
    }

    /// <summary>
    /// Stops location gathering.
    /// </summary>
    public void StopUpdates() {
      lm.RemoveUpdates(this);
    }

    /// <summary>
    /// Requests a single location update form the location manager.
    /// </summary>
    /// <returns>The single location.</returns>
    public Task<Location> RequestSingleLocation() {
      return Task.Factory.StartNew(() => {
        var old = lastKnownLocation;

        lm.RequestSingleUpdate(LocationManager.GpsProvider, this, Looper.MainLooper);

        var start = DateTime.Now;
        while (!disposed && (lastKnownLocation == null || !lastKnownLocation.Equals(old))) {
          Log.D(this, "gps location not found after " + (DateTime.Now - start));
          Thread.Sleep(250);
        }
        var ellapsed = DateTime.Now - start;
        Log.E(this, "GpsAltitudeProvider took " + ellapsed + " to find a single location");

        return lastKnownLocation;
      });
    }

    /// <summary>
    /// Throws a new event to the altitude event.
    /// </summary>
    /// <param name="ae">Ae.</param>
    private void NotifyEvent(AltitudeEvent ae) {
      if (onAltitudeEvent != null) {
        onAltitudeEvent(this, ae);
      }
    }
  }
}

