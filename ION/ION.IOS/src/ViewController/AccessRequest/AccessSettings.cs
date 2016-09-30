using System;
using CoreGraphics;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {

	public class AccessSettings {
		public UIView settingsView;
		public UIButton onlineButton;
		
		
		public AccessSettings(UIView parentView) {
			settingsView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height - 50));
			settingsView.BackgroundColor = UIColor.White;
			settingsView.Hidden = true;
			
			onlineButton = new UIButton(new CGRect(.25 * settingsView.Bounds.Width, .85 * settingsView.Bounds.Height, .5 * settingsView.Bounds.Width, .1 * settingsView.Bounds.Height));
			onlineButton.Layer.CornerRadius = 5f;
			onlineButton.Layer.BorderWidth = 1f;
			onlineButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			onlineButton.SetTitle("Start Uploading", UIControlState.Normal);
			onlineButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			onlineButton.TouchDown += (sender, e) => {onlineButton.BackgroundColor = UIColor.Blue;};
			onlineButton.TouchUpOutside += (sender, e) => {onlineButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			settingsView.AddSubview(onlineButton);
		}
		
	}
}

