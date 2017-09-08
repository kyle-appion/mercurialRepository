namespace ION.Core.Devices.Protocols {

  using System.Collections.Generic;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.Devices;
  using ION.Core.Sensors;

  /// <summary>
  /// This protocol was the first bluetooth protocol ever used for ion. It is highly deprecated and unsupported. The
  /// only reason it exists even this much is due to the small but significant quantity of these old gauges out in the
  /// wild.
  /// </summary>
  /*
   * EXAMPLE PACKET:
   *   1 STA 100 SEN 1 P513G0001 0 P500 0.0 PSI
   */
  public class ClassicProtocol : IGaugeProtocol {
    /// <summary>
    /// Queries the version of the protocol.
    /// </summary>
    /// <value>The version.</value>
    public EProtocolVersion version { get { return EProtocolVersion.Classic; } }
    /// <summary>
    /// The value that indicates that a sensor is not attached to a device.
    /// </summary>
    /// <value>The removed gauge value.</value>
    public int removedGaugeValue { get { return int.MaxValue; } }
    /// <summary>
    /// Queries whether or not the protocol supports broadcasting.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool supportsBroadcasting { get { return false; } }

    /// <summary>
    /// The dictionary that will lookup the units for the synapse protocol.
    /// </summary>
    public Dictionary<string, Unit> pressureUnitLookup = new Dictionary<string, Unit>();
    /// <summary>
    /// The dictionary that will lookup the units for the synapse protocol.
    /// </summary>
    public Dictionary<string, Unit> vacuumUnitLookup = new Dictionary<string, Unit>();

    public ClassicProtocol() {
      pressureUnitLookup.Add("psi", Units.Pressure.PSIG);
      pressureUnitLookup.Add("kg", Units.Pressure.KG_CM);
      pressureUnitLookup.Add("bar", Units.Pressure.BAR);
      pressureUnitLookup.Add("mbar", Units.Pressure.MILLIBAR);
      pressureUnitLookup.Add("kpa", Units.Pressure.KILOPASCAL);
      pressureUnitLookup.Add("mpa", Units.Pressure.MEGAPASCAL);
      pressureUnitLookup.Add("inhg", Units.Pressure.IN_HG);

      vacuumUnitLookup.Add("psia", Units.Vacuum.PSIA);
      vacuumUnitLookup.Add("mbar", Units.Vacuum.MILLIBAR);
      vacuumUnitLookup.Add("inhg", Units.Vacuum.IN_HG);
      vacuumUnitLookup.Add("kpa", Units.Vacuum.KILOPASCAL);
      vacuumUnitLookup.Add("micron", Units.Vacuum.MICRON);
      vacuumUnitLookup.Add("mtorr", Units.Vacuum.MILLITORR);
    }

    /// <summary>
    /// Parses the provided packet. If the packet cannot be parsed, this method will
    /// throw an argument exception.
    /// </summary>
    /// <param name="packet">The packet to parse.</param>
    /// <returns></returns>
    /// <param name="packetIn">Packet in.</param>
    public GaugePacket ParsePacket(byte[] packetIn) {
      var packet = System.Text.Encoding.UTF8.GetString(packetIn, 0, packetIn.Length);
      packet = packet.Trim();

      var tokens = packet.Split(new char[] { ' ' });

      var battery = int.Parse(tokens[2]);
      var serialNumber = GaugeSerialNumber.Parse(tokens[5]);
      var meas = double.Parse(tokens[8]);
      Unit ru;
      GaugeReading gr;

      if (EDeviceModel.AV760 == serialNumber.deviceModel) {
        ru = vacuumUnitLookup[tokens[9].ToLower()];
        gr = new GaugeReading() {
          removed = false,
          sensorType = ESensorType.Vacuum,
          reading = ru.OfScalar(meas),
        };
      } else {
        ru = pressureUnitLookup[tokens[9].ToLower()];
        gr = new GaugeReading() {
          removed = false,
          sensorType = ESensorType.Pressure,
          reading = ru.OfScalar(meas),
        };
      }



      return new GaugePacket(version, battery, new GaugeReading[] { gr });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ION.Core.Devices.Protocols.ClassicPressureProtocol"/> class.
    /// </summary>
    /// <param name="sensorIndex">Sensor index.</param>
    /// <param name="sensorType">Sensor type.</param>
    /// <param name="unit">Unit.</param>
    public byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
      return new byte[] { };
    }

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will zero the 
    /// terminus' reading. This will have no effect on termini that do not support
    /// this command.
    /// </summary>
    /// <param name="sensorIndex">The 1-based index of the sensor to zero</param>
    /// <returns></returns>
    public byte[] CreateZeroSensorCommand(int sensorIndex) {
      return new byte[] { };
    }

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will set the
    /// altitude that is retained within the terminus' memory.
    /// </summary>
    /// <param name="altitude">A scalar whose base unit is METER.</param>
    /// <returns></returns>
    public byte[] CreateSetAltitudeCommand(Scalar altitude) {
      return new byte[] { };
    }

    /// <summary>
    /// Parses the serial number from the given packet.
    /// </summary>
    /// <returns>The serial number.</returns>
    /// <param name="packtIn">Packet in.</param>
    public static bool ParseSerialNumber(byte[] packetIn, out GaugeSerialNumber serialNumber) {
      var packet = System.Text.Encoding.UTF8.GetString(packetIn, 0, packetIn.Length);
      Log.D("ClassicProtocol", "Packet is: '" + packet + "'");
      var tokens = packet.Split(new char[] { ' ' });
      if (tokens.Length != 10) {
        serialNumber = null;
        return false;
      }
      var usn = tokens[5];
      if (GaugeSerialNumber.IsValid(usn)) {
        serialNumber = GaugeSerialNumber.Parse(usn);
        return true;
      } else {
        serialNumber = null;
        return false;
      }
    }
  }
}

