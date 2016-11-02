namespace Playground {

	using System;

	/// <summary>
	/// A simple structure that will wrap the magnitudes of a unit. 
	/// </summary>
	public struct Unit<T> where T : Quantity {
		/// <summary>
		/// The order the unit has with repsect to the ampere unit.
		/// </summary>
		public byte ampere { get; internal set; }
		/// <summary>
		/// The order that the units has with respect to the kelvin unit.
		/// </summary>
		public byte kelvin { get; internal set; }
		/// <summary>
		/// The order the unit has with respect to the kilogram unit.
		/// </summary>
		public byte kilogram { get; internal set; }
		/// <summary>
		/// The order the unit has with respect to the meter unit.
		/// </summary>
		public byte meter { get; internal set; }
		/// <summary>
		/// The order the unit has with respect to the radian unit.
		/// </summary>
		public byte radian { get; internal set; }
		/// <summary>
		/// The order the unit has with respect to the second unit.
		/// </summary>
		public byte second { get; internal set; }
		/// <summary>
		/// Whether or not the unit is relative (marked as not having a high or low bound).
		/// </summary>
		/// <value><c>true</c> if relative; otherwise, <c>false</c>.</value>
		public bool absolute { get; internal set; }

		public Unit(byte ampere=0, byte kelvin=0, byte kilogram=0, byte meter=0, byte radian=0, byte second=0, bool absolute=false) {
			this.ampere = ampere;
			this.kelvin = kelvin;
			this.kilogram = kilogram;
			this.meter = meter;
			this.radian = radian;
			this.second = second;
			this.absolute = absolute;
		}

		public Scalar<T> OfScalar(double magnitude) {
			return new Scalar<T>(magnitude, this);
		}

		/// <summary>
		/// Creates a unit that is the result of multiplying two units.
		/// </summary>
		/// <param name="first">The <see cref="Playground.Unit<T>"/> to multiply.</param>
		/// <param name="second">The <see cref="Playground.Unit<T>"/> to multiply.</param>
		/// <returns>The <see cref="T:Playground.Unit`1"/> that is the <c>first</c> * <c>second</c>.</returns>
		public static Unit<T> operator* (Unit<T> first, Unit<T> second) {
			return new Unit<T>() {
				ampere = (byte)(first.ampere + second.ampere),
				kelvin = (byte)(first.kelvin + second.kelvin),
				kilogram = (byte)(first.kilogram + second.kilogram),
				meter = (byte)(first.meter + second.meter),
				radian = (byte)(first.radian + second.radian),
				second = (byte)(first.second + second.second),
				absolute = first.absolute & second.absolute,
			};
		}

		/// <summary>
		/// Creates a unit that is the result of dividing two units.
		/// </summary>
		/// <param name="first">The <see cref="Playground.Unit<T>"/> to divide (the divident).</param>
		/// <param name="second">The <see cref="Playground.Unit<T>"/> to divide (the divisor).</param>
		/// <returns>The <see cref="T:Playground.Unit`1"/> that is the <c>first</c> / <c>second</c>.</returns>
		public static Unit<T> operator/ (Unit<T> first, Unit<T> second) {
			return new Unit<T>() {
				ampere = (byte)(first.ampere - second.ampere),
				kelvin = (byte)(first.kelvin - second.kelvin),
				kilogram = (byte)(first.kilogram - second.kilogram),
				meter = (byte)(first.meter - second.meter),
				radian = (byte)(first.radian - second.radian),
				second = (byte)(first.second - second.second),
				absolute = first.absolute & second.absolute,
			};
		}
	}

	public struct Scalar<T> where T : Quantity {
		public double magnitude;
		public Unit<T> unit;

		public Scalar(double magnitude, Unit<T> unit) {
			this.magnitude = magnitude;
			this.unit = unit;
		}
	}

	public struct ScalarSpan<T> where T : Quantity {
		public double magnitude { get; private set; }
		public Unit<T> unit { get; private set; }

		public ScalarSpan(double magnitude, Unit<T> unit) {
			this.magnitude = magnitude;
			this.unit = unit;
		}
	}

	/// <summary>
	/// The class that represents a known list of dimensions. All units must have a quantity associated with it.
	/// </summary>
	public abstract class Quantity {
		/// <summary>
		/// The rate of change of velocity with respect to time. The primitive unit for
		/// this quantity is "m/s²" meter per square second.
		/// </summary>
		public class Acceleration : Quantity {}
		/// <summary>
		/// The figure formed by two lines diverging from a common point. The primitive
		/// unit for this quantity is "rad" radian. This quantity is dimensionless.
		/// </summary>
		public class Angle : Quantity {}
		/// <summary>
		/// The extent of a planar region or of the surface of a solid measured in
		/// square units. The primitive for this quantity is "m²" square meter.
		/// </summary>
		public class Area : Quantity {}
		/// <summary>
		/// The measurement of an amount of binary data. The primitive unit for this
		/// quantity is "bit". This quantity is dimensionless.
		/// </summary>
		public class BinaryDataAmount : Quantity {}
		/// <summary>
		/// The measurement of an amount of binary data being translocated with respect
		/// to time. The primitive for this quantity is "bit/s" bit per second.
		/// </summary>
		public class BinaryDataRate : Quantity {}
		/// <summary>
		/// The measurement of a dimensionless quantity.
		/// </summary>
		public class Dimensionless : Quantity {}
		/// <summary>
		/// The measurement of electric capacitance. The primitive unit for this
		/// quantity is "F" Farad.
		/// </summary>
		public class ElectricCapacitance : Quantity {}
		/// <summary>
		/// The measurement of electric charge. The primitive unit for this quantity is
		/// "C" Coulomb.
		/// </summary>
		public class ElectricCharge : Quantity {}
		/// <summary>
		/// The measurement of electric conductance. The primitive unit for this quantity
		/// is "S" Siemens.
		/// </summary>
		public class ElectricConductance : Quantity {}
		/// <summary>
		/// The measurement of electric charge. The primitive unit for this quantity is
		/// "A" Ampere.
		/// </summary>
		public class ElectricCurrent : Quantity {}
		/// <summary>
		/// The measurement of electric inductance. The primitive unit for this quantity
		/// is "H" Henry.
		/// </summary>
		public class ElectricInductance : Quantity {}
		/// <summary>
		/// The measurement of electric potential. The primitive unit for this quantity
		///  is "V" Volt.
		/// </summary>
		public class ElectricPotential : Quantity {}
		/// <summary>
		/// The measurement of electric resistance. The primitive unit for this quantity
		/// is "Ω" Ohm.
		/// </summary>
		public class ElectricResistance : Quantity {}
		/// <summary>
		/// The measurement of the capacity of a physical system to do work. The
		/// primitive unit for this quantity is "J" Joule.
		/// </summary>
		public class Energy : Quantity {}
		/// <summary>
		/// The measurement of a quantity that tends to produce an acceleration of a
		/// body in the direction of its application. The primitive unit for this
		/// quantity is "N" Newton.
		/// </summary>
		public class Force : Quantity {}
		/// <summary>
		/// The measurement of the number of times a specified phenomenon occurs within
		/// a specified interval. The primitive unit for this quantity is "Hz" Hertz.
		/// </summary>
		public class Frequency : Quantity {}
		/// <summary>
		/// The measurement of the relative air saturation. The primitive unit for
		/// humidity is "RH" relative humidity.
		/// </summary>
		public class Humidity : Quantity {}
		/// <summary>
		/// The measurement of the extent of something along its greatest dimension of
		/// the extent of space between two points. The primitive for this quantity is
		/// "m" meter.
		/// </summary>
		public class Length : Quantity {}
		/// <summary>
		/// The measurement of the quantity of matter that a body or object contains.
		/// The mass of the body is not dependent on gravity and therefore is different
		/// from, but proportional to, weight. The primitive unit for this quantity is
		/// "kg" kilogram.
		/// </summary>
		public class Mass : Quantity {}
		/// <summary>
		/// The measurement of the movement of mass per time unit. The primitive unit
		/// for this quantity is "kg/s" kilogram per second.
		/// </summary>
		public class MassFlowRate : Quantity {}
		/// <summary>
		/// The measurement of the rate at which work is done. The primitive unit for
		/// this quantity is "W" Watt.
		/// </summary>
		public class Power : Quantity {}
		/// <summary>
		/// The measurement of a force applies uniformly over a surface. The primitive
		/// unit for this quantity is "Pa" Pascal.
		/// <p>
		///   Note: vacuum is still a pressure. It is lesser pressure exerted upon a
		///   surface that deviates from the pressure applied to the opposing face of
		///   the same surface.
		/// </p>
		/// </summary>
		public class Pressure : Quantity {}
		/// <summary>
		/// The measurement of the degree of hotness or coldness of a body or
		/// environment. The primitive unit for this quantity is "K" Kelvin.
		/// </summary>
		public class Temperature : Quantity {}
		/// <summary>
		/// The measurement of time. The primitive for this quantity is "s" or second.
		/// </summary>
		public class Time : Quantity {}
		/// <summary>
		/// The measurement of the moment of a force. The primitive unit for this
		/// quantity is "N·m" Newton-meter.
		/// <p>
		///   Note: the Newton-meter is also a way for expressing a Joule (unit of
		///   energy). However, torque is not energy. So, to avoid confusion, we will
		///   use the units "N·m" for torque and not Joule. This distinction occurs due
		///   to the scalar nature of energy and the vector nature of torque.
		/// </p>
		/// </summary>
		public class Torque : Quantity {}
		public class Vacuum : Quantity {}
		/// <summary>
		/// The measurement of the amount of distance traveled divided by the time of
		/// travel. The primitive unit for this quantity is "m/s" meter per second.
		/// </summary>
		public class Velocity : Quantity {}
		/// <summary>
		/// The measurement of the amount of space occupied by a three-dimensional
		/// object or region of space, expressed in cubic units. The primitive unit for
		/// this quantity is "m³" cubic meters.
		/// </summary>
		public class Volume : Quantity {}
		/// <summary>
		/// The measurement of a mass per unit of volume of a substance under specified
		/// conditions of pressure and temperature. The primitive unit for this quantity
		/// is "kg/m³" kilogram per cubic meter.
		/// </summary>
		public class VolumetricDensity : Quantity {}
		/// <summary>
		/// The measurement of the volume of fluid passing a point in a system per unit
		/// of time. The primitive unit for this quantity is "m³/s" cubic meter per
		/// second.
		/// </summary>
		public class VolumetricFlowRate : Quantity {}
	}
}
