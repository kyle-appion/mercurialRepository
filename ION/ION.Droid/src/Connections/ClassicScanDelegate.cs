namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;

	using Appion.Commons.Util;

	using ION.Droid.App;

	/// <summary>
	/// Classic scan delegate.
	/// </summary>
	internal class ClassicScanDelegate : BroadcastReceiver, IScanDelegate {
		/// <summary>
		/// The device name that signifies the device as an appion device.
		/// </summary>
		private const string APPION_GAUGE = "APPION Gauge";

		// Implemented from IScanDelegate
		public bool isScanning {
			get {
				return adapter.IsDiscovering;
			}
		}

		private AndroidION ion;
		private BluetoothAdapter adapter;
		private InternalClassicDeviceFound deviceFound;
		private Handler handler;
		private bool isBroadcastRegistered;

		private List<BluetoothDevice> pendingDevices = new List<BluetoothDevice>();


		public ClassicScanDelegate(AndroidION ion, BluetoothAdapter adapter, InternalClassicDeviceFound internalDeviceFound) {
			this.ion = ion;
			this.adapter = adapter;
			this.deviceFound = internalDeviceFound;
			this.handler = new Handler();
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}

			var ret = adapter.StartDiscovery();
			if (ret) {
				var filter = new IntentFilter();
				filter.AddAction(BluetoothDevice.ActionFound);

				lock (this) {
					if (!isBroadcastRegistered) {
						ion.RegisterReceiver(this, filter);
						isBroadcastRegistered = true;
					}
				}

				handler.PostDelayed(() => {
					StopScan();
				}, 12 * 1000);
			}

			return ret;
		}

		public async void StopScan() {
			if (isScanning) {
				try {
					lock (this) {
						if (isBroadcastRegistered) {
							ion.UnregisterReceiver(this);
							isBroadcastRegistered = false;
						}
					}
				} catch (Exception e) {
					Log.E(this, "Calling unregister too many times", e);
				}
				adapter.CancelDiscovery();

				var pd = new HashSet<BluetoothDevice>(pendingDevices);
				pendingDevices.Clear();

				foreach (var device in pd) {
					var serial = await ClassicConnection.ResolveSerialNumber(device);
					Log.D(this, "Found serial number: " + serial);
					if (serial != null) {
						deviceFound(serial, device);
					}
				}
			}
		}

		private bool IsDeviceKnown(BluetoothDevice device) {
			Log.D(this, "Looking for: " + device);
			foreach (var idevice in ion.deviceManager.devices) {
				var bd = idevice.connection.nativeDevice as BluetoothDevice;
				Log.D(this, "Looking at: " + bd);
				if (device.Equals(bd)) {
					return true;
				}
			}

	    return false;
		}

		public override async void OnReceive(Context context, Intent intent) {
			var action = intent.Action;

			switch (action) {
				case BluetoothDevice.ActionFound:
					Log.D(this, "Found classic device");
					var device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;
					Log.D(this, "Device name: " + device.Name);

					if ((device.Name == null || APPION_GAUGE.Equals(device.Name)) && !IsDeviceKnown(device)) {
						pendingDevices.Add(device);
					}
					break;
			}
		}
	}
}
