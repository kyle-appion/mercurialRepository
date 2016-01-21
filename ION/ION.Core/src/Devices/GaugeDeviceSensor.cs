using System;

using ION.Core.Measure;
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
    /// The 0-based index that the sensor is within the device.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
    /// <summary>
    /// Whether or not the gauge device sensor is removable from its host device.
    /// </summary>
    /// <value><c>true</c> if removable; otherwise, <c>false</c>.</value>
    public bool removable { get; internal set; }
    /// <summary>
    /// Whether or not the gauge device sensor is removed. A removed GaugeDeviceSensor
    /// will not have its measurement updated.
    /// </summary>
    /// <value><c>true</c> if removed; otherwise, <c>false</c>.</value>
    public bool removed { get; internal set; }

    public GaugeDeviceSensor(GaugeDevice device, int index, ESensorType sensorType, bool relative = true)
      : base(sensorType, relative, false) {
      this.device = device;
      this.index = index;
      this.name = device.serialNumber.ToString();
    }

    /// <summary>
    /// A hook that is to be called when a device receives a measurement packet.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    internal void SetMeasurement(Scalar measurement) {
      ForceSetMeasurement(measurement);
    }
  }
}

