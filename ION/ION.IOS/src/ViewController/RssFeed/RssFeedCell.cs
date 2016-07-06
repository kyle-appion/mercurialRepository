using System;
using CoreGraphics;
using UIKit;
using Foundation;


namespace ION.IOS.ViewController.RssFeed {

	public class RssFeedCell : UITableViewCell{
	
		public UILabel header;
		public UITextView content;
		
		public RssFeedCell(IntPtr handle) : base(handle) {
		} 
		
		public void makeCellData(Update feedInfo, nfloat cellHeight, UITableView table){
			Console.WriteLine("Making cell with height: " + cellHeight);
			header = new UILabel(new CGRect(0,0, table.Bounds.Width,30));
			header.Text = "Update " + feedInfo.title;
			header.BackgroundColor = UIColor.Clear;
			header.AdjustsFontSizeToFitWidth = true;
			header.TextAlignment = UITextAlignment.Center;
			
			content = new UITextView(new CGRect(0,30,table.Bounds.Width,cellHeight - 30));
			foreach(var sub in feedInfo.description){
				content.Text += sub + "\n";
			//NSError error = null;
   //   var htmlString = new NSAttributedString (
			//	NSData.FromString(feedInfo.description),
			//	new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8},
			//	ref error
			//);
			//content.AttributedText = htmlString;				
			}

			content.UserInteractionEnabled = false;
			
			this.Layer.BorderWidth = 1f;
			
			this.AddSubview(header);
			this.AddSubview(content);
		}
		
	}
}

