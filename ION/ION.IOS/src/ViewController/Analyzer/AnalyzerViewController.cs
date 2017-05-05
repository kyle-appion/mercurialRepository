using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoreGraphics;
using Foundation;
using UIKit;

using Appion.Commons.Measure;
using Appion.Commons.Util;

using ION.IOS.ViewController.DeviceManager;
using ION.IOS.ViewController.Alarms;
using ION.IOS.App;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.Sensors;
using ION.Core.Net;
using ION.IOS.ViewController.RemoteDeviceManager;
using ION.IOS.Viewcontroller.RemoteAccess;
using ION.Core.Database;
using ION.IOS.ViewController.ScreenshotReport;
using ION.IOS.Util;
using ION.IOS.Devices;

namespace ION.IOS.ViewController.Analyzer { 
  
	public partial class AnalyzerViewController : BaseIONViewController {
    public static manualEntry start = new manualEntry ();
    public static actionPopup sensorActions = new actionPopup();
    public static sensorGroup analyzerSensors;
    public static LowHighArea lowHighSensors;
    public static ManualView mentryView;
    public static AnalyzerViewController arvc;

    public static UIButton dataRecord;
    public static UIImageView compressor;
    public static UIImageView expansion;
    public UILabel remoteTitle;
    public UIView blockerView;
    public RemoteControls remoteControl;
    public UITapGestureRecognizer outsideTap;

    private IosION ion;
    public WebPayload webServices;

    /// <summary>
    /// The analyzer that we are working with.
    /// </summary>
    /// <value>The Analyzer</value>
    private ION.Core.Content.Analyzer analyzer { get; set; }
    
    public bool remoteMode = false;

    static bool UserInterfaceIdiomIsPhone {
      get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
    }

		public AnalyzerViewController (IntPtr handle) : base (handle) {

		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      viewAnalyzerContainer.Bounds = View.Bounds;      
			viewAnalyzerContainer.BackgroundColor = UIColor.Clear;
			
			blockerView = new UIView(viewAnalyzerContainer.Bounds);
			blockerView.Hidden = true;
		
      arvc = this;
      
      analyzerSensors = new sensorGroup(viewAnalyzerContainer, this);      
      lowHighSensors = new LowHighArea (viewAnalyzerContainer, this, analyzerSensors);

      mentryView = new ManualView(viewAnalyzerContainer);
      
      InitNavigationBar("ic_nav_analyzer", false); 
      ion = AppState.context as IosION;      

      webServices = ion.webServices;
      AutomaticallyAdjustsScrollViewInsets = false;

      backAction = () => {
        root.navigation.ToggleMenu();
      };
     
			analyzer = ion.currentAnalyzer;
			analyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
			analyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
			
			if(remoteMode){
				remoteTitle = new UILabel(new CGRect(0, 0, 480, 44));
				remoteTitle.BackgroundColor = UIColor.Clear;
				remoteTitle.Lines = 2;
				remoteTitle.Font = UIFont.BoldSystemFontOfSize(14f);
				remoteTitle.ShadowColor = UIColor.FromWhiteAlpha(0.0f,.5f);
				remoteTitle.TextAlignment = UITextAlignment.Center;
				remoteTitle.TextColor = UIColor.Black;
				remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEVIEW;
				
				this.NavigationItem.TitleView = remoteTitle;				
			} else {
	      Title = Util.Strings.Analyzer.SELF;
			}
      
      createSensors ();

      mentryView.mmeasurementType.TouchUpInside += showManualPicker;
      mentryView.dtypeButton.TouchUpInside += showDeviceTypePicker;

      mentryView.mtextValue.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      mentryView.mcloseButton.TouchUpInside += delegate {
        mentryView.mtextValue.Text = "";
        mentryView.mView.Hidden = true;
        mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
        mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
        mentryView.mbuttonText.Text = start.pressures[0];
        mentryView.textValidation.Hidden = true;
        mentryView.mtextValue.ResignFirstResponder();
        start.pressedSensor.isManual = false;
        viewAnalyzerContainer.SendSubviewToBack (mentryView.mView);
      };
      viewAnalyzerContainer.SendSubviewToBack(lowHighSensors.lowArea.snapArea);  
      viewAnalyzerContainer.SendSubviewToBack(lowHighSensors.highArea.snapArea);
      
      if(remoteMode){
      	var totalSize = NSFileManager.DefaultManager.GetFileSystemAttributes (Environment.GetFolderPath (Environment.SpecialFolder.Personal)).Size;
				totalSize /= 1024;
				totalSize /= 1024;
				var freeSpace = NSFileManager.DefaultManager.GetFileSystemAttributes (Environment.GetFolderPath (Environment.SpecialFolder.Personal)).FreeSize;
				freeSpace /= 1024;
				freeSpace /= 1024;
				
				Console.WriteLine("You have a total size of " + totalSize + " and " + freeSpace + " of that is free");
			
				UIView blockerView = new UIView(new CGRect(0,0,viewAnalyzerContainer.Bounds.Width,viewAnalyzerContainer.Bounds.Height));
				blockerView.BackgroundColor = UIColor.Clear;
				blockerView.Hidden = true;

				var remoteButton = new UIButton(new CGRect(0,0,65,35));
				remoteButton.SetTitle(Util.Strings.Analyzer.OPTIONS, UIControlState.Normal);
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

	   		remoteControl = new RemoteControls(0,viewAnalyzerContainer);
	   		remoteControl.controlView.ExclusiveTouch = true;
	   		remoteControl.disconnectButton.TouchUpInside += (sender, e) => {
	   			blockerView.Hidden = true;
					disconnectRemoteMode();
				};
						
	   		viewAnalyzerContainer.AddSubview(remoteControl.controlView);
	      AnalyserUtilities.confirmLayout(analyzerSensors,viewAnalyzerContainer);

				refreshSensorLayout();
      	//webServices.paused += pauseRemote;
      	viewAnalyzerContainer.AddSubview(blockerView);
      	viewAnalyzerContainer.BringSubviewToFront(blockerView);
      	viewAnalyzerContainer.BringSubviewToFront(remoteControl.controlView);
      } else {
				if(analyzer.sensorList == null){
		      analyzer.sensorList = new List<Sensor>();
				}
	      var screenshot = new UIButton(new CGRect(0, 0, 31, 30));
	      screenshot.TouchUpInside += (obj, args) => {
	        TakeScreenshot();
	      };
	      screenshot.SetImage(UIImage.FromBundle("ic_camera"), UIControlState.Normal);    
				
	      dataRecord = new UIButton(new CGRect(0,0,35,35));
	      dataRecord.BackgroundColor = UIColor.Clear;
	      dataRecord.TouchDown += (sender, e) => {dataRecord.BackgroundColor = UIColor.LightGray;};
	      dataRecord.TouchUpOutside += (sender, e) => {dataRecord.BackgroundColor = UIColor.Black;};
	      dataRecord.TouchUpInside += (sender, e) => {
	        recordDevices();
	      };
	
	      if (ion.dataLogManager.isRecording) {
	        dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
	      } else {
	        dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
	      }
	
	      var button = new UIBarButtonItem(dataRecord);
				var button2 = new UIBarButtonItem(screenshot);
	      NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{button2,button};
				layoutAnalyzer();
			}
      ion.onIonStateChanged += updateLogging;
			viewAnalyzerContainer.AddSubview(blockerView);
    }

    /// <summary>
    /// Clicking this button begins the recording process for the currently connected analyzer devcies
    /// Stores the serial #, measurement, current date, session start time, session end time
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public async void recordDevices(){
      var recordingMessage = "";
      if(webServices.uploading){
        if (ion.dataLogManager.isRecording) {
          await webServices.SetRemoteDataLog(KeychainAccess.ValueForKey("userID"),KeychainAccess.ValueForKey("layoutid"),"0");
          ion.dataLogManager.StopRecording();
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTOPPED;
        } else {
          await webServices.SetRemoteDataLog(KeychainAccess.ValueForKey("userID"),KeychainAccess.ValueForKey("layoutid"),"1");
          ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTARTED;
        }
      } else {      
        if (ion.dataLogManager.isRecording) {
          dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          ion.dataLogManager.StopRecording();
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTOPPED;
        } else {
          dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTARTED;
        }
      }
      showRecordingToast(recordingMessage);
    }

    public async void showRecordingToast(string recordingMessage){
      UIAlertView messageBox = new UIAlertView(recordingMessage, null,null,null);
      messageBox.Show();
      await Task.Delay(TimeSpan.FromSeconds(1));
      messageBox.DismissWithClickedButtonIndex(0, true);
    }

    /// <summary>
    /// CREATE ALL SENSOR SUBVIEW STARTING POSITIONS AND CENTER POINTS
    /// </summary>
    void createSensors () 
    {

      AnalyserUtilities.CreateSnapArea(analyzerSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea1, "1", analyzerSensors.snapRect1, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea2, "2", analyzerSensors.snapRect2, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea3, "3", analyzerSensors.snapRect3, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea4, "4", analyzerSensors.snapRect4, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea5, "5", analyzerSensors.snapRect5, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea6, "6", analyzerSensors.snapRect6, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea7, "7", analyzerSensors.snapRect7, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.ApplySnapArea (analyzerSensors.snapArea8, "8", analyzerSensors.snapRect8, analyzerSensors, lowHighSensors, viewAnalyzerContainer);
      if (UserInterfaceIdiomIsPhone) {
        expansion = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width, .023 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height));
        compressor = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width,.365 * viewAnalyzerContainer.Bounds.Height,.044 * viewAnalyzerContainer.Bounds.Height,.044 * viewAnalyzerContainer.Bounds.Height));
      } else {
        expansion = new UIImageView(new CGRect(.47 * View.Bounds.Width, .025 * View.Bounds.Height, .044 * View.Bounds.Height, .044 * View.Bounds.Height));
        compressor = new UIImageView(new CGRect(.47 * View.Bounds.Width,.36 * View.Bounds.Height,.044 * View.Bounds.Height,.044 * View.Bounds.Height));
      }
      compressor.Image = UIImage.FromBundle("ic_compressor");
      expansion.Image = UIImage.FromBundle("ic_expansionchamber");

			viewAnalyzerContainer.AddSubview(compressor); 
			viewAnalyzerContainer.AddSubview(expansion);

      AnalyserUtilities.AddHighLowArea (lowHighSensors, viewAnalyzerContainer);
      AnalyserUtilities.CreateManualViews(mentryView, viewAnalyzerContainer);

      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea1, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea2, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea3, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea4, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea5, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea6, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea7, analyzerSensors.animator, analyzerSensors.snap);
      AddSensorGestures (analyzerSensors, analyzerSensors.snapArea8, analyzerSensors.animator, analyzerSensors.snap);

      AddLowHighGestures ();

      mentryView.mView.Layer.CornerRadius = 10;     
    }

    /// <summary>
    /// CREATE AND SHOW POPUP FOR SINGLE SENSORS
    /// </summary>
    /// <param name="popupType">Popup type.</param>
    void ShowPopup(sensor pressedArea, int popupType){
      if (pressedArea.availableView.Hidden) {
        ///IF SENSOR IS ACTIVE SET THAT SENSOR'S INFO IN THE POPUP
        pressedArea.sactionView.pdeviceName.Text = pressedArea.topLabel.Text;

        pressedArea.sactionView.pgaugeValue.Text = pressedArea.middleLabel.Text;

        pressedArea.sactionView.pvalueType.Text = pressedArea.bottomLabel.Text;

        if (pressedArea.currentSensor != null && pressedArea.currentSensor.device.isConnected) {
         
          pressedArea.sactionView.pconnection.Image = UIImage.FromBundle("ic_bluetooth_connected");
          pressedArea.sactionView.pconnectionStatus.Text = "";
          pressedArea.sactionView.connectionColor.BackgroundColor = UIColor.Green;
          pressedArea.sactionView.connectionColor.Hidden = false;

          if (pressedArea.currentSensor.device.battery > 75) {
            pressedArea.sactionView.pbatteryImage.Image = UIImage.FromBundle("img_battery_100");
          } else if (pressedArea.currentSensor.device.battery > 50) {
            pressedArea.sactionView.pbatteryImage.Image = UIImage.FromBundle("img_battery_75");
          } else if (pressedArea.currentSensor.device.battery > 25) {
            pressedArea.sactionView.pbatteryImage.Image = UIImage.FromBundle("img_battery_50");
          } else if (pressedArea.currentSensor.device.battery > 0) {
            pressedArea.sactionView.pbatteryImage.Image = UIImage.FromBundle("img_battery_25");
          } else {
            pressedArea.sactionView.pbatteryImage.Image = UIImage.FromBundle("img_battery_0");
          }
          pressedArea.sactionView.pdeviceImage.Image = pressedArea.deviceImage.Image;

        } else if (pressedArea.currentSensor != null && !pressedArea.isManual) {
          pressedArea.sactionView.pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
          pressedArea.sactionView.pconnectionStatus.Text = Util.Strings.Device.DISCONNECTED;
          pressedArea.sactionView.pconnectionStatus.TextColor = UIColor.Red;
          pressedArea.sactionView.pdeviceImage.Image = pressedArea.deviceImage.Image;
          pressedArea.sactionView.connectionColor.BackgroundColor = UIColor.Red;
          pressedArea.sactionView.connectionColor.Hidden = false;
          pressedArea.sactionView.conDisButton.Hidden = false;
        } else {
          pressedArea.sactionView.pdeviceImage.Image = UIImage.FromBundle("ic_edit");
          pressedArea.sactionView.pconnectionStatus.Text = "";
          pressedArea.sactionView.pconnection.Image = null;
          pressedArea.sactionView.pconnection.Hidden = true;
          pressedArea.sactionView.pbatteryImage.Image = null;
          pressedArea.sactionView.connectionColor.Hidden = true;
          pressedArea.sactionView.conDisButton.Hidden = true;
        }

				/////CHECK IF LOW OR HIGH AREA SENSOR MATCHES SELECTED SENSOR TO POPULATE LINK INFORMATION
				if(lowHighSensors.lowArea.currentSensor != null && pressedArea.currentSensor != null && lowHighSensors.lowArea.currentSensor == pressedArea.currentSensor){
          pressedArea.sactionView.pLowHigh.SetTitle(Util.Strings.Analyzer.LOWSIDE, UIControlState.Normal);
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Blue;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				} else if (lowHighSensors.highArea.currentSensor != null && pressedArea.currentSensor != null && lowHighSensors.highArea.currentSensor == pressedArea.currentSensor){
          pressedArea.sactionView.pLowHigh.SetTitle(Util.Strings.Analyzer.HIGHSIDE, UIControlState.Normal);
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Red;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				} else if (lowHighSensors.lowArea.manualSensor != null && pressedArea.manualSensor != null && lowHighSensors.lowArea.manualSensor == pressedArea.manualSensor){
          pressedArea.sactionView.pLowHigh.SetTitle(Util.Strings.Analyzer.LOWSIDE, UIControlState.Normal);
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Blue;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				} else if (lowHighSensors.highArea.manualSensor != null && pressedArea.manualSensor != null && lowHighSensors.highArea.manualSensor == pressedArea.manualSensor){
          pressedArea.sactionView.pLowHigh.SetTitle(Util.Strings.Analyzer.HIGHSIDE, UIControlState.Normal);
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Red;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				} else {
          pressedArea.sactionView.pLowHigh.SetTitle(Util.Strings.Analyzer.UNSPECIFIED, UIControlState.Normal);
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.White;
          pressedArea.sactionView.pLowHigh.SetTitleColor(UIColor.Black, UIControlState.Normal);
        }

        ///SHOW POPUP
        pressedArea.sactionView.aView.Hidden = false;
        //View.BringSubviewToFront (pressedArea.sactionView.aView);
        viewAnalyzerContainer.BringSubviewToFront (pressedArea.sactionView.aView);

        pressedArea.sactionView.pcloseButton.TouchUpInside += delegate {
          pressedArea.sactionView.aView.Hidden = true;
          blockerView.Hidden = true;
        };

        ///STORE PASSED SENSOR SUBVIEW INFORMATION FOR CHANGES
        sensorActions.pressedSensor = pressedArea;
        sensorActions.pressedView = pressedArea.snapArea;
        sensorActions.addLong = pressedArea.holdGesture;
        sensorActions.addPan = pressedArea.panGesture;
        sensorActions.topLabel = pressedArea.topLabel;
        sensorActions.middleLabel = pressedArea.middleLabel;
        sensorActions.bottomLabel = pressedArea.bottomLabel;
        sensorActions.pressedSensor.isManual = pressedArea.isManual;

        ///SHOW ACTIONSHEET FOR SENSOR OPTIONS
        pressedArea.sactionView.pactionButton.TouchUpInside += handleActionPopup;
        
        outsideTap = new UITapGestureRecognizer(() => {
        	Console.WriteLine("tapped analyzer container");
					pressedArea.sactionView.aView.Hidden = true;
					blockerView.RemoveGestureRecognizer(outsideTap);
					blockerView.Hidden = true;
				});
        blockerView.AddGestureRecognizer(outsideTap);
				blockerView.Hidden = false;
      }
      else {

        ///SHOW ACTIONSHEET FOR ADDING A NEW SENSOR
        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create (Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Device.Manager.SELF, UIAlertActionStyle.Default, (action) => {
          OnRequestViewer(pressedArea);
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.CREATEMANUAL, UIAlertActionStyle.Default, (action) => {
          start = new manualEntry();
          start.pressedSensor = pressedArea;
          start.addPan = pressedArea.panGesture; 
          start.pressedView = pressedArea.snapArea;
          start.availableView = pressedArea.availableView;
          start.addLong = pressedArea.holdGesture;
          start.topLabel = pressedArea.topLabel;
          start.middleLabel = pressedArea.middleLabel;
          start.bottomLabel = pressedArea.bottomLabel;
          start.addIcon = pressedArea.addIcon;
					mentryView.mdoneButton.TouchUpInside -= handleManualLHPopup;
          mentryView.mdoneButton.TouchUpInside += handleManualPopup;
          viewAnalyzerContainer.BringSubviewToFront(mentryView.mView);
          mentryView.mView.Hidden = false;
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {Console.WriteLine ("Cancel Action");blockerView.Hidden = true;}));
        this.View.Window.RootViewController.PresentViewController (addDeviceSheet, true, null);
      }
    }

    /// <summary>
    /// EVENT FUNCTION FOR MANUAL ENTRY POPUP
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void handleManualPopup(object sender, EventArgs e){
      if (mentryView.mtextValue.Text.Contains(".")) {
        var p1 = mentryView.mtextValue.Text.Split('.');
        var check = p1[1];
        if (check.Length.Equals(0)) {
          mentryView.textValidation.Text = Util.Strings.Analyzer.VALIDMEASUREMENT;
          mentryView.textValidation.Hidden = false;
          return;
        }
      }
      if (mentryView.mtextValue.Text.Length <= 0) {
        mentryView.textValidation.Text = Util.Strings.Analyzer.MISSINGVALUE;
        mentryView.textValidation.Hidden = false;
        return;
      }
      if (mentryView.mtextValue.Text.Contains("-") && mentryView.mtextValue.Text.Length.Equals(1)) {
        mentryView.textValidation.Text = Util.Strings.Analyzer.VALIDMEASUREMENT;
        mentryView.textValidation.Hidden = false;
        return;
      }
			if(start.addPan == null){
				Console.WriteLine("Pan gesture is null");
			}
			start.pressedSensor.isManual = true;
      start.pressedView.AddGestureRecognizer (start.addPan);
      start.availableView.Hidden = true;
      start.pressedView.BackgroundColor = UIColor.White;
      start.topLabel.Hidden = false;
      start.middleLabel.Hidden = false;
      start.bottomLabel.Hidden = false;
      start.topLabel.Text = " " + mentryView.dtypeButton.AccessibilityIdentifier;
      var amount = Convert.ToDecimal(mentryView.mtextValue.Text);
      if (mentryView.dtypeButton.AccessibilityIdentifier.Equals("Vaccum")) {
        start.middleLabel.Text = ((int)amount).ToString();
      } else {
        start.middleLabel.Text = amount.ToString("N");
      }
      start.bottomLabel.Text = mentryView.mbuttonText.Text;
      start.pressedSensor.isManual = true;
      start.addIcon.Hidden = true;

      ///CREATE MANUAL MANIFOLDS
      if(mentryView.dtypeButton.AccessibilityIdentifier.Equals("Pressure")){
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Pressure);     
      } else if (mentryView.dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Temperature);
      } else {
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Vacuum);
      }

      ///SET MANUALSENSOR MEASUREMENT AND UNIT TYPE
      start.pressedSensor.manualSensor.unit = AnalyserUtilities.getManualUnit(start.pressedSensor.manualSensor.type,mentryView.mbuttonText.Text.ToLower());

      start.pressedSensor.manualSensor.measurement = new Scalar(start.pressedSensor.manualSensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));
      
      mentryView.textValidation.Hidden = true;
      mentryView.mdoneButton.TouchUpInside -= handleManualPopup;
      mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      mentryView.mbuttonText.Text = start.pressures[0];
      mentryView.mtextValue.Text = "";
      start = null;
      mentryView.mView.Hidden = true;
      mentryView.mtextValue.ResignFirstResponder();
    }
    /// <summary>
    /// EVENT FUNCTION FOR ACTION POPUP FOR SINGLE SENSOR
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void handleActionPopup(object sender, EventArgs e)
    {

      UIAlertController addDeviceSheet;

      addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.DEVICEACTIONS, "", UIAlertControllerStyle.Alert);

      UIPopoverPresentationController presentationPopover = addDeviceSheet.PopoverPresentationController;
      if (presentationPopover!=null) {
        presentationPopover.SourceView = this.View;
        presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Right;
      }
      if (sensorActions.pressedSensor.isManual.Equals(false)) {
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
          alarmRequestViewer(sensorActions);
        }));
      }
      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.RENAME, UIAlertActionStyle.Default, (action) => {
        renamePopup();
      }));
      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.REMOVESENSOR, UIAlertActionStyle.Default, (action) => {
        AnalyserUtilities.RemoveDevice(sensorActions, lowHighSensors, analyzerSensors, analyzer.sensorList);
        blockerView.Hidden = true;
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {Console.WriteLine ("Cancel Action");blockerView.Hidden = true;}));
      this.View.Window.RootViewController.PresentViewController (addDeviceSheet, true, null);
    }
      /// <summary>
      /// SHOWS THE PICKER FOR THE MEASUREMENT TYPES
      /// </summary>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
    void showManualPicker(object sender, EventArgs e){
      UIAlertController mtypeAlert = UIAlertController.Create (Util.Strings.Analyzer.UNITPICKER, "", UIAlertControllerStyle.Alert);

      if (mentryView.dtypeButton.AccessibilityIdentifier == "Pressure") {
        foreach (string unit in start.pressures) {
          mtypeAlert.AddAction (UIAlertAction.Create (unit, UIAlertActionStyle.Default, (action) => {
            mentryView.mbuttonText.Text = unit;
          }));
        }
      } else if (mentryView.dtypeButton.AccessibilityIdentifier == "Temperature") {
        foreach (string unit in start.temperatures) {
          mtypeAlert.AddAction (UIAlertAction.Create (unit, UIAlertActionStyle.Default, (action) => {
            mentryView.mbuttonText.Text = unit;
          }));
        }
      } else if (mentryView.dtypeButton.AccessibilityIdentifier == "Vacuum") {
        foreach (string unit in start.vacuum) {
          mtypeAlert.AddAction (UIAlertAction.Create (unit, UIAlertActionStyle.Default, (action) => {           
            mentryView.mbuttonText.Text = unit;
          }));
        }
      }

      mtypeAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
      this.View.Window.RootViewController.PresentViewController (mtypeAlert, true, null);
      mentryView.mtextValue.ResignFirstResponder();
    }
    /// <summary>
    /// SHOWS THE ALERT FOR WHAT TYPE OF DEVICE TO USE
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void showDeviceTypePicker(object sender, EventArgs e){
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      UIAlertController dtypeAlert = UIAlertController.Create (Util.Strings.Analyzer.CHOOSEDEVICE, "", UIAlertControllerStyle.Alert);

      dtypeAlert.AddAction (UIAlertAction.Create (Util.Strings.Sensor.Type.PRESSURE, UIAlertActionStyle.Default, (action) => {
        mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
        mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
        mentryView.mbuttonText.Text = start.pressures[0];
      }));
      if(!mentryView.isManual){
	      dtypeAlert.AddAction (UIAlertAction.Create (Util.Strings.Sensor.Type.TEMPERATURE, UIAlertActionStyle.Default, (action) => {
	        mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.TEMPERATURE, UIControlState.Normal);
	        mentryView.dtypeButton.AccessibilityIdentifier = "Temperature";
	        mentryView.mbuttonText.Text = start.temperatures[0];
	      }));
      }
      dtypeAlert.AddAction (UIAlertAction.Create (Util.Strings.Sensor.Type.VACUUM, UIAlertActionStyle.Default, (action) => {
        mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.VACUUM, UIControlState.Normal);
        mentryView.dtypeButton.AccessibilityIdentifier = "Vacuum";
        mentryView.mbuttonText.Text = start.vacuum[0];
      }));
      dtypeAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
      this.View.Window.RootViewController.PresentViewController (dtypeAlert, true, null);
    }
    
    /// <summary>
    /// POPUP TO DETERMINE LOW/HIGH AREA ACTIONS
    /// </summary>
    /// <param name="pressedArea">LOCATION OF SENSOR</param>
    public void ShowPopup(UITableView tableArea, UIView pressedArea, lowHighSensor lowHighArea, sensor associatedSensor, string middleText){    
      UIAlertController addDeviceSheet;
      ///LOW/HIGH AREA IS ASSOCIATED WITH A SINGLE SENSOR ALREADY

      addDeviceSheet = UIAlertController.Create (Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);
      
      if(lowHighArea.snapArea.AccessibilityIdentifier == "low" || lowHighArea.snapArea.AccessibilityIdentifier == "high"){
      	addDeviceSheet = UIAlertController.Create (Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Device.Manager.SELF, UIAlertActionStyle.Default, (action) => {
          if(!AnalyserUtilities.freeSpot(analyzerSensors,associatedSensor,lowHighArea.snapArea.AccessibilityIdentifier)){
            showFullAlert();
          } else {
          	Console.WriteLine("Low high pressed");
            lhOnRequestViewer(lowHighArea);
          }
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.CREATEMANUAL, UIAlertActionStyle.Default, (action) => {

          if(!AnalyserUtilities.freeSpot(analyzerSensors,associatedSensor, lowHighArea.snapArea.AccessibilityIdentifier)){
            showFullAlert();
          } else {
            start = new manualEntry();
            start.pressedView = pressedArea;
            start.topLabel = lowHighArea.LabelTop;
            start.middleLabel = lowHighArea.LabelMiddle;
            start.bottomLabel = lowHighArea.LabelBottom;
            start.subviewLabel = lowHighArea.LabelSubview;
            mentryView.mView.AccessibilityIdentifier = "Pressure";
            mentryView.isManual = true;
						mentryView.mdoneButton.TouchUpInside -= handleManualPopup;
            mentryView.mdoneButton.TouchUpInside += handleManualLHPopup;
            viewAnalyzerContainer.BringSubviewToFront(mentryView.mView);
            mentryView.mView.Hidden = false;
          }
        }));
      } else {
	      addDeviceSheet = UIAlertController.Create (lowHighArea.LabelTop.Text + " " + Util.Strings.ACTIONS, "", UIAlertControllerStyle.Alert);
	      if (associatedSensor.currentSensor != null && !associatedSensor.currentSensor.device.isConnected){
	        addDeviceSheet.AddAction(UIAlertAction.Create(Strings.Device.RECONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
	          lowHighSensors.lowArea.connectionSpinner(2);
	        }));
	      }
	
	      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.ADDSUBVIEW, UIAlertActionStyle.Default, (action) => {
	        AnalyserUtilities.subviewOptionChosen(lowHighArea);
	      }));
	
				if(!lowHighArea.isManual){
		      addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
		        AnalyserUtilities.alarmRequestViewer(associatedSensor);
		      }));
				}
	      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.REMOVESENSOR, UIAlertActionStyle.Default, (action) => {
	        var attached = AnalyserUtilities.RemoveDevice (associatedSensor, lowHighSensors);
	        Console.WriteLine("attached was: " + attached);
	        if(attached == "low"){
						for(int i = 0; i < 4; i++){
							analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
							analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
						}
					} else if (attached == "high"){
						for(int i = 4; i < 8; i++){
							analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
							analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
						}
					}
	      }));
	
	      if (associatedSensor.currentSensor != null && associatedSensor.currentSensor.device.isConnected) {
	        addDeviceSheet.AddAction(UIAlertAction.Create(Strings.Device.DISCONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
	          lowHighSensors.lowArea.connectionSpinner(1);
	        }));
	      }
	
	      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.RENAME, UIAlertActionStyle.Default, (action) => {
	        AnalyserUtilities.renamePopup(associatedSensor,lowHighArea);
	      }));
  
			}

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

      this.View.Window.RootViewController.PresentViewController (addDeviceSheet, true, null);
    }
    /// <summary>
    /// EVENT FUNCTION FOR LOW HIGH MANUAL POPUP. ASSOCIATES A SINGLE SENSOR TO THE LOW/HIGH AREA WHEN MAKING A MANUAL LOW/HIGH ENTRY
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void handleManualLHPopup(object sender, EventArgs e){
      
      if (mentryView.mtextValue.Text.Contains(".")) {
        var p1 = mentryView.mtextValue.Text.Split('.');
        var check = p1[1];
        if (check.Length.Equals(0)) {
          mentryView.mtextValue.Text += "0";
          return;
        }
      }
      if (mentryView.mtextValue.Text.Length <= 0) {        
        mentryView.textValidation.Text = Util.Strings.Analyzer.MISSINGVALUE;
        mentryView.textValidation.Hidden = false;
        return;
      }

      var amount = Convert.ToDecimal(mentryView.mtextValue.Text);
      var begin = 0;
      var end = 4;
      var color = UIColor.Blue;
      if (start.pressedView.AccessibilityIdentifier == "high") {
        begin = 4;
        end = 8;
        color = UIColor.Red;
      }

      for (int i = begin; i < end; i++) {
        if (!analyzerSensors.viewList[i].availableView.Hidden) {
          analyzerSensors.viewList[i].addIcon.Hidden = true;
          analyzerSensors.viewList[i].availableView.Hidden = true;
          analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.White;
          analyzerSensors.viewList[i].snapArea.AddGestureRecognizer (analyzerSensors.viewList [i].panGesture);
          analyzerSensors.viewList[i].topLabel.Text = " " + mentryView.dtypeButton.AccessibilityIdentifier;
          analyzerSensors.viewList[i].topLabel.Hidden = false;
          analyzerSensors.viewList[i].topLabel.BackgroundColor = color;
          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;

          if (mentryView.mbuttonText.Text == "micron") {
            analyzerSensors.viewList[i].middleLabel.Text = ((int)amount).ToString();
          } else {
            analyzerSensors.viewList[i].middleLabel.Text = amount.ToString("N");
          }
          analyzerSensors.viewList[i].middleLabel.Hidden = false;
          analyzerSensors.viewList[i].bottomLabel.Text = mentryView.mbuttonText.Text;
          analyzerSensors.viewList[i].bottomLabel.Hidden = false;
          analyzerSensors.viewList[i].sactionView.connectionColor.Hidden = true;
          analyzerSensors.viewList[i].isManual = true;
          
          if(mentryView.dtypeButton.AccessibilityIdentifier.Equals("Pressure")){
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Pressure);
          } else if (mentryView.dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Temperature);
          } else {
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Vacuum);
          }
          analyzerSensors.viewList[i].manualSensor.name = analyzerSensors.viewList[i].topLabel.Text;
          analyzerSensors.viewList[i].manualSensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,mentryView.mbuttonText.Text.ToLower());
          analyzerSensors.viewList[i].manualSensor.measurement = new Scalar(analyzerSensors.viewList[i].manualSensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));
         
					Console.WriteLine("Handlemanuallhpopup about to low high associate");
          if (begin == 4) {
						/////ASSOCIATE HIGH AREA TO MANUALLY CREATED SENSOR 
          	lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[i].manualSensor;
          	lowHighSensors.highArea.manualGType = analyzerSensors.viewList[i].manualSensor.type.ToString();
						lowHighSensors.highArea.isManual = true;
						lowHighSensors.highArea.manifold = new Manifold(lowHighSensors.highArea.manualSensor);      
						lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
						lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						lowHighSensors.highArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
					  var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[i].manualSensor.type, analyzerSensors.viewList[i].manualSensor.measurement, true).Split(' ');
						lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
						lowHighSensors.highArea.LabelBottom.Text = analyzerSensors.viewList[i].manualSensor.unit.ToString();
						lowHighSensors.highArea.LabelSubview.Text = " " + lowHighSensors.highArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
            ///////////////////// change the high manifold to be based on moved location instead of created position
            Console.WriteLine("addLHDeviceSensor already on analyzer Should change highAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = i.ToString();
            analyzer.highAccessibility = i.ToString();
            /////////////////////                          
            //analyzer.highSubviews = analyzerSensors.viewList[i].highArea.tableSubviews; 
            analyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
            lowHighSensors.showView(lowHighSensors.highArea);
						lowHighSensors.highArea.Connection.Hidden = true;
						lowHighSensors.highArea.connectionColor.Hidden = true;            
          } else {
						/////ASSOCIATE LOW AREA TO MANUALLY CREATED SENSOR 
          	lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[i].manualSensor;
          	lowHighSensors.lowArea.manualGType = analyzerSensors.viewList[i].manualSensor.type.ToString();
						lowHighSensors.lowArea.isManual = true;  
						lowHighSensors.lowArea.manifold = new Manifold(lowHighSensors.lowArea.manualSensor);      
						lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
						lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						lowHighSensors.lowArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
					  var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[i].manualSensor.type, analyzerSensors.viewList[i].manualSensor.measurement, true).Split(' ');
						lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
						lowHighSensors.lowArea.LabelBottom.Text = analyzerSensors.viewList[i].manualSensor.unit.ToString();
						lowHighSensors.lowArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;						
            ///////////////////// change the low manifold to be based on moved location instead of created position
            Console.WriteLine("handleManualLHPopup Should change lowAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = i.ToString();
            analyzer.lowAccessibility = i.ToString();
            /////////////////////
            analyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
            lowHighSensors.showView(lowHighSensors.lowArea);
						lowHighSensors.lowArea.Connection.Hidden = true;
						lowHighSensors.lowArea.connectionColor.Hidden = true;             
          }

          break; 
        }
      }

      mentryView.mdoneButton.TouchUpInside -= handleManualLHPopup;
      mentryView.dtypeButton.SetTitle (Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      mentryView.mtextValue.Text = "";
      mentryView.mbuttonText.Text = start.pressures[0];
      start = null;
      mentryView.mView.Hidden = true;
      mentryView.mtextValue.ResignFirstResponder();
    }

    /// <summary>
    /// CREATE GESTURE RECOGNIZERS FOR DRAG/DROP AND LONG PRESS
    /// </summary>
    void AddSensorGestures(sensorGroup sensorGroup, sensor Sensor, UIDynamicAnimator animator, UISnapBehavior snap){
      ////CREATE OFFSETS TO FOLLOW EACH SUBVIEW AS IT MOVES

      nfloat dx = 0;
      nfloat dy = 0;

      Sensor.panGesture = new UIPanGestureRecognizer (() => {
        if (Sensor.panGesture.State == UIGestureRecognizerState.Began) {
          //View.BringSubviewToFront(Sensor.snapArea);
          viewAnalyzerContainer.BringSubviewToFront(Sensor.snapArea);
        }
        if ((Sensor.panGesture.State == UIGestureRecognizerState.Began || Sensor.panGesture.State == UIGestureRecognizerState.Changed) && (Sensor.panGesture.NumberOfTouches == 1)) {

          // remove any previosuly applied snap behavior to avoid a flicker that will occur if both the gesture and physics are operating on the view simultaneously
          if (snap != null)
            animator.RemoveBehavior (snap);

          //var p0 = Sensor.panGesture.LocationInView (View);
          var p0 = Sensor.panGesture.LocationInView (viewAnalyzerContainer);

          if (dx == 0) {
            dx = p0.X - Sensor.snapArea.Center.X;
          }

          if (dy == 0) {
            dy = p0.Y - Sensor.snapArea.Center.Y;
          }

          // this is where the offsets are applied so that the location of the image follows the point where the image is touched as it is dragged,
          // otherwise the center of the image would snap to the touch point at the start of the pan gesture

          var p1 = new CGPoint (p0.X - dx, p0.Y - dy);

          Sensor.snapArea.Center = p1;


        } else if (Sensor.panGesture.State == UIGestureRecognizerState.Ended) {
          //View.SendSubviewToBack(Sensor.snapArea);
          viewAnalyzerContainer.SendSubviewToBack(Sensor.snapArea);
          // reset offsets when dragging ends so that they will be recalculated for next touch and drag that occurs
          dx = 0;
          dy = 0;

          /// CHECK IF SENSOR WAS DROPPED IN LOW OR HIGH SECTION

          //AnalyserUtilities.updateLowHighArea(Sensor.panGesture.LocationInView(View), Sensor, lowHighSensors, sensorGroup, View);
          AnalyserUtilities.updateLowHighArea(Sensor.panGesture.LocationInView(viewAnalyzerContainer), Sensor, lowHighSensors, sensorGroup, viewAnalyzerContainer);

          ////FIGURE OUT WHERE TO SNAP THE SUBVIEW BASED ON IT'S LOCATION AND IDENTIFIER
          //AnalyserUtilities.LHSwapCheck(sensorGroup, lowHighSensors, Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier), Sensor.panGesture.LocationInView (View), View, analyzer);
          AnalyserUtilities.LHSwapCheck(sensorGroup, lowHighSensors, Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier), Sensor.panGesture.LocationInView (viewAnalyzerContainer), View, analyzer);

        } else if (Sensor.panGesture.State == UIGestureRecognizerState.Failed) {
          Console.WriteLine ("Touch has failed to be recognized for "+Sensor.snapArea.AccessibilityIdentifier+" area");
        }
      });

      Sensor.holdGesture = new UILongPressGestureRecognizer (() => {
        if (Sensor.holdGesture.State == UIGestureRecognizerState.Began) {

        }
      });

      Sensor.shortPressGesture = new UITapGestureRecognizer (() => {
        if(Sensor.shortPressGesture.State == UIGestureRecognizerState.Ended){
          ShowPopup(Sensor, 1);
        }
      });     

      Sensor.snapArea.AddGestureRecognizer (Sensor.shortPressGesture);
    }
    /// <summary>
    /// Adds the low high gestures.
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    void AddLowHighGestures(){

      lowHighSensors.lowArea.shortPress = new UITapGestureRecognizer (() => {
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[0],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[1],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[2],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[3],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[4],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[5],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[6],"Low Side Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.viewList[7],"Low Side Not Defined");}
        else {ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[7],"Low Side Not Defined");}
      });

      lowHighSensors.highArea.shortPress = new UITapGestureRecognizer (() => {
        if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[0],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[1],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[2],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[3],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[4],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[5],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[6],"High Side Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[7],"High Side Not Defined");}
        else {ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[0],"High Side Not Defined");}
      });

      lowHighSensors.lowArea.snapArea.AddGestureRecognizer (lowHighSensors.lowArea.shortPress);
      lowHighSensors.highArea.snapArea.AddGestureRecognizer (lowHighSensors.highArea.shortPress);
    }  
    /// <summary>
    /// Shows the popup to rename a sensor
    /// </summary>
    void renamePopup(){
      UIAlertController textAlert = UIAlertController.Create (Util.Strings.Analyzer.ENTERNAME, sensorActions.topLabel.Text, UIAlertControllerStyle.Alert);
      textAlert.AddTextField(textField => {});
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
        sensorActions.topLabel.Text = " " + textAlert.TextFields[0].Text;
        sensorActions.pressedSensor.sactionView.pdeviceName.Text = textAlert.TextFields[0].Text;

        if(sensorActions.pressedSensor.currentSensor != null){
          ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,sensorActions.pressedSensor.currentSensor.device.serialNumber.ToString());
          ion.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,sensorActions.pressedSensor.currentSensor.device.serialNumber.ToString());
          //////RENAME LOW SIDE IF ASSOCIATED TO GAUGE SENSOR
					if(lowHighSensors.lowArea.currentSensor == sensorActions.pressedSensor.currentSensor){
						lowHighSensors.lowArea.LabelTop.Text = textAlert.TextFields[0].Text;
						lowHighSensors.lowArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;					
					} else if (lowHighSensors.highArea.currentSensor == sensorActions.pressedSensor.currentSensor){
						lowHighSensors.highArea.LabelTop.Text = textAlert.TextFields[0].Text;
						lowHighSensors.highArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;
					}
        } else {
          //////RENAME LOW SIDE IF ASSOCIATED TO MANUAL SENSOR
					if(lowHighSensors.lowArea.manualSensor == sensorActions.pressedSensor.manualSensor){
						lowHighSensors.lowArea.LabelTop.Text = textAlert.TextFields[0].Text;
						lowHighSensors.lowArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;					
					} else if (lowHighSensors.highArea.manualSensor == sensorActions.pressedSensor.manualSensor){
						lowHighSensors.highArea.LabelTop.Text = textAlert.TextFields[0].Text;
						lowHighSensors.highArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;
					}
				}
        }));
      this.View.Window.RootViewController.PresentViewController(textAlert, true, null);
    }

    /// <summary>
    /// Called to inflate the device manager viewcontroller and allow BT connections for single sensors
    /// </summary>
    private void OnRequestViewer(sensor area) {
			if(!remoteMode){
	      var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
						addDeviceSensor(area,sensor);
	      };
	      NavigationController.PushViewController(sb, true);
	    } else {
	      var sb = InflateViewController<RemoteDeviceManagerViewController>(VC_REMOTE_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
						addDeviceSensor(area,sensor);
	      };
	      NavigationController.PushViewController(sb, true);
			}
    }
    /// <summary>
    /// Called to inflate the device manager viewcontroller and allow BT connections for single sensors
    /// </summary>
    private void lhOnRequestViewer(lowHighSensor area) {
			if(!remoteMode){
	      var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
	      	if(sensor.type == ESensorType.Temperature){
						UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
						tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
						this.PresentViewController(tempAlert,true,null);
					}else {
	        	addLHDeviceSensor(area,sensor);
	        }
	      };
	      NavigationController.PushViewController(sb, true);
	    } else {
	      var sb = InflateViewController<RemoteDeviceManagerViewController>(VC_REMOTE_DEVICE_MANAGER);
	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
	      	if(sensor.type == ESensorType.Temperature){
						UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
						tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
						this.PresentViewController(tempAlert,true,null);
					}else {
	        	addLHDeviceSensor(area,sensor);
	        }
	      };
	      NavigationController.PushViewController(sb, true);
			}
    }
    
    public void addDeviceSensor(sensor area, GaugeDeviceSensor sensor){
    	//Console.WriteLine("Adding a device sensor " + sensor.device.serialNumber.rawSerial + " that is located at spot " + analyzerSensors.viewList.IndexOf(area) + " but belongs to area " + area.snapArea.AccessibilityIdentifier);
  		bool existingConnection = false;
      foreach(sensor item in analyzerSensors.viewList){
        if(item.currentSensor != null && item.currentSensor == sensor){
          existingConnection = true;
          break;
        } 
      }

      if(!existingConnection){
        //sensor.analyzerSlot = Convert.ToInt32(area.snapArea.AccessibilityIdentifier) - 1;
        sensor.analyzerSlot = analyzerSensors.viewList.IndexOf(area);
        sensor.analyzerArea = Convert.ToInt32(area.snapArea.AccessibilityIdentifier);
        if(analyzer.sensorList == null){
					analyzer.sensorList = new List<Sensor>();
				}
				
				if(!analyzer.sensorList.Contains(sensor)){
        	analyzer.sensorList.Add(sensor);
        }
        
        area.currentSensor = sensor;
        area.sactionView.currentSensor = sensor;

        area.deviceImage.Image = Devices.DeviceUtil.GetUIImageFromDeviceModel(area.currentSensor.device.serialNumber.deviceModel);
        area.connectionImage.Image = UIImage.FromBundle("ic_bluetooth_connected");
        area.snapArea.BackgroundColor = UIColor.White;
        area.availableView.Hidden = true;
        area.snapArea.AddGestureRecognizer(area.panGesture);
        area.topLabel.Text = " " + sensor.device.name;
        area.topLabel.Hidden = false;
        if(sensor.unit != Units.Vacuum.MICRON){
          area.middleLabel.Text = sensor.measurement.amount.ToString("N") + " ";
        } else {
          area.middleLabel.Text = sensor.measurement.amount + " ";
        }
        
        area.middleLabel.Hidden = false;
        area.bottomLabel.Text = sensor.measurement.unit.ToString();
        area.bottomLabel.Hidden = false;
        area.addIcon.Hidden = true;
        area.isManual = false;

        ///Check for low association for newly added sensor and alert for incorrect placement/setup
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
					var checkSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)];
					if(lowHighSensors.lowArea.manifold.secondarySensor != null){
						if(lowHighSensors.lowArea.manifold.secondarySensor == sensor){
							lowHighSensors.lowArea.attachedSensor = area;
							area.topLabel.BackgroundColor = UIColor.Blue;
							area.topLabel.TextColor = UIColor.Gray;
						}
					}
				}
        ///Check for high association for newly added sensor and alert for incorrect placement/setup
				if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
					var checkSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier)];
					if(lowHighSensors.highArea.manifold.secondarySensor != null){
						if(lowHighSensors.highArea.manifold.secondarySensor == sensor){
							lowHighSensors.highArea.attachedSensor = area;
							area.topLabel.BackgroundColor = UIColor.Red;
							area.topLabel.TextColor = UIColor.Gray;
						}
					}
				}
      }
		}
    /// <summary>
    /// Adds the LH Device sensor from the low or high menu.
    /// </summary>
    /// <param name="area">Area.</param>
    /// <param name="sensor">Sensor.</param>
    public void addLHDeviceSensor(lowHighSensor area, GaugeDeviceSensor sensor){
      bool existingConnection = false;
      int start, stop,existStart, existStop;
      
        if(area.location == "low"){
          start = 0;
          stop = 4;
          existStart = 4;
          existStop = 8;
        } else {
          existStart = 0;
          existStop = 4;
          start = 4;
          stop = 8;
        }
        ///DON'T ALLOW A USER TO ADD AN EXISTING SENSOR TO THE OPPOSITE SIDE. JUST LET THEM KNOW IT IS ALREADY ON THE ANALYZER
				for(int i = existStart; i < existStop; i++){
					if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == sensor){
			      UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.SENSOREXISTS, UIAlertControllerStyle.Alert);
			
			      fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            
			
			      PresentViewController (fullPopup, true, null);
						return;				
					}   
				}
				
				
        for(int i = start; i < stop; i ++){
          if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == sensor){
            if(start == 0){
              analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Blue;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              lowHighSensors.lowArea.currentSensor = analyzerSensors.viewList[i].currentSensor;
              lowHighSensors.lowArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
              lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew);

              ///////////////////// change the low manifold to be based on moved location instead of created position
	            Console.WriteLine("addLHDeviceSensor already on analyzer Should change lowAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
              analyzer.lowAccessibility = i.ToString();
	            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = i.ToString();
	            /////////////////////
	            
              analyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
			        //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
			        Console.WriteLine("AnalyzerViewController addLHDeviceSensor set low");
			        analyzer.lowFluid = lowHighSensors.lowArea.manifold.ptChart.fluid;
			        
              lowHighSensors.lowArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
              lowHighSensors.lowArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
							lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
				      var startMeasurement = SensorUtils.ToFormattedString(sensor.type, sensor.measurement, true).Split(' ');
	            lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];								
							lowHighSensors.lowArea.LabelBottom.Text = sensor.unit.ToString();
			        
              if(sensor != null && sensor.device.isConnected.Equals(true)){
                lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
                lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Green;
              } else if (sensor != null && !sensor.device.isConnected){
                lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
                lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Red;
              } else {
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Clear;
                lowHighSensors.lowArea.connectionColor.Hidden = true;
              }
			        
              lowHighSensors.showView(lowHighSensors.lowArea);
            } else {
            	if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == sensor){
								return;
							}
              analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              lowHighSensors.highArea.currentSensor = analyzerSensors.viewList[i].currentSensor;
              lowHighSensors.highArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
              lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew); 
              
              ///////////////////// change the high manifold to be based on moved location instead of created position
	            Console.WriteLine("addLHDeviceSensor already on analyzer Should change highAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
              analyzer.highAccessibility = i.ToString();
	            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = i.ToString();
	            /////////////////////              
              analyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
			        //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
			        Console.WriteLine("AnalyzerViewController addLHDeviceSensor set high");
			        analyzer.highFluid = lowHighSensors.highArea.manifold.ptChart.fluid;

              lowHighSensors.highArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
              lowHighSensors.highArea.LabelSubview.Text = " " + lowHighSensors.highArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
							lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
				      var startMeasurement = SensorUtils.ToFormattedString(sensor.type, sensor.measurement, true).Split(' ');
	            lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];								
							lowHighSensors.highArea.LabelBottom.Text = sensor.unit.ToString();
			        
              if(sensor != null && sensor.device.isConnected.Equals(true)){
                lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
                lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Green;
              } else if (sensor != null && !sensor.device.isConnected){
                lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
                lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Red;
              } else {
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Clear;
                lowHighSensors.highArea.connectionColor.Hidden = true;
              } 
             
              lowHighSensors.showView(lowHighSensors.highArea);
            }
            existingConnection = true;
            break;
          }
        }

        if(!existingConnection){
          for(int i = start; i < stop; i ++){
            if(!analyzerSensors.viewList[i].availableView.Hidden){
              if(!analyzer.sensorList.Contains(sensor)){
                analyzer.sensorList.Add(sensor);
              }
              sensor.analyzerArea = Convert.ToInt32(analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
              sensor.analyzerSlot = i;
              analyzerSensors.viewList[i].currentSensor = sensor;
              analyzerSensors.viewList[i].sactionView.currentSensor = sensor;

              analyzerSensors.viewList[i].isManual = false;

              analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.White;
              analyzerSensors.viewList[i].availableView.Hidden = true;
              analyzerSensors.viewList[i].addIcon.Hidden = true;
              analyzerSensors.viewList[i].topLabel.Text =" " + sensor.device.name;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              analyzerSensors.viewList[i].topLabel.Hidden = false;
              if(sensor.unit == Units.Vacuum.MICRON){
                analyzerSensors.viewList[i].middleLabel.Text = sensor.measurement.amount.ToString();
              } else {
                analyzerSensors.viewList[i].middleLabel.Text = sensor.measurement.amount.ToString("N");
              }
              analyzerSensors.viewList[i].middleLabel.Hidden = false;
              analyzerSensors.viewList[i].bottomLabel.Text = sensor.measurement.unit.ToString();
              analyzerSensors.viewList[i].bottomLabel.Hidden = false;
              analyzerSensors.viewList[i].deviceImage.Image = Devices.DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
              analyzerSensors.viewList[i].connectionImage.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[i].connectionImage.BackgroundColor = UIColor.Clear;
              analyzerSensors.viewList[i].snapArea.AddGestureRecognizer(analyzerSensors.viewList[i].panGesture);

              area.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              
              if(start == 0){
                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Blue;
                ///////SET THE LOW SIDE AREA TO THE ADDED SENSOR
                lowHighSensors.lowArea.currentSensor = sensor;
                lowHighSensors.lowArea.manifold = new Manifold(sensor);
                lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew);
                lowHighSensors.lowArea.LabelTop.Text = sensor.name;
                lowHighSensors.lowArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
								lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
					      var startMeasurement = SensorUtils.ToFormattedString(sensor.type, sensor.measurement, true).Split(' ');
		            lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];								
								lowHighSensors.lowArea.LabelBottom.Text = sensor.unit.ToString();
		            ///////////////////// change the low manifold to be based on moved location instead of created position
		            Console.WriteLine("addLHDeviceSensor doesn't exist on analyzer yet. Should change lowAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
                analyzer.lowAccessibility = i.ToString();
		            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = i.ToString();
		            /////////////////////                
				        //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
				        Console.WriteLine("AnalyzerViewController addLHDeviceSensor set low");
				        analyzer.lowFluid = lowHighSensors.lowArea.manifold.ptChart.fluid;
				        
                analyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
	              if(sensor != null && sensor.device.isConnected.Equals(true)){
	                lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
	                lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Green;
	              } else if (sensor != null && !sensor.device.isConnected){
	                lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
	                lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Red;
	              } else {
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Clear;
	                lowHighSensors.lowArea.connectionColor.Hidden = true;
	              }                
                lowHighSensors.showView(lowHighSensors.lowArea);
              } else {
                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Red;
                
                ///////SET THE LOW SIDE AREA TO THE ADDED SENSOR
                lowHighSensors.highArea.currentSensor = sensor;
                lowHighSensors.highArea.manifold = new Manifold(sensor);
                lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew);
                lowHighSensors.highArea.LabelTop.Text = sensor.name;
                lowHighSensors.highArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
								lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
					      var startMeasurement = SensorUtils.ToFormattedString(sensor.type, sensor.measurement, true).Split(' ');
		            lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];								
								lowHighSensors.highArea.LabelBottom.Text = sensor.unit.ToString();								
	              ///////////////////// change the high manifold to be based on moved location instead of created position
		            Console.WriteLine("addLHDeviceSensor already on analyzer Should change highAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
	              analyzer.highAccessibility = i.ToString();
		            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = i.ToString();
		            /////////////////////                            	
              	analyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
	              if(sensor != null && sensor.device.isConnected.Equals(true)){
	                lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
	                lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Green;
	              } else if (sensor != null && !sensor.device.isConnected){
	                lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
	                lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Red;
	              } else {
	                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Clear;
	                lowHighSensors.highArea.connectionColor.Hidden = true;
	              }              	
				        //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
				        Console.WriteLine("AnalyzerViewController addLHDeviceSensor set high 2");
				        analyzer.highFluid = lowHighSensors.highArea.manifold.ptChart.fluid;            	
                lowHighSensors.showView(lowHighSensors.highArea);
              }

			        ///Check for low high association for newly added sensor
			        if(lowHighSensors.lowArea.attachedSensor != null){
								var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier));
								var checkSensor = analyzerSensors.viewList[checkIndex];
								if(lowHighSensors.lowArea.attachedSensor.currentSensor != null){
									if(lowHighSensors.lowArea.attachedSensor.currentSensor == sensor){
										analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Blue;
										analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
									}
								}
							}
							
							if(lowHighSensors.highArea.attachedSensor != null){
								var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier));
								var checkSensor = analyzerSensors.viewList[checkIndex];
								if(lowHighSensors.highArea.attachedSensor.currentSensor != null){
									if(lowHighSensors.highArea.attachedSensor.currentSensor == sensor){
										analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Red;
										analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
									}
								}
							} 
              break;
            }
          }

        }
        if(start == 0){
          start = 4;
          stop = 8;
        } else {
          start = 0;
          stop = 4;
        }

        for(int i = start; i < stop; i ++){        
          if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == sensor){
            ///CHECK IF SENSOR WAS ON THE OPPOSITE SIDE AND LINKED TO THE HIGH OR LOW TO REMOVE THOSE ASSOCIATIONS AS WELL
		        if(start == 0){
							if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier){
								Console.WriteLine("low side was attached to the sensor before and needs to be cleared out");
		            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
		            analyzer.lowAccessibility = "low";
				        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
				        Console.WriteLine("AnalyzerViewController addLHDeviceSensor remove low");
				        analyzer.lowFluid = null;        
							}
						} else {
							if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier){
								Console.WriteLine("high side was attached to the sensor before and needs to be cleared out");
		            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
		            analyzer.highAccessibility = "high";
				        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
				        Console.WriteLine("AnalyzerViewController addLHDeviceSensor remove high");
				        analyzer.highFluid = null;
							}
						}
            analyzerSensors.viewList[i].topLabel.Hidden = true;
            analyzerSensors.viewList[i].middleLabel.Hidden = true;
            analyzerSensors.viewList[i].bottomLabel.Hidden = true;
            analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[i].addIcon.Hidden = false;
            analyzerSensors.viewList[i].availableView.Hidden = false;
            analyzerSensors.viewList[i].currentSensor = null;
            analyzerSensors.viewList[i].sactionView.currentSensor = null;
          }
        }       
		}

    private void alarmRequestViewer(actionPopup area) {
      var alarm = InflateViewController<SensorAlarmViewController>(VC_SENSOR_ALARMS);
      alarm.sensor = area.pressedSensor.currentSensor as Sensor;
      NavigationController.PushViewController(alarm, true);
    }

    void showFullAlert(){ 
      UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

      fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

      PresentViewController (fullPopup, true, null);
    }
    /// <summary>
    /// sets up the analyzer based off of user layout
    /// </summary>
    public void layoutAnalyzer(){
			for(int i = 0; i < analyzer.sensorList.Count; i++){
				//var viewIndex = analyzerSensors.areaList.IndexOf(analyzer.sensorList[i].analyzerArea);
				//Console.WriteLine("Looking at sensor list item location " + analyzer.sensorList[i].analyzerSlot + " that is package area " + analyzer.sensorList[i].analyzerArea);
				var viewIndex = analyzer.sensorList[i].analyzerSlot;
				addDeviceSensor(analyzerSensors.viewList[viewIndex],(GaugeDeviceSensor)analyzer.sensorList[i]);
			}
		}
		
		public async void refreshSensorLayout(){
			await Task.Delay(TimeSpan.FromMilliseconds(1000));
			
			while(webServices.downloading){
					AnalyserUtilities.confirmLayout(analyzerSensors,viewAnalyzerContainer);
					remoteViewOrder();				
					layoutAnalyzer();
			
					for(int a = 0; a < analyzerSensors.viewList.Count; a++){
						if(analyzerSensors.viewList[a].currentSensor != null && !analyzer.sensorList.Contains(analyzerSensors.viewList[a].currentSensor)){						
							AnalyserUtilities.RemoveRemoteDevice(analyzerSensors.viewList[a],lowHighSensors,analyzerSensors);
						}  
					}
					
					if(analyzer.lowAccessibility != "low" && analyzer.lowSideManifold != null){
						if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
							////LOW SIDE ASSOCIATION DOESN'T MATCH THE REMOTE LOW SIDE ASSOCIATION AND NEEDS TO BE SWITCHED
							if(analyzer.lowSideManifold != null && lowHighSensors.lowArea.currentSensor != null && lowHighSensors.lowArea.currentSensor.name != analyzer.lowSideManifold.primarySensor.name){
								lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzer.lowAccessibility;
								var previousSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)];
	
								AnalyserUtilities.RemoveLHAssociation(previousSensor);
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(analyzer.lowAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE LOW AREA
                if(newSensor.currentSensor != null){                
								  AnalyserUtilities.addLHSensorAssociation("low",newSensor, lowHighSensors);
                }
							}
							
							//////CHECK IF REMOTE LOW SIDE HAS A SECONDARY SENSOR
              if(analyzer.lowSideManifold != null && lowHighSensors.lowArea.manifold != null){
  							if(analyzer.lowSideManifold.secondarySensor == null){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(null);
  							} else if (lowHighSensors.lowArea.manifold.secondarySensor == null){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(analyzer.lowSideManifold.secondarySensor);
  							} else if (lowHighSensors.lowArea.manifold.secondarySensor != analyzer.lowSideManifold.secondarySensor){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(analyzer.lowSideManifold.secondarySensor);
  							}
              }
						////LOW SIDE IS CURRENTLY UNASSOCIATED AND NEEDS TO MATCH THE REMOTE ASSOCIATION
						} else {
								lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzer.lowAccessibility;
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(analyzer.lowAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE LOW AREA
                if(newSensor.currentSensor != null){                
								  AnalyserUtilities.addLHSensorAssociation("low",newSensor, lowHighSensors);
                }	
						}
						confirmSubviews("low");
					} else {
						if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
							//Console.WriteLine("Low area should be clearing sensor from slot " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
							foreach(var clearSensor in analyzerSensors.viewList){
								if(clearSensor.currentSensor == lowHighSensors.lowArea.currentSensor){
									lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
									lowHighSensors.lowArea.manifold.SetSecondarySensor(null);
									lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
									AnalyserUtilities.RemoveLHAssociation(clearSensor);
									lowHighSensors.hideView(lowHighSensors.lowArea);   
									break;
								}
							}
						}
					}
					//////CHECK IF HIGH SIDE SHOULD HAVE AN ASSOCIATION
					if(analyzer.highAccessibility != "high" && analyzer.highSideManifold != null){
						////CHECK IF HIGH SIDE IS ALREADY ASSOCIATED TO A SENSOR MOUNT
						if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){						
							////CHECK IF THE CURRENT HIGH ASSOCIATION DOESN'T MATCH THE REMOTE ASSOCIATION
							if(analyzer.highSideManifold != null && lowHighSensors.highArea.currentSensor != null && lowHighSensors.highArea.currentSensor.name != analyzer.highSideManifold.primarySensor.name){
								lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzer.highAccessibility;
								var previousSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier)];
	
								AnalyserUtilities.RemoveLHAssociation(previousSensor);
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(analyzer.highAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE HIGH AREA
                if(newSensor.currentSensor != null){                
								  AnalyserUtilities.addLHSensorAssociation("high",newSensor, lowHighSensors);
                }
							}

							//////CHECK IF REMOTE HIGH SIDE HAS A SECONDARY SENSOR
              if(analyzer.highSideManifold != null && lowHighSensors.highArea.manifold != null){
  							if(analyzer.highSideManifold.secondarySensor == null){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(null);
  							} else if (lowHighSensors.highArea.manifold.secondarySensor == null){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(analyzer.highSideManifold.secondarySensor);
  							} else if (lowHighSensors.highArea.manifold.secondarySensor != analyzer.highSideManifold.secondarySensor){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(analyzer.highSideManifold.secondarySensor);
  							}
              }
						} 
						////HIGH SIDE IS CURRENTLY UNASSOCIATED AND NEEDS TO MATCH THE REMOTE ASSOCIATION
						else {
								lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzer.highAccessibility;
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(analyzer.highAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE HIGH AREA
                if(newSensor.currentSensor != null){
								  AnalyserUtilities.addLHSensorAssociation("high",newSensor, lowHighSensors);
                }
						}
						confirmSubviews();
					} else {
						if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
							//Console.WriteLine("High area should be clearing sensor from slot " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
							if(lowHighSensors.highArea.currentSensor != null){
								foreach(var clearSensor in analyzerSensors.viewList){
									if(clearSensor.currentSensor == lowHighSensors.highArea.currentSensor){
										lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
										lowHighSensors.highArea.manifold.SetSecondarySensor(null);
										lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
										AnalyserUtilities.RemoveLHAssociation(clearSensor);
										lowHighSensors.hideView(lowHighSensors.highArea);
										break;
									}
								}
							}
						}
					}
					await Task.Delay(TimeSpan.FromMilliseconds(1000));
			}
		}
		
		public void remoteViewOrder(){
			bool unordered = false;
			for(int i = 0; i < 8; i++){
				if(analyzer.sensorPositions[i] != Convert.ToInt32(analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier)){
					//Console.WriteLine("The view list does not match the order of the ordered positions");
					unordered = true;
					break;
				}
			}
			
			////TODO NEED TO FIGURE OUT A NON INTENSIVE WAY TO SORT THE VIEW LIST BASED ON THE SENSOR LAYOUT IF THEY DIFFER
			if(unordered){
				for(int v = 0; v < 8; v++){
					if(analyzerSensors.viewList[v].snapArea.AccessibilityIdentifier != analyzer.sensorPositions[v].ToString()){
						for(int l = 0; l < 8; l++){
							if(analyzerSensors.viewList[l].snapArea.AccessibilityIdentifier == analyzer.sensorPositions[v].ToString()){
								var repositionSensor = analyzerSensors.viewList[l];
								analyzerSensors.viewList.RemoveAt(l);
								analyzerSensors.viewList.Insert(v,repositionSensor);
								break;
							}
						}
					}
				}
			}			
		}
		
		//public void pauseRemote(bool paused){
		//	if(paused == false){
		//		refreshSensorLayout();
		//	}
		//}
	
		public async void disconnectRemoteMode(){
      var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
		 	remoteControl.controlView.Hidden = true;

		 	webServices.downloading = false;
		 	webServices.remoteViewing = false;
		 	webServices.paused = null;
		 	
			NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");

			await ion.setOriginalDeviceManager();
			rootVC.setMainMenu();
		}
		
		public void confirmSubviews(string section = "high"){
			//Console.WriteLine(section + " section");
			if(section == "low"){
				////SETUP HIGH AREA TABLE SOURCE IF NULL
				if(lowHighSensors.lowArea.subviewTable.Source == null){
						lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.lowArea.tableSubviews, lowHighSensors.lowArea);
				}
			
				///ADD ANY REMOTE SUBVIEWS NOT ALREADY IN THE TABLE
				foreach(var existing in analyzer.lowSubviews){
					if(!lowHighSensors.lowArea.tableSubviews.Contains(existing)){
						Console.WriteLine("Added low area subview " + existing);   
						lowHighSensors.lowArea.tableSubviews.Add(existing);
					}
				}
				
				////REMOVE ANY SUBVIEWS THAT ARE NO LONGER SENT FROM THE REMOTE LAYOUT
				foreach(var removal in lowHighSensors.lowArea.tableSubviews.ToArray()){
					if(!analyzer.lowSubviews.Contains(removal)){
						Console.WriteLine("Removed low area subview" + removal);
						lowHighSensors.lowArea.tableSubviews.Remove(removal);
					}
				}


				lowHighSensors.lowArea.subviewTable.Hidden = false;
				viewAnalyzerContainer.BringSubviewToFront(lowHighSensors.lowArea.subviewTable);
				lowHighSensors.lowArea.subviewTable.ReloadData();
			} else {
				////SETUP HIGH AREA TABLE SOURCE IF NULL
				if(lowHighSensors.highArea.subviewTable.Source == null){
						lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.highArea.tableSubviews, lowHighSensors.highArea);
				}
				///ADD ANY REMOTE SUBVIEWS NOT ALREADY IN THE TABLE
				foreach(var existing in analyzer.highSubviews){
					if(!lowHighSensors.highArea.tableSubviews.Contains(existing)){
						Console.WriteLine("Added high area subview " + existing);
						lowHighSensors.highArea.tableSubviews.Add(existing);
					}
				}
				////REMOVE ANY SUBVIEWS THAT ARE NO LONGER SENT FROM THE REMOTE LAYOUT
				foreach(var removal in lowHighSensors.highArea.tableSubviews.ToArray()){
					if(!analyzer.highSubviews.Contains(removal)){
						Console.WriteLine("Removed high area subview" + removal);
						lowHighSensors.highArea.tableSubviews.Remove(removal);
					}
				}
							
				lowHighSensors.highArea.subviewTable.Hidden = false;
				viewAnalyzerContainer.BringSubviewToFront(lowHighSensors.highArea.subviewTable);
				lowHighSensors.highArea.subviewTable.ReloadData();
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
    
    public void updateLogging(IonState.EType eventType){   
      InvokeOnMainThread ( () => {        
        if (ion.dataLogManager.isRecording) {
          dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          var recordingMessage = Util.Strings.Analyzer.RECORDINGSTARTED;
          showRecordingToast(recordingMessage);
        } else {
          dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          var recordingMessage = Util.Strings.Analyzer.RECORDINGSTOPPED;
          showRecordingToast(recordingMessage);
        }
      });
    }
		
    public override void ViewDidAppear(bool animated) {
	    if(!remoteMode){
  	      if (ion.dataLogManager.isRecording) {
  	        dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
  	        dataRecord.BackgroundColor = UIColor.Clear;
  	      } else {
  	        dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
  	        dataRecord.BackgroundColor = UIColor.Clear;
  	      }
	    } else {
				if(webServices.downloading){
					remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEVIEW;
				} else {
					remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEEDIT;
				}
				if(remoteControl != null && remoteControl.remoteLoggingButton != null){
					if(ion.remoteDevice.loggingStatus){
						NSUserDefaults.StandardUserDefaults.SetString("1","remoteLogging");
						remoteControl.remoteLoggingButton.SetTitle("Stop Logging", UIControlState.Normal);
					} else {
						NSUserDefaults.StandardUserDefaults.SetString("","remoteLogging");					
						remoteControl.remoteLoggingButton.SetTitle("Start Logging", UIControlState.Normal);
					}
				}					
			}
    }
	}
}
