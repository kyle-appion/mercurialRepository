using System;
using Foundation;
using UIKit;
using CoreGraphics;
using ION.IOS.Util;

namespace ION.IOS.ViewController.AccessRequest {
	public partial class AccessRequestViewController : BaseIONViewController {
	
		public AccessRequestViewController(IntPtr handle) : base(handle) {		
		}
		public AccessRequestManager requestManager;

 		
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      View.BackgroundColor = UIColor.White;
      NavigationItem.Title = Strings.AccessManager.SELF.FromResources();
      
			requestManager = new AccessRequestManager(View);
			
			View.AddSubview(requestManager.accessView);
		}


		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


