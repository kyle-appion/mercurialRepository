#if true
namespace ION.IOS.Connections {

  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using CoreFoundation;
  using Foundation;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.IO;
  using ION.Core.Util;

  using ION.IOS.App;

  /// <summary>
  /// The connection helper that will perform the IOS bluetooth interactions.
  /// </summary>
  public class LeConnectionHelper : CBCentralManagerDelegate, IConnectionHelper {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    public event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    public event OnDeviceFound onDeviceFound;

		/// <summary>
		/// The event that is called when a peripheral is disconnected.
		/// </summary>
		public event EventHandler<CBPeripheral> onPeripheralDisconnected;

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
    public CBCentralManager centralManager { get; private set; }
    /// <summary>
    /// The object that will cancel a scan in progress.
    /// </summary>
    private CancellationTokenSource cancelSource;

    public LeConnectionHelper() {
      var cboptions = new CBCentralInitOptions();
      cboptions.ShowPowerAlert = false;
			centralManager = new CBCentralManager(this, new DispatchQueue("ION iOS Bluetooth", true), cboptions);
    }

    public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
    }

    public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
    }

    public override async void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
      Log.D(this, "Discovered Peripheral: " + peripheral.Name);

      var name = peripheral.Name;
      var adData = advertisementData;

      if (name == null) {
        Log.E(this, "No name was provided for peripheral {" + peripheral.Identifier + "}. Attempting to pull from scan record.");
        if (adData != null) {
          var data = adData[CBAdvertisement.DataLocalNameKey] as NSString;
          if (data != null) {
            name = data.ToString();
          }
        }
      }

//      if (name == null) {
//        // Try connecting to the peripheral as a last resort to get the peripheral stuff.
//        Log.E(this, "Failed to get peripheral name from advertisement record. Trying to connect to peripheral instead.");
//        var connection = new IosLeConnection(this, peripheral);
//        name = await connection.PullDeviceName();
        if (name == null) {
          Log.E(this, "Failed to resolve peripheral name. The peripheral will not be presented to the application.");
          return;
        }
//      }

      if (!SerialNumberExtensions.IsValidSerialNumber(name)) {
        Log.D(this, name + " is not a valid serial number");
        return;
      }

      try {
        var serialNumber = SerialNumberExtensions.ParseSerialNumber(name);
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
          if (serialNumber.rawSerial.Length == 8) {
            // TODO ahodder@appioninc.com: Finish 
            //            protocol = EProtocolVersion.Rigado;
          } else if (EProtocolVersion.V1 == rawProtocol || EProtocolVersion.V2 == rawProtocol || EProtocolVersion.V3 == rawProtocol) {
            protocol = rawProtocol;
          } else {
            protocol = EProtocolVersion.V1;
					}
        }

				if (serialNumber.rawSerial.StartsWith("S")) {
					protocol = EProtocolVersion.V4;
				}

        NotifyDeviceFound(serialNumber, peripheral.Identifier.AsString(), broadcastPacket, protocol);
      } catch (Exception ex) {
        Log.E(this, "Failed to resolve newly found peripheral: " + name, ex);
      }
    }

    public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
    }

    public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
    }

    public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
    }

    public override async void UpdatedState(CBCentralManager central) {
      var ion = AppState.context;
			if (ion != null) {
	      if (!ion.deviceManager.isInitialized) {
	        var result = await ion.deviceManager.InitAsync();
	        if (result.success) {
	          var wb = await ion.LoadWorkbenchAsync(ion.fileManager.GetApplicationInternalDirectory().GetFile(IosION.FILE_WORKBENCH));
	          if (wb != null) {
	            ion.currentWorkbench = wb;
	          }
	        } else {
	          Log.E(this, "Failed to reinitialized the device manager on bluetooth state change: " + result.errorMessage);
	        }
	      }
	      NotifyScanStateChanged();
			}
    }

    public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
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
     Log.D(this, serialNumber + ": {" + broadcastPacket.AsString() + "}");
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, broadcastPacket, protocol);
      }
    }
  }
}



#else
namespace ION.IOS.Connections {

  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using CoreFoundation;
  using Foundation;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.IO;
  using ION.Core.Util;

  using ION.IOS.App;

  /// <summary>
  /// The connection helper that will perform the IOS bluetooth interactions.
  /// </summary>
  public class LeConnectionHelper : CBCentralManagerDelegate, IConnectionHelper {
		/// <summary>
		/// The company code that identifies appion bluetooth devices.
		/// </summary>
		private const int APPION_COMPANY_CODE = 0xffff;
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    public event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    public event OnDeviceFound onDeviceFound;

		/// <summary>
		/// The event that is called when a peripheral is disconnected.
		/// </summary>
		public event EventHandler<CBPeripheral> onPeripheralDisconnected;

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
    public CBCentralManager centralManager { get; private set; }
    /// <summary>
    /// The object that will cancel a scan in progress.
    /// </summary>
    private CancellationTokenSource cancelSource;

