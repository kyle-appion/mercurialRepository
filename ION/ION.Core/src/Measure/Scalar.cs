namespace ION.Core.Measure {
  
  using System;
  using System.Runtime.Serialization;


  /// <summary>
  /// A Scalar is a numerical representation of a physical quantity.
  /// </summary>
  [DataContract(Name="Scalar")]
  public class Scalar : IComparable<Scalar> {

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

    // Overridden from Object
    public override string ToString() {
      return amount + " " + unit;
    }

    // Overridden from Object
    public override int GetHashCode() {
      int h = ((Double)amount).GetHashCode();
      return h ^ unit.GetHashCode();
    }

    // Overridden from Object
    public override bool Equals(object obj) {
      var other = obj as Scalar;
      if (other == null) {
        return false;
      } else {
        return unit == other?.unit && amount == other?.amount;
      }
    }

    public bool AssertEquals(Scalar other, double epsilon) {
      if (unit.GetDimension() != other.unit.GetDimension()) {
        return false;
      }

      Unit baseUnit = unit.standardUnit;
      double thisValue = unit.GetConverterTo(baseUnit).Convert(amount);
      double otherValue = other.unit.GetConverterTo(baseUnit).Convert(other.amount);

      return System.Math.Abs(thisValue - otherValue) < epsilon;
    }

    public bool AssertEquals(double otherAmount, double epislon) {
      return System.Math.Abs(otherAmount - amount) < epislon;
    }

    public bool CompatibleWith(Quantity quantity) {
      return unit.quantity == quantity;
    }

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

  /// <summary>
  /// An event that is used to retrieve a scalar that has been changed.
  /// </summary>
  public sealed class ScalarChangedEvent {
    /// <summary>
    /// The scalar that the catalyst was before the event fired.
    /// </summary>
    /// <value>The old scalar.</value>
    public Scalar oldScalar { get; private set; }
    /// <summary>
    /// The new scalar of the catalyst.
    /// </summary>
    public Scalar newScalar { get; private set; }

    public ScalarChangedEvent(Scalar oldScalar, Scalar newScalar) {
      this.oldScalar = oldScalar;
      this.newScalar = newScalar;
    }
  }
}

