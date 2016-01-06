using System;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Devices.Protocols {
  /// <summary>
  /// A base class that provides methods that are used in the majority of
  /// binary operations.
  /// </summary>
  public abstract class BaseBinaryProtocol : IGaugeProtocol {
    // Overridden from IGaugeProtocol
    public virtual int version { get { return int.MinValue; } }
    // Overridden from IGagueProtocol
    public virtual bool supportsBroadcasting { get { return false; } }

    // Overridden from IGaugeProtocol
    public abstract GaugePacket ParsePacket(byte[] packet);

    // Overridden from IGaugeProtocol
    public abstract byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit);

    // Overridden from IGaugeProtocol
    public abstract byte[] CreateZeroSensorCommand(int sensorIndex);

    // Overridden from IGaugeProtocol
    public abstract byte[] CreateSetAltitudeCommand(Scalar altitude);


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

