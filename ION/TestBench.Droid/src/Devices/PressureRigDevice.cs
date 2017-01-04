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
				this.g5StepperAngle = p.g5StepperAngle;
				this.exhaustStepperAngle = p.exhaustStepperAngle;
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

	public enum EState {
		// The state/command that will shut the rig
		Shutdown = 1,
		// The state/command type that puts the rig into an idle state.
		Idle = 2,
		// The state/command type that puts the rig into a pressurize until target state.
		Pressurize = 3,
		// The state/command type that puts the rig into a hold pressure state.
		HoldPressure = 4,
		// The state/command that will depressurize the rig.
		Depressurize = 5,
		// The state/command that will is used when the rig crashes.
		Crash = 6,
	}

	public class PressureRigPacket {
		public EState state;
		public uint errorCode;
		public Scalar pressure;
		public Scalar g5StepperAngle;
		public Scalar exhaustStepperAngle;
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
					errorCode = usEc, 
					pressure = Units.Pressure.PSIG.OfScalar(usPressure),
					g5StepperAngle = Units.Angle.DEGREE.OfScalar(usG5Rot),
					exhaustStepperAngle = Units.Angle.DEGREE.OfScalar(usERot),
				};
			}
		}

		/// <summary>
		/// Creates a packet that will shutdown the rig.
		/// Note: this will also disconnect the device.
		/// </summary>
		/// <returns>The shutdown command.</returns>
		public byte[] CreateShutdownCommand() {
			return new byte[] { (byte)EState.Shutdown };
		}

		/// <summary>
		/// Creates a packet that will place the rig into an idle state.
		/// </summary>
		/// <returns>The idle command.</returns>
		public byte[] CreateIdleCommand() {
			return new byte[] { (byte)EState.Idle };
		}

		/// <summary>
		/// Creates a new command that will tell the rig to go to the given pressure.
		/// </summary>
		/// <returns>The goto pressure command.</returns>
		/// <param name="targetPressure">Target pressure.</param>
		public byte[] CreatePressurizeCommand(Scalar targetPressure) {
			var ret = new MemoryStream();

			using (var w = new BinaryWriter(ret)) {
				w.Write((byte)EState.Pressurize);
				w.Write((float)targetPressure.ConvertTo(Units.Pressure.PSIG).amount);
			}

			return ret.ToArray();
		}

		/// <summary>
		/// Creates a new command that will have the rig attempt to hold at the current pressure.
		/// </summary>
		/// <returns>The hold pressure command.</returns>
		public byte[] CreateHoldPressureCommand() {
			return new byte[] { (byte)EState.HoldPressure };
		}

		/// <summary>
		/// Creates a new command that will have the rig purge itself of pressure.
		/// </summary>
		/// <returns>The purge command.</returns>
		public byte[] CreateDepressurizeCommand() {
			return new byte[] { (byte)EState.Depressurize };
		}
	}
}
