namespace ION.Core.Location {

  using System;
  using System.Threading.Tasks;

	using Appion.Commons.Measure;

  using ION.Core.App;

  /// <summary>
  /// This location manager is a location manager is primarily used when a platform's real location manager fails to
  /// resolve. This will allow the rest of the application to always depend on a location manager, even if it is not
  /// doing anything.
  /// </summary>
  public class ManualLocationManager : ILocationManager {
		// Implemented from ILocationManager
		public event OnLocationChanged onLocationChanged;

		// Implemented from ILocationManager
		public bool isEnabled { get { return true; } } 
    // Implemented from ILocationManager
    public bool supportsAltitudeTracking { get { return false; } }
		// Implemented from ILocationManager
		public bool allowLocationTracking { get { return true; } set {} } 
		// Implemented from ILocationManager
		public ILocation lastKnownLocation { get; private set; }
		// Implemented from ILocationManager
		public bool isPolling { get { return true; } }
		// Implemented from ILocationManager
		public bool isInitialized { get { return true; } }

    public ManualLocationManager() {
      allowLocationTracking = false;
    }

		// Implemented from ILocationManager
		public Task<InitializationResult> InitAsync() {
			return Task.FromResult(new InitializationResult() {
				success = true,
			});
		}

    // Implemented from ILocationManager
    public void PostInit() {
    }


		// Implemented from ILocationManager
		public void Dispose() {
		}

		// Implemented from ILocationManager
		public bool StartAutomaticLocationPolling() {
			return true;
		}

		// Implemented from ILocationManager
		public void StopAutomaticLocationPolling() {
		}
		// Implemented from ILocationManager
		public void setLocationRemote(double remoteAltitude){
		}

		// Implemented from ILocationManager
		public bool AttemptSetLocation(Scalar remoteAltitude) {
			return false;
		}

		// Implemented from ILocationManager
		public Task<Address> GetAddressFromLocationAsync(ILocation location) {
			return null;
		}
  }
}

