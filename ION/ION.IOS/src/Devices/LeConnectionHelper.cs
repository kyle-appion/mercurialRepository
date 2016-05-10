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
  /// The connection helper that will perform the IOS bluetooth interactions.
  /// </summary>
  public class LeConnectionHelper : IConnectionHelper {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    public event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    public event OnDeviceFound onDeviceFound;

    /// <summary>
    /// Whether or not the connection helper's backend is enabled.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isEnabled {
      get {
        return CBCentralManagerState.PoweredOn == centralManager.State;
      }
    }
    /// <summary>
    /// Whether or not the connection helper is currently scanning.
    /// </summary>
    /// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
    public bool isScanning { 
      get {
        return __isScanning; 
      }
      set {
        __isScanning = value;
        NotifyScanStateChanged();
      }
    } bool __isScanning;

    /// <summary>
    /// The iOS bluetooth manager that will allow us to access the bluetooth module.
    /// </summary>
    /// <value>The central manager.</value>
    private CBCentralManager centralManager;
    /// <summary>
    /// The object that will cancel a scan in progress.
    /// </summary>
    private CancellationTokenSource cancelSource;

    public LeConnectionHelper(CBCentralManager centralManager) {
      this.centralManager = centralManager;
      centralManager.UpdatedState += (object sender, EventArgs e) => {
        NotifyScanStateChanged();
      };

      centralManager.DiscoveredPeripheral += OnDiscoveredPeripheral;

      centralManager.ConnectedPeripheral += (object sender, CBPeripheralEventArgs e) => {
      };

      centralManager.FailedToConnectPeripheral += (object sender, CBPeripheralErrorEventArgs e) => {
      };

      centralManager.DisconnectedPeripheral+= (object sender, CBPeripheralErrorEventArgs e) => {
      };

      centralManager.RetrievedConnectedPeripherals += (object sender, CBPeripheralsEventArgs e) => {
      };

      centralManager.WillRestoreState += (object sender, CBWillRestoreEventArgs e) => {
      };
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.IOS.Devices.LeConnectionHelper"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.IOS.Devices.LeConnectionHelper"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.IOS.Devices.LeConnectionHelper"/> in an unusable state.
    /// After calling <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.IOS.Devices.LeConnectionHelper"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.IOS.Devices.LeConnectionHelper"/> was occupying.</remarks>
    public void Dispose() {
    }

    /// <summary>
    /// Enables the connection helper's backend.
    /// </summary>
    public Task<bool> Enable() {
      return Task.FromResult(false);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ION.IOS.Devices.LeConnectionHelper"/> class.
    /// </summary>
    /// <param name="protocol">Protocol.</param>
    public bool CanResolveProtocol(EProtocolVersion protocol) {
      return EProtocolVersion.Classic != protocol;
    }

/*
    /// <summary>
    /// Initializes a new instance of the <see cref="ION.IOS.Devices.LeConnectionHelper"/> class.
    /// </summary>
    /// <param name="address">Address.</param>
    /// <param name="protocolVersion">Protocol version.</param>
    public IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion) {
      var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
      if (peripheral == null) {
        throw new Exception("Cannot create connection to " + address + ": the address is not valid");
      }

      return new IosLeConnection(centralManager, peripheral);
    }
*/

    /// <summary>
    /// Performs a scan for the given scan time. Note: the scan time is 
    /// nothing more than a hint. The connection helper does NOT necessarily need
    /// to use the exact time. This is to allow for various hardware or 
    /// system constraints that prevent explict control over scan systems.
    /// The provided options describe how many times and how frequently the
    /// scan should be refired.
    /// </summary>
    /// <param name="scanTime">Scan time.</param>
    /// <returns>True if the scan was started, false otherwise. False may
    /// be returned if the connection helper is currently scanning.</returns>
    public async Task<bool> Scan(TimeSpan scanTime) {
      if (isScanning) {
        return true;
      }

      isScanning = true;
      cancelSource = new CancellationTokenSource();
      var options = new PeripheralScanningOptions();
      options.AllowDuplicatesKey = false;
      centralManager.ScanForPeripherals((CBUUID[])null, options);

      var startTime = DateTime.Now;

      while (!cancelSource.Token.IsCancellationRequested && DateTime.Now - startTime < scanTime) {
        await Task.Delay(50);
      }

      isScanning = false;

      return true;
    }

    /// <summary>
    /// Stops a scan regardless of whether or not it is in progress. The
    /// scan will NOT repeat after this call. A call to Reset must be made
    /// to reactive the scanner.
    /// </summary>
    public void Stop() {
      if (cancelSource != null) {
        cancelSource.Cancel();
      }

      centralManager.StopScan();
    }

    /// <summary>
    /// Called when a new peripheral is found.
    /// </summary>
    private async void OnDiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e) {
      Log.D(this, "Discovered Peripheral: " + e.Peripheral.Name);

      var peripheral = e.Peripheral;
      var name = peripheral.Name;
      var adData = e.AdvertisementData;

      if (name == null) {
        Log.E(this, "No name was provided for peripheral {" + peripheral.Identifier + "}. Attempting to pull from scan record.");
        if (adData != null) {
          var data = adData[CBAdvertisement.DataLocalNameKey] as NSString;
          if (data != null) {
            name = data.ToString();
          }
        }
      }

      if (name == null) {
        // Try connecting to the peripheral as a last resort to get the peripheral stuff.
        Log.E(this, "Failed to get peripheral name from advertisement record. Trying to connect to peripheral instead.");
        var connection = new IosLeConnection(centralManager, peripheral);
        name = await connection.PullDeviceName();
        if (name == null) {
          Log.E(this, "Failed to resolce peripheral name. The peripheral will not be presented to the application.");
          return;
        }
      }

      if (!SerialNumberExtensions.IsValidSerialNumber(name)) {
        return;
      }

      try {
        var serialNumber = SerialNumberExtensions.ParseSerialNumber(name);
        Log.D(this, "Resolving: " + serialNumber);
        var ourData = adData[CBAdvertisement.DataManufacturerDataKey];
        byte[] broadcastPacket = null;
        var protocol = EProtocolVersion.V1;
        if (ourData != null) {
          broadcastPacket = ((NSData)ourData).ToArray();
          var bytes = new byte[20];
          Array.Copy(broadcastPacket, 2, bytes, 0, Math.Min(broadcastPacket.Length - 2, bytes.Length));
          broadcastPacket = bytes;
          // Note: This will NOT be correct for the early v4 le gauges as they did not support broadcasting.
          var rawProtocol = (EProtocolVersion)broadcastPacket[0];
          if (EProtocolVersion.V1 == rawProtocol || EProtocolVersion.V2 == rawProtocol || EProtocolVersion.V3 == rawProtocol) {
            protocol = rawProtocol;
          } else {
            protocol = EProtocolVersion.V1;
          }
        }

        NotifyDeviceFound(serialNumber, peripheral.Identifier.AsString(), broadcastPacket, protocol);
      } catch (Exception ex) {
        Log.E(this, "Failed to resolve newly found peripheral: " + name, ex);
      }
    }

    /// <summary>
    /// Notifies the OnScanStateChange event that the connection helper's state has changed.
    /// </summary>
    private void NotifyScanStateChanged() {
      if (onScanStateChanged != null) {
        onScanStateChanged(this);
      }
    }

    /// <summary>
    /// Notifies the OnDeviceFound event that a new device has been discovered by the connection helper.
    /// </summary>
    private void NotifyDeviceFound(ISerialNumber serialNumber, string address, byte[] broadcastPacket, EProtocolVersion protocol) {
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, broadcastPacket, protocol);
      }
    }
  }
}

