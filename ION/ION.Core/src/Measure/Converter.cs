using System;
using System.Runtime.Serialization;


namespace ION.Core.Measure {

  /// <summary>
  /// UnitConvert represents the conversion from one unit to another unit.
  /// </summary>
  [DataContract(Name="UnitConverter")]
  public abstract class UnitConverter {

    public static readonly UnitConverter IDENTITY = new IdentityConverter ();

    /// <summary>
    /// Default constructor for the <see cref="Measure.UnitConverter"/> class.
    /// </summary>
    protected UnitConverter () {
      // Nope
    }

    /// <summary>
    /// Returns the reverse operation of this conversion. This is used when converting to
    /// and from various units..
    /// </summary>
    /// <returns>The inverse of this unit converter.</returns>
    public abstract UnitConverter Inverse ();

    /// <summary>
    /// Performs this converter's conversion operation upon the given x.
    /// </summary>
    /// <param name="x">The value to apply the conversion to.</param>
    /// <returns>The result of the conversion</returns>.
    public abstract double Convert (double x);

    /// <summary>
    /// Determines whether this conversion is linear.
    /// </summary>
    /// <returns><c>true</c> if this instance is linear; otherwise, <c>false</c>.</returns>
    public abstract bool IsLinear ();

    /// <summary>
    /// Concatenates this converter with another converter. The resulting converter
    /// is equivalent to the first converter concatenated with this converter.
    /// 
    /// Implementations must ensure that the IDENTITY instance is returned if the
    /// resulting converter is an identity converter.
    /// </summary>
    /// <param name="converter">The converter to concatenate with this converter.</param>
    /// <returns>The concatenation of this converter with the other</returns>
    public virtual UnitConverter Concatenate (UnitConverter converter) {
      return converter == IDENTITY ? this : new CompoundConverter (converter, this);
    }

    public override int GetHashCode() {
      if (IDENTITY == this) {
        return IDENTITY.GetHashCode();
      } else { 
      return base.GetHashCode();
      }
    }

    public override bool Equals (System.Object other) {
      if (!(other is UnitConverter)) {
        return false;
      } else {
        return Concatenate (((UnitConverter)other).Inverse ()) == IDENTITY;
      }
    }

    /// <summary>
    /// Finds the greatest common denominator of m and n.
    /// <code>
    /// long m = 12;
    /// long n = 3;
    /// Console.WriteLine("gcd(" + m + ", " + n + ") => " + gcd(m, n));
    /// >>> OUT
    ///   gcd(12, 3) -> 3
    /// </code>
    /// </summary>
    /// 
    /// <param name="m">The first parameter used to calculate the gcd.</param>
    /// <param name="n">The second paramter used to calculate the gcd.</param>
    /// 
    /// <returns>The greatest common denominator of m and n.</returns>
    public static long gcd (long m, long n) {
      if (n == 0) {
        return m;
      } else {
        return gcd (n, m % n);
      }
    }
  }

  /// <summary>
  /// An identity converter is a converter that performs exactly no conversion to
  /// the provided values.
  /// </summary>
  [DataContract(Name="IdentityConverter")]
  sealed class IdentityConverter : UnitConverter {
    internal IdentityConverter() { }

    public override UnitConverter Inverse () {
      return this;
    }

    public override double Convert (double x) {
      return x;
    }

    public override bool IsLinear () {
      return true;
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      return converter;
    }

    public override string ToString () {
      return "IdentityConverter[ONE]";
    }
  }

  /// <summary>
  /// A converter that represents a compouned expression, such a 4 + 2. Note:
  /// sometimes the converter may be able to be concatenated on the fly, ig.
  /// 4 + 2 + 5 is equal to on AdditionConverter with an offset of 11. It is
  /// recommended to concatenate the converters when possible to reduce call 
  /// overhead.
  /// </summary>
  [DataContract(Name="CompountConverter")]
  sealed class CompoundConverter : UnitConverter {
    /// <summary>
    /// The first converter in the compound conversion expression.
    /// </summary>
    [DataMember(Name="first")]
    private readonly UnitConverter __first;
    /// <summary>
    /// The second converter in the compound conversion expression.
    /// </summary>
    [DataMember(Name="second")]
    private readonly UnitConverter __second;

    internal CompoundConverter (UnitConverter first, UnitConverter second) {
      __first = first;
      __second = second;
    }

    public override UnitConverter Inverse () {
      return new CompoundConverter (__second.Inverse (), __first.Inverse ());
    }

    public override double Convert (double x) {
      return __second.Convert (__first.Convert (x));
    }

    public override bool IsLinear () {
      return __first.IsLinear () && __second.IsLinear ();
    }

    public override int GetHashCode() {
      return __first.GetHashCode() * 21 ^ __second.GetHashCode();
    }

    public override bool Equals (System.Object other) {
      if (!(other is CompoundConverter)) {
        return false;
      } else {
        CompoundConverter occ = (CompoundConverter)other;
        return __first == occ.__first && __second == occ.__second;
      }
    }

    public override string ToString () {
      return "UnitConverter[first: " + __first + ", second: " + __second + "]";
    }
  }

  [DataContract(Name="RationalConverter")]
  public class RationalConverter : UnitConverter {
    /// <summary>
    /// The dividend of the conversion operation.
    /// </summary>
    [DataMember(Name="dividend")]
    private readonly long __dividend;
    /// <summary>
    /// The divisor of the conversion operation.
    /// </summary>
    [DataMember(Name="divisor")]
    private readonly long __divisor;

