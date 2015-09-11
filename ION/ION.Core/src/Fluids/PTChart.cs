using System;

using ION.Core.Fluids;
using ION.Core.Math;
using ION.Core.Measure;
using ION.Core.Util;

namespace ION.Core.Fluids {
  public class PTChart {

    /// <summary>
    /// The state that the fluid is in.
    /// </summary>
    /// <value>The state.</value>
    public Fluid.EState state { get; private set; }
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
        return __elevation;
      }
      set {
        if (value == null) {
          value = Units.Length.METER.OfScalar(0);
        }
        __elevation = value;
      }
    } Scalar __elevation;


    public PTChart(Fluid.EState state, Fluid fluid) : this(state, fluid, Units.Length.METER.OfScalar(0)) {
      // Nope
    }

    public PTChart(Fluid.EState state, Fluid fluid, Scalar elevation) {
      if (fluid == null) {
        throw new Exception("Cannot create a PTChart with a null fluid");
      }
      this.state = state;
      this.fluid = fluid;
      this.elevation = elevation;
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

      Log.D(this, "PT Pressure is: " + pressure);

      switch (state) {
        case Fluid.EState.Bubble:
          return fluid.GetTemperatureFromBubblePressure(pressure);
        case Fluid.EState.Dew:
          return fluid.GetTemperatureFromDewPressure(pressure);
        default:
          throw new ArgumentException("Cannot get temperature: invalid fluid state " + state);
      }
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
      Scalar ret = null;

      switch (state) {
        case Fluid.EState.Bubble:
          ret = fluid.GetBubblePressureFromTemperature(temperature);
          break;
        case Fluid.EState.Dew:
          ret = fluid.GetDewPressureFromTemperature(temperature);
          break;
        default:
          throw new ArgumentException("Cannot get pressure: invalid fluid state " + state);
      }

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
      switch (state) {
        case Fluid.EState.Bubble: // Subcool
          return CalculateSubcool(pressure, temperature, isRelative);
        case Fluid.EState.Dew: // Superheat
          return CalculateSuperheat(pressure, temperature, isRelative);
        default:
          throw new Exception("Unknown ptchart state: " + state);
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

      Scalar superheat = fluid.GetTemperatureFromDewPressure(pressure).ConvertTo(temperature.unit);
      return temperature - superheat;
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

      Scalar subcool = fluid.GetTemperatureFromBubblePressure(pressure).ConvertTo(temperature.unit);
      return subcool - temperature;
    }
  }
}

