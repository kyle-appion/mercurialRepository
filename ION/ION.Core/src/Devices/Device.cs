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
  } // End EDeviceType

  public partial class DeviceTypeUtils {
    public static EDeviceType FromString(string name) {
      var ret = EDeviceType.Unknown;
      Enum.TryParse(name, out ret);
      return ret;
    }
  }

  /// <summary>
  /// The delegate that is notified when the device's state changes.
  /// </summary>
  /// <param name="device"></param>
  /// <param name="state"></param>
  public delegate void OnDeviceStateChanged(IDevice device);

  /// <summary>
  /// The delegate that is notified when the device's content changes
  /// (ie. readings, name etc...).
  /// </summary>
  public delegate void OnDeviceContentChanged(IDevice device);

  /// <summary>
  /// The contract for an ION device.
  /// </summary>
  public interface IDevice : IDisposable {
    /// <summary>
    /// The event registery that will be notified when the device's
    /// connection state changes
    /// </summary>
    event OnDeviceStateChanged onStateChanged;
    /// <summary>
    /// The event registery that will be notified when the device's
    /// content changes.
    /// </summary>
    event OnDeviceContentChanged onContentChanged;

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
//    bool isKnown { get; }

    /// <summary>
    /// Informs the device that is should hanldle the given packet.
    /// </summary>
    /// <param name="packet">Packet.</param>
    void HandlePacket(byte[] packet);
  }
}
