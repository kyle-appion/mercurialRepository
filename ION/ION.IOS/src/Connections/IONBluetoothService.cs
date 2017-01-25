namespace ION.IOS.Connections {

	using System;
	using System.Collections.Generic;

	using CoreBluetooth;
	using CoreFoundation;
	using Foundation;

	using Appion.Commons.Util;

	using ION.Core.Connections;
	using ION.Core.Devices;
	using ION.Core.Devices.Connections;
	using ION.Core.Devices.Protocols;
	using ION.Core.Thread;

	public class IONBluetoothService : CBCentralManagerDelegate, IConnectionHelper, IConnectionFactory {
		/// <summary>
		/// The Appion manufacturing id that is used to identify appion products.
		/// </summary>
		public const int MANFAC_ID = 0x8c03;

		private const int DEFAULT_BROADCAST_SCAN_INTERVAL = 7500;
		private const int DEFAULT_BROADCAST_SCAN_DURATION = 1500;
		private const int CATEGORY_BROADCAST = 1;

		// Implemented from IConnectionHelper
		public event OnScanStateChanged onScanStateChanged;
		// Implemented from IConnectionHelper
		public event OnDeviceFound onDeviceFound;

		/// <summary>
		/// The event that is called when a peripheral is disconnected.
		/// </summary>
		public event EventHandler<CBPeripheral> onPeripheralDisconnected;

		// Implemented from IConnection Helper
		public bool isEnabled { get { return true; } }
		// Implemented from IConnectionHelper
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

		public bool isBroadcastingEnabled {
			get {
				return __broadcastingEnabled;
			}
			set {
				__broadcastingEnabled = value;
//				StartScan(TimeSpan.FromMilliseconds(DEFAULT_BROADCAST_SCAN_DURATION));
			}
		} bool __broadcastingEnabled;

		/// <summary>
		/// The iOS bluetooth manager that will allow us to access the bluetooth module.
		/// </summary>
		public CBCentralManager centralManager { get; private set; }

		/// <summary>
		/// The dictionary that is used to lookup known connections.
		/// </summary>
		private Dictionary<CBUUID, IConnection> connectionLookup = new Dictionary<CBUUID, IConnection>();
		/// <summary>
		/// The message queue that it is used to perform broadcasting callbacks.
		/// </summary>
		private MessageQueue messageQueue;

		public IONBluetoothService() {
			var options = new CBCentralInitOptions();
			options.ShowPowerAlert = false;
			centralManager = new CBCentralManager(this, new DispatchQueue("ION iOS Bluetooth", true), options);
			messageQueue = new MessageQueue();
			messageQueue.Start(System.Threading.ThreadPriority.BelowNormal);
			isBroadcastingEnabled = true;
		}

		// Implemented from IConnectionFactory
		public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
			if (connectionLookup.ContainsKey(CBUUID.FromString(address))) {
				return connectionLookup[CBUUID.FromString(address)];
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

			if (serialNumber.rawSerial.StartsWith("S", StringComparison.Ordinal)) {
				ret = new IosRigadoConnection(this, peripheral);
			} else {
				ret = new IosLeConnection(this, peripheral);
			}

			return ret;
		}

		// Implemented from IConnectionHelper
		public bool StartScan(TimeSpan scanTime) {
			Log.D(this, "StartScan called from: " + System.Threading.Thread.CurrentThread.Name);
			if (isScanning) {
				messageQueue.RemoveAllPendingActions();
				centralManager.StopScan();
			}

			Log.D(this, "Starting Scan for: " + scanTime.ToString());
			Log.D(this, "Removed " + messageQueue.RemovePendingActionsWithCategoryId(CATEGORY_BROADCAST) + " broadcast messages from the message queue");

			isScanning = true;

			var options = new PeripheralScanningOptions();
			options.AllowDuplicatesKey = true;
			centralManager.ScanForPeripherals(default(CBUUID[]), options);
			messageQueue.PostDelayed(StopScan, scanTime);

			return true;
		}

		// Implemented from IConnectionHelper
		public void StopScan() {
			Log.D(this, "StopScan called");
			if (isScanning) {
				centralManager.StopScan();
			}

			isScanning = false;

			if (isBroadcastingEnabled) {
				messageQueue.PostDelayed(() => {
					StartScan(TimeSpan.FromMilliseconds(DEFAULT_BROADCAST_SCAN_DURATION));
        }, DEFAULT_BROADCAST_SCAN_INTERVAL, CATEGORY_BROADCAST);
			}
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);

			messageQueue.Stop();
			StopScan();
			centralManager.Dispose();
		}

		public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
//			base.ConnectedPeripheral(central, peripheral);
		}

		public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
//			base.DisconnectedPeripheral(central, peripheral, error);
		}

		public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
			string name = null;

			var data = advertisementData[CBAdvertisement.DataManufacturerDataKey];
			byte[] bytes = null;
			if (data != null) {
				bytes = ((NSData)data).ToArray();
			}
//			Log.D(this, "NAME: " + name + " BYTES: " + bytes?.ToByteString());


			if (!AttemptNameFetch(peripheral, advertisementData, out name)) {
				Log.E(this, "Failed to resolve peripheral name'" + name + "'. The peripheral will not be presented to the application.");
				return;
			}

			if (!name.IsValidSerialNumber()) {
				Log.D(this, name + " is not a valid serial number. Ignoring device");
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
					NotifyOfDeviceFound(sn, uuid.ToString(), packet, EProtocolVersion.V4);
				} else {
					// The connection already exists. Give it a new packet.
					connection.lastPacket = packet;
					connection.lastSeen = DateTime.Now;
				}
			} else {
				// We didn't receive a valid broadcasting packet, but the device may still be ours.
				if (name.IsValidSerialNumber()) {
					// See, the device has a valid serial number.
					sn = name.ParseSerialNumber();
					if (connection == null) {
						var p = FindProtocolFromDeviceModel(sn.deviceModel);
						// We have not discovered this device before. Notify the world
						NotifyOfDeviceFound(sn, uuid.ToString(), null, p);
					} else {
						// The connection already exists. Update the last time that it was seen.
						connection.lastSeen = DateTime.Now;
					}
				} else {
					Log.D(this, "Ignoring non-appion device: " + name);
				}
			}
		}

		public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
		}

		public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
		}

		public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
		}

		public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
		}

		public override void UpdatedState(CBCentralManager central) {
			if (onScanStateChanged != null) {
				onScanStateChanged(this);
			}
		}

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
		/// Extracts the broadcast information form the ad data.
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

		private void NotifyOfDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion version) {
			if (onDeviceFound != null) {
				onDeviceFound(this, serialNumber, address, packet, version);
			}
		}
	}
}
