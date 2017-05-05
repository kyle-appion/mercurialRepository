namespace ION.Core.Devices.Protocols {

	using Appion.Commons.Measure;

	using ION.Core.Sensors;

  /// <summary>
  /// An enumeration of the supported protocols.
  /// </summary>
  public enum EProtocolVersion {
    Classic = 0,
    V1 = 1,
    V4 = 4,
  }

  public class Protocol {
    /// <summary>
    /// The Appion manufacturing id that is used to identify appion products.
    /// </summary>
    public const int MANFAC_ID = 0x8c03;
    /// <summary>
    /// The device name for a broadcasting v4 device.
    /// </summary>
    public const string RIGDFU = "RigDfu";
    /// <summary>
    /// The name of the original appion gauges.
    /// </summary>
    public const string APPION_CLASSIC_DEVICE_NAME = "APPION Gauge";

    /// <summary>
    /// The array of supported BLE protocols.
    /// </summary>
    private static IGaugeProtocol[] PROTOCOLS = new IGaugeProtocol[] {
      new ClassicProtocol(),
      new BleV1Protocol(),
      new RidgadoV4Protocol(),
    };

    /// <summary>
    /// Queries the protocol that matches the given version. If not protocol is
    /// found, we will return null.
    /// </summary>
    /// <returns>The protocol from version.</returns>
    /// <param name="version">Version.</param>
    public static IGaugeProtocol FindProtocolFromVersion(EProtocolVersion version) {
      // Could be made more efficient if the protocol count keeps increasing.
      foreach (IGaugeProtocol protocol in PROTOCOLS) {
        if (protocol.version == version) {
          return protocol;
        }
      }

      return null;
    }

    public static IGaugeProtocol FindProtocolFromVersion(int version) {
      switch (version) {
        case -1: 
          return FindProtocolFromVersion(EProtocolVersion.Classic);
        case 1: 
          return FindProtocolFromVersion(EProtocolVersion.V1);
        case 2: // These are prevalent
          return FindProtocolFromVersion(EProtocolVersion.V1);
        case 3: // Note: These never left the warehouse
          return FindProtocolFromVersion(EProtocolVersion.V1);
				case 4:
					return FindProtocolFromVersion(EProtocolVersion.V4);
        default:
          return null;
      }
    }
  }

  public interface IProtocol {
    /// <summary>
    /// Queries the version of the protocol.
    /// </summary>
    EProtocolVersion version { get; }
  }

  public interface IGaugeProtocol : IProtocol {
    /// <summary>
    /// The value that indicates that a sensor is not attached to a device.
    /// </summary>
    /// <value>The removed gauge value.</value>
    int removedGaugeValue { get; }

    /// <summary>
    /// Queries whether or not the protocol supports broadcasting.
    /// </summary>
    /// <value><c>true</c> if supports broadcasting; otherwise, <c>false</c>.</value>
    bool supportsBroadcasting { get; }

    /// <summary>
    /// Parses the provided packet. If the packet cannot be parsed, this method will
    /// throw an argument exception.
    /// </summary>
    /// <param name="packet">The packet to parse.</param>
    /// <returns></returns>
    GaugePacket ParsePacket(byte[] packet);

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will set the unit
    /// for a given sensor.
    /// </summary>
    /// <param name="sensorIndex">The 1-based index of the sensor to set the unit for.</param>
    /// <param name="sensorType">The sensor type. This is necessary to deduce the proper
    /// unit code for the packet.</param>
    /// <param name="unit">The unit that the terminus will be set to.</param>
    /// <returns></returns>
    byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit);

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will zero the 
    /// terminus' reading. This will have no effect on termini that do not support
    /// this command.
    /// </summary>
    /// <param name="sensorIndex">The 1-based index of the sensor to zero</param>
    /// <returns></returns>
    byte[] CreateZeroSensorCommand(int sensorIndex);

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will set the
    /// altitude that is retained within the terminus' memory.
    /// </summary>
    /// <param name="altitude">A scalar whose base unit is METER.</param>
    /// <returns></returns>
    byte[] CreateSetAltitudeCommand(Scalar altitude);
  }

  /// <summary>
  /// A struct that represents a parsed data packet from a gauge device.
  /// </summary>
  public struct GaugePacket {
    /// <summary>
    /// Queries the version of the protocol that parsed the gauge packet.
    /// </summary>
    public EProtocolVersion version { get; private set; }
    /// <summary>
    /// Queries the battery level of the terminus who spawned this packet.
    /// </summary>
    public int battery { get; private set; }
    /// <summary>
    /// Queries the array of gauge readings from the terminus.
    /// </summary>
    public GaugeReading[] gaugeReadings { get; private set; }
    /// <summary>
    /// Queries a single gauge reading from the packet.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GaugeReading this[int index] { get { return gaugeReadings[index];  } }

    /// <summary>
    /// Creates a new GaugePacket.
    /// </summary>
    /// <param name="version"></param>
    /// <param name="battery"></param>
    /// <param name="readings"></param>
    public GaugePacket(EProtocolVersion version, int battery, GaugeReading[] readings) : this() {
      this.version = version;
      this.battery = battery;
      this.gaugeReadings = readings;
    }
  }

  /// <summary>
  /// A struct that represents a single gauge reading from a gauge device.
  /// </summary>
  public struct GaugeReading {
    /// <summary>
    /// Whether or not the gauge is removed.
    /// </summary>
    /// <value><c>true</c> if removed; otherwise, <c>false</c>.</value>
    public bool removed { get; set; }
    /// <summary>
    /// Queries the sensor type of the sensor whose reading is represented.
    /// </summary>
    public ESensorType sensorType { get; set; }
    /// <summary>
    /// Queries the reading of the represented sensor.
    /// </summary>
    public Scalar reading { get; set; }
  }
}
