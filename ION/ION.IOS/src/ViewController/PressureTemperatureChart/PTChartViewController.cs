#if False
namespace ION.IOS.ViewController.PressureTemperatureChart {

  using System;
  using System.Drawing;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Location;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.IOS.Devices;
  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.Dialog;
  using ION.IOS.ViewController.DeviceManager;
  using ION.IOS.ViewController.FluidManager;
  public delegate void onUnitChanged (Unit changedUnit);

  /// <summary>
  /// ONLY SET THE MANFOLD FOR THIS CLASS AFTER INSTANTIATION.
  /// </summary>
  public partial class PTChartViewController : BaseIONViewController {
    public event onUnitChanged pUnitChanged;
    public event onUnitChanged tUnitChanged;
    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;

    public SliderView ptSlider;

    public Manifold initialManifold { get; set; }
    /// <summary>
    /// The ptchart that provides the calculations for the controller.
    /// </summary>
    /// <value>The ptchart.</value>
    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        SynchronizePTChartWidgets();
      }
    } PTChart __ptChart;

    /// <summary>
    /// The sensor that will provide pressure readings for the view controller
    /// </summary>
    /// <value>The pressure sensor.</value>
    public Sensor pressureSensor { 
      get {
        return __pressureSensor;
      }
      set {
        if (__pressureSensor != null || __temperatureSensor != null) {
          entryMode?.Unbind();
          entryMode = null;
        }
        __pressureSensor = value;
        __temperatureSensor = null;

        if (__pressureSensor != null) {
          entryMode = new SensorEntryMode(this, __pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
        }

        InvalidateViewController();
      }
    } Sensor __pressureSensor;

    /// <summary>
    /// The sensor that will provide temperature readings for the view controller.
    /// </summary>
    /// <value>The temperature sensor.</value>
    public Sensor temperatureSensor {
      get {
        return __temperatureSensor;
      }
      set {
        if (__pressureSensor != null || __temperatureSensor != null) {
          entryMode?.Unbind();
          entryMode = null;
        }
        __temperatureSensor = value;
        __pressureSensor = null;

        if (__temperatureSensor != null) {
          entryMode = new SensorEntryMode(this, __temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
        }
        InvalidateViewController();
      }
    } Sensor __temperatureSensor;

    private Unit pressureUnit {
      get {
        return __pressureUnit;
      }
      set {
        __pressureUnit = value;
        buttonPressureUnit.SetTitle(value.ToString(), UIControlState.Normal);

        if (pUnitChanged != null) {
          pUnitChanged(value);
        }

        if (pressureSensor != null) {
          if (pressureSensor.isEditable) {
            pressureSensor.unit = value;
          }
        } else {
          if (entryMode != null) {
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
          }
        }
      }
    } Unit __pressureUnit;

    private Unit temperatureUnit {
      get {
        return __temperatureUnit;
      }
      set {
        __temperatureUnit = value;
        buttonTemperatureUnit.SetTitle(value.ToString(), UIControlState.Normal);

        if (tUnitChanged != null) {
          tUnitChanged(value);
        }

        if (temperatureSensor != null) {
          if (temperatureSensor.isEditable) {
            temperatureSensor.unit = value;
          }
        } else {
          if (entryMode != null) {
            entryMode = new SensorEntryMode(this,pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
          }
        }
      }
    } Unit __temperatureUnit;

    /// <summary>
    /// Whether or not the pressure sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if pressure sensor locked; otherwise, <c>false</c>.</value>
    public bool pressureSensorLocked {
      get {
        return ESensorType.Pressure == initialManifold?.primarySensor.type || temperatureSensor is GaugeDeviceSensor;
      }
    }

    /// <summary>
    /// Whether or not the temperature sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if temperature sensor locked; otherwise, <c>false</c>.</value>
    public bool temperatureSensorLocked {
      get {
        return ESensorType.Temperature == initialManifold?.primarySensor.type || pressureSensor is GaugeDeviceSensor;
      }
    }

    /// <summary>
    /// The ion applicaiton context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The entry mode that will dictate how the fields are entered.
    /// </summary>
    private SensorEntryMode entryMode {
      get {
        return __entryMode;
      }
      set {
        __entryMode?.Unbind();
        __entryMode = value;
      }
    } SensorEntryMode __entryMode;

    public PTChartViewController (IntPtr handle) : base (handle) {
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      if (initialManifold == null) {
        InitNavigationBar("ic_nav_pt_chart", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      }

      ion = AppState.context;

      ion.locationManager.onLocationChanged += DeltaOnLocationChanged;

      ptChart = PTChart.New(ion, Fluid.EState.Dew);

      pressureUnit = Units.Pressure.PSIG;
      temperatureUnit = Units.Temperature.FAHRENHEIT;

      InitPTChartWidgets();
      InitPressureWidgets();
      InitTemperatureWidgets();

      ClearPressureInput();
      ClearTemperatureInput();

      pressureSensor = new ManualSensor(ESensorType.Pressure, true);
      temperatureSensor = new ManualSensor(ESensorType.Temperature, true);

      if (initialManifold != null) {
        ptChart = initialManifold.ptChart;
        var sensor = initialManifold.primarySensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
        } else {
          throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
        }
      }

      var ptValueLabel = new UILabel(new CGRect(.05 * View.Bounds.Width, .7 * View.Bounds.Height, .9 * View.Bounds.Width, .3 * View.Bounds.Height));
      ptValueLabel.Lines = 0;

      var topMark = new UILabel(new CGRect(.5 * View.Bounds.Width - 1,.48 * View.Bounds.Height, 1,128 + (.04 * View.Bounds.Height)));
      topMark.BackgroundColor = UIColor.Black;

      ptSlider = new SliderView(View,ptChart, pressureUnit, temperatureUnit, temperatureSensor);
      ptSlider.pUnitLabel.Text = pressureUnit.ToString();
      ptSlider.tUnitLabel.Text = temperatureUnit.ToString();
      temperatureSensor.measurement = ptSlider.temperatureView.minTemperature.unit.OfScalar(ptSlider.temperatureView.minTemperature.amount);
      editTemperature.Text = temperatureSensor.measurement.amount.ToString("N");

      ptSlider.temperatureScroller.Scrolled += (sender, e) => {
        if(ptSlider.temperatureScroller.Dragging){
          if(temperatureSensor == null){
            Log.D(this, "temperature sensor was null");
            temperatureSensor = new ManualSensor(ESensorType.Temperature,true);
            temperatureSensor.unit = temperatureUnit;
          }

          var value = setTemperatureValueFromSlider();
          Console.WriteLine("using temperature unit: " + temperatureUnit + " and pressure unit: " + pressureUnit);
          var pressureValue = ptChart.GetPressure(new Scalar(temperatureUnit,value),true).ConvertTo(pressureUnit).amount;

          var pressureOffset = ptSlider.pressureView.pressTicks * (pressureValue - ptSlider.pressureView.minPressure);

          ptSlider.pressureScroller.ContentOffset = new CGPoint(pressureOffset,0);

          editTemperature.Text = value.ToString("N");
          editPressure.Text = pressureValue.ToString("N");

          ptValueLabel.Text = "temp Offset: " + ptSlider.temperatureScroller.ContentOffset.X + "\n" +
            "press Offset: " + pressureOffset + "\n" +
            "looking at temp: "+ value + "\n" +
            "temp ticks: " + ptSlider.temperatureView.tempTicks + "\n" +
            "press ticks: " + ptSlider.pressureView.pressTicks + "\n" + 
            "min temp: " + ptSlider.temperatureView.minTemperature.amount + " max temp: " + ptSlider.temperatureView.maxTemperature + "\n" +
            "pressure for " + value + temperatureUnit + " temp: " + pressureValue;
        }  
      };
       

      ptSlider.pressureScroller.Scrolled += (sender, e) => {

        if(ptSlider.pressureScroller.Dragging){
          if(pressureSensor == null){
            Log.D(this, "pressure sensor was null");
            pressureSensor = new ManualSensor(ESensorType.Pressure,true);
            pressureSensor.unit = pressureUnit;
          }

          var value = setPressureValueFromSlider();

          var temperatureValue = ptChart.GetTemperature(new Scalar(pressureUnit,value),true).ConvertTo(temperatureUnit).amount;
          var temperatureOffset = ptSlider.temperatureView.tempTicks * (temperatureValue - ptSlider.temperatureView.minTemperature.amount);

          ptSlider.temperatureScroller.ContentOffset = new CGPoint(temperatureOffset,0);

          editPressure.Text = value.ToString("N");
          editTemperature.Text = temperatureValue.ToString("N");

          ptValueLabel.Text = "Offset: " + ptSlider.pressureScroller.ContentOffset.X + "\n" +
            "looking at pressure: "+ value + "\n" +
            "press ticks: " + ptSlider.pressureView.pressTicks + "\n" +
            "min pressure: " + ptSlider.pressureView.minPressure + " max pressure: " + ptSlider.pressureView.maxPressure + "\n" +
            "temperature for " + value + pressureUnit + " press: " + temperatureValue;
        } 

 

      };
        
      View.AddSubview(ptSlider.temperatureScroller);
      View.AddSubview(ptSlider.pressureScroller);
      View.AddSubview(ptValueLabel);
      View.AddSubview(topMark);
    }

    // Overridden from ViewController
    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);

      if (pressureSensor == null) {
        ClearPressureInput();
      }

      if (temperatureSensor == null) {
        ClearTemperatureInput();
      }

      InvalidateViewController();

      SynchronizePressureIcons();
      SynchronizeTemperatureIcons();

      if (ptChart != null) {
        ptChart = PTChart.New(ion, ptChart.state);
      }
    }

    // Overridden from ViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);

      if (initialManifold != null) {
        initialManifold.ptChart = this.ptChart;
      }
    }

    /// <summary>
    /// Initializes the states and event handlers the for ptchart widgets
    /// </summary>
    private void InitPTChartWidgets() {
      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {
          ptChart = PTChart.New(ion, ptChart.state, fluid);

          if (pressureSensor is GaugeDeviceSensor) {
            entryMode = new SensorEntryMode(this,pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
          } else if (temperatureSensor is GaugeDeviceSensor){
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
          }

          ptSlider.temperatureView.resetData(ptChart,pressureUnit,temperatureUnit);
          ptSlider.pressureView.resetData(ptChart,pressureUnit,temperatureUnit);

          InvalidateViewController();
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_BUBBLE:
            ptChart.state = Fluid.EState.Bubble;
            break;
          case SECTION_DEW:
            ptChart.state = Fluid.EState.Dew;
            break;
        }
        ptSlider.temperatureView.resetData(ptChart,pressureUnit,temperatureUnit);
        ptSlider.pressureView.resetData(ptChart,pressureUnit,temperatureUnit);

        InvalidateViewController();
      };
    }

    /// <summary>
    /// Initializes the state and the event handlers for the pressure widgets.
    /// </summary>
    private void InitPressureWidgets() {
      imagePressureLock.Image = UIImage.FromBundle("ic_lock");

      viewPressureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureUnit = sensor.unit;
            pressureSensor = sensor;
            ptSlider.pressureScroller.ScrollEnabled = false;
            ptSlider.temperatureScroller.ScrollEnabled = false;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      editPressure.EditingDidEnd += (s, e) => {
      HAHAHAHA, trollololol. i'm kyle. i went on vacation and left poor old andy to have to do all the ios work. ya, well, this
      is his response to remind you that this code needs to work on both pressure and temperature. why, you may ask. well,
      kyle, cause it will reset the view the text is cleared and the idiot user didnt enter anything into the text field.
      THAT IS WHY, MISTER KYLE. now, if you will excuse me, i have the rest of the app to ruin. MUWHAHAHAHAHA! (>'.')>
      if (string.IsNullOrEmpty(editPressure.Text)) {
      pressureSensor.measurement = pressureSensor.unit.OfScalar(0);
      }
      };

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = null;
          ClearPressureInput();
          ClearTemperatureInput();
          ptSlider.pressureScroller.ScrollEnabled = true;
          ptSlider.temperatureScroller.ScrollEnabled = true;
        }
        InvalidateViewController();
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        if (pressureSensor == null) {
          pressureSensor = new ManualSensor(ESensorType.Pressure, true);
        }

        SetPressureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor == null || pressureSensor.isEditable) {
          var supportedUnits = pressureSensor != null ? pressureSensor.supportedUnits : SensorUtils.DEFAULT_PRESSURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            pressureUnit = unit;
            if(pressureSensor != null){
              entryMode = new SensorEntryMode(this,pressureSensor,temperatureUnit,ptChart,editPressure,editTemperature);
            } else {
              entryMode = new SensorEntryMode(this,temperatureSensor,pressureUnit,ptChart,editTemperature,editPressure);
            }
            ptSlider.temperatureView.resetData(ptChart,unit,temperatureUnit);
            ptSlider.pressureView.resetData(ptChart,unit,temperatureUnit);
            ptSlider.pUnitLabel.Text = unit.ToString();
            SetPressureMeasurementFromEditText();
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    /// <summary>
    /// Initializes the state and the event handlers for the temperature widgets.
    /// </summary>
    private void InitTemperatureWidgets() {
      imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");
      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureUnit = sensor.unit;
            temperatureSensor = sensor;
            ptSlider.temperatureScroller.ScrollEnabled = false;
            ptSlider.pressureScroller.ScrollEnabled = false;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          temperatureSensor = null;
          ClearPressureInput();
          ClearTemperatureInput();
          ptSlider.temperatureScroller.ScrollEnabled = true;
          ptSlider.pressureScroller.ScrollEnabled = true;
        }
        InvalidateViewController();
      }));

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.AddTarget((object obj, EventArgs args) => {
        if (temperatureSensor == null) {
          temperatureSensor = new ManualSensor(ESensorType.Temperature, true);
        }

        SetTemperatureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor == null || temperatureSensor.isEditable) {
          var supportedUnits = temperatureSensor != null ? temperatureSensor.supportedUnits : SensorUtils.DEFAULT_TEMPERATURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            temperatureUnit = unit;
            if(pressureSensor != null){
              entryMode = new SensorEntryMode(this,pressureSensor,temperatureUnit,ptChart,editPressure,editTemperature);
            } else {
              entryMode = new SensorEntryMode(this,temperatureSensor,pressureUnit,ptChart,editTemperature,editPressure);
            }
            ptSlider.temperatureView.resetData(ptChart,pressureUnit,unit);
            ptSlider.pressureView.resetData(ptChart,pressureUnit,unit);
            ptSlider.tUnitLabel.Text = unit.ToString();
            SetTemperatureMeasurementFromEditText();
          });
          PresentViewController(dialog, true, null);
        }
      };
    } 

    private void SetPressureMeasurementFromEditText() {
      try {
        var amount = double.Parse(editPressure.Text);
        pressureSensor.measurement = pressureSensor.unit.OfScalar(amount);
      } catch (Exception e) {
        Log.E(this, "Failed to set pressure", e);
        ClearPressureInput();
        ClearTemperatureInput();

        if (entryMode != null) {
          entryMode.Invalidate();
        }
      }
    }

    private void SetTemperatureMeasurementFromEditText() {
      try {
        var amount = double.Parse(editTemperature.Text);
        temperatureSensor.measurement = temperatureSensor.unit.OfScalar(amount);
      } catch (Exception e) {
        Log.E(this, "Failed to set temperature", e);
        ClearPressureInput();
        ClearTemperatureInput();

        if (entryMode != null) {
          entryMode.Invalidate();
        }
      }
    }

    /// <summary>
    /// Synchronizes the state of the fluid and the UI.
    /// </summary>
    private void SynchronizePTChartWidgets() {
      var fluid = ptChart.fluid;
      viewFluidColor.BackgroundColor = new UIColor(Colors.FromInt((uint)fluid.color));
      labelFluidName.Text = fluid.name;
      switchFluidState.Hidden = !fluid.mixture;
    }

    /// <summary>
    /// Synchronizes the state of the pressure icons to the pressure sensor.
    /// </summary>
    private void SynchronizePressureIcons() {
      if (pressureSensor is GaugeDeviceSensor) {
        var gds = pressureSensor as GaugeDeviceSensor;
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Hidden = false;
        imagePressureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Hidden = temperatureSensor is GaugeDeviceSensor;
        imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
      }
    }

    /// <summary>
    /// Synchronizes the icons for the temperature sensor. Note: this will affect the pressure lock
    /// icon if the temperature sensor is a gauge device sensor.
    /// </summary>
    private void SynchronizeTemperatureIcons() {
      if (temperatureSensor is GaugeDeviceSensor) {
        var gds = temperatureSensor as GaugeDeviceSensor;
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Hidden = false;
        imageTemperatureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Hidden = pressureSensor is GaugeDeviceSensor;
        imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
      }
    }

    /// <summary>
    /// Invalidates the view controller in a current sensor sensitive way.
    /// </summary>
    private void InvalidateViewController() {
      switch (ptChart.state) {
        case Fluid.EState.Bubble:
          switchFluidState.SelectedSegment = SECTION_BUBBLE;
          switchFluidState.TintColor = new UIColor(Colors.RED);
          break;
        case Fluid.EState.Dew:
          switchFluidState.SelectedSegment = SECTION_DEW;
          switchFluidState.TintColor = new UIColor(Colors.BLUE);
          break;
      }

      SynchronizePressureIcons();
      SynchronizeTemperatureIcons();

      if (entryMode != null) {
        entryMode.Invalidate();
      }

      var hasSensor = __pressureSensor is GaugeDeviceSensor || __temperatureSensor is GaugeDeviceSensor;
      editPressure.Enabled = !hasSensor;
      editTemperature.Enabled = !hasSensor;
    }

    /// <summary>
    /// Clears the pressure input without updating the pressure sensor's reading.
    /// </summary>
    private void ClearPressureInput() {
      editPressure.Text = "";
    }

    /// <summary>
    /// Clears the temperature input without updating the temperature sensor's reading.
    /// </summary>
    private void ClearTemperatureInput() {
      editTemperature.Text = "";
    }

    private class SensorEntryMode {
      PTChartViewController vc;
      /// <summary>
      /// The sensor that will drive the sensor mode.
      /// </summary>
      /// <value>The sensor.</value>
      Sensor sensor;
      /// <summary>
      /// The sensor for the conversion.
      /// </summary>
      /// <value>The other.</value>
      Unit otherUnit;
      /// <summary>
      /// The ptchart that will be used for pt lookups.
      /// </summary>
      PTChart ptChart;

      UITextField primaryTextView;
      UITextField derivedTextView;

      public SensorEntryMode(PTChartViewController vc, Sensor sensor, Unit otherUnit, PTChart ptChart, UITextField primary, UITextField derived) {
        // TODO ahodder@appioninc.com: This should probably be sensor type checked at some point.
        this.vc = vc;
        this.sensor = sensor;
        this.otherUnit = otherUnit;
        this.ptChart = ptChart;
        this.primaryTextView = primary;
        this.derivedTextView = derived;

        this.sensor.onSensorStateChangedEvent += OnSensorChanged;

        Invalidate();
      }

      public void Unbind() {
        sensor.onSensorStateChangedEvent -= OnSensorChanged;
      }

      public void Invalidate() {
        if (!(sensor is ManualSensor)) {
          primaryTextView.Text = sensor.ToFormattedString(false);
        }
        Scalar measurement;
        switch (sensor.type) {
          case ESensorType.Pressure:
            measurement = ptChart.GetTemperature(sensor).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, measurement); 
            vc.pressureUnit = sensor.unit;
            break;
          case ESensorType.Temperature:
            measurement = ptChart.GetPressure(sensor).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, measurement); 
            vc.temperatureUnit = sensor.unit;
            break;
        }
      }

      protected void OnSensorChanged(Sensor sensor) {
        Invalidate();
      }
    }
    /// <summary>
    /// Takes the offset of the slider and translates it into the current unit's value at that point in the slider
    /// then it sets the edit text and calls the method to calculate the temperature equivalent
    /// </summary>
    private double setTemperatureValueFromSlider(){
      if(ptSlider.temperatureScroller.ContentOffset.X >= 0 && ptSlider.temperatureScroller.ContentOffset.X < ptSlider.temperatureView.measurementWidth){
        var temperatureValue = ptSlider.temperatureScroller.ContentOffset.X / ptSlider.temperatureView.tempTicks + ptSlider.temperatureView.minTemperature.amount;

        return temperatureValue;
      }
      return double.Parse(editTemperature.Text);
    }

    private double setPressureValueFromSlider(){
      if(ptSlider.pressureScroller.ContentOffset.X >= 0 && ptSlider.pressureScroller.ContentOffset.X < ptSlider.pressureView.measurementWidth){
        var pressureValue = ptSlider.pressureScroller.ContentOffset.X / ptSlider.pressureView.pressTicks + ptSlider.pressureView.minPressure;

        return pressureValue;
      }
      return double.Parse(editPressure.Text);
    }

    public void DeltaOnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
      InvalidateViewController();
    }
  }
}
#else
namespace ION.IOS.ViewController.PressureTemperatureChart {

