namespace ION.Core.Fluids
{

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Math;

	/// <summary>
	/// The fluid class is a lookup table that is used to calculate pressures and
	/// temperatures for refrigerant fluids.
	/// </summary>
	public class Fluid
	{
		/*
			public Scalar minAbsolutePressure { get { return fluid.GetMinimumPressure(state); } }
			public Scalar minRelativePressure {
			  get {
				var relPressure = Physics.ConvertAbsolutePressureToRelative(minAbsolutePressure, elevationProvider());
				return relPressure;
			  }
			}

			public Scalar maxAbsolutePressure { get { return fluid.GetMaximumPressure(state); } }
			public Scalar maxRelativePressure {
			  get {
				var relPressure = Physics.ConvertAbsolutePressureToRelative(maxAbsolutePressure, elevationProvider());
				return relPressure;
			  }
			}


			public Scalar minTemperature { get { return fluid.GetMinimumTemperature(); } }
			public Scalar maxTemperature { get { return fluid.GetMaximumTemperature(); } }
		*/

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
		/// The safety type for this fluid.
		/// </summary>
		/// <value>The safety.</value>
		public ESafety safety { get; internal set; }
		/// <summary>
		/// The ARGB8888 color of the fluid.
		/// </summary>
		/// <value>The color.</value>
		public int color { get; internal set; }
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
		public Fluid(string name, bool mixture, double tmin, double tmax, double step, int rows, double[] temperatures, double[] pressureValues)
		{
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
		/// Converts the given pressure into an absolute pressure.
		/// </summary>
		/// <returns>The relative pressure to absolute.</returns>
		/// <param name="relativePressure">Relative pressure.</param>
		/// <param name="elevation">Elevation.</param>
		public Scalar ConvertRelativePressureToAbsolute(Scalar relativePressure, Scalar elevation)
		{
			return Physics.ConvertRelativePressureToAbsolute(relativePressure, elevation);
		}

		/// <summary>
		/// Converts the absolute pressure to relative pressure.
		/// </summary>
		/// <returns>The absolute pressure to relative.</returns>
		/// <param name="absolutePressure">Absolute pressure.</param>
		/// <param name="elevation">Elevation.</param>
		public Scalar ConvertAbsolutePressureToRelative(Scalar absolutePressure, Scalar elevation)
		{
			return Physics.ConvertAbsolutePressureToRelative(absolutePressure, elevation);
		}

		/// <summary>
		/// Queries the minimum temperature of the fluid.
		/// </summary>
		/// <returns>The minimum temperature.</returns>
		public Scalar GetMinimumTemperature()
		{
			return TEMPERATURE.OfScalar(tmin);
		}

		/// <summary>
		/// Queries the maximum temperature of the fluid.
		/// </summary>
		/// <returns>The maximum temperature.</returns>
		public Scalar GetMaximumTemperature()
		{
			return TEMPERATURE.OfScalar(tmax);
		}

		/// <summary>
		/// Queries the minimum pressure of the fluid regardless of state.
		/// </summary>
		/// <returns>The minimum pressure.</returns>
		public Scalar GetMinimumPressure()
		{
			var dewLow = GetMinimumPressure(EState.Dew);
			var bubLow = GetMinimumPressure(EState.Bubble);

			return Scalar.Min(dewLow, bubLow);
		}

		/// <summary>
		/// Queries the maximum pressure of the fluid regardless of state.
		/// </summary>
		/// <returns>The maximum pressure.</returns>
		public Scalar GetMaximumPressure()
		{
			var dewLow = GetMaximumPressure(EState.Dew);
			var bubLow = GetMaximumPressure(EState.Bubble);

			return Scalar.Max(dewLow, bubLow);
		}

		/// <summary>
		/// Queries the minimum pressure of the fluid in the given state.
		/// </summary>
		/// <returns>The pressure.</returns>
		/// <param name="state">State.</param>
		public Scalar GetMinimumPressure(EState state)
		{
			if (!mixture)
			{
				return PRESSURE.OfScalar(pressureValues[0]);
			}

			switch (state)
			{
				case EState.Bubble:
					return PRESSURE.OfScalar(pressureValues[0]);
				case EState.Dew:
					return PRESSURE.OfScalar(pressureValues[rows]);
				default:
#if DEBUG
					// todo ahodder@appioninc.com: it would be nice to know what causes these.
					throw new ArgumentException("Cannot get minimum pressure for state: " + state);
#else
          return PRESSURE.OfScalar(0);
#endif
			}
		}

		/// <summary>
		/// Queries the maximim pressure of the fluid in the given state.
		/// </summary>
		/// <returns>The maximum pressure.</returns>
		/// <param name="state">State.</param>
		public Scalar GetMaximumPressure(EState state)
		{
			if (!mixture)
			{
				return PRESSURE.OfScalar(pressureValues[rows - 1]);
			}

			switch (state)
			{
				case EState.Bubble:
					return PRESSURE.OfScalar(pressureValues[rows - 1]);
				case EState.Dew:
					return PRESSURE.OfScalar(pressureValues[2 * rows - 1]);
				default:
#if DEBUG
					// todo ahodder@appioninc.com: it would be nice to know what causes these.
					throw new ArgumentException("Cannot get minimum pressure for state: " + state);
#else
          return PRESSURE.OfScalar(0);
#endif
			}
		}

		/// <summary>
		/// Queries the median pressure for the state.
		/// </summary>
		/// <returns>The median pressure.</returns>
		/// <param name="state">State.</param>
		public Scalar GetMedianAbsolutePressure(EState state)
		{
			var index = (mixture && state == EState.Dew) ? rows : 0;
			return Units.Pressure.KILOPASCAL.OfScalar(index + rows / 2);
		}

		/// <summary>
		/// Queries the expected saturated temperature of the fluid at the given state and absolute pressure.
		/// </summary>
		/// <param name="absolutePressure">The absolute pressure to match to an expected temperature.</param>
		/// <returns></returns>
		public Scalar GetSaturatedTemperature(EState state, Scalar absolutePressure)
		{
			absolutePressure = absolutePressure.ConvertTo(PRESSURE);
			Scalar pmin = GetMinimumPressure(state), pmax = GetMaximumPressure(state);

			if (absolutePressure < pmin || absolutePressure > pmax)
			{
				return TEMPERATURE.OfScalar(double.NaN);
			}
			else if (absolutePressure == pmin)
			{
				return TEMPERATURE.OfScalar(temperatures[0]);
			}
			else if (absolutePressure == pmax)
			{
				return TEMPERATURE.OfScalar(temperatures[rows - 1]);
			}

			var offset = (mixture && EState.Dew == state) ? rows : 0;
			int i = BinSearch(pressureValues, absolutePressure.amount, 0 + offset, rows + offset);

			if (i >= 0)
			{
				return TEMPERATURE.OfScalar(temperatures[i]);
			}
			else
			{
				i = ~i;
				if (i >= pressureValues.Length)
				{
					return TEMPERATURE.OfScalar(temperatures[i - offset]);
				}
				else
				{
					double magnitude = FindMagnitudeOf(absolutePressure.amount, pressureValues[i], pressureValues[i + 1]);
					return TEMPERATURE.OfScalar(Interpolate(magnitude, temperatures[i % rows], temperatures[(i + 1) % rows]));
				}
			}
		}

		/// <summary>
		/// Queries pressure of the fluid in the given state and saturated temperature.
		/// </summary>
		/// <param name="saturatedTemperature"></param>
		/// <returns></returns>
		public Scalar GetPressureFromSaturatedTemperature(EState state, Scalar saturatedTemperature)
		{
			saturatedTemperature = saturatedTemperature.ConvertTo(TEMPERATURE);
			// The offset to the start of the proper pressure slice
			var offset = (mixture && EState.Dew == state) ? rows : 0;

			if (saturatedTemperature < tmin || saturatedTemperature > tmax)
			{
				return PRESSURE.OfScalar(double.NaN);
			}
			else if (saturatedTemperature.amount == tmin)
			{
				return PRESSURE.OfScalar(pressureValues[0 + offset]);
			}
			else if (saturatedTemperature.amount == tmax)
			{
				return PRESSURE.OfScalar(pressureValues[rows - 1 + offset]);
			}

			int i = BinSearch(temperatures, saturatedTemperature.amount, 0, rows);

			if (i >= 0)
			{
				return PRESSURE.OfScalar(pressureValues[i + offset]);
			}
			else
			{
				i = ~i;
				if (i >= temperatures.Length - 1)
				{
					return PRESSURE.OfScalar(pressureValues[offset + i]);
				}
				else
				{
					double magnitude = FindMagnitudeOf(saturatedTemperature.amount, temperatures[i], temperatures[i + 1]);
					var ret = PRESSURE.OfScalar(Interpolate(magnitude, pressureValues[i + offset], pressureValues[i + 1 + offset]));
					return ret;
				}
			}
		}

		/// <summary>
		/// Calculates the effective superheat or subcool of the fluid given absolute pressure and measured temperature.
		/// </summary>
		/// <returns>The temperature delta absolute.</returns>
		/// <param name="absolutePressure">Absolute pressure.</param>
		/// <param name="measuredTemperature">Measured temperature.</param>
		public ScalarSpan CalculateTemperatureDelta(EState fluidState, Scalar absolutePressure, Scalar measuredTemperature)
		{
			if (mixture)
			{
				switch (fluidState)
				{
					case Fluid.EState.Dew:
						return CalculateSuperheatAbsolute(fluidState, absolutePressure, measuredTemperature);
					case Fluid.EState.Bubble:
						return CalculateSubcoolAbsolute(fluidState, absolutePressure, measuredTemperature);
					default:
						throw new Exception("Cannot calculate temperature delta with unknown fluid state: " + fluidState);
				}
			}
			else
			{
				var satTemp = GetSaturatedTemperature(fluidState, absolutePressure).ConvertTo(measuredTemperature.unit);
				return measuredTemperature - satTemp;
			}
		}

		/// <summary>
		/// Calculates the superheat for the fluid.
		/// </summary>
		/// <returns>The superheat.</returns>
		/// <param name="absolutePressure">Absolute pressure.</param>
		/// <param name="measuredTemperature">Measured temperature.</param>
		public ScalarSpan CalculateSuperheatAbsolute(EState fluidState, Scalar absolutePressure, Scalar measuredTemperature)
		{
			var satTemp = GetSaturatedTemperature(fluidState, absolutePressure).ConvertTo(measuredTemperature.unit);
			return measuredTemperature - satTemp;
		}

		/// <summary>
		/// Calculates the subcool for the fluid.
		/// </summary>
		/// <returns>The subcool.</returns>
		/// <param name="absolutePressure">Absolute pressure.</param>
		/// <param name="measuredTemperature">Measured temperature.</param>
		public ScalarSpan CalculateSubcoolAbsolute(EState fluidState, Scalar absolutePressure, Scalar measuredTemperature)
		{
			var satTemp = GetSaturatedTemperature(fluidState, absolutePressure).ConvertTo(measuredTemperature.unit);
			return satTemp - measuredTemperature;
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
		private static int BinSearch(double[] a, double target, int first, int last)
		{
			if (first >= last)
			{
				return ~(last - 1);
			}

			int mid = (first + last) / 2;

			if (a[mid] == target)
			{
				return mid;
			}
			else if (target < a[mid])
			{
				return BinSearch(a, target, first, mid - 1);
			}
			else
			{
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
		private static double FindMagnitudeOf(double amount, double lower, double higher)
		{
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
		private static double Interpolate(double magnitude, double lower, double higher)
		{
			return lower + (higher - lower) * magnitude;
		}

		/// <summary>
		/// Enumerates the state that a fluid can be on.
		/// </summary>
		public enum EState
		{
			Bubble,
			Dew,
		} // End State

		public enum ESafety
		{
			A1 = 1,
			A2 = 2,
			A2L = 3,
			A3 = 4,
			B1 = 5,
			B2 = 6,

			Unknown = 0,
		}
	}
}
