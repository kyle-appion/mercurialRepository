using System;

using ION.Core.Connections;
using ION.Core.Sensors;

namespace ION.Core.Devices {

  public enum EDeviceType {
    /// <summary>
    /// A Gauge is a device [sensor] that measures physical phenomena.
    /// </summary>
    Gauge,
    /// <summary>
    /// A marker for an unknown device.
    /// </summary>
    Unknown,
  }

  /// <summary>
  /// The contract for an ION device.
  /// </summary>
  public interface IDevice {
    /// <summary>
    /// The event registery that will be notified when the device's
    /// connection state changes
    /// </summary>
    event EventHandler<EConnectionState> onStateChanged;
    /// <summary>
    /// Queries the serial number of the device.
    /// </summary>
    ISerialNumber serialNumber { get; }
    /// <summary>
    /// Queries the name of the device.
    /// </summary>
    string name { get; set; }
    /// <summary>
    /// Queries the connection responsible for communicating with the device's
    /// remote terminus.
    /// </summary>
    IConnection connection { get; }
  }

  /// <summary>
  /// A GaugeDevice is a device that contains 1 or more sensors.
  /// </summary>
  public class GaugeDevice : IDevice {
    // Overriden from IDevice
    public GaugeSerialNumber serialNumber { get; private set; }
    // Overriden from IDevice
    public string name { get; set; }
    // Override from IDevice
    public IConnection connection { get; private set; }
    /// <summary>
    /// The device manager who is managing this device.
    /// </summary>
    private IDeviceManager deviceManager { private get; private set; }
    /// <summary>
    /// The sensors that are contained within the gauge.
    /// </summary>
    public Sensor[] sensors { get; private set; }
    public Sensor this[int index] {
      get {
        return sensors[index];
      }
      private set {
        sensors[index] = value;
      }
    }

    public GaugeDevice(IDeviceManager deviceManager, IConnection connection, GaugeSerialNumber serialNumber) {
      this.deviceManager = deviceManager;
      this.connection = connection;
      this.serialNumber = serialNumber;
    }
  }
}
