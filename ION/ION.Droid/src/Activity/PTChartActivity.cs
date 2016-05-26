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

  [Activity(Label = "@string/ptchart", Icon = "@drawable/ic_nav_ptconversion", Theme = "@style/TerminalActivityTheme")]      
  public class PTChartActivity : IONActivity {
    /// <summary>
    /// The extra that is used to attach a sensor paload to the activity.
    /// </summary>
    public const string EXTRA_SENSOR = "ION.Droid.Activity.extra.SENSOR";
    /// <summary>
    /// The extra that will return the secondary unit for the pt chart.
    /// </summary>
    public const string EXTRA_RETURN_UNIT = "ION.Droid.Activity.extra.RETURN_UNIT";

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
        if (__ptChart.fluid.mixture) {
          fluidPhaseToggleView.Visibility = ViewStates.Visible;
          helpView.Visibility = ViewStates.Visible;
        } else {
          fluidPhaseToggleView.Visibility = ViewStates.Invisible;
            helpView.Visibility = ViewStates.Invisible;
        }
        fluidPhaseToggleView.Checked = __ptChart.state == Fluid.EState.Bubble;
        if (sensor != null) {
          OnSensorChanged(sensor);
        } else {
          ClearInput();
        }
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
    /// The button that will show the help dialog.
    /// </summary>
    /// <value>The help view.</value>
    private ImageButton helpView { get; set; }
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
    /// Whether or not the activity has locked the sensors.
    /// </summary>
    private bool sensorLocked {
      get {
        return __sensorLocked;
      }
      set {
        __sensorLocked = value;

        pressureLockIconView.Visibility = value ? ViewStates.Visible : ViewStates.Invisible;
        temperatureLockIconView.Visibility = value ? ViewStates.Visible : ViewStates.Invisible;
      }
    } bool __sensorLocked;

    /// <summary>
    /// The non-manual sensor that the activity is using to calculate pt measurements.
    /// </summary>
    /// <value>The sensor.</value>
    private Sensor sensor {
      get {
        return __sensor;
      }
      set {
        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent -= OnSensorChanged;
        }

        __sensor = value;

        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent += OnSensorChanged;
          switch (__sensor.type) {
            case ESensorType.Pressure:
              if (value is GaugeDeviceSensor) {
                pressureSensorIconView.SetImageBitmap(cache.GetBitmap(((GaugeDeviceSensor)value).device.GetDeviceIcon()));
              } else {
                pressureSensorIconView.SetImageBitmap(cache.GetBitmap(Android.Resource.Drawable.IcMenuEdit));
              }
              temperatureSensorIconView.Visibility = ViewStates.Invisible;
              break;
            case ESensorType.Temperature:
              if (value is GaugeDeviceSensor) {
                temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(((GaugeDeviceSensor)value).device.GetDeviceIcon()));
              } else {
                temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(Android.Resource.Drawable.IcMenuEdit));
              }
              pressureSensorIconView.Visibility = ViewStates.Invisible;
              break;
          }

          pressureClearView.Visibility = ViewStates.Gone;
          temperatureClearView.Visibility = ViewStates.Gone;
        } else {
          pressureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
          temperatureSensorIconView.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add));
          pressureClearView.Visibility = ViewStates.Visible;
          temperatureClearView.Visibility = ViewStates.Visible;
        }

        Refresh();
      }
    } Sensor __sensor;

    private bool hasPressureSensor {
      get {
        return sensor != null && ESensorType.Pressure == sensor.type;
      }
    }

    private bool hasTemperatureSensor {
      get {
        return sensor != null && ESensorType.Temperature == sensor.type;
      }
    }

    private Unit pressureUnit {
      get {
        return (__sensor != null && ESensorType.Pressure == __sensor.type) ? sensor.unit : __pressureUnit;
      }
      set {
        __pressureUnit = value;
      }
    } Unit __pressureUnit;

    private Unit temperatureUnit {
      get {
        return __temperatureUnit;
      }
      set  {
        __temperatureUnit = value;
      }
    } Unit __temperatureUnit;

    // Overridden from IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_ptconversion, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_ptchart);

      FindViewById(Resource.Id.fluid).SetOnClickListener(new ViewClickAction((view) => {
        var i = new Intent(this, typeof(FluidManagerActivity));
        i.SetAction(Intent.ActionPick);
        i.PutExtra(FluidManagerActivity.EXTRA_SELECTED, ion.fluidManager.lastUsedFluid.name);
        StartActivityForResult(i, REQUEST_FLUID);
      }));

      fluidColorView = FindViewById(Resource.Id.color);
      fluidNameView = FindViewById<TextView>(Resource.Id.name);
      fluidPhaseToggleView = FindViewById<Switch>(Resource.Id.state);
      fluidPhaseToggleView.SetOnCheckedChangeListener(new ViewCheckChangedAction((button, isChecked) => {
        if (isChecked) {
          ptChart = PTChart.New(ion, Fluid.EState.Bubble, ptChart.fluid);
        } else {
          ptChart = PTChart.New(ion, Fluid.EState.Dew, ptChart.fluid);
        }
      }));
      helpView = FindViewById<ImageButton>(Resource.Id.help);
      helpView.SetOnClickListener(new ViewClickAction((v) => {
        var ldb = new IONAlertDialog(this, Resource.String.fluid_help_select_state);
        ldb.SetMessage(Resource.String.fluid_help_clarification);
        ldb.SetNegativeButton(Resource.String.ok, (obj, args) => {
          var dialog = obj as Android.App.Dialog;
          dialog.Dismiss();
        });
        ldb.Show();
      }));

      pressureUnit = ion.defaultUnits.pressure;
      temperatureUnit = ion.defaultUnits.temperature;

      InitPressureWidgets();
      InitTemperatureWidgets();

      ptChart = PTChart.New(ion, Fluid.EState.Bubble);

      if (Intent.HasExtra(EXTRA_SENSOR)) {
        var sp = (SensorParcelable)Intent.GetParcelableExtra(EXTRA_SENSOR);
        var sensor = sp.Get(ion);
        if (ESensorType.Pressure == sensor?.type || ESensorType.Temperature == sensor?.type) {
          this.sensor = sensor;
          sensorLocked = true;
        } else {
          Error("Cannot start PTChartActivity. Expected a pressure/temperature sensor, received: " + sensor?.type);
          SetResult(Result.Canceled);
          Finish();
        }
      } else {
        sensorLocked = false;
      }

      ;
    }

    // Overridden from IONActivity
    protected override void OnResume() {
      base.OnResume();
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
          }
          break;

        case REQUEST_PRESSURE_SENSOR:
          if (data != null && data.HasExtra(DeviceManagerActivity.EXTRA_SENSOR)) {
            var psp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
            sensor = psp.Get(ion);
          }
          break;

        case REQUEST_TEMPERATURE_SENSOR:
          if (data != null && data.HasExtra(DeviceManagerActivity.EXTRA_SENSOR)) {
            var tsp = (SensorParcelable)data?.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
            sensor = tsp.Get(ion);
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
          if (!"".Equals(text) && sensor == null) {
            var amount = double.Parse(text);
            var temp = ptChart.GetTemperature(pressureUnit.OfScalar(amount)).ConvertTo(temperatureUnit);
//            SetTemperatureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Temperature, temp));
            SetTemperatureInputQuietly(temp.amount + "");
          } else {
            ClearInput();
          }
        } catch (System.Exception e) {
        }
      });

      pressureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!sensorLocked && !hasTemperatureSensor) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_DEVICE_FILTER, (int)EDeviceFilter.Pressure);
          StartActivityForResult(i, REQUEST_PRESSURE_SENSOR);
        }
      }));

      pressureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!sensorLocked) {
          sensor = null;
          ClearInput();
        }
      }));

      pressureClearView.SetOnClickListener(new ViewClickAction((view) => {
        ClearInput();
      }));

      pressureUnitView.Text = pressureUnit.ToString();
      pressureUnitView.SetOnClickListener(new ViewClickAction((v) => {
        if (sensor == null || ESensorType.Temperature == sensor.type) {
          UnitDialog.Create(this, SensorUtils.DEFAULT_PRESSURE_UNITS, (obj, unit) => {
            pressureUnit = unit;
            Refresh();
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
          if (!"".Equals(text)) {
            var amount = double.Parse(text);
            var press = ptChart.GetPressure(temperatureUnit.OfScalar(amount)).ConvertTo(pressureUnit);
//            SetPressureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Pressure, press));
            SetPressureInputQuietly(press.amount + "");
          } else {
            ClearInput();
          }
        } catch (System.Exception e) {
        }
      });

      temperatureAddView.SetOnClickListener(new ViewClickAction((view) => {
        if (!sensorLocked && !hasPressureSensor) {
          var i = new Intent(this, typeof(DeviceManagerActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(DeviceManagerActivity.EXTRA_DEVICE_FILTER, (int)EDeviceFilter.Temperature);
          StartActivityForResult(i, REQUEST_TEMPERATURE_SENSOR);
        }
      }));

      temperatureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
        if (!sensorLocked) {
          sensor = null;
          ClearInput();
        }
      }));

      temperatureClearView.SetOnClickListener(new ViewClickAction((view) => {
        ClearInput();
      }));

      temperatureUnitView.Text = temperatureUnit.ToString();
      temperatureUnitView.SetOnClickListener(new ViewClickAction((v) => {
        if (sensor == null || ESensorType.Pressure == sensor.type) {
          UnitDialog.Create(this, SensorUtils.DEFAULT_TEMPERATURE_UNITS, (obj, unit) => {
            temperatureUnit = unit;
            Refresh();
          }).Show();
        }
      }));
      temperatureEntryView.AddTextChangedListener(temperatureTextWatcher);
    }

    /// <summary>
    /// Called when the pressure sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorChanged(Sensor sensor) {
      switch (sensor.type) {
        case ESensorType.Pressure:
          var temp = ptChart.GetTemperature(sensor.measurement).ConvertTo(temperatureUnit);
          SetTemperatureInputQuietly(temp.amount + "");
          SetPressureInputQuietly(sensor.measurement.amount + "");
/*
          SetTemperatureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Temperature, temp));
          SetPressureInputQuietly(sensor.ToFormattedString());
*/
          break;

        case ESensorType.Temperature:
          var press = ptChart.GetPressure(sensor.measurement).ConvertTo(pressureUnit);
          SetPressureInputQuietly(press.amount + "");
          SetTemperatureInputQuietly(sensor.measurement.amount + "");
/*
          SetPressureInputQuietly(SensorUtils.ToFormattedString(ESensorType.Pressure, press));
          SetTemperatureInputQuietly(sensor.ToFormattedString());
*/
          break;
      }
    }

    /// <summary>
    /// Refresh this instance.
    /// </summary>
    private void Refresh() {
      if (sensor != null) {
        OnSensorChanged(sensor);
        pressureEntryView.Enabled = false;
        temperatureEntryView.Enabled = false;
      } else {
        ClearInput();
        pressureEntryView.Enabled = true;
        temperatureEntryView.Enabled = true;

        pressureSensorIconView.Visibility = ViewStates.Visible;
        temperatureSensorIconView.Visibility = ViewStates.Visible;
      }

      pressureUnitView.Text = pressureUnit.ToString();
      temperatureUnitView.Text = temperatureUnit.ToString();
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

