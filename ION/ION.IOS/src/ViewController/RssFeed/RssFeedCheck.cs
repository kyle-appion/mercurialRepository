using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace ION.IOS.ViewController.RssFeed {
	public class RssFeedCheck {
		public const string FEEDURL = "http://www.buildtechhere.com/RSS/feed.xml";
		
		public RssFeedCheck() {
		
		}
		public static bool IsReadingXML { get; set; }
    /// <summary>
    /// Begins the read XMLS stream for the last rss feed item.
    /// </summary>
    /// <returns>The read XMLS tream single.</returns>
    public async void BeginReadXMLStreamSingle()
    {
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
        IsReadingXML = true;

        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(FEEDURL);
        httpRequest.BeginGetResponse(new AsyncCallback(FinishWebRequestSingle), httpRequest);
    }
		/// <summary>
		/// Finishs the web request for the last rss feed item.
		/// </summary>
		/// <returns>The web request single.</returns>
		/// <param name="result">Result.</param>
    public async void FinishWebRequestSingle(IAsyncResult result)
    {
    		await Task.Delay(TimeSpan.FromMilliseconds(1));
        IsReadingXML = true; 

        HttpWebResponse httpResponse = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
		var returnView = new UIView();
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            Stream httpResponseStream = httpResponse.GetResponseStream();
            ReturnLastFeed(httpResponseStream);
        }
    } 

    /// <summary>
    /// Returns the last feed item only.
    /// </summary>
    /// <returns>The last feed.</returns>
    /// <param name="xmlStream">Xml stream.</param>
    public async void ReturnLastFeed(Stream xmlStream){
    		await Task.Delay(TimeSpan.FromMilliseconds(1));
    		Update lastFeed = new Update();
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
						lastFeed = item;
						break;
					}
				}
				break;				
            }
        }
        //Done
		string lastTitle = NSUserDefaults.StandardUserDefaults.StringForKey("rssTitle");
		if(string.IsNullOrEmpty(lastTitle) || lastTitle != lastFeed.title){
			using(var pool = new NSAutoreleasePool())
			{
				try
				{
					pool.InvokeOnMainThread (delegate{
						createRssPopup(lastFeed);
					});
				} catch (Exception e){
					Console.WriteLine("Error: " + e);
				}
			}
		}
        IsReadingXML = false;
       
		}
		public async void createRssPopup(Update feed){
			await Task.Delay(TimeSpan.FromSeconds(2));
      		try{
			     var window = UIApplication.SharedApplication.KeyWindow;
			      var vc = window.RootViewController;
			      while (vc.PresentedViewController != null) {
			        vc = vc.PresentedViewController;
			      }
		      	var rssView = new UIView(new CGRect(.05 * vc.View.Bounds.Width,.11 * vc.View.Bounds.Height,.9 * vc.View.Bounds.Width, .78 * vc.View.Bounds.Height));

				rssView.Layer.CornerRadius = 5f;
				rssView.ClipsToBounds = true;
				rssView.Layer.BorderWidth = 1f;
				
				var rssHeader = new UILabel(new CGRect(0,0,rssView.Bounds.Width,.1 * rssView.Bounds.Height));
				rssHeader.BackgroundColor = UIColor.FromRGB(9,211,255);
				rssHeader.ClipsToBounds = true;
				rssHeader.Text = "New Update";
				rssHeader.AdjustsFontSizeToFitWidth = true;
				rssHeader.TextAlignment = UITextAlignment.Center;
				
					
			  var linkTapGesture = new UITapGestureRecognizer(() => {
		    	if(!String.IsNullOrEmpty(feed.link)){
		      		UIApplication.SharedApplication.OpenUrl(new NSUrl(feed.link));
		      	}
			  });
					
			  var rssContent = new UITextView(new CGRect(0,.1 * rssView.Bounds.Height,rssView.Bounds.Width,rssView.Bounds.Height));
			  rssContent.Font = UIFont.BoldSystemFontOfSize (18f);
	
			  NSError error = null;
		      var htmlString = new NSAttributedString (
					NSData.FromString("<div style=\"font-size: 150%\">" +feed.title + "</br>" + feed.description +"</div>"),
					new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8},
					ref error
			  );
				
			  rssContent.AttributedText = htmlString;
					
			  rssContent.AddGestureRecognizer(linkTapGesture); 
					
		      var closeButton = new UIButton(new CGRect(0,.9 * rssView.Bounds.Height,rssView.Bounds.Width,.1 * rssView.Bounds.Height));
		      closeButton.BackgroundColor = UIColor.White;
		      closeButton.Layer.BorderWidth = 1f;
		      closeButton.ClipsToBounds = true;
			  closeButton.SetTitle("Close",UIControlState.Normal);
			  closeButton.SetTitleColor(UIColor.Black,UIControlState.Normal);
		      
		      //closeButton.TouchDown += (sender, e) => {closeButton.BackgroundColor = UIColor.Blue;};
		      //closeButton.TouchUpOutside += (sender, e) => {closeButton.BackgroundColor = UIColor.White;};
		      closeButton.TouchUpInside += (sender, e) => {
				rssView.RemoveFromSuperview();
			  };
			  var newTime = DateTime.Now.ToLocalTime().ToString();

			  NSUserDefaults.StandardUserDefaults.SetString(newTime, "rssCheck");
			  NSUserDefaults.StandardUserDefaults.SetString(feed.title, "rssTitle");
		      
		      rssView.AddSubview(rssHeader);
		      rssView.AddSubview(rssContent);
		      rssView.AddSubview(closeButton);
		      vc.View.AddSubview(rssView);
		      vc.View.BringSubviewToFront(rssView);

			} catch (Exception e){
				Console.WriteLine("Error: " + e);
			}     
		} 
	}
}

