namespace ION.Droid.Location {

  using System;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Location;

  using ION.Droid.App;


  public class AndroidLocationManager : ILocationManager {
    // Overridden from ILocationManager
    public event OnLocationChanged onLocationChanged;

    // Overridden from ILocationManager;
    public ILocation lastKnownLocation {
      get {
        return __lastKnownLocation;
      }
      set {
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
    private AndroidION ion { get; set; }

    public AndroidLocationManager(AndroidION ion) {
      this.ion = ion;
    }

    // Overridden from ILocationManager
    public async Task<InitializationResult> InitAsync() {
      return new InitializationResult() { success = true };
    }

    // Overridden from ILocationManager
    public void Dispose() {
    }

    // Overridden from ILocationManager
    public bool StartAutomaticLocationPolling() {
      return false;
    }

    // Overridden from ILocationManager
    public void StopAutomaticLocationPolling() {
    }
  }
}

