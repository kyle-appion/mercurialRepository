using System;
using System.Net;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.App;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ION.IOS.ViewController.WebServices;

namespace ION.IOS.ViewController.RemoteAccess {
	public partial class RemoteSystemViewController : BaseIONViewController {
		public remoteSelectionView remoteView;
		public RemoteLoginView loginView;
		public const string loginUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/applogin.php";
		public IION ion;
		public UIBarButtonItem logOut;
		public SessionPayload webServices;
		WebClient wc;
		public RemoteSystemViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      ion = AppState.context;
			wc = new WebClient(); 
			wc.Proxy = null;
			
      webServices = new SessionPayload();
      var checkLogin = KeychainAccess.ValueForKey("stayLogged");
      var loginUsrId = KeychainAccess.ValueForKey("userID");
      
      var button  = new UIButton(new CGRect(0, 0, 70, 30));
			button.SetTitle("Log Out", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			button.Layer.BorderWidth = 1f;
			button.TouchDown += (sender, e) => {button.BackgroundColor = UIColor.Blue;};
			button.TouchUpOutside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button.TouchUpInside += (sender, e) => {button.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			button.TouchUpInside += setupLoginView;
			logOut = new UIBarButtonItem(button);		
      
      if(string.IsNullOrEmpty(checkLogin) || checkLogin == "no"){
      	loginView = new RemoteLoginView(View);
	      loginView.submitButton.TouchUpInside += credentialsCheck;
				View.AddSubview(loginView.loginView);
				this.NavigationItem.RightBarButtonItem = null;
      } else {
      	remoteView = new remoteSelectionView(View, ion,webServices);
				View.AddSubview(remoteView.selectionView);
				this.NavigationItem.RightBarButtonItem = logOut;
				remoteView.remoteMenuButton.TouchDown += (sender, e) => {remoteView.remoteMenuButton.BackgroundColor = UIColor.Blue;};
			}	
		}

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
					Console.WriteLine("Login Successful. Setting user id to " + loginID);
					if(loginView.checkMark){
						KeychainAccess.SetValueForKey("yes","stayLogged");
					} else {
						KeychainAccess.SetValueForKey("no","stayLogged");
					}
					KeychainAccess.SetValueForKey(loginID,"userID");
					KeychainAccess.SetValueForKey(loginView.userName.Text,"userName");
					KeychainAccess.SetValueForKey(loginView.password.Text,"userPword");
					remoteView = new remoteSelectionView(View, ion,webServices);
					View.AddSubview(remoteView.selectionView);
					loginView.loadingLogin.StopAnimating();
					View.WillRemoveSubview(loginView.loginView);
					this.NavigationItem.RightBarButtonItem = logOut;					
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
			}					
		}

		public void setupLoginView(object sender, EventArgs e){									
				KeychainAccess.SetValueForKey("no", "stayLogged");
				KeychainAccess.SetValueForKey("","userID");
				KeychainAccess.SetValueForKey("","userName");
				KeychainAccess.SetValueForKey("","userPword");
				loginView = new RemoteLoginView(View);
				View.AddSubview(loginView.loginView);
				View.WillRemoveSubview(remoteView.selectionView);
	      loginView.submitButton.TouchUpInside += credentialsCheck;
	      this.NavigationItem.RightBarButtonItem = null;
		}
		
		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			var loggedIn = KeychainAccess.ValueForKey("userID");
			if(!string.IsNullOrEmpty(loggedIn)){
				if(remoteView != null){
					remoteView.selectedUser.Clear();
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


