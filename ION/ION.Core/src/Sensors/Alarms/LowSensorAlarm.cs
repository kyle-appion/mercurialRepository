namespace ION.Core.Sensors.Alarms {

  using System;

  using ION.Core.Alarms;
  using ION.Core.Devices;
  using ION.Core.Measure;
  using ION.Core.Sensors;

  public class LowSensorAlarm : BoundedSensorAlarm {
    
    public LowSensorAlarm(string name, string description, Sensor sensor) : base(name, description, sensor) {
    }

    // Overridden from BoundedSensorAlarm
    public override bool IsTriggered() {
      var triggered = sensor.measurement < bounds;
      var gds = sensor as GaugeDeviceSensor;

      if (gds != null) {
        return gds.device.isConnected && triggered;
      } else {
        return triggered;
      }
    }
  }
}

