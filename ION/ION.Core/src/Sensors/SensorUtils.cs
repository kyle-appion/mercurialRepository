namespace ION.Core.Sensors
{

	using System;
	using System.Collections.Generic;

	using Appion.Commons.Measure;


	/// <summary>
	/// A simple utility class that will provide functions that aid in the use of the
	/// ESensorType in conjunction with an ISensor.
	/// </summary>
	public partial class SensorUtils
	{
		/// <summary>
		/// The default pressure units to use for sensors that do not provide their own
		/// unit list.
		/// </summary>
		public static Unit[] DEFAULT_PRESSURE_UNITS = new Unit[] {
//      Units.Pressure.PSIA,
      Units.Pressure.PSIG,
	  Units.Pressure.BAR,
	  Units.Pressure.IN_HG,
	  Units.Pressure.CM_HG,
	  Units.Pressure.KG_CM,
	  Units.Pressure.KILOPASCAL,
	  Units.Pressure.MEGAPASCAL,
	};

		/// <summary>
		/// The default temperature units to use for sensors that do not provide their
		/// own unit list.
		/// </summary>
		public static Unit[] DEFAULT_TEMPERATURE_UNITS = new Unit[] {
	  Units.Temperature.FAHRENHEIT,
	  Units.Temperature.CELSIUS,
	  Units.Temperature.KELVIN,
	};

		/// <summary>
		/// The default vacuum units to use for senors that do not provide their own
		/// unit list.
		/// </summary>
		public static Unit[] DEFAULT_VACUUM_UNITS = new Unit[] {
	  Units.Vacuum.MICRON,
	  Units.Vacuum.IN_HG,
	  Units.Vacuum.MILLITORR,
	  Units.Vacuum.PSIA,
	  Units.Vacuum.KILOPASCAL,
	};

		/// <summary>
		/// The default weight units to use for senors that do not provide their own
		/// unit list.
		/// </summary>
		public static Unit[] DEFAULT_WEIGHT_UNITS = new Unit[] {
	  Units.Weight.KILOGRAM,
	  Units.Weight.POUND_FORCE,
	  Units.Weight.POUND_OUNCE_FORCE,
	};

		/// <summary>
		/// Builds and returns a mapping of the application default units to sensor type mapping.
		/// </summary>
		/// <returns>The sensor type unit mapping.</returns>
		public static Dictionary<ESensorType, IEnumerable<Unit>> GetSensorTypeUnitMapping()
		{
			var ret = new Dictionary<ESensorType, IEnumerable<Unit>>();

			ret[ESensorType.Pressure] = DEFAULT_PRESSURE_UNITS;
			ret[ESensorType.Temperature] = DEFAULT_TEMPERATURE_UNITS;
			ret[ESensorType.Vacuum] = DEFAULT_VACUUM_UNITS;
			ret[ESensorType.Weight] = DEFAULT_WEIGHT_UNITS;

			return ret;
		}

		public static ESensorType FromString(string sensorType)
		{
			var ret = ESensorType.Unknown;
			Enum.TryParse(sensorType, out ret);
			return ret;
		}

		[Obsolete("Use 'ToFormattedString(Scalar measurement, bool includeUnit = false)' instead")]
		public static string ToFormattedString(ESensorType sensorType, Scalar measurement, bool includeUnit = false)
		{
			return ToFormattedString(measurement, includeUnit);
		}

		public static string ToFormattedString(Scalar measurement, bool includeUnit = false)
		{
			return ToFormattedString(measurement.amount, measurement.unit, includeUnit);
		}

		public static string ToFormattedString(ScalarSpan measurement, bool includeUnit = false)
		{
			return ToFormattedString(measurement.magnitude, measurement.unit, includeUnit);
		}

		public static string ToFormattedString(double amount, Unit unit, bool includeUnit = false)
		{
			string ret = "";

			if (double.IsNaN(amount))
			{
				ret = "---";
			}
			else
			{
				// PRESSURE UNITS
				if (Units.Pressure.PASCAL.Equals(unit))
				{
					ret = amount.ToString("0");
				}
				else if (Units.Pressure.KILOPASCAL.Equals(unit))
				{
					ret = amount.ToString("0");
				}
				else if (Units.Pressure.MEGAPASCAL.Equals(unit))
				{
					ret = amount.ToString("0.000");
				}
				else if (Units.Pressure.MILLIBAR.Equals(unit))
				{
					ret = amount.ToString("0.000");
				}
				else if (Units.Pressure.PSIG.Equals(unit))
				{
					ret = amount.ToString("0.0");
				}
				else if (Units.Pressure.PSIA.Equals(unit))
				{
					ret = amount.ToString("0.0000");
				}
				else if (Units.Pressure.IN_HG.Equals(unit))
				{
					ret = amount.ToString("0.00");
				}
				// VACUUM PRESSURE
				else if (Units.Vacuum.IN_HG.Equals(unit))
				{
					ret = amount.ToString("0.000");
				}
				else if (Units.Vacuum.KILOPASCAL.Equals(unit))
				{
					ret = amount.ToString("0.0000");
				}
				else if (Units.Vacuum.MICRON.Equals(unit))
				{
					ret = amount.ToString("###,##0");
				}
				else if (Units.Vacuum.MILLITORR.Equals(unit))
				{
					ret = amount.ToString("###,##0");
				}
				else if (Units.Vacuum.MILLIBAR.Equals(unit))
				{
					ret = amount.ToString("000.000");
				}
				// DEFAULT
				else
				{
					ret = amount.ToString("0.00");
				}
			}

			if (includeUnit)
			{
				ret += " " + unit.ToString();
			}

			return ret;
		}

		public static string ToFormattedString(ESensorType sensorType, ScalarSpan measurement, bool includeUnit = false)
		{
			var unit = measurement.unit;
			var amount = measurement.magnitude;

			string ret = "";

			if (double.IsNaN(amount))
			{
				ret = "---";
			}
			else
			{
				// PRESSURE UNITS
				if (Units.Pressure.PASCAL.Equals(unit))
				{
					ret = amount.ToString("0");
				}
				else if (Units.Pressure.KILOPASCAL.Equals(unit))
				{
					if (ESensorType.Vacuum == sensorType)
					{
						ret = amount.ToString("0.0000");
					}
					else
					{
						ret = amount.ToString("0");
					}
				}
				else if (Units.Pressure.MEGAPASCAL.Equals(unit))
				{
					ret = amount.ToString("0.000");
				}
				else if (Units.Pressure.MILLIBAR.Equals(unit))
				{
					ret = amount.ToString("0.000");
				}
				else if (Units.Pressure.PSIG.Equals(unit))
				{
					ret = amount.ToString("0.0");
				}
				else if (Units.Pressure.PSIA.Equals(unit))
				{
					ret = amount.ToString("0.0000");
				}
				else if (Units.Pressure.IN_HG.Equals(unit))
				{
					if (ESensorType.Vacuum == sensorType)
					{
						ret = amount.ToString("0.000");
					}
					else
					{
						ret = amount.ToString("0.00");
					}
				}
				// VACUUM PRESSURE
				else if (Units.Vacuum.MICRON.Equals(unit))
				{
					ret = amount.ToString("###,##0");
				}
				else if (Units.Vacuum.MILLITORR.Equals(unit))
				{
					ret = amount.ToString("###,##0");
				}
				// DEFAULT
				else
				{
					ret = amount.ToString("0.00");
				}
			}

			if (includeUnit)
			{
				ret += " " + unit.ToString();
			}

			return ret;
		}

		/// <summary>
		/// Determines whether or not the given unit is valid with the provided sensor type.
		/// </summary>
		/// <param name="sensorType"></param>
		/// <param name="unit"></param>
		/// <returns>True if the unit is valid for the sensor type, false otherwise.</returns>
		public static bool IsCompatibleWith(ESensorType sensorType, Unit unit)
		{
			switch (sensorType)
			{
				case ESensorType.Length:
					{
						return unit.IsCompatible(Units.Length.METER);
					}
				case ESensorType.Humidity:
					{
						return unit.IsCompatible(Units.Humidity.RELATIVE_HUMIDITY);
					}
				case ESensorType.Weight:
					{
						return unit.IsCompatible(Units.Weight.KILOGRAM);
					}
				case ESensorType.Pressure:
					{
						return unit.IsCompatible(Units.Pressure.PASCAL);
					}
				case ESensorType.Temperature:
					{
						return unit.IsCompatible(Units.Temperature.KELVIN);
					}
				case ESensorType.Vacuum:
					{
						return unit.IsCompatible(Units.Vacuum.MICRON);
					}
				default:
					{
						return false;
					}
			}
		}
	}
}
