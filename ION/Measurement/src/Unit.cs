namespace Measurement {

  using System;
  using System.Runtime.Serialization;

  [Serializable]
  public abstract class Unit {
    /// <summary>
    /// The identity unit.
    /// </summary>
    public static readonly Unit ONE = ProductUnit.ONE;

    /// <summary>
    /// Queries whether or nor the unit is a standard unit (a base or alternate unit).
    /// </summary>
    /// <value><c>true</c> if is standard unit; otherwise, <c>false</c>.</value>
    public bool isStandardUnit {
      get {
        return this.Equals(standardUnit);
      }
    }

    /// <summary>
    /// The dimension of the unit.
    /// </summary>
    /// <value>The dimension.</value>
    public virtual Dimension dimension {
      get {
        var su = standardUnit;
        if (su is BaseUnit) {
          return Units.MODEL.GetDimensionOfUnit((BaseUnit)su);
        }

        if (su is AlternateUnit) {
          return ((AlternateUnit)su).parent.dimension;
        }

        return su.dimension;
      }
    }

    /// <summary>
    /// Returns the BaseUnit, AlternateUnit or ProductUnit (which contains base or alternate units) that this unit is
    /// derived from.
    /// </summary>
    /// <value>The standard unit.</value>
    public abstract Unit standardUnit { get; }

    /// <summary>
    /// Returns the converter that will converter from this unit to the this unit's system (base) unit.
    /// </summary>
    /// <value>The standard unit.</value>
    public abstract UnitConverter toStandardUnit { get; }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="Measurement.Unit"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="Measurement.Unit"/>.</returns>
    public override string ToString() {
      return Units.GetSymbolForUnit(this);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.Unit"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public abstract int GetHashCode();

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.Unit"/>.
    /// </summary>
    /// <param name="other">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.Unit"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.Unit"/>;
    /// otherwise, <c>false</c>.</returns>
    public abstract bool Equals(object other);

    /// <summary>
    /// Queries whether or not the unit is compatible with the given unit.
    /// </summary>
    /// <returns><c>true</c> if this instance is compatible the specified other; otherwise, <c>false</c>.</returns>
    /// <param name="other">Other.</param>
    public bool IsCompatible(Unit other) {
      return this == other || standardUnit.Equals(other.standardUnit) || dimension.Equals(other.dimension);
    }


    /// <summary>
    /// Returns the unit that is derived from this unit using the spcified converter.
    /// </summary>
    /// <param name="converter">Converter.</param>
    public Unit Transform(UnitConverter converter) {
      if (converter == UnitConverter.IDENTITY) {
        return this;
      }

      var tf = this as TransformedUnit;
      if (tf != null) {
        var unit = tf.parentUnit;
        var toUnit = tf.toParentUnit.Concatenate(converter);
        if (toUnit == UnitConverter.IDENTITY) {
          return unit;
        } else {
          return new TransformedUnit(unit, toUnit);
        }
      } else {
        return new TransformedUnit(this, converter);
      }
    }

    /// <summary>
    /// Returns a new derived unit who is offset from this unit by the given amount.
    /// </summary>
    /// <param name="offset">Offset.</param>
    public Unit Add(double offset) {
      return Transform(new AdditionUnitConverter(offset));
    }

    /// <summary>
    /// Returns a new derived unit who is multiplied by the given factor.
    /// </summary>
    /// <param name="factor">Factor.</param>
    public Unit Multiply(double factor) {
      return Transform(new MultiplicationUnitConverter(factor));
    }

    /// <summary>
    /// Returns a new derived unit who is multipled by the given factor.
    /// </summary>
    /// <param name="factor">Factor.</param>
    public Unit Multiply(long factor) {
      return Transform(new RationalUnitConverter(factor, 1));
    }

    /// <summary>
    /// Returns a new derived unit who is multipled with the given factor.
    /// </summary>
    /// <param name="factor">Factor.</param>
    public Unit Multiply(Unit factor) {
      return ProductUnit.GetProductInstance(this, factor);
    }

    /// <summary>
    /// Returns the inverse of this unit.
    /// </summary>
    public Unit Inverse() {
      return ProductUnit.GetQuotientInstance(ONE, this);
    }

    /// <summary>
    /// Returns a new derived unit who is divided by the given divisor.
    /// </summary>
    /// <param name="divisor">Divisor.</param>
    public Unit Divide(double divisor) {
      return Transform(new MultiplicationUnitConverter(1 / divisor));      
    }

    /// <summary>
    /// Returns a new derived unit who is divided by the given factor.
    /// </summary>
    /// <param name="divisor">divisor.</param>
    public Unit Divide(long divisor) {
      return Transform(new RationalUnitConverter(1, divisor));
    }

    /// <summary>
    /// Returns a 
    /// </summary>
    /// <param name="divisor">Divisor.</param>
    public Unit Divide(Unit divisor) {
      return Multiply(divisor.Inverse());
    }

    /// <summary>
    /// Returns a unit equal to the given root of this unit.
    /// </summary>
    /// <param name="n"> The root's order. </param>
    /// <returns> The resultint unit. </returns>
    /// <exception cref="ArithmeticException"> if n == 0; </exception>
    public Unit Root(int n) {
      if (n > 0) {
        return ProductUnit.GetRootInstance(this, n);
      } else if (n == 0) {
        throw new ArithmeticException("Root order cannot be 0");
      } else {
        return ONE.Divide(Root(-n));
      }
    }

    /// <summary>
    /// Returns a unit equal to this unit raised to the given exponent.
    /// </summary>
    /// <param name="n"> The exponent
    /// @return </param>
    public Unit Pow(int n) {
      if (n > 0) {
        return Multiply(Pow(n - 1));
      } else if (n == 0) {
        return ONE;
      } else {
        return ONE.Divide(Pow(-n));
      }
    }


    /// <summary>
    /// The quantity that this unit is measured against.
    /// </summary>
    /// <value>The quantity.</value>
//    public abstract EQuantity quantity { get; }


  }
}

