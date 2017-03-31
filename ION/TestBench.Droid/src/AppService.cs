using ION.Core.Devices;
namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.OS;
	using Android.Support.V4.Content;

	using Appion.Commons.Util;

	using ION.Core.Devices.Connections;

	[Service]
	public class AppService : Service {
		public static AppService INSTANCE { get; private set; }

		public event Action<AppService> onScanStateChanged;
		public event Action<IConnection> onConnectionFound;
		public event Action<IRig> onRigFound;

		public bool isScanning { get { return scanDelegate.isScanning; } }

		public BluetoothManager manager { get; private set; }
		public BluetoothAdapter adapter { get; private set; }

		public IScanDelegate scanDelegate { get; private set; }

		public ITest currentTest { get; set; }

		private Dictionary<string, IConnection> addressConnectionLookup = new Dictionary<string, IConnection>();
		private Dictionary<string, IRig> addressRigLookup = new Dictionary<string, IRig>();

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
			INSTANCE = this;
			Appion.Commons.Util.Log.printer = new LogPrinter();
			this.manager = GetSystemService(Context.BluetoothService) as BluetoothManager;
			this.adapter = manager.Adapter;

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				if (Permission.Granted == ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation)) {
					Log.D(this, "This user is using the 5.0 bluetooth api");
					scanDelegate = new Api21Scanner(manager.Adapter);
				} else {
					Log.D(this, "This user is using the 4.3 bluetooth api due to rejected permissions");
					scanDelegate = new Api18Scanner(manager.Adapter);
				}
			} else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
				Log.D(this, "This user is using the 4.3 bluetooth api due to versioning");
				scanDelegate = new Api18Scanner(manager.Adapter);
			} else {
				// TODO ahodder@appioninc.com: Catch and display user message
				// No good, the user's device cannot support le connections.
				throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
			}
