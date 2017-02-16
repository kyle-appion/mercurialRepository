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
    public RemoteControls remoteControl;
    public UITapGestureRecognizer outsideTap;

    private IosION ion;
    public WebPayload webServices;

    /// <summary>
    /// The analyzer that we are working with.
    /// </summary>
    /// <value>The Analyzer.</value>
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
      	webServices.paused += pauseRemote;
      	viewAnalyzerContainer.AddSubview(blockerView);
      	viewAnalyzerContainer.BringSubviewToFront(blockerView);
      	viewAnalyzerContainer.BringSubviewToFront(remoteControl.controlView);
      } else {
				if(analyzer.sensorList == null){
		      var sensorList = new List<Sensor>();
		      analyzer.sensorList = sensorList;
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
			/*
			var dbButton = new UIButton(new CGRect(.3 * viewAnalyzerContainer.Bounds.Width,.3 * viewAnalyzerContainer.Bounds.Height,.3 * viewAnalyzerContainer.Bounds.Width, .1 * viewAnalyzerContainer.Bounds.Height));
			dbButton.BackgroundColor = UIColor.White;
			dbButton.Layer.BorderWidth = 1f;
			
			dbButton.TouchUpInside+= (sender, e) => {
				var sessions = ion.database.Query<SessionRow>("SELECT SID, frn_JID, sessionStart, sessionEnd FROM SessionRow ORDER BY SID");
        foreach (var MID in sessions) {
					Console.WriteLine("Session : " + MID.SID + " JID " + MID.frn_JID + " start: " + MID.sessionStart +  " end: " + MID.sessionEnd);
        }
				
				try{
					DateTime startPoint = DateTime.UtcNow.ToLocalTime();
	        var session = new SessionRow() {
	          frn_JID = 0,
	          sessionStart = startPoint,
	          sessionEnd = startPoint.AddMinutes(120),
	        };
	        var SID = ion.database.SaveAsync<SessionRow>(session).Result;
	        Console.WriteLine("Inserted session with id " + SID);

					var rows = new List<SensorMeasurementRow>();				
					for(int i = 0; i < 120; i++){
						var date = startPoint.AddMinutes(i);
			      var ret = new SensorMeasurementRow();    
						
						if(i > 69 && i < 74){
				      ret.serialNumber = "P314J0169";		
				      ret.frn_SID = session.SID;
				      ret.sensorIndex = 0;
				      ret.recordedDate = date;
				      ret.measurement = 1068687.3804;
						} else {
				      ret.serialNumber = "P314J0169";		
				      ret.frn_SID = session.SID;
				      ret.sensorIndex = 0;
				      ret.recordedDate = date;
				      ret.measurement = 1103161.1669;
				    }
	
						rows.Add(ret);				
					}
					var inserted = ion.database.InsertAll(rows, true);
					Console.WriteLine("Inserted all the rows " + inserted);
				} catch (Exception except){
					Console.WriteLine("data failed " + except);
				}
			};
			viewAnalyzerContainer.AddSubview(dbButton);
			viewAnalyzerContainer.BringSubviewToFront(dbButton);
			*/
    }

    /// <summary>
    /// Clicking this button begins the recording process for the currently connected analyzer devcies
    /// Stores the serial #, measurement, current date, session start time, session end time
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void recordDevices(){
      var recordingMessage = "";
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
        compressor = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width, .023 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height));
        expansion = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width,.365 * viewAnalyzerContainer.Bounds.Height,.044 * viewAnalyzerContainer.Bounds.Height,.044 * viewAnalyzerContainer.Bounds.Height));
      } else {
        compressor = new UIImageView(new CGRect(.47 * View.Bounds.Width, .025 * View.Bounds.Height, .044 * View.Bounds.Height, .044 * View.Bounds.Height));
        expansion = new UIImageView(new CGRect(.47 * View.Bounds.Width,.36 * View.Bounds.Height,.044 * View.Bounds.Height,.044 * View.Bounds.Height));
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


        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == pressedArea.snapArea.AccessibilityIdentifier) {
          pressedArea.sactionView.pLowHigh.Text = Util.Strings.Analyzer.LOWSIDE;
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Blue;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.TextColor = UIColor.White;
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == pressedArea.snapArea.AccessibilityIdentifier) {
          pressedArea.sactionView.pLowHigh.Text = Util.Strings.Analyzer.HIGHSIDE;
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.Red;
          pressedArea.sactionView.pLowHigh.Layer.CornerRadius = 6f;
          pressedArea.sactionView.pLowHigh.TextColor = UIColor.White;
        } else {
          pressedArea.sactionView.pLowHigh.Text = Util.Strings.Analyzer.UNSPECIFIED;
          pressedArea.sactionView.pLowHigh.BackgroundColor = UIColor.White;
          pressedArea.sactionView.pLowHigh.TextColor = UIColor.Black;
        }
        ///SHOW POPUP
        pressedArea.sactionView.aView.Hidden = false;
        //View.BringSubviewToFront (pressedArea.sactionView.aView);
        viewAnalyzerContainer.BringSubviewToFront (pressedArea.sactionView.aView);

        pressedArea.sactionView.pcloseButton.TouchUpInside += delegate {
          pressedArea.sactionView.aView.Hidden = true;
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
					pressedArea.sactionView.aView.Hidden = true;
					viewAnalyzerContainer.RemoveGestureRecognizer(outsideTap);
				});
        viewAnalyzerContainer.AddGestureRecognizer(outsideTap);
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
          start.isManual = pressedArea.isManual;
					mentryView.isManual = false;
					mentryView.mdoneButton.TouchUpInside -= handleManualLHPopup;
          mentryView.mdoneButton.TouchUpInside += handleManualPopup;
          //View.BringSubviewToFront(mentryView.mView);
          viewAnalyzerContainer.BringSubviewToFront(mentryView.mView);
          mentryView.mView.Hidden = false;
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel Action")));
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

      start.pressedSensor.lowArea.isManual = true;

      start.pressedSensor.lowArea.LabelTop.Text = mentryView.dtypeButton.AccessibilityIdentifier;
      start.pressedSensor.lowArea.LabelMiddle.Text = start.middleLabel.Text;
      start.pressedSensor.lowArea.LabelBottom.Text = mentryView.mbuttonText.Text;
      start.pressedSensor.lowArea.LabelSubview.Text = "  " + mentryView.dtypeButton.AccessibilityIdentifier + Util.Strings.Analyzer.LHTABLE;
      start.pressedSensor.lowArea.Connection.Hidden = true;
      start.pressedSensor.lowArea.connectionColor.Hidden = true;
      start.pressedSensor.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");

      start.pressedSensor.highArea.isManual = true;
      start.pressedSensor.highArea.LabelTop.Text = mentryView.dtypeButton.AccessibilityIdentifier;
      start.pressedSensor.highArea.LabelMiddle.Text = start.middleLabel.Text;        
      start.pressedSensor.highArea.LabelBottom.Text = mentryView.mbuttonText.Text;
      start.pressedSensor.highArea.LabelSubview.Text = "  " + mentryView.dtypeButton.AccessibilityIdentifier + Util.Strings.Analyzer.LHTABLE;
      start.pressedSensor.highArea.Connection.Hidden = true;
      start.pressedSensor.highArea.connectionColor.Hidden = true;
      start.pressedSensor.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");

      ///CREATE MANUAL MANIFOLDS
      if(mentryView.dtypeButton.AccessibilityIdentifier.Equals("Pressure")){
        start.pressedSensor.lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Pressure));
        start.pressedSensor.highArea.manifold = new Manifold(new ManualSensor(ESensorType.Pressure));
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Pressure);
        start.pressedSensor.lowArea.manualGType = "Pressure";
        start.pressedSensor.highArea.manualGType = "Pressure";       
      } else if (mentryView.dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
        start.pressedSensor.lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Temperature));
        start.pressedSensor.highArea.manifold = new Manifold(new ManualSensor(ESensorType.Temperature));
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Temperature);
        start.pressedSensor.lowArea.manualGType = "Temperature";
        start.pressedSensor.highArea.manualGType = "Temperature";
      } else {
        start.pressedSensor.lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Vacuum));
        start.pressedSensor.highArea.manifold = new Manifold(new ManualSensor(ESensorType.Vacuum));
        start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Vacuum);
        start.pressedSensor.lowArea.manualGType = "Vacuum";
        start.pressedSensor.highArea.manualGType = "Vacuum";
      }

      ///SET MANUALSENSOR MEASUREMENT AND UNIT TYPE
      start.pressedSensor.manualSensor.unit = AnalyserUtilities.getManualUnit(start.pressedSensor.manualSensor.type,mentryView.mbuttonText.Text.ToLower());
      start.pressedSensor.lowArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(start.pressedSensor.manualSensor.type,mentryView.mbuttonText.Text.ToLower());
      start.pressedSensor.highArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(start.pressedSensor.manualSensor.type,mentryView.mbuttonText.Text.ToLower());

      start.pressedSensor.manualSensor.measurement = new Scalar(start.pressedSensor.lowArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));

      start.pressedSensor.lowArea.manualSensor = start.pressedSensor.manualSensor;
      start.pressedSensor.highArea.manualSensor = start.pressedSensor.manualSensor;
      ///CREATE PTCHART AND MANIFOLD MEASUREMENTS
      if(start.pressedSensor.manualSensor.type == ESensorType.Pressure || start.pressedSensor.manualSensor.type == ESensorType.Temperature){
        //Console.WriteLine(start.pressedSensor.manualSensor.type.ToString() + " sensor given so making ptChart");
        start.pressedSensor.lowArea.manifold.ptChart = PTChart.New(start.pressedSensor.lowArea.ion, Fluid.EState.Dew);
        start.pressedSensor.lowArea.manifold.primarySensor.measurement = new Scalar(start.pressedSensor.lowArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));
        start.pressedSensor.highArea.manifold.ptChart = PTChart.New(start.pressedSensor.highArea.ion, Fluid.EState.Dew);
        start.pressedSensor.highArea.manifold.primarySensor.measurement = new Scalar(start.pressedSensor.highArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));
      } else {
       // Console.WriteLine(start.pressedSensor.manualSensor.type.ToString() + " sensor given so hiding the buttons that allow pt/scsh changes");
        start.pressedSensor.highArea.changeFluid.Hidden = true;
        start.pressedSensor.highArea.changePTFluid.Hidden = true;
        start.pressedSensor.lowArea.changeFluid.Hidden = true;
        start.pressedSensor.lowArea.changePTFluid.Hidden = true;
      }
      
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
    /// EVENT FUNCTION FOR ACTION POPUP
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
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel Action")));
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
    public void ShowPopup(UITableView tableArea, UIView pressedArea, lowHighSensor lowHighArea, sensor removeSensor, string middleText){    
      UIAlertController addDeviceSheet;
      ///LOW/HIGH AREA IS ASSOCIATED WITH A SINGLE SENSOR ALREADY

        addDeviceSheet = UIAlertController.Create (Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Device.Manager.SELF, UIAlertActionStyle.Default, (action) => {
          if(!AnalyserUtilities.freeSpot(analyzerSensors,removeSensor,lowHighArea.snapArea.AccessibilityIdentifier)){
            showFullAlert();
          } else {
          	Console.WriteLine("Low high pressed");
            lhOnRequestViewer(lowHighArea);
          }
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.CREATEMANUAL, UIAlertActionStyle.Default, (action) => {

          if(!AnalyserUtilities.freeSpot(analyzerSensors,removeSensor, lowHighArea.snapArea.AccessibilityIdentifier)){
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
            //View.BringSubviewToFront(mentryView.mView);
            viewAnalyzerContainer.BringSubviewToFront(mentryView.mView);
            mentryView.mView.Hidden = false;
          }
        }));

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
          //mentryView.textValidation.Text = "**Please enter a number after the decimal for this sensor's measurement**";
          //mentryView.textValidation.Hidden = false;
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
          analyzerSensors.viewList[i].topLabel.Text = " " + mentryView.dtypeButton.AccessibilityIdentifier + ": " + mentryView.dtypeButton.AccessibilityIdentifier [0] + mentryView.dtypeButton.AccessibilityIdentifier [1] + mentryView.dtypeButton.AccessibilityIdentifier [2] + mentryView.dtypeButton.AccessibilityIdentifier [3];
          analyzerSensors.viewList[i].topLabel.Hidden = false;
          analyzerSensors.viewList[i].topLabel.BackgroundColor = color;
          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
          analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = color;
          analyzerSensors.viewList[i].tLabelBottom.Hidden = false;
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

          analyzerSensors.viewList[i].lowArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
          analyzerSensors.viewList[i].lowArea.LabelMiddle.Text = analyzerSensors.viewList[i].middleLabel.Text;
          analyzerSensors.viewList[i].lowArea.LabelBottom.Text = mentryView.mbuttonText.Text;
          analyzerSensors.viewList[i].lowArea.LabelSubview.Text = "  " + analyzerSensors.viewList[i].topLabel.Text + Util.Strings.Analyzer.LHTABLE;
          analyzerSensors.viewList[i].lowArea.connectionColor.Hidden = true;          
          analyzerSensors.viewList[i].lowArea.Connection.Hidden = true;
          analyzerSensors.viewList[i].lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");

          analyzerSensors.viewList[i].highArea.LabelTop.Text = analyzerSensors.viewList[i].topLabel.Text;
          analyzerSensors.viewList[i].highArea.LabelMiddle.Text =  analyzerSensors.viewList[i].middleLabel.Text;
          analyzerSensors.viewList[i].highArea.LabelBottom.Text = mentryView.mbuttonText.Text;
          analyzerSensors.viewList[i].highArea.LabelSubview.Text = "  " + analyzerSensors.viewList[i].topLabel.Text + Util.Strings.Analyzer.LHTABLE;
          analyzerSensors.viewList[i].highArea.connectionColor.Hidden = true;          
          analyzerSensors.viewList[i].highArea.Connection.Hidden = true;
          analyzerSensors.viewList[i].highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
					Console.WriteLine("Handlemanuallhpopup about to low high associate");
          if (begin == 4) {
            analyzerSensors.viewList[i].lowArea.snapArea.Hidden = true;
            analyzerSensors.viewList[i].highArea.snapArea.Hidden = false;
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
            analyzer.highAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
            analyzer.highSubviews = analyzerSensors.viewList[i].highArea.tableSubviews;
            //View.BringSubviewToFront(analyzerSensors.viewList[i].highArea.snapArea);
            viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].highArea.snapArea);
          } else {
            analyzerSensors.viewList[i].lowArea.snapArea.Hidden = false;
            analyzerSensors.viewList[i].highArea.snapArea.Hidden = true;
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
            analyzer.lowAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
            analyzer.lowSubviews = analyzerSensors.viewList[i].lowArea.tableSubviews;
            //View.BringSubviewToFront(analyzerSensors.viewList[i].lowArea.snapArea);
            viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].lowArea.snapArea);
          }

          analyzerSensors.viewList[i].lowArea.isManual = true;
          analyzerSensors.viewList[i].highArea.isManual = true;

          if(mentryView.dtypeButton.AccessibilityIdentifier.Equals("Pressure")){
            analyzerSensors.viewList[i].lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Pressure));
            analyzerSensors.viewList[i].highArea.manifold = new Manifold(new ManualSensor(ESensorType.Pressure));
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Pressure);
            analyzerSensors.viewList[i].lowArea.manualGType = "Pressure";
            analyzerSensors.viewList[i].highArea.manualGType = "Pressure";
          } else if (mentryView.dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
            analyzerSensors.viewList[i].lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Temperature));
            analyzerSensors.viewList[i].highArea.manifold = new Manifold(new ManualSensor(ESensorType.Temperature));
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Temperature);
            analyzerSensors.viewList[i].lowArea.manualGType = "Temperature";
            analyzerSensors.viewList[i].highArea.manualGType = "Temperature";
          } else {
            analyzerSensors.viewList[i].lowArea.manifold = new Manifold(new ManualSensor(ESensorType.Vacuum));
            analyzerSensors.viewList[i].highArea.manifold = new Manifold(new ManualSensor(ESensorType.Vacuum));
            analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Vacuum);
            analyzerSensors.viewList[i].lowArea.manualGType = "Vacuum";
            analyzerSensors.viewList[i].highArea.manualGType = "Vacuum";
          }
          analyzerSensors.viewList[i].manualSensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,mentryView.mbuttonText.Text.ToLower());
          analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,mentryView.mbuttonText.Text.ToLower());
          analyzerSensors.viewList[i].highArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,mentryView.mbuttonText.Text.ToLower());
          analyzerSensors.viewList[i].manualSensor.measurement = new Scalar(analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));

          if(analyzerSensors.viewList[i].manualSensor.type == ESensorType.Pressure || analyzerSensors.viewList[i].manualSensor.type == ESensorType.Temperature){
            //Console.WriteLine(analyzerSensors.viewList[i].manualSensor.type.ToString() + " sensor given so making ptChart");
            analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[i].lowArea.manifold.primarySensor.measurement = new Scalar(analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));

            analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].highArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[i].highArea.manifold.primarySensor.measurement = new Scalar(analyzerSensors.viewList[i].highArea.manifold.primarySensor.unit,Convert.ToDouble(mentryView.mtextValue.Text));

          } else {
            //Console.WriteLine(analyzerSensors.viewList[i].manualSensor.type.ToString() + " sensor given so hiding the buttons that allow pt/scsh changes");
            analyzerSensors.viewList[i].highArea.changeFluid.Hidden = true;
            analyzerSensors.viewList[i].highArea.changePTFluid.Hidden = true;
            analyzerSensors.viewList[i].lowArea.changeFluid.Hidden = true;
            analyzerSensors.viewList[i].lowArea.changePTFluid.Hidden = true;
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
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea1,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea2,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea3,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea4,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea5,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea6,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea7,"Low Viewer Not Defined");}
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8"){ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea,analyzerSensors.snapArea8,"Low Viewer Not Defined");}
        else {ShowPopup(lowHighSensors.lowArea.subviewTable, lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.snapArea8,"Low Viewer Not Defined");}
      });

      lowHighSensors.highArea.shortPress = new UITapGestureRecognizer (() => {
        if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea1,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea2,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea3,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea4,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea5,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea6,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea7,"High Viewer Not Defined");}
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "8"){ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea8,"High Viewer Not Defined");}
        else {ShowPopup(lowHighSensors.highArea.subviewTable, lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.snapArea1,"High Viewer Not Defined");}
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
        sensorActions.pressedSensor.lowArea.LabelTop.Text = textAlert.TextFields[0].Text;
        sensorActions.pressedSensor.highArea.LabelTop.Text = textAlert.TextFields[0].Text;
        sensorActions.pressedSensor.lowArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;
        sensorActions.pressedSensor.highArea.LabelSubview.Text = "  " + textAlert.TextFields[0].Text + Util.Strings.Analyzer.LHTABLE;
        if(sensorActions.pressedSensor.currentSensor != null){
          ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,sensorActions.pressedSensor.currentSensor.device.serialNumber.ToString());
          ion.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,sensorActions.pressedSensor.currentSensor.device.serialNumber.ToString());
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
  		bool existingConnection = false;
      foreach(sensor item in analyzerSensors.viewList){
        if(item.currentSensor != null && item.currentSensor == sensor){
          existingConnection = true;
          break;
        } 
      }

      if(!existingConnection){
        sensor.analyzerSlot = Convert.ToInt32(area.snapArea.AccessibilityIdentifier) - 1;
        sensor.analyzerArea = Convert.ToInt32(area.snapArea.AccessibilityIdentifier);
        if(analyzer.sensorList == null){
					analyzer.sensorList = new List<Sensor>();
				}
				if(!analyzer.sensorList.Contains(sensor)){
        	analyzer.sensorList.Add(sensor);
        }
        area.currentSensor = sensor;
        area.sactionView.currentSensor = sensor;
        area.lowArea.currentSensor = sensor;
        area.highArea.currentSensor = sensor;
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
        area.lowArea.manifold = new Manifold(sensor);
        area.highArea.manifold = new Manifold(sensor);

        if(sensor.type == ESensorType.Pressure || sensor.type == ESensorType.Temperature){
          area.lowArea.manifold.ptChart = PTChart.New(area.lowArea.ion, Fluid.EState.Dew);
          area.highArea.manifold.ptChart = PTChart.New(area.highArea.ion, Fluid.EState.Dew);
        }else{
          area.lowArea.changeFluid.Hidden = true;
          area.lowArea.changePTFluid.Hidden = true;
          area.highArea.changeFluid.Hidden = true;
          area.highArea.changePTFluid.Hidden = true;
        }

        area.highArea.LabelTop.Text = " " + sensor.device.name;
        area.lowArea.LabelTop.Text = " " + sensor.device.name;
        area.lowArea.LabelMiddle.Text = area.middleLabel.Text;
        area.lowArea.LabelBottom.Text = sensor.measurement.unit.ToString() + "   ";
        area.lowArea.LabelSubview.Text = "  " + sensor.device.name + Util.Strings.Analyzer.LHTABLE;
        area.lowArea.DeviceImage.Image = area.deviceImage.Image;
        area.lowArea.isManual = false;

        area.highArea.LabelTop.Text = " " + sensor.device.name;
        area.highArea.LabelMiddle.Text = area.middleLabel.Text;
        area.highArea.LabelBottom.Text = sensor.measurement.unit.ToString() + "   ";
        area.highArea.LabelSubview.Text = "  " + sensor.device.name + Util.Strings.Analyzer.LHTABLE;
        area.highArea.DeviceImage.Image = area.deviceImage.Image;
        area.highArea.isManual = false;
        ///Check for low high association for newly added sensor and alert for incorrect placement/setup
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
					var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier));
					var checkSensor = analyzerSensors.viewList[checkIndex];
					if(checkSensor.lowArea.manifold.secondarySensor != null){
						if(checkSensor.lowArea.manifold.secondarySensor == sensor){
							checkSensor.lowArea.attachedSensor = area;
							area.topLabel.BackgroundColor = checkSensor.lowArea.LabelSubview.BackgroundColor;
							area.tLabelBottom.BackgroundColor = checkSensor.lowArea.LabelSubview.BackgroundColor;
							area.topLabel.TextColor = UIColor.White;
						}
					}
				}
				if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
					var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier));
					var checkSensor = analyzerSensors.viewList[checkIndex];
					if(checkSensor.highArea.manifold.secondarySensor != null){
						if(checkSensor.highArea.manifold.secondarySensor == sensor){
							checkSensor.highArea.attachedSensor = area;
							area.topLabel.BackgroundColor = checkSensor.highArea.LabelSubview.BackgroundColor;
							area.tLabelBottom.BackgroundColor = checkSensor.highArea.LabelSubview.BackgroundColor;
							area.topLabel.TextColor = UIColor.White;
						}
					}
				}
      }
		}
    
    public void addLHDeviceSensor(lowHighSensor area, GaugeDeviceSensor sensor){
      bool existingConnection = false;
      int start, stop;
      
        if(area.location == "low"){
          start = 0;
          stop = 4;
        } else {
          start = 4;
          stop = 8;
        }

        for(int i = start; i < stop; i ++){
          if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == sensor){
            if(start == 0){
              analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Blue;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = UIColor.Blue;
              analyzerSensors.viewList[i].tLabelBottom.Hidden = false;
              analyzerSensors.viewList[i].snapArea.BringSubviewToFront(analyzerSensors.viewList[i].lowArea.snapArea);
              analyzerSensors.viewList[i].lowArea.snapArea.Hidden = false;
              analyzerSensors.viewList[i].highArea.snapArea.Hidden = true;
              lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              analyzer.lowAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              analyzer.lowSubviews = analyzerSensors.viewList[i].lowArea.tableSubviews;
              viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].lowArea.snapArea);
            } else {
            	if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == sensor){
								return;
							}
              analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[i].tLabelBottom.Hidden = false;
              analyzerSensors.viewList[i].snapArea.BringSubviewToFront(analyzerSensors.viewList[i].highArea.snapArea);
              analyzerSensors.viewList[i].highArea.snapArea.Hidden = false;
              analyzerSensors.viewList[i].lowArea.snapArea.Hidden = true;
              lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              analyzer.highAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              analyzer.highSubviews = analyzerSensors.viewList[i].highArea.tableSubviews;
              viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].highArea.snapArea);
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
              analyzerSensors.viewList[i].currentSensor = sensor;
              analyzerSensors.viewList[i].sactionView.currentSensor = sensor;
              analyzerSensors.viewList[i].lowArea.currentSensor = sensor;
              analyzerSensors.viewList[i].highArea.currentSensor = sensor;
              analyzerSensors.viewList[i].isManual = false;
              analyzerSensors.viewList[i].lowArea.manifold = new Manifold(sensor as Sensor);
              analyzerSensors.viewList[i].highArea.manifold = new Manifold(sensor as Sensor);

              if(sensor.type == ESensorType.Pressure || sensor.type == ESensorType.Temperature){
                analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
                analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].highArea.ion, Fluid.EState.Dew);
              } else {
                analyzerSensors.viewList[i].highArea.changeFluid.Hidden = true;
                analyzerSensors.viewList[i].highArea.changePTFluid.Hidden = true;
                analyzerSensors.viewList[i].lowArea.changeFluid.Hidden = true;
                analyzerSensors.viewList[i].lowArea.changePTFluid.Hidden = true;
              }

              analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.White;
              analyzerSensors.viewList[i].availableView.Hidden = true;
              analyzerSensors.viewList[i].addIcon.Hidden = true;
              analyzerSensors.viewList[i].topLabel.Text =" " + sensor.device.name;
              analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
              analyzerSensors.viewList[i].topLabel.Hidden = false;
              analyzerSensors.viewList[i].tLabelBottom.Hidden = false;
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
              analyzerSensors.viewList[i].lowArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;              
              analyzerSensors.viewList[i].lowArea.LabelTop.Text = " " + analyzerSensors.viewList[i].topLabel.Text;
              analyzerSensors.viewList[i].lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
              analyzerSensors.viewList[i].lowArea.LabelMiddle.Text = analyzerSensors.viewList[i].middleLabel.Text;
              analyzerSensors.viewList[i].lowArea.LabelBottom.Text = sensor.measurement.unit.ToString() + " ";
              analyzerSensors.viewList[i].lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
              analyzerSensors.viewList[i].lowArea.LabelSubview.Text = "  " + analyzerSensors.viewList[i].topLabel.Text + Util.Strings.Analyzer.LHTABLE;
              analyzerSensors.viewList[i].lowArea.DeviceImage.Image = analyzerSensors.viewList[i].deviceImage.Image;
              analyzerSensors.viewList[i].lowArea.isManual = false;
              analyzerSensors.viewList[i].highArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              analyzerSensors.viewList[i].highArea.LabelTop.Text = " " + analyzerSensors.viewList[i].topLabel.Text;
              analyzerSensors.viewList[i].highArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
              analyzerSensors.viewList[i].highArea.LabelMiddle.Text = analyzerSensors.viewList[i].middleLabel.Text;
              analyzerSensors.viewList[i].highArea.LabelBottom.Text = sensor.measurement.unit.ToString() + " ";
              analyzerSensors.viewList[i].highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
              analyzerSensors.viewList[i].highArea.LabelSubview.Text = "  " + analyzerSensors.viewList[i].topLabel.Text + Util.Strings.Analyzer.LHTABLE;
              analyzerSensors.viewList[i].highArea.DeviceImage.Image = analyzerSensors.viewList[i].deviceImage.Image;
              analyzerSensors.viewList[i].highArea.isManual = false;
              area.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;

              if(start == 0){
                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Blue;
                analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = UIColor.Blue;
                analyzerSensors.viewList[i].lowArea.DeviceImage.Hidden = false;           
                analyzerSensors.viewList[i].lowArea.Connection.Hidden = false;
                analyzerSensors.viewList[i].lowArea.headingDivider.Hidden = false;
                analyzerSensors.viewList[i].lowArea.subviewHide.Hidden = false;
                viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].lowArea.snapArea);
                analyzerSensors.viewList[i].lowArea.snapArea.Hidden = false;
                analyzer.lowAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
                analyzer.lowSubviews = analyzerSensors.viewList[i].lowArea.tableSubviews;
              } else {
                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].highArea.DeviceImage.Hidden = false;           
                analyzerSensors.viewList[i].highArea.Connection.Hidden = false;
                analyzerSensors.viewList[i].highArea.headingDivider.Hidden = false;
                analyzerSensors.viewList[i].highArea.subviewHide.Hidden = false;
                viewAnalyzerContainer.BringSubviewToFront(analyzerSensors.viewList[i].highArea.snapArea);
                analyzerSensors.viewList[i].highArea.snapArea.Hidden = false;
              	analyzer.highAccessibility = analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier;
              	analyzer.highSubviews = analyzerSensors.viewList[i].highArea.tableSubviews;
              }

              if(sensor != null && sensor.device.isConnected.Equals(true)){
                analyzerSensors.viewList[i].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
                analyzerSensors.viewList[i].lowArea.connectionColor.BackgroundColor = UIColor.Green;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Green;
                analyzerSensors.viewList[i].lowArea.connectionColor.Hidden = false;
                analyzerSensors.viewList[i].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
                analyzerSensors.viewList[i].highArea.connectionColor.BackgroundColor = UIColor.Green;
                analyzerSensors.viewList[i].highArea.connectionColor.Hidden = false;
              } else if (sensor != null && !sensor.device.isConnected){
                analyzerSensors.viewList[i].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
                analyzerSensors.viewList[i].lowArea.connectionColor.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].lowArea.connectionColor.Hidden = false;
                analyzerSensors.viewList[i].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
                analyzerSensors.viewList[i].highArea.connectionColor.BackgroundColor = UIColor.Red;
                analyzerSensors.viewList[i].highArea.connectionColor.Hidden = false;
              } else {
                analyzerSensors.viewList[i].sactionView.connectionColor.BackgroundColor = UIColor.Clear;
                analyzerSensors.viewList[i].lowArea.connectionColor.Hidden = true;
                analyzerSensors.viewList[i].highArea.connectionColor.Hidden = true;
              }
			        ///Check for low high association for newly added sensor
			        if(lowHighSensors.lowArea.attachedSensor != null){
								var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier));
								var checkSensor = analyzerSensors.viewList[checkIndex];
								if(checkSensor.lowArea.attachedSensor.currentSensor != null){
									if(checkSensor.lowArea.attachedSensor.currentSensor == sensor){
										analyzerSensors.viewList[i].topLabel.BackgroundColor = checkSensor.lowArea.LabelSubview.BackgroundColor;
										analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = checkSensor.lowArea.LabelSubview.BackgroundColor;
										analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;
									}
								}
							}
							if(lowHighSensors.highArea.attachedSensor != null){
								var checkIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier));
								var checkSensor = analyzerSensors.viewList[checkIndex];
								if(checkSensor.highArea.attachedSensor.currentSensor != null){
									if(checkSensor.highArea.attachedSensor.currentSensor == sensor){
										analyzerSensors.viewList[i].topLabel.BackgroundColor = checkSensor.highArea.LabelSubview.BackgroundColor;
										analyzerSensors.viewList[i].tLabelBottom.BackgroundColor = checkSensor.highArea.LabelSubview.BackgroundColor;
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
							}
						} else {
							if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier){
								Console.WriteLine("high side was attached to the sensor before and needs to be cleared out");
		            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
		            analyzer.highAccessibility = "high";
							}
						}
            analyzerSensors.viewList[i].topLabel.Hidden = true;
            analyzerSensors.viewList[i].tLabelBottom.Hidden = true;
            analyzerSensors.viewList[i].middleLabel.Hidden = true;
            analyzerSensors.viewList[i].bottomLabel.Hidden = true;
            analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[i].addIcon.Hidden = false;
            analyzerSensors.viewList[i].availableView.Hidden = false;
            analyzerSensors.viewList[i].currentSensor = null;
            analyzerSensors.viewList[i].sactionView.currentSensor = null;
            analyzerSensors.viewList[i].lowArea.currentSensor = null;
            analyzerSensors.viewList[i].highArea.currentSensor = null;
            analyzerSensors.viewList[i].lowArea.snapArea.Hidden = true;
            analyzerSensors.viewList[i].highArea.snapArea.Hidden = true;
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
				var viewIndex = analyzerSensors.areaList.IndexOf(analyzer.sensorList[i].analyzerArea);
				addDeviceSensor(analyzerSensors.viewList[viewIndex],(GaugeDeviceSensor)analyzer.sensorList[i]);
			}
		}
		
		public async void refreshSensorLayout(){

			await Task.Delay(TimeSpan.FromMilliseconds(1000));
			while(webServices.downloading){
				AnalyserUtilities.confirmLayout(analyzerSensors,viewAnalyzerContainer);				
				layoutAnalyzer();
		
				for(int a = 0; a < analyzerSensors.viewList.Count; a++){
					if(analyzerSensors.viewList[a].currentSensor != null && !analyzer.sensorList.Contains(analyzerSensors.viewList[a].currentSensor)){						
						AnalyserUtilities.RemoveRemoteDevice(analyzerSensors.viewList[a],lowHighSensors,analyzerSensors);
					}
				}
				
				if(analyzer.lowAccessibility != "low"){
					if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
						if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != analyzer.lowAccessibility){
							var previousIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier));
							AnalyserUtilities.RemoveLHAssociation(analyzerSensors.viewList[previousIndex]);
						}
					}
					var newIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(analyzer.lowAccessibility));
					AnalyserUtilities.addLHSensorAssociation("low",analyzerSensors.viewList[newIndex]);
					confirmSubviews(analyzerSensors.viewList[newIndex],"low");   
				} else {
					foreach(var clearSensor in analyzerSensors.viewList){
						if(!clearSensor.lowArea.snapArea.Hidden){
							lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
							AnalyserUtilities.RemoveLHAssociation(clearSensor);
							break;
						}
					}
				}
				
				if(analyzer.highAccessibility != "high"){
					if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
						if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != analyzer.highAccessibility){
							var previousIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier));
							AnalyserUtilities.RemoveLHAssociation(analyzerSensors.viewList[previousIndex]);
						}
					}
					var newIndex = analyzerSensors.areaList.IndexOf(Convert.ToInt32(analyzer.highAccessibility));
					AnalyserUtilities.addLHSensorAssociation("high",analyzerSensors.viewList[newIndex]);
					confirmSubviews(analyzerSensors.viewList[newIndex]);
				} else {
					foreach(var clearSensor in analyzerSensors.viewList){
						if(!clearSensor.highArea.snapArea.Hidden){
							lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
							AnalyserUtilities.RemoveLHAssociation(clearSensor);
							break;
						}
					}
				}
				await Task.Delay(TimeSpan.FromMilliseconds(1000));
			}

		}
		
		public void pauseRemote(bool paused){
			if(paused == false){
				refreshSensorLayout();
			}
		}
	
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
		
		public void confirmSubviews(sensor updateSensor, string section = "high"){
			Console.WriteLine(section);
			if(section == "low"){
				foreach(var existing in analyzer.lowSubviews){
					if(!updateSensor.lowArea.tableSubviews.Contains(existing)){
						Console.WriteLine("Added subview " + existing);
						updateSensor.lowArea.tableSubviews.Add(existing);
						updateSensor.lowArea.subviewTable.Source = new AnalyzerTableSource(updateSensor.lowArea.tableSubviews, updateSensor.lowArea);
						updateSensor.lowArea.subviewTable.Hidden = false;
						updateSensor.lowArea.subviewTable.ReloadData();
					}
				}
				foreach(var removal in updateSensor.lowArea.tableSubviews.ToArray()){
					if(!analyzer.lowSubviews.Contains(removal)){
						Console.WriteLine("Removed " + removal);
						updateSensor.lowArea.tableSubviews.Remove(removal);
						updateSensor.lowArea.subviewTable.ReloadData();
					}
				}
			} else {
				foreach(var existing in analyzer.highSubviews){
					if(!updateSensor.highArea.tableSubviews.Contains(existing)){
						Console.WriteLine("Added subview " + existing);
						updateSensor.highArea.tableSubviews.Add(existing);
						updateSensor.highArea.subviewTable.Source = new AnalyzerTableSource(updateSensor.highArea.tableSubviews, updateSensor.highArea);
						updateSensor.highArea.subviewTable.Hidden = false;
						updateSensor.highArea.subviewTable.ReloadData();
					}
				}
				foreach(var removal in updateSensor.highArea.tableSubviews.ToArray()){
					if(!analyzer.highSubviews.Contains(removal)){
						Console.WriteLine("Removed " + removal);
						updateSensor.highArea.tableSubviews.Remove(removal);
						updateSensor.highArea.subviewTable.ReloadData();
					}
				}
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
			}
	    //viewAnalyzerContainer.SetNeedsDisplay();
    }
	}
}
