using System;
using System.Threading;
using System.Threading.Tasks;

using CoreBluetooth;
using Foundation;

using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Devices.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.Util;

using ION.IOS.Connections;

namespace ION.IOS.Devices {
  /// <summary>
  /// The default scan mode for iOS.
  /// </summary>
  public class LeConnectionHelper : BaseConnectionHelper {

    // Overridden from BaseConnectionHelper
    public override bool isEnabled {
      get {
        return centralManager.State == CBCentralManagerState.PoweredOn;
      }
    }

    /// <summary>
    /// The iOS bluetooth manager that will allow us to access the bluetooth
    /// module.
    /// </summary>
    /// <value>The central manager.</value>
    private CBCentralManager centralManager { get; set; }


    public LeConnectionHelper(CBCentralManager centralManager) {
      this.centralManager = centralManager;
      centralManager.DiscoveredPeripheral += OnDiscoveredPeripheral;
      centralManager.Init();
    }

    // Overridden from BaseConnectionHelper
    protected override void DoStartScan() {
      var options = new PeripheralScanningOptions();
      options.AllowDuplicatesKey = false;
      centralManager.ScanForPeripherals((CBUUID[])null, options);     
    }

    // Overridden from BaseConnectionHelper
    protected override void DoStopScan() {
      centralManager.StopScan();
    }

    // Overridden from BaseConnectionHelper
    protected override void OnDispose() {
      base.OnDispose();

      centralManager.DiscoveredPeripheral -= OnDiscoveredPeripheral;
    }

    // Overridden from BaseConnectionHelper
    public override bool Enable() {
      return true;
    }

    // Overridden from BaseConnectionHelper
    public override IConnection CreateConnectionFor(string address) {
      var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
      if (peripheral == null) {
        throw new ArgumentException("Cannot create connection: " + address + " is not a valid connection identifier");
      }
      return new IosLeConnection(centralManager, peripheral);
    }

    /// <summary>
    /// Called when the central manager discovers a new peripheral.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void OnDiscoveredPeripheral(object obj, CBDiscoveredPeripheralEventArgs args) {
      if (isScanning) {
        string name = args.Peripheral.Name;
        if (name == null) {
          if (args.AdvertisementData != null) {
            var data = args.AdvertisementData[CBAdvertisement.DataLocalNameKey] as NSString;
            if (data != null) {
              name = data.ToString();
            }
          }
        }

        Log.D(this, "Found device: " + name);

        try {
          if (!IsAppionDevice(args.Peripheral)) {
            return;
          }

          // TODO Make this serial number parser more abstract.
          ISerialNumber serialNumber;
          try {
            serialNumber = GaugeSerialNumber.Parse(name);
          } catch (ArgumentException) {
            Log.E(this, "Invalid GaugeSerialNumber: " + name);
            return;
          }

          var v = args.AdvertisementData[CBAdvertisement.DataManufacturerDataKey];
          byte[] scanRecord = new byte[20];
          int protocol = 1; // Default to oldest BLE protocol
          if (v != null) {
            scanRecord = ((NSData)v).ToArray();
            var bytes = new byte[20];
            Array.Copy(scanRecord, 2, bytes, 0, Math.Min(scanRecord.Length - 2, bytes.Length));
            scanRecord = bytes;
            protocol = scanRecord[0];
          }
          Log.D(this, name + " scanRecord after: " + String.Join(", ", scanRecord));

          NotifyDeviceFound(serialNumber, args.Peripheral.Identifier.AsString(), scanRecord, protocol);
        } catch (Exception e) {
          Log.E(this, "Failed to resolve newly found device", e);
        }

        Log.D(this, "Device discovered");
      }
    }

    /// <summary>
    /// Attempts to deduce whether or not the given bluetooth device is a valid
    /// ION device.
    /// </summary>
    /// <param name="peripheral"></param>
    /// <returns></returns>
    private bool IsAppionDevice(CBPeripheral peripheral) {
      return GaugeSerialNumber.IsValid(peripheral.Name);
    }
  }
}

 