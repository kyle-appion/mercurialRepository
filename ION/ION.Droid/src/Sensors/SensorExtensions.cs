namespace ION.Droid.Sensors {

  using ION.Core.App;
  using ION.Core.Sensors;

  using ION.Droid.App;

  public static class SensorExtensions {
    /// <summary>
    /// Queries the localized user friendly string representing a sensor type.
    /// </summary>
    /// <returns>The type string.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public static string GetTypeString(this ESensorType sensorType) {
      var ion = (AndroidION)AppState.context;
      var c = ion.context;

      switch (sensorType) {
        case ESensorType.Humidity:
          return c.GetString(Resource.String.measurement_humidity);
        case ESensorType.Length:
          return c.GetString(Resource.String.measurement_length);
        case ESensorType.Mass:
          return c.GetString(Resource.String.measurement_mass);
        case ESensorType.Pressure:
          return c.GetString(Resource.String.measurement_pressure);
        case ESensorType.Temperature:
          return c.GetString(Resource.String.measurement_temperature);
        default:
          return c.GetString(Resource.String.measurement_unknown);
      }
    }
  }
}

