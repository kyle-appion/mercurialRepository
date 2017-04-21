namespace ION.IOS.ViewController.Workbench {

	using System;
	using System.Threading.Tasks;

	using CoreGraphics;
	using Foundation;
	using UIKit;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Pdf;
	using ION.Core.Report;
	using ION.Core.Sensors;

	using ION.IOS.Devices;
	using ION.IOS.Sensors;
	using ION.IOS.UI;
	using ION.IOS.Util;
	using ION.IOS.ViewController.DeviceManager;
	using ION.IOS.ViewController.ScreenshotReport;
	using ION.IOS.ViewController.RemoteDeviceManager;
	using AudioToolbox;
	using ION.IOS.App;
	using ION.Core.Net;
	using ION.IOS.Viewcontroller.RemoteAccess;

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
    private WorkbenchTableSource source { get; set; }

    public UIButton recordButton;
		public WebPayload webServices;    
    public bool remoteMode = false;
    public UIScrollView remoteBlocker;
    public UILabel remoteTitle;
		RemoteControls remoteControl;
    public WorkbenchViewController (IntPtr handle) : base (handle) {
      // Nope
    }

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      View.BringSubviewToFront(tableContent);
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      AutomaticallyAdjustsScrollViewInsets = false;
			
			if(remoteMode){
				remoteTitle = new UILabel(new CGRect(0, 0, 480, 44));
				remoteTitle.BackgroundColor = UIColor.Clear;
				remoteTitle.Lines = 2;
				remoteTitle.Font = UIFont.BoldSystemFontOfSize(14f);
				remoteTitle.ShadowColor = UIColor.FromWhiteAlpha(0.0f,.5f);
				remoteTitle.TextAlignment = UITextAlignment.Center;
				remoteTitle.TextColor = UIColor.Black;
				remoteTitle.Text = "Workbench\nRemote Viewing";
				this.NavigationItem.TitleView = remoteTitle;
			} else {
      	Title = Strings.Workbench.SELF.FromResources();
			}

      ion = AppState.context; 			
     	var webIon = ion as IosION;
     	webServices = webIon.webServices;

			if(remoteMode){
				workbench = new Workbench(ion);
				ion.currentWorkbench.storedWorkbench = workbench;
				tableContent.Bounces = false;
				
				remoteBlocker = new UIScrollView(new CGRect(0,45,View.Bounds.Width,.82 * View.Bounds.Height));
				remoteBlocker.BackgroundColor = UIColor.Clear;
				remoteBlocker.ContentSize = tableContent.ContentSize;
				remoteBlocker.Bounces = false;
				remoteBlocker.ShowsVerticalScrollIndicator = false;
				
				var remoteButton = new UIButton(new CGRect(0,0,65,35));
				remoteButton.SetTitle("Options", UIControlState.Normal);
				remoteButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
				remoteButton.TouchUpInside += (sender, e) =>{
					if(remoteControl.controlView.Hidden){
						remoteControl.controlView.Hidden = false;
					} else {
						remoteControl.controlView.Hidden = true;
					}
				};
				
				UIBarButtonItem button = new UIBarButtonItem(remoteButton);
				this.NavigationItem.RightBarButtonItem = button;
				this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
				
				remoteControl = new RemoteControls(45,View);
				remoteControl.disconnectButton.TouchUpInside += (sender, e) => {
					disconnectRemoteMode();
				};
				
				remoteBlocker.Scrolled += (sender, e) => {
					tableContent.SetContentOffset(remoteBlocker.ContentOffset,false);		
				};
				
				View.AddSubview(remoteBlocker);
				View.AddSubview(remoteControl.controlView);
				View.BringSubviewToFront(remoteControl.controlView);
				workbench.onWorkbenchEvent += updateBlockerHeight;
				initializeBlockerHeight();
			} else {
	      var button = new UIButton(new CGRect(0, 0, 31, 30));
	      button.TouchUpInside += (obj, args) => {
	        TakeScreenshot();
	      };
	      button.SetImage(UIImage.FromBundle("ic_camera"), UIControlState.Normal);
	
	      recordButton = new UIButton(new CGRect(0,0,35,35));
	      recordButton.TouchUpInside += (sender, e) => {
	        RecordDevices();
	      };
	      recordButton.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
	
	      var barButton = new UIBarButtonItem(button);
	      var barButton2 = new UIBarButtonItem(recordButton);
	
	      NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{barButton,barButton2};		
				workbench = ion.currentWorkbench;		
				tableContent.Bounces = true;
			}
			
      tableContent.AllowsSelection = true;
      tableContent.ContentInset = new UIEdgeInsets(0, 0, 0, 0);

      source = new WorkbenchTableSource(this, ion, tableContent);
			source.SetWorkbench(workbench);
      source.onAddClicked = OnRequestViewer;

      tableContent.Source = source;

			AppState.context.onWorkbenchChanged += this.OnWorkbenchChanged;
			if(workbench == null){
				Console.WriteLine("workbench for ion isn't created for some reason");
			}
			workbench.onWorkbenchEvent += OnWorkbenchEvent;
    }    

    // Overridden from BaseIONViewController
    public override void ViewDidAppear(bool animated) {
      base.ViewDidAppear(animated);

      tableContent.ReloadData();
      
      if(!ion.deviceManager.connectionHelper.isEnabled){
			  UIAlertView bluetoothWarning = new UIAlertView("Bluetooth Disconnected", "Bluetooth needs to be connected to work with peripherals", null,"Close","Settings");
	          bluetoothWarning.Clicked += (sender, e) => {
	            if(e.ButtonIndex.Equals(1)){
	              UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
	            }
	          };
	          bluetoothWarning.Show();
	  	}
	  	if(!remoteMode){
	      if (ion.dataLogManager.isRecording) {
	        recordButton.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
	      } else {
	        recordButton.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
	      }
	    } else {
				if(webServices.downloading){
					remoteTitle.Text = "Workbench\nRemote Viewing";
				} else {
					remoteTitle.Text = "Workbench\nRemote Editing";
				}
				
				if(remoteControl != null){
					if(ion.remoteDevice.loggingStatus == 1){
						NSUserDefaults.StandardUserDefaults.SetString("1","remoteLogging");
						remoteControl.remoteLoggingButton.SetTitle("Stop Logging", UIControlState.Normal);
					} else {
						NSUserDefaults.StandardUserDefaults.SetString("","remoteLogging");					
						remoteControl.remoteLoggingButton.SetTitle("Start Logging", UIControlState.Normal);
					}
				}			
			}
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      ion.currentWorkbench.onWorkbenchEvent -= OnWorkbenchEvent;
			AppState.context.onWorkbenchChanged -= this.OnWorkbenchChanged;
    }

		private void OnWorkbenchChanged(Workbench workbench) {
			source.SetWorkbench(workbench);
			workbench.onWorkbenchEvent += OnWorkbenchEvent;
		}

    /// <summary>
    /// Called when the viewer source wishes to request a new viewer.
    /// </summary>
    private void OnRequestViewer() {
    	if(!remoteMode){
	      var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
	        Log.D(this,"Adding device to workbench");
	        workbench.AddSensor(sensor);
	      };
	      NavigationController.PushViewController(sb, true);
      } else {
	      var sb = InflateViewController<RemoteDeviceManagerViewController>(VC_REMOTE_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
	        workbench.AddSensor(sensor);
	      };
	      NavigationController.PushViewController(sb, true);
			}
    }

    /// <summary>
    /// Called when the backing workbench throws a new event.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    private async void OnWorkbenchEvent(WorkbenchEvent workbenchEvent) {
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
          goto case WorkbenchEvent.EType.Swapped;
        case WorkbenchEvent.EType.Removed:
          goto case WorkbenchEvent.EType.Swapped;
        case WorkbenchEvent.EType.Swapped:
          ion.SaveWorkbenchAsync();
          if(remoteMode){
          	initializeBlockerHeight();
          }  
          break;
      }
    }

    private void TakeScreenshot() {
      try {
        var vc = InflateViewController<ScreenshotReportViewController>(VC_SCREENSHOT_REPORT);
        vc.image = View.Capture();
        // TODO: ahodder@appioninc.com: I have to be ignorant; this is a fucking stupid way to have to dismiss a fucking view controller
        vc.closer = () => {
          NavigationController.PopViewController(true);
        };
        NavigationController.PushViewController(vc, true);
      } catch (Exception e) {
        Log.E(this, "Failed to create pdf", e);
      }
    }

    private void RecordDevices(){
      var recordingMessage = "";
      if (ion.dataLogManager.isRecording) {
        recordButton.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
        recordButton.BackgroundColor = UIColor.Clear;
        ion.dataLogManager.StopRecording();
        recordingMessage = "Session recording has stopped";
      } else {
        recordButton.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
        recordButton.BackgroundColor = UIColor.Clear;
        ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
        recordingMessage = "Session recording has started";
      }
      showRecordingToast(recordingMessage);
    }

    public async void showRecordingToast(string recordingMessage){
      UIAlertView messageBox = new UIAlertView(recordingMessage, null,null,null);
      messageBox.Show();
      await Task.Delay(TimeSpan.FromSeconds(1));
      messageBox.DismissWithClickedButtonIndex(0, true);
    }
    
		public void pauseRemote(bool paused){		
			if(paused == false){
				initializeBlockerHeight();
			}
		 	tableContent.ReloadData();
		}
		
		/// <summary>
		/// TableView is not updating it's content size in a way that allows it to 
		/// be used as a height for the scrollview. Using a timed event that grabs it 
		/// every second to confirm the height.
		/// </summary>
		/// <param name="workbenchEvent">Workbench event.</param>
		public void updateBlockerHeight(WorkbenchEvent workbenchEvent){	
			if(workbenchEvent.type == WorkbenchEvent.EType.ManifoldEvent){
				if(workbenchEvent.manifoldEvent.type == ManifoldEvent.EType.SensorPropertyAdded || workbenchEvent.manifoldEvent.type == ManifoldEvent.EType.SensorPropertyRemoved){
					//Console.WriteLine("Added or removed a sensor property");
					initializeBlockerHeight();
				}
			}
		}
		
		public async void initializeBlockerHeight(){
      //Console.WriteLine("table content size is " + tableContent.ContentSize);		
			await Task.Delay(TimeSpan.FromMilliseconds(1000));
      //Console.WriteLine("setting remote blocker content size to " + tableContent.ContentSize);
			tableContent.LayoutSubviews();
			remoteBlocker.ContentSize = new CGSize(tableContent.ContentSize.Width,tableContent.ContentSize.Height - 68);
		}

		public async void disconnectRemoteMode(){    
      var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
		 	remoteControl.controlView.Hidden = true;
		 	webServices.downloading = false;
		 	webServices.remoteViewing = false;
			webServices.paused = null;
			
			await ion.setOriginalDeviceManager();
			rootVC.setMainMenu();
		}
  }
}
