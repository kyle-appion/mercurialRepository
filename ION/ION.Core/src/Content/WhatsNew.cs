namespace ION.Core.Content {
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml;
	using System.Xml.Serialization;
  using System.Xml.Linq;

  /// <summary>
  /// A simple object that contains an app version's what's new diaload content. For every version there is suppose to
  /// be a what's new update description.
  /// </summary>
	[XmlRoot("display")]
  public class WhatsNew : IComparable<WhatsNew>  {
    /// <summary>
    /// The version code of the app that this What's New belongs to.
    /// </summary>
    /// <value>The version code.</value>
		[XmlAttribute("version")]
    public AppVersion versionCode { get; internal set; }
    /// <summary>
    /// The list of the updated
    /// </summary>
    /// <value>The whats updated.</value>
		[XmlElement("update")]
    public List<string> whatsUpdated { get; internal set; }
    /// <summary>
    /// The list of the whats new.
    /// </summary>
    /// <value>The whats updated.</value>
		[XmlElement("new")]
    public List<string> whatsNew { get; internal set; }
    /// <summary>
    /// The list of the what was fixed in the app.
    /// </summary>
    /// <value>The whats updated.</value>
		[XmlElement("fixed")]
    public List<string> whatsFixed { get; internal set; }

    public int CompareTo(WhatsNew other) {
      return versionCode.CompareTo(other.versionCode);
    }

    /// <summary>
    /// Creates a list of WhatsNew objects from the given stream.
    /// </summary>
    /// <exception cref="Exception">If the stream does not parse into a valid whats new list.</exception>
    /// <returns>The from stream.</returns>
    /// <param name="stream">Stream.</param>
    public static List<WhatsNew> ParseWithException(Stream stream) {
//      return ParseWithException(XmlReader.Create(stream));
			return WhatsNewFactory.ParseOrThrow(stream);
    }

    /// <summary>
    /// Creates a list of WhatsNew object from the given xml reader. Throws an excption on failure.
    /// </summary>
    public static List<WhatsNew> ParseWithException(XmlReader xml) {
      return WhatsNewFactory.ParseWithException(xml);
    }
  }

  internal class WhatsNewFactory {
    private const string WHATSNEW = "whatsnew";
    private const string DISPLAY = "display";
    private const string VERSION = "version";
    private const string NEW = "new";
    private const string UPDATE = "update";
    private const string FIXED = "fixed";

    private WhatsNewFactory() {
    }

		public static List<WhatsNew> ParseOrThrow(Stream stream) {
			var xml = new XmlSerializer(typeof(WhatsNew));
			var ret = xml.Deserialize(stream) as WhatsNewXml;
			if (ret != null) {
				return ret.whatsNew;
			} else {
				throw new Exception("Failed to parse what's new");
			}
		}

    /// <summary>
    /// Parses a list of WhatsNew objects from the given xml reader.
    /// </summary>
    /// <returns>The with exception.</returns>
    /// <param name="xml">Xml.</param>
    public static List<WhatsNew> ParseWithException(XmlReader xml) {
      var ret = new List<WhatsNew>();

      var root = XElement.Load(xml);
      foreach (var element in root.Elements()) {
        ret.Add(ParseWhatsNew(element));
      }

      return ret;
    }

    /// <summary>
    /// Parses a single WhatsNew object from the stream.
    /// </summary>
    /// <returns>The whats new.</returns>
    /// <param name="element">Element.</param>
    private static WhatsNew ParseWhatsNew(XElement element) {
			var version = AppVersion.ParseOrThrow(element.Attribute(VERSION).Value);

      var @new = new List<string>();
      var updated = new List<string>();
      var @fixed = new List<string>();

      foreach (var e in element.Elements()) {
        switch (e.Name.ToString()) {
          case NEW:
            @new.Add(e.Value);
            break;
          case UPDATE:
            updated.Add(e.Value);
            break;
          case FIXED:
            @fixed.Add(e.Value);
            break;
          default:
            throw new Exception("Unexpected element {" + e.Name + "} in what's new stream");
        }
      }

      return new WhatsNew() {
        versionCode = version,
        whatsNew = @new,
        whatsUpdated = updated,
        whatsFixed = @fixed,
      };
    }

		[XmlRoot("whatsnew")]
		private class WhatsNewXml {
			public List<WhatsNew> whatsNew;
		}
  }
}

