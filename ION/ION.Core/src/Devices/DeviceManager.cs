namespace ION.Core.Devices {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using ION.Core.App;
	using ION.Core.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Sensors;

  public class DeviceManagerEvent {
    public EType type { get; private set; }
    /// <summary>
    /// The device manager that caused the event.
    /// </summary>
    /// <value>The device manager.</value>
    public IDeviceManager deviceManager { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    /// <value>The device event.</value>
    public DeviceEvent deviceEvent { get; set; }

    public DeviceManagerEvent(EType type, IDeviceManager deviceManager) {
      this.type = type;
      this.deviceManager = deviceManager;
      this.deviceEvent = deviceEvent;
    }

    public DeviceManagerEvent(IDeviceManager deviceManager, DeviceEvent deviceEvent) {
      this.type = EType.DeviceEvent;
      this.deviceManager = deviceManager;
      this.deviceEvent = deviceEvent;
    }

    /// <summary>
    /// The enumeration of the types of events that are thrown.
    /// </summary>
    public enum EType {
      /// <summary>
      /// The type of event indicating that the device manager started a scan.
      /// </summary>
      ScanStarted,
      /// <summary>
      /// The type of event indicating that the device manager stopped a scan.
      /// </summary>
      ScanStopped,
      /// <summary>
      /// The type of event indicating that the device manager recieved a device event.
      /// </summary>
      DeviceEvent,
    }
  }

  /// <summary>
  /// The delegate that is used to create devices. Note: this is an agnostic creation.
  /// The factory does not know anything about previously create devices. Because of
  /// this, the factory can create duplicate devices. It is up to the hosting type to
  /// manage collisions.
  /// </summary>
  public delegate IDevice DeviceFactoryDelegate(ISerialNumber serialNumber, string connectionIdentifier, EProtocolVersion protocol);
  /// <summary>
  /// The delegate that is notified when the device manager's state changes.
  /// </summary>
  /// <param name="deviceManager"></param>
  public delegate void OnDeviceManagerEvent(DeviceManagerEvent deviceManagerEvent);

  /// <summary>
  /// A DeviceManager is a construct that is supposed to manage the lifecycle 
  /// and access of devices within the ION application.
  /// </summary>
  public interface IDeviceManager : IManager {
    /// <summary>
    /// An event handler that will be notified when a device manager or device event is thrown.
    /// </summary>
    event OnDeviceManagerEvent onDeviceManagerEvent;

    /// <summary>
    /// Used to query a specific device from the device manager.
    /// <para>
    /// Note: When using this query, you may get a device that is either
    /// known or simply found.
    /// </para>
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>The found device or null if not device could be found with
    /// the given serial number.</returns>
    IDevice this[ISerialNumber serialNumber] { get; }

    /// <summary>
    /// Queries a list of all the devices within the device manager. 
    /// </summary>
    List<IDevice> devices { get; }
    /// <summary>
    /// A list of all the devices that are known by the device manager.
    /// <para>
    /// A known device is a device, that is asked, the device manager could
    /// retreive at any time (that is complicated speak for saying a device
    /// that is persistently known).
    /// </para>
    /// </summary>
    List<IDevice> knownDevices { get; }
    /// <summary>
    /// A list of all the devices that the device manager has found, but has
    /// </summary>
    List<IDevice> foundDevices { get; }

    /// <summary>
    /// The manager that is responsible for platform specific management of connections.
    /// </summary>
    /// <value>The connection factory.</value>
    IConnectionManager connectionManager { get; set; }

    DeviceFactory deviceFactory { get; }


    /// <summary>
    /// Forgets all devices that the device manager has found, but are not known.
    /// This is useful for clearing out clutter when the device manager view is
    /// refreshed.
    /// </summary>
    void ForgetFoundDevices();

    /// <summary>
    /// Creates a new device using the provided serial number, connection address and protocol.
    /// Note: this device is NOT registered, meaning it will not be found in known devices.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="connectionAddress">Connection address.</param>
    /// <param name="protocol">Protocol.</param>
    /// <param name="markAsFound">Whether or not the device manager should mark the device as found.</param>
    IDevice CreateDevice(ISerialNumber serialNumber, string connectionAddress, EProtocolVersion protocol);

    /// <summary>
    /// Queries whether or not the device manager is aware of a device with the given address.
    /// </summary>
    /// <returns><c>true</c>, if connection for address was hased, <c>false</c> otherwise.</returns>
    /// <param name="address">Address.</param>
    bool HasDeviceForAddress(string address);

    /// <summary>
    /// Queries all of the device that are of the given type.
    /// </summary>
    /// <returns>The all devices of type.</returns>
    /// <param name="deviceType">Device type.</param>
    List<IDevice> GetAllDevicesOfType(EDeviceType deviceType);
    /// <summary>
    /// Queries all the gauge device sensors of the given sensor type.
    /// </summary>
    /// <returns>The all gauge devices of type.</returns>
    /// <param name="sensorType">Sensor type.</param>
    List<GaugeDeviceSensor> GetAllGaugeDeviceSensorsOfType(ESensorType sensorType);

    /// <summary>
    /// Saves the given device to the database.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="device">Device.</param>
    Task<bool> SaveDevice(IDevice device);

    /// <summary>
    /// Permanentely deletes the device from the device manager and any persistent backend
    /// it is using. The device will be moved to the "Found Devices" collection.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    Task<bool> DeleteDevice(ISerialNumber serialNumber);

    /// <summary>
    /// Determines whether this instance is device known the specified device.
    /// </summary>
    /// <returns><c>true</c> if this instance is device known the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    bool IsDeviceKnown(IDevice device);

    /// <summary>
    /// Marks or unmarks the found state of a device.
    /// Note: a found device is a device that is clearable using ForgetFoundDevices.
    /// </summary>
    /// <param name="device">Device.</param>
    /// <param name="known">If set to <c>true</c> known.</param>
    void MarkDeviceAsFound(IDevice device, bool known);

    /// <summary>
    /// Registers the device to the known device's mapping.
    /// </summary>
    /// <param name="device">Device.</param>
    void Register(IDevice device);

    /// <summary>
    /// Unregisters the device from the device manager.
    /// </summary>
    /// <param name="device">Device.</param>
    void Unregister(IDevice device);
  } // End IDeviceManager
}
