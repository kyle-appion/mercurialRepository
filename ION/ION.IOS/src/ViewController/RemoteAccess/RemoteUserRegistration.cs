using System;
using UIKit;
using CoreGraphics;
using ION.IOS.ViewController.JobManager;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteUserRegistration {
		public UIView regView;
		public UILabel signupLabel;
		public UILabel requirementLabel;
		public UITextField email;
		public UITextField password;
		public UITextField confirmPassword;
		public UIButton submitButton;
		public UIButton cancelbutton;
		public UIActivityIndicatorView loadingRegistration;
	
		public RemoteUserRegistration(UIView parentView) {

      regView = new UIView(new CGRect(.05 * parentView.Bounds.Width, parentView.Bounds.Height, .9 * parentView.Bounds.Width, .85 * parentView.Bounds.Height));
      regView.BackgroundColor = UIColor.White;
      regView.Layer.CornerRadius = 5f;
      regView.Layer.BorderWidth = 1f;
      regView.ClipsToBounds = true;
      
      regView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        regView.EndEditing(true);
      }));
	
			
			signupLabel = new UILabel(new CGRect(0,0,regView.Bounds.Width, .1 * regView.Bounds.Height));
			signupLabel.Font = UIFont.BoldSystemFontOfSize(20f);
			signupLabel.Text = "Register";
			signupLabel.TextAlignment = UITextAlignment.Center;
			signupLabel.BackgroundColor = UIColor.FromRGB(9,211,255);
			signupLabel.TextColor = UIColor.Black;
			signupLabel.AdjustsFontSizeToFitWidth = true;
			signupLabel.ClipsToBounds = true;
      
      cancelbutton = new UIButton(new CGRect(regView.Bounds.Width - .1 * regView.Bounds.Width, .015 * regView.Bounds.Height ,.09 * regView.Bounds.Width,.09 * regView.Bounds.Width));
      cancelbutton.SetImage(UIImage.FromBundle("img_button_blackclosex"),UIControlState.Normal);

      email = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.14 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
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
      
      confirmPassword = new FloatLabeledTextField(new CGRect(.1 * regView.Bounds.Width,.42 * regView.Bounds.Height,.8 * regView.Bounds.Width,.09 * regView.Bounds.Height)){
        Placeholder = "Confirm Password",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      confirmPassword.Layer.BorderWidth = 1f;
      confirmPassword.Layer.CornerRadius = 5f;
      confirmPassword.AutocorrectionType = UITextAutocorrectionType.No;
      confirmPassword.AutocapitalizationType = UITextAutocapitalizationType.None;
      confirmPassword.SecureTextEntry = true;
      confirmPassword.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
      requirementLabel = new UILabel(new CGRect(0,.51 * regView.Bounds.Height, regView.Bounds.Width,.17 * regView.Bounds.Height));
      requirementLabel.AdjustsFontSizeToFitWidth = true;
      requirementLabel.Lines = 0;
      requirementLabel.Text = "Passwords must be 8 characters long and have at least 1 uppercase character";
      requirementLabel.Font = UIFont.ItalicSystemFontOfSize(15);
      
      submitButton = new UIButton(new CGRect(.25 * regView.Bounds.Width, .68 * regView.Bounds.Height, .5 * regView.Bounds.Width, .09 * regView.Bounds.Height));
      submitButton.SetTitle("Submit",UIControlState.Normal);
      submitButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      submitButton.Layer.BorderWidth = 1f;
      submitButton.TouchDown += (sender, e) => {submitButton.BackgroundColor = UIColor.Blue;};
      submitButton.TouchUpOutside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      submitButton.TouchUpInside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

			regView.AddSubview(signupLabel);
      regView.AddSubview(email);
      regView.AddSubview(password);
      regView.AddSubview(confirmPassword);
      regView.AddSubview(requirementLabel);
      regView.AddSubview(submitButton);
      regView.AddSubview(cancelbutton);
      regView.BringSubviewToFront(cancelbutton);
		}
		
	}
}

