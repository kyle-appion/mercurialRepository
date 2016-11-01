namespace ION.Core.Fluids {

  using System;
  using ION.Core.Util;
  using ION.Core.Measure;

  /// <summary>
  /// The fluid class is a lookup table that is used to calculate pressures and
  /// temperatures for refrigerant fluids.
  /// </summary>
  // TODO ahodder@appioninc.com: Calculate temperatures as needed in place of the temperature array
  public class Fluid {
    /// <summary>
    /// The unit that all the temperatures are in.
    /// </summary>
    private static readonly Unit TEMPERATURE = Units.Temperature.KELVIN;
    /// <summary>
    /// The unit that all the pressures are in.
    /// </summary>
    private static readonly Unit PRESSURE = Units.Pressure.KILOPASCAL;

    /// <summary>
    /// The name of the fluid.
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// The ARGB8888 color of the fluid.
    /// </summary>
    /// <value>The color.</value>
    public int color { get; set; }
    /// <summary>
    /// Queries whether or not the fluid is a mixture.
    /// </summary>
    /// <value><c>true</c> if mixute; otherwise, <c>false</c>.</value>
    public bool mixture { get; private set; }

    /// <summary>
    /// The minimum temperature for the fluid. This is necessary for all lookups.
    /// </summary>
    private double tmin { get; set; }
    /// <summary>
    /// The maximum temperature for the fluid. This is necessary for all lookups.
    /// </summary>
    private double tmax { get; set; }
    /// <summary>
    /// The interval between temperatures within the table.
    /// </summary>
    private double step { get; set; }
    /// <summary>
    /// The number of rows in the table.
    /// </summary>
    private int rows { get; set; }
    /// <summary>
    /// The temperature column.
    /// </summary>
    private double[] temperatures { get; set; }
    /// <summary>
    /// The the pressures for the bubble points matched to the temperature column.
    /// </summary>
    private double[] pressureValues { get; set; }

    /// <summary>
    /// Creates a new fluid.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tmin"></param>
    /// <param name="tmax"></param>
    /// <param name="step"></param>
    /// <param name="rows"></param>
    /// <param name="temperatures"></param>
    /// <param name="bubblePressures"></param>
    /// <param name="dewPressures"></param>
    public Fluid(string name, bool mixture, double tmin, double tmax, double step, int rows, double[] temperatures, double[] pressureValues) {
      this.name = name;
      this.mixture = mixture;
      this.tmin = tmin;
      this.tmax = tmax;
      this.step = step;
      this.rows = rows;
      this.temperatures = temperatures;
      this.pressureValues = pressureValues;
    }

    /// <summary>
    /// Queries the minimum temperature of the fluid.
    /// </summary>
    /// <returns>The minimum temperature.</returns>
    public Scalar GetMinimumTemperature() {
      return TEMPERATURE.OfScalar(tmin);
    }

    /// <summary>
    /// Queries the maximum temperature of the fluid.
    /// </summary>
    /// <returns>The maximum temperature.</returns>
    public Scalar GetMaximumTemperature() {
      return TEMPERATURE.OfScalar(tmax);
    }

    /// <summary>
    /// Queries the minimum pressure of the fluid in the given state.
    /// </summary>
    /// <returns>The pressure.</returns>
    /// <param name="state">State.</param>
    public Scalar GetMinimumPressure(EState state) {
      if (!mixture) {
        return PRESSURE.OfScalar(pressureValues[0]);
      }

      switch (state) {
        case EState.Bubble:
          return PRESSURE.OfScalar(pressureValues[0]);
        case EState.Dew:
          return PRESSURE.OfScalar(pressureValues[rows]);
        default:
          throw new ArgumentException("Cannot get minimum pressure for state: " + state);
      }
    }

    /// <summary>
    /// Queries the maximim pressure of the fluid in the given state.
    /// </summary>
    /// <returns>The maximum pressure.</returns>
    /// <param name="state">State.</param>
    public Scalar GetMaximumPressure(EState state) {
      if (!mixture) {
        return PRESSURE.OfScalar(pressureValues[rows - 1]);
      }

      switch (state) {
        case EState.Bubble:
          return PRESSURE.OfScalar(pressureValues[rows - 1]);
        case EState.Dew:         
          return PRESSURE.OfScalar(pressureValues[2 * rows - 1]);
        default:
          throw new ArgumentException("Cannot get minimum pressure for state: " + state);
      }
    }

		/// <summary>
		/// Queries the median pressure for the state.
		/// </summary>
		/// <returns>The median pressure.</returns>
		/// <param name="state">State.</param>
		public double GetMedianPressure(EState state) {
			var index = (state == EState.Dew) ? rows : 0;
			return index + rows / 2;
		}

    /// <summary>
    /// Queries the expected temperature of the fluid at the given bubble point 
    /// pressure.
    /// </summary>
    /// <param name="pressure">The absolute pressure to match to an expected temperature.</param>
    /// <returns></returns>
    public Scalar GetTemperatureFromAbsolutePressure(EState state, Scalar pressure) {
      pressure = pressure.ConvertTo(PRESSURE);
      Scalar pmin = GetMinimumPressure(state), pmax = GetMaximumPressure(state);

      if (pressure < pmin || pressure > pmax) {
        return TEMPERATURE.OfScalar(double.NaN);
      } else if (pressure == pmin) {
        return TEMPERATURE.OfScalar(temperatures[0]);
      } else if (pressure == pmax) {
        return TEMPERATURE.OfScalar(temperatures[rows - 1]); 
      }

      var offset = (mixture && EState.Dew == state) ? rows : 0;
      int i = BinSearch(pressureValues, pressure.amount, 0 + offset, rows + offset);

      //if (i >= 0 - 1) {
      if (i >= 0) {
        return TEMPERATURE.OfScalar(temperatures[i]);
      } else {
        i = ~i;
        if (i >= pressureValues.Length) {
          return TEMPERATURE.OfScalar(temperatures[i - offset]);
        } else {
          double magnitude = FindMagnitudeOf(pressure.amount, pressureValues[i], pressureValues[i + 1]);
          return TEMPERATURE.OfScalar(Interpolate(magnitude, temperatures[i % rows], temperatures[(i + 1) % rows]));
        }
      }
    }

    /// <summary>
    /// Queries the expected bubble pressure of the fluid at the given temperature.
    /// </summary>
    /// <param name="temperature"></param>
    /// <returns></returns>
    public Scalar GetPressureFromTemperature(EState state, Scalar temperature) {
      temperature = temperature.ConvertTo(TEMPERATURE);
      // The offset to the start of the proper pressure slice
      var offset = (mixture && EState.Dew == state) ? rows : 0;

      if (temperature < tmin || temperature > tmax) {
        return PRESSURE.OfScalar(double.NaN);
      } else if (temperature.amount == tmin) {
        return PRESSURE.OfScalar(pressureValues[0 + offset]);
      } else if (temperature.amount == tmax) {
        return PRESSURE.OfScalar(pressureValues[rows - 1 + offset]);
      }

      int i = BinSearch(temperatures, temperature.amount, 0, rows);

      if (i >= 0) {
        return PRESSURE.OfScalar(pressureValues[i + offset]);
      } else {
        i = ~i;
        if (i >= temperatures.Length - 1) {
          return PRESSURE.OfScalar(pressureValues[offset + i]);
        } else {
          double magnitude = FindMagnitudeOf(temperature.amount, temperatures[i], temperatures[i + 1]);
          var ret = PRESSURE.OfScalar(Interpolate(magnitude, pressureValues[i + offset], pressureValues[i + 1 + offset]));
          return ret;
        }
      }
    }
      
    /// <summary>
    /// Searches the given array for the given target. If the target is not
    /// found, we will return the bit flipped index of where the target should
    /// be. For example:
    /// <code>
    /// double[] a = new double[] { 1.0, 2.0, 3.0 };
    /// double target = 1.5;
    /// int index = BinSearch(a, target, 0, target.Length - 1);
    /// // Index should be -2 as 1.5 should be inserted into index 1.
    /// </code>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="target"></param>
    /// <param name="first"></param>
    /// <param name="last">Exclusive last index</param>
    /// <returns></returns>
    private static int BinSearch(double[] a, double target, int first, int last) {
      if (first >= last) {
        return ~(last - 1);
      }

      int mid = (first + last) / 2;

      if (a[mid] == target) {
        return mid;
      } else if (target < a[mid]) {
        return BinSearch(a, target, first, mid - 1);
      } else {
        return BinSearch(a, target, mid + 1, last);
      }
    }

    /// <summary>
    /// Queries the magnitude difference between the amount and the given lower
    /// and higher boundaries.
    /// </summary>
    /// <param name="amount">lower <= amount <= higher</param>
    /// <param name="lower"></param>
    /// <param name="higher"></param>
    /// <returns></returns>
    private static double FindMagnitudeOf(double amount, double lower, double higher) {
      return (amount - lower) / (higher - lower);
    }

    /// <summary>
    /// Performs a linear interpolation between the lower and higher values.
    /// </summary>
    /// <param name="magnitude">The [0-1] magnitude of the interpolation. Values
    /// out of range will yield predictibly unexpected results.</param>
    /// <param name="lower"></param>
    /// <param name="higher"></param>
    /// <returns></returns>
    private static double Interpolate(double magnitude, double lower, double higher) {
      return lower + (higher - lower) * magnitude;
    }

    /// <summary>
    /// Enumerates the state that a fluid can be on.
    /// </summary>
    public enum EState {
      Bubble,
      Dew,
    } // End State
  }
}
