namespace Measurement {

  using System;

  /// <summary>
  /// Dimension represents a fundamental unit that is used to represent a base quantity. From a collection of dimenions,
  /// an entire system of units can be constructed.
  /// </summary>
  public class Dimension {

    /// <summary>
    /// The dimension of numbers.
    /// </summary>
    public static readonly Dimension NONE = new Dimension(Unit.ONE);
    /// <summary>
    /// The amount of a substance dimension.
    /// </summary>
    public static readonly Dimension AMOUNT_OF_SUBSTANCE = new Dimension('N');
    /// <summary>
    /// The electrical current dimension.
    /// </summary>
    public static readonly Dimension ELECTRIC_CURRENT = new Dimension('I');
    /// <summary>
    /// The Length dimension.
    /// </summary>
    public static readonly Dimension LENGTH = new Dimension('L');
    /// <summary>
    /// The Mass Dimension.
    /// </summary>
    public static readonly Dimension MASS = new Dimension('M');
    /// <summary>
    /// The temperature dimension.
    /// </summary>
    public static readonly Dimension TEMPERATURE = new Dimension('θ');
    /// <summary>
    /// The dimension of time.
    /// </summary>
    public static readonly Dimension TIME = new Dimension('T');

    /// <summary>
    /// The rough unit that is used to indentify the unit.
    /// </summary>
    private readonly Unit pseudoUnit;

    public Dimension(char symbol) {
      pseudoUnit = new BaseUnit("[" + symbol + "]");
    }

    internal Dimension(Unit pseudoUnit) {
      this.pseudoUnit = pseudoUnit;
    }


    /// <summary>
    /// Returns the multiplication of this dimension and the other.
    /// </summary>
    /// <param name="other">Other.</param>
    public Dimension Mul(Dimension other) {
      return new Dimension(pseudoUnit.Multiply(other.pseudoUnit));
    }

    /// <summary>
    /// Returns the division of this dimension and the other.
    /// </summary>
    /// <param name="other">Other.</param>
    public Dimension Div(Dimension other) {
      return new Dimension(pseudoUnit.Divide(other.pseudoUnit));
    }

    /// <summary>
    /// Returns the exponentiation of this dimension and the other.
    /// </summary>
    /// <param name="other">Other.</param>
    public Dimension Pow(int n) {
      return new Dimension(pseudoUnit.Pow(n));
    }

    /// <summary>
    /// Returns the nth root of this dimension.
    /// </summary>
    /// <param name="other">Other.</param>
    public Dimension Root(int n) {
      return new Dimension(pseudoUnit.Root(n));
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.Dimension"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return pseudoUnit.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.Dimension"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.Dimension"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.Dimension"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var d = obj as Dimension;

      return d != null && pseudoUnit.Equals(d.pseudoUnit);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="Measurement.Dimension"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="Measurement.Dimension"/>.</returns>
    public override string ToString() {
      return string.Format("[Dimension]");
    }
  }
}

