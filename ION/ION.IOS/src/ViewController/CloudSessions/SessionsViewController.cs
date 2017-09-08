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

namespace ION.IOS.ViewController.CloudSessions {
	public partial class SessionsViewController : BaseIONViewController {
		public AccessSettings settingsManager;
	
		public SessionsViewController(IntPtr handle) : base(handle) {
		}	
		
		
		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
			Title = Util.Strings.Remote.UPLOADSESSION;				
			
			setupInitialView();		
		}
		
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			
			settingsManager = new AccessSettings(cloudSessionHolder);			
			cloudSessionHolder.AddSubview(settingsManager.settingsView);
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

