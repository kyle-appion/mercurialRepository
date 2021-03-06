﻿namespace ION.Droid.Activity {

  using System;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.OS;
	using Android.Text;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Fluids;
  using ION.Core.Location;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	// Using ION.Droid
	using Devices;
	using Dialog;
	using Sensors;
	using Views;
	using Widgets;

	[Activity(Label = "@string/ptchart", Icon = "@drawable/ic_nav_ptconversion", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
	public class PTChartActivity : IONActivity {
		/// <summary>
		/// Determines whether or not the given sensor is valid for the superheat subcool calculator.
		/// </summary>
		/// <returns>The sensor valid.</returns>
		/// <param name="sensor">Sensor.</param>
		public static bool IsSensorValid(Sensor sensor) {
			return sensor == null || sensor.type == ESensorType.Pressure || sensor.type == ESensorType.Temperature;
		}

		/// <summary>
		/// The extra that is used to attach a sensor paload to the activity.
		/// </summary>
		public const string EXTRA_SENSOR = "ION.Droid.Activity.extra.SENSOR";
		/// <summary>
		/// The extra that will return the secondary unit for the pt chart.
		/// </summary>
		public const string EXTRA_RETURN_UNIT = "ION.Droid.Activity.extra.RETURN_UNIT";
		/// <summary>
		/// The extra that is used to get whether or not the fluid is locked in the activity.
		/// </summary>
		public const string EXTRA_LOCK_FLUID = "ION.Droid.Activity.extra.LOCK_FLUID";
		/// <summary>
		/// The name of the fluid that is to be used for the activity.
		/// </summary>
		public const string EXTRA_FLUID_NAME = "ION.Droid.Activity.extra.FLUID_NAME";
		/// <summary>
		/// The int representation of a Fluid.EState.
		/// </summary>
		public const string EXTRA_FLUID_STATE = "ION.Droid.Activity.extra.FLUID_STATE";
		/// <summary>
		/// The extra that is used if you with to pass a workbench manifold instead of individual sensors to the activity.
		/// This extra is paired with the index that the manifold is located at within the current app's workbench.
		/// </summary>
		public const string EXTRA_WORKBENCH_MANIFOLD = "ION.Droid.Activity.extra.WORKBENCH_MANIFOLD";
		/// <summary>
		/// The extra that is used if you with to pass a analyzer manifold instead of individual sensors to the activity.
		/// This extra is paired with the index that the Analyzer.ESide that the manifold came from within the analyzer.
		/// </summary>
		public const string EXTRA_ANALYZER_MANIFOLD = "ION.Droid.Activity.extra.ANALYZER_MANIFOLD";

		/// <summary>
		/// The value that indicates that an activity action/result was for the selection of a fluid.
		/// </summary>
		public const int REQUEST_FLUID = 1;

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
				slider.ptChart = value;

        if (__ptChart == null) {
          Log.E(this, "Why was ptchart set to null?");
        }

				var name = __ptChart.fluid.name;
				fluidColorView.SetBackgroundColor(new Color(ion.fluidManager.GetFluidColor(name)));
				fluidNameView.Text = name;
				if (__ptChart.fluid.mixture) {
					fluidPhaseToggleView.Visibility = ViewStates.Visible;
				} else {
					fluidPhaseToggleView.Visibility = ViewStates.Invisible;
				}
				fluidPhaseToggleView.Checked = __ptChart.state == Fluid.EState.Bubble;

				if (initialManifold != null) {
					initialManifold.ptChart = __ptChart;
				}
				if (_sensor != null) {
					OnSensorChanged(_sensor);
				} else {
          var pressure = Units.Pressure.PSIG.OfScalar(0);
					slider.ScrollToPressure(pressure, true);
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
    /// The current elevation that is used for calculations in pt measurements.
    /// </summary>
    private TextView elevation;

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
		/// The slider view.
		/// </summary>
		private FluidSliderView slider;


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
		/// The initial manifold that the activity started with.
		/// </summary>
		private Manifold initialManifold;

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
		private Sensor _sensor {
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
				return _sensor != null && ESensorType.Pressure == _sensor.type;
			}
		}

		private bool hasTemperatureSensor {
			get {
				return _sensor != null && ESensorType.Temperature == _sensor.type;
			}
		}

		private Unit pressureUnit {
			get {
				return (__sensor != null && ESensorType.Pressure == __sensor.type) ? _sensor.unit : __pressureUnit;
			}
			set {
				__pressureUnit = value;
				slider.pressureUnit = value;
			}
		} Unit __pressureUnit;

		private Unit temperatureUnit {
			get {
				return __temperatureUnit;
			}
			set  {
				Log.E(this, "Setting temperature unit to: " + value);
				__temperatureUnit = value;
				slider.temperatureUnit = value;
			}
		} Unit __temperatureUnit;

		// Overridden from IONActivity
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

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
          ptChart = ptChart.fluid.GetPtChart(Fluid.EState.Bubble);
				} else {
          ptChart = ptChart.fluid.GetPtChart(Fluid.EState.Dew);
				}
			}));

			slider = FindViewById<FluidSliderView>(Resource.Id.ptchart);

      pressureUnit = ion.preferences.units.pressure;
      temperatureUnit = ion.preferences.units.temperature;

      // Init elevation widgets
      var container = FindViewById(Resource.Id.elevation);
      elevation = container.FindViewById<TextView>(Resource.Id.text);

			InitPressureWidgets();
			InitTemperatureWidgets();
			var contentView = FindViewById(Resource.Id.content);
			contentView.SetOnTouchListener(new ClearFocusListener(pressureEntryView, temperatureEntryView));

			// Note: ahodder@appioninc.com: apparently we want to always change the fluid to the last used fluid per christian and kyle 1 Feb 2017
      ptChart = ion.fluidManager.lastUsedFluid.GetPtChart(Fluid.EState.Dew);

			if (Intent.HasExtra(EXTRA_WORKBENCH_MANIFOLD)) {
				var index = Intent.GetIntExtra(EXTRA_WORKBENCH_MANIFOLD, -1);
				if (index == -1) {
					Alert(Resource.String.ptchart_error_no_manifold);
					SetResult(Result.Canceled);
					Finish();
					return;
				} else {
					var manifold = ion.currentWorkbench[index];
					InitFromManifold(manifold);
				}
			} else if (Intent.HasExtra(EXTRA_ANALYZER_MANIFOLD)) {
				var index = Intent.GetIntExtra(EXTRA_ANALYZER_MANIFOLD, -1);
				var side = (Analyzer.ESide)index;

				if (index == -1) {
					Alert(Resource.String.ptchart_error_no_manifold);
					SetResult(Result.Canceled);
					Finish();
					return;
				} else {
					var manifold = ion.currentAnalyzer.GetManifoldFromSide(side);
					InitFromManifold(manifold);
				}
			} else if (Intent.HasExtra(EXTRA_SENSOR)) {
				var sp = (SensorParcelable)Intent.GetParcelableExtra(EXTRA_SENSOR);
				var s = sp.Get(ion);
				if (ESensorType.Pressure == s?.type || ESensorType.Temperature == s?.type) {
					_sensor = s;
					sensorLocked = true;
				} else {
					Error(string.Format(GetString(Resource.String.ptchart_error_cannot_start_activity_1sarg), s?.type.GetTypeString()));
					SetResult(Result.Canceled);
					Finish();
				}
			} else {
				sensorLocked = false;
			}

			ClearInput();
		}

		// Overridden from IONActivity
		protected override void OnResume() {
			base.OnResume();
      if (!sensorLocked) {
        var s = new ManualSensor(ESensorType.Pressure, false);
        s.unit = ion.preferences.units.pressure;
        s.ForceSetMeasurement(ion.preferences.units.pressure.OfScalar(0));
        OnSensorChanged(s);
        ion.PostToMainDelayed(() => {
          slider.ScrollToPressure(s.measurement, true);
        }, TimeSpan.FromMilliseconds(500));
      }
			Refresh();
			slider.onScroll += OnSliderScroll;
      ion.locationManager.onLocationChanged += OnLocationChanged;
      elevation.Text = SensorUtils.ToFormattedString(ion.locationManager.lastKnownLocation.altitude.ConvertTo(ion.preferences.units.length), true);
		}

		protected override void OnPause() {
			base.OnPause();
			slider.onScroll -= OnSliderScroll;
      ion.locationManager.onLocationChanged -= OnLocationChanged;
		}

    // Overridden from Activity
    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.help, menu);

      var item = menu.FindItem(Resource.Id.help);
      var view = item.ActionView as Button;
