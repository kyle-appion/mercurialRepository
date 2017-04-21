/*

namespace ION.Core.Devices.Connections {
	using System;
  using System.Threading;
  using System.Threading.Tasks;

  using ION.Core.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;

  /// <summary>
  /// BaseScanMode provides a basic implementation of common scan modes.
  /// This allows children modes to simply implement that platform specific
  /// code, while allowing the BaseScanMode to handle most of the heavy
  /// lifting.
  /// </summary>
  public abstract class BaseConnectionHelper : IConnectionHelper {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    public event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    public event OnDeviceFound onDeviceFound;

    // Overridden from IScanMode
    public abstract bool isEnabled { get; }
    // Overridden from IScanMode
    public virtual bool isScanning {
      get {
        return __isScanning;
      }
      set {
        __isScanning = value;
        NotifyScanStateChanged();
      }
    } bool __isScanning;
    /// <summary>
    /// Whether or not the connection helper requires its own timer to handle scanning.
    /// </summary>
    protected virtual bool requiresCustomTime {
      get {
        return false;
      }
    }

    /// <summary>
    /// The token that is used to cancel the scan task.
    /// </summary>
    private CancellationTokenSource cancellationToken;

    // Overridden from IScanMode
    public void Dispose() {
      Stop();
    }

    // Overridden from IScanMode
    public async Task<bool> Scan(TimeSpan scanTime) {
      lock (this) {
        if (isScanning) {
          return false;
        } else {
          isScanning = true;
        }
      }

      cancellationToken = new CancellationTokenSource();
      var token = cancellationToken.Token;

      try {
        var task = OnScan(scanTime, token);

        var start = DateTime.Now;

        while (!(task.IsCompleted || task.IsCanceled || task.IsFaulted || token.IsCancellationRequested)) {
          if (!requiresCustomTime && (DateTime.Now - start > scanTime)) {
            cancellationToken.Cancel();
            break;
          } else {
            await Task.Delay(50);
          }
        }

        cancellationToken = null;
      } catch (Exception e) {
        Log.E(this, "Something broke during scanning", e);
      } finally {
        Stop();
      }

      return true;
    }

    // Overridden from IScanMode
    public void Stop() {
      lock (this) {
        if (isScanning) {
          if (cancellationToken != null) {
            cancellationToken.Cancel();
          }
          OnStop();
        }
        isScanning = false;
      }
    }

    /// <summary>
    /// Notifies all of the OnScanStateChanged delegates that the scan mode's state changed.
    /// </summary>
    protected void NotifyScanStateChanged() {
      if (onScanStateChanged != null) {
        onScanStateChanged(this);
      }
    }

    /// <summary>
    /// Notified all of the OnDeviceFound delegate that the scan mode found a new device.
    /// </summary>
    /// <param name="device">SerialNumber.</param>
    /// <param name="packet">Packet.</param>
    /// <param name="protocol">Protocol version.</param>
    protected void NotifyDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocolVersion) {
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, packet, protocolVersion);
      }
    }

    /// <summary>
    /// Enables the connection helper's backend.
    /// </summary>
    public abstract Task<bool> Enable();

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    /// <param name="address">Address.</param>
//    public abstract IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion);
    /// <summary>
    /// Queries whether or not the connection helper can resolve the given protocol.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="protocol">Protocol.</param>
    public abstract bool CanResolveProtocol(EProtocolVersion protocol);
    /// <summary>
    /// Platform code for starting a scan.
    /// </summary>
    /// <returns>The scan async.</returns>
    protected abstract Task OnScan(TimeSpan scanTime, CancellationToken token);
    /// <summary>
    /// Platform code for stopping a scan.
    /// </summary>
    protected abstract void OnStop();
  }
}

*/