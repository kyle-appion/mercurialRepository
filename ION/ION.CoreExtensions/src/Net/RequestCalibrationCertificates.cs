namespace ION.Core.Net {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Net;
  using System.Threading;
  using System.Threading.Tasks;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

	using Appion.Commons.Security;
	using Appion.Commons.Util;

  using ION.Core.App;
	using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Devices.Certificates;

  /// <summary>
  /// A task that will download the calibration certificates from the appion server.
  /// </summary>
  public class RequestCalibrationCertificates {
    private const int FLAGS_NONE = 0;
    private const int FLAGS_DEBUG = 1;

    private const string SERVER = "http://www.appioninc.com/cgi-bin/nistserver.py";

    private const string CONTENT_TYPE = "content-type";
    private const string TYPE_JSON = "text/json";
    private const string METHOD_POST = "POST";

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

    private static readonly IObfuscator OBFUSCATOR = new XORObfuscator();

    /// <summary>
    /// The general "key" that is used in obfuscation.
    /// </summary>
    private static readonly byte[] GENERAL_OBFUSCATION_KEY = "4251592816643008".ToBytes();

    /// <summary>
    /// The bytes that where polled from the appion server.
    /// </summary>
    /// <value>The bytes.</value>
    public byte[] bytes { get; private set; }

    /// <summary>
    /// The ion instance this request is aware of. Used for building post data.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The serial number of the devices whose certificates are requested.
    /// </summary>
    /// <value>The serial numbers.</value>
    private ISerialNumber[] serialNumbers { get; set; }
    /// <summary>
    /// Whether or not the task has been started.
    /// </summary>
    /// <value><c>true</c> if started; otherwise, <c>false</c>.</value>
    private bool started { get; set; }
    /// <summary>
    /// The cancellation token source that is used to cancel the task. This is automatically
    /// created for the task when initially created, however, you may assign a custom source
    /// if you wish.
    /// </summary>
    /// <value>The token source.</value>
    public CancellationTokenSource tokenSource { get; set; }

    public RequestCalibrationCertificates(IION ion, params ISerialNumber[] serialNumbers) {
      Log.D(this, GENERAL_OBFUSCATION_KEY.ToByteString());
      this.ion = ion;
      this.serialNumbers = serialNumbers;
      tokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// Attempts to cancel the task.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel ; otherwise, <c>false</c>.</returns>
    public void Cancel() {
      if (!started) {
        throw new InvalidOperationException("Cannot cancel task: task not started");
      }
      tokenSource.Cancel();
    }

    /// <summary>
    /// Requests the calibration certificates for all of the task's serial numbers.
    /// </summary>
    /// <param name="tokenSource">Token source.</param>
    public async Task<List<CalibrationCertificateRequestResult>> Request() {
      started = true;

      try {
        var ct = tokenSource.Token;
        var ret = new List<CalibrationCertificateRequestResult>();

        var data = BuildPostData();

        // Bail if cancelled.
        ct.ThrowIfCancellationRequested();

        // Build Request
        var req = (HttpWebRequest)WebRequest.Create(SERVER);
        req.ContentType = TYPE_JSON;
        req.Method = METHOD_POST;
        using (var stream = req.GetRequestStream()) {
          stream.Write(data, 0, data.Length);
          stream.Flush();
          stream.Close();
        }

        // Bail if cancelled.
        ct.ThrowIfCancellationRequested();

        // Await Response
        var res = (HttpWebResponse)req.GetResponse();

        // Bail if cancelled.
        ct.ThrowIfCancellationRequested();

        using (var stream = new StreamReader(res.GetResponseStream())) {
          var result = stream.ReadToEnd();
          var obj = JObject.Parse(result);

          foreach (var s in obj.Properties()) {
            try {
              var settings = new JsonSerializerSettings();
              settings.MissingMemberHandling = MissingMemberHandling.Ignore;
              var response = JsonConvert.DeserializeObject<CalibrationCertificateResponse>(s.Value.ToString());//, settings);
              if (response.errorCode != null) {
                Log.E(this, "Failed to get calibration certificate for: " + response.serialNumber + "{" + response.errorCode + "}");
                ret.Add(new CalibrationCertificateRequestResult(GaugeSerialNumber.Parse(s.Name)));
              } else {
                ret.Add(new CalibrationCertificateRequestResult(response.ToCalibrationCertificate()));
              }
            } catch (Exception e) {
              Log.E(this, "Failed to parse certificate: " + s.Value, e);
              ret.Add(new CalibrationCertificateRequestResult(GaugeSerialNumber.Parse(s.Name)));
            }
          }
        }

        return ret;
      } finally {
        started = false;
      }
    }

    /// <summary>
    /// Builds the accompanying post data.
    /// </summary>
    /// <returns>The post data.</returns>
    private byte[] BuildPostData() {
      string[] snsa = new string[serialNumbers.Length];
      for (int i = 0; i < serialNumbers.Length; i++) {
        snsa[i] = serialNumbers[i].ToString();
      }

			var t = new CalibrationCertificateRequest() {
				sourceName = ion.name,
				sourceVersion = ion.version,
				sourceFlags = FLAGS_NONE,
				serials = snsa
			};

      var asString = JsonConvert.SerializeObject(t, Formatting.Indented);

      var ret = asString.ToBytes();
      ret = OBFUSCATOR.Obfuscate(ret, GENERAL_OBFUSCATION_KEY);
      return ret;
    }

    private GaugeDeviceCalibrationCertificate Parse(JObject obj) {
      var ret = new GaugeDeviceCalibrationCertificate();

      ret.controlSerial = obj.Value<string>(KEY_CONTROL_SERIAL);
      ret.controlInstrument = obj.Value<string>(KEY_CONTROL_INSTRUMENT);
      ret.controlTransducer = obj.Value<string>(KEY_CONTROL_TRANSDUCER);


      return ret;
    }
  }

  public class CalibrationCertificateRequestResult {
    public ISerialNumber serialNumber { get; set; }
    public GaugeDeviceCalibrationCertificate certificate { get; set; }
    public bool success { get; set; }

    public CalibrationCertificateRequestResult(ISerialNumber serialNumber) {
      this.serialNumber = serialNumber;
      this.success = false;
    }

    public CalibrationCertificateRequestResult(GaugeDeviceCalibrationCertificate certificate) {
      this.serialNumber = certificate.testSerialNumber;
      this.certificate = certificate;
      this.success = true;
    }
  }

  /// <summary>
  /// The data structure that represents a calibration certificate request.
  /// </summary>
  [Preserve (AllMembers = true)]
  internal class CalibrationCertificateRequest {
    [JsonProperty("source_name")]
    public string sourceName { get; set; }
    [JsonProperty("source_version")]
    public string sourceVersion { get; set; }
    [JsonProperty("source_flags")]
    public int sourceFlags { get; set; }
    [JsonProperty("serial_lists")]
    public string[] serials { get; set; }
  }

  /// <summary>
  /// The data structure that represents a response from the appion server
  /// containing a calibration certificate request.
  /// </summary>
  [Preserve (AllMembers = true)]
	internal class CalibrationCertificateResponse {
    [JsonProperty("errorCode")]
    public string errorCode { get; set; }
    [JsonProperty("certifiedBy")]
    public string certifiedBy { get; set; }
    [JsonProperty("ambientTemperature")]
    public double temperature { get; set; }
    [JsonProperty("serialNumber")]
    public string serialNumber { get; set; }
    [JsonProperty("ambientRelativeHumidity")]
    public double humidity { get; set; }
    [JsonProperty("accurancyLimit")]
    public double accuracyLimit { get; set; }
    [JsonProperty("partNumber")]
    public string partNumber { get; set; }
    [JsonProperty("lastCalibrationDate")]
    public string calDate { get; set; }
    [JsonProperty("id")]
    public long id { get; set; }

    [JsonProperty("calibrationStandardDevice")]
    public CalibrationStandardDevice controlDevice { get; set; }
    [JsonProperty("performanceData")]
    public PerformanceData performanceData { get; set; }

    public GaugeDeviceCalibrationCertificate ToCalibrationCertificate() {
      var ret = new GaugeDeviceCalibrationCertificate();

      ret.controlSerial = controlDevice.serial;
      ret.controlInstrument = controlDevice.instrumentModel;
      ret.controlTransducer = controlDevice.transducerModel;
      ret.controlAccuracy = controlDevice.accuracy;

      ret.testSerialNumber = GaugeSerialNumber.Parse(serialNumber);
      ret.testPartNumber = partNumber;
      ret.testAccuracy = accuracyLimit;
      ret.environmentTemperature = temperature;
      ret.environmentHumidity = humidity;
      ret.certifiedBy = certifiedBy;

      var parts = controlDevice.calDate.Split('-');
      ret.lastControlCalibrationDate = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));

      parts = calDate.Split('-');
      ret.lastTestCalibrationDate = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
      ret.calibrationDataPoints.Add("calibrationStandard", performanceData.standardValues.ToArray());
      ret.calibrationDataPoints.Add("appionGauge", performanceData.gaugeValues.ToArray());
      ret.calibrationDataPoints.Add("minReading", performanceData.minValues.ToArray());
      ret.calibrationDataPoints.Add("maxReading", performanceData.maxValues.ToArray());
      ret.calibrationDataPoints.Add("calibrationUnit", performanceData.gaugeUnits.ToArray());

      return ret;
    }
  }
	[Preserve (AllMembers = true)]
  internal class PerformanceData {
    [JsonProperty("calibrationStandard")]
    public List<string> standardValues { get; set; }
    [JsonProperty("appionGauge")]
    public List<string> gaugeValues { get; set; }
    [JsonProperty("minReading")]
    public List<string> minValues { get; set; }
    [JsonProperty("maxReading")]
    public List<string> maxValues { get; set; }
    [JsonProperty("calibrationUnit")]
    public List<string> gaugeUnits { get; set; }
  }
	[Preserve (AllMembers = true)]
  internal class CalibrationStandardDevice {
    [JsonProperty("transducerModel")]
    public string transducerModel { get; set; }
    [JsonProperty("serialNumber")]
    public string serial { get; set; }
    [JsonProperty("instrumentModel")]
    public string instrumentModel { get; set; }
    [JsonProperty("accuracyOfStandard")]
    public double accuracy { get; set; }
    [JsonProperty("lastCalibrationDate")]
    public string calDate { get; set; }
    [JsonProperty("id")]
    public long id { get; set; }
  }
}

