﻿namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.OS;
  using Android.Runtime;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using Java.Lang;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Devices;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Views;

  [Activity(Label = "@string/shsc", Icon = "@drawable/ic_nav_ptconversion", Theme = "@style/TerminalActivityTheme")]      
  public class PTChartActivity : IONActivity {

    /// <summary>
    /// The value that indicates that an activity action/result was for the selection of a fluid.
    /// </summary>
    public const int REQUEST_FLUID = 1;
    /// <summary>
    /// The value that indicates that we are requesting a pressure sensor.
    /// </summary>
    public const int REQUEST_PRESSURE_SENSOR = 2;
    /// <summary>
    /// The value that indicates that we are requesting a temperature sensor.
    /// </summary>
    public const int REQUEST_TEMPERATURE_SENSOR = 3;

    /// <summary>
    /// The pt chart that will perform the calculations for the fluid.
    /// </summary>
    /// <value>The point chart.</value>
    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;

        var name = __ptChart.fluid.name;
        fluidColorView.SetBackgroundColor(new Color(ion.fluidManager.GetFluidColor(name)));
        fluidNameView.Text = name;
        fluidPhaseToggleView.Visibility = (__ptChart.fluid.mixture) ? ViewStates.Visible : ViewStates.Invisible;
      }
    } PTChart __ptChart;

    /// <summary>
    /// The view that will hold the fluid color for the ptchart.
    /// </summary>
    /// <value>The color of the fluid.</value>
    private View fluidColorView { get; set; }
    /// <summary>
    /// The view that will gold the fluid name.
    /// </summary>
    /// <value>The fluid name view.</value>
    private TextView fluidNameView { get; set; }
    /// <summary>
    /// The view that will switch between the fluid phases.
    /// </summary>
    /// <value>The fluid state toggle.</value>
    private Switch fluidPhaseToggleView { get; set; }
    /// <summary>
    /// The view that maintains the click events for the pressure sensor interaction.
    /// </summary>
    /// <value>The pressure add view.</value>
    private View pressureAddView { get; set; }
    /// <summary>
    /// The view that will display the pressure's lock icon.
    /// </summary>
    /// <value>The pressure add icon view.</value>
    private ImageView pressureLockIconView { get; set; }
    /// <summary>
    /// The view that will display the pressure's sensor icon.
    /// </summary>
    /// <value>The pressure sensor icon view.</value>
    private ImageView pressureSensorIconView { get; set; }
    /// <summary>
    /// The view that will display (allow editing of) the pressure sensor's measurement.
    /// </summary>
    /// <value>The pressure entry view.</value>
    private EditText pressureEntryView { get; set; }
    /// <summary>
    /// The view that will display the pressure sensor's unit.
    /// </summary>
    /// <value>The pressure unit view.</value>
    private Button pressureUnitView { get; set; }
    /// <summary>
    /// The button that will clear the pressure text.
    /// </summary>
    /// <value>The pressure clear.</value>
    private Button pressureClearView { get; set; }

    /// <summary>
    /// The view that maintains the click events for the temperature sensor interaction.
    /// </summary>
    /// <value>The temperature add view.</value>
    private View temperatureAddView { get; set; }
    /// <summary>
    /// The view that will display the temperature's sensor icon.
    /// </summary>
    /// <value>The temperature sensor icon view.</value>
    private ImageView temperatureSensorIconView { get; set; }
    /// <summary>
    /// The view that will display the temperature's lock icon.
    /// </summary>
    /// <value>The temperature add icon view.</value>
    private ImageView temperatureLockIconView { get; set; }
    /// <summary>
    /// The view that will display (allow editing of) the temperature sensor's measurement.
    /// </summary>
    /// <value>The temperature entry view.</value>
    private EditText temperatureEntryView { get; set; }
    /// <summary>
    /// The view that will display the temperature sensor's unit.
    /// </summary>
    /// <value>The temperature unit V iew.</value>
    private Button temperatureUnitView { get; set; }
    /// <summary>
    /// The button that will clear the temperature text.
    /// </summary>
    /// <value>The temperature clear.</value>
    private Button temperatureClearView { get; set; }


    /// <summary>
    /// The text watcher that will watch the pressure entry's text.
    /// </summary>
    /// <value>The pressure text watcher.</value>
    private Watcher pressureTextWatcher { get; set; }
    /// <summary>
    /// The text watcher that will watch the temperature entry's text.
    /// </summary>
    /// <value>The temperature text watcher.</value>
    private Watcher temperatureTextWatcher { get; set; }

    /// <summary>
    /// The sensor that will hold / provide the pressure measurements for calculation.
    /// </summary>
    /// <value>The pressure sensor.</value>
    private Sensor pressureSensor {
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
        }

        SynchronizeSensorsToViews();
      }
    } Sensor __pressureSensor;
    /// <summary>
    /// The sensor that will hold / provide the temperature measurements for calculation.
    /// </summary>
    /// <value>The temperautre sensor.</value>
    private Sensor temperatureSensor { 
      get {
        return __temperatureSensor;
      }
      set {
        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;
        }

        __temperatureSensor = value;

        if (__temperatureSensor != null) {
          __temperatureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
          OnTemperatureSensorChanged(__temperatureSensor);
        }

        SynchronizeSensorsToViews();
      }
    } Sensor __temperatureSensor;

    // Overridden from IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_ptconversion, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_ptchart);

      __pressureSensor = new ManualSensor(ESensorType.Pressure);
      __temperatureSensor = new ManualSensor(ESensorType.Temperature, false);


      FindViewById(Resource.Id.fluid).SetOnClickListener(new ViewClickAction((view) => {
        var i = new Intent(this, typeof(FluidManagerActivity));
        i.SetAction(Intent.ActionPick);
        i.PutExtra(FluidManagerActivity.EXTRA_SELECTED, ion.fluidManager.lastUsedFluid.name);
        StartActivityForResult(i, REQUEST_FLUID);
      }));

      fluidColorView = FindViewById(Resource.Id.color);
      fluidNameView = FindViewById<TextView>(Resource.Id.name);
      fluidPhaseToggleView = FindViewById<Switch>(Resource.Id.state);

      InitPressureWidgets();
      InitTemperatureWidgets();

      ptChart = PTChart.New(ion, Fluid.EState.Bubble);

      pressureSensor.unit = ion.defaultUnits.pressure;
      temperatureSensor.unit = ion.defaultUnits.temperature;
    }

    // Overridden from IONActivity
    protected override void OnResume() {
      base.OnResume();

      InvalidateActivity();
    }

    // Overridden from Activity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }


    // Overridden from IONActivity
    protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      if (Result.Ok != resultCode) {
        base.OnActivityResult(requestCode, resultCode, data);
      }

      switch (requestCode) {
        case REQUEST_FLUID:
          var fluidName = data?.GetStringExtra(FluidManagerActivity.EXTRA_SELECTED);
          if (fluidName != null) {
            // TODO ahodder@appioninc.com: loading dialog?
            var fluid = await ion.fluidManager.GetFluidAsync(fluidName);
            var state = (ptChart == null) ? Fluid.EState.Bubble : ptChart.state;
            ptChart = PTChart.New(ion, state, fluid);
//          } else {
//            Error(GetString(Resource.String.fluid_failed_to_load));
          }
          break;

        case REQUEST_PRESSURE_SENSOR:
          if (data != null && data.HasExtra(DeviceManagerActivity.EXTRA_SENSOR)) {
            var psp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
            pressureSensor = psp.Get(ion);
          }
          break;

        case REQUEST_TEMPERATURE_SENSOR:
          if (data != null && data.HasExtra(DeviceManagerActivity.EXTRA_SENSOR)) {
            var tsp = (SensorParcelable)data?.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
            temperatureSensor = tsp.Get(ion);
          }
          break;
      }
    }

    /// <summary>
    /// Initializes the pressure widgets for the activity.
    /// </summary>
    private void InitPressureWidgets() {
      var pressureView = FindViewById(Resource.Id.pressure);

      pressureAddView = pressureView.FindViewById(Resource.Id.add);
      pressureLockIconView = pressureAddView.FindViewById<ImageView>(Resource.Id.padlock);
      pressureSensorIconView = pressureAddView.FindViewById<ImageView>(Resource.Id.icon);
      pressureEntryView = pressureView.FindViewById<EditText>(Resource.Id.edit);
      pressureUnitView = pressureView.FindViewById<Button>(Resource.Id.unit);
      pressureClearView = pressureView.FindViewById<Button>(Resource.Id.clear);
      pressureTextWatcher = new Watcher((editable) => {
        var text = editable.ToString();
        try {
          if ("".Equals(text)) {
            ClearPressureInput();
          } else {
            pressureSensor.onSensorStateChangedEvent -= OnPressureSensorChanged;

            var amount = double.Parse(text);
            pressureSensor.measurement = pressureSensor.unit.OfScalar(amount);

            pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
            UpdateTemperatureMeasurement();
          }
        } catch (System.Exception e) {
          Log.D(this, "Failed to resolve pressure input", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      });

      pressureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_SENSOR_TYPES, new int[] { (int)ESensorType.Pressure });
          StartActivityForResult(i, REQUEST_PRESSURE_SENSOR);
        }
      }));

      pressureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          pressureSensor = new ManualSensor(ESensorType.Pressure, true);
          pressureEntryView.Enabled = true;
          temperatureEntryView.Enabled = true;
          InvalidateActivity();
        }
      }));

      pressureClearView.SetOnClickListener(new ViewClickAction((view) => {
        ClearInput();
      }));

      pressureUnitView.Text = pressureSensor.unit.ToString();
      pressureUnitView.SetOnClickListener(new ViewClickAction((v) => {
        if (pressureSensor.isEditable) {
          UnitDialog.Create(this, pressureSensor.supportedUnits, (obj, unit) => {
            pressureSensor.unit = unit;
          }).Show();
        }
      }));
      pressureEntryView.AddTextChangedListener(pressureTextWatcher);
    }

    /// <summary>
    /// Initializes the temperature widgets for the activity.
    /// </summary>
    private void InitTemperatureWidgets() {
      var temperatureView = FindViewById(Resource.Id.temperature);

      temperatureAddView = temperatureView.FindViewById(Resource.Id.add);
      temperatureLockIconView = temperatureAddView.FindViewById<ImageView>(Resource.Id.padlock);
      temperatureSensorIconView = temperatureAddView.FindViewById<ImageView>(Resource.Id.icon);
      temperatureEntryView = temperatureView.FindViewById<EditText>(Resource.Id.edit);
      temperatureUnitView = temperatureView.FindViewById<Button>(Resource.Id.unit);
      temperatureClearView = temperatureView.FindViewById<Button>(Resource.Id.clear);

      temperatureTextWatcher = new Watcher((editable) => {
        var text = editable.ToString();
        try {
          if ("".Equals(text)) {
            ClearTemperatureInput();
          } else {
            temperatureSensor.onSensorStateChangedEvent -= OnTemperatureSensorChanged;

            var amount = double.Parse(text);
            temperatureSensor.measurement = temperatureSensor.unit.OfScalar(amount);

            temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
            UpdatePressureMeasurement();
          }
        } catch (System.Exception e) {
          Log.D(this, "Failed to resolve temperature input", e);
          ClearPressureInput();
          ClearTemperatureInput();
        }
      });

      temperatureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!(pressureSensor is GaugeDeviceSensor)) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_SENSOR_TYPES, new int[] { (int)ESensorType.Temperature });
          StartActivityForResult(i, REQUEST_TEMPERATURE_SENSOR);
        }
      }));

      temperatureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!(pressureSensor is GaugeDeviceSensor)) {
          temperatureSensor = new ManualSensor(ESensorType.Temperature, false);
          temperatureEntryView.Enabled = true;
          pressureEntryView.Enabled = true;
          InvalidateActivity();
        }
      }));

      temperatureClearView.SetOnClickListener(new ViewClickAction((view) => {
        ClearInput();
      }));

      temperatureUnitView.Text = temperatureSensor.unit.ToString();
      temperatureUnitView.SetOnClickListener(new ViewClickAction((v) => {
        if (temperatureSensor.isEditable) {
          UnitDialog.Create(this, temperatureSensor.supportedUnits, (obj, unit) => {
            temperatureSensor.unit = unit;
          }).Show();
        }
      }));
      temperatureEntryView.AddTextChangedListener(temperatureTextWatcher);
    }

    /// <summary>
    /// Called when the pressure sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnPressureSensorChanged(Sensor sensor) {
      SynchronizePressureMeasurement(sensor.measurement);
      UpdateTemperatureMeasurement();
    }

    /// <summary>
    /// Called when the temperature sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnTemperatureSensorChanged(Sensor sensor) {
      SynchronizeTemperatureMeasurement(sensor.measurement);
      UpdatePressureMeasurement();
    }

    /// <summary>
    /// Updates the pressure's displayed measurement with the expected pressure at the
    /// temperature sensor's current measurements
    /// </summary>
    private void UpdatePressureMeasurement() {
      SynchronizePressureMeasurement(ptChart.GetPressure(temperatureSensor.measurement).ConvertTo(pressureSensor.measurement.unit));
    }

    /// <summary>
    /// Updates the temperature's displayed measurement with the saturated temperature at the
    /// pressure sensor's current measurement.
    /// </summary>
    private void UpdateTemperatureMeasurement() {
      SynchronizeTemperatureMeasurement(ptChart.GetTemperature(pressureSensor.measurement).ConvertTo(temperatureSensor.unit));
    }

    /// <summary>
    /// Synchronizes the views for the pt chart. Note: this will not actually set the entry text, as we cannot
    /// determin who is the primary sensor.
    /// </summary>
    private void SynchronizeSensorsToViews() {
      var locked = false;
      if (pressureSensor is GaugeDeviceSensor) {
        var ps = pressureSensor as GaugeDeviceSensor;
        pressureSensorIconView.SetImageBitmap(cache.GetBitmap(ps.device.GetDeviceIcon()));
        locked = true;
      } else {
        if (temperatureSensor is GaugeDeviceSensor) {
          pressureSensorIconView.Visibility = ViewStates.Invisible;
        } else {
          pressureSensorIconView.Visibility = ViewStates.Visible;
          pressureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
        }
      }

      if (temperatureSensor is GaugeDeviceSensor) {
        var ts = temperatureSensor as GaugeDeviceSensor;
        temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(ts.device.GetDeviceIcon()));
        locked = true;
      } else {
        if (pressureSensor is GaugeDeviceSensor) {
          temperatureSensorIconView.Visibility = ViewStates.Invisible;
        } else {
          temperatureSensorIconView.Visibility = ViewStates.Visible;
          temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
        }
      }

      if (locked) {
        pressureLockIconView.Visibility = ViewStates.Visible;
        temperatureLockIconView.Visibility = ViewStates.Visible;
      } else {
        pressureLockIconView.Visibility = ViewStates.Invisible;
        temperatureLockIconView.Visibility = ViewStates.Invisible;
      }

      pressureEntryView.Enabled = !locked && pressureSensor.isEditable;
      temperatureEntryView.Enabled = !locked && temperatureSensor.isEditable;
    }

    private void SynchronizePressureMeasurement(Scalar measurement) {
      SetPressureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Pressure, measurement));
      pressureUnitView.Text = measurement.unit.ToString();
    }

    /// <summary>
    /// Synchronizes the UI with the given temperature measurement.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    private void SynchronizeTemperatureMeasurement(Scalar measurement) {
      SetTemperatureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Temperature, measurement));
      temperatureUnitView.Text = measurement.unit.ToString();
    }

    /// <summary>
    /// Clears the input to the pressure and temperature entries.
    /// </summary>
    private void ClearInput() {
      ClearPressureInput();
      ClearTemperatureInput();
    }

    /// <summary>
    /// Clears the text form the pressure entry view.
    /// </summary>
    private void ClearPressureInput() {
      SetPressureInputQuietly("");
    }

    /// <summary>
    /// Clears the text from the temperature entry view.
    /// </summary>
    private void ClearTemperatureInput() {
      SetTemperatureInputQuietly("");
    }

    /// <summary>
    /// Sets the pressure entry view's text such that a text watcher callback is not made.
    /// </summary>
    /// <param name="text">Text.</param>
    private void SetPressureInputQuietly(string text) {
      pressureEntryView.RemoveTextChangedListener(pressureTextWatcher);

      pressureEntryView.Text = text;

      pressureEntryView.AddTextChangedListener(pressureTextWatcher);
    }

    /// <summary>
    /// Sets the temperature entry view's text such that a text watcher callback is not made.
    /// </summary>
    /// <param name="text">Text.</param>
    private void SetTemperatureInputQuietly(string text) {
      temperatureEntryView.RemoveTextChangedListener(temperatureTextWatcher);

      temperatureEntryView.Text = text;

      temperatureEntryView.AddTextChangedListener(temperatureTextWatcher);
    }

    /// <summary>
    /// Invalidates and updates the views for the activity.
    /// </summary>
    private void InvalidateActivity() {
      switch (ptChart.state) {
        case Fluid.EState.Bubble:
          fluidPhaseToggleView.Checked = true;
          break;
        case Fluid.EState.Dew:
          fluidPhaseToggleView.Checked = false;
          break;
      }

      SynchronizeSensorsToViews();

      if (pressureSensor is GaugeDeviceSensor) {
        UpdateTemperatureMeasurement();
      } else if (temperatureSensor is GaugeDeviceSensor) {
        UpdateTemperatureMeasurement();
      } else {
        ClearPressureInput();
        ClearTemperatureInput();
      }
    }

    /// <summary>
    /// The text watcher that will manage the text changes for the pressure and
    /// temperature edit texts.
    /// </summary>
    private class Watcher : Java.Lang.Object, ITextWatcher {
      public delegate void OnAfterTextChanged(IEditable editable);

      private OnAfterTextChanged afterTextChanged { get; set; }

      public Watcher(OnAfterTextChanged afterTextChanged) {
        this.afterTextChanged = afterTextChanged;
      }
      
      // Overridden from ITextWatcher
      public void AfterTextChanged(IEditable editable) {
        Log.D(this, "Stuff is happening: " + editable);
        if (afterTextChanged != null) {
          afterTextChanged(editable);
        }
      }
      // Overridden from ITextWatcher
      public void BeforeTextChanged(ICharSequence text, int start, int count, int after) {
      }

      // Overridden from ITextWatcher
      public void OnTextChanged(ICharSequence text, int start, int before, int count) {
      }
    }
  }
}

