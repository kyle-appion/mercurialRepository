// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.Sensors;
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

    /// <summary>
    /// The ion context.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; set; }
    /// <summary>
    /// The pt chart that the view controller is interacting with.
    /// </summary>
    /// <value>The point chart.</value>
    public ION.Core.Fluids.PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        var name = ptChart.fluid.name;
        labelFluidName.Text = name;
        viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(name));
      }
    } ION.Core.Fluids.PTChart __ptChart;

    /// <summary>
    /// The pressure sensor that will update the pressure input view.
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

        if (value == null) {
          value = new Sensor(ESensorType.Pressure);
        }
        __pressureSensor = value;
        if (__pressureSensor is GaugeDeviceSensor) {
          ClearPressureInput();
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__pressureSensor;
          imagePressureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          imagePressureLock.Image = UIImage.FromBundle("ic_lock");
        } else {
          imagePressureIcon.Image = UIImage.FromBundle("ic_device_add");
          imagePressureLock.Image = null;
        }

        editPressure.Enabled = editTemperature.Enabled = __pressureSensor.isEditable;

        pressureUnit = value.unit;
        __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
      }
    } Sensor __pressureSensor;

    /// <summary>
    /// The unit that the pressure sensor will be set to, if editable.
    /// </summary>
    /// <value>The pressure unit.</value>
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

    /// <summary>
    /// The temperature sensor that will update the temperature input view.
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

        if (value == null) {
          value = new Sensor(ESensorType.Temperature);
        }
        __temperatureSensor = value;
        if (__temperatureSensor is GaugeDeviceSensor) {
          ClearTemperatureInput();
          GaugeDeviceSensor sensor = (GaugeDeviceSensor)__temperatureSensor;
          imageTemperatureIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(sensor.device.serialNumber.deviceModel);
          imageTemperatureLock.Image = UIImage.FromBundle("ic_lock");
        } else {
          imageTemperatureIcon.Image = UIImage.FromBundle("ic_device_add");
          imageTemperatureLock.Image = null;
        }

        editPressure.Enabled = editTemperature.Enabled = __temperatureSensor.isEditable;
        temperatureUnit = value.unit;
        __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
      }
    } Sensor __temperatureSensor;

    /// <summary>
    /// The unit that the tempreature sensor will be set to, if editable.
    /// </summary>
    /// <value>The temperature unit.</value>
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

    private bool isEditingPressure { get; set; }
    private bool isEditingTemperature { get; set; }

    public PTChartViewController (IntPtr handle) : base (handle) {
    }

    // Overridden from PressureTemperatureViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_pt_chart", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };

      __pressureSensor = new Sensor(ESensorType.Pressure);
      __temperatureSensor = new Sensor(ESensorType.Temperature);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, pressureSensor.GetSupportedUnits(), (obj, unit) => {
            pressureUnit = unit;
          });
          PresentViewController(dialog, true, null);
        }
      };

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor.isEditable) {
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, temperatureSensor.GetSupportedUnits(), (obj, unit) => {
            temperatureUnit = unit;
          });
          PresentViewController(dialog, true, null);
        }
      };

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_DEW:
            ptChart = new ION.Core.Fluids.PTChart(Fluid.EState.Dew, ptChart.fluid, ptChart.elevation);
            switchFluidState.TintColor = new UIColor(Colors.BLUE);
            break;
          case SECTION_BUBBLE:
            ptChart = new ION.Core.Fluids.PTChart(Fluid.EState.Bubble, ptChart.fluid, ptChart.elevation);
            switchFluidState.TintColor = new UIColor(Colors.RED);
            break;
        }
      };

      ion = AppState.context;

      ptChart = new ION.Core.Fluids.PTChart(Fluid.EState.Dew, ion.fluidManager.lastUsedFluid);
      pressureSensor = new Sensor(ESensorType.Pressure, Units.Pressure.PSIG.OfScalar(0), true);
      temperatureSensor = new Sensor(ESensorType.Temperature, Units.Temperature.FAHRENHEIT.OfScalar(0), false);

      NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      View.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editPressure.ResignFirstResponder();
        editTemperature.ResignFirstResponder();
      }));

      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = OnFluidSelected;
        NavigationController.PushViewController(sb, true);
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
        dm.displayFilter = new SensorTypeFilter(ESensorType.Pressure);
        dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
          pressureSensor = sensor;
        };
        NavigationController.PushViewController(dm, true);
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        pressureSensor = null;
        ClearPressureInput();
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
        UpdateTemperature();
      }, UIControlEvent.EditingChanged);

      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var dm = InflateViewController<DeviceManagerViewController>("deviceManagerViewController");
        dm.displayFilter = new SensorTypeFilter(ESensorType.Temperature);
        dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
          temperatureSensor = sensor;
        };
        NavigationController.PushViewController(dm, true);
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        temperatureSensor = null;
        ClearTemperatureInput();
      }));

      editTemperature.AddTarget((object obj, EventArgs args) => {
        UpdatePressure();
      }, UIControlEvent.EditingChanged);

      OnFluidSelected(ion.fluidManager.lastUsedFluid);
    }

    /// <summary>
    /// Called when a fluid is selected for the view controller.
    /// </summary>
    /// <param name="fluid">Fluid.</param>
    private void OnFluidSelected(Fluid fluid) {
      ptChart = new ION.Core.Fluids.PTChart(ptChart.state, fluid, this.ptChart.elevation);
    }

    private void OnPressureSensorChanged(Sensor sensor) {
      if (!isEditingTemperature) {
        editPressure.Text = sensor.ToFormattedString();
        buttonPressureUnit.SetTitle(sensor.unit.ToString(), UIControlState.Normal);
      }
      UpdateTemperature();
    }

    private void OnTemperatureSensorChanged(Sensor sensor) {
      if (!isEditingPressure) {
        editTemperature.Text = sensor.ToFormattedString();
        buttonTemperatureUnit.SetTitle(sensor.unit.ToString(), UIControlState.Normal);
      }
      UpdatePressure();
    }

    private void UpdatePressure() {
      if (isEditingPressure || isEditingTemperature) {
        return;
      }
      isEditingPressure = true;

      try {
        if (pressureSensor.isEditable) {
          var measurement = temperatureUnit.OfScalar(double.Parse(editTemperature.Text));
          temperatureSensor.measurement = measurement;

          pressureSensor.measurement = ptChart.GetPressure(measurement).ConvertTo(pressureUnit);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to UpdatePressure: invalid string " + editTemperature.Text, e);
        ClearPressureInput();
      }

      isEditingPressure = false;
    }

    private void UpdateTemperature() {
      if (isEditingPressure || isEditingTemperature) {
        return;
      }
      isEditingTemperature = true;

      try {
        if (temperatureSensor.isEditable) {
          var measurement = pressureUnit.OfScalar(double.Parse(editPressure.Text));
          pressureSensor.measurement = measurement;

          temperatureSensor.measurement = ptChart.GetTemperature(measurement).ConvertTo(temperatureUnit);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to UpdateTemperature: invalid string " + editPressure.Text + ".", e);
        ClearTemperatureInput();
      }

      isEditingTemperature = false;
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
