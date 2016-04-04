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
  GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener, ISharedPreferencesOnSharedPreferenceChangeListener {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;

    private const long REQUEST_INTERVAL = 60 * 1000 * 15;
    private const long INITIAL_WAIT = 5000;

    // Overridden from ILocationManager
    public bool allowLocationTracking {
      get {
        return ion.preferences.location.allowsGps;
      }
      set {
        ion.preferences.location.allowsGps = true;
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
          Log.E(this, "New Last Known Android Location " + __lastKnownLocation);
        } else {
          Log.E(this, "Last Known Android Location is null");
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
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public async Task<InitializationResult> InitAsync() {
      Log.D(this, "Altitude: " + await CoerceAltitude());

      ion.preferences.prefs.RegisterOnSharedPreferenceChangeListener(this);

      if (ion.preferences.location.allowsGps) {
        if (await InitGoogleApis()) {
          return new InitializationResult() {
            success = true,
          };
        }
      }

      lastKnownLocation = new SimpleLocation() {
        altitude = ion.preferences.units.length.OfScalar(ion.preferences.location.customElevation),
      };

      return new InitializationResult() {
        success = true,
      };
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
    public new void Dispose() {
      base.Dispose();
      ion.preferences.prefs.UnregisterOnSharedPreferenceChangeListener(this);
      LocationServices.FusedLocationApi.RemoveLocationUpdates(google, this);
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
    /// Queries an address from the given location.
    /// </summary>
    /// <param name="location">Location.</param>
    public async Task<ION.Core.Location.Address> GetAddressFromLocationAsync(ILocation location) {
      var lat = location.latitude.amount;
      var lng = location.longitude.amount;
      var geocoder = new Geocoder(ion, Java.Util.Locale.Default);
      var addresses = await geocoder.GetFromLocationAsync(lat, lng, 1);
      if (addresses == null || addresses.Count <= 0) {
        return new ION.Core.Location.Address();
      }
      var first = addresses[0];
      if (first == null) {
        return new ION.Core.Location.Address();
      } else {
        return new ION.Core.Location.Address() {
          address = first.GetAddressLine(0),
          city = first.Locality,
          state = first.AdminArea,
          zip = first.PostalCode,
        };
      }
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
      var loc = BuildLocation(location);
      lastKnownLocation = loc;
    }

    /// <summary>
    /// Raises the shared preferences changed event.
    /// </summary>
    /// <param name="prefs">Prefs.</param>
    /// <param name="key">Key.</param>
    public async void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (ion.GetString(Resource.String.pkey_location_gps).Equals(key)) {
        if (prefs.GetBoolean(key, true)) {
          if (!await InitGoogleApis()) {
            Log.E(this, "Failed to initialize the google APIs after preferences changed");
          }
        } else {
          lastKnownLocation = new SimpleLocation() {
            altitude = ion.preferences.units.length.OfScalar(ion.preferences.location.customElevation),
          };
        }
      } else if (ion.GetString(Resource.String.pkey_location_elevation).Equals(key)) {
        lastKnownLocation = new SimpleLocation() {
          altitude = ion.preferences.units.length.OfScalar(ion.preferences.location.customElevation),
        };
      }
    }

    /// <summary>
    /// Builds a new simple location from the Android location.
    /// </summary>
    /// <returns>The location.</returns>
    /// <param name="location">Location.</param>
    private ILocation BuildLocation(Location location) {
      return new SimpleLocation(true, location.Altitude, location.Longitude, location.Latitude);
    }

    /// <summary>
    /// Attempts to coerce the altitude from the Location APIs.
    /// </summary>
    private async Task<double> CoerceAltitude() {
      var lm = ion.GetSystemService(Context.LocationService) as LocationManager;
      if (lm == null) {
        return 0;
      }

      var lp = lm.GetProvider(LocationManager.GpsProvider);
      if (lp == null) {
        return 0;
      }

      if (!lp.SupportsAltitude()) {
        Log.D(this, "Cannot get altitude: GPS does not support the feature.");
        return 0;
      }

      var location = lm.GetLastKnownLocation(LocationManager.GpsProvider);

      lm.RequestSingleUpdate(LocationManager.GpsProvider, new AndroidLocationListener() {
        locationHandler = (o, l) => {
          location = l;
        },
      }, Looper.MainLooper);

      var start = DateTime.Now;
      while (location == null && DateTime.Now - start < TimeSpan.FromMilliseconds(1500)) {
        Log.D(this, "Waiting for location");
        await Task.Delay(50);
      }

      return location != null ? location.Altitude : 0;
    }

    /// <summary>
    /// Creates a generic location request.
    /// </summary>
    /// <returns>The location request.</returns>
    private LocationRequest CreateLocationRequestSingleUpdate() {
      var ret = new LocationRequest();

      ret.SetInterval(1000);
      ret.SetPriority(LocationRequest.PriorityHighAccuracy);
      ret.SetNumUpdates(1);

      return ret;
    }

    /// <summary>
    /// Initializes the google map APIs.
    /// </summary>
    /// <returns>The google apis.</returns>
    private async Task<bool> InitGoogleApis() {
//      return Task.Factory.StartNew(() => {
      google = new GoogleApiClient.Builder(ion)
        .AddConnectionCallbacks(this)
        .AddOnConnectionFailedListener(this)
        .AddApi(LocationServices.API)
        .Build();
      
      google.Connect();

      var started = DateTime.Now;

      while (!google.IsConnected && DateTime.Now - started < TimeSpan.FromMilliseconds(INITIAL_WAIT)) {
        await Task.Delay(10);
      }

      Log.D(this, "google.IsConnected: " + google.IsConnected);
      LocationServices.FusedLocationApi.RequestLocationUpdates(google, CreateLocationRequestSingleUpdate(), this);

      if (!google.IsConnected) {
        return false;
      } else {
        return true;
      }
//      });
    }
  }

  /// <summary>
  /// The listener that will listen for requested updates.
  /// </summary>
  internal class AndroidLocationListener : Java.Lang.Object, Android.Locations.ILocationListener {
    public EventHandler<Location> locationHandler;
    public EventHandler<string> providerDisabledHandler;
    public EventHandler<string> providerEnabledHandler;

    /// <Docs>The new location, as a Location object.</Docs>
    /// <remarks>Called when the location has changed.</remarks>
    /// <summary>
    /// Raises the location changed event.
    /// </summary>
    /// <param name="location">Location.</param>
    public void OnLocationChanged(Location location) {
      if (locationHandler != null) {
        locationHandler(this, location);
      }
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
    public void OnProviderEnabled (string provider) {
      if (providerEnabledHandler != null) {
        providerEnabledHandler(this, provider);
      }
    }
    /// <Docs>the name of the location provider associated with this
    ///  update.</Docs>
    /// <remarks>Called when the provider is disabled by the user. If requestLocationUpdates
    ///  is called on an already disabled provider, this method is called
    ///  immediately.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the provider disabled event.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public void OnProviderDisabled (string provider) {
      if (providerDisabledHandler != null) {
        providerDisabledHandler(this, provider);
      }
    }
    /// <Docs>the name of the location provider associated with this
    ///  update.</Docs>
    /// <summary>
    /// Raises the status changed event.
    /// </summary>
    /// <param name="provider">Provider.</param>
    /// <param name="status">Status.</param>
    /// <param name="extras">Extras.</param>
    public void OnStatusChanged (string provider, Availability status, Bundle extras) {
      // Nope
    }
  }
}

