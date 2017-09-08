using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Walkthrough {

	public class WalkthroughHelp {
		
		public UIView helpView;
		public UILabel headerLabel;
		public UILabel contentLabel;
		public UIButton closeButton;
		
		public WalkthroughHelp(UIView parentView) {
			helpView = new UIView(new CGRect(.05 * parentView.Bounds.Width, .25 * parentView.Bounds.Height, .9 * parentView.Bounds.Width, .4 * parentView.Bounds.Height));
			helpView.BackgroundColor = UIColor.White;
			helpView.Layer.CornerRadius = 5f;
			helpView.Layer.BorderWidth = 3f;
			helpView.ClipsToBounds = true;
			
			headerLabel = new UILabel(new CGRect(0, 0, helpView.Bounds.Width, .2 * helpView.Bounds.Height));
			headerLabel.ClipsToBounds = true;
			headerLabel.Layer.BorderWidth = 1f;
			headerLabel.TextAlignment = UITextAlignment.Center;
			headerLabel.Text = "App Walkthrough";

			
			contentLabel = new UILabel(new CGRect(.1 * helpView.Bounds.Width, .2 * helpView.Bounds.Height, .8 * helpView.Bounds.Width, .6 * helpView.Bounds.Height));
			contentLabel.ClipsToBounds = true;
			contentLabel.Lines = 0;
			contentLabel.Text = "This walkthrough will introduce you to the primary features of the ION HVAC/R app. If you need to access the walkthrough at a later point, it will be found in the 'Help' section of the main menu";
			contentLabel.AdjustsFontSizeToFitWidth = true;
			
			closeButton = new UIButton(new CGRect(0, .8 * helpView.Bounds.Height, helpView.Bounds.Width, .2 * helpView.Bounds.Height));
			closeButton.SetTitle("Close", UIControlState.Normal);
			closeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			closeButton.Layer.BorderWidth = 1f;
			closeButton.ClipsToBounds = true;
			
			closeButton.TouchDown += (sender, e) => {closeButton.BackgroundColor = UIColor.Blue;};
			closeButton.TouchUpOutside += (sender, e) => {closeButton.BackgroundColor = UIColor.White;};
			closeButton.TouchUpInside += (sender, e) => {helpView.Hidden = true; closeButton.BackgroundColor = UIColor.White;};
			
			helpView.AddSubview(headerLabel);
			helpView.AddSubview(contentLabel);
			helpView.AddSubview(closeButton);
		}
		
	}
}
