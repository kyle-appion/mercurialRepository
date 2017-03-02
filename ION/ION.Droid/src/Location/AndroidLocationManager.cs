namespace ION.Droid.Location {

  using System;
  using System.Threading.Tasks;

  using Android.Content;
	using Android.Content.PM;
  using Android.Gms.Common;
  using Android.Gms.Common.Apis;
  using Android.Gms.Location;
  using Android.Locations;
  using Android.OS;
  using Android.Support.V4.Content;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Location;

  using ION.Droid.App;

  public class AndroidLocationManager : Java.Lang.Object, ILocationManager, GoogleApiClient.IConnectionCallbacks,
  GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener,
	ISharedPreferencesOnSharedPreferenceChangeListener {
    /// <summary>
    /// The event that will be notified when the location manager's location changes.
    /// </summary>
    public event OnLocationChanged onLocationChanged;

		public bool isInitialized { get { return true; } }

    /// <summary>
    /// Whether or not the location manager is enabled (in a working state).
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isEnabled {
      get {
				return ion.preferences.location.allowsGps && (client != null && client.IsConnected);
      }
    }
    /// <summary>
    /// Whether or not the location manager should track locations.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool allowLocationTracking { get; set; }
    /// <summary>
    /// The last known location of the location manager.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    /// <summary>
    /// The last location known by the location manager.
    /// </summary>
    /// <value>The last know location.</value>
    public ILocation lastKnownLocation {
      get {
        return __lastKnownLocation;
      }
      internal set {
        if (value == null) {
          value = new SimpleLocation();
        }
        __lastKnownLocation = value;
      }
    } ILocation __lastKnownLocation;
    /// <summary>
    /// Whether or not the location manager is polling locations.
    /// </summary>
    /// <value><c>true</c> if is polling; otherwise, <c>false</c>.</value>
    public bool isPolling { get; internal set; }
    /// <summary>
    /// The ion instance that provides a android 
    /// </summary>
		private BaseAndroidION ion;
    /// <summary>
    /// The client that will communication with the location services.
    /// </summary>
    private GoogleApiClient client;
    /// <summary>
    /// The provider that will attempt to glean the user's altitude.
    /// </summary>
    private GpsAltitudeProvider altitudeProvider;
    /// <summary>
    /// The current running location request. Null is not request is running.
    /// </summary>
    private LocationRequest request;

		public AndroidLocationManager(BaseAndroidION ion) {
      this.ion = ion;
      lastKnownLocation = new SimpleLocation();
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
    private void DoDispose() {
      StopAutomaticLocationPolling();
      ion.preferences.prefs.UnregisterOnSharedPreferenceChangeListener(this);
      if (altitudeProvider != null) {
        altitudeProvider.Dispose();
      }
			onLocationChanged = null;
    }

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			DoDispose();
		}

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public async Task<InitializationResult> InitAsync() {
			if (Permission.Granted == ContextCompat.CheckSelfPermission(ion.context, Android.Manifest.Permission.AccessFineLocation)) {
				altitudeProvider = new GpsAltitudeProvider(ion.context.GetSystemService(Context.LocationService) as LocationManager);
				altitudeProvider.onAltitudeEvent += (ap, e) => {
					lastKnownLocation = new SimpleLocation(true, e.location.Altitude, e.location.Longitude, e.location.Latitude);
					Log.D(this, "The GpsAltitudeProvider sent us a location of: " + lastKnownLocation);
				};

	      if (IsGooglePlayServicesInstalled()) {
	        client = InitGooglePlayServices();
	        client.Connect();
	        while (client.IsConnecting && !client.IsConnected) {
	          await Task.Delay(50);
	        }
	        if (client.IsConnected) {
	          StartAutomaticLocationPolling();
	        }
	      }
	     
			} else {
				Log.E(this, "The user denied the location permission. We will not allow the location to update.");
				ion.preferences.location.allowsGps = false;
			}

			ion.preferences.prefs.RegisterOnSharedPreferenceChangeListener(this);

      return new InitializationResult() {
        success = true,
      };
    }

    /// <summary>
    /// Attempts to start automatic location polling.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public bool StartAutomaticLocationPolling() {
			if (ion.preferences.location.allowsGps) {
	      Log.D(this, "Startings automatic location polling");
	      request = new LocationRequest();
	      request.SetFastestInterval(500);
	      request.SetInterval(3000);
	      request.SetNumUpdates(60);
	      request.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);
	      LocationServices.FusedLocationApi.RequestLocationUpdates(client, request, this);
	      isPolling = true;
				altitudeProvider.StartUpdates();
				altitudeProvider.PostRequestSingleLocation();
	      return true;
			} else {
				return false;
			}
    }

    /// <summary>
    /// Stops location polling.
    /// </summary>
    public void StopAutomaticLocationPolling() {
      Log.D(this, "Stopping automatic location polling");
			if (client != null) {
      	LocationServices.FusedLocationApi.RemoveLocationUpdates(client, this);
			}
      isPolling = false;
			altitudeProvider.StopUpdates();
    }

		public bool AttemptSetLocation(Scalar altitude){
			return false;
		}

    /// <summary>
    /// Gets the address from location async. This may throw a timeout exception if the geocoder fails to work.
    /// </summary>
    /// <returns>The address from location async.</returns>
    /// <param name="location">Location.</param>
    public async Task<ION.Core.Location.Address> GetAddressFromLocationAsync(ILocation location) {
      var lat = location.latitude.amount;
      var lng = location.longitude.amount;
      var geocoder = new Geocoder(ion.context, Java.Util.Locale.Default);
      var addresses = await geocoder.GetFromLocationAsync(lat, lng, 1);
      if (addresses == null || addresses.Count <= 0) {
        return new ION.Core.Location.Address();
      }
      var first = addresses[0];
      if (first == null) {
        return new ION.Core.Location.Address();
      } else {
        return new ION.Core.Location.Address() {
          address1 = first.GetAddressLine(0),
          city = first.Locality,
          state = first.AdminArea,
          zip = first.PostalCode,
        };
      }
    }

    /// <summary>
    /// Called when connected to the GoogleApiClient.
    /// </summary>
    /// <param name="bundle">Bundle.</param>
    public void OnConnected(Bundle bundle) {
      Log.D(this, "OnConnected to the GoogleApiClient");
    }

    /// <summary>
    /// Called when disconnected from the GoogleApiClient.
    /// </summary>
    public void OnDisconnected() {
      Log.D(this, "OnDisconnected from the GoogleApiClient");
    }

    /// <summary>
    /// Called when the connection to the GoogleApiClient fails.
    /// </summary>
    /// <param name="bundle">Bundle.</param>
    public void OnConnectionFailed(ConnectionResult bundle) {
      Log.E(this, "Failed to connect to the GoogleApiClient");
    }

    /// <summary>
    /// Called when the GoogleApiClient notifies of a location change.
    /// </summary>
    /// <param name="location">Location.</param>
    public void OnLocationChanged(Location location) {
      var altitude = location.Altitude;
      if (altitude == 0) {
				// TODO ahodder@appioninc.com: the altitude provider should not be null
				if (altitudeProvider != null) {
	        var l = altitudeProvider.lastKnownLocation;
	        if (l != null) {
	          altitude = l.Altitude;
	        }
				} else {
					Log.E(this, "The altitude provider was null");
				}
      }
			Log.D(this, "Location changed: " + location + ", Altitude: " + altitude + ", hasAltitude: " + location.HasAltitude);
			if (location.HasAltitude) {
				lastKnownLocation = new SimpleLocation(true, altitude, location.Longitude, location.Latitude);
			} else {
				lastKnownLocation = new SimpleLocation(true, altitude, location.Longitude, lastKnownLocation.altitude.ConvertTo(Units.Length.METER).amount);
			}
    }

    /// <summary>
    /// Called when the location is suspended.
    /// </summary>
    /// <param name="i">The index.</param>
    public void OnConnectionSuspended(int i) {
      Log.D(this, "GoogleApiClient connection suspended");
    }

    /// <summary>
    /// Raises the shared preferences changed event.
    /// </summary>
    /// <param name="prefs">Prefs.</param>
    /// <param name="key">Key.</param>
    public async void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (ion.context.GetString(Resource.String.pkey_location_gps).Equals(key)) {
        if (prefs.GetBoolean(key, false)) {
					ion.preferences.location.askForPermissions = true;
          await InitAsync();
        } else {
          StopAutomaticLocationPolling();
          lastKnownLocation = new SimpleLocation() {
            altitude = ion.preferences.units.length.OfScalar(ion.preferences.location.customElevation),
          };
        }
      } else if (ion.context.GetString(Resource.String.pkey_location_elevation).Equals(key)) {
        lastKnownLocation = new SimpleLocation() {
          altitude = ion.preferences.units.length.OfScalar(ion.preferences.location.customElevation),
        };
      }
    }

    /// <summary>
    /// Queries whether or not google play services is installed.
    /// </summary>
    /// <returns><c>true</c> if this instance is google play services installed; otherwise, <c>false</c>.</returns>
    private bool IsGooglePlayServicesInstalled() {
      var instance = GoogleApiAvailability.Instance;
      int queryResult = instance.IsGooglePlayServicesAvailable(ion.context);
      if (ConnectionResult.Success == queryResult) {
        return true;
      }

      if (instance.IsUserResolvableError(queryResult)) {
        Log.E(this, string.Format("Google play services are not installed on this device: {0}", instance.GetErrorString(queryResult)));
      }

      return false;
    }

    /// <summary>
    /// Inits the google play services.
    /// </summary>
    /// <returns><c>true</c>, if google play services was inited, <c>false</c> otherwise.</returns>
    private GoogleApiClient InitGooglePlayServices() {
      return new GoogleApiClient.Builder(ion.context, this, this)
        .AddApi(LocationServices.API).Build();
    }
  }
}
