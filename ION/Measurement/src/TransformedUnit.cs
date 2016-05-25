namespace Measurement {

  using System;

  /// <summary>
  /// A transformed unit represents a unit that is derived from other units via a UnitConverter.
  /// </summary>
  /// <example>
  /// var celsius = kelvin.Add(273.15);
  /// </example>
  public class TransformedUnit : DerivedUnit {

    /// <summary>
    /// The unit that the transformed unit is based off of.
    /// </summary>
    public Unit parentUnit { get; private set; }
    /// <summary>
    /// The converter that will get the transformed unit to the parent unit.
    /// </summary>
    public UnitConverter toParentUnit { get; private set; }

    /// <summary>
    /// Returns the BaseUnit, AlternateUnit or ProductUnit (which contains base or alternate units) that this unit is
    /// derived from.
    /// </summary>
    /// <value>The standard unit.</value>
    public override Unit standardUnit {
      get {
        return parentUnit.standardUnit;
      }
    }

    /// <summary>
    /// Returns the converter that will converter from this unit to the this unit's system (base) unit.
    /// </summary>
    /// <value>The standard unit.</value>
    /// <returns>The standard unit.</returns>
    public override UnitConverter toStandardUnit { 
      get {
        return __toStandardUnit;
      }
    } UnitConverter __toStandardUnit;

    public TransformedUnit(Unit parentUnit, UnitConverter toParentUnit) {
      this.parentUnit = parentUnit;
      this.toParentUnit = toParentUnit;
      __toStandardUnit = parentUnit.toStandardUnit.Concatenate(toParentUnit);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.TransformedUnit"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return parentUnit.GetHashCode() ^ toParentUnit.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.TransformedUnit"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.TransformedUnit"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.TransformedUnit"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var tu = obj as TransformedUnit;
      return tu != null && parentUnit.Equals(tu.parentUnit) && toParentUnit.Equals(tu.toParentUnit);
    }
  }
}

