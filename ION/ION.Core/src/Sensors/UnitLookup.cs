using System;
using System.Collections.Generic;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors {
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
    /// The dictionary used to lookup units from human readable strings.
    /// </summary>
    private static readonly Dictionary<string, Unit> STRING_TO_UNIT = new Dictionary<string, Unit>();


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
    /// Queries the unit for the given raw string.
    /// </summary>
    /// <returns>The unit.</returns>
    /// <param name="rawString">Raw string.</param>
    public static Unit GetUnit(string rawString) {
      if (STRING_TO_UNIT.ContainsKey(rawString)) {
        return STRING_TO_UNIT[rawString];
      } else {
        return null;
      }
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

    /// <summary>
    /// Adds a new unit lookup.
    /// </summary>
    /// <param name="rawUnit">Raw unit.</param>
    /// <param name="unit">Unit.</param>
    private static void Add(string rawUnit, Unit unit) {
      STRING_TO_UNIT[rawUnit] = unit;
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

      //////////////////////////////////////////////////////
      /// String lookups
      //////////////////////////////////////////////////////

      // Length
      Add("m", Units.Length.METER);
      Add("ft", Units.Length.FOOT);
      Add("in", Units.Length.INCH);
      Add("mi", Units.Length.MILE);

      // Mass
      Add("kg", Units.Mass.KILOGRAM);

      // Pressure
      Add("pa", Units.Pressure.PASCAL);
      Add("kpa", Units.Pressure.KILOPASCAL);
      Add("mpa", Units.Pressure.MEGAPASCAL);
      Add("bar", Units.Pressure.BAR);
      Add("millibar", Units.Pressure.MILLIBAR);
      Add("atmo", Units.Pressure.ATMOSPHERE);
      Add("inhg", Units.Pressure.IN_HG);
      Add("cmhg", Units.Pressure.CM_HG);
      Add("kgcm", Units.Pressure.KG_CM);
      Add("psia", Units.Pressure.PSIA);
      Add("psig", Units.Pressure.PSIG);
      Add("torr", Units.Pressure.TORR);
      Add("millitorr", Units.Pressure.MILLITORR);
      Add("micron", Units.Pressure.MICRON);

      // Temperature
      Add("kelvin", Units.Temperature.KELVIN);
      Add("celsius", Units.Temperature.CELSIUS);
      Add("fahrenheit", Units.Temperature.FAHRENHEIT);

      // Time
      Add("second", Units.Time.SECOND);
      Add("minute", Units.Time.HOUR);
      Add("hour", Units.Time.HOUR);
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
