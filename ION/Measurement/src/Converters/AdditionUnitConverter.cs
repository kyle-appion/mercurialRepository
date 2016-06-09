namespace Measurement {
  
  using System;

  /// <summary>
  /// A unit converter that will apply a non-linear transformation to a unit conversion. The resulting converter is as
  /// follows: f(x) = x + offset.
  /// </summary>
  public class AdditionUnitConverter : UnitConverter {

    /// <summary>
    /// The offset that is added to the unit converter.
    /// </summary>
    public double offset { get; private set; }

    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return false;
      }
    }

    public AdditionUnitConverter(double offset) {
      this.offset = offset;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. This allows for compound translations to a unit.
    /// </summary>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      var ac = converter as AdditionUnitConverter;

      if (ac != null) {
        return FromValue(offset + ac.offset);
      } else {
        return base.Concatenate(converter);
      }
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return FromValue(-offset);
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return IDENTITY;
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return x + offset;
    }

    private static UnitConverter FromValue(double value) {
      return value == 0.0 ? IDENTITY : new AdditionUnitConverter(value);
    }
  }
}

