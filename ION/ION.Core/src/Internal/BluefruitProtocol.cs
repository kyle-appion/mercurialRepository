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
        writer.Write((int)ECommands.Initialize);
        writer.Flush();
      }

      return ret;
    }

/*
    /// <summary>
    /// Creates the interface command.
    /// </summary>
    /// <returns>The interface command.</returns>
    /// <param name="builder">Builder.</param>
    public byte[] CreateInterfaceCommand(CommandBuilder builder) {
      var ret = new byte[20];

      var writer = new BinaryWriter(new MemoryStream(ret));
      writer.Write(builder.exhaustOpen);
      writer.Write(builder.throttleAngle);
      writer.Flush();

      return ret;
    }
*/

    /// <summary>
    /// Creates a command that is used to set the target microns for the test rig.
    /// </summary>
    /// <returns>The set target micron command.</returns>
    /// <param name="microns">Microns.</param>
    public byte[] CreateSetTargetMicronCommand(Scalar microns) {
      if (microns.unit.IsCompatible(Units.Vacuum.MICRON)) {
        throw new Exception("Cannot create command: expected a vacuum measurement, received: " + microns.unit);
      }

      microns = microns.ConvertTo(Units.Vacuum.MICRON);

      var ret = new byte[20];

      using (var writer = new BinaryWriter(new MemoryStream(ret))) {
        writer.Write((int)ECommands.TargetMicron);
        writer.Write((int)microns.amount);
        writer.Flush();
      }

      return ret;
    }

    /// <summary>
    /// Creates a new command that will modify the current target degree angle by the given amount.
    /// </summary>
    /// <returns>The modify target degree angle.</returns>
    /// <param name="degreeAngle">Degree angle.</param>
    public byte[] CreateModifyTargetDegreeAngle(float degreeAngle) {
      var ret = new byte[20];

      using (var writer = new BinaryWriter(new MemoryStream(ret))) {
        writer.Write((int)ECommands.ModifyTargetDegreeAngle);
        writer.Write(degreeAngle);
        writer.Flush();
      }

      return ret;
    }

    /// <summary>
    /// The numeration of the comand values for the Bluefruit device test rig.
    /// </summary>
    private enum ECommands {
      Initialize = 1,
      TargetMicron = 2,
      ModifyTargetDegreeAngle = 3,
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
  }
}

