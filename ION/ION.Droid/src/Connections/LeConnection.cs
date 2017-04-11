namespace ION.Droid.Connections {

	using Android.Bluetooth;
	using Android.Content;

	using Java.Util;

	using Appion.Commons.Util;

  public class LeConnection : BaseLeConnection {
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
		private static readonly UUID WRITE_CHARACTERISTIC = Inflate("ffe9");

    /// <summary>
    /// The characteristic that is used to read from the bluetooth device.
    /// </summary>
    private BluetoothGattCharacteristic readCharacteristic;
    /// <summary>
    /// The characteristic that is used to write to the bluetooth device.
    /// </summary>
    private BluetoothGattCharacteristic writeCharacteristic;

    public LeConnection(Context context, BluetoothManager manager, BluetoothDevice device) : base(context, manager, device) {
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

    // Overridden from BaseLeConnection
    protected override bool OnConnectionSuccess() {
      if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
        Log.E(this, "Failed to set rigado read characteristic to notify");
        return false;
      }

      return base.OnConnectionSuccess();
    }
	}
}

