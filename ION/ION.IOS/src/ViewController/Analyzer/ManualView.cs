using System;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Analyzer {
  public class ManualView {
    
    public ManualView() {
      mtextValue.KeyboardType = UIKeyboardType.NumberPad;               
      mView.AddSubview(mcloseButton);
      mView.AddSubview(mdoneButton);
      mView.AddSubview(mmeasurementType);
      mView.AddSubview(dtypeButton);
      mView.AddSubview(mtextValue);
      mView.AddSubview(mbuttonText);
      mView.AddSubview(mdeviceType);
      mView.AddSubview(popupText);
    }

    public UIView mView = new UIView(new CGRect(20, 100, 280, 195));
    public UIButton mcloseButton = new UIButton(new CGRect(0,147,141,48));
    public UIButton mdoneButton = new UIButton(new CGRect(140, 147, 140,48));
    public UIButton mmeasurementType = new UIButton (new CGRect(150, 104, 122, 44));
    public UIButton dtypeButton = new UIButton (new CGRect(150,35,122,30));
    public UITextField mtextValue = new UITextField (new CGRect(8,75,264,30));
    public UITextField mbuttonText = new UITextField (new CGRect(163,107,109,40));
    public UILabel mdeviceType = new UILabel (new CGRect(8, 33, 133, 34));
    public UILabel popupText = new UILabel (new CGRect(0,0,280,31));

  }
}

