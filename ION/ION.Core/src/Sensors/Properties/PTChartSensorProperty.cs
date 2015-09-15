using System;

using ION.Core.Fluids;
using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  public class PTChartSensorProperty : AbstractSensorProperty {

    // Overriden from AbstractSensorProperty
    public override ION.Core.Measure.Scalar modifiedMeasurement {
      get {
        if (sensor.type == ESensorType.Pressure) {
          return ptChart.GetTemperature(sensor.measurement, sensor.isRelative);
        } else if (sensor.type == ESensorType.Temperature) {
          return ptChart.GetPressure(sensor.measurement);
        } else {
          throw new Exception("Cannot get measurement: sensor not a pressure or temperature sensor.");
        }
      }
    }

    /// <summary>
    /// The ptchart used for calculations.
    /// </summary>
    /// <value>The point chart.</value>
    private PTChart ptChart { get; set; }

    public PTChartSensorProperty(Sensor sensor, PTChart ptChart) : base(sensor) {
      if (sensor.type != ESensorType.Pressure && sensor.type == ESensorType.Temperature) {
        throw new Exception("Cannot create PTChartSensorProperty: expected a pressure o temperature sensor");
      }

      this.ptChart = ptChart;
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      base.Reset();
    }
  }
}

