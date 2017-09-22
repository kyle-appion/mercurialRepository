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
using ION.IOS.Devices;

namespace ION.IOS.ViewController.Analyzer
{
	public class lowHighSensor
	{
    public string undefinedText;
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
    public AnalyzerViewController __analyzerviewcontroller;
    public Unit tUnit;
    public Unit pUnit;
    public UIActivityIndicatorView activityConnectStatus;

    public List<string> altUnits = new List<string>{"kg/cm","inHg","psig","cmHg","bar","kPa","mPa"};
    public List<string> tempUnits = new List<string>{"celsius","fahrenheit","kelvin"};
    public List<string> vacUnits = new List<string>{ "pa", "kpa","bar", "millibar","atmo", "inhg", "cmhg", "kg/cm","psia", "torr","millitorr", "micron",};

    private bool isUpdating { get; set; }
    public bool isManual;
    public bool isLinked;
    public string location;
    public RateOfChangeSensorProperty roc;
    public AlternateUnitSensorProperty alt;
    public List<sensor> sensorList;
    List<int> locationList;
    public LowHighArea lharea;

		public sensor attachedSensor {
      get { return __attachedSensor;}
      set { 
						__attachedSensor = value;						
					}
    } sensor __attachedSensor;
    
    public GaugeDeviceSensor currentSensor{

      get { return __currentSensor; }
      set {if (__currentSensor != null) {
          __currentSensor.onSensorEvent -= gaugeUpdating;

        }
        __currentSensor = value;
        if (__currentSensor != null) {
          __currentSensor.onSensorEvent += gaugeUpdating;
          gaugeUpdating(new SensorEvent(SensorEvent.EType.Invalidated,__currentSensor));
        }
      }
    } GaugeDeviceSensor __currentSensor;

    public ManualSensor manualSensor{
      get { return __manualSensor; }
      set {
        __manualSensor = value;  
      }
    } ManualSensor __manualSensor;

