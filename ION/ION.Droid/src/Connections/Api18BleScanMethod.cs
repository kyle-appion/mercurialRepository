namespace ION.Droid.Connections {

  using System;

  using Android.Bluetooth;

  using Appion.Commons.Util;

  using ION.Core.Devices.Protocols;

  public class Api18BleScanMethod : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanMethod {
    // Implemented for IScanMethod
    public bool isScanning { get; private set; }

    /// <summary>
    /// The connection manager that provides the backing state for application bluetooth setup.
    /// </summary>
    private AndroidConnectionManager manager;

    public Api18BleScanMethod(AndroidConnectionManager manager) {
      this.manager = manager;
    }

    // Implemented for IScanMethod
    public bool StartScan() {
      lock (manager.locker) {
        if (isScanning) {
          return true;
        }

        try {
          isScanning = manager.bm.Adapter.StartLeScan(this);
          if (isScanning) {
            Log.D(this, "Starting Scan!");
          }
          return isScanning;
        } catch (Exception e) { 
          Log.E(this, "Failed to start scan", e);
          return false;
        }
      }
    }

    // Implemented for IScanMethod
    public void StopScan() {
      Log.D(this, "Stopping Scan!");
      lock (manager.locker) {
        isScanning = false;
        manager.bm.Adapter.StopLeScan(this);
      }
    }

    // Implemented for BluetoothAdapter.ILeScanCallback
    public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
      manager.OnDeviceFound(device, ParseBroadcastPayloadFromScanRecord(scanRecord));
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
