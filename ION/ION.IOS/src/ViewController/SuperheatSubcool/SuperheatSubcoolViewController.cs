// This file has been autogenerated from a class added in the UI designer.

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

namespace ION.IOS.ViewController.SuperheatSubcool {
	public partial class SuperheatSubcoolViewController : BaseIONViewController {
    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;

    public IION ion { get; set; }
    public ION.Core.Fluids.PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        var name = ptChart.fluid.name;
        labelFluidName.Text = name;
        viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(name));

        switchFluidState.Hidden = !__ptChart.fluid.mixture;

        switch (ptChart.state) {
          case Fluid.EState.Dew:
            switchFluidState.SelectedSegment = SECTION_DEW;
            labelFluidState.Text = Strings.Fluid.SUPERHEAT;
            UpdateDelta();
            break;
          case Fluid.EState.Bubble:
            switchFluidState.SelectedSegment = SECTION_BUBBLE;
            labelFluidState.Text = Strings.Fluid.SUBCOOL;
            UpdateDelta();
            break;
        }

        if (pressureSensor != null) {
          OnPressureSensorChanged(pressureSensor);
        }

        if (temperatureSensor != null) {
          OnTemperatureSensorChanged(temperatureSensor);
        }

        if (initialManifold != null) {
          initialManifold.ptChart = __ptChart;
        }
      }
    } ION.Core.Fluids.PTChart __ptChart;

    /// <summary>
    /// The pressure sensor used in the pressure temperature calculations.
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
          OnPressureSensorChanged(__pressureSensor);
          pressureUnit = __pressureSensor.unit;
        } else {
          ClearPressureInput();
        }

        SynchronizePressureIcons();

        if (__pressureSensor is GaugeDeviceSensor || __pressureSensor is ManualSensor) {
          editPressure.Enabled = false;
        } else {
          editPressure.Enabled = true;
        }
      }
    } Sensor __pressureSensor;

    /// <summary>
    /// The temperature sensor used in the pressure temperature calculations.
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
          OnTemperatureSensorChanged(__temperatureSensor);
          temperatureUnit = __temperatureSensor.unit;
        } else {
          ClearTemperatureInput();
        }

        SynchronizeTemperatureIcons();

        if (__temperatureSensor is GaugeDeviceSensor || __temperatureSensor is ManualSensor) {
          editTemperature.Enabled = false;
        } else {
          editTemperature.Enabled = true;
        }
      }
    } Sensor __temperatureSensor;

    /// <summary>
    /// The preferred unit for the pressure sensor.
    /// </summary>
    /// <value>The pressure unit.</value>
    private Unit pressureUnit { get; set; }
    /// <summary>
    /// The preferred unit for the temperature sensor.
    /// </summary>
    /// <value>The temperature unit.</value>
    private Unit temperatureUnit { get; set; }

    /// <summary>
    /// Whether or not the pressure sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if pressure sensor locked; otherwise, <c>false</c>.</value>
    public bool pressureSensorLocked {
      get {
        return initialManifold != null && ESensorType.Pressure == initialManifold.primarySensor.type;
      }
    }

    /// <summary>
    /// Whether or not the temperature sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if temperature sensor locked; otherwise, <c>false</c>.</value>
    public bool temperatureSensorLocked {
      get {
        return initialManifold != null && ESensorType.Temperature == initialManifold.primarySensor.type;
      }
    }

    /// <summary>
    /// The manifold that is used to initialize the view controller. If this is null
    /// the view controller will initialize to its default settings. When the view
    /// controller is finishes (removed from parent), if the manifold is not null, the
    /// manifold will be updated to the state of the view controller.
    /// </summary>
    /// <value>The initial manifold.</value>
    public Manifold initialManifold { get; set; }

    public SuperheatSubcoolViewController (IntPtr handle) : base (handle) {
      // Nope
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      if (initialManifold == null) {
        InitNavigationBar("ic_nav_superheat_subcool", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      }

      NavigationItem.Title = Strings.Fluid.SUPERHEAT_SUBCOOL;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      InitializeFluidControlWidgets();

      ion = AppState.context;

      ptChart = PTChart.New(ion, Fluid.EState.Dew);

      pressureUnit = Units.Pressure.PSIG;
      temperatureUnit = Units.Temperature.FAHRENHEIT;

      pressureSensor = new ManualSensor(ESensorType.Pressure, true);
      temperatureSensor = new ManualSensor(ESensorType.Temperature, false);

      pressureSensor.unit = pressureUnit;
      temperatureSensor.unit = temperatureUnit;

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += OnPressureUnitChanged;

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += OnTemperatureUnitChanged;

      imagePressureLock.Image = UIImage.FromBundle("ic_lock");
      imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");

      View.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editPressure.ResignFirstResponder();
        editTemperature.ResignFirstResponder();
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureSensor = sensor;
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = new ManualSensor(ESensorType.Pressure, true);
          ClearPressureInput();
        }
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        try {
          if (pressureSensor != null && pressureSensor.isEditable) {
            var measurement = pressureUnit.OfScalar(double.Parse(editPressure.Text));
            pressureSensor.measurement = measurement;
          }
        } catch (Exception e) {
          Log.E(this, "Failed to UpdatePressure: invalid string " + editPressure.Text, e);
//          ClearPressureInput();
        }
        UpdateDelta();
      }, UIControlEvent.EditingChanged);

      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureSensor = sensor;
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          temperatureSensor = new ManualSensor(ESensorType.Temperature, false);
          ClearTemperatureInput();
        }
      }));

      editTemperature.AddTarget((object obj, EventArgs args) => {
        try {
          if (temperatureSensor != null && temperatureSensor.isEditable) {
            var measurement = temperatureUnit.OfScalar(double.Parse(editTemperature.Text));
            temperatureSensor.measurement = measurement;
          }
        } catch (Exception e) {
          Log.E(this, "Failed to UpdateTemperature: invalid string " + editTemperature.Text + ".", e);
//          ClearTemperatureInput();
        }
        UpdateDelta();
      }, UIControlEvent.EditingChanged);

      ptChart = PTChart.New(ion, Fluid.EState.Bubble);


      if (initialManifold != null) {
        ptChart = initialManifold.ptChart;
        var sensor = initialManifold.primarySensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
          if (initialManifold.secondarySensor != null) {
            temperatureSensor = initialManifold.secondarySensor;
          } else {
            temperatureSensor = new ManualSensor(ESensorType.Temperature, false);
          }
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
          if (initialManifold.secondarySensor != null) {
            pressureSensor = initialManifold.secondarySensor;
          } else {
            pressureSensor = new ManualSensor(ESensorType.Pressure, true);
          }
        } else {
          throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
        }
      }
    }

    // Overridden from BaseIONViewController
    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);

      OnPressureSensorChanged(pressureSensor);
      OnTemperatureSensorChanged(temperatureSensor);
    }

    // Overridden from BaseIONViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);

      if (IsMovingFromParentViewController) {
        if (initialManifold != null) {
          initialManifold.ptChart = ptChart;
          var type = initialManifold.primarySensor.type;
          if (ESensorType.Pressure == type) {
            initialManifold.secondarySensor = temperatureSensor;
          } else if (ESensorType.Temperature == type) {
            initialManifold.secondarySensor = pressureSensor;
          } else {
            ION.Core.Util.Log.E(this, "Failed to update manifold: invalid primary sensor type " + type);
          }
        }
      }
    }

    /// <summary>
    /// Initializes all of view controller's fluid control widgets.
    /// </summary>
    private void InitializeFluidControlWidgets() {
      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {
          ptChart = PTChart.New(ion, ptChart.state, fluid);
          if (pressureSensor != null) {
            OnPressureSensorChanged(pressureSensor);
          }
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        Log.D(this, "Altitude: " + ion.locationManager.lastKnownLocation.altitude);
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_DEW:
            ptChart = PTChart.New(ion, Fluid.EState.Dew, ptChart.fluid);
            break;
          case SECTION_BUBBLE:
            ptChart = PTChart.New(ion, Fluid.EState.Bubble, ptChart.fluid);
            break;
        }
        if (pressureSensor != null) {
          OnPressureSensorChanged(pressureSensor);
        }
      };
    }

    /// <summary>
    /// Called when a fluid is selected for the view controller.
    /// </summary>
    /// <param name="fluid">Fluid.</param>
    private void OnFluidSelected(Fluid fluid) {
      ptChart = PTChart.New(ion, ptChart.state, fluid);
      switchFluidState.Hidden = !fluid.mixture;
      UpdateDelta();
    }

    /// <summary>
    /// Updates the calculated temperature delta value for the superheat/subcool calculations.
    /// </summary>
    private void UpdateDelta() {
      labelFluidState.BackgroundColor = new UIColor(Colors.RED);
      switchFluidState.TintColor = new UIColor(Colors.RED);

      if (editPressure.Text.Equals("") || editTemperature.Text.Equals("") || pressureSensor == null || temperatureSensor == null) {
        labelFluidDelta.Text = "";
        return;
      }
      
      var pressureScalar = pressureSensor.measurement;
      var temperatureScalar = temperatureSensor.measurement;

      var calculation = ptChart.CalculateSystemTemperatureDelta(pressureScalar, temperatureScalar, false).ConvertTo(temperatureUnit);

      if (ptChart.fluid.mixture) {
        switch (ptChart.state) {
          case Fluid.EState.Bubble:
            labelFluidState.BackgroundColor = new UIColor(Colors.RED);
            switchFluidState.TintColor = new UIColor(Colors.RED);
            labelFluidState.Text = Strings.Fluid.SUBCOOL;
            break;
          case Fluid.EState.Dew:
            labelFluidState.BackgroundColor = new UIColor(Colors.BLUE);
            switchFluidState.TintColor = new UIColor(Colors.BLUE);
            labelFluidState.Text = Strings.Fluid.SUPERHEAT;
            break;
          default:
            throw new Exception("Cannot update delta for state: " + ptChart.state);
        }
      } else {
        if (calculation.AssertEquals(0, 0.01)) {
          labelFluidState.BackgroundColor = new UIColor(Colors.GREEN);
          labelFluidState.Text = Strings.Fluid.SATURATED;
        } else if (calculation > 0) {
          labelFluidState.BackgroundColor = new UIColor(Colors.BLUE);
          labelFluidState.Text = Strings.Fluid.SUPERHEAT;
        } else {
          labelFluidState.BackgroundColor = new UIColor(Colors.RED);
          labelFluidState.Text = Strings.Fluid.SUBCOOL;
          //should never show a negative temperature so multiply by -1
          calculation = calculation * -1;
        }
      }

      labelFluidDelta.Text = calculation.amount.ToString("N") + calculation.unit.ToString();
    }

    /// <summary>
    /// Called when the pressure sensor's unit is changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnPressureUnitChanged(object sender, EventArgs args) {
      if (pressureSensor.isEditable) {
        var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.supportedUnits, (obj, unit) => {
          pressureUnit = unit;
          if (pressureSensor != null && pressureSensor.isEditable) {
            pressureSensor.unit = pressureUnit;
          }
          buttonPressureUnit.SetTitle(unit + "", UIControlState.Normal);
        });

        var popover = dialog.PopoverPresentationController;
        if (popover != null) {
          popover.SourceView = View;
        }
        this.PresentViewController(dialog, true, null);
      }
    }

    /// <summary>
    /// Called when the temperature sensor's unit is changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnTemperatureUnitChanged(object sensor, EventArgs args) {
      if (temperatureSensor.isEditable) {
        var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.supportedUnits, (obj, unit) => {
          temperatureUnit = unit;
          if (temperatureSensor != null && temperatureSensor.isEditable) {
            temperatureSensor.unit = temperatureUnit;
          }
          buttonTemperatureUnit.SetTitle(unit + "", UIControlState.Normal);
        });

        var popover = dialog.PopoverPresentationController;
        if (popover != null) {
          popover.SourceView = View;
        }
        this.PresentViewController(dialog, true, null);
      }      
    }

    /// <summary>
    /// Called when the pressure sensor's measurement is changed.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnPressureSensorChanged(Sensor sensor) {
      var measurement = sensor.measurement;

      if (!editPressure.IsEditing) {
        editPressure.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, measurement);
      }

      pressureUnit = measurement.unit;
      buttonPressureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);

      var temp = ptChart.GetTemperature(sensor).ConvertTo(temperatureUnit);

      labelSatTempMeasurement.Text = temp.amount.ToString("0.00");
      labelSatTempUnit.Text = temp.unit.ToString();

      UpdateDelta();
    }

    /// <summary>
    /// Called when the temperature sensor's measurement is changed.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnTemperatureSensorChanged(Sensor sensor) {
      var measurement = sensor.measurement;

      if (!editTemperature.IsEditing) {
        editTemperature.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, measurement);  
      }

      temperatureUnit = measurement.unit;
      buttonTemperatureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);

      var temp = ptChart.GetTemperature(pressureSensor).ConvertTo(temperatureUnit);

      labelSatTempMeasurement.Text = temp.amount.ToString("0.00");
      labelSatTempUnit.Text = temp.unit.ToString();

      UpdateDelta();
    }

    /// <summary>
    /// Clears the pressure input without updating the pressure sensor's reading.
    /// </summary>
    private void ClearPressureInput() {
      editPressure.Text = "";
      labelSatTempMeasurement.Text = "";
      UpdateDelta();
    }

    /// <summary>
    /// Clears the temperature input without updating the temperature sensor's reading.
    /// </summary>
    private void ClearTemperatureInput() {
      editTemperature.Text = "";
      UpdateDelta();
    }

    /// <summary>
    /// Synchronizes the state of the pressure icons to the pressure sensor.
    /// </summary>
    private void SynchronizePressureIcons() {
      var im = initialManifold;
      var locked = im != null && im.primarySensor == pressureSensor;

      if (pressureSensor is GaugeDeviceSensor) {
        var gds = pressureSensor as GaugeDeviceSensor;
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imagePressureLock.Hidden = !pressureSensorLocked;
        imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
      }

      buttonPressureUnit.SetTitle(pressureUnit.ToString(), UIControlState.Normal);
    }

    /// <summary>
    /// Synchronizes the state of the temperature icons to the temperature sensor.
    /// </summary>
    private void SynchronizeTemperatureIcons() {
      if (temperatureSensor is GaugeDeviceSensor) {
        var gds = temperatureSensor as GaugeDeviceSensor;
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Image = gds.device.serialNumber.deviceModel.GetUIImageFromDeviceModel();
      } else {
        imageTemperatureLock.Hidden = !temperatureSensorLocked;
        imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
      }

      buttonTemperatureUnit.SetTitle(temperatureUnit.ToString(), UIControlState.Normal);
    }
	}
}
