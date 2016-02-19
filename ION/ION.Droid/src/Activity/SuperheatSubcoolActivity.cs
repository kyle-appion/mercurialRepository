namespace ION.Droid.Activity {

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

  [Activity(Label = "@string/shsc", Icon = "@drawable/ic_nav_supersub", Theme = "@style/TerminalActivityTheme")]      
  public class SuperheatSubcoolActivity : IONActivity {

    /// <summary>
    /// The name of the fluid that is to be used for the activity.
    /// </summary>
    public const string EXTRA_FLUID_NAME = "ION.Droid.Activity.extra.FLUID_NAME";
    /// <summary>
    /// The int representation of a Fluid.EState.
    /// </summary>
    public const string EXTRA_FLUID_STATE = "ION.Droid.Activity.extra.FLUID_STATE";
    /// <summary>
    /// The extra that is used if you wish to provide a pressure sensor to the activity.
    /// </summary>
    public const string EXTRA_PRESSURE_SENSOR = "ION.Droid.Activity.extra.PRESSURE_SENSOR";
    /// <summary>
    /// The extra that is used if the wish to provide a temperature sensor to the activity.
    /// </summary>
    public const string EXTRA_TEMPERATURE_SENSOR = "ION.Droid.Activity.extra.TEMPERATURE_SENSOR";

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
    /// The view that will display (allow editing of) the pressure sensor's measurement.
    /// </summary>
    /// <value>The pressure entry view.</value>
    private TextView saturatedTemperatureTextView { get; set; }
    /// <summary>
    /// The view that will display the pressure sensor's unit.
    /// </summary>
    /// <value>The pressure unit view.</value>
    private TextView saturatedTemperatureUnitView { get; set; }

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
    /// The text view that will display the fluid state for the calculation.
    /// </summary>
    /// <value>The fluid state text view.</value>
    private TextView fluidStateTextView { get; set; }
    /// <summary>
    /// The view that will display the calculated measurement.
    /// </summary>
    /// <value>The calculation text view.</value>
    private TextView calculationTextView { get; set; }

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
    /// The pt chart that will perform the calculations for the fluid.
    /// </summary>
    /// <value>The point chart.</value>
    private PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;

        var name = __ptChart.fluid.name;
        fluidColorView.SetBackgroundColor(new Color(ion.fluidManager.GetFluidColor(name)));
        fluidNameView.Text = name;
        fluidPhaseToggleView.Visibility = (__ptChart.fluid.mixture) ? ViewStates.Visible : ViewStates.Invisible;
        fluidPhaseToggleView.Checked = __ptChart.state == Fluid.EState.Bubble;
        UpdateCalculationMeasurements();
      }
    } PTChart __ptChart;

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

        if (value == null) {
          value = new ManualSensor(ESensorType.Pressure, true);
        }

        __pressureSensor = value;

        __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;
        OnPressureSensorChanged(__pressureSensor);

        if (value is GaugeDeviceSensor) {
          pressureSensorIconView.SetImageBitmap(cache.GetBitmap(((GaugeDeviceSensor)value).device.GetDeviceIcon()));
          pressureEntryView.Enabled = false;
          pressureClearView.Visibility = ViewStates.Gone;
        } else {
          pressureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
          pressureEntryView.Enabled = isPressureLocked;
          pressureClearView.Visibility = ViewStates.Visible;
        }
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

        if (value == null) {
          value = new ManualSensor(ESensorType.Temperature, false);
        }

        __temperatureSensor = value;

        __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;
        OnTemperatureSensorChanged(__temperatureSensor);

        if (value is GaugeDeviceSensor) {
          temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(((GaugeDeviceSensor)value).device.GetDeviceIcon()));
          temperatureEntryView.Enabled = false;
          temperatureClearView.Visibility = ViewStates.Gone;
        } else {
          temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
          temperatureEntryView.Enabled = isTemperatureLocked;
          temperatureClearView.Visibility = ViewStates.Visible;
        }
      }
    } Sensor __temperatureSensor;

    /// <summary>
    /// Whether or not the pressure sensor is locked.
    /// </summary>
    /// <value><c>true</c> if is pressure locked; otherwise, <c>false</c>.</value>
    private bool isPressureLocked {
      get {
        return __isPressureLocked;
      }
      set {
        __isPressureLocked = value;
        pressureLockIconView.Visibility = value ? ViewStates.Visible : ViewStates.Invisible;
      }
    } bool __isPressureLocked;

    /// <summary>
    /// Whether or not the temperature sensor is locked.
    /// </summary>
    /// <value><c>true</c> if is temperature locked; otherwise, <c>false</c>.</value>
    private bool isTemperatureLocked {
      get {
        return __isTemperatureLocked;
      }

      set {
        __isTemperatureLocked = value;
        temperatureLockIconView.Visibility = value ? ViewStates.Visible : ViewStates.Invisible;
      }
    } bool __isTemperatureLocked;

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override async void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_superheat_subcool);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_supersub, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      __pressureSensor = new ManualSensor(ESensorType.Pressure);
      __pressureSensor.onSensorStateChangedEvent += OnPressureSensorChanged;

      __temperatureSensor = new ManualSensor(ESensorType.Temperature, false);
      __temperatureSensor.onSensorStateChangedEvent += OnTemperatureSensorChanged;

      FindViewById(Resource.Id.fluid).SetOnClickListener(new ViewClickAction((view) => {
        var i = new Intent(this, typeof(FluidManagerActivity));
        i.SetAction(Intent.ActionPick);
        i.PutExtra(FluidManagerActivity.EXTRA_SELECTED, ion.fluidManager.lastUsedFluid.name);
        StartActivityForResult(i, REQUEST_FLUID);
      }));

      fluidColorView = FindViewById(Resource.Id.color);
      fluidNameView = FindViewById<TextView>(Resource.Id.name);
      fluidPhaseToggleView = FindViewById<Switch>(Resource.Id.state);
      fluidPhaseToggleView.SetOnCheckedChangeListener(new ViewCheckChangedAction((v, isChecked) => {
        if (isChecked) {
          ptChart = PTChart.New(ion, Fluid.EState.Bubble, ptChart.fluid);
        } else {
          ptChart = PTChart.New(ion, Fluid.EState.Dew, ptChart.fluid);
        }
      }));

      InitPressureWidgets();
      InitSaturatedTemperatureWidgets();
      InitTemperatureWidgets();

      pressureSensor.unit = ion.defaultUnits.pressure;
      temperatureSensor.unit = ion.defaultUnits.temperature;

      if (Intent.HasExtra(EXTRA_FLUID_NAME)) {
        var name = Intent.GetStringExtra(EXTRA_FLUID_NAME);
        var fluid = await ion.fluidManager.GetFluidAsync(name);

        var state = (Fluid.EState)Intent.GetIntExtra(EXTRA_FLUID_STATE, (int)Fluid.EState.Dew);

        ptChart = PTChart.New(ion, state, fluid);
      } else {
        ptChart = PTChart.New(ion, Fluid.EState.Dew);
      }

      isPressureLocked = false;
      if (Intent.HasExtra(EXTRA_PRESSURE_SENSOR)) {
        var sp = Intent.GetParcelableExtra(EXTRA_PRESSURE_SENSOR) as SensorParcelable;
        if (sp != null) {
          isPressureLocked = true;
          pressureSensor = sp.Get(ion);
        }
      }

      isTemperatureLocked = false;
      if (Intent.HasExtra(EXTRA_TEMPERATURE_SENSOR)) {
        var sp = Intent.GetParcelableExtra(EXTRA_TEMPERATURE_SENSOR) as SensorParcelable;
        if (sp != null) {
          isTemperatureLocked = true;
          temperatureSensor = sp.Get(ion);
        }
      }

      UpdateCalculationMeasurements();
    }


    /// <summary>
    /// Raises the resume event.
    /// </summary>
    protected override void OnResume() {
      base.OnResume();
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
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

    /// <Docs>The integer request code originally supplied to
    ///  startActivityForResult(), allowing you to identify who this
    ///  result came from.</Docs>
    /// <param name="data">An Intent, which can return result data to the caller
    ///  (various data can be attached to Intent "extras").</param>
    /// <summary>
    /// Raises the activity result event.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      if (Result.Ok != resultCode) {
        base.OnActivityResult(requestCode, resultCode, data);
        return;
      }

      switch (requestCode) {
        case REQUEST_FLUID:
          var fluidName = data?.GetStringExtra(FluidManagerActivity.EXTRA_SELECTED);
          if (fluidName != null) {
            // TODO ahodder@appioninc.com: loading dialog?
            var fluid = await ion.fluidManager.GetFluidAsync(fluidName);
            var state = (ptChart == null) ? Fluid.EState.Dew : ptChart.state;
            ptChart = PTChart.New(ion, state, fluid);
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
          if (!"".Equals(text)) {
            pressureSensor.measurement = pressureSensor.unit.OfScalar(double.Parse(text));
          }
        } catch (System.Exception e) {
          ClearInput();
        }
      });

      pressureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!(temperatureSensor is GaugeDeviceSensor)) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_DEVICE_FILTER, (int)EDeviceFilter.Pressure);
          StartActivityForResult(i, REQUEST_PRESSURE_SENSOR);
        }
      }));

      pressureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!isPressureLocked) {
          pressureSensor = new ManualSensor(ESensorType.Pressure, true);
          pressureEntryView.Enabled = true;
        }
      }));

      pressureClearView.SetOnClickListener(new ViewClickAction((view) => {
        SetPressureInputQuietly("");
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
    /// Initializes the saturated temperature widgets for the activity.
    /// </summary>
    private void InitSaturatedTemperatureWidgets() {
      var satTempView = FindViewById(Resource.Id.ptchart_saturated_temperature);

      saturatedTemperatureTextView = satTempView.FindViewById<TextView>(Resource.Id.measurement);
      saturatedTemperatureUnitView = satTempView.FindViewById<TextView>(Resource.Id.unit);

      var calculations = FindViewById(Resource.Id.ptchart_calculations);
      fluidStateTextView = calculations.FindViewById<TextView>(Resource.Id.title);
      calculationTextView = calculations.FindViewById<TextView>(Resource.Id.measurement);
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
          if (!"".Equals(text)) {
            temperatureSensor.measurement = temperatureSensor.unit.OfScalar(double.Parse(text));
          }
        } catch (System.Exception e) {
          ClearInput();
        }
      });

      temperatureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!isTemperatureLocked) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_DEVICE_FILTER, (int)EDeviceFilter.Temperature);
          StartActivityForResult(i, REQUEST_TEMPERATURE_SENSOR);
        }
      }));

      temperatureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!isTemperatureLocked) {
          temperatureSensor = new ManualSensor(ESensorType.Temperature, false);
          temperatureEntryView.Enabled = true;
        }
      }));

      temperatureClearView.SetOnClickListener(new ViewClickAction((view) => {
        SetTemperatureInputQuietly("");
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
      if (!pressureEntryView.HasFocus) {
        SetPressureInputQuietly(sensor.ToFormattedString());
      }
      pressureUnitView.Text = sensor.unit.ToString();

      UpdateCalculationMeasurements();
    }

    /// <summary>
    /// Called when the temperature sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnTemperatureSensorChanged(Sensor sensor) {
      if (!temperatureEntryView.HasFocus) {
        SetTemperatureInputQuietly(sensor.ToFormattedString());
      }
      temperatureUnitView.Text = sensor.unit.ToString();

      UpdateCalculationMeasurements();
    }

    /// <summary>
    /// Updates the state of the calculation measurement.
    /// </summary>
    private void UpdateCalculationMeasurements() {
      var tu = temperatureSensor.unit;

      var satTemp = ptChart.GetTemperature(pressureSensor).ConvertTo(tu);
      saturatedTemperatureTextView.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, satTemp);
      saturatedTemperatureUnitView.Text = temperatureSensor.unit.ToString();

      var calculation = ptChart.CalculateSystemTemperatureDelta(pressureSensor.measurement,
        temperatureSensor.measurement, pressureSensor.isRelative).ConvertTo(tu);

      if (!ptChart.fluid.mixture) {
        if (calculation < 0) {
          // Bubble
          ptChart.state = Fluid.EState.Bubble;
        } else {
          // Dew
          ptChart.state = Fluid.EState.Dew;
        }
        calculationTextView.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, calculation.Abs(), true);
      } else {
        calculationTextView.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, calculation, true);
      }

      switch (ptChart.state) {
        case Fluid.EState.Bubble:
          fluidStateTextView.Text = GetString(Resource.String.fluid_sc_abrv);
          fluidStateTextView.SetBackgroundColor(new Android.Graphics.Color(GetColor(Resource.Color.red)));          
          break;
        case Fluid.EState.Dew:
          fluidStateTextView.Text = GetString(Resource.String.fluid_sh_abrv);
          fluidStateTextView.SetBackgroundColor(new Android.Graphics.Color(GetColor(Resource.Color.blue)));
          break;
      }
    }

    /// <summary>
    /// Clears the input to the pressure and temperature entries.
    /// </summary>
    private void ClearInput() {
      SetPressureInputQuietly("");
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

