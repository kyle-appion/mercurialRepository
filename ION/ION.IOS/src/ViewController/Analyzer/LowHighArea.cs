using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Analyzer
{


  public class LowHighArea
  {

    public lowHighSensor lowArea;
    public lowHighSensor highArea;

    public List<string> availableSubviews = new List<string> {
      Util.Strings.Workbench.Viewer.HOLD_DESC,Util.Strings.Workbench.Viewer.MAX_DESC, Util.Strings.Workbench.Viewer.MIN_DESC, Util.Strings.Workbench.Viewer.ALT_DESC,Util.Strings.Workbench.Viewer.ROC_DESC, Util.Strings.Analyzer.SHSCDESC, Util.Strings.Analyzer.PTDESC
    };
    
    public LowHighArea (UIView mainView, AnalyzerViewController ViewController, sensorGroup analyzerSensors)
    {
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.25 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
      lowArea.location = "low";
      lowArea.undefinedText = Strings.Analyzer.LOWUNDEFINED;
      lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      lowArea.LabelSubview.BackgroundColor = UIColor.Blue;

			lowArea.snapArea.BackgroundColor = UIColor.White;
			lowArea.snapArea.Alpha = 1f;
			lowArea.snapArea.UserInteractionEnabled = true;
			lowArea.snapArea.AccessibilityIdentifier = "low";
			lowArea.snapArea.Layer.CornerRadius = 5;

			lowArea.LabelTop.AdjustsFontSizeToFitWidth = true;
			lowArea.LabelTop.Text = "";
			lowArea.LabelTop.Layer.CornerRadius = 5;
			lowArea.LabelTop.ClipsToBounds = true;

			lowArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
			lowArea.LabelMiddle.Text = Util.Strings.Analyzer.LOWUNDEFINED;
			lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

			lowArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
			lowArea.LabelBottom.Text = "";
			lowArea.LabelBottom.TextAlignment = UITextAlignment.Right;
			lowArea.LabelBottom.BackgroundColor = UIColor.Clear;

			lowArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
			lowArea.LabelSubview.Text = "";
			lowArea.LabelSubview.TextColor = UIColor.White;
			lowArea.LabelSubview.ClipsToBounds = true;

			lowArea.subviewTable.BackgroundColor = UIColor.Clear;
			lowArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			lowArea.subviewTable.Hidden = true;
			lowArea.subviewTable.Source = null;

			lowArea.subviewHide.BackgroundColor = UIColor.Blue;
			lowArea.subviewHide.SetImage(null, UIControlState.Normal);
			lowArea.subviewHide.Hidden = true;

			lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
			lowArea.Connection.BackgroundColor = UIColor.Clear;
			lowArea.Connection.Hidden = true;
			lowArea.connectionColor.Layer.CornerRadius = 5;
			lowArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
			lowArea.connectionColor.Layer.BorderWidth = 1f;
			lowArea.connectionColor.Hidden = true;
			lowArea.subviewDivider.BackgroundColor = UIColor.Black;
			lowArea.headingDivider.Hidden = true;
			lowArea.headingDivider.BackgroundColor = UIColor.Black;

			lowArea.snapArea.AddSubview(lowArea.LabelTop);
			lowArea.snapArea.AddSubview(lowArea.LabelMiddle);
			lowArea.snapArea.AddSubview(lowArea.LabelBottom);
			lowArea.snapArea.AddSubview(lowArea.LabelSubview);
			lowArea.snapArea.AddSubview(lowArea.DeviceImage);
			lowArea.snapArea.AddSubview(lowArea.Connection);
			lowArea.snapArea.AddSubview(lowArea.subviewDivider);
			lowArea.snapArea.AddSubview(lowArea.headingDivider);
			lowArea.snapArea.AddSubview(lowArea.subviewHide);
			lowArea.snapArea.BringSubviewToFront(lowArea.headingDivider);
			lowArea.snapArea.AddSubview(lowArea.connectionColor);
			lowArea.snapArea.BringSubviewToFront(lowArea.Connection);
			lowArea.snapArea.AddSubview(lowArea.conDisButton);
			lowArea.snapArea.BringSubviewToFront(lowArea.conDisButton);
			lowArea.snapArea.BringSubviewToFront(lowArea.subviewDivider);
      
      highArea = new lowHighSensor (new CGRect (.507 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.507 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.25 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
      highArea.location = "high";
			highArea.undefinedText = Strings.Analyzer.HIGHUNDEFINED;
			highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      highArea.LabelSubview.BackgroundColor = UIColor.Red;

      highArea.snapArea.BackgroundColor = UIColor.White;
      highArea.snapArea.Alpha = 1f;
      highArea.snapArea.UserInteractionEnabled = true;
      highArea.snapArea.AccessibilityIdentifier = "high";
      highArea.snapArea.Layer.CornerRadius = 5;

      highArea.LabelTop.AdjustsFontSizeToFitWidth = true;
      highArea.LabelTop.Text = "";
      highArea.LabelTop.Layer.CornerRadius = 5;
      highArea.LabelTop.ClipsToBounds = true;

      highArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      highArea.LabelMiddle.Text = Util.Strings.Analyzer.HIGHUNDEFINED;
      highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

      highArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
      highArea.LabelBottom.Text = "";
      highArea.LabelBottom.TextAlignment = UITextAlignment.Right;

      highArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
      highArea.LabelSubview.Text = "";
      highArea.LabelSubview.TextColor = UIColor.White;
      highArea.LabelSubview.ClipsToBounds = true;

      highArea.subviewTable.BackgroundColor = UIColor.Clear;
      highArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      highArea.subviewTable.Hidden = true;
      highArea.subviewTable.Source = null;

      highArea.subviewHide.BackgroundColor = UIColor.Red;
      highArea.subviewHide.SetImage(null, UIControlState.Normal);
      highArea.subviewHide.Hidden = true;

      highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      highArea.Connection.Hidden = true;
      highArea.Connection.BackgroundColor = UIColor.Clear;
      highArea.connectionColor.Layer.CornerRadius = 5;
      highArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      highArea.connectionColor.Layer.BorderWidth = 1f;
      highArea.connectionColor.Hidden = true;
      highArea.subviewDivider.BackgroundColor = UIColor.Black;
      highArea.headingDivider.Hidden = true;
      highArea.headingDivider.BackgroundColor = UIColor.Black;

      highArea.snapArea.AddSubview(highArea.LabelTop);
      highArea.snapArea.AddSubview(highArea.LabelMiddle);
      highArea.snapArea.AddSubview(highArea.LabelBottom);
      highArea.snapArea.AddSubview(highArea.LabelSubview);
      highArea.snapArea.AddSubview(highArea.DeviceImage);
      highArea.snapArea.AddSubview(highArea.Connection);
      highArea.snapArea.AddSubview(highArea.subviewDivider);
      highArea.snapArea.AddSubview(highArea.headingDivider);
      highArea.snapArea.AddSubview(highArea.subviewHide);
      highArea.snapArea.BringSubviewToFront(highArea.headingDivider);
      highArea.snapArea.AddSubview(highArea.connectionColor);
      highArea.snapArea.BringSubviewToFront(highArea.Connection);
      highArea.snapArea.AddSubview(highArea.conDisButton);
      highArea.snapArea.BringSubviewToFront(highArea.conDisButton);
      highArea.snapArea.BringSubviewToFront(highArea.subviewDivider);

			lowArea.hideLHUI();
			highArea.hideLHUI();
    }

  }
}

