using System;

using ION.Core.Measure;

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
  /// A simple utility class that will provide functions that aid in the use of the
  /// ESensorType in conjunction with an ISensor.
  /// </summary>
  public class SensorUtils {
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
    /// The delegate that is fired when the sensor's reading is changed.
    /// </summary>
    /// <param name="sensor"></param>
    /// <param name="reading"></param>
    public delegate void OnReadingChanged(Sensor sensor, Scalar reading);
    /// <summary>
    /// The event pool that fires events when the sensor's reading changes.
    /// </summary>
    public event OnReadingChanged readingChanged;
    // TODO ahodder@appioninc.com: Throw an exception if the value reading does not match the sensor type.
    /// <summary>
    /// Queries the current reading of the sensor provider.
    /// </summary>
    /// <value>The reading.</value>
    public Scalar reading { get {
      return reading;
      } protected set {
        reading = value;
        lastUpdate = DateTime.Now;
        PerformOnReadingChanged();
      }
    }
    /// <summary>
    /// Queries whether or not the sensor's readings are relative. Relative readings are
    /// readings that are offset from a known and mutable source. Absolute readings are
    /// readings that regardless of other parameters is exactly known.
    /// </summary>
    public bool isRelative { get; private set; }
    /// <summary>
    /// A convenience property to get or set the sensor's unit. Note: if the unit is not
    /// compatible with the sensor, an ArgumentException will be thrown.
    /// </summary>
    public Unit unit { get { return reading.unit; } set {
        unit = value;
        reading = reading.ConvertTo(unit);
      }
    }
    /// <summary>
    /// Queries the type of sensor that this object is representing.
    /// </summary>
    public ESensorType sensorType { get; private set; }
    /// <summary>
    /// Queries the last time that this sensor had is reading changed. Note: for sensors
    /// that are newly created, this will be the sensor's creation time.
    /// </summary>
    /// <value>The last update.</value>
    public DateTime lastUpdate { get; protected set; }


    public Sensor(ESensorType sensorType, bool relative, Scalar initialReading) {
      // TODO ahodder@appioninc.com: Assert reading to sensor type.
      this.sensorType = sensorType;
      reading = initialReading;
      isRelative = relative;
    }

    /// <summary>
    /// Posts an event to the readingChanged event.
    /// </summary>
    private void PerformOnReadingChanged() {
      if (readingChanged != null) {
        readingChanged(this, reading);
      }
    }
  }
}

