using System;

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
using ION.IOS.ViewController;
using ION.IOS.ViewController.Dialog;
using ION.IOS.ViewController.DeviceManager;
using ION.IOS.ViewController.FluidManager;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public partial class PTChartViewController : BaseIONViewController {

    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;

    public Manifold initialManifold { get; set; }

    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        labelFluidName.Text = ptChart.fluid.name;
        viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ptChart.fluid.color);
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

        if (initialManifold != null) {
          initialManifold.ptChart = ptChart;
        }
      }
    } PTChart __ptChart;

    public Sensor pressureSensor {
      get {
        return __pressureSensor;
      }
      set {
        if (__pressureSensor != null) {
          __pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;
        }

        if (value == null) {
          value = new Sensor(ESensorType.Pressure, true, true);
          value.unit = ion.defaultUnits.pressure;
        }

        __pressureSensor = value;

        if (__pressureSensor is GaugeDeviceSensor) {
          ClearPressureInput();
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__pressureSensor;
          imagePressureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          imageTemperatureIcon.Hidden = true;
        } else {
          imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
          imageTemperatureIcon.Hidden = false;
        }
        UpdateUILocks();

        __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;

        if (__pressureSensor != null && __temperatureSensor != null) {
          var enabled = __pressureSensor.isEditable && __temperatureSensor.isEditable;;
          editPressure.Enabled = enabled;
          editTemperature.Enabled = enabled;
        }

        UpdatePressureDisplay();
      }
    } Sensor __pressureSensor;

    public bool isPressureSensorChangable {
      get {
        return initialManifold?.primarySensor != pressureSensor;
      }
    }

    public Sensor temperatureSensor {
      get {
        return __temperatureSensor;
      }
      set {
        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;
        }

        if (value == null) {
          value = new Sensor(ESensorType.Temperature, false, true);
          value.unit = ion.defaultUnits.temperature;
        }

        __temperatureSensor = value;

        if (__temperatureSensor is GaugeDeviceSensor) {
          ClearTemperatureInput();
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__temperatureSensor;
          imageTemperatureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          imagePressureIcon.Hidden = true;
        } else {
          imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
          imagePressureIcon.Hidden = false;
        }
        UpdateUILocks();

        __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;

        if (__pressureSensor != null && __temperatureSensor != null) {
          var enabled = __pressureSensor.isEditable && __temperatureSensor.isEditable;;
          editPressure.Enabled = enabled;
          editTemperature.Enabled = enabled;
        }

        UpdateTemperatureDisplay();
      }
    } Sensor __temperatureSensor;

    public bool isTemperatureSensorChangable {
      get {
        return initialManifold?.primarySensor != temperatureSensor;
      }
    }

    /// <summary>
    /// The ion instance for the view controller.
    /// </summary>
    private readonly IION ion = AppState.context;

    public PTChartViewController (IntPtr handle) : base (handle) {
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      if (initialManifold == null) {
        InitNavigationBar("ic_nav_pt_chart", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      } else {
        NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;
      }

//      NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      /*
      View.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editPressure.ResignFirstResponder();
        editTemperature.ResignFirstResponder();
      }));
      */
      InitializeFluidControlWidgets();
      InitializePressureWidgets();
      InitializeTemperatureWidgets();

      if (initialManifold != null) {
        ptChart = initialManifold.ptChart;
        var sensor = initialManifold.primarySensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
          temperatureSensor = null;
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
          pressureSensor = null;
        } else {
          // TODO ahodder@appioninc.com: Throw a valid, user-friendly exception.
          throw new Exception("Cannot accept sensor that is not a pressure/temperature sensor.");
        }
        InvalidateViewController();
      } else {
        // Lazily let the setters initialize the sensors.
        ptChart = new PTChart(Fluid.EState.Dew, ion.fluidManager.lastUsedFluid, ion.locationManager.lastKnownLocation.altitude);
        pressureSensor = null;
        temperatureSensor = null;
        ClearPressureInput();
        ClearTemperatureInput();
      }
    }

    /// <summary>
    /// Initializes all of view controller's fluid control widgets.
    /// </summary>
    private void InitializeFluidControlWidgets() {
      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {
          ptChart = new ION.Core.Fluids.PTChart(ptChart.state, fluid, this.ptChart.elevation);
          InvalidateViewController();
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        Log.D(this, "Altitude: " + ion.locationManager.lastKnownLocation.altitude);
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_DEW:
            ptChart = new ION.Core.Fluids.PTChart(Fluid.EState.Dew, ptChart.fluid, ion.locationManager.lastKnownLocation.altitude);
            break;
          case SECTION_BUBBLE:
            ptChart = new ION.Core.Fluids.PTChart(Fluid.EState.Bubble, ptChart.fluid, ion.locationManager.lastKnownLocation.altitude);
            break;
        }
        InvalidateViewController();
      };
    }

    /// <summary>
    /// Initializes all of the initial settings and properties for the view controller's
    /// pressure widgets.
    /// </summary>
    private void InitializePressureWidgets() {
      viewPressureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
        dm.displayFilter = new SensorTypeFilter(ESensorType.Pressure);
        dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
          pressureSensor = sensor;
        };
        NavigationController.PushViewController(dm, true);
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        pressureSensor = new Sensor(ESensorType.Pressure, true, true);
        ClearPressureInput();
        ClearTemperatureInput();
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

            UpdateTemperatureWithCurrentPressure();
            Log.D(this, "PressureSensor.measurement: " + pressureSensor.measurement);
          }
        } catch (Exception e) {
          Log.E(this, "Failed to format: " + editPressure.Text + " into a scalar.", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.supportedUnits, (obj, unit) => {
            pressureSensor.measurement = pressureSensor.measurement.ConvertTo(unit);
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    /// <summary>
    /// Initializes all of the initial settings and properties for the view controller's
    /// temperature widgets.
    /// </summary>
    private void InitializeTemperatureWidgets() {
      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
        dm.displayFilter = new SensorTypeFilter(ESensorType.Temperature);
        dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
          temperatureSensor = sensor;
        };
        NavigationController.PushViewController(dm, true);
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        temperatureSensor = new Sensor(ESensorType.Temperature, false, true);
        ClearTemperatureInput();
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

            UpdatePressureWithCurrentTemperature();
          }
        } catch (Exception e) {
          Log.E(this, "Failed to format: " + editTemperature.Text + " into a scalar.", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.supportedUnits, (obj, unit) => {
            temperatureSensor.measurement = temperatureSensor.measurement.ConvertTo(unit);
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    /// <summary>
    /// Called when the pressure sensor's reading changes.
    /// </summary>
    private void OnPressureSensorChanged(Sensor sensor) {
      UpdatePressureDisplay();
      UpdateTemperatureWithCurrentPressure();
    }

    /// <summary>
    /// Called when the temperature sensor's reading changes.
    /// </summary>
    private void OnTemperatureSensorChanged(Sensor sensor) {
      UpdateTemperatureDisplay();
      UpdatePressureWithCurrentTemperature();
    }

    /// <summary>
    /// Updates the edit pressure view to the formated string measurement of the
    /// pressure sensor. Further, the button pressure units will be updated to the
    /// pressure's current unit.
    /// </summary>
    private void UpdatePressureDisplay() {
      editPressure.Text = pressureSensor.ToFormattedString();
      buttonPressureUnit.SetTitle(pressureSensor.measurement.unit.ToString(), UIControlState.Normal);
    }

    /// <summary>
    /// Updates the edit temperature view to the formatted string measurement of
    /// the temperature sensor. Further, the button temperature units will be updated
    /// to the temperature's current unit.
    /// </summary>
    private void UpdateTemperatureDisplay() {
      editTemperature.Text = temperatureSensor.ToFormattedString();
      buttonTemperatureUnit.SetTitle(temperatureSensor.measurement.unit.ToString(), UIControlState.Normal);
    }

    /// <summary>
    /// Updates the pressure edit text for an updated temperature.
    /// </summary>
    private void UpdatePressureWithCurrentTemperature() {
      if (!(pressureSensor is GaugeDeviceSensor) && !editPressure.IsEditing) {
        bool validated = false;
        temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;

        if (ptChart.IsTemperatureAboveBounds(temperatureSensor.measurement)) {
          if (temperatureSensor.isEditable) {
            temperatureSensor.measurement = ptChart.fluid.GetMaximumTemperature();
            validated = true;
          }
        } else if (ptChart.IsTemperatureBelowBounds(temperatureSensor.measurement)) {
          if (temperatureSensor.isEditable) {
            temperatureSensor.measurement = ptChart.fluid.GetMinimumTemperature();
            validated = true;
          }
        } else {
          validated = true;
        }

        temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;

        if (validated) {
          pressureSensor.measurement = ptChart.GetPressure(temperatureSensor.measurement).ConvertTo(pressureSensor.unit);
        } else {
          // The temperature sensor is not editable (meaning we can't assume a new temperature).
          // Display an error.
          editPressure.Text = Strings.Fluid.OUT_OF_RANGE;
        }
      }
    }

    /// <summary>
    /// Updates the temperature edit text for an updated pressure
    /// </summary>
    private void UpdateTemperatureWithCurrentPressure() {
      if (!(temperatureSensor is GaugeDeviceSensor) && !editTemperature.IsEditing) {
        bool validated = false;
        pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;

        if (ptChart.IsPressureAboveBounds(pressureSensor.measurement)) {
          if (pressureSensor.isEditable) {
            switch (ptChart.state) {
              case Fluid.EState.Bubble:
                pressureSensor.measurement = ptChart.fluid.GetMaximumBubblePressure();
                validated = true;
                break;
              case Fluid.EState.Dew:
                pressureSensor.measurement = ptChart.fluid.GetMaximumDewPressure();
                validated = true;
                break;
            }
          }
        } else if (ptChart.IsPressureBelowBounds(pressureSensor.measurement)) {
          if (pressureSensor.isEditable) {
            switch (ptChart.state) {
              case Fluid.EState.Bubble:
                pressureSensor.measurement = ptChart.fluid.GetMinimumBubblePressure();
                validated = true;
                break;
              case Fluid.EState.Dew:
                pressureSensor.measurement = ptChart.fluid.GetMinimumDewPressure();
                validated = true;
                break;
            }
          }
        } else {
          validated = true;
        }

        pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;

        if (validated) {
          temperatureSensor.measurement = ptChart.GetTemperature(pressureSensor.measurement).ConvertTo(temperatureSensor.unit);
        } else {
          editTemperature.Text = Strings.Fluid.OUT_OF_RANGE;
        }
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

    /// <summary>
    /// Called after an event that invalidates the entire state of the view controller. (ie.
    /// ptchart changed).
    /// </summary>
    private void InvalidateViewController() {
      if (pressureSensor is GaugeDeviceSensor) {
        UpdateTemperatureWithCurrentPressure();
      } else if (temperatureSensor is GaugeDeviceSensor) {
        UpdatePressureWithCurrentTemperature();
      } else {
        ClearPressureInput();
        ClearTemperatureInput();
      }
    }

    /// <summary>
    /// Updates the visual locks on the view controller.
    /// A lock should appear next to a gauge iff
    ///   1) The sensor is wholly dependent on another sensor's measurement
    ///   2) The sensor is the primary sensor for a provided manifold
    /// </summary>
    private void UpdateUILocks() {
      bool plocked = false;
      bool tlocked = false;

      if (temperatureSensor != null && pressureSensor != null) {
        if (!temperatureSensor.isEditable || !isPressureSensorChangable) {
          // This means that the pressure sensor is dependent on the temperature sensor
          plocked = true;
        }

        if (!pressureSensor.isEditable || !isTemperatureSensorChangable) {
          // This means that the temperature sensor is dependent on the pressure sensor
          tlocked = true;
        }
      }

      if (plocked) {
        imagePressureLock.Image = UIImage.FromBundle("ic_lock");
      } else {
        imagePressureLock.Image = null;
      }

      if (tlocked) {
        imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");
      } else {
        imageTemperatureLock.Image = null;
      }
    }
  }
}
