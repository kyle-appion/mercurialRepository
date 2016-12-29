namespace ION.Droid.Connections {

	using System;

	using Android.Bluetooth;

	/// <summary>
	/// The scan delegate that is used for older than Android 5 (api level 21) devices.
	/// </summary>
	internal class Api18ScanDelegate : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanDelegate {
		// Implemented from IScanDelegate
		public bool isScanning { get; private set; }

		private BluetoothAdapter adapter;
		private InternalDeviceFound deviceFound;

		public Api18ScanDelegate(BluetoothAdapter adapter, InternalDeviceFound internalDeviceFound) {
			this.adapter = adapter;
			deviceFound = internalDeviceFound;
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}

			try {
				var ret = adapter.StartLeScan(this);
				isScanning = true;
				return ret;
			} catch (Exception e) {
				ION.Core.Util.Log.E(this, "Wtf?", e);
				return false;
			}
		}

		public void StopScan() {
			if (isScanning) {
				adapter.StopLeScan(this);
			}
			isScanning = false;
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
			deviceFound(device, scanRecord);
		}
	}
}

