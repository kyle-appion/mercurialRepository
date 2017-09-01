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
using Newtonsoft.Json;
using Foundation;

namespace ION.IOS.ViewController.CloudSessions {
	public partial class RemoteViewingViewController : BaseIONViewController {
		remoteSelectionView selectionView;
		public UIBarButtonItem uploadButton;
		public IosION ion;	
		UIButton startButton;
		UIButton stopButton;
		
		public RemoteViewingViewController(IntPtr handle) : base(handle) {
		
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
			
			ion = AppState.context as IosION;
			
			///////THIS BUTTON WILL BEGIN UPLOADING A USER'S LAYOUT AND THEN BE REPLACED BY THE SECOND BUTTON
      startButton  = new UIButton(new CGRect(0, 0, 120, 30));
			startButton.SetTitle("Start Upload", UIControlState.Normal);
			startButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			startButton.BackgroundColor = UIColor.Clear;
			


			///////THIS BUTTON WILL STOP UPLOADING A USER'S LAYOUT AND THEN BE REPLACED BY THE FIRST BUTTON
      stopButton  = new UIButton(new CGRect(0, 0, 120, 30));
			stopButton.SetTitle("Stop Upload", UIControlState.Normal);
			stopButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			stopButton.BackgroundColor = UIColor.Clear;
			
			startButton.TouchDown += (sender, e) => {startButton.BackgroundColor = UIColor.Blue;};
			startButton.TouchUpOutside += (sender, e) => {startButton.BackgroundColor = UIColor.Clear;};
			startButton.TouchUpInside += (sender, e) => {startButton.BackgroundColor = UIColor.Clear; selectionView.remoteMenuButton.Enabled = false;};
			startButton.TouchUpInside += (sender, e) => {startUploadStatus();};			
			
			stopButton.TouchDown += (sender, e) => {stopButton.BackgroundColor = UIColor.Blue;};
			stopButton.TouchUpOutside += (sender, e) => {stopButton.BackgroundColor = UIColor.Clear;};
			stopButton.TouchUpInside += (sender, e) => {stopButton.BackgroundColor = UIColor.Clear; selectionView.remoteMenuButton.Enabled = true;};
			stopButton.TouchUpInside += (sender, e) => {stopUploadStatus();};
			
			if(ion.webServices.uploading){
				uploadButton = new UIBarButtonItem(stopButton);
			}else {
				uploadButton = new UIBarButtonItem(startButton);
			}
			
			this.NavigationItem.RightBarButtonItem = uploadButton;
							
			setupInitialView();
		}
		
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			selectionView = new remoteSelectionView(remoteHolderView);
			
			remoteHolderView.AddSubview(selectionView.selectionView);
			
			selectionView.remoteMenuButton.TouchUpInside += (sender, e) => {
				this.NavigationItem.RightBarButtonItem.Enabled = false;
			};
			selectionView.fullMenuButton.TouchUpInside += (sender, e) => {
				this.NavigationItem.RightBarButtonItem.Enabled = true;
			};			
		}
		
