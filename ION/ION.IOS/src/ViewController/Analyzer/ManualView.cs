using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace ION.IOS.ViewController.Analyzer {
  public class ManualView {
    
    public ManualView(UIView mainView) {
      
      mView = new UIView(new CGRect(.062 * mainView.Bounds.Width, .176 * mainView.Bounds.Height, .875 * mainView.Bounds.Width, .343 * mainView.Bounds.Height));
      mcloseButton = new UIButton(new CGRect(0,.753 * mView.Bounds.Height, .503 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mdoneButton = new UIButton(new CGRect(.5 * mView.Bounds.Width, .753 * mView.Bounds.Height, .5 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mmeasurementType = new UIButton (new CGRect(.535 * mView.Bounds.Width, .533 * mView.Bounds.Height, .435 * mView.Bounds.Width, .225 * mView.Bounds.Height));
      dtypeButton = new UIButton (new CGRect(.535 * mView.Bounds.Width, .179 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mtextValue = new UITextField (new CGRect(.028 * mView.Bounds.Width, .384 * mView.Bounds.Height, .942 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mtextValue.KeyboardType = UIKeyboardType.DecimalPad;
      mtextValue.ShouldChangeCharacters = (textField, range, replacementString) => {
        if(replacementString.Contains("0") || replacementString.Contains("1") || replacementString.Contains("2") || replacementString.Contains("3") ||
           replacementString.Contains("4") || replacementString.Contains("5") || replacementString.Contains("6") || replacementString.Contains("7") ||
           replacementString.Contains("8") || replacementString.Contains("9") || replacementString.Contains(".") || replacementString.Contains("-") || 
           replacementString.Length.Equals(0)){

          if((replacementString.Contains(".") && textField.Text.Contains(".")) || replacementString.Contains(".") && textField.Text.Length.Equals(0)){
            return false;
          }
          if((replacementString.Contains("-") && textField.Text.Contains("-")) || replacementString.Contains("-") && textField.Text.Length > 0){
            return false;
          }
          return true;
        }
        else {
          return false;
        }
      };
      mbuttonText = new UITextField (new CGRect(.582 * mView.Bounds.Width, .548 * mView.Bounds.Height, .389 * mView.Bounds.Width, .205 * mView.Bounds.Height));
      mdeviceType = new UILabel (new CGRect(.028 * mView.Bounds.Width, .169 * mView.Bounds.Height, .682 * mView.Bounds.Width, .174 * mView.Bounds.Height));
      popupText = new UILabel (new CGRect(0,0,mView.Bounds.Width,.158 * mView.Bounds.Height));
      mbuttonBorder = new UIView (new CGRect(0,.753 * mView.Bounds.Height,mView.Bounds.Width,1));
      mbuttonBorder.BackgroundColor = UIColor.LightGray;
      mbuttonBorder2 = new UIView(new CGRect(.5 * mView.Bounds.Width,.753 * mView.Bounds.Height,1,.246 * mView.Bounds.Height));
      mbuttonBorder2.BackgroundColor = UIColor.LightGray;
      textValidation = new UILabel(new CGRect(.028 * mView.Bounds.Width,.553 * mView.Bounds.Height,.5 *mView.Bounds.Width,.153 * mView.Bounds.Height));
      textValidation.TextColor = UIColor.Red;
      textValidation.Hidden = true;
      textValidation.Lines = 2;
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
    }

    //public UIView mView = new UIView(new CGRect(20, 100, 280, 195));
    public UIView mView;
    //public UIButton mcloseButton = new UIButton(new CGRect(0,147,141,48));
    public UIButton mcloseButton;
    //public UIButton mdoneButton = new UIButton(new CGRect(140, 147, 140,48));
    public UIButton mdoneButton;
    //public UIButton mmeasurementType = new UIButton (new CGRect(150, 104, 122, 44));
    public UIButton mmeasurementType;
    //public UIButton dtypeButton = new UIButton (new CGRect(150,35,122,30));
    public UIButton dtypeButton;
    //public UITextField mtextValue = new UITextField (new CGRect(8,75,264,30));
    public UITextField mtextValue;
    //public UITextField mbuttonText = new UITextField (new CGRect(163,107,109,40));
    public UITextField mbuttonText;
    //public UILabel mdeviceType = new UILabel (new CGRect(8, 33, 133, 34));
    public UILabel mdeviceType;
    //public UILabel popupText = new UILabel (new CGRect(0,0,280,31));
    public UILabel popupText;
    //public UILabel textValidation = new UILabel (new CGRect(0, 108, 140, 30));
    public UILabel textValidation;
    //UIView mbuttonBorder = new UIView (new CGRect(0,147,281,1));
    public UIView mbuttonBorder;
    //UIView mbuttonBorder2 = new UIView(new CGRect(141,147,1,48));
    public UIView mbuttonBorder2;
  }
}

