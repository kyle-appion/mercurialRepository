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
		public UIBarButtonItem uploadButton;
		public WebPayload webServices;
		public IosION ion;	
	
		public RemoteViewingViewController(IntPtr handle) : base(handle) {
		
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
			
			ion = AppState.context as IosION;
			webServices = ion.webServices;
			
      var button  = new UIButton(new CGRect(0, 0, 120, 30));
			button.SetTitle("Upload Layout", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button.BackgroundColor = UIColor.Clear;
			
			button.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.Clear;};
			button.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.Clear;};
			button.TouchUpInside += (sender, e) => {changeUploadStatus();};
			uploadButton = new UIBarButtonItem(button);
			
			
			this.NavigationItem.RightBarButtonItem = uploadButton;
							
			setupInitialView();
		}
		
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			selectionView = new remoteSelectionView(remoteHolderView);
			
			remoteHolderView.AddSubview(selectionView.selectionView);
		}
		
		public async void changeUploadStatus(){	
		var window = UIApplication.SharedApplication.KeyWindow;
    var rootVC = window.RootViewController as IONPrimaryScreenController;
		
			if(webServices.uploading){
				var feedback = await webServices.updateOnlineStatus("0");
				
				if(feedback != null){
					var textResponse = await feedback.Content.ReadAsStringAsync();
					Console.WriteLine(textResponse);
					//parse the text string into a json object to be deserialized
					JObject response = JObject.Parse(textResponse);
	
					webServices.uploading = false;
					this.NavigationItem.RightBarButtonItem.Title = "Upload Layout";
	
					var errorMessage = response.GetValue("message").ToString();
					
					var alert = UIAlertController.Create ("End Upload", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);	
				} else {
					var alert = UIAlertController.Create ("End Upload", "There was an issue during the status update. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);			
				}			

			} else {
				var feedback = await webServices.updateOnlineStatus("1");
				
				if(feedback != null){				
					var textResponse = await feedback.Content.ReadAsStringAsync();
					Console.WriteLine(textResponse);
					//parse the text string into a json object to be deserialized
					JObject response = JObject.Parse(textResponse);
	
					webServices.uploading = true;
					this.NavigationItem.RightBarButtonItem.Title = "Uploading";

					var errorMessage = response.GetValue("message").ToString();
					
					var alert = UIAlertController.Create ("Begin Upload", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
					
					startUploading();					
	
				} else {
					var alert = UIAlertController.Create ("Begin Upload", "Unable to begin upload. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);			
				}
			}
			selectionView.GetAccessList();
			
		}
		
		public override void ViewDidAppear(bool animated) {
			if(selectionView != null){
				if(webServices.remoteViewing){
					selectionView.fullMenuButton.Hidden = false;
					selectionView.remoteMenuButton.Hidden = true;
				} else {
					selectionView.fullMenuButton.Hidden = true;
					selectionView.remoteMenuButton.Hidden = false;		
				}  
			}
		}
		
		public async void startUploading(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			
			while(webServices.uploading){
				await webServices.uploadSystemLayout();
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

