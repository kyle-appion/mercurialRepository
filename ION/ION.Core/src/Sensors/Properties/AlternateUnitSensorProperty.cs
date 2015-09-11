using System;

using ION.Core.IO;
using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  /// <summary>
  /// A property that will display the senor's measurement in a different unit.
  /// </summary>
  public class AlternateUnitSensorProperty : AbstractSensorProperty {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement; 
      }
      protected set {
        __modifiedMeasurement = value.ConvertTo(unit);
      }
    } Scalar __modifiedMeasurement;

    /// <summary>
    /// The alternate unit for the property. Setting this unit will throw an 
    /// argument exception if the unit is not compatible with the sensor.
    /// </summary>
    /// <value>The unit.</value>
    public Unit unit { 
      get {
        return __unit;
      }
      set {
        if (!sensor.unit.IsCompatible(value)) {
          throw new ArgumentException("Cannot set unit: " + value + " is not compatible with " + sensor.unit);
        }

        __unit = value;
        modifiedMeasurement = sensor.measurement;
        NotifyChanged();
      }
    } Unit __unit;

    /// <summary>
    /// Used by the serialization system.
    /// </summary>
    public AlternateUnitSensorProperty() {
      // Nope
    }

    public AlternateUnitSensorProperty(Sensor sensor) : base(sensor) {
      this.unit = sensor.measurement.unit;
      this.modifiedMeasurement = sensor.measurement;
    }

    /// <summary>
    /// Creates a new alternate unit property. Note: this construct will throw an
    /// argument exception if the unit is not compatible with the sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="unit">Unit.</param>
    public AlternateUnitSensorProperty(Sensor sensor, Unit unit) : base(sensor) {
      this.unit = unit;
    }
  }
}

