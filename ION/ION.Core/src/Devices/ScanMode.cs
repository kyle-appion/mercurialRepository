using System;
using System.Threading;
using System.Threading.Tasks;

using ION.Core.Util;

namespace ION.Core.Devices {
  /// <summary>
  /// The delegate that is called when the scan mode's scan state changes.
  /// </summary>
  public delegate void OnScanStateChanged(IScanMode scanMode, bool scanning);

  /// <summary>
  /// ScanMode is an interface that is used to abstract away various scan
  /// methods that a device manager may use. For example, the device manager
  /// may simply with to perform a single long scan, or it may wish to
  /// perform quick sequential scans to resolve broadcasting.
  /// </summary>
  public interface IScanMode : IDisposable {
    /// <summary>
    /// The event pool that is notified when the scan mode state changes.
    /// </summary>
    event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// Whether or not the scan mode is currently scanning.
    /// </summary>
    bool isScanning { get; }
    /// <summary>
    /// Queries whether or not the scan is finished (the scan mode has no
    /// pending repeats).
    /// </summary>
    /// <value><c>true</c> if is finished; otherwise, <c>false</c>.</value>
    bool isFinished { get; }

    /// <summary>
    /// Performs a scan for the given scan time. Note: the scan time is 
    /// nothing more than a hint. The scan mode does NOT necessarily need
    /// to use the exact time. This is to allow for various hardware or 
    /// system constraints that prevent explict control over scan systems.
    /// The provided options describe how many times and how frequently the
    /// scan should be refired.
    /// </summary>
    /// <param name="scanTime">Scan time.</param>
    /// <param name="options">The repeat options for the scan mode. Maybe
    /// null is a scan repeat is not desired.</param>
    /// <returns>True if the scan was started, false otherwise. False may
    /// be returned if the scan mode is currently scanning.</returns>
    bool Scan(TimeSpan scanTime, ScanRepeatOptions options=null);
    /// <summary>
    /// Stops a scan regardless of whether or not it is in progress. The
    /// scan will NOT repeat after this call. A call to Reset must be made
    /// to reactive the scanner.
    /// </summary>
    void Stop();
  } // End IScanMode

  /// <summary>
  /// The options that used to determine how the scan mode will resolve
  /// repeats.
  /// </summary>
  public class ScanRepeatOptions {
    public const int REPEAT_FOREVER = -1;
    /// <summary>
    /// The number of times that the scan will repeat after a completed
    /// scan has resolved. When this reaches 0, the scan will is considered
    /// complete and will not longer repeat. If repeat count is equal to
    /// REPEAT_FOREVER, then the scan will repeat indefinitely (until
    /// explicitly stopped).
    /// </summary>
    public int repeatCount;
    /// <summary>
    /// The time that will span repeated scans.
    /// </summary>
    public TimeSpan restInterval;

    public ScanRepeatOptions(int repeatCount, TimeSpan restInterval) {
      this.repeatCount = repeatCount;
      this.restInterval = restInterval;
    }
  } // End RepeatOptions

  /// <summary>
  /// BaseScanMode provides a basic implementation of common scan modes.
  /// This allows children modes to simply implement that platform specific
  /// code, while allowing the BaseScanMode to handle most of the heavy
  /// lifting.
  /// </summary>
  public abstract class BaseScanMode : IScanMode {
    // Overridden from IScanMode
    public event OnScanStateChanged onScanStateChanged;
    // Overridden from IScanMode
    public bool isScanning {
      get {
        return __isScanning;
      }
      set {
        __isScanning = value;
        NotifyScanStateChanged();
      }
    } bool __isScanning;
    // Overridden from IScanMode
    public bool isFinished { 
      get {
        return !isScanning && (scanOptions != null && scanOptions.repeatCount == 0);
      }
    }

    /// <summary>
    /// The scan repeats options that were set at the start of the scan.
    /// </summary>
    /// <value>The scan options.</value>
    private ScanRepeatOptions scanOptions { get; set; }

    private CancellationTokenSource cancellationToken;

    // Overridden from IScanMode
    public void Dispose() {
      Stop();
    }

    // Overridden from IScanMode
    public bool Scan(TimeSpan scanTime, ScanRepeatOptions options) {
      lock (this) {
        if (isScanning) {
          return false;
        }

        cancellationToken = new CancellationTokenSource();
        var token = cancellationToken.Token;

        Task.Factory.StartNew(async () => {
          try {
            token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
            Log.D(this, "Start scan");

            StartScan();
            await Task.Delay(scanTime);
            token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
            StopScan();

            if (options != null) {
              while (options.repeatCount == ScanRepeatOptions.REPEAT_FOREVER || --options.repeatCount > 0) {
                token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
                Log.D(this, "Continuing scan");
                await Task.Delay(options.restInterval);
                token.ThrowIfCancellationRequested(); // Check that we aren't cancelled.
                StartScan();
                await Task.Delay(scanTime);
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
    }

    // Overridden from IScanMode
    public void Stop() {
      lock (this) {
        Log.D(this, "Stopping scan");
        if (cancellationToken != null) {
          cancellationToken.Cancel();
        }
        isScanning = false;
      }
    }

    private void StartScan() {
      isScanning = true;
      DoStartScan();
    }

    private void StopScan() {
      DoStopScan();
      isScanning = false;
    }


    private void NotifyScanStateChanged() {
      if (onScanStateChanged != null) {
        onScanStateChanged(this, isScanning);
      }
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

