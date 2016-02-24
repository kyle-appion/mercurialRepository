using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class ChooseGraphing : UIView {

    public UIView graphingType;
    public UILabel header;
    public UITextView rawData;
    public nfloat cellHeight;

    public ChooseGraphing(UIView mainView) {
      graphingType = new UIView(new CGRect(.01 * mainView.Bounds.Width, .23 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      graphingType.BackgroundColor = UIColor.White;
      graphingType.Layer.BorderColor = UIColor.Black.CGColor;
      graphingType.Layer.BorderWidth = 1f;
      graphingType.Layer.CornerRadius = 8;
      graphingType.Hidden = true;

      cellHeight = .07f * mainView.Bounds.Height;

      header = new UILabel(new CGRect(0,0,graphingType.Bounds.Width, cellHeight));
      header.Layer.BorderColor = UIColor.Black.CGColor;
      header.Layer.BorderWidth = 1f;
      header.TextAlignment = UITextAlignment.Center;
      header.Text = "Graphing";
      header.TextColor = UIColor.Black;
      header.AdjustsFontSizeToFitWidth = true;
      header.Hidden = true;

      rawData = new UITextView(new CGRect(0,cellHeight, graphingType.Bounds.Width, (.72 * mainView.Bounds.Height) - cellHeight));
      rawData.Editable = false;
      rawData.Text = "";
      rawData.TextColor = UIColor.Black;
      rawData.Hidden = true;

      graphingType.AddSubview(header);
      graphingType.BringSubviewToFront(header);
      graphingType.AddSubview(rawData);
    }
  }
}

