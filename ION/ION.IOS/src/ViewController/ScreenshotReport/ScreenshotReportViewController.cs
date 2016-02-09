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

    private IItem reportTitle { get; set; }
    private DateTime date { get; set; }
    private IItem notes { get; set; }
    private UITapGestureRecognizer noKeyboard{get; set;}
    private List<IItem> items { get; set; }

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
        savingDialog.RemoveFromParentViewController();
        if (result.success) {
          Log.D(this, "Dismissed view controller");
          ION.Core.App.AppState.context.PostToMain(() => NavigationController.DismissViewControllerAsync(true) );
          closer();
        } else {
          var dialog = UIAlertController.Create(Strings.Errors.SCREENSHOT, Strings.Errors.SCREENSHOT_MISSING_TITLE, UIAlertControllerStyle.Alert);
          dialog.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null)); 
          PresentViewController(dialog, true, null);
        }
      });

      reportTitle = new EntryItem(Strings.Report.TITLE);
      date = DateTime.Now;
      notes = new NotesItem(Strings.Report.NOTES);
      //notes = new UITextField(new CGRect(0,0,100, 60));
      items = new List<IItem>();
      items.Add(new EntryItem(Strings.Report.CITY));
      items.Add(new EntryItem(Strings.Report.STATE));
      items.Add(new EntryItem(Strings.Report.ZIP));

      var sourceList = new List<IItem>();

      sourceList.Add(reportTitle);
      sourceList.Add(new DisplayItem(Strings.DATE, date.ToLocalTime().ToShortDateString()));
      sourceList.AddRange(items);
      //sourceList.Add(notes);

      source = new ScreenshotReportSource(sourceList);
      table.Source = source;

      imageScreenshot.Image = image;
    }

    /// <summary>
    /// Commits the current state of the screenshot report to a file.
    /// </summary>
    private Task<Result> CommitScreenshotReport() {
      return Task.Factory.StartNew(() => {
        var report = new ScreenshotReport();
        report.created = date;
        report.title = Strings.Report.SCREENSHOT_TITLE;
        report.subtitle = reportTitle.value;
        report.notes = notes.value;
        report.screenshot = image.AsPNG().ToArray();

        if (report.subtitle == null || report.subtitle.Equals("")) {
          return new Result(Strings.Errors.SCREENSHOT_MISSING_TITLE);
        }

        var data = new string[items.Count, 2];
        for (int i = 0; i < items.Count; i++) {
          var item = items[i];
          data[i, 0] = item.header;
          data[i, 1] = item.value;
        }

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
      });
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
