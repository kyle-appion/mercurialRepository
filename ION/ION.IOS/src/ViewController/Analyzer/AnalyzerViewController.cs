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
using ION.IOS.ViewController.DeviceGrid;
using static ION.Core.Content.Analyzer;

namespace ION.IOS.ViewController.Analyzer { 
  
	public partial class AnalyzerViewController : BaseIONViewController {
    public static manualEntry start;
    public static actionPopup sensorActions = new actionPopup();
    public static sensorGroup analyzerSensors;
    public static LowHighArea lowHighSensors;
    public static ManualView mentryView;
		public static ActionView sactionView;
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
			ion = AppState.context as IosION;

			blockerView = new UIView(viewAnalyzerContainer.Bounds);
			blockerView.Hidden = true;
		
      arvc = this;
      start = new manualEntry();
      analyzerSensors = new sensorGroup(viewAnalyzerContainer, this);      
      lowHighSensors = new LowHighArea (viewAnalyzerContainer, this, analyzerSensors);
      analyzerSensors.lowHighSensors = lowHighSensors;

	    mentryView = new ManualView(viewAnalyzerContainer);
      mentryView.start = start;
      mentryView.analyzerSensors = analyzerSensors;
      mentryView.lowHighSensors = lowHighSensors;
			sactionView = new ActionView(viewAnalyzerContainer);
			sactionView.pactionButton.TouchUpInside += handleActionPopup;

			InitNavigationBar("ic_nav_analyzer", false); 

      webServices = ion.webServices;
      AutomaticallyAdjustsScrollViewInsets = false;

      backAction = () => {
        
        root.navigation.ToggleMenu();
      };

			//if(remoteMode){
			//	remoteTitle = new UILabel(new CGRect(0, 0, 480, 44));
			//	remoteTitle.BackgroundColor = UIColor.Clear;
			//	remoteTitle.Lines = 2;
			//	remoteTitle.Font = UIFont.BoldSystemFontOfSize(14f);
			//	remoteTitle.ShadowColor = UIColor.FromWhiteAlpha(0.0f,.5f);
			//	remoteTitle.TextAlignment = UITextAlignment.Center;
			//	remoteTitle.TextColor = UIColor.Black;
			//	remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEVIEW;
				
			//	this.NavigationItem.TitleView = remoteTitle;				
			//} else {
	      Title = Util.Strings.Analyzer.SELF;
			//}

			if(ion.currentAnalyzer.sensorList == null){
	      ion.currentAnalyzer.sensorList = new List<Sensor>();
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

			if (UserInterfaceIdiomIsPhone) {
				expansion = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width, .023 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height));
				compressor = new UIImageView(new CGRect(.46 * viewAnalyzerContainer.Bounds.Width, .365 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height, .044 * viewAnalyzerContainer.Bounds.Height));
			} else {
				expansion = new UIImageView(new CGRect(.47 * View.Bounds.Width, .025 * View.Bounds.Height, .044 * View.Bounds.Height, .044 * View.Bounds.Height));
				compressor = new UIImageView(new CGRect(.47 * View.Bounds.Width, .36 * View.Bounds.Height, .044 * View.Bounds.Height, .044 * View.Bounds.Height));
			}
			compressor.Image = UIImage.FromBundle("ic_compressor");
			expansion.Image = UIImage.FromBundle("ic_expansionchamber");

			viewAnalyzerContainer.AddSubview(compressor);
			viewAnalyzerContainer.AddSubview(expansion);
			viewAnalyzerContainer.AddSubview(lowHighSensors.lowArea.snapArea);
			viewAnalyzerContainer.AddSubview(lowHighSensors.lowArea.subviewTable);
			viewAnalyzerContainer.AddSubview(lowHighSensors.highArea.snapArea);
			viewAnalyzerContainer.AddSubview(lowHighSensors.highArea.subviewTable);
			viewAnalyzerContainer.AddSubview(blockerView);
			viewAnalyzerContainer.AddSubview(mentryView.mView);
			viewAnalyzerContainer.AddSubview(sactionView.aView);

