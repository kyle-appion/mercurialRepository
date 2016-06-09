using System;

namespace Measurement {
  public sealed class ProductUnit : DerivedUnit {
    /// <summary>
    /// A dimensionless unit of value one. Essentially, this is the unit identity.
    /// </summary>
    public static readonly ProductUnit ONE = new ProductUnit(new Element[0]);


    public override Dimension dimension {
      get {
        var ret = Dimension.NONE;

        for (int i = 0; i < elements.Length; i++) {
          var unit = elements[i].unit;
          var dimension = unit.dimension.Pow(elements[i].pow).Root(elements[i].root);
          ret = ret.Mul(dimension);
        }

        return ret;
      }
    }
    /// <summary>
    /// Returns the BaseUnit, AlternateUnit or ProductUnit (which contains base or alternate units) that this unit is
    /// derived from.
    /// </summary>
    /// <value>The standard unit.</value>
    public override Unit standardUnit {
      get {
        return __standardUnit;
      }
    } Unit __standardUnit;
    /// <summary>
    /// Returns the converter that will converter from this unit to the this unit's system (base) unit.
    /// </summary>
    /// <value>The standard unit.</value>
    public override UnitConverter toStandardUnit { 
      get {
        return __toStandardUnit;
      }
    } UnitConverter __toStandardUnit;

    /// <summary>
    /// Queries the number of units that are present in the product unit.
    /// </summary>
    /// <value>The unit count.</value>
    public int unitCount {
      get {
        return elements.Length;
      }
    }

    /// <summary>
    /// The sub elements that compse the product unit.
    /// </summary>
    private Element[] elements;
    /// <summary>
    /// The cached hash code for the product unit. The is retained so we don't need to keep recalculating the unit's hash.
    /// </summary>
    private int cachedHashCode;

    private ProductUnit(Element[] elements) {
      this.elements = elements;
      Init();
    }

