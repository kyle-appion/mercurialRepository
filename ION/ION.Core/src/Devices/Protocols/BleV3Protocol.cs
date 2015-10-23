﻿using System;
using System.IO;
using System.Text;

using ION.Core.IO;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Devices.Protocols {
  /// <summary>
  /// The protocol is a subset of the first bluetooth LE protocol that is used
  /// to encode three sensors into a smaller packet that is broadcast capable.
  /// </summary>
  public class BleV3Protocol : BaseBinaryProtocol {
    // Overridden from IGaugeProtocol
    public override int version { get { return 3; } }
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

        int version = r.ReadByte();
        if (version != this.version) {
          throw new ArgumentException("Cannot parse: invalid version code");
        }

        int battery = r.ReadByte();

        int gaugeCount = (len - 2) / 4;
        GaugeReading[] readings = new GaugeReading[gaugeCount];

        for(int i = 0; i < gaugeCount; i++) {
          int exponent = r.ReadByte();
          int encodedReading = r.ReadInt16BE();
          int unitCode = r.ReadByte();

          double reading = encodedReading / System.Math.Pow(10, exponent);

          UnitEntry ue = UnitLookup.GetUnitEntry(unitCode);
          readings[i] = new GaugeReading(ue.sensorType, ue.unit.OfScalar(reading));
        }

        return new GaugePacket(version, battery, readings);
      }
    }

    // Overridden from IGaugeProtocol
    public override byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
      return new byte[] { 0x02, (byte)UnitLookup.GetCode(sensorType, unit), (byte)sensorIndex };
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
      return new byte[] { 0x04, ub, lb, (byte)UnitLookup.GetCode(ESensorType.Length, altitude.unit) };
    }
  }
}

