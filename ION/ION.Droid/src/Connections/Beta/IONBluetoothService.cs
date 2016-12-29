namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V4.Content;

	using ION.Core.Connections;
	using ION.Core.Devices;
	using ION.Core.Devices.Connections;
	using ION.Core.Devices.Protocols;
	using ION.Core.Util;

	public class IONBluetoothService : IConnectionHelper, IConnectionFactory {
		/// <summary>
		/// The Appion manufacturing id that is used to identfy appion products.
		/// </summary>
		public const int MANFAC_ID = 0x8c03;

		private const int LE_SCAN_DURATION = 7500;
		private const int CLASSIC_SCAN_DURATION = 12000;
		private const int DEFAULT_BROADCAST_SCAN_INTERVAL = 7500;
		private const int DEFAULT_BROADCAST_SCAN_DURATION = 1500;

		private const int HANDLE_START_LE = 1;
		private const int HANDLE_STOP_LE = 2;
		private const int HANDLE_START_CLASSIC = 3;
		private const int HANDLE_STOP_CLASSIC = 4;

		// Implemented from IConnectionHelper
		public event OnScanStateChanged onScanStateChanged;
		// Implemented from IConnectionHelper
		public event OnDeviceFound onDeviceFound;

		// Implemented from IConnectionHelper
		public bool isEnabled { get { return adapter.IsEnabled; } }
		// Implemented from IConnectionHelper
		public bool isScanning {
			get {
				return __isScanning;
			}
			private set {
				__isScanning = value;
				if (onScanStateChanged != null) {
					onScanStateChanged(this);
				}
			}
		} bool __isScanning;
		// Implemented From IConnectionHelper
		public bool isBroadcastingEnabled {
			get {
				return __broadcastingEnabled;
			}
			set {
				__broadcastingEnabled = value;
			}
		} bool __broadcastingEnabled;


		public Context context { get; private set; }

		public BluetoothManager manager { get; private set; }
		public BluetoothAdapter adapter { get; private set; }

		public TimeSpan broadcastInterval { get; set; }
		public TimeSpan broadcastDuration { get; set; }

		private Dictionary<string, IConnection> connectionLookup = new Dictionary<string, IConnection>();
		private Handler handler;
		private HandlerThread handlerThread;

		private IScanDelegate leScanDelegate;
		private IScanDelegate classicScanDelegate;
		private IScanDelegate broadcastDelegate;

		public IONBluetoothService(Context context) {
			this.context = context;

			manager = context.GetSystemService(Context.BluetoothService) as BluetoothManager;
			adapter = manager.Adapter;

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				if (Permission.Granted == ContextCompat.CheckSelfPermission(context, Android.Manifest.Permission.AccessFineLocation)) {
					Log.D(this, "This user is using the 5.0 bluetooth api");
					leScanDelegate = new Api21LeScanDelegate(this);
				} else {
					Log.D(this, "This user is using the 4.3 bluetooth api due to rejected permissions");
					leScanDelegate = new Api18LeScanDelegate(this);
				}
			} else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
				Log.D(this, "This user is using the 4.3 bluetooth api due to versioning");
				leScanDelegate = new Api18LeScanDelegate(this);
			} else {
				// TODO ahodder@appioninc.com: Catch and display user message
				// No good, the user's device cannot support le connections.
				throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
			}

			handlerThread = new HandlerThread(typeof(IONBluetoothService).Name);
			handlerThread.Start();
			handler = new Handler(handlerThread.Looper);

			classicScanDelegate = new ClassicScanDelegatee(this, handlerThread.Looper);
			broadcastDelegate = new BroadcastScanDelegate(leScanDelegate, handlerThread.Looper, DEFAULT_BROADCAST_SCAN_INTERVAL, DEFAULT_BROADCAST_SCAN_DURATION, context);

			isBroadcastingEnabled = true;
			handler.PostDelayed(() => broadcastDelegate.StartScan(), 15000);
		}

		// Implemented from IConnectionFactory
		public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
			if (connectionLookup.ContainsKey(address)) {
				return connectionLookup[address];
			}

			var device = adapter.GetRemoteDevice(address);

			if (device == null) {
				throw new Exception("Cannot create connection: Failed to resilve connection's device.");
			}

			IConnection ret = null;

			switch (protocolVersion) {
				case EProtocolVersion.Classic:
					ret = new ClassicConnection(device);
					break;
				case EProtocolVersion.V1:
					ret = new LeConnection(context, manager, device);
					break;
				case EProtocolVersion.V4:
					ret = new RigadoConnection(context, manager, device);
					break;
				default:
					throw new Exception("Cannot create connection for protocol version: " + protocolVersion);
			}

			connectionLookup[address] = ret;

			return ret;
		}

		/// <summary>
		/// Shutsdown the service. Once this is called, the service can not longer be used.
		/// </summary>
		public void Dispose() {
			foreach (var connection in connectionLookup.Values) {
				connection.Disconnect();
			}

			handler.RemoveCallbacksAndMessages(null);
			handlerThread.QuitSafely();

			handlerThread = null;
			handler = null;
		}

		/// <summary>
		/// Toggles a scan action. If the service is currently scanning, we will stop it. Otherwise, we will start a scan.
		/// </summary>
		public void ToggleScan() {
			if (isScanning) {
				StopScan();
			} else {
				StartScan(TimeSpan.FromMilliseconds(0));
			}
		}

		// Implemented From IConnectionHelper
		public bool StartScan(TimeSpan span) {
			if (isScanning) {
				return false;
			}

			if (broadcastDelegate.isScanning) {
				broadcastDelegate.StopScan();
			}

			isScanning = true;

			if (leScanDelegate.StartScan()) {
				handler.PostDelayed(leScanDelegate.StopScan, LE_SCAN_DURATION);
				handler.PostDelayed(() => classicScanDelegate.StartScan(), LE_SCAN_DURATION);
				handler.PostDelayed(StopScan, LE_SCAN_DURATION + CLASSIC_SCAN_DURATION);
				return true;
			} else {
				isScanning = false;
				return false;
			}
		}

		// Implemented From IConnectionHelper
		public void StopScan() {
			if (isScanning) {
				handler.RemoveCallbacksAndMessages(null);
				leScanDelegate.StopScan();
				classicScanDelegate.StopScan();
			}

			isScanning = false;

			if (isBroadcastingEnabled) {
				handler.PostDelayed(() => broadcastDelegate.StartScan(), 500);
			}
		}

		/// <summary>
		/// Posts an event to the bluetooth service that a new device was found.
		/// </summary>
		/// <param name="device">Device.</param>
		/// <param name="type">Type.</param>
		/// <param name="scanRecord">Scan record.</param>
		public void PostDeviceFound(BluetoothDevice device, BluetoothDeviceType type, byte[] scanRecord) {
			handler.Post(() => OnDeviceFound(device, type, scanRecord));
		}

		/// <summary>
		/// A conveneience method that returns a 'what' message.
		/// </summary>
		/// <param name="what">What.</param>
		private Message Msg(int what) {
			return handler.ObtainMessage(what);
		}

		/// <summary>
		/// Called by a scan delegate when it finds a new device.
		/// </summary>
		/// <param name="device">Device.</param>
		/// <param name="type">Type.</param>
		/// <param name="scanRecord">The manufacture data that was present in the scan record.</param>
		private void OnDeviceFound(BluetoothDevice device, BluetoothDeviceType type, byte[] scanRecord) {
			// TODO ahodder@appioninc.com: We are trying to create the device a lot. Once here and once in the device manager.
			IConnection connection = null;
			if (connectionLookup.ContainsKey(device.Address)) {
				connection = connectionLookup[device.Address];
			}

			if (type == BluetoothDeviceType.Classic) {
				if (connection == null) {
					connection = CreateConnection(device.Address, EProtocolVersion.Classic);
				}
			} else {
				ISerialNumber sn = null;
				byte[] packet = null;

				if (RigadoBroadcastParser.ParseBroadcastPacket(scanRecord, out sn, out packet)) {
					// We received a fully valid broadcast packet. We know that the gauge is an Appion gauge and should resolve it.
					if (connection == null) {
						// We have not discovered this device before. Notify the world
						NotifyOfDeviceFound(sn, device.Address, packet, EProtocolVersion.V4);
					} else {
						// The connection already exists. Give it a new packet.
						connection.lastPacket = packet;
						connection.lastSeen = DateTime.Now;
					}
				} else {
					// We didn't receive a valid broadcasting packet, but the device may still be ours.
					if (device.Name.IsValidSerialNumber()) {
						// See, the device has a valid serial number.
						sn = device.Name.ParseSerialNumber();
						if (connection == null) {
							// We have not discovered this device before. Notify the world
							NotifyOfDeviceFound(sn, device.Address, null, EProtocolVersion.V1);
						} else {
							// The connection already exists. Update the last time that it was seen.
							connection.lastSeen = DateTime.Now;
						}
					} else {
						Log.D(this, "Ignoring non-appion device: " + device.Name);
					}
				}
			}
		}

		private void NotifyOfDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion version) {
			if (this.onDeviceFound != null) {
				onDeviceFound(this, serialNumber, address, packet, version);
			}
		}
	}







	/// <summary>
	/// The scan delegate that is resposible for resolving found le devices. This is the latest android ble api.
	/// </summary>
	internal class Api18LeScanDelegate : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanDelegate {
		// Implemented from IScanDelegate
		public bool isScanning { get; private set; }

		private IONBluetoothService service;

		public Api18LeScanDelegate(IONBluetoothService service) {
			this.service = service;
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}

			isScanning = true;
			service.adapter.StartLeScan(this);
			return true;
		}

		public void StopScan() {
			if (isScanning) {
				service.adapter.StopLeScan(this);
			}
			isScanning = false;
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
			service.PostDeviceFound(device, BluetoothDeviceType.Le, scanRecord);
		}
	}

	/// <summary>
	/// The scan delegate that is responsible for resolving found le devices. This is the latest android ble api.
	/// </summary>
	internal class Api21LeScanDelegate : ScanCallback, IScanDelegate {
		// Implemented from IScanDelegate
		public bool isScanning { get; private set; } 

		private IONBluetoothService service;

		public Api21LeScanDelegate(IONBluetoothService service) {
			this.service = service;
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}
			service.adapter.BluetoothLeScanner.StartScan(this);
			isScanning = true;
			return true;
		}

		public void StopScan() {
			if (isScanning) {
				service.adapter.BluetoothLeScanner.StopScan(this);
			}
			isScanning = false;
		}

		public override void OnBatchScanResults(IList<ScanResult> results) {
		}

		public override void OnScanFailed(ScanFailure errorCode) {
		}

		public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
			var data = result.ScanRecord.GetBytes().Subset(2);
			Log.D(this, "All Data: " + result.ScanRecord.GetBytes());
			Log.D(this, "Manfac Data: " + data);
			// TODO ahodder@appioninc.com: This is a little weird. ScanRecord.GetManufacturerSpecificData was returning null.
			service.PostDeviceFound(result.Device, BluetoothDeviceType.Le, data);
		}
	}

	/// <summary>
	/// The scan deletate that is responsbile for resolving found classic devices. This is a legacy from the initial
	/// version of the app.
	/// </summary>
	internal class ClassicScanDelegatee : BroadcastReceiver, IScanDelegate {
		private const int SCAN_DURATION = 12000;

		// Implemented from IScanDelegate
		public bool isScanning {
			get {
				return service.adapter.IsDiscovering;
			}
		}

		private IONBluetoothService service;
		private Handler handler;

		public ClassicScanDelegatee(IONBluetoothService service, Looper looper) {
			this.service = service;
			this.handler = new Handler(looper);
		}

		public bool StartScan() {
			if (isScanning) {
				return false;
			}
			var filter = new IntentFilter();
			filter.AddAction(BluetoothDevice.ActionFound);

			service.context.RegisterReceiver(this, filter);
			service.adapter.StartDiscovery();
			handler.PostDelayed(() => service.adapter.CancelDiscovery(), SCAN_DURATION);
			return true;
		}

		public void StopScan() {
			if (isScanning) {
				handler.RemoveCallbacksAndMessages(null);
				try {
					service.context.UnregisterReceiver(this);
				} catch (Exception) {
				}
				service.adapter.CancelDiscovery();
			}
		}

		public override void OnReceive(Context context, Intent intent) {
			switch (intent.Action) {
				case BluetoothDevice.ActionFound:
					var device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;
					ResolveDevice(device);
					break;
			}
		}

		private Task ResolveDevice(BluetoothDevice device) {
			return Task.Factory.StartNew(async () => {
/*
				var sn = await ClassicConnection.ResolveSerialNumber(device);
				if (sn != null) {
					service.PostDeviceFound(device, BluetoothDeviceType.Classic, null);
				}
*/
			});
		}
	}

	/// <summary>
	/// The scan delegate that is responsible for performing broadcasting. 
	/// </summary>
	internal class BroadcastScanDelegate : IScanDelegate {
		public bool isScanning { get { return scanDelegate.isScanning; } }

		private IScanDelegate scanDelegate;
		private Handler handler;
		private long interval;
		private long duration;
		private Context context;

		public BroadcastScanDelegate(IScanDelegate scanDelegate, Looper looper, long interval, long duration, Context context) {
			this.scanDelegate = scanDelegate;
			this.handler = new Handler(looper);
			this.interval = interval;
			this.duration = duration;
			this.context = context;
		}

		public bool StartScan() {
			Android.Widget.Toast.MakeText(context, "Starting Broadcast", Android.Widget.ToastLength.Short).Show();
			return scanDelegate.StartScan();
		}

		public void StopScan() {
			scanDelegate.StopScan();
			handler.RemoveCallbacksAndMessages(null);
		}

		private void ToggleScanMode() {
			if (isScanning) {
				scanDelegate.StopScan();
				handler.PostDelayed(ToggleScanMode, interval);
			} else {
				scanDelegate.StartScan();
				handler.PostDelayed(ToggleScanMode, duration);
			}
		}
	}
}
