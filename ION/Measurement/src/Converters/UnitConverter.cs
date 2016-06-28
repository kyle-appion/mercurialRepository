namespace Measurement {
  
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// UnitConverter represents a conversion direction from one unit to another unit. All unit converters are contractually 
  /// obligated to have an immutable state and are thus making them safe to pass around to anyone. If a unit converter
  /// fails to meet this requirement, its state will be clobbered thoughout the use of the measurement library.
  /// </summary>
  [DataContract(Name="UnitConverter")]
  public abstract class UnitConverter {

    /// <summary>
    /// The identity converter. This is used to identify a converter that does nothing. Essentially, this converter is
    /// equivalent to multuplying by one or adding 0 to the converter.
    /// </summary>
    public static readonly UnitConverter IDENTITY = new IdentityConverter();

    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value><c>true</c> if is linear; otherwise, <c>false</c>.</value>
    public abstract bool isLinear { get; }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>
    /// Note: implementations MUST return IDENTITY if their conversion is an identity conversion.
    /// </remarks>
    /// <param name="converter">Converter.</param>
    public virtual UnitConverter Concatenate(UnitConverter converter) {
      if (this == IDENTITY && converter == IDENTITY) {
        return IDENTITY;
      } else if (this == IDENTITY) {
        return converter;
      } else if (converter == IDENTITY) {
        return this;
      } else {
        return converter == IDENTITY ? this : new CompoundConverter(converter, this);
      }
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public abstract UnitConverter Inverse();

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public abstract UnitConverter TakeDerivative();

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public abstract double Convert(double x);

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.UnitConverter"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      if (IDENTITY == this) {
        return IDENTITY.GetHashCode();
      } else {
        return base.GetHashCode();
      }
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.UnitConverter"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.UnitConverter"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.UnitConverter"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var uc = obj as UnitConverter;

      if (uc != null) {
        return Concatenate(uc).Inverse() == IDENTITY;
      } else {
        return false;
      }
    }
  }

  /// <summary>
  /// IndentityConverter is a converter that perform exactly zero conversion to provided value.
  /// </summary>
  sealed class IdentityConverter : UnitConverter {
    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return true;
      }
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      return converter;
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return this;
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return this;
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return x;
    }
  }

  /// <summary>
  /// A converter that is a combination of multiple unit converters into a single descrete converter. This is used to 
  /// combine and concatenate various transforms into a non-linear transformation.
  /// </summary>
  [DataContract(Name="CompoundConverter")]
  sealed class CompoundConverter : UnitConverter {
    /// <summary>
    /// The first unit converter. This is first only in that it is the first term in the expression.
    /// </summary>
    private UnitConverter first;
    /// <summary>
    /// The second unit converter. This is second only in that it is the second term in the expression.
    /// </summary>
    private UnitConverter second; 

    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return first.isLinear && second.isLinear;
      }
    }

    internal CompoundConverter(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return new CompoundConverter(second, first);
    }

    /// <summary>
    /// Takes the derivative of this unit converter. This is done to get the effective rate of change of the converter
    /// as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      // A unit converter can be expressed a second(first(x)). Thus, when we take the derivative, we expect
      // second'(first(x)) * first'(x)
      var df = first.TakeDerivative();
      var ds = second.TakeDerivative();

      var ret = new MultiplyUnitConverters(ds.Concatenate(first), df);

      if (ret.Inverse() == IDENTITY) {
        return IDENTITY;
      } else {
        return ret;
      }
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return second.Convert(first.Convert(x));
    }
  }

  /// <summary>
  /// A converter that is a the product of two unit converters. This is primarily used when the derivative of a unit
  /// converter is taken.
  /// </summary>
  sealed class MultiplyUnitConverters : UnitConverter {
    /// <summary>
    /// The first unit converter. This is first only in that it is the first term in the expression.
    /// </summary>
    private UnitConverter first;
    /// <summary>
    /// The second unit converter. This is second only in that it is the second term in the expression.
    /// </summary>
    private UnitConverter second; 

    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return first.isLinear && second.isLinear;
      }
    }

    internal MultiplyUnitConverters(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      return new MultiplyUnitConverters(first.Concatenate(converter), second.Concatenate(converter));
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return new MultiplyUnitConverters(first.Inverse(), second.Inverse());
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      var df = first.TakeDerivative();
      var ds = second.TakeDerivative();

      return new AddUnitConverters(new MultiplyUnitConverters(df, second), new MultiplyUnitConverters(first, ds));
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return first.Convert(x) * second.Convert(x);
    }
  }

  sealed class AddUnitConverters : UnitConverter {
    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return false;
      }
    }
    /// <summary>
    /// The first unit converter. This is first only in that it is the first term in the expression.
    /// </summary>
    private UnitConverter first;
    /// <summary>
    /// The second unit converter. This is second only in that it is the second term in the expression.
    /// </summary>
    private UnitConverter second; 

    internal AddUnitConverters(UnitConverter first, UnitConverter second) {
      this.first = first;
      this.second = second;
    }

    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      return new AddUnitConverters(this, converter);
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return new AddUnitConverters(first.Inverse(), second.Inverse());
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return FromValue(first.TakeDerivative(), second.TakeDerivative());
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return first.Convert(x) + second.Convert(x);
    }

    private static UnitConverter FromValue(UnitConverter first, UnitConverter second) {
      if (first == IDENTITY && second == IDENTITY) {
        return IDENTITY;
      } else if (first == IDENTITY) {
        return second;
      } else if (second == IDENTITY) {
        return first;
      } else {
        return new AddUnitConverters(first, second);
      }
    }
  }

  /// <summary>
  /// A unit converter that always returns one. This unit converter can be the result of some mathematical operations.
  /// </summary>
  sealed class OneUnitConverter : UnitConverter {
    /// <summary>
    /// Whether or not the unit converter is a linear transformation.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isLinear {
      get {
        return true;
      }
    }
    /// <summary>
    /// Concatenates this unit converter to the given unit converter. The concatenation works as follows: suppose
    /// that this converter is g(x) and the given converter is f(x). The converter that is then resulting from the
    /// concatenation is g(f(x)).
    /// </summary>
    /// <remarks>Note: implementations MUST return IDENTITY if their conversion is an identity conversion.</remarks>
    /// <param name="converter">Converter.</param>
    public override UnitConverter Concatenate(UnitConverter converter) {
      return converter;
    }

    /// <summary>
    /// Takes the inverse (1 / this.converter) of this converter. This is used to convert one way, and then back from a
    /// unit.
    /// </summary>
    public override UnitConverter Inverse() {
      return this;
    }

    /// <summary>
    /// Takes the derivative of this unit converter with respect to x. This is done to get the effective rate of change
    /// of the converter as it is applied to the unit.
    /// </summary>
    /// <returns>The derivative.</returns>
    public override UnitConverter TakeDerivative() {
      return IDENTITY;
    }

    /// <summary>
    /// Performs the conversion of the converter. This performs the mathematics of the conversion from one unit to
    /// another.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public override double Convert(double x) {
      return 1;
    }
  }
}

