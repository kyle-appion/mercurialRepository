namespace ION.Core.Math {

  using System;

  using ION.Core.Measure;

  /// <summary>
  /// A collection of functions that compute physics calculations.
  /// </summary>
  public static class Physics {
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

