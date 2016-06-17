using System;

namespace Measurement {
  /// <summary>
  /// A unit converter that will apply a linear transformation to a unit conversion. The resulting converter is as
  /// follows: f(x) = x * factor.
  /// </summary>
  public class MultiplicationUnitConverter : UnitConverter {
    /// <summary>
    /// The offset that is added to the unit converter.
    /// </summary>
    /// <value>The factor.</value>
    public double factor { get; private set; }

    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return true;
      }
    }

    public MultiplicationUnitConverter(double factor) {
      this.factor = factor;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      if (converter is MultiplicationUnitConverter) {
        var mc = converter as MultiplicationUnitConverter;
        return FromValue(factor * mc.factor);
      } else if (converter is RationalUnitConverter) {
        var rc = converter as RationalUnitConverter;
        return FromValue(factor * rc.dividend / rc.divisor);
      } else {
        return base.Concatenate(converter);
      }
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return FromValue(1 / factor);
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return new AdditionUnitConverter(factor);
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return x * factor;
    }

    private static UnitConverter FromValue(double x) {
      return x == 1.0 ? IDENTITY : new MultiplicationUnitConverter(x);
    }
  }
}

