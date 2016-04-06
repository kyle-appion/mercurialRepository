                namespace ION.IOS.Devices {

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


    public LeConnectionHelper() {
      centralManager = new CBCentralManager(connectionDelegate = new ConnectionDelegate(this), new DispatchQueue("ION Bluetooth", true));
      centralManager.Init();
    }

    // Overridden from BaseConnectionHelper
    protected async override Task OnScan(TimeSpan scanTime, CancellationToken token) {
      Log.D(this, "Scanning");
      var options = new PeripheralScanningOptions();
      options.AllowDuplicatesKey = false;
      centralManager.ScanForPeripherals(default(CBUUID[]), options);
      await Task.Delay(scanTime);
    }

    // Overridden from BaseConnectionHelper
    protected override void OnStop() {
      Log.D(this, "Stopping");
      centralManager.StopScan();
    }

    // Overridden from BaseConnectionHelper
    public override Task<bool> Enable() {
      return Task.FromResult(true);
    }

    // Overridden from BaseConnectionHelper
    public override IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion) {
      var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
      if (peripheral == null) {
        throw new ArgumentException("Cannot create connection: " + address + " is not a valid connection identifier");
      }
      var ret = new IosLeConnection(centralManager, peripheral);
      __connections[peripheral] = ret;
      return ret;
    }

    /// <summary>
    /// Queries whether or not the connection helper can resolve the given protocol.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="protocol">Protocol.</param>
    public override bool CanResolveProtocol(EProtocolVersion protocol) {
      switch (protocol) {
        case EProtocolVersion.V1:
          return true;
        case EProtocolVersion.V2:
          return true;
        case EProtocolVersion.V3:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    /// Called when the central manager discovers a new peripheral.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void OnDiscoveredPeripheral(object obj, CBDiscoveredPeripheralEventArgs args) {
      HandleDiscoveredPeripheral(args.Peripheral, args.AdvertisementData);
    }

    private async void HandleDiscoveredPeripheral(CBPeripheral peripheral, NSDictionary adData) {
      string name = peripheral.Name;
      Log.D(this, "Handling discovered peripheral: " + name);
      if (name == null) {
        Log.E(this, "No name given, attempting to pull from scan record.");
        if (adData != null) {
          var data = adData[CBAdvertisement.DataLocalNameKey] as NSString;
          if (data != null) {
            name = data.ToString();
          }
        }

        if (name == null) {
          // The ultimate last resort
          Log.E(this, "Trying really, really hard to resolve device name");
          var connection = new IosLeConnection(centralManager, peripheral);
          name = await connection.PullDeviceName();
          if (name == null) {
            Log.E(this, "Failed to resolve device name");
            return;
          }
        }
      }

      try {
        if (!IsAppionDevice(name)) {
          return;
        }

        // TODO Make this serial number parser more abstract.
        var serialNumber = GaugeSerialNumber.Parse(name);

        var v = adData[CBAdvertisement.DataManufacturerDataKey];
        byte[] scanRecord = new byte[20];
        var protocol = EProtocolVersion.V1; // Default to oldest BLE protocol
        if (v != null) {
          scanRecord = ((NSData)v).ToArray();
          var bytes = new byte[20];
          Array.Copy(scanRecord, 2, bytes, 0, Math.Min(scanRecord.Length - 2, bytes.Length));
          scanRecord = bytes;
          // TODO ahodder@appioninc.com: This is 255 for v4 of the bt gauges.
          var unsafeProtocolValue = (int)scanRecord[0];
          protocol = ResolveProtocolValue(unsafeProtocolValue);
        }

        NotifyDeviceFound(serialNumber, peripheral.Identifier.AsString(), scanRecord, protocol);
      } catch (Exception e) {
        Log.E(this, "Failed to resolve newly found device: " + name, e);
      }
    }

    /// <summary>
    /// Attempts to resolve the protocol value.
    /// </summary>
    /// <returns>The protocol value.</returns>
    /// <param name="value">Value.</param>
    private EProtocolVersion ResolveProtocolValue(int value) {
      var pv = (EProtocolVersion)value;
      if (EProtocolVersion.V1 == pv || EProtocolVersion.V2 == pv || EProtocolVersion.V3 == pv) {
        return pv;
      } else {
        return EProtocolVersion.V1;
      }
    }

    /// <summary>
    /// Attempts to deduce whether or not the given bluetooth device is a valid
    /// ION device.
    /// </summary>
    /// <param name="peripheral"></param>
    /// <returns></returns>
    private bool IsAppionDevice(string name) {
      return GaugeSerialNumber.IsValid(name);
    }

    public class ConnectionDelegate : CBCentralManagerDelegate {
      private LeConnectionHelper helper { get; set; }

      public ConnectionDelegate(LeConnectionHelper helper) {
        this.helper = helper;
      }

      public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
        Log.D(this, "disconnected peripheral");
        if (helper.__connections.ContainsKey(peripheral)) {
          var connection = helper.__connections[peripheral];
          if (connection != null) {
            connection.Disconnect();
          }
        }
      }

      public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
        Log.D(this, "Connected peripheral");
      }

      public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
        Log.E(this, "Failed to connect to peripheral");
      }

      public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
        Log.E(this, "Failed to retreive connected peripherals");
      }

      public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
        Log.D(this, "Will resote state");
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