  using System;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Location;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.IOS.Devices;
  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.Dialog;
  using ION.IOS.ViewController.DeviceManager;
  using ION.IOS.ViewController.FluidManager;
  public delegate void onUnitChanged (Unit changedUnit);

  /// <summary>
  /// ONLY SET THE MANFOLD FOR THIS CLASS AFTER INSTANTIATION.
  /// </summary>
  public partial class PTChartViewController : BaseIONViewController {
    public event onUnitChanged pUnitChanged;
    public event onUnitChanged tUnitChanged;
    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;

    public Manifold initialManifold { get; set; }
    /// <summary>
    /// The ptchart that provides the calculations for the controller.
    /// </summary>
    /// <value>The ptchart.</value>
    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        SynchronizePTChartWidgets();
      }
    } PTChart __ptChart;

    /// <summary>
    /// The sensor that will provide pressure readings for the view controller
    /// </summary>
    /// <value>The pressure sensor.</value>
    public Sensor pressureSensor { 
      get {
        return __pressureSensor;
      }
      set {
        if (__pressureSensor != null || __temperatureSensor != null) {
          entryMode?.Unbind();
          entryMode = null;
        }
        __pressureSensor = value;
        __temperatureSensor = null;

        if (__pressureSensor != null) {
          entryMode = new SensorEntryMode(this, __pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
        }

        InvalidateViewController();
      }
    } Sensor __pressureSensor;

    /// <summary>
    /// The sensor that will provide temperature readings for the view controller.
    /// </summary>
    /// <value>The temperature sensor.</value>
    public Sensor temperatureSensor {
      get {
        return __temperatureSensor;
      }
      set {
        if (__pressureSensor != null || __temperatureSensor != null) {
          entryMode?.Unbind();
          entryMode = null;
        }
        __temperatureSensor = value;
        __pressureSensor = null;

        if (__temperatureSensor != null) {
          entryMode = new SensorEntryMode(this, __temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
        }
        InvalidateViewController();
      }
    } Sensor __temperatureSensor;

    private Unit pressureUnit {
      get {
        return __pressureUnit;
      }
      set {
        __pressureUnit = value;
        buttonPressureUnit.SetTitle(value.ToString(), UIControlState.Normal);

        if (pUnitChanged != null) {
          pUnitChanged(value);
        }

        if (pressureSensor != null) {
          if (pressureSensor.isEditable) {
            pressureSensor.unit = value;
          }
        } else {
          if (entryMode != null) {
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
          }
        }
      }
    } Unit __pressureUnit;

    private Unit temperatureUnit {
      get {
        return __temperatureUnit;
      }
      set {
        __temperatureUnit = value;
        buttonTemperatureUnit.SetTitle(value.ToString(), UIControlState.Normal);

        if (tUnitChanged != null) {
          tUnitChanged(value);
        }

        if (temperatureSensor != null) {
          if (temperatureSensor.isEditable) {
            temperatureSensor.unit = value;
          }
        } else {
          if (entryMode != null) {
            entryMode = new SensorEntryMode(this,pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
          }
        }
      }
    } Unit __temperatureUnit;

    /// <summary>
    /// Whether or not the pressure sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if pressure sensor locked; otherwise, <c>false</c>.</value>
    public bool pressureSensorLocked {
      get {
        return ESensorType.Pressure == initialManifold?.primarySensor.type || temperatureSensor is GaugeDeviceSensor;
      }
    }

    /// <summary>
    /// Whether or not the temperature sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if temperature sensor locked; otherwise, <c>false</c>.</value>
    public bool temperatureSensorLocked {
      get {
        return ESensorType.Temperature == initialManifold?.primarySensor.type || pressureSensor is GaugeDeviceSensor;
      }
    }

    /// <summary>
    /// The ion applicaiton context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The entry mode that will dictate how the fields are entered.
    /// </summary>
    private SensorEntryMode entryMode {
      get {
        return __entryMode;
      }
      set {
        __entryMode?.Unbind();
        __entryMode = value;
      }
    } SensorEntryMode __entryMode;

    public PTChartViewController (IntPtr handle) : base (handle) {
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      if (initialManifold == null) {
        InitNavigationBar("ic_nav_pt_chart", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      }

      ion = AppState.context;

      ion.locationManager.onLocationChanged += DeltaOnLocationChanged;

      ptChart = PTChart.New(ion, Fluid.EState.Dew);

      pressureUnit = Units.Pressure.PSIG;
      temperatureUnit = Units.Temperature.FAHRENHEIT;

      InitPTChartWidgets();
      InitPressureWidgets();
      InitTemperatureWidgets();

      ClearPressureInput();
      ClearTemperatureInput();

      if (initialManifold != null) {
        ptChart = initialManifold.ptChart;
        var sensor = initialManifold.primarySensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
        } else {
          throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
        }
      }
    }

    // Overridden from ViewController
    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);

      if (pressureSensor == null) {
        ClearPressureInput();
      }

      if (temperatureSensor == null) {
        ClearTemperatureInput();
      }

      InvalidateViewController();

      SynchronizePressureIcons();
      SynchronizeTemperatureIcons();

      if (ptChart != null) {
        ptChart = PTChart.New(ion, ptChart.state);
      }
    }

    // Overridden from ViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);

      if (initialManifold != null) {
        initialManifold.ptChart = this.ptChart;
      }
    }

    /// <summary>
    /// Initializes the states and event handlers the for ptchart widgets
    /// </summary>
    private void InitPTChartWidgets() {
      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {
          ptChart = PTChart.New(ion, ptChart.state, fluid);

          if (pressureSensor is GaugeDeviceSensor) {
            entryMode = new SensorEntryMode(this,pressureSensor, temperatureUnit, ptChart, editPressure, editTemperature);
          } else if (temperatureSensor is GaugeDeviceSensor){
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, ptChart, editTemperature, editPressure);
          }

          InvalidateViewController();
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_BUBBLE:
            ptChart.state = Fluid.EState.Bubble;
            break;
          case SECTION_DEW:
            ptChart.state = Fluid.EState.Dew;
            break;
        }
        InvalidateViewController();
      };
    }

    /// <summary>
    /// Initializes the state and the event handlers for the pressure widgets.
    /// </summary>
    private void InitPressureWidgets() {
      imagePressureLock.Image = UIImage.FromBundle("ic_lock");

      viewPressureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureUnit = sensor.unit;
            pressureSensor = sensor;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = null;
          ClearPressureInput();
          ClearTemperatureInput();
        }
        InvalidateViewController();
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.EditingDidEnd += (s, e) => {
        if (string.IsNullOrEmpty(editPressure.Text)) {
          entryMode.sensor.measurement = entryMode.sensor.unit.OfScalar(0);
        }
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        if (pressureSensor == null) {
          pressureSensor = new ManualSensor(ESensorType.Pressure, true);
        }

        SetPressureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor == null || pressureSensor.isEditable) {
          var supportedUnits = pressureSensor != null ? pressureSensor.supportedUnits : SensorUtils.DEFAULT_PRESSURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            pressureUnit = unit;
            SetPressureMeasurementFromEditText();
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    /// <summary>
    /// Initializes the state and the event handlers for the temperature widgets.
    /// </summary>
    private void InitTemperatureWidgets() {
      imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");
      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureUnit = sensor.unit;
            temperatureSensor = sensor;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          temperatureSensor = null;
          ClearPressureInput();
          ClearTemperatureInput();
        }
        InvalidateViewController();
      }));

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.EditingDidEnd += (s, e) => {
        if (string.IsNullOrEmpty(editTemperature.Text)) {
          entryMode.sensor.measurement = entryMode.sensor.unit.OfScalar(0);
        }
      };

      editTemperature.AddTarget((object obj, EventArgs args) => {
        if (temperatureSensor == null) {
          temperatureSensor = new ManualSensor(ESensorType.Temperature, true);
        }

        SetTemperatureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor == null || temperatureSensor.isEditable) {
          var supportedUnits = temperatureSensor != null ? temperatureSensor.supportedUnits : SensorUtils.DEFAULT_TEMPERATURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            temperatureUnit = unit;
            SetTemperatureMeasurementFromEditText();
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    private void SetPressureMeasurementFromEditText() {
      try {
        var amount = double.Parse(editPressure.Text);
        pressureSensor.measurement = pressureSensor.unit.OfScalar(amount);
      } catch (Exception e) {
        Log.E(this, "Failed to set pressure", e);
        ClearPressureInput();
        ClearTemperatureInput();

        if (entryMode != null) {
          entryMode.Invalidate();
        }
      }
    }

    private void SetTemperatureMeasurementFromEditText() {
      try {
        var amount = double.Parse(editTemperature.Text);
        Console.WriteLine("gathered measurement: " + amount);
        Console.WriteLine("using unit gives amount: " + temperatureSensor.unit.OfScalar(amount).amount);
        temperatureSensor.measurement = temperatureSensor.unit.OfScalar(amount);
      } catch (Exception e) {
        Log.E(this, "Failed to set temperature", e);
        ClearPressureInput();
        ClearTemperatureInput();

        if (entryMode != null) {
          entryMode.Invalidate();
        }
      }
    }

    /// <summary>
    /// Synchronizes the state of the fluid and the UI.
    /// </summary>
    private void SynchronizePTChartWidgets() {
      var fluid = ptChart.fluid;
      viewFluidColor.BackgroundColor = new UIColor(Colors.FromInt((uint)fluid.color));
      labelFluidName.Text = fluid.name;
      switchFluidState.Hidden = !fluid.mixture;
    }

    /// <summary>
    /// Synchronizes the state of the pressure icons to the pressure sensor.
    /// </summary>
    private void SynchronizePressureIcons() {
      if (pressureSensor is GaugeDeviceSensor) {
        var gds = pressureSensor as GaugeDeviceSensor;
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Hidden = false;
        imagePressureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Hidden = temperatureSensor is GaugeDeviceSensor;
        imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
      }
    }

    /// <summary>
    /// Synchronizes the icons for the temperature sensor. Note: this will affect the pressure lock
    /// icon if the temperature sensor is a gauge device sensor.
    /// </summary>
    private void SynchronizeTemperatureIcons() {
      if (temperatureSensor is GaugeDeviceSensor) {
        var gds = temperatureSensor as GaugeDeviceSensor;
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Hidden = false;
        imageTemperatureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Hidden = pressureSensor is GaugeDeviceSensor;
        imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
      }
    }

    /// <summary>
    /// Invalidates the view controller in a current sensor sensitive way.
    /// </summary>
    private void InvalidateViewController() {
      switch (ptChart.state) {
        case Fluid.EState.Bubble:
          switchFluidState.SelectedSegment = SECTION_BUBBLE;
          switchFluidState.TintColor = new UIColor(Colors.RED);
          break;
        case Fluid.EState.Dew:
          switchFluidState.SelectedSegment = SECTION_DEW;
          switchFluidState.TintColor = new UIColor(Colors.BLUE);
          break;
      }

      SynchronizePressureIcons();
      SynchronizeTemperatureIcons();

      if (entryMode != null) {
        entryMode.Invalidate();
      }

      var hasSensor = __pressureSensor is GaugeDeviceSensor || __temperatureSensor is GaugeDeviceSensor;
      editPressure.Enabled = !hasSensor;
      editTemperature.Enabled = !hasSensor;
    }

    /// <summary>
    /// Clears the pressure input without updating the pressure sensor's reading.
    /// </summary>
    private void ClearPressureInput() {
      editPressure.Text = "";
    }

    /// <summary>
    /// Clears the temperature input without updating the temperature sensor's reading.
    /// </summary>
    private void ClearTemperatureInput() {
      editTemperature.Text = "";
    }

    private class SensorEntryMode {
      PTChartViewController vc;
      /// <summary>
      /// The sensor that will drive the sensor mode.
      /// </summary>
      /// <value>The sensor.</value>
      public Sensor sensor;
      /// <summary>
      /// The sensor for the conversion.
      /// </summary>
      /// <value>The other.</value>
      Unit otherUnit;
      /// <summary>
      /// The ptchart that will be used for pt lookups.
      /// </summary>
      PTChart ptChart;

      UITextField primaryTextView;
      UITextField derivedTextView;

      public SensorEntryMode(PTChartViewController vc, Sensor sensor, Unit otherUnit, PTChart ptChart, UITextField primary, UITextField derived) {
        // TODO ahodder@appioninc.com: This should probably be sensor type checked at some point.
        this.vc = vc;
        this.sensor = sensor;
        this.otherUnit = otherUnit;
        this.ptChart = ptChart;
        this.primaryTextView = primary;
        this.derivedTextView = derived;

        this.sensor.onSensorStateChangedEvent += OnSensorChanged;

        Invalidate();
      }

      public void Unbind() {
        sensor.onSensorStateChangedEvent -= OnSensorChanged;
      }

      public void Invalidate() {
        if (!(sensor is ManualSensor)) {
          primaryTextView.Text = sensor.ToFormattedString(false);
        }

        Scalar measurement;
        switch (sensor.type) {
          case ESensorType.Pressure:
            measurement = ptChart.GetTemperature(sensor).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, measurement); 
            vc.pressureUnit = sensor.unit;
            break;
          case ESensorType.Temperature:
            measurement = ptChart.GetPressure(sensor).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, measurement); 
            vc.temperatureUnit = sensor.unit;
            break;
        }
      }

      protected void OnSensorChanged(Sensor sensor) {
        Invalidate();
      }

    }
    public void DeltaOnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
      InvalidateViewController();
    }
  }
}

#endif