namespace TestBench.Droid.Bluetooth {

	using System;
	using System.Collections.Generic;

	using Android.Content;
	using Android.Content.PM;
	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.OS;
	using Android.Support.V4.Content;

	using Appion.Commons.Util;

	using ION.Core.Devices;

	/// <summary>
	/// A simple object that is responsible for perform bluetooth scans to find valid ION devices.
	/// </summary>
	public class BtScanner {
		public event Action onScanStarted;
		public event Action onScanStopped;
		public event Action<IONBtDevice> onDeviceFound;

		public HashSet<IONBtDevice> foundDevices {
			get {
				return new HashSet<IONBtDevice>(__foundDevices);
			}
		} HashSet<IONBtDevice> __foundDevices = new HashSet<IONBtDevice>();

		public bool isScanning { get; private set; }

		private Context context;
		private BluetoothManager manager;
		private BluetoothAdapter adapter;
		private IScanDelegate scanDelegate;

		public BtScanner(Context context) {
			this.context = context;

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				if (Permission.Granted == ContextCompat.CheckSelfPermission(context, Android.Manifest.Permission.AccessFineLocation)) {
					Log.D(this, "This user is using the 5.0 bluetooth api");
					scanDelegate = new Api21Scanner(manager.Adapter);
				} else {
					Log.D(this, "This user is using the 4.3 bluetooth api due to rejected permissions");
					scanDelegate = new Api18Scanner(manager.Adapter);
				}
			} else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
				Log.D(this, "This user is using the 4.3 bluetooth api due to versioning");
				scanDelegate = new Api18Scanner(manager.Adapter);
			} else {
				// TODO ahodder@appioninc.com: Catch and display user message
				// No good, the user's device cannot support le connections.
				throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
			}
		}

		public void StartScan() {
			scanDelegate.onBluetoothDeviceFound += OnDeviceFound;
			scanDelegate.StartScan();
			isScanning = true;
			if (onScanStarted != null) {
				onScanStarted();
			}
		}

		public void StopScan() {
			scanDelegate.onBluetoothDeviceFound -= OnDeviceFound;
			scanDelegate.StopScan();
			isScanning = false;
			if (onScanStopped != null) {
				onScanStopped();
			}
		}

		public void ClearFoundDevices() {
			__foundDevices.Clear();
		}

		private void OnDeviceFound(BluetoothDevice device, byte[] adPacket) {
			if (IsDeviceValid(device)) {
				var d = new IONBtDevice(device, device.Name.ParseSerialNumber());
				__foundDevices.Add(d);
				if (onDeviceFound != null) {
					onDeviceFound(d);
				}
			}
		}

		private bool IsDeviceValid(BluetoothDevice device) {
			return SerialNumberExtensions.IsValidSerialNumber(device.Name);
		}

		public class IONBtDevice {
			public BluetoothDevice bluetoothDevice { get; private set; }
			public ISerialNumber serialNumber { get; private set; }

			public IONBtDevice(BluetoothDevice device, ISerialNumber serialNumber) {
				this.bluetoothDevice = device;
				this.serialNumber = serialNumber;
			}
		}

		public delegate void OnBluetoothDeviceFound(BluetoothDevice device, byte[] advertisementPacket);
		private interface IScanDelegate {
			event OnBluetoothDeviceFound onBluetoothDeviceFound;
			bool isScanning { get; }

			void StartScan();
			void StopScan();
		}

		private class Api21Scanner : ScanCallback, IScanDelegate {
			public event OnBluetoothDeviceFound onBluetoothDeviceFound;

			public bool isScanning { get; private set; }

			private BluetoothAdapter adapter;

			public Api21Scanner(BluetoothAdapter adapter) {
				this.adapter = adapter;
			}

			public void StartScan() {
				isScanning = true;
				adapter.BluetoothLeScanner.StartScan(this);
			}

			public void StopScan() {
				adapter.BluetoothLeScanner.StopScan(this);
				isScanning = false;
			}

			public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
				base.OnBatchScanResults(results);
			}

			public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
				if (onBluetoothDeviceFound != null) {
					onBluetoothDeviceFound(result.Device, result.ScanRecord.GetBytes());
				}
			}

			public override void OnScanFailed(ScanFailure errorCode) {
				base.OnScanFailed(errorCode);
			}
		}

		private class Api18Scanner : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanDelegate {
			// Implemented from IScanDelegate
			public bool isScanning { get; private set; }
			public event OnBluetoothDeviceFound onBluetoothDeviceFound;


			private BluetoothAdapter adapter;
			private Handler handler;

			public Api18Scanner(BluetoothAdapter adapter) {
				this.adapter = adapter;
				this.handler = new Handler();
			}

			public void StartScan() {
				if (isScanning) {
					return;
				}

				try {
					var ret = adapter.StartLeScan(this);
					isScanning = true;
				} catch (Exception e) {
					Log.E(this, "Wtf?", e);
				}
			}

			public void StopScan() {
				if (isScanning) {
					adapter.StopLeScan(this);
				}
				isScanning = false;
			}

			public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
				handler.Post(() => {
					if (onBluetoothDeviceFound != null) {
						onBluetoothDeviceFound(device, scanRecord);
					}
				});
			}
		}
	}
}
