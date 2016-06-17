using System;
using CoreGraphics;
using Foundation;
using UIKit;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using ION.Core.App;


namespace ION.IOS.App {

	public class WhatsNewView
	{
		public UIView infoView;
		public UILabel viewHeader;
		public UIScrollView contentHolder;
		public UITextView content;
		public UIButton closeCorner;
		public UIButton closeBottom;
		private int fontSize;
		private int headingSize;
		
	
		
		public WhatsNewView(UIView parentView, UIViewController parentVC)
		{
			infoView = new UIView(new CGRect(.025 * parentView.Bounds.Width, 80, .95 * parentView.Bounds.Width, .85 * parentView.Bounds.Height));
			infoView.Layer.CornerRadius = 5;
			infoView.ClipsToBounds = true;
			infoView.BackgroundColor = UIColor.White;
			infoView.Layer.BorderWidth = 1f;
			
			viewHeader = new UILabel(new CGRect(0,0,infoView.Bounds.Width, 90));
			viewHeader.Lines = 0;
			viewHeader.BackgroundColor = UIColor.FromRGB(9,211,255);
			viewHeader.ClipsToBounds = true;
			viewHeader.TextAlignment = UITextAlignment.Left;
			viewHeader.Font = UIFont.SystemFontOfSize(30);
			viewHeader.Layer.ShadowColor = UIColor.FromRGB(8,199,240 ).CGColor;
			viewHeader.Layer.ShadowOpacity = .5f;
			viewHeader.Layer.ShadowRadius = 1.0f;
			viewHeader.Layer.ShadowOffset = new CGSize(1,2);
			viewHeader.Layer.MasksToBounds = false;
			viewHeader.Text = " New This Version";
			
			closeCorner = new UIButton(new CGRect(viewHeader.Bounds.Width - 45,25,35,35));
			closeCorner.SetImage(UIImage.FromBundle("img_button_blackclosex"),UIControlState.Normal);
			closeCorner.BackgroundColor = UIColor.Clear;
			closeCorner.TouchUpInside += (sender, e) => {
				infoView.RemoveFromSuperview();
			};
			
			contentHolder = new UIScrollView(new CGRect(0,90,infoView.Bounds.Width,infoView.Bounds.Height - 140));
			contentHolder.Layer.CornerRadius = 5;
			contentHolder.ClipsToBounds = true;
			contentHolder.ContentSize = new CGSize(contentHolder.Bounds.Width,1.2 * contentHolder.Bounds.Height);
			
			closeBottom = new UIButton(new CGRect(0,infoView.Bounds.Height - 50,infoView.Bounds.Width,50));
			closeBottom.ClipsToBounds = true;
			closeBottom.SetTitle("Close",UIControlState.Normal);
			closeBottom.SetTitleColor(UIColor.Black,UIControlState.Normal);
			closeBottom.Layer.BorderWidth = 1f;
			closeBottom.Layer.BorderColor = UIColor.LightGray.CGColor;
			closeBottom.TouchUpInside += (sender, e) => {
				infoView.RemoveFromSuperview();
			};
			
			content = new UITextView(new CGRect(0,0,contentHolder.Bounds.Width,1.2 * contentHolder.Bounds.Height));
			content.Layer.CornerRadius = 5;
			content.ClipsToBounds = true;
			content.UserInteractionEnabled = false;
			content.Font = UIFont.SystemFontOfSize(30);
			
			if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone){
				headingSize = 5;
				fontSize = 12;
			} else {
				headingSize = 2;
				fontSize = 15;
			}
			
		FileStream stream = new FileStream("./xml/whats_new.xml",FileMode.Open,FileAccess.Read);			
			
		var sb = new StringBuilder();
		var whatsNew = ION.Core.Content.WhatsNew.ParseWithException(stream);
      foreach (var wn in whatsNew) {
        sb.Append("<h2>").Append(" Version " + wn.versionCode).Append("</h2>");

        sb.Append("<b><h"+headingSize+">").Append(" NEW").Append("</h"+headingSize+"></b>");
        foreach (var n in wn.whatsNew) {
          sb.Append("<p style='font-size:"+fontSize+"px'> •\t").Append(n).Append("</p>");
        }

        sb.Append("<b><h"+headingSize+">").Append(" UPDATED").Append("</h"+headingSize+"></b>");
        foreach (var u in wn.whatsUpdated) {
          sb.Append("<p style='font-size:"+fontSize+"px'> •\t").Append(u).Append("</p>");
        }

        sb.Append("<b><h"+headingSize+">").Append(" FIXED").Append("</h"+headingSize+"></b>");
        foreach (var f in wn.whatsFixed) {
          sb.Append("<p style='font-size:"+fontSize+"px'> •\t").Append(f).Append("</p>");
        }  
      }

      NSError error = null;
      var htmlString = new NSAttributedString (
				NSData.FromString(sb.ToString()),
				new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8},
				ref error
			);
      content.AttributedText = htmlString;
			
			contentHolder.AddSubview(content);
			infoView.AddSubview(viewHeader);
			infoView.AddSubview(closeCorner);
			infoView.BringSubviewToFront(closeCorner);
			infoView.AddSubview(closeBottom);
			infoView.AddSubview(contentHolder);
		}
	}
}

