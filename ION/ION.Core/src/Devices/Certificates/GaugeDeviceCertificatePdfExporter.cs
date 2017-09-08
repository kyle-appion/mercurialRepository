namespace ION.Core.Devices.Certificates {
  
  using System;
  using System.Collections.Generic;
  using System.IO;

  using Xfinium.Pdf;
  using Xfinium.Pdf.Graphics;
  using Xfinium.Pdf.Forms;

  using ION.Core.App;
  using ION.Core.IO;

  /// <summary>
  /// Exports an AV760Certificate to a pdf.
  /// </summary>
  public class GaugeDeviceCertificatePdfExporter {
    private const string
    /** The field name for the person who certified the test unit. */
    FIELD_CERT_BY = "certifiedby",
    /** The field name for the date the test unit was certified. */
    FIELD_CERT_DATE = "certifieddate",
    /** The field name for the part number of the tested unit. */
    FIELD_PART_NO = "dev_partno",
    /** The field name for the serial number of the tested unit. */
    FIELD_SERIAL_NO = "dev_serialno",
    /** The field name for the control unit's model name. */
    FIELD_CAL_MODEL = "cal_model",
    /** The field name for the control unit's serial number. */
    FIELD_CAL_SERIAL_NO = "cal_serialno",
    /** The field name for the control unit's accuracy range. */
    FIELD_CAL_ACCURACY = "cal_accuracy",
    /** The field name for the control unit's last calibrated date. */
    FIELD_CAL_DATE = "cal_caldate",
    /** The field name for the ambient conditions during the tested unit's certification. */
    FIELD_AMB_TEMP = "ambient_temp",
    /** The field name for the ambient relative/humidity during the tested unit's certification. */
    FIELD_AMB_RH = "ambient_rh",
    /** The field name for the accuracy limit that was applied to the device's certification. */
    FIELD_CAL_LIMIT = "error_band";

    /*
     * The following fields are used as a template to find a table of fields.
     * Append an arbitrary iterative number (table row) to the template to get the
     * field name.
     */
    private const string
    /** The field name for the calibration standard (target number). */
    FIELD_CAL_PT_I = "cal_pt",
    /** The field name for the unit of the calibration standard. */
    FIELD_CAL_PT_UNIT_I = "unit",
    /** The field name for the calibrated Appion gauge */
    FIELD_CAL_APPION_DEVICE = "dev_pt",
    /** The field name for the minimum allowed reading of the gauge. */
    FIELD_CAL_LOW = "min_pt",
    /** The field name for the maximum allowed reading of the gauge. */
    FIELD_CAL_HIGH = "max_pt";

    /*
    {\n\"calibrationStandard\" : [0, 50, 100, 200, 400, 800]," +
    "\n\"calibrationUnit\" : [\"psig\", \"psig\", \"psig\", \"psig\", \"psig\", \"psig\"]," +
    "\n\"appionGauge\" : [0.02, 50.1, 99.8, 200.2, 400, 801]," +
    "\n\"maxReading\" : [0.03, 50.125, 100.25, 200.5, 401, 802]," +
    "\n\"minReading\" : [-0.03, 49.875, 99.75, 199.5, 399, 798]\n}"
    */
    /** The performance data keys that we require. */
    private static readonly Pair[] TABLE_KEYS = new Pair[] {
      new Pair(FIELD_CAL_PT_I, GaugeDeviceCalibrationCertificate.KEY_TABLE_STANDARD),
      new Pair(FIELD_CAL_APPION_DEVICE, GaugeDeviceCalibrationCertificate.KEY_TABLE_ACTUAL),
      new Pair(FIELD_CAL_LOW, GaugeDeviceCalibrationCertificate.KEY_TABLE_MIN),
      new Pair(FIELD_CAL_HIGH, GaugeDeviceCalibrationCertificate.KEY_TABLE_MAX),
      new Pair(FIELD_CAL_PT_UNIT_I, GaugeDeviceCalibrationCertificate.KEY_TABLE_UNIT),
    };

    public static void Export(IION ion, GaugeDeviceCalibrationCertificate certificate, Stream outStream) {
      var pdf = new PdfFixedDocument(EmbeddedResource.Load("digital_gauge_cert.pdf"));
      // Header
      (pdf.Form.Fields[FIELD_CERT_BY] as PdfTextBoxField).Value = certificate.certifiedBy;
      (pdf.Form.Fields[FIELD_CERT_DATE] as PdfTextBoxField).Value = certificate.lastTestCalibrationDate.ToString("d");
      // Unit Tested
      (pdf.Form.Fields[FIELD_PART_NO] as PdfTextBoxField).Text = certificate.testPartNumber;
      (pdf.Form.Fields[FIELD_SERIAL_NO] as PdfTextBoxField).Text = certificate.testSerialNumber + "";
      // Calibration Standard
      (pdf.Form.Fields[FIELD_CAL_MODEL] as PdfTextBoxField).Text = certificate.controlInstrument;
      (pdf.Form.Fields[FIELD_CAL_SERIAL_NO] as PdfTextBoxField).Text = certificate.controlSerial;
//      (pdf.Form.Fields[KEY_CONTROL_TRANSDUCER] as PdfTextBoxField).Text = certificate.controlTransducer;
      (pdf.Form.Fields[FIELD_CAL_ACCURACY] as PdfTextBoxField).Text = certificate.controlAccuracy + "";
      (pdf.Form.Fields[FIELD_CAL_DATE] as PdfTextBoxField).Text = certificate.lastControlCalibrationDate.ToString("d");
      // Ambient Conditions
      (pdf.Form.Fields[FIELD_AMB_TEMP] as PdfTextBoxField).Text = certificate.environmentTemperature + "";
      (pdf.Form.Fields[FIELD_AMB_RH] as PdfTextBoxField).Text = certificate.environmentHumidity + "";

      // Performance Data Table
      (pdf.Form.Fields[FIELD_CAL_LIMIT] as PdfTextBoxField).Text = certificate.testAccuracy + "";
      foreach (var pair in TABLE_KEYS) {
        var data = certificate.calibrationDataPoints[pair.second];
        for (int i = 0; i < data.Length; i++) {
          (pdf.Form.Fields[pair.first + (i + 1)] as PdfTextBoxField).Text = data[i];
        }
      }

      pdf.Form.FlattenFields();
      pdf.Save(outStream);
    }
  }

  internal class Pair {
    public string first { get; set; }
    public string second { get; set; }

    public Pair(string first, string second) {
      this.first = first;
      this.second = second;
    }
  }
}

