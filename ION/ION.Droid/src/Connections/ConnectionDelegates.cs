namespace ION.Droid.Connections {

	using Android.Bluetooth;

	using ION.Core.Devices;

	internal delegate void InternalClassicDeviceFound(ISerialNumber serialNumber, BluetoothDevice device);
	internal delegate void InternalDeviceFound(BluetoothDevice device, byte[] scanRecord);
}

