namespace TestBench.Droid.Devices {

	using System;
	using System.IO;

	using Appion.Commons.Measure;

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
		public IProtocol protocol { get; private set; }
		// Implemented from IDevice
		public bool isConnected { get { return connection.connectionState == EConnectionState.Connected; } }
		// Implemented from IDevice
		public bool isNearby { get; } 

		// Implemented from IRig
		public ERigType rigType { get { return ERigType.Pressure; } }
		// Implemented from IRig
		public string address { get { return connection.address; } }
		// Implemented from IRig


		public PressureRigDevice() {
			serialNumber = new PressureRigSerialNumber("PR00Z001");
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
		Off = 0x01,
		Idle = 0x02,
		Initialize = 0x03,
		GotoPressure = 0x04,
		HoldPressure = 0x05,
		Purge = 0x06,
	}

	public class PressureRigPacket {
		public EState state;
		public Scalar pressure;
		public Scalar currentStepperRotation;
	}

	public class PressureRigProtocol : IProtocol {
		public EProtocolVersion version { get; }

		public PressureRigPacket ParsePacket(byte[] bytes) {
			var ms = new MemoryStream(bytes);

			using (var r = new BinaryReader(ms)) {
				var state = (EState)r.ReadByte();
				var usPressure = r.ReadFloat32BE();
				var usRot = r.ReadFloat32BE();

				return new PressureRigPacket() {
					state = state,
					pressure = Units.Pressure.PSIG.OfScalar(usPressure),
					currentStepperRotation = Units.Angle.DEGREE.OfScalar(usRot),
				};
			}
		}

		/// <summary>
		/// Creates a packet that will turn the rig off (aside from bluetooth).
		/// </summary>
		/// <returns>The off command.</returns>
		public byte[] CreateOffCommand() {
			return new byte[] { (byte)EState.Off };
		}

		/// <summary>
		/// Creates a packet that will place the rig into an idle state.
		/// </summary>
		/// <returns>The idle command.</returns>
		public byte[] CreateIdleCommand() {
			return new byte[] { (byte)EState.Idle };
		}

		/// <summary>
		/// Creates a packet that will intialize the remote rig.
		/// </summary>
		/// <returns>The initialize command.</returns>
		public byte[] CreateInitializeCommand() {
			return new byte[] { (byte)EState.Initialize };
		}

		/// <summary>
		/// Creates a new command that will tell the rig to go to the given pressure.
		/// </summary>
		/// <returns>The goto pressure command.</returns>
		/// <param name="targetPressure">Target pressure.</param>
		public byte[] CreateGotoPressureCommand(Scalar targetPressure) {
			var ret = new MemoryStream();

			using (var w = new BinaryWriter(ret)) {
				w.Write((byte)EState.GotoPressure);
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
		public byte[] CreatePurgeCommand() {
			return new byte[] { (byte)EState.Purge };
		}
	}
}
