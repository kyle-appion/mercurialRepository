namespace ION.Core.App {

  using System;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Measure;
  using ION.Core.Location;
  using ION.Core.Util;

  /// <summary>
  /// The entity that will manage the user's location such that we can determine his
  /// elevation for use in PT calculations.
  /// </summary>
  public class RemoteIosLocationManager : ILocationManager {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;
    /// <summary>
    /// Whether or not the location manager is enabled (in a working state).
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isEnabled {
      get {
        return false;
      }
    }
  
  
    // Overridden from ILocationManager
    public bool allowLocationTracking { 
      get {
        return false;
      }
      set {
        
      }
    }
    
    public bool isPolling { get; private set; }
    
    public bool isInitialized { get { return __isInitialized; } } bool __isInitialized;
    
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

    public RemoteIosLocationManager() {

    }
    // Overridden from ILocationManager
    public async Task<InitializationResult> InitAsync() {

      return new InitializationResult() { success = __isInitialized = true };
    }    
    public bool StartAutomaticLocationPolling(){
			return true;
		}		
    public void StopAutomaticLocationPolling(){

		}

    // Overridden frm ILocationManager
    public void Dispose() {
      
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
	}
}