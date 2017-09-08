using System;
using System.Collections.Generic;
using System.Globalization;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreAnimation;

using Appion.Commons.Measure;
using Appion.Commons.Util;

using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using ION.Core.App;

using ION.IOS.ViewController.Alarms;
using ION.IOS.Util;
using System.Collections.ObjectModel;
using ION.IOS.App;
using System.Linq;
using ION.IOS.Devices;
using ION.Core.Sensors.Filters;

namespace ION.IOS.ViewController.Analyzer
{
	class AnalyserUtilities 
	{
    public static IONPrimaryScreenController root { get; set; }
    public static IION ion = AppState.context;

		/// <summary>
		/// Removes 
		/// </summary>
		public static void RemoveRemoteDevice(sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors){
			Console.WriteLine("Removing Device: " + removeSensor.currentSensor.name);
      removeSensor.snapArea.RemoveGestureRecognizer (removeSensor.holdGesture);
      removeSensor.snapArea.RemoveGestureRecognizer(removeSensor.panGesture);
      removeSensor.snapArea.BackgroundColor = UIColor.Clear;
      removeSensor.availableView.Hidden = false;
      removeSensor.addIcon.Hidden = false;
      removeSensor.topLabel.Hidden = true;
      removeSensor.topLabel.BackgroundColor = UIColor.Clear;
      removeSensor.topLabel.TextColor = UIColor.Gray;
      removeSensor.middleLabel.Hidden = true;
      removeSensor.bottomLabel.Hidden = true;
      removeSensor.topLabel.Text = "Press ";
      removeSensor.middleLabel.Text = "0.00";
      removeSensor.bottomLabel.Text = "";
      removeSensor.currentSensor = null;
      removeSensor.manualSensor = null;
      removeSensor.isManual = false;
		}		