		public lowHighSensor (CGRect areaRect, CGRect tblRect, AnalyzerViewController ViewController, List<sensor> viewList, List<int> areaList = null)
		{
			sensorList = viewList;
			locationList = areaList;
			snapArea = new UIView (areaRect);
			this.areaRect = areaRect;
      cellHeight = 72;
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
      rocReading = new UILabel(new CGRect(.2 * tblRect.Width, 36, .79 * tblRect.Width, 36));
      rocReading.AdjustsFontSizeToFitWidth = true;
      rocImage = new UIImageView(new CGRect(0, 36, 36, 36));
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
		  if(currentSensor.sensorProperties.Count > 0){
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
    	if(!(property is RateOfChangeSensorProperty)){
				return;
			}
    	await Task.Delay(TimeSpan.FromMilliseconds(2));
			var rocproperty = property as RateOfChangeSensorProperty;

			var roc = rocproperty.GetPrimaryAverageRateOfChange();
			var abs = Math.Abs(roc.magnitude);
      var range = (rocproperty.sensor.maxMeasurement - rocproperty.sensor.minMeasurement) / 10;

			if (abs > range.magnitude) {
        rocReading.Text = ">" + SensorUtils.ToFormattedString(rocproperty.sensor.type, range, false) + " " + roc.unit.ToString() +"/min";
      } else {
				rocReading.Text = SensorUtils.ToFormattedString(rocproperty.sensor.type, roc.unit.OfScalar(abs), false) + " " + roc.unit.ToString() + "/min";
      }

      if (roc.magnitude == 0) {
        rocImage.Image = null;
        rocReading.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        
        if (roc.magnitude < 0) {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_down");
        } else {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_up");
        }
        await Task.Delay(TimeSpan.FromMilliseconds(500));
        DoUpdateRocCell(rocproperty);
      }
    }

    /// EVENT THAT UPDATES THE LOW/HIGH AREA SUBVIEWS IF THEY HAVE BEEN ADDED
    /// MAX MIN HOLD ALT RoC SH/SC P/T VALUES DETERMINED FROM SENSOR VALUES
    public void gaugeUpdating(SensorEvent Event){
    	//Console.WriteLine("lowHighSensor gaugeUpdating called. Sensor name " + manifold.primarySensor.name + " working with fluid " + manifold.ptChart.fluid.name);
    	
      if (currentSensor.device.isConnected) {
        connectionColor.BackgroundColor = UIColor.Green;
        Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");       
      } else {        
        connectionColor.BackgroundColor = UIColor.Red;
        Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      }

      if (currentSensor.device.connection.connectionState == EConnectionState.Connecting) {
        activityConnectStatus.StartAnimating();
      } else {
        activityConnectStatus.StopAnimating();      
      }

      if (currentSensor.unit != Units.Vacuum.MICRON) {
        LabelMiddle.Text = " " + Event.sensor.measurement.amount.ToString("N");
      } else {
        LabelMiddle.Text = " " + Event.sensor.measurement.amount;
      }
      
      LabelBottom.Text = Event.sensor.measurement.unit.ToString() + "  ";

			foreach (var property in currentSensor.sensorProperties) {
        
        if (property is MaxSensorProperty) {

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
        else if (property is MinSensorProperty) {
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
        else if (property is HoldSensorProperty) {          
          holdReading.Text = LabelMiddle.Text + " " + LabelBottom.Text + " ";
        }
        else if (property is AlternateUnitSensorProperty) {
          
          var tempUnit = alt.unit;

          alt.Dispose();

					alt = property as AlternateUnitSensorProperty;

					alt.unit = tempUnit;

          altReading.Text = SensorUtils.ToFormattedString(alt.modifiedMeasurement, true);      
        }
        else if (property is RateOfChangeSensorProperty){
          roc.onSensorPropertyChanged -= DoUpdateRocCell;
          roc.onSensorPropertyChanged += DoUpdateRocCell;
				}
      }

      if (Event.type == SensorEvent.EType.LinkedSensorAdded) {
				Console.WriteLine("Adding secondary sensor");
	
				var compareSensor = currentSensor.linkedSensor;
				/////CHECK IF SENSOR IS ON THE ANALYZER ALREADY TO MAKE ASSOCIATION
				foreach (var slot in sensorList)
				{
					if (slot.currentSensor != null)
					{
						////SECONDARY SENSOR MATCHES THE SENSOR OF A SENSOR MOUNT
						if (slot.currentSensor == compareSensor) {
							var window = UIApplication.SharedApplication.KeyWindow;
							var vc = window.RootViewController;
							while (vc.PresentedViewController != null)
							{
								vc = vc.PresentedViewController;
							}
              var locationIndex = ion.currentAnalyzer.IndexOfSensor(currentSensor.linkedSensor);

							Console.WriteLine("Added sensor from location " + locationIndex);
							if (LabelSubview.BackgroundColor == UIColor.Blue && locationIndex > 3) {
								Console.WriteLine("blue. Adding sensor from location " + locationIndex);
								UIAlertController noneAvailable;
								noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
								noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => { }));
								vc.PresentViewController(noneAvailable, true, null);
								
								ion.currentAnalyzer.lowSideSensor.SetLinkedSensor(null);
								updateSHSCCell();
								return;
							}	else if (LabelSubview.BackgroundColor == UIColor.Red && locationIndex < 4)	{
								Console.WriteLine("red. Adding sensor from location " + locationIndex);
								UIAlertController noneAvailable;
								noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
								noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => { }));
								vc.PresentViewController(noneAvailable, true, null);
								
								ion.currentAnalyzer.highSideSensor.SetLinkedSensor(null);
								updateSHSCCell();
								return;
							}

							attachedSensor = slot;
							slot.topLabel.BackgroundColor = LabelSubview.BackgroundColor;
							slot.topLabel.TextColor = UIColor.White;
							break;
						}
					}
				}
			} else if (Event.type == SensorEvent.EType.LinkedSensorRemoved){
				//Console.WriteLine("current sensor secondary sensor removed");
				if (attachedSensor != null)
				{
					attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
					attachedSensor.topLabel.TextColor = UIColor.Gray;
					attachedSensor = null;
				}   
			}

			updateSHSCCell();

			updatePTCell();

			if (currentSensor != null && currentSensor.linkedSensor != null) {
					secondaryReading.Text = currentSensor.linkedSensor.measurement.amount.ToString("N") + " " + currentSensor.linkedSensor.unit;
			}	else if (manualSensor != null && manualSensor.linkedSensor != null) {
					secondaryReading.Text = manualSensor.linkedSensor.measurement.amount.ToString("N") + " " + manualSensor.linkedSensor.unit;
			}	else {
				secondaryReading.Text = "Not Linked";
			}
    }

