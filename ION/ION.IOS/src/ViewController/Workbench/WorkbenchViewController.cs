namespace ION.IOS.ViewController.Workbench {

	using System;
	using System.Threading.Tasks;

	using CoreGraphics;
	using Foundation;
	using UIKit;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Devices;
  
  using ION.CoreExtensions.Net;

	using ION.IOS.Util;
	using ION.IOS.ViewController.DeviceManager;
	using ION.IOS.ViewController.ScreenshotReport;
	using ION.IOS.ViewController.RemoteDeviceManager;
	using AudioToolbox;
	using ION.IOS.App;
	using ION.Core.Net;
	using ION.IOS.Viewcontroller.RemoteAccess;
  using ION.IOS.ViewController.DeviceGrid;

  public partial class WorkbenchViewController : BaseIONViewController {
    /// <summary>
    /// The current ion context.
    /// </summary>
    /// <value>The ion.</value>
    public IosION ion { get; private set; }
    /// <summary>
    /// The workbench that we are working with.
    /// </summary>
    /// <value>The workbench.</value>
    private Workbench workbench { get; set; }
    /// <summary>
    /// The source that will provide Viewer views to the table view.
    /// </summary>
    /// <value>The source.</value>
    private WorkbenchTableSource source { get; set; }

    public UIButton recordButton;
    public bool remoteMode = false;
    public UIScrollView remoteBlocker;
    public UILabel remoteTitle;
	  
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

      ion = AppState.context as IosION;
	    AppState.onIonChanged += OnIonChanged;

			if(remoteMode){
        workbench = ion.currentWorkbench;
				tableContent.Bounces = false;
				
				remoteBlocker = new UIScrollView(new CGRect(0,45,View.Bounds.Width,.82 * View.Bounds.Height));
				remoteBlocker.BackgroundColor = UIColor.Clear;
				remoteBlocker.ContentSize = tableContent.ContentSize;
				remoteBlocker.Bounces = false;
				remoteBlocker.ShowsVerticalScrollIndicator = false;
				
				NavigationController.NavigationBar.BarTintColor = UIColor.Red;
				
				remoteBlocker.Scrolled += (sender, e) => {
					tableContent.SetContentOffset(remoteBlocker.ContentOffset,false);		
				};
				
				View.AddSubview(remoteBlocker);
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
      //tableContent.RegisterClassForCellReuse(typeof(ViewerTableCell),"cellViewer");
      source = new WorkbenchTableSource(this, ion, tableContent);
			source.SetWorkbench(workbench);
      source.onAddClicked = OnRequestViewer;

      tableContent.Source = source;

			AppState.context.onWorkbenchChanged += this.OnWorkbenchChanged;
			workbench.onWorkbenchEvent += OnWorkbenchEvent;
    }    

    // Overridden from BaseIONViewController
    public override void ViewDidAppear(bool animated) {
      base.ViewDidAppear(animated);
      tableContent.ReloadData();
      
      if(!ion.deviceManager.connectionManager.isEnabled){
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
				if(ion.webServices.downloading){
					remoteTitle.Text = "Workbench\nRemote Viewing";
				} else {
					remoteTitle.Text = "Workbench\nRemote Editing";
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
				//var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
				var sb = InflateViewController<DeviceGridViewController>(VC_DEVICE_GRID);
        sb.fromWorkbench = true;
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

    private async void RecordDevices(){
      var recordingMessage = "";
      if(ion.webServices.uploading){
        Console.WriteLine("Workbench currently uploading and want to update datalogging for userid " + KeychainAccess.ValueForKey("userID") + " and layoutid " + KeychainAccess.ValueForKey("layoutid"));
        if (ion.dataLogManager.isRecording) {
          await ion.webServices.SetRemoteDataLog(KeychainAccess.ValueForKey("userID"),KeychainAccess.ValueForKey("layoutid"),"0");
          ion.dataLogManager.StopRecording();
          recordingMessage = "Session recording has stopped";
        } else {
          await ion.webServices.SetRemoteDataLog(KeychainAccess.ValueForKey("userID"),KeychainAccess.ValueForKey("layoutid"),"1");
          ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
          recordingMessage = "Session recording has started";
        }
      } else {
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
       }
      showRecordingToast(recordingMessage);
    }

	  private void OnIonChanged(IION ion) {
		  updateLogging();
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
    
     public void updateLogging(){
      InvokeOnMainThread ( () => {
        if (ion.dataLogManager.isRecording) {
          recordButton.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
          var recordingMessage = "Session recording has started";
          showRecordingToast(recordingMessage);
        } else {
          var recordingMessage = "Session recording has stopped";
          recordButton.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
          showRecordingToast(recordingMessage);
        }
      });
    }
  }
}
