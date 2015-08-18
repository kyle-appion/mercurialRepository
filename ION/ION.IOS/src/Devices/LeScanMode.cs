using System;
using System.Threading;
using System.Threading.Tasks;

using CoreBluetooth;

using ION.Core.Devices;

namespace ION.IOS.Devices {
  /// <summary>
  /// The default scan mode for iOS.
  /// </summary>
  public class LeScanMode : BaseScanMode {

    /// <summary>
    /// The iOS bluetooth manager that will allow us to access the bluetooth
    /// module.
    /// </summary>
    /// <value>The central manager.</value>
    private CBCentralManager centralManager { get; set; }


    public LeScanMode(CBCentralManager centralManager) {
      this.centralManager = centralManager;
    }

    // Overridden from BaseScanMode
    protected override void DoStartScan() {
      var options = new PeripheralScanningOptions();
      options.AllowDuplicatesKey = false;
      centralManager.ScanForPeripherals((CBUUID[])null, options);     
    }

    // Overridden from BaseScanMode
    protected override void DoStopScan() {
      centralManager.StopScan();
    }
  }
}

