using System;
using System.Collections.Generic;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors {
  public class UnitLookup {
    /// <summary>
    /// The dictionary used to lookup units given a unit code.
    /// </summary>
    private static readonly Dictionary<int, Unit> CODE_TO_UNIT = new Dictionary<int, Unit>();
    /// <summary>
    /// The dictionary used to lookup unit given a unit code.
    /// </summary>
    private static readonly Dictionary<Unit, int> UNIT_TO_CODE = new Dictionary<Unit, int>();

    /// <summary>
    /// Queries the unit for the given code.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Unit GetUnit(int code) {
      try {
      return CODE_TO_UNIT[code];
      } catch (Exception e) {
        ION.Core.Util.Log.D("UnitLookup", "invalid unit code: " + code);
        throw e;
      }
    }


    /// <summary>
    /// Queries the unit code for the given unit.
    /// </summary>
    /// <returns>The code.</returns>
    /// <param name="unit">Unit.</param>
    public static int GetCode(Unit unit) {
      return UNIT_TO_CODE[unit];
    }

    /// <summary>
    /// Queries the expected sensor type from the given unit code.
    /// </summary>
    /// <returns>The sensor type from code.</returns>
    /// <param name="code">Code.</param>
    public static ESensorType GetSensorTypeFromCode(int code) {
      if (code >= 0x00 && code <= 0x0a) {
        return ESensorType.Pressure;
      } else if (code >= 0x10 && code <= 0x12) {
        return ESensorType.Temperature;
      } else if (code == 0x13) {
        return ESensorType.Humidity;
      } else if (code == 0x14 || code == 0x15) {
        return ESensorType.Length;
      } else if (code >= 0x16 && code <= 0x1a) {
        return ESensorType.Mass;
      } else if (code >= 0x20 && code <= 0x2a) {
        return ESensorType.Vacuum;
      } else {
        throw new ArgumentException("Cannot find sensor type for code: " + code);
      }
    }

    public static Unit GetUnit(ESensorType sensorType, string code) {
      switch (sensorType) {
        case ESensorType.Humidity:
          return GetHumidityUnit(code);
        case ESensorType.Length:
          return GetLengthUnit(code);
        case ESensorType.Mass:
          return GetMassUnit(code);
        case ESensorType.Pressure:
          return GetPressureUnit(code);
        case ESensorType.Temperature:
          return GetTemperatureUnit(code);
        case ESensorType.Vacuum:
          return GetVacuumUnit(code);
        default:
          throw new ArgumentException("Cannot find unit of type: " + sensorType + " for code " + code);
      }
    }

    private static Unit GetHumidityUnit(string code) {
      switch (code) {
        case "%rh":
          return Units.Humidity.RELATIVE_HUMIDITY;
        default:
          throw new ArgumentException("Cannot find humidity unit for code: " + code);
      }
    }

    private static Unit GetLengthUnit(string code) {
      switch (code) {
        case "ft":
          return Units.Length.FOOT;
        case "m":
          return Units.Length.METER;
        default:
          throw new ArgumentException("Cannot find length unit for code: " + code);
      }
    }

    private static Unit GetMassUnit(string code) {
      switch (code) {
        case "kg":
          return  Units.Mass.KILOGRAM;
        default:
          throw new ArgumentException("Cannot find length mass for code: " + code);
      }
    }

    private static Unit GetPressureUnit(string code) {
      switch (code) {
        case "pa":
          return Units.Pressure.PASCAL;
        case "kpa":
          return Units.Pressure.KILOPASCAL;
        case "mpa":
          return Units.Pressure.MEGAPASCAL;
        case "bar":
          return Units.Pressure.BAR;
        case "millibar":
          return Units.Pressure.MILLIBAR;
        case "atmo":
          return Units.Pressure.ATMOSPHERE;
        case "inhg":
          return Units.Pressure.IN_HG;
        case "cmhg":
          return Units.Pressure.CM_HG;
        case "kgcm":
          return Units.Pressure.KG_CM;
        case "psia":
          return Units.Pressure.PSIA;
        case "psig":
          return Units.Pressure.PSIG;
        default:
          throw new ArgumentException("Cannot find pressure unit for code: " + code);
      }
    }

    private static Unit GetTemperatureUnit(string code) {
      switch (code) {
        case "celsius":
          return Units.Temperature.CELSIUS;
        case "fahrenheit":
          return Units.Temperature.FAHRENHEIT;
        case "kelvin":
          return Units.Temperature.KELVIN;
        default:
          throw new ArgumentException("Cannot find temperature unit for code: " + code);
      }
    }

    private static Unit GetVacuumUnit(string code) {
      switch (code) {
        case "pa":
          return Units.Vacuum.PASCAL;
        case "kpa":
          return Units.Vacuum.KILOPASCAL;
        case "bar":
          return Units.Vacuum.BAR;
        case "millibar":
          return Units.Vacuum.MILLIBAR;
        case "atmo":
          return Units.Vacuum.ATMOSPHERE;
        case "inhg":
          return Units.Vacuum.IN_HG;
        case "cmhg":
          return Units.Vacuum.CM_HG;
        case "kgcm":
          return Units.Vacuum.KG_CM;
        case "psia":
          return Units.Vacuum.PSIA;
        case "torr":
          return Units.Vacuum.TORR;
        case "millitorr":
          return Units.Vacuum.MILLITORR;
        case "micron":
          return Units.Vacuum.MICRON;
        default:
          throw new ArgumentException("Cannot find vacuum unit for code: " + code);
      }
    }

    /// <summary>
    /// Adds a new unit/code lookup.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="sensorType"></param>
    /// <param name="unit"></param>
    private static void Add(int code, Unit unit) {
      CODE_TO_UNIT[code] = unit;
      UNIT_TO_CODE[unit] = code;
    }

    static UnitLookup() {
      // Pressure
      Add(0x01, Units.Pressure.KILOPASCAL);
      Add(0x02, Units.Pressure.MEGAPASCAL);
      Add(0x03, Units.Pressure.BAR);
      Add(0x04, Units.Pressure.CM_HG);
      Add(0x05, Units.Pressure.IN_HG);
      Add(0x06, Units.Pressure.PSIA);
      Add(0x07, Units.Pressure.PSIG);
      Add(0x08, Units.Pressure.KG_CM);

      // Temperature
      Add(0x10, Units.Temperature.CELSIUS);
      Add(0x11, Units.Temperature.KELVIN);
      Add(0x12, Units.Temperature.FAHRENHEIT);

      // Humidity
      Add(0x13, Units.Humidity.RELATIVE_HUMIDITY);

      // Length
      Add(0x14, Units.Length.FOOT);
      Add(0x15, Units.Length.METER);

      // Vacuum
      Add(0x20, Units.Vacuum.PASCAL);
      Add(0x21, Units.Vacuum.KILOPASCAL);
      Add(0x22, Units.Vacuum.BAR);
      Add(0x23, Units.Vacuum.MILLIBAR);
      Add(0x24, Units.Vacuum.CM_HG);
      Add(0x25, Units.Vacuum.IN_HG);
      Add(0x26, Units.Vacuum.PSIA);
      Add(0x27, Units.Vacuum.MICRON);
    }
  }
}
