using System;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.Util;

using ION.IOS.ViewController.Analyzer;
using ION.IOS.ViewController.PressureTemperatureChart;
using ION.IOS.ViewController.Settings;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.Workbench;
using ION.IOS.ViewController.Logging;
using ION.IOS.ViewController.JobManager;
using MonoTouch.Dialog;
using ION.IOS.ViewController.Help;
using ION.IOS.ViewController.Walkthrough;
using ION.IOS.ViewController.RssFeed;
using ION.IOS.App;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ION.IOS.ViewController.RemoteAccess {
	public partial class RemoteSystemViewController : BaseIONViewController {
		public remoteSelectionView remoteView;
		public RemoteLoginView loginView;
		public const string loginUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/applogin.php";
		public IION ion;
		public UIBarButtonItem logOut;
		
		public RemoteSystemViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      ion = AppState.context;
      var checkLogin = KeychainAccess.ValueForKey("stayLogged");
      var loginUsrId = KeychainAccess.ValueForKey("userID");
      
      var button  = new UIButton(new CGRect(0, 0, 70, 40));
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
      	remoteView = new remoteSelectionView(View, ion);
				View.AddSubview(remoteView.selectionView);
				this.NavigationItem.RightBarButtonItem = logOut;
				remoteView.remoteMenu.TouchDown += (sender, e) => {remoteView.remoteMenu.BackgroundColor = UIColor.Blue;};
			}	
		}

		public async void credentialsCheck(object sender, EventArgs e){
			if(loginView.loadingLogin != null){
				loginView.loadingLogin = null;
			}			
			loginView.loadingLogin = new UIActivityIndicatorView(new CGRect(0,0, loginView.loginView.Bounds.Width, loginView.loginView.Bounds.Height));
			loginView.loadingLogin.BackgroundColor = UIColor.Black;
			loginView.loadingLogin.Alpha = .8f;
			loginView.loginView.AddSubview(loginView.loadingLogin);
			loginView.loginView.BringSubviewToFront(loginView.loadingLogin);
			loginView.loadingLogin.StartAnimating();
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
    	
    	if(string.IsNullOrEmpty(loginView.userName.Text) || string.IsNullOrEmpty(loginView.password.Text)){
				Console.WriteLine("No username or password entered");
				loginView.loadingLogin.StopAnimating();
				return; 
			}

			var checkID = KeychainAccess.ValueForKey("userID");
			var checkuname = KeychainAccess.ValueForKey("userName");
			var checkpword = KeychainAccess.ValueForKey("userPword");
			Console.WriteLine("Stored id: " + checkID + " stored uname: " + checkuname + " stored pword: " + checkpword);

			WebClient wc = new WebClient(); 
			wc.Proxy = null;
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();

			data.Add("loginUser","returning");
			data.Add("uname",loginView.userName.Text);			
			data.Add("usrPword",loginView.password.Text);

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
				remoteView = new remoteSelectionView(View, ion);
				View.AddSubview(remoteView.selectionView);
				loginView.loadingLogin.StopAnimating();
				View.WillRemoveSubview(loginView.loginView);
				this.NavigationItem.RightBarButtonItem = logOut;					
			} else {
				loginView.loadingLogin.StopAnimating();
				var failMessage = response.GetValue("message").ToString();
				Console.WriteLine("Unable to login because: " + failMessage);
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
    
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


