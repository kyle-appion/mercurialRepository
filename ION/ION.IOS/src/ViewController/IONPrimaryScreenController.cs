namespace ION.IOS.ViewController {
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Foundation;
  using MessageUI;
  using MonoTouch.Dialog;
  using UIKit;

  using FlyoutNavigation;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Devices.Certificates;
  using ION.Core.IO;
  using ION.Core.Util;

  using ION.IOS.App;
  using ION.IOS.Net;
  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.Analyzer;
  using ION.IOS.ViewController.FileManager;
  using ION.IOS.ViewController.Help;
  using ION.IOS.ViewController.PressureTemperatureChart;
  using ION.IOS.ViewController.Settings;
  using ION.IOS.ViewController.SuperheatSubcool;
  using ION.IOS.ViewController.Workbench;
  using ION.IOS.ViewController.Logging;
  using ION.IOS.ViewController.JobManager;

	public partial class IONPrimaryScreenController : UIViewController {
    /// <summary>
    /// The view controller that will manage the navigation items for the screen.
    /// </summary>
    /// <value>The navigation.</value>
    public FlyoutNavigationController navigation { get; private set; }


		public IONPrimaryScreenController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      // Create the navigation drawer
      navigation = new FlyoutNavigationController();
      var r = UIScreen.MainScreen.Bounds;
      r.Y += 20;
      navigation.View.Frame = r;
      View.AddSubview(navigation.View);
      navigation.NavigationTableView.BackgroundColor = new UIColor(Colors.BLACK);

      navigation.NavigationRoot = new RootElement("BS Navigation Menu") {
        new Section (Strings.Navigation.MAIN.ToUpper()) {
          new IONElement(Strings.Workbench.SELF, UIImage.FromBundle("ic_nav_workbench")),
          new IONElement(Strings.Analyzer.SELF, UIImage.FromBundle("ic_nav_analyzer")),
        },
        new Section (Strings.Navigation.CALCULATORS.ToUpper()) {
          new IONElement(Strings.Fluid.PT_CHART, UIImage.FromBundle("ic_nav_pt_chart")),
          new IONElement(Strings.Fluid.SUPERHEAT_SUBCOOL, UIImage.FromBundle("ic_nav_superheat_subcool")),
        },
        new Section(Strings.Report.REPORTS) {
          new IONElement(Strings.Report.MANAGER, UIImage.FromBundle("ic_job_settings")),
          new IONElement(Strings.Report.LOGGING, UIImage.FromBundle("ic_graph_menu")),
          new IONElement(Strings.Report.CALIBRATION_CERTIFICATES, OnCalibrationCertificateClicked, UIImage.FromBundle("ic_nav_certificate")),
          new IONElement(Strings.Report.SCREENSHOT_ARCHIVE, OnScreenshotArchiveClicked, UIImage.FromBundle("ic_camera")),
        },
        new Section (Strings.Navigation.CONFIGURATION.ToUpper()) {
          new IONElement(Strings.SETTINGS, OnNavSettingsClicked, UIImage.FromBundle("ic_settings")),
          new IONElement(Strings.HELP, OnHelpClicked, UIImage.FromBundle("ic_help")),
        },
      };
      navigation.ViewControllers = BuildViewControllers();
      // Create the menu
    }
    /// <summary>
    /// Opens the application's settings.
    /// </summary>
    private void OnNavSettingsClicked() {
      UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
    }

    /// <summary>
    /// Links the user to a repository of help and knowledge.
    /// </summary>
    private void OnNavHelpClicked() {
      // TODO Do Helpful things
    }

    /// <summary>
    /// Opens up the application's screenshot report archive.
    /// </summary>
    private void OnScreenshotArchiveClicked() {
      try {
        var vc = InflateViewController<FileBrowserViewController>(BaseIONViewController.VC_FILE_MANAGER);
        vc.title = Strings.Report.SCREENSHOT_ARCHIVE;
        vc.rootFolder = AppState.context.screenshotReportFolder;
        PresentViewControllerFromSelected(vc);
      } catch (Exception e) {
        Log.E(this, "Failed to get le folder", e);
      }
    }

    /// <summary>
    /// Opens up a file manager that will allow the perusal of downloaded calibration certificates.
    /// </summary>
    private void OnCalibrationCertificateClicked() {
      try {
        var vc = InflateViewController<FileBrowserViewController>(BaseIONViewController.VC_FILE_MANAGER);
        vc.title = Strings.Report.CALIBRATION_CERTIFICATES;
        vc.rootFolder = AppState.context.calibrationCertificateFolder;
        var image = UIImage.FromBundle("ic_download");
        var button = new UIButton(new CoreGraphics.CGRect(0, 0, 31, 30));
        button.SetImage(image, UIControlState.Normal);
        button.TouchUpInside += (object sender, EventArgs e) => {
          var failures = new List<ISerialNumber>();

          var source = new CancellationTokenSource();
          var task = DoTheThings(source, failures, () => {
            vc.Refresh();
          });
            
          var alert = UIAlertController.Create(Strings.PLEASE_WAIT, Strings.Report.DOWNLOADING_CERTIFICATES, UIAlertControllerStyle.Alert);
          alert.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {
            source.Cancel();
          }));

          alert.Show();

          task.ContinueWith((t) => {
            AppState.context.PostToMainDelayed(() => {
              alert.DismissModalViewController(true);
              vc.Refresh();

              if (failures.Count > 0) {
                Log.D(this, failures.Count + " failures");
                var a = UIAlertController.Create(Strings.Report.DOWNLOADING_CERTIFICATES_FAILURES,
                  string.Format(Strings.Report.FAILED_TO_DOWNLOAD, string.Join(", ", failures)),
                  UIAlertControllerStyle.Alert);
                a.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Cancel, null));
                a.Show();
              }
            }, TimeSpan.FromMilliseconds(500));
          });
        };
        vc.NavigationItem.RightBarButtonItem = new UIBarButtonItem(button);
        PresentViewControllerFromSelected(vc);
      } catch (Exception e) {
        Log.E(this, "Failed to load calibration certificate cache.", e);
      }
    }

    /// <summary>
    /// Starts the task that will download the calibration certificates.
    /// </summary>
    /// <returns>The the things.</returns>
    private Task DoTheThings(CancellationTokenSource source, List<ISerialNumber> failures, Action onLoad) {
      return Task.Factory.StartNew(() => {
        var ion = AppState.context;
        var serials = new List<ISerialNumber>();

        foreach (var device in ion.deviceManager.devices) {
          serials.Add(device.serialNumber);
        }

        var task = new RequestCalibrationCertificatesTask(ion, serials.ToArray());
        task.tokenSource = source;

        foreach (var result in task.Request().Result) {
          if (!result.success) {
            failures.Add(result.serialNumber);
            continue;
          }

          var file = ion.calibrationCertificateFolder.GetFile(result.serialNumber + " Certification.pdf", EFileAccessResponse.ReplaceIfExists);
          var stream = file.OpenForWriting();

          try {
            GaugeDeviceCertificatePdfExporter.Export(ion, result.certificate, stream);
            ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET nistDate = ?",result.certificate.lastTestCalibrationDate);
          } catch (Exception e) {
            Log.E(this, "Failed to export certificate.", e);
            file.Delete();
            failures.Add(result.serialNumber);
          } finally {
            stream?.Close();
          } 

          ion.PostToMain(() => {
            Log.D(this, "Resolved a certification for: " + result.serialNumber);
            onLoad();
          });
        }
      });
    }

    /// <summary>
    /// Shows the help menu.
    /// </summary>
    private void OnHelpClicked() {
      var vc = InflateViewController<HelpViewController>(BaseIONViewController.VC_HELP);

      var landing = new HelpPageBuilder(Strings.HELP)
        .Link(new HelpPageBuilder(Strings.Help.ABOUT)
          .Info(Strings.Help.VERSION, NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString())
          .Build())
        .Link(Strings.Help.SEND_FEEDBACK, (object obj, HelpViewController ovc) => {
          if (!MFMailComposeViewController.CanSendMail) {
            Toast.New(View, Strings.Errors.CANNOT_SEND_FEEBACK);
          } else {
            DoSendAppionFeedback();
          }
        }).Build();

      vc.page = landing;

      PresentViewControllerFromSelected(vc);
    }

    /// <summary>
    /// Constructs and initialized the view controllers that are used in the application.
    /// Order in array is the same as the menu order in the app
    /// </summary>
    /// <returns>The view controllers.</returns>
    private UIViewController[] BuildViewControllers() {
      var ret = new UINavigationController[] {
        new UINavigationController(InflateViewController<WorkbenchViewController>(BaseIONViewController.VC_WORKBENCH)),
        new UINavigationController(InflateViewController<AnalyzerViewController>(BaseIONViewController.VC_ANALYZER)),
        new UINavigationController(InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART)),
        new UINavigationController(InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL)),
        new UINavigationController(InflateViewController<JobViewController>(BaseIONViewController.VC_JOB_MANAGER)), 
        new UINavigationController(InflateViewController<LoggingViewController>(BaseIONViewController.VC_LOGGING)),
        null, // Screenshot Navigation
        null, // Settings navigation
        null, // Help Navigation
      };

      return ret;
    }

    /// <summary>
    /// Prepares and displays an email resolver such that the user can fire
    /// off an email to complain to appion.
    /// </summary>
    private void DoSendAppionFeedback() {
      var vc = new MFMailComposeViewController();
      vc.MailComposeDelegate = new MailDelegate();
      vc.SetSubject("ION App Feedback");
      vc.SetMessageBody("Hello,\n\n", false);
      vc.SetToRecipients(new String[] { AppionServerHelper.Email.APPION_SUPPORT });

      var file = AppionServerHelper.Email.CreatePlatformDescriptionFile(AppState.context.fileManager);

      vc.AddAttachmentData(NSData.FromFile(file.fullPath), "application/json", file.name);

      navigation.PresentViewController(vc, true, null);
    }

    /// <summary>
    /// Presents a new view controller using the current navigation view
    /// controller as the host.
    /// </summary>
    /// <param name="vc">Vc.</param>
    private void PresentViewControllerFromSelected(UIViewController vc) {
      var nvc = ((UINavigationController)navigation.CurrentViewController).VisibleViewController;
      nvc.NavigationController.PushViewController(vc, true);
      navigation.HideMenu();
    }

    /// <summary>
    /// Inflates an ION view controller from the storyboard.
    /// </summary>
    /// <returns>The view controller.</returns>
    /// <param name="key">Key.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    private T InflateViewController<T>(string key) where T : BaseIONViewController {
      var ret = (T)Storyboard.InstantiateViewController(key);
      ret.root = this;
      return ret;
    }
	}

  internal class MailDelegate : MFMailComposeViewControllerDelegate {
    // Overridden from MFMailComposeViewControllerDelegate
    public override void Finished(MFMailComposeViewController controller, MFMailComposeResult result, NSError error) {
      // TODO ahodder@appioninc.com: These toasts don't work 
      // the view controller is gone by the time they post. we will need to figure out
      // a better way to notify the user.
      switch (result) {
        case MFMailComposeResult.Sent:
          Toast.New(controller.View, Strings.Help.SENT_FEEDBACK);
          break;
        case MFMailComposeResult.Failed:
          Toast.New(controller.View, Strings.Errors.FAILED_TO_SEND_FEEDBACK);
          break;
      }

      controller.DismissModalViewController(true);
    }
  }

  internal class IONElement : ImageStringElement {
    /// <summary>
    /// Gets the cell key.
    /// </summary>
    /// <value>The cell key.</value>
    protected override NSString CellKey {
      get {
        return new NSString("IONElement");
      }
    }

    private string title { get; set; }
    private UIImage image { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ION.IOS.ViewController.IONElement"/> class.
    /// </summary>
    /// <param name="title">Title.</param>
    /// <param name="image">Image.</param>
    public IONElement(string title, UIImage image) : base(title, image) {
      this.title = title;
      this.image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ION.IOS.ViewController.IONElement"/> class.
    /// </summary>
    /// <param name="title">Title.</param>
    /// <param name="action">Action.</param>
    /// <param name="image">Image.</param>
    public IONElement(string title, Action action, UIImage image) : base(title, action, image) {
      this.title = title;
      this.image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
    }


    /// <Docs>The containing table view.</Docs>
    /// <returns></returns>
    /// <summary>
    /// Gets the cell.
    /// </summary>
    /// <param name="tv">Tv.</param>
    public override UITableViewCell GetCell(UITableView tv) {
      var ret = IONNavigationCell.Create();

      ret.UpdateTo(title, image);

      return ret;
    }
  }
}
