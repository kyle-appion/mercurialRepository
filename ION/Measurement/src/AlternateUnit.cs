namespace Measurement {

  using System;

  /// <summary>
  /// AlternateUnit represents units that are of the same dimension, but represent a different nature of measurement.
  /// </summary>
  public class AlternateUnit : DerivedUnit {
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
    /// <summary>
    /// The unit that the alternate unit is effectively derived from.
    /// </summary>
    /// <value>The parent.</value>
    public Unit parent { get; private set; }

    public AlternateUnit(string symbol, Unit parent) {
      if (!parent.isStandardUnit) {
        throw new Exception("Cannot create an alternate unit for: " + symbol + " the unit is not a standard unit.");
      }
      this.symbol = symbol;
      this.parent = parent;
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.AlternateUnit"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return symbol.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.AlternateUnit"/>.
    /// </summary>
    /// <param name="other">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.AlternateUnit"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.AlternateUnit"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object other) {
      var au = other as AlternateUnit;
      return au != null && symbol.Equals(au.symbol);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="Measurement.AlternateUnit"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="Measurement.AlternateUnit"/>.</returns>
    public override string ToString() {
      return symbol;
    }
  }
}

