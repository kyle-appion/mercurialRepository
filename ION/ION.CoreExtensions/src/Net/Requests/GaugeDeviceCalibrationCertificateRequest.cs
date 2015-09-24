using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ION.Core.Devices;
using ION.Core.Util;

namespace ION.Core.Net {
  /// <summary>
  /// An HTTP request that polls device(s) calibration certificates from the 
  /// appion service.
  /// </summary>
  public class GaugeDeviceCalibrationCertificateRequest {
    /// <summary>
    /// The general "key" that is used in obfuscation.
    /// </summary>
    private static readonly byte[] GENERAL_OFUSCATION_KEY = "4251592816643008".ToBytes();

    /// <summary>
    /// The serial numbers of the devices whose certificates are requested.
    /// </summary>
    /// <value>The serial numbers.</value>
    private GaugeSerialNumber[] serialNumbers { get; set; }
    /// <summary>
    /// The pending request for certificates.
    /// </summary>
    /// <value>The web request.</value>
    private WebRequest webRequest { get; set; }

    public GaugeDeviceCalibrationCertificateRequest(params GaugeSerialNumber[] serialNumbers) {
      this.serialNumbers = serialNumbers;
    }

    public async Task<bool> Poll() {
      try {
        var data = GetPostData();

        var req = (HttpWebRequest)WebRequest.Create(BuildRequestUri());
        req.ContentLength = data.Length;
        req.ContentType = ContentTypes.TYPE_JSON;
        req.Method = "POST";

        using (var stream = req.GetRequestStream()) {
          stream.Write(data, 0, data.Length);
        }

        var res = (HttpWebResponse)req.GetResponse();
        var responseString = new StreamReader(res.GetResponseStream()).ReadToEnd();
        Log.D(this, "ResponseString: " + responseString);
        return true;
      } catch (Exception e) {
        Log.E(this, "Failed to do network things", e);
        return false;
      }
    }

    private byte[] GetPostData() {
      
      return null;
    }

    private string BuildRequestUri() {
      var sb = new StringBuilder();

      return sb.ToString();
    }
  }
}

