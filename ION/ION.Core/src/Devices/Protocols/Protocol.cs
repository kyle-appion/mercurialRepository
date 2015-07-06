﻿using System;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Devices.Protocols {
  public interface IProtocol<T> {
    /// <summary>
    /// Queries the version of the protocol.
    /// </summary>
    int version { get; }

    /// <summary>
    /// Parsed the provided packet. If the packet cannot be parsed, this method will
    /// throw an argument exception.
    /// </summary>
    /// <param name="packet">The packet to parse.</param>
    /// <returns></returns>
    T Parse(byte[] packet);
  }

  public interface IGaugeProtocol : IProtocol<GaugePacket> {
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
    public int version { get; private set; }
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
    public GaugePacket(int version, int battery, GaugeReading[] readings) {
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
    /// Queries the sensor type of the sensor whose reading is represented.
    /// </summary>
    public ESensorType sensorType { get; private set; }
    /// <summary>
    /// Queries the reading of the represented sensor.
    /// </summary>
    public Scalar reading { get; private set; }

    public GaugeReading(ESensorType sensorType, Scalar reading) {
      this.sensorType = sensorType;
      this.reading = reading;
    }
  }
}
