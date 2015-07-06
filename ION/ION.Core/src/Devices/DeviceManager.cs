using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ION.Core.Devices {
  /// <summary>
  /// A DeviceManager is a construct that is supposed to manage the lifecycle 
  /// and access of devices within the ION application.
  /// </summary>
  public interface IDeviceManager {
    /// <summary>
    /// An event handler that will be notified when a device is found by a scan.
    /// </summary>
    event EventHandler<IDevice<ISerialNumber>> onDeviceFound;
    /// <summary>
    /// An event handler that will be notified when a device's state has
    /// changed.
    /// </summary>
    event EventHandler<IDevice<ISerialNumber>> onDeviceStateChanged;
    /// <summary>
    /// An event handler that will be notified when the device manager's
    /// state changes.
    /// </summary>
    event EventHandler<ECommState> onDeviceMangaerStateChanged;

    /// <summary>
    /// Used to query a specific device from the device manager.
    /// <para>
    /// Note: When using this query, you may get a device that is either
    /// known or unknown.
    /// </para>
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>The found device or null if not device could be found with
    /// the given serial number.</returns>
    IDevice<ISerialNumber> this[ISerialNumber serialNumber] { get; }

    /// <summary>
    /// A list of all the devices that are known by the device manager.
    /// <para>
    /// A known device is a device, that is asked, the device manager could
    /// retreive at any time (that is complicated speak for saying a device
    /// that is persistently known).
    /// </para>
    /// </summary>
    List<IDevice<ISerialNumber>> knownDevices { get; }
    /// <summary>
    /// A list of all the devices that the device manager has found, but has
    /// </summary>
    List<IDevice<ISerialNumber>> foundDevices { get; }

    /// <summary>
    /// Queries the current state of the device manager.
    /// </summary>
    ECommState state{ get; }

    /// <summary>
    /// Requests that the device manager enable its communication backend.
    /// </summary>
    /// <returns>A task that will return true if the backend was enabled,
    /// false otherwise</returns>
    Task<bool> Enable();

    /// <summary>
    /// Informs the DeviceManager that an active scan has been requested. It is
    /// up to the implementation to determine what qualifies as an active scan. 
    /// <para>
    /// If the DeviceManager is currently performing another task (ie. connecting
    /// an IConnection or enabling), then the task will wait until the preceding
    /// task has completed.
    /// </para>
    /// <para>
    /// This method should really only be called when the user explicitly
    /// requests a scan to be started.
    /// </para>
    /// </summary>
    /// <returns></returns>
    Task<bool> DoActiveScan();

    /// <summary>
    /// Terminates a currently running active scan. If the scan has not started
    /// yet, it will be prevented from running. If a scan is not currently running,
    /// we will return quietly.
    /// </summary>
    Task StopActiveScan();

    /// <summary>
    /// Informs the DeviceManager that a passive scan has been requested. A
    /// passive scan is a scan that is intended to be executed in the background
    /// without explicit user awareness.
    /// <para>
    /// Note: a passive scan is a scan that will cede precedence to an active
    /// scan. If an active scan is requested and the DeviceManager is currently
    /// passive scanning, then the passive scan will be terminated and the active
    /// scan will begin.
    /// </para>
    /// </summary>
    /// <returns>A task that will return true when the passive scan completes its
    /// procedure or false if the scan failed. Note: true will be returned even if
    /// the scan is interrupted by an active scan.</returns>
    Task<bool> DoPassiveScan();

    /// <summary>
    /// Terminates a currently running passive scan. If the scan has not started
    /// yet, it will be prevented from running. If a scan is not currently running,
    /// we will return quietly.
    /// </summary>
    Task StopPassiveScan();

    /// <summary>
    /// Forgets all devices that the device manager has found, but are not known.
    /// This is useful for clearing out clutter when the device manager view is
    /// refreshed.
    /// </summary>
    void ForgetFoundDevices();

    /// <summary>
    /// Attempts to connect to the given device. 
    /// </summary>
    /// <param name="device"></param>
    /// <returns>A Task that will return true when the device has connected, or
    /// false if the connection attempt failed.</returns>
    Task<bool> ConnectDevice(IDevice<ISerialNumber> device);

    /// <summary>
    /// Disconnects the given device from the application.
    /// </summary>
    /// <param name="device"></param>
    void DisconnectDevice(IDevice<ISerialNumber> device);
  }


  /// <summary>
  /// Enumerates the possible states that a DeviceManager can be in.
  /// </summary>
  public enum ECommState {
    /// <summary>
    /// The DeviceManager's communication backend is currently disabled.
    /// </summary>
    Disabled,
    /// <summary>
    /// The DeviceManager's communication backend is currently activating. At
    /// this point any calls to the device manager will be delayed until the 
    /// backend is fully enabled.
    /// </summary>
    Enabling,
    /// <summary>
    /// The DeviceManager's communication backend is enabled, and nothing is
    /// happening. Ideally, this is where the adapter spends most of its time.
    /// </summary>
    Idle,
    /// <summary>
    /// The DeviceManager is currently attempting to connect to a terminus. At
    /// this point any calls to the device manager will be delayed until the
    /// connection attempt has resolved.
    /// </summary>
    Connecting,
    /// <summary>
    /// The DeviceManager is currently performing an active scan.
    /// </summary>
    ActiveScanning,
    /// <summary>
    /// The DeviceManager is currently performing a passive scan.
    /// </summary>
    PassiveScanning,
  }
}
