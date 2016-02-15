using System;

using ION.Core.Alarms;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors.Alarms {
  public class HighSensorAlarm : BoundedSensorAlarm {

    public HighSensorAlarm(string name, string description, Sensor sensor) : base(name, description, sensor) {
    }

    // Overridden from BoundedSensorAlarm
    public override bool IsTriggered() {
      return sensor.measurement > bounds;
    }
  }
}

