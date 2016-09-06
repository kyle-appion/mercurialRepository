using System;
using UIKit;
using CoreGraphics;
using ION.IOS.ViewController.JobManager;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteUserRegistration {
		public UIView regView;
		public UILabel signupLabel;
		public UITextField userName;
		public UITextField password;
		public UITextField email;
		public UITextField displayName;
		public UIButton submitButton;
		public UIButton cancelbutton;
	
		public RemoteUserRegistration(UIView parentView) {

      regView = new UIView(new CGRect(.05 * parentView.Bounds.Width, parentView.Bounds.Height, .9 * parentView.Bounds.Width, .85 * parentView.Bounds.Height));
      regView.BackgroundColor = UIColor.White;
      regView.Layer.CornerRadius = 5f;
      regView.Layer.BorderWidth = 1f;
      regView.ClipsToBounds = true;
      
      regView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        regView.EndEditing(true);
      }));
	
			
			signupLabel = new UILabel(new CGRect(0,0,regView.Bounds.Width, .09 * regView.Bounds.Height));
			signupLabel.Font = UIFont.BoldSystemFontOfSize(20f);
			signupLabel.Text = "Register";
			signupLabel.TextAlignment = UITextAlignment.Center;
			signupLabel.BackgroundColor = UIColor.FromRGB(9,211,255);
			signupLabel.TextColor = UIColor.Black;
			signupLabel.AdjustsFontSizeToFitWidth = true;
			signupLabel.ClipsToBounds = true;
      
      cancelbutton = new UIButton(new CGRect(regView.Bounds.Width - .1 * regView.Bounds.Width, .02 * regView.Bounds.Height ,.09 * regView.Bounds.Width,.09 * regView.Bounds.Width));
      cancelbutton.SetImage(UIImage.FromBundle("img_button_blackclosex"),UIControlState.Normal);

      userName = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.14 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
        Placeholder = "Username",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      userName.Layer.BorderWidth = 1f;
      userName.Layer.CornerRadius = 5f;
      userName.AutocorrectionType = UITextAutocorrectionType.No;
      userName.AutocapitalizationType = UITextAutocapitalizationType.None;
      userName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      password = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.28 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
        Placeholder = "Password",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      password.Layer.BorderWidth = 1f;
      password.Layer.CornerRadius = 5f;
      password.AutocorrectionType = UITextAutocorrectionType.No;
      password.AutocapitalizationType = UITextAutocapitalizationType.None;
      password.SecureTextEntry = true;
      password.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      email = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.42 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
        Placeholder = "Email",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      email.Layer.BorderWidth = 1f;
      email.Layer.CornerRadius = 5f;
      email.AutocorrectionType = UITextAutocorrectionType.No;
      email.AutocapitalizationType = UITextAutocapitalizationType.None;
      email.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      displayName = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.56 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
        Placeholder = "Display Name",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      displayName.Layer.BorderWidth = 1f;
      displayName.Layer.CornerRadius = 5f;
      displayName.AutocorrectionType = UITextAutocorrectionType.No;
      displayName.AutocapitalizationType = UITextAutocapitalizationType.None;
      displayName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
      submitButton = new UIButton(new CGRect(.25 * regView.Bounds.Width, .68 * regView.Bounds.Height, .5 * regView.Bounds.Width, .09 * regView.Bounds.Height));
      submitButton.SetTitle("Submit",UIControlState.Normal);
      submitButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      submitButton.Layer.BorderWidth = 1f;
      submitButton.TouchDown += (sender, e) => {submitButton.BackgroundColor = UIColor.Blue;};
      submitButton.TouchUpOutside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      submitButton.TouchUpInside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

			regView.AddSubview(signupLabel);
      regView.AddSubview(userName);
      regView.AddSubview(password);
      regView.AddSubview(email);
      regView.AddSubview(displayName);
      regView.AddSubview(submitButton);
      regView.AddSubview(cancelbutton);
      regView.BringSubviewToFront(cancelbutton);
		}
		
	}
}

