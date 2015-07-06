using System;

using ION.Core.Connections;
using ION.Core.Devices.Protocols;
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
  /// The delegate that is notified when the device's state changes.
  /// </summary>
  /// <param name="device"></param>
  /// <param name="state"></param>
  public delegate void OnDeviceStateChanged(IDevice device, EConnectionState state);

  /// <summary>
  /// The contract for an ION device.
  /// </summary>
  public interface IDevice {
    /// <summary>
    /// The event registery that will be notified when the device's
    /// connection state changes
    /// </summary>
    event OnDeviceStateChanged onStateChanged;
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
    /// <summary>
    /// Queries the protocol that the device is using to format communications
    /// with the connection.
    /// </summary>
    IProtocol protocol { get; }
  }

  /// <summary>
  /// A GaugeDevice is a device that contains 1 or more sensors.
  /// </summary>
  public class GaugeDevice : IDevice {
    // Overridden from IDevice
    public event OnDeviceStateChanged onStateChanged;
    // Overridden from IDevice
    public ISerialNumber serialNumber { get { return __serialNumber; } }
    // Overridden from IDevice
    public string name { get; set; }
    // Overridden from IDevice
    public IConnection connection { get; private set; }
    // Overridden from IDevice
    public IProtocol protocol { get { return __protocol; } }
    /// <summary>
    /// The device manager who is managing this device.
    /// </summary>
    private IDeviceManager deviceManager { get; set; }
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

    private GaugeSerialNumber __serialNumber;
    private IGaugeProtocol __protocol;

    public GaugeDevice(IDeviceManager deviceManager, GaugeSerialNumber serialNumber, IConnection connection, IGaugeProtocol protocol) {
      this.deviceManager = deviceManager;
      __serialNumber = serialNumber;
      this.connection = connection;
      __protocol = protocol;

      name = serialNumber.ToString();
    }
  }
}
