using System;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController.FileManager;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseSaved {
    public UIView showReports;
    public UITableView spreadsheetTable;
    public UITableView pdfTable;
    public UIButton spreadsheetButton;
    public UIButton pdfButton;
		public UILabel savedHeader;
		public UILabel pdfHighlight;
		public UILabel spreadsheetHighlight;

		public ChooseSaved(UIView containerView) {
      showReports = new UIView(new CGRect(0, 55, containerView.Bounds.Width, .85 * containerView.Bounds.Height));
      showReports.BackgroundColor = UIColor.White;
      showReports.Layer.CornerRadius = 5;
      showReports.Layer.BorderColor = UIColor.Black.CGColor;
      showReports.Layer.BorderWidth = 1f;
      showReports.Hidden = true;
      showReports.ClipsToBounds = true;

      var cellHeight = .07f * containerView.Bounds.Height;

      savedHeader = new UILabel(new CGRect(0,0,showReports.Bounds.Width, 40));
      savedHeader.Layer.CornerRadius = 8f;
      savedHeader.Text = Util.Strings.Report.SELECTION;
      savedHeader.TextAlignment = UITextAlignment.Center;
      savedHeader.Font = UIFont.BoldSystemFontOfSize(20);
      savedHeader.BackgroundColor = UIColor.FromRGB(230, 103, 39);

			///button to switch to session listing
			pdfButton = new UIButton(new CGRect(0, 40, .5 * containerView.Bounds.Width, 40));
			pdfButton.Layer.BorderColor = UIColor.Black.CGColor;
			pdfButton.SetTitle("PDF", UIControlState.Normal);
			pdfButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			pdfButton.BackgroundColor = UIColor.LightGray;

			///user feedback for button press
			pdfButton.TouchUpInside += switchPDFTab;

      pdfHighlight = new UILabel(new CGRect(0, 80, pdfButton.Bounds.Width,5));
      pdfHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

      ///button to switch to job listing
      spreadsheetButton = new UIButton(new CGRect(.5 * containerView.Bounds.Width,40,.5 * containerView.Bounds.Width, 40));
      spreadsheetButton.Layer.BorderColor = UIColor.Black.CGColor;
      spreadsheetButton.SetTitle(Util.Strings.SPREADSHEET, UIControlState.Normal);
      spreadsheetButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      spreadsheetButton.BackgroundColor = UIColor.White;

      ///user feedback for button press
      spreadsheetButton.TouchUpInside += switchSpreadsheetTab;

			spreadsheetHighlight = new UILabel(new CGRect(.5 * containerView.Bounds.Width, 80, spreadsheetButton.Bounds.Width, 5));
			spreadsheetHighlight.BackgroundColor = UIColor.Black;

      spreadsheetTable = new UITableView(new CGRect(0, 85, showReports.Bounds.Width, 9 * cellHeight));
      spreadsheetTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      spreadsheetTable.BackgroundColor = UIColor.Clear;
      spreadsheetTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

      pdfTable = new UITableView(new CGRect(0, 85, showReports.Bounds.Width, 9 * cellHeight));
      pdfTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      pdfTable.BackgroundColor = UIColor.Clear;
      pdfTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

      showReports.AddSubview(savedHeader);
			showReports.AddSubview(spreadsheetButton);
			showReports.AddSubview(spreadsheetHighlight);
			showReports.AddSubview(pdfButton);
			showReports.AddSubview(pdfHighlight);
      showReports.AddSubview(spreadsheetTable);
      showReports.BringSubviewToFront(spreadsheetTable);
      showReports.AddSubview(pdfTable);
      showReports.BringSubviewToFront(pdfTable);
      pdfButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
    }

    /// <summary>
    /// Animate the pdf tab and then update the pdfs
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void switchPDFTab(object sender, EventArgs e){
      spreadsheetButton.BackgroundColor = UIColor.LightGray;
      spreadsheetButton.Font = UIFont.SystemFontOfSize(18);
      pdfButton.BackgroundColor = UIColor.White;
      pdfButton.Font = UIFont.BoldSystemFontOfSize(22);
      pdfHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);
      spreadsheetHighlight.BackgroundColor = UIColor.Black;

      spreadsheetTable.Hidden = true;
      pdfTable.Hidden = false;
    } 

    /// <summary>
    /// Animate the switch to the spreadsheet tab and then load spreadsheets
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void switchSpreadsheetTab(object sender, EventArgs e){
      pdfButton.BackgroundColor = UIColor.LightGray;
      pdfButton.Font = UIFont.SystemFontOfSize(18);
      spreadsheetButton.BackgroundColor = UIColor.White;
      spreadsheetButton.Font = UIFont.BoldSystemFontOfSize(22);
			pdfHighlight.BackgroundColor = UIColor.Black;
			spreadsheetHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

      pdfTable.Hidden = true;
      spreadsheetTable.Hidden = false;
    }
  }
}