		public async void startUploadStatus(){	
			var window = UIApplication.SharedApplication.KeyWindow;
    	var rootVC = window.RootViewController as IONPrimaryScreenController;
			var userID = KeychainAccess.ValueForKey("userID");
		
			var uploadingDevice = new PhysicalDevice {
				name = UIDevice.CurrentDevice.Name,
				model = UIDevice.CurrentDevice.Model,
				uniqueIdentifier = UIDevice.CurrentDevice.IdentifierForVendor.ToString(),
			};
			////CREATE OR UPDATE A LAYOUT ENTRY FOR A DEVICE UNDER THE LOGGED IN ACCOUNT AND SET THAT LAYOUT ENTRY TO BE UPDATED THIS SESSION
			var feedback = await ion.webServices.createSystemLayout(ion, userID, uploadingDevice.uniqueIdentifier, uploadingDevice.name);
			
			if(feedback != null){				
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);

				var success = response.GetValue("success");
				var errorMessage = response.GetValue("message").ToString();
				
				if(success.ToString() == "true"){
					var layoutID = response.GetValue("layoutid").ToString();
					KeychainAccess.SetValueForKey(layoutID,"layoutid");
					
					ion.webServices.uploading = true;
				
					var alert = UIAlertController.Create ("Begin Upload", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
					startUploading();					
					uploadButton = new UIBarButtonItem(stopButton);
					this.NavigationItem.RightBarButtonItem = uploadButton;
				}	 else {
					////AN UPLOADING SESSION FAILED TO BE UPDATED. MOST LIKELY DUE TO A SECOND DEVICE STARTING AN UPLOAD ON THE SAME ACCOUNT			
					var alert = UIAlertController.Create ("Start Upload", errorMessage.ToString(), UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);	
				}	
				
			} else {
				var alert = UIAlertController.Create ("Begin Upload", "Unable to begin upload. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);			
			}
			
			selectionView.GetAccessList();			
		}
		
		public async void stopUploadStatus(){
			var window = UIApplication.SharedApplication.KeyWindow;
    	var rootVC = window.RootViewController as IONPrimaryScreenController;
    	
			var userID = KeychainAccess.ValueForKey("userID");		
			var layoutID = KeychainAccess.ValueForKey("layoutid");		
		
			var feedback = await ion.webServices.updateOnlineStatus(userID, layoutID);
			
			if(feedback != null){
				KeychainAccess.SetValueForKey(null,"layoutid");
				
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);

				ion.webServices.uploading = false;
				uploadButton = new UIBarButtonItem(startButton);
				this.NavigationItem.RightBarButtonItem = uploadButton;

				var errorMessage = response.GetValue("message").ToString();
				
				var alert = UIAlertController.Create ("End Upload", errorMessage, UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);	
			} else {
				var alert = UIAlertController.Create ("End Upload", "There was an issue during the status update. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);			
			}
			selectionView.GetAccessList();		
			
		}
		
		public override void ViewDidAppear(bool animated) {
			if(selectionView != null){
				if(ion.webServices.remoteViewing){
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
			var userID = KeychainAccess.ValueForKey("userID");
			var layoutID = KeychainAccess.ValueForKey("layoutid");		
			
			/////A LAYOUT ID WAS CREATED OR RETRIEVED AND CAN BE USED FOR UPLOADING THE CURRENT DEVICE LAYOUT
			while(ion.webServices.uploading){
				var feedback = await ion.webServices.uploadSystemLayout(ion, userID, layoutID);
				if(feedback != null){
					var textResponse = await feedback.Content.ReadAsStringAsync();
					JObject response = JObject.Parse(textResponse);
					var success = response.GetValue("success").ToString();
					
					if(success.ToString() == "false"){
						var window = UIApplication.SharedApplication.KeyWindow;
			    	var rootVC = window.RootViewController as IONPrimaryScreenController;
					
						ion.webServices.uploading = false;
						
						var errorMessage = response.GetValue("message").ToString();   
						
						var alert = UIAlertController.Create ("End Upload", errorMessage, UIAlertControllerStyle.Alert);
						alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
						rootVC.PresentViewController (alert, animated: true, completionHandler: null);
						selectionView.GetAccessList();
						uploadButton = new UIBarButtonItem(startButton);
						this.NavigationItem.RightBarButtonItem = uploadButton;											
					} else {
            var logging = response.GetValue("logging").ToString();
          
						/////CHECK LOGGING STATUS TO BEGIN DATA LOGGING FROM REMOTE CONTROL
						if(logging == "1" && !ion.dataLogManager.isRecording){
							await ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
						} 
						/////CHECK LOGGING STATUS TO END DATA LOGGING FROM REMOTE CONTROL
						else if (logging == "0" && ion.dataLogManager.isRecording){
							await ion.dataLogManager.StopRecording();
						}
					}
				} else {
					ion.webServices.uploading = false;
				}
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		
		[Foundation.Preserve(AllMembers = true)]
		public class PhysicalDevice{
			public PhysicalDevice(){}
			[JsonProperty("deviceName")]
			public string name { get; set; }
			[JsonProperty("deviceModel")]
			public string model { get; set; }
			[JsonProperty("deviceIdentifier")]
			public string uniqueIdentifier { get; set; }			
		}
	}
}

