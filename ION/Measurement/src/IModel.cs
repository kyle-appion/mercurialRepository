namespace Measurement {

  using System;

  /// <summary>
  /// The Model interface represents the mapping between BaseUnits and a Dimension.
  /// </summary>
  public interface IModel {
    /// <summary>
    /// Queries the dimension of the given unit.
    /// </summary>
    /// <value>The dimension.</value>
    Dimension GetDimensionOfUnit(BaseUnit unit);
    /// <summary>
    /// Queries the normalization transform of the specified base unit.
    /// </summary>
    /// <returns>The transform of unit.</returns>
    /// <param name="unit">Unit.</param>
    UnitConverter GetTransformOfUnit(BaseUnit unit);
  }
}

