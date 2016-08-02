using System;

namespace Measurement {
  /// <summary>
  /// A unit converter that will apply a linear transformation to a unit conversion. The resulting convert is as follows:
  /// f(x) = x * dividend / divisor.
  /// </summary>
  public class RationalUnitConverter : UnitConverter{
    /// <summary>
    /// The dividend of the multiplcand.
    /// </summary>
    /// <value>The dividend.</value>
    public long dividend { get; private set; }
    /// <summary>
    /// The divisor of the multiplicand.
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
        return true;
      }
    }

    public RationalUnitConverter(long dividend, long divisor) {
      if (divisor == 0) {
        throw new ArithmeticException("Divisor cannot equal 0");
      }

      this.dividend = dividend;
      this.divisor = divisor;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      if (converter is RationalUnitConverter) {
        var rc = (RationalUnitConverter)converter;
        long dividendLong = dividend * rc.dividend;
        long divisorLong = divisor * rc.divisor;
        double dividendDouble = ((double)dividend) * rc.dividend;
        double divisorDouble = ((double)divisor) * rc.divisor;
        if ((dividendLong != dividendDouble) || (divisorLong != divisorDouble)) {
          return new MultiplicationUnitConverter (dividendDouble / divisorDouble);
        }
        long gcd = Gcd (dividendLong, divisorLong);
        return ValueOf (dividendLong / gcd, divisorLong / gcd);
      } else if (converter is MultiplicationUnitConverter) {
        return converter.Concatenate(this);
      } else {
        return base.Concatenate (converter);
      }
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      if (dividend < 0) {
        return ValueOf(-divisor, -dividend);
      } else {
        return ValueOf(divisor, dividend);
      }
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return new AdditionUnitConverter(dividend / (double)divisor);
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return x * dividend / (double)divisor;
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

    private static UnitConverter ValueOf(long dividend, long divisor) {
      if (dividend == divisor) {
        return IDENTITY;
      } else {
        return new RationalUnitConverter(dividend, divisor);
      }
    }
  }
}

