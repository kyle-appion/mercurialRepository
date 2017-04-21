
using System;
using UIKit;
using CoreGraphics;
using ION.IOS.Util;
using ION.IOS.App;
using ION.Core.Net;
using System.Threading.Tasks;
using AudioToolbox;
using ION.Core.App;

namespace ION.IOS.ViewController.AccessRequest {
	public partial class AccessRequestViewController : BaseIONViewController {
	
		public AccessRequestViewController(IntPtr handle) : base(handle) {		
		}
		public AccessRequestManager requestManager;
		public AccessSettings settingsManager;
		public UILabel loggedOutLabel;
		public UIBarButtonItem settingsButton;
		public WebPayload webServices; 
		public IosION ion;
		public DateTime startedViewing;
		
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      InitNavigationBar("ic_nav_workbench", false); 
      backAction = () => {
        root.navigation.ToggleMenu();
      };
			ion = AppState.context as IosION;
      NavigationItem.Title = Strings.AccessManager.SELF.FromResources();
      
      webServices = ion.webServices;
      
      var button  = new UIButton(new CGRect(0, 0, 40, 40));
			button.SetImage(UIImage.FromBundle("ic_settings"),UIControlState.Normal);
			button.TouchUpInside += SetupAccessView;
			settingsButton = new UIBarButtonItem(button);			
			
      this.NavigationItem.RightBarButtonItem = settingsButton;
      
      loggedOutLabel = new UILabel(new CGRect(0,0,View.Bounds.Width, View.Bounds.Height));
      loggedOutLabel.TextAlignment = UITextAlignment.Center;
      loggedOutLabel.Text = "Must Log In To Use Manager";
      loggedOutLabel.Lines = 0;
      loggedOutLabel.Hidden = true;
      accessHolderView.Bounds = View.Bounds;
      
  		requestManager = new AccessRequestManager(accessHolderView);
    	settingsManager = new AccessSettings(accessHolderView);
    	
			if(string.IsNullOrEmpty(KeychainAccess.ValueForKey("userID"))){
				requestManager.accessView.Hidden = true;
				this.NavigationItem.RightBarButtonItem = null;
			}
			accessHolderView.AddSubview(requestManager.accessView);
			accessHolderView.AddSubview(settingsManager.settingsView);			
			accessHolderView.AddSubview(loggedOutLabel);
		}
		
		public void SetupAccessView(object sender, EventArgs e){								
			if(settingsManager.settingsView.Hidden){
        UIView.Transition(
          fromView:requestManager.accessView,
          toView:settingsManager.settingsView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	requestManager.accessView.Hidden = true;
          	settingsManager.settingsView.Hidden = false;
            accessHolderView.SendSubviewToBack(requestManager.accessView);
            accessHolderView.BringSubviewToFront(settingsManager.settingsView);
          }
        );
			}	else {
        UIView.Transition(
          fromView:settingsManager.settingsView,
          toView:requestManager.accessView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	settingsManager.settingsView.Hidden = true;
          	requestManager.accessView.Hidden = false;
            accessHolderView.SendSubviewToBack(settingsManager.settingsView);
            accessHolderView.BringSubviewToFront(requestManager.accessView);
          }
        );
			}
		}
		
		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			Console.WriteLine("AccessRequestViewController userID stored " + KeychainAccess.ValueForKey("userID"));
			if(string.IsNullOrEmpty(KeychainAccess.ValueForKey("userID"))){
				requestManager.pendingTable.DataSource = null;
				requestManager.pendingTable.ReloadData();
        UIView.Transition(
          fromView:settingsManager.settingsView,
          toView:requestManager.accessView,
          duration:.1,
          options: UIViewAnimationOptions.TransitionNone,
          completion: () => {
          	settingsManager.settingsView.Hidden = true;
          	requestManager.accessView.Hidden = true;
						loggedOutLabel.Hidden = false;
            accessHolderView.SendSubviewToBack(settingsManager.settingsView);
            accessHolderView.BringSubviewToFront(requestManager.accessView);
          }
        );			
				this.NavigationItem.RightBarButtonItem = null;
			} else {
				requestManager.getAllRequests(this,EventArgs.Empty);
				requestManager.accessView.Hidden = false;
				settingsManager.settingsView.Hidden = false;
				this.NavigationItem.RightBarButtonItem = settingsButton;
				loggedOutLabel.Hidden = true;
			}
		}
				
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
