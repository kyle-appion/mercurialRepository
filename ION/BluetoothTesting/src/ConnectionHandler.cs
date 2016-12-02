namespace BluetoothTesting {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.Content;
	using Android.OS;

	public delegate void OnBluetoothDeviceStateChange(BluetoothDevice device, ProfileState state);
	public delegate void OnDeviceFound(BluetoothDevice device);

	public class ConnectionHandler : Android.Bluetooth.BluetoothGattCallback, BluetoothAdapter.ILeScanCallback {
		public event OnBluetoothDeviceStateChange onDeviceStateChange;
		public event OnDeviceFound onDeviceFound;

		private BackendBluetoothService service;
		private Handler handler;

		private Dictionary<string, BluetoothGatt> connections = new Dictionary<string, BluetoothGatt>();

		public ConnectionHandler(BackendBluetoothService service) {
			this.service = service;
			this.handler = new Handler();
		}

		public void StartScan() {
			service.adapter.StartLeScan(this);
		}

		public void StopScan() {
			service.adapter.StopLeScan(this);
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
			if (onDeviceFound != null) {
				onDeviceFound(device);
			}
		}

		public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
			switch (newState) {
				case ProfileState.Connected:
					gatt.DiscoverServices();
					break;
				case ProfileState.Connecting:
					break;
				case ProfileState.Disconnected:
					Disconnect(gatt.Device);
					break;
				case ProfileState.Disconnecting:
					Disconnect(gatt.Device);
					break;
			}

			// Finally, notify that the device state has changed.
			NotifyOfChange(gatt.Device, newState);
		}

		public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
		}

		public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
		}

		public bool Connect(BluetoothDevice device) {
			if (GetProfileStateFor(device) == ProfileState.Disconnected) {
				var gatt = device.ConnectGatt(service, false, this);
				connections[device.Address] = gatt;
				return true;
			} else {
				return false;
			}
		}

		public void Disconnect(BluetoothDevice device) {
			BluetoothGatt gatt = null;
			if (connections.TryGetValue(device.Address, out gatt)) {
				gatt.Disconnect();
				gatt.Close();
			}

			connections.Remove(device.Address);
		}

		public ProfileState GetProfileStateFor(BluetoothDevice device) {
			return service.manager.GetConnectionState(device, ProfileType.Gatt);
		}

		private void NotifyOfChange(BluetoothDevice device, ProfileState state) {
			if (onDeviceStateChange != null) {
				onDeviceStateChange(device, state);
			}
		}
	}
}
