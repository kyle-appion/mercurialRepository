using System;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using ION.Core.Net;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteUserProfileView {
		
		public UIView profileView;
		public UIButton logoutButton;
		public UIButton changePassButton;
		public UIButton changeEmailButton;
		public UILabel settingsHeader;
		public UILabel usernameLabel;
		public UILabel passwordLabel;
		public UILabel emailLabel;
		public UITextField passwordField;
		public UITextField emailField;
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
			
			passwordLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width,.2 * profileView.Bounds.Height,.9 * profileView.Bounds.Width,.05 * profileView.Bounds.Height));
			passwordLabel.TextAlignment = UITextAlignment.Left;
			passwordLabel.Text = "Password";
			passwordLabel.BackgroundColor = UIColor.White;
			
			passwordField = new UITextField(new CGRect(.05 * profileView.Bounds.Width,.25 * profileView.Bounds.Height, .6 * profileView.Bounds.Width,.09 * profileView.Bounds.Height));
			passwordField.Layer.CornerRadius = 5f;
			passwordField.Layer.BorderWidth = 1f;
			passwordField.AutocorrectionType = UITextAutocorrectionType.No;
			passwordField.AutocapitalizationType = UITextAutocapitalizationType.None;
			passwordField.TextAlignment = UITextAlignment.Center;
			
			changePassButton = new UIButton(new CGRect(.67 * profileView.Bounds.Width, .26 * profileView.Bounds.Height, .3 * profileView.Bounds.Width, .07 * profileView.Bounds.Height));
			changePassButton.SetTitle("Update", UIControlState.Normal);
			changePassButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			changePassButton.Layer.CornerRadius = 5f;
			changePassButton.Layer.BorderWidth = 1f;
			changePassButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			emailLabel = new UILabel(new CGRect(.05 * profileView.Bounds.Width, .34 * profileView.Bounds.Height, .9 * profileView.Bounds.Width, .05 * profileView.Bounds.Height));
			emailLabel.Text = "Email";
			emailLabel.TextAlignment = UITextAlignment.Left;
			emailLabel.BackgroundColor = UIColor.White;
			
			emailField = new UITextField(new CGRect(.05 * profileView.Bounds.Width, .4 * profileView.Bounds.Height, .6 * profileView.Bounds.Width, .09 * profileView.Bounds.Height));
			emailField.Layer.BorderWidth = 1f;
			emailField.Layer.CornerRadius = 5f;
			emailField.Text = uEmail;
			emailField.AdjustsFontSizeToFitWidth = true;
			emailField.TextAlignment = UITextAlignment.Center;
			emailField.AutocorrectionType = UITextAutocorrectionType.No;
			emailField.AutocapitalizationType = UITextAutocapitalizationType.None;
			
			changeEmailButton = new UIButton(new CGRect(.67 * profileView.Bounds.Width, .41 * profileView.Bounds.Height, .3 * profileView.Bounds.Width, .07 * profileView.Bounds.Height));
			changeEmailButton.SetTitle("Update", UIControlState.Normal);
			changeEmailButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			changeEmailButton.Layer.CornerRadius = 5f;
			changeEmailButton.Layer.BorderWidth = 1f;
			changeEmailButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			
			logoutButton = new UIButton(new CGRect(.25 * profileView.Bounds.Width, .7 * profileView.Bounds.Height, .5 * profileView.Bounds.Width, .09 * profileView.Bounds.Height));
			logoutButton.SetTitle("Log Out", UIControlState.Normal);
			logoutButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			logoutButton.Layer.BorderWidth = 1f;
			logoutButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			changePassButton.TouchUpInside += UpdatePassword;		
			changeEmailButton.TouchUpInside += UpdateEmail;	
		
			profileView.AddSubview(settingsHeader);
			profileView.AddSubview(usernameLabel);
			profileView.AddSubview(passwordLabel);
			profileView.AddSubview(passwordField);
			profileView.AddSubview(changePassButton);
			profileView.AddSubview(emailLabel);
			profileView.AddSubview(emailField);
			profileView.AddSubview(changeEmailButton);
			profileView.AddSubview(logoutButton);
		}
	
		public async void UpdatePassword(object sender, EventArgs e){
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			if(string.IsNullOrEmpty(passwordField.Text)){
				passwordField.BackgroundColor = UIColor.Red;
				passwordField.Alpha = .6f;
			} else {
				passwordField.BackgroundColor = UIColor.White;
				passwordField.Alpha = 1f;
				webServices.updatePassword(passwordField.Text);
			}
			passwordField.Text = "";
		}
		
		public async void UpdateEmail(object sender, EventArgs e){
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			
			if(string.IsNullOrEmpty(emailField.Text)){
				emailField.BackgroundColor = UIColor.Red;
				emailField.Alpha = .6f;
			} else {
				emailField.BackgroundColor = UIColor.White;
				emailField.Alpha = 1f;
				webServices.updateEmail(emailField.Text, emailField);
			}			
		}	
	}
}
