namespace ION.Core.Sensors.Properties {

	using Appion.Commons.Measure;

  /// <summary>
  /// A property that will retain the maximum measurement that a sensor makes.
  /// </summary>
  public class MaxSensorProperty : AbstractSensorProperty {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        if (value == null) {
        } else {
          if (__modifiedMeasurement == null || value > __modifiedMeasurement) {
            __modifiedMeasurement = value;
          }
        }
        NotifyChanged();
      }
    } Scalar __modifiedMeasurement;


    public MaxSensorProperty(Sensor sensor) : base(sensor) {
      // Nope
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