    public ProductUnit(ProductUnit productUnit) {
      elements = productUnit.elements;
      Init();
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Measurement.ProductUnit"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      if (cachedHashCode != 0) {
        return cachedHashCode;
      } else {
        var ret = 0;

        foreach (var e in elements) {
          ret += e.unit.GetHashCode() * (e.pow * 3 - e.root * 2);
        }

        return cachedHashCode = ret;
      }
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.ProductUnit"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.ProductUnit"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="Measurement.ProductUnit"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      if (this == obj) {
        return true;
      } else if (obj is ProductUnit) {
        var pu = obj as ProductUnit;
        if (elements.Length == pu.elements.Length) {
          // A product unit may equal another product unit, even if the elements (units) are out of order.
          for (int i = 0; i < elements.Length; i++) {
            var found = false;
            for (int j = 0; j < pu.elements.Length; j++) {
              if (elements[i].unit.Equals(pu.elements[j].unit)) {
                found = true;
                break;
              }
            }
            if (!found) {
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
    /// Initializes the power units caches.
    /// </summary>
    private void Init() {
      __standardUnit = FindStandardUnit();
      __toStandardUnit = BuildToStandardUnitConverter();
    }

    /// <summary>
    /// Queries whether or not the product unit contains only standard units.
    /// </summary>
    /// <returns><c>true</c> if this instance has only standard unit; otherwise, <c>false</c>.</returns>
    private bool HasOnlyStandardUnit() {
      foreach (var e in elements) {
        if (!e.unit.isStandardUnit) {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Finds the standard unit for the product unit.
    /// </summary>
    /// <returns>The standard unit.</returns>
    private Unit FindStandardUnit() {
      if (HasOnlyStandardUnit()) {
        return this;
      } else {
        Unit ret = ONE;

        foreach (var e in elements) {
          var u = e.unit;
          u = u.Pow(e.pow);
          u = u.Root(e.root);
          ret = ret.Multiply(u);
        }

        return ret;
      }
    }

    /// <summary>
    /// Builds the product unit's unit converter.
    /// </summary>
    /// <returns>The unit converter.</returns>
    private UnitConverter BuildToStandardUnitConverter() {
      if (HasOnlyStandardUnit()) {
        return UnitConverter.IDENTITY;
      }

      var converter = UnitConverter.IDENTITY;

      foreach (var e in elements) {
        var uc = e.unit.toStandardUnit;
        if (!uc.isLinear) {
          throw new ArithmeticException("Cannot build unit converter: " + e.unit + " is not linear");
        }

        if (e.root != 1) {
          throw new ArithmeticException("Cannot build unit converter: " + e.unit + " has a fractional exponent");
        }

        var pow = e.pow;
        if (e.pow < 0) {
          pow = -pow;
          uc = uc.Inverse();
        }

        for (int j = 0; j < pow; j++) {
          converter = converter.Concatenate(uc);
        }
      }

      return converter;
    }

    /// <summary>
    /// Returns the product unit as defined by the left and right elements.
    /// </summary>
    /// <returns>The instance.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    private static Unit GetInstance(Element[] left, Element[] right) {
      // Merge left and right elements.
      Element[] result = new Element[left.Length + right.Length];
      int resultIndex = 0;
      for (int i = 0; i < left.Length; i++) {
        Unit unit = left[i].unit;
        int p1 = left[i].pow;
        int r1 = left[i].root;

        int p2 = 0;
        int r2 = 1;
        for (int j = 0; j < right.Length; j++) {
          if (unit.Equals(right[j].unit)) {
            p2 = right[j].pow;
            r2 = right[j].root;
            break; // No duplicates
          }
        }
        int pow = p1 * r2 + p2 * r1;
        int root = r1 * r2;
        if (pow != 0) {
          int gcd = Gcd(System.Math.Abs(pow), root);
          result[resultIndex++] = new Element(unit, pow / gcd, root / gcd);
        }
      }

      // Append the remaining right elements not merged.
      for (int i = 0; i < right.Length; i++) {
        Unit unit = right[i].unit;
        bool hasBeenMerged = false;
        for (int j = 0; j < left.Length; j++) {
          if (unit.Equals(left[j].unit)) {
            hasBeenMerged = true;
            break;
          }
        }

        if (!hasBeenMerged) {
          result[resultIndex++] = right[i];
        }
      }

      // Return the product instance
      if (resultIndex == 0) {
        return ONE;
      } else if ((resultIndex == 1) && (result[0].pow == result[0].root)) {
        return result[0].unit;
      } else {
        Element[] elms = new Element[resultIndex];
        Array.Copy(result, 0, elms, 0, resultIndex);
        return new ProductUnit(elms);
      }
    }

    /// <summary>
    /// Returns the product of the specified units.
    /// </summary>
    /// <returns>The product instance.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    internal static Unit GetProductInstance(Unit left, Unit right) {
      Element[] leftElements, rightElements;

      if (left is ProductUnit) {
        leftElements = ((ProductUnit)left).elements;
      } else {
        leftElements = new Element[] { new Element(left, 1, 1) };
      }

      if (right is ProductUnit) {
        rightElements = ((ProductUnit)right).elements;
      } else {
        rightElements = new Element[] { new Element(right, 1, 1) };
      }

      return GetInstance(leftElements, rightElements);
    }

    /// <summary>
    /// Returns the quotient of the specified units.
    /// </summary>
    /// <returns>The quotient instance.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    internal static Unit GetQuotientInstance(Unit left, Unit right) {
      Element[] leftElements, rightElements;

      if (left is ProductUnit) {
        leftElements = ((ProductUnit)left).elements;
      } else {
        leftElements = new Element[] { new Element(left, 1, 1) };
      }

      if (right is ProductUnit) {
        var elements = ((ProductUnit)right).elements;
        rightElements = new Element[elements.Length];
        for (int i = 0; i < elements.Length; i++) {
          rightElements[i] = new Element(elements[i].unit, -elements[i].pow, elements[i].pow);
        }
      } else {
        rightElements = new Element[] { new Element(right, -1, 1) };
      }

      return GetInstance(leftElements, rightElements);
    }

    /// <summary>
    /// Returns the product unit corresponding to the specified unit to the gven root.
    /// </summary>
    /// <returns>The root instance.</returns>
    /// <param name="unit">Unit.</param>
    /// <param name="n">N.</param>
    public static Unit GetRootInstance(Unit unit, int n) {
      Element[] elements;
      if (unit is ProductUnit) {
        Element[] es = ((ProductUnit) unit).elements;
        elements = new Element[es.Length];
        for (int i = 0; i < es.Length; i++) {
          int gcd = Gcd(System.Math.Abs(es[i].pow), es[i].root * n);
          elements[i] = new Element(es[i].unit, es[i].pow / gcd, es[i].root * n / gcd);
        }
      } else {
        elements = new Element[] { new Element(unit, 1, n) };
      }
      return GetInstance(elements, new Element[0]);
    }

    /// <summary>
    /// Returns the product unit corresponding to the unit raised to the specified exponent.
    /// </summary>
    /// <returns>The pow instance.</returns>
    /// <param name="unit">Unit.</param>
    /// <param name="n">N.</param>
    internal static Unit GetPowInstance(Unit unit, int n) {
      Element[] elements;

      if (unit is ProductUnit) {
        var es = ((ProductUnit)unit).elements;
        elements = new Element[es.Length];
        for (int i = 0; i < elements.Length; i++) {
          int gcd = Gcd(Math.Abs(es[i].pow * n), es[i].root);
          elements[i] = new Element(es[i].unit, es[i].pow / gcd, es[i].root / gcd);
        }
      } else {
        elements = new Element[] { new Element(unit, n, 1) };
      }

      return GetInstance(elements, new Element[0]);
    }

    /// <summary>
    /// Performs a Euclidean GCD operation to find the greatest common divisor for m and n.
    /// </summary>
    /// <param name="m">M.</param>
    /// <param name="n">N.</param>
    private static int Gcd(int m, int n) {
      while (n != 0) {
        var tm = m;
        m = n;
        n = tm % n;
      }

      return m;
    }

    /// <summary>
    /// An inner product element that represents a rational power of a single unit.
    /// </summary>
    private class Element {
      /// <summary>
      /// The unit whose converter is affected by the element's power.
      /// </summary>
      public Unit unit { get; private set; }
      /// <summary>
      /// The power applied to the element.
      /// </summary>
      public  int pow { get; private set; }
      /// <summary>
      /// The root applies to the element.
      /// </summary>
      public  int root { get; private set; }

      internal Element(Unit unit, int pow, int root) {
        this.unit = unit;
        this.pow = pow;
        this.root = root;
      }

      /// <summary>
      /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Measurement.ProductUnit+Element"/>.
      /// </summary>
      /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Measurement.ProductUnit+Element"/>.</param>
      /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
      /// <see cref="Measurement.ProductUnit+Element"/>; otherwise, <c>false</c>.</returns>
      public override bool Equals(object obj) {
        var e = obj as Element;
        return e != null && unit.Equals(e.unit) && pow == e.pow && root == e.root;
      }
    }
  }
}

