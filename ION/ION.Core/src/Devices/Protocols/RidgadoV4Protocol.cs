﻿namespace ION.Core.Devices.Protocols {

  using System;

  using System.IO;

  using ION.Core.IO;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  /// <summary>
  /// The protocol that is used to communicate with the Rigado bluetooth chipset based devices. This protocol allows
  /// for variable sized gauge packets.
  /// </summary>
  public class RidgadoV4Protocol : BaseBinaryProtocol {
    /// <summary>
    /// The mask that is used to retrieve a unit code from a definition byte.
    /// </summary>
    private const int MASK_UNIT = 0x3f;
    /// <summary>
    /// The mask that is used to retrieve whether or not a sensor is connected from a definition byte. 0 is diconnected,
    /// 1 is connected.
    /// </summary>
    private const int MASK_CONNECTED = 0x40;
    /// <summary>
    /// The mask that is used to retrieve the measurement mode from of a sensor from a definition byte.
    /// </summary>
    private const int MASK_MODE = 0x80;

    /// <summary>
    /// Queries the version of the protocol.
    /// </summary>
    /// <value>The version.</value>
    public override EProtocolVersion version { get { return EProtocolVersion.V4; } }
    /// <summary>
    /// The value that indicates that a sensor is not attached to a device.
    /// </summary>
    /// <value>The removed gauge value.</value>
    public override int removedGaugeValue { get { return 0; } }
    /// <summary>
    /// Queries whether or not the protocol supports broadcasting.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool supportsBroadcasting { get { return true; } }

    /// <summary>
    /// Parses the provided packet. If the packet cannot be parsed, this method will
    /// throw an argument exception.
    /// </summary>
    /// <param name="packet">The packet to parse.</param>
    /// <returns></returns>
    public override GaugePacket ParsePacket(byte[] packet) {
      using (BinaryReader r = new BinaryReader(new MemoryStream(packet))) {
        var version = (EProtocolVersion)r.ReadByte();
        var battery = (int)r.ReadByte();

        var readings = new GaugeReading[4];

        // According to the rigado protocol specification, the packet length should be no more than 19 bytes. 
        var count = 17; // 19 - version and battery
        var index = 0;
        while (count >= 4) { // While the count still has another potential packet, pull the next packet.
          var sp = new SensorPayload(r);
          count -= sp.length;
          readings[index++] = new GaugeReading() {
            removed = sp.connected,
            sensorType = UnitLookup.GetSensorTypeFromCode(sp.unitCode),
            reading = sp.unit.OfScalar(sp.measurement),
          };
        }

        return new GaugePacket(version, battery, readings);
      }
    }

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will set the unit
    /// for a given sensor.
    /// </summary>
    /// <param name="sensorIndex">The 1-based index of the sensor to set the unit for.</param>
    /// <param name="sensorType">The sensor type. This is necessary to deduce the proper
    /// unit code for the packet.</param>
    /// <param name="unit">The unit that the terminus will be set to.</param>
    /// <returns></returns>
    // Overridden from IGaugeProtocol
    public override byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
      return new byte[] { 0x02, (byte)UnitLookup.GetCode(unit), (byte)sensorIndex };
    }

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will zero the 
    /// terminus' reading. This will have no effect on termini that do not support
    /// this command.
    /// </summary>
    /// <param name="sensorIndex">The 1-based index of the sensor to zero</param>
    /// <returns></returns>
    public override byte[] CreateZeroSensorCommand(int sensorIndex) {
      return new byte[] { 0x03, (byte)sensorIndex };
    }

    /// <summary>
    /// Creates a packet that, when received by a remote terminus, will set the
    /// altitude that is retained within the terminus' memory.
    /// </summary>
    /// <param name="altitude">A scalar whose base unit is METER.</param>
    /// <returns></returns>
    public override byte[] CreateSetAltitudeCommand(Scalar altitude) {
      var v = (int)altitude.amount;
      var msb = (byte)((v >> 8) & 0xff);
      var lsb = (byte)(v & 0xff);
      return new byte[] { 0x04, msb, lsb, (byte)UnitLookup.GetCode(altitude.unit) };
    }

    /// <summary>
    /// The data presented from a sensor.
    /// </summary>
    internal struct SensorPayload {
      /// <summary>
      /// Queries the unit that is described by the definition byte.
      /// </summary>
      /// <value>The unit.</value>
      public Unit unit { get { return UnitLookup.GetUnit(definitionByte & MASK_UNIT); } }
      /// <summary>
      /// Queries whether or not a sensor is connected, according to the byte.
      /// </summary>
      /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
      public bool connected { get { return (definitionByte & MASK_CONNECTED) == MASK_CONNECTED; } }
      /// <summary>
      /// Queries whether or not the definition byte is in short mode. If not, then the byte is in long mode.
      /// </summary>
      /// <value><c>true</c> if is long mode; otherwise, <c>false</c>.</value>
      public bool isShortMode { get { return (definitionByte & MASK_MODE) == MASK_MODE; } }

      /// <summary>
      /// The raw definition byte that represents the some meta data about the payload.
      /// </summary>
      public byte definitionByte { get; private set; }
      /// <summary>
      /// The measurement presented by the sensor.
      /// </summary>
      /// <value>The measurement.</value>
      public float measurement { get; private set; }
      /// <summary>
      /// The unit code for the sensor payload.
      /// </summary>
      /// <value>The unit code.</value>
      public byte unitCode { get; private set; }
      /// <summary>
      /// The length of the sensor payload.
      /// </summary>
      /// <value>The length.</value>
      public int length { get { return 2 + (isShortMode ? 2 : 4 ); } }

      public SensorPayload(BinaryReader reader) : this() {
        definitionByte = reader.ReadByte();
        float m = 0;
        if (isShortMode) {
          m = reader.ReadInt16BE();
        } else {
          m = reader.ReadInt32BE();
        }
        unitCode = reader.ReadByte();
        m = m / (10 * unitCode);
      }
    }
  }
}

