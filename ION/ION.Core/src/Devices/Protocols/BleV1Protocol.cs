using System;
using System.IO;
using System.Text;

using ION.Core.Measure;
using ION.Core.Util;

namespace ION.Core.Devices.Protocols {
  public class BleV1Protocol : IGaugeProtocol {
    // Overriden from IGaugeProtocol
    int version { get { return 1; } }

    // Override from IGaugeProtocol
    public GaugePacket Parse(byte[] packet) {
      packet = Trim(packet);
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
          int encodedReading = r.ReadByte();
          int unitCode = r.ReadByte();

          double reading = encodedReading / Math.Pow(10, exponent);

          UnitEntry ue = UnitLookup.GetUnitEntry(unitCode);
          readings[i] = new GaugeReading(ue.sensorType, ue.unit.OfScalar(reading));
        }

        return new GaugePacket(version, battery, readings);
      }
    }

    // Overridden from IGaugeProtocol
    public byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
      Log.E(this, "Failed to CreateSetUnitCommand: not implemented");
      return new byte[] { 0x00 };
    }

    // Overridden from IGaugeProtocol
    public byte[] CreateZeroSensorCommand(int sensorIndex) {
      Log.E(this, "Failed to CreateZeroSensorCommand: not implemented");
      return new byte[] { 0x00 };
    }

    // Overriden from IGaugeProtocol
    public byte[] CreateSetAltitudeCommand(Scalar altitude) {
      Log.E(this, "Failed to CreateSetAltitudeCommand: not implemented");
      return new byte[] { 0x00 };
    }

    /// <summary>
    /// Trims the trailing null bytes out of the packet.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected byte[] Trim(byte[] data) {
      int index = data.Length - 1;

      while (data[index] == 0 && index >= 0) {
        index--;
      }

      byte[] ret = new byte[index + 1];
      Array.Copy(data, 0, ret, 0, ret.Length);
      return ret;
    }
  }
}
