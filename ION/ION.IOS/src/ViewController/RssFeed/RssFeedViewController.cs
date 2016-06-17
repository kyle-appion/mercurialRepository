using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;
using UIKit;

namespace ION.IOS.ViewController.RssFeed {
		public class Update {
		  public string title;
		  public string link;
		  public string description;
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
			feedItems = new List<Update>();
			rssTable = new UITableView(new CGRect(0,50,View.Bounds.Width,View.Bounds.Height - 50));
			rssTable.RegisterClassForCellReuse(typeof(RssFeedCell),"rssFeedCell");
			
			BeginReadXMLStream("feed.xml");			
		}

    public void BeginReadXMLStream(string currFileName)
    {
        IsReadingXML = true;

        string ImagesRootFolder = "http://www.buildtechhere.com/RSS/";
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(ImagesRootFolder + currFileName);
        httpRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), httpRequest);
    }

    private void FinishWebRequest(IAsyncResult result)
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
    	Console.WriteLine("build item list");
        try
        {
            using (XmlReader myXMLReader = XmlReader.Create((xmlStream)))
            {
            	Console.WriteLine("About to read");
                while (myXMLReader.Read())
                {
                	if(myXMLReader.Name == "title"){
                		Update item = new Update();

                		var pulledTitle = myXMLReader.ReadElementContentAsString();
                		item.title = pulledTitle;
										Console.WriteLine(pulledTitle);
										while(myXMLReader.Name != "link"){
											myXMLReader.Read();
										}										
										var pulledLink = myXMLReader.ReadElementContentAsString();
										item.link = pulledLink;
										Console.WriteLine(pulledLink);
										while(myXMLReader.Name != "description"){
											myXMLReader.Read();
										}
										
										var pulledDescription = myXMLReader.ReadElementContentAsString();
										item.description = pulledDescription;
										Console.WriteLine(pulledDescription);
										feedItems.Add(item);
									}
                }
              Console.WriteLine("Finished pulling the updates. There were " + feedItems.Count + " in total"); 
              rssTable.Source = new RssFeedDataSource(feedItems);
            }
        }
        catch {
					Console.WriteLine("Error error will robinson");
				}

        //Done
        IsReadingXML = false;
    }
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


