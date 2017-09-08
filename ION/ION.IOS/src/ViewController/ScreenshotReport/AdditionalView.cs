using System;
using CoreGraphics;
using Foundation;
using ION.IOS.Util;
using ION.IOS.ViewController.JobManager;
using UIKit;


namespace ION.IOS.ViewController.ScreenshotReport {
	public class AdditionalView {
		public UIView detailsView;
		public UIScrollView detailsScroll;
		public UITextField titleField;
		public UILabel dateLabel;
		public UILabel dateValue;
		public UILabel versionLabel;
		public UILabel versionValue;
		public UITextField addressField1;
		public UITextField addressField2;
		public UITextField cityField;
		public UITextField stateField;
		public UITextField zipcodeField;
		

	
		public AdditionalView(UIView parentView) {
			detailsView = new UIView(new CGRect(5,190,parentView.Bounds.Width - 10,parentView.Bounds.Height - 195));
			detailsScroll = new UIScrollView (new CGRect(0,0,detailsView.Bounds.Width,detailsView.Bounds.Height));
			detailsView.AddSubview(detailsScroll);
			
			if(parentView.Bounds.Height < 600){
				Console.WriteLine("Super small screen");
				detailsScroll.ContentSize = new CGSize(detailsView.Bounds.Width, detailsView.Bounds.Height * 1.9);
			}
			else if(parentView.Bounds.Height < 800){      
				detailsScroll.ContentSize = new CGSize(detailsView.Bounds.Width, detailsView.Bounds.Height * 1.4);
			}
			
			titleField = new FloatLabeledTextField(new CGRect(.1 * detailsView.Bounds.Width,10, .8 * detailsView.Bounds.Width,45)){
        Placeholder = "Report Title",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      titleField.Layer.BorderWidth = 1f;
      titleField.Layer.CornerRadius = 5f;
      titleField.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			dateLabel = new UILabel(new CGRect(.1 * detailsView.Bounds.Width, 60, .3 * detailsView.Bounds.Width, 45));
			dateLabel.Text = Strings.DATE;
			dateLabel.Font = UIFont.BoldSystemFontOfSize(20);
			
			dateValue = new UILabel(new CGRect(.4 * detailsView.Bounds.Width, 60, .3 * detailsView.Bounds.Width, 45));
			dateValue.Font = UIFont.SystemFontOfSize(16);
			dateValue.TextAlignment = UITextAlignment.Left;
			
			versionLabel = new UILabel(new CGRect(.1 * detailsView.Bounds.Width, 110, .3 * detailsView.Bounds.Width, 45));
			versionLabel.Text = "App Version";
			versionLabel.Font = UIFont.BoldSystemFontOfSize(16);
			
			versionValue = new UILabel(new CGRect(.4 * detailsView.Bounds.Width, 110, .3 * detailsView.Bounds.Width, 45));
			versionValue.Text = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
			versionValue.Font = UIFont.SystemFontOfSize(16);
			versionValue.TextAlignment = UITextAlignment.Left;		
			
			addressField1 = new FloatLabeledTextField(new CGRect(.1 * detailsView.Bounds.Width, 160, .8 * detailsView.Bounds.Width, 45)){
        Placeholder = Strings.Report.ADDRESS + " 1",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      addressField1.Layer.BorderWidth = 1f;
      addressField1.Layer.CornerRadius = 5f;
      addressField1.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			addressField2 = new FloatLabeledTextField(new CGRect(.1 * detailsView.Bounds.Width,210, .8 * detailsView.Bounds.Width,45)){
        Placeholder = Strings.Report.ADDRESS + " 2",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      addressField2.Layer.BorderWidth = 1f;
      addressField2.Layer.CornerRadius = 5f;
      addressField2.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			cityField = new FloatLabeledTextField(new CGRect(.1 * detailsView.Bounds.Width,260, .8 * detailsView.Bounds.Width,45)){
        Placeholder = Strings.Report.CITY,
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      cityField.Layer.BorderWidth = 1f;
      cityField.Layer.CornerRadius = 5f;
      cityField.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			stateField = new FloatLabeledTextField(new CGRect(.1 * detailsView.Bounds.Width,310, .8 * detailsView.Bounds.Width,45)){
        Placeholder = "State/Province/Region",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      stateField.Layer.BorderWidth = 1f;
      stateField.Layer.CornerRadius = 5f;
      stateField.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			zipcodeField = new FloatLabeledTextField(new CGRect(.1* detailsView.Bounds.Width,360, .8 * detailsView.Bounds.Width,45)){
        Placeholder = "ZIP/Postal Code",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Left,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      zipcodeField.Layer.BorderWidth = 1f;
      zipcodeField.Layer.CornerRadius = 5f;
      zipcodeField.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      
			detailsScroll.AddSubview(titleField);			
			detailsScroll.AddSubview(dateLabel);			
			detailsScroll.AddSubview(dateValue);			
			detailsScroll.AddSubview(versionLabel);			
			detailsScroll.AddSubview(versionValue);			
			detailsScroll.AddSubview(addressField1);			
			detailsScroll.AddSubview(addressField2);			
			detailsScroll.AddSubview(cityField);			
			detailsScroll.AddSubview(stateField);			
			detailsScroll.AddSubview(zipcodeField);			
			
		}
	}
}
