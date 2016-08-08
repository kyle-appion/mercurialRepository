namespace TestBench {

	using System;

	using CoreBluetooth;

	using ION.Core.Devices;

	public static class CBPeripheralExtensions {
		/// <summary>
		/// Attempts to determine the serial number of the peripheral device. If the device has a valid serial number, we
		/// will return that, otherwise, null.
		/// </summary>
		/// <returns>The appion device.</returns>
		/// <param name="peripheral">Peripheral.</param>
		public static ISerialNumber DetermineSerialNumber(this CBPeripheral peripheral) {
			if (SerialNumberExtensions.IsValidSerialNumber(peripheral.Name)) {
				return SerialNumberExtensions.ParseSerialNumber(peripheral.Name);
			} else {
				return null;
			}
		}
	}
}

