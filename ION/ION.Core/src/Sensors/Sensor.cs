using System;

using ION.Core.Measure;
using ION.Core.Util;

namespace ION.Core.Sensors {

  /// <summary>
  /// Enumerates the possible sensors.
  /// </summary>
  public enum ESensorType {
    Length,
    Humidity,
    Mass,
    Pressure,
    Temperature,
    Vacuum,
    Unknown,
  }

  /// <summary>
  /// Some utility extensions around the sensor type enum.
  /// </summary>
  public static class SensorTypeExtensions {
    /// <summary>
    /// Queries the default unit for the given sensor type.
    /// </summary>
    /// <returns>The default unit.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public static Unit GetDefaultUnit(this ESensorType sensorType) {
      switch (sensorType) {
        /*
        case ESensorType.Humidity: {
          return DEFAULT_HUMIDITY_UNITS[0];
        }
        case ESensorType.Length: {
          return DEFAULT_LENGTH_UNITS[0];
        }
        case ESensorType.Mass: {
          return DEFAULT_MASS_UNITS[0];
        }
*/
        case ESensorType.Pressure: {
            return SensorUtils.DEFAULT_PRESSURE_UNITS[0];
          }
        case ESensorType.Temperature:{
            return SensorUtils.DEFAULT_TEMPERATURE_UNITS[0];
          }
        case ESensorType.Vacuum: {
            return SensorUtils.DEFAULT_VACUUM_UNITS[0];
          }
        default: {
            throw new ArgumentException("Cannot get default unit for " + sensorType);
          }
      }
    }
  }

  /// <summary>
  /// A simple utility class that will provide functions that aid in the use of the
  /// ESensorType in conjunction with an ISensor.
  /// </summary>
  public partial class SensorUtils {
    /// <summary>
    /// The default pressure units to use for sensors that do not provide their own
    /// unit list.
    /// </summary>
    public static Unit[] DEFAULT_PRESSURE_UNITS = new Unit[] {
      Units.Pressure.PSIG,
      Units.Pressure.BAR,
      Units.Pressure.IN_HG,
      Units.Pressure.CM_HG,
      Units.Pressure.KG_CM,
      Units.Pressure.KILOPASCAL,
      Units.Pressure.MEGAPASCAL,
    };

    /// <summary>
    /// The default temperature units to use for sensors that do not provide their
    /// own unit list.
    /// </summary>
    public static Unit[] DEFAULT_TEMPERATURE_UNITS = new Unit[] {
      Units.Temperature.FAHRENHEIT,
      Units.Temperature.CELSIUS,
      Units.Temperature.KELVIN,
    };

    /// <summary>
    /// The default vacuum units to use for senors that do not provide their own
    /// unit list.
    /// </summary>
    public static Unit[] DEFAULT_VACUUM_UNITS = new Unit[] {
      Units.Pressure.MICRON,
      Units.Pressure.IN_HG,
      Units.Pressure.MILLITORR,
      Units.Pressure.PSIA,
      Units.Pressure.KILOPASCAL,
    };

    /// <summary>
    /// Determines whether or not the given unit is valid with the provided sensor type.
    /// </summary>
    /// <param name="sensorType"></param>
    /// <param name="unit"></param>
    /// <returns>True if the unit is valid for the sensor type, false otherwise.</returns>
    public static bool IsCompatibleWith(ESensorType sensorType, Unit unit) {
      switch (sensorType) {
        case ESensorType.Length: {
          return unit.IsCompatible(Units.Length.METER);
        }
        case ESensorType.Humidity: {
          return unit.IsCompatible(Units.Humidity.RELATIVE_HUMIDITY);
        }
        case ESensorType.Mass: {
          return unit.IsCompatible(Units.Mass.KILOGRAM);
        }
        case ESensorType.Pressure: {
          return unit.IsCompatible(Units.Pressure.PASCAL);
        }
        case ESensorType.Temperature: {
          return unit.IsCompatible(Units.Temperature.KELVIN);
        }
        case ESensorType.Vacuum: {
          return unit.IsCompatible(Units.Pressure.MICRON);
        }
        default: {
          return false;
        }
      }
    }
  }

  /// <summary>
  /// A Sensor is a device that is used to measure physical phenomena and/or their effect
  /// on physical reality.
  /// <para>
  /// This class will be used to organize and represent all that is needed to proxy the
  /// measurement of this phenomena.
  /// </para>
  /// </summary>
  public class Sensor {
    /// <summary>
    /// The delegate that will be notified when the sensor's state
    /// is changed. Note: the measurement change can be cause by either/or
    /// a change to the unit or magnitude.
    /// </summary>
    public delegate void OnSensorStateChanged(Sensor sensor);

    /// <summary>
    /// The event that will be notified when the sensor changes.
    /// </summary>
    public event OnSensorStateChanged onSensorStateChangedEvent;

    /// <summary>
    /// The type of sensor this is. From this, the base conversion unit is
    /// derived as well as sensor uses.
    /// </summary>
    public ESensorType sensorType { get; private set; }
    /// <summary>
    /// Whether or not the sensor's reading is relative.
    /// </summary>
    public bool isRelative { get; private set; }
    /// <summary>
    /// The custom name for the specific sensor. 
    /// </summary>
    public string name {
      get {
        return __name;
      }
      set {
        __name = value;
        NotifySensorStateChanged();
      }
    } string __name;
    /// <summary>
    /// The unit that the sensor's measurement is quantitated in.
    /// </summary>
    public Unit unit {
      get {
        return measurement.unit;
      }
      set {
        measurement = measurement.ConvertTo(value);
        NotifySensorStateChanged();
      }
    }
    /// <summary>
    /// The measurement of the sensor.
    /// </summary>
    public Scalar measurement {
      get {
        return __measurement;
      }
      set {
        if (!value.unit.IsCompatible(unit)) {
          throw new ArgumentException("Cannot set measurement: " + value.unit + " is not compatible with " + unit);
        }
        __measurement = value;
        NotifySensorStateChanged();
      }
    } Scalar __measurement;
    /// <summary>
    /// The maxumimum measurement that the sensor can accurately measure.
    /// </summary>
    public Scalar maxMeasurement { get; set; }
    /// <summary>
    /// The minumum measurement that the sensor can acurrately measure.
    /// </summary>
    public Scalar minMeasurement { get; set; }
    /// <summary>
    /// Whether or not the sensor is overloaded. The reading cannot be regarded
    /// as reliable if the measurement is overloaded.
    /// </summary>
    public bool isOverloaded {
      get {
        return maxMeasurement != null && measurement >= maxMeasurement;
      }
    }

    /// <summary>
    /// Creates a new sensor.
    /// </summary>
    /// <param name="sensorType">Sensor type.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Sensor(ESensorType sensorType, bool isRelative=true)
      : this(sensorType, sensorType.GetDefaultUnit().OfScalar(0), isRelative) { 
    }
    /// <summary>
    /// Creates a new sensor.
    /// </summary>
    /// <param name="sensorType">Sensor type.</param>
    /// <param name="initialMeasurement">Initial measurement.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    public Sensor(ESensorType sensorType, Scalar initialMeasurement, bool isRelative=true) {
      this.sensorType = sensorType;
      this.__measurement = initialMeasurement;
      this.isRelative = isRelative;
    }

    /// <summary>
    /// Notifies the sensors event that the sensor state changed.
    /// </summary>
    public void NotifySensorStateChanged() {
      if (onSensorStateChangedEvent != null) {
        onSensorStateChangedEvent(this);
      }
    }
  }
}

