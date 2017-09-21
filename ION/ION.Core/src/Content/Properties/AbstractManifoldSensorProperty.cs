namespace ION.Core.Content.Properties {

	using Appion.Commons.Measure;

	using ION.Core.Location;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

  public class AbstractManifoldSensorProperty : ISensorProperty {

    // Overridden from ISensorProperty
    public event OnSensorPropertyChanged onSensorPropertyChanged;

    // Overridden from ISensorProperty
    public Sensor sensor {
      get {
				//return manifold.primarySensor;
				return __sensor;
      } 
      set {
        __sensor = value;
      }
    } Sensor __sensor;

    // Overridden from ISensorProperty
    public virtual Scalar modifiedMeasurement {
      get {
        return sensor.measurement;
      }
      protected set {
        // Nope
      }
    }

    // Overridden from ISensorProperty
    public virtual bool supportedReset {
      get {
        return false;
      }
    }

		/// <summary>
		/// The manifold that this property applies to.
		/// </summary>
		/// <value>The manifold.</value>
		//public Manifold manifold { get; private set; }

		//public AbstractManifoldSensorProperty(Manifold manifold){
		public AbstractManifoldSensorProperty(Sensor sensor) {
      this.sensor = sensor;
      //manifold.onManifoldEvent += OnManifoldEvent;
      App.AppState.context.locationManager.onLocationChanged += OnLocationChanged;
    }

    // Overridden from ISensorProperty
    public virtual void Dispose() {
      //manifold.onManifoldEvent -= OnManifoldEvent;

      App.AppState.context.locationManager.onLocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
      NotifyChanged();
    }

    // Overridden from ISensorProperty
    public virtual void Reset() {
      modifiedMeasurement = sensor.measurement;
    }

    /// <summary>
    /// Notifies the properties onSensorPropertyChanged event that the property
    /// has changed.
    /// </summary>
    protected void NotifyChanged() {
      if (onSensorPropertyChanged != null) {
        onSensorPropertyChanged(this);
      }
    }

    /// <summary>
    /// The delegate that is called when the property's manifold changes.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldEvent(ManifoldEvent me) {
      NotifyChanged();
    }
  }
}

