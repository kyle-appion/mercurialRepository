namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;
	using System.IO;

	using Android.Bluetooth;

	using Java.Util;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	public delegate void OnNewVrcReading(VacuumRig controller, Scalar lastMeas, Scalar newMeas);
	public class VacuumRig : BluetoothGattCallback, IRig {

		public enum EVrcRigCommand {
			Idle = 1,
			Reset = 2,
			Test = 3,
		}

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

		private static readonly UUID SERVICE = Inflate("ff00"); 
		private static readonly UUID WRITE = Inflate("ff01"); 
		private static readonly UUID READ = Inflate("ff02"); 

		// Implemented from IRig
		public event Action<IRig> onConnectionStateChanged;
		/// <summary>
		/// The event that will notify listeners of new received VRC readings.
		/// </summary>
		public event OnNewVrcReading onNewVrcReading;

		// Implemented from IRig
		public ERigType rigType { get { return ERigType.Vacuum; } }
		// Implemented from IRig
		public bool isConnected { get { return service.manager.GetConnectionState(device, ProfileType.Gatt) == ProfileState.Connected; } }
		// Implemented from IRig
		public string address { get { return device.Address; } }

		public ProfileState state { get { return service.manager.GetConnectionState(device, ProfileType.Gatt); } }
		public Scalar vrcMeasurement;
		public Scalar rigAngle;

		private AppService service;
    private BluetoothDevice device;
		private BluetoothGatt gatt;

		private BluetoothGattCharacteristic readCharacteristic;
		private BluetoothGattCharacteristic writeCharacteristic;

		public VacuumRig(AppService service, BluetoothDevice device) {
			this.service = service;
			this.device = device;
		}

		// Implemented from IRig
		public void Connect() {
			if (gatt != null) {
				return;
			}

			gatt = device.ConnectGatt(service, false, this);
		}

		// Implemented from IRig
		public void Disconnect() {
			try {
				if (gatt != null) {
					gatt.Disconnect();
					gatt.Close();
					NotifyConnectionState();
				}

				gatt = null;
			} catch (Exception e) {
				Log.E(this, "Failed to disconnect", e);
			}
		}

		public void WriteCommand(EVrcRigCommand command) {
			if (gatt == null) {
				return;
			}

			var ms = new MemoryStream(20);
			using (var writer = new BinaryWriter(ms)) {
				writer.Write((byte)command);
			}

			writeCharacteristic.SetValue(ms.ToArray());
			gatt.WriteCharacteristic(writeCharacteristic);
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
				var d = readCharacteristic.Descriptors[0];

				if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
					Log.E(this, "Failed to set the read characteristic to notify.");
					Disconnect();
					return;
				} 

				if (d.SetValue(new List<byte>(BluetoothGattDescriptor.EnableNotificationValue).ToArray())) {
					if (!gatt.WriteDescriptor(d)) {
						Log.E(this, "Failed to set notification to read descriptor");
						Disconnect();
						return;
					}
				}

				NotifyConnectionState();
				vrcMeasurement = Units.Vacuum.MICRON.OfScalar(600000);
				rigAngle = Units.Angle.DEGREE.OfScalar(90);
			}
		}

		private void NotifyConnectionState() {
			if (onConnectionStateChanged != null) {
				onConnectionStateChanged(this);
			}
		}
/*
		private void UpdateMockData() {
			var dm = (vrcMeasurement.amount < 10000) ? 100 : 1000;
			vrcMeasurement = Units.Vacuum.MICRON.OfScalar(Math.Max(vrcMeasurement.amount - dm, 0));
			rigAngle = Units.Angle.DEGREE.OfScalar(Math.Max(rigAngle.amount - 0.1, 0));

			var ms = new MemoryStream();
			using (var w = new BinaryWriter(ms)) {
				w.Write((byte)3);
				w.Write((int)vrcMeasurement.amount);
				w.Write((float)rigAngle.amount);
				w.Flush();
			}

			ReceivePacket(ms.ToArray());
		}
*/

		private void ReceivePacket(byte[] packet) {
			try {
				var lastMeas = vrcMeasurement;
				using (var reader = new BinaryReader(new MemoryStream(packet))) {
					var state = reader.ReadByte();
					var rawVrc = reader.ReadUInt32();
					var rawIca = reader.ReadSingle();
					vrcMeasurement = Units.Vacuum.MILLITORR.OfScalar(rawVrc);
					rigAngle = Units.Angle.DEGREE.OfScalar(rawIca);
				}

				if (onNewVrcReading != null) {
					onNewVrcReading(this, lastMeas, vrcMeasurement);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to update characteristic", e);
			}
		}

		/// <summary>
		/// Attempts to find and validate all the characteristic that are present in the controller.
		/// </summary>
		/// <returns><c>true</c>, if services was validated, <c>false</c> otherwise.</returns>
		private bool ValidateServices() {
			if (gatt == null) {
				return false;
			}

			foreach (var service in gatt.Services) {
				Log.D(this, "Service: " + service.Uuid);
				foreach (var characteristic in service.Characteristics) {
					Log.D(this, "\tCharacteristic: " + characteristic.Uuid);
					foreach (var desc in characteristic.Descriptors) {
						Log.D(this, "\t\tDescriptor: " + desc.Uuid); 
					}
				}
			}

			readCharacteristic = null;
			writeCharacteristic = null;

			var baseService = gatt.GetService(SERVICE);
			if (baseService != null) {
				readCharacteristic = baseService.GetCharacteristic(READ);
				writeCharacteristic = baseService.GetCharacteristic(WRITE);
			}

			return readCharacteristic != null && writeCharacteristic != null;
		}
	}
}
