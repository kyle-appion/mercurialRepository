using System;

namespace ION.Core.Devices {
  /// <summary>
  /// Enumerates the types of gauges that are supported by the application.
  /// </summary>
  public enum EGaugeType {
    P300,
    P500,
    P800,
    AV760,
    _3XTM, // I kind of hate how Greg names some things.
    HT,
  }

  /// <summary>
  /// A simple utility class that will provide functions that aid in the use of the
  /// EGaugeType enumeration.
  /// </summary>
  public class GaugeTypeUtils {
    /// <summary>
    /// The code that is indicative of a P300 pressure gauge.
    /// </summary>
    public static string P300_CODE = "P3";

    /// <summary> 
    /// The code that is indicative of a P500 pressure gauge.
    /// </summary>
    public static string P500_CODE = "P5";

    /// <summary> 
    /// The code that is indicative of a P800 pressure gauge.
    /// </summary>
    public static string P800_CODE = "P8";

    /// <summary> 
    /// The code that is indicative of a AV760 vacuum gauge.
    /// </summary>
    public static string AV760_CODE = "V7";

    /// <summary>
    /// Given a string representitive of a gauge type, we will parse it into a valid
    /// EGaugeType enumeration. If the given gauge type is not valid, we will throw
    /// an ArgumentException.
    /// </summary>
    /// <returns>The string.</returns>
    /// <param name="gaugeType">Gauge type.</param>
    public static EGaugeType FromString(string gaugeType) {
      if (gaugeType == null) {
        throw new ArgumentException("Cannot parse GaugeType: string is null");
      }

      if (gaugeType.Length != 2) {
        throw new ArgumentException("Cannot parse GaugeType: expected gauge type of length 2, received length " + gaugeType.Length);
      }

      if (P300_CODE.Equals(gaugeType)) {
        return EGaugeType.P300;
      } else if (P500_CODE.Equals(gaugeType)) {
        return EGaugeType.P500;
      } else if (P800_CODE.Equals(gaugeType)) {
        return EGaugeType.P800;
      } else if (AV760_CODE.Equals(gaugeType)) {
        return EGaugeType.AV760;
      } else {
        throw new ArgumentException("Cannot parse GaugeType: unknown type " + gaugeType);
      }
    }
  }
}

