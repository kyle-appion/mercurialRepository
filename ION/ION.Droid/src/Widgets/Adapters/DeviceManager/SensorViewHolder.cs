namespace ION.Droid.Widgets.Adapters.DeviceManager {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  public class SensorRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get { return (int)EViewType.Sensor; } }
    public bool isExpandable { get { return false; } }
    public bool isExpanded { get; set; }

    public Sensor sensor { get; set; }

    public SensorRecord(Sensor sensor) {
      this.sensor = sensor;
    }
  }

  public class SensorViewHolder : DMViewHolder {
    /// <summary>
    /// The current ion instance.
    /// </summary>
    private IION ion;
    /// <summary>
    /// The cache that will hold bitmaps for resuse.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache;
    /// <summary>
    /// The sensor that will be displayed by the view holder.
    /// </summary>
    private SensorRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.sensor.onSensorStateChangedEvent -= OnSensorEvent;
        }

        __record = value;

        if (__record != null) {
          __record.sensor.onSensorStateChangedEvent += OnSensorEvent;
          Invalidate();
        }
      }
    } SensorRecord __record;

    private OnSensorReturnClicked onSensorAddClickedAction;

    private ImageView icon;
    private TextView type;
    private TextView measurement;
    private ImageView workbench;
    private ImageView analyzer;
    private ImageButton add;

    public SensorViewHolder(ViewGroup parent, IION ion, BitmapCache cache) : base(parent, Resource.Layout.list_item_device_manager_sensor) {
      this.ion = ion;
      this.cache = cache;

      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      type = view.FindViewById<TextView>(Resource.Id.type);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
      workbench = view.FindViewById<ImageView>(Resource.Id.workbench);
      analyzer = view.FindViewById<ImageView>(Resource.Id.analyzer);
      add = view.FindViewById<ImageButton>(Resource.Id.add);

      add.SetOnClickListener(new ViewClickAction((v) => {
        if (onSensorAddClickedAction != null) {
          onSensorAddClickedAction(record.sensor);
        }
      }));

      workbench.SetOnClickListener(new ViewClickAction((v) => {
        var w = ion.currentWorkbench;
        if (w.ContainsSensor(record.sensor)) {
          var adb = new IONAlertDialog(parent.Context);

          adb.SetTitle(Resource.String.workbench_remove);
          adb.SetMessage(Resource.String.workbench_remove_sensor);

          adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
            var dialog = obj as Dialog;
            dialog.Dismiss();
          });

          adb.SetPositiveButton(Resource.String.ok, (obj, e) => {
            var dialog = obj as Dialog;
            dialog.Dismiss();
            w.Remove(record.sensor);
            Invalidate();
          });

          adb.Show();
        } else {
          w.AddSensor(record.sensor);
          Toast.MakeText(parent.Context, Resource.String.workbench_added_sensor, ToastLength.Short).Show();
          Invalidate();
        }
      }));

      analyzer.SetOnClickListener(new ViewClickAction((v) => {
        var a = ion.currentAnalyzer;
        if (a.HasSensor(record.sensor)) {
          var adb = new IONAlertDialog(parent.Context);

          adb.SetTitle(Resource.String.analyzer_remove_sensor);
          adb.SetMessage(Resource.String.analyzer_remove_sensor_remote);

          adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
            var dialog = obj as Dialog;
            dialog.Dismiss();
          });

          adb.SetPositiveButton(Resource.String.ok, (obj, e) => {
            var dialog = obj as Dialog;
            dialog.Dismiss();
            a.RemoveSensor(record.sensor);
            Invalidate();
          });

          adb.Show();
        } else {
          if (a.CanAddSensorToSide(Analyzer.ESide.Low)) {
            a.AddSensorToSide(Analyzer.ESide.Low, record.sensor);
            Toast.MakeText(parent.Context, Resource.String.analyzer_added_to_low, ToastLength.Short).Show();
          } else if(a.CanAddSensorToSide(Analyzer.ESide.High)) {
            a.AddSensorToSide(Analyzer.ESide.High, record.sensor);
            Toast.MakeText(parent.Context, Resource.String.analyzer_added_to_high, ToastLength.Short).Show();
          } else {
            Toast.MakeText(parent.Context, Resource.String.analyzer_full, ToastLength.Long).Show();
          }
          Invalidate();
        }
      }));
    }

    public void BindTo(SensorRecord record, OnSensorReturnClicked sensorAddClickedAction = null) {
      this.onSensorAddClickedAction = sensorAddClickedAction;
      this.record = record;
    }

    public override void Unbind() {
      this.record = null;
    }

    private void Invalidate() {
      var sensor = record.sensor;
      type.Text = sensor.type.GetTypeString();
      measurement.Text = sensor.ToFormattedString(true);

      if (onSensorAddClickedAction == null) {
        add.Visibility = ViewStates.Gone;
      } else {
        add.Visibility = ViewStates.Visible;
      }

      if (ion.currentWorkbench.ContainsSensor(record.sensor)) {
        workbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_workbench));
        workbench.SetBackgroundResource(Resource.Drawable.np_square_black_border_white_background);
      } else {
        workbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add_to_workbench));
        workbench.SetBackgroundResource(Resource.Drawable.img_button_up_gold);
      }

      if (ion.currentAnalyzer.HasSensor(record.sensor)) {
        analyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_analyzer));
        analyzer.SetBackgroundResource(Resource.Drawable.np_square_black_border_white_background);
      } else {
        analyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_add_to_analyzer));
        analyzer.SetBackgroundResource(Resource.Drawable.img_button_up_gold);
      }
    }

    private void OnSensorEvent(Sensor sensorEvent) {
      Invalidate();
    }
  }
}

