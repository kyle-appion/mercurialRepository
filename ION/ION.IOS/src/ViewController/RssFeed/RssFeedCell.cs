using System;
using System.Globalization;
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
			var culture = CultureInfo.CreateSpecificCulture("en-US");
			var style = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;
			var feedDate = DateTime.Parse(feedInfo.pubDate,culture,style);
			
			header = new UILabel(new CGRect(0,0, table.Bounds.Width,30));
			header.Lines = 0;
			header.Text = feedInfo.title + " " + feedDate + " MST";
			header.BackgroundColor = UIColor.Clear;
			header.AdjustsFontSizeToFitWidth = true;
			header.TextAlignment = UITextAlignment.Center;
      header.Font = UIFont.BoldSystemFontOfSize(20f);
			
			content = new UITextView(new CGRect(0,30,table.Bounds.Width,cellHeight - 30));
			content.Font = UIFont.SystemFontOfSize(30f);
			
			NSError error = null;
      var htmlString = new NSAttributedString (
				NSData.FromString("<div style=\"font-size: 150%\">" +feedInfo.description+"</div>"),
				new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8},
				ref error
			);
			content.AttributedText = htmlString;
      content.TextContainer.MaximumNumberOfLines = 0;
      content.TextContainer.LineBreakMode = UILineBreakMode.TailTruncation;

			content.UserInteractionEnabled = false;
			
			this.Layer.BorderWidth = 1f;
			
			this.AddSubview(header);
			this.AddSubview(content);
		}
		
	}
}

