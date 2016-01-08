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

  public partial class PTChartViewController : BaseIONViewController {

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
        if (__pressureSensor != null) {
          __pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;
        }

        __pressureSensor = value;

        if (__pressureSensor != null) {
          __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
          SynchronizePressureMeasurement(pressureSensor.measurement);
        } else {
          ClearPressureInput();
        }

        SynchronizePressureIcons();
        SynchronizeTemperatureIcons();

        if (__pressureSensor is GaugeDeviceSensor) {
          editPressure.Enabled = false;
          editTemperature.Enabled = false;
        } else {
          editPressure.Enabled = true;
          if (temperatureSensor is GaugeDeviceSensor) {
            editTemperature.Enabled = false;
          } else {
            editTemperature.Enabled = true;
          }
        }
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
        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;
        }

        __temperatureSensor = value;

        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
          SynchronizeTemperatureMeasurement(temperatureSensor.measurement);
        } else {
          ClearTemperatureInput();
        }

        SynchronizePressureIcons();
        SynchronizeTemperatureIcons();

        if (__temperatureSensor is GaugeDeviceSensor) {
          editTemperature.Enabled = false;
          editPressure.Enabled = false;
        } else {
          editTemperature.Enabled = true;
          if (pressureSensor is GaugeDeviceSensor) {
            editPressure.Enabled = false;
          } else {
            editPressure.Enabled = true;
          }
        }
      }
    } Sensor __temperatureSensor;

    private Unit pressureUnit { get; set; }
    private Unit temperatureUnit { get; set; }

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

      pressureSensor = new Sensor(ESensorType.Pressure, true, true);
      temperatureSensor = new Sensor(ESensorType.Temperature, false, true);

      pressureSensor.unit = pressureUnit;
      temperatureSensor.unit = temperatureUnit;

      InitPTChartWidgets();
      InitPressureWidgets();
      InitTemperatureWidgets();

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

      ClearPressureInput();
      ClearTemperatureInput();
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
          dm.displayFilter = new SensorTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureSensor = sensor;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = new Sensor(ESensorType.Pressure, true, true);
          InvalidateViewController();
        }
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        try {
          if (editTemperature.IsEditing && "".Equals(editPressure.Text)) {
            ClearTemperatureInput();
          } else {
            pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;

            var amount = double.Parse(editPressure.Text);
            pressureSensor.measurement = pressureSensor.unit.OfScalar(amount);

            pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;

            UpdateTemperatureMeasurement();
          }
        } catch (Exception e) {
          Log.E(this, "Failed to set pressure", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.supportedUnits, (obj, unit) => {
            var old = pressureSensor.measurement;
            Log.D(this, "Converting " + old + " to " + unit + " equals: " + old.ConvertTo(unit));

            pressureUnit = unit;
            SynchronizePressureMeasurement(pressureSensor.measurement.ConvertTo(pressureUnit));
            buttonPressureUnit.SetTitle(unit.ToString(), UIControlState.Normal);
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
          dm.displayFilter = new SensorTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureSensor = sensor;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          temperatureSensor = new Sensor(ESensorType.Temperature, true, true);
          InvalidateViewController();
        }
      }));

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.AddTarget((object obj, EventArgs args) => {
        try {
          if (editTemperature.IsEditing && "".Equals(editTemperature.Text)) {
            ClearPressureInput();
          } else {
            temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;

            var amount = double.Parse(editTemperature.Text);
            temperatureSensor.measurement = temperatureSensor.unit.OfScalar(amount);

            temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;

            UpdatePressureMeasurement();
          }
        } catch (Exception e) {
          Log.E(this, "Failed to set temperature", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.supportedUnits, (obj, unit) => {
            temperatureUnit = unit;
            SynchronizeTemperatureMeasurement(temperatureSensor.measurement.ConvertTo(temperatureUnit));
            buttonTemperatureUnit.SetTitle(unit.ToString(), UIControlState.Normal);
          });
          PresentViewController(dialog, true, null);
        }
      };
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
    /// Called when the pressure sensor's measurement is changed.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnPressureSensorChanged(Sensor sensor) {
      SynchronizePressureMeasurement(sensor.measurement);
      UpdateTemperatureMeasurement();
    }

    /// <summary>
    /// Updates the pressure's displated measurement based on the temperatureSensor's measurement.
    /// </summary>
    private void UpdatePressureMeasurement() {
      SynchronizePressureMeasurement(ptChart.GetPressure(temperatureSensor.measurement).ConvertTo(pressureUnit));
    }

    /// <summary>
    /// Synchronizes the UI with the given pressure measurement.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    private void SynchronizePressureMeasurement(Scalar measurement) {
      Log.D(this, "Pressure sensor measurement: " + measurement);
      pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;
      pressureUnit = measurement.unit;
      if (pressureSensor.isEditable) {
        pressureSensor.measurement = measurement;
      }
      pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
      editPressure.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, measurement);
      buttonPressureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);
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
    /// Called when the temperature sensor's measurement is changed.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnTemperatureSensorChanged(Sensor sensor) {
      SynchronizeTemperatureMeasurement(sensor.measurement);
      UpdatePressureMeasurement();
    }

    /// <summary>
    /// Updates the temperature's displayed measurement with the saturated temperature
    /// at the pressureSensor's measurement.
    /// </summary>
    private void UpdateTemperatureMeasurement() {
      SynchronizeTemperatureMeasurement(ptChart.GetTemperature(pressureSensor.measurement).ConvertTo(temperatureUnit));
    }

    /// <summary>
    /// Synchronizes the UI with the given temperature measurement.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    private void SynchronizeTemperatureMeasurement(Scalar measurement) {
      temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;
      temperatureUnit = measurement.unit;
      if (temperatureSensor.isEditable) {
        temperatureSensor.measurement = measurement;
      }
      temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
      editTemperature.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, measurement);  
      buttonTemperatureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);
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
    private async void InvalidateViewController() {
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

      if (pressureSensor is GaugeDeviceSensor) {
        SynchronizePressureMeasurement(pressureSensor.measurement);
        UpdateTemperatureMeasurement();
      } else if (temperatureSensor is GaugeDeviceSensor) {
        UpdatePressureMeasurement();
        SynchronizeTemperatureMeasurement(temperatureSensor.measurement);
      } else {
        ClearPressureInput();
        ClearTemperatureInput();
      }

      SynchronizePressureIcons();
      SynchronizeTemperatureIcons();
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
  }
}
