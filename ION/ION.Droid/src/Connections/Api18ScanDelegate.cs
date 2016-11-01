namespace ION.Droid.Connections {

	using System;

	using Android.Bluetooth;

	/// <summary>
	/// The scan delegate that is used for older than Android 5 (api level 21) devices.
	/// </summary>
	internal class Api18ScanDelegate : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanDelegate {
		private BluetoothAdapter adapter;
		private InternalDeviceFound deviceFound;

		public Api18ScanDelegate(BluetoothAdapter adapter, InternalDeviceFound internalDeviceFound) {
			this.adapter = adapter;
			deviceFound = internalDeviceFound;
		}

		public bool StartScan() {
			try {
			return adapter.StartLeScan(this);
			} catch (Exception e) {
				ION.Core.Util.Log.E(this, "Wtf?", e);
				return false;
			}
		}

		public void StopScan() {
			adapter.StopLeScan(this);
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
			deviceFound(device, scanRecord);
		}
	}
}

