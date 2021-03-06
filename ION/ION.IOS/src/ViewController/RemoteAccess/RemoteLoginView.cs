﻿using System;

using Foundation;
using CoreGraphics;
using UIKit;

using ION.IOS.ViewController.JobManager;
using ION.Core.App;
using ION.IOS.App;
using ION.Core.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ION.IOS.ViewController.RemoteAccess {
	public class RemoteLoginView {
		public UIView loginView;
		public UIView bannerHeader;
		public UIView bannerFooter;
		public UIImageView loginHeaderImage;
		public UILabel saveLoginLabel;
		public UITextField userName;
		public UITextField password;
		public UIButton submitButton;
		public UIButton checkboxButton;
		public UIButton recoveryButton;
		public UIActivityIndicatorView loadingLogin;
		public bool checkMark = false;	
		
		public RemoteLoginView(UIView parentView) {
			loginView = new UIView(new CGRect(0,0, parentView.Bounds.Width, parentView.Bounds.Height));
			loginView.BackgroundColor = UIColor.White;
			loginView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
				userName.ResignFirstResponder();
				password.ResignFirstResponder();
			}));
			
			bannerHeader = new UIView(new CGRect(0, 0, loginView.Bounds.Width, .05 * loginView.Bounds.Height));
			bannerHeader.BackgroundColor = UIColor.Black;
			
			loginHeaderImage = new UIImageView(new CGRect(0, .05 * loginView.Bounds.Height, loginView.Bounds.Width, .15 * loginView.Bounds.Height));
			loginHeaderImage.BackgroundColor = UIColor.Black;
			loginHeaderImage.Image = UIImage.FromBundle("appion_log_mountain");
			
			bannerFooter = new UIView(new CGRect(0, .2 * loginView.Bounds.Height, loginView.Bounds.Width, .05 * loginView.Bounds.Height));
			bannerFooter.BackgroundColor = UIColor.Black;
			
			userName = new FloatLabeledTextField(new CGRect(.1 * parentView.Bounds.Width, .35 * parentView.Bounds.Height,.8 * parentView.Bounds.Width, .07 * parentView.Bounds.Height));
			userName.Placeholder = "email";
			userName.TextAlignment = UITextAlignment.Center;
			userName.Layer.CornerRadius = 5f;
			userName.Font = UIFont.FromName("Helvetica", 25f);
			userName.BackgroundColor = UIColor.White;
			userName.AutocorrectionType = UITextAutocorrectionType.No;
			userName.AutocapitalizationType = UITextAutocapitalizationType.None;
			userName.Layer.BorderWidth = 1f;
			userName.ShouldReturn += (textField) => {
				userName.ResignFirstResponder();
				return false;
			};
			
			password = new FloatLabeledTextField(new CGRect(.1 * parentView.Bounds.Width, .45 * parentView.Bounds.Height, .8 * parentView.Bounds.Width, .07 * parentView.Bounds.Height));
			password.Placeholder = "password";
			password.TextAlignment = UITextAlignment.Center;
			password.Layer.CornerRadius = 5f;
			password.Font = UIFont.FromName("Helvetica", 25f);
			password.BackgroundColor = UIColor.White;
			password.SecureTextEntry = true;
			password.AutocorrectionType = UITextAutocorrectionType.No;
			password.AutocapitalizationType = UITextAutocapitalizationType.None;
			password.Layer.BorderWidth = 1f;
			password.ShouldReturn += (textField) => {
				password.ResignFirstResponder();
				return false;
			};
			
			checkboxButton = new UIButton(new CGRect(.15 * parentView.Bounds.Width,.55 * parentView.Bounds.Height, .05 * parentView.Bounds.Width,.05 * parentView.Bounds.Width));
			checkboxButton.SetImage(UIImage.FromBundle("blank_checkbox"), UIControlState.Normal);
			checkboxButton.TouchUpInside += (sender, e) => {
				if(!checkMark){
					checkboxButton.SetImage(UIImage.FromBundle("filled_checkbox"), UIControlState.Normal);
					checkMark = true;
				} else {
					checkboxButton.SetImage(UIImage.FromBundle("blank_checkbox"), UIControlState.Normal);
					checkMark = false;
				}
			};
			
			saveLoginLabel = new UILabel(new CGRect(.2 * parentView.Bounds.Width, .55 * parentView.Bounds.Height, .7 * parentView.Bounds.Width, .05 * parentView.Bounds.Width));
			saveLoginLabel.Text = "Remember me";
			saveLoginLabel.TextAlignment = UITextAlignment.Center;
			saveLoginLabel.AdjustsFontSizeToFitWidth = true;
			saveLoginLabel.TextColor = UIColor.Black;
			
			submitButton = new UIButton(new CGRect(.2 * parentView.Bounds.Width, .7 * parentView.Bounds.Height, .6 * parentView.Bounds.Width, .1 * parentView.Bounds.Height));
			submitButton.SetTitle("Submit", UIControlState.Normal);
			submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			submitButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			submitButton.Layer.BorderWidth = 1f;
			
			submitButton.TouchUpInside += (sender, e) => {
				password.ResignFirstResponder();
				userName.ResignFirstResponder();
			};
			var underlineAttr = new UIStringAttributes { UnderlineStyle = NSUnderlineStyle.Single, ForegroundColor = UIColor.Blue };
			
			recoveryButton = new UIButton(new CGRect(.05 * loginView.Bounds.Width, .9 * loginView.Bounds.Height, .4 * loginView.Bounds.Width, .1 * loginView.Bounds.Height));
			recoveryButton.Font = UIFont.ItalicSystemFontOfSize(15f);
			recoveryButton.SetAttributedTitle(new NSAttributedString("Forgot Password", underlineAttr), UIControlState.Normal);
			recoveryButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			recoveryButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			recoveryButton.TouchUpInside += recoverAccount;
			
			loginView.AddSubview(loginHeaderImage);   
			loginView.AddSubview(bannerHeader);   
			loginView.AddSubview(bannerFooter);   
			loginView.AddSubview(userName);
			loginView.AddSubview(password);
			loginView.AddSubview(checkboxButton);
			loginView.AddSubview(saveLoginLabel);
			loginView.AddSubview(submitButton);
			loginView.AddSubview(recoveryButton);
		}
		/// <summary>
		/// Recovers the account.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void recoverAccount(object sender, EventArgs e){
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;

			var alert = UIAlertController.Create ("Password Reset", "Please enter your email", UIAlertControllerStyle.Alert);
			alert.AddTextField(textField => {});
			
			alert.AddAction (UIAlertAction.Create ("Recover", UIAlertActionStyle.Default, (action) =>{
				if(!string.IsNullOrEmpty(alert.TextFields[0].Text)){
					handleResetResponse(alert.TextFields[0].Text);
				}
			}));
			
			alert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);	
		}
		
		public async void handleResetResponse(string email){
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;

			var ion = AppState.context as IosION;
      var response = await ion.portal.RequestResetUserPasswordAsync(email);

/*
			var feedback = await ion.webServices.resetPassword(email);
			
			if(feedback != null){
				var textResponse = await feedback.Content.ReadAsStringAsync();
				
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
	
				var errorMessage = response.GetValue("message").ToString();
				
				var resetAlert = UIAlertController.Create ("Password Recovery", errorMessage, UIAlertControllerStyle.Alert);
				resetAlert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (resetAlert, animated: true, completionHandler: null);
			}
*/
		}
	}
}
