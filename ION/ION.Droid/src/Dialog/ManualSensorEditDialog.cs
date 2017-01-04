namespace ION.Droid.Dialog {

  using System;

  using Android.Content;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Measure;

  using ION.Core.Sensors;

  using ION.Droid.Views;

  public class ManualSensorEditDialog {

    private Context context;
    private Sensor sensor;
    private EventHandler<Sensor> action;

    public ManualSensorEditDialog(Context context, ESensorType sensorType, bool isRelative, EventHandler<Sensor> action) {
      this.context = context;
      this.sensor = new ManualSensor(sensorType);
      this.action = action;

      sensor.name = context.GetString(Resource.String.manual);
    }

    public ManualSensorEditDialog(Context context, ManualSensor sensor, EventHandler<Sensor> action) {
      this.context = context;
      this.sensor = sensor;
      this.action = action;
    }

    public Android.App.Dialog Show() {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_manual_sensor_edit, null);

      var title = view.FindViewById<TextView>(Resource.Id.title);
      var name = view.FindViewById<EditText>(Resource.Id.name);
      var measurement = view.FindViewById<EditText>(Resource.Id.measurement);
      var button = view.FindViewById<Button>(Resource.Id.unit);

      var unit = sensor.unit;
      button.Text = unit.ToString();

      button.SetOnClickListener(new ViewClickAction((v) => {
        UnitDialog.Create(context, sensor.supportedUnits, (obj, u) => {
          unit = u;
          button.Text = unit.ToString();
        }).Show();
      }));

      var adb = new IONAlertDialog(context);
      adb.SetTitle(Resource.String.edit_manual_entry);
      adb.SetView(view);

      adb.SetNegativeButton(Resource.String.cancel, (obj, args) => {
        var d = obj as Android.App.Dialog;
        d.Dismiss();
      });

      adb.SetPositiveButton(Resource.String.ok_done, (obj, args) => {
        var d = obj as Android.App.Dialog;

        try {
          sensor.name = name.Text;

          var meas = double.Parse(measurement.Text);
          sensor.measurement = unit.OfScalar(meas);

          action(d, sensor);

          d.Dismiss();
        } catch (Exception e) {
          Toast.MakeText(context, Resource.String.error_failed_to_parse_number, ToastLength.Long).Show();
        }
      });

      return adb.Show();
    }
  }
}

