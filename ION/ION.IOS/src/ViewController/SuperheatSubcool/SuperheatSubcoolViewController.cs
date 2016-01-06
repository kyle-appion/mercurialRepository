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
        switch (ptChart.state) {
          case Fluid.EState.Dew:
            switchFluidState.SelectedSegment = SECTION_DEW;
            labelFluidState.Text = Strings.Fluid.SUPERHEAT;
            labelFluidState.BackgroundColor = new UIColor(Colors.BLUE);
            switchFluidState.TintColor = new UIColor(Colors.BLUE);
            UpdateDelta();
            break;
          case Fluid.EState.Bubble:
            switchFluidState.SelectedSegment = SECTION_BUBBLE;
            labelFluidState.Text = Strings.Fluid.SUBCOOL;
            labelFluidState.BackgroundColor = new UIColor(Colors.RED);
            switchFluidState.TintColor = new UIColor(Colors.RED);
            UpdateDelta();
            break;
        }
        if (initialManifold != null) {
          initialManifold.ptChart = __ptChart;
        }
      }
    } ION.Core.Fluids.PTChart __ptChart;

    public Sensor pressureSensor {
      get {
        return __pressureSensor;
      }
      set {
        if (__pressureSensor != null) {
          __pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;
        }

        if (value == null) {
          value = new Sensor(ESensorType.Pressure);
        }

        __pressureSensor = value;

        if (__pressureSensor is GaugeDeviceSensor) {
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__pressureSensor;
          imagePressureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          if (pressureSensorLocked) {
            imagePressureLock.Image = UIImage.FromBundle("ic_lock");
          }
        } else {
          imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
          imagePressureLock.Image = null;
        }

        OnPressureSensorChanged(pressureSensor);

        editPressure.Enabled = editTemperature.Enabled = __pressureSensor.isEditable;
        pressureUnit = value.unit;
        __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
      }
    } Sensor __pressureSensor;

    public bool pressureSensorLocked {
      get {
        return initialManifold != null && ESensorType.Pressure == initialManifold.primarySensor.type;
      }
    }

    public Unit pressureUnit {
      get {
        return pressureSensor.unit;
      }
      set {
        if (value == null) {
          value = Units.Pressure.PSIG;
        }

        if (pressureSensor.isEditable) {
          pressureSensor.measurement = value.OfScalar(pressureSensor.measurement.amount);
          buttonPressureUnit.SetTitle(value.ToString(), UIControlState.Normal);
        }
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
          value = new Sensor(ESensorType.Temperature);
        }

        __temperatureSensor = value;

        if (__temperatureSensor is GaugeDeviceSensor) {
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__temperatureSensor;
          imageTemperatureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          if (temperatureSensorLocked) {
            imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");
          }
        } else {
          imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
          imageTemperatureLock.Image = null;
        }

        OnTemperatureSensorChanged(temperatureSensor);

        editPressure.Enabled = editTemperature.Enabled = __temperatureSensor.isEditable;
        temperatureUnit = value.unit;
        __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
      }
    } Sensor __temperatureSensor;

    public bool temperatureSensorLocked {
      get {
        return initialManifold != null && ESensorType.Temperature == initialManifold.primarySensor.type;
      }
    }

    public Unit temperatureUnit {
      get {
        return temperatureSensor.unit;
      }
      set {
        if (value == null) {
          value = Units.Temperature.FAHRENHEIT;
        }

        if (temperatureSensor.isEditable) {
          temperatureSensor.measurement = value.OfScalar(temperatureSensor.measurement.amount);
          buttonTemperatureUnit.SetTitle(value.ToString(), UIControlState.Normal);
        }
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

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.supportedUnits, (obj, unit) => {
            pressureUnit = unit;
          });

          PresentViewController(dialog, true, null);
        }
      };

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.supportedUnits, (obj, unit) => {
            temperatureUnit = unit;
          });
          PresentViewController(dialog, true, null);
        }
      };

      ion = AppState.context;

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
          pressureSensor = null;
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
          if (pressureSensor.isEditable) {
            var measurement = pressureUnit.OfScalar(double.Parse(editPressure.Text));
            pressureSensor.measurement = measurement;
          }
        } catch (Exception e) {
          Log.E(this, "Failed to UpdatePressure: invalid string " + editPressure.Text, e);
          ClearPressureInput();
        }
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
          temperatureSensor = null;
          ClearTemperatureInput();
        }
      }));

      editTemperature.AddTarget((object obj, EventArgs args) => {
        try {
          if (temperatureSensor.isEditable) {
            var measurement = temperatureUnit.OfScalar(double.Parse(editTemperature.Text));
            temperatureSensor.measurement = measurement;
          }
        } catch (Exception e) {
          Log.E(this, "Failed to UpdateTemperature: invalid string " + editTemperature.Text + ".", e);
          ClearTemperatureInput();
        }
      }, UIControlEvent.EditingChanged);
    }

    // Overriden from BaseIONViewController
    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);

      __pressureSensor = new Sensor(ESensorType.Pressure);
      __temperatureSensor = new Sensor(ESensorType.Temperature);

      if (initialManifold == null) {
        ptChart = PTChart.New(ion, Fluid.EState.Dew);
        pressureSensor = new Sensor(ESensorType.Pressure);
        temperatureSensor = new Sensor(ESensorType.Temperature, false);
      } else {
        var im = initialManifold;
        if (im.ptChart == null) {
          ptChart = im.ptChart = PTChart.New(ion, Fluid.EState.Dew);
        } else {
          ptChart = im.ptChart;
        }
        if (im.primarySensor.type == ESensorType.Pressure) {
          pressureSensor = im.primarySensor;
          temperatureSensor = im.secondarySensor;
        } else if (im.primarySensor.type == ESensorType.Temperature) {
          pressureSensor = im.secondarySensor;
          temperatureSensor = im.secondarySensor;
        } else {
          // TODO ahodder@appioninc.com: Display a user friendly error message
          throw new Exception("Cannot display view controller: invalid manifold");
        }
      }

      labelFluidDelta.Text = "";
      OnFluidSelected(ion.fluidManager.lastUsedFluid);

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
          OnPressureSensorChanged(pressureSensor);
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
        OnPressureSensorChanged(pressureSensor);
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

    private void OnPressureSensorChanged(Sensor sensor) {
      if (!editPressure.IsEditing) {
        editPressure.Text = sensor.ToFormattedString();
      }
      buttonPressureUnit.SetTitle(sensor.unit.ToString(), UIControlState.Normal);

      if (editPressure.Text == "") {
        labelSatTempMeasurement.Text = "";
      } else {
        labelSatTempMeasurement.Text = ptChart.GetTemperature(pressureSensor.measurement, pressureSensor.isRelative).ConvertTo(temperatureUnit).amount.ToString("0.00");
      }
      labelSatTempUnit.Text = temperatureUnit.ToString();

      UpdateDelta();
    }

    private void OnTemperatureSensorChanged(Sensor sensor) {
      if (!editTemperature.IsEditing) {
        editTemperature.Text = sensor.ToFormattedString();
      }
      buttonTemperatureUnit.SetTitle(sensor.unit.ToString(), UIControlState.Normal);
      labelSatTempUnit.Text = sensor.unit.ToString();

      UpdateDelta();
    }

    private void UpdateDelta() {
      if (editPressure.Text == "" && editTemperature.Text == "") {
        labelFluidDelta.Text = "";
      } else {
        Scalar calculation = ptChart.CalculateSystemTemperatureDelta(pressureSensor.measurement, temperatureSensor.measurement, pressureSensor.isRelative);
        Log.D(this, "Calculation: " + calculation);
        labelFluidDelta.Text = calculation.amount.ToString("0.00") + calculation.unit.ToString();
      }
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
	}
}
