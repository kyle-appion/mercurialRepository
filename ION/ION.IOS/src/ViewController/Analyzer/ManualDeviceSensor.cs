using System;
using ION.Core.Sensors;

namespace ION.IOS {
  public class ManualDeviceSensor : Sensor {
    
    public ManualDeviceSensor(ESensorType sensorType,bool relative = true): base(sensorType, relative, true) {
      
    }

  }
}

