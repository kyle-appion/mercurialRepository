namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Content;

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
        if (unit == null) {
          __modifiedMeasurement = value;
        } else {
          __modifiedMeasurement = value.ConvertTo(unit);
        }
      }
    } Scalar __modifiedMeasurement;

    // Overridden from AbstractSensorProperty
    public override bool supportedReset {
      get {
        return false;
      }
    }

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

		//[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		//public AlternateUnitSensorProperty(Sensor sensor) : base(new Manifold(sensor)){
		public AlternateUnitSensorProperty(Sensor sensor) : base(sensor) {
			this.__unit = sensor.measurement.unit;
			this.modifiedMeasurement = sensor.measurement;
		}

		//[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		public AlternateUnitSensorProperty(Sensor sensor, Unit unit) : base(sensor, unit) {
			this.__unit = unit;
			this.modifiedMeasurement = sensor.measurement;
		}

    //public AlternateUnitSensorProperty(Manifold manifold) : base(manifold) {
    //  this.__unit = sensor.measurement.unit;
    //  this.modifiedMeasurement = sensor.measurement;
    //}

    /// <summary>
    /// Creates a new alternate unit property. Note: this construct will throw an
    /// argument exception if the unit is not compatible with the sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="unit">Unit.</param>
    //public AlternateUnitSensorProperty(Manifold manifold, Unit unit) : this(manifold) {
    //  this.__unit = unit;
    //}
  }
}

