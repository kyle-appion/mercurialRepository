namespace ION.Core.Measure {

  using System;

  /// <summary>
  /// UnitConvert represents the conversion from one unit to another unit.
  /// </summary>
  public abstract class UnitConverter {

    public static readonly UnitConverter IDENTITY = new IdentityConverter();

    /// <summary>
    /// Determines whether this conversion is linear.
    /// </summary>
    /// <returns><c>true</c> if this instance is linear; otherwise, <c>false</c>.</returns>
    public abstract bool isLinear { get; }

    /// <summary>
    /// Default constructor for the <see cref="UnitConverter"/> class.
    /// </summary>
    protected UnitConverter () {
      // Nope
    }

    /// <summary>
    /// Returns the reverse operation of this conversion. This is used when converting to
    /// and from various units..
    /// </summary>
    /// <returns>The inverse of this unit converter.</returns>
    public abstract UnitConverter Inverse();

    /// <summary>
    /// Performs this converter's conversion operation upon the given x.
    /// </summary>
    /// <param name="x">The value to apply the conversion to.</param>
    /// <returns>The result of the conversion</returns>.
    public abstract double Convert(double x);

    /// <summary>
    /// Creates a UnitConverter that is the derivative of this unit converter.
    /// </summary>
    public abstract UnitConverter Derivative();


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

    public override bool Equals(object obj) {
      if (!(obj is UnitConverter)) {
        return false;
      } else {
        return Concatenate (((UnitConverter)obj).Inverse()) == IDENTITY;
      }
    }

    public bool IsZeroConverter() {
      if (this is ConstantConverter) {
        return ((ConstantConverter)this).constant == 0.0;
      } else {
        return false;
      }
    }

    public bool IsOneConverter() {
      if (this is ConstantConverter) {
        return ((ConstantConverter)this).constant == 1.0;
      } else {
        return false;
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
    public static long gcd(long m, long n) {
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
  sealed class IdentityConverter : UnitConverter {
    public override bool isLinear { get { return true; } }

    internal IdentityConverter() { }

    public override UnitConverter Inverse() {
      return this;
    }

    public override double Convert(double x) {
      return x;
    }

    public override UnitConverter Derivative() {
      return IDENTITY;
    }

    public override UnitConverter Concatenate(UnitConverter converter) {
      return converter;
    }

    public override string ToString () {
      return "IdentityConverter[ONE]";
    }
  }

  sealed class ConstantConverter : UnitConverter {
    public override bool isLinear { get { return true; } }

    public double constant { get; private set; }

    internal ConstantConverter(double constant) {
      this.constant = constant;
    }

    public override UnitConverter Inverse() {
      return new ConstantConverter(-constant);
    }

    public override double Convert(double x) {
      return constant;
    }

    public override UnitConverter Derivative() {
      return new ConstantConverter(0);
    }

    public override UnitConverter Concatenate(UnitConverter converter) {
      return converter;
    }

    public override string ToString () {
      return "ConstantConverter[constant: " + constant + "]";
    }
  }

  /// <summary>
  /// A converter that represents a compouned expression, such a 4 + 2. Note:
  /// sometimes the converter may be able to be concatenated on the fly, ig.
  /// 4 + 2 + 5 is equal to on AdditionConverter with an offset of 11. It is
  /// recommended to concatenate the converters when possible to reduce call 
  /// overhead.
  /// </summary>
  sealed class CompoundConverter : UnitConverter {
    /// <summary>
    /// The first converter in the compound conversion expression.
    /// </summary>
    public UnitConverter first { get; private set; }
    /// <summary>
    /// The second converter in the compound conversion expression.
    /// </summary>
    public UnitConverter second { get; private set; }

    public override bool isLinear { get {  return first.isLinear && second.isLinear;  } }

    internal CompoundConverter(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    public override UnitConverter Inverse() {
      return new CompoundConverter(second.Inverse(), first.Inverse());
    }

    public override double Convert(double x) {
      return second.Convert(first.Convert(x));
    }

    public override UnitConverter Derivative() {
      var fd = first.Derivative();
      var sd = second.Derivative();

      if (sd.IsZeroConverter() || fd.IsZeroConverter()) {
        return new ConstantConverter(0);
      } else {
        return new CompoundConverter(fd, sd);
      }
    }

    public override int GetHashCode() {
      return first.GetHashCode() * 21 ^ second.GetHashCode();
    }

    public override bool Equals(object obj) {
      if (!(obj is CompoundConverter)) {
        return false;
      } else {
        CompoundConverter occ = (CompoundConverter)obj;
        return first == occ.first && second == occ.second;
      }
    }

    public override string ToString () {
      return "CompoundConverter[first: " +   first + ", second: " + second + "]";
    }
  }

  public class RationalConverter : UnitConverter {
    public override bool isLinear { get { return true; } } 

    /// <summary>
    /// The dividend of the conversion operation.
    /// </summary>
    public long dividend { get; private set; }
    /// <summary>
    /// The divisor of the conversion operation.
    /// </summary>
    public long divisor { get; private set; }

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

      this.dividend = dividend;
      this.divisor = divisor;
    }

    public override UnitConverter Inverse () {
      if (dividend < 0) {
        return new RationalConverter (-divisor, -dividend);
      } else {
        return new RationalConverter (divisor, dividend);
      }
    }

    public override double Convert (double x) {
      return x * dividend / (double)divisor;
    }

    public override UnitConverter Derivative() {
      return new MultiplyConverter((double)dividend / (double)divisor);
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      if (converter is RationalConverter) {
        RationalConverter rc = (RationalConverter)converter;
        long dividendLong = dividend * rc.dividend;
        long divisorLong = divisor * rc.divisor;
        double dividendDouble = ((double)dividend) * rc.dividend;
        double divisorDouble = ((double)divisor) * rc.divisor;
        if ((dividendLong != dividendDouble) || (divisorLong != divisorDouble)) {
          return new MultiplyConverter (dividendDouble / divisorDouble);
        }
        long gcd = Gcd (dividendLong, divisorLong);
        return ValueOf (dividendLong / gcd, divisorLong / gcd);
      } else if (converter is MultiplyConverter) {
        return converter.Concatenate(this);
      } else if (converter is ConstantConverter) {
        var factor = dividend * ((ConstantConverter)converter).constant / dividend;
        if (factor == 0) {
          return new ConstantConverter(0);
        } else {
          return new MultiplyConverter(factor);
        }
      } else {
        return base.Concatenate(converter);
      }
    }

    public override string ToString () {
      return "RationConverter[dividend: " + dividend + ", divisor: " + divisor + "]";
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
  public class MultiplyConverter : UnitConverter {
    public override bool isLinear { get { return true; } }

    /// <summary>
    /// The factor of conversion.
    /// </summary>
    public double factor { get; private set; }

    public MultiplyConverter (double factor) {
      this.factor = factor;
    }

    public override UnitConverter Inverse () {
      return new MultiplyConverter (1.0 / factor);
    }

    public override double Convert (double x) {
      return factor * x;
    }

    public override UnitConverter Derivative() {
      return new MultiplyConverter(factor);
    }

    public override UnitConverter Concatenate(UnitConverter converter) {
      if (converter is MultiplyConverter) {
        return ValueOf (factor * ((MultiplyConverter)converter).factor);
      } else if (converter is RationalConverter) {
        RationalConverter rc = (RationalConverter)converter;
        double f = this.factor * rc.dividend / rc.divisor;
        return ValueOf(f);
      } else if (converter is ConstantConverter) {
        return ValueOf(factor * ((ConstantConverter)converter).constant);
      } else {
        return base.Concatenate(converter);
      }
    }

    public override string ToString() {
      return "MultiplyConverter[factor: " + factor + "]";
    }

    /// <summary>
    /// Wraps the creation of MultiplyConverter such that we don't spam identities.
    /// </summary>
    /// <returns>The converter for the value.</returns>
    /// <param name="value">The converter's value.</param>
    public static UnitConverter ValueOf (double value) {
      if (value == 0) {
        return new ConstantConverter(0);
      } else if (value == 1.0) {
        return IDENTITY;
      } else {
        return new MultiplyConverter (value);
      }
    }
  }

  /// <summary>
  /// The converter that applies additive conversions to units.
  /// </summary>
  public class AddConverter : UnitConverter {
    public override bool isLinear { get { return false; } }

    public double offset { get; private set; }

    public AddConverter (double offset) {
      if (offset == 0.0) {
        throw new ArithmeticException ("Offset cannot equal 0");
      }
      this.offset = offset;
    }

    public override UnitConverter Inverse () {
      return new AddConverter (-offset);
    }

    public override double Convert (double x) {
      return x + offset;
    }

    public override UnitConverter Derivative() {
      return new MultiplyConverter(1);
    }

    public override UnitConverter Concatenate (UnitConverter converter) {
      if (converter is AddConverter) {
        return ValueOf (offset + ((AddConverter)converter).offset);
      } else {
        return base.Concatenate (converter);
      }
    }

    public override string ToString () {
      return "AddConverter[offset: " + offset + "]";
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

  public class ProductOfConvertersConverter : UnitConverter {
    public override bool isLinear { get { return first.isLinear && second.isLinear; } }

    private UnitConverter first;
    private UnitConverter second;

    public ProductOfConvertersConverter(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    public override UnitConverter Inverse() {
      return new ProductOfConvertersConverter(first.Inverse(), second.Inverse());
    }

    public override double Convert(double x) {
      return first.Convert(x) * second.Convert(x);
    }

    public override UnitConverter Derivative() {
      return new AdditionOfConvertersConverter(new ProductOfConvertersConverter(first, first.Derivative()), 
                                               new ProductOfConvertersConverter(second.Derivative(), second));
    }

    public override UnitConverter Concatenate(UnitConverter converter) {
      if (converter is ProductOfConvertersConverter) {
        var pc = converter as ProductOfConvertersConverter;
        var f = first.Concatenate(pc.first);
        var s = second.Concatenate(pc.second);

        if (f.Concatenate(s).Equals(IDENTITY)) {
          return new ConstantConverter(0);
        } else if (f.Equals(IDENTITY) && s.Equals(IDENTITY)) {
          return new ConstantConverter(0);
        } else if (f.Equals(IDENTITY)) {
          return s;
        } else if (s.Equals(IDENTITY)) {
          return f;
        } else {
          return new ProductOfConvertersConverter(f, s);
        }
      } else {
        return base.Concatenate(converter);
      }
    }
  }

  public class AdditionOfConvertersConverter : UnitConverter {
    public override bool isLinear { get { return false; } }

    private UnitConverter first;
    private UnitConverter second;

    public AdditionOfConvertersConverter(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    public override double Convert(double x) {
      return first.Convert(x) + second.Convert(x);
    }

    public override UnitConverter Inverse() {
      throw new NotImplementedException("There is no inverse to the AdditionOfConvertersConverter");
    }

    public override UnitConverter Derivative() {
      return new AdditionOfConvertersConverter(first.Derivative(), second.Derivative());
    }

    public override UnitConverter Concatenate(UnitConverter converter) {
      if (converter is AdditionOfConvertersConverter) {
        var ac = converter as AdditionOfConvertersConverter;
        var f = first.Concatenate(ac.first);
        var s = second.Concatenate(ac.second);

        if (f.Concatenate(s).Equals(IDENTITY)) {
          return new ConstantConverter(0);
        } else if (f.Equals(IDENTITY) && s.Equals(IDENTITY)) {
          return new ConstantConverter(0);
        } else if (f.Equals(IDENTITY)) {
          return s;
        } else if (s.Equals(IDENTITY)) {
          return f;
        } else {
          return new AdditionOfConvertersConverter(f, s);
        }
      } else {
        return base.Concatenate(converter);
      }
    }
  }
}

