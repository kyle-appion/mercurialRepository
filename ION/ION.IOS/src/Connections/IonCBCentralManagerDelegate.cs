namespace ION.IOS.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using CoreFoundation;
  using Foundation;

  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  public class IonCBCentralManagerDelegate : CBCentralManagerDelegate, IConnectionManager {
    internal static TimeSpan SCAN_TIME = TimeSpan.FromMilliseconds(5000);
    internal static TimeSpan DOWN_TIME = TimeSpan.FromMilliseconds(1500);

    // Implemented from IConnectionManager
    public event OnScanStateChanged onScanStateChanged;
    // Implemented from IConnectionManager
    public event OnDeviceFound onDeviceFound;

    internal event Action<CBPeripheral> onDeviceConnected;

    // Implemented for IConnectionManager
    public bool isEnabled { get { return CBCentralManagerState.PoweredOn == centralManager.State; } }
    // Implemented for IConnectionManager
    public bool isScanning {
      get {
        return __isScanning;
      }
      private set {
        __isScanning = value;
        if (onScanStateChanged != null) {
          onScanStateChanged(this);
        }
      }
    } bool __isScanning;
    // Implemented for IConnectionManager
    public bool isBroadcastScanning { get { return broadcastTask != null; } }

    /// <summary>
    /// The central manager for interacting with the bluetooth stack.
    /// </summary>
    /// <value>The central manager.</value>
    public CBCentralManager centralManager { get; private set; }

    /// <summary>
    /// The dictionary that will hold the collection of discovered connections.
    /// Note: this list may be independent of the device manager.
    /// </summary>
    private Dictionary<CBUUID, IConnection> connectionLookup = new Dictionary<CBUUID, IConnection>();
    /// <summary>
    /// The token source that will cancel the broadcast task.
    /// </summary>
    private CancellationTokenSource broadcastTokenSource;
    /// <summary>
    /// The task that references a running broadcast action.
    /// </summary>
    private Task broadcastTask;

    private IION ion;

    public IonCBCentralManagerDelegate(IION ion) {
      this.ion = ion;
      var options = new CBCentralInitOptions();
      options.ShowPowerAlert = false;

      centralManager = new CBCentralManager(this, new DispatchQueue("ION Bluetooth", true), options);
    }

    // Implemented for IConnectionManager
    public bool StartScan() {
      lock (centralManager) {
        if (isScanning) {
          return true;
        }

        try {
          var options = new PeripheralScanningOptions();
          options.AllowDuplicatesKey = true;
          centralManager.ScanForPeripherals(default(CBUUID[]), options);
          isScanning = true;
          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to start ios scan", e);
          isScanning = false;
          return false;
        }
      }
    }

    // Implemented for IConnectionManager
    public bool StartBroadcastScan() {
      lock (centralManager) {
        if (isBroadcastScanning) {
          return true;
        }

        if (isScanning) {
          StopScan();
        }

        broadcastTokenSource = new CancellationTokenSource();
        broadcastTask = StartBroadcastTask(broadcastTokenSource, SCAN_TIME, DOWN_TIME);
        return true;
      }
    }

    // Implemented for IConnectionManager
    public void StopScan() {
      lock (centralManager) {
        isScanning = false;
        centralManager.StopScan();
      }
    }

    // Implemented from IConnectionFactory
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      lock (centralManager) {
        var uuid = CBUUID.FromString(address);
        if (connectionLookup.ContainsKey(uuid)) {
          return connectionLookup[uuid];
        }

        var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
        if (peripheral == null) {
          throw new Exception("Cannot create connection to " + address + ": the address is not valid");
        }

        if (!peripheral.Name.IsValidSerialNumber()) {
          throw new Exception("Cannot create connection: peripheral does not have a valid serial number.");
        }

        var serialNumber = peripheral.Name.ParseSerialNumber();

        IConnection ret = null;

        // TODO ahodder@appioninc.com: This is awful. Make an actual parser to create real devices.
        if (serialNumber.rawSerial.StartsWith("S", StringComparison.Ordinal)) {
          ret = new IosRigadoConnection(this, peripheral);
        } else {
          ret = new IosLeConnection(this, peripheral);
        }

        connectionLookup[uuid] = ret;

        return ret;
      }
    }

    // Overridden form CBCentralManagerDelegate
    public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
      string name = null;

      var values = advertisementData.Values;
      //Log.V(this, "" + advertisementData);

      var data = advertisementData[CBAdvertisement.DataManufacturerDataKey];

      if (!AttemptNameFetch(peripheral, advertisementData, out name)) {
        //Log.E(this, "Failed to resolve peripheral name '" + name + "'. The peripheral will not be presented to the application.");
        return;
      }

      if (!name.IsValidSerialNumber()) {
        return;
      }

      IConnection connection = null;
      var uuid = CBUUID.FromNSUuid(peripheral.Identifier);
      if (connectionLookup.ContainsKey(uuid)) {
        connection = connectionLookup[uuid];
      }

      ISerialNumber sn = null;
      byte[] packet = null;

      if (RigadoBroadcastParser.ParseBroadcastPacket(ExtractBroadcastData(advertisementData), out sn, out packet)) {
        // We received a fully valid broadcast packet. We know that the gauge is an Appion gauge and should resolve it.
        if (connection == null) {
          // We have not discovered this device before. Notify the world
          NotifyDeviceFound(sn, uuid.ToString(), packet, EProtocolVersion.V4);
        } else {
          // The connection already exists. Give it a new packet.
          NotifyDeviceFound(sn, uuid.ToString(), null, EProtocolVersion.V4);
          connection.lastPacket = packet;
          connection.lastSeen = DateTime.Now;
        }
      } else {
        // We didn't receive a valid broadcasting packet, but the device may still be ours.
        if (name.IsValidSerialNumber()) {
          // See, the device has a valid serial number.
          sn = name.ParseSerialNumber();
          // Check that an iserialnumber was returned
          if(sn != null){
            if (connection == null) {
              var p = FindProtocolFromDeviceModel(sn.deviceModel);
              // We have not discovered this device before. Notify the world
              NotifyDeviceFound(sn, uuid.ToString(), null, p);
            } else {
              // The connection already exists. Update the last time that it was seen.
              var d = ion.deviceManager[sn];
              if (d != null) {
                NotifyDeviceFound(sn, uuid.ToString(), null, d.protocol.version);
                connection.lastSeen = DateTime.Now;
              }
            }
          }
        } else {
          Log.D(this, "Ignoring non-appion device: " + name);
        }
      }
    }

    // Overridden from CBCentralManagerDelegate
    public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
      var address = peripheral.Identifier.AsString();
      var uuid = CBUUID.FromString(address);
      if (connectionLookup.ContainsKey(uuid)) {
        var connection = connectionLookup[uuid];
        if (onDeviceConnected != null) {
          onDeviceConnected(peripheral);
        }
      }
    }

    // Overridden from CBCentralManagerDelegate
    public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
      lock (centralManager) {
        var address = peripheral.Identifier.AsString();
        var uuid = CBUUID.FromString(address);
        if (!connectionLookup.ContainsKey(uuid)) {
          Log.E(this, "Attempted to disconnect a peripheral that did not exist in the central manager");
        }

        if (connectionLookup.ContainsKey(uuid)) {
          connectionLookup[uuid].Disconnect(true);
        }
      }
    }

    // Overridden from CBCentralManagerDelegate
    public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
    }

    // Overridden from CBCentralManagerDelegate
    public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
    }

    // Overridden from CBCentralManagerDelegate
    public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
    }

    // Overridden from CBCentralManagerDelegate
    public override void UpdatedState(CBCentralManager central) {
    }

    public void ClearUnusedConnections() {
      connectionLookup.Clear();
      foreach (var device in ion.deviceManager.knownDevices) {
        connectionLookup[CBUUID.FromString(device.connection.address)] = device.connection;
      }
    }

    /// <summary>
    /// Toggles whether or not we are performing a scan for broadcasting.
    /// </summary>
    private Task StartBroadcastTask(CancellationTokenSource source, TimeSpan scanTime, TimeSpan downTime) {
      return Task.Factory.StartNew(async () => {
        try {
          var sleepTime = downTime;
          var lastTime = DateTime.Now;

          while (!source.Token.IsCancellationRequested) {
            if (!isScanning) {
              if (!StartScan()) {
                Log.E(this, "Failed to start ble scan for broadcasting");
              }
              await Task.Delay(scanTime, source.Token);
            } else {
              StopScan();
              await Task.Delay(downTime, source.Token);
            }
          }
        } catch (Exception e) {
          Log.E(this, "Unexpected failure in executing broadcast task.", e);
        } finally {
          StopScan();
        }
      });
    }

    /// <summary>
    /// Attempts to find the name of the peripheral using the device itself and its advertisment data.
    /// </summary>
    /// <returns><c>true</c>, if name fetch was attempted, <c>false</c> otherwise.</returns>
    /// <param name="peripheral">Peripheral.</param>
    /// <param name="adData">Ad data.</param>
    /// <param name="name">Name.</param>
    private bool AttemptNameFetch(CBPeripheral peripheral, NSDictionary adData, out string name) {
      name = peripheral.Name;

      if (name == null) {
        if (adData != null) {
          var data = adData[CBAdvertisement.DataLocalNameKey] as NSString;
          if (data != null) {
            name = data.ToString();
          }
        }
      }

      return name != null;
    }

    /// <summary>
    /// Extracts the broadcast information from the ad data.
    /// </summary>
    /// <returns>The broadcast data.</returns>
    /// <param name="adData">Ad data.</param>
    private byte[] ExtractBroadcastData(NSDictionary adData) {
      var data = adData[CBAdvertisement.DataManufacturerDataKey];
      if (data != null) {
        return ((NSData)data).ToArray();
      } else {
        return null;
      }
    }

    /// <summary>
    /// Returns the default protocol for the given version number. This is only used on the older, second, thrid and
    /// forth gen devices.
    /// </summary>
    /// <returns>The protocol from device model.</returns>
    /// <param name="dm">Dm.</param>
    private EProtocolVersion FindProtocolFromDeviceModel(EDeviceModel dm) {
      switch (dm) {
        case EDeviceModel._1XTM:
        case EDeviceModel._3XTM:
          return EProtocolVersion.V4;
        case EDeviceModel.AV760:
          return EProtocolVersion.V1;
        case EDeviceModel.HT:
          return EProtocolVersion.V4;
        case EDeviceModel.P300:
        case EDeviceModel.P500:
        case EDeviceModel.P800:
          return EProtocolVersion.V1;
        case EDeviceModel.PT300:
        case EDeviceModel.PT500:
        case EDeviceModel.PT800:
        case EDeviceModel.WL:
          return EProtocolVersion.V4;
        default:
          return EProtocolVersion.V1;
      }
    }

    /// <summary>
    /// Notifes the OnDeviceFound callback of a newly found device.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="address">Address.</param>
    /// <param name="packet">Packet.</param>
    /// <param name="version">Version.</param>
    private void NotifyDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion version) {
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, packet, version);
      }
    }
  }
}
