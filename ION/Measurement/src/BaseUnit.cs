namespace Measurement {

  using System;

  /// <summary>
  /// The base unit from which all other units are derived.
  /// </summary>
  public class BaseUnit : Unit {
    /// <summary>
    /// Returns the BaseUnit, AlternateUnit or ProductUnit (which contains base or alternate units) that this unit is
    /// derived from.
    /// </summary>
    /// <value>The standard unit.</value>
    public override Unit standardUnit {
      get {
        return this;
      }
    }
    /// <summary>
    /// Returns the converter that will converter from this unit to the this unit's system (base) unit.
    /// </summary>
    /// <value>The standard unit.</value>
    public override UnitConverter toStandardUnit {
      get {
        return UnitConverter.IDENTITY;
      }
    }

    /// <summary>
    /// The human readable symbol that is used to identify the unit.
    /// </summary>
    /// <value>The symbol.</value>
    public string symbol { get; private set; }

    public BaseUnit(string symbol) {
      this.symbol = symbol;
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.BaseUnit"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return symbol.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.BaseUnit"/>.
    /// </summary>
    /// <param name="other">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.BaseUnit"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.BaseUnit"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object other) {
      var bu = other as BaseUnit;
      return bu != null && symbol.Equals(bu.symbol);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="Measurement.BaseUnit"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="Measurement.BaseUnit"/>.</returns>
    public override string ToString() {
      return symbol;
    }
  }
}

