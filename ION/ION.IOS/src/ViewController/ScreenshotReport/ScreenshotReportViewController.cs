namespace ION.IOS.ViewController.ScreenshotReport {
  
  using System;
  using System.Collections.Generic;

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

    public UIImage image { get; set; }

    private IItem reportTitle { get; set; }
    private DateTime date { get; set; }
    private IItem notes { get; set; }
    private List<IItem> items { get; set; }

    private ScreenshotReportSource source { get; set; }
    
		public ScreenshotReportViewController (IntPtr handle) : base (handle) {
		}

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.SAVE, UIBarButtonItemStyle.Plain, delegate {
        CommitScreenshotReport();
      });

      reportTitle = new EntryItem(Strings.Report.NAME);
      date = DateTime.Now;
      notes = new NotesItem(Strings.Report.NOTES);


      items = new List<IItem>();
      items.Add(new EntryItem(Strings.Report.CITY));
      items.Add(new EntryItem(Strings.Report.STATE));
      items.Add(new EntryItem(Strings.Report.ZIP));

      var sourceList = new List<IItem>();

      sourceList.Add(reportTitle);
      sourceList.Add(new DisplayItem(Strings.DATE, date.ToLocalTime().ToString()));
      sourceList.AddRange(items);
      sourceList.Add(notes);

      source = new ScreenshotReportSource(sourceList);
      table.Source = source;

      imageScreenshot.Image = image;
    }

    /// <summary>
    /// Commits the current state of the screenshot report to a file.
    /// </summary>
    private void CommitScreenshotReport() {
      var report = new ScreenshotReport();
      report.created = date;
      report.title = reportTitle.value;
      report.notes = notes.value;
      report.screenshot = image.AsPNG().ToArray();

      var data = new string[items.Count, 2];
      for (int i = 0; i < items.Count; i++) {
        var item = items[i];
        data[i, 0] = item.header;
        data[i, 1] = item.value;
      }

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

      RemoveFromParentViewController();
    }
	}
}
