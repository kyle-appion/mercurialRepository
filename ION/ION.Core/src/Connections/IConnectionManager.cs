namespace ION.Core.Connections {

  using System;

  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  /// <summary>
  /// The delegate that is called when the connection helper's scan state changes.
  /// </summary>
  public delegate void OnScanStateChanged(IConnectionManager connectionHelper);
  /// <summary>
  /// The delegate that is called when the connection helper finds a device.
  /// </summary>
  /// <param name="connectionHelper">The connection helper that found the device.</param>
  /// <param name="serialNumber">The serial number of device that was found.</param>
  /// <param name="packet">The packet that we provided with the scan event. May be null.</param>
  public delegate void OnDeviceFound(IConnectionManager connectionManager, ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocolVersion);


  public interface IConnectionManager : IDisposable {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    event OnDeviceFound onDeviceFound;

    /// <summary>
    /// Whether or not the connection manager has it's internal systems enabled.
    /// </summary>
    /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
    bool isEnabled { get; }
    /// <summary>
    /// Whether or not the connection manager is currently scanning.
    /// </summary>
    /// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
    bool isScanning { get; }
    /// <summary>
    /// Whether or not the connection manager is broadcast scanning.
    /// </summary>
    /// <value><c>true</c> if is broadcast scanning; otherwise, <c>false</c>.</value>
    bool isBroadcastScanning { get; }

    /// <summary>
    /// Starts an asynchronous scan for new devices.
    /// </summary>
    bool StartScan();
    /// <summary>
    /// Starts an ascynhronous scan for new or far away devices. Broadcast scanning is used to periodically scan for
    /// devices that are outside of connection range but still in scan range. This allows the system to read the
    /// broadcast packet to pull gauge data.
    /// </summary>
    /// <returns><c>true</c>, if broadcast scan was started, <c>false</c> otherwise.</returns>
    bool StartBroadcastScan();
    /// <summary>
    /// Stops any running scan.
    /// </summary>
    void StopScan();

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="address">Address.</param>
    IConnection CreateConnection(string address, EProtocolVersion protocolVersion);
  }
}
