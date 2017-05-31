namespace ION.Droid.Sensors {

  using System;

  using Android.Content;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Alarms;

  using ION.Droid.App;
  using ION.Droid.Devices;

  public static class SensorExtensions {
    /// <summary>
    /// Queries the localized user friendly string representing a sensor type.
    /// </summary>
    /// <returns>The type string.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public static string GetTypeString(this ESensorType sensorType) {
			var ion = (BaseAndroidION)AppState.context;
      switch (sensorType) {
        case ESensorType.Humidity:
          return ion.context.GetString(Resource.String.humidity);
        case ESensorType.Length:
          return ion.context.GetString(Resource.String.length);
        case ESensorType.Weight:
          return ion.context.GetString(Resource.String.weight);
        case ESensorType.Pressure:
          return ion.context.GetString(Resource.String.pressure);
        case ESensorType.Temperature:
          return ion.context.GetString(Resource.String.temperature);
        case ESensorType.Vacuum:
          return ion.context.GetString(Resource.String.vacuum);
        default:
          return ion.context.GetString(Resource.String.unknown);
      }
    }

    /// <summary>
    /// Queries the icon for the given sensor.
    /// Note: if the sensor is null or is an unknown sensor type, then we will return Resource.Drawable.ic_devices_add.
    /// </summary>
    /// <returns>The icon.</returns>
    /// <param name="sensor">Sensor.</param>
    public static int GetIcon(this Sensor sensor) {
      if (sensor is GaugeDeviceSensor) {
        var gds = sensor as GaugeDeviceSensor;
        return gds.device.GetDeviceIcon();
      } else if (sensor is ManualSensor) {
        return Resource.Drawable.ic_action_edit;
      } else {
        return Resource.Drawable.ic_devices_add;
      }
    }

		/// <summary>
		/// Queries the localized user friendly abreviation string representation of a sensor type.
		/// </summary>
		/// <returns>The type abreviation string.</returns>
		/// <param name="sensorType">Sensor type.</param>
		public static string GetTypeAbreviationString(this ESensorType sensorType) {
			var ion = (BaseAndroidION)AppState.context;
			switch (sensorType) {
				case ESensorType.Humidity:
					return ion.context.GetString(Resource.String.humidity_short);
				case ESensorType.Length:
					return ion.context.GetString(Resource.String.length_short);
				case ESensorType.Weight:
					return ion.context.GetString(Resource.String.weight_short);
				case ESensorType.Pressure:
					return ion.context.GetString(Resource.String.pressure_short);
				case ESensorType.Temperature:
					return ion.context.GetString(Resource.String.temperature_short);
				case ESensorType.Vacuum:
					return ion.context.GetString(Resource.String.vacuum_short);
				default:
					return ion.context.GetString(Resource.String.unknown);
			}
		}

    /// <summary>
    /// Converts the sensor to a sensor parcelable
    /// </summary>
    /// <returns>The parcelable.</returns>
    /// <param name="sensor">Sensor.</param>
    public static SensorParcelable ToParcelable(this Sensor sensor) {
      if (sensor is GaugeDeviceSensor) {
        return new GaugeDeviceSensorParcelable(sensor as GaugeDeviceSensor);
      } else if (sensor is ManualSensor) {
        return new ManualSensorParcelable(sensor as ManualSensor);
      } else {
        throw new Exception("Cannot get sensor parcelable for sensor: " + sensor);
      }
    }
  }
}

