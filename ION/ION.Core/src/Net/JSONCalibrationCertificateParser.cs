
using System;

namspace ION.Core.Net {
  public class JSONCalibrationCertificateParser {
    private const string KEY_CONTROL = "CalibrationCStandarddevice";
    private const string KEY_CONTROL_SERIAL = "serialNumber";
    private const string KEY_CONTROL_INTSTRUMENT = "intrumentModel";
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

    private const string KEY_ERROR_CODE = "errorCode";

    
  }
}