//			scanDelegate = new Api21Scanner(adapter);
			scanDelegate.onBluetoothDeviceFound += OnBluetoothDeviceFound;

			return StartCommandResult.NotSticky;
		}

		public override IBinder OnBind(Intent intent) {
			return new Binder() { service = this };
		}

		public void StartScan() {
			if (!scanDelegate.isScanning) {
				scanDelegate.StartScan();
				NotifyScanStateChanged();
			}
		}

		public void StopScan() {
			scanDelegate.StopScan();
			NotifyScanStateChanged();
		}

		public List<IConnection> GetConnectionsOfModel(EDeviceModel deviceModel) {
			var ret = new List<IConnection>();

			foreach (var connection in addressConnectionLookup.Values) {
				if (connection.serialNumber.deviceModel == deviceModel) {
					ret.Add(connection);
				}
			}

			return ret;
		}

		public List<IRig> GetRigsOfType(ERigType rigType) {
			var ret = new List<IRig>();

			foreach (var rig in addressRigLookup.Values) {
				if (rig.rigType == rigType) {
					ret.Add(rig);
				}
			}

			return ret;
		}

		public IRig GetRigByAddress(string address) {
			IRig ret = null;

			addressRigLookup.TryGetValue(address, out ret);

			return ret;
		}

		public IConnection GetConnection(ISerialNumber serialNumber) {
			foreach (var connection in addressConnectionLookup.Values) {
				if (connection.serialNumber.Equals(serialNumber)) {
					return connection;
				}
			}

			return null;
		}

		public class Binder : Android.OS.Binder {
			public AppService service { get; internal set; }
		}

		private void OnBluetoothDeviceFound(BluetoothDevice device, byte[] advertisementPacket) {
			lock (addressConnectionLookup) {
				if (device.Name.IsValidSerialNumber()) {
					IConnection connection = null;
					if (!addressConnectionLookup.ContainsKey(device.Address)) {
						var sn = device.Name.ParseSerialNumber();

						if (sn.rawSerial.StartsWith("S", StringComparison.Ordinal)) {
							connection = new RigadoConnection(this, device, sn);
							addressConnectionLookup[device.Address] = connection;
							NotifyNewConnection(connection);
						} else {
							connection = new LeConnection(this, device, sn);
							addressConnectionLookup[device.Address] = connection;
							NotifyNewConnection(connection);
						}
					} else {
						connection = addressConnectionLookup[device.Address];
					}

//					connection.ReceivePacket(advertisementPacket);
				} else if (device.Name.IsValidRigIdentifier()) {
					ERigType rigType;
					if (device.Name.TryGetRigType(out rigType)) {
						IRig rig;
						if (!addressRigLookup.TryGetValue(device.Address, out rig)) {
							switch (rigType) {
								case ERigType.Vacuum:
									rig = new VacuumRig(this, device);
									addressRigLookup[device.Address] = rig;
									break;
								case ERigType.Pressure:
//									addressRigLookup[device.Address] = rig = new PressureRig(this, device);
									break;
							}

							NotifyNewRig(rig);
						}
					} else {
						Log.E(this, "Failed to find rig type for: " + device.Name);
					}
					
				} else {
					// The check if the advertisement packet has a serial number.
					ISerialNumber sn;
					byte[] packet;
					if (RigadoBroadcastParser.ParseBroadcastPacket(advertisementPacket, out sn, out packet)) {
						foreach (var c in addressConnectionLookup.Values) {
							if (c.serialNumber.Equals(sn)) {
								c.ReceivePacket(packet);
							}
						}
					} else {
						Log.D(this, "Invalid ION device");
					}
				}
			}
		}

		private void NotifyScanStateChanged() {
			if (onScanStateChanged != null) {
				onScanStateChanged(this);
			}
		}

		private void NotifyNewConnection(IConnection connection) {
			if (onConnectionFound != null) {
				onConnectionFound(connection);
			}
		}

		private void NotifyNewRig(IRig rig) {
			if (onRigFound != null) {
				onRigFound(rig);
			}
		}
	}


	public delegate void OnBluetoothDeviceFound(BluetoothDevice device, byte[] advertisementPacket);
	public interface IScanDelegate {
		event OnBluetoothDeviceFound onBluetoothDeviceFound;
		bool isScanning { get; }

		void StartScan();
		void StopScan();
	}

	internal class Api21Scanner : ScanCallback, IScanDelegate {
		public event OnBluetoothDeviceFound onBluetoothDeviceFound;

		public bool isScanning { get; private set; }

		private BluetoothAdapter adapter;

		public Api21Scanner(BluetoothAdapter adapter) {
			this.adapter = adapter;
		}

		public void StartScan() {
			isScanning = true;
			adapter.BluetoothLeScanner.StartScan(this);
		}

		public void StopScan() {
			adapter.BluetoothLeScanner.StopScan(this);
			isScanning = false;
		}

		public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
			base.OnBatchScanResults(results);
		}

		public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
			if (onBluetoothDeviceFound != null) {
				onBluetoothDeviceFound(result.Device, result.ScanRecord.GetBytes());
			}
		}

		public override void OnScanFailed(ScanFailure errorCode) {
			base.OnScanFailed(errorCode);
		}
	}

	internal class Api18Scanner : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IScanDelegate {
		// Implemented from IScanDelegate
		public bool isScanning { get; private set; }
		public event OnBluetoothDeviceFound onBluetoothDeviceFound;


		private BluetoothAdapter adapter;
		private Handler handler;

		public Api18Scanner(BluetoothAdapter adapter) {
			this.adapter = adapter;
			this.handler = new Handler(Looper.MainLooper);
		}

		public void StartScan() {
			if (isScanning) {
				return;
			}

			try {
				var ret = adapter.StartLeScan(this);
				isScanning = true;
			} catch (Exception e) {
				Log.E(this, "Wtf?", e);
			}
		}

		public void StopScan() {
			if (isScanning) {
				adapter.StopLeScan(this);
			}
			isScanning = false;
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
			handler.Post(() => {
				if (onBluetoothDeviceFound != null) {
					onBluetoothDeviceFound(device, scanRecord);
				}
			});
		}
	}
}
