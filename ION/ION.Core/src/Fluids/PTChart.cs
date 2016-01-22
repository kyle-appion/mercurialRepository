namespace ION.Core.Fluids {

  using System;

  using ION.Core.App;
  using ION.Core.Fluids;
  using ION.Core.Math;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  public class PTChart {

    /// <summary>
    /// Creates a new PTChart.
    /// </summary>
    /// <param name="ion">Ion.</param>
    /// <param name="state">State.</param>
    /// <param name="fluid">Fluid.</param>
    public static PTChart New(IION ion, Fluid.EState state) {
      return New(ion, state, ion.fluidManager.lastUsedFluid);
    }

    /// <summary>
    /// Creates a new PTChart.
    /// </summary>
    /// <param name="ion">Ion.</param>
    /// <param name="state">State.</param>
    /// <param name="fluid">Fluid.</param>
    public static PTChart New(IION ion, Fluid.EState state, Fluid fluid) {
      var elevation = Units.Length.METER.OfScalar(0);

      if (ion.locationManager.allowLocationTracking) {
        elevation = ion.locationManager.lastKnownLocation.altitude;
      }

      return new PTChart(ion, state, fluid);//, elevation);
    }

    /// <summary>
    /// The state that the fluid is in.
    /// </summary>
    /// <value>The state.</value>
    public Fluid.EState state { get; set; }
    /// <summary>
    /// The fluid that the ptchart is using for calculations.
    /// </summary>
    /// <value>The fluid.</value>
    public Fluid fluid { get; private set; }
    /// <summary>
    /// The elevation above sea level. This will affect end calculations by a
    /// small, but significant amount.
    /// </summary>
    /// <value>The elevation.</value>
    public Scalar elevation {
      get {
        return ion.locationManager.lastKnownLocation.altitude;
      }
    }

    private IION ion { get; set; }
/*
      set {
        if (value == null) {
          value = Units.Length.METER.OfScalar(0);
        }
        __elevation = value;
      }
    } Scalar __elevation;
*/


    private PTChart(IION ion, Fluid.EState state, Fluid fluid) {//, Scalar elevation) {
      if (fluid == null) {
        throw new Exception("Cannot create a PTChart with a null fluid");
      }
      this.ion = ion;
      this.state = state;
      this.fluid = fluid;
//      this.elevation = elevation;
    }

    /// <summary>
    /// Queries the temperature for the given sensor.
    /// </summary>
    /// <returns>The temperature.</returns>
    /// <param name="temperatureSensor">Temperature sensor.</param>
    public Scalar GetTemperature(Sensor pressureSensor) {
      return GetTemperature(pressureSensor.measurement, pressureSensor.isRelative || Units.Pressure.PSIA.Equals(pressureSensor.unit));
    }

    /// <summary>
    /// Queries the temperature of the fluid at the given state provided a pressure.
    /// </summary>
    /// <remarks>
    /// IsRelative is used to indicate that the pressure measurement is a relative
    /// measurement. If you don't know whether or not the measurement is relative, then
    /// it most likely is.
    /// </remarks>
    /// <returns>The temperature.</returns>
    /// <param name="state">State.</param>
    /// <param name="pressure">Pressure.</param>
    /// <param name="isRelative">Is relative.</param>
    public Scalar GetTemperature(Scalar pressure, bool isRelative = true) {
      if (isRelative) {
        pressure = Physics.ConvertRelativePressureToAbsolute(pressure, elevation);
      }

      return fluid.GetTemperatureFromPressure(state, pressure);
    }

    /// <summary>
    /// Queries the pressure of the fluid at the given temperature
    /// </summary>
    /// <returns>The pressure.</returns>
    /// <param name="pressureSensor">Pressure sensor.</param>
    public Scalar GetPressure(Sensor temperatureSensor) {
      return GetPressure(temperatureSensor.measurement, temperatureSensor.isRelative);
    }

    /// <summary>
    /// Queries the pressure of the fluid at the given temperature.
    /// </summary>
    /// <remarks>
    /// IsRelative is used to indicate whether or not the returned pressure should
    /// be relative.
    /// </remarks>
    /// <returns>The pressure.</returns>
    /// <param name="state">State.</param>
    /// <param name="temperature">Temperature.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Scalar GetPressure(Scalar temperature, bool isRelative = true) {
      Scalar ret = fluid.GetPressureFromTemperature(state, temperature);

      if (isRelative) {
        ret = Physics.ConvertAbsolutePressureToRelative(ret, elevation);
      }

      return ret;
    }

    /// <summary>
    /// Calculates the systems superheat or subcool based on the given inputs
    /// and the state of the pt chart.
    /// </summary>
    /// <returns>The system temperature delta.</returns>
    /// <param name="pressure">Pressure.</param>
    /// <param name="temperature">Temperature.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Scalar CalculateSystemTemperatureDelta(Scalar pressure, Scalar temperature, bool isRelative = true) {
      if (fluid.mixture) {
        switch (state) {
          case Fluid.EState.Bubble: // Subcool
            return CalculateSubcool(pressure, temperature, isRelative);
          case Fluid.EState.Dew: // Superheat
            return CalculateSuperheat(pressure, temperature, isRelative);
          default:
            throw new Exception("Unknown ptchart state: " + state);
        }
      } else {
        return GetTemperature(pressure).ConvertTo(temperature.unit) - temperature;
      }
    }

    /// <summary>
    /// Calculates the superheat of the fluid.
    /// </summary>
    /// <remarks>
    /// Superheat is the difference between the measured temperature and
    /// the dew point of a fluid at a given temperature.
    /// </remarks>
    /// <returns>The superheat.</returns>
    /// <param name="pressure">Pressure.</param>
    /// <param name="temperature">Temperature.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Scalar CalculateSuperheat(Scalar pressure, Scalar temperature, bool isRelative = true) {
      if (isRelative) {
        pressure = Physics.ConvertRelativePressureToAbsolute(pressure, elevation);
      }

      Scalar saturatedTemperature = GetTemperature(pressure).ConvertTo(temperature.unit);
      return saturatedTemperature - temperature;
    }

    /// <summary>
    /// Calculates the subcool of the fluid.
    /// </summary>
    /// <remarks>
    /// Subcool is the difference between the measured temperature and
    /// the bubble point of a fluid at a given temperature.
    /// </remarks>
    /// <returns>The subcool.</returns>
    /// <param name="pressure">Pressure.</param>
    /// <param name="temperature">Temperature.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Scalar CalculateSubcool(Scalar pressure, Scalar temperature, bool isRelative = true) {
      if (isRelative) {
        pressure = Physics.ConvertRelativePressureToAbsolute(pressure, elevation);
      }

      Scalar saturatedTemperature = GetTemperature(pressure).ConvertTo(temperature.unit);
      return temperature - saturatedTemperature;
    }

    /// <summary>
    /// Queries whether or not the given pressure is within the ptchart's fluid bounds.
    /// </summary>
    /// <returns><c>true</c> if this instance is pressure within bounds the specified pressure; otherwise, <c>false</c>.</returns>
    /// <param name="pressure">Pressure.</param>
    public bool IsPressureWithinBounds(Scalar pressure) {
      return !IsPressureAboveBounds(pressure) && !IsPressureBelowBounds(pressure);
    }

    /// <summary>
    /// Queries whether or not the given temperature is within the ptchart's fluid bounds.
    /// </summary>
    /// <returns><c>true</c> if this instance is temperature within bounds the specified temperature; otherwise, <c>false</c>.</returns>
    /// <param name="temperature">Temperature.</param>
    public bool IsTemperatureWithinBounds(Scalar temperature) {
      return !IsTemperatureAboveBounds(temperature) && !IsTemperatureBelowBounds(temperature);
    }

    /// <summary>
    /// Queries whether or not the given pressure measurement is above the bounds
    /// of the ptchart's fluid bounds.
    /// </summary>
    public bool IsPressureAboveBounds(Scalar pressure) {
      return pressure > fluid.GetMaximumPressure(state);
    }

    /// <summary>
    /// Queries whether or not the given pressure measurement is below the bounds
    /// of the ptchart's fluid bounds.
    /// </summary>
    public bool IsPressureBelowBounds(Scalar pressure) {
      return pressure < fluid.GetMaximumPressure(state);
    }

    /// <summary>
    /// Queries whether or not the given temperature measurement is above the bounds
    /// of the ptchart's fluid bounds.
    /// </summary>
    public bool IsTemperatureAboveBounds(Scalar temperature) {
      return temperature > fluid.GetMaximumTemperature();
    }

    /// <summary>
    /// Queries whether or not the given temperature measurement is below the bounds
    /// of the ptchart's fluid bounds.
    /// </summary>
    public bool IsTemperatureBelowBounds(Scalar temperature) {
      return temperature < fluid.GetMinimumTemperature();
    }
  }
}

