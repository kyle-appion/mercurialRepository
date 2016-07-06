namespace ION.Core.Internal {
  
	using System;
  using System.IO;

  using ION.Core.Devices.Protocols;
	using ION.Core.Measure;

  /// <summary>
  /// A protocol that is used to communicate with the appion internal gauge test rig.
  /// </summary>
  public class BluefruitProtocol : IProtocol {
		private const int MASK_RPS = 0x3f;
		private const int MASK_AT_HOME = 0x40;
		private const int MASK_AT_END = 0x80;

    /// <summary>
    /// Queries the version of the protocol.
    /// </summary>
    /// <value>The version.</value>
    public EProtocolVersion version { 
      get {
        return EProtocolVersion.TestBenchInternal;
      }
    }

		/// <summary>
		/// Parses the incoming data packet from a Bluefruit device.
		/// </summary>
		/// <param name="packet">Packet.</param>
		public BluefruitPacket Parse(byte[] packet) {
			var ret = new BluefruitPacket();
			ret.isResetting = true;

			for (int i = 0; i < packet.Length; i++) {
				if (packet[i] != 0xff) {
					ret.isResetting = false; 
					break;
				}
			}

			if (!ret.isResetting) {
				using (var s = new BinaryReader(new MemoryStream(packet))) {
					ret.targetInputAngle = s.ReadSingle();
					ret.currentInputAngle = s.ReadSingle();
					var inputStateByte = s.ReadByte();
					ret.inputRPS = (ESpeed)(inputStateByte & MASK_RPS);
					ret.inputAtHome = (inputStateByte & MASK_AT_HOME) == MASK_AT_HOME;
					ret.inputAtEnd = (inputStateByte & MASK_AT_END) == MASK_AT_END;

					ret.targetExhaustAngle = s.ReadSingle();
					ret.currentExhaustAngle = s.ReadSingle();
					var exhaustStateByte = s.ReadByte();
					ret.exhaustRPS = (ESpeed)(exhaustStateByte & MASK_RPS);
					ret.exhaustAtHome = (exhaustStateByte & MASK_AT_HOME) == MASK_AT_HOME;
					ret.exhaustAtEnd = (exhaustStateByte & MASK_AT_END) == MASK_AT_END;

					var vrcComp = s.ReadUInt16();
					var sig = (vrcComp & 0x07ff);
					var pow = (vrcComp & 0xf800) >> 11;

					ret.vrc = (int)(sig * (Math.Pow(10, pow)));
				}
			}

			return ret;
		}

		/// <summary>
		/// Creates a new command packet.
		/// </summary>
		/// <returns>The command packet.</returns>
		/// <param name="targetInputAngle">Target input angle.</param>
		/// <param name="inputRps">Input rps.</param>
		/// <param name="targetExhaustAngle">Target exhaust angle.</param>
		/// <param name="exhaustRps">Exhaust rps.</param>
		public byte[] CreateCommandPacket(Scalar targetInputAngle, ESpeed inputSpeed, Scalar targetExhaustAngle, ESpeed exhaustSpeed) {
			// Get the denominator of the rotations.
			var ret = new byte[20];

			using (var s = new BinaryWriter(new MemoryStream(ret))) {
				s.Write((byte)ECommand.CommandPacket);
				s.Write((float)(targetInputAngle.ConvertTo(Units.Angle.RADIAN).amount));
				s.Write((byte)inputSpeed);

				s.Write((float)(targetExhaustAngle.ConvertTo(Units.Angle.RADIAN).amount));
				s.Write((byte)exhaustSpeed);
			}

			return ret;
		}

		/// <summary>
		/// Creates a new packet that will instruct the rig to disregard the current test. This command will have no effect
		/// if the rig is not currently testing.
		/// </summary>
		/// <returns>The cacel test packet.</returns>
		public byte[] CreateResetPacket() {
			return new byte[] { (byte)ECommand.Reset } ;
		}

		/// <summary>
		/// Takes the given value and breaks it into the number of zeros in its tail and the value at the head. Essentially,
		/// this reduces the number to a psuedo scientific 2-byte number. This is done because the test points are all % 10
		/// numbers. Because of this, we can significantly reduce their size while still showing numbers larger than 64k.
		/// </summary>
		/// <returns>The to short significant as two bytes. The first byte is the significant values, and the second byte
		/// is the decmial exponentiation removed from the significant values.</returns>
		/// <param name="value">Value.</param>
		private byte[] ConvertToShortSignificant(int value) {
			var e = 0;

			while (value % 10 == 0) {
				e++;
				value = value / 10;
			}

			return new byte[] { (byte)value, (byte)e };
		}

		public float GetRPSFromSpeed(ESpeed speed) {
			switch (speed) {
				case ESpeed.Fastest:
				return 2;
				case ESpeed.Fast:
				return 1;
				case ESpeed.Normal:
				return 1 / 5.0f;
				case ESpeed.Slow:
				return 1 / 20.0f;
				case ESpeed.Slowest:
				return 1 / 40.0f;
				case ESpeed.Turtle:
				return 1 / 80.0f;
				default:
				return 1 / 5.0f;
			}
		}

		/// <summary>
		/// The enumerations of the commands that the rig will reslove.
		/// </summary>
		private enum ECommand {
			Reset = 1,
			CommandPacket = 2,
		}
  }

	[Flags]
	public enum ERigState {
		InputFullOpen 		= 1 << 0,
		InputFullClosed 	= 1 << 1,
		ExhaustFullOpen 	= 1 << 2,
		ExhaustFullClosed	= 1 << 3,
	}

	public enum ESpeed {
		Fastest = 0,
		Fast = 1,
		Normal = 2,
		Slow = 3,
		Slowest = 4,
		Turtle = 5,
	}

	/// <summary>
	/// A simple packet that is transmitted from the bluefruit durning a test.
	/// </summary>
	public class BluefruitPacket {
		public bool isResetting { get; internal set; }
		/// <summary>
		/// The target angel of the input stepper motor in radians.
		/// </summary>
		public float targetInputAngle { get; internal set; }
		/// <summary>
		/// The current angle of the input stepper motor in radians.
		/// </summary>
		/// <value>The current input angle.</value>
		public float currentInputAngle { get; internal set; }
		/// <summary>
		/// The rotational speed of stepper 1 in radians per second.
		/// </summary>
		public ESpeed inputRPS { get; internal set; }
		/// <summary>
		/// Whether or not the input's home switch is pressed.
		/// </summary>
		/// <value>The input at home.</value>
		public bool inputAtHome { get; internal set; }
		/// <summary>
		/// Whether or not the input's end switch is pressed.
		/// </summary>
		/// <value>The input at end.</value>
		public bool inputAtEnd { get; internal set; }

		/// <summary>
		/// The target angel of the exhaust stepper motor in radians.
		/// </summary>
		public float targetExhaustAngle { get; internal set; }
		/// <summary>
		/// The current angle of the exhaust stepper motor in radians.
		/// </summary>
		/// <value>The current input angle.</value>
		public float currentExhaustAngle { get; internal set; }
		/// <summary>
		/// The rotational speed of stepper 2 in radians per second.
		/// </summary>
		public ESpeed exhaustRPS { get; internal set; }
		/// <summary>
		/// Whether or not the exhaust's home switch is pressed.
		/// </summary>
		/// <value>The input at home.</value>
		public bool exhaustAtHome { get; internal set; }
		/// <summary>
		/// Whether or not the exhaust's end switch is pressed.
		/// </summary>
		/// <value>The input at end.</value>
		public bool exhaustAtEnd { get; internal set; }
		/// <summary>
		/// The current measurement of the vrc.
		/// </summary>
		public int vrc { get; internal set; }
	}
}

