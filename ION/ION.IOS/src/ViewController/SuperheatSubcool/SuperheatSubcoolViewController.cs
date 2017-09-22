namespace ION.IOS.ViewController.SuperheatSubcool {
  // This file has been autogenerated from a class added in the UI designer.

  using System;

  using CoreFoundation;
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

  using ION.IOS.Devices;
  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController;
  using ION.IOS.ViewController.Dialog;
  using ION.IOS.ViewController.DeviceManager;
  using ION.IOS.ViewController.FluidManager;

	public partial class SuperheatSubcoolViewController : BaseIONViewController {
    private const int SECTION_DEW = 0;
    private const int SECTION_BUBBLE = 1;
    public int lowHigh = 0;

    public IION ion { get; set; }
 
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
          __pressureSensor.onSensorEvent -= OnPressureSensorChanged;
        }

        __pressureSensor = value;

        if (__pressureSensor != null) {
          __pressureSensor.onSensorEvent += OnPressureSensorChanged;
          OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated,__pressureSensor));
          pressureUnit = __pressureSensor.unit;
        } else {
          ClearPressureInput();
        }

        SynchronizePressureIcons();

        if (__pressureSensor is GaugeDeviceSensor) {
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
          __temperatureSensor.onSensorEvent -= OnTemperatureSensorChanged;
        }

        __temperatureSensor = value;

        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorEvent += OnTemperatureSensorChanged;
          OnTemperatureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated,__temperatureSensor));
          temperatureUnit = __temperatureSensor.unit;
        } else {
          ClearTemperatureInput();
        }

        SynchronizeTemperatureIcons();

        if (__temperatureSensor is GaugeDeviceSensor) {
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
				return initialSensor != null && ESensorType.Pressure == initialSensor.type;
      }
    }

    /// <summary>
    /// Whether or not the temperature sensor can be set by the user.
    /// </summary>
    /// <value><c>true</c> if temperature sensor locked; otherwise, <c>false</c>.</value>
    public bool temperatureSensorLocked {
      get {
				return initialSensor != null && ESensorType.Temperature == initialSensor.type;
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
		public Sensor initialSensor { get; set; }

    public SuperheatSubcoolViewController (IntPtr handle) : base (handle) {
      // Nope
    }

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

			if (initialSensor == null) {
        InitNavigationBar("ic_nav_superheat_subcool", false);
        backAction = () => {
          root.navigation.ToggleMenu();
        }; 
      }
			ion = AppState.context;

			NavigationItem.Title = Strings.Fluid.SUPERHEAT_SUBCOOL;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.HELP, UIBarButtonItemStyle.Plain, delegate {
        var dialog = new UIAlertView(Strings.HELP, Strings.Fluid.STATE_HELP, null, Strings.OK);
        dialog.Show();
      });

      InitializeFluidControlWidgets();


      ion.locationManager.onLocationChanged += DeltaOnLocationChanged;

      pressureUnit = Units.Pressure.PSIG;
      temperatureUnit = Units.Temperature.FAHRENHEIT;

			pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
			temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0), false);

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
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Pressure);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            pressureSensor = sensor;
            buttonPressureUnit.Enabled = true;
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewPressureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!pressureSensorLocked) {
          pressureSensor = null;
          buttonPressureUnit.Enabled = false;
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
            ((ManualSensor)pressureSensor).SetMeasurement(measurement);
          }
          ////if user removes a pressure sensor, they need to be able to create a new manual sensor
          if (pressureSensor == null){
				    pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
            var measurement = pressureUnit.OfScalar(double.Parse("0.00"));
            ((ManualSensor)pressureSensor).SetMeasurement(measurement);
          }
          buttonPressureUnit.Enabled = true;
        } catch (Exception e) {
          Log.E(this, "Failed to UpdatePressure: invalid string " + editPressure.Text, e);
        }
        UpdateDelta();
      }, UIControlEvent.EditingChanged);

      viewTemperatureTouchArea.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          var dm = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
          dm.displayFilter = new SensorOfTypeFilter(ESensorType.Temperature);
          dm.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
            temperatureSensor = sensor;
            buttonTemperatureUnit.Enabled = true;
          };
          NavigationController.PushViewController(dm, true);
        }
      }));

      viewTemperatureTouchArea.AddGestureRecognizer(new UILongPressGestureRecognizer(() => {
        if (!temperatureSensorLocked) {
          temperatureSensor = null;
          buttonTemperatureUnit.Enabled = false;
          ClearTemperatureInput();
        } 
      }));

      editTemperature.AddTarget((object obj, EventArgs args) => {
        try {
          if (temperatureSensor != null && temperatureSensor.isEditable) {
            var measurement = temperatureUnit.OfScalar(double.Parse(editTemperature.Text));
            ((ManualSensor)temperatureSensor).SetMeasurement(measurement);
          }
          ////if user removes a temperature sensor, they need to be able to create a new manual sensor
          if (temperatureSensor == null){
				    temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0), false);
            var measurement = temperatureUnit.OfScalar(double.Parse("0.00"));
            ((ManualSensor)temperatureSensor).SetMeasurement(measurement);
          }
          buttonTemperatureUnit.Enabled = true;
        } catch (Exception e) {
          Log.E(this, "Failed to UpdateTemperature: invalid string " + editTemperature.Text + ".", e);
        }
        UpdateDelta();
      }, UIControlEvent.EditingChanged);

      editPressure.EditingDidEnd += (s, e) => {
        if (string.IsNullOrEmpty(editPressure.Text)) {
          ((ManualSensor)pressureSensor).SetMeasurement(pressureSensor.unit.OfScalar(0));
        }
      };

			if (initialSensor != null) {
				var sensor = initialSensor;
        if (ESensorType.Pressure == sensor.type) {
          pressureSensor = sensor;
					if (initialSensor.linkedSensor != null) {
						temperatureSensor = initialSensor.linkedSensor;
          } else {
						temperatureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Temperature, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0), false);
          }
        } else if (ESensorType.Temperature == sensor.type) {
          temperatureSensor = sensor;
					if (initialSensor.linkedSensor != null) {
            pressureSensor = initialSensor.linkedSensor;
          } else {
						pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, AppState.context.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0), true);
          }
        } else {
        	Log.E(this, "Cannot accept sensor that is not a pressure or temperature sensor");
          //throw new Exception("Cannot accept sensor that is not a pressure or temperature sensor");
        }
      }
    }

    // Overridden from BaseIONViewController
    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);
      if (pressureSensor != null) {
        OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated,pressureSensor));
      }
      if (temperatureSensor != null && pressureSensor != null) {
        OnTemperatureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, temperatureSensor));
      }

      OnSettingsChanged(null);
      settingsObserver = NSNotificationCenter.DefaultCenter.AddObserver(NSUserDefaults.DidChangeNotification, OnSettingsChanged);
    }

    private NSObject settingsObserver;
    private void OnSettingsChanged(NSNotification defaults) {
      if (pressureSensor != null) {
        pressureUnit = ion.preferences.units.pressure;
        OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, pressureSensor));
      }
      if (temperatureSensor != null) {
        temperatureUnit = ion.preferences.units.temperature;
        OnTemperatureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, temperatureSensor));
      }
      UpdateDelta();
    }

    // Overridden from BaseIONViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);
			
      if (IsMovingFromParentViewController) {
				if (initialSensor != null) {
					var type = initialSensor.type;
          
          if (ESensorType.Pressure == type) {
						if(initialSensor.linkedSensor != null && !initialSensor.linkedSensor.Equals(temperatureSensor)){
							initialSensor.SetLinkedSensor(temperatureSensor);
						} else if (initialSensor.linkedSensor == null){
							initialSensor.SetLinkedSensor(temperatureSensor);
						}
          } else if (ESensorType.Temperature == type) {
						initialSensor.SetLinkedSensor(pressureSensor);
           	//}
          } else {
            Log.E(this, "Failed to update manifold: invalid primary sensor type " + type);
          }
        }
      }

      NSNotificationCenter.DefaultCenter.RemoveObserver(settingsObserver);
    }

    /// <summary>
    /// Initializes all of view controller's fluid control widgets.
    /// </summary>
    private void InitializeFluidControlWidgets() {
			viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
			labelFluidName.Text = ion.fluidManager.lastUsedFluid.name;

      viewFluidHeader.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        var sb = InflateViewController<FluidManagerViewController>(VC_FLUID_MANAGER);
        sb.onFluidSelectedDelegate = (Fluid fluid) => {
    			viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(ion.fluidManager.GetFluidColor(ion.fluidManager.lastUsedFluid.name));
    			labelFluidName.Text = ion.fluidManager.lastUsedFluid.name;
			    if (pressureSensor != null) {
            OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, pressureSensor));
          }
        };
        NavigationController.PushViewController(sb, true);
      }));

      switchFluidState.ValueChanged += (object sender, EventArgs e) => {
        Log.D(this, "Altitude: " + ion.locationManager.lastKnownLocation.altitude);
        switch ((int)switchFluidState.SelectedSegment) {
          case SECTION_DEW:
            pressureSensor.fluidState = Fluid.EState.Dew;
            break;
          case SECTION_BUBBLE:
            pressureSensor.fluidState = Fluid.EState.Bubble;
            break;
        }
        if (pressureSensor != null) {
          OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, pressureSensor));
        }
      };
    }

    /// <summary>
    /// Called when a fluid is selected for the view controller.
    /// </summary>
    /// <param name="fluid">Fluid.</param>
    private void OnFluidSelected(Fluid fluid) {
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
        switch (pressureSensor.fluidState) {
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
            labelFluidState.BackgroundColor = new UIColor(Colors.BLUE);
            switchFluidState.TintColor = new UIColor(Colors.BLUE);
            labelFluidState.Text = Strings.Fluid.SUPERHEAT;
						break;        	
            //throw new Exception("Cannot update delta for state: " + ptChart.state);
        }
        return;
      }
      
      var pressureScalar = ion.fluidManager.lastUsedFluid.ConvertRelativePressureToAbsolute(pressureSensor.measurement, ion.locationManager.lastKnownLocation.altitude);
      var temperatureScalar = temperatureSensor.measurement;

      var calculation = ion.fluidManager.lastUsedFluid.CalculateTemperatureDelta(pressureSensor.fluidState,pressureScalar, temperatureScalar, ion.locationManager.lastKnownLocation.altitude).ConvertTo(temperatureUnit);

      if (ion.fluidManager.lastUsedFluid.mixture) {
        switch (pressureSensor.fluidState) {
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
            labelFluidState.BackgroundColor = new UIColor(Colors.BLUE);
            switchFluidState.TintColor = new UIColor(Colors.BLUE);
            labelFluidState.Text = Strings.Fluid.SUPERHEAT;
						Log.E(this, "Cannot update delta for state: " + pressureSensor.fluidState);
						break;          
            //throw new Exception("Cannot update delta for state: " + ptChart.state);
        }
        if(calculation.magnitude < 0){
					imageNegativeWarning.Hidden = false;
					View.BringSubviewToFront(imageNegativeWarning);  
				} else {
					imageNegativeWarning.Hidden = true;
					View.BringSubviewToFront(imageNegativeWarning);
				}
      } else {
      	imageNegativeWarning.Hidden = true;
				if (System.Math.Abs(calculation.magnitude) < 0.1) {
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
      labelFluidDelta.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, calculation, true);
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
          OnPressureSensorChanged(new SensorEvent(SensorEvent.EType.Invalidated, pressureSensor));
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
    private void OnPressureSensorChanged(SensorEvent sensorEvent) {
      var measurement = sensorEvent.sensor.measurement;

      if (!editPressure.IsEditing) {
        editPressure.Text = SensorUtils.ToFormattedString(measurement);
      }

      pressureUnit = measurement.unit;

      if (sensorEvent.sensor != null) {
        var temp = ion.fluidManager.lastUsedFluid.GetSaturatedTemperature(sensorEvent.sensor.fluidState,sensorEvent.sensor.measurement, ion.locationManager.lastKnownLocation.altitude).ConvertTo(temperatureUnit);
	      labelSatTempMeasurement.Text = SensorUtils.ToFormattedString(temp);
	      labelSatTempUnit.Text = temp.unit.ToString();
			}

			UpdateDelta();   
    }

    /// <summary>
    /// Called when the temperature sensor's measurement is changed.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnTemperatureSensorChanged(SensorEvent sensorEvent) {
      var measurement = sensorEvent.sensor.measurement;

      if (!editTemperature.IsEditing) {
        editTemperature.Text = SensorUtils.ToFormattedString(measurement);  
      }

      temperatureUnit = measurement.unit;
      buttonTemperatureUnit.SetTitle(measurement.unit.ToString(), UIControlState.Normal);
      if(pressureSensor == null){
				pressureSensor = new ManualSensor(ion.manualSensorContainer, ESensorType.Pressure, new Scalar(pressureUnit, 0.0));
      }
      var temp = ion.fluidManager.lastUsedFluid.GetSaturatedTemperature(pressureSensor.fluidState,pressureSensor.measurement, ion.locationManager.lastKnownLocation.altitude).ConvertTo(temperatureUnit);
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
			var im = initialSensor;
			var locked = im != null && im == pressureSensor;

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

    public void DeltaOnLocationChanged(ILocationManager locationManager, ILocation oldLocation, ILocation newLocation){
      pressureSensor = pressureSensor;

      UpdateDelta();
    }
	}
}
