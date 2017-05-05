namespace ION.Droid.Connections {

  using System;

  using Android.Bluetooth.LE;

  using Appion.Commons.Util;

  using ION.Core.Devices.Protocols;

  public class Api21BleScanMethod : ScanCallback, IScanMethod {
    // Implemented for IScanMethod
    public bool isScanning { get; private set; }

    private AndroidConnectionManager manager;

    public Api21BleScanMethod(AndroidConnectionManager manager) {
      this.manager = manager;
    }

    // Implemented for IScanMethod
    public bool StartScan() {
      lock (manager.locker) {
        if (isScanning) {
          return true;
        }

        try {
          isScanning = true;
          manager.bm.Adapter.BluetoothLeScanner.StartScan(this);
          Log.D(this, "Starting Scan!");
          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to start api 21 scan.", e);
          return false;
        }
      }
    }

    // Implemented for IScanMethod
    public void StopScan() {
      Log.D(this, "Stopping Scan!");
      lock (manager.locker) {
        isScanning = false;
        manager.bm.Adapter.BluetoothLeScanner.StopScan(this);
      }
    }

    public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
    }

    public override void OnScanFailed(ScanFailure errorCode) {
      Log.D(this, "Scan failed with error code: " + errorCode);
    }

    public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
//      var scanData = result.ScanRecord.GetManufacturerSpecificData(Protocol.MANFAC_ID);
      var bytes = result.ScanRecord.GetBytes();
      manager.OnDeviceFound(result.Device, ParseBroadcastPayloadFromScanRecord(bytes));
    }

    /// <summary>
    /// Parses the broadcast payload from the scan record.
    /// </summary>
    /// <returns>The scan record for payload.</returns>
    /// <param name="scanRecord">Scan record.</param>
    private byte[] ParseBroadcastPayloadFromScanRecord(byte[] scanRecord) {
      if (scanRecord == null) {
        return null;
      }

      byte[] ret = null;

      var i = 0;

      while (i < scanRecord.Length) {
        var len = scanRecord[i];
        var type = scanRecord[i + 1];
        if (type == 0xff) {
          var companyCode = scanRecord[i + 2] << 8 | scanRecord[i + 3];
          if (companyCode == Protocol.MANFAC_ID) {
            ret = new byte[len - 2];
            Array.Copy(scanRecord, i + 4, ret, 0, ret.Length);
          }
        }
        i += len; // We already incremented for the len and type
      }

      return ret;
    }
  }
}
