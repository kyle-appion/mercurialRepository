namespace ION.Droid.Connections {

	using System;
	using System.Text;

	using Android.Bluetooth;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V4.Content;

	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.Devices.Connections;
	using ION.Core.Devices.Protocols;

	using ION.Droid.App;

	public class AndroidConnectionHelper : IConnectionHelper {
		/// <summary>
		/// The device name of all rigado devices.
		/// </summary>
		private const string RIGDFU = "RigCom";
		/// <summary>
		///  The appion specific company code.
		/// </summary>
		private const int APPION_COMPANY_CODE = 0x8c03;

		/// <summary>
		/// The delay between scan actions in milliseconds.
		/// </summary>
		private const long RADIO_DELAY = 500;
		/// <summary>
		/// How long a classic scan is in milliseconds.
		/// </summary>
		private const long CLASSIC_SCAN_TIME = 12 * 1000;

		public event OnScanStateChanged onScanStateChanged;

		public event OnDeviceFound onDeviceFound;

		public bool isEnabled { get { return manager.Adapter.IsEnabled; } }

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
		/// The context that is driving the connection helper.
		/// </summary>
		private AndroidION ion;
		/// <summary>
		/// The bluetooth manager that the connection helper is using to start scans.
		/// </summary>
		private BluetoothManager manager;
		/// <summary>
		/// The handler that we will post our connection actions to.
		/// </summary>
		private Handler handler;
		/// <summary>
		/// The delegate that is used to scan for le.
		/// </summary>
		/// <param name="context">Context.</param>
		private IScanDelegate leScanDelegate;
		/// <summary>
		/// The delegate that is used to scan for classic devices.
		/// </summary>
		/// <param name="context">Context.</param>
		private IScanDelegate classicScanDelegate;
		/// <summary>
		/// The entity that is used to manager broadcasting.
		/// </summary>
//		private BackgroundBle broadcasting;

		public AndroidConnectionHelper(AndroidION ion) {
			manager = (BluetoothManager)ion.GetSystemService(Context.BluetoothService);
			handler = new Handler();
//			broadcasting = new BackgroundBle(manager.Adapter);

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				if (Permission.Granted == ContextCompat.CheckSelfPermission(ion, Android.Manifest.Permission.AccessFineLocation)) {
					Log.D(this, "This user is using the 5.0 bluetooth api");
					leScanDelegate = new Api21ScanDelegate(manager.Adapter, NotifyDeviceFound);
				} else {
					Log.D(this, "This user is using the 4.3 bluetooth api due to rejected permissions");
					leScanDelegate = new Api18ScanDelegate(manager.Adapter, NotifyDeviceFound);
				}
			} else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
				Log.D(this, "This user is using the 4.3 bluetooth api due to versioning");
				leScanDelegate = new Api18ScanDelegate(manager.Adapter, NotifyDeviceFound);
			} else {
				// TODO ahodder@appioninc.com: Catch and display user message
				// No good, the user's device cannot support le connections.
				throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
			}

			classicScanDelegate = new ClassicScanDelegate(ion, manager.Adapter, NotifyClassicDeviceFound);
		}

		public void Dispose() {
			handler.RemoveCallbacksAndMessages(null);
		}

		public bool StartScan(TimeSpan scanTime) {
			if (isScanning) {
				return false;
			}
/*
			broadcasting.StartBroadcastReceiving();
			if (onScanStateChanged != null) {
				onScanStateChanged(this);
			}
			return true;
*/
			var ret = leScanDelegate.StartScan();

			if (ret) {
				isScanning = ret;

				var stl = (long)scanTime.TotalMilliseconds;

				handler.PostDelayed(() => {
					leScanDelegate.StopScan();
				}, stl);

				handler.PostDelayed(() => {
					classicScanDelegate.StartScan();
				}, stl + RADIO_DELAY);

				handler.PostDelayed(() => {
					StopScan();
				}, stl + RADIO_DELAY + CLASSIC_SCAN_TIME);
			}

			return ret;
		}

		public void StopScan() {
/*
			broadcasting.StopBroadcastReceiving();
			if (onScanStateChanged != null) {
				onScanStateChanged(this);
			}
*/
			handler.RemoveCallbacksAndMessages(null);

			if (isScanning) {
				isScanning = false;
				leScanDelegate.StopScan();
				classicScanDelegate.StopScan();
			}
		}

		private void NotifyClassicDeviceFound(ISerialNumber serialNumber, BluetoothDevice device) {
			handler.Post(() => {
				onDeviceFound(this, serialNumber, device.Address, null, EProtocolVersion.Classic);
			});
		}

		private void NotifyDeviceFound(BluetoothDevice device, byte[] scanRecord) {
			Log.D(this, "Found device: " + device.Name);
			ISerialNumber serialNumber = null;

			if (!DiscoverSerialNumber(device, scanRecord, out serialNumber)) {
				Log.D(this, "Discarding device: " + device.Name);
				return; // The device is not ours. Discard it.
			}

			Log.D(this, "ScanRecord: " + scanRecord.ToByteString());
			byte[] broadcastPayload = ParseBroadcastPayloadFromScanRecord(scanRecord);
			Log.D(this, "BroadcastPayload: " + broadcastPayload?.ToByteString());

			var protocolVersion = DiscoverProtocolVersion(device, serialNumber, broadcastPayload);

			if (onDeviceFound != null) {
				handler.Post(() => {
					onDeviceFound(this, serialNumber, device.Address, broadcastPayload, protocolVersion);
				});
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
						serialNumber = SerialNumberExtensions.ParseSerialNumber(raw);
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
			if (scanRecord == null) {
				return null;
			}

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
				// TODO ahodder@appioninc.com: We need a better way to determine a device's protocol.
				if (serialNumber.rawSerial.Length == 8) {
					return EProtocolVersion.V4;
				} else if (device.Type == BluetoothDeviceType.Classic) {
					return EProtocolVersion.Classic;
				} else {
					return EProtocolVersion.V1;
				}
			}
		}
	}
}

