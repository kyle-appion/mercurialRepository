using System;
using System.Threading;
using System.Threading.Tasks;

using ION.Core.Connections;
using ION.Core.Util;

namespace ION.Core.Devices.Connections {
  /// <summary>
  /// BaseScanMode provides a basic implementation of common scan modes.
  /// This allows children modes to simply implement that platform specific
  /// code, while allowing the BaseScanMode to handle most of the heavy
  /// lifting.
  /// </summary>
  public abstract class BaseConnectionHelper : IConnectionHelper {
    // Overridden from IScanMode
    public event OnScanStateChanged onScanStateChanged;
    // Overridden from IScanMode
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
    // Overridden from IScanMode
    public virtual bool isFinished { 
      get {
        return !isScanning && (scanOptions != null && scanOptions.repeatCount == 0);
      }
    }

    /// <summary>
    /// The scan repeats options that were set at the start of the scan.
    /// </summary>
    /// <value>The scan options.</value>
    private ScanRepeatOptions scanOptions { get; set; }

    /// <summary>
    /// The running scan task. Will be null if not scanning.
    /// </summary>
    /// <value>The scan task.</value>
    private Task scanTask { get; set; }
    /// <summary>
    /// The token that is used to cancel the scan task.
    /// </summary>
    private CancellationTokenSource cancellationToken;

    // Overridden from IScanMode
    public void Dispose() {
      Stop();
    }

    // Overridden from IScanMode
    public abstract bool Enable();

    // Overridden from IScanMode
    public abstract IConnection CreateConnectionFor(string address);

    // Overridden from IScanMode
    public bool Scan(TimeSpan scanTime, ScanRepeatOptions options) {
      lock (this) {
        if (isScanning) {
          return false;
        }
      }

      cancellationToken = new CancellationTokenSource();
      var token = cancellationToken.Token;

      scanTask = Task.Factory.StartNew(async () => {
        try {
          token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
          Log.D(this, "Start scan for " + scanTime.TotalMilliseconds + "ms");

          StartScan();

          var timer = DateTime.Now;
          while (DateTime.Now - timer < scanTime) {
            token.ThrowIfCancellationRequested();
            await Task.Delay(100);
          }
          token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
          StopScan();
          Log.D(this, "Stopping scan");

          if (options != null) {
            while (options.repeatCount == ScanRepeatOptions.REPEAT_FOREVER || --options.repeatCount > 0) {
              token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
              Log.D(this, "Continuing scan");
              timer = DateTime.Now;
              while (DateTime.Now - timer < options.restInterval) {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100);
              }
              token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
              StartScan();
              timer = DateTime.Now;
              while (DateTime.Now - timer < scanTime) {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100);
              }
              token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
              StopScan();
            }
          }
        } catch (Exception e) {
          Log.E(this, "Something broke during scanning", e);
        } finally {
          Log.D(this, "Stopping scan");
          Stop();
        }
      }, cancellationToken.Token);
      return true;
    }

    // Overridden from IScanMode
    public void Stop() {
      Log.D(this, "stop");
      lock (this) {
        if (this.isScanning) {
          cancellationToken.Cancel();
          StopScan();
        }
        scanTask = null;
      }
    }

    /// <summary>
    /// Called when the scan mode is being disposed.
    /// </summary>
    protected virtual void OnDispose() {
      // Nope
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
    protected void NotifyDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, int protocolVersion) {
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, packet, protocolVersion);
      }
    }

    private void StartScan() {
      isScanning = true;
      DoStartScan();
    }

    private void StopScan() {
      Log.D(this, "Stopping scan");
      DoStopScan();
      isScanning = false;
    }

    /// <summary>
    /// Platform code for starting a scan.
    /// </summary>
    /// <returns>The scan async.</returns>
    protected abstract void DoStartScan();
    /// <summary>
    /// Platform code for stopping a scan.
    /// </summary>
    protected abstract void DoStopScan();
  }
}

