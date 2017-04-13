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
      Log.D(this, "Starting Scan!");
      lock (manager.locker) {
        if (isScanning) {
          return true;
        }

        try {
          isScanning = true;
          manager.bm.Adapter.BluetoothLeScanner.StartScan(this);
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
    }

    public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
      manager.OnDeviceFound(result.Device, result.ScanRecord.GetManufacturerSpecificData(Protocol.MANFAC_ID));
    }
  }
}
