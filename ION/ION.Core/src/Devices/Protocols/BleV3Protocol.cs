namespace ION.Core.Devices.Protocols {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;

  using ION.Core.IO;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;
  
  /// <summary>
  /// The protocol is a subset of the first bluetooth LE protocol that is used
  /// to encode three sensors into a smaller packet that is broadcast capable.
  /// </summary>
  public class BleV3Protocol : BaseBinaryProtocol {
    // Overridden from IGaugeProtocol
    public override EProtocolVersion version { get { return EProtocolVersion.V3; } }
    // Overridden from IGaugeProtocol
    public override int removedGaugeValue { get { return short.MaxValue; } }
    // Overridden from IGagueProtocol
    public override bool supportsBroadcasting { get { return true; } }

    // Overridden from IGaugeProtocol
    public override GaugePacket ParsePacket(byte[] packetIn) {
      byte[] packet = Trim(packetIn);
      using (BinaryReader r = new BinaryReader(new MemoryStream(packet))) {
        int len = packet.Length;

        if (len < 6 || len % 4 != 2) {
          throw new ArgumentException("Cannot parse: bad packet size {" + len + "}");
        }

        var v = (EProtocolVersion)((int)r.ReadByte());
        if (v != this.version) {
          throw new ArgumentException("Cannot parse: invalid version code");
        }

        int battery = r.ReadByte();

        int gaugeCount = (len - 2) / 4;
        var readings = new List<GaugeReading>();

        for(int i = 0; i < gaugeCount; i++) {
          int exponent = r.ReadByte();
          int encodedReading = r.ReadInt16BE();
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

        return new GaugePacket(v, battery, readings.ToArray());
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

