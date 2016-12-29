namespace ION.IOS.ViewController.ScreenshotReport {
  
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.IO;
  using ION.Core.Pdf;
  using ION.Core.Report;
  using ION.Core.Util;

  using ION.IOS.Util;
  using ION.IOS.ViewController;

	public partial class ScreenshotReportViewController : BaseIONViewController {

    public Action closer { get; set; }
    public UIImage image { get; set; }
    public AdditionalView reportData;
    
    public string subtitle;
    public string address1;
    public string address2;
    public string city;
    public string state;
    public string zipcode;
    public string appversion;

    //private IItem reportTitle { get; set; }
    private DateTime date { get; set; }
    private IItem notes { get; set; }
    private UITapGestureRecognizer noKeyboard{get; set;}
    //private List<IItem> items { get; set; }

    private ScreenshotReportSource source { get; set; }
    
		public ScreenshotReportViewController (IntPtr handle) : base (handle) {
		}

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      noKeyboard = new UITapGestureRecognizer(() => {
        View.EndEditing(true);
      });

      View.AddGestureRecognizer(noKeyboard);
      NavigationItem.Title = Strings.Report.SCREENSHOT;

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.SAVE, UIBarButtonItemStyle.Plain, async delegate {
        var savingDialog = UIAlertController.Create(Strings.PLEASE_WAIT, Strings.SAVING, UIAlertControllerStyle.Alert);
        PresentViewController(savingDialog, true, null);

        var result = await CommitScreenshotReport();

				savingDialog.DismissViewController(true, null);   

				await Task.Delay(500);

				// TODO ahodder@appioninc.com: We need to switch on the error, some errors are not title errors.
        if (result.success) {
          ION.Core.App.AppState.context.PostToMain(() => NavigationController.DismissViewControllerAsync(true) );
          closer();
        } else {
          var dialog = UIAlertController.Create(Strings.Errors.SCREENSHOT, Strings.Errors.SCREENSHOT_MISSING_TITLE, UIAlertControllerStyle.Alert);
          dialog.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null)); 
          PresentViewController(dialog, true, null);
        }
      });

      //reportTitle = new EntryItem(Strings.Report.TITLE);
      date = DateTime.Now;
      reportData = new AdditionalView(View);
      View.AddSubview(reportData.detailsView);
      reportData.dateValue.Text =  date.ToLocalTime().ToShortDateString();
      notes = new NotesItem(Strings.Report.NOTES);
      ////notes = new UITextField(new CGRect(0,0,100, 60));
      //items = new List<IItem>();
      //items.Add(new EntryItem(Strings.Report.ADDRESS));
      //items.Add(new EntryItem(Strings.Report.CITY));
      //items.Add(new EntryItem(Strings.Report.STATE));
      //items.Add(new EntryItem(Strings.Report.ZIP));
      //items.Add(new NotesItem(Strings.Report.NOTES));

      //var sourceList = new List<IItem>();

      //sourceList.Add(reportTitle);
      //sourceList.Add(new DisplayItem(Strings.DATE, date.ToLocalTime().ToShortDateString()));
      //sourceList.AddRange(items);
      //sourceList.Add(notes);

      //source = new ScreenshotReportSource(sourceList);
      //table.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      //table.Source = source;

      imageScreenshot.Image = image;
    }

    /// <summary>
    /// Commits the current state of the screenshot report to a file.
    /// </summary>
    private Task<Result> CommitScreenshotReport() {
    	subtitle = reportData.titleField.Text;
    	address1 = reportData.addressField1.Text;
    	address2 = reportData.addressField2.Text;
    	city = reportData.cityField.Text;
    	state = reportData.stateField.Text;
    	zipcode = reportData.zipcodeField.Text;
    	appversion = reportData.versionValue.Text;
      return Task.Factory.StartNew(SaveScreenshot);
    }

		private Result SaveScreenshot() {
			var report = new ScreenshotReport();

			///save date in original format for localization later
			//report.created = date;
			report.title = Strings.Report.SCREENSHOT_TITLE;
			//report.subtitle = reportTitle.value;
			report.subtitle = subtitle;
			//report.notes = notes.value;
			report.notes = notes.value;
			report.screenshot = image.AsPNG().ToArray();

			//if (report.subtitle == null || report.subtitle.Equals("")) {
			//	return new Result(Strings.Errors.SCREENSHOT_MISSING_TITLE);
			//}
			if (string.IsNullOrEmpty(subtitle)) {
				return new Result(Strings.Errors.SCREENSHOT_MISSING_TITLE);
			}
			/// adding an extra spot to manually add the localized date
			/// return here for possible raw data storage for different
			/// exporting formats
			//var data = new string[items.Count + 1, 2];
			var data = new string[7, 2];
			data[0,0] = Strings.DATE;
			data[0,1] = date.ToLocalTime().ToShortDateString();

			//for (int i = 1; i <= items.Count; i++) {
			//	var item = items[i - 1];
			//	data[i, 0] = item.header;
			//	data[i, 1] = item.value;
			//}
			data[1,0] = "App Version";
			data[1,1] = appversion;		
			data[2,0] = "Address Line 1";
			data[2,1] = address1;
			data[3,0] = "Address Line 2";
			data[3,1] = address2;
			data[4,0] = "City";
			data[4,1] = city;
			data[5,0] = "State/Province/Region";
			data[5,1] = state;
			data[6,0] = "ZIP/Postal Code";
			data[6,1] = zipcode;
			
			report.tableData = data;

			try {
				var dir = AppState.context.screenshotReportFolder;
				var file = dir.GetFile(report.subtitle + ".pdf", EFileAccessResponse.CreateIfMissing);
				using (var stream = file.OpenForWriting()) {
					ScreenshotReportPdfExporter.Export(report, stream);
				}
			} catch (Exception e) {
				// TODO ahodder@appioninc.com: this needs a user-friendly dialog that will post on catch
				Log.E(this, "Failed to export pdf", e);
			}

			return new Result();
		}
	}

  internal class Result {
    public bool success { get; private set; }
    public string errorReason { get; private set; }

    /// <summary>
    /// Creates a new success result.
    /// </summary>
    public Result() {
      success = true;
    }

    /// <summary>
    /// Creates a new error result.
    /// </summary>
    /// <param name="errorReason">Error reason.</param>
    public Result(string errorReason) {
      success = false;
      this.errorReason = errorReason;
    }
  }
}
