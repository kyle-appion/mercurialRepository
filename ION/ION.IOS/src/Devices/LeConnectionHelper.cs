using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CoreBluetooth;
using CoreFoundation;
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
    /// The delegate that is necessary because xamarin (after the 9.0 update)
    /// stopped supporting a scan via the naked central manager. Instead, we
    /// need to use this delegate which makes life more difficult.
    /// </summary>
    /// <value>The connection delegate.</value>
    private ConnectionDelegate connectionDelegate { get; set; }
    /// <summary>
    /// The iOS bluetooth manager that will allow us to access the bluetooth
    /// module.
    /// </summary>
    /// <value>The central manager.</value>
    private CBCentralManager centralManager { get; set; }

    private Dictionary<CBPeripheral, IosLeConnection> __connections = new Dictionary<CBPeripheral, IosLeConnection>();


    public LeConnectionHelper(CBCentralManager centralManager) {
//      this.centralManager = centralManager;
      this.centralManager = new CBCentralManager(connectionDelegate = new ConnectionDelegate(this), new DispatchQueue("ION Bluetooth", false));
//      centralManager.DiscoveredPeripheral += OnDiscoveredPeripheral;
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
      var ret = new IosLeConnection(centralManager, peripheral);
      __connections[peripheral] = ret;
      return ret;
    }

    /// <summary>
    /// Called when the central manager discovers a new peripheral.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void OnDiscoveredPeripheral(object obj, CBDiscoveredPeripheralEventArgs args) {
      HandleDiscoveredPeripheral(args.Peripheral, args.AdvertisementData);
    }

    private void HandleDiscoveredPeripheral(CBPeripheral peripheral, NSDictionary adData) {
      Log.D(this, "OnDiscoveredPeripheral: " + peripheral?.Name);
      if (isScanning) {
        string name = peripheral.Name;
        if (name == null) {
          if (adData != null) {
            var data = adData[CBAdvertisement.DataLocalNameKey] as NSString;
            if (data != null) {
              name = data.ToString();
            }
          }
        }

        Log.D(this, "Found device: " + name);

        try {
          if (!IsAppionDevice(peripheral)) {
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

          var v = adData[CBAdvertisement.DataManufacturerDataKey];
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

          NotifyDeviceFound(serialNumber, peripheral.Identifier.AsString(), scanRecord, protocol);
        } catch (Exception e) {
          Log.E(this, "Failed to resolve newly found device", e);
        }

        Log.D(this, "Device discovered");
      }
    }

/*
    private void HandleDeviceDisconnect(CBPeripheral peripheral) {
      // TODO ahodder@appioninc.com: Do serial number validation
      var sn = GaugeSerialNumber.Parse(peripheral.Name);
      var device = this[sn];

    }
*/

    /// <summary>
    /// Attempts to deduce whether or not the given bluetooth device is a valid
    /// ION device.
    /// </summary>
    /// <param name="peripheral"></param>
    /// <returns></returns>
    private bool IsAppionDevice(CBPeripheral peripheral) {
      return GaugeSerialNumber.IsValid(peripheral.Name);
    }

    public class ConnectionDelegate : CBCentralManagerDelegate {
      private LeConnectionHelper helper { get; set; }

      public ConnectionDelegate(LeConnectionHelper helper) {
        this.helper = helper;
      }

      public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
        var connection = helper.__connections[peripheral];
        if (connection != null) {
          connection.Disconnect();
        }
      }

      public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
        Log.D(this, "Discovered Peripheral " + peripheral?.Name);
        helper.HandleDiscoveredPeripheral(peripheral, advertisementData);
      }

      public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
        Log.D(this, "Retrieved peripherals");
      }

      public override void UpdatedState(CBCentralManager central) {
        Log.D(this, "State changed: " + central.State);
      }
    }
  }
}

 