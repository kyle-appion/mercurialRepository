using System;
namespace ION.Core.Measure {

  using Measure;

  /// <summary>
  /// A ScalarSpan is a magnitude that is not bound to the range of a unit. This is simply a difference between two
  /// relative scalars.
  /// </summary>
  public struct ScalarSpan {
    /// <summary>
    /// The unit that the scalar is referring to.
    /// </summary>
    /// <value>The unit.</value>
    public Unit unit { get; private set; }
    /// <summary>
    /// The magnitude of the scalar.
    /// </summary>
    /// <value>The value of the magnitude.</value>
    public double magnitude { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Measurement.ScalarSpan"/> struct with the given unit and magnitude.
    /// </summary>
    /// <param name="unit">Unit.</param>
    /// <param name="magnitude">Magnitude.</param>
    public ScalarSpan(Unit unit, double magnitude) {
      this.unit = unit;
      this.magnitude = magnitude;
    }

		public override string ToString() {
			return magnitude.ToString("0.##") + " " + unit.ToString();
		}

    /// <summary>
    /// Converts the ScalarSpan to the given unit.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="unit">Unit.</param>
    public ScalarSpan ConvertTo(Unit unit) {
      if (this.unit.Equals(unit)) {
        return this;
      }
      var converter = this.unit.GetConverterTo(unit);
      return new ScalarSpan(unit, converter.Derivative().Convert(magnitude));
    }

		public bool IsCompatable(Unit otherUnit) {
			return unit.IsCompatible(otherUnit);
		}

		private void ThrowIfNotCompatable(Unit otherUnit) {
			if (!IsCompatable(otherUnit)) {
				throw new ArgumentException(unit + " is not compatable with " + otherUnit);
			}
		}

		public ScalarSpan Abs() {
			return new ScalarSpan(unit, System.Math.Abs(magnitude));
		}

		public static ScalarSpan operator +(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span + addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static ScalarSpan operator +(ScalarSpan span, double addend) {
			return new ScalarSpan(span.unit, span.magnitude + addend);
		}

		public static ScalarSpan operator -(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span - addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static ScalarSpan operator -(ScalarSpan span, double subtrahend) {
			return new ScalarSpan(span.unit, span.magnitude - subtrahend);
		}

		public static ScalarSpan operator *(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span * addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static ScalarSpan operator *(ScalarSpan span, double multiplier) {
			return new ScalarSpan(span.unit, span.magnitude * multiplier);
		}

		public static ScalarSpan operator /(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span / addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static ScalarSpan operator /(ScalarSpan span, double divisor) {
			return new ScalarSpan(span.unit, span.magnitude / divisor);
		}

		public static bool operator >(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span > addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator >(ScalarSpan o1, double o2) {
			return o1.magnitude > o2;
		}

		public static bool operator >=(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span >= addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator >=(ScalarSpan o1, double o2) {
			return o1.magnitude >= o2;
		}

		public static bool operator <(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span < addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator <(ScalarSpan o1, double o2) {
			return o1.magnitude < o2;
		}

		public static bool operator <=(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span <= addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator <=(ScalarSpan o1, double o2) {
			return o1.magnitude <= o2;
		}

		public static bool operator !=(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span != addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator !=(ScalarSpan o1, double o2) {
			return !(o1 == o2);
		}

		public static bool operator ==(ScalarSpan span, ScalarSpan addend) {
			span.ThrowIfNotCompatable(addend.unit);
			return span == addend.unit.GetConverterTo(span.unit).Convert(addend.magnitude);
		}

		public static bool operator ==(ScalarSpan o1, double o2) {
			return o1.magnitude == o2;
		}
  }
}

