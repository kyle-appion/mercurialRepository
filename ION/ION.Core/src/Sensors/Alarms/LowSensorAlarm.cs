using System;

using ION.Core.Alarms;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors.Alarms {
  public class LowSensorAlarm : BoundedSensorAlarm {
    
    public LowSensorAlarm(string name, string description, Sensor sensor) : base(name, description, sensor) {
    }

    // Overridden from BoundedSensorAlarm
    public override bool IsTriggered() {
      ION.Core.Util.Log.D(this, sensor.measurement + " < " + bounds + " == " + (sensor.measurement < bounds)); 
      return sensor.measurement < bounds;
    }
  }
}

