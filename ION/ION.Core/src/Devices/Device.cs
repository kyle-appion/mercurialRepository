namespace ION.Core.Devices {

	using System;

	using Appion.Commons.Util;

	using ION.Core.Connections;
	using ION.Core.Devices.Protocols;
	using ION.Core.Sensors;

  public enum EDeviceType {
    /// <summary>
    /// A Gauge is a device [sensor] that measures physical phenomena.
    /// </summary>
    Gauge,
    /// <summary>
    /// A device that is used by appion internally.
    /// </summary>
    InternalInterface,
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
  /// The delegate that is used when a device fires off an event.
  /// </summary>
  /// <param name="deviceManager"></param>
  /// <param name="device"></param>
  public delegate void OnDeviceEvent(DeviceEvent deviceEvent);


  /// <summary>
  /// The event describing what the action device the did or had done to it..
  /// </summary>
  public class DeviceEvent {
    /// <summary>
    /// The type of event.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; set; }
    /// <summary>
    /// The device who caused the event.
    /// </summary>
    /// <value>The device.</value>
    public IDevice device { get; set; }

    public DeviceEvent(EType type, IDevice device) {
      this.type = type;
      this.device = device;
    }

    /// <summary>
    /// The enumeration describing the event type.
    /// </summary>
    public enum EType {
      Found,
      ConnectionChange,
      /// <summary>
      /// Note: The device is deleted by the time this event comes around.
      /// </summary>
      Deleted,
      NameChanged,
      NewData,
    }
  }

  /// <summary>
  /// The contract for an ION device.
  /// </summary>
  public interface IDevice : IDisposable, IComparable<IDevice> {
    /// <summary>
    /// The event registery that will be notified when the device's
    /// connection state changes
    /// </summary>
    event OnDeviceEvent onDeviceEvent;

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
