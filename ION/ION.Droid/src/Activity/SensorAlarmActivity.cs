namespace ION.Droid.Activity {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Alarms;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Alarms;

  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Views;

  [Activity(Label = "@string/alarm_sensor", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
  public class SensorAlarmActivity : IONActivity {

    /// <summary>
    /// The key that is used to retrieve the sensor parcelable from the intent.
    /// </summary>
    public const string EXTRA_SENSOR = "ION.Droid.Activity.extra.SENSOR";

    private Sensor sensor;
    private BoundedHandler lowAlarm;
    private BoundedHandler highAlarm;

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_sensor_alarm);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

      if (Intent.HasExtra(EXTRA_SENSOR)) {
        var sp = Intent.GetParcelableExtra(EXTRA_SENSOR) as SensorParcelable;
        sensor = sp.Get(ion);
      } else {
        Error(GetString(Resource.String.error_missing_sensor_for_alarm_activity));
        Finish();
        return;
      }

      lowAlarm = new BoundedHandler(this, ion, CreateLowSensorAlarm(sensor), FindViewById(Resource.Id.low_alarm));
      highAlarm = new BoundedHandler(this, ion, CreateHighSensorAlarm(sensor), FindViewById(Resource.Id.high_alarm));
    }

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.save, menu);

			menu.FindItem(Resource.Id.save).ActionView.Click += (sender, e) => {
				SaveAndFinish();
			};

      return true;
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.save:
					SaveAndFinish();
          return true;
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);          
      }
    }

		/// <summary>
		/// Saves and finishes the alarm activity
		/// </summary>
		private void SaveAndFinish() {
			lowAlarm.Commit();
			highAlarm.Commit();
			Finish();
		}

    /// <summary>
    /// Creates a new low sensor alarm.
    /// </summary>
    /// <returns>The low sensor alarm.</returns>
    private BoundedSensorAlarm CreateLowSensorAlarm(Sensor sensor) {
      var ret = ion.alarmManager.GetAlarmOfTypeFromHost<LowSensorAlarm>(sensor);

      if (ret == null) {
        ret = new LowSensorAlarm(GetString(Resource.String.alarm_low), GetString(Resource.String.alarm_low_summary), sensor);
        ion.alarmManager.RegisterAlarmToHost(sensor, ret);
      }

      return ret;
    }

    /// <summary>
    /// Creates a new high sensor alarm.
    /// </summary>
    /// <returns>The high sensor alarm.</returns>
    /// <param name="sensor">Sensor.</param>
    private BoundedSensorAlarm CreateHighSensorAlarm(Sensor sensor) {
      var ret = ion.alarmManager.GetAlarmOfTypeFromHost<HighSensorAlarm>(sensor);

      if (ret == null) {
        ret = new HighSensorAlarm(GetString(Resource.String.alarm_high), GetString(Resource.String.alarm_high_summary), sensor);
        ion.alarmManager.RegisterAlarmToHost(sensor, ret);
      }

      return ret;
    }

    private class BoundedHandler {
      SensorAlarmActivity activity;
      BoundedSensorAlarm alarm;

      Switch toggle;
      EditText measurement;
      Button unit;
      int unitCode;

      public BoundedHandler(SensorAlarmActivity activity, IION ion, BoundedSensorAlarm alarm, View view) {
        this.activity = activity;
        this.alarm = alarm;

        toggle = view.FindViewById<Switch>(Resource.Id.toggle);
        measurement = view.FindViewById<EditText>(Resource.Id.measurement);
        unit = view.FindViewById<Button>(Resource.Id.unit);

        toggle.SetOnCheckedChangeListener(new ViewCheckChangedAction((but, check) => {
          alarm.Reset();
        }));
        unit.SetOnClickListener(new ViewClickAction((v) => {
          UnitDialog.Create(activity, alarm.sensor.supportedUnits, (obj, u) => {
            var dialog = obj as Android.App.Dialog;

            if (dialog != null) {
              dialog.Dismiss();
            }

            unit.Text = u.ToString();
            unitCode = UnitLookup.GetCode(u);
          }).Show();
        }));

				toggle.Checked = alarm.enabled;
        measurement.Text = alarm.bounds.amount + "";
        unit.Text = alarm.bounds.unit.ToString();
        unitCode = UnitLookup.GetCode(alarm.bounds.unit);
      }

      public void Commit() {
        if (toggle.Checked) {
          try {
            var title = string.Format(activity.GetString(Resource.String.alarm_low_for_1arg), alarm.sensor.name);
            var summary = string.Format(activity.GetString(Resource.String.alarm_low_for_summary_2arg), alarm.sensor.name, alarm.bounds);
            alarm.name = title;
            alarm.description = summary;
            alarm.enabled = toggle.Checked;
            alarm.bounds = UnitLookup.GetUnit(unitCode).OfScalar(double.Parse(measurement.Text));
          } catch (Exception) {
            activity.Alert(Resource.String.error_invalid_number_entry);
          }
        }
      }
    }
  }
}

