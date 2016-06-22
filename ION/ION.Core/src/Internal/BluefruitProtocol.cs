namespace ION.Core.Internal {
  
	using System;
  using System.IO;

  using ION.Core.Devices.Protocols;

  /// <summary>
  /// A protocol that is used to communicate with the appion internal gauge test rig.
  /// </summary>
  public class BluefruitProtocol : IProtocol {
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
		/// Creates the new test requirement packet that is to be sent to the test rig.
		/// </summary>
		/// <returns>The test requirement packet.</returns>
		/// <param name="errorAllowance">The max percent error from the target points that is allowed.</param>
		/// <param name="targetPoints">The target points that the test rig will must reach for the test.</param>
		public byte[] CreateStartNewTestPacket(float errorAllowance, int[] targetPoints) {
			var ret = new byte[20];

			using (var s = new BinaryWriter(new MemoryStream(ret))) {
				s.Write((byte)ECommand.StartNewTest);
				s.Write((byte)targetPoints.Length);
				s.Write((byte)(errorAllowance * 100));
				foreach (var i in targetPoints) {
					s.Write(ConvertToShortSignificant(i));
				}
			}

			return ret;
		}

		/// <summary>
		/// Creates a new packet that will instruct the rig to disregard the current test. This command will have no effect
		/// if the rig is not currently testing.
		/// </summary>
		/// <returns>The cacel test packet.</returns>
		public byte[] CreateResetPacket() {
			return new byte[] { (byte)ECommand.StartNewTest } ;
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

		/// <summary>
		/// The enumerations of the commands that the rig will reslove.
		/// </summary>
		private enum ECommand {
			Reset = 1,
			StartNewTest = 2,
		}
  }

	[Flags]
	public enum ERigState {
		InputFullOpen 		= 1 << 0,
		InputFullClosed 	= 1 << 1,
		ExhaustFullOpen 	= 1 << 2,
		ExhaustFullClosed	= 1 << 3,
	}

	/// <summary>
	/// A simple packet that is transmitted from the bluefruit durning a test.
	/// </summary>
	public class BluefruitPacket {
		/// <summary>
		/// The input angle of stepper 1 in radians.
		/// </summary>
		public float inputAngle;
		/// <summary>
		/// The rotational speed of stepper 1 in radians per second.
		/// </summary>
		public float inputRPS;
		/// <summary>
		/// The exhaust angle of stepper 2 in radians.
		/// </summary>
		public float exhaustAngle; 
		/// <summary>
		/// The rotational speed of stepper 2 in radians per second.
		/// </summary>
		public float exhaustRPS;
		/// <summary>
		/// The current measurement of the vrc.
		/// </summary>
		public int vrc;
		public BluefruitPacket() {
		}
	}
}

