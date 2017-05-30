namespace ION.Droid.Location {

	using System;
	using System.Threading.Tasks;

	using Appion.Commons.Measure;

	using ION.Core.App;
	using ION.Core.Location;

	public class RemoteLocationManager : ILocationManager {
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
		// Implemented for ILocationManager
		public DateTime lastTimeLocationChanged { get; private set; }
		// Implemented from ILocationManager
		public bool isPolling { get { return true; } }
		// Implemented from ILocationManager
		public bool isInitialized { get { return true; } }

		public RemoteLocationManager() {
		}

		// Implemented from ILocationManager
		public Task<InitializationResult> InitAsync() {
			return Task.FromResult(new InitializationResult() {
				success = true,
			});
		}

    // Implemented for ILocationManager
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

		public bool AttemptSetLocation(Scalar altitude){
			if (altitude.unit.quantity != Quantity.Length) {
				return false;
			} else {
				lastKnownLocation = new SimpleLocation(true, altitude.ConvertTo(Units.Length.METER).amount, double.NaN, double.NaN);
				return true;
			}
		}

		// Implemented from ILocationManager
		public Task<Address> GetAddressFromLocationAsync(ILocation location) {
			return null;
		}
	}
}
