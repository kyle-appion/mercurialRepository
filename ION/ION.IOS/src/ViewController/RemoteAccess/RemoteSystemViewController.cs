using System;
using System.Net;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.App;
using Newtonsoft.Json.Linq;
using System.Text;
using Appion.Commons.Util;
using ION.Core.Net;
using ION.CoreExtensions.Net.Portal;
using ION.IOS.ViewController.AccessRequest;
using ION.IOS.ViewController.CloudSessions;
using System.Linq;
using ION.IOS.Util;

namespace ION.IOS.ViewController.RemoteAccess {
	public partial class RemoteSystemViewController : BaseIONViewController {
		public PortalMenu portalControl;
		public RemoteLoginView loginView;
		public RemoteUserProfileView profileView;
		public RemoteUserRegistration registerView;
		public IosION ion;
		public UIBarButtonItem settingsButton;
		public UIBarButtonItem register;
		WebClient wc;
		public RemoteSystemViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
			Title = Util.Strings.Remote.APPIONPORTAL;				
      
      ion = AppState.context as IosION;
			wc = new WebClient(); 
			wc.Proxy = null;

      var button  = new UIButton(new CGRect(0, 0, 40, 40));
			button.SetImage(UIImage.FromBundle("ic_settings"),UIControlState.Normal);
			button.BackgroundColor = UIColor.Clear;
			button.TouchUpInside += flipAccountViews;
			settingsButton = new UIBarButtonItem(button);		

