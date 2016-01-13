using System;
using System.IO;
using System.Text;

using ION.Core.IO;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Devices.Protocols {
  /// <summary>
  /// This protocol was the first broadcasting protocol.
  /// </summary>
  public class BleV2Protocol : BaseBinaryProtocol {
    // Overriden from IGaugeProtocol
    public override int version { get { return 2; } }
    // Overridden from IGagueProtocol
    public override bool supportsBroadcasting { get { return true; } }

    // Overridden from IGaugeProtocol
    public override GaugePacket ParsePacket(byte[] packetIn) {
      byte[] packet = Trim(packetIn);
      using (BinaryReader r = new BinaryReader(new MemoryStream(packet))) {
        int len = packet.Length;

        if (len < 8 || len % 6 != 2) {
          throw new ArgumentException("Cannot parse: bad packet size {" + len + "}");
        }

        int version = r.ReadByte();

        if (version != this.version) {
          throw new ArgumentException("Cannot parse: invalid version code");
        }

        int battery = r.ReadByte();

        int gaugeCount = (len - 2) / 6;
        GaugeReading[] readings = new GaugeReading[gaugeCount];

        for (int i = 0; i < gaugeCount; i++) {
          int exponent = r.ReadByte();
          int encodedReading = r.ReadInt32BE();
          int unitCode = r.ReadByte();

          double reading = encodedReading / System.Math.Pow(10, exponent);

          Unit unit = UnitLookup.GetUnit(unitCode);
          readings[i] = new GaugeReading(UnitLookup.GetSensorTypeFromCode(unitCode), unit.OfScalar(reading));
        }

        return new GaugePacket(version, battery, readings);
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
