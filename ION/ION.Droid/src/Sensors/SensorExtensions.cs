﻿namespace ION.Droid.Sensors {

  using System;

  using Android.Content;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Alarms;

  using ION.Droid.App;

  public static class SensorExtensions {
    /// <summary>
    /// Queries the localized user friendly string representing a sensor type.
    /// </summary>
    /// <returns>The type string.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public static string GetTypeString(this ESensorType sensorType) {
      var ion = (AndroidION)AppState.context;
      switch (sensorType) {
        case ESensorType.Humidity:
          return ion.GetString(Resource.String.humidity);
        case ESensorType.Length:
          return ion.GetString(Resource.String.length);
        case ESensorType.Mass:
          return ion.GetString(Resource.String.mass);
        case ESensorType.Pressure:
          return ion.GetString(Resource.String.pressure);
        case ESensorType.Temperature:
          return ion.GetString(Resource.String.temperature);
        case ESensorType.Vacuum:
          return ion.GetString(Resource.String.vacuum);
        default:
          return ion.GetString(Resource.String.unknown);
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

