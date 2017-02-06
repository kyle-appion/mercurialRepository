namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.Bluetooth;
	using Android.Content;
	using Android.OS;

	using Java.Util;

	using Appion.Commons.Util;

	// Using ION
	using Core.Connections;


	public class LeConnection : BluetoothGattCallback, IConnection, Handler.ICallback {
		/// <summary>
		/// A utility method used to create the UUIDs necessary for use for the
		/// bluetooth api.
		/// </summary>
		/// <param name="content">Content.</param>
		private static UUID Inflate(string content) {
			return UUID.FromString(BASE_UUID.Replace("****", content));
		}

		/// <summary>
		/// The base uuid that identifies services, characteristics and descriptors
		/// within the bluetooth gatt api.
		/// </summary>
		private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
		/// <summary>
		/// The UUID for the read service.
		/// </summary>
		private static readonly UUID READ_SERVICE = Inflate("ffe0");
		/// <summary>
		/// The UUID for the read characteristic.
		/// </summary>
		private static readonly UUID READ_CHARACTERISTIC = Inflate("ffe4");
		/// <summary>
		/// The UUID for the write service.
		/// </summary>
		private static readonly UUID WRITE_SERVICE = Inflate("ffe5");
		/// <summary>
		/// The UUID for the write characteristic.
		/// </summary>
		/// <param name="content">Content.</param>
		private static readonly UUID WRITE_CHARACTERISTIC = Inflate("ffe9");
		/// <summary>
		/// The default time that is allowed for a rigado connection attempt.
		/// </summary>
		private static readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(45);
		/// <summary>
		/// The delay from a disconnecto to a reconnect attempt.
		/// </summary>
		private const long RECONNECT_DELAY = 3000;

		/// <summary>
		/// The message that is used when a connection of other attempt 
		/// </summary>
		private const int MSG_TIMEOUT = -1;
		/// <summary>
		/// The message that is used when a connection needs to append a reconnect attempt.
		/// </summary>
		private const int MSG_RECONNECT = 1;

		public event OnConnectionStateChanged onStateChanged;

		public event OnDataReceived onDataReceived;

		public EConnectionState connectionState {
			get {
				return __connectionState;
			}
			private set {
				var oldState = __connectionState;
				__connectionState = value;
				Log.D(this, "Connection state is : " + value);
				if (onStateChanged != null) {
					onStateChanged(this, oldState);
				}
			}
		} EConnectionState __connectionState;

		public bool isConnected { get { return connectionState == EConnectionState.Connected; } }

		public string name { get { return device.Name; } }

		public string address { get { return device.Address; } }

		public ESignalStrength signalStrength { get; }

		public byte[] lastPacket {
			get {
				return __lastPacket;
			}
			set {
				__lastPacket = value;
				lastSeen = DateTime.Now;
				if (onDataReceived != null) {
					onDataReceived(this, __lastPacket);
				}
			}
		} byte[] __lastPacket;

		public DateTime lastSeen { get; set; }

		public object nativeDevice { get { return device; } }

		public TimeSpan connectionTimeout { get; set; }

		/// <summary>
		/// The context that is needed to create modern bluetooth connections.
		/// </summary>
		private Context context;
		/// <summary>
		/// The manager that is necessary to glean details about a specific bluetooth device.
		/// </summary>
		private BluetoothManager manager;
		/// <summary>
		/// The android native bluetooth device that the connection is wrapping communications for.
		/// </summary>
		private BluetoothDevice device;
		/// <summary>
		/// The bluetooth gatt that the connection is using to interface with the bluetooth module.
		/// </summary>
		private BluetoothGatt gatt;
		/// <summary>
		/// The characteristic that is used to read from the bluetooth device.
		/// </summary>
		private BluetoothGattCharacteristic readCharacteristic;
		/// <summary>
		/// The characteristic that is used to write to the bluetooth device.
		/// </summary>
		private BluetoothGattCharacteristic writeCharacteristic;
		/// <summary>
		/// The handler that is responsible for synchronizing and orchestrating connection events.
		/// </summary>
		private Handler handler;
		/// <summary>
		/// The number of attempted reconnects that the handler has performed.
		/// </summary>
		private int reconnectAttempts;

		public LeConnection(Context context, BluetoothManager manager, BluetoothDevice device) {
			this.context = context;
			this.manager = manager;
			this.device = device;
			connectionState = EConnectionState.Disconnected;
			connectionTimeout = DEFAULT_TIMEOUT;
			lastSeen = DateTime.FromFileTimeUtc(0);
			handler = new Handler(this);
			reconnectAttempts = 0;
		}

		public Task<bool> ConnectAsync() {
			lock (this) {
				if (connectionState != EConnectionState.Disconnected || !manager.Adapter.IsEnabled) {
					return Task.FromResult(false);
				} else {
					connectionState = EConnectionState.Connecting;
				}
			}

			var start = DateTime.Now;

			gatt = device.ConnectGatt(context, false, this);

			if (gatt == null) {
				connectionState = EConnectionState.Disconnected;
				return Task.FromResult(false);
			} else {
				return Task.FromResult(true);
			}
		}

		public void Disconnect() {
			handler.RemoveMessages(MSG_RECONNECT);

			if (gatt != null) {
				gatt.Disconnect();
				gatt.Close();
			}

			readCharacteristic = null;
			writeCharacteristic = null;

			lastSeen = DateTime.Now;
			connectionState = EConnectionState.Disconnected;
		}

		public bool Write(byte[] packet) {
			if (!isConnected) {
				return false;
			}

			if (writeCharacteristic.SetValue(packet)) {
				return gatt.WriteCharacteristic(writeCharacteristic);
			} else {
				return false;
			}
		}

		public bool HandleMessage(Message msg) {
			switch (msg.What) {
				case MSG_TIMEOUT:
					Disconnect();
					return true;
				case MSG_RECONNECT:
					if (reconnectAttempts < 2) {
						ConnectAsync();
					} else {
						Log.D(this, "Failed to reconnect too many times. Stopping");
						reconnectAttempts = 0;
					}
					return true;
				default:
					return false;
			}
		}

		public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

		public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

		public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
			Log.D(this, "The Rigado device is in state: " + newState);
			switch (newState) {
				case ProfileState.Connected:
					connectionState = EConnectionState.Resolving;
					lastSeen = DateTime.Now; // We at least know that the device is nearby at this point.

					if (!gatt.DiscoverServices()) {
						Log.E(this, "Failed to start service discovery for: " + device.Name);
						Disconnect();
					} else {
						reconnectAttempts = 0;
						connectionState = EConnectionState.Resolving;
						handler.SendMessageDelayed(handler.ObtainMessage(MSG_TIMEOUT, 0, 0), (long)connectionTimeout.TotalMilliseconds);
					}
					break;
				case ProfileState.Connecting:
					break;
				case ProfileState.Disconnected:
					DoUnexpectedDisconnect();
					break;
				case ProfileState.Disconnecting:
					DoUnexpectedDisconnect();
					break;
			}
		}

		public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
			if (ValidateServices()) {
				if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
					Log.E(this, "Failed to set rigado read characteristic to notify");
					Disconnect();
					return;
				}

				handler.RemoveMessages(MSG_TIMEOUT);
				connectionState = EConnectionState.Connected;
			} else {
				Log.D(this, "Failed to discover services. Trying again...");
				connectionState = EConnectionState.Resolving;
				gatt.DiscoverServices();
			}
		}

		/// <summary>
		/// Attempts to heal a connection on an unexpected disconnect.
		/// </summary>
		private void DoUnexpectedDisconnect() {
			if (connectionState == EConnectionState.Disconnected) {
				return;
			}

			Disconnect();

			if (!handler.HasMessages(MSG_RECONNECT)) {
				reconnectAttempts++;
				handler.SendEmptyMessageDelayed(MSG_RECONNECT, RECONNECT_DELAY);
			}
		}

		/// <summary>
		/// Checks the gatt object to see if we have successfully validated the services. If the gatt object is null, then
		/// obviously the services are not valid.
		/// </summary>
		/// <returns>The services.</returns>
		private bool ValidateServices() {
			if (gatt == null) {
				return false;
			}

			readCharacteristic = null;
			writeCharacteristic = null;

			var readService = gatt.GetService(READ_SERVICE);
			if (readService != null) {
				readCharacteristic = readService.GetCharacteristic(READ_CHARACTERISTIC);
			}

			var writeService = gatt.GetService(WRITE_SERVICE);
			if (writeService != null) {
				writeCharacteristic = writeService.GetCharacteristic(WRITE_CHARACTERISTIC);
			}

			return readCharacteristic != null && writeCharacteristic != null;
		}
	}
}

