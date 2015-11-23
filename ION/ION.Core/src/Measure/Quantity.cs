using System;

namespace ION.Core.Measure {

  /// <summary>
  /// The enumeration of the types of measurements that the ION application is currently,
  /// of expected to calculate. This enumeration allows for the interoperability and ease
  /// of unit conversion in a moderately safe fashion.
  /// </summary>
  public enum Quantity {
    /// <summary>
    /// The rate of change of velocity with respect to time. The primitive unit for
    /// this quantity is "m/s²" meter per square second.
    /// </summary>
    Acceleration,
    /// <summary>
    /// The figure formed by two lines diverging from a common point. The primitive
    /// unit for this quantity is "rad" radian. This quantity is dimensionless.
    /// </summary>
    Angle,
    /// <summary>
    /// The extent of a planar region or of the surface of a solid measured in
    /// square units. The primitive for this quantity is "m²" square meter.
    /// </summary>
    Area,
    /// <summary>
    /// The measurement of an amount of binary data. The primitive unit for this
    /// quantity is "bit". This quantity is dimensionless.
    /// </summary>
    BinaryDataAmount,
    /// <summary>
    /// The measurement of an amount of binary data being translocated with respect
    /// to time. The primitive for this quantity is "bit/s" bit per second.
    /// </summary>
    BinaryDataRate,
    /// <summary>
    /// The measurement of a dimensionless quantity.
    /// </summary>
    Dimensionless,
    /// <summary>
    /// The measurement of electric capacitance. The primitive unit for this
    /// quantity is "F" Farad.
    /// </summary>
    ElectricCapacitance,
    /// <summary>
    /// The measurement of electric charge. The primitive unit for this quantity is
    /// "C" Coulomb.
    /// </summary>
    ElectricCharge,
    /// <summary>
    /// The measurement of electric conductance. The primitive unit for this quantity
    /// is "S" Siemens.
    /// </summary>
    ElectricConductance,
    /// <summary>
    /// The measurement of electric charge. The primitive unit for this quantity is
    /// "A" Ampere.
    /// </summary>
    ElectricCurrent,
    /// <summary>
    /// The measurement of electric inductance. The primitive unit for this quantity
    /// is "H" Henry.
    /// </summary>
    ElectricInductance,
    /// <summary>
    /// The measurement of electric potential. The primitive unit for this quantity
    ///  is "V" Volt.
    /// </summary>
    ElectricPotential,
    /// <summary>
    /// The measurement of electric resistance. The primitive unit for this quantity
    /// is "Ω" Ohm.
    /// </summary>
    ElectricResistance,
    /// <summary>
    /// The measurement of the capacity of a physical system to do work. The
    /// primitive unit for this quantity is "J" Joule.
    /// </summary>
    Energy,
    /// <summary>
    /// The measurement of a quantity that tends to produce an acceleration of a
    /// body in the direction of its application. The primitive unit for this
    /// quantity is "N" Newton.
    /// </summary>
    Force,
    /// <summary>
    /// The measurement of the number of times a specified phenomenon occurs within
    /// a specified interval. The primitive unit for this quantity is "Hz" Hertz.
    /// </summary>
    Frequency,
    /// <summary>
    /// The measurement of the relative air saturation. The primitive unit for
    /// humidity is "RH" relative humidity.
    /// </summary>
    Humidity,
    /// <summary>
    /// The measurement of the extent of something along its greatest dimension of
    /// the extent of space between two points. The primitive for this quantity is
    /// "m" meter.
    /// </summary>
    Length,
    /// <summary>
    /// The measurement of the quantity of matter that a body or object contains.
    /// The mass of the body is not dependent on gravity and therefore is different
    /// from, but proportional to, weight. The primitive unit for this quantity is
    /// "kg" kilogram.
    /// </summary>
    Mass,
    /// <summary>
    /// The measurement of the movement of mass per time unit. The primitive unit
    /// for this quantity is "kg/s" kilogram per second.
    /// </summary>
    MassFlowRate,
    /// <summary>
    /// The measurement of the rate at which work is done. The primitive unit for
    /// this quantity is "W" Watt.
    /// </summary>
    Power,
    /// <summary>
    /// The measurement of a force applies uniformly over a surface. The primitive
    /// unit for this quantity is "Pa" Pascal.
    /// <p>
    ///   Note: vacuum is still a pressure. It is lesser pressure exerted upon a
    ///   surface that deviates from the pressure applied to the opposing face of
    ///   the same surface.
    /// </p>
    /// </summary>
    Pressure,
    /// <summary>
    /// The measurement of the degree of hotness or coldness of a body or
    /// environment. The primitive unit for this quantity is "K" Kelvin.
    /// </summary>
    Temperature,
    /// <summary>
    /// The measurement of time. The primitive for this quantity is "s" or second.
    /// </summary>
    Time,
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
    Torque,
    /// <summary>
    /// The measurement of the amount of distance traveled divided by the time of
    /// travel. The primitive unit for this quantity is "m/s" meter per second.
    /// </summary>
    Velocity,
    /// <summary>
    /// The measurement of the amount of space occupied by a three-dimensional
    /// object or region of space, expressed in cubic units. The primitive unit for
    /// this quantity is "m³" cubic meters.
    /// </summary>
    Volume,
    /// <summary>
    /// The measurement of a mass per unit of volume of a substance under specified
    /// conditions of pressure and temperature. The primitive unit for this quantity
    /// is "kg/m³" kilogram per cubic meter.
    /// </summary>
    VolumetricDensity,
    /// <summary>
    /// The measurement of the volume of fluid passing a point in a system per unit
    /// of time. The primitive unit for this quantity is "m³/s" cubic meter per
    /// second.
    /// </summary>
    VolumetricFlowRate
  }
}

