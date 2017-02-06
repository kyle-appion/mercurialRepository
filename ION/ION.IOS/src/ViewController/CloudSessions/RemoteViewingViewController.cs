using System;
using System.Net;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.App;
using Newtonsoft.Json.Linq;
using System.Text;
using ION.Core.Net;
using ION.IOS.ViewController.AccessRequest;
using ION.IOS.ViewController.RemoteAccess;

namespace ION.IOS.ViewController.CloudSessions {
	public partial class RemoteViewingViewController : BaseIONViewController {
		remoteSelectionView selectionView;
		
	
		public RemoteViewingViewController(IntPtr handle) : base(handle) {
		
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
			
			setupInitialView();
		}
		
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			selectionView = new remoteSelectionView(remoteHolderView);
			
			remoteHolderView.AddSubview(selectionView.selectionView);
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