    public LeConnectionHelper() {
      var cboptions = new CBCentralInitOptions();
      cboptions.ShowPowerAlert = false;
			centralManager = new CBCentralManager(this, new DispatchQueue("ION iOS Bluetooth", true), cboptions);
    }

    public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
    }

    public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
    }

    public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
			var name = peripheral.Name;

			var blacklist = new string[] { 
				"Square Reader",
				"Apple",
				"RigDfu",
				"iPhone",
			};

			foreach (var s in blacklist) {
				if (name != null && name.StartsWith(s)) {
					return;
				}
			}

      Log.D(this, "Discovered Peripheral: " + peripheral.Name);

			try {
				var adData = ((NSData)advertisementData[CBAdvertisement.DataManufacturerDataKey])?.ToArray();
				byte[] broadcastPacket = null;
				if (adData != null) {
					FindBroadcastPacket(adData, out broadcastPacket);
					if (broadcastPacket != null) {
						Log.D(this, broadcastPacket.ToByteString());
					}
				}

		    if (name == null) {
					Log.D(this, "Found a device with serial: " + name + " from broadcast packet: " + broadcastPacket?.ToByteString());
		      if (advertisementData != null) {
		        var data = advertisementData[CBAdvertisement.DataLocalNameKey] as NSString;
		        if (data != null) {
		          name = data.ToString();
		        }
					}
		    }

		    if (name == null) {
					if (broadcastPacket != null) {
						name = System.Text.Encoding.UTF8.GetString(broadcastPacket, 4, 8);
						Log.D(this, "Found a ridgado device with serial: " + name);
						Array.Copy(broadcastPacket, 12, broadcastPacket, 0, 19);
					}
/*
					if (name == null) {
		        // Try connecting to the peripheral as a last resort to get the peripheral stuff.
		        Log.E(this, "Failed to get peripheral name from advertisement record. Trying to connect to peripheral instead.");
		        var connection = new IosLeConnection(this, peripheral);
		        name = await connection.PullDeviceName();
		        if (name == null) {
		          Log.E(this, "Failed to resolve peripheral name. The peripheral will not be presented to the application.");
		          return;
		        }
					}
*/
		    }

		    if (!SerialNumberExtensions.IsValidSerialNumber(name)) {
		      Log.D(this, name + " is not a valid serial number");
		      return;
		    }

        var serialNumber = SerialNumberExtensions.ParseSerialNumber(name);
        var protocol = EProtocolVersion.V1;
        if (broadcastPacket != null) {
          // Note: This will NOT be correct for the early v4 le gauges as they did not support broadcasting.
          var rawProtocol = (EProtocolVersion)broadcastPacket[0];
          if (serialNumber.rawSerial.Length == 8) {
            // TODO ahodder@appioninc.com: Finish 
            //            protocol = EProtocolVersion.Rigado;
          } else if (EProtocolVersion.V1 == rawProtocol || EProtocolVersion.V2 == rawProtocol || EProtocolVersion.V3 == rawProtocol) {
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

    public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
    }

    public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
    }

    public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
    }

    public override async void UpdatedState(CBCentralManager central) {
      var ion = AppState.context;
			if (ion != null) {
	      if (!ion.deviceManager.isInitialized) {
	        var result = await ion.deviceManager.InitAsync();
	        if (result.success) {
	          var wb = await ion.LoadWorkbenchAsync(ion.fileManager.GetApplicationInternalDirectory().GetFile(IosION.FILE_WORKBENCH));
	          if (wb != null) {
	            ion.currentWorkbench = wb;
	          }
	        } else {
	          Log.E(this, "Failed to reinitialized the device manager on bluetooth state change: " + result.errorMessage);
	        }
	      }
	      NotifyScanStateChanged();
			}
    }

    public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
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
      options.AllowDuplicatesKey = true;
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
		/// Finds the broadcast packet that is nested in an advertisement packet.
		/// </summary>
		/// <returns>The broadcast packet.</returns>
		/// <param name="advertisement">Advertisement.</param>
		/// <param name="broadcastPacket">Broadcast packet.</param>
		private bool FindBroadcastPacket(byte[] advertisement, out byte[] broadcastPacket) {
			var i = 0;
			while (i < advertisement.Length) {
				var len = advertisement[i++];
				var type = advertisement[i++];
				if (type == 0xff) {
					var companyCode = (int)((advertisement[i] << 8) | advertisement[i + 1]);
					if (companyCode == APPION_COMPANY_CODE) {
						var chars = System.Text.Encoding.UTF8.GetString(advertisement, i + 2, 8);
						if (SerialNumberExtensions.IsValidSerialNumber(chars)) {
							broadcastPacket = new byte[19];
							Array.Copy(advertisement, i + 10, broadcastPacket, 0, 19);
							return true;
						} else {
							broadcastPacket = null;
							return false;
						}
					}
				}
				i += len - 2;
			}

			broadcastPacket = null;
			return false;
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

#endif