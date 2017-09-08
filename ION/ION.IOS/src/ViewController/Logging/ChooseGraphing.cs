using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseGraphing : UIView {

    public UIView graphingType;
    public UILabel header;

    public nfloat cellHeight;

    public GraphingView graphingView;
    public LegendView legendView;
    public ChooseData checkData;

    public ChooseGraphing(UIView containerView, ChooseData dataSection) {
      graphingType = new UIView(new CGRect(0, 0, containerView.Bounds.Width, .9 * containerView.Bounds.Height));
      graphingType.BackgroundColor = UIColor.White;
      graphingType.Layer.BorderColor = UIColor.Black.CGColor;
      graphingType.Layer.BorderWidth = 1f;
      graphingType.Layer.CornerRadius = 5;

      cellHeight = .07f * containerView.Bounds.Height;

      checkData = dataSection;
    }
  }
}

