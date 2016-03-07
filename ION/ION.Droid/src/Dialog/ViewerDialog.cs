namespace ION.Droid.Dialog {

  using System;

  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Activity;
  using ION.Droid.Sensors;
  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.Templates;

  public class ViewerDialog {

    private Context context;
    private Analyzer analyzer;
    private Sensor sensor;
    private Analyzer.ESide side;
    private BitmapCache cache;

    public ViewerDialog(Context context, Analyzer analyzer, Sensor sensor, Analyzer.ESide side) {
      this.context = context;
      this.analyzer = analyzer;
      this.sensor = sensor;
      this.side = side;
      this.cache = new BitmapCache(context.Resources);
    }

    public Android.App.Dialog Show() {
      var r = context.Resources;

      var view = LayoutInflater.From(context).Inflate(Resource.Layout.analyzer_dialog_viewer, null);
      var state = view.FindViewById<TextView>(Resource.Id.state);

      switch (side) {
        case Analyzer.ESide.High:
          state.SetText(Resource.String.analyzer_side_high);
          state.SetBackgroundColor(r.GetColor(Resource.Color.red));
          state.SetTextColor(r.GetColor(Resource.Color.white));
          break;
        case Analyzer.ESide.Low:
          state.SetText(Resource.String.analyzer_side_low);
          state.SetBackgroundColor(r.GetColor(Resource.Color.blue));
          state.SetTextColor(r.GetColor(Resource.Color.white));
          break;
        default:
          state.SetText(Resource.String.analyzer_side_none);
          state.SetBackgroundColor(r.GetColor(Resource.Color.white));
          state.SetTextColor(r.GetColor(Resource.Color.black));
          break;
      }

      var template = new ManifoldViewTemplate(view, cache);
      template.Bind(new Manifold(sensor));

      var adb = new IONAlertDialog(context);

      adb.SetTitle(sensor.name);
      adb.SetView(view);

      adb.SetNegativeButton(Resource.String.cancel, (obj, which) => {
        var d = obj as Android.App.Dialog;
        d.Dismiss();
      });

      adb.SetPositiveButton(Resource.String.actions, (EventHandler<DialogClickEventArgs>)null);

      var ret = adb.Create();

      var button = ret.GetButton((int)DialogButtonType.Positive);
      button.SetOnClickListener(new ViewClickAction((v) => {
        var ldb = new ListDialogBuilder(context);
        ldb.SetTitle(string.Format(context.GetString(Resource.String.devices_actions_1arg), sensor.name));

        var dgs = sensor as GaugeDeviceSensor;

        if (dgs != null && !dgs.device.isConnected) {
          ldb.AddItem(Resource.String.reconnect, () => {
            dgs.device.connection.Connect();
          });
        }

        ldb.AddItem(Resource.String.rename, () => {
          new RenameDialog(sensor).Show(context);
        });

        ldb.AddItem(Resource.String.alarm, () => {
          var i = new Intent(context, typeof(SensorAlarmActivity));
          i.PutExtra(SensorAlarmActivity.EXTRA_SENSOR, sensor.ToParcelable());
          context.StartActivity(i);
        });

        if (dgs != null && dgs.device.isConnected) {
          ldb.AddItem(Resource.String.disconnect, () => {
            dgs.device.connection.Disconnect();
          });
        }

        ldb.AddItem(Resource.String.remove, () => {
          analyzer.RemoveSensor(sensor);
        });

        ldb.Show();
      }));

      return adb.Show();
    }
  }
}

