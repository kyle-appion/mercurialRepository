namespace ION.Droid.Devices {

  using System;

  using Android.Bluetooth;

  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

	public static class DeviceManagerExtensions {
    /// <summary>
    /// Creates a new device using the scan record to poll the device's serial number and version.
    /// </summary>
    /// <returns>The ble device.</returns>
    /// <param name="deviceManager">Device manager.</param>
    /// <param name="address">Address.</param>
    /// <param name="scanRecord">Scan record.</param>
    public static IDevice CreateBleDeviceOrThrow(this IDeviceManager deviceManager, BluetoothDevice device, byte[] scanRecord) {
      ISerialNumber sn;
			if (!ResolveSerialNumber(device, scanRecord, out sn)) {
        // We could not resolve the device (or it is not ours)
        throw new Exception("Could not validate serial number for: " + device.Name + " @ " + device.Address);
			}

      var ret = deviceManager[sn];

      if (ret != null) {
        ret.connection.lastPacket = scanRecord;
      } else {
				var pv = ResolveProtocolVersion(device, sn, scanRecord);
        ret = deviceManager.CreateDevice(sn, device.Address, pv);
        ret.connection.lastPacket = scanRecord;
			}

      return ret;
    }

    /// <summary>
    /// Attempts to resolve a serial number from either the connection name of the scan record.
    /// </summary>
    /// <returns><c>true</c>, if serial number was resolved, <c>false</c> otherwise.</returns>
    /// <param name="scanRecord">Scan record.</param>
    /// <param name="sn">Sn.</param>
    private static bool ResolveSerialNumber(BluetoothDevice device, byte[] scanRecord, out ISerialNumber sn) {
			if (device.Name.IsValidSerialNumber()) {
				sn = device.Name.ParseSerialNumber();
				return true;
			} else if (scanRecord != null) {
				var ussn = System.Text.Encoding.UTF8.GetString(scanRecord, 0, 8);
				if (ussn.IsValidSerialNumber()) {
					sn = ussn.ParseSerialNumber();
					// We need to format the scan record.
					var buffer = new byte[20];
					Array.Copy(scanRecord, 8, buffer, 0, buffer.Length);
					Array.Clear(scanRecord, 0, scanRecord.Length);
					Array.Copy(buffer, scanRecord, buffer.Length);
					return true;
				} else {
					sn = null;
					return false;
				}
			} else {
				sn = null;
				return false;
			}
    }

		/// <summary>
		/// Attempts to resolve the protocol version for the device.
		/// </summary>
		/// <returns>The protocol version.</returns>
		/// <param name="device">Device.</param>
		/// <param name="sn">Sn.</param>
		/// <param name="scanRecord">Scan record.</param>
		private static EProtocolVersion ResolveProtocolVersion(BluetoothDevice device, ISerialNumber sn, byte[] scanRecord) {
			if (scanRecord != null && scanRecord[0] == 4) {
				return EProtocolVersion.V4;
			} else {
				// TODO ahodder@appioninc.com: We need a better way to determine a device's protocol.
				if (sn.rawSerial.Length == 8) {
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
