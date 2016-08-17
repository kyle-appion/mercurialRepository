namespace ION.Core.Sensors {

  using System;

  using Newtonsoft.Json;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Measure;
  using ION.Core.Sensors.Serialization;
  using ION.Core.Util;

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
  public static class SensorExtensions {
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
        case ESensorType.Pressure:
          return SensorUtils.DEFAULT_PRESSURE_UNITS[0];
        case ESensorType.Temperature:
          return SensorUtils.DEFAULT_TEMPERATURE_UNITS[0];
        case ESensorType.Vacuum:
          return SensorUtils.DEFAULT_VACUUM_UNITS[0];
        default:
          throw new ArgumentException("Cannot get default unit for " + sensorType);
      }
    }

    /// <summary>
    /// Builds a formatted string of this sensor's measurement.
    /// </summary>
    /// <remarks>
    /// The string will NOT include the sensor's unit.
    /// </remarks>
    /// <returns>The formatted string.</returns>
    /// <param name="sensor">Sensor.</param>
    public static string ToFormattedString(this Sensor sensor, bool includeUnit = false) {
      return SensorUtils.ToFormattedString(sensor.type, sensor.measurement, includeUnit);
    }
  } // End SensorExtensions

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
//      Units.Pressure.PSIA,
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
      Units.Vacuum.MICRON,
      Units.Vacuum.IN_HG,
      Units.Vacuum.MILLITORR,
      Units.Vacuum.PSIA,
      Units.Vacuum.KILOPASCAL,
    };

    public static ESensorType FromString(string sensorType) {
      var ret = ESensorType.Unknown;
      Enum.TryParse(sensorType, out ret);
      return ret;
    }

    public static string ToFormattedString(ESensorType sensorType, Scalar measurement, bool includeUnit = false) {
      var unit = measurement.unit;
      var amount = measurement.amount;

      string ret = "";

      if (double.IsNaN(amount)) {
        ret = "---";
      } else {
        // PRESSURE UNITS
        if (Units.Pressure.PASCAL.Equals(unit)) {
          ret = amount.ToString("0");
        } else if (Units.Pressure.KILOPASCAL.Equals(unit)) {
          if (ESensorType.Vacuum == sensorType) {
            ret = amount.ToString("0.0000");
          } else {
            ret = amount.ToString("0");
          }
        } else if (Units.Pressure.MEGAPASCAL.Equals(unit)) {
          ret = amount.ToString("0.000");
        } else if (Units.Pressure.MILLIBAR.Equals(unit)) {
          ret = amount.ToString("0.000");
        } else if (Units.Pressure.PSIG.Equals(unit)) {
          ret = amount.ToString("0.0");
        } else if (Units.Pressure.PSIA.Equals(unit)) {
          ret = amount.ToString("0.0000");
        } else if (Units.Pressure.IN_HG.Equals(unit)) {
          if (ESensorType.Vacuum == sensorType) {
            ret = amount.ToString("0.000");
          } else {
            ret = amount.ToString("0.00");
          }
        }
      // VACUUM PRESSURE
      else if (Units.Vacuum.MICRON.Equals(unit)) {
          ret = amount.ToString("###,##0");
        } else if (Units.Vacuum.MILLITORR.Equals(unit)) {
          ret = amount.ToString("###,##0");
        }
      // DEFAULT
      else {
          ret = amount.ToString("0.00");
        }
      }

      if (includeUnit) {
        ret += " " + unit.ToString();
      }

      return ret;
    }

		public static string ToFormattedString(ESensorType sensorType, ScalarSpan measurement, bool includeUnit = false) {
			var unit = measurement.unit;
			var amount = measurement.magnitude;

			string ret = "";

			if (double.IsNaN(amount)) {
				ret = "---";
			} else {
				// PRESSURE UNITS
				if (Units.Pressure.PASCAL.Equals(unit)) {
					ret = amount.ToString("0");
				} else if (Units.Pressure.KILOPASCAL.Equals(unit)) {
					if (ESensorType.Vacuum == sensorType) {
						ret = amount.ToString("0.0000");
					} else {
						ret = amount.ToString("0");
					}
				} else if (Units.Pressure.MEGAPASCAL.Equals(unit)) {
					ret = amount.ToString("0.000");
				} else if (Units.Pressure.MILLIBAR.Equals(unit)) {
					ret = amount.ToString("0.000");
				} else if (Units.Pressure.PSIG.Equals(unit)) {
					ret = amount.ToString("0.0");
				} else if (Units.Pressure.PSIA.Equals(unit)) {
					ret = amount.ToString("0.0000");
				} else if (Units.Pressure.IN_HG.Equals(unit)) {
					if (ESensorType.Vacuum == sensorType) {
						ret = amount.ToString("0.000");
					} else {
						ret = amount.ToString("0.00");
					}
				}
				// VACUUM PRESSURE
				else if (Units.Vacuum.MICRON.Equals(unit)) {
					ret = amount.ToString("###,##0");
				} else if (Units.Vacuum.MILLITORR.Equals(unit)) {
					ret = amount.ToString("###,##0");
				}
				// DEFAULT
				else {
					ret = amount.ToString("0.00");
				}
			}

			if (includeUnit) {
				ret += " " + unit.ToString();
			}

			return ret;
		}

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
          return unit.IsCompatible(Units.Vacuum.MICRON);
        }
        default: {
          return false;
        }
      }
    }
  } // End SensorUtils

  /// <summary>
  /// A Sensor is a device that is used to measure physical phenomena and/or their effect
  /// on physical reality.
  /// <para>
  /// This class will be used to organize and represent all that is needed to proxy the
  /// measurement of this phenomena.
  /// </para>
  /// </summary>
  [JsonConverter(typeof(DefaultSensorJsonConverter))]
  public abstract class Sensor {
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
    public ESensorType type { get; private set; }
    /// <summary>
    /// Whether or not the sensor's reading is relative.
    /// </summary>
    public bool isRelative { get; private set; }
    /// <summary>
    /// Whether or not te sensor's reading is editable.
    /// </summary>
    /// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
    public abstract bool isEditable { get; }
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
        if (!isEditable) {
          throw new UnauthorizedAccessException("Cannot set sensor unit: sensor is not editable.");
        }
        ForceSetUnit(value);
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
        if (!isEditable) {
          throw new UnauthorizedAccessException("Cannot set sensor measurement: sensor is not editable.");
        }
        ForceSetMeasurement(value);
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
    /// The array of units that the sensor will support for unit changes.
    /// </summary>
    /// <value>The supported units.</value>
    public virtual Unit[] supportedUnits {
      get {
        if (__supportedUnits == null) {
          switch (type) {
            case ESensorType.Pressure:
              return SensorUtils.DEFAULT_PRESSURE_UNITS;
            case ESensorType.Temperature:
              return SensorUtils.DEFAULT_TEMPERATURE_UNITS;
            case ESensorType.Vacuum:
              return SensorUtils.DEFAULT_VACUUM_UNITS;
            default:
              return new Unit[] { };
          }
        } else {
          return __supportedUnits;
        }
      }
      set {
        foreach (var u in value) {
          if (!unit.IsCompatible(u)) {
            throw new Exception("Cannot set units: quantity mismatch. Expected: " + unit.quantity + " received: " + u.quantity);
          }
        }

        __supportedUnits = value;
      }
    } Unit[] __supportedUnits;
    
    /// <summary>
    /// Gets or sets the index of a sensor's location in the analyzer.
    /// </summary>
    /// <value>The analyzer slot.</value>
    public int analyzerSlot { get; set; }

    /// <summary>
    /// Creates a new sensor.
    /// </summary>
    /// <param name="sensorType">Sensor type.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    protected Sensor(ESensorType sensorType, bool isRelative=true)
      : this(sensorType, ION.Core.App.AppState.context.defaultUnits.DefaultUnitFor(sensorType).OfScalar(0), isRelative) {
    }
    /// <summary>
    /// Creates a new sensor.
    /// </summary>
    /// <param name="sensorType">Sensor type.</param>
    /// <param name="initialMeasurement">Initial measurement.</param>
    /// <param name="isRelative">If set to <c>true</c> is relative.</param>
    protected Sensor(ESensorType sensorType, Scalar initialMeasurement, bool isRelative=true) {
      this.type = sensorType;
      this.__measurement = initialMeasurement;
      this.isRelative = isRelative;
    }

    /// <summary>
    /// Notifies the sensors event that the sensor state changed.
    /// </summary>
    public void NotifySensorStateChanged() {
      // TODO ahodder@appioninc.com: This post and posts like it need to disappear.
      var ion = AppState.context;
      if (ion != null) {
        ion.PostToMain(() => {
          if (onSensorStateChangedEvent != null) {            
            onSensorStateChangedEvent(this);
          }
        });
      }
    }

    /// <summary>
    /// Formats this sensor's measurement into a user friendly string.
    /// </summary>
    /// <returns>The formatted string.</returns>
    /// <param name="includeUnit">If set to <c>true</c> include unit.</param>
    public string ToFormattedString(bool includeUnit) {
      return SensorUtils.ToFormattedString(type, measurement, includeUnit);
    }

    /// <summary>
    /// Forces the measurement of the sensor to be set to the given value.
    /// </summary>
    /// <remarks>
    /// This operation will throw an exception if the scalar is not
    /// compatible with the sensor's unit.
    /// </remarks>
    /// <param name="value">Value.</param>
    protected void ForceSetMeasurement(Scalar value) {
      if (!value.unit.IsCompatible(unit)) {
        throw new ArgumentException("Cannot set measurement: " + value.unit + " is not compatible with " + unit);
      }
      __measurement = value;
      NotifySensorStateChanged();
    }

    /// <summary>
    /// Forces the unit of the sensor to be set.
    /// </summary>
    /// <remarks>
    /// This operation will throw an exception if the unit is not compatible
    /// with the sensor's previous unit.
    /// </remarks>
    /// <param name="unit">Unit.</param>
    protected void ForceSetUnit(Unit unit) {
      if (this.unit.Equals(unit)) {
        return;
      }

      if (!this.unit.IsCompatible(unit)) {
        throw new ArgumentException("Cannot set unit: " + unit + " is not compatible with " + this.unit);
      }
      if (this.unit.Equals(unit)) {
        return;
      }
      __measurement = __measurement.ConvertTo(unit);
      NotifySensorStateChanged();
    }
  }
}
