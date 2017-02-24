namespace ION.Droid.Activity.DeviceManager {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Content;
	using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  public class SensorRecord : RecordAdapter.Record<Sensor> {
    public override int viewType { get { return (int)EViewType.Sensor; } }

		public OnSensorReturnClicked onSensorClicked;
    public bool isExpandable { get { return false; } }
    public bool isExpanded { get; set; }

		public SensorRecord(Sensor sensor) : base(sensor) {
    }
  }

	public class SensorViewHolder : RecordAdapter.RecordViewHolder<SensorRecord> {
    /// <summary>
    /// The current ion instance.
    /// </summary>
    private IION ion;
    /// <summary>
    /// The cache that will hold bitmaps for resuse.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache;

    private ImageView icon;
    private TextView type;
    private TextView measurement;
    private ImageView workbench;
    private ImageView analyzer;
    private ImageButton add;

		public SensorViewHolder(ViewGroup parent, IION ion, BitmapCache cache) : base(parent, Resource.Layout.list_item_device_manager_sensor) {
      this.ion = ion;
      this.cache = cache;

			icon = ItemView.FindViewById<ImageView>(Resource.Id.icon);
			type = ItemView.FindViewById<TextView>(Resource.Id.type);
			measurement = ItemView.FindViewById<TextView>(Resource.Id.measurement);
			workbench = ItemView.FindViewById<ImageView>(Resource.Id.workbench);
			analyzer = ItemView.FindViewById<ImageView>(Resource.Id.analyzer);
			add = ItemView.FindViewById<ImageButton>(Resource.Id.add);
			add.Clickable = true;
			add.SetOnClickListener(new ViewClickAction((v) => {
				record.onSensorClicked(record.data);
			}));
    }

		public override void Bind() {
			record.data.onSensorStateChangedEvent += OnSensorEvent;
		}

    public override void Unbind() {
			if (record != null) {
				record.data.onSensorStateChangedEvent -= OnSensorEvent;
			}
    }

    public override void Invalidate() {
			var sensor = record.data;
      type.Text = sensor.type.GetTypeString();

			var gds = sensor as GaugeDeviceSensor;
			if (gds != null) {
				if (gds.removed || !gds.device.isConnected) {
					measurement.Text = "- - -";
				} else {
					measurement.Text = sensor.ToFormattedString(true);
				}
			} else {
      	measurement.Text = sensor.ToFormattedString(true);
			}

			if (record.onSensorClicked == null) {
        add.Visibility = ViewStates.Gone;
      } else {
        add.Visibility = ViewStates.Visible;
      }

      if (ion.currentWorkbench.ContainsSensor(record.data)) {
        workbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_workbench));
      } else {
				workbench.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_nav_workbench));
      }

      if (ion.currentAnalyzer.HasSensor(record.data)) {
        analyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_devices_on_analyzer));
      } else {
				analyzer.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_nav_analyzer));
      }
    }

    private void OnSensorEvent(Sensor sensorEvent) {
      Invalidate();
    }
  }
}

