using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace ION.IOS.ViewController.Analyzer {
  public class ManualView {
  
    public UIView mView;
    public UIButton mcloseButton;
    public UIButton mdoneButton;
    public UIButton mmeasurementType;
    public UIButton dtypeButton;
    public UITextField mtextValue;
    public UITextField mbuttonText;
    public UILabel mdeviceType;
    public UILabel popupText;
    public UILabel textValidation;
    public UIView mbuttonBorder;
    public UIView mbuttonBorder2;
    public bool isManual = false;
  
    public ManualView(UIView mainView) {
      
      mView = new UIView(new CGRect(.062 * mainView.Bounds.Width, .176 * mainView.Bounds.Height, .875 * mainView.Bounds.Width, .343 * mainView.Bounds.Height));
      mView.ClipsToBounds = true;
      mcloseButton = new UIButton(new CGRect(0,.753 * mView.Bounds.Height, .503 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mcloseButton.ReverseTitleShadowWhenHighlighted = true;
      mdoneButton = new UIButton(new CGRect(.5 * mView.Bounds.Width, .753 * mView.Bounds.Height, .5 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mdoneButton.ReverseTitleShadowWhenHighlighted = true;
      mmeasurementType = new UIButton (new CGRect(.535 * mView.Bounds.Width, .384 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));

      mmeasurementType.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      mmeasurementType.Layer.BorderColor = UIColor.Black.CGColor;
      mmeasurementType.Layer.BorderWidth = 2f;      
      dtypeButton = new UIButton (new CGRect(.535 * mView.Bounds.Width, .179 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      dtypeButton.AdjustsImageWhenHighlighted = true;
      dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      dtypeButton.Layer.BorderColor = UIColor.Black.CGColor;
      dtypeButton.Layer.BorderWidth = 2f;
      mtextValue = new UITextField (new CGRect(.028 * mView.Bounds.Width, .384 * mView.Bounds.Height, .5 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mtextValue.AttributedPlaceholder = new NSAttributedString (
        "Enter Measurement"
      );
      mtextValue.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
      mtextValue.AdjustsFontSizeToFitWidth = true;
      mtextValue.ShouldChangeCharacters = (textField, range, replacementString) => {
        if(replacementString.Contains("0") || replacementString.Contains("1") || replacementString.Contains("2") || replacementString.Contains("3") ||
           replacementString.Contains("4") || replacementString.Contains("5") || replacementString.Contains("6") || replacementString.Contains("7") ||
           replacementString.Contains("8") || replacementString.Contains("9") || replacementString.Contains(".") || replacementString.Contains("-") || 
           replacementString.Length.Equals(0)){

          if(replacementString.Contains(".") && textField.Text.Contains(".")){
            return false;
          }
          if((replacementString.Contains("-") && textField.Text.Contains("-")) || replacementString.Contains("-") && textField.Text.Length > 0){
            return false;
          }
          if(replacementString.Contains(".") && textField.Text.Length.Equals(0)){
            textField.Text = "0";
            return true;
          }
          return true;
        }
        else {
          return false;
        }
      };
      mbuttonText = new UITextField (new CGRect(.535 * mView.Bounds.Width, .384 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mbuttonText.Layer.BorderColor = UIColor.Black.CGColor;
      mbuttonText.Layer.BorderWidth = 2f;
      mdeviceType = new UILabel (new CGRect(.028 * mView.Bounds.Width, .169 * mView.Bounds.Height, .5 * mView.Bounds.Width, .174 * mView.Bounds.Height));
      mdeviceType.TextAlignment = UITextAlignment.Right;
      mdeviceType.Font = UIFont.FromName("Helvetica-Bold", 20f);
      popupText = new UILabel (new CGRect(0,0,mView.Bounds.Width,.158 * mView.Bounds.Height));
      popupText.ClipsToBounds = true;
      mbuttonBorder = new UIView (new CGRect(0,.753 * mView.Bounds.Height,mView.Bounds.Width,1));
      mbuttonBorder.BackgroundColor = UIColor.LightGray;
      mbuttonBorder2 = new UIView(new CGRect(.5 * mView.Bounds.Width,.753 * mView.Bounds.Height,1,.246 * mView.Bounds.Height));
      mbuttonBorder2.BackgroundColor = UIColor.LightGray;
      textValidation = new UILabel(new CGRect(.028 * mView.Bounds.Width,.553 * mView.Bounds.Height,.942 *mView.Bounds.Width,.153 * mView.Bounds.Height));
      textValidation.TextAlignment = UITextAlignment.Center;
      textValidation.TextColor = UIColor.Red;
      textValidation.Hidden = true;
      textValidation.LineBreakMode = UILineBreakMode.WordWrap;
      textValidation.AdjustsFontSizeToFitWidth = true;
      mView.AddSubview(mcloseButton);
      mView.AddSubview(mdoneButton);
      mView.AddSubview(mmeasurementType);
      mView.AddSubview(dtypeButton);
      mView.AddSubview(mtextValue);
      mView.AddSubview(mbuttonText);
      mView.AddSubview(mdeviceType);
      mView.AddSubview(popupText);
      mView.AddSubview(mbuttonBorder);
      mView.AddSubview(mbuttonBorder2);
      mView.AddSubview(textValidation);

      mmeasurementType.TouchDown += delegate {
        mbuttonText.BackgroundColor = UIColor.Blue;
      };

      mmeasurementType.TouchUpInside += delegate {
        mbuttonText.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      mmeasurementType.TouchUpOutside += delegate {
        mbuttonText.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      dtypeButton.TouchDown += delegate {
        dtypeButton.BackgroundColor = UIColor.Blue;
      };

      dtypeButton.TouchUpInside += delegate {
        dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      dtypeButton.TouchUpOutside += delegate {
        dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      mcloseButton.TouchDown += delegate {
        mcloseButton.BackgroundColor = UIColor.Blue;
      };

      mcloseButton.TouchUpInside += delegate {
        mcloseButton.BackgroundColor = UIColor.Clear;
      };
      mcloseButton.TouchUpOutside += delegate {
        mcloseButton.BackgroundColor = UIColor.Clear;
      };

      mdoneButton.TouchDown += delegate {
        mdoneButton.BackgroundColor = UIColor.Blue;
      };

      mdoneButton.TouchUpInside += delegate {
        mdoneButton.BackgroundColor = UIColor.Clear;
      };

      mdoneButton.TouchUpOutside += delegate {
        mdoneButton.BackgroundColor = UIColor.Clear;
      };
    }

  }
}

