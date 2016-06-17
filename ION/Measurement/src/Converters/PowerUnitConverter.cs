namespace Measurement {

  using System;

  /// <summary>
  /// A unit converter that will apply a non-linear transformation to a unit converter. The resulting converter is as
  /// follows: f(x) = x^(exponent).
  /// </summary>
  public class PowerUnitConverter : UnitConverter {
    /// <summary>
    /// The dividend of the exponent.
    /// </summary>
    /// <value>The dividend.</value>
    public long dividend { get; private set; }
    /// <summary>
    /// The divisor of the exponent.
    /// </summary>
    /// <value>The divisor.</value>
    public long divisor { get; private set; }

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

    /// <summary>
    /// Creates a new PowerUnitConverter whose exponent is a rational approximation to the given exponent.
    /// </summary>
    /// <param name="exponent">Exponent.</param>
    public PowerUnitConverter(double exponent) : this((long)(exponent * (exponent - (long)exponent) * 1E12), (long)1E12) {
    }

    public PowerUnitConverter(long dividend, long divisor) {
      if (divisor == 0) {
        throw new ArithmeticException("Divisor cannot equal 0");
      }

      long gcd = Gcd(dividend, divisor);
      this.dividend = dividend / gcd;
      this.divisor = divisor / gcd;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      var pc = converter as PowerUnitConverter;
      if (pc != null) {
        return FromValue(dividend * pc.dividend, divisor * pc.divisor);
      } else {
        return base.Concatenate(converter);
      }
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      // The derivative of a x^y is equal to y(x^y-1)
      return new RationalUnitConverter(dividend, divisor).Concatenate(FromValue(dividend - divisor, divisor));
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return FromValue(divisor, dividend);
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return Math.Pow(x, dividend / (double)divisor);
    }

    /// <summary>
    /// Finds the greatest common divisor of m and n.
    /// <para>
    /// Example Usage:
    /// <code>
    /// long m = 12;
    /// long n = 3;
    /// System.out.println("gcd(" + m + ", " + n + ") => " + MathUtils.gcd(m, n));
    /// >>> OUT
    /// gcd(12, 3) => 3
    /// </code>
    /// </para> </summary>
    /// <param name="m"> </param>
    /// <param name="n"> </param>
    /// <returns> The greatest common denominator or m if it is. </returns>
    private static long Gcd(long m, long n) {
      if (n == 0L) {
        return m;
      } else {
        return Gcd(n, m % n);
      }
    }

    private static UnitConverter FromValue(long dividend, long divisor) {
      if (divisor == 0) {
        throw new ArithmeticException("Divisor cannot equal 0");
      }

      var gcd = Gcd(dividend, divisor);
      dividend /= gcd;
      divisor /= gcd;

      if (dividend == divisor) {
        return IDENTITY;
      } else if (dividend == 0) {
        return new OneUnitConverter();
      } else {
        return new PowerUnitConverter(dividend, divisor);
      }
    }
  }
}

