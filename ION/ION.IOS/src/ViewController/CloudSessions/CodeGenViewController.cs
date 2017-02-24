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
	public partial class CodeGenViewController : BaseIONViewController {
		public AccessRequestManager requestControl;
		
		public CodeGenViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
			Title = Util.Strings.Remote.ACCESSCODE;				
			
			setupInitialView();
		}
		
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			
			
			requestControl = new AccessRequestManager(codeGenHolder);
			codeGenHolder.AddSubview(requestControl.accessView);
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

