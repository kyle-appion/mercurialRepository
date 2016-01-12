using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ION.Core.App;
using ION.Core.Devices.Connections;

namespace ION.Core.Devices {

  /// <summary>
  /// The delegate that is used to create devices. Note: this is an agnostic creation.
  /// The factory does not know anything about previously create devices. Because of
  /// this, the factory can create duplicate devices. It is up to the hosting type to
  /// manage collisions.
  /// </summary>
  public delegate IDevice DeviceFactoryDelegate(ISerialNumber serialNumber, string connectionIdentifier, int protocol);
  /// <summary>
  /// The delegate that is notified when the device manager's state changes.
  /// </summary>
  /// <param name="deviceManager"></param>
  public delegate void OnDeviceManagerStatesChanged(IDeviceManager deviceManager);

  /// <summary>
  /// A DeviceManager is a construct that is supposed to manage the lifecycle 
  /// and access of devices within the ION application.
  /// </summary>
  public interface IDeviceManager : IIONManager {
    /// <summary>
    /// An event handler that will be notified when a device event is thrown.
    /// </summary>
    event OnDeviceEvent onDeviceEvent;
    /// <summary>
    /// An event handler that will be notified when the device manager's
    /// state changes.
    /// </summary>
    event OnDeviceManagerStatesChanged onDeviceManagerStatesChanged;

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
    /// Queries the current states of the device manager.
    /// </summary>
    EDeviceManagerStates states { get; }

    /// <summary>
    /// The delegate that is used to create devices.
    /// </summary>
    /// <remarks>
    /// Note: the device manager will NOT register devices that are created by this factory.
    /// </remarks>
    /// <value>The device factory delegate.</value>
    DeviceFactoryDelegate deviceFactoryDelegate { get; }
    /// <summary>
    /// The scan mode that delegates out the platform specific scan procedures.
    /// </summary>
    /// <value>The scanner.</value>
    IConnectionHelper connectionHelper { get; }

    /// <summary>
    /// Requests that the device manager enable its communication backend.
    /// </summary>
    /// <returns>A task that will return true if the backend was enabled,
    /// false otherwise</returns>
//    Task<bool> EnableAsync();

    /// <summary>
    /// Forgets all devices that the device manager has found, but are not known.
    /// This is useful for clearing out clutter when the device manager view is
    /// refreshed.
    /// </summary>
    void ForgetFoundDevices();

    /// <summary>
    /// Creates a new device using the provided serial number, connection identifier
    /// and protocol. If the device cannot be created because of an invalid connection
    /// identifier, then an AgumentException will be raised. If a device already exists
    /// with the given serial number, that will be returned instead, unless the
    /// connection identifier is different. Then we will throw a massive WTF exception.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="connectionAddress">Connection address.</param>
    /// <param name="protocol">Protocol.</param>
    IDevice CreateDevice(ISerialNumber serialNumber, string connectionAddress, int protocol);

    /// <summary>
    /// Permanentely deletes the device from the device manager and any persistent backend
    /// it is using. The device will be moved to the "Found Devices" collection.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    void DeleteDevice(ISerialNumber serialNumber);

    /// <summary>
    /// Determines whether this instance is device known the specified device.
    /// </summary>
    /// <returns><c>true</c> if this instance is device known the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    bool IsDeviceKnown(IDevice device);

    /// <summary>
    /// Attempts to connect to the given device. 
    /// </summary>
    /// <param name="device"></param>
    /// <returns>A Task that will return true when the device has connected, or
    /// false if the connection attempt failed.</returns>
//    Task<bool> ConnectDeviceAsync(IDevice device);

    /// <summary>
    /// Disconnects the given device from the application.
    /// </summary>
    /// <param name="device"></param>
//    void DisconnectDevice(IDevice device);
  } // End IDeviceManager

  /// <summary>
  /// A factory type who is responsible for instantiating devices from 
  /// </summary>
  public interface IDeviceFactory {
  } // End IDeviceFactory


  /// <summary>
  /// Enumerates the possible states that a DeviceManager can be in.
  /// </summary>
  [Flags]
  public enum EDeviceManagerStates {
    /// <summary>
    /// This is a state where the device manager is uninitialized.
    /// </summary>
    None = 0,
    /// <summary>
    /// Whether or not the device manager is enabled. Enabled means that the device
    /// manager as a hot connection to the platforms communication stack.
    /// </summary>
    Enabled = 1,
    /// <summary>
    /// Whether or not the device manager is performing a scan.
    /// </summary>
    Scanning = 2,
  } // End EDeviceManagerState
}
