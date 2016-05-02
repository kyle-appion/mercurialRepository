using System;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController.FileManager;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseSaved {
    public UIView showReports;
    public UITableView reportTable;
    public UILabel header;
    public UIActivityIndicatorView activityLoadingReports;
    private nfloat cellHeight;

    public ChooseSaved(UIView mainView) {
      showReports = new UIView(new CGRect(.01 * mainView.Bounds.Width,.05 * mainView.Bounds.Height,.98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      showReports.BackgroundColor = UIColor.White;
      showReports.Layer.BorderColor = UIColor.Black.CGColor;
      showReports.Layer.BorderWidth = 1f;
      showReports.Hidden = true;
      showReports.Layer.CornerRadius = 8;
      showReports.ClipsToBounds = true;

      cellHeight = .07f * mainView.Bounds.Height;

      header = new UILabel(new CGRect(0,0,showReports.Bounds.Width, cellHeight));
      header.BackgroundColor = UIColor.FromRGB(95,212,48);
      header.Font = UIFont.BoldSystemFontOfSize(20);
      header.ClipsToBounds = true;
      header.Layer.BorderColor = UIColor.Black.CGColor;
      header.Layer.BorderWidth = 1f;
      header.TextAlignment = UITextAlignment.Center;
      header.Text = "Saved Reports";
      header.TextColor = UIColor.Black;
      header.AdjustsFontSizeToFitWidth = true;
      header.Hidden = true;

      activityLoadingReports = new UIActivityIndicatorView(new CGRect(0,0, showReports.Bounds.Width, showReports.Bounds.Height));

      reportTable = new UITableView(new CGRect(0, cellHeight, showReports.Bounds.Width, .75 * mainView.Bounds.Height - cellHeight));
      reportTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      reportTable.BackgroundColor = UIColor.Clear;
      reportTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      reportTable.Hidden = true;

      showReports.AddSubview(reportTable);
      showReports.BringSubviewToFront(reportTable);
      showReports.AddSubview(header);
      showReports.BringSubviewToFront(header);
    }

  }
}

