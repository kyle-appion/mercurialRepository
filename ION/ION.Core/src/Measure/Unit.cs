namespace ION.Core.Measure {

  using System;

  public abstract class Unit {
    /// <summary>
    /// The units that is indicative of a unit of magnitude one.
    /// </summary>
    public static readonly Unit ONE = new ProductUnit();

    /// <summary>
    /// Returns the quantity that the unit is representative of.
    /// </summary>
    public Quantity quantity { get { return __quantity; } }
    private Quantity __quantity;

    /// <summary>
    /// Returns the <seealso cref="BaseUnit"/>, <seealso cref="AlternateUnit"/> or
    /// product of base units and alternate units this unit is derived from. The
    /// standard unit identifies the "type" of <seealso cref="Quantity"/> that this
    /// unit is.
    /// </summary>
    /// <returns> The unit this unit is derived from. </returns>
    public abstract Unit standardUnit { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="ION.Measure.Unit"/> class.
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    protected Unit(Quantity quantity) {
      __quantity = quantity;
    }

    /// <summary>
    /// Returns this exact unit with a new quantity. This is used for remapping a
    /// unit into a new "mode".
    /// </summary>
    /// <returns> This unit with a new quantity. </returns>
//    public abstract Unit ToQuantity();

    /// <summary>
    /// Returns the <seealso cref="UnitConverter"/> that will convert this unit to its
    /// standard unit.
    /// </summary>
    /// <returns> The standard unit converter. </returns>
    public abstract UnitConverter ToStandardUnit();

    /// <summary>
    /// Returns the BaseUnit or AlternateUnit or Product of BaseUnits and AlternateUnits
    /// that this unit is derived from. The standard unit identifies the "type" of Quantity
    /// that this unit is.
    /// </summary>
    /// <returns>The standard unit.</returns>
//    public abstract Unit GetStandardUnit();

    /// <summary>
    /// Force child classes to re-implement GetHashCode.
    /// </summary>
    /// <returns> The hash code of the unit. </returns>
    public override abstract int GetHashCode();

    /// <summary>
    /// Force child classes to re-implement equals.
    /// </summary>
    /// <param name="other"> The object to attempt to equate to this unit. </param>
    /// <returns> True if this unit is equal to other. </returns>
    public override abstract bool Equals(object other);

    public override sealed string ToString() {
      if (this is BaseUnit) {
        return ((BaseUnit)this).symbol;
      } else if (this is AlternateUnit) {
        return ((AlternateUnit)this).symbol;
      } else if (this is NamedUnit) {
        return ((NamedUnit)this).symbol;
      } else {
        return "unk";
      }
    }

    /// <summary>
    /// Creates a new Scalar that will wrap this Unit with a magnitude.
    /// </summary>
    /// <returns>The scalar.</returns>
    /// <param name="magnitude">The magnitude of the created Scalar.</param>
    public Scalar OfScalar(double magnitude) {
      return new Scalar(this, magnitude);
    }

    /// <summary>
    /// Creates a new scalar span that will represents a "distance" magnitude for this unit.
    /// </summary>
    /// <returns>The span.</returns>
    /// <param name="magnitude">Magnitude.</param>
    public ScalarSpan OfSpan(double magnitude) {
      return new ScalarSpan(this, magnitude);
    }

    /// <summary>
    /// Determines whether this instance is standard unit.
    /// </summary>
    /// <returns><c>true</c> if this instance is standard unit; otherwise, <c>false</c>.</returns>
    public virtual bool IsStandardUnit() {
      return standardUnit.Equals(this);
    }

    /// <summary>
    /// Determines whether this instance is compatible the specified other.
    /// </summary>
    /// <returns><c>true</c> if this instance is compatible the specified other; otherwise, <c>false</c>.</returns>
    /// <param name="other">The Unit to determine compatibility with.</param>
    public bool IsCompatible(Unit other) {
      return quantity == other.quantity;
    }

    /// <summary>
    /// Queries the dimension of this unit. The returned dimension depends on the current
    /// dimension model.
    /// </summary>
    /// <returns>The dimension.</returns>
    public Dimension GetDimension() {
      Unit standard = standardUnit;

      if (standard is BaseUnit) {
        return Dimension.CURRENT_MODEL.GetDimension((BaseUnit)standard);
      }

      if (standard is AlternateUnit) {
        return ((AlternateUnit)standard).parent.GetDimension();
      }

      ProductUnit pu = (ProductUnit)standard;
      Dimension ret = Dimension.NONE;
      for (int i = 0; i < pu.unitCount; i++) {
        Unit u = pu.GetUnit(i);
        Dimension d = u.GetDimension().Pow(pu.GetUnitPow(i)).Root(pu.GetUnitRoot(i));
        ret = ret.Mul(d);
      }

      return ret;
    }

    /// <summary>
    /// Returns a Unitconverter that will convert this unit to the other unit.
    /// </summary>
    /// <returns>The UnitConverter.</returns>
    /// <param name="other">The unit to build the converter to.</param>
    public UnitConverter GetConverterTo(Unit other) {
      if (Equals(other)) {
        return UnitConverter.IDENTITY;
      }

      Unit thisStandardUnit = standardUnit;
      Unit otherStandardUnit = other.standardUnit;

      if (thisStandardUnit.Equals(otherStandardUnit)) {
        return other.ToStandardUnit().Inverse().Concatenate(ToStandardUnit());
      }

      // TODO ahodder@appioninc.com: Clean this up
      if (!thisStandardUnit.IsCompatible(other)) {
        throw new ArithmeticException("Cannot convert between " + this + " and " + other);
      }

      UnitConverter thisTransform = ToStandardUnit().Concatenate(TransformOf(GetBaseUnits()));
      UnitConverter otherTransform = other.ToStandardUnit().Concatenate(TransformOf(other.GetBaseUnits()));

      return otherTransform.Inverse().Concatenate(thisTransform);
    }

    /// <summary>
    /// Returns a unit equivalent to this unit, but used to either distinguish
    /// between quantities of a different nature but of the same dimension, or to
    /// apply a custom name to the unit. </summary>
    /// <param name="symbol"> The symbol used to quickly identify the unit. </param>
    /// <returns> The alternate unit for this unit. </returns>
    public AlternateUnit Alternate(Quantity quantity, string symbol) {
      return new AlternateUnit(quantity, this, symbol);
    }

    /// <summary>
    /// Returns the combination of this unit with the specified sub-unit.
    /// Compound units are typically used for formatting purposes.
    /// </summary>
    public CompoundUnit Compound(Unit unit) {
      return new CompoundUnit(this, unit);
    }

    /// <summary>
    /// Returns the unit derived from this unit using the specified converter. The
    /// converter does not need to be linear.
    /// </summary>
    /// <param name="operation"> The converter from the transformed unit to this unit. </param>
    public Unit Transform(UnitConverter operation) {
      if (this is TransformedUnit) {
        TransformedUnit tu = (TransformedUnit)this;
        Unit parent = tu.parent;
        UnitConverter converter = tu.toParent.Concatenate(operation);
        if (converter == UnitConverter.IDENTITY) {
          return parent;
        } else {
          return new TransformedUnit(parent.__quantity, parent, converter);
        }
      } else if (operation == UnitConverter.IDENTITY) {
        return this;
      } else {
        return new TransformedUnit(__quantity, this, operation);
      }
    }

    /// <summary>
    /// Returns the result of adding an offset to this unit. The returned unit is
    /// convertible with all units that are convertible with this unit.
    /// </summary>
    /// <param name="offset"> The offset added. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Add(double offset) {
      return Transform(new AddConverter(offset));
    }

    /// <summary>
    /// Returns the result of multiplying this unit by an exact factor.
    /// </summary>
    /// <param name="factor"> The exact scale factor. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Mul(long factor) {
      return Transform(new RationalConverter(factor, 1));
    }

    /// <summary>
    /// Returns the result of multiplying this unit by an approximate factor.
    /// </summary>
    /// <param name="factor"> The approximate factor. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Mul(double factor) {
      return Transform(new MultiplyConverter(factor));
    }

    /// <summary>
    /// Returns the product of this unit with the one specified.
    /// <para>
    ///   Note: when this transform action is used, the resulting unit will have
    ///   this units quantity. This is important to remember if the provided unit
    ///   does NOT share the same quantity.
    /// </para>
    /// </summary>
    /// <param name="other"> The multiplicand unit. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Mul(Unit other) {
      return ProductUnit.GetProductInstance(this, other);
    }

    /// <summary>
    /// Returns the inverse of this unit.
    /// </summary>
    /// <returns> The unit representing 1 / this </returns>
    public Unit Inverse() {
      return ProductUnit.GetQuotientInstance(ONE, this);
    }

    /// <summary>
    /// Returns the result of dividing this unit by an exact divisor.
    /// </summary>
    /// <param name="divisor"> The exact divisor. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Div(long divisor) {
      return Transform(new RationalConverter(1, divisor));
    }

    /// <summary>
    /// Returns the result of dividing this unit by an approximate divisor.
    /// </summary>
    /// <param name="divisor"> The approximate divisor. </param>
    /// <returns> The transformed unit. </returns>
    public Unit Div(double divisor) {
      return Transform(new MultiplyConverter(1.0 / divisor));
    }

    /// <summary>
    /// Returns the quotient of this unit with the other.
    /// <para>
    ///   Note: when this transform action is used, the resulting unit will have
    ///   this units quantity. This is important to remember if the provided unit
    ///   does NOT share the same quantity.
    /// </para>
    /// </summary>
    /// <param name="other"> The divisor unit. </param>
    /// <returns> The unit representing this / other. </returns>
    public Unit Div(Unit other) {
      return Mul(other.Inverse());
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
        return ONE.Div(Root(-n));
      }
    }

    /// <summary>
    /// Returns a unit equal to this unit raised to the given exponent.
    /// </summary>
    /// <param name="n"> The exponent
    /// @return </param>
    public Unit Pow(int n) {
      if (n > 0) {
        return Mul(Pow(n - 1));
      } else if (n == 0) {
        return ONE;
      } else {
        return ONE.Div(Pow(-n));
      }
    }

    private Unit GetBaseUnits() {
      if (standardUnit is BaseUnit) {
        return standardUnit;
      }

      if (standardUnit is AlternateUnit) {
        return ((AlternateUnit)standardUnit).parent.GetBaseUnits();
      }

      if (standardUnit is ProductUnit) {
        ProductUnit productUnit = (ProductUnit)standardUnit;
        Unit baseUnits = ONE;

        for (int i = 0; i < productUnit.unitCount; i++) {
          Unit unit = productUnit.GetUnit(i).GetBaseUnits();
          unit = unit.Pow(productUnit.GetUnitPow(i));
          unit = unit.Root(productUnit.GetUnitRoot(i));
          baseUnits = baseUnits.Mul(unit);
        }

        return baseUnits;
      } else {
        throw new ArithmeticException("System cannot be an instance of " + this);
      }
    }

    private static UnitConverter TransformOf(Unit unit) {
      if (unit is BaseUnit) {
        return UnitConverter.IDENTITY;
      }

      ProductUnit productUnit = (ProductUnit)unit;
      UnitConverter ret = UnitConverter.IDENTITY;

      for (int i = 0; i < productUnit.unitCount; i++) {
        Unit u = productUnit.GetUnit(i);
        UnitConverter converter = TransformOf(u);

        if (!converter.isLinear) {
          throw new ArithmeticException("Cannot convert: " + unit + " is non-linear.");
        }

        if (productUnit.GetUnitRoot(i) != 1) {
          throw new ArithmeticException("Cannot convert: " + productUnit + " holds a base unit with a fractional exponent.");
        }

        int pow = productUnit.GetUnitPow(i);
        if (pow < 0) {
          pow = -pow;
          converter = converter.Inverse();
        }

        for (int j = 0; j < pow; j++) {
          ret = ret.Concatenate(converter);
        }
      }

      return ret;
    }
  }

  /// <summary>
  /// BaseUnit represents the building blocks on top which all other units are
  /// created. BaseUnit is the root of all other units and for all other units,
  /// a BaseUnit is the standard unit.
  /// </summary>
  public sealed class BaseUnit : Unit {
    /// <summary>
    /// The symbol that identifies this unit.
    /// </summary>
    public string symbol { get { return __symbol; } }
    private string __symbol;

    public override Unit standardUnit { get { return this; } }

    public BaseUnit(Quantity quantity, string symbol) : base(quantity) {
      __symbol = symbol;
    }

    public override UnitConverter ToStandardUnit() {
      return UnitConverter.IDENTITY;
    }

    public override int GetHashCode() {
      return __symbol.GetHashCode();
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is BaseUnit) {
        return __symbol.Equals(((BaseUnit)other).__symbol);
      } else {
        return false;
      }
    }
  }

  /// <summary>
  /// A Marker extension that marks a unit as having been derived
  /// from another.
  /// </summary>
  public abstract class DerivedUnit : Unit {
    protected DerivedUnit(Quantity quantity) : base(quantity) {
    }
  }

  public class CompoundUnit : DerivedUnit {
    public override Unit standardUnit { get { return lower.standardUnit; } }

    public Unit higher { get; private set; }
    public Unit lower { get; private set; }

    internal CompoundUnit(Unit high, Unit low) : base(high.quantity) {
      if (!high.standardUnit.Equals(low.standardUnit)) {
        throw new ArithmeticException("Cannot build unit: " + high + " and " + low + " must share a standard unit.");
      }

      higher = high;
      lower = low;
    }

    public override UnitConverter ToStandardUnit() {
      return lower.ToStandardUnit();
    }
    public override int GetHashCode() {
      return higher.GetHashCode() & lower.GetHashCode();
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is CompoundUnit) {
        CompoundUnit unit = (CompoundUnit)other;
        return higher.Equals(unit.higher) && lower.Equals(unit.lower);
      } else {
        return false;
      }
    }
  }

  /// <summary>
  /// AlternateUnit represents a type of unit that is the base unit for a non-standard
  /// unit type. For example, if you were to create the unit bits-per-second, you would
  /// create a new alternate unit encapsulating the unit bits / time.second.
  /// </summary>
  public sealed class AlternateUnit : DerivedUnit {
    public override Unit standardUnit { get { return this; } }

    public Unit parent { get; private set; }

    public string symbol { get; private set; }

    public AlternateUnit(Quantity quantity, Unit parent, string symbol) : base(quantity) {
      this.parent = parent;
      this.symbol = symbol;
    }

    public override UnitConverter ToStandardUnit() {
      return UnitConverter.IDENTITY;
    }

    public override int GetHashCode() {
      return symbol.GetHashCode();
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is AlternateUnit) {
        return ((AlternateUnit)other).symbol.Equals(symbol);
      } else {
        return false;
      }
    }
  }

  public class NamedUnit : DerivedUnit {
    public override Unit standardUnit { get { return parent.standardUnit; } }

    public Unit parent { get; private set; }

    public string symbol { get; private set; }

    public NamedUnit(Unit parent, string symbol) : base(parent.quantity) {
      this.parent = parent;
      this.symbol = symbol;
    }

    public override UnitConverter ToStandardUnit() {
      return parent.ToStandardUnit();
    }

    public override int GetHashCode() {
      return parent.GetHashCode() ^ symbol.GetHashCode();
    }

    public override bool Equals(Object other) {
      if (this == other) {
        return true;
      } else if (other is NamedUnit) {
        NamedUnit n = (NamedUnit)other;
        return parent.Equals(n.parent) && symbol.Equals(n.symbol);
      } else {
        return false;
      }
    }
  }

  public sealed class ProductUnit : DerivedUnit {
    public override Unit standardUnit {
      get {
        if (HasOnlyStandardUnit()) {
          return this;
        }

        Unit ret = Unit.ONE;

        for (int i = 0; i < elements.Length; i++) {
          Element e = elements[i];
          Unit u = e.unit.standardUnit;
          u = u.Pow(e.pow);
          u = u.Root(e.root);
          ret = ret.Mul(u);
        }

        return ret;
      }
    }

    /// <summary>
    /// Queries the number of units in this product unit.
    /// </summary>
    /// <value>The unit count.</value>
    public int unitCount { get { return elements.Length; } }

    // The elements that compose this product unit.
    private Element[] elements;
    // The hash code cache, so we don't keep recalculating the code.
    private int __hashCode;

    public ProductUnit() : base(Quantity.Dimensionless) {
      elements = new Element[0];
    }

    private ProductUnit(Quantity quantity, Element[] elements) : base(quantity) {
      this.elements = elements;
    }

    public ProductUnit(ProductUnit other) : base(other.quantity) {
      elements = other.elements;
    }

    /// <summary>
    /// Queries the unit at the given index.
    /// </summary>
    /// <returns>The unit.</returns>
    /// <param name="index">The index of the unit to fetch.</param>
    public Unit GetUnit(int index) {
      return elements[index].unit;
    }

    /// <summary>
    /// Queries the power of the unit at the given index.
    /// </summary>
    /// <returns>The unit pow.</returns>
    /// <param name="index">The index of the pow to fetch.</param>
    public int GetUnitPow(int index) {
      return elements[index].pow;
    }

    /// <summary>
    /// Queries the root of the units at the given index.
    /// </summary>
    /// <returns>The unit root.</returns>
    /// <param name="index">The index of the root to fetch.</param>
    public int GetUnitRoot(int index) {
      return elements[index].root;
    }

    public override UnitConverter ToStandardUnit() {
      UnitConverter ret = UnitConverter.IDENTITY;

      if (HasOnlyStandardUnit()) {
        return ret;
      }

      for (int i = 0; i < elements.Length; i++) {
        Element e = elements[i];
        UnitConverter uc = e.unit.ToStandardUnit();

        if (!uc.isLinear) {
          throw new ArithmeticException("Cannot convert: " + e.unit + " is not linear");
        }

        if (e.root != 1) {
          throw new ArithmeticException("Cannot convert: " + e.unit + " holds a base unit with fractional exponent");
        }

        int pow = e.pow;

        if (pow < 0) {
          pow = -pow;
          uc = uc.Inverse();
        }

        for (int j = 0; j < pow; j++) {
          ret = ret.Concatenate(uc);
        }
      }

      return ret;
    }



    /// <summary>
    /// Determines whether this instance has only standard units.
    /// </summary>
    /// <returns><c>true</c> if this instance has only standard unit; otherwise, <c>false</c>.</returns>
    private bool HasOnlyStandardUnit() {
      for (int i = 0; i < elements.Length; i++) {
        if (!elements[i].unit.IsStandardUnit()) {
          return false;
        }
      }
      return true;
    }

    public override int GetHashCode() {
      if (__hashCode != 0) {
        return __hashCode;
      }

      int code = 0;
      for (int i = 0; i < elements.Length; i++) {
        code += elements[i].unit.GetHashCode() * (elements[i].pow * 3 - elements[i].root * 2);
      }
      __hashCode = code;

      return __hashCode;
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is ProductUnit) {
        ProductUnit p = (ProductUnit)other;
        Element[] e = p.elements;
        if (elements.Length == e.Length) {
          for (int i = 0; i < elements.Length; i++) {
            if (!elements[i].Equals(p.elements[i])) {
              return false;
            }
          }
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }

    /// <summary>
    /// Returns the unit defiend from the product of the specified elements.
    /// </summary>
    /// <returns>The corresponding unit.</returns>
    /// <param name="quantity">The quantity that the unit will represent.</param>
    /// <param name="left">The left multiplicand elements.</param>
    /// <param name="right">The right multplicand elements.</param>
    private static Unit GetInstance(Quantity quantity, Element[] left, Element[] right) {
      int llen = left.Length;
      int rlen = right.Length;

      // Merge left and right elements.
      Element[] result = new Element[llen + rlen];
      int index = 0;
      for (int i = 0; i < llen; i++) {
        Unit unit = left[i].unit;
        int p1 = left[i].pow;
        int r1 = left[i].root;

        int p2 = 0;
        int r2 = 1;
        for (int j = 0; j < rlen; j++) {
          if (unit.Equals(right[j].unit)) {
            p2 = right[j].pow;
            r2 = right[j].root;
            break; // No duplicates
          }
        }
        int pow = p1 * r2 + p2 * r1;
        int root = r1 * r2;
        if (pow != 0) {
          int gcd = Gcd(Math.Abs(pow), root);
          result[index++] = new Element(unit, pow / gcd, root / gcd);
        }
      }

      // Append the remaining right elements not merged.
      for (int i = 0; i < rlen; i++) {
        Unit unit = right[i].unit;
        bool hasBeenMerged = false;
        for (int j = 0; j < llen; j++) {
          if (unit.Equals(left[j].unit)) {
            hasBeenMerged = true;
            break;
          }
        }

        if (!hasBeenMerged) {
          result[index++] = right[i];
        }
      }

      // Return the product instance
      if (index == 0) {
        return ONE;
      } else if ((index == 1) && (result[0].pow == result[0].root)) {
        return result[0].unit;
      } else {
        Element[] elms = new Element[index];
        Array.Copy(result, 0, elms, 0, index);
        return new ProductUnit(quantity, elms);
      }
    }

    /// <summary>
    /// Returns the product of the specified units.
    /// <para>
    /// Note: when this transform action is used, the resulting units will have the left unit's
    /// quantity. This is important to remember if the provided units do NOT share a common
    /// quantity.
    /// </para>
    /// </summary>
    /// <returns>The product instance.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    public static Unit GetProductInstance(Unit left, Unit right) {
      Element[] leftElements;
      if (left is ProductUnit) {
        leftElements = ((ProductUnit)left).elements;
      } else {
        leftElements = new Element[] { new Element(left, 1, 1) };
      }

      Element[] rightElements;
      if (right is ProductUnit) {
        rightElements = ((ProductUnit)right).elements;
      } else {
        rightElements = new Element[] { new Element(left, 1, 1) };
      }

      return GetInstance(left.quantity, leftElements, rightElements);
    }

    /// <summary>
    /// Returns the quotient of the specified units.
    /// <para>
    /// Note: when this transform action is used, the resulting units will have the left unit's
    /// quantity. This is important to remember if the provided units do NOT share a common
    /// quantity.
    /// </para>
    /// </summary>
    /// <returns>The product instance.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    public static Unit GetQuotientInstance(Unit left, Unit right) {
      Element[] leftElements;
      if (left is ProductUnit) {
        leftElements = ((ProductUnit)left).elements;
      } else {
        leftElements = new Element[] { new Element(left, 1, 1) };
      }

      Element[] rightElements;
      if (right is ProductUnit) {
        Element[] es = ((ProductUnit)right).elements;
        rightElements = new Element[es.Length];
        for (int i = 0; i < es.Length; i++) {
          Element e = es[i];
          rightElements[i] = new Element(e.unit, -e.pow, e.root);
        }
      } else {
        rightElements = new Element[] { new Element(left, -1, 1) };
      }

      return GetInstance(left.quantity, leftElements, rightElements);
    }

    /// <summary>
    /// Returns the product unit corresponding to the unit raised to the specified exponent.
    /// </summary>
    /// <returns>The pow instance.</returns>
    /// <param name="unit">Unit.</param>
    /// <param name="n">N.</param>
    public static Unit GetPowInstance(Unit unit, int n) {
      Element[] elms;
      if (unit is ProductUnit) {
        Element[] e = ((ProductUnit)unit).elements;
        elms = new Element[e.Length];
        for (int i = 0; i < e.Length; i++) {
          int gcd = Gcd(Math.Abs(e[i].pow * n), e[i].root);
          elms[i] = new Element(e[i].unit, e[i].pow * n / gcd, e[i].root / gcd);
        }
      } else {
        elms = new Element[] { new Element(unit, n, 1) };
      }
      return GetInstance(unit.quantity, elms, new Element[0]);
    }

    /// <summary>
    /// Returns the product unit corresponding to the specified unit to the gven root.
    /// </summary>
    /// <returns>The root instance.</returns>
    /// <param name="unit">Unit.</param>
    /// <param name="n">N.</param>
    public static Unit GetRootInstance(Unit unit, int n) {
      Element[] elms;
      if (unit is ProductUnit) {
        Element[] e = ((ProductUnit) unit).elements;
        elms = new Element[e.Length];
        for (int i = 0; i < e.Length; i++) {
          int gcd = Gcd(Math.Abs(e[i].pow), e[i].root * n);
          elms[i] = new Element(e[i].unit, e[i].pow / gcd, e[i].root * n / gcd);
        }
      } else {
        elms = new Element[] { new Element(unit, 1, n) };
      }
      return GetInstance(unit.quantity, elms, new Element[0]);
    }

    /// <summary>
    /// Finds the greatest common denominator of m and n.
    /// <para>
    /// Example Usage:
    /// <code>
    /// long m = 12;
    /// long n = 3;
    /// System.out.println("gcd(" + m + ", " + n + ") => " + MathUtils.gcd(m, n));
    /// >>> OUT
    /// gcd(12, 3) => 3
    /// </code>
    /// </para> </summary>
    /// <param name="m"> </param>
    /// <param name="n"> </param>
    /// <returns> The greatest common denominator or m if it is. </returns>
    public static int Gcd(int m, int n) {
      if (n == 0) {
        return m;
      } else {
        return Gcd(n, m % n);
      }
    }

    internal struct Element {
      public Unit unit { get; private set; }
      public int pow { get; private set; }
      public int root { get; private set; }

       internal Element(Unit unit, int pow, int root) {
        this.unit = unit;
        this.pow = pow;
        this.root = root;
      }

      public override bool Equals(object other) {
        if (other is Element) {
          Element e = (Element)other;
          return unit.Equals(e.unit) && pow == e.pow && root == e.root;
        } else {
          return false;
        }
      }

      public override int GetHashCode() {
        return unit.GetHashCode() * (pow * 3 - root * 2);
      }
    }
  }

  public class TransformedUnit : DerivedUnit {
    public override Unit standardUnit { get { return parent.standardUnit; } }
    public Unit parent { get; private set; }
    public UnitConverter toParent { get; private set; }

    internal TransformedUnit(Quantity quantity, Unit parentUnit, UnitConverter converter) : base(quantity) {
      this.parent = parentUnit;
      this.toParent = converter;
    }

    public override UnitConverter ToStandardUnit() {
      return parent.ToStandardUnit().Concatenate(toParent);
    }

    public override int GetHashCode() {
      return parent.GetHashCode() ^ toParent.GetHashCode();
    }

    public override bool Equals(object other) {
      if (this == other) {
        return true;
      } else if (other is TransformedUnit) {
        TransformedUnit tu = (TransformedUnit)other;
        return parent.Equals(tu.parent) && toParent.Equals(tu.toParent);
      } else {
        return false;
      }
    }
  }
}
