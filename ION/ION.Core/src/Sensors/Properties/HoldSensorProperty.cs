using System;

using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  public class HoldSensorProperty : AbstractSensorProperty {

    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        // Nope
      }
    } Scalar __modifiedMeasurement;

    public HoldSensorProperty(Sensor sensor) : base(sensor) {
      // Nope
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

