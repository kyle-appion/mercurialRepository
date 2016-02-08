using System;
using System.Collections.Generic;

namespace ION.Core.Devices.Certificates {
  /// <summary>
  /// A certificate that provides validation of an AV760's calibration.
  /// </summary>
  public class GaugeDeviceCalibrationCertificate : ICalibrationCertificate {


    private const string KEY_CONTROL = "CalibrationCStandarddevice";
    private const string KEY_CONTROL_SERIAL = "serialNumber";
    private const string KEY_CONTROL_INSTRUMENT = "intrumentModel";
    private const string KEY_CONTROL_TRANSDUCER  = "transducerModel";
    private const string KEY_CONTROL_CALIBRATION_DATE = "lastCalibrationDate";
    private const string KEY_CONTROL_ACCURACY = "accuracyOfStandard";

    private const string KEY_TEST_SERIAL = "serialNumber";
    private const string KEY_TEST_PART = "partNumber";
    private const string KEY_TEST_TRANSDUCER = "transducerModel";
    private const string KEY_TEST_TEMPERATURE = "ambientTemperature";
    private const string KEY_TEST_HUMIDITY = "abientRelativeHumidity";
    private const string KEY_TEST_ACCURACY = "accuracyLimit";

    private const string KEY_TEST_PERFORMANCE_DATA = "performanceData";
    private const string KEY_TEST_CALIBRATION_DATE = "lastCalibrationDate";
    private const string KEY_TEST_CERTIFIED_BY = "certifiedBy";

    public const string KEY_TABLE_STANDARD = "calibrationStandard";
    public const string KEY_TABLE_ACTUAL = "appionGauge";
    public const string KEY_TABLE_MIN = "minReading";
    public const string KEY_TABLE_MAX = "maxReading";
    public const string KEY_TABLE_UNIT = "calibrationUnit";

    /// <summary>
    /// The control device's serial number. 
    /// </summary>
    /// <value>The control serial.</value>
    public string controlSerial { get; set; }
    /// <summary>
    /// The control device's instrument that what used in the calibration.
    /// </summary>
    /// <value>The control instrument.</value>
    public string controlInstrument { get; set; }
    /// <summary>
    /// The control device's transducer (
    /// </summary>
    /// <value>The control transducer.</value>
    public string controlTransducer { get; set; }

    /// <summary>
    /// The accuracy of the control's measurement as a per cent.
    /// </summary>
    /// <value>The control accuracy.</value>
    public double controlAccuracy { get; set; }

    /// <summary>
    /// The serial number of the test device.
    /// </summary>
    /// <value>The test serial number.</value>
    public GaugeSerialNumber testSerialNumber { get; set; }
    /// <summary>
    /// The part number for the GaugeDevice.
    /// </summary>
    /// <value>The test part number.</value>
    public string testPartNumber { get; set; }
    /// <summary>
    /// The transducer for the GaugeDevice.
    /// </summary>
    /// <value>The test transducer.</value>
    public string testTransducer { get; set; }
    /// <summary>
    /// The accuracy of the GaugeDevice as a per cent.
    /// </summary>
    /// <value>The test accuracy.</value>
    public double testAccuracy { get; set; }

    /// <summary>
    /// The temperature of the test environment in celsius.
    /// </summary>
    /// <value>The environment temperature.</value>
    public double environmentTemperature { get; set; }
    /// <summary>
    /// The humidity of the test environment.
    /// </summary>
    /// <value>The environment humidity.</value>
    public double environmentHumidity { get; set; }

    /// <summary>
    /// Who certified the test device.
    /// </summary>
    /// <value>The certified by.</value>
    public string certifiedBy { get; set; }

    /// <summary>
    /// The date that the control was last calibrated.
    /// </summary>
    /// <value>The last control calibration date.</value>
    public DateTime lastControlCalibrationDate { get; set; }
    /// <summary>
    /// The date that the test was last calibrated.
    /// </summary>
    /// <value>The last test calibration date.</value>
    public DateTime lastTestCalibrationDate { get; set; }

    /// <summary>
    /// The calibration data points.
    /// </summary>
    public Dictionary<string, string[]> calibrationDataPoints = new Dictionary<string, string[]>();
  }
}

