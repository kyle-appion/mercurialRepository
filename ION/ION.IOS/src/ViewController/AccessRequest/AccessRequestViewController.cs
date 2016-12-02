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
      
  		requestManager = new AccessRequestManager(accessHolderView,webServices);				
    	settingsManager = new AccessSettings(accessHolderView, webServices);
    	settingsManager.onlineButton.TouchUpInside += (sender, e) => {
				uploadTimer();
			};
    	
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
			
			if(string.IsNullOrEmpty(KeychainAccess.ValueForKey("userID"))){
				requestManager.accessView.Hidden = true;
				loggedOutLabel.Hidden = false;
			} else {
				requestManager.accessView.Hidden = false;
				loggedOutLabel.Hidden = true;
			}
		}
		
		public async void uploadTimer(){			
			settingsManager.onlineButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			startedViewing = DateTime.Now;
			if(!webServices.uploading){
				webServices.uploading = true;
				settingsManager.onlineButton.SetTitle("Stop Remote", UIControlState.Normal);
				await webServices.updateOnlineStatus("1");
			} else {
				webServices.uploading = false;
				settingsManager.onlineButton.SetTitle("Allow Remote", UIControlState.Normal);
				await webServices.updateOnlineStatus("0");
			}
			while(webServices.uploading){
			  var timeDifference = DateTime.Now.Subtract(startedViewing).Minutes;
			 	SystemSound newSound = new SystemSound (1005);
				if(timeDifference < 30){
					var loggedUser = KeychainAccess.ValueForKey("userID");
					if(!string.IsNullOrEmpty(loggedUser)){
						if(!webServices.webClient.IsBusy){					
							await webServices.uploadSystemLayout();
							await Task.Delay(TimeSpan.FromMilliseconds(1500));
						} else {
							await Task.Delay(TimeSpan.FromMilliseconds(500)); 
						}
					} else {
						webServices.uploading = false;
					}
				} else {
					webServices.uploading = false;
					settingsManager.onlineButton.SetTitle("Allow Remote", UIControlState.Normal);
					var window = UIApplication.SharedApplication.KeyWindow;
		  		var rootVC = window.RootViewController as IONPrimaryScreenController;
					
					var alert = UIAlertController.Create ("Uploading Layout", "Are you still uploading your layout?", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Yes", UIAlertActionStyle.Default, (action) => {
						uploadTimer();
						newSound.Close();
					}));
					alert.AddAction (UIAlertAction.Create ("No", UIAlertActionStyle.Cancel, async (action) => {
						newSound.Close();
						await webServices.updateOnlineStatus("0");
					}));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
					AbsentRemoteTurnoff(alert);

					newSound.PlaySystemSound();
					await Task.Delay(TimeSpan.FromSeconds(2));
					newSound.Close();
				}
			}
		}
		
		public async void AbsentRemoteTurnoff(UIAlertController alert){
			await Task.Delay(TimeSpan.FromSeconds(15));
			alert.DismissViewController(false,null);
			if(!webServices.uploading){
				Console.WriteLine("dismissed alert and user didn't choose to continue uploading");
				await webServices.updateOnlineStatus("0");
			}
		}
		
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


