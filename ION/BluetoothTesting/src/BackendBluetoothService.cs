namespace BluetoothTesting {

	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;
	using Android.Util;

	[Service]
	public class BackendBluetoothService : Service {

		public const string ACTION_START_SCAN = "BackendBluetoothService.ActionStartScan";
		public const string ACTION_STOP_SCAN = "BackendBluetoothService.ActionStopScan";
		public const string ACTION_SCAN_STARTED = "BackendBluetoothService.ActionScanStarted";
		public const string ACTION_SCAN_STOPPED = "BackendBluetoothService.ActionScanStopped";
		public const string ACTION_DEVICE_FOUND = "BackendBluetoothService.ActionDeviceFound";

		public event Action<BackendBluetoothService> onScanStarted;
		public event Action<BackendBluetoothService> onScanStopped;
		public event Action<BackendBluetoothService, BluetoothDevice> onDeviceFound;
		public event Action<BluetoothDevice> onDeviceChanged;

		public BluetoothManager manager { get; private set; }
		public BluetoothAdapter adapter { get; private set; }
		public bool isScanning { get; private set; }
		public ConnectionHandler connectionHandler { get; private set; }

		private Dictionary<string, BluetoothDevice> deviceLookup = new Dictionary<string, BluetoothDevice>();

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
			base.OnStartCommand(intent, flags, startId);

			manager = GetSystemService(BluetoothService) as BluetoothManager;
			adapter = manager.Adapter;
			connectionHandler = new ConnectionHandler(this);
			connectionHandler.onDeviceStateChange += OnDeviceStateChange;
			connectionHandler.onDeviceFound += OnDeviceFound;

			D("Started BluetoothService");

			return StartCommandResult.NotSticky;
		}

		public override IBinder OnBind(Intent intent) {
			return new Binder() { service = this };
		}

		public override void OnDestroy() {
			base.OnDestroy();
		}

		public void OnConnected(Bundle bundle) {
		}

		public void OnDisconnected() {
		}

		public void OnConnectionSuspended(int i) {
		}

		public void StartScan() {
			if (isScanning) {
				return;
			}
			connectionHandler.StartScan();
			isScanning = true;
			if (onScanStarted != null) {
				onScanStarted(this);
			}
		}

		public void StopScan() {
			connectionHandler.StopScan();
			isScanning = false;
			if (onScanStopped != null) {
				onScanStopped(this);
			}
		}

		private void OnDeviceStateChange(BluetoothDevice device, ProfileState state) {
			if (onDeviceChanged != null) {
				onDeviceChanged(device);
			}
		}

		private void OnDeviceFound(BluetoothDevice device) {
			if (!deviceLookup.ContainsKey(device.Address)) {
				deviceLookup[device.Address] = device;

				if (onDeviceFound != null) {
					onDeviceFound(this, device);
				}
			}
		}

		public static void D(string msg) {
			Log.Debug(typeof(BackendBluetoothService).Name, msg);
		}

		public class Binder : Android.OS.Binder {
			public BackendBluetoothService service { get; internal set; }
		}
	}
}




/*
namespace BluetoothTesting {

	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;
	using Android.Util;

	[Service]
	public class BackendBluetoothService : Service {

		public const string ACTION_START_SCAN = "BackendBluetoothService.ActionStartScan";
		public const string ACTION_STOP_SCAN = "BackendBluetoothService.ActionStopScan";
		public const string ACTION_SCAN_STARTED = "BackendBluetoothService.ActionScanStarted";
		public const string ACTION_SCAN_STOPPED = "BackendBluetoothService.ActionScanStopped";
		public const string ACTION_DEVICE_FOUND = "BackendBluetoothService.ActionDeviceFound";

		public event Action<BackendBluetoothService> onScanStarted;
		public event Action<BackendBluetoothService> onScanStopped;
		public event Action<BackendBluetoothService, BluetoothConnection> onDeviceFound;
		public event Action<BluetoothConnection> onConnectionChanged;

		public BluetoothManager manager { get; private set; }
		public BluetoothAdapter adapter { get; private set; }
		public bool isScanning { get; private set; }


		private LeScanDelegate_4_3 scanDelegate;

		private Handler handler;
		private Dictionary<string, BluetoothConnection> connectionLookup = new Dictionary<string, BluetoothConnection>();
		private List<BluetoothConnection> connections = new List<BluetoothConnection>();

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
			base.OnStartCommand(intent, flags, startId);

			handler = new Handler();
			manager = GetSystemService(BluetoothService) as BluetoothManager;
			adapter = manager.Adapter;
			scanDelegate = new LeScanDelegate_4_3(this, adapter);

			D("Started BluetoothService");

			return StartCommandResult.NotSticky;
		}

		public override IBinder OnBind(Intent intent) {
			return new Binder() { service = this };
		}

		public override void OnDestroy() {
			base.OnDestroy();
		}

		public void OnConnected(Bundle bundle) {
		}

		public void OnDisconnected() {
		}

		public void OnConnectionSuspended(int i) {
		}

		public void StartScan() {
			if (isScanning) {
				return;
			}
			scanDelegate.StartScan();
			isScanning = true;
			NotifyScanStarted();
		}

		public void StopScan() {
			scanDelegate.StopScan();
			isScanning = false;
			NotifyScanStopped();
		}

		private void NotifyScanStarted() {
			if (onScanStarted != null) {
				onScanStarted(this);
			}
		}

		private void NotifyScanStopped() {
			if (onScanStopped != null) {
				onScanStopped(this);
			}
		}

		private void NotifyDeviceFound(BluetoothDevice device) {
			if (!connectionLookup.ContainsKey(device.Address)) {
				var conn = new BluetoothConnection(this, device);
				conn.onConnectionChanged += NotifyConnectionChanged;
				connectionLookup[device.Address] = conn;
				connections.Add(conn);

				if (onDeviceFound != null) {
					onDeviceFound(this, conn);
				}
			}
		}

		private void NotifyConnectionChanged(BluetoothConnection connection) {
			if (onConnectionChanged != null) {
				onConnectionChanged(connection);
			}
		}

    public static void D(string msg) {
			Log.Debug(typeof(BackendBluetoothService).Name, msg);
		}

		public class Binder : Android.OS.Binder {
			public BackendBluetoothService service { get; internal set; }
		}

		internal class LeScanDelegate_4_3 : Java.Lang.Object, Android.Bluetooth.BluetoothAdapter.ILeScanCallback {
			private BackendBluetoothService service;
			private BluetoothAdapter adapter;

			public LeScanDelegate_4_3(BackendBluetoothService service, BluetoothAdapter adapter) {
				this.service = service;
				this.adapter = adapter;
			}

			public void StartScan() {
				adapter.StartLeScan(this);
			}

			public void StopScan() {
				try {
					adapter.StopLeScan(this);
				} catch (Exception e) {
					D("The service scan has already been stopped.");
				}
			}

			public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
				service.NotifyDeviceFound(device);
			}
		}
	}
}
*/