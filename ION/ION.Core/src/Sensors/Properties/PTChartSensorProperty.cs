namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Content;
	using ION.Core.Content.Properties;
	using ION.Core.App;

  public class PTChartSensorProperty : AbstractManifoldSensorProperty {

    // Overriden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        // The else is asserted to be valid by the check in the constructor.
        if (sensor.type == ESensorType.Pressure) {
					//return sensor.ptChart.GetTemperature(sensor.measurement, sensor.isRelative).ConvertTo(unit);
					return AppState.context.fluidManager.lastUsedFluid.GetSaturatedTemperature(sensor.fluidState,sensor.measurement).ConvertTo(unit);
        } else {
					//return sensor.ptChart.GetRelativePressure(sensor.measurement).ConvertTo(unit);
					return AppState.context.fluidManager.lastUsedFluid.GetPressureFromSaturatedTemperature(sensor.fluidState, sensor.measurement).ConvertTo(unit);
        }
      }
    }

    /// <summary>
    /// The opposing unit that the ptsensor property will use for calculated measurements.
    /// </summary>
		public Unit unit {
			get {
				return __unit;
			}
			set {
				__unit = value;
				NotifyChanged();
			}
		} Unit __unit;

		//public PTChartSensorProperty(Manifold manifold) : base(manifold){
		public PTChartSensorProperty(Sensor sensor) : base(sensor) {
			//if (!IsSensorValid(manifold.primarySensor)){
			if (!IsSensorValid(sensor)) {
        throw new Exception("Cannot create PTChartSensorProperty: expected a pressure or temperature sensor");
      }

			//if (manifold.secondarySensor != null)	{
			if (sensor.linkedSensor != null) {
        __unit = sensor.linkedSensor.unit;
      } else {
				switch (sensor.type) {
					case ESensorType.Pressure:
            __unit = AppState.context.preferences.units.temperature;
  					break;
					case ESensorType.Temperature:
            __unit = AppState.context.preferences.units.pressure;
  					break;
				}
      }
    }

    private static bool IsSensorValid(Sensor sensor) {
      return ESensorType.Pressure == sensor.type || ESensorType.Temperature == sensor.type;
    }
  }
}

