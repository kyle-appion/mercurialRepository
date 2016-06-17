using System;
using CoreGraphics;
using Foundation;
using UIKit;


namespace ION.IOS.App {

	public class WhatsNewView
	{
		public UIView infoView;
		public UILabel viewHeader;
		public UILabel appVersion;
		public UIScrollView contentHolder;
		public UITextView content;
		public UIButton closeCorner;
		public UIButton closeBottom;
		
		public WhatsNewView(UIView parentView, UIViewController parentVC)
		{
			infoView = new UIView(new CGRect(.1 * parentView.Bounds.Width, .15 * parentView.Bounds.Height, .8 * parentView.Bounds.Width, .8 * parentView.Bounds.Height));
			infoView.Layer.CornerRadius = 5;
			infoView.ClipsToBounds = true;
			infoView.BackgroundColor = UIColor.White;
			infoView.Layer.BorderWidth = 1f;
			infoView.Tag = 99;
			
			viewHeader = new UILabel(new CGRect(0,0,infoView.Bounds.Width, 90));
			viewHeader.BackgroundColor = UIColor.FromRGB(9,211,255);
			viewHeader.ClipsToBounds = true;
			viewHeader.AdjustsFontSizeToFitWidth = true;
			viewHeader.TextAlignment = UITextAlignment.Center;
			viewHeader.Font = UIFont.SystemFontOfSize(25);
			viewHeader.Layer.ShadowColor = UIColor.FromRGB(8,199,240 ).CGColor;
			viewHeader.Layer.ShadowOpacity = .5f;
			viewHeader.Layer.ShadowRadius = 1.0f;
			viewHeader.Layer.ShadowOffset = new CGSize(1,1);
			viewHeader.Layer.MasksToBounds = false;
			viewHeader.Text = "What's New this version";
			
			appVersion = new UILabel(new CGRect(0,90,infoView.Bounds.Width,20));
			appVersion.Font = UIFont.BoldSystemFontOfSize(20);
			appVersion.Text = " Version " + NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
			
			closeCorner = new UIButton(new CGRect(viewHeader.Bounds.Width - 45,25,35,35));
			closeCorner.SetImage(UIImage.FromBundle("img_button_blackclosex"),UIControlState.Normal);
			closeCorner.BackgroundColor = UIColor.Clear;
			closeCorner.TouchUpInside += (sender, e) => {
				infoView.RemoveFromSuperview();
			};
			
			contentHolder = new UIScrollView(new CGRect(0,110,infoView.Bounds.Width,infoView.Bounds.Height - 160));
			contentHolder.Layer.CornerRadius = 5;
			contentHolder.ClipsToBounds = true;
			contentHolder.ContentSize = new CGSize(contentHolder.Bounds.Width,contentHolder.Bounds.Height);			

			
			closeBottom = new UIButton(new CGRect(0,infoView.Bounds.Height - 50,infoView.Bounds.Width,50));
			closeBottom.ClipsToBounds = true;
			closeBottom.SetTitle("Close",UIControlState.Normal);
			closeBottom.SetTitleColor(UIColor.Black,UIControlState.Normal);
			closeBottom.Layer.BorderWidth = 1f;
			closeBottom.Layer.BorderColor = UIColor.LightGray.CGColor;
			closeBottom.TouchUpInside += (sender, e) => {
				infoView.RemoveFromSuperview();
			};


			content = new UITextView(new CGRect(0,0,contentHolder.Bounds.Width,contentHolder.Bounds.Height));
			content.Layer.CornerRadius = 5;
			content.ClipsToBounds = true;
			content.UserInteractionEnabled = false;
			content.Font = UIFont.SystemFontOfSize(15);
			
			
			
			contentHolder.AddSubview(content);
			infoView.AddSubview(viewHeader);
			infoView.AddSubview(closeCorner);
			infoView.AddSubview(appVersion);
			infoView.BringSubviewToFront(closeCorner);
			infoView.AddSubview(closeBottom);
			infoView.AddSubview(contentHolder);
		}
	}
}

