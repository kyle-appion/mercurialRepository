namespace ION.Core.Math {

  using System;

	using Appion.Commons.Measure;

  /// <summary>
  /// A collection of functions that compute physics calculations.
  /// </summary>
  public static class Physics {
		public static ScalarSpan PRESSURE_AT_SEA_LEVEL = Units.Pressure.PSIA.OfSpan(14.6959488);
    /// <summary>
    /// Queries the atmospheric pressure at a given elevation.
    /// </summary>
    /// <remarks>
    /// This function does not account for weather or natural effects.
    /// </remarks>
    /// <returns>The pressure from elevation.</returns>
    /// <param name="elevation">Elevation.</param>
    public static ScalarSpan AtmosphericPressureFromElevation(Scalar elevation) {
      var meters = elevation.ConvertTo(Units.Length.METER);
      var pascals = 101325 * Math.Pow(1 - (2.25577 * Math.Pow(10, -5) * meters.amount), 5.25588); 
      return Units.Pressure.PASCAL.OfSpan(pascals);
    }

		/// <summary>
		/// Queries the pressure that is adjusted for the given elevation.
		/// For example: if the given pressure is 20 psig and the elevation is 5000 ft, then the absolute pressure is
		/// actually, 17.5 psia. At 5000 ft, the "pressure lose" from elevation is approximately 2.5 psi. Thus, we will need
		/// to subtract that psi from the gauge pressure to convert to absolute. 
		/// </summary>
		/// <returns>The pressure adjusted for elevation.</returns>
		/// <param name="gaugePressure">Gauge pressure.</param>
		/// <param name="elevation">Elevation.</param>
		public static Scalar GetGaugePressureAdjustedForElevation(Scalar gaugePressure, Scalar elevation) {
			var adjusted = PRESSURE_AT_SEA_LEVEL - AtmosphericPressureFromElevation(elevation);
			return gaugePressure + adjusted;
		}

    /// <summary>
    /// Queries the pressure adjustment that must be made for relative pressures to convert them to absolute.
    /// </summary>
    /// <returns>The span adjustment for elevation.</returns>
    /// <param name="elevation">Elevation.</param>
    public static ScalarSpan GetSpanAdjustmentForElevation(Scalar elevation) {
      var kpa = Units.Pressure.KILOPASCAL;
      return (PRESSURE_AT_SEA_LEVEL - AtmosphericPressureFromElevation(elevation)).ConvertTo(kpa);
    }

    /// <summary>
    /// Converts the given relative pressure to an absolute pressure.
    /// </summary>
    /// <remarks>
    /// A relative pressure is a pressure that does not account for any external
    /// affects (such as elevation, weather etc...). Simply, a relative pressure
    /// us a pressure that is relative to a zero that does not neccessarily needs
    /// to be absolute zero.
    /// </remarks>
    /// <returns>The relative pressure to absolute.</returns>
    /// <param name="gaugePressure">Gauge pressure.</param>
    /// <param name="elevation">Elevation.</param>
    public static Scalar ConvertRelativePressureToAbsolute(Scalar gaugePressure, Scalar elevation) {
      return gaugePressure + AtmosphericPressureFromElevation(elevation);
    }

    /// <summary>
    /// Converts the given absolute pressure to a relative pressure.
    /// </summary>
    /// <remarks>
    /// A relative pressure is a pressure that does not account for any external
    /// affects (such as elevation, weather etc...). Simply, a relative pressure
    /// us a pressure that is relative to a zero that does not neccessarily needs
    /// to be absolute zero.
    /// </remarks>
    /// <returns>The absolute pressure to relative.</returns>
    /// <param name="absolutePressure">Absolute pressure.</param>
    /// <param name="elevation">Elevation.</param>
    public static Scalar ConvertAbsolutePressureToRelative(Scalar absolutePressure, Scalar elevation) {
      return absolutePressure - AtmosphericPressureFromElevation(elevation);
    }
  }
}

