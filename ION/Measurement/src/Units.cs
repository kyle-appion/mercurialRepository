namespace Measurement {

  using System;
  using System.Collections.Generic;

  /// <summary>
  /// The units that are present in the standard model.
  /// </summary>
  public class Units {

    /// <summary>
    /// The encapsulation of the standard model of units.
    /// </summary>
    public static class SI {
      public static readonly UnitConverter
        YOTTA = Mc(1e24),
        ZETTA = Mc(1e21),
        EXA = Mc(1e18),
        PETA = Mc(1e15),
        TERA = Mc(1e12),
        GIGA = Mc(1e9),
        MEGA = Mc(1e6),
        KILO = Mc(1e3),
        HECTO = Mc(1e2),
        DECA = Mc(1e1),

        CENTI = Mc(1e-2),
        MILLI = Mc(1e-3),
        MICRO = Mc(1e-6),
        NANO = Mc(1e-9),
        PICO = Mc(1e-12),
        FEMTO = Mc(1e-15),
        ATTO = Mc(1e-18),
        ZEPTO = Mc(1e-21),
        YOCTO = Mc(1e-24);

      public static readonly Unit AMPERE = null;
      public static readonly Unit KELVIN = null;
      public static readonly Unit KILOGRAM = null;
      public static readonly Unit METER = null;
      public static readonly Unit MOLE = null;
      public static readonly Unit SECOND = null;

      /// <summary>
      /// A utility function that will return a simple multiply converter.
      /// </summary>
      private static MultiplicationUnitConverter Mc(double factor) {
        return new MultiplicationUnitConverter(factor);
      }
    }

    public static class Angle {
      public static readonly Unit RADIAN = null;
    }

    public class Pressure {
      public static readonly Unit PASCAL = null;//new BaseUnit(EQuantity.Pressure, "Pascal", "Pa");
    }

    /// <summary>
    /// The dictionary that will map units to there symbols.
    /// </summary>
    internal static Dictionary<Unit, string> UNIT_TO_SYMBOL = new Dictionary<Unit, string>();

    /// <summary>
    /// The current model that is used by the units.
    /// </summary>
    /// <value>The MODE.</value>
    public static IModel MODEL { get; set; }
    static Units() {
      MODEL = new StandardModel();
    }

    /// <summary>
    /// Creates a new base unit.
    /// </summary>
    /// <param name="symbol">Symbol.</param>
    public static Unit Base(string symbol) {
      var ret = new BaseUnit(symbol);

      return ret;
    }

    /// <summary>
    /// Creates a new alternate unit around the given parent unit.
    /// </summary>
    /// <param name="symbol">Symbol.</param>
    /// <param name="parentUnit">Parent unit.</param>
    public static Unit Alt(string symbol, Unit parentUnit) {
      var ret = new AlternateUnit(symbol, parentUnit);

      return ret;
    }

    /// <summary>
    /// Registers the given unit to the system as the given symbol.
    /// </summary>
    /// <param name="symbol">Symbol.</param>
    /// <param name="unit">Unit.</param>
    public static Unit Named(string symbol, Unit unit) {
      if (UNIT_TO_SYMBOL.ContainsKey(unit)) {
        throw new Exception("Cannot add named unit to units: " + symbol + " already exists");
      }

      UNIT_TO_SYMBOL.Add(unit, symbol);

      return unit;
    }

    /// <summary>
    /// Queries the symbol for the given unit, if it is registered.
    /// </summary>
    /// <returns>The symbol for unit.</returns>
    /// <param name="unit">Unit.</param>
    public static string GetSymbolForUnit(Unit unit) {
      return UNIT_TO_SYMBOL[unit];
    }

    /// <summary>
    /// The standard physical model.
    /// </summary>
    public class StandardModel : IModel {
      /// <summary>
      /// Gets the dimension of unit.
      /// </summary>
      /// <returns>The dimension of unit.</returns>
      /// <param name="unit">Unit.</param>
      public Dimension GetDimensionOfUnit(BaseUnit unit) {
        if (Units.SI.AMPERE.Equals(unit)) {
          return Dimension.ELECTRIC_CURRENT;
        } else if (Units.SI.KELVIN.Equals(unit)) {
          return Dimension.TEMPERATURE;
        } else if (Units.SI.KILOGRAM.Equals(unit)) {
          return Dimension.MASS;
        } else if (Units.SI.METER.Equals(unit)) {
          return Dimension.LENGTH;
        } else if (Units.SI.MOLE.Equals(unit)) {
          return Dimension.AMOUNT_OF_SUBSTANCE;
        } else if (Units.SI.SECOND.Equals(unit)) {
          return Dimension.TIME;
        } else {
          return new Dimension(new BaseUnit("[" + unit.symbol + "]"));
        }
      }

      /// <summary>
      /// Queries the normalization transform of the specified base unit.
      /// </summary>
      /// <returns>The transform of unit.</returns>
      /// <param name="unit">Unit.</param>
      public UnitConverter GetTransformOfUnit(BaseUnit unit) {
        return UnitConverter.IDENTITY;
      }
    }
  }
}