			ion.onIonStateChanged += updateLogging;
      addSlotGestures();
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
          await ion.dataLogManager.StopRecording();
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTOPPED;
        } else {
          await webServices.SetRemoteDataLog(KeychainAccess.ValueForKey("userID"),KeychainAccess.ValueForKey("layoutid"),"1");
          await ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTARTED;
        }
      } else {      
        if (ion.dataLogManager.isRecording) {
          dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          await ion.dataLogManager.StopRecording();
          recordingMessage = Util.Strings.Analyzer.RECORDINGSTOPPED;
        } else {
          dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
          dataRecord.BackgroundColor = UIColor.Clear;
          await ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval")));
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
    void addSlotGestures () 
    {
      Console.WriteLine("Setting up the slot tap gestures");
			analyzerSensors.snapArea1.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea1.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea1);
				}
			});
			analyzerSensors.snapArea1.snapArea.AddGestureRecognizer(analyzerSensors.snapArea1.shortPressGesture);

			analyzerSensors.snapArea2.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea2.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea2);
				}
			});
			analyzerSensors.snapArea2.snapArea.AddGestureRecognizer(analyzerSensors.snapArea2.shortPressGesture);
			analyzerSensors.snapArea3.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea3.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea3);
				}
			});
			analyzerSensors.snapArea3.snapArea.AddGestureRecognizer(analyzerSensors.snapArea3.shortPressGesture);
			analyzerSensors.snapArea4.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea4.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea4);
				}
			});
			analyzerSensors.snapArea4.snapArea.AddGestureRecognizer(analyzerSensors.snapArea4.shortPressGesture);
			analyzerSensors.snapArea5.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea5.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea5);
				}
			});
			analyzerSensors.snapArea5.snapArea.AddGestureRecognizer(analyzerSensors.snapArea5.shortPressGesture);
			analyzerSensors.snapArea6.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea6.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea6);
				}
			});
			analyzerSensors.snapArea6.snapArea.AddGestureRecognizer(analyzerSensors.snapArea6.shortPressGesture);
			analyzerSensors.snapArea7.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea7.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea7);
				}
			});
			analyzerSensors.snapArea7.snapArea.AddGestureRecognizer(analyzerSensors.snapArea7.shortPressGesture);
			analyzerSensors.snapArea8.shortPressGesture = new UITapGestureRecognizer(() => {
				if (analyzerSensors.snapArea8.shortPressGesture.State == UIGestureRecognizerState.Ended) {
					ShowPopup(analyzerSensors.snapArea8);
				}
			});
			analyzerSensors.snapArea8.snapArea.AddGestureRecognizer(analyzerSensors.snapArea8.shortPressGesture);

			lowHighSensors.lowArea.shortPress = new UITapGestureRecognizer(() => {

				if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[0]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[1]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[2]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[3]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[4]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[5]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[6]); } 
        else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[7]); } 
        else { ShowPopup(lowHighSensors.lowArea.snapArea, lowHighSensors.lowArea, analyzerSensors.viewList[7]); }
			});

			lowHighSensors.highArea.shortPress = new UITapGestureRecognizer(() => {
				if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[0]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[1]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[2]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[3]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[4]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[5]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[6]); } 
        else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[7]); } 
        else { ShowPopup(lowHighSensors.highArea.snapArea, lowHighSensors.highArea, analyzerSensors.viewList[0]); }
			});

			lowHighSensors.lowArea.snapArea.AddGestureRecognizer(lowHighSensors.lowArea.shortPress);
			lowHighSensors.highArea.snapArea.AddGestureRecognizer(lowHighSensors.highArea.shortPress);
    }
		/// <summary>
		/// CREATE AND SHOW POPUP FOR SINGLE SENSORS
		/// </summary>
		void ShowPopup(sensor pressedArea) {
			///IF SENSOR IS ACTIVE SET THAT SENSOR'S INFO IN THE POPUP
			if (pressedArea.availableView.Hidden) {

        if(pressedArea.currentSensor != null){
					sactionView.manualSensor = null;
					sactionView.currentSensor = pressedArea.currentSensor;
        } else {
          sactionView.currentSensor = null;
          sactionView.manualSensor = pressedArea.manualSensor;
        }

        sactionView.setUI(lowHighSensors.lowArea.manualSensor, lowHighSensors.highArea.manualSensor);

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

			} else {

				///SHOW ACTIONSHEET FOR ADDING A NEW SENSOR
				UIAlertController addDeviceSheet;

				addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);

				addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.Device.Manager.SELF, UIAlertActionStyle.Default, (action) => {
					OnRequestViewer(pressedArea);
				}));

				addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.Analyzer.CREATEMANUAL, UIAlertActionStyle.Default, (action) => {
					start.pressedSensor = pressedArea;
					start.addPan = pressedArea.panGesture;
					start.pressedView = pressedArea.snapArea;
					start.availableView = pressedArea.availableView;
					start.addLong = pressedArea.holdGesture;
					start.topLabel = pressedArea.topLabel;
					start.middleLabel = pressedArea.middleLabel;
					start.bottomLabel = pressedArea.bottomLabel;
					start.addIcon = pressedArea.addIcon;
					mentryView.setDoneAction(true);
					mentryView.mView.Hidden = false;
				}));

				addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => { Console.WriteLine("Cancel Action"); blockerView.Hidden = true; }));
				this.View.Window.RootViewController.PresentViewController(addDeviceSheet, true, null);
			}
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
        AnalyserUtilities.RemoveDevice(sensorActions, lowHighSensors, analyzerSensors, ion.currentAnalyzer.sensorList);
        blockerView.Hidden = true;
        sactionView.aView.Hidden = true;
	    }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {Console.WriteLine ("Cancel Action");blockerView.Hidden = true;}));
      this.View.Window.RootViewController.PresentViewController (addDeviceSheet, true, null);

    }

    /// <summary>
    /// POPUP TO DETERMINE LOW/HIGH AREA ACTIONS
    /// </summary>
    /// <param name="pressedArea">LOCATION OF SENSOR</param>
    public void ShowPopup(UIView pressedArea, lowHighSensor lowHighArea, sensor associatedSensor){    
      UIAlertController addDeviceSheet;
      ///LOW/HIGH AREA IS ASSOCIATED WITH A SINGLE SENSOR ALREADY

      addDeviceSheet = UIAlertController.Create (Util.Strings.Analyzer.ADDFROM, "", UIAlertControllerStyle.Alert);
      
      if(lowHighArea.snapArea.AccessibilityIdentifier == "low" || lowHighArea.snapArea.AccessibilityIdentifier == "high"){

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Device.Manager.SELF, UIAlertActionStyle.Default, (action) => {
          if((lowHighArea.snapArea.AccessibilityIdentifier == "low" && ion.currentAnalyzer.IsSideFull(ESide.Low)) || (lowHighArea.snapArea.AccessibilityIdentifier == "high" && ion.currentAnalyzer.IsSideFull(ESide.High))){ 
            showFullAlert();
          } else {
          	Console.WriteLine("Low high pressed");
            lhOnRequestViewer(lowHighArea);
          }
        }));

        addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.CREATEMANUAL, UIAlertActionStyle.Default, (action) => {

          if((lowHighArea.snapArea.AccessibilityIdentifier == "low" && ion.currentAnalyzer.IsSideFull(ESide.Low)) || (lowHighArea.snapArea.AccessibilityIdentifier == "high" && ion.currentAnalyzer.IsSideFull(ESide.High))){
            showFullAlert();
          } else {
            //start = new manualEntry();
            start.pressedView = pressedArea;
            start.topLabel = lowHighArea.LabelTop;
            start.middleLabel = lowHighArea.LabelMiddle;
            start.bottomLabel = lowHighArea.LabelBottom;
            start.subviewLabel = lowHighArea.LabelSubview;
            mentryView.mView.AccessibilityIdentifier = "Pressure";
            mentryView.setDoneAction();
            mentryView.isManual = true;
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
    /// Shows the popup to rename a sensor
    /// </summary>
    void renamePopup(){
      UIAlertController textAlert = UIAlertController.Create (Util.Strings.Analyzer.ENTERNAME, sensorActions.topLabel.Text, UIAlertControllerStyle.Alert);
      textAlert.AddTextField(textField => {});
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
        sensorActions.topLabel.Text = " " + textAlert.TextFields[0].Text;       

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
				var dg = InflateViewController<DeviceGridViewController>(VC_DEVICE_GRID);
        dg.fromAnalyzer = analyzerSensors.viewList.IndexOf(area);
        dg.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
			    addDeviceSensor(area,sensor);
		    };

				NavigationController.PushViewController(dg, true);
    }

		public void addDeviceSensor(sensor area, GaugeDeviceSensor sensor) {
			Console.WriteLine("Adding a device sensor " + sensor.device.serialNumber.rawSerial + " that is located at spot " + analyzerSensors.viewList.IndexOf(area) + " but belongs to area " + area.snapArea.AccessibilityIdentifier);
			bool existingConnection = false;

      if (ion.currentAnalyzer.IndexOfSensor(sensor) == -1){
        ion.currentAnalyzer.PutSensor(analyzerSensors.viewList.IndexOf(area),sensor,true);
				sensor.analyzerSlot = analyzerSensors.viewList.IndexOf(area);
				sensor.analyzerArea = Convert.ToInt32(area.snapArea.AccessibilityIdentifier);
				if (ion.currentAnalyzer.sensorList == null) {
					ion.currentAnalyzer.sensorList = new List<Sensor>();
				}

				if (!ion.currentAnalyzer.sensorList.Contains(sensor)) {
					ion.currentAnalyzer.sensorList.Add(sensor);
				}
				area.currentSensor = sensor;
				area.updateUI();
      } else {
        Console.WriteLine("Sensor exists at index " + ion.currentAnalyzer.IndexOfSensor(sensor));
      }
		}

    /// <summary>
    /// Called to inflate the device manager viewcontroller and allow BT connections for single sensors
    /// </summary>
    private void lhOnRequestViewer(lowHighSensor area) {

				var sb = InflateViewController<DeviceGridViewController>(VC_DEVICE_GRID);
        sb.fromAnalyzerLH = true;

	      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
	      	if(sensor.type == ESensorType.Temperature){
						UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
						tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
						this.PresentViewController(tempAlert,true,null);
					}else {
	        	addLHDeviceSensor(area,sensor);
  				  if (area.location == "low") {
  					  lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lowHighSensors.lowArea);
            } else {
  					  lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lowHighSensors.highArea);
  				  }
	        }
	      };
	      NavigationController.PushViewController(sb, true);

    }
    

    /// <summary>
    /// Adds the LH Device sensor from the low or high menu.
    /// </summary>
    /// <param name="area">Area.</param>
    /// <param name="sensor">Sensor.</param>
    public void addLHDeviceSensor(lowHighSensor area, GaugeDeviceSensor sensor){
 
      ///DON'T ALLOW A USER TO ADD AN EXISTING SENSOR TO THE OPPOSITE SIDE. JUST LET THEM KNOW IT IS ALREADY ON THE ANALYZER
      ESide side;
      var exists = ion.currentAnalyzer.GetSideOfSensor(sensor, out side);

      if ((exists && area.location == "low" && side == ESide.High) || (exists && area.location == "high" && side == ESide.Low)) {
				UIAlertController fullPopup = UIAlertController.Create(Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.SENSOREXISTS, UIAlertControllerStyle.Alert);

				fullPopup.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => { }));

				PresentViewController(fullPopup, true, null);
				return;
			} else  {
        if (area.location == "low" && ion.currentAnalyzer.IsSensorOnSide(sensor, ESide.Low)){
          var foundSensor = analyzerSensors.GetSlotFromSensor(sensor);
				  foundSensor.topLabel.BackgroundColor = UIColor.Blue;
				  foundSensor.topLabel.TextColor = UIColor.White;
          ion.currentAnalyzer.SetManifold(ESide.Low,sensor);
          ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);

          lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
					ion.currentAnalyzer.lowAccessibility = foundSensor.snapArea.AccessibilityIdentifier;
					area.snapArea.AccessibilityIdentifier = foundSensor.snapArea.AccessibilityIdentifier;

					area.gaugeUpdating(sensor);

				} else if (area.location == "high" && ion.currentAnalyzer.IsSensorOnSide(sensor, ESide.High)){
					var foundSensor = analyzerSensors.GetSlotFromSensor(sensor);
					foundSensor.topLabel.BackgroundColor = UIColor.Red;
					foundSensor.topLabel.TextColor = UIColor.White;
					ion.currentAnalyzer.SetManifold(ESide.High, sensor);
					ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
					lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
					ion.currentAnalyzer.highAccessibility = foundSensor.snapArea.AccessibilityIdentifier;
				  area.snapArea.AccessibilityIdentifier = foundSensor.snapArea.AccessibilityIdentifier;

					area.gaugeUpdating(sensor);

				} else {
					if (!ion.currentAnalyzer.sensorList.Contains(sensor)) {
						ion.currentAnalyzer.sensorList.Add(sensor);
					}
          var addedIndex = -1;
          if(area.location == "low"){
            ion.currentAnalyzer.AddSensorToSide(ESide.Low,sensor);
            addedIndex = ion.currentAnalyzer.IndexOfSensor(sensor);

						analyzerSensors.viewList[addedIndex].topLabel.BackgroundColor = UIColor.Blue;
						ion.currentAnalyzer.SetManifold(ESide.Low, sensor);
						lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
						lowHighSensors.lowArea.currentSensor = sensor;
            ion.currentAnalyzer.lowAccessibility = addedIndex.ToString();

					} else {
					  ion.currentAnalyzer.AddSensorToSide(ESide.High, sensor);
					  addedIndex = ion.currentAnalyzer.IndexOfSensor(sensor);

						analyzerSensors.viewList[addedIndex].topLabel.BackgroundColor = UIColor.Red;
						ion.currentAnalyzer.SetManifold(ESide.High, sensor);
						lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
						lowHighSensors.highArea.currentSensor = sensor;
						ion.currentAnalyzer.highAccessibility = addedIndex.ToString();

					}

					sensor.analyzerArea = Convert.ToInt32(analyzerSensors.viewList[addedIndex].snapArea.AccessibilityIdentifier);
					sensor.analyzerSlot = addedIndex;

					analyzerSensors.viewList[addedIndex].topLabel.TextColor = UIColor.White;
					analyzerSensors.viewList[addedIndex].isManual = false;
					analyzerSensors.viewList[addedIndex].currentSensor = sensor;
          analyzerSensors.viewList[addedIndex].updateUI();
					area.snapArea.AccessibilityIdentifier = addedIndex.ToString();
          area.gaugeUpdating(sensor);
					area.setLHUI();

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
      Console.WriteLine("Analyzer checking slot layout");
			for(int i = 0; i < ion.currentAnalyzer.sensorList.Count; i++){
				var viewIndex = ion.currentAnalyzer.sensorList[i].analyzerSlot;
				addDeviceSensor(analyzerSensors.viewList[viewIndex],(GaugeDeviceSensor)ion.currentAnalyzer.sensorList[i]);
			}
		}
		
		public async void refreshSensorLayout(){
			await Task.Delay(TimeSpan.FromMilliseconds(1000));
			
			while(webServices.downloading){
					AnalyserUtilities.confirmLayout(analyzerSensors);
					   				
					layoutAnalyzer();
			
					for(int a = 0; a < analyzerSensors.viewList.Count; a++){
						if(analyzerSensors.viewList[a].currentSensor != null && !ion.currentAnalyzer.sensorList.Contains(analyzerSensors.viewList[a].currentSensor)){						
							AnalyserUtilities.RemoveRemoteDevice(analyzerSensors.viewList[a],lowHighSensors,analyzerSensors);
						}  
					}
					
					if(ion.currentAnalyzer.lowAccessibility != "low" && ion.currentAnalyzer.lowSideManifold != null){
						if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low"){
							////LOW SIDE ASSOCIATION DOESN'T MATCH THE REMOTE LOW SIDE ASSOCIATION AND NEEDS TO BE SWITCHED
							if(ion.currentAnalyzer.lowSideManifold != null && lowHighSensors.lowArea.currentSensor != null && lowHighSensors.lowArea.currentSensor.name != ion.currentAnalyzer.lowSideManifold.primarySensor.name){
								lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = ion.currentAnalyzer.lowAccessibility;
								var previousSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)];
	
								lowHighSensors.lowArea.RemoveLHAssociation();
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(ion.currentAnalyzer.lowAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE LOW AREA
                if(newSensor.currentSensor != null){
  								ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, newSensor.currentSensor);
  								ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
  								lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
                  lowHighSensors.lowArea.addLHSensorAssociation(newSensor);
                }
							}
							
							//////CHECK IF REMOTE LOW SIDE HAS A SECONDARY SENSOR
              if(ion.currentAnalyzer.lowSideManifold != null && lowHighSensors.lowArea.manifold != null){
  							if(ion.currentAnalyzer.lowSideManifold.secondarySensor == null){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(null);
  							} else if (lowHighSensors.lowArea.manifold.secondarySensor == null){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(ion.currentAnalyzer.lowSideManifold.secondarySensor);
  							} else if (lowHighSensors.lowArea.manifold.secondarySensor != ion.currentAnalyzer.lowSideManifold.secondarySensor){
  								lowHighSensors.lowArea.manifold.SetSecondarySensor(ion.currentAnalyzer.lowSideManifold.secondarySensor);
  							}
              }
						////LOW SIDE IS CURRENTLY UNASSOCIATED AND NEEDS TO MATCH THE REMOTE ASSOCIATION
						} else {
								lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = ion.currentAnalyzer.lowAccessibility;
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(ion.currentAnalyzer.lowAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE LOW AREA
                if(newSensor.currentSensor != null){
    							ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, newSensor.currentSensor);
    							ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
    							lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
    							lowHighSensors.lowArea.addLHSensorAssociation(newSensor);
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
	
									lowHighSensors.lowArea.hideLHUI();   
									break;
								}
							}
						}
					}
					//////CHECK IF HIGH SIDE SHOULD HAVE AN ASSOCIATION
					if(ion.currentAnalyzer.highAccessibility != "high" && ion.currentAnalyzer.highSideManifold != null){
						////CHECK IF HIGH SIDE IS ALREADY ASSOCIATED TO A SENSOR MOUNT
						if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){						
							////CHECK IF THE CURRENT HIGH ASSOCIATION DOESN'T MATCH THE REMOTE ASSOCIATION
							if(ion.currentAnalyzer.highSideManifold != null && lowHighSensors.highArea.currentSensor != null && lowHighSensors.highArea.currentSensor.name != ion.currentAnalyzer.highSideManifold.primarySensor.name){
								lowHighSensors.highArea.snapArea.AccessibilityIdentifier = ion.currentAnalyzer.highAccessibility;
								var previousSensor = analyzerSensors.viewList[Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier)];
	
								lowHighSensors.highArea.RemoveLHAssociation();
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(ion.currentAnalyzer.highAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE HIGH AREA
                if(newSensor.currentSensor != null){
  								ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, newSensor.currentSensor);
  								ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
  								lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
  								lowHighSensors.highArea.addLHSensorAssociation(newSensor);
                }
							}

							//////CHECK IF REMOTE HIGH SIDE HAS A SECONDARY SENSOR
              if(ion.currentAnalyzer.highSideManifold != null && lowHighSensors.highArea.manifold != null){
  							if(ion.currentAnalyzer.highSideManifold.secondarySensor == null){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(null);
  							} else if (lowHighSensors.highArea.manifold.secondarySensor == null){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(ion.currentAnalyzer.highSideManifold.secondarySensor);
  							} else if (lowHighSensors.highArea.manifold.secondarySensor != ion.currentAnalyzer.highSideManifold.secondarySensor){
  								lowHighSensors.highArea.manifold.SetSecondarySensor(ion.currentAnalyzer.highSideManifold.secondarySensor);
  							}
              }
						} 
						////HIGH SIDE IS CURRENTLY UNASSOCIATED AND NEEDS TO MATCH THE REMOTE ASSOCIATION
						else {
								lowHighSensors.highArea.snapArea.AccessibilityIdentifier = ion.currentAnalyzer.highAccessibility;
								var newSensor = analyzerSensors.viewList[Convert.ToInt32(ion.currentAnalyzer.highAccessibility)];
                ///ONLY IF REMOTE SENSOR IS A GAUGE SENSOR DO WE ADD IT TO THE HIGH AREA
                if(newSensor.currentSensor != null){
    							ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, newSensor.currentSensor);
    							ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
    							lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
    							lowHighSensors.highArea.addLHSensorAssociation(newSensor);
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
										lowHighSensors.highArea.hideLHUI();
										break;
									}
								}
							}
						}
					}
					await Task.Delay(TimeSpan.FromMilliseconds(1000));
			}
		}
		
		//public void remoteViewOrder(){
		//	bool unordered = false;
		//	for(int i = 0; i < 8; i++){
		//		if(ion.currentAnalyzer.sensorPositions[i] != Convert.ToInt32(analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier)){
		//			//Console.WriteLine("The view list does not match the order of the ordered positions");
		//			unordered = true;
		//			break;
		//		}
		//	}
			
		//	////TODO NEED TO FIGURE OUT A LESS INTENSIVE WAY TO SORT THE VIEW LIST BASED ON THE SENSOR LAYOUT IF THEY DIFFER
		//	if(unordered){
		//		for(int v = 0; v < 8; v++){
		//			if(analyzerSensors.viewList[v].snapArea.AccessibilityIdentifier != ion.currentAnalyzer.sensorPositions[v].ToString()){
		//				for(int l = 0; l < 8; l++){
		//					if(analyzerSensors.viewList[l].snapArea.AccessibilityIdentifier == ion.currentAnalyzer.sensorPositions[v].ToString()){
		//						var repositionSensor = analyzerSensors.viewList[l];
		//						analyzerSensors.viewList.RemoveAt(l);
		//						analyzerSensors.viewList.Insert(v,repositionSensor);
		//						break;
		//					}
		//				}
		//			}
		//		}
		//	}			
		//}
		
		//public void pauseRemote(bool paused){
		//	if(paused == false){
		//		refreshSensorLayout();
		//	}
		//}
	
		//public async void disconnectRemoteMode(){
  //    var window = UIApplication.SharedApplication.KeyWindow;
  //    var rootVC = window.RootViewController as IONPrimaryScreenController;
      
		// 	remoteControl.controlView.Hidden = true;

		// 	webServices.downloading = false;
		// 	webServices.remoteViewing = false;
		// 	webServices.paused = null;
		 	
		//	NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");

		//	await ion.setOriginalDeviceManager();
		//	rootVC.setMainMenu();
		//}
		
		public void confirmSubviews(string section = "high"){
     
			//Console.WriteLine(section + " section");
			if(section == "low"){
				////SETUP HIGH AREA TABLE SOURCE IF NULL
				if(lowHighSensors.lowArea.subviewTable.Source == null){
						lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lowHighSensors.lowArea);
				}

				lowHighSensors.lowArea.subviewTable.Hidden = false;
				viewAnalyzerContainer.BringSubviewToFront(lowHighSensors.lowArea.subviewTable);
        lowHighSensors.lowArea.subviewTable.ReloadData();

			} else {
				////SETUP HIGH AREA TABLE SOURCE IF NULL
				if(lowHighSensors.highArea.subviewTable.Source == null){
						lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lowHighSensors.highArea);
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
	    //if(!remoteMode){
  	      if (ion.dataLogManager.isRecording) {
  	        dataRecord.SetImage(UIImage.FromBundle("ic_stop"), UIControlState.Normal);
  	        dataRecord.BackgroundColor = UIColor.Clear;
  	      } else {
  	        dataRecord.SetImage(UIImage.FromBundle("ic_record"), UIControlState.Normal);
  	        dataRecord.BackgroundColor = UIColor.Clear;
  	      }
        layoutAnalyzer();
	  //  } else {
			//	if(webServices.downloading){
			//		remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEVIEW;
			//	} else {
			//		remoteTitle.Text = Util.Strings.Analyzer.ANALYZERREMOTEEDIT;
			//	}
			//	if(ion.remoteDevice != null && remoteControl != null && remoteControl.remoteLoggingButton != null){
			//		if(ion.remoteDevice.loggingStatus){
			//			NSUserDefaults.StandardUserDefaults.SetString("1","remoteLogging");
			//			remoteControl.remoteLoggingButton.SetTitle("Stop Logging", UIControlState.Normal);
			//		} else {
			//			NSUserDefaults.StandardUserDefaults.SetString("","remoteLogging");					
			//			remoteControl.remoteLoggingButton.SetTitle("Start Logging", UIControlState.Normal);
			//		}
			//	}					
			//}
    }
	}
}