    //// Updates the calculations and labels for a SH/SC cell
    public void updateSHSCCell(){
			//Console.WriteLine("lowHighSensor updateSHSCCell");
			if (currentSensor.linkedSensor != null) {
      	//Console.WriteLine("Secondary sensor is not null");
        isLinked = true;

				if (currentSensor.type == ESensorType.Pressure)	{

					shFluidType.Text = ion.fluidManager.lastUsedFluid.name;

          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
          var calculation = ion.fluidManager.lastUsedFluid.CalculateTemperatureDelta(currentSensor.fluidState,currentSensor.measurement, currentSensor.linkedSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
          ptAmount = calculation.magnitude;
          if (!ion.fluidManager.lastUsedFluid.mixture && calculation < 0) {
            calculation = calculation * -1;
          }
					shReading.Text = calculation.magnitude.ToString("N") + calculation.unit.ToString();
        } else if (currentSensor.type == ESensorType.Temperature) {
          shFluidType.Text = ion.fluidManager.lastUsedFluid.name;

          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
          var calculation = ion.fluidManager.lastUsedFluid.CalculateTemperatureDelta(currentSensor.fluidState, currentSensor.linkedSensor.measurement, currentSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
					ptAmount = calculation.magnitude;
          if (!ion.fluidManager.lastUsedFluid.mixture && calculation < 0) {
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

      if (!ion.fluidManager.lastUsedFluid.mixture){
        if (ptAmount < 0) {
          shFluidState.Text = Util.Strings.Analyzer.SC;
        } else {
          shFluidState.Text = Util.Strings.Analyzer.SH;
        }
      } else if (currentSensor.fluidState.Equals(Fluid.EState.Bubble)) {
        shFluidState.Text = Util.Strings.Analyzer.SC;
      } else if (currentSensor.fluidState.Equals(Fluid.EState.Dew)) {
        shFluidState.Text = Util.Strings.Analyzer.SH;
      }
    }

    //// Updates the calculations and labels for a pt cell
    public void updatePTCell(){
      if (currentSensor.type == ESensorType.Pressure) {
        ptFluidType.Text = ion.fluidManager.lastUsedFluid.name;

        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
				var ptcalc = ion.fluidManager.lastUsedFluid.GetSaturatedTemperature(currentSensor.fluidState,currentSensor.measurement, ion.locationManager.lastKnownLocation.altitude);

        ptReading.Text = ptcalc.ConvertTo(tUnit).amount.ToString("N") + " " + ptcalc.unit;
      } else if (currentSensor.type == ESensorType.Temperature) {
        ptFluidType.Text = ion.fluidManager.lastUsedFluid.name;

        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
        var ptcalc = ion.fluidManager.lastUsedFluid.GetPressureFromSaturatedTemperature(currentSensor.fluidState, currentSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
        ptcalc = ion.fluidManager.lastUsedFluid.ConvertAbsolutePressureToRelative(ptcalc, ion.locationManager.lastKnownLocation.altitude); 

        ptReading.Text = ptcalc.ConvertTo(pUnit).amount.ToString("N") + " " + ptcalc.unit;
      }

			if (!ion.fluidManager.lastUsedFluid.mixture) {
				ptFluidState.Text = "PT";
			}	else if (currentSensor.fluidState.Equals(Fluid.EState.Bubble)) {
				ptFluidState.Text = "PTBub";
			}	else if (currentSensor.fluidState.Equals(Fluid.EState.Dew))	{
				ptFluidState.Text = "PTDew";
			}
		}

    //// EVENT TO OPEN THE SH/SC VIEW CONTROLLER
    public void openSHSC(object sender, EventArgs e){
      var vc = __analyzerviewcontroller;
      var scsh = vc.InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL);
      scsh.initialSensor = currentSensor;
      if (LabelSubview.BackgroundColor == UIColor.Red) {
        scsh.lowHigh = 1;
      }
			vc.NavigationController.PushViewController(scsh, true);
    }

    //// EVENT TO OPEN THE PT CHART VIEW CONTROLLER
    public void openPTC(object sender, EventArgs e){
      var vc = __analyzerviewcontroller;
      var ptc = vc.InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART);

      if(LabelSubview.BackgroundColor == UIColor.Red){	
				ptc.lowHigh = 1;		
			}
			ptc.initialSensor = currentSensor;
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

    public void setLHUI(){
      if (currentSensor != null) {
        LabelTop.Text = currentSensor.name;
        LabelMiddle.Text = SensorUtils.ToFormattedString(currentSensor.measurement);
        LabelBottom.Text = currentSensor.measurement.unit.ToString() + "  ";
        LabelSubview.Text = " " + LabelTop.Text + Util.Strings.Analyzer.LHTABLE;
				if (currentSensor.device.isConnected) {
					Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
					connectionColor.BackgroundColor = UIColor.Green;
					LabelMiddle.Font = UIFont.FromName("DroidSans-Bold", 42f);
				} else {
					Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
					connectionColor.BackgroundColor = UIColor.Red;
					LabelMiddle.Font = UIFont.FromName("DroidSans", 20f);
				}
				DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(currentSensor.device.serialNumber.deviceModel);

        Connection.Hidden = false;
        connectionColor.Hidden = false;
      } else if (manualSensor != null){
				LabelTop.Text = manualSensor.name;
				LabelMiddle.Text = SensorUtils.ToFormattedString(manualSensor.measurement);
				LabelBottom.Text = manualSensor.measurement.unit.ToString() + "  ";
				LabelSubview.Text = " " + LabelTop.Text + Util.Strings.Analyzer.LHTABLE;
				DeviceImage.Image = UIImage.FromBundle("ic_edit");
				Connection.Hidden = true;
				connectionColor.Hidden = true;
			}

			LabelTop.Hidden = false;
			DeviceImage.Hidden = false;
			LabelMiddle.Hidden = false;
			LabelBottom.Hidden = false;
			LabelSubview.Hidden = false;
			subviewHide.Hidden = false;
			subviewTable.Hidden = false;
			headingDivider.Hidden = false;
			LabelMiddle.Font = UIFont.FromName("DroidSans-Bold", 42f);
		}

    public void hideLHUI(){
      LabelMiddle.Text = undefinedText;
			LabelTop.Hidden = true;
			Connection.Hidden = true;
			DeviceImage.Hidden = true;
			LabelBottom.Hidden = true;
			LabelSubview.Hidden = true;
			subviewTable.Hidden = true;
			headingDivider.Hidden = true;
			connectionColor.Hidden = true;
			subviewHide.Hidden = true;
			subviewTable.Source = null;
			subviewTable.ReloadData();
			subviewTable.Hidden = true;
			max = 0;
			min = 0;
			LabelMiddle.Font = UIFont.FromName("DroidSans", 20f);

			if (attachedSensor != null) {
				attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
				attachedSensor.topLabel.TextColor = UIColor.Gray;
				attachedSensor = null;
			}
			subviewHide.SetImage(null, UIControlState.Normal);
			currentSensor = null;
			manualSensor = null;
			isManual = false;
		}
		/// <summary>
		/// Removes the Low or high Sensor association for a remote sensor update
		/// </summary>
		/// <param name="attachSensor">Attach sensor.</param>
		public void RemoveLHAssociation() {
			if (attachedSensor != null) {
				attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
				attachedSensor.topLabel.TextColor = UIColor.Gray;
				attachedSensor = null;
			}
		}

		/// Sets the low/high area for the active sensor
		public void addLHSensorAssociation(sensor activeSensor) {
			activeSensor.topLabel.BackgroundColor = LabelTop.BackgroundColor;
			activeSensor.topLabel.TextColor = UIColor.White;
      currentSensor = activeSensor.currentSensor;
			setLHUI();
		}

    public void connectionSpinner(int conn){
      if (conn == 1) {
        Connection.Image = Connection.Image;
        currentSensor.device.connection.Disconnect();
      } else if (conn == 2) {
        Connection.Image = Connection.Image;
        currentSensor.device.connection.Connect();
      }
    }

    public void OnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
			if(currentSensor != null){
        updateSHSCCell();
        updatePTCell();
      }
    }

	}
}

