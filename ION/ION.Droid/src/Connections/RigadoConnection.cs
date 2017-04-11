namespace ION.Droid.Connections {

	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Content;

	using Java.Util;

	using Appion.Commons.Util;

  public class RigadoConnection : BaseLeConnection {
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

    public RigadoConnection(Context context, BluetoothManager manager, BluetoothDevice device) : base(context, manager, device) {
		}

    // Overridden from BaseLeConnection
    protected override void OnDisconnect() {
      readCharacteristic = null;
      writeCharacteristic = null;
    }

    // Overridden from BaseLeConnection
		public override bool Write(byte[] packet) {
			if (!isConnected) {
				return false;
			}

			if (writeCharacteristic.SetValue(packet)) {
				return gatt.WriteCharacteristic(writeCharacteristic);
			} else {
				return false;
			}
		}

    // Overridden from BaseLeConnection
		public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

    // Overridden from BaseLeConnection
		public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

    // Overridden from BaseLeConnection
		protected override bool ValidateServices() {
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

    // Overridden from BaseLeConnection
    protected override bool OnConnectionSuccess() {
      if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
        Log.E(this, "Failed to set rigado read characteristic to notify");
        return false;
      }

      if (!readCharacteristicDescriptor.SetValue(new List<byte>(BluetoothGattDescriptor.EnableNotificationValue).ToArray())) {
        Log.E(this, "Failed to set notification to read descriptor");
        return false;
      }

      if (!gatt.WriteDescriptor(readCharacteristicDescriptor)) {
        Log.E(this, "Failed to write read notification descriptor");
        return false;
      }

      return base.OnConnectionSuccess();
    }
	}
}