		/// WHAT TODO WHEN THEY WANT TO REMOVE A SENSOR FROM LOW/HIGH AREA
		public static string RemoveDevice(sensor removeSensor, LowHighArea lowHighSensors){
			Console.WriteLine("AnalyzerUtilities RemoveDevice from low/high area. Sensor name " + removeSensor.topLabel.Text + " and low area name " + lowHighSensors.lowArea.LabelTop.Text + " and high area name " + lowHighSensors.highArea.LabelTop.Text);
 			var attached = "null";
			removeSensor.topLabel.BackgroundColor = UIColor.Clear;
      removeSensor.topLabel.TextColor = UIColor.Gray;
      
			if(removeSensor.isManual){
				////CHECK WHICH AREA THE MANUAL SENSOR WAS ATTACHED TO
	      if (removeSensor.manualSensor == lowHighSensors.lowArea.manualSensor){
					/////CHECK IF LOW AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						attached = "low";
					}	      
	        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer low manifold");
	        ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);
					lowHighSensors.lowArea.hideLHUI();
	      }
	      else if (removeSensor.manualSensor == lowHighSensors.highArea.manualSensor){
					/////CHECK IF HIGH AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						attached = "high";
					}	      
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer high manifold");
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);
					lowHighSensors.highArea.hideLHUI();
				}				
			} else {
				////CHECK WHICH AREA THE GAUGE SENSOR WAS ATTACHED TO
	      if (removeSensor.currentSensor == lowHighSensors.lowArea.currentSensor){
					/////CHECK IF LOW AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						attached = "low";
					}	      
	        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer low manifold");
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);
					lowHighSensors.lowArea.hideLHUI();
	      }
	      else if (removeSensor.currentSensor == lowHighSensors.highArea.currentSensor){
					/////CHECK IF HIGH AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						attached = "high";
					}
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer high manifold");
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);
					lowHighSensors.highArea.hideLHUI();
				}			
			}

      Console.WriteLine("Low side identifier after LH removal: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("High side identifier after LH removal: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("Sending back attached: " + attached);
      return attached;
		}
		
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SINGLE SENSOR
    public static void RemoveDevice(actionPopup Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, List<Sensor> sensorList){
    	////REMOVE SENSOR FROM ACTIVE SENSOR LIST
      for(int i = 0; i < sensorList.Count; i++){
        if (sensorList[i] != null) {
          if (Sensor.pressedSensor.currentSensor != null) {
            if (sensorList[i].name == Sensor.pressedSensor.currentSensor.device.name && sensorList[i].type == Sensor.pressedSensor.currentSensor.type) {
              sensorList.Remove(Sensor.pressedSensor.currentSensor);
            }
          }
        }
      }
			/////RESET SENSOR MOUNT TO BE BLANK
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer (Sensor.addLong);
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer(Sensor.addPan);
      Sensor.pressedSensor.availableView.Hidden = false;
      Sensor.pressedView.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.addIcon.Hidden = false;
      Sensor.pressedSensor.topLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.topLabel.TextColor = UIColor.Gray;
      Sensor.pressedSensor.middleLabel.Hidden = true;
      Sensor.pressedSensor.bottomLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.Text = "Press " + Sensor.pressedSensor.snapArea.AccessibilityIdentifier;
      Sensor.pressedSensor.middleLabel.Text = "0.00";
      Sensor.pressedSensor.bottomLabel.Text = "psig";
      //not sure if removing should disconnect the device.....
      if (Sensor.pressedSensor.isManual.Equals(false)) {
        Sensor.pressedSensor.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.gaugeUpdating;
        ////CHECK IF REMOVING GAUGE SENSOR IS THE LOW SIDE
        if(lowHighSensors.lowArea.currentSensor != null && Sensor.pressedSensor.currentSensor == lowHighSensors.lowArea.currentSensor){
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);
					lowHighSensors.lowArea.hideLHUI();
				} 
        ////CHECK IF REMOVING GAUGE SENSOR IS THE HIGH SIDE
				else if (lowHighSensors.highArea.currentSensor != null && Sensor.pressedSensor.currentSensor == lowHighSensors.highArea.currentSensor){
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);
					lowHighSensors.highArea.hideLHUI();					
				}
      } else {
        ////CHECK IF REMOVING MANUAL SENSOR IS THE LOW SIDE
        if(lowHighSensors.lowArea.manualSensor != null && Sensor.pressedSensor.manualSensor == lowHighSensors.lowArea.manualSensor){
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);

					/////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.lowArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.lowArea.attachedSensor = null;
					}					
					
					lowHighSensors.lowArea.hideLHUI();
				} 
        ////CHECK IF REMOVING MANUAL SENSOR IS THE HIGH SIDE
				else if (lowHighSensors.highArea.manualSensor != null && Sensor.pressedSensor.manualSensor == lowHighSensors.highArea.manualSensor){
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
					ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);
				
	        /////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.highArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.highArea.attachedSensor = null;
					}
					lowHighSensors.highArea.hideLHUI();
				}
			}
			Sensor.pressedSensor.currentSensor = null;
			Sensor.pressedSensor.manualSensor = null;
			Sensor.pressedSensor.isManual = false;
		}

    /// <summary>
    /// Popup to show available subview options to add to LOW / HIGH area
    /// </summary>
    /// <param name="pressedArea">The LOW or High section pressed</param>
    public static void subviewOptionChosen(lowHighSensor pressedArea){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      var workingManifold = ion.currentAnalyzer.lowSideManifold;

      if(pressedArea.location == "high"){
        workingManifold = ion.currentAnalyzer.highSideManifold;
      }

      UIAlertController subviewAlert = UIAlertController.Create (Util.Strings.Analyzer.SUBVIEW, "", UIAlertControllerStyle.Alert);

			if (workingManifold.secondarySensor != null) {
				if (!workingManifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
  				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.SECONDARY, UIAlertActionStyle.Default, (action) => { 
            workingManifold.AddSensorProperty(new SecondarySensorProperty(workingManifold));
					  pressedArea.subviewTable.ReloadData();
					  pressedArea.subviewTable.Hidden = false;
				  }));
			  }
			}

			// The location of this block is kind of obnoxious, by pt chart is used by both of the below blocks.
			var ptChartFilter = new OrFilterCollection<Sensor>(new SensorOfTypeFilter(ESensorType.Pressure), new SensorOfTypeFilter(ESensorType.Temperature));
			var ptChart = workingManifold.ptChart;
			if (ptChart == null) {
				ptChart = PTChart.New(ion, Fluid.EState.Dew);
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty)) && ptChartFilter.Matches(workingManifold.primarySensor)) {
				workingManifold.ptChart = ptChart;
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.PT_CHART_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new PTChartSensorProperty(workingManifold));
          pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			// TODO Bug in checking sensor types
			// While the sensors are verified that they are pressure or temperature, they are not verified that they are exactly one
			// temperature and one pressure sensor. I let this be for the time being, in lieu expedience. This will bite you later,
			// mister maintainer. I am sorry. 
			if (!workingManifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty)) &&
			  ptChartFilter.Matches(workingManifold.primarySensor) && (workingManifold.secondarySensor == null || ptChartFilter.Matches(workingManifold.secondarySensor))) {
				workingManifold.ptChart = ptChart;
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.SHSC_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
          pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.MIN_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new MinSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.MAX_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new MaxSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.HOLD_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new HoldSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ROC_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new RateOfChangeSensorProperty(workingManifold, ion.preferences.device.trendInterval));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ALT_DESC, UIAlertActionStyle.Default, (action) => {
          workingManifold.AddSensorProperty(new AlternateUnitSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.TIMER_DESC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new TimerSensorProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

			if (!workingManifold.HasSensorPropertyOfType(typeof(TargetSuperheatSubcoolProperty))) {
				subviewAlert.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.TARGET_SHSC, UIAlertActionStyle.Default, (action) => {
					workingManifold.AddSensorProperty(new TargetSuperheatSubcoolProperty(workingManifold));
					pressedArea.subviewTable.ReloadData();
					pressedArea.subviewTable.Hidden = false;
				}));
			}

      subviewAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
      vc.PresentViewController (subviewAlert, true, null);
    }
		/// <summary>
		/// ARRANGES THE SINGLE SENSOR SUBVIEWS TO HOW THE USER HAS ORDERED THEM
		/// </summary>
		/// <param name="touchPoint">LOCATION OF SUBVIEW WHEN FINGER WAS REMOVED</param>
		/// <param name="position">WHICH SUBVIEW WAS MOVING</param>
    public static void sensorSwap(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint){
    	Console.WriteLine("AnalyserUtilities sensorSwap called. Low association: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high association: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
			int start = ion.currentAnalyzer.sensorPositions.IndexOf(position);
			int swap = 0;

      bool removeLH = false;

			////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
			if (analyzerSensors.snapRect1.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[0];

        ion.currentAnalyzer.sensorPositions[0] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start > 3) {
          removeLH = true;
        }
         swap = 0;
        
			} else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[1];

        ion.currentAnalyzer.sensorPositions[1] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start > 3) {
          removeLH = true;
        }
          swap = 1;
			} else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[2];
       
       	ion.currentAnalyzer.sensorPositions[2] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;
			
        if (start > 3) {
          removeLH = true;
        }
          swap = 2;
			} else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[3];
       	ion.currentAnalyzer.sensorPositions[3] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start > 3) {
          removeLH = true;
        }
          swap = 3;
			} else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[4];
       	ion.currentAnalyzer.sensorPositions[4] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start < 4) {
          removeLH = true;
        }
          swap = 4;
			} else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[5];
       	ion.currentAnalyzer.sensorPositions[5] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start < 4) {
          removeLH = true;
        }
          swap = 5;
			} else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[6];
       	ion.currentAnalyzer.sensorPositions[6] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;
        if (start < 4) {
          removeLH = true;
        }
          swap = 6;
			} else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
        swap = ion.currentAnalyzer.sensorPositions[7];
       	ion.currentAnalyzer.sensorPositions[7] = position;
        ion.currentAnalyzer.sensorPositions[start] = swap;

        if (start < 4) {
          removeLH = true;
        }
          swap = 7;
			} else {
				swap = start;
			}
      confirmLayout(analyzerSensors);

			arrangeViews(analyzerSensors);

			int swapLocation = ion.currentAnalyzer.sensorPositions.IndexOf(position);

			//////SENSOR MOVED TO OPPOSITE "SIDE" AND NEEDS TO CHECK IF IT IS ASSOCIATED TO THE CORRESPONDING LOW OR HIGH AREA
      if (removeLH) {   
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()){
          Console.WriteLine("In utilities low start sensorSwap and set low accessibility to " + start + " and high accessibility to " + swap);
					//////SWAP THE SENSOR MOUNT COLORS TO CORRESPOND TO THEIR NEW LOW HIGH ASSOCIATIONS
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Red;
   
          ///////////////////// change the high manifold to be based on moved location instead of created position
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = swap.ToString();
          ion.currentAnalyzer.highAccessibility = swap.ToString();

          ///////////////////// change the low manifold to be based on moved location instead of created position
          ion.currentAnalyzer.lowAccessibility = start.ToString();
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = start.ToString();

					var holderProperties = new List<ISensorProperty>(ion.currentAnalyzer.lowSideManifold.sensorProperties);

          //////ASSOCIATE THE LOW SIDE TO THE MOVING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[start].isManual) {
          	Console.WriteLine("Low side is going to look at gauge sensor " + analyzerSensors.viewList[start].topLabel.Text);
						lowHighSensors.lowArea.isManual = false;
						lowHighSensors.lowArea.manualSensor = null;
          	lowHighSensors.lowArea.currentSensor = analyzerSensors.viewList[start].currentSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, analyzerSensors.viewList[start].currentSensor);
					} else {
          	Console.WriteLine("Low side is going to look at manual sensor " + analyzerSensors.viewList[start].topLabel.Text);
          	lowHighSensors.lowArea.isManual = true;
          	lowHighSensors.lowArea.currentSensor = null;
          	lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[start].manualSensor;
            ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low,analyzerSensors.viewList[start].manualSensor);          	
					}

					foreach (var property in ion.currentAnalyzer.highSideManifold.sensorProperties) {
						ion.currentAnalyzer.lowSideManifold.AddSensorProperty(property);
					}
					ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
					lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
					lowHighSensors.lowArea.setLHUI();



          lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold,lowHighSensors.lowArea);

          ////HIDE SUBVIEW TOGGLE FOR LOW SIDE EMPTY SUBVIEW TABLES
					if(ion.currentAnalyzer.lowSideManifold.sensorProperties.Count == 0){
	      		lowHighSensors.lowArea.subviewHide.SetImage(null, UIControlState.Normal);						
					} else {					
	      		lowHighSensors.lowArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}
          ////HIDE SUBVIEW TOGGLE FOR HIGH SIDE EMPTY SUBVIEW TABLES
					if(ion.currentAnalyzer.highSideManifold.sensorProperties.Count == 0){
	      		lowHighSensors.highArea.subviewHide.SetImage(null, UIControlState.Normal);
					} else {
	      		lowHighSensors.highArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}

					//////RELOAD THE SUBVIEW TABLES WITH THEIR NEW SUBVIEW SETUPS
          lowHighSensors.lowArea.subviewTable.ReloadData();
          lowHighSensors.lowArea.subviewTable.Hidden = false;

					/////REMOVE THE LOW SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.lowArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.lowArea.attachedSensor = null;
          }

					/////REMOVE THE HIGH SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.highArea.attachedSensor != null) {
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.highArea.attachedSensor = null;
          }
          
          //////ASSOCIATE THE HIGH SIDE TO THE SWAPPING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[swap].isManual) {
          	Console.WriteLine("High side is going to look at gauge sensor " + analyzerSensors.viewList[swap].topLabel.Text);
          	lowHighSensors.highArea.isManual = false;
          	lowHighSensors.highArea.manualSensor = null;
          	lowHighSensors.highArea.currentSensor = analyzerSensors.viewList[swap].currentSensor;
          	ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High,analyzerSensors.viewList[swap].currentSensor);
					} else {
          	Console.WriteLine("High side is going to look at manual sensor " + analyzerSensors.viewList[swap].topLabel.Text);
          	lowHighSensors.highArea.isManual = true;
          	lowHighSensors.highArea.currentSensor = null;
          	lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[swap].manualSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, analyzerSensors.viewList[swap].manualSensor);
					}

					foreach (var property in holderProperties) {
						ion.currentAnalyzer.highSideManifold.AddSensorProperty(property);
					}
					ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
					lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
					lowHighSensors.highArea.setLHUI();
					lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lowHighSensors.highArea);
					lowHighSensors.highArea.subviewTable.ReloadData();
					lowHighSensors.highArea.subviewTable.Hidden = false;

				}
        else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()){
    
          Console.WriteLine("In utilities high start sensorSwap and set low accessibility to " + swap + " and high accessibility to " + start);
					//////SWAP THE SENSOR MOUNT COLORS TO CORRESPOND TO THEIR NEW LOW HIGH ASSOCIATIONS
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Red;
					
          ///////////////////// change the low associations to be based on moved location instead of created position
          ion.currentAnalyzer.lowAccessibility = swap.ToString();
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = swap.ToString();          
          /////////////////////

          ///////////////////// change the high associations to be based on moved location instead of created position
          ion.currentAnalyzer.highAccessibility = start.ToString();
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = start.ToString();
					///////////////////// 
					var holderProperties = new List<ISensorProperty>(ion.currentAnalyzer.highSideManifold.sensorProperties);

					//////ASSOCIATE THE HIGH SIDE TO THE MOVING SENSOR'S INFORMATION
					if (!analyzerSensors.viewList[start].isManual) {
          	Console.WriteLine("High side is going to look at gauge sensor " + analyzerSensors.viewList[start].topLabel.Text);
						lowHighSensors.highArea.isManual = false;
						lowHighSensors.highArea.manualSensor = null;
          	lowHighSensors.highArea.currentSensor = analyzerSensors.viewList[start].currentSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, analyzerSensors.viewList[start].currentSensor);
					} else {
          	Console.WriteLine("High side is going to look at manual sensor " + analyzerSensors.viewList[start].topLabel.Text);
						lowHighSensors.highArea.isManual = true; 
          	lowHighSensors.highArea.currentSensor = null;
          	lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[start].manualSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, analyzerSensors.viewList[start].manualSensor);
					}

					ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
					lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
					lowHighSensors.highArea.setLHUI();

					foreach (var property in ion.currentAnalyzer.lowSideManifold.sensorProperties) {
						ion.currentAnalyzer.highSideManifold.AddSensorProperty(property);
					}

          lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold,lowHighSensors.highArea);

					//////RELOAD THE SUBVIEW TABLES WITH THEIR NEW SUBVIEW SETUPS
          lowHighSensors.highArea.subviewTable.ReloadData();
          lowHighSensors.highArea.subviewTable.Hidden = false;
        
					/////REMOVE THE LOW SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.lowArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.lowArea.attachedSensor = null;
          }

					/////REMOVE THE HIGH SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.highArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.highArea.attachedSensor = null;
          }
          					
            //////ASSOCIATE THE LOW SIDE TO THE SWAPPING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[swap].isManual) {
          	Console.WriteLine("Low side is going to look at gauge sensor " + analyzerSensors.viewList[swap].topLabel.Text);
						lowHighSensors.lowArea.isManual = false; 
          	lowHighSensors.lowArea.manualSensor = null;
          	lowHighSensors.lowArea.currentSensor = analyzerSensors.viewList[swap].currentSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, analyzerSensors.viewList[swap].currentSensor);
					} else {
          	Console.WriteLine("Low side is going to look at manual sensor " + analyzerSensors.viewList[swap].topLabel.Text);
						lowHighSensors.lowArea.isManual = true; 
          	lowHighSensors.lowArea.currentSensor = null;
          	lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[swap].manualSensor;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, analyzerSensors.viewList[swap].manualSensor);
					}
					foreach (var property in holderProperties) {
						ion.currentAnalyzer.lowSideManifold.AddSensorProperty(property);
					}
					ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
					lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
					lowHighSensors.lowArea.setLHUI();

					lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lowHighSensors.lowArea);
					lowHighSensors.lowArea.subviewTable.ReloadData();
					lowHighSensors.lowArea.subviewTable.Hidden = false;

				} else {
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[start].topLabel.TextColor = UIColor.Gray;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString()) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";

            ion.currentAnalyzer.lowAccessibility = "low";
						ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);
						//REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE

						if(lowHighSensors.lowArea.attachedSensor != null){
	            for (int i = 0; i < 8; i++) {
	            	
	              if (analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.attachedSensor.currentSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;

	              } else if (analyzerSensors.viewList[i].manualSensor != null && analyzerSensors.viewList[i].manualSensor == lowHighSensors.lowArea.attachedSensor.manualSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
	              }
	              
	            }
						}
						lowHighSensors.highArea.currentSensor = lowHighSensors.lowArea.currentSensor;
						lowHighSensors.highArea.attachedSensor = null;
            lowHighSensors.lowArea.hideLHUI();                   
          }
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString()) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
            ion.currentAnalyzer.highAccessibility = "high";
						ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);

						if(lowHighSensors.highArea.attachedSensor != null){
	            for (int i = 0; i < 8; i++) {
	            	
	              if (analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.attachedSensor.currentSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;

	              } else if (analyzerSensors.viewList[i].manualSensor != null && analyzerSensors.viewList[i].manualSensor == lowHighSensors.highArea.attachedSensor.manualSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
	              }
	              
	            }
						}
						lowHighSensors.lowArea.currentSensor = lowHighSensors.highArea.currentSensor;
						lowHighSensors.lowArea.attachedSensor = null;
            lowHighSensors.highArea.hideLHUI();           
          }

          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[swap].topLabel.TextColor = UIColor.Gray;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
            ion.currentAnalyzer.lowAccessibility = "low";
						ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);

					}
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
            ion.currentAnalyzer.highAccessibility = "high";
            ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);
          }
        }
      } else {
      	Console.WriteLine("Same side sensor mount swap. Moving mount location: " + start + " swapping mount location: " + swap + " swaplocation: " + swapLocation);
				////////////////////UPDATE THE LOW HIGH CORRESPONDING BASED SAME SIDE MOVE
				if(start.ToString() == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("Low side start should now be looking at position " + swap);
					ion.currentAnalyzer.lowAccessibility = swap.ToString();
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = swap.ToString();
				} else if (start.ToString() == lowHighSensors.highArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("High side start should now be looking at position " + swap);
					ion.currentAnalyzer.highAccessibility = swap.ToString();
					lowHighSensors.highArea.snapArea.AccessibilityIdentifier = swap.ToString();
				} else if (swap.ToString() == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("Low side swap should now be looking at position " + start);
					ion.currentAnalyzer.lowAccessibility = start.ToString();
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = start.ToString();
				} else if (swap.ToString() == lowHighSensors.highArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("High side swap should now be looking at position " + start);
					ion.currentAnalyzer.highAccessibility = start.ToString();
					lowHighSensors.highArea.snapArea.AccessibilityIdentifier = start.ToString();
				}
				
				Console.WriteLine("Low side associated to slot: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " High side associated to slot: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
				
			}
    	Console.WriteLine("AnalyserUtilities sensorSwap ended. Low association: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high association: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
				
		}
    /// <summary>
    /// Checks if the sensor being moved is moving from low to high or vice versa and if it is associated to a low or high area
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="position">Position.</param>
    /// <param name="touchPoint">Touch point.</param>
    /// <param name="View">View.</param>
    public static void LHSwapCheck(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View){
    	Console.WriteLine("AnalyzerUtilities LHSwapCheck");
      int start = ion.currentAnalyzer.sensorPositions.IndexOf(position);
      int swap = 0;
      bool removeLH = false;

      ////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
      if (analyzerSensors.snapRect1.Contains (touchPoint)) {
        if (start > 3) {          
          swap = 0;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
        if (start > 3) {
          swap = 1;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
        if (start > 3) {
          swap = 2;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
        if (start > 3) {
          swap = 3;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
        if (start < 4) {
          swap = 4;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
        if (start < 4) {
          swap = 5;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
        if (start < 4) {
          swap = 6;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
        if (start < 4) {
          swap = 7;
          removeLH = true;
        }
      }
      if (removeLH) {
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("occupied low area is moving to occupied high side");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("occupied high area is moving to occupied low area");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() || lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString()) {
          Console.WriteLine("low or high area is starting a swap with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString() || lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("low or high area is a swapie with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, analyzerSensors.viewList[swap]);
        } else {
        	Console.WriteLine("In the else part of swap check because it did not find a low or high association.");
          bool foundAssociation = false;
          foreach (var item in analyzerSensors.viewList){
            if (lowHighSensors.lowArea.attachedSensor != null) {
            	Console.WriteLine("Low area attached sensor is not null for sensor " + item.topLabel.Text + " " + item.bottomLabel.Text);
              if (lowHighSensors.lowArea.attachedSensor.currentSensor != null && item.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == item.currentSensor) {
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, item);
                foundAssociation = true;
              } else if (lowHighSensors.lowArea.attachedSensor.manualSensor != null && item.manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == item.manualSensor){
								Console.WriteLine("Low area attached manual sensor is not null for sensor " + item.manualSensor.name + " " + item.manualSensor.type);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, item);
                foundAssociation = true;
              } else {
								Console.WriteLine("low area Didn't fit in any category");   
							}
            } else if( lowHighSensors.highArea.attachedSensor != null){
							if (lowHighSensors.highArea.attachedSensor.currentSensor != null && item.currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == item.currentSensor){
            		Console.WriteLine("High area attached sensor is not null for sensor " + item.topLabel.Text + " " + item.bottomLabel.Text);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, item);
                foundAssociation = true;
							} else if (lowHighSensors.highArea.attachedSensor.manualSensor != null && item.manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == item.manualSensor){
								Console.WriteLine("High area attached manual sensor is not null for sensor " + item.manualSensor.name + " " + item.manualSensor.type);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, item);
                foundAssociation = true;
							} else {
								Console.WriteLine("high area Didn't fit in any category");
							}
						}
          }
          if (foundAssociation) {
          	Console.WriteLine("Found an association to high or low");
            return;
          } else {
          	Console.WriteLine("Didn't find an association to high or low");
            sensorSwap(analyzerSensors, lowHighSensors, position, touchPoint);
          }
        }
      } else {
      	Console.WriteLine("Skipped the removeLH");
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint);
      }
    }
    /// <summary>
    /// Creates an alert to confirm a swap if there are any high low associations
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="position">Position.</param>
    /// <param name="touchPoint">Touch point.</param>
    public static void LHSwapAlert(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, sensor item = null){
    	//Console.WriteLine("AnalyzerUtilities LHSwapAlert");
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController addDeviceSheet; 

      addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.REMOVESETUP, UIAlertControllerStyle.Alert);
      addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
        Console.WriteLine("Ok for switching low high stuff");
  			/////DISASSOCIATE THE LOW OR HIGH AREA WITH A SENSOR BEING SWAPPED TO ANOTHER SIDE
        if(item != null){
        	Console.WriteLine("sent a sensor item");
        	if(item.currentSensor != null){
        		////CHECK IF SENT GAUGE SENSOR IS ASSOCIATED TO THE LOW OR HIGH AREAS
  					if(lowHighSensors.lowArea.currentSensor == item.currentSensor){
        			Console.WriteLine("low area gauge sensor matches sent gauge sensor");

  						lowHighSensors.lowArea.hideLHUI();
  					} else if (lowHighSensors.highArea.currentSensor == item.currentSensor){
        			Console.WriteLine("high area gauge sensor matches sent gauge sensor");
  						
  						lowHighSensors.highArea.hideLHUI();
  					}
        		////CHECK IF SENT SENSOR IS THE SECONDAY SENSOR FOR THE LOW OR HIGH AREA
        		if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == item.currentSensor){
        			Console.WriteLine("low area attached gauge sensor matches sent gauge sensor");
              lowHighSensors.lowArea.attachedSensor = null;        		
  					  ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(null);
  					  lowHighSensors.lowArea.updateSHSCCell();
  				  }else if (lowHighSensors.highArea.attachedSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == item.currentSensor){						
        			Console.WriteLine("low area attached gauge sensor matches sent gauge sensor");
        			lowHighSensors.highArea.attachedSensor = null;        			
  					  ion.currentAnalyzer.highSideManifold.SetSecondarySensor(null);
					    lowHighSensors.highArea.updateSHSCCell();
  				  }
  				} else {
        		////CHECK IF SENT MANUAL SENSOR IS ASSOCIATED TO THE LOW OR HIGH AREAS
  					if(lowHighSensors.lowArea.manualSensor == item.manualSensor){
        			Console.WriteLine("low area manual sensor matches sent manual sensor");
  						
  						lowHighSensors.lowArea.hideLHUI();
  					} else if (lowHighSensors.highArea.manualSensor == item.manualSensor){
        			Console.WriteLine("high area manual sensor matches sent manual sensor");

  						lowHighSensors.highArea.hideLHUI();
  					}
        		////CHECK IF SENT SENSOR IS THE SECONDAY SENSOR FOR THE LOW OR HIGH AREA
        		if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == item.manualSensor){
        			Console.WriteLine("low area attached manual sensor matches sent manual sensor");
        			lowHighSensors.lowArea.attachedSensor = null;        		
              ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(null);
  					  lowHighSensors.lowArea.updateSHSCCell();
  					}else if (lowHighSensors.highArea.attachedSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == item.manualSensor){
        			Console.WriteLine("low area attached manual sensor matches sent manual sensor");
        			lowHighSensors.highArea.attachedSensor = null;						
  				    ion.currentAnalyzer.highSideManifold.SetSecondarySensor(null);
  					  lowHighSensors.highArea.updateSHSCCell();
  			    }
  				}
        }
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint);
      }));
      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {
        confirmLayout(analyzerSensors);
      }));
      vc.DismissViewController(false, null);
      vc.PresentViewController (addDeviceSheet, true, null);
       confirmLayout(analyzerSensors);
    }
    /// <summary>
    /// Ensures the sensors are in their correct placement after swapping
    /// UIDYNAMICBEHAVIOR SUCKS
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void confirmLayout(sensorGroup analyzerSensors){
      ////MOVE SENSORS BASED ON THEIR LOCATION
      for (int i = 0; i < 8; i++) {
      	
        if (ion.currentAnalyzer.sensorPositions [i] == 1) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{
              analyzerSensors.snapArea1.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions [i] == 2) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea2.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions [i] == 3) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea3.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions[i] == 4) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea4.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions[i] == 5) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea5.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions[i] == 6) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea6.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions[i] == 7) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea7.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (ion.currentAnalyzer.sensorPositions[i] == 8) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea8.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        }
      }
    }
		/// <summary>
		/// TRIGGERS AN ALERT TO ASK IF THE USER WANTS TO REPLACE THE CURRENT LOW OR HIGH AREA INFORMATION WITH ANOTHER SENSOR'S DATA
		/// </summary>
		/// <param name="title">HEADER FOR THE ALERT</param>
		/// <param name="message">INSTRUCTIONS FOR THE ALERT</param>
		/// <param name="type">HOW THE ALERT IS BEING REPLACED 1 = OCCUPIED LOW SIDE GIVEN UNATTACHED SENSOR 2 = OCCUPIED HIGH SIDE GIVEN UNATTACHED SENSOR 3 = MOVING HIGH SENSOR TO LOW SIDE 4 = MOVING LOW SENSOR TO HIGH SIDE</param>
		/// <param name="topLabel">NEW SENSOR NAME LABEL</param>
		/// <param name="middleLabel">NEW SENSOR READING LABEL</param>
		/// <param name="bottomLabel">NEW SENSOR MEASUREMENT TYPE LABEL</param>
		/// <param name="origin">IDENTIFIER FOR FROM NEW SENSOR TO MATCH LOW/HIGH SIDE</param>
		/// <param name="removeLabel">OLD SENSOR REMOVING ITS BACKGROUND COLOR</param>
		/// <param name="tableArea">SUBVIEW TABLEVIEW TO REMOVE ELEMENTS</param>
     public static void replaceAlert(string message, int type, sensor Sensor, sensor removeSensor, lowHighSensor lowHighSensor, sensorGroup analyzerSensors, UIView View, LowHighArea lowHigh){
     	Console.WriteLine("AnalyzerUtilities replaceAlert of type " + type);
     	///Don't allow temperature sensors to be the main focus of the low/high viewer area
			if(Sensor.isManual){
				if(Sensor.manualSensor.type == ESensorType.Temperature){
					if(removeSensor.isManual){
						if(removeSensor.manualSensor.type != ESensorType.Pressure){
							return;
						}
					} else {
						if(removeSensor.currentSensor.type != ESensorType.Pressure){
							return;
						}
					}
				}
			} else {
				if(Sensor.currentSensor.type == ESensorType.Temperature){
					if(removeSensor.isManual){
						if(removeSensor.manualSensor.type != ESensorType.Pressure){
							return;
						}
					} else {
						if(removeSensor.currentSensor.type != ESensorType.Pressure){
							return;
						}
					}
				}
			}
			
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      if (Sensor.isManual.Equals(true) || removeSensor.isManual.Equals(true)) {
      	//Console.WriteLine("One of the sensors is manual");
        bool spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);

        if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.manualSensor.type == removeSensor.manualSensor.type) {
            spotOpen = true;
          } else {
            if (Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum) {              
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.manualSensor.type.ToString(),removeSensor.manualSensor.type.ToString());
            }
          }
        } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
          if(Sensor.manualSensor.type == removeSensor.currentSensor.type){
            spotOpen = true;
          }else{
            if(Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.currentSensor.type == ESensorType.Vacuum){
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.manualSensor.type.ToString(),removeSensor.currentSensor.type.ToString());
            }
          }
        } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.currentSensor.type == removeSensor.manualSensor.type) {  
            spotOpen = true;
          } else {
            if(Sensor.currentSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum){
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.currentSensor.type.ToString(),removeSensor.manualSensor.type.ToString());
            }
          }
        }

        if (spotOpen.Equals(false)) {
        	//Console.WriteLine("First same side");
          UIAlertController noneAvailable;
          noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
          noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
          vc.PresentViewController(noneAvailable, true, null);
          return;
        }
        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
 
          if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("Both sensors are manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              //Console.WriteLine("low high sensor is pressure and adding sensor is temperature");
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;

            	lowHighSensor.attachedSensor = Sensor;
    					if (type == 1 || type == 3) {
    						ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					} else {
    						ion.currentAnalyzer.highSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					}

            } else {
              //Console.WriteLine("dealing with a vacuum sensor");
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
            Console.WriteLine("low high sensor is manual and adding sensor is not");
            if (removeSensor.currentSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              lowHighSensor.attachedSensor = Sensor;
    					if (type == 1 || type == 3) {
    						ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					} else {
    						ion.currentAnalyzer.highSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					}
            }else {
            	//Console.WriteLine("calling the replace methods");		
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("low high sensor is not manual and adding sensor is manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.currentSensor.type == ESensorType.Temperature) {
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              lowHighSensor.attachedSensor = Sensor;
    					if (type == 1 || type == 3) {
    						ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					} else {
    						ion.currentAnalyzer.highSideManifold.SetSecondarySensor(Sensor.currentSensor);
    					}
            } else {
            	//Console.WriteLine("calling the replace methods");		
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          }
        }));
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController(addDeviceSheet, true, null);
      } else {
				//Console.WriteLine("neither of the sensors are manual");
        if ( removeSensor.currentSensor != null && removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
          var spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);
          if (spotOpen.Equals(false)) {
            UIAlertController noneAvailable;
            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
            vc.PresentViewController(noneAvailable, true, null);
            return;
          }
          message = Util.Strings.Analyzer.ADDTEMP;
        }

        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
        ///REMOVE THE ATTACHED SENSORS
				removeAttachedSensor(lowHighSensor, analyzerSensors);
					
          if (removeSensor.currentSensor != null && removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
            //Console.WriteLine("Adding temp device " + Sensor.currentSensor.device.name + "'s sensor as device " + removeSensor.currentSensor.device.name + "'s secondary sensor");
            Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
            Sensor.topLabel.TextColor = UIColor.White;
						Console.WriteLine("Setting attached sensor for " + lowHighSensor.LabelTop.Text);
            lowHighSensor.attachedSensor = Sensor;
            if(type == 1 || type == 3){
              ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(Sensor.currentSensor);
            } else{
    					ion.currentAnalyzer.highSideManifold.SetSecondarySensor(Sensor.currentSensor);
    				}
              
          } else {
            if (type == 1) {         
                replaceLowUnattached(Sensor,removeSensor,lowHighSensor,analyzerSensors,View);            
            } else if (type == 2) {
                replaceHighUnattached(Sensor,removeSensor,lowHighSensor,analyzerSensors,View);           
            } else if (type == 3) {
                replaceLowAttached(Sensor,removeSensor,lowHighSensor, lowHigh.highArea, analyzerSensors,View);           
            } else if (type == 4) {
                replaceHighAttached(Sensor,removeSensor,lowHighSensor, lowHigh.lowArea, analyzerSensors,View);
            }
          }
          Console.WriteLine("Set secondary Sensor");
        }));

        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController(addDeviceSheet, true, null);
      }
		}
	/// <summary>
    /// SETS THE LOW OR HIGH AREAS TO MONITOR THE SENSOR THAT WAS DROPPED ON IT
  /// </summary>
  /// <param name="touchPoint">Location on the screen where single sensor was dropped</param>
  /// <param name="Sensor">Single sensor used to update low high sensor</param>
  /// <param name="lowHighSensors">Low high sensors being updated</param>
  /// <param name="analyzerSensors">Collection of single sensor information</param>
  /// <param name="View">View.</param>
  /// HOW THE ALERT IS BEING REPLACED 1 = OCCUPIED LOW SIDE GIVEN UNATTACHED SENSOR 2 = OCCUPIED HIGH SIDE GIVEN UNATTACHED SENSOR 3 = MOVING HIGH SENSOR TO LOW SIDE 4 = MOVING LOW SENSOR TO HIGH SIDE</param>
    public static void updateLowHighArea(CGPoint touchPoint, sensor Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors,UIView View){
    	Console.WriteLine("AnalyzerUtilities updateLowHighArea. Low area: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high area: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }
      if( lowHighSensors.lowArea.areaRect.Contains(touchPoint)){
      	Console.WriteLine("Dragged to low area. Low area accessibility identifier: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high area accessiblity identifier " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList.IndexOf(Sensor).ToString()) {
        	Console.WriteLine("high area was originally attached to this sensor");
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;

  					if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[0], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[1], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[2], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[3], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[4], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[5], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[6], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[7], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "low") {
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.HIGHLOST, UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
              	Console.WriteLine("Changed ok");
                var goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low");
                if (goOn) {
				          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
                
			            if(Sensor.currentSensor != null){
			            	Sensor.currentSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
			            	lowHighSensors.lowArea.manifold = new Manifold(Sensor.currentSensor);
	                  ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low,Sensor.currentSensor);
			            } else {
			            	Sensor.manualSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.lowArea.isManual = true;
			            	lowHighSensors.lowArea.manualGType = Sensor.topLabel.Text;
			            	lowHighSensors.lowArea.manualSensor = Sensor.manualSensor;
      						  ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, Sensor.manualSensor);
									}

      					  ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
      					  lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
			               
									ion.currentAnalyzer.highSideManifold.sensorProperties.Clear();

					        lowHighSensors.lowArea.setLHUI();

      					  ///////////////////// change the high manifold to be based on moved location instead of created position
      					  Console.WriteLine("updateLowHighArea removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
      				          
				          ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();
      					  /////////////////////                     
      					  if (lowHighSensors.lowArea.subviewTable.Source == null) {
      						  lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lowHighSensors.lowArea);
      					  }
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
                  ion.currentAnalyzer.highAccessibility = "high";
                  ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);

                  Sensor.topLabel.BackgroundColor = UIColor.Blue;
                  Sensor.topLabel.TextColor = UIColor.White;								

                  lowHighSensors.highArea.hideLHUI();
                }
              }));            
              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);
            }
          }
				} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low") {
					Console.WriteLine("low area was not empty ");
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            
            var lowStart = Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
            
            if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0" && analyzerSensors.viewList.IndexOf(Sensor) != 0) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1" && analyzerSensors.viewList.IndexOf(Sensor) != 1) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2" && analyzerSensors.viewList.IndexOf(Sensor) != 2) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3" && analyzerSensors.viewList.IndexOf(Sensor) != 3) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4" && analyzerSensors.viewList.IndexOf(Sensor) != 4) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5" && analyzerSensors.viewList.IndexOf(Sensor) != 5) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6" && analyzerSensors.viewList.IndexOf(Sensor) != 6) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7" && analyzerSensors.viewList.IndexOf(Sensor) != 7) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            }
          }

				} else {
					Console.WriteLine("Low side was empty and moving sensor was not attached to the high area");
          if(Sensor.isManual){
						if(Sensor.manualSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE,UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					} else {
						if(Sensor.currentSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					}
					bool goOn = orderSensors (analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "low");
					if (goOn) {
	          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
	          
            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.topLabel.TextColor = UIColor.White;

            if(Sensor.currentSensor != null){
            	Sensor.currentSensor.analyzerSlot = areaIndex;
            	lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
              ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low,Sensor.currentSensor);
            } else {
            	Sensor.manualSensor.analyzerSlot = areaIndex;
            	lowHighSensors.lowArea.isManual = true;
            	lowHighSensors.lowArea.manualGType = Sensor.manualSensor.type.ToString();
            	lowHighSensors.lowArea.manualSensor = Sensor.manualSensor;
							ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, Sensor.manualSensor);
						}

						ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
						lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;
						lowHighSensors.lowArea.setLHUI();

						///////////////////// change the low manifold to be based on moved location instead of created position
						Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
	          ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();
            /////////////////////
            if (lowHighSensors.lowArea.subviewTable.Source == null){
              lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lowHighSensors.lowArea);
            }
            return;
					}

          UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}

			} else if (lowHighSensors.highArea.areaRect.Contains (touchPoint)){
				Console.WriteLine("Dragged to high area. Low area: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier +  " high area: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList.IndexOf(Sensor).ToString()) {
        	Console.WriteLine("sensor was associated to the low area");
          if(!freeSpot(analyzerSensors,Sensor,"high")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[0], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[1], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[2], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[3], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[4], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[5], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[6], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[7], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "high") {
            	Console.WriteLine("High side was empty");
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.LOWLOST    , UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
    				  bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high");
				      if (goOn) {
				          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
                
			            if(Sensor.currentSensor != null){
      						  Sensor.currentSensor.analyzerSlot = areaIndex;
      						  lowHighSensors.highArea.isManual = false;
			            	lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
      						  ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.currentSensor);

			            } else {
			            	Sensor.manualSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.highArea.isManual = true;
			            	lowHighSensors.highArea.manualGType = Sensor.manualSensor.type.ToString();
			            	lowHighSensors.highArea.manualSensor = Sensor.manualSensor;
      						  ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.manualSensor);

									}

      					  ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
      					  lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
      					  ion.currentAnalyzer.lowSideManifold.sensorProperties.Clear();

					        lowHighSensors.highArea.setLHUI();

      					  ///////////////////// change the high manifold to be based on moved location instead of created position
      					  Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
				          ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();
      					  /////////////////////                     
      					  if (lowHighSensors.highArea.subviewTable.Source == null) {
      						  lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lowHighSensors.highArea);
      					  }
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
                  ion.currentAnalyzer.lowAccessibility = "low";
					        ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);

					        Sensor.topLabel.BackgroundColor = UIColor.Red;
                  Sensor.topLabel.TextColor = UIColor.White;
               	  
									lowHighSensors.lowArea.hideLHUI();                  
                }
              }));

              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);

            } 
          }
				} else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
					Console.WriteLine("High area was not empty and has a sensor attached");
          if (!freeSpot(analyzerSensors,Sensor, "high")) {
            UIAlertController fullPopup = UIAlertController.Create(Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
            }));

            vc.PresentViewController(fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;

            var highStart = Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier);

            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0" && analyzerSensors.viewList.IndexOf(Sensor) != 0) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1" && analyzerSensors.viewList.IndexOf(Sensor) != 1) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2" && analyzerSensors.viewList.IndexOf(Sensor) != 2) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3" && analyzerSensors.viewList.IndexOf(Sensor) != 3) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4" && analyzerSensors.viewList.IndexOf(Sensor) != 4) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5" && analyzerSensors.viewList.IndexOf(Sensor) != 5) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6" && analyzerSensors.viewList.IndexOf(Sensor) != 6) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7" && analyzerSensors.viewList.IndexOf(Sensor) != 7) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } 
          }
				} else {
					Console.WriteLine("High area is empty and sensor was not attached to low area");
          if(Sensor.isManual){
						if(Sensor.manualSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					} else {
						if(Sensor.currentSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					}
					bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high");
					if (goOn) {
	          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
	          
            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.topLabel.TextColor = UIColor.White;
            
             if(Sensor.currentSensor != null){
             	Sensor.currentSensor.analyzerSlot = areaIndex;
             	lowHighSensors.highArea.isManual = false;
            	lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
							ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.currentSensor);
            } else {
             	Sensor.manualSensor.analyzerSlot = areaIndex;
            	lowHighSensors.highArea.isManual = true;
            	lowHighSensors.highArea.manualGType = Sensor.manualSensor.type.ToString();
            	lowHighSensors.highArea.manualSensor = Sensor.manualSensor;
            	ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.manualSensor);
						}

						ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
						lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;
						lowHighSensors.highArea.setLHUI();

						///////////////////// change the high manifold to be based on moved location instead of created position
						Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
	          ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();
						/////////////////////
						if (lowHighSensors.highArea.subviewTable.Source == null) {
							lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lowHighSensors.highArea);
						}
            return;
					}

          UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}
			}
		}

		public static bool orderSensors(sensorGroup analyzerSensors, int viewLocation ,string side){
			bool available = false;
			
			if (side == "low") {
				if (viewLocation != 0 && viewLocation != 1 && viewLocation != 2 && viewLocation != 3) {
					if (!analyzerSensors.viewList [0].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[0];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
		       	ion.currentAnalyzer.sensorPositions[0] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [1].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[1];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
						ion.currentAnalyzer.sensorPositions[1] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [2].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[2];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
						ion.currentAnalyzer.sensorPositions[2] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [3].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[3];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
						ion.currentAnalyzer.sensorPositions[3] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
					} 
				} else {

					available = true;
				}
			} else if (side == "high") {
				if (viewLocation != 4 && viewLocation != 5 && viewLocation != 6 && viewLocation != 7) {
          if (!analyzerSensors.viewList [4].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[4];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
						ion.currentAnalyzer.sensorPositions[4] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;						
						available = true;
          } else if (!analyzerSensors.viewList [5].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[5];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
		       	ion.currentAnalyzer.sensorPositions[5] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [6].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[6];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
		       	ion.currentAnalyzer.sensorPositions[6] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [7].availableView.Hidden) {
						int end = ion.currentAnalyzer.sensorPositions[7];
						int start = ion.currentAnalyzer.sensorPositions[viewLocation];
		       	ion.currentAnalyzer.sensorPositions[7] = start;
		        ion.currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
					}
				} else {
					available = true;
				}
			}
      confirmLayout(analyzerSensors);

			////ARRANGE SENSOR LIST BASED ON THEIR SNAP POINT ASSOCIATIONS
			analyzerSensors.viewList = new List<sensor> ();
			Array.Clear(ion.currentAnalyzer.sensors, 0, 8);
			for(int i = 0; i < ion.currentAnalyzer.sensorPositions.Count; i++) {
				if (ion.currentAnalyzer.sensorPositions[i] == 1) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea1);
          ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea1.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 2) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea2);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea2.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 3) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea3);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea3.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 4) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea4);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea4.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 5) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea5);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea5.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 6) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea6);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea6.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 7) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea7);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea7.currentSensor;
				} else if (ion.currentAnalyzer.sensorPositions[i] == 8) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea8);
					ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea8.currentSensor;
				}
			}
			return available;
		}
    /// <summary>
    /// Checks if a sensor is on the correct side of the analyzer before adding it as a secondary sensor to a high or low area
    /// </summary>
    /// <returns><c>true</c>, if sensor is on the same side as the low/high addition, <c>false</c> otherwise.</returns>
    /// <param name="Sensor">The sensor being added as a secondary sensor to the existing sensor</param>
    /// <param name="existingSensor">The sensor being added to</param>
    /// <param name="analyzerSensors">holds the positions of all the sensors</param>
    public static bool secondarySlotSpot(sensor Sensor, sensor existingSensor, sensorGroup analyzerSensors, int type){
      bool available = false;
      if (type == 1 || type == 3) {
        for (int i = 0; i < 4; i++) {
          if (analyzerSensors.viewList[i] == Sensor) {
            available = true;
            break;
          }
        }
      } else {
        for (int i = 4; i < 8; i++) {
          if (analyzerSensors.viewList[i] == Sensor) {
            available = true;
            break;
          }
        }
      }
      return available;
    }
 
    /// <summary>
    /// checks if the low or high side has a free area spot
    /// </summary>
    /// <returns><c>true</c>, if spot was free, <c>false</c> otherwise.</returns>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="identifier">Identifier.</param>
    public static bool freeSpot(sensorGroup analyzerSensors, sensor Sensor, string identifier){      
      if (identifier == "low") {
        for (int i = 0; i < 4; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden || analyzerSensors.viewList.LastIndexOf(Sensor) < 4) {
            return true;
          }
        }
      } else if (identifier == "high") {
        for (int i = 4; i < 8; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden || analyzerSensors.viewList.LastIndexOf(Sensor) > 3) {
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// The low area has a sensor association already and is swapping with another sensor without a high area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceLowUnattached(sensor Sensor, sensor removeSensor, lowHighSensor lhSensor, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceLowUnattached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
			bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low");
			if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
   
        ion.currentAnalyzer.lowSideManifold.sensorProperties.Clear();
        lhSensor.subviewTable.ReloadData();

				var holdFluidState = lhSensor.manifold.ptChart.state;
				if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = true;
 					lhSensor.manualGType = Sensor.manualSensor.type.ToString();
        	lhSensor.currentSensor = null;        	
					lhSensor.manualSensor = Sensor.manualSensor;
          ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low,Sensor.manualSensor);
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = false;
        	lhSensor.manualSensor = null;
					lhSensor.currentSensor = Sensor.currentSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, Sensor.currentSensor);
				}
				ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, holdFluidState);
				lhSensor.manifold = ion.currentAnalyzer.lowSideManifold;

				lhSensor.setLHUI();				
				
        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
        lhSensor.snapArea.AccessibilityIdentifier = areaIndex.ToString();
				///////////////////// 
				if (lhSensor.subviewTable.Source == null) {
					lhSensor.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lhSensor);
				}      
        Console.WriteLine("Occupied Low side given unattached sensor with identifier " + areaIndex);
        //Console.WriteLine("The high side is currently identified with sensor " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      }
    }
    /// <summary>
    /// The high area has a sensor association already and is swapping with another sensor without a low area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceHighUnattached(sensor Sensor, sensor removeSensor, lowHighSensor lhSensor, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceHighUnattached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
			bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high");
			if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
 
      	ion.currentAnalyzer.highSideManifold.sensorProperties.Clear();
      	lhSensor.subviewTable.ReloadData();
				var holdFluidState = lhSensor.manifold.ptChart.state;

				if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = true;
 					lhSensor.manualGType = Sensor.manualSensor.type.ToString();
        	lhSensor.currentSensor = null;
					lhSensor.manualSensor = Sensor.manualSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.manualSensor);
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = false;
        	lhSensor.manualSensor = null;
					lhSensor.currentSensor = Sensor.currentSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.currentSensor);
				}

				ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, holdFluidState);
				lhSensor.manifold = ion.currentAnalyzer.highSideManifold;
				lhSensor.setLHUI();

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the high manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
        lhSensor.snapArea.AccessibilityIdentifier = areaIndex.ToString();
				/////////////////////
				if (lhSensor.subviewTable.Source == null) {
					lhSensor.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lhSensor);
				}
        Console.WriteLine("Occupied High side given unattached sensor with identifier " + areaIndex + ":" + lhSensor.snapArea.AccessibilityIdentifier);
        //Console.WriteLine("The low side is currently identified with sensor " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      }
    }
    /// <summary>
    /// The low area has a sensor association already and is swapping with another sensor with a high area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceLowAttached(sensor Sensor, sensor removeSensor, lowHighSensor lhAdd, lowHighSensor lhRemove, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceLowAttached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
			bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low");
			if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
				
				ion.currentAnalyzer.lowSideManifold.sensorProperties.Clear();
				lhAdd.subviewTable.ReloadData();
				var holdFluidState = lhAdd.manifold.ptChart.state;

				if(Sensor.isManual){
					Sensor.manualSensor.analyzerSlot = areaIndex;       	
					lhAdd.isManual = true;
					lhAdd.manualGType = Sensor.manualSensor.type.ToString();     	
					lhAdd.manualSensor = Sensor.manualSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, Sensor.manualSensor);
				} else {
					Sensor.currentSensor.analyzerSlot = areaIndex;       	
					lhAdd.isManual = false;
					lhAdd.currentSensor = Sensor.currentSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, Sensor.currentSensor);
				}

				ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, holdFluidState);
				lhAdd.manifold = ion.currentAnalyzer.lowSideManifold;
				lhAdd.setLHUI();				

        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
        lhAdd.snapArea.AccessibilityIdentifier = areaIndex.ToString();
				/////////////////////        
				if (lhAdd.subviewTable.Source == null) {
					lhAdd.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.lowSideManifold, lhAdd);
				}
        lhRemove.snapArea.AccessibilityIdentifier = "high";
        ion.currentAnalyzer.highAccessibility = "high";
				ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.highSideManifold);

				lhRemove.currentSensor = null;
				lhRemove.manualSensor = null;
        lhRemove.hideLHUI();

 				/////////////////////////////////////////////////////////
 				
        Console.WriteLine("Occupied Low side given High side sensor with identifier " + areaIndex + ":" + lhAdd.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The high side is currently identified with sensor " + lhRemove.snapArea.AccessibilityIdentifier);
      } 
    }
    /// <summary>
    /// The high area has a sensor association already and is swapping with another sensor with a low area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceHighAttached(sensor Sensor, sensor removeSensor, lowHighSensor lhAdd, lowHighSensor lhRemove, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceHighAttached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
			bool goOn = orderSensors(analyzerSensors, ion.currentAnalyzer.sensorPositions.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high");
			if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
				
				ion.currentAnalyzer.highSideManifold.sensorProperties.Clear();
				lhAdd.subviewTable.ReloadData();
				var holdFluidState = lhAdd.manifold.ptChart.state;

				if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
					lhAdd.isManual = true;     	
					lhAdd.manualSensor = Sensor.manualSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.manualSensor);
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
					lhAdd.isManual = false;     	
					lhAdd.currentSensor = Sensor.currentSensor;
					ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High, Sensor.currentSensor);
				}

				ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, holdFluidState);
				lhAdd.manifold = ion.currentAnalyzer.highSideManifold;
				lhAdd.setLHUI();				

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
        lhAdd.snapArea.AccessibilityIdentifier = areaIndex.ToString();
				/////////////////////        
				if (lhAdd.subviewTable.Source == null) {
					lhAdd.subviewTable.Source = new AnalyzerTableSource(ion.currentAnalyzer.highSideManifold, lhAdd);
				}
				lhRemove.snapArea.AccessibilityIdentifier = "low";
        ion.currentAnalyzer.lowAccessibility = "low";
				ion.currentAnalyzer.RemoveManifold(ion.currentAnalyzer.lowSideManifold);

				////////////////////CLEAR OUT THE LOW SIDE MANIFOLD
				lhRemove.currentSensor = null;
				lhRemove.manualSensor = null;
				lhRemove.hideLHUI();

				Console.WriteLine("Occupied High side given Low side sensor with identifier " + areaIndex + ":" + lhAdd.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The Low side is currently identified with sensor " + lhRemove.snapArea.AccessibilityIdentifier);
      }
    }

    public static void alarmRequestViewer(sensor area) {
      var alarm = Analyzer.AnalyzerViewController.arvc.InflateViewController<SensorAlarmViewController>("viewControllerSensorAlarms");
      alarm.sensor = area.currentSensor;
      Analyzer.AnalyzerViewController.arvc.NavigationController.PushViewController(alarm, true);
    }

    /// <summary>
    /// Shows the popup to rename a sensor
    /// </summary>
    public static void renamePopup(sensor Sensor, lowHighSensor lhArea){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController textAlert = UIAlertController.Create (Util.Strings.Analyzer.ENTERNAME, Sensor.topLabel.Text, UIAlertControllerStyle.Alert);
      textAlert.AddTextField(textField => {});
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
        Sensor.topLabel.Text = " " + textAlert.TextFields[0].Text;
        
				lhArea.LabelTop.Text = " " + textAlert.TextFields[0].Text;
				lhArea.LabelSubview.Text = " " + lhArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
					
        if(Sensor.currentSensor != null){
          AppState.context.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,Sensor.currentSensor.device.serialNumber.ToString());
          AppState.context.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,Sensor.currentSensor.device.serialNumber.ToString());
        } 
      }));
      vc.PresentViewController(textAlert, true, null);
    }

		
		public static void arrangeViews(sensorGroup analyzerSensors){
			////ARRANGE SENSOR LIST BASED ON THEIR SNAP POINT ASSOCIATIONS
			analyzerSensors.viewList = new List<sensor> ();
      Array.Clear(ion.currentAnalyzer.sensors,0,8);
			for(int i = 0; i < ion.currentAnalyzer.sensorPositions.Count; i++) {
				if (ion.currentAnalyzer.sensorPositions[i] == 1) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea1);
					if(analyzerSensors.snapArea1.currentSensor != null){
						analyzerSensors.snapArea1.currentSensor.analyzerSlot = i;
            ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea1.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 2) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea2);
					if(analyzerSensors.snapArea2.currentSensor != null){
						analyzerSensors.snapArea2.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea2.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 3) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea3);
					if(analyzerSensors.snapArea3.currentSensor != null){
						analyzerSensors.snapArea3.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea3.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 4) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea4);
					if(analyzerSensors.snapArea4.currentSensor != null){
						analyzerSensors.snapArea4.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea4.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 5) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea5);
					if(analyzerSensors.snapArea5.currentSensor != null){
						analyzerSensors.snapArea5.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea5.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 6) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea6);
					if(analyzerSensors.snapArea6.currentSensor != null){
						analyzerSensors.snapArea6.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea6.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 7) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea7);
					if(analyzerSensors.snapArea7.currentSensor != null){
						analyzerSensors.snapArea7.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea7.currentSensor;
					}
				} else if (ion.currentAnalyzer.sensorPositions[i] == 8) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea8);
					if(analyzerSensors.snapArea8.currentSensor != null){
						analyzerSensors.snapArea8.currentSensor.analyzerSlot = i;
						ion.currentAnalyzer.sensors[i] = analyzerSensors.snapArea8.currentSensor;
					}
				}
			}
		}		
		
		public static void removeAttachedSensor(lowHighSensor lowHigh, sensorGroup analyzerSensors){
			if(lowHigh.attachedSensor == null){
				return;
			} else {
        for(int i = 0; i < 8; i++){
					if(analyzerSensors.viewList[i].currentSensor != null && lowHigh.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor){
						analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
						analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
						lowHigh.attachedSensor = null;
						break;
					}
				}
			}
		}
		public static Unit getManualUnit(ESensorType sensorType, string code) {
			switch (sensorType) {
				case ESensorType.Humidity:
				return Units.Humidity.RELATIVE_HUMIDITY;
				case ESensorType.Length:
				return Units.Length.FOOT;
				case ESensorType.Weight:
				return Units.Weight.KILOGRAM;
				case ESensorType.Pressure:
				switch (code) {
					case "pa":
					return Units.Pressure.PASCAL;
					case "kpa":
					return Units.Pressure.KILOPASCAL;
					case "mpa":
					return Units.Pressure.MEGAPASCAL;
					case "bar":
					return Units.Pressure.BAR;
					case "millibar":
					return Units.Pressure.MILLIBAR;
					case "atmo":
					return Units.Pressure.ATMOSPHERE;
					case "inhg":
					return Units.Pressure.IN_HG;
					case "cmhg":
					return Units.Pressure.CM_HG;
					case "kg/cm2":
					return Units.Pressure.KG_CM;
					case "psia":
					return Units.Pressure.PSIA;
					case "psig":
					return Units.Pressure.PSIG;
					default:
					return Units.Pressure.PSIG;
				}
				case ESensorType.Temperature:
				switch (code) {
					case "ºc":
					return Units.Temperature.CELSIUS;
					case "ºf":
					return Units.Temperature.FAHRENHEIT;
					case "ºk":
					return Units.Temperature.KELVIN;
					default:
					return Units.Temperature.FAHRENHEIT;
				}
				case ESensorType.Vacuum:
				switch (code) {
					case "pa":
					return Units.Vacuum.PASCAL;
					case "kpa":
					return Units.Vacuum.KILOPASCAL;
					case "mbar":
					return Units.Vacuum.BAR;
					case "millibar":
					return Units.Vacuum.MILLIBAR;
					case "atmo":
					return Units.Vacuum.ATMOSPHERE;
					case "inhg":
					return Units.Vacuum.IN_HG;
					case "cmhg":
					return Units.Vacuum.CM_HG;
					case "kgcm":
					return Units.Vacuum.KG_CM;
					case "psia":
					return Units.Vacuum.PSIA;
					case "mtorr":
					return Units.Vacuum.TORR;
					case "millitorr":
					return Units.Vacuum.MILLITORR;
					case "micron":
					return Units.Vacuum.MICRON;
					default:
					return Units.Vacuum.MICRON;
				}
				default:
				return Units.Pressure.PSIG;
			}
		}
	}
}

