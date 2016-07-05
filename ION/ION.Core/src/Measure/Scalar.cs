namespace ION.Core.Measure {
  
  using System;
  using System.Runtime.Serialization;


  /// <summary>
  /// A Scalar is a numerical representation of a physical quantity.
  /// </summary>
  [DataContract(Name="Scalar")]
  public struct Scalar : IComparable<Scalar> {

    /// <summary>
    /// Queries the physical quntity that the scalar is represented as.
    /// </summary>
    public Unit unit { get { return __unit; } }
    [DataMember(Name = "unit")]
    private Unit __unit;

    /// <summary>
    /// Queries the amount of the phsysical quantity that this Scalar is representing.
    /// </summary>
    public double amount { get { return __amount; } }
    [DataMember(Name = "amount")]
    private double __amount;

    public Scalar(Unit unit, double amount) {
      __unit = unit;
      __amount = amount;
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Measure.Scalar"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Measure.Scalar"/>.</returns>
    public override string ToString() {
			return amount.ToString("#.##") + " " + unit;
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="ION.Core.Measure.Scalar"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such
    /// as a hash table.</returns>
    public override int GetHashCode() {
      int h = ((Double)amount).GetHashCode();
      return h ^ unit.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ION.Core.Measure.Scalar"/>.
    /// </summary>
    /// <param name="other">The <see cref="System.Object"/> to compare with the current <see cref="ION.Core.Measure.Scalar"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="ION.Core.Measure.Scalar"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object other) {
      if (other is Scalar) {
        return AssertEquals(((Scalar)other), amount);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Asserts that this scalar and the other scalar are equal within an error margin of epslion.
    /// </summary>
    /// <returns><c>true</c>, if equals was asserted, <c>false</c> otherwise.</returns>
    /// <param name="other">Other.</param>
    /// <param name="epsilon">Epsilon.</param>
    public bool AssertEquals(Scalar other, double epsilon) {
      if (unit.GetDimension() != other.unit.GetDimension()) {
        return false;
      }

      Unit baseUnit = unit.standardUnit;
      double thisValue = unit.GetConverterTo(baseUnit).Convert(amount);
      double otherValue = other.unit.GetConverterTo(baseUnit).Convert(other.amount);

      return System.Math.Abs(thisValue - otherValue) < epsilon;
    }

    /// <summary>
    /// Asserts that this scalar's amount is equal to the given amount within an error margin of epslion.
    /// </summary>
    /// <returns><c>true</c>, if equals was asserted, <c>false</c> otherwise.</returns>
    /// <param name="otherAmount">Other amount.</param>
    /// <param name="epislon">Epislon.</param>
    public bool AssertEquals(double otherAmount, double epislon) {
      return System.Math.Abs(otherAmount - amount) < epislon;
    }

    /// <summary>
    /// Queries whether or not the scalar is compatible with the given dimension.
    /// </summary>
    /// <returns><c>true</c>, if with was compatibled, <c>false</c> otherwise.</returns>
    /// <param name="quantity">Quantity.</param>
    public bool CompatibleWith(Quantity quantity) {
      return unit.quantity == quantity;
    }

    /// <Docs>To be added.</Docs>
    /// <para>Returns the sort order of the current instance compared to the specified object.</para>
    /// <summary>
    /// Compares this scalar to the other scalar. This should be used for sorting ONLY.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(Scalar other) {
      other = other.ConvertTo(unit);
      if (amount > other.amount) {
        return (int)System.Math.Ceiling(amount - other.amount);
      } else {
        return (int)System.Math.Floor(amount - other.amount);
      }
    }

    /// <summary>
    /// Converts this scalar into a new scalar with the given unit. The magnitude
    /// of the scalar will change based on the conversion from this unit to the
    /// given unit.
    /// </summary>
    /// <param name="other">
    ///     The unit to get a new scalar for.
    /// </param>
    /// <returns> The new scalar.
    /// </returns>
    public Scalar ConvertTo(Unit other) {
      AssertCompatible(unit, other);

      if (unit.Equals(other)) {
        return this;
      }

      UnitConverter converter = unit.GetConverterTo(other);
      return new Scalar(other, converter.Convert(amount));
    }

    /// <summary>
    /// Returns a scalar of the absolute value of this scalar's measurement.
    /// </summary>
    public Scalar Abs() {
      return new Scalar(unit, System.Math.Abs(amount));
    }

    public static Scalar operator +(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return new Scalar(o1.unit, o1.amount + o2.ConvertTo(o1.unit).amount);
    }

    public static Scalar operator +(Scalar o1, double o2) {
      return new Scalar(o1.unit, o1.amount + o2);
    }

    public static Scalar operator -(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return new Scalar(o1.unit, o1.amount - o2.ConvertTo(o1.unit).amount);
    }

    public static Scalar operator -(Scalar o1, double o2) {
      return new Scalar(o1.unit, o1.amount - o2);
    }

    public static Scalar operator *(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return new Scalar(o1.unit, o1.amount * o2.ConvertTo(o1.unit).amount);
    }

    public static Scalar operator *(Scalar o1, double o2) {
      return new Scalar(o1.unit, o1.amount * o2);
    }

    public static Scalar operator /(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return new Scalar(o1.unit, o1.amount / o2.ConvertTo(o1.unit).amount);
    }

    public static Scalar operator /(Scalar o1, double o2) {
      return new Scalar(o1.unit, o1.amount / o2);
    }

    public static bool operator >(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return o1.amount > o2.ConvertTo(o1.unit).amount;
    }

    public static bool operator >(Scalar o1, double o2) {
      return o1.amount > o2;
    }

    public static bool operator >=(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return o1.amount >= o2.ConvertTo(o1.unit).amount;
    }

    public static bool operator >=(Scalar o1, double o2) {
      return o1.amount >= o2;
    }

    public static bool operator <(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return o1.amount < o2.ConvertTo(o1.unit).amount;
    }

    public static bool operator <(Scalar o1, double o2) {
      return o1.amount < o2;
    }

    public static bool operator <=(Scalar o1, Scalar o2) {
      AssertCompatible(o1.unit, o2.unit);
      return o1.amount <= o2.ConvertTo(o1.unit).amount;
    }

    public static bool operator <=(Scalar o1, double o2) {
      return o1.amount <= o2;
    }


    public static bool operator !=(Scalar o1, Scalar o2) {
      return !(o1 == o2);
    }

    public static bool operator ==(Scalar o1, Scalar o2) {
      return (!object.ReferenceEquals(o1, null) && !object.ReferenceEquals(o2, null)) && (o1.amount == o2.amount && o1.unit.Equals(o2.unit));
    }



    /// <summary>
    /// Asserts that the two units are compatible with eachother.
    /// </summary>
    /// <returns>The compatible.</returns>
    /// <param name="o1">The first unit.</param>
    /// <param name="o2">The second unit.</param>
    private static void AssertCompatible(Unit o1, Unit o2) {
      if (!o1.IsCompatible(o2)) {
        throw new ArithmeticException("Cannot perform operation: " + o1 + " is incompatible with " + o2);
      }
    }
  }
}

