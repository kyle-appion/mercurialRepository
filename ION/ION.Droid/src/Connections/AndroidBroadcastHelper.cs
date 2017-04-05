namespace ION.Droid.Connections {

	using System;
	using System.Text;

	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;

	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.Devices.Connections;
	using ION.Core.Devices.Protocols;

	/// <summary>
	/// A connection helper that will receive broadcast packets from ION devices that are broadcast capable.
	/// </summary>
	public class AndroidBroadcastHelper : Java.Lang.Object, IConnectionHelper, Handler.ICallback {
		/// <summary>
		/// The device name of all rigado devices.
		/// </summary>
		private const string RIGDFU = "RigCom";
		/// <summary>
		///  The appion specific company code.
		/// </summary>
		private const int APPION_COMPANY_CODE = 0x8c03;

		private const int MSG_STOP_SCAN = 1;
		private const int MSG_START_BROADCAST = 2;

		public event OnScanStateChanged onScanStateChanged;

		public event OnDeviceFound onDeviceFound;

		public bool isEnabled {
			get {
				return manager.Adapter.IsEnabled;
			}
		}

		public bool isScanning { 
			get {
				return __isScanning;
			}
			set {
				__isScanning = value;
				if (onScanStateChanged != null) {
					onScanStateChanged(this);
				}
			}
		} bool __isScanning;

		/// <summary>
		/// The interval that that broadcast scan is performed.
		/// </summary>
		public TimeSpan scanInterval;
		/// <summary>
		/// The duration of a broadcast scan.
		/// </summary>
		public TimeSpan scanTime;

		/// <summary>
		/// The bluetooth manager that the connection helper is using to start scans.
		/// </summary>
		private BluetoothManager manager;
		/// <summary>
		/// The handler that we will post our connection actions to.
		/// </summary>
		private Handler handler;
		/// <summary>
		/// The delegate that is used to start scanning.
		/// </summary>
		private IScanDelegate scanDelegate;

		public AndroidBroadcastHelper(Context context, TimeSpan scanInterval, TimeSpan scanTime) {
			this.scanInterval = scanInterval + scanTime;
			this.scanTime = scanTime;
			manager = (BluetoothManager)context.GetSystemService(Context.BluetoothService);
			handler = new Handler(this);

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				scanDelegate = new Api21ScanDelegate(manager.Adapter, NotifyDeviceFound);
			} else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
				scanDelegate = new Api18ScanDelegate(manager.Adapter, NotifyDeviceFound);
			} else {
				// TODO ahodder@appioninc.com: Catch and display user message
				// No good, the user's device cannot support le connections.
				throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
			}

			handler.SendEmptyMessageDelayed(MSG_START_BROADCAST, (long)scanInterval.TotalMilliseconds);
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			handler.RemoveCallbacksAndMessages(null);
		}

		public bool HandleMessage(Message msg) {
			switch (msg.What) {
				case MSG_STOP_SCAN:
					StopScan();
					return true;
				case MSG_START_BROADCAST:
					Log.D(this, "MSG_START_BROADCAST");
					handler.RemoveMessages(MSG_START_BROADCAST);
					if (isScanning) {
						StopScan();
					}

					StartScan(scanTime);	
					handler.SendEmptyMessageDelayed(MSG_START_BROADCAST, (long)scanInterval.TotalMilliseconds);
					return true;
				default:
					return false;
			}
		}

		public bool StartScan(TimeSpan scanTime) {
			if (isScanning) {
				return false;
			}
			Log.D(this, "Starting Scan");

			var ret = scanDelegate.StartScan();

			isScanning = ret;

			handler.PostDelayed(() => {
				StopScan();
			}, (long)scanTime.TotalMilliseconds);

			return ret;
		}

		public void StopScan() {
			Log.D(this, "Stopping scan");
			scanDelegate.StopScan();
			isScanning = false;
		}

		private void NotifyDeviceFound(BluetoothDevice device, byte[] scanRecord) {
			Log.D(this, "Found device: " + device.Name);
			ISerialNumber serialNumber = null;
			if (!DiscoverSerialNumber(device, scanRecord, out serialNumber)) {
				return; // The device is not ours. Discard it.
			}

			Log.D(this, "ScanRecord: " + scanRecord.ToByteString());
			byte[] broadcastPayload = ParseBroadcastPayloadFromScanRecord(scanRecord);
			Log.D(this, "BroadcastPayload: " + broadcastPayload?.ToByteString());

			var protocolVersion = DiscoverProtocolVersion(device, serialNumber, broadcastPayload);
			if (protocolVersion == EProtocolVersion.V4) {
				if (onDeviceFound != null) {
					handler.Post(() => {
						onDeviceFound(this, serialNumber, device.Address, broadcastPayload, protocolVersion);
					});
				}
			} else {
				Log.D(this, "Ignoring device due to invalid broadcast packet: " + serialNumber);
			}
		}

		/// <summary>
		/// Queries whether or not the given device is a valid Appion device.
		/// </summary>
		/// <returns><c>true</c> if this instance is appion device the specified device; otherwise, <c>false</c>.</returns>
		/// <param name="device">Device.</param>
		private bool DiscoverSerialNumber(BluetoothDevice device, byte[] scanRecord, out ISerialNumber serialNumber) {
			if (SerialNumberExtensions.IsValidSerialNumber(device.Name)) {
				serialNumber = SerialNumberExtensions.ParseSerialNumber(device.Name);
				return true;
			} else if (RIGDFU.Equals(device.Name)) {
				if (scanRecord[1] == 0xff) {
					var serialBytes = new byte[8];
					Array.Copy(scanRecord, 4, serialBytes, 0, 8);
					var raw = Encoding.UTF8.GetString(serialBytes);
					if (SerialNumberExtensions.IsValidSerialNumber(raw)) {
						serialNumber = null;
						return true;
					} else {
						serialNumber = null;
						return false;
					}
				}

				serialNumber = null;
				return false;
			} else {
				serialNumber = null;
				return false;
			}
		}

		/// <summary>
		/// Parses the broadcast payload from the scan record.
		/// </summary>
		/// <returns>The scan record for payload.</returns>
		/// <param name="scanRecord">Scan record.</param>
		private byte[] ParseBroadcastPayloadFromScanRecord(byte[] scanRecord) {
			byte[] ret = null;

			var i = 0;
			while (i < scanRecord.Length) {
				var len = scanRecord[i++];
				var type = scanRecord[i++];
				if (type == 0xff) {
					var companyCode = scanRecord[i] << 8 | scanRecord[i + 1];
					if (companyCode == APPION_COMPANY_CODE) {
						ret = new byte[19];
						Array.Copy(scanRecord, i + 10, ret, 0, 19);
					}
				}
				i += len - 2; // We already incremented for the len and type
			}

			return ret;
		}

		/// <summary>
		/// Determines the protocol that should be used for the given device.
		/// </summary>
		/// <returns>The protocol version.</returns>
		/// <param name="device">Device.</param>
		/// <param name="broadcastPayload">Broadcast payload.</param>
		private EProtocolVersion DiscoverProtocolVersion(BluetoothDevice device, ISerialNumber serialNumber, byte[] broadcastPayload) { 
			if (broadcastPayload != null && broadcastPayload[0] == 4) {
				return EProtocolVersion.V4;
			} else {
				if (serialNumber.rawSerial.StartsWith("S")) {
					return EProtocolVersion.V4;
				} else {
					return EProtocolVersion.V1;
				}
			}
		}
	}
}

