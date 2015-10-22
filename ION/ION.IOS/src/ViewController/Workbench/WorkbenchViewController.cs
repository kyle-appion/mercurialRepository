namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Collections.Generic;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Content.Parsers;
  using ION.Core.Devices;
  using ION.Core.IO;
  using ION.Core.Measure;
  using ION.Core.Pdf;
  using ION.Core.Report;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.IOS.Devices;
  using ION.IOS.Sensors;
  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.DeviceManager;
  using ION.IOS.ViewController.ScreenshotReport;

	public partial class WorkbenchViewController : BaseIONViewController {
    /// <summary>
    /// The current ion context.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }
    /// <summary>
    /// The workbench that we are working with.
    /// </summary>
    /// <value>The workbench.</value>
    private ION.Core.Content.Workbench workbench { get; set; }
    /// <summary>
    /// The source that will provide Viewer views to the table view.
    /// </summary>
    /// <value>The source.</value>
    private WorkbenchSource source { get; set; }

		public WorkbenchViewController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };

      NavigationItem.RightBarButtonItem = new UIBarButtonItem("NULL", UIBarButtonItemStyle.Plain, delegate {
        ShowOptions();
      });

      Title = Strings.Workbench.SELF.FromResources();

      ion = AppState.context;
      workbench = ion.currentWorkbench;

      tableContent.AllowsSelection = true;

      source = new WorkbenchSource(this, workbench);
      source.onRequestViewerDelegate = OnRequestViewer;

      tableContent.Source = source;

      ion.currentWorkbench.onManifoldAdded += OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved += OnManifoldRemoved;
    }

    // Overridden from BaseIONViewController
    public override void ViewDidAppear(bool animated) {
      base.ViewDidAppear(animated);

      tableContent.ReloadData();
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      ion.currentWorkbench.onManifoldAdded -= OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved -= OnManifoldRemoved;
    }

    /// <summary>
    /// Called when the viewer source wishes to request a new viewer.
    /// </summary>
    private void OnRequestViewer() {
      var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
        workbench.AddSensor(sensor);
      };
      // TODO ahodder@appioninc.com: Set initialial arguments.
      NavigationController.PushViewController(sb, true);
      //          Toast.New(tableView, "Clicked 'Add New Viewer'");
    }

    /// <summary>
    /// Called when the workbench adds a new manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private async void OnManifoldAdded(ION.Core.Content.Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
      await ion.SaveWorkbenchAsync();
    }

    /// <summary>
    /// Called when the workbench removes a manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private async void OnManifoldRemoved(ION.Core.Content.Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
      await ion.SaveWorkbenchAsync();
    }

    private void ShowOptions() {
      try {
        var vc = InflateViewController<ScreenshotReportViewController>(VC_SCREENSHOT_REPORT);
        vc.image = View.Capture();
        NavigationController.PushViewController(vc, true);
/*
        var report = new ScreenshotReport();

        report.title = Strings.Report.SCREENSHOT;
        report.subtitle = "Test Report 2";

        report.tableData = new string[,] {
          { Strings.Report.CITY, "Englewood" },
          { Strings.Report.STATE, "Colorado" },
          { Strings.Report.ZIP, "80110" },
        };

        report.notes = "A lovely bunch of coconuts are banged together to make a significant amount of noice to indicate the approach of the king.";

        var image = View.Capture();
        var data = image.AsPNG();
        report.screenshot = data.ToArray();


        var folder = ion.screenshotReportFolder;
        var file = folder.GetFile(report.subtitle + ".pdf", EFileAccessResponse.CreateIfMissing);

        using (var stream = file.OpenForWriting()) {
          ScreenshotReportPdfExporter.Export(report, stream);
        }
*/
//        var d = UIDocumentInteractionController.FromUrl(new NSUrl(file.fullPath, false));
//        d.PresentOpenInMenu(View.Frame, View, true);
      } catch (Exception e) {
        Log.E(this, "Failed to create pdf", e);
      }
    }
	}
}
