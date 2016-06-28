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
		public const string FEEDURL = "feed2.xml";
		
		public RssFeedViewController(IntPtr handle) : base(handle) {
		}

    public static bool IsReadingXML { get; set; }
    
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
      AutomaticallyAdjustsScrollViewInsets = false; 
      
			feedItems = new List<Update>();
			feedHeader = new RssHeader();
			loadingFeed = new UIActivityIndicatorView(new CGRect(0,45,View.Bounds.Width, View.Bounds.Height - 45));
			
			rssTable = new UITableView(new CGRect(0,45,View.Bounds.Width,View.Bounds.Height - 45));
			rssTable.RegisterClassForCellReuse(typeof(RssFeedCell),"rssFeedCell");
			rssTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			refreshFeed = new UIRefreshControl();
      refreshFeed.ValueChanged += (sender, e) => {
      		feedItems = new List<Update>();
       		loadingFeed.StartAnimating();
          BeginReadXMLStream("feed2.xml");
      };
      
      rssTable.InsertSubview(refreshFeed,0);
      rssTable.SendSubviewToBack(refreshFeed);
			
			View.AddSubview(rssTable);
			View.AddSubview(loadingFeed);
			View.BringSubviewToFront(loadingFeed);
			
			loadingFeed.StartAnimating();
			BeginReadXMLStream("feed2.xml");
		}

    public void BeginReadXMLStream(string currFileName)
    {
        IsReadingXML = true;

        string ImagesRootFolder = "http://www.buildtechhere.com/RSS/";
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(ImagesRootFolder + currFileName);
        httpRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), httpRequest);
    }

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

    public void BuildItemList(Stream xmlStream) 
    {
        using (XmlReader myXMLReader = XmlReader.Create((xmlStream)))
        {

            while (myXMLReader.Read())
            {
							//if(myXMLReader.NodeType == XmlNodeType.Element){
								//if(myXMLReader.Name == "title"){
								//  Update item = new Update();
								//  item.description = new List<string>();
								//	var feedTitle = myXMLReader.ReadElementContentAsString();
								//	item.title = feedTitle;
								//	while(myXMLReader.Name != "channel"){
								//		if(myXMLReader.Name == "new" || myXMLReader.Name == "updated" || myXMLReader.Name == "fixed"){
								//			var entry = "•\t" + myXMLReader.ReadElementContentAsString() + "\n";
								//			item.description.Add(entry);
								//		}
								//		myXMLReader.Read();
								//	}
								//	feedItems.Add(item);
								//}
							//}

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


