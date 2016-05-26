namespace ION.Core.Internal {
  
  using System;
  using System.IO;

  using ION.Core.Devices.Protocols;
  using ION.Core.Measure;

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
    /// The value that indicates that a sensor is not attached to a device.
    /// </summary>
    /// <value>The removed gauge value.</value>
    public int removedGaugeValue {
      get {
        return 0;
      }
    }
    /// <summary>
    /// Queries whether or not the protocol supports broadcasting.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool supportsBroadcasting {
      get {
        return false;
      }
    }

    /// <summary>
    /// Creates a new command that will initialize the rig.
    /// </summary>
    /// <returns>The initialize command.</returns>
    public byte[] CreateInitializeCommand() {
      var ret = new byte[20];

      using (var writer = new BinaryWriter(new MemoryStream(ret))) {
        writer.Write((byte)1);
        writer.Write((byte)4);
        writer.Flush();
      }

      return ret;
    }

    /// <summary>
    /// Creates a new command that will modify the current target degree angle by the given amount.
    /// </summary>
    /// <returns>The modify target degree angle.</returns>
    /// <param name="degreeAngle">Degree angle.</param>
    public byte[] CreateModifyTargetDegreeAngle(float degreeAngle, EDirection direction) {
      var ret = new byte[20];

      using (var writer = new BinaryWriter(new MemoryStream(ret))) {
        writer.Write((byte)6);
        writer.Write((byte)7);
        writer.Write((byte)direction);
        writer.Write(degreeAngle);
        writer.Flush();
      }

      return ret;
    }

    public class CommandBuilder {
      /// <summary>
      /// Whether or not the exhaust is open on the interface device.
      /// </summary>
      /// <value><c>true</c> if exhaust open; otherwise, <c>false</c>.</value>
      public bool exhaustOpen { get; set; }
      /// <summary>
      /// The angle of the throttle stepper. Note: this is designed to be between 0 and 90 degrees (0 and pi/2 radians).
      /// </summary>
      /// <value>The throttle angle.</value>
      public float throttleAngle { get; set; }
    }

    /// <summary>
    /// Enumerates the direction that the steppers can rotate.
    /// </summary>
    public enum EDirection {
      Close = 1,
      Open = 0,
    }
  }
}

