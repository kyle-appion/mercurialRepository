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

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Devices.Certificates;
	using ION.Core.IO;

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
	using ION.IOS.ViewController.Walkthrough;
	using ION.IOS.ViewController.RssFeed;
  using ION.IOS.ViewController.RemoteAccess;
  using ION.IOS.ViewController.AccessRequest;   
  using ION.IOS.ViewController.CloudSessions;   
  
	using System.Net;
	using System.Text;
	using Newtonsoft.Json.Linq;

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
        new Section(Strings.Report.REPORTS.ToUpper()) {
          new IONElement(Strings.Report.MANAGER, UIImage.FromBundle("ic_job_settings")),
          new IONElement(Strings.Report.LOGGING, UIImage.FromBundle("ic_graph_menu")),
          new IONElement(Strings.Report.SCREENSHOT_ARCHIVE, OnScreenshotArchiveClicked, UIImage.FromBundle("ic_camera")),
          new IONElement(Strings.Report.CALIBRATION_CERTIFICATES, OnCalibrationCertificateClicked, UIImage.FromBundle("ic_nav_certificate")),
        },
        new Section("Cloud".ToUpper()){
					new IONElement("Appion Portal", UIImage.FromBundle("cloud_menu_icon")),
				},        
        new Section (Strings.Navigation.CONFIGURATION.ToUpper()) {
          new IONElement(Strings.SETTINGS, OnNavSettingsClicked, UIImage.FromBundle("ic_settings")),
          new IONElement(Strings.HELP, OnHelpClicked, UIImage.FromBundle("ic_help")),
        },
        new Section (Strings.Exit.SHUTDOWN.ToUpper()) {
          new IONElement(Strings.Exit.SHUTDOWN, OnShutdownClicked, UIImage.FromBundle("ic_nav_power")),
        }
      };

      var v = new Section();

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
    /// Disconnects all gauges, stops data logging, and remote viewing
    /// </summary>
    private async void OnShutdownClicked(){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
			Console.WriteLine("Clicked shutdown");
	    var alert = UIAlertController.Create(Strings.Exit.SHUTDOWN, "This will disconnect all gauges, end data logging, and disconnect any remote operations", UIAlertControllerStyle.Alert);
	    alert.AddAction(UIAlertAction.Create(Strings.OK, UIAlertActionStyle.Default, (action) => {
        Console.WriteLine("Turn everything off");
        var ion = AppState.context;
       	var userID = KeychainAccess.ValueForKey("userID");
				foreach(var device in ion.deviceManager.devices){
       		if(device.isConnected){
						device.connection.Disconnect();
					}
				}
				
				if(ion.dataLogManager.isRecording){
					ion.dataLogManager.StopRecording();
				}
				var ioion = ion as IosION;
				if(ioion.webServices.uploading){
					ioion.webServices.updateOnlineStatus("0",userID);
					ioion.webServices.uploading = false;					
				}
				
				if(ioion.webServices.downloading){
					ioion.webServices.remoteViewing = false;
					ioion.webServices.downloading = false;
					ioion.webServices.paused = null;
					///SET THE APP MENU AND DEVICE MANAGER BACK TO THE LOCAL DEVICE'S SETTINGS
					ioion.setOriginalDeviceManager();
					setMainMenu();
				}
        
      }));
      alert.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {
        Console.WriteLine("Cancel shutdown");
      }));

      alert.Show();
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

				Log.D(this, Arrays.AsString<ISerialNumber>(serials.ToArray()));
				var task = new ION.Core.Net.RequestCalibrationCertificates(ion, serials.ToArray());
        //var task = new RequestCalibrationCertificatesTask(ion, serials.ToArray());
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
            Log.D(this, "Device nist date is " + result.certificate.lastTestCalibrationDate.ToShortDateString());
            var existing = ion.database.Query<ION.Core.Database.LoggingDeviceRow>("SELECT * FROM LoggingDeviceRow WHERE serialNumber = ?", result.serialNumber.rawSerial);

            if(existing.Count.Equals(0)){
              Log.D(this,"Creating new entry for device: " + result.serialNumber.rawSerial + " with a calibration date of: " + result.certificate.lastTestCalibrationDate.ToShortDateString());
              var addDevice = new ION.Core.Database.LoggingDeviceRow(){serialNumber = result.serialNumber.rawSerial, nistDate = result.certificate.lastTestCalibrationDate.ToShortDateString()};
              ion.database.Insert(addDevice);
            }else {
              Log.D(this,"Updated entry for device: " + result.serialNumber.rawSerial + " with a calibration date of: " + result.certificate.lastTestCalibrationDate.ToShortDateString());
            	ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET nistDate = ? WHERE serialNumber = ?",result.certificate.lastTestCalibrationDate.ToShortDateString(),result.serialNumber.rawSerial);
						}
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
    		.Link(Util.Strings.Walkthrough.INTRODUCTORY,(object obj, HelpViewController ovc) => {
    			OpenWalkthroughSections();
    		} )
