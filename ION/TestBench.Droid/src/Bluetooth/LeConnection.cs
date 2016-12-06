namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;

	using Java.Util;

	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
	using ION.Core.Util;

	public class LeConnection : BluetoothGattCallback, IConnection {
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

		// Implemented from IConnection
		public event OnNewPacket onNewPacket;
		// Implemented from IRig
		public event Action<IConnection> onConnectionStateChanged;

		public bool isConnected { get { return state == ProfileState.Connected; } }
		public ProfileState state { get { return service.manager.GetConnectionState(device, ProfileType.Gatt); } }
		public ISerialNumber serialNumber { get; private set; }
		public GaugePacket lastPacket { 
			get {
				return __lastPacket;
			}
			private set {
				__lastPacket = value;
				if (onNewPacket != null) {
					onNewPacket(this, __lastPacket);
				}
			}
		} GaugePacket __lastPacket;

		/// <summary>
		/// The context that is needed to create modern bluetooth connections.
		/// </summary>
		private AppService service;
		/// <summary>
		/// The android native bluetooth device that the connection is wrapping communications for.
		/// </summary>
		private BluetoothDevice device;
		/// <summary>
		/// The protocol that will parse the data that the connection received from its gauge.
		/// </summary>
		private IGaugeProtocol protocol;
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

		public LeConnection(AppService service, BluetoothDevice device, ISerialNumber serialNumber) {
			this.service = service;
			this.device = device;
			this.serialNumber = serialNumber;
			this.protocol = Protocol.FindProtocolFromVersion(EProtocolVersion.V1);
		}

		public void Connect() {
			lock (this) {
				if (gatt == null) {
					gatt = device.ConnectGatt(service, false, this);
				}
			}
		}

		public void Disconnect() {
			lock (this) {
				if (gatt != null) {
					gatt.Disconnect();
					gatt.Close();

					if (onConnectionStateChanged != null) {
						onConnectionStateChanged(this);
					}
				}

				gatt = null;

			}
		}

		public void ReceivePacket(byte[] packet) {
			try {
				lastPacket = protocol.ParsePacket(packet);
			} catch (Exception e) {
				Log.E(this, "Failed to parse packet: " + packet.ToByteString(), e);
			}
		}

		public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
			if (characteristic.Equals(readCharacteristic)) {
				ReceivePacket(characteristic.GetValue());
			}
		}

		public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
			if (characteristic.Equals(readCharacteristic)) {
				ReceivePacket(characteristic.GetValue());
			}
		}

		public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
			switch (newState) {
				case ProfileState.Connected:
					gatt.DiscoverServices();
				break;
				case ProfileState.Connecting:
				break;
				case ProfileState.Disconnecting:
					Disconnect();
				break;
				case ProfileState.Disconnected:
					Disconnect();
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
