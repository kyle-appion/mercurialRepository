namespace ION.Core.IO {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;
  using System.Xml;
  using System.Xml.Linq;
  using System.Xml.Serialization;

  using Appion.Commons.Util;

  public class RssParser {
    public static Rss ParseFeedOrThrow(Stream stream) {
      var xml = new XmlSerializer(typeof(Rss));
      return (Rss)xml.Deserialize(stream);
    }
  }

  [XmlRoot("rss")]
  public class Rss {
    [XmlElement("channel")]
    public Feed channelFeed;
  }

  public class Feed {
    [XmlElement("title")]
    public string title { get; internal set; }
    [XmlElement("link")]
    public string link { get; internal set; }
    [XmlElement("description")]
    public string description { get; internal set; }
    [XmlElement("language")]
    public string language { get; internal set; }
    [XmlElement("copyright")]
    public string copyright { get; internal set; }
    [XmlElement("pubDate")]
    public string publishDate { get; internal set; }
    [XmlElement("lastBuildDate")]
    public string lastBuildDate { get; internal set; }
    [XmlElement("category")]
    public string category { get; internal set; }
    [XmlElement("generator")]
    public string generator { get; internal set; }
    [XmlElement("docs")]
    public string docs { get; internal set; }
    [XmlElement("item")]
    public List<FeedItem> items { get; internal set; }
  }

  public class FeedItem {
    [XmlElement("title")]
    public string title { get; internal set; }
    [XmlElement("link")]
    public string link { get; internal set; }
    [XmlElement("description")]
    public string description { get; internal set; }
    [XmlElement("pubDate")]
    public string publishDate { get; internal set; }
  }
}
