namespace ION.Core.Sensors.Properties {

	using Appion.Commons.Measure;

	using ION.Core.Content;

  /// <summary>
  /// A common implementation of a sensor property that will allow for quick
  /// implementation for a sensor property and provides common utility methods.
  /// </summary>
  /// <remarks>
  /// As part of the implementation of this class, the modifiedMeasurement will
  /// automatically be "set" when the sensors measurement is changed. Thus, if
  /// you wish to react to changes within the property sensor, simply resolve
  /// them in modifiedMeasurement.set.
  /// </remarks>
  public abstract class AbstractSensorProperty : ISensorProperty {
    // Overridden from ISensorProperty
    public event OnSensorPropertyChanged onSensorPropertyChanged;

		// Overridden from ISensorProperty
		//public Manifold manifold { get; private set; }

		// Overridden from ISensorProperty
		//public Sensor sensor { get { return manifold.primarySensor; } }
		public Sensor sensor { 
      get { 
        return __sensor; 
      } 
      set {
        if(__sensor != null){
					//__sensor.onSensorStateChangedEvent -= SensorChangeEvent;
					__sensor.onSensorEvent -= SensorChangeEvent;
          __sensor = null;
        }
        __sensor = value;
				if (__sensor != null)
				{
					//__sensor.onSensorStateChangedEvent += SensorChangeEvent;
					__sensor.onSensorEvent += SensorChangeEvent;
				}
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
        return true;
      }
    }

    /// <summary>
    /// This is EXCLUSSIVELY used for serialization. I hate it too, but it c# would
    /// have offered a custom fucking [de]serialization method for a property, then
    /// we wouldn't have to deal with this.
    /// </summary>
    /// <value>The serialized sensor.</value>
    private byte[] serializedSensor { get; set; }

    /// <summary>
    /// Used by the serialization system.
    /// </summary>
    public AbstractSensorProperty() {
      // Nope
    }

		//public AbstractSensorProperty(Manifold manifold){
		public AbstractSensorProperty(Sensor sensor) {
			this.sensor = sensor;
      sensor.onSensorEvent += SensorChangeEvent;
      Reset();
    }

		public AbstractSensorProperty(Sensor sensor, Unit unit)
		{
			this.sensor = sensor;
			sensor.onSensorEvent += SensorChangeEvent;
			Reset();
		}
    // Overridden from ISensorProperty
    public virtual void Dispose() {
			//sensor.onSensorStateChangedEvent -= SensorChangeEvent;
			sensor.onSensorEvent -= SensorChangeEvent;
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
    /// Called when the sensor property changes
    /// </summary>
    protected virtual void OnSensorChanged() {
    }

		protected virtual void OnManifoldEvent(ManifoldEvent e) {
		}

		/// <summary>
		/// The callback that will set the sensor's modified measurement to the
		/// sensor's new reading.
		/// </summary>
		/// <param name="sensor">Sensor.</param>
		//protected void SensorChangeEvent(Sensor sensor)	{
		protected void SensorChangeEvent(SensorEvent sensorEvent) {
      modifiedMeasurement = sensorEvent.sensor.measurement;

      OnSensorChanged();

      NotifyChanged();
    }
  }
}

