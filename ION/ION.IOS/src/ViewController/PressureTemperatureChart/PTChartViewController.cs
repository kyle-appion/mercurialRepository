namespace ION.IOS.ViewController.PressureTemperatureChart {

  using System;

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
        SynchronizePressureIcons();

        if (__pressureSensor != null) {
          __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
          SynchronizePressureMeasurement(pressureSensor.measurement);
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
        SynchronizeTemperatureIcons();

        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
          SynchronizeTemperatureMeasurement(temperatureSensor.measurement);
        }
      }
    } Sensor __temperatureSensor;

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
      ptChart = PTChart.New(ion, Fluid.EState.Bubble);

      InitPTChartWidgets();
      InitPressureWidgets();
      InitTemperatureWidgets();

      if (initialManifold != null) {
        ptChart = initialManifold.ptChart;
        var sensor = initialManifold.primarySensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
          temperatureSensor = new Sensor(ESensorType.Temperature);
        } else if (ESensorType.Temperature == sensor.type) {
          pressureSensor = new Sensor(ESensorType.Pressure);
          temperatureSensor = initialManifold.primarySensor;
        } else {
          throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
        }
      } else {
        pressureSensor = new Sensor(ESensorType.Pressure);
        temperatureSensor = new Sensor(ESensorType.Temperature);
        ClearPressureInput();
        ClearTemperatureInput();
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
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureSensor = sensor;
            editPressure.Enabled = false;
            editTemperature.Enabled = false;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          pressureSensor = new Sensor(ESensorType.Pressure, true, true);
          editPressure.Enabled = true;
          editTemperature.Enabled = true;
          InvalidateViewController();
        }
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        try {
          if ("".Equals(editPressure.Text)) {
            ClearTemperatureInput();
          } else {
            pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;

            var amount = double.Parse(editPressure.Text);
            pressureSensor.measurement = pressureSensor.unit.OfScalar(amount);

            pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;

            UpdateTemperatureMeasurement();
          }
        } catch (Exception e) {
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.supportedUnits, (obj, unit) => {
            if (editPressure.Text == null || "".Equals(editPressure.Text)) {
              pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;

              pressureSensor.unit = unit;
              buttonPressureUnit.SetTitle(unit.ToString(), UIControlState.Normal);

              pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
            } else {
              pressureSensor.unit = unit;
            }
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
        if (!(pressureSensor is GaugeDeviceSensor)) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureSensor = sensor;
            editPressure.Enabled = false;
            editTemperature.Enabled = false;
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!(pressureSensor is GaugeDeviceSensor)) {
          temperatureSensor = new Sensor(ESensorType.Temperature, true, true);
          editPressure.Enabled = true;
          editTemperature.Enabled = true;
          InvalidateViewController();
        }
      }));

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.AddTarget((object obj, EventArgs args) => {
        try {
          if ("".Equals(editTemperature.Text)) {
            ClearPressureInput();
          } else {
            temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;

            var amount = double.Parse(editTemperature.Text);
            temperatureSensor.measurement = temperatureSensor.unit.OfScalar(amount);

            temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;

            UpdatePressureMeasurement();
          }
        } catch (Exception e) {
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.supportedUnits, (obj, unit) => {
            if (editTemperature.Text == null || "".Equals(editTemperature.Text)) {
              temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;

              temperatureSensor.unit = unit;
              buttonTemperatureUnit.SetTitle(unit.ToString(), UIControlState.Normal);

              temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
            } else {
              temperatureSensor.unit = unit;
            }
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
      var expectedPressure = ptChart.GetPressure(temperatureSensor.measurement);
      var convertedPressure = expectedPressure.ConvertTo(pressureSensor.unit);

      SynchronizePressureMeasurement(ptChart.GetPressure(temperatureSensor.measurement).ConvertTo(pressureSensor.unit));
    }

    /// <summary>
    /// Synchronizes the UI with the given pressure measurement.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    private void SynchronizePressureMeasurement(Scalar measurement) {
      editPressure.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, measurement);
      buttonPressureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);
    }

    /// <summary>
    /// Synchronizes the icons for the pressure sensor. Note: this will affect the temperature lock
    /// icon if the pressure sensor is a gauge device sensor.
    /// </summary>
    private void SynchronizePressureIcons() {
      if (pressureSensor is GaugeDeviceSensor) {
        var gds = pressureSensor as GaugeDeviceSensor;
        imageTemperatureLock.Hidden = false;
        imageTemperatureIcon.Hidden = true;
        imagePressureLock.Hidden = true;
        imagePressureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        // Prevents the lock from persisting when the gds is removed
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          imagePressureLock.Hidden = true;
        }
        imagePressureIcon.Hidden = false;
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
      SynchronizeTemperatureMeasurement(ptChart.GetTemperature(pressureSensor.measurement).ConvertTo(temperatureSensor.unit));
    }

    /// <summary>
    /// Synchronizes the UI with the given temperature measurement.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    private void SynchronizeTemperatureMeasurement(Scalar measurement) {
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
        imagePressureLock.Hidden = false;
        imagePressureIcon.Hidden = true;
        imageTemperatureLock.Hidden = true;
        imageTemperatureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        // Prevents the lock from persisting when the gds is removed
        if (!(pressureSensor is GaugeDeviceSensor)) {
          imageTemperatureLock.Hidden = true;
        }
        imageTemperatureIcon.Hidden = false;
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

      if (pressureSensor is GaugeDeviceSensor) {
        UpdateTemperatureMeasurement();
        SynchronizePressureIcons();
      } else if (temperatureSensor is GaugeDeviceSensor) {
        UpdatePressureMeasurement();
        SynchronizeTemperatureIcons();
      } else {
        SynchronizePressureIcons();
        SynchronizeTemperatureIcons();
        ClearPressureInput();
        ClearTemperatureInput();
      }
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
