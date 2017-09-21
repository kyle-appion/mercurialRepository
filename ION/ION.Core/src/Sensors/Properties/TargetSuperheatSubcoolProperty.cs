namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Content.Properties;

	public class TargetSuperheatSubcoolProperty : AbstractManifoldSensorProperty {

		// Overridden from AbstractSensorProperty
		public override Scalar modifiedMeasurement {
			get {
				if (isValid) {
					return temperatureDelta.unit.OfScalar(temperatureDelta.magnitude);
				} else {
					return AppState.context.preferences.units.temperature.OfScalar(0);
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
				//if (ESensorType.Pressure == manifold.primarySensor.type){
				if (ESensorType.Pressure == sensor.type) {
					//return manifold.primarySensor;
					return sensor;
				} else {
					//return manifold.secondarySensor;
					return sensor.linkedSensor;
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
				//if (ESensorType.Temperature == manifold.primarySensor.type)	{
				if (ESensorType.Temperature == sensor.type) {
					//return manifold.primarySensor;
					return sensor;
				} else {
					//return manifold.secondarySensor;
					return sensor.linkedSensor;
				}
			}
		}

		public ScalarSpan temperatureDelta {
			get {
				if (isValid) {
					return AppState.context.fluidManager.lastUsedFluid.CalculateTemperatureDelta(sensor.fluidState, pressureSensor.measurement, temperatureSensor.measurement);
				} else {
					return AppState.context.preferences.units.temperature.OfSpan(0);
				}
			}
		}

		public bool isValid { get { return pressureSensor != null && temperatureSensor != null; } }

		//public TargetSuperheatSubcoolProperty(Manifold manifold) : base(manifold){
		public TargetSuperheatSubcoolProperty(Sensor sensor) : base(sensor) {
			//bool isValid = IsSensorValid(manifold.primarySensor) && (manifold.secondarySensor == null || IsSensorValid(manifold.secondarySensor) || manifold.primarySensor.type != manifold.secondarySensor.type);
			bool isValid = IsSensorValid(sensor) && (sensor.linkedSensor == null || IsSensorValid(sensor.linkedSensor) ||	sensor.type != sensor.linkedSensor.type);
			if (!isValid) {
				throw new Exception("Cannot create TargetSuperheatSubcoolProperty: expected a pressure and temperature sensor");
			}
		}


		private static bool IsSensorValid(Sensor sensor) {
			return ESensorType.Pressure == sensor.type || ESensorType.Temperature == sensor.type;
		}
	}
}

