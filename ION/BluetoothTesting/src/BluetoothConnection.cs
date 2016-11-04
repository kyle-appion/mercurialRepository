namespace BluetoothTesting {


	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Bluetooth.LE;
	using Android.Content;
	using Android.OS;
	using Android.Util;


	public class BluetoothConnection : BluetoothGattCallback {

		public event Action<BluetoothConnection> onConnectionChanged;
		public event Action<BluetoothConnection> onNewData;

		public BluetoothDevice device { get; private set; }
		public ProfileState connectionState { get; private set; }

		private BackendBluetoothService service;
		private BluetoothGatt gatt;

		public BluetoothConnection(BackendBluetoothService service, BluetoothDevice device) {
			this.service = service;
			this.device = device;
		}

		public bool Connect() {
			try {
				gatt = device.ConnectGatt(service, false, this);
				return true;
			} catch (Exception e) {
				E("Failed to acquire gatt.", e);
				return false;
			}
		}

		public void Disconnect() {
			if (gatt != null) {
				gatt.Disconnect();
				gatt.Close();
			}

			connectionState = ProfileState.Disconnected;
			NotifyConnectionChanged();
		}

		public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
			connectionState = newState;

			switch (newState) {
				case ProfileState.Connected:
					break;
				case ProfileState.Connecting:
					break;
				case ProfileState.Disconnected:
					Disconnect();
					break;
				case ProfileState.Disconnecting:
					Disconnect();
					break;
			}

			NotifyConnectionChanged();
		}

		private void NotifyConnectionChanged() {
			if (onConnectionChanged != null) {
				onConnectionChanged(this);
			}
		}

		public static void E(string msg, Exception e=null) {
			Log.Error(typeof(BluetoothConnection).Name, msg, e);
		}
	}
}
