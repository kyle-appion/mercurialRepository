using System;
using Foundation;
using UIKit;
using CoreGraphics;
using ION.IOS.Util;
using ION.IOS.App;
using ION.IOS.ViewController.WebServices;

namespace ION.IOS.ViewController.AccessRequest {
	public partial class AccessRequestViewController : BaseIONViewController {
	
		public AccessRequestViewController(IntPtr handle) : base(handle) {		
		}
		public AccessRequestManager requestManager;
		public UILabel loggedOutLabel;
		public UIBarButtonItem online;
		public UIBarButtonItem offline;
		public SessionPayload webServices;
		public bool uploading = false;
		
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      View.BackgroundColor = UIColor.White;
      NavigationItem.Title = Strings.AccessManager.SELF.FromResources();
      
      webServices = new SessionPayload();
      
      var button  = new UIButton(new CGRect(0, 0, 80, 30));
			button.SetTitle("Online", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			button.Layer.BorderWidth = 1f;
			button.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button.TouchUpInside += SwitchOffline;
			online = new UIBarButtonItem(button);
			
      var button2  = new UIButton(new CGRect(0, 0, 80, 30));
			button2.SetTitle("Offline", UIControlState.Normal);
			button2.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button2.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			button2.Layer.BorderWidth = 1f;
			button2.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button2.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button2.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button2.TouchUpInside += SwitchOnline;
			offline = new UIBarButtonItem(button2);
			
      this.NavigationItem.RightBarButtonItem = offline;
      
      loggedOutLabel = new UILabel(new CGRect(0,0,View.Bounds.Width, View.Bounds.Height));
      loggedOutLabel.TextAlignment = UITextAlignment.Center;
      loggedOutLabel.Text = "Must Log In To Use Manager";
      loggedOutLabel.Lines = 0;
      loggedOutLabel.Hidden = true; 
      
  		requestManager = new AccessRequestManager(View,webServices);				
    
			if(string.IsNullOrEmpty(KeychainAccess.ValueForKey("userID"))){
				requestManager.accessView.Hidden = true;
				this.NavigationItem.RightBarButtonItem = null;
			}
			View.AddSubview(requestManager.accessView);			
			View.AddSubview(loggedOutLabel);
		}

		public async void SwitchOnline(object sender, EventArgs e){								
				uploading = true;
				var window = UIApplication.SharedApplication.KeyWindow;
				var rootVC = window.RootViewController as IONPrimaryScreenController;
			
				bool response = await webServices.updateOnlineStatus("1",rootVC);
				if(response){
					this.NavigationItem.RightBarButtonItem = online;
				}
		}
		
		public async void SwitchOffline(object sender, EventArgs e){
				uploading = false;
				var window = UIApplication.SharedApplication.KeyWindow;
				var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				bool response = await webServices.updateOnlineStatus("0",rootVC);
				if(response){
					this.NavigationItem.RightBarButtonItem = offline;
				}
		}
		
		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			if(uploading){
				this.NavigationItem.RightBarButtonItem = online;
			} else {
				this.NavigationItem.RightBarButtonItem = offline;
			}
			
			if(string.IsNullOrEmpty(KeychainAccess.ValueForKey("userID"))){
				requestManager.accessView.Hidden = true;
				loggedOutLabel.Hidden = false;
				this.NavigationItem.RightBarButtonItem.Enabled = false;
			} else {
				requestManager.accessView.Hidden = false;
				loggedOutLabel.Hidden = true;
				this.NavigationItem.RightBarButtonItem.Enabled = true;
			}			
		}
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


