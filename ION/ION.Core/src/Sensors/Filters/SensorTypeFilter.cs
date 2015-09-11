using System;

using ION.Core.Util;

namespace ION.Core.Sensors {
  public class SensorTypeFilter : IFilter<Sensor> {
    /// <summary>
    /// The sensor type to match to.
    /// </summary>
    private ESensorType sensorType;

    public SensorTypeFilter(ESensorType sensorType) {
      this.sensorType = sensorType;
    }

    // Overridden from IFilter
    public bool Matches(Sensor sensor) {
      return sensorType == sensor.type;
    }
  }
}

