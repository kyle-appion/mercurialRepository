namespace ION.Core.Devices.Protocols {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.IO;
  using ION.Core.Sensors;


  /// <summary>
  /// This protocol was the first bluetooth LE protocol used by most gauges.
  /// This protocol is primarily used during active communications.
  /// </summary>
  public class BleV1Protocol : BaseBinaryProtocol {
    // Overriden from IGaugeProtocol
    public override EProtocolVersion version { get { return EProtocolVersion.V1; } }
    // Overridden from IGagueProtocol
    public override bool supportsBroadcasting { get { return false; } }

    // Overridden from IGaugeProtocol
    public override GaugePacket ParsePacket(byte[] packetIn) {
      byte[] packet = Trim(packetIn);
      using (BinaryReader r = new BinaryReader(new MemoryStream(packet))) {
        int len = packet.Length;

        if (len < 8 || len % 6 != 2) {
          throw new ArgumentException("Cannot parse: bad packet size {" + len + "}");
        }

        var v = r.ReadByte();

        if (v != 1 && v != 2) {
          throw new ArgumentException("Cannot parse: invalid version code");
        }

        int battery = r.ReadByte();

        int maxGaugeCount = (len - 2) / 6;
        var readings = new List<GaugeReading>();

        for (int i = 0; i < maxGaugeCount; i++) {
          int exponent = r.ReadByte();
          int encodedReading = r.ReadInt32BE();
          int unitCode = r.ReadByte();

          if (unitCode == 0) {
            break;
          }

          Unit unit = UnitLookup.GetUnit(unitCode);

          var gr = new GaugeReading() {
            removed = removedGaugeValue == encodedReading,
            sensorType = UnitLookup.GetSensorTypeFromCode(unitCode),
            reading = unit.OfScalar(encodedReading / System.Math.Pow(10, exponent)),
          };

          if (gr.removed) {
            gr.reading = unit.OfScalar(0);
          }

          readings.Add(gr);
        }

        return new GaugePacket(version, battery, readings.ToArray());
      }
    }

    // Overridden from IGaugeProtocol
    public override byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
      return new byte[] { 0x02, (byte)UnitLookup.GetCode(unit), (byte)sensorIndex };
    }

    // Overridden from IGaugeProtocol
    public override byte[] CreateZeroSensorCommand(int sensorIndex) {
      return new byte[] { 0x03, (byte)sensorIndex };
    }

    // Overriden from IGaugeProtocol
    public override byte[] CreateSetAltitudeCommand(Scalar altitude) {
      var v = (int)altitude.amount;
      var ub = (byte)((v >> 8) & 0xff);
      var lb = (byte)(v & 0xff);
      return new byte[] { 0x04, ub, lb, (byte)UnitLookup.GetCode(altitude.unit) };
    }
  }
}