    /// <summary>
    /// Initializes a new instance of the <see cref="Measure.RationalConverter"/> class.
    /// </summary>
    /// <param name="dividend">The conversion dividend.</param>
    /// <param name="divisor">The conversion divisor.</param>
    /// <exception cref="ArithmeticException">If the divisor is less than 0 or the 
    /// dividend equals the divisor.</exception>
    public RationalConverter (long dividend, long divisor) {
      if (divisor < 0) {
        throw new ArithmeticException ("Divisor cannot equal 0");
      }

      if (dividend == divisor) {
        throw new ArithmeticException ("Dividend cannot equal divisor");
      }

      __dividend = dividend;
      __divisor = divisor;
    }

    public override UnitConverter Inverse () {
      if (__dividend < 0) {
        return new RationalConverter (-__divisor, -__dividend);
      } else {
        return new RationalConverter (__divisor, __dividend);
      }
    }

    public override double Convert (double x) {
      return x * __dividend / (double)__divisor;
    }

    public override bool IsLinear () {
      return true;
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      if (converter is RationalConverter) {
        RationalConverter rc = (RationalConverter)converter;
        long dividendLong = __dividend * rc.__dividend;
        long divisorLong = __divisor * rc.__divisor;
        double dividendDouble = ((double)__dividend) * rc.__dividend;
        double divisorDouble = ((double)__divisor) * rc.__divisor;
        if ((dividendLong != dividendDouble) || (divisorLong != divisorDouble)) {
          return new MultiplyConverter (dividendDouble / divisorDouble);
        }
        long gcd = Gcd (dividendLong, divisorLong);
        return ValueOf (dividendLong / gcd, divisorLong / gcd);
      } else if (converter is MultiplyConverter) {
        return converter.Concatenate(this);
      } else {
        return base.Concatenate (converter);
      }
    }

    public override string ToString () {
      return "RationConverter[dividend: " + __dividend + ", divisor: " + __divisor + "]";
    }

    /// <summary>
    /// Queries the dividend of the converter.
    /// </summary>
    /// <returns>The dividend.</returns>
    public double GetDividend () {
      return __dividend;
    }

    /// <summary>
    /// Queries the divisor of the converter.
    /// </summary>
    /// <returns>The divisor.</returns>
    public double GetDivisor () {
      return __divisor;
    }

    /// <summary>
    /// Wraps the creation of RationalConverters such that we don't spam identities.
    /// </summary>
    /// <returns>The converter matching the dividend and divisor.</returns>
    /// <param name="dividend">The converter's dividend.</param>
    /// <param name="divisor">The converter's divisor.</param>
    private static UnitConverter ValueOf (long dividend, long divisor) {
      return (dividend == 1) && (divisor == 1) ? IDENTITY : new RationalConverter (dividend, divisor);
    }

    /// <summary>
    /// Finds the greatest common denominator of m and n.
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
    public static long Gcd(long m, long n) {
      if (n == 0L) {
        return m;
      } else {
        return Gcd(n, m % n);
      }
    }
  }

  /// <summary>
  /// The converter that applies multiplicative conversions to units.
  /// </summary>
  [DataContract(Name="MultiplyConverter")]
  public class MultiplyConverter : UnitConverter {
    /// <summary>
    /// The factor of conversion.
    /// </summary>
    [DataMember(Name="factor")]
    private readonly double __factor;

    public MultiplyConverter (double factor) {
      __factor = factor;
    }

    public override UnitConverter Inverse () {
      return new MultiplyConverter (1.0 / __factor);
    }

    public override double Convert (double x) {
      return __factor * x;
    }

    public override bool IsLinear () {
      return true;
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      if (converter is MultiplyConverter) {
        return ValueOf (__factor * ((MultiplyConverter)converter).__factor);
      } else if (converter is RationalConverter) {
        RationalConverter rc = (RationalConverter)converter;
        double factor = __factor * rc.GetDividend () / rc.GetDivisor ();
        return ValueOf(factor);
      } else {
        return base.Concatenate (converter);
      }
    }

    public override string ToString () {
      return "MultiplyConverter[factor: " + __factor + "]";
    }

    /// <summary>
    /// Queries the factor of the converter.
    /// </summary>
    /// <returns>The factor.</returns>
    public double GetFactor () {
      return __factor;
    }

    /// <summary>
    /// Wraps the creation of MultiplyConverter such that we don't spam identities.
    /// </summary>
    /// <returns>The converter for the value.</returns>
    /// <param name="value">The converter's value.</param>
    public static UnitConverter ValueOf (double value) {
      return value == 1.0 ? IDENTITY : new MultiplyConverter (value);
    }
  }

  /// <summary>
  /// The converter that applies additive conversions to units.
  /// </summary>
  [DataContract(Name="AddConverter")]
  public class AddConverter : UnitConverter {
    [DataMember(Name="offset")]
    private readonly double __offset;

    public AddConverter (double offset) {
      if (offset == 0.0) {
        throw new ArithmeticException ("Offset cannot equal 0");
      }
      __offset = offset;
    }

    public override UnitConverter Inverse () {
      return new AddConverter (-__offset);
    }

    public override bool IsLinear() {
      return false;
    }

    public override double Convert (double x) {
      return x + __offset;
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      if (converter is AddConverter) {
        return ValueOf (__offset + ((AddConverter)converter).__offset);
      } else {
        return base.Concatenate (converter);
      }
    }

    public override string ToString () {
      return "AddConverter[offset: " + __offset + "]";
    }

    public double getOffset () {
      return __offset;
    }

    /// <summary>
    /// Wraps the creation of AddConverters such that we don't spam identities.
    /// </summary>
    /// <returns>The converter matching the value.</returns>
    /// <param name="value">The converter's value.</param>
    private static UnitConverter ValueOf (double value) {
      return value == 0.0 ? IDENTITY : new AddConverter (value);
    }
  }
}

