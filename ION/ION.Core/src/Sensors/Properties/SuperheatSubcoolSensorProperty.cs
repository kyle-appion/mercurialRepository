using System;

using ION.Core.Fluids;
using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  public class SuperheatSubcoolSensorProperty : AbstractSensorProperty {

    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return ptChart.CalculateSystemTemperatureDelta(pressureSensor.measurement, temperatureSensor.measurement, pressureSensor.isRelative);
      }
    }

    /// <summary>
    /// The pressure sensor.
    /// </summary>
    /// <value>The other sensor.</value>
    public Sensor pressureSensor { get; private set; }
    /// <summary>
    /// The temperature sensor.
    /// </summary>
    /// <value>The temperature sensor.</value>
    public Sensor temperatureSensor { get; private set; }
    /// <summary>
    /// The ptchart used for calculations.
    /// </summary>
    /// <value>The point chart.</value>
    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        NotifyChanged();
      }
    } PTChart __ptChart;

    public SuperheatSubcoolSensorProperty(Sensor sensor, Sensor other, PTChart ptChart) : base(sensor) {
      if (sensor.type == ESensorType.Pressure && other.type == ESensorType.Temperature) {
        pressureSensor = sensor;
        temperatureSensor = other;
      } else if (sensor.type == ESensorType.Temperature && other.type == ESensorType.Pressure) {
        pressureSensor = other;
        temperatureSensor = sensor;
      } else {
        throw new Exception("Cannot create SuperheatSubcoolSensorProperty: expected a pressure and temperature sensor");
      }

      this.ptChart = ptChart;
    }
  }
}

