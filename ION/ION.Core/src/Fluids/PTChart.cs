namespace ION.Core.Fluids {

  using System;

  using Appion.Commons.Measure;

  using ION.Core.App;
  using ION.Core.Math;
  using ION.Core.Sensors;


	/// <summary>
	/// A simple delegate that is used to retrieve the desired elevation for the pt chart.
	/// </summary>
	public delegate Scalar ElevationProvider();


  public class PTChart {

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

    /// <summary>
    /// The fluid that the pt chart is wrapping.
    /// </summary>
    /// <value>The fluid.</value>
    public Fluid fluid { get; private set; }
    /// <summary>
    /// The fluid state for the pt chart.
    /// </summary>
    /// <value>The state.</value>
    public Fluid.EState state { get; private set; }
    /// <summary>
    /// The callback that will return elevations for calculating absolute pressures.
    /// </summary>
    public ElevationProvider elevationProvider { get; private set; }

    /// <summary>
    /// Creates a new PT chart.
    /// </summary>
    /// <param name="fluid">The fluid to have the pt chart manage.</param>
    /// <param name="state">The initial state of the fluid.</param>
    /// <param name="elevationProvider">The delegate that will provide current elevations for pt chart calculations.</param>
    public PTChart(Fluid fluid, Fluid.EState state, ElevationProvider elevationProvider = null) {
      this.fluid = fluid;
      this.state = state;

      if (elevationProvider == null) {
        elevationProvider = () => {
          var ion = AppState.context;
          return ion.locationManager.lastKnownLocation.altitude;
        };
      }

			this.elevationProvider = elevationProvider;
		}

    /// <summary>
    /// Queries the absolute pressure for the fluid at the given temperature.
    /// </summary>
    /// <returns>The absolute pressure.</returns>
    /// <param name="temperature">Temperature.</param>
    public Scalar GetAbsolutePressure(Scalar temperature) {
      return fluid.GetPressureFromTemperature(state, temperature);
    }

		/// <summary>
		/// Queries the relative [gauge] pressure for the fluid at the given temperature. 
		/// </summary>
		/// <returns>The relative pressure.</returns>
		/// <param name="temperature">Temperature.</param>
		public Scalar GetRelativePressure(Scalar temperature) {
      var absPressure = fluid.GetPressureFromTemperature(state, temperature);
      var relPressure = Physics.ConvertAbsolutePressureToRelative(absPressure, elevationProvider());
      return relPressure;
    }

    /// <summary>
    /// Queries the saturated temperature for the given pressure using isRelative to properly adjust the pressure as necessary.
    /// </summary>
    /// <returns>The temperature.</returns>
    /// <param name="pressure">Pressure.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Scalar GetTemperature(Scalar pressure, bool isRelative) {
      if (isRelative) {
        return GetTemperatureFromRelativePressure(pressure);
      } else {
        return GetTemperatureFromAbsolutePressure(pressure);
      }
    }

		/// <summary>
		/// Queries the saturated temperature of a fluid at the given absolute pressure.
		/// </summary>
		/// <returns>The temperature from absolute pressure.</returns>
		/// <param name="absolutePressure">Pressure.</param>
    public Scalar GetTemperatureFromAbsolutePressure(Scalar absolutePressure) {
      return fluid.GetTemperatureFromAbsolutePressure(state, absolutePressure);
    }

		/// <summary>
		/// Queries the saturated temperature of a fluid at the given relative pressure.
		/// </summary>
		/// <returns>The temperature from relative pressure.</returns>
		/// <param name="relativePressure">Pressure.</param>
    public Scalar GetTemperatureFromRelativePressure(Scalar relativePressure) {
      var absPressure = Physics.ConvertRelativePressureToAbsolute(relativePressure, elevationProvider());
      return GetTemperatureFromAbsolutePressure(absPressure);
    }

    /// <summary>
    /// Calculates the effective superheat or subcool of the fluid given the two gauge measurements.
    /// </summary>
    /// <returns>The temperature delta absolute.</returns>
    /// <param name="absolutePressure">Absolute pressure.</param>
    /// <param name="measuredTemperature">Measured temperature.</param>
    public ScalarSpan CalculateTemperatureDeltaAbsolute(Scalar absolutePressure, Scalar measuredTemperature) {
      if (fluid.mixture) {
        switch (state) {
					case Fluid.EState.Dew:
            return CalculateSuperheatAbsolute(absolutePressure, measuredTemperature);
					case Fluid.EState.Bubble:
  					return CalculateSubcoolAbsolute(absolutePressure, measuredTemperature);
          default:
            throw new Exception("Cannot calculate temperature delta with unknown fluid state: " + state);
				}        
      } else {
        var satTemp = GetTemperature(absolutePressure, false).ConvertTo(measuredTemperature.unit);
        return measuredTemperature - satTemp;
      }
    }

    /// <summary>
    /// Calculates the superheat for the fluid.
    /// </summary>
    /// <returns>The superheat.</returns>
    /// <param name="absolutePressure">Absolute pressure.</param>
    /// <param name="measuredTemperature">Measured temperature.</param>
    public ScalarSpan CalculateSuperheatAbsolute(Scalar absolutePressure, Scalar measuredTemperature) {
      var satTemp = GetTemperature(absolutePressure, false).ConvertTo(measuredTemperature.unit);
      return measuredTemperature - satTemp;
    }

		/// <summary>
		/// Calculates the subcool for the fluid.
		/// </summary>
		/// <returns>The subcool.</returns>
		/// <param name="absolutePressure">Absolute pressure.</param>
		/// <param name="measuredTemperature">Measured temperature.</param>
		public ScalarSpan CalculateSubcoolAbsolute(Scalar absolutePressure, Scalar measuredTemperature) {
			var satTemp = GetTemperature(absolutePressure, false).ConvertTo(measuredTemperature.unit);
			return satTemp - measuredTemperature;
		}

    /// <summary>
    /// Calculates the effective superheat or subcool of the fluid given the two gauge measurements.
    /// </summary>
    /// <returns>The temperature delta relative.</returns>
    /// <param name="relativePressure">Relative pressure.</param>
    /// <param name="measuredTemperature">Measured temperature.</param>
    public ScalarSpan CalculateTemperatureDeltaRelative(Scalar relativePressure, Scalar measuredTemperature) {
      var absPressure = Physics.ConvertRelativePressureToAbsolute(relativePressure, elevationProvider());
      return CalculateTemperatureDeltaAbsolute(absPressure, measuredTemperature);
    }
  }
}
