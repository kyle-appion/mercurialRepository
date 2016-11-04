using System;
using CoreGraphics;
using Foundation;
using UIKit;


namespace ION.IOS.ViewController.Logging {
	public class ReportType {
	
		public UIView blackoutView;
		public UIView popupView;
		public UIView pdfView;
		public UIView spreadsheetView;
		
		public UIButton pdfButton;
		public UIButton spreadsheetButton;
		
		public UIButton pdfExport;
		public UIButton spreadsheetExport;
		public UIButton pdfCancel;
		public UIButton spreadsheetCancel;
		public UIButton pdfSingle;
		public UIButton pdfDetailed;
		public UIButton spreadsheetCSV;
		public UIButton spreadsheetXLSX;
		
		public UILabel rawLabel;
		public UILabel singleSummary;
		public UILabel detailedSummary;
		public UILabel csvFormat;
		public UILabel xslxFormat;
		
		public UISwitch rawData;
		public UITapGestureRecognizer outsideTap;
		
		public int pdfType = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.StringForKey("user_pdf_default"));
		public int spreadsheetType = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.StringForKey("user_spreadsheet_default"));
		
		public ReportType(UIView parentView) {
				/********************REPORTING DEFAULTS**************************/
				var defaultSpreadsheet = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.StringForKey("user_spreadsheet_default"));
				var defaultData = Convert.ToBoolean(Convert.ToInt32(NSUserDefaults.StandardUserDefaults.StringForKey("user_data_default")));
				var defaultPDF = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.StringForKey("user_pdf_default"));
				/****************************************************************/
				outsideTap = new UITapGestureRecognizer((obj) => {
					closeExport();
				});
				blackoutView = new UIView(new CGRect(0, -.2 * parentView.Bounds.Height,parentView.Bounds.Width, 1.4 * parentView.Bounds.Height));
				blackoutView.BackgroundColor = UIColor.DarkGray;
				blackoutView.Alpha = .7f;
				blackoutView.Hidden = true;
				blackoutView.AddGestureRecognizer(outsideTap);
				
				popupView = new UIView(new CGRect(.05 * parentView.Bounds.Width, .2 * parentView.Bounds.Height, .9 * parentView.Bounds.Width, .6 * parentView.Bounds.Height));
				popupView.Layer.BorderWidth = 2f;
				popupView.BackgroundColor = UIColor.White;
				popupView.Hidden = true;
				popupView.ClipsToBounds = true;
				
				pdfButton = new UIButton(new CGRect(0,0,.5 * popupView.Bounds.Width, .2 * popupView.Bounds.Height));
				pdfButton.SetTitle("PDF", UIControlState.Normal);
				pdfButton.SetTitleColor(UIColor.White, UIControlState.Normal);
				pdfButton.BackgroundColor = UIColor.Black;
				pdfButton.TouchUpInside += switchPDF;
				pdfButton.ClipsToBounds = true;
				
				spreadsheetButton = new UIButton(new CGRect(.5 * popupView.Bounds.Width, 0, .5 * popupView.Bounds.Width, .2 * popupView.Bounds.Height));
				spreadsheetButton.SetTitle(Util.Strings.SPREADSHEET, UIControlState.Normal);
				spreadsheetButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
				spreadsheetButton.TouchUpInside += switchSpreadsheet;
				spreadsheetButton.ClipsToBounds = true;
				
				pdfView = new UIView(new CGRect(0,.2 * popupView.Bounds.Height, popupView.Bounds.Width, .8 * popupView.Bounds.Height));
				pdfView.Layer.BorderWidth = 1f;
				
				pdfSingle = new UIButton(new CGRect(.05 * pdfView.Bounds.Width, .1 * pdfView.Bounds.Height, .12 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				
				if(defaultPDF == 0){
					pdfSingle.SetImage(UIImage.FromBundle("ic_radio_filled"),UIControlState.Normal);
				} else {
					pdfSingle.SetImage(UIImage.FromBundle("ic_radio_blank"),UIControlState.Normal);					
				}
				pdfSingle.TouchUpInside += (sender, e) => {
					setPDF(0);
				};
				
				singleSummary = new UILabel(new CGRect(.2 * pdfView.Bounds.Width, .1 * pdfView.Bounds.Height, .8 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				singleSummary.Text = Util.Strings.Report.ONEPAGE;
				singleSummary.AdjustsFontSizeToFitWidth = true;
				singleSummary.TextAlignment = UITextAlignment.Left;
				
				pdfDetailed = new UIButton(new CGRect(.05 * pdfView.Bounds.Width, .3 * pdfView.Bounds.Height, .12 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				if(defaultPDF == 0){
					pdfDetailed.SetImage(UIImage.FromBundle("ic_radio_blank"),UIControlState.Normal);					
				} else {
					pdfDetailed.SetImage(UIImage.FromBundle("ic_radio_filled"),UIControlState.Normal);
				}
				pdfDetailed.TouchUpInside += (sender, e) => {
					setPDF(1);
				};
				
				detailedSummary = new UILabel(new CGRect(.2 * pdfView.Bounds.Width, .3 * pdfView.Bounds.Height, .8 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				detailedSummary.Text = Util.Strings.Report.DETAILED;
				detailedSummary.AdjustsFontSizeToFitWidth = true;
				detailedSummary.TextAlignment = UITextAlignment.Left;
				
				rawData = new UISwitch(new CGRect(.2 * pdfView.Bounds.Width, .55 * pdfView.Bounds.Height, .2 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				rawData.On = defaultData;
				
				rawLabel = new UILabel(new CGRect(.4 * pdfView.Bounds.Width, .55 * pdfView.Bounds.Height, .6 * pdfView.Bounds.Width, .15 * pdfView.Bounds.Height));
				rawLabel.Text = Util.Strings.Report.RAWDATA;
				rawLabel.AdjustsFontSizeToFitWidth = true;
				rawLabel.TextAlignment = UITextAlignment.Left;
				
				pdfExport = new UIButton(new CGRect(.5 * pdfView.Bounds.Width, .8 * pdfView.Bounds.Height, .5 * pdfView.Bounds.Width, .2 * pdfView.Bounds.Height));
				pdfExport.SetTitle(Util.Strings.Report.EXPORT, UIControlState.Normal);
				pdfExport.SetTitleColor(UIColor.Black, UIControlState.Normal);
				pdfExport.Layer.BorderWidth = 1f;       
				
				pdfCancel = new UIButton(new CGRect(0, .8 * pdfView.Bounds.Height, .5 * pdfView.Bounds.Width, .2 * pdfView.Bounds.Height));
				pdfCancel.SetTitle(Util.Strings.CANCEL, UIControlState.Normal);
				pdfCancel.SetTitleColor(UIColor.Black, UIControlState.Normal);
				pdfCancel.TouchUpInside += (sender, e) => {
					closeExport();
				};
				pdfCancel.Layer.BorderWidth = 1f;
				
				spreadsheetView = new UIView(new CGRect(0,.2 * popupView.Bounds.Height, popupView.Bounds.Width, .8 * popupView.Bounds.Height));
				spreadsheetView.Layer.BorderWidth = 1f;
				spreadsheetView.Hidden = true;
				
				spreadsheetXLSX = new UIButton(new CGRect(.1 * spreadsheetView.Bounds.Width, .2 * spreadsheetView.Bounds.Height, .12 * spreadsheetView.Bounds.Width, .15 * spreadsheetView.Bounds.Height));
				if(spreadsheetType == 0){
					spreadsheetXLSX.SetImage(UIImage.FromBundle("ic_radio_filled"),UIControlState.Normal);
				} else {
					spreadsheetXLSX.SetImage(UIImage.FromBundle("ic_radio_blank"),UIControlState.Normal);
				}
				spreadsheetXLSX.TouchUpInside += (sender, e) => {   
					setSpreadsheet(0);
				};    
				
				xslxFormat = new UILabel(new CGRect(.25 * spreadsheetView.Bounds.Width, .2 * spreadsheetView.Bounds.Height, .15 * spreadsheetView.Bounds.Width, .15 * spreadsheetView.Bounds.Height));
				xslxFormat.Text = "XLSX";
				xslxFormat.TextAlignment = UITextAlignment.Left;
				
				spreadsheetCSV = new UIButton(new CGRect(.1 * spreadsheetView.Bounds.Width, .4 * spreadsheetView.Bounds.Height, .12 * spreadsheetView.Bounds.Width, .15 * spreadsheetView.Bounds.Height));
				if(spreadsheetType == 0){
					spreadsheetCSV.SetImage(UIImage.FromBundle("ic_radio_blank"),UIControlState.Normal);
				} else {
					spreadsheetCSV.SetImage(UIImage.FromBundle("ic_radio_filled"),UIControlState.Normal);
				}
				spreadsheetCSV.TouchUpInside += (sender, e) => {
					setSpreadsheet(1);
				};				
				csvFormat = new UILabel(new CGRect(.25 * spreadsheetView.Bounds.Width, .4 * spreadsheetView.Bounds.Height, .15 * spreadsheetView.Bounds.Width, .15 * spreadsheetView.Bounds.Height));
				csvFormat.Text = "CSV";
				csvFormat.TextAlignment = UITextAlignment.Left;
				
				spreadsheetExport = new UIButton(new CGRect(.5 * spreadsheetView.Bounds.Width, .8 * spreadsheetView.Bounds.Height, .5 * spreadsheetView.Bounds.Width, .2 * spreadsheetView.Bounds.Height));
				spreadsheetExport.SetTitle(Util.Strings.Report.EXPORT, UIControlState.Normal);
				spreadsheetExport.SetTitleColor(UIColor.Black, UIControlState.Normal);
				spreadsheetExport.Layer.BorderWidth = 1f;
				
				spreadsheetCancel = new UIButton(new CGRect(0, .8 * spreadsheetView.Bounds.Height, .5 * spreadsheetView.Bounds.Width, .2 * spreadsheetView.Bounds.Height));
				spreadsheetCancel.SetTitle(Util.Strings.CANCEL, UIControlState.Normal);
				spreadsheetCancel.SetTitleColor(UIColor.Black, UIControlState.Normal);
				spreadsheetCancel.TouchUpInside += (sender, e) => {
					closeExport();
				};
				spreadsheetCancel.Layer.BorderWidth = 1f;
				
				popupView.AddSubview(pdfButton);
				popupView.AddSubview(spreadsheetButton);
				popupView.AddSubview(pdfView);
				pdfView.AddSubview(pdfSingle);
				pdfView.AddSubview(singleSummary);
				//pdfView.AddSubview(pdfDetailed);
				//pdfView.AddSubview(detailedSummary);
				pdfView.AddSubview(rawData);
				pdfView.AddSubview(rawLabel);
				pdfView.AddSubview(pdfExport);
				pdfView.AddSubview(pdfCancel);
				popupView.AddSubview(spreadsheetView);
				spreadsheetView.AddSubview(spreadsheetXLSX);
				spreadsheetView.AddSubview(xslxFormat);
				spreadsheetView.AddSubview(spreadsheetCSV);
				spreadsheetView.AddSubview(csvFormat);
				spreadsheetView.AddSubview(spreadsheetExport);
				spreadsheetView.AddSubview(spreadsheetCancel);
		}
		
		public void closeExport(){
			blackoutView.Hidden = true;
			popupView.Hidden = true;
			popupView.ExclusiveTouch = false;
		}
		
		public void setPDF(int type){
			pdfType = type;
			if(type == 0){
				pdfSingle.SetImage(UIImage.FromBundle("ic_radio_filled"), UIControlState.Normal);
				pdfDetailed.SetImage(UIImage.FromBundle("ic_radio_blank"), UIControlState.Normal);
			} else {
				pdfSingle.SetImage(UIImage.FromBundle("ic_radio_blank"), UIControlState.Normal);
				pdfDetailed.SetImage(UIImage.FromBundle("ic_radio_filled"), UIControlState.Normal);
			}
		}
		
		public void setSpreadsheet(int type){
			spreadsheetType = type;
			if(type == 0){
				spreadsheetXLSX.SetImage(UIImage.FromBundle("ic_radio_filled"), UIControlState.Normal);
				spreadsheetCSV.SetImage(UIImage.FromBundle("ic_radio_blank"), UIControlState.Normal);
			} else {
				spreadsheetXLSX.SetImage(UIImage.FromBundle("ic_radio_blank"), UIControlState.Normal);
				spreadsheetCSV.SetImage(UIImage.FromBundle("ic_radio_filled"), UIControlState.Normal);
			}
		}

		public void switchPDF(object sender, EventArgs e){
			spreadsheetButton.BackgroundColor = UIColor.White;
			spreadsheetButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			
			pdfButton.BackgroundColor = UIColor.Black;
			pdfButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			spreadsheetView.Hidden = true;		
			pdfView.Hidden = false;
		}

		public void switchSpreadsheet(object sender, EventArgs e){
			spreadsheetButton.BackgroundColor = UIColor.Black;
			spreadsheetButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			
			pdfButton.BackgroundColor = UIColor.White;
			pdfButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			
			spreadsheetView.Hidden = false;
			pdfView.Hidden = true;
		}	
	}
}