/*
      view.Text = GetString(Resource.String.help);
      view.SetOnClickListener(new ViewClickAction((v) => {
        OnMenuItemSelected(Resource.Id.help, menu.FindItem(Resource.Id.help));
      }));
*/

      return true;
    }

		// Overridden from Activity
		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
			  	return true;
        case Resource.Id.help:
          var adb = new IONAlertDialog(this);
          adb.SetTitle(Resource.String.help);
          adb.SetMessage(Resource.String.fluid_help_mixture_clarification);
          adb.SetNegativeButton(Resource.String.close, (sender, e) => {
          });
          adb.SetPositiveButton(Resource.String.settings, (sender, e) => {
            var i = new Intent(this, typeof(AppPreferenceActivity));
            StartActivity(i);
          });
          adb.Show();
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
            ptChart = fluid.GetPtChart(state);
					}
				break;
			}

			Refresh();
			ClearInput();
		}

    /// <summary>
    /// Called when the application's location changes.
    /// </summary>
    /// <param name="lm">Lm.</param>
    /// <param name="oldLocation">Old location.</param>
    /// <param name="newLocation">New location.</param>
    private void OnLocationChanged(ILocationManager lm, ILocation oldLocation, ILocation newLocation) {
      elevation.Text = SensorUtils.ToFormattedString(newLocation.altitude.ConvertTo(ion.preferences.units.length) , true);
    }

		/// <summary>
		/// Initializes the activity using the given manifold.
		/// </summary>
		/// <returns>The from manifold.</returns>
		/// <param name="manifold">Manifold.</param>
		private void InitFromManifold(Manifold manifold) {
			if (!IsSensorValid(manifold.primarySensor)) {
				Error(GetString(Resource.String.ptchart_error_invalid_manifold));
				SetResult(Result.Canceled);
				Finish();
				return;	
			}

			switch (manifold.primarySensor.type) {
				case ESensorType.Pressure:
					_sensor = manifold.primarySensor;
					pressureUnit = manifold.primarySensor.unit;
					if (manifold.secondarySensor?.type == ESensorType.Temperature) {
						temperatureUnit = manifold.secondarySensor.unit;
					} else {
						var sp = manifold.GetSensorPropertyOfType<PTChartSensorProperty>();
						if (sp != null) {
							temperatureUnit = sp.unit;
						}
					}
				break;
				case ESensorType.Temperature:
					_sensor = manifold.primarySensor;
					temperatureUnit = manifold.primarySensor.unit;
					if (manifold.secondarySensor?.type == ESensorType.Pressure) {
						pressureUnit = manifold.secondarySensor.unit;
					} else {
						var sp = manifold.GetSensorPropertyOfType<PTChartSensorProperty>();
						if (sp != null) {
							temperatureUnit = sp.unit;
						}
					}
				break;
			}

			initialManifold = manifold;
			sensorLocked = true;
		}

		private void OnSliderScroll(FluidSliderView slider, bool touching, Scalar pressure, Scalar temperature) {
			if (touching) {
				SetPressureInputQuietly(SensorUtils.ToFormattedString(pressure.ConvertTo(pressureUnit)));
				SetTemperatureInputQuietly(SensorUtils.ToFormattedString(temperature.ConvertTo(temperatureUnit)));
			} else {
				if (!pressureEntryView.HasFocus) {
					SetPressureInputQuietly(SensorUtils.ToFormattedString(pressure.ConvertTo(pressureUnit)));
				}

				if (!temperatureEntryView.HasFocus) {
					SetTemperatureInputQuietly(SensorUtils.ToFormattedString(temperature.ConvertTo(temperatureUnit)));
				}
			}
		}

		private void UpdateManifold(Unit unit) {
			try {
				if (initialManifold == null) {
					return;
				}

				if (initialManifold.secondarySensor != null && initialManifold.secondarySensor.unit.IsCompatible(unit)) {
					initialManifold.secondarySensor.unit = unit;
				}

				var ptchart = initialManifold.GetSensorPropertyOfType<PTChartSensorProperty>();
				if (ptchart != null) {
					ptchart.unit = unit;
				}
			} catch (System.Exception e) {
				Appion.Commons.Util.Log.E(this, "Failed to update manifold", e);
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

			pressureView.SetOnTouchListener(new ClearFocusListener(pressureEntryView));

			pressureTextWatcher = new Watcher((editable) => {
				var text = editable.ToString();
				try {
					if (!"".Equals(text) && _sensor == null) {
						var amount = double.Parse(text);
						var ps = pressureUnit.OfScalar(amount);
            var temp = ptChart.GetTemperature(ps, true).ConvertTo(temperatureUnit);
						SetTemperatureInputQuietly(temp.amount.ToString("#.##"));
						slider.ScrollToPressure(ps, false);
					} else {
						ClearInput();
					}
				} catch (Exception) {
          return;
				}
			});

			pressureAddView.SetOnClickListener(new ViewClickAction((view) => {
				if (!sensorLocked && !hasTemperatureSensor) {
					new GaugeDeviceSensorSelectDialog(this, ion, ESensorType.Pressure, (sensor) => {
						_sensor = sensor;
					}).Show();
				}
			}));

			pressureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
				if (!sensorLocked) {
					_sensor = null;
					slider.ScrollToPressure(pressureUnit.OfScalar(0), true);
				}
			}));

			pressureClearView.SetOnClickListener(new ViewClickAction((view) => {
				ClearInput();
			}));

			pressureUnitView.Text = pressureUnit.ToString();
			pressureUnitView.SetOnClickListener(new ViewClickAction((v) => {
				if (_sensor == null || ESensorType.Temperature == _sensor.type) {
					UnitDialog.Create(this, SensorUtils.DEFAULT_PRESSURE_UNITS, (obj, unit) => {
						if (initialManifold != null && initialManifold.primarySensor.type == ESensorType.Temperature) {
							UpdateManifold(unit);
						}

						var text = pressureEntryView.Text;
						var oldUnit = pressureUnit;
						pressureUnit = unit;

						try {
							if (!"".Equals(text) && _sensor == null) {
								var amount = double.Parse(text);
								var ps = oldUnit.OfScalar(amount).ConvertTo(pressureUnit);
								SetPressureInputQuietly(SensorUtils.ToFormattedString(ps));
							} else {
								ClearInput();
							}
						} catch (System.Exception) {
						}

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

			temperatureView.SetOnTouchListener(new ClearFocusListener(temperatureEntryView));

			temperatureTextWatcher = new Watcher((editable) => {
				var text = editable.ToString();
				if (!"".Equals(text)) {
					double amount = 0;
					if (double.TryParse(text, out amount)) {
						var ts = temperatureUnit.OfScalar(amount);
            var press = ptChart.GetRelativePressure(ts).ConvertTo(pressureUnit);
						SetPressureInputQuietly(press.amount.ToString("#.##"));
						slider.ScrollToTemperature(ts, false);
					}
				} else {
					ClearInput();
				}
			});

			temperatureAddView.SetOnClickListener(new ViewClickAction((view) => {
				if (!sensorLocked && !hasPressureSensor) {
					new GaugeDeviceSensorSelectDialog(this, ion, ESensorType.Temperature, (sensor) => {
						_sensor = sensor;
					}).Show();
				}
			}));

			temperatureAddView.SetOnLongClickListener(new ViewLongClickAction((view) => {
				if (!sensorLocked) {
					_sensor = null;
					slider.ScrollToTemperature(temperatureUnit.OfScalar(0), true);
				}
			}));

			temperatureClearView.SetOnClickListener(new ViewClickAction((view) => {
				ClearInput();
			}));

			temperatureUnitView.Text = temperatureUnit.ToString();
			temperatureUnitView.SetOnClickListener(new ViewClickAction((v) => {
				if (_sensor == null || ESensorType.Pressure == _sensor.type) {
					UnitDialog.Create(this, SensorUtils.DEFAULT_TEMPERATURE_UNITS, (obj, unit) => {
						if (initialManifold != null && initialManifold.primarySensor.type == ESensorType.Pressure) {
							UpdateManifold(unit);
						}

						var text = temperatureEntryView.Text;
						var oldUnit = temperatureUnit;
						temperatureUnit = unit;

						try {
							if (!"".Equals(text) && _sensor == null) {
								var amount = double.Parse(text);
								var ts = oldUnit.OfScalar(amount).ConvertTo(temperatureUnit);
								SetTemperatureInputQuietly(SensorUtils.ToFormattedString(ts));
							} else {
								ClearInput();
							}
						} catch (Exception) {
              return;
						}

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
          var temp = ptChart.GetTemperature(sensor.measurement, sensor.isRelative).ConvertTo(temperatureUnit);
					SetTemperatureInputQuietly(temp.amount.ToString("#.##") + "");
					SetPressureInputQuietly(sensor.ToFormattedString(false));
					slider.ScrollToTemperature(temp, false);
					break;

				case ESensorType.Temperature:
          var press = ptChart.GetRelativePressure(sensor.measurement).ConvertTo(pressureUnit);
					SetPressureInputQuietly(press.amount.ToString("#.##") + "");
					SetTemperatureInputQuietly(sensor.ToFormattedString(false));
					slider.ScrollToTemperature(sensor.measurement, false);
					break;
			}
		}

		/// <summary>
		/// Refresh this instance.
		/// </summary>
		private void Refresh() {
			if (_sensor != null) {
				OnSensorChanged(_sensor);
				pressureEntryView.Enabled = false;
				temperatureEntryView.Enabled = false;
				slider.Visibility = ViewStates.Invisible;
			} else {
				slider.Visibility = ViewStates.Visible;
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
      public void BeforeTextChanged(Java.Lang.ICharSequence text, int start, int count, int after) {
			}

			// Overridden from ITextWatcher
      public void OnTextChanged(Java.Lang.ICharSequence text, int start, int before, int count) {
			}
		}
	}
}

