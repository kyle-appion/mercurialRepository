using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using CoreGraphics;
using ION.Core.App;
using ION.Core.Measure;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;
using ION.Core.Fluids;
using ION.IOS.ViewController.FluidManager;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.PressureTemperatureChart;

namespace ION.IOS.ViewController.Analyzer
{
	public class lowHighSensor
	{
		public lowHighSensor (CGRect areaRect, CGRect tblRect, AnalyzerViewController ViewController)
		{
      //new CGRect(9, 325, 149,115)
			snapArea = new UIView (areaRect);
      cellHeight = .521f * snapArea.Bounds.Height;
      //Low: new CGRect(9,438,149,122)
      //High: new CGRect(165,438,149,122)
      subviewTable = new UITableView (tblRect);
      //new CGRect(0,0,128,25), new CGRect(0,25,149,40), new CGRect(0,65,149,25),new CGRect(0,92,150,25)
      LabelTop = new UILabel (new CGRect(0,0, .859 * areaRect.Width, .217 * areaRect.Height));
      LabelMiddle = new UILabel (new CGRect(0, .217 * areaRect.Height, areaRect.Width, .347 * areaRect.Height));
      LabelBottom = new UILabel (new CGRect(0, .565 * areaRect.Height, areaRect.Width, .217 * areaRect.Height));
      LabelSubview = new UILabel (new CGRect(-1, .8 * areaRect.Height, .8 * snapArea.Bounds.Width, .204 * areaRect.Height));
      subviewHide = new UIButton(new CGRect(.791 * snapArea.Bounds.Width, .8 * areaRect.Height, .213 * snapArea.Bounds.Width, .204 * areaRect.Height));
      subviewDivider = new UIView(new CGRect(0,.8 * areaRect.Height,areaRect.Width,2));
      headingDivider = new UIView(new CGRect(.033 * areaRect.Width,.234 * areaRect.Height,.932 * areaRect.Width,1));
      connectionColor = new UIView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height, .14 * areaRect.Width,.217 * areaRect.Height));
      connectionColor.BackgroundColor = UIColor.Red;
      Connection = new UIImageView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height,  .14 * areaRect.Width,.217 * areaRect.Height));
      conDisButton = new UIButton(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height, .14 * areaRect.Width,.217 * areaRect.Height));
      conDisButton.BackgroundColor = UIColor.Clear;
      DeviceImage = new UIImageView(new CGRect(0, .234 * areaRect.Height, .214 * areaRect.Width,.321 * areaRect.Height));
      subviewTable.RegisterClassForCellReuse(typeof(maximumTableCell),"Maximum");
      subviewTable.RegisterClassForCellReuse(typeof(minimumTableCell),"Minimum");
      subviewTable.RegisterClassForCellReuse(typeof(holdTableCell), "Hold");
      subviewTable.RegisterClassForCellReuse(typeof(altTableCell), "Alternate");
      subviewTable.RegisterClassForCellReuse(typeof(RoCTableCell), "Rate");
      subviewTable.RegisterClassForCellReuse(typeof(SHSCTableCell), "Superheat");
      subviewTable.RegisterClassForCellReuse(typeof(PTTableCell), "Pressure");
      maxReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      minReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      holdReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      shReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      shFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      shFluidType.Layer.BorderColor = UIColor.Black.CGColor;
      shFluidType.Layer.BorderWidth = 1f;
      shFluidState = new UILabel(new CGRect(0,0, 1.006 * tblRect.Width, .5 * cellHeight));
      shFluidState.TextAlignment = UITextAlignment.Center;
      shFluidState.TextColor = UIColor.White;
      shFluidState.BackgroundColor = UIColor.Black;
      changeFluid = new UIButton(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changeFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changeFluid.Layer.BorderWidth = 1f;
      changeFluid.BackgroundColor = UIColor.Clear;
      ptReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      ptFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changePTFluid = new UIButton(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changePTFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changePTFluid.Layer.BorderWidth = 1f;
      changePTFluid.BackgroundColor = UIColor.Clear;
      altReading = new UILabel(new CGRect(0, .5 * cellHeight, tblRect.Width, .5 * cellHeight));
      rocReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      rocImage = new UIImageView(new CGRect(0, .5 * cellHeight, .2 * tblRect.Width, .5 * cellHeight));
      ion = AppState.context;
      __analyzerviewcontroller = ViewController;
      tUnit = Units.Temperature.FAHRENHEIT;
      pUnit = Units.Pressure.PSIG;
      maxType = "hold";
      minType = "hold";
      isManual = false;

      conDisButton.TouchUpInside += delegate {
        if(currentSensor != null){
          if(currentSensor.device.isConnected){
            currentSensor.device.connection.Disconnect();
          } else {
            currentSensor.device.connection.Connect();
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
//      changeFluid.TouchUpInside += delegate {
//        Console.WriteLine("clicked the scsh button for sensor " + snapArea.AccessibilityIdentifier);
//      };
//      changePTFluid.TouchUpInside += delegate {
//        Console.WriteLine("clicked the pt button for sensor " + snapArea.AccessibilityIdentifier);
//      };
      changeFluid.TouchUpInside += openSHSC;
      changePTFluid.TouchUpInside += openPTC;
		}

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
    public UILabel ptFluidType;
    public UIButton changePTFluid;
    public UILabel altReading;
    public Unit altUnit;
    public UILabel rocReading;
    public UIImageView rocImage;
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
		public List<string> tableSubviews = new List<string>();
    public List<string> altUnits = new List<string>{"kg/cm","inHg","psig","cmHg","bar","kPa","mPa"};
    public List<string> tempUnits = new List<string>{"celsius","fahrenheit","kelvin"};
    public List<string> vacUnits = new List<string>{ "pa", "kpa","bar", "millibar","atmo", "inhg", "cmhg", "kg/cm","psia", "torr","millitorr", "micron",};
    public List<string> availableSubviews = new List<string> {
      "Hold Reading (HOLD)","Maximum Reading (MAX)", "Minimum Reading (MIN)", "Alternate Unit(ALT)","Rate of Change (RoC)", "Superheat / Subcool (S/H or S/C)", "Pressure / Temperature (P/T)"
    };
    private bool isUpdating { get; set; }
    public bool isManual;
    private RateOfChangeSensorProperty roc;
    public AlternateUnitSensorProperty alt;

    public GaugeDeviceSensor currentSensor{

      get { return __currentSensor;}
      set { if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent -= gaugeUpdating;
        }
        __currentSensor = value;
        if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent += gaugeUpdating;
        }
      }
    } GaugeDeviceSensor __currentSensor;

    public Manifold manifold{

      get { return __manifold;}
      set { if (__manifold != null) {
          __manifold.onManifoldChanged -= manifoldUpdating;
        }
        __manifold = value;
        if (__manifold != null) {
          __manifold.onManifoldChanged += manifoldUpdating;
        }
      }
    } Manifold __manifold;

    private async void DoUpdateRocCell() {

      var meas = roc.modifiedMeasurement;
      var abs = meas.Abs();
      var range = (roc.sensor.maxMeasurement - roc.sensor.minMeasurement) / 10;

      if (abs > range) {
        rocReading.Text = ">" + SensorUtils.ToFormattedString(roc.sensor.type, range, true);
      } else {
        rocReading.Text = SensorUtils.ToFormattedString(roc.sensor.type, abs, true);
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

      LabelMiddle.Text = " " + sensor.measurement.amount.ToString();
      LabelBottom.Text = sensor.measurement.unit.ToString() + "  ";
      LabelSubview.Text = LabelTop.Text + "'s Subviews";

      foreach (string subview in tableSubviews) {
        
        if (subview.Equals("Maximum")) {       

          if (Convert.ToDouble(LabelMiddle.Text) > max) {
            max = Convert.ToDouble(LabelMiddle.Text);
            maxType = currentSensor.unit.ToString();
          }

          maxReading.Text = max.ToString("0.00") + " " + maxType;
        } 
        if (subview.Equals("Minimum")) {
          if (Convert.ToDouble(LabelMiddle.Text) < min) {
            min = Convert.ToDouble(LabelMiddle.Text);
            minType = currentSensor.unit.ToString();
          }
          
          minReading.Text = min.ToString("0.00") + " " + minType;
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
    public void manifoldUpdating(Manifold manifold){
      if (manifold.secondarySensor != null) {
        if (manifold.primarySensor.type == ESensorType.Pressure && manifold.ptChart != null) {
          shFluidType.Text = manifold.ptChart.fluid.name;
          var shname = manifold.ptChart.fluid.name;
          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(shname));
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.primarySensor.measurement, manifold.secondarySensor.measurement, false);
          shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
        } else if (manifold.primarySensor.type == ESensorType.Temperature && manifold.ptChart != null){
          shFluidType.Text = manifold.ptChart.fluid.name;
          var shname = manifold.ptChart.fluid.name;
          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(shname));
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.secondarySensor.measurement, manifold.primarySensor.measurement, false);
          shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
        }
      }

      if (manifold.primarySensor.type == ESensorType.Pressure && manifold.ptChart != null) {
        ptFluidType.Text = manifold.ptChart.fluid.name;
        var ptname = manifold.ptChart.fluid.name;
        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ptname));
        var ptcalc = manifold.ptChart.GetTemperature(manifold.primarySensor).ConvertTo(tUnit);
        ptReading.Text = ptcalc.amount.ToString("0.00") + " " + ptcalc.unit;
      } else if (manifold.primarySensor.type == ESensorType.Temperature && manifold.ptChart != null) {
        ptFluidType.Text = manifold.ptChart.fluid.name;
        var ptname = manifold.ptChart.fluid.name;
        ptFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ptname));
        var ptcalc = manifold.ptChart.GetPressure(manifold.primarySensor).ConvertTo(pUnit);
        ptReading.Text = ptcalc.amount.ToString("0.00") + " " + ptcalc.unit;
      }

      if (manifold.ptChart != null) {
        if (manifold.ptChart.state.Equals(Fluid.EState.Bubble)) {
          shFluidState.Text = "S/C";
        } else if (manifold.ptChart.state.Equals(Fluid.EState.Dew)) {
          shFluidState.Text = "S/H";
        }
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
      manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
      ptc.initialManifold = manifold;
      ptc.pUnitChanged += pUnitUpdating;
      ptc.tUnitChanged += tUnitUpdating;
      vc.NavigationController.PushViewController(ptc, true);
    }

    public void pUnitUpdating(Unit unit){
      pUnit = unit;
      Console.WriteLine("Changed pressure unit to " + pUnit.ToString());
    }

    public void tUnitUpdating(Unit unit){
      tUnit = unit;
      Console.WriteLine("Changed temperature unit to " + tUnit.ToString());
    }
	}
}

