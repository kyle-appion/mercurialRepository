namespace ION.IOS.ViewController.Workbench {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

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
	using ION.IOS.ViewController.RemoteDeviceManager;

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
    
    public bool remoteMode = false;

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

      Title = Strings.Workbench.SELF.FromResources();

      ion = AppState.context;
      
			if(remoteMode){
				workbench = new Workbench(ion);
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
			}

      tableContent.AllowsSelection = true;
      tableContent.ContentInset = new UIEdgeInsets(0, 0, 0, 0);

      source = new WorkbenchTableSource(this, ion, tableContent);
			source.SetWorkbench(workbench);
      source.onAddClicked = OnRequestViewer;

      tableContent.Source = source;

			AppState.context.onWorkbenchChanged += this.OnWorkbenchChanged;
			ion.currentWorkbench.onWorkbenchEvent += OnWorkbenchEvent;
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
	        Log.D(this,"Adding device to workbench");
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
      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
          goto case WorkbenchEvent.EType.Swapped;
        case WorkbenchEvent.EType.Removed:
          goto case WorkbenchEvent.EType.Swapped;
        case WorkbenchEvent.EType.Swapped:
          ion.SaveWorkbenchAsync();

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
  }
}
