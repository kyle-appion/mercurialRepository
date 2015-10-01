using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;

using ION.Core.App;
using ION.Core.Devices;
using ION.Core.Devices.Certificates;
using ION.Core.Security;
using ION.Core.Util;

namespace ION.IOS.Net {
  public class RequestCalibrationCertificatesTask {
    private const int FLAGS_NONE = 0;
    private const int FLAGS_DEBUG = 1;

    private const string SERVER = "http://www.appioninc.com/cgi-bin/nistserver.py";

    private const string CONTENT_TYPE = "content-type";
    private const string TYPE_JSON = "text/json";
    private const string METHOD_POST = "POST";

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

    public RequestCalibrationCertificatesTask(IION ion, params ISerialNumber[] serialNumbers) {
      Log.D(this, GENERAL_OBFUSCATION_KEY.ToByteString());
      this.ion = ion;
      this.serialNumbers = serialNumbers;
    }

    public async Task<List<AV760CalibrationCertificate>> Request() {
      var ret = new List<AV760CalibrationCertificate>();

      try {
        var data = BuildPostData();

        // Build Request
        var req = (HttpWebRequest)WebRequest.Create(SERVER);
        req.ContentType = TYPE_JSON;
        req.Method = METHOD_POST;
        using (var stream = req.GetRequestStream()) {
          stream.Write(data, 0, data.Length);
          stream.Flush();
          stream.Close();
        }

        // Await Response
        var res = (HttpWebResponse)req.GetResponse();
        using (var stream = new StreamReader(res.GetResponseStream())) {
          var result = stream.ReadToEnd();
          Log.D(this, "Raw result: " + result);
          var response = JsonConvert.DeserializeObject<CalibrationCertificateResponse>(result);
        }

      } catch (Exception e) {
        Log.E(this, "Failed to resolve network request", e);
      }

      return ret;
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

      var asString = JsonConvert.SerializeObject(new CalibrationCertificateRequest() {
        sourceName = ion.name,
        sourceVersion = ion.version,
        sourceFlags = FLAGS_NONE,
        serials = snsa
      }, Formatting.None);

      var ret = asString.ToBytes();
      ret = OBFUSCATOR.Obfuscate(ret, GENERAL_OBFUSCATION_KEY);
      return ret;
    }
  }

  /// <summary>
  /// The data structure that represents a calibration certificate request.
  /// </summary>
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
  internal class CalibrationCertificateResponse {
    [JsonProperty("errorCode")]
    public string errorCode { get; set; }
    [JsonProperty("Request")]
    public string request { get; set; }
  }
}

