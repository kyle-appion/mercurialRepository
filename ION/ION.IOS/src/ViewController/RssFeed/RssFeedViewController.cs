using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;
using UIKit;

namespace ION.IOS.ViewController.RssFeed {

	public class Update {
	  public string title;
	  public string link;
	  public string description;
	  public string pubDate;
	}
	
	public class RssHeader{
		public string title;
		public string link;
		public string description;
		public string copyright;
		public DateTime pubDate;
		public DateTime buildDate;
	}
  	
	public partial class RssFeedViewController  : BaseIONViewController {

		public List<Update> feedItems;
		public RssHeader feedHeader;
		public UITableView rssTable;
		public UIActivityIndicatorView loadingFeed;
		public UIRefreshControl refreshFeed;
    public const string FEEDURL = "http://portal.appioninc.com/RSS/feed.xml";
		
		public RssFeedViewController(IntPtr handle) : base(handle) {
		}

    public static bool IsReadingXML { get; set; }
    
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
      AutomaticallyAdjustsScrollViewInsets = false; 
      feedItems = new List<Update>();
      feedHeader = new RssHeader();     

      setupRSSFeed();
		}
    
    public async void setupRSSFeed(){
      await Task.Delay(TimeSpan.FromMilliseconds(100));

      loadingFeed = new UIActivityIndicatorView(new CGRect(0,45,View.Bounds.Width, View.Bounds.Height - 45));
      
      rssTable = new UITableView(new CGRect(0,45,View.Bounds.Width,View.Bounds.Height - 45));
      rssTable.RegisterClassForCellReuse(typeof(RssFeedCell),"rssFeedCell");
      rssTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      rssTable.AlwaysBounceVertical = true;
      
      refreshFeed = new UIRefreshControl();
      refreshFeed.ValueChanged += (sender, e) => {
          feedItems = new List<Update>();
          loadingFeed.StartAnimating();
          BeginReadXMLStream();
      };
      rssTable.InsertSubview(refreshFeed,0);
      rssTable.SendSubviewToBack(refreshFeed);
      
      View.AddSubview(rssTable);
      View.AddSubview(loadingFeed);
      View.BringSubviewToFront(loadingFeed);
      
      loadingFeed.StartAnimating();
      BeginReadXMLStream();
    }
		/// <summary>
		/// Begins the read XMLS stream for entire rss feed.
		/// </summary>
		/// <returns>The read XMLS tream.</returns>
    public void BeginReadXMLStream()
    {
        IsReadingXML = true;

        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(FEEDURL);
        httpRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), httpRequest);
    }

    /// <summary>
    /// Finishs the web request for entire rss feed
    /// </summary>
    /// <returns>The web request.</returns>
    /// <param name="result">Result.</param>
    public void FinishWebRequest(IAsyncResult result)
    {
        IsReadingXML = true; 

        HttpWebResponse httpResponse = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;

        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            Stream httpResponseStream = httpResponse.GetResponseStream();
            BuildItemList(httpResponseStream);
        }
    }

    /// <summary>
    /// build feed for all feed items
    /// </summary>
    /// <returns>The item list.</returns>
    /// <param name="xmlStream">Xml stream.</param>
    public void BuildItemList(Stream xmlStream) 
    {
        using (XmlReader myXMLReader = XmlReader.Create((xmlStream)))
        {
            while (myXMLReader.Read())
            {
							while(myXMLReader.Name != "title"){
								myXMLReader.Read();
							}

							var hTitle = myXMLReader.ReadElementContentAsString();
							//feedHeader.title = hTitle;

							myXMLReader.Read();

							var hLink = myXMLReader.ReadElementContentAsString();
							//feedHeader.link = hLink;

							myXMLReader.Read();

							var hDescription = myXMLReader.ReadElementContentAsString();
							myXMLReader.Read();

							var hLanguage = myXMLReader.ReadElementContentAsString();
							myXMLReader.Read();

							var hCopyright = myXMLReader.ReadElementContentAsString();
							myXMLReader.Read();

							var hPubdate = myXMLReader.ReadElementContentAsString();							
							while(myXMLReader.Name != "item"){
								myXMLReader.Read();
							}
							
							while(myXMLReader.Read() && myXMLReader.Name != "rss"){

								if(myXMLReader.Name == "title" && myXMLReader.NodeType == XmlNodeType.Element){
									var item = new Update();
									var fTitle = myXMLReader.ReadElementContentAsString();

									item.title = fTitle;
									myXMLReader.Read();
									var fLink = myXMLReader.ReadElementContentAsString();

									item.link = fLink;
									myXMLReader.Read();
									var fDescription = myXMLReader.ReadElementContentAsString();

									item.description = fDescription;
									myXMLReader.Read();

									var fPubdate = myXMLReader.ReadElementContentAsString();

									item.pubDate = fPubdate;
									feedItems.Add(item);
								}
							}
							if(myXMLReader.Name == "rss" && myXMLReader.NodeType != XmlNodeType.Element){

								break;
							}											
            }
            InvokeOnMainThread(()=> reloadTableData());
        }
        //Done
        IsReadingXML = false;
    }
    
    public void reloadTableData(){
    		loadingFeed.StopAnimating();
    		refreshFeed.EndRefreshing();
    		
        rssTable.Source = new RssFeedDataSource(feedItems);
        rssTable.ReloadData();
		}
		
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


