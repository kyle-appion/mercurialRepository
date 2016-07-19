﻿namespace ION.Core.Sensors.Properties {

	using System;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Content.Properties;
	using ION.Core.Fluids;
	using ION.Core.Measure;
  public class SuperheatSubcoolSensorProperty : AbstractManifoldSensorProperty {

    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
				if (isValid) {
					return temperatureDelta.unit.OfScalar(temperatureDelta.magnitude);
				} else {
					return AppState.context.defaultUnits.temperature.OfScalar(0);
				}
      }
    }

    /// <summary>
    /// Queries whether or not the property has both sensors.
    /// </summary>
    /// <value><c>true</c> if is complete; otherwise, <c>false</c>.</value>
    public bool isComplete {
      get {
        return pressureSensor != null && temperatureSensor != null;
      }
    }

    /// <summary>
    /// The pressure sensor.
    /// </summary>
    /// <value>The other sensor.</value>
    public Sensor pressureSensor {
      get {
        // The else is asserted to be valid by the check in the constructor.
        if (ESensorType.Pressure == manifold.primarySensor.type) {
          return manifold.primarySensor;
        } else {
          return manifold.secondarySensor;
        }
      }
    }
    /// <summary>
    /// The temperature sensor.
    /// </summary>
    /// <value>The temperature sensor.</value>
    public Sensor temperatureSensor {
      get {
        // The else is asserted to be valid by the check in the constructor.
        if (ESensorType.Temperature == manifold.primarySensor.type) {
          return manifold.primarySensor;
        } else {
          return manifold.secondarySensor;
        }
      }
    }

		public ScalarSpan temperatureDelta {
			get {
				if (isValid) {
					return manifold.ptChart.CalculateSystemTemperatureDelta(pressureSensor.measurement, temperatureSensor.measurement, pressureSensor.isRelative);
				} else {
					return AppState.context.defaultUnits.temperature.OfSpan(0);
				}
			}
		}

		public bool isValid { get { return pressureSensor != null || temperatureSensor != null; } } 

    public SuperheatSubcoolSensorProperty(Manifold manifold) : base(manifold) {
      bool isValid = IsSensorValid(manifold.primarySensor) &&
        (manifold.secondarySensor == null || IsSensorValid(manifold.secondarySensor) ||
          manifold.primarySensor.type != manifold.secondarySensor.type);
      if (!isValid) {
        throw new Exception("Cannot create SuperheatSubcoolSensorProperty: expected a pressure and temperature sensor");
      }
    }


    private static bool IsSensorValid(Sensor sensor) {
      return ESensorType.Pressure == sensor.type || ESensorType.Temperature == sensor.type;
    }
  }
}

