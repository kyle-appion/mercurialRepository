
namespace ION.IOS.ViewController.PressureTemperatureChart {

  using System;
  using System.Threading.Tasks;

  using CoreFoundation;
  using CoreGraphics;
  using Foundation;
  using UIKit;

  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Location;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Sensors.Properties;

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
    public int lowHigh = 0;
    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;

    public SliderView ptSlider {
      get {
        return __ptSlider;
      }
      set {
        if (__ptSlider != null) {
          __ptSlider.entryMode.sensor.onSensorEvent -= __ptSlider.setOffsetFromSensorMeasurement;
        }

        __ptSlider = value;

        if (__ptSlider != null) {
          __ptSlider.entryMode.sensor.onSensorEvent += __ptSlider.setOffsetFromSensorMeasurement;
        }
      }
    }
    SliderView __ptSlider;

		public Sensor initialSensor { get; set; }

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
          entryMode = new SensorEntryMode(this, __pressureSensor, temperatureUnit, editPressure, editTemperature);
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
          entryMode = new SensorEntryMode(this, __temperatureSensor, pressureUnit, editTemperature, editPressure);
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
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
            UpdateManifoldUnit();
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
            entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
            UpdateManifoldUnit();
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
				//return ESensorType.Pressure == initialManifold?.primarySensor.type || temperatureSensor is GaugeDeviceSensor;
				return ESensorType.Pressure == initialSensor?.type || temperatureSensor is GaugeDeviceSensor;
      }
    }

    /// <summary>
    /// Whether or not the temperature sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if temperature sensor locked; otherwise, <c>false</c>.</value>
    public bool temperatureSensorLocked {
      get {
				//return ESensorType.Temperature == initialManifold?.primarySensor.type || pressureSensor is GaugeDeviceSensor;
				return ESensorType.Temperature == initialSensor?.type || pressureSensor is GaugeDeviceSensor;
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
        if (ptSlider != null) {
          ptSlider.entryMode = __entryMode;
        }
      }
    } SensorEntryMode __entryMode;

    public PTChartViewController(IntPtr handle) : base(handle) {
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = Strings.Fluid.PT_CALCULATOR;

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

			if (initialSensor == null) {
        InitNavigationBar("ic_nav_pt_chart", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        };
      }

      ion = AppState.context;

      ion.locationManager.onLocationChanged += DeltaOnLocationChanged;

      pressureUnit = ion.preferences.units.pressure;

      temperatureUnit = ion.preferences.units.temperature;

      InitPTChartWidgets();
      InitPressureWidgets();
      InitTemperatureWidgets();

      ClearPressureInput();
      ClearTemperatureInput();

			if (initialSensor != null) {
				var sensor = initialSensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
        } else {
          //throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
          Log.E(this, "Cannot accept sensor that is not a pressure or temperature sensor");
        }
      }
      var manualEdit = false;
      var topMark = new UILabel(new CGRect(.5 * View.Bounds.Width - 1, 310, 1, 128 + 20));

			if (initialSensor == null) {
        topMark.BackgroundColor = UIColor.Black;
        if (temperatureSensor == null) {
          temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0), true);
        }
        ptSlider = new SliderView(View, pressureUnit, temperatureUnit, temperatureSensor, entryMode);

        ((ManualSensor)temperatureSensor).SetMeasurement(ptSlider.ptView.minTemperature.unit.OfScalar(ptSlider.ptView.minTemperature.amount));

        editTemperature.Text = temperatureSensor.measurement.amount.ToString("N");

        ptSlider.ptScroller.Scrolled += (sender, e) => {
          if (!(pressureSensor is GaugeDeviceSensor) && !(temperatureSensor is GaugeDeviceSensor) & manualEdit == false) {
            if (temperatureSensor == null) {
					    temperatureSensor = new ManualSensor(ion.manualSensorContainer,ESensorType.Temperature,AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0),true );
            }
            setTemperatureValueFromSlider();
          }
        };
      }

      editPressure.Started += (sender, e) => {
        manualEdit = true;
        if (ptSlider != null) {
          ptSlider.ptScroller.ScrollEnabled = false;
        }
      };

      editPressure.EditingChanged += (sender, e) => {
        var valid = 0.0;
        double.TryParse(editPressure.Text, out valid);
        if (ptSlider != null) {
          if (valid >= ptSlider.ptView.minPressure && valid <= ptSlider.ptView.maxPressure.amount) {
				    var tempForPressure = ion.fluidManager.lastUsedFluid.GetSaturatedTemperature(pressureSensor.fluidState,new Scalar(pressureUnit, valid),ion.locationManager.lastKnownLocation.altitude).ConvertTo(temperatureUnit).amount;
            var manualOffset = (tempForPressure - ptSlider.ptView.minTemperature.amount) * ptSlider.ptView.tempTicks;
            ptSlider.ptScroller.SetContentOffset(new CGPoint(manualOffset, 0), false);
          }
        }
      };

      editPressure.Ended += (sender, e) => {
        manualEdit = false;
        if (ptSlider != null) {
          ptSlider.ptScroller.ScrollEnabled = true;
        }
      };

      editTemperature.Started += (sender, e) => {
        manualEdit = true;
        if (ptSlider != null) {
          ptSlider.ptScroller.ScrollEnabled = false;
        }
      };

      editTemperature.EditingChanged += (sender, e) => {
        var valid = 0.0;
        double.TryParse(editTemperature.Text, out valid);
        if (ptSlider != null) {
          var manualOffset = (valid - ptSlider.ptView.minTemperature.amount) * ptSlider.ptView.tempTicks;
          ptSlider.ptScroller.SetContentOffset(new CGPoint(manualOffset, 0), false);
        }
      };

      editTemperature.Ended += (sender, e) => {
        manualEdit = false;
        if (ptSlider != null) {
          ptSlider.ptScroller.ScrollEnabled = true;
        }
      };
      if (ptSlider != null) {
        View.AddSubview(ptSlider.sliderLabel);
        View.AddSubview(ptSlider.ptScroller);
        View.AddSubview(topMark);
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

      OnSettingsChanged(null);
      settingsObserver = NSNotificationCenter.DefaultCenter.AddObserver(NSUserDefaults.DidChangeNotification, OnSettingsChanged);
    }

    private NSObject settingsObserver;
    private void OnSettingsChanged(NSNotification defaults) {
			//bool hasSensor = initialManifold != null;
			bool hasSensor = initialSensor != null;
      if (!hasSensor) {
        pressureUnit = ion.preferences.units.pressure;
      }
      if (!hasSensor) {
        temperatureUnit = ion.preferences.units.temperature;
      }

      SetPressureMeasurementFromEditText();

      if (ptSlider != null) {
        ptSlider.ptView.lookup = pressureUnit;
        ptSlider.ptView.tempLookup = temperatureUnit;
        recalculateSlider(ptSlider.ptView.lookup, temperatureUnit);
      }
    }

    // Overridden from ViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);

      NSNotificationCenter.DefaultCenter.RemoveObserver(settingsObserver);
    }

    public override void ViewWillUnload() {
      base.ViewWillUnload();
      entryMode = null;
      ptSlider = null;
      pressureSensor = null;
      temperatureSensor = null;
			initialSensor = null;
      temperatureUnit = null;
      pressureUnit = null;
    }

    /// <summary>
    /// Attempts to update the pt chart sensor property unit.
    /// </summary>
    /// <returns>The manifold unit.</returns>
    private void UpdateManifoldUnit() {
			if (initialSensor == null) {
        return;
      }

			var prop = initialSensor.GetSensorPropertyOfType<PTChartSensorProperty>();
      if (prop != null) {
		    switch (initialSensor.type) {
          case ESensorType.Pressure:
          prop.unit = this.temperatureUnit;
          break;
          case ESensorType.Temperature:
          prop.unit = this.pressureUnit;
          break;
        }
      }
    }

    /// <summary>
    /// Initializes the states and event handlers the for ptchart widgets
    /// </summary>
    private void InitPTChartWidgets() {
			viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
			labelFluidName.Text = ion.fluidManager.lastUsedFluid.name;

      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {

          ion.preferences.fluid.preferredFluid = fluid.name;
          viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
          labelFluidName.Text = ion.fluidManager.lastUsedFluid.name;
          if (pressureSensor is GaugeDeviceSensor) {
            entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
          } else if (temperatureSensor is GaugeDeviceSensor) {
            entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
          } else {
            if (pressureSensor != null) {
              entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
            } else if (temperatureSensor != null){
              entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
            }
          }

          if (pressureSensor != null && pressureSensor is GaugeDeviceSensor) {
            if (ptSlider != null) {
              ptSlider.ptView.resetData(pressureUnit, temperatureUnit);
              ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated,pressureSensor));
            }
          } else if (temperatureSensor != null && temperatureSensor is GaugeDeviceSensor) {
            if (ptSlider != null) {
              ptSlider.ptView.resetData(pressureUnit, temperatureUnit);
              ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated, temperatureSensor));
            }
          } else {
            recalculateSlider(pressureUnit, temperatureUnit);
          }
          InvalidateViewController();
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        var activeSensor = pressureSensor;
        if(activeSensor == null){
          activeSensor = temperatureSensor;
        }
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_BUBBLE:
            activeSensor.fluidState = Fluid.EState.Bubble;
            break;
          case SECTION_DEW:
				    activeSensor.fluidState = Fluid.EState.Dew;

				  break;
        }
        if (pressureSensor != null) {
          entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
        } else {
          entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
        }

        if (pressureSensor != null && pressureSensor is GaugeDeviceSensor) {
          if (ptSlider != null) {
            ptSlider.ptView.resetData(pressureUnit, temperatureUnit);
            ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated, pressureSensor));
          }
        } else if (temperatureSensor != null && temperatureSensor is GaugeDeviceSensor) {
          if (ptSlider != null) {
            ptSlider.ptView.resetData(pressureUnit, temperatureUnit);
            ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated, temperatureSensor));
          }
        } else {
          recalculateSlider(pressureUnit, temperatureUnit);
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
            if (ptSlider != null) {
              ptSlider.ptScroller.ScrollEnabled = false;
              ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated, sensor));
            }
            InvalidateViewController();
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      editPressure.EditingDidEnd += (s, e) => {
        if (string.IsNullOrEmpty(editPressure.Text)) {
          if (pressureSensor == null) {
				    pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0));
				    //pressureSensor = new ManualSensor(ESensorType.Pressure,AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0));
          }
          ((ManualSensor)pressureSensor).SetMeasurement(pressureSensor.unit.OfScalar(0));
        }
      };

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = null;
          ClearPressureInput();
          ClearTemperatureInput();
          if (ptSlider != null) {
            ptSlider.ptScroller.ScrollEnabled = true;
          }
        }
        InvalidateViewController();
      }));

      editPressure.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editPressure.AddTarget((object obj, EventArgs args) => {
        if (pressureSensor == null) {
          var oldUnit = pressureUnit;
			    pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
          pressureUnit = oldUnit;
          pressureSensor.unit = oldUnit;
        }

        SetPressureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonPressureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonPressureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (pressureSensor == null || pressureSensor.isEditable) {
          var supportedUnits = pressureSensor != null ? pressureSensor.supportedUnits : SensorUtils.DEFAULT_PRESSURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            pressureUnit = unit;
            if (ptSlider != null) {
              ptSlider.ptView.lookup = unit;
            }
            if (pressureSensor != null) {
              entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
            } else {
              if (temperatureSensor == null) {
					      temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
              }
              entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
            }
            SetPressureMeasurementFromEditText();
            if (ptSlider != null) {
              recalculateSlider(ptSlider.ptView.lookup, temperatureUnit);
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
        if (!temperatureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureUnit = sensor.unit;
            temperatureSensor = sensor;
            if (ptSlider != null) {
              ptSlider.ptScroller.ScrollEnabled = false;
              ptSlider.setOffsetFromSensorMeasurement(new SensorEvent(SensorEvent.EType.Invalidated, sensor));
            }
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
          if (ptSlider != null) {
            ptSlider.ptScroller.ScrollEnabled = true;
          }
        }
        InvalidateViewController();
      }));

      editTemperature.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editTemperature.AddTarget((object obj, EventArgs args) => {
        if (temperatureSensor == null) {
			    temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
        }

        SetTemperatureMeasurementFromEditText();
      }, UIControlEvent.EditingChanged);

      buttonTemperatureUnit.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonTemperatureUnit.TouchUpInside += (object sender, EventArgs e) => {
        if (temperatureSensor == null || temperatureSensor.isEditable) {
          var supportedUnits = temperatureSensor != null ? temperatureSensor.supportedUnits : SensorUtils.DEFAULT_TEMPERATURE_UNITS;
          var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, supportedUnits, (obj, unit) => {
            temperatureUnit = unit;
            if (ptSlider != null) {
              ptSlider.ptView.tempLookup = unit;
            }
            if (pressureSensor != null) {
              entryMode = new SensorEntryMode(this, pressureSensor, temperatureUnit, editPressure, editTemperature);
            } else {
              entryMode = new SensorEntryMode(this, temperatureSensor, pressureUnit, editTemperature, editPressure);
            }
            SetTemperatureMeasurementFromEditText();
            if (ptSlider != null) {
              recalculateSlider(pressureUnit, ptSlider.ptView.tempLookup);
            }
          });
          PresentViewController(dialog, true, null);
        }
      };
    }

    private async void recalculateSlider(Unit pUnit, Unit tUnit) {
      if (ptSlider == null) {
        return;
      }
      await Task.Delay(TimeSpan.FromMilliseconds(1));
      ptSlider.ptView.resetData(pUnit, tUnit);
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      var previousOffset = ptSlider.ptScroller.ContentOffset.X;
      ptSlider.ptScroller.SetContentOffset(new CGPoint(ptSlider.ptScroller.ContentOffset.X + 5, 0), false);
      ptSlider.ptScroller.SetContentOffset(new CGPoint(previousOffset, 0), false);
    }

    private void SetPressureMeasurementFromEditText() {
      try {
        var amount = double.Parse(editPressure.Text);
        ((ManualSensor)pressureSensor).SetMeasurement(pressureSensor.unit.OfScalar(amount));
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
        ((ManualSensor)temperatureSensor).SetMeasurement(temperatureSensor.unit.OfScalar(amount));
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
      viewFluidColor.BackgroundColor = new UIColor(Colors.FromInt((uint)ion.fluidManager.lastUsedFluid.color));
      labelFluidName.Text = ion.fluidManager.lastUsedFluid.name;
      switchFluidState.Hidden = !ion.fluidManager.lastUsedFluid.mixture;
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
      var activeSensor = pressureSensor;
      if(activeSensor == null){
        activeSensor = temperatureSensor;
      }

      switch (activeSensor.fluidState) {
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

    internal class SensorEntryMode {
      PTChartViewController vc;
      /// <summary>
      /// The sensor that will drive the sensor mode.
      /// </summary>
      /// <value>The sensor.</value>
      internal Sensor sensor;
      /// <summary>
      /// The sensor for the conversion.
      /// </summary>
      /// <value>The other.</value>
      internal Unit otherUnit;

      UITextField primaryTextView;
      UITextField derivedTextView;

      public SensorEntryMode(PTChartViewController vc, Sensor sensor, Unit otherUnit,  UITextField primary, UITextField derived) {
        // TODO ahodder@appioninc.com: This should probably be sensor type checked at some point.
        this.vc = vc;
        this.sensor = sensor;
        this.otherUnit = otherUnit;     
        this.primaryTextView = primary;
        this.derivedTextView = derived;

        this.sensor.onSensorEvent += OnSensorChanged;

        Invalidate();
      }

      public void Unbind() {
        sensor.onSensorEvent -= OnSensorChanged;
      }

      public void Invalidate() {
        if (!(sensor is ManualSensor)) {
          primaryTextView.Text = sensor.ToFormattedString(false);
        }
        Scalar measurement;
        switch (sensor.type) {
          case ESensorType.Pressure:
            measurement = AppState.context.fluidManager.lastUsedFluid.GetSaturatedTemperature(sensor.fluidState,sensor.measurement, AppState.context.locationManager.lastKnownLocation.altitude).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(measurement); 
            vc.pressureUnit = sensor.unit;
            break;
          case ESensorType.Temperature:
            measurement = AppState.context.fluidManager.lastUsedFluid.GetPressureFromSaturatedTemperature(sensor.fluidState,sensor.measurement, AppState.context.locationManager.lastKnownLocation.altitude).ConvertTo(otherUnit);
            derivedTextView.Text = SensorUtils.ToFormattedString(measurement); 
            vc.temperatureUnit = sensor.unit;
            break;
        }
      }

      protected void OnSensorChanged(SensorEvent sensorEvent) {
        Invalidate();
      }
    }
    /// <summary>
    /// Takes the offset of the slider and translates it into the current unit's value at that point in the slider
    /// then it sets the edit text and calls the method to calculate the temperature equivalent
    /// </summary>
    private double setTemperatureValueFromSlider() {
      if (ptSlider.ptScroller.ContentOffset.X <= 0) {
        return ptSlider.ptView.setPressureStart(pressureUnit);
      } else if (ptSlider.ptScroller.ContentOffset.X >= ptSlider.ptView.measurementWidth) {
        return ptSlider.ptView.maxTemperature;
      } else {
        var temperatureValue = ptSlider.ptScroller.ContentOffset.X / ptSlider.ptView.tempTicks + ptSlider.ptView.minTemperature.amount;
        editTemperature.Text = temperatureValue.ToString("N");
        SetTemperatureMeasurementFromEditText();
        return temperatureValue;
      }
    }

    public void DeltaOnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation) {
      InvalidateViewController();
    }
  }
}
