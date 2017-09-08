using System.Globalization;
using OxyPlot.Series;

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
    private const string RSS = "rss";
    private const string CHANNEL = "channel";
    private const string VERSION = "version";
    private const string TITLE = "title";
    private const string LINK = "link";
    private const string DESCRIPTION = "description";
    private const string LANGUAGE = "language";
    private const string COPYRIGHT = "copyright";
    private const string PUBLISH_DATE = "pubDate";
    private const string EXPIRE_DATE = "expDate";
    private const string LAST_BUILD_DATE = "lasBuildDate";
    private const string CATEGORY = "category";
    private const string GENERATOR = "generator";
    private const string DOCS = "docs";
    private const string ITEM = "item";
    private const string DATE_FORMAT = "dd MMM yyyy HH:mm:SS";
    
    
    public static Rss ParseFeedOrThrow(Stream stream) {
      var root = XElement.Load(XmlReader.Create(stream));
      var devices = root.Elements();

      var rss = new Rss();
      rss.version = root.Attribute(VERSION)?.Value + "";

      foreach (var e in root.Elements()) {
        switch (e.Name.ToString()) {
          case CHANNEL:
            rss.channels.Add(ParseChannelOrThrow(e));
            break;
        }
      }

      return rss;
    }

    private static RssChannel ParseChannelOrThrow(XElement element) {
      var ret = new RssChannel();

			ret.language = new CultureInfo("en-us");

      string usPubDate = null;
      string usLastBuildDate = null;
      
      foreach (var e in element.Elements()) {
        switch (e.Name.ToString()) {
          case TITLE:
            ret.title = e.Value;
            break;
            
          case LINK:
            ret.link = e.Value;
            break;
            
          case DESCRIPTION:
            ret.description = e.Value;
            break;
            
          case LANGUAGE:
            ret.language = new CultureInfo(e.Value);
            break;
            
          case COPYRIGHT:
            ret.copyright = e.Value;
            break;
            
          case PUBLISH_DATE:
            usPubDate = e.Value;
            break;
            
          case LAST_BUILD_DATE:
            usLastBuildDate = e.Value;
            break;
            
          case CATEGORY:
            ret.category = e.Value;
            break;
            
          case GENERATOR:
            ret.generator = e.Value;
            break;
            
          case DOCS:
            ret.docs = e.Value;
            break;
            
          case ITEM:
            ret.items.Add(ParseItemOrThrow(ret.language, e));
            break;
        }
      }

      if (usPubDate != null) {
				usPubDate = usPubDate.Substring(5);
        ret.publishDate = DateTime.Parse(usPubDate);
			}

      if (usLastBuildDate != null) {
				usLastBuildDate = usLastBuildDate.Substring(5);
        ret.lastBuildDate = DateTime.Parse(usLastBuildDate);
			}

      return ret;
    }
    
    private static RssItem ParseItemOrThrow(CultureInfo ci, XElement element) {
      var ret = new RssItem();

      string usPubDate = null;
      string usExpDate = null;

      foreach (var e in element.Elements()) {
        switch (e.Name.ToString()) {
          case TITLE:
            ret.title = e.Value;
            break;
            
          case LINK:
            ret.link = e.Value;
            break;
            
          case DESCRIPTION:
            ret.description = e.Value;
            break;
            
          case PUBLISH_DATE:
            usPubDate = e.Value;
            break;
            
          case EXPIRE_DATE:
            usExpDate = e.Value;
            break;
        }
      }

			if (usPubDate != null) {
        usPubDate = usPubDate.Substring(5);
        ret.publishDate = DateTime.Parse(usPubDate);
			}

      if (usExpDate != null) {
				usExpDate = usExpDate.Substring(5);
        ret.expireDate = DateTime.Parse(usExpDate);
			}

      return ret;
    }
  }
  
  public class Rss {
    public List<RssChannel> channels { get; internal set; }
    public string version { get; internal set; }

    public Rss() {
      channels = new List<RssChannel>();
    }
  }

  public class RssChannel {
    public string title { get; internal set; }
    public string link { get; internal set; }
    public string description { get; internal set; }
    public CultureInfo language { get; internal set; }
    public string copyright { get; internal set; }
    public DateTime publishDate { get; internal set; }
    public DateTime lastBuildDate { get; internal set; }
    public string category { get; internal set; }
    public string generator { get; internal set; }
    public string docs { get; internal set; }
    public List<RssItem> items { get; internal set; }

    public RssChannel() {
      items = new List<RssItem>();
    }
  }

  public class RssItem {
    public string title { get; internal set; }
    public string link { get; internal set; }
    public string description { get; internal set; }
    public DateTime publishDate { get; internal set; }
    public DateTime expireDate { get; internal set; }
  }
}
