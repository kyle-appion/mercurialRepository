using System;

using ION.Core.Sensors;

namespace ION.Core.Devices {
  /// <summary>
  /// A Type of sensor that exists within a GaugeDevice.
  /// </summary>
  public class GaugeDeviceSensor : Sensor {
    /// <summary>
    /// The device that this sensor belongs to.
    /// </summary>
    /// <value>The device.</value>
    public GaugeDevice device { get; internal set; }
    /// <summary>
    /// The index that the sesnor is within the device.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }

    public GaugeDeviceSensor(GaugeDevice device, int index, ESensorType sensorType, bool relative = true)
      : base(sensorType, relative) {
      this.device = device;
      this.index = index;
    }
  }
}

