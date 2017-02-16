namespace Appion.Commons.Measure {

  using System;
  using System.Collections.Generic;


  public class Units {
    /// <summary>
    /// The lookup table of unit symbols to units.
    /// </summary>
    public static IDictionary<string, Unit> SYMBOL_TO_UNIT { get { return __SYMBOL_TO_UNIT; } }
    private static Dictionary<string, Unit> __SYMBOL_TO_UNIT = new Dictionary<string, Unit>();

    /// <summary>
    /// A utility function that will return a simple multiply converter.
    /// </summary>
    private static MultiplyConverter Mc(double factor) {
      return new MultiplyConverter(factor);
    }

    /// <summary>
    /// Creates a new BaseUnit.
    /// </summary>
    /// <param name="quantity">The quantity that the unit will live in.</param>
    /// <param name="symbol">The symbol for the unit.</param>
    private static Unit Base(Quantity quantity, string symbol) {
      BaseUnit ret = new BaseUnit(quantity, symbol);

      if (SYMBOL_TO_UNIT.ContainsKey(symbol)) {
        throw new Exception("Cannot create base unit: " + symbol + " is already registered to a unit.");
      }

      SYMBOL_TO_UNIT[symbol] = ret;
      return ret;
    }

    /// <summary>
    /// Creates a new alternate unit.
    /// </summary>
    /// <param name="baseUnit">Base unit.</param>
    /// <param name="quantity">Quantity.</param>
    /// <param name="symbol">Symbol.</param>
    private static Unit Alt(Quantity quantity, Unit unit, string symbol) {
      return new AlternateUnit(quantity, unit, symbol);
    }

    private static Unit Named(Unit unit, string symbol) {
      return new NamedUnit(unit, symbol);
    }

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
    }

    public static class Standard {
      public static readonly Unit APMERE = ElectricCurrent.AMPERE;
      public static readonly Unit KELVIN = Temperature.KELVIN;
      public static readonly Unit KILOGRAM = Mass.KILOGRAM;
      public static readonly Unit METER = Length.METER;
      public static readonly Unit RADIAN = Angle.RADIAN;
      public static readonly Unit SECOND = Time.SECOND;
    }

		public static class Dimensionless {
			public static readonly Unit NONE = Base(Quantity.Dimensionless, "");
		}

    public static class Angle {
      public static readonly Unit RADIAN = Base(Quantity.Angle, "rad");
      public static readonly Unit DEGREE = Named(RADIAN.Mul(System.Math.PI).Div(180), "°");
    }

    public static class ElectricCurrent {
      public static readonly Unit AMPERE = Base(Quantity.ElectricCurrent, "A");
    }

    public static class Force {
      public static readonly Unit NEWTON = Alt(Quantity.Force, Length.METER.Mul(Mass.KILOGRAM).Div(Time.SECOND.Pow(2)), "N");
			public static readonly Unit KILOGRAM = Named(NEWTON.Mul(9.80665), "kgf");
			public static readonly Unit POUND_FORCE = Named(NEWTON.Mul(4.44822), "lbf");
			public static readonly Unit POUND_OUNCE_FORCE = Named(NEWTON.Mul(4.44822), "lbf/oz");
    }

    public static class Humidity {
      public static readonly Unit RELATIVE_HUMIDITY = Base(Quantity.Humidity, "%RH");
    }

    public static class Length {
      public static readonly Unit METER = Base(Quantity.Length, "m");
      public static readonly Unit FOOT = Named(METER.Mul(0.3048), "ft");
      public static readonly Unit INCH = Named(FOOT.Div(12), "in");
      public static readonly Unit MILE = Named(FOOT.Mul(5280), "mi");
    }

    public static class Mass {
      public static readonly Unit KILOGRAM = Base(Quantity.Mass, "kg");
    }

    public static class Pressure {
      public static readonly Unit PASCAL = Alt(Quantity.Pressure, Force.NEWTON.Div(Length.METER.Pow(2)), "Pa");
      public static readonly Unit KILOPASCAL = Named(PASCAL.Transform(SI.KILO), "kPa");
      public static readonly Unit MEGAPASCAL = Named(PASCAL.Transform(SI.MEGA), "mPa");
      public static readonly Unit BAR = Named(PASCAL.Mul(100000), "bar");
      public static readonly Unit MILLIBAR = Named(BAR.Transform(SI.MILLI), "mbar");
      public static readonly Unit ATMOSPHERE = Named(PASCAL.Mul(101325), "atm");
      public static readonly Unit IN_HG = Named(PASCAL.Mul(3386.388333), "inHg");
      public static readonly Unit CM_HG = Named(PASCAL.Mul(1333.224), "cmHg");
      public static readonly Unit KG_CM = Named(PASCAL.Mul(98066.5), "kg/cm²");
//      public static readonly Unit PSIA = Named(PASCAL.Mul(6894.757293).Add(14.6959488), "psia");
      public static readonly Unit PSIA = Named(PASCAL.Mul(6894.757293), "psia");
      public static readonly Unit PSIG = Named(PASCAL.Mul(6894.757293), "psig");
    }

    public static class Temperature {
      public static readonly Unit KELVIN = Base(Quantity.Temperature, "°K");
      public static readonly Unit CELSIUS = Named(KELVIN.Add(273.15), "°C");
      public static readonly Unit FAHRENHEIT = Named(CELSIUS.Mul(5).Div(9).Add(-32), "°F");
    }

    public static class Time {
      public static readonly Unit SECOND = Base(Quantity.Time, "s"); 
      public static readonly Unit MINUTE = Named(SECOND.Mul(60), "m");
      public static readonly Unit HOUR = Named(MINUTE.Mul(60), "h");
    }

    public static class Vacuum {
      public static readonly Unit PASCAL = Alt(Quantity.Vacuum, Force.NEWTON.Div(Length.METER.Pow(2)), "Pa");
      public static readonly Unit KILOPASCAL = Named(PASCAL.Transform(SI.KILO), "kPa");
      public static readonly Unit BAR = Named(PASCAL.Mul(100000), "bar");
      public static readonly Unit MILLIBAR = Named(BAR.Transform(SI.MILLI), "mbar");
      public static readonly Unit ATMOSPHERE = Named(PASCAL.Mul(101325), "atm");
      public static readonly Unit IN_HG = Named(PASCAL.Mul(3386.388333), "inHg");
      public static readonly Unit CM_HG = Named(PASCAL.Mul(1333.224), "cmHg");
      public static readonly Unit KG_CM = Named(PASCAL.Mul(98066.5), "kg/cm²");
//      public static readonly Unit PSIA = Named(PASCAL.Mul(6894.757293).Add(14.6959488), "psia");
      public static readonly Unit PSIA = Named(PASCAL.Mul(6894.757293), "psia");
      public static readonly Unit TORR = Named(PASCAL.Mul(133.3223684), "Torr");
      public static readonly Unit MILLITORR = Named(TORR.Transform(SI.MILLI), "mTorr");
      public static readonly Unit MICRON = Named(MILLITORR, "micron");
    }

		/// <summary>
		/// Weight is just a remap of force as Weight is simply Mass * Gravity in units of newtons.
		/// </summary>
		public static class Weight {
			public static readonly Unit NEWTON = Alt(Quantity.Force, Force.NEWTON, "N");
			public static readonly Unit KILOGRAM = Named(NEWTON.Mul(9.80665), "kgf");
			public static readonly Unit POUND_FORCE = Named(NEWTON.Mul(4.44822), "lbf");
			public static readonly Unit POUND_OUNCE_FORCE = Named(NEWTON.Mul(4.44822), "lbf/oz").SetStringer(amt => {
				var pounds = (int)amt;
				var ounces = (int)((amt - pounds) * 16);
				return pounds + "lb " + ounces + "oz";
			});
		}
  }
}

