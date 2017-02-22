using System;
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

namespace ION.IOS.ViewController.Analyzer
{
	public class lowHighSensor
	{
    public IION ion { get; set; }
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
        }
        __currentSensor = value;
        if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent += gaugeUpdating;
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
      ion = AppState.context;
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
    private async void DoUpdateRocCell() {

      var meas = roc.modifiedMeasurement;
			var abs = Math.Abs(meas.amount);
      var range = (roc.sensor.maxMeasurement - roc.sensor.minMeasurement) / 10;

			if (abs > range.magnitude) {
        rocReading.Text = ">" + SensorUtils.ToFormattedString(roc.sensor.type, range, false) + "/min";
      } else {
				rocReading.Text = SensorUtils.ToFormattedString(roc.sensor.type, meas.unit.OfScalar(abs), false) + "/min";
      }

      if (roc.isStable) {
        rocImage.Hidden = true;
        rocReading.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        rocImage.Hidden = false;
        if (meas < 0) {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_down");
        } else {
          rocImage.Image = UIImage.FromBundle("ic_arrow_trend_up");
        }

        await Task.Delay(100);
        DoUpdateRocCell();
      }
    }

    /// <summary>
    /// EVENT THAT UPDATES THE LOW/HIGH AREA SUBVIEWS IF THEY HAVE BEEN ADDED
    /// MAX MIN HOLD ALT RoC SH/SC P/T VALUES DETERMINED FROM SENSOR VALUES
    /// </summary>
    /// <param name="sensor">THE SENSOR THE LOW/HIGH AREA IS MONITORING</param>
    public void gaugeUpdating(Sensor sensor){
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
        if (subview.Equals("Minimum")) {
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
        if (subview.Equals("Hold")) {          
          holdReading.Text = LabelMiddle.Text + " " + LabelBottom.Text + " ";
        }
        if (subview.Equals("Alternate")) {
          
          var tempUnit = alt.unit;

          alt.Dispose();

          alt = new AlternateUnitSensorProperty(sensor);

          alt.unit = tempUnit;

          altReading.Text = SensorUtils.ToFormattedString(alt.sensor.type, alt.modifiedMeasurement, true);      
        }
        if (subview.Equals("Rate")) {
          
          if (roc == null) {
            roc = new RateOfChangeSensorProperty(sensor);
          }
          DoUpdateRocCell();
        }

      }
    }
    /// <summary>
    /// Manifold Event to update Superheat/Subcool and PT 
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void manifoldUpdating(ManifoldEvent Event){
      //var manifold = Event.manifold;
      //Console.WriteLine(Event.type);
			if(Event.type == ManifoldEvent.EType.SecondarySensorAdded){
				var compareSensor = __manifold.secondarySensor;
					foreach(var slot in sensorList){
						if(slot.currentSensor != null){
							if(slot.currentSensor == compareSensor){
					      var window = UIApplication.SharedApplication.KeyWindow;
					      var vc = window.RootViewController;
					      while (vc.PresentedViewController != null) {
					        vc = vc.PresentedViewController;
					      }
								var location = locationList.IndexOf(Convert.ToInt32(slot.snapArea.AccessibilityIdentifier));
								//Console.WriteLine("Added sensor from location " + location);
								if(LabelSubview.BackgroundColor == UIColor.Blue && location > 3){
									//Console.WriteLine("blue. Adding sensor from location " + location);
									__manifold.SetSecondarySensor(null);
									ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low,null);
			            UIAlertController noneAvailable;
			            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
			            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
			            vc.PresentViewController(noneAvailable, true, null);
									return;
								} else if (LabelSubview.BackgroundColor == UIColor.Red && location < 4){
									//Console.WriteLine("red. Adding sensor from location " + location);
									__manifold.SetSecondarySensor(null);
									ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High,null);
			            UIAlertController noneAvailable;
			            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
			            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
			            vc.PresentViewController(noneAvailable, true, null);
									return;
								}
								
								attachedSensor = slot;
								slot.topLabel.BackgroundColor = LabelSubview.BackgroundColor;
								slot.tLabelBottom.BackgroundColor = LabelSubview.BackgroundColor;
								slot.topLabel.TextColor = UIColor.White;
							}
						}
					}
					///SET THE CURRENT ANALYZER MANIFOLDS TO THE ATTACHED SENSOR FOR REMOTE VIEWING						
					if(LabelSubview.BackgroundColor == UIColor.Blue){
						Console.WriteLine("lowHighSensor Set low side manifold in Analyzer class");
						ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.Low,__manifold.secondarySensor) ;		
					} else if (LabelSubview.BackgroundColor == UIColor.Red){
						Console.WriteLine("lowHighSensor Set high side manifold in Analyzer class");
						ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.High,__manifold.secondarySensor);	
					}
					
			} else if ( Event.type == ManifoldEvent.EType.SecondarySensorRemoved){
			 var compareSensor = __manifold.secondarySensor;
				if(currentSensor != compareSensor && attachedSensor != null){
					foreach(var slot in sensorList){
						if(slot.currentSensor != null){
							if(slot.currentSensor == compareSensor){
								slot.topLabel.BackgroundColor = UIColor.Clear;
								slot.tLabelBottom.BackgroundColor = UIColor.Clear;
								slot.topLabel.TextColor = UIColor.Black;	
							}
							if(slot.currentSensor == currentSensor){    
								slot.lowArea.attachedSensor = null;
								slot.highArea.attachedSensor = null;
							}
						} else if (slot.manualSensor != null){
							if(slot.manualSensor == compareSensor){
								slot.topLabel.BackgroundColor = UIColor.Clear;
								slot.tLabelBottom.BackgroundColor = UIColor.Clear;
								slot.topLabel.TextColor = UIColor.Black;	
							}
							if(__manualSensor != null && slot.manualSensor == __manualSensor){
								slot.lowArea.attachedSensor = null;
								slot.highArea.attachedSensor = null;
							}
						}
					}
				}
				///SET THE CURRENT ANALYZER MANIFOLDS TO THE ATTACHED SENSOR FOR REMOTE VIEWING						
				if(LabelSubview.BackgroundColor == UIColor.Blue){
					Console.WriteLine("lowHighSensor Remove low side manifold in Analyzer class");
					ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.Low,null) ;		
				} else if (LabelSubview.BackgroundColor == UIColor.Red){
					Console.WriteLine("lowHighSensor Remove high side manifold in Analyzer class");
					ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.High,null);	
				}				
			}
			foreach(var slot in sensorList){
				if(manifold.secondarySensor!=null)
					if(slot.manualSensor != null && slot.manualSensor == manifold.secondarySensor){											
						slot.middleLabel.Text = manifold.secondarySensor.measurement.amount.ToString("N");
						slot.bottomLabel.Text = manifold.secondarySensor.unit.ToString();
						break;
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
            attachedSensor.tLabelBottom.BackgroundColor = UIColor.Clear;
            attachedSensor.topLabel.TextColor = UIColor.Black;
            attachedSensor = null;
          }
        }
      }

      if (manifold.ptChart != null) {
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

      if (currentSensor.device.isConnected) {
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

