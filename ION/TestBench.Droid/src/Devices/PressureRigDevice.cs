namespace TestBench.Droid.Devices {

	using System;
	using System.IO;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Connections;
	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
	using ION.Core.IO;

	public class PressureRigDevice : IDevice, IRig {
		// Implemented from IDevice
		public event OnDeviceEvent onDeviceEvent;
		// Implemented from IRig
		public event Action<IRig> onConnectionStateChanged;

		// Implemented from IDevice
		public ISerialNumber serialNumber { get; private set; }
		// Implemented from IDevice
		public EDeviceType type { get; private set; }
		// Implemented from IDevice
		public string name { get; set; }
		// Implemented from IDevice
		public int battery { get; private set; }
		// Implemented from IDevice
		public IConnection connection { get; private set; }
		// Implemented from IDevice
		public IProtocol protocol { get { return pressureRigProtocol; } }
		// Implemented from IDevice
		public bool isConnected { get { return connection.connectionState == EConnectionState.Connected; } }
		// Implemented from IDevice
		public bool isNearby { get; } 

		// Implemented from IRig
		public ERigType rigType { get { return ERigType.Pressure; } }
		// Implemented from IRig
		public string address { get { return connection.address; } }

		/// <summary>
		/// The protocol that the pressure rig is using.
		/// </summary>
		public readonly PressureRigProtocol pressureRigProtocol;

		/// <summary>
		/// The last known pressure from the rig device.
		/// </summary>
		public Scalar pressure;
		/// <summary>
		/// The last known angle of the pressure rig's g5 stepper.
		/// </summary>
		public Scalar g5StepperAngle;
		/// <summary>
		/// The last known angle of the pressure rig's exhaust stepper.
		/// </summary>
		public Scalar exhaustStepperAngle;

		public PressureRigDevice() {
			serialNumber = new PressureRigSerialNumber("PR00Z001");
			pressureRigProtocol = new PressureRigProtocol();
		}

		// Implemented from IDevice
		public void Dispose() {
			connection.Disconnect();
		}

		// Implemented from IDevice
		public int CompareTo(IDevice other) {
			return serialNumber.CompareTo(other.serialNumber);
		}

		// Implemented from IRig
		public void Connect() {
			connection.ConnectAsync();
		}

		// Implemented from IRig
		public void Disconnect() {
			connection.Disconnect();
		}

		public void HandlePacket(byte[] packet) {
			try {
				var p = pressureRigProtocol.ParsePacket(packet);
				this.pressure = p.pressure;
			} catch (Exception e) {
				Log.E(this, "Failed to parse pressure rig packet", e);
			}
		}
	}

	public class PressureRigSerialNumber : ISerialNumber {
		// Implemented from ISerialNumber
		public EDeviceModel deviceModel { get { return EDeviceModel.Internal; } }
		// Implemented from ISerialNumber
		public EDeviceType deviceType { get { return EDeviceType.InternalInterface; } }
		// Implemented from ISerialNumber
		public string deviceCode { get; }
		// Implemented from ISerialNumber
		public string rawSerial { get; }
		// Implemented from ISerialNumber
		public DateTime manufactureDate { get; }
		// Implemented from ISerialNumber
		public ushort batchId { get; }

		public PressureRigSerialNumber(string rawSerialNumber) {
			this.rawSerial = rawSerialNumber;
		}

		// Implemented from ISerialNumber
		public int CompareTo(ISerialNumber other) {
			return rawSerial.CompareTo(other.rawSerial);
		}

		// Implemented from object
		public override int GetHashCode() {
			return rawSerial.GetHashCode();
		}

		// Implemented from object
		public override bool Equals(object obj) {
			if (this == obj) {
				return true;
			} else if (obj is PressureRigSerialNumber) {
				return ((PressureRigSerialNumber)obj).rawSerial.Equals(rawSerial);
			} else {
				return false;
			}
		}

		// Implemented from object
		public override string ToString() {
			return rawSerial;
		}
	}

	[Flags]
	public enum EState {
		SinOpen = 1 << 0,
		SinClosed = 1 << 1,
		SoutOpen = 1 << 2,
		SoutClosed = 1 << 3,
		Crashed = 1 << 7,
	}

	[Flags]
	public enum ECommand {
		SinOpen = 1 << 0,
		SinClosed = 1 << 1,
		SoutOpen = 1 << 2,
		SoutClosed = 1 << 3,
		G5On = 1 << 4,
		G5Off = 1 << 5,
	}

	public class PressureRigPacket {
		public EState state;
		public Scalar pressure;
		public Scalar sinAngle;
		public Scalar soutAngle;
	}

	public class PressureRigProtocol : IProtocol {
		public EProtocolVersion version { get; }

		public PressureRigPacket ParsePacket(byte[] bytes) {
			var ms = new MemoryStream(bytes);

			using (var r = new BinaryReader(ms)) {
				var state = (EState)r.ReadByte();
				var usEc = r.ReadUInt32BE();
				var usPressure = r.ReadFloat32BE();
				var usG5Rot = r.ReadFloat32BE();
				var usERot = r.ReadFloat32BE();

				return new PressureRigPacket() {
					state = state,
					pressure = Units.Pressure.PSIG.OfScalar(usPressure),
					sinAngle = Units.Angle.DEGREE.OfScalar(usG5Rot),
					soutAngle = Units.Angle.DEGREE.OfScalar(usERot),
				};
			}
		}

		public byte[] CreateCommand(ECommand command) {
			var ms = new MemoryStream(new byte[20]);

			using (var w = new BinaryWriter(ms)) {
				w.Write((int)command);
			}

			return ms.ToArray();
		}
	}
}
