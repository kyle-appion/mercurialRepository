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
  }
}
