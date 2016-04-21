using System;

using ION.Core.Content;
using ION.Core.Content.Properties;
using ION.Core.Fluids;
using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  public class PTChartSensorProperty : AbstractManifoldSensorProperty {

    // Overriden from AbstractSensorProperty
    public override ION.Core.Measure.Scalar modifiedMeasurement {
      get {
        // The else is asserted to be valid by the check in the constructor.
        if (sensor.type == ESensorType.Pressure) {
          return manifold.ptChart.GetTemperature(sensor.measurement, sensor.isRelative).ConvertTo(unit);
        } else {
          return manifold.ptChart.GetPressure(sensor.measurement).ConvertTo(unit);
        }
      }
    }

    /// <summary>
    /// The opposing unit that the ptsensor property will use for calculated measurements.
    /// </summary>
    public Unit unit;

    public PTChartSensorProperty(Manifold manifold) : base(manifold) {
      if (!IsSensorValid(manifold.primarySensor)) {
        throw new Exception("Cannot create PTChartSensorProperty: expected a pressure or temperature sensor");
      }

      if (manifold.secondarySensor != null) {
        unit = manifold.secondarySensor.unit;
      } else {
        switch (manifold.primarySensor.type) {
          case ESensorType.Pressure:
            unit = ION.Core.App.AppState.context.defaultUnits.DefaultUnitFor(ESensorType.Temperature);
            break;
          case ESensorType.Temperature:
            unit = ION.Core.App.AppState.context.defaultUnits.DefaultUnitFor(ESensorType.Pressure);
            break;
        }
      }
    }

    private static bool IsSensorValid(Sensor sensor) {
      return ESensorType.Pressure == sensor.type || ESensorType.Temperature == sensor.type;
    }
  }
}

