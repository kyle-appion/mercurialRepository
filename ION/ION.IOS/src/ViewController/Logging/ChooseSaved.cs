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
    public UILabel savedHeader;
    public UIButton spreadsheetButton;
    public UIButton pdfButton;
    public UILabel middleBorder;
    public UILabel bottomBorder;

    public ChooseSaved(UIView mainView) {
      showReports = new UIView(new CGRect(.01 * mainView.Bounds.Width, .02 * mainView.Bounds.Height,.98 * mainView.Bounds.Width, .09 * mainView.Bounds.Height));
      showReports.BackgroundColor = UIColor.White;
      showReports.Layer.CornerRadius = 5;
      showReports.Layer.BorderColor = UIColor.Black.CGColor;
      showReports.Layer.BorderWidth = 1f;
      showReports.Hidden = true;
      showReports.ClipsToBounds = true;

      var cellHeight = .07f * mainView.Bounds.Height;

      savedHeader = new UILabel(new CGRect(0,0,showReports.Bounds.Width, .061 * mainView.Bounds.Height));
      savedHeader.Layer.CornerRadius = 8f;
      savedHeader.Text = "Report Selection";
      savedHeader.TextAlignment = UITextAlignment.Center;
      savedHeader.Font = UIFont.BoldSystemFontOfSize(20);
      savedHeader.BackgroundColor = UIColor.FromRGB(95,212,48);
      savedHeader.Hidden = true;

      ///button to switch to job listing
      spreadsheetButton = new UIButton(new CGRect(0,.06 * mainView.Bounds.Height,.49 * mainView.Bounds.Width, .06 * mainView.Bounds.Height));
      spreadsheetButton.Layer.BorderColor = UIColor.Black.CGColor;
      spreadsheetButton.SetTitle("Spreadsheet", UIControlState.Normal);
      spreadsheetButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      spreadsheetButton.BackgroundColor = UIColor.White;
      spreadsheetButton.Hidden = true;

      ///user feedback for button press
      spreadsheetButton.TouchUpInside += switchSpreadsheetTab;
      spreadsheetButton.TouchDown += (sender, e) => { spreadsheetButton.BackgroundColor = UIColor.White;};
      spreadsheetButton.TouchUpOutside += (sender, e) => {
        if(!spreadsheetTable.Hidden){
          spreadsheetButton.BackgroundColor = UIColor.White;
        } else {
          spreadsheetButton.BackgroundColor = UIColor.LightGray;
        }
      };
      ///button to switch to session listing
      pdfButton = new UIButton(new CGRect(.49 * mainView.Bounds.Width,.06 * mainView.Bounds.Height,.49 * mainView.Bounds.Width, .06 * mainView.Bounds.Height));
      pdfButton.Layer.BorderColor = UIColor.Black.CGColor;
      pdfButton.SetTitle("PDF", UIControlState.Normal);
      pdfButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      pdfButton.BackgroundColor = UIColor.LightGray;
      pdfButton.Hidden = true;

      ///user feedback for button press
      pdfButton.TouchUpInside += switchPDFTab;
      pdfButton.TouchDown += (sender, e) => { pdfButton.BackgroundColor = UIColor.White;};
      pdfButton.TouchUpOutside += (sender, e) => {
        if(!pdfTable.Hidden){
          pdfButton.BackgroundColor = UIColor.White;
        } else {
          pdfButton.BackgroundColor = UIColor.LightGray;
        }
      };

      middleBorder = new UILabel(new CGRect(.5 * showReports.Bounds.Width,pdfButton.Bounds.Height,2,pdfButton.Bounds.Height));
      middleBorder.BackgroundColor = UIColor.Black;
      middleBorder.Layer.CornerRadius = 8f;
      middleBorder.Hidden = true;

      bottomBorder = new UILabel(new CGRect(.45 * pdfButton.Bounds.Width,1.76 * pdfButton.Bounds.Height,.2 * pdfButton.Bounds.Width,.08 * pdfButton.Bounds.Height));
      bottomBorder.BackgroundColor = UIColor.Blue;
      bottomBorder.Layer.ShadowColor = UIColor.Black.CGColor;
      bottomBorder.Layer.ShadowOpacity = .1f;
      bottomBorder.Layer.ShadowRadius = .3f;
      bottomBorder.Layer.ShadowOffset = new CGSize(0f, 1f);
      bottomBorder.Layer.ShouldRasterize = true;
      bottomBorder.Layer.MasksToBounds = true;
      bottomBorder.Hidden = true;

      spreadsheetTable = new UITableView(new CGRect(0, 2 * pdfButton.Bounds.Height, showReports.Bounds.Width, 9 * cellHeight));
      spreadsheetTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      spreadsheetTable.BackgroundColor = UIColor.Clear;
      spreadsheetTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      spreadsheetTable.Hidden = true;


      pdfTable = new UITableView(new CGRect(0, 2 * pdfButton.Bounds.Height, showReports.Bounds.Width, 9 * cellHeight));
      pdfTable.RegisterClassForCellReuse(typeof(SpreadsheetCell),"spreadsheetCell");
      pdfTable.BackgroundColor = UIColor.Clear;
      pdfTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      pdfTable.Hidden = true;

      showReports.AddSubview(savedHeader);
      showReports.AddSubview(spreadsheetButton);
      showReports.AddSubview(pdfButton);
      showReports.AddSubview(middleBorder);
      showReports.AddSubview(bottomBorder);
      showReports.AddSubview(spreadsheetTable);
      showReports.BringSubviewToFront(spreadsheetTable);
      showReports.AddSubview(pdfTable);
      showReports.BringSubviewToFront(pdfTable);
      spreadsheetButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
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

      pdfButton.Enabled = false;
      spreadsheetTable.Hidden = true;
      pdfTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        var newCenter = bottomBorder.Frame;
        newCenter.X = .7f * showReports.Bounds.Width;
        bottomBorder.Frame = newCenter;
      },() => {
        GetAllPDFS();
      });
    } 

    public void GetAllPDFS(){
      spreadsheetButton.Enabled = true;
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

      spreadsheetButton.Enabled = false;
      pdfTable.Hidden = true;
      spreadsheetTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        var newCenter = bottomBorder.Frame;
        newCenter.X = .4f * spreadsheetButton.Bounds.Width;
        bottomBorder.Frame = newCenter;
      },() => {
        GetAllSpreadsheets();
      });
    }

    public void GetAllSpreadsheets(){
      pdfButton.Enabled = true;
    }

  }
}

