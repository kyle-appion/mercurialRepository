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
		public UIScrollView profileScroll;
		public UIButton logoutButton;
		public UIButton changeNameButton;	
		public UIButton changePassButton;	
		public UILabel settingsHeader;
		public UILabel firstNameLabel;
		public UILabel emailLabel;
		public UILabel passwordLabel;
		public UILabel updatingLabel;
		public UITextField firstNameField;
		public UITextField lastNameField;
		public UITextField passwordField;
		public UITextField confirmPasswordField;
		public WebPayload webServices;
		
		public RemoteUserProfileView(UIView parentView, string dName, string uEmail, WebPayload webServices) {
			this.webServices = webServices;
			var splitName = dName.Split(' ');
			
			var viewTap = new UITapGestureRecognizer(() => {
				profileView.EndEditing(true);
			});
		
			profileView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			profileView.BackgroundColor = UIColor.White; 
			profileView.Hidden = true;
			profileView.AddGestureRecognizer(viewTap);
			
			profileScroll = new UIScrollView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			profileScroll.ContentSize = new CGSize(parentView.Bounds.Width, 1.4 * parentView.Bounds.Height);
			
			settingsHeader = new UILabel(new CGRect(0,0,profileView.Bounds.Width, .08 * profileView.Bounds.Height));
			settingsHeader.Text = "User Profile";
			settingsHeader.TextAlignment = UITextAlignment.Center;
			settingsHeader.Font = UIFont.BoldSystemFontOfSize(20f);
			
			emailLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.08 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.05 * profileView.Bounds.Height));
			emailLabel.Text = "Email: " + uEmail;
			emailLabel.TextAlignment = UITextAlignment.Left;
			emailLabel.AdjustsFontSizeToFitWidth = true;
			emailLabel.Font = UIFont.BoldSystemFontOfSize(20f);			
			
			firstNameLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width, .14 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			firstNameLabel.Text = "Display Name";
			firstNameLabel.TextAlignment = UITextAlignment.Left;
			firstNameLabel.AdjustsFontSizeToFitWidth = true;

			firstNameField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.23 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			firstNameField.Placeholder = "First Name";
			firstNameField.Layer.BorderWidth = 1f;
			firstNameField.AutocorrectionType = UITextAutocorrectionType.No;
			firstNameField.AutocapitalizationType = UITextAutocapitalizationType.None;
			firstNameField.TextAlignment = UITextAlignment.Center;
			firstNameField.Text = splitName[0];
			
			lastNameField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.32 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			lastNameField.Placeholder = "Last Name";
			lastNameField.Layer.BorderWidth = 1f;
			lastNameField.AutocorrectionType = UITextAutocorrectionType.No;
			lastNameField.AutocapitalizationType = UITextAutocapitalizationType.None;
			lastNameField.TextAlignment = UITextAlignment.Center;
			lastNameField.Text = splitName[1];
						
			changeNameButton = new UIButton(new CGRect(.67 * profileView.Bounds.Width, .33 * profileView.Bounds.Height, .3 * profileView.Bounds.Width, .07 * profileView.Bounds.Height));
			changeNameButton.SetTitle("Update", UIControlState.Normal);
			changeNameButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			changeNameButton.Layer.CornerRadius = 5f;
			changeNameButton.Layer.BorderWidth = 1f;
			changeNameButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			changeNameButton.TouchDown += (sender, e) => {changeNameButton.BackgroundColor = UIColor.Blue;};
			changeNameButton.TouchUpOutside += (sender, e) => {changeNameButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
					
			passwordLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.41 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.05 * profileView.Bounds.Height));
			passwordLabel.TextAlignment = UITextAlignment.Left;
			passwordLabel.Text = "Password";
			passwordLabel.BackgroundColor = UIColor.White;
			
			passwordField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.46 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			passwordField.Placeholder = "Password";   
			passwordField.Layer.BorderWidth = 1f;
			passwordField.SecureTextEntry = true;
			passwordField.AutocorrectionType = UITextAutocorrectionType.No;
			passwordField.AutocapitalizationType = UITextAutocapitalizationType.None;
			passwordField.TextAlignment = UITextAlignment.Center;

			confirmPasswordField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.55 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			confirmPasswordField.Placeholder = "Confirm Password";
			confirmPasswordField.Layer.BorderWidth = 1f;
			confirmPasswordField.SecureTextEntry = true;
			confirmPasswordField.AutocorrectionType = UITextAutocorrectionType.No;
			confirmPasswordField.AutocapitalizationType = UITextAutocapitalizationType.None;
			confirmPasswordField.TextAlignment = UITextAlignment.Center;
			
			changePassButton = new UIButton(new CGRect(.67 * profileView.Bounds.Width, .56 * profileView.Bounds.Height, .3 * profileView.Bounds.Width, .07 * profileView.Bounds.Height));
			changePassButton.SetTitle("Update", UIControlState.Normal);
			changePassButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			changePassButton.Layer.CornerRadius = 5f;
			changePassButton.Layer.BorderWidth = 1f;
			changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			changePassButton.TouchDown += (sender, e) => {changePassButton.BackgroundColor = UIColor.Blue;};
			changePassButton.TouchUpOutside += (sender, e) => {changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			updatingLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.63 * profileView.Bounds.Height, .95 * profileView.Bounds.Width,.1 * profileView.Bounds.Height));
			updatingLabel.Text = "Updating password. Please Wait.";
			updatingLabel.AdjustsFontSizeToFitWidth = true;
			updatingLabel.Hidden = true;
						
			logoutButton = new UIButton(new CGRect(.25 * profileView.Bounds.Width, .7 * profileView.Bounds.Height, .5 * profileView.Bounds.Width, .09 * profileView.Bounds.Height));
			logoutButton.SetTitle("Log Out", UIControlState.Normal);
			logoutButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			logoutButton.Layer.BorderWidth = 1f;
			logoutButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			changePassButton.TouchUpInside += UpdatePassword;
			changeNameButton.TouchUpInside += UpdateName;
		
			profileScroll.AddSubview(settingsHeader);
			profileScroll.AddSubview(firstNameLabel);
			profileScroll.AddSubview(firstNameField);
			profileScroll.AddSubview(lastNameField);
			profileScroll.AddSubview(changeNameButton);
			profileScroll.AddSubview(emailLabel);
			profileScroll.AddSubview(passwordLabel);
			profileScroll.AddSubview(passwordField);
			profileScroll.AddSubview(confirmPasswordField);
			profileScroll.AddSubview(changePassButton);
			profileScroll.AddSubview(updatingLabel);
			profileScroll.AddSubview(logoutButton);
			
			profileView.AddSubview(profileScroll);
		}
	
		public async void UpdatePassword(object sender, EventArgs e){
			changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			updatingLabel.Text = "Updating password. Please Wait.";
			updatingLabel.Hidden = false;
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			if(string.IsNullOrEmpty(passwordField.Text) || string.IsNullOrEmpty(confirmPasswordField.Text)){
				passwordField.BackgroundColor = UIColor.Red;
				passwordField.Alpha = .6f;
				confirmPasswordField.BackgroundColor = UIColor.Red;
				confirmPasswordField.Alpha = .6f;
				updatingLabel.Hidden = true;				
			} else if (!passwordField.Text.Equals(confirmPasswordField.Text)){
				passwordField.BackgroundColor = UIColor.Red;
				passwordField.Alpha = .6f;
				confirmPasswordField.BackgroundColor = UIColor.Red;
				confirmPasswordField.Alpha = .6f;			
				var window = UIApplication.SharedApplication.KeyWindow;
  			var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
				var alert = UIAlertController.Create ("Password Update", "Passwords do not match", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			} else if (passwordField.Text.Length < 8 || !passwordField.Text.Any(char.IsUpper)) {
				var window = UIApplication.SharedApplication.KeyWindow;
  			var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
				var alert = UIAlertController.Create ("Password Update", "Passwords must be 8 characters long and have at least 1 uppercase character", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}else {
				passwordField.BackgroundColor = UIColor.White;
				passwordField.Alpha = 1f;
				confirmPasswordField.BackgroundColor = UIColor.White;
				confirmPasswordField.Alpha = 1f;
				
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
			confirmPasswordField.Text = "";
		}
		
		public async void UpdateName(object sender, EventArgs e){
			changeNameButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
					
			var updateResponse = await webServices.updateDisplayName(firstNameField.Text, lastNameField.Text);
				
			if(updateResponse != null){
				var textResponse = await updateResponse.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
	
				var errorMessage = response.GetValue("message").ToString();
				
				var alert = UIAlertController.Create ("Name Update", errorMessage, UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			} else {
				var alert = UIAlertController.Create ("Name Update", "Connection was lost. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
	}
}
