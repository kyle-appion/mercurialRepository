using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using ION.Core.App;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;
using UIKit;
using CoreGraphics;
using ION.IOS.ViewController.FluidManager;

namespace ION.IOS.ViewController.Analyzer
{
	public class lowHighSensor
	{
		public lowHighSensor (CGRect areaRect, CGRect tblRect)
		{
      //new CGRect(9, 325, 149,115)
			snapArea = new UIView (areaRect);
      cellHeight = .521f * snapArea.Bounds.Height;
      //Low: new CGRect(9,438,149,122)
      //High: new CGRect(165,438,149,122)
      subviewTable = new UITableView (tblRect);
      Console.WriteLine("The table has bounds " + tblRect.Width + " " + tblRect.Height);
      //new CGRect(0,0,128,25), new CGRect(0,25,149,40), new CGRect(0,65,149,25),new CGRect(0,92,150,25)
      LabelTop = new UILabel (new CGRect(0,0, .859 * areaRect.Width, .217 * areaRect.Height));
      LabelMiddle = new UILabel (new CGRect(0, .217 * areaRect.Height, areaRect.Width, .347 * areaRect.Height));
      LabelBottom = new UILabel (new CGRect(0, .565 * areaRect.Height, 1.006 * areaRect.Width, .217 * areaRect.Height));
      LabelSubview = new UILabel (new CGRect(-1, .8 * areaRect.Height, 1.006 * snapArea.Bounds.Width, .204 * areaRect.Height));
      //public UIView subviewDivider = new UIView(new CGRect(0,93,149,2));
      subviewDivider = new UIView(new CGRect(0,.8 * areaRect.Height,areaRect.Width,2));
      //public UIView headingDivider = new UIView(new CGRect(5,27,139,1));
      headingDivider = new UIView(new CGRect(.033 * areaRect.Width,.234 * areaRect.Height,.932 * areaRect.Width,1));
      //public UIView connectionColor = new UIView(new CGRect(125, 2, 21,25));
      connectionColor = new UIView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height, .14 * areaRect.Width,.217 * areaRect.Height));
      connectionColor.BackgroundColor = UIColor.Red;
      //public UIImageView Connection = new UIImageView(new CGRect(125, 2, 21,25));
      Connection = new UIImageView(new CGRect(.838 * areaRect.Width, .017 * areaRect.Height,  .14 * areaRect.Width,.217 * areaRect.Height));
      //public UIImageView DeviceImage = new UIImageView(new CGRect(0, 27, 32,40));
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
      //maxReading = new UILabel(new CGRect(30,30,119,30));
      maxReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      //minReading = new UILabel(new CGRect(30,30,119,30));
      minReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      //holdReading = new UILabel(new CGRect(30,30,119,30));
      holdReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      //shReading = new UILabel(new CGRect(100,30,75,30));
      shReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      //shFluidType = new UILabel(new CGRect(0,30,75,30));
      shFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      //public UIButton changeFluid = new UIButton(new CGRect(100, 30, 49, 30));
      changeFluid = new UIButton(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changeFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changeFluid.Layer.BorderWidth = 1f;
      changeFluid.BackgroundColor = UIColor.Clear;
      //ptReading = new UILabel(new CGRect(100,30,49,30));
      ptReading = new UILabel(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      //ptFluidtype = new UILabel(new CGRect(0,30,75,30));
      ptFluidType = new UILabel(new CGRect(0, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      //public UIButton changeFluid = new UIButton(new CGRect(100, 30, 49, 30));
      changePTFluid = new UIButton(new CGRect(.5 * tblRect.Width, .5 * cellHeight, .5 * tblRect.Width, .5 * cellHeight));
      changePTFluid.Layer.BorderColor = UIColor.Black.CGColor;
      changePTFluid.Layer.BorderWidth = 1f;
      changePTFluid.BackgroundColor = UIColor.Clear;
      //altReading = new UILabel(new CGRect(0,30,149,30));
      altReading = new UILabel(new CGRect(0, .5 * cellHeight, tblRect.Width, .5 * cellHeight));
      //rocReading = new UILabel(new CGRect(30,30,119,30));
      rocReading = new UILabel(new CGRect(.2 * tblRect.Width, .5 * cellHeight, .8 * tblRect.Width, .5 * cellHeight));
      //rocImage = new UIImageView(new CGRect(0,30,30,30));
      rocImage = new UIImageView(new CGRect(0, .5 * cellHeight, .2 * tblRect.Width, .5 * cellHeight));
      ion = AppState.context;
      max = 0.00;
      maxType = "psig";
      min = 0.00;
      minType = "psig";

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
		}
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
    public UILabel shReading;
    public UILabel shFluidType;
    public UIButton changeFluid;
    public UILabel ptReading;
    public UILabel ptFluidType;
    public UIButton changePTFluid;
    public UILabel altReading;
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
		public UITableView subviewTable;
    public UIImageView Connection;
    public UIImageView DeviceImage;
    public UIButton conDisButton;
		public List<string> tableSubviews = new List<string>();
    public List<string> altUnits = new List<string>{"kg/cm","inHg","psig","cmHg","bar","kPa","mPa"};
    private bool isUpdating { get; set; }
    public bool isManual;
    private RateOfChangeSensorProperty roc;
    public AlternateUnitSensorProperty alt;

    public string UpdateMax(double currentReading, string type){
      if (currentReading > max) {
        max = currentReading;
        maxType = currentSensor.unit.ToString();
      }

      return max.ToString("0.00") + " " + maxType;
    }

    public string UpdateMin(double currentReading, string type){
      if (currentReading < min) {
        min = currentReading;
        minType = currentSensor.unit.ToString();
      }
      
      return min.ToString("0.00") + " " + minType;
    }

    public void UpdateRoc(Sensor sensorProperty){
      if (roc == null) {
        roc = new RateOfChangeSensorProperty(sensorProperty);
      }

      DoUpdateRocCell();
    }

    private async void DoUpdateRocCell() {

      var meas = roc.modifiedMeasurement;
      //ION.Core.Util.Log.D(this, "Meas: " + meas);
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


    public void updateSHSC(){
    }

    public void updatePT(){
    }

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


      LabelMiddle.Text = sensor.measurement.amount.ToString();
      LabelBottom.Text = sensor.measurement.unit.ToString();
      LabelSubview.Text = LabelTop.Text + "'s Subviews";

      foreach (string subview in tableSubviews) {
        
        if (subview.Equals("Maximum")) {
          Console.WriteLine("inside maximum");
          maxReading.Text = UpdateMax(Convert.ToDouble(LabelMiddle.Text), LabelBottom.Text);
        } 
        if (subview.Equals("Minimum")) {          
          minReading.Text = UpdateMin(Convert.ToDouble(LabelMiddle.Text), LabelBottom.Text);
        } 
        if (subview.Equals("Hold")) {          
          holdReading.Text = LabelMiddle.Text + " " + LabelBottom.Text;
        }
        if (subview.Equals("Alternate")) {
          
          var tempUnit = alt.unit;

          alt.Dispose();

          alt = new AlternateUnitSensorProperty(sensor);

          alt.unit = tempUnit;

          altReading.Text = SensorUtils.ToFormattedString(alt.sensor.type, alt.modifiedMeasurement, true);      
        }
        if (subview.Equals("Rate")) {
          UpdateRoc(sensor);
        }
        if (subview.Equals("Superheat")) {
          shFluidType.Text = manifold.ptChart.fluid.name;
          var name = manifold.ptChart.fluid.name;
          shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(name));

          if(manifold.primarySensor.type == ESensorType.Pressure){
            var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.primarySensor.measurement, manifold.secondarySensor.measurement, false);
            shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
          } else {
            var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.secondarySensor.measurement, manifold.primarySensor.measurement, false);
            shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
          }
        }
        if (subview.Equals("Pressure")) {
          
        }
      }
    }

    public void manifoldUpdating(Manifold manifold){
      shFluidType.Text = manifold.ptChart.fluid.name;
      var name = manifold.ptChart.fluid.name;
      shFluidType.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(name));
      if (manifold.secondarySensor != null) {
        if (manifold.primarySensor.type == ESensorType.Pressure) {
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.primarySensor.measurement, manifold.secondarySensor.measurement, false);
          shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
        } else {
          var calculation = manifold.ptChart.CalculateSystemTemperatureDelta(manifold.secondarySensor.measurement, manifold.primarySensor.measurement, false);
          shReading.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
        }
      }
    }

	}
}

