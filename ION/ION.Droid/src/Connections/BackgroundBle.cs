namespace ION.Droid.Connections {

	using System;
	using System.Text;

	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.Content;
	using Android.Content.PM;
	using Android.Gms.Common;
	using Android.Gms.Common.Apis;
	using Android.Gms.Nearby.Messages;
	using Android.Gms.Nearby;
	using Android.Support.V4.Content;
	using Android.Text;
	using Android.OS;

	using ION.Core.Util;

	public class BackgroundBle {
		private const int MANFAC_ID = 0x8c03;
		private const long SCAN_INTERVAL = 2500;
		private const long SCAN_DURATION = 500;

		public bool isScanning { get { return scanDelegate.isScanning; } }

		private Handler handler;
		private SD scanDelegate;

		public BackgroundBle(BluetoothAdapter adapter) {
			this.scanDelegate = new SD(adapter);
			this.handler = new Handler();
		}

		public void StartBroadcastReceiving() {
			scanDelegate.StartScan();
			handler.Post(ToggleScan);
		}

		public void StopBroadcastReceiving() {
			handler.RemoveCallbacksAndMessages(null);
			scanDelegate.StopScan();
		}

		private void ToggleScan() {
			if (scanDelegate.isScanning) {
				scanDelegate.StopScan();
				handler.PostDelayed(ToggleScan, SCAN_INTERVAL);
			} else {
				scanDelegate.StartScan();
				handler.PostDelayed(ToggleScan, SCAN_DURATION);
			}
		}

		internal class SD : ScanCallback, IScanDelegate {

			public bool isScanning { get; private set; }

			private BluetoothAdapter adapter;

			public SD(BluetoothAdapter adapter) {
				this.adapter = adapter;
			}

			public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
				var sb = new StringBuilder();
				sb.Append("OnBatchScanResults(Results: {\n");
				foreach (var result in results) {
					sb.Append("\t{Device: ")
					  .Append(result.Device.Name)
					  .Append(", Packet: ")
					  .Append(result.ScanRecord.GetBytes().ToByteString())
					  .Append("}\n");
				}
				sb.Append("}");
				Log.D(this, sb.ToString());
			}

			public override void OnScanFailed(ScanFailure errorCode) {
				Log.E(this, "Scan Failed: " + errorCode);
			}

			public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
				var sb = new StringBuilder();
				sb.Append("OnScanResult(ScanCallbackType: ")
				  .Append(callbackType)
				  .Append(", ScanResult: {Device: ")
				  .Append(result.Device.Name)
				  .Append(", Packet: ")
				  .Append(result.ScanRecord.GetBytes().ToByteString())
				  .Append("})");
				Log.D(this, sb.ToString());
			}

			public bool StartScan() {
				isScanning = true;
				adapter.BluetoothLeScanner.StartScan(this);
				return true;
			}

			public void StopScan() {
				adapter.BluetoothLeScanner.StopScan(this);
				isScanning = false;
			}
		}
	}
}