#if DEBUG 

    //		.Link("RSS Feed", (object obj, HelpViewController ovc) => {
				//	ShowRSSFeed();
				//} )
#endif
        .Link(Strings.Help.SEND_FEEDBACK, (object obj, HelpViewController ovc) => {
	        if (!MFMailComposeViewController.CanSendMail) {
	          Toast.New(View, Strings.Errors.CANNOT_SEND_FEEBACK);
	        } else {
	          DoSendAppionFeedback();
	        }
      	} )
		.Build();

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
        null, // Calibration Certificates
        null, // Screenshot Navigation
        new UINavigationController(InflateViewController<RemoteSystemViewController>(BaseIONViewController.VC_REMOTE_VIEWING)),
        
        null, // Settings navigation
        null, // Help Navigation
        null, // Shutdown process
      };    

      return ret;  
    }    

    /// <summary>
    /// Constructs and initialized the view controllers that are used in the application.
    /// Order in array is the same as the menu order in the app
    /// </summary>
    /// <returns>The view controllers.</returns>
    //private void BuildRemoteViewControllers() {
    private void BuildRemoteViewControllers() {

				var remoteWb = InflateViewController<WorkbenchViewController>(BaseIONViewController.VC_WORKBENCH);
				remoteWb.remoteMode = true;
				var remoteAn = InflateViewController<AnalyzerViewController>(BaseIONViewController.VC_ANALYZER);
				remoteAn.remoteMode = true;
				var remotePortal = InflateViewController<RemoteSystemViewController>(BaseIONViewController.VC_REMOTE_VIEWING);
				
				navigation.ViewControllers[0] = new UINavigationController(remoteWb);
				navigation.ViewControllers[1] = new UINavigationController(remoteAn);
				navigation.ViewControllers[4] = null;
				navigation.ViewControllers[5] = null;
				navigation.ViewControllers[6] = new UINavigationController(remotePortal);
				navigation.ViewControllers[7] = null;
				navigation.ViewControllers[8] = null;
				navigation.ViewControllers[9] = null;
    }
    /// <summary>
    /// Prepares and displays an email resolver such that the user can fire
    /// off an email to complain to appion.
    /// </summary>
    private void DoSendAppionFeedback() {
      var vc = new MFMailComposeViewController();     
      vc.MailComposeDelegate = new MailDelegate();
      vc.SetSubject(Util.Strings.APPFEEDBACK);
      vc.SetMessageBody("Hello,\n\n", false);
      vc.SetToRecipients(new String[] { AppionServerHelper.Email.APPION_SUPPORT });

      var file = AppionServerHelper.Email.CreatePlatformDescriptionFile(AppState.context.fileManager);

      vc.AddAttachmentData(NSData.FromFile(file.fullPath), "application/json", file.name);

      navigation.PresentViewController(vc, true, null);
    }

    /// <summary>
    /// Opens up a list of options for walkthroughs. They are broken up between the main
    /// sections of the app
    /// </summary>
    private void OpenWalkthroughSections() {
      var wvc = InflateViewController<WalkthroughScreenshotViewController>(BaseIONViewController.VC_WALKTHROUGH_MENU);
      PresentViewControllerFromSelected(wvc);
    }

    /// <summary>
    /// Opens up a list of options for walkthroughs. They are broken up between the main
    /// sections of the app
    /// </summary>
    //private void ShowRSSFeed() {  
    //  var wvc = InflateViewController<RssFeedViewController>(BaseIONViewController.VC_RSS_FEED);
    //  PresentViewControllerFromSelected(wvc);
    //}

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

		/// <summary>
		/// Returns the navigation menu to the original layout
		/// </summary>
		/// <returns>The main menu.</returns>
		public void setMainMenu(){			
			navigation.NavigationRoot.Clear();
			navigation.NavigationRoot.Add(
				new Section (Strings.Navigation.CALCULATORS.ToUpper()) {
	        new IONElement(Strings.Workbench.SELF, UIImage.FromBundle("ic_nav_workbench")),
	        new IONElement(Strings.Analyzer.SELF, UIImage.FromBundle("ic_nav_analyzer")),
        }
			);
			navigation.NavigationRoot.Add(
				new Section (Strings.Navigation.CALCULATORS.ToUpper()) {
          new IONElement(Strings.Fluid.PT_CHART, UIImage.FromBundle("ic_nav_pt_chart")),
          new IONElement(Strings.Fluid.SUPERHEAT_SUBCOOL, UIImage.FromBundle("ic_nav_superheat_subcool")),
        }
			);
	
			navigation.NavigationRoot.Add(
				new Section(Strings.Report.REPORTS.ToUpper()) {
          new IONElement(Strings.Report.MANAGER, UIImage.FromBundle("ic_job_settings")),
          new IONElement(Strings.Report.LOGGING, UIImage.FromBundle("ic_graph_menu")),
          new IONElement(Strings.Report.CALIBRATION_CERTIFICATES, OnCalibrationCertificateClicked, UIImage.FromBundle("ic_nav_certificate")),
          new IONElement(Strings.Report.SCREENSHOT_ARCHIVE, OnScreenshotArchiveClicked, UIImage.FromBundle("ic_camera")),
        }
			);
			navigation.NavigationRoot.Add(
				new Section("Cloud".ToUpper()){
					new IONElement("Appion Portal", UIImage.FromBundle("cloud_menu_icon")),
				}
			);		
			navigation.NavigationRoot.Add(
				new Section (Strings.Navigation.CONFIGURATION.ToUpper()) {
          new IONElement(Strings.SETTINGS, OnNavSettingsClicked, UIImage.FromBundle("ic_settings")),
          new IONElement(Strings.HELP, OnHelpClicked, UIImage.FromBundle("ic_help")),
        }
			);
			navigation.NavigationRoot.Add(
				new Section (Strings.Exit.SHUTDOWN.ToUpper()) {
          new IONElement(Strings.Exit.SHUTDOWN, OnShutdownClicked, UIImage.FromBundle("ic_nav_power")),
        }
			);
			navigation.ViewControllers = BuildViewControllers();
		}
		/// <summary>
		/// Changes the navigation menu when entering remote viewing mode
		/// </summary>
		/// <returns>The new menu.</returns>
		
		public void setRemoteMenu(){
			navigation.NavigationRoot.Clear();
			navigation.NavigationRoot.Add(
	        new Section (Strings.Navigation.MAIN.ToUpper()) {
	          new IONElement(Strings.Workbench.SELF, UIImage.FromBundle("ic_nav_workbench")),
	          new IONElement(Strings.Analyzer.SELF, UIImage.FromBundle("ic_nav_analyzer")),
	        }
			);
			navigation.NavigationRoot.Add(
	         new Section (Strings.Navigation.CALCULATORS.ToUpper()) {
	          new IONElement(Strings.Fluid.PT_CHART, UIImage.FromBundle("ic_nav_pt_chart")),
	          new IONElement(Strings.Fluid.SUPERHEAT_SUBCOOL, UIImage.FromBundle("ic_nav_superheat_subcool")),
	        }
			);
			navigation.NavigationRoot.Add(
	        new Section (Strings.Navigation.CONFIGURATION.ToUpper()) {
	          new IONElement(Strings.SETTINGS, OnNavSettingsClicked, UIImage.FromBundle("ic_settings")),
	          new IONElement(Strings.HELP, OnHelpClicked, UIImage.FromBundle("ic_help")),
	        }
			);
			navigation.NavigationRoot.Add(
				new Section("Cloud".ToUpper()){
					new IONElement("Appion Portal", UIImage.FromBundle("cloud_menu_icon")),
				}
			);
			navigation.NavigationRoot.Add(
				new Section (Strings.Exit.SHUTDOWN.ToUpper()) {
          new IONElement(Strings.Exit.SHUTDOWN, OnShutdownClicked, UIImage.FromBundle("ic_nav_power")),
        }
			);
			BuildRemoteViewControllers();
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

