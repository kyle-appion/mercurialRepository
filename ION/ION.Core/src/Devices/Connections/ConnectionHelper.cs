namespace ION.Core.Devices.Connections {

  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using ION.Core.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;

  /// <summary>
  /// The delegate that is called when the connection helper's scan state changes.
  /// </summary>
  public delegate void OnScanStateChanged(IConnectionHelper connectionHelper);
  /// <summary>
  /// The delegate that is called when the connection helper finds a device.
  /// </summary>
  /// <param name="connectionHelper">The connection helper that found the device.</param>
  /// <param name="serialNumber">The serial number of device that was found.</param>
  /// <param name="packet">The packet that we provided with the scan event. May be null.</param>
  public delegate void OnDeviceFound(IConnectionHelper connectionHelper, ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocolVersion);

  /// <summary>
  /// ConnectionHelper is an interface that is used to abstract away various
  /// platform specific scan and backend connection management
  /// methods that a device manager may use. For example, the device manager
  /// may simply with to perform a single long scan, or it may wish to
  /// perform quick sequential scans to resolve broadcasting.
  /// </summary>
  public interface IConnectionHelper : IDisposable {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    event OnDeviceFound onDeviceFound;

    /// <summary>
    /// Whether or not the connection helper's backend is enabled.
    /// </summary>
    /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
    bool isEnabled { get; }
    /// <summary>
    /// Whether or not the connection helper is currently scanning.
    /// </summary>
    bool isScanning { get; }

    /// <summary>
    /// Enables the connection helper's backend.
    /// </summary>
    Task<bool> Enable();
    /// <summary>
    /// Queries whether or not the connection helper can resolve the given protocol.
    /// </summary>
    /// <returns><c>true</c> if this instance can resole protocol the specified protocol; otherwise, <c>false</c>.</returns>
    /// <param name="protocol">Protocol.</param>
    bool CanResolveProtocol(EProtocolVersion protocol);
    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion);
    /// <summary>
    /// Performs a scan for the given scan time. Note: the scan time is 
    /// nothing more than a hint. The connection helper does NOT necessarily need
    /// to use the exact time. This is to allow for various hardware or 
    /// system constraints that prevent explict control over scan systems.
    /// The provided options describe how many times and how frequently the
    /// scan should be refired.
    /// </summary>
    /// <param name="scanTime">Scan time.</param>
    /// <returns>True if the scan was started, false otherwise. False may
    /// be returned if the connection helper is currently scanning.</returns>
    Task<bool> Scan(TimeSpan scanTime);
    /// <summary>
    /// Stops a scan regardless of whether or not it is in progress. The
    /// scan will NOT repeat after this call. A call to Reset must be made
    /// to reactive the scanner.
    /// </summary>
    void Stop();
  } // End IScanMode
}

