namespace ION.Droid.Connections {

	using Android.Bluetooth;

	internal delegate void InternalDeviceFound(BluetoothDevice device, byte[] scanRecord);
}

