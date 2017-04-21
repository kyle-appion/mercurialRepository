﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using CoreGraphics;

using Appion.Commons.Measure;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.Location;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;

using ION.IOS.Util;
using ION.IOS.ViewController.FluidManager;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.PressureTemperatureChart;
using ION.IOS.App;

namespace ION.IOS.ViewController.Analyzer
{
	public class lowHighSensor
	{
    public IosION ion { get; set; }
    public nfloat cellHeight;
    public UILabel maxReading;
    public double max;
    public string maxType;
    public UILabel minReading;
    public double min;
    public string minType;
    public string manualGType;
    public UILabel holdReading;
    public double holdValue;
    public string holdType; 
    public UILabel shReading;
    public UILabel shFluidType;
    public UILabel shFluidState;
    public UIButton changeFluid;
    public UILabel ptReading;
    public UILabel ptFluidState;
    public double ptAmount = 0;
    public UILabel ptFluidType;
    public UIButton changePTFluid;
    public UILabel altReading;
    public Unit altUnit;
    public UILabel rocReading;
    public UIImageView rocImage;
    public UILabel secondaryReading;
    public UIView snapArea;
    public UIView subviewDivider;
    public UIView headingDivider;
    public UIView connectionColor;
    public UITapGestureRecognizer shortPress;
    public CGRect areaRect;
    public UILabel LabelTop;
    public UILabel LabelMiddle;
    public UILabel LabelBottom;
    public UILabel LabelSubview;
    public UIButton subviewHide;
    public UITableView subviewTable;
    public UIImageView Connection;
    public UIImageView DeviceImage;
    public UIButton conDisButton;
    private AnalyzerViewController __analyzerviewcontroller;
    public Unit tUnit;
    public Unit pUnit;
    public UIActivityIndicatorView activityConnectStatus;
    public List<string> tableSubviews = new List<string>();
    public List<string> altUnits = new List<string>{"kg/cm","inHg","psig","cmHg","bar","kPa","mPa"};
    public List<string> tempUnits = new List<string>{"celsius","fahrenheit","kelvin"};
    public List<string> vacUnits = new List<string>{ "pa", "kpa","bar", "millibar","atmo", "inhg", "cmhg", "kg/cm","psia", "torr","millitorr", "micron",};
    public List<string> availableSubviews = new List<string> {
      "Linked Sensor (Linked)","Pressure / Temperature (P/T)","Superheat / Subcool (S/H or S/C)", "Minimum Reading (MIN)","Maximum Reading (MAX)", "Hold Reading (HOLD)",  "Rate of Change (RoC)", "Alternate Unit(ALT)" 
    };
    private bool isUpdating { get; set; }
    public bool isManual;
    public bool isLinked;
    public string location;
    private RateOfChangeSensorProperty roc;
    public AlternateUnitSensorProperty alt;
    public List<sensor> sensorList;
    List<int> locationList;
    public LowHighArea lharea;
		public sensor attachedSensor{
      get { return __attachedSensor;}
      set { 
						__attachedSensor = value;						
					}
    } sensor __attachedSensor;
    
