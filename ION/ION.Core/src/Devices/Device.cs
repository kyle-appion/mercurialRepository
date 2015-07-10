using System;

using ION.Core.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.Sensors;
using ION.Core.Util;

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
  public delegate void OnDeviceStateChanged(IDevice device);

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
    /// Queries the device type for this device.
    /// </summary>
    EDeviceType type { get; }
    /// <summary>
    /// Queries the name of the device.
    /// </summary>
    string name { get; set; }
    /// <summary>
    /// The current battery level (0-100] for the device.
    /// </summary>
    /// <value>The battery.</value>
    int battery { get; }
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
    /// <summary>
    /// Queries whether or not the device is connected.
    /// </summary>
    bool isConnected { get; }
    /// <summary>
    /// Queries whether or not the device is considered nearby.
    /// </summary>
    bool isNearby { get; }
    /// <summary>
    /// Queries whether or not the device is known by it device manager.
    /// </summary>
    bool isKnown { get; }
  }

  /// <summary>
  /// A GaugeDevice is a device that contains 1 or more sensors.
  /// </summary>
  public class GaugeDevice : IDevice {
    /// <summary>
    /// The timespan that a device should have communicated within to be
    /// considered nearby.
    /// </summary>
    private static readonly TimeSpan TIMEOUT_NEARBY = TimeSpan.FromMilliseconds(5000);

    // Overridden from IDevice
    public event OnDeviceStateChanged onStateChanged;
    // Overridden from IDevice
    public ISerialNumber serialNumber { get { return __serialNumber; } } GaugeSerialNumber __serialNumber;
    // Overridden from IDevice
    public EDeviceType type { get { return __serialNumber.deviceType; } }
    // Overridden from IDevice
    public string name {
      get {
        return __name;
      }
      set {
        __name = value;
        NotifyOfStateChange();
      }
    } string __name;
    // Overridden from IDevice
    public int battery { get; private set; }
    // Overridden from IDevice
    public IConnection connection { get; private set; }
    // Overridden from IDevice
    public IProtocol protocol { get { return __protocol; } } IGaugeProtocol __protocol;
    // Overridden from IDevice
    public bool isConnected { get { return EConnectionState.Connected == connection.connectionState; } }
    // Overridden from IDevice
    public bool isNearby {
      get {
        return DateTime.Now - connection.timeLastPacketReceived <= TIMEOUT_NEARBY;
      }
    }
    // Overridden from IDevice
    public bool isKnown { get { return deviceManager.knownDevices.Contains(this); } }

    /// <summary>
    /// The device manager who is managing this device.
    /// </summary>
    private IDeviceManager deviceManager { get; set; }
    /// <summary>
    /// The sensors that are contained within the gauge.
    /// </summary>
    public Sensor[] sensors { get; private set; }
    /// <summary>
    /// Queries the number of sensors that are present in the gauge.
    /// </summary>
    public int sensorCount { get { return sensors.Length; } }
    /// <summary>
    /// Queries the sensor at the given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Sensor this[int index] {
      get {
        return sensors[index];
      }
      private set {
        sensors[index] = value;
      }
    }

    public GaugeDevice(IDeviceManager deviceManager, GaugeSerialNumber serialNumber, IConnection connection, IGaugeProtocol protocol, Sensor[] sensors) {
      this.deviceManager = deviceManager;
      __serialNumber = serialNumber;
      this.connection = connection;
      __protocol = protocol;
      this.sensors = sensors;
      name = serialNumber.ToString();

      connection.onStateChanged += ((IConnection conn, EConnectionState state) => {
        NotifyOfStateChange();
      });

      connection.onDataReceived += ((IConnection conn, byte[] packet) => {
        try {
          GaugePacket gp = protocol.ParsePacket(packet);

          if (sensorCount == gp.gaugeReadings.Length) {
            battery = gp.battery;

            for (int i = 0; i < sensorCount; i++) {
              GaugeReading reading = gp.gaugeReadings[i];
              if (reading.sensorType != this[i].sensorType) {
                throw new ArgumentException("Cannot set device sensor measurement: Sensor at " + i + " of type " + 
                  this[i].sensorType + " is not valid with received type " + reading.sensorType);
              }
              if (this[i].measurement != gp.gaugeReadings[i].reading) {
                this[i].TryForceMeasurementSet(gp.gaugeReadings[i].reading);
              }
            }

            NotifyOfStateChange();
          } else {
            throw new ArgumentException("Failed to resolve packet: Expected " + sensorCount + " sensor data input, received: " + gp.gaugeReadings.Length);
          }
        } catch (Exception e) {
          Log.E(this, "Cannot resolve packet: unresolved exception {packet=> " + packet.ToByteString() + "}", e);
        }
      });
    }

    /// <summary>
    /// Notifies the device's onStateChange delegates that it has changed.
    /// </summary>
    private void NotifyOfStateChange() {
      if (onStateChanged != null) {
        onStateChanged(this);
      }
    }
  }
}
