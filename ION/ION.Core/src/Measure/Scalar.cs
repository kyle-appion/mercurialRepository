namespace ION.Core.Measure {

  using System;

  /// <summary>
  /// A Scalar is a one dimensional value with a unit.
  /// </summary>
  public struct Scalar : IComparable<Scalar> {
    /// <summary>
    /// The unit that the scalar is referring to.
    /// </summary>
    /// <value>The unit.</value>
    public Unit unit { get; private set; }
    /// <summary>
    /// The magnitude of the scalar.
    /// </summary>
    /// <value>The value of the magnitude.</value>
		public double amount { get; private set; }

    /// <summary>
    /// Creates a new Scalar.
    /// </summary>
    /// <param name="unit">Unit.</param>
    /// <param name="magnitude">Magnitude.</param>
    public Scalar(Unit unit, double magnitude) {
      this.unit = unit;
      this.amount = magnitude;
    }

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Scalar"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current <see cref="Scalar"/>.</returns>
		public override string ToString() {
			return amount.ToString("0.##") + " " + unit;
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Scalar"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such
		/// as a hash table.</returns>
		public override int GetHashCode() {
			int h = ((Double)amount).GetHashCode();
			return h ^ unit.GetHashCode();
		}

		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="Scalar"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Scalar"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is equal to the current
		/// <see cref="Scalar"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj) {
			if (obj != null) {
				var s = (Scalar)obj;
				AssertCompatible(unit, s.unit);
				return DEquals(amount, s.ConvertTo(unit).amount);
			} else {
				return false;
			}
		}

		// Implemented From IComparable
		public int CompareTo(Scalar other) {
			if (!CompatibleWith(other.unit.quantity)) {
				throw new Exception("Scalar with unit: " + unit + " is not compatible with quantity " + other.unit + "[" + other.unit.quantity + "]");
			}

			return amount.CompareTo(other.ConvertTo(unit).amount);
		}

		/// <summary>
		/// Performs a epsilon appromiximation equality.
		/// </summary>
		/// <returns>The quals.</returns>
		/// <param name="value">Value.</param>
		/// <param name="epsilon">Epsilon.</param>
		public bool DEquals(double value, double epsilon = 0.005) {
			return Math.Abs(amount - value) < epsilon;
		}

    /// <summary>
    /// Converts this scalar to another unit.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="unit">Unit.</param>
    public Scalar ConvertTo(Unit unit) {
      return new Scalar(unit, this.unit.GetConverterTo(unit).Convert(amount));
    }

		/// <summary>
		/// Queries whether or not the scalar is compatible with the given dimension.
		/// </summary>
		/// <returns><c>true</c>, if with was compatibled, <c>false</c> otherwise.</returns>
		/// <param name="quantity">Quantity.</param>
		public bool CompatibleWith(Quantity quantity) {
			return unit.quantity == quantity;
		}

		/// <summary>
		/// Asserts that the two units are compatible with eachother.
		/// </summary>
		/// <returns>The compatible.</returns>
		/// <param name="o1">The first unit.</param>
		/// <param name="o2">The second unit.</param>
		private static void AssertCompatible(Unit o1, Unit o2) {
			if (!o1.IsCompatible(o2)) {
				throw new ArithmeticException("Cannot perform operation: " + o1 + " is incompatible with " + o2);
			}
		}

    public static Scalar operator +(Scalar scalar, ScalarSpan span) {
			AssertCompatible(scalar.unit, span.unit);
      return new Scalar(scalar.unit, scalar.amount + span.ConvertTo(scalar.unit).magnitude);
    }

    public static Scalar operator -(Scalar scalar, ScalarSpan span) {
			AssertCompatible(scalar.unit, span.unit);
      return new Scalar(scalar.unit, scalar.amount - span.ConvertTo(scalar.unit).magnitude);
    }

    public static Scalar operator *(Scalar scalar, ScalarSpan span) {
			AssertCompatible(scalar.unit, span.unit);
      return new Scalar(scalar.unit, scalar.amount * span.ConvertTo(scalar.unit).magnitude);
    }

    public static Scalar operator /(Scalar scalar, ScalarSpan span) {
			AssertCompatible(scalar.unit, span.unit);
      return new Scalar(scalar.unit, scalar.amount / span.ConvertTo(scalar.unit).magnitude);
    }

		public static ScalarSpan operator +(Scalar left, Scalar right) {
			AssertCompatible(left.unit, right.unit);
			var tmp = right.ConvertTo(left.unit);			
			return tmp.unit.OfSpan(left.amount + tmp.amount);
		}

		public static ScalarSpan operator -(Scalar left, Scalar right) {
			AssertCompatible(left.unit, right.unit);
			var tmp = right.ConvertTo(left.unit);			
			return tmp.unit.OfSpan(left.amount - tmp.amount);
		}

		public static ScalarSpan operator *(Scalar left, Scalar right) {
			AssertCompatible(left.unit, right.unit);
			var tmp = right.ConvertTo(left.unit);			
			return tmp.unit.OfSpan(left.amount *tmp.amount);
		}

		public static ScalarSpan operator /(Scalar left, Scalar right) {
			AssertCompatible(left.unit, right.unit);
			var tmp = right.ConvertTo(left.unit);			
			return tmp.unit.OfSpan(left.amount / tmp.amount);
		}

		public static bool operator >(Scalar o1, Scalar o2) {
			AssertCompatible(o1.unit, o2.unit);
			return o1.amount > o2.ConvertTo(o1.unit).amount;
		}

		public static bool operator >(Scalar o1, double o2) {
			return o1.amount > o2;
		}

		public static bool operator >=(Scalar o1, Scalar o2) {
			AssertCompatible(o1.unit, o2.unit);
			return o1.amount >= o2.ConvertTo(o1.unit).amount;
		}

		public static bool operator >=(Scalar o1, double o2) {
			return o1.amount >= o2;
		}

		public static bool operator <(Scalar o1, Scalar o2) {
			AssertCompatible(o1.unit, o2.unit);
			return o1.amount < o2.ConvertTo(o1.unit).amount;
		}

		public static bool operator <(Scalar o1, double o2) {
			return o1.amount < o2;
		}

		public static bool operator <=(Scalar o1, Scalar o2) {
			AssertCompatible(o1.unit, o2.unit);
			return o1.amount <= o2.ConvertTo(o1.unit).amount;
		}

		public static bool operator <=(Scalar o1, double o2) {
			return o1.amount <= o2;
		}


		public static bool operator !=(Scalar o1, Scalar o2) {
			return !(o1 == o2);
		}

		public static bool operator ==(Scalar o1, Scalar o2) {
			return (!object.ReferenceEquals(o1, null) && !object.ReferenceEquals(o2, null)) && (o1.amount == o2.amount && o1.unit.Equals(o2.unit));
		}
  }
}