    public GaugeDeviceSensor currentSensor{

      get { return __currentSensor; }
      set {if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent -= gaugeUpdating;
          roc.onSensorPropertyChanged -= DoUpdateRocCell;
          roc = null;
        }
        __currentSensor = value;
        if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent += gaugeUpdating;
          roc = new RateOfChangeSensorProperty(value);
        }
      }
    } GaugeDeviceSensor __currentSensor;

    public ManualSensor manualSensor{
      get { return __manualSensor; }
      set {
        __manualSensor = value;  
      }
    } ManualSensor __manualSensor;

    public Manifold manifold{

      get { return __manifold;}
      set { if (__manifold != null) {
          __manifold.onManifoldEvent -= manifoldUpdating;
        }
        __manifold = value;
        if (__manifold != null) {
          __manifold.onManifoldEvent += manifoldUpdating;
        }
      }
    } Manifold __manifold;

		public lowHighSensor (CGRect areaRect, CGRect tblRect, AnalyzerViewController ViewController, List<sensor> viewList, List<int> areaList = null)
		{
			sensorList = viewList;
			locationList = areaList;
			snapArea = new UIView (areaRect);
			this.areaRect = areaRect;
      cellHeight = .521f * snapArea.Bounds.Height;
      subviewTable = new UITableView (tblRect);
      subviewTable.Bounces = false;
      
      LabelTop = new UILabel (new CGRect(0,0, .859 * areaRect.Width, .217 * areaRect.Height));
      LabelMiddle = new UILabel (new CGRect(.214 * areaRect.Width, .217 * areaRect.Height, .686 * areaRect.Width, .347 * areaRect.Height));
      LabelBottom = new UILabel (new CGRect(0, .565 * areaRect.Height, areaRect.Width, .217 * areaRect.Height));
      LabelSubview = new UILabel (new CGRect(0, .8 * areaRect.Height, .8 * snapArea.Bounds.Width, .204 * areaRect.Height));
      LabelSubview.ClipsToBounds = true;
      subviewHide = new UIButton(new CGRect(.791 * snapArea.Bounds.Width, .8 * areaRect.Height, .209 * snapArea.Bounds.Width, .203 * areaRect.Height));
      subviewHide.ClipsToBounds = true;
      subviewDivider = new UIView(new CGRect(0,.8 * areaRect.Height,areaRect.Width,2));
      headingDivider = new UIView(new CGRect(.033 * areaRect.Width,.234 * areaRect.Height,.932 * areaRect.Width,1));
      connectionColor = new UIView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height, .14 * areaRect.Width,.217 * areaRect.Height));
      connectionColor.BackgroundColor = UIColor.Red;
      Connection = new UIImageView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height,  .14 * areaRect.Width,.217 * areaRect.Height));
      Connection.BackgroundColor = UIColor.Clear;
      activityConnectStatus = new UIActivityIndicatorView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height,  .14 * areaRect.Width,.217 * areaRect.Height));
      activityConnectStatus.Hidden = true;
      conDisButton = new UIButton(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height, .14 * areaRect.Width,.217 * areaRect.Height));
      conDisButton.BackgroundColor = UIColor.Clear;
      DeviceImage = new UIImageView(new CGRect(0, .3 * areaRect.Height, .214 * areaRect.Width,.214 * areaRect.Width));
      subviewTable.RegisterClassForCellReuse(typeof(maximumTableCell),"Maximum");
      subviewTable.RegisterClassForCellReuse(typeof(minimumTableCell),"Minimum");
      subviewTable.RegisterClassForCellReuse(typeof(holdTableCell), "Hold");
      subviewTable.RegisterClassForCellReuse(typeof(altTableCell), "Alternate"); 
      subviewTable.RegisterClassForCellReuse(typeof(RoCTableCell), "Rate");
      subviewTable.RegisterClassForCellReuse(typeof(SHSCTableCell), "Superheat");
      subviewTable.RegisterClassForCellReuse(typeof(PTTableCell), "Pressure");
      subviewTable.RegisterClassForCellReuse(typeof(secondarySensorCell), "Linked");
      maxReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .79 * tblRect.Width, .5 * cellHeight));
      minReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .79 * tblRect.Width, .5 * cellHeight));
      holdReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .79 * tblRect.Width, .5 * cellHeight));
      shReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      shFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      shFluidType.Layer.BorderColor = UIColor.Black.CGColor;
      shFluidType.Layer.BorderWidth = 1f;
      shFluidState = new UILabel(new CGRect(0,0, 1.006 * tblRect.Width, .5 * cellHeight));
      shFluidState.TextAlignment = UITextAlignment.Center;
      shFluidState.TextColor = UIColor.White;
      shFluidState.BackgroundColor = UIColor.Black;
      ptFluidState = new UILabel(new CGRect(0, 0, 1.006 * tblRect.Width, .5 * cellHeight));
      ptFluidState.TextAlignment = UITextAlignment.Center;
      ptFluidState.TextColor = UIColor.White;
      ptFluidState.BackgroundColor = UIColor.Black;
      ptFluidState.Text = "PTDew";
      changeFluid = new UIButton(new CGRect(0, .5 * cellHeight,tblRect.Width, .5 * cellHeight));
      changeFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changeFluid.Layer.BorderWidth = 1f;
      changeFluid.BackgroundColor = UIColor.Clear;
      ptReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      ptFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changePTFluid = new UIButton(new CGRect(0, .5 * cellHeight, tblRect.Width, .5 * cellHeight));
      changePTFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changePTFluid.Layer.BorderWidth = 1f;
      changePTFluid.BackgroundColor = UIColor.Clear;
      altReading = new UILabel(new CGRect(0, .5 * cellHeight, .99 * tblRect.Width, .5 * cellHeight));
      rocReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .79 * tblRect.Width, .5 * cellHeight));
      rocImage = new UIImageView(new CGRect(0, .5 * cellHeight, .2 * tblRect.Width, .5 * cellHeight));
      secondaryReading = new UILabel(new CGRect(0, .5 * cellHeight, tblRect.Width, .5 * cellHeight));
      ion = AppState.context as IosION;
      __analyzerviewcontroller = ViewController;
      tUnit = Units.Temperature.FAHRENHEIT;
      pUnit = Units.Pressure.PSIG;
      maxType = "hold";
      minType = "hold";
      holdType = "hold";
      isManual = false;
      isLinked = false;

      conDisButton.TouchUpInside += delegate {
        if(currentSensor != null){
          if (activityConnectStatus != null){
            activityConnectStatus.StopAnimating();
            activityConnectStatus = null;
          }
          activityConnectStatus = new UIActivityIndicatorView(new CGRect(.867 * snapArea.Bounds.Width, .035 * snapArea.Bounds.Height, .103 * snapArea.Bounds.Width, .179 * snapArea.Bounds.Height));
          snapArea.AddSubview(activityConnectStatus);

          if(currentSensor.device.isConnected){
            connectionSpinner(1);
          } else {
            connectionSpinner(2);
          }
        }
      };

      subviewHide.TouchUpInside += delegate {
        if(tableSubviews.Count > 0){
          if(subviewTable.Hidden){
            subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
            subviewTable.Hidden = false;
          } else {
            subviewHide.SetImage(UIImage.FromBundle("ic_arrow_upwhite"), UIControlState.Normal);
            subviewTable.Hidden = true;
          }
        }
      };

      changeFluid.TouchUpInside += openSHSC;
      changePTFluid.TouchUpInside += openPTC;
      ion.locationManager.onLocationChanged += OnLocationChanged;
		}		
		
    private async void DoUpdateRocCell(ISensorProperty property) {
    	if(!tableSubviews.Contains("Rate")){
				return;
			}
    	await Task.Delay(TimeSpan.FromMilliseconds(2));
			var rocproperty = property as RateOfChangeSensorProperty;

			var roc = rocproperty.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
			var abs = Math.Abs(roc.amount);
      var range = (rocproperty.sensor.maxMeasurement - rocproperty.sensor.minMeasurement) / 10;
			Console.WriteLine("Updating rate of change subview. meas: " + roc + " abs: " + abs + " range: " + range);

			if (abs > range.magnitude) {
        rocReading.Text = ">" + SensorUtils.ToFormattedString(rocproperty.sensor.type, range, false) + "/min";
      } else {
				rocReading.Text = SensorUtils.ToFormattedString(rocproperty.sensor.type, roc.unit.OfScalar(abs), false) + "/min";
      }

      if (roc.amount == 0) {
        rocImage.Hidden = true;
        rocReading.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        rocImage.Hidden = false;
        if (roc.amount < 0) {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_down");
        } else {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_up");
        }
        await Task.Delay(TimeSpan.FromMilliseconds(500));
        DoUpdateRocCell(rocproperty);
      }
    }

    /// <summary>
    /// EVENT THAT UPDATES THE LOW/HIGH AREA SUBVIEWS IF THEY HAVE BEEN ADDED
    /// MAX MIN HOLD ALT RoC SH/SC P/T VALUES DETERMINED FROM SENSOR VALUES
    /// </summary>
    /// <param name="sensor">THE SENSOR THE LOW/HIGH AREA IS MONITORING</param>
    public void gaugeUpdating(Sensor sensor){
    	//Console.WriteLine("lowHighSensor gaugeUpdating called. Sensor name " + manifold.primarySensor.name + " working with fluid " + manifold.ptChart.fluid.name);
    	
      if (currentSensor.device.isConnected) {
        connectionColor.BackgroundColor = UIColor.Green;
        Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");       
      } else {        
        connectionColor.BackgroundColor = UIColor.Red;
        Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      }

      if (currentSensor.unit != Units.Vacuum.MICRON) {
        LabelMiddle.Text = " " + sensor.measurement.amount.ToString("N");
      } else {
        LabelMiddle.Text = " " + sensor.measurement.amount;
      }
      
      LabelBottom.Text = sensor.measurement.unit.ToString() + "  ";
      LabelSubview.Text = " " + LabelTop.Text + Util.Strings.Analyzer.LHTABLE;

      foreach (string subview in tableSubviews) {
        
        if (subview.Equals("Maximum")) {

          if (Convert.ToDouble(LabelMiddle.Text) > max) {
            max = Convert.ToDouble(LabelMiddle.Text);
            maxType = currentSensor.unit.ToString();
          }
          if (currentSensor.unit != Units.Vacuum.MICRON) {
            maxReading.Text = max.ToString("N") + " " + maxType;
          } else {
            maxReading.Text = max.ToString() + " " + maxType;
          }
        } 
        else if (subview.Equals("Minimum")) {
          if (Convert.ToDouble(LabelMiddle.Text) < min) {
            min = Convert.ToDouble(LabelMiddle.Text);
            minType = currentSensor.unit.ToString();
          }
          if (currentSensor.unit != Units.Vacuum.MICRON) {
            minReading.Text = min.ToString("N") + " " + minType + " ";
          } else {
            minReading.Text = min.ToString() + " " + minType + " ";
          }
        } 
        else if (subview.Equals("Hold")) {          
          holdReading.Text = LabelMiddle.Text + " " + LabelBottom.Text + " ";
        }
        else if (subview.Equals("Alternate")) {
          
          var tempUnit = alt.unit;

          alt.Dispose();

          alt = new AlternateUnitSensorProperty(sensor);

          alt.unit = tempUnit;

          altReading.Text = SensorUtils.ToFormattedString(alt.sensor.type, alt.modifiedMeasurement, true);      
        }
        else if(subview.Equals("Rate")){
          roc.onSensorPropertyChanged -= DoUpdateRocCell;
          roc.onSensorPropertyChanged += DoUpdateRocCell;
				}
      }
    }
    /// <summary>
    /// Manifold Event to update Superheat/Subcool and PT 
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void manifoldUpdating(ManifoldEvent Event){
    	////MANIFOLD UPDATES FOR REMOTE VIEWING
    	if(ion.webServices.downloading){
	    	if(LabelSubview.BackgroundColor == UIColor.Blue){
					if(ion.currentAnalyzer.lowSideManifold != null && manifold.ptChart != null &&  manifold.ptChart.fluid != ion.currentAnalyzer.lowSideManifold.ptChart.fluid){
	    			Console.WriteLine("Manifold is updating low side fluid");
						manifold.ptChart = PTChart.New(ion, manifold.ptChart.state,ion.currentAnalyzer.lowSideManifold.ptChart.fluid);
					}
				} else if (LabelSubview.BackgroundColor == UIColor.Red){
					if(ion.currentAnalyzer.highSideManifold != null && manifold.ptChart != null && manifold.ptChart.fluid != ion.currentAnalyzer.highSideManifold.ptChart.fluid){
	    			Console.WriteLine("Manifold is updating high side fluid");
						manifold.ptChart = PTChart.New(ion, manifold.ptChart.state,ion.currentAnalyzer.highSideManifold.ptChart.fluid);
					}
				}
			}
      //var manifold = Event.manifold;
      //Console.WriteLine(Event.type);
			if(Event.type == ManifoldEvent.EType.SecondarySensorAdded){
				Console.WriteLine("Adding secondary sensor");
				var compareSensor = __manifold.secondarySensor;
				  /////CHECK IF SENSOR IS ON THE ANALYZER ALREADY TO MAKE ASSOCIATION
					foreach(var slot in sensorList){
						if(slot.currentSensor != null){
							////SECONDARY MANIFOLD SENSOR MATCHES THE SENSOR OF A SENSOR MOUNT
							if(slot.currentSensor == compareSensor){
					      var window = UIApplication.SharedApplication.KeyWindow;
					      var vc = window.RootViewController;
					      while (vc.PresentedViewController != null) {
					        vc = vc.PresentedViewController;
					      }
								//var location = locationList.IndexOf(Convert.ToInt32(slot.snapArea.AccessibilityIdentifier));
								var location = slot.currentSensor.analyzerSlot;
								Console.WriteLine("Added sensor from location " + location);
								if(LabelSubview.BackgroundColor == UIColor.Blue && location > 3){
									Console.WriteLine("blue. Adding sensor from location " + location);
			            UIAlertController noneAvailable;
			            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
			            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
			            vc.PresentViewController(noneAvailable, true, null);
									return;
								} else if (LabelSubview.BackgroundColor == UIColor.Red && location < 4){
									Console.WriteLine("red. Adding sensor from location " + location);
			            UIAlertController noneAvailable;
			            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
			            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
			            vc.PresentViewController(noneAvailable, true, null);
									return;
								}
								
								attachedSensor = slot;
								slot.topLabel.BackgroundColor = LabelSubview.BackgroundColor;
								slot.topLabel.TextColor = UIColor.White;
							}
						}
					}
					///SET THE CURRENT ANALYZER MANIFOLD SECONDARY SENSOR TO THE ATTACHED SENSOR FOR REMOTE VIEWING	
					if(!ion.webServices.downloading){
						if(LabelSubview.BackgroundColor == UIColor.Blue){
							if(ion.currentAnalyzer.lowSideManifold == null){
								Console.WriteLine("lowHighSensor low side manifold was  null when adding a secondary sensor");
								ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.Low,__manifold.primarySensor,__manifold.ptChart.fluid);
							}						
							ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(__manifold.secondarySensor);		
								Console.WriteLine("lowHighSensor set low side secondary sensor to " + __manifold.secondarySensor.name);
						} else if (LabelSubview.BackgroundColor == UIColor.Red){
							if(ion.currentAnalyzer.highSideManifold == null){
							Console.WriteLine("lowHighSensor high side manifold was  null when adding a secondary sensor");
								ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.High,__manifold.primarySensor,__manifold.ptChart.fluid);
							}
							ion.currentAnalyzer.highSideManifold.SetSecondarySensor(__manifold.secondarySensor);		
							Console.WriteLine("lowHighSensor set high side secondary sensor to " + __manifold.secondarySensor.name);
						}
					} else {
						
					}
					
			} else if ( Event.type == ManifoldEvent.EType.SecondarySensorRemoved){
			
				if(!ion.webServices.downloading){
					if(attachedSensor != null){
						attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
						attachedSensor.topLabel.TextColor = UIColor.Gray;
						attachedSensor = null;
					}
					///SET THE CURRENT ANALYZER MANIFOLDS TO THE ATTACHED SENSOR FOR REMOTE VIEWING						
					if(LabelSubview.BackgroundColor == UIColor.Blue){
						Console.WriteLine("lowHighSensor Remove low side manifold in Analyzer class");
						ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(null);
						
					} else if (LabelSubview.BackgroundColor == UIColor.Red){
						Console.WriteLine("lowHighSensor Remove high side manifold in Analyzer class");
						ion.currentAnalyzer.highSideManifold.SetSecondarySensor(null);
					}
				} else {
					//Console.WriteLine("local manifold secondary sensor removed");
					if(attachedSensor != null){
						attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
						attachedSensor.topLabel.TextColor = UIColor.Gray;
						attachedSensor = null;
					}
				}		
			}
			
			subviewTable.ReloadData();
			
      updateSHSCCell(manifold);

      updatePTCell(manifold);

      if (currentSensor != null && manifold.secondarySensor != null) {
        if (currentSensor != manifold.primarySensor) {
          secondaryReading.Text = manifold.primarySensor.measurement.amount.ToString("N") + " " + manifold.primarySensor.unit;
        } else if (currentSensor == manifold.primarySensor) {
          secondaryReading.Text = manifold.secondarySensor.measurement.amount.ToString("N") + " " + manifold.secondarySensor.unit;
        } else {
          secondaryReading.Text = "Not Linked";
        }
      } else if (manualSensor != null && manifold.secondarySensor != null) {
        if(manualSensor.type.Equals(ESensorType.Pressure)){
          secondaryReading.Text = manifold.secondarySensor.measurement.amount.ToString("N") + " " + manifold.secondarySensor.unit;
        } else {
          secondaryReading.Text = manifold.primarySensor.measurement.amount.ToString("N") + " " + manifold.primarySensor.unit;
        }
      } else {
        secondaryReading.Text = "Not Linked";      
      }
    }
    /// <summary>
    /// Updates the calculations and labels for a SH/SC cell
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void updateSHSCCell(Manifold manifold){
    	//Console.WriteLine("lowHighSensor updateSHSCCell");
      if (manifold.secondarySensor != null) {
      	//Console.WriteLine("Secondary sensor is not null");
        isLinked = true;
        if (manifold.primarySensor.type == ESensorType.Pressure && manifold.ptChart != null) {
          shFluidType.Text = manifold.ptChart.fluid.name;
          var shname = manifold.ptChart.fluid.name;
          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(shname));
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.primarySensor.measurement, manifold.secondarySensor.measurement, manifold.primarySensor.isRelative);
          ptAmount = calculation.magnitude;
          if (!manifold.ptChart.fluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
					shReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        } else if (manifold.primarySensor.type == ESensorType.Temperature && manifold.ptChart != null) {
          shFluidType.Text = manifold.ptChart.fluid.name;
          var shname = manifold.ptChart.fluid.name;
          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(shname));
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.secondarySensor.measurement, manifold.primarySensor.measurement, manifold.secondarySensor.isRelative);
					ptAmount = calculation.magnitude;
          if (!manifold.ptChart.fluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
					shReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        } else {
          manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.primarySensor.measurement, manifold.secondarySensor.measurement, manifold.primarySensor.isRelative);
					ptAmount = calculation.magnitude;
          if (!manifold.ptChart.fluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
					shReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        }
      } else {
        shReading.Text = Util.Strings.Analyzer.SETUP;
        if (isLinked.Equals(true)) {
          isLinked = false;
          if (attachedSensor != null) {            
            attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            attachedSensor.topLabel.TextColor = UIColor.Black;
            attachedSensor = null;
          }
        }
      }

      if (manifold.ptChart != null && manifold.ptChart.fluid != null) {
        if (!manifold.ptChart.fluid.mixture){
          if (ptAmount < 0) {
            shFluidState.Text = Util.Strings.Analyzer.SC;
          } else {
            shFluidState.Text = Util.Strings.Analyzer.SH;
          }
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Bubble)) {
          shFluidState.Text = Util.Strings.Analyzer.SC;
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Dew)) {
          shFluidState.Text = Util.Strings.Analyzer.SH;
        }  
      }
    }
    /// <summary>
    /// Updates the calculations and labels for a pt cell
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void updatePTCell(Manifold manifold){
   // 	if(LabelSubview.BackgroundColor == UIColor.Blue){
			//	if(ion.currentAnalyzer.lowFluid != null && ion.currentAnalyzer.lowFluid != manifold.ptChart.fluid){
			//		manifold.ptChart.setRemoteFluid(ion.currentAnalyzer.lowFluid);
			//	}
			//} else {
			//	if(ion.currentAnalyzer.highFluid != null && ion.currentAnalyzer.highFluid != manifold.ptChart.fluid){
			//		manifold.ptChart.setRemoteFluid(ion.currentAnalyzer.highFluid);
			//	}
			//}
    
      if (manifold.primarySensor.type == ESensorType.Pressure && manifold.ptChart != null) {
        ptFluidType.Text = manifold.ptChart.fluid.name;
        var ptname = manifold.ptChart.fluid.name;
        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ptname));
        var ptcalc = manifold.ptChart.GetTemperature(manifold.primarySensor).ConvertTo(tUnit);
        if (!manifold.ptChart.fluid.mixture) {
          ptFluidState.Text = "PT";
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Bubble)) {
          ptFluidState.Text = "PTBub";
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Dew)) {
          ptFluidState.Text = "PTDew";
        }
        ptReading.Text = ptcalc.amount.ToString("N") + " " + ptcalc.unit;
      } else if (manifold.primarySensor.type == ESensorType.Temperature && manifold.ptChart != null) {
        ptFluidType.Text = manifold.ptChart.fluid.name;
        var ptname = manifold.ptChart.fluid.name;
        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ptname));
        var ptcalc = manifold.ptChart.GetPressure(manifold.primarySensor).ConvertTo(pUnit);

        if (!manifold.ptChart.fluid.mixture) {
          ptFluidState.Text = "PT";
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Bubble)) {
          ptFluidState.Text = "PTBub";
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Dew)) {
          ptFluidState.Text = "PTDew";
        } 
        ptReading.Text = ptcalc.amount.ToString("N") + " " + ptcalc.unit;
      }
    }
    /// <summary>
    /// EVENT TO OPEN THE SH/SC VIEW CONTROLLER
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void openSHSC(object sender, EventArgs e){
      var vc = __analyzerviewcontroller;
      var scsh = vc.InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL);
      scsh.initialManifold = manifold;
      vc.NavigationController.PushViewController(scsh, true);
    }
    /// <summary>
    /// EVENT TO OPEN THE PT CHART VIEW CONTROLLER
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void openPTC(object sender, EventArgs e){
      var vc = __analyzerviewcontroller;
      var ptc = vc.InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART);
      if(manifold.ptChart == null)
        manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
      if(LabelSubview.BackgroundColor == UIColor.Red){	
				ptc.lowHigh = 1;		
			}
      ptc.initialManifold = manifold;
      ptc.pUnitChanged += pUnitUpdating;
      ptc.tUnitChanged += tUnitUpdating;
      vc.NavigationController.PushViewController(ptc, true);
    }

    public void pUnitUpdating(Unit unit){
      pUnit = unit;
    }

    public void tUnitUpdating(Unit unit){
      tUnit = unit;
    }

    public async void connectionSpinner(int conn){
      activityConnectStatus.StartAnimating();
      if (conn == 1) {
        Connection.Image = Connection.Image;
        currentSensor.device.connection.Disconnect();
      } else if (conn == 2) {
        Connection.Image = Connection.Image;
       await currentSensor.device.connection.ConnectAsync();
      }

      await Task.Delay(TimeSpan.FromSeconds(2));
      activityConnectStatus.StopAnimating();			
						
      if (currentSensor != null && currentSensor.device.isConnected) {
        Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      } else {
        Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      }
    }

    public void OnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
      if(this.__manifold != null){
        updateSHSCCell(__manifold);
        updatePTCell(__manifold);
      }
    }
    
    /// <summary>
    /// Checks if a sensor is on the correct side of the analyzer before adding it as a secondary sensor to a high or low area
    /// </summary>
    /// <returns><c>true</c>, if sensor is on the same side as the low/high addition, <c>false</c> otherwise.</returns>
    /// <param name="Sensor">The sensor being added as a secondary sensor to the existing sensor</param>
    /// <param name="existingSensor">The sensor being added to</param>
    /// <param name="analyzerSensors">holds the positions of all the sensors</param>
    public static bool secondarySlotSpot(sensor Sensor, sensor existingSensor){
      bool available = false;
				
      return available;
    }    
	}
}

