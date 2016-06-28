using System;

using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  /// <summary>
  /// A property that will retain the minimum measurement that a sensor makes.
  /// </summary>
  public class MinSensorProperty : AbstractSensorProperty {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        if (value == null) {
        } else {
          if (__modifiedMeasurement == null || value < __modifiedMeasurement) {
            __modifiedMeasurement = value;
          }
        }
        NotifyChanged();
      }
    } Scalar __modifiedMeasurement;


    public MinSensorProperty(Sensor sensor) : base(sensor) {
      // Nope
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

