namespace ION.Core.Sensors.Filters {

	using Appion.Commons.Util;

  public class SensorOfTypeFilter : IFilter<Sensor> {
    /// <summary>
    /// The sensor type to match to.
    /// </summary>
    private ESensorType sensorType;

    public SensorOfTypeFilter(ESensorType sensorType) {
      this.sensorType = sensorType;
    }

    // Overridden from IFilter
    public bool Matches(Sensor sensor) {
      return sensorType == sensor.type;
    }
  }
}

