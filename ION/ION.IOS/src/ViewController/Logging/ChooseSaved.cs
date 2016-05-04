using System;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController.FileManager;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseSaved {
    public UIView showReports;
    public UITableView reportTable;

    public ChooseSaved(UIView mainView) {
      showReports = new UIView(new CGRect(.01 * mainView.Bounds.Width,.05 * mainView.Bounds.Height,.98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      showReports.BackgroundColor = UIColor.White;
      showReports.Layer.BorderColor = UIColor.Black.CGColor;
      showReports.Layer.BorderWidth = 1f;
      showReports.Hidden = true;
      showReports.Layer.CornerRadius = 8;
      showReports.ClipsToBounds = true;

      reportTable = new UITableView(new CGRect(0, 0, showReports.Bounds.Width, .75 * mainView.Bounds.Height));
      reportTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      reportTable.BackgroundColor = UIColor.Clear;
      reportTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      reportTable.Hidden = true;

      showReports.AddSubview(reportTable);
      showReports.BringSubviewToFront(reportTable);
    }
  }
}

