using System;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using ION.Core.Net;
using Newtonsoft.Json.Linq;
using ION.IOS.ViewController.CloudSessions;
using Foundation;
using ION.IOS.App;

namespace ION.IOS.ViewController.RemoteAccess {

	public class PortalMenu {
		public UIView portalView;
		public UILabel menuHeader;
		public UIButton uploadButton;
		public UIButton codeButton;
		public UIButton accessButton;
		public UIButton remoteButton;
		public UIButton webPortalButton; 
	
		public PortalMenu(UIView parentView) {
			portalView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			portalView.BackgroundColor = UIColor.White;
			
			menuHeader = new UILabel(new CGRect(0, 0, parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			menuHeader.TextAlignment = UITextAlignment.Center;
			menuHeader.Font = UIFont.BoldSystemFontOfSize(20);
			menuHeader.Text = "Portal Menu";
			menuHeader.BackgroundColor = UIColor.Black;
			menuHeader.TextColor = UIColor.FromRGB(255, 215, 101);
			
			uploadButton = new UIButton(new CGRect(.05 * parentView.Bounds.Width, .15 * parentView.Bounds.Height, .4 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			uploadButton.SetTitle("Upload Session", UIControlState.Normal);
			uploadButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			uploadButton.Layer.BorderWidth = 1f;
			uploadButton.Layer.CornerRadius = 5f;
			uploadButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			uploadButton.TouchDown += (sender, e) => {uploadButton.BackgroundColor = UIColor.Blue;};
			uploadButton.TouchUpOutside += (sender, e) => {uploadButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			codeButton = new UIButton(new CGRect(.55 * parentView.Bounds.Width, .15 * parentView.Bounds.Height, .4 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			codeButton.SetTitle("Access Codes", UIControlState.Normal);
			codeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			codeButton.Layer.BorderWidth = 1f;   
			codeButton.Layer.CornerRadius = 5f;			
			codeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			codeButton.TouchDown += (sender, e) => {codeButton.BackgroundColor = UIColor.Blue;};
			codeButton.TouchUpOutside += (sender, e) => {codeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

			accessButton = new UIButton(new CGRect(.05 * parentView.Bounds.Width, .3 * parentView.Bounds.Height, .4 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			accessButton.SetTitle("Manage Access", UIControlState.Normal);
			accessButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			accessButton.Layer.BorderWidth = 1f;
			accessButton.Layer.CornerRadius = 5f;
			accessButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			accessButton.TouchDown += (sender, e) => {accessButton.BackgroundColor = UIColor.Blue;};
			accessButton.TouchUpOutside += (sender, e) => {accessButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
						
			remoteButton = new UIButton(new CGRect(.55 * parentView.Bounds.Width, .3 * parentView.Bounds.Height, .4 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			remoteButton.SetTitle("Remote Viewing", UIControlState.Normal);
			remoteButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			remoteButton.Layer.BorderWidth = 1f;
			remoteButton.Layer.CornerRadius = 5f;
			remoteButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			remoteButton.TouchDown += (sender, e) => {remoteButton.BackgroundColor = UIColor.Blue;};
			remoteButton.TouchUpOutside += (sender, e) => {remoteButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			webPortalButton = new UIButton(new CGRect(.05 * parentView.Bounds.Width, .8 * parentView.Bounds.Height, .9 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			webPortalButton.SetTitle("Web Portal", UIControlState.Normal);
			webPortalButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			webPortalButton.Layer.BorderWidth = 1f;
			webPortalButton.Layer.CornerRadius = 5f;
			webPortalButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			webPortalButton.TouchDown += (sender, e) => {webPortalButton.BackgroundColor = UIColor.Blue;};
			webPortalButton.TouchUpOutside += (sender, e) => {webPortalButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			webPortalButton.TouchUpInside += (sender, e) => {
				webPortalButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				var uEmail = KeychainAccess.ValueForKey("userEmail");
				var uPword = KeychainAccess.ValueForKey("userPword");
				
				var url = "http://portal.appioninc.com/joomla/modules/mod_processing/appWebLogin.php?usrEmail=" + uEmail + "&usrPass=" + uPword;
				
				if(UIDevice.CurrentDevice.CheckSystemVersion(10,0)){
					UIApplication.SharedApplication.OpenUrl(new NSUrl(url),new NSDictionary(), null);
				} else {
					UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
				}
			};
			
			portalView.AddSubview(menuHeader);
			portalView.AddSubview(uploadButton);
			portalView.AddSubview(codeButton);
			portalView.AddSubview(accessButton);
			portalView.AddSubview(remoteButton);
			portalView.AddSubview(webPortalButton);
		}
	}
}
