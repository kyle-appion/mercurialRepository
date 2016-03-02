namespace ION.Droid.Location {

  using System;
  using System.Threading.Tasks;

  using Android.Content;
  using Android.Gms.Common;
  using Android.Gms.Common.Apis;
  using Android.Gms.Location;
  using Android.Locations;
  using Android.OS;

  using ION.Core.App;
  using ION.Core.Location;
  using ION.Core.Measure;
  using ION.Core.Util;

  using ION.Droid.App;


  public class AndroidLocationManager : Java.Lang.Object, ILocationManager, GoogleApiClient.IConnectionCallbacks,
  GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;

    private const long REQUEST_INTERVAL = 60 * 1000 * 15;
    private const long INITIAL_WAIT = 5000;

    // Overridden from ILocationManager
    public bool allowLocationTracking {
      get {
        ion.preferences.GetBoolean(ion.GetString(Resource.String.preferences_location_use), false);
        return false;
      }
      set {
      }
    }

    // Overridden from ILocationManager;
    public ILocation lastKnownLocation {
      get {
        return __lastKnownLocation;
      }
      set {
        var old = __lastKnownLocation;
        __lastKnownLocation = value;
        if (__lastKnownLocation != null) {
          Log.D(this, "New Last Known Android Location " + __lastKnownLocation);
        } else {
          Log.D(this, "Last Known Android Location is null");
        }
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
    private AndroidION ion { get; set; }
    /// <summary>
    /// The google client that will resolve the location management.
    /// </summary>
    /// <value>The google.</value>
    private GoogleApiClient google { get; set; }

    public AndroidLocationManager(AndroidION ion) {
      this.ion = ion;
      google = new GoogleApiClient.Builder(ion)
        .AddConnectionCallbacks(this)
        .AddOnConnectionFailedListener(this)
        .AddApi(LocationServices.API)
        .Build();
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.Factory.StartNew(() => {
        google.Connect();

        var started = DateTime.Now;

        while (!google.IsConnected && DateTime.Now - started < TimeSpan.FromMilliseconds(INITIAL_WAIT)) {
          Task.Delay(10);
        }

        Log.D(this, "google.IsConnected: " + google.IsConnected);

        if (!google.IsConnected) {
          return new InitializationResult() {
            success = false,
            errorMessage = "Failed to initialize location manager",
          };
        } else {
          return new InitializationResult() {
            success = true,
          };
        }
      });
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Droid.Location.AndroidLocationManager"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Droid.Location.AndroidLocationManager"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Droid.Location.AndroidLocationManager"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Droid.Location.AndroidLocationManager"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.Droid.Location.AndroidLocationManager"/> was occupying.</remarks>
    public void Dispose() {
      base.Dispose();
    }

    /// <summary>
    /// Attempts to start automatic location polling.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public bool StartAutomaticLocationPolling() {
      if (isPolling) {
        LocationServices.FusedLocationApi.RemoveLocationUpdates(google, this);
      }

      var request = LocationRequest.Create();
      request.SetPriority(LocationRequest.PriorityNoPower);
      request.SetInterval(REQUEST_INTERVAL);

      // TODO ahodder@appioninc.com: This code is broken; the request could fail or be cancelled, and we would never know.
      var pendingResult = LocationServices.FusedLocationApi.RequestLocationUpdates(google, request, this);
      isPolling = true;
      return isPolling;
    }

    /// <summary>
    /// Stops location polling.
    /// </summary>
    public void StopAutomaticLocationPolling() {
      LocationServices.FusedLocationApi.RemoveLocationUpdates(google, this);
      isPolling = false;
    }

    /// <summary>
    /// Raises the connected event.
    /// </summary>
    /// <param name="connectionHint">Connection hint.</param>
    public void OnConnected(Bundle connectionHint) {
      var location = LocationServices.FusedLocationApi.GetLastLocation(google);
      if (location != null) {
        lastKnownLocation = new SimpleLocation(true, location.Altitude, location.Longitude, location.Latitude);
      } else {
        lastKnownLocation = new SimpleLocation();
      }
    }

    /// <summary>
    /// Raises the connection suspended event.
    /// </summary>
    /// <param name="cause">Cause.</param>
    public void OnConnectionSuspended(int cause) {
      Log.E(this, "Connection was suspended with cause: " + cause);
    }

    /// <summary>
    /// Raises the connection failed event.
    /// </summary>
    /// <param name="result">Result.</param>
    public void OnConnectionFailed(ConnectionResult result) {
      Log.E(this, "Location connection failed: {" + result.ErrorCode + ": " + result.ErrorMessage + "}");
    }

    /// <summary>
    /// Raises the location changed event.
    /// </summary>
    /// <param name="location">Location.</param>
    public void OnLocationChanged(Location location) {
      lastKnownLocation = new SimpleLocation(true, location.Altitude, location.Longitude, location.Latitude);
    }
  }
}

