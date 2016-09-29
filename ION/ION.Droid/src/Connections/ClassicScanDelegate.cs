namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;

	using ION.Core.Util;

	/// <summary>
	/// Classic scan delegate.
	/// </summary>
	internal class ClassicScanDelegate : BroadcastReceiver, IScanDelegate {
		/// <summary>
		/// The device name that signifies the device as an appion device.
		/// </summary>
		private const string APPION_GAUGE = "APPION Gauge";

		private Context context;
		private BluetoothAdapter adapter;
		private InternalDeviceFound deviceFound;
		private Handler handler;

		private List<BluetoothDevice> pendingDevices = new List<BluetoothDevice>();

		public ClassicScanDelegate(Context context, BluetoothAdapter adapter, InternalDeviceFound internalDeviceFound) {
			this.context = context;
			this.adapter = adapter;
			this.deviceFound = internalDeviceFound;
			this.handler = new Handler();
		}

		public bool StartScan() {
			var ret = adapter.StartDiscovery();
			if (ret) {
				var filter = new IntentFilter();
				filter.AddAction(BluetoothDevice.ActionFound);

				context.RegisterReceiver(this, filter);

				handler.PostDelayed(() => {
					StopScan();
				}, 12 * 1000);
			}

			return ret;
		}

		public void StopScan() {
			try {
				context.UnregisterReceiver(this);
			} catch (Exception e) {
				Log.E(this, "Calling unregister too many times", e);
			}
			adapter.CancelDiscovery();

			foreach (var device in pendingDevices) {
				deviceFound(device, null);
			}
		}

		public override void OnReceive(Context context, Intent intent) {
			var action = intent.Action;

			switch (action) {
				case BluetoothDevice.ActionFound:
					Log.D(this, "Found classic device");
					var device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;
					Log.D(this, "Device name: " + device.Name);

					if (APPION_GAUGE.Equals(device.Name)) {
						pendingDevices.Add(device);
					}
/*
					if (device != null) {
						deviceFound(device, null);
					} else {
						Log.D(this, "Failed to resolve device: not a bluetooth device");
					}
*/
					break;
			}
		}
	}
}
