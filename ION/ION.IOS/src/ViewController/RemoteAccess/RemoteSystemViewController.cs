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

namespace ION.IOS.ViewController.RemoteAccess {
	public partial class RemoteSystemViewController : BaseIONViewController {
		public remoteSelectionView remoteView;
		public RemoteUserProfileView profileView;
		public RemoteLoginView loginView;
		public RemoteUserRegistration registerView;
		public const string loginUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/applogin.php";
		public IION ion;
		public UIBarButtonItem settingsButton;
		public UIBarButtonItem register;
		WebPayload webServices;
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
      ion = AppState.context;
			wc = new WebClient(); 
			wc.Proxy = null;
			var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
      webServices = appDelegate.webServices;
            
      var button  = new UIButton(new CGRect(0, 0, 40, 40));
			button.SetImage(UIImage.FromBundle("ic_settings"),UIControlState.Normal);
			button.TouchUpInside += flipAccountViews;
			settingsButton = new UIBarButtonItem(button);		

     var button2  = new UIButton(new CGRect(0, 0, 70, 30));
			button2.SetTitle("Register", UIControlState.Normal);
			button2.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button2.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			button2.Layer.BorderWidth = 1f;
			button2.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button2.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button2.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
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
			var checkLogin = KeychainAccess.ValueForKey("stayLogged");
			
      if(string.IsNullOrEmpty(checkLogin) || checkLogin == "no"){
      	loginView = new RemoteLoginView(remoteHolderView, webServices);
	      loginView.submitButton.TouchUpInside += credentialsCheck;
	      
				remoteHolderView.AddSubview(loginView.loginView);
				this.NavigationItem.RightBarButtonItem = register;
      } else {
      	remoteView = new remoteSelectionView(remoteHolderView, ion,webServices);
				remoteHolderView.AddSubview(remoteView.selectionView);
				profileView = new RemoteUserProfileView(remoteHolderView,KeychainAccess.ValueForKey("userName"), KeychainAccess.ValueForKey("userDisplay"), KeychainAccess.ValueForKey("userEmail"),webServices);
				remoteHolderView.AddSubview(profileView.profileView);
				this.NavigationItem.RightBarButtonItem = settingsButton;
				profileView.logoutButton.TouchUpInside += LogOutUser;
			};
		}			
		
		/// <summary>
		/// Removes the saved settings for a user and returns them to the log in screen
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void LogOutUser(object sender, EventArgs e){

			await webServices.updateOnlineStatus("0",null);
			
			KeychainAccess.SetValueForKey("no", "stayLogged");
			KeychainAccess.SetValueForKey("","userID");
			KeychainAccess.SetValueForKey("","userName");
			KeychainAccess.SetValueForKey("","userPword");
			
			loginView = new RemoteLoginView(remoteHolderView, webServices);
      loginView.submitButton.TouchUpInside += credentialsCheck;
			remoteHolderView.AddSubview(loginView.loginView);
			
			remoteView.selectionView.RemoveFromSuperview();
			remoteView = null;
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
          fromView:remoteView.selectionView,
          toView:profileView.profileView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	remoteView.selectionView.Hidden = true;
          	profileView.profileView.Hidden = false;
            remoteHolderView.SendSubviewToBack(remoteView.selectionView);
            remoteHolderView.BringSubviewToFront(profileView.profileView);
          }
        );
			}	else {
        UIView.Transition(
          fromView:profileView.profileView,
          toView:remoteView.selectionView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () => {
          	profileView.profileView.Hidden = true;
          	remoteView.selectionView.Hidden = false;
            remoteHolderView.SendSubviewToBack(profileView.profileView);
            remoteHolderView.BringSubviewToFront(remoteView.selectionView);
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
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
    	
    	if(string.IsNullOrEmpty(loginView.userName.Text) || string.IsNullOrEmpty(loginView.password.Text)){
				loginView.loadingLogin.StopAnimating();
				return; 
			}
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();

			data.Add("loginUser","returning");
			data.Add("uname",loginView.userName.Text);			
			data.Add("usrPword",loginView.password.Text);
			try{
				//initiate the post request and get the request result in a byte array 
				byte[] result = wc.UploadValues(loginUserUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
	
				await Task.Delay(TimeSpan.FromSeconds(1));
				JObject response = JObject.Parse(textResponse);
				var userFound = response.GetValue("found").ToString();
				
				if(userFound == "true"){
					var loginID = response.GetValue("message").ToString();
					var userDisplay = response.GetValue("display").ToString();
					var userEmail = response.GetValue("email").ToString();
					
					if(loginView.checkMark){
						KeychainAccess.SetValueForKey("yes","stayLogged");
					} else {
						KeychainAccess.SetValueForKey("no","stayLogged"); 
					}
					
					KeychainAccess.SetValueForKey(loginID,"userID");
					KeychainAccess.SetValueForKey(loginView.userName.Text,"userName");
					KeychainAccess.SetValueForKey(loginView.password.Text,"userPword");
					KeychainAccess.SetValueForKey(userEmail,"userEmail");
					KeychainAccess.SetValueForKey(userDisplay,"userDisplay");
					
					remoteView = new remoteSelectionView(remoteHolderView, ion,webServices);
					remoteHolderView.AddSubview(remoteView.selectionView);
					profileView = new RemoteUserProfileView(remoteHolderView,KeychainAccess.ValueForKey("userName"), KeychainAccess.ValueForKey("userDisplay"), KeychainAccess.ValueForKey("userEmail"),webServices);
					profileView.logoutButton.TouchUpInside += LogOutUser;
					
					remoteHolderView.AddSubview(profileView.profileView);
					loginView.loadingLogin.StopAnimating();
					loginView.loginView.RemoveFromSuperview();
					loginView = null;
					this.NavigationItem.RightBarButtonItem = settingsButton;					
				} else {
					loginView.loadingLogin.StopAnimating();
					var failMessage = response.GetValue("message").ToString();
					var window = UIApplication.SharedApplication.KeyWindow;
	    		var rootVC = window.RootViewController as IONPrimaryScreenController;
					
					var alert = UIAlertController.Create ("Log In", failMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Log In", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				loginView.loadingLogin.StopAnimating();				
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
  		
			if(string.IsNullOrEmpty(registerView.userName.Text) || string.IsNullOrEmpty(registerView.password.Text) || string.IsNullOrEmpty(registerView.displayName.Text) || string.IsNullOrEmpty(registerView.email.Text)){
				var alert = UIAlertController.Create ("User Registration", "Please enter a value for each field", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				return;
			}
			var registered = await webServices.RegisterUser(registerView.userName.Text,registerView.password.Text,registerView.displayName.Text,registerView.email.Text);

    					
			if(registered){			
				var alert = UIAlertController.Create ("User Registration", "Registration Successful", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				
				loginView.userName.Text = registerView.userName.Text;
				loginView.password.Text = registerView.password.Text;			
				registerView.regView.RemoveFromSuperview();
				registerView = null;
			} else {			
				var alert = UIAlertController.Create ("User Registration", "Username already taken", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);				
			}			
		}
		
		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			var loggedIn = KeychainAccess.ValueForKey("userID");
			if(!string.IsNullOrEmpty(loggedIn)){
				if(remoteView != null){
					//remoteView.selectedUser.Clear();
					remoteView.GetAccessList();
				}
			}			
		}
    
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


