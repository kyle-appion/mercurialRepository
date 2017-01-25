using System;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using ION.Core.Net;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteUserProfileView {
		
		public UIView profileView;
		public UIButton logoutButton;
		public UIButton changePassButton;	
		public UILabel settingsHeader;
		public UILabel usernameLabel;
		public UILabel emailLabel;
		public UILabel passwordLabel;
		public UILabel updatingLabel;
		public UITextField passwordField;
		public WebPayload webServices;
		
		public RemoteUserProfileView(UIView parentView, string dName, string uEmail, WebPayload webServices) {
			this.webServices = webServices;
			
			var viewTap = new UITapGestureRecognizer(() => {
				profileView.EndEditing(true);
			});
		
			profileView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			profileView.BackgroundColor = UIColor.White; 
			profileView.Hidden = true;
			profileView.AddGestureRecognizer(viewTap);
			
			settingsHeader = new UILabel(new CGRect(0,0,profileView.Bounds.Width, .1 * profileView.Bounds.Height));
			settingsHeader.Text = "User Profile";
			settingsHeader.TextAlignment = UITextAlignment.Center;
			settingsHeader.Font = UIFont.BoldSystemFontOfSize(20f);
			
			usernameLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width, .1 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			usernameLabel.Text = "Display Name: " + dName;
			usernameLabel.TextAlignment = UITextAlignment.Left;
			usernameLabel.AdjustsFontSizeToFitWidth = true;
			usernameLabel.Font = UIFont.BoldSystemFontOfSize(20f);			

			emailLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.2 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.05 * profileView.Bounds.Height));
			emailLabel.Text = "Email: " + uEmail;
			emailLabel.TextAlignment = UITextAlignment.Left;
			emailLabel.AdjustsFontSizeToFitWidth = true;
			emailLabel.Font = UIFont.BoldSystemFontOfSize(20f);
		
			passwordLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.26 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.05 * profileView.Bounds.Height));
			passwordLabel.TextAlignment = UITextAlignment.Left;
			passwordLabel.Text = "Password";
			passwordLabel.BackgroundColor = UIColor.White;
			
			passwordField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.3 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			passwordField.Layer.CornerRadius = 5f;
			passwordField.Layer.BorderWidth = 1f;
			passwordField.SecureTextEntry = true;
			passwordField.AutocorrectionType = UITextAutocorrectionType.No;
			passwordField.AutocapitalizationType = UITextAutocapitalizationType.None;
			passwordField.TextAlignment = UITextAlignment.Center;
			
			changePassButton = new UIButton(new CGRect(.67 * profileView.Bounds.Width, .31 * profileView.Bounds.Height, .3 * profileView.Bounds.Width, .07 * profileView.Bounds.Height));
			changePassButton.SetTitle("Update", UIControlState.Normal);
			changePassButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			changePassButton.Layer.CornerRadius = 5f;
			changePassButton.Layer.BorderWidth = 1f;
			changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			changePassButton.TouchDown += (sender, e) => {changePassButton.BackgroundColor = UIColor.Blue;};
			changePassButton.TouchUpOutside += (sender, e) => {changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			updatingLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.4 * profileView.Bounds.Height, .95 * profileView.Bounds.Width,.1 * profileView.Bounds.Height));
			updatingLabel.Text = "Updating Email. Please Wait.";
			updatingLabel.AdjustsFontSizeToFitWidth = true;
			updatingLabel.Hidden = true;
						
			logoutButton = new UIButton(new CGRect(.25 * profileView.Bounds.Width, .7 * profileView.Bounds.Height, .5 * profileView.Bounds.Width, .09 * profileView.Bounds.Height));
			logoutButton.SetTitle("Log Out", UIControlState.Normal);
			logoutButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			logoutButton.Layer.BorderWidth = 1f;
			logoutButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			changePassButton.TouchUpInside += UpdatePassword;		
		
			profileView.AddSubview(settingsHeader);
			profileView.AddSubview(usernameLabel);
			profileView.AddSubview(emailLabel);
			profileView.AddSubview(passwordLabel);
			profileView.AddSubview(passwordField);
			profileView.AddSubview(changePassButton);
			profileView.AddSubview(updatingLabel);
			profileView.AddSubview(logoutButton);
		}
	
		public async void UpdatePassword(object sender, EventArgs e){
			changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			updatingLabel.Hidden = false;
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			if(string.IsNullOrEmpty(passwordField.Text)){
				passwordField.BackgroundColor = UIColor.Red;
				passwordField.Alpha = .6f;
				updatingLabel.Hidden = true;				
			} else if (passwordField.Text.Length < 8 || !passwordField.Text.Any(char.IsUpper)) {
				var window = UIApplication.SharedApplication.KeyWindow;
  			var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
				var alert = UIAlertController.Create ("Password Update", "Passwords must be 8 characters long and have at least 1 uppercase character", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}else {
				passwordField.BackgroundColor = UIColor.White;
				passwordField.Alpha = 1f;
				var window = UIApplication.SharedApplication.KeyWindow;
  			var rootVC = window.RootViewController as IONPrimaryScreenController;
					
				var updateResponse = await webServices.updatePassword(passwordField.Text);
				updatingLabel.Hidden = true;
				
				if(updateResponse != null){
					var textResponse = await updateResponse.Content.ReadAsStringAsync();
					Console.WriteLine(textResponse);
					//parse the text string into a json object to be deserialized
					JObject response = JObject.Parse(textResponse);
		
					var errorMessage = response.GetValue("message").ToString();
					
					var alert = UIAlertController.Create ("Password Update", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				} else {
					var alert = UIAlertController.Create ("Password Update", "Connection was lost. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
			}
			updatingLabel.Hidden = true;
		
			passwordField.Text = "";
		}	
	}
}
