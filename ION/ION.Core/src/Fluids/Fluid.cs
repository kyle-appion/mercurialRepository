
using ION.Core.Measure;

namespace ION.Core.Fluids {
  /// <summary>
  /// The fluid class is a lookup table that is used to calculate pressures and
  /// temperatures for refrigerant fluids.
  /// </summary>
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
    /// The minimum temperature for the fluid. This is necessary for all lookups.
    /// </summary>
    public double tmin { get; private set; }
    /// <summary>
    /// The maximum temperature for the fluid. This is necessary for all lookups.
    /// </summary>
    public double tmax { get; private set; }
    /// <summary>
    /// The ARGB8888 color of the fluid.
    /// </summary>
    /// <value>The color.</value>
    public int color { get; set; }

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
    private double[] bubblePressures { get; set; }
    /// <summary>
    /// The pressures for the dew points matched to the temperature column.
    /// </summary>
    private double[] dewPressures { get; set; }

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
    public Fluid(string name, double tmin, double tmax, double step, int rows, double[] temperatures, double[] bubblePressures, double[] dewPressures) {
      this.name = name;
      this.tmin = tmin;
      this.tmax = tmax;
      this.step = step;
      this.rows = rows;
      this.temperatures = temperatures;
      this.bubblePressures = bubblePressures;
      this.dewPressures = dewPressures;
    }

    /// <summary>
    /// Queries the minumum bubble point of the fluid.
    /// </summary>
    /// <returns></returns>
    public Scalar GetMinimumBubblePressure() {
      return PRESSURE.OfScalar(bubblePressures[0]);
    }

    /// <summary>
    /// Queries the maximum bubble point of the fluid.
    /// </summary>
    /// <returns></returns>
    public Scalar GetMaximumBubblePressure() {
      return PRESSURE.OfScalar(bubblePressures[rows - 1]);
    }

    /// <summary>
    /// Queries the maximum dew point of the fluid.
    /// </summary>
    /// <returns></returns>
    public Scalar GetMinimumDewPressure() {
      return PRESSURE.OfScalar(dewPressures[0]);
    }

    /// <summary>
    /// Queries the maximum dew point of the fluid.
    /// </summary>
    /// <returns></returns>
    public Scalar GetMaximumDewPressure() {
      return PRESSURE.OfScalar(dewPressures[rows - 1]);
    }

    /// <summary>
    /// Queries the expected temperature of the fluid at the given bubble point 
    /// pressure.
    /// </summary>
    /// <param name="bubblePressure">The absolute pressure to match to an expected
    /// temperature.</param>
    /// <returns></returns>
    public Scalar GetTemperatureFromBubblePressure(Scalar bubblePressure) {
      bubblePressure = bubblePressure.ConvertTo(PRESSURE);

      Scalar pmin = GetMinimumBubblePressure(), pmax = GetMaximumBubblePressure();

      if (bubblePressure < pmin || bubblePressure > pmax) {
        return TEMPERATURE.OfScalar(double.NaN);
      } else if (bubblePressure == pmin) {
        return TEMPERATURE.OfScalar(temperatures[0]);
      } else if (bubblePressure == pmax) {
        return TEMPERATURE.OfScalar(temperatures[rows - 1]); 
      }

      int i = BinSearch(bubblePressures, bubblePressure.amount, 0, rows);

      if (i >= 0) {
        return TEMPERATURE.OfScalar(temperatures[i]);
      } else {
        i = ~i;
        double magnitude = FindMagnitudeOf(bubblePressure.amount, bubblePressures[i], bubblePressures[i + 1]);
        return TEMPERATURE.OfScalar(Interpolate(magnitude, temperatures[i], temperatures[i + 1]));
      }
    }

    /// <summary>
    /// Queries the expected bubble pressure of the fluid at the given temperature.
    /// </summary>
    /// <param name="temperature"></param>
    /// <returns></returns>
    public Scalar GetBubblePressureFromTemperature(Scalar temperature) {
      temperature = temperature.ConvertTo(TEMPERATURE);

      if (temperature < tmin || temperature > tmax) {
        return PRESSURE.OfScalar(double.NaN);
      } else if (temperature.amount == tmin) {
        return PRESSURE.OfScalar(bubblePressures[0]);
      } else if (temperature.amount == tmax) {
        return PRESSURE.OfScalar(bubblePressures[rows - 1]);
      }

      int i = BinSearch(temperatures, temperature.amount, 0, rows);

      if (i >= 0) {
        return PRESSURE.OfScalar(bubblePressures[i]);
      } else {
        i = ~i;
        double magnitude = FindMagnitudeOf(temperature.amount, temperatures[i], temperatures[i + 1]);
        return PRESSURE.OfScalar(Interpolate(magnitude, bubblePressures[i], bubblePressures[i + 1]));
      }
    }

    /// <summary>
    /// Queries the expected temperature of the fluid at the given dew point 
    /// pressure.
    /// </summary>
    /// <param name="dewPressure">The absolute pressure to match to an expected
    /// temperature.</param>
    /// <returns></returns>
    public Scalar GetTemperatureFromDewPressure(Scalar dewPressure) {
      dewPressure = dewPressure.ConvertTo(PRESSURE);

      Scalar pmin = GetMinimumDewPressure(), pmax = GetMaximumDewPressure();

      if (dewPressure < pmin || dewPressure > pmax) {
        return TEMPERATURE.OfScalar(double.NaN);
      } else if (dewPressure == pmin) {
        return TEMPERATURE.OfScalar(temperatures[0]);
      } else if (dewPressure == pmax) {
        return TEMPERATURE.OfScalar(temperatures[rows - 1]);
      }

      int i = BinSearch(dewPressures, dewPressure.amount, 0, rows);

      if (i >= 0) {
        return TEMPERATURE.OfScalar(temperatures[i]);
      } else {
        i = ~i;
        double magnitude = FindMagnitudeOf(dewPressure.amount, dewPressures[i], dewPressures[i + 1]);
        var ret = TEMPERATURE.OfScalar(Interpolate(magnitude, temperatures[i], temperatures[i + 1]));
        return ret;
      }
    }

    /// <summary>
    /// Queries the expected dew pressure of the fluid at the given temperature.
    /// </summary>
    /// <param name="temperature"></param>
    /// <returns></returns>
    public Scalar GetDewPressureFromTemperature(Scalar temperature) {
      temperature = temperature.ConvertTo(TEMPERATURE);

      if (temperature < tmin || temperature > tmax) {
        return PRESSURE.OfScalar(double.NaN);
      } else if (temperature.amount == tmin) {
        return PRESSURE.OfScalar(dewPressures[0]);
      } else if (temperature.amount == tmax) {
        return PRESSURE.OfScalar(dewPressures[rows - 1]);
      }

      int i = BinSearch(temperatures, temperature.amount, 0, rows);

      if (i >= 0) {
        return PRESSURE.OfScalar(dewPressures[i]);
      } else {
        i = ~i;
        double magnitude = FindMagnitudeOf(temperature.amount, temperatures[i], temperatures[i + 1]);
        return PRESSURE.OfScalar(Interpolate(magnitude, dewPressures[i], dewPressures[i + 1]));
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
    /// <param name="last"></param>
    /// <returns></returns>
    private static int BinSearch(double[] a, double target, int first, int last) {
      if (first >= last) {
        return ~last;
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
      return 1 - ((higher / amount) / (higher / lower));
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
    public enum State {
      Bubble,
      Dew,
    } // End State
  }
}
