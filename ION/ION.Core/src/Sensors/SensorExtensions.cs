namespace ION.Core.Sensors
{

	using System;

	using Appion.Commons.Measure;


	/// <summary>
	/// Some utility extensions around the sensor type enum.
	/// </summary>
	public static class SensorExtensions
	{
		/// <summary>
		/// Queries the default unit for the given sensor type.
		/// </summary>
		/// <returns>The default unit.</returns>
		/// <param name="sensorType">Sensor type.</param>
		public static Unit GetDefaultUnit(this ESensorType sensorType)
		{
			switch (sensorType)
			{
				/*
						case ESensorType.Humidity: {
						  return DEFAULT_HUMIDITY_UNITS[0];
						}
						case ESensorType.Length: {
						  return DEFAULT_LENGTH_UNITS[0];
						}
						case ESensorType.Mass: {
						  return DEFAULT_MASS_UNITS[0];
						}
				*/
				case ESensorType.Pressure:
					return SensorUtils.DEFAULT_PRESSURE_UNITS[0];
				case ESensorType.Temperature:
					return SensorUtils.DEFAULT_TEMPERATURE_UNITS[0];
				case ESensorType.Vacuum:
					return SensorUtils.DEFAULT_VACUUM_UNITS[0];
				case ESensorType.Weight:
					return SensorUtils.DEFAULT_WEIGHT_UNITS[0];
				default:
					throw new ArgumentException("Cannot get default unit for " + sensorType);
			}
		}

		public static ESensorType AsSensorType(this Quantity quantity)
		{
			switch (quantity)
			{
				case Quantity.Pressure: return ESensorType.Pressure;
				case Quantity.Temperature: return ESensorType.Temperature;
				case Quantity.Vacuum: return ESensorType.Vacuum;
				case Quantity.Mass: return ESensorType.Weight;
				default: return ESensorType.Unknown;
			}
		}

		/// <summary>
		/// Builds a formatted string of this sensor's measurement.
		/// </summary>
		/// <remarks>
		/// The string will NOT include the sensor's unit.
		/// </remarks>
		/// <returns>The formatted string.</returns>
		/// <param name="sensor">Sensor.</param>
		public static string ToFormattedString(this Sensor sensor, bool includeUnit = false)
		{
			return SensorUtils.ToFormattedString(sensor.measurement, includeUnit);
		}
	}
}
