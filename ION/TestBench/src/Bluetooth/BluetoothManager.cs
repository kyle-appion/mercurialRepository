namespace TestBench {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using CoreBluetooth;
	using CoreFoundation;
	using Foundation;

	using ION.Core.Connections;
	using ION.Core.Devices.Connections;
	using ION.Core.Devices.Protocols;
	using ION.Core.Util;

	public class BluetoothManager : CBCentralManagerDelegate, IConnectionHelper {

		public event OnScanStateChanged onScanStateChanged;

		public event OnDeviceFound onDeviceFound;

		public bool isEnabled { get { return CBCentralManagerState.PoweredOn == centralManager.State; } }

		public bool isScanning { get { return centralManager.IsScanning; } }

		/// <summary>
		/// Whether or not the scan has been requested to stop.
		/// </summary>
		private bool isScanStopRequested;


		/// <summary>
		/// The central manager that provides access to the IOS bluetooth module.
		/// </summary>
		private CBCentralManager centralManager;
		/// <summary>
		/// The mapping of peripherals to their connection, if any.
		/// </summary>
		private Dictionary<CBPeripheral, IConnection> connectionMapping = new Dictionary<CBPeripheral, IConnection>();

		public BluetoothManager() {
			centralManager = new CBCentralManager(this, new DispatchQueue("Test Bench Bluetooth Queue", true));
		}

		public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral) {
			base.ConnectedPeripheral(central, peripheral);
		}

		public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
			base.DisconnectedPeripheral(central, peripheral, error);
		}

		public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
			var serialNumber = peripheral.DetermineSerialNumber();

			if (serialNumber == null) {
				Log.E(this, "Failed to resolve serial number for " + peripheral.Name);
				return;
			}
		}

		public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error) {
			base.FailedToConnectPeripheral(central, peripheral, error);
		}

		public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
			base.RetrievedConnectedPeripherals(central, peripherals);
		}

		public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals) {
			base.RetrievedPeripherals(central, peripherals);
		}

		public override void UpdatedState(CBCentralManager central) {
			throw new NotImplementedException();
		}

		public override void WillRestoreState(CBCentralManager central, NSDictionary dict) {
			base.WillRestoreState(central, dict);
		}

		public Task<bool> Enable() {
			return Task.FromResult(centralManager.State == CBCentralManagerState.PoweredOn);
		}

		public bool CanResolveProtocol(EProtocolVersion protocol) {
			return true;
		}

		public async Task<bool> Scan(TimeSpan scanTime) {
			lock (this) {
				if (isScanning) {
					return false;
				} else {
					isScanStopRequested = false;
				}
			}

			var options = new PeripheralScanningOptions();

			options.AllowDuplicatesKey = false;

			centralManager.ScanForPeripherals(default(CBUUID[]), options);

			var startTime = DateTime.Now;
			while (!isScanStopRequested && DateTime.Now - startTime < scanTime) {
				await Task.Delay(100);
			}

			return true;
		}

		public void Stop() {
			isScanStopRequested = true;
			centralManager.StopScan();
		}
	}
}

