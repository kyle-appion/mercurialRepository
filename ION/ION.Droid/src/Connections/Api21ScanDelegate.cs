namespace ION.Droid.Connections {

	using Android.Bluetooth;
	using Android.Bluetooth.LE;

	internal class Api21ScanDelegate : ScanCallback, IScanDelegate {
		private BluetoothAdapter adapter;
		private InternalDeviceFound deviceFound;
		private bool isScanning;

		public Api21ScanDelegate(BluetoothAdapter adapter, InternalDeviceFound internalDeviceFound) {
			this.adapter = adapter;
			deviceFound = internalDeviceFound;
			isScanning = false;
		}

		public bool StartScan() {
			adapter.BluetoothLeScanner.StartScan(this);
			isScanning = true;
			return true;
		}

		public void StopScan() {
			if (isScanning) {
				adapter.BluetoothLeScanner.StopScan(this);
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

