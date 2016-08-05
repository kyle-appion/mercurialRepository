namespace ION.Core.Measure {

  using System;


  /// <summary>
  /// This class represens the dimension of an unit. Two units u1 and u2 are
  /// compatible if and only if u1.GetDimension().Equals(u2.GetDimension()).
  /// 
  /// Dimensions are immutable instances. All mathematical operations that
  /// are performs on dimensions return a new instance.
  /// </summary>
  public sealed class Dimension {
    /// <summary>
    /// The current physical model.
    /// </summary>
    /// <value>The CURREN t MODE.</value>
    public static IModel CURRENT_MODEL { get { return __CURRENT_MODEL; } set { __CURRENT_MODEL = value; } }

    private static IModel __CURRENT_MODEL = new StandardModel();

    /// <summary>
    /// This dimension represents the concept of dimensionlessness.
    /// </summary>
    public static readonly Dimension NONE = new Dimension(Unit.ONE);
    /// <summary>
    /// The dimension representing length and distance.
    /// </summary>
    public static readonly Dimension LENGTH = new Dimension("L");
    /// <summary>
    /// The dimension representing mass.
    /// </summary>
    public static readonly Dimension MASS = new Dimension("M");
    /// <summary>
    /// The dimension representing time.
    /// </summary>
    public static readonly Dimension TIME = new Dimension("T");
    /// <summary>
    /// The dimension representing electric current.
    /// </summary>
    public static readonly Dimension ELECTRIC_CURRENT = new Dimension("I");
    /// <summary>
    /// The dimension representing temperature.
    /// </summary>
    public static readonly Dimension TEMPERATURE = new Dimension("T");
    /// <summary>
    /// The dimension representing the abount of a substance.
    /// </summary>
    public static readonly Dimension AMOUNT_OF_SUBSTANCE = new Dimension("N");

    /// <summary>
    /// The unit that is associated to this dimension.
    /// </summary>
    private readonly Unit __psuedoUnit;

    public Dimension(string symbol) {
      __psuedoUnit = new BaseUnit(Quantity.Dimensionless, symbol);
    }

    public Dimension(Unit unit) {
      __psuedoUnit = unit;
    }

    public Dimension Mul(Dimension other) {
      return new Dimension(__psuedoUnit.Mul(other.__psuedoUnit));
    }

    public Dimension Div(Dimension other) {
      return new Dimension(__psuedoUnit.Div(other.__psuedoUnit));
    }

    /// <summary>
    /// Raises this Dimension by the given exponent.
    /// </summary>
    /// <param name="n">The exponent to raise this dimension to.</param>
    public Dimension Pow(int n) {
      return new Dimension(__psuedoUnit.Pow(n));
    }

    /// <summary>
    /// Reduces this Dimension by the given root.
    /// </summary>
    /// <param name="n">The root to reduce this dimension by.</param>
    public Dimension Root(int n) {
      return new Dimension(__psuedoUnit.Root(n));
    }

    public override string ToString() {
      return __psuedoUnit.ToString();
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is Dimension) {
        return ((Dimension)other).__psuedoUnit.Equals(__psuedoUnit);
      } else {
        return false;
      }
    }

    public override int GetHashCode() {
      return __psuedoUnit.GetHashCode();
    }
  }

  public interface IModel {
    /// <summary>
    /// Queries the dimension of the specified base unit.
    /// </summary>
    /// <returns>The dimension.</returns>
    /// <param name="unit">The base unit to get the dimension of..</param>
    Dimension GetDimension(BaseUnit unit);
  }

  internal class StandardModel : IModel {
    public Dimension GetDimension(BaseUnit unit) {
      if (Units.Length.METER.Equals(unit)) {
        return Dimension.LENGTH;
      } else if (Units.Mass.KILOGRAM.Equals(unit)) {
        return Dimension.MASS;
      } else if (Units.Temperature.KELVIN.Equals(unit)) {
        return Dimension.TEMPERATURE;
      } else if (Units.Time.SECOND.Equals(unit)) {
        return Dimension.TIME;
      } else {
        throw new Exception("Failed to resolve dimension for " + unit);
      }
    }
  }
}

