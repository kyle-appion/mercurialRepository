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
	  public List<string> description;
	  //public string description;
	}
  	
	public partial class RssFeedViewController  : BaseIONViewController {

		public List<Update> feedItems;
		public UITableView rssTable;
		
		public RssFeedViewController(IntPtr handle) : base(handle) {
		}

    public static bool IsReadingXML { get; set; }
    
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
      AutomaticallyAdjustsScrollViewInsets = false;
      
			feedItems = new List<Update>();
			
			rssTable = new UITableView(new CGRect(0,45,View.Bounds.Width,View.Bounds.Height - 45));
			rssTable.RegisterClassForCellReuse(typeof(RssFeedCell),"rssFeedCell");
			rssTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			View.AddSubview(rssTable);
			BeginReadXMLStream("feed.xml");
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
							if(myXMLReader.NodeType == XmlNodeType.Element){
								if(myXMLReader.Name == "title"){
								  Update item = new Update();
								  item.description = new List<string>();
									var feedTitle = myXMLReader.ReadElementContentAsString();
									item.title = feedTitle;
									while(myXMLReader.Name != "channel"){
										if(myXMLReader.Name == "new" || myXMLReader.Name == "updated" || myXMLReader.Name == "fixed"){
											var entry = "•\t" + myXMLReader.ReadElementContentAsString() + "\n";
											item.description.Add(entry);
										}
										myXMLReader.Read();
									}
									feedItems.Add(item);
								}
								//if(myXMLReader.Name == "title"){
								//	Update item = new Update();
								//	var feedTitle = myXMLReader.ReadElementContentAsString();
								//	Console.WriteLine("Title: " + feedTitle);
								//	item.title = feedTitle;
								//	myXMLReader.Read();
								//	Console.WriteLine("Next element: " + myXMLReader.Name);									
									

								//	//feedItems.Add(item);
								//}
							}
            }
            InvokeOnMainThread(()=> reloadTableData());
        }
        //Done
        IsReadingXML = false;
    }
    
    public void reloadTableData(){
        rssTable.Source = new RssFeedDataSource(feedItems);
        rssTable.ReloadData();
		}
		
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


