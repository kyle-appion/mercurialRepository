/// <summary>
/// NOTE:
/// 
/// THIS CLASS IS DEDICATED TO PROVIDED IOS EXLCUSIVE EXTENSIONS TO THE SENSOR UTIL
/// CLASS IN THE CORE LIBRARY. AS SUCH, LEAVE THE FUCKING NAMESPACE ALONE!
/// </summary>
namespace ION.IOS.Sensors {
  
  using System;

  using ION.Core.Sensors;

  using ION.IOS.Util;

  public static class SensorExtensions {
    /// <summary>
    /// Queries the localized user friendly string representing a sensor type.
    /// </summary>
    /// <param name="sensorType"></param>
    /// <returns></returns>
    public static string GetTypeString(this ESensorType sensorType) {
      switch (sensorType) {
        case ESensorType.Length: {
          return Strings.Sensor.Type.LENGTH.FromResources();
        }
        case ESensorType.Humidity: {
          return Strings.Sensor.Type.HUMIDITY.FromResources();
        }
        case ESensorType.Mass: {
          return Strings.Sensor.Type.MASS.FromResources();
        }
        case ESensorType.Pressure: {
          return Strings.Sensor.Type.PRESSURE.FromResources();
        }
        case ESensorType.Temperature: {
          return Strings.Sensor.Type.TEMPERATURE.FromResources();
        }
        case ESensorType.Vacuum: {
          return Strings.Sensor.Type.VACUUM.FromResources();
        }
        default: {
          return Strings.Sensor.Type.UNKNOWN.FromResources();
        }
      }
    }
  }

  public partial class SensorUtils {
  }
}

