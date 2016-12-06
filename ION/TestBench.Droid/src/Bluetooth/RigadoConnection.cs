namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;

	using Java.Util;

	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
	using ION.Core.Util;

	public class RigadoConnection : BluetoothGattCallback, IConnection {
		/// <summary>
		/// The UUID that is used to identify services and characteristics within the ridago BLE connection.
		/// </summary>
		private const string RIGADO_UUID = "6E40****-B5A3-F393-E0A9-E50E24DCCA9E";
		/// <summary>
		/// The uuid that is used to identify the read service for the rigado connection.
		/// </summary>
		private static readonly UUID BASE_SERVICE = UUID.FromString(RIGADO_UUID.Replace("****", "0001"));
		/// <summary>
		/// The uuid that is used to identify the read characterstistic for the rigado connection.
		/// </summary>
		private static readonly UUID READ_CHARACTERISTIC = UUID.FromString(RIGADO_UUID.Replace("****", "0003"));
		/// <summary>
		/// The descriptor for the read characteristic.
		/// </summary>
		private static readonly UUID READ_CHARACTERISTIC_DESCRIPTOR = UUID.FromString("00002902-0000-1000-8000-00805f9b34fb");
		/// <summary>
		/// The uuid that is used to identify the write characteristic for the rigado connection.
		/// </summary>
		private static readonly UUID WRITE_CHARACTERISTIC = UUID.FromString(RIGADO_UUID.Replace("****", "0002"));

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
				onNewPacket(this, __lastPacket);
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
		/// The descriptor of the read characterisitic. This is necessary to set notify.
		/// </summary>
		private BluetoothGattDescriptor readCharacteristicDescriptor;
		/// <summary>
		/// The characteristic that is used to write to the bluetooth device.
		/// </summary>
		private BluetoothGattCharacteristic writeCharacteristic;

		public RigadoConnection(AppService service, BluetoothDevice device, ISerialNumber serialNumber) {
			this.service = service;
			this.device = device;
			this.serialNumber = serialNumber;
			this.protocol = Protocol.FindProtocolFromVersion(EProtocolVersion.V4);
		}

		public void Connect() {
			if (gatt == null) {
				gatt = device.ConnectGatt(service, false, this);
			}
		}

		public void Disconnect() {
			if (gatt != null) {
				gatt.Disconnect();
				gatt.Close();

				if (onConnectionStateChanged != null) {
					onConnectionStateChanged(this);
				}
			}

			gatt = null;
		}

		public void ReceivePacket(byte[] packet) {
			try {
				lastPacket = protocol.ParsePacket(packet);
			} catch (Exception e) {
				Log.E(this, "Failed to parse packet: " + packet.ToByteString());
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

				if (!readCharacteristicDescriptor.SetValue(new List<byte>(BluetoothGattDescriptor.EnableNotificationValue).ToArray())) {
					Log.E(this, "Failed to set notification to read descriptor");
					Disconnect();
					return;
				}

				if (!gatt.WriteDescriptor(readCharacteristicDescriptor)) {
					Log.E(this, "Failed to write read notification descriptor");
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

			var baseService = gatt.GetService(BASE_SERVICE);
			if (baseService != null) {
				readCharacteristic = baseService.GetCharacteristic(READ_CHARACTERISTIC);

				if (readCharacteristic != null) {
					readCharacteristicDescriptor = readCharacteristic.GetDescriptor(READ_CHARACTERISTIC_DESCRIPTOR);
				}

				writeCharacteristic = baseService.GetCharacteristic(WRITE_CHARACTERISTIC);
			}

			return readCharacteristic != null && readCharacteristicDescriptor != null && writeCharacteristic != null;
		}
	}
}
