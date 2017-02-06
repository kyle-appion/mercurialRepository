namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Bluetooth.LE;

	using Appion.Commons.Util;

	internal class Api21ScanDelegate : ScanCallback, IScanDelegate {
		// Implemented from IScanDelegate
		public bool isScanning { get; private set; }

		private BluetoothAdapter adapter;
		private InternalDeviceFound deviceFound;

		public Api21ScanDelegate(BluetoothAdapter adapter, InternalDeviceFound internalDeviceFound) {
			this.adapter = adapter;
			deviceFound = internalDeviceFound;
			isScanning = false;
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}
			try {
				adapter.BluetoothLeScanner.StartScan(this);
				isScanning = true;
				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to start scan. This is likely due to the bluetooth mosule having been turned off before the scan.", e);
				return false;
			}
		}

		public void StopScan() {
			if (isScanning) {
				try {
					adapter.BluetoothLeScanner.StopScan(this);
					isScanning = false;
				} catch (Exception e) {
					Log.E(this, "Failed to stop scan. This is likely due to the bluetooth module having been turned off mid scan", e);
				}
			}
		}

		public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
		}

		public override void OnScanFailed(ScanFailure errorCode) {
		}

		public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
			deviceFound(result.Device, result.ScanRecord.GetBytes());
		}
	}
}

