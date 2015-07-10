using System;
using System.Collections.Generic;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Devices.Protocols {
  public class UnitLookup {
    /// <summary>
    /// The dictionary used to lookup units given a unit code.
    /// </summary>
    private static readonly Dictionary<int, UnitEntry> CODE_TO_UNIT = new Dictionary<int, UnitEntry>();
    /// <summary>
    /// The dictionary used to lookup unit given a unit code.
    /// </summary>
    private static readonly Dictionary<UnitEntry, int> UNIT_TO_CODE = new Dictionary<UnitEntry, int>();

    /// <summary>
    /// Queries the unit entry that is paied to the given code.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static UnitEntry GetUnitEntry(int code) {
      return CODE_TO_UNIT[code];
    }

    /// <summary>
    /// Queries the unit for the given code.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Unit GetUnit(int code) {
      return CODE_TO_UNIT[code].unit;
    }

    /// <summary>
    /// Queries the code for the given unit endoding.
    /// </summary>
    /// <param name="sensorType"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static int GetCode(ESensorType sensorType, Unit unit) {
      return UNIT_TO_CODE[new UnitEntry(sensorType, unit)];
    }

    /// <summary>
    /// Adds a new unit/code lookup.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="sensorType"></param>
    /// <param name="unit"></param>
    private static void Add(int code, ESensorType sensorType, Unit unit) {
      UnitEntry entry = new UnitEntry(sensorType, unit);

      CODE_TO_UNIT[code] = entry;
      UNIT_TO_CODE[entry] = code;
    }

    static UnitLookup() {
      // Pressure
      Add(0x01, ESensorType.Pressure, Units.Pressure.KILOPASCAL);
      Add(0x02, ESensorType.Pressure, Units.Pressure.MEGAPASCAL);
      Add(0x03, ESensorType.Pressure, Units.Pressure.BAR);
      Add(0x04, ESensorType.Pressure, Units.Pressure.CM_HG);
      Add(0x05, ESensorType.Pressure, Units.Pressure.IN_HG);
      Add(0x06, ESensorType.Pressure, Units.Pressure.PSIA);
      Add(0x07, ESensorType.Pressure, Units.Pressure.PSIG);
      Add(0x08, ESensorType.Pressure, Units.Pressure.KG_CM);

      // Temperature
      Add(0x10, ESensorType.Temperature, Units.Temperature.CELSIUS);
      Add(0x11, ESensorType.Temperature, Units.Temperature.KELVIN);
      Add(0x12, ESensorType.Temperature, Units.Temperature.FAHRENHEIT);

      // Humidity
      Add(0x13, ESensorType.Humidity, Units.Humidity.RELATIVE_HUMIDITY);

      // Length
      Add(0x14, ESensorType.Length, Units.Length.FOOT);
      Add(0x15, ESensorType.Length, Units.Length.METER);

      // Vacuum
      Add(0x20, ESensorType.Vacuum, Units.Pressure.PASCAL);
      Add(0x21, ESensorType.Vacuum, Units.Pressure.KILOPASCAL);
      Add(0x22, ESensorType.Vacuum, Units.Pressure.MEGAPASCAL);
      Add(0x23, ESensorType.Vacuum, Units.Pressure.MILLIBAR);
      Add(0x24, ESensorType.Vacuum, Units.Pressure.CM_HG);
      Add(0x25, ESensorType.Vacuum, Units.Pressure.IN_HG);
      Add(0x26, ESensorType.Vacuum, Units.Pressure.PSIA);
      Add(0x27, ESensorType.Vacuum, Units.Pressure.MICRON);
      Add(0x28, ESensorType.Vacuum, Units.Pressure.TORR);
      Add(0x29, ESensorType.Vacuum, Units.Pressure.MILLITORR);
    }
  }

  public struct UnitEntry {
    /// <summary>
    /// The sensor type who expects the provided unit.
    /// </summary>
    public ESensorType sensorType { get; private set; }
    /// <summary>
    /// The unit.
    /// </summary>
    public Unit unit { get; private set; }

    public UnitEntry(ESensorType sensorType, Unit unit) : this() {
      this.sensorType = sensorType;
      this.unit = unit;
    }

    public override bool Equals(object obj) {
      return obj is UnitEntry && this == (UnitEntry)obj;
    }

    public override int GetHashCode() {
      return sensorType.GetHashCode() ^ unit.GetHashCode();
    }

    public static bool operator ==(UnitEntry x, UnitEntry y) {
      return x.sensorType == y.sensorType && x.unit.Equals(y.unit);
    }

    public static bool operator !=(UnitEntry x, UnitEntry y) {
      return x.sensorType != y.sensorType && x.unit.Equals(y.unit);
    }
  }
}