     var button2  = new UIButton(new CGRect(0, 0, 70, 30));
			button2.SetTitle("Register", UIControlState.Normal);
			button2.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button2.BackgroundColor = UIColor.Clear;
			//button2.Layer.BorderWidth = 1f;
			button2.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button2.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.Clear;};
			button2.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.Clear;};
			button2.TouchUpInside += (sender, e) => {userRegistrationSetup();};
			register = new UIBarButtonItem(button2); 
     
			setupInitialView();
		}
		
		/// <summary>
		/// checks if a user has opted to stay logged in or not
		/// shows their access list or a login view depending
		/// </summary>
		public async void setupInitialView(){
			await Task.Delay(TimeSpan.FromMilliseconds(1));
      
      var needsLogin = true;
      if (ion.settings.portal.rememberMe) {
        needsLogin = !await CredentialsCheck(ion.settings.portal.rememberMe, ion.settings.portal.username, ion.settings.portal.password);
      }
			
      if(needsLogin) {
        loginView = new RemoteLoginView(remoteHolderView);
        loginView.submitButton.TouchUpInside += credentialsCheck;
        remoteHolderView.AddSubview(loginView.loginView);
        this.NavigationItem.RightBarButtonItem = register;
			};
		}			
		
		/// <summary>
		/// Removes the saved settings for a user and returns them to the log in screen
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void LogOutUser(object sender, EventArgs e){
			var userID = KeychainAccess.ValueForKey("userID");

      await ion.portal.LogoutAsync();

      ion.settings._portal.rememberMe = false;
      ion.settings._portal.userId = 0;
      ion.settings._portal.username = "";
      ion.settings._portal.password = "";
			
			loginView = new RemoteLoginView(remoteHolderView);
      loginView.submitButton.TouchUpInside += credentialsCheck;
			remoteHolderView.AddSubview(loginView.loginView);
			portalControl.portalView.RemoveFromSuperview();
			portalControl.uploadButton.TouchUpInside -= showUploads;
			portalControl.codeButton.TouchUpInside -= showCodeManager;
			portalControl.accessButton.TouchUpInside -= showAccessManager;
			portalControl.remoteButton.TouchUpInside -= showRemoteViewing;
			
			portalControl = null;

			profileView.profileView.RemoveFromSuperview();
			profileView = null;
      this.NavigationItem.RightBarButtonItem = register;
		}
		/// <summary>
		/// Rotates the views to display either the profile page or the selection page
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void flipAccountViews(object sender, EventArgs e){
			if(profileView.profileView.Hidden){
        UIView.Transition(
          fromView:portalControl.portalView,
          toView:profileView.profileView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	portalControl.portalView.Hidden = true;
          	profileView.profileView.Hidden = false;
            remoteHolderView.SendSubviewToBack(portalControl.portalView);
            remoteHolderView.BringSubviewToFront(profileView.profileView);
          }
        );
			}	else {
        UIView.Transition(
          fromView:profileView.profileView,
          toView:portalControl.portalView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	profileView.profileView.Hidden = true;
          	portalControl.portalView.Hidden = false;
            remoteHolderView.SendSubviewToBack(profileView.profileView);
            remoteHolderView.BringSubviewToFront(portalControl.portalView);
          }
        );
			}			
		}		
		/// <summary>
		/// Validates the username and password a user enters and logs them in if correct
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
 		public async void credentialsCheck(object sender, EventArgs e){
      loginView.loadingLogin = null;
  
      loginView.loadingLogin = new UIActivityIndicatorView(new CGRect(0,0, loginView.loginView.Bounds.Width, loginView.loginView.Bounds.Height));
      loginView.loadingLogin.BackgroundColor = UIColor.Black;
      loginView.loadingLogin.Alpha = .8f;
      loginView.loginView.AddSubview(loginView.loadingLogin);
      loginView.loginView.BringSubviewToFront(loginView.loadingLogin);
      loginView.loadingLogin.StartAnimating();
      
      if(string.IsNullOrEmpty(loginView.userName.Text) || string.IsNullOrEmpty(loginView.password.Text)){
        loginView.loadingLogin.StopAnimating();
        return; 
      }
        
      await CredentialsCheck(loginView.checkMark, loginView.userName.Text, loginView.password.Text);
      
      loginView.loadingLogin.StopAnimating();
      loginView.loginView.RemoveFromSuperview();
      loginView = null;
    }
    
    private async Task<bool> CredentialsCheck(bool remember, string username, string password) {
      try {
        var response = await ion.portal.RequestLoginAsync(username, password);
        
        if (response.success) {
          if (remember) {
            ion.settings.portal.rememberMe = true;
            ion.settings.portal.username = response.result.username;
            ion.settings.portal.password = response.result.password;
          }
          
          portalControl = new PortalMenu(remoteHolderView);
          portalControl.uploadButton.TouchUpInside += showUploads;
          portalControl.codeButton.TouchUpInside += showCodeManager;
          portalControl.accessButton.TouchUpInside += showAccessManager;   
          portalControl.remoteButton.TouchUpInside -= showRemoteViewing;
          portalControl.remoteButton.TouchUpInside += showRemoteViewing;
          
          remoteHolderView.AddSubview(portalControl.portalView);
          profileView = new RemoteUserProfileView(remoteHolderView);
          profileView.logoutButton.TouchUpInside += LogOutUser;
          
          remoteHolderView.AddSubview(profileView.profileView);
          this.NavigationItem.RightBarButtonItem = settingsButton;
          return true;
        } else {
          var window = UIApplication.SharedApplication.KeyWindow;
          var rootVC = window.RootViewController as IONPrimaryScreenController;
          switch (response.error) {
            case IONPortalService.EError.InternalError: {
              var alert = UIAlertController.Create ("Log In", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
              alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
              rootVC.PresentViewController (alert, animated: true, completionHandler: null);
            } break;
            default: {
              var failMessage = "US Failed to login: US";
              ion.settings.portal.rememberMe = false;
              var alert = UIAlertController.Create ("Log In", failMessage, UIAlertControllerStyle.Alert);
              alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
              rootVC.PresentViewController (alert, animated: true, completionHandler: null);  
            } break;
          }
          return false;
        }
      } catch (Exception e) {
        Log.E(this, "Failed to validate credentials", e);
        return false;
      }
		}

		/// <summary>
		/// User is registering an account so it loads the view for that
		/// </summary>
		public void userRegistrationSetup(){
			this.NavigationItem.RightBarButtonItem = null;
			registerView = new RemoteUserRegistration(remoteHolderView);
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{
          registerView.regView.Frame = new CGRect(.05 * remoteHolderView.Bounds.Width, .05 * remoteHolderView.Bounds.Height, .9 * remoteHolderView.Bounds.Width, .85 * remoteHolderView.Bounds.Height);
        }, 
        () => {});
			
			registerView.submitButton.TouchUpInside += submitUserForRegistration;
			registerView.cancelbutton.TouchUpInside += (sender, e) => {
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{
          registerView.regView.Frame = new CGRect(.05 * remoteHolderView.Bounds.Width, remoteHolderView.Bounds.Height, .9 * remoteHolderView.Bounds.Width, .85 * remoteHolderView.Bounds.Height);
        }, 
        () => {
					registerView.regView.RemoveFromSuperview();
					registerView = null;
					this.NavigationItem.RightBarButtonItem = register;
				});
			};
			remoteHolderView.AddSubview(registerView.regView);
		}

		
		/// <summary>
		/// Checks the fields for a user registration and submits it if everything is filled out
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void submitUserForRegistration(object sender, EventArgs e){
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
			if(string.IsNullOrEmpty(registerView.email.Text) || string.IsNullOrEmpty(registerView.password.Text) || string.IsNullOrEmpty(registerView.confirmPassword.Text)){
				var alert = UIAlertController.Create ("User Registration", "Please enter a value for each field", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				return;
			}
			
			if(registerView.password.Text != registerView.confirmPassword.Text){
				var alert = UIAlertController.Create ("User Registration", "Passwords must match", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				return;
			}
			
			if(!registerView.password.Text.Any(char.IsUpper) || registerView.password.Text.Length < 8){
				var alert = UIAlertController.Create ("User Registration", "Passwords must be 8 characters long and have at least 1 uppercase character", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				return;
			}
			
			if(!registerView.email.ToString().Contains("@")){
				var alert = UIAlertController.Create ("User Registration", "Invalid email address entered", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				return;
			}
			
			registerView.loadingRegistration = new UIActivityIndicatorView(new CGRect(0, 0, registerView.regView.Bounds.Width, registerView.regView.Bounds.Height));
			registerView.loadingRegistration.BackgroundColor = UIColor.Black;
			registerView.loadingRegistration.Alpha = .8f;
			registerView.regView.AddSubview(registerView.loadingRegistration);
			registerView.regView.BringSubviewToFront(registerView.loadingRegistration);
			registerView.loadingRegistration.StartAnimating();

      // todo ahodder@appioninc.com: this needs to be localized. also, we can use error codes instead of strings
      var response = await ion.portal.RegisterUser(registerView.email.Text, registerView.password.Text);
      
      registerView.loadingRegistration.StopAnimating();
      
      if (response.success) {
        var alert = UIAlertController.Create("US User Registration", "US Success", UIAlertControllerStyle.Alert);
        alert.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null));
        rootVC.PresentViewController(alert, animated: true, completionHandler: null);
        
        loginView.userName.Text = registerView.email.Text;
        loginView.password.Text = registerView.password.Text;
      } else {
        switch (response.error) {
          case IONPortalService.EError.InternalError: {
            var alert = UIAlertController.Create("US Internal Error", "US There was an internal error", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null));
            rootVC.PresentViewController(alert, animated: true, completionHandler: null);
            break;
          }
          case IONPortalService.EError.ServerError: {
            var alert = UIAlertController.Create("US Server Error", "US There was a server error", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null));
            rootVC.PresentViewController(alert, animated: true, completionHandler: null);
            break;
          }
        }
      }
		}

		public void showUploads(object sender, EventArgs e){
				portalControl.uploadButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			  var vc = InflateViewController<SessionsViewController>(VC_CLOUD_SESSIONS);

        NavigationController.PushViewController(vc, true);			
		}

		public void showCodeManager(object sender, EventArgs e){
				portalControl.codeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			  var vc = InflateViewController<CodeGenViewController>(VC_CLOUD_CODES);

        NavigationController.PushViewController(vc, true);			
		}

		public void showAccessManager(object sender, EventArgs e){
				portalControl.accessButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			  var vc = InflateViewController<ViewingControlViewController>(VC_CLOUD_ACCESS);

        NavigationController.PushViewController(vc, true);			
		}
	
		public void showRemoteViewing(object sender, EventArgs e){
				portalControl.remoteButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			  var vc = InflateViewController<RemoteViewingViewController>(VC_CLOUD_REMOTE);

        NavigationController.PushViewController(vc, true);			
		}	
	
		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
		}
    
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
