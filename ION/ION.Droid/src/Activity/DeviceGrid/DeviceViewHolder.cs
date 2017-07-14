namespace ION.Droid.Activity.Grid {

	using System;

	using Android.App;
	using Android.Content;
  using Android.Graphics;
  using Android.Support.V7.Widget;
  using Android.OS;
	using Android.Views;
	using Android.Widget;

	using ION.Core.App;
  using ION.Core.Connections;
	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Sensors;

	using ION.Droid.Devices;
	using ION.Droid.Dialog;
	using ION.Droid.Sensors;
	using ION.Droid.Util;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

  public class DeviceViewHolder : RecyclerView.ViewHolder {

    public IION ion { get; private set; }
    public GaugeDeviceSensor sensor {
      get {
        return __sensor;
      }
      private set {
        lock (locker) {
          if (__sensor != null) {
            __sensor.onSensorStateChangedEvent -= OnSensorStateChangeEvent;
          }

          __sensor = value;

          if (__sensor != null) {
            __sensor.onSensorStateChangedEvent += OnSensorStateChangeEvent;
          }
        }
      }
    } GaugeDeviceSensor __sensor;

    private TextView type;
    private TextView measurement;
    private TextView usage;
    private TextView unit;
    private ImageView state;
    private ImageView workbench;
    private ImageView analyzer;

    private View content;

    private View bluetooth;

    private Handler handler;
    private object locker = new object();

    public DeviceViewHolder(ViewGroup parent) : base(CreateViewFrom(parent)) {
      type = ItemView.FindViewById<TextView>(Resource.Id.type);
			measurement = ItemView.FindViewById<TextView>(Resource.Id.measurement);
			usage = ItemView.FindViewById<TextView>(Resource.Id.text);
			unit = ItemView.FindViewById<TextView>(Resource.Id.unit);
			state = ItemView.FindViewById<ImageView>(Resource.Id.status);
      workbench = ItemView.FindViewById<ImageView>(Resource.Id.workbench);
      analyzer = ItemView.FindViewById<ImageView>(Resource.Id.analyzer);

      content = ItemView.FindViewById(Resource.Id.content);
      bluetooth = ItemView.FindViewById(Resource.Id.connect);

      handler = new Handler();
		}

    public void Bind(IION ion, GaugeDeviceSensor gds) {
      this.ion = ion;
      this.sensor = gds;

      type.Text = gds.device.serialNumber.deviceModel.GetTypeString();

      Invalidate();
    }

    private void Invalidate() {
      lock (locker) {
        if (sensor == null) {
          return;
        }

				if (!sensor.device.isConnected) {
					bluetooth.Visibility = ViewStates.Visible;
					content.Visibility = ViewStates.Gone;
				} else {
					bluetooth.Visibility = ViewStates.Gone;
					content.Visibility = ViewStates.Visible;
				}

				switch (sensor.device.connection.connectionState) {
					case EConnectionState.Connected:
						state.SetColorFilter(GetColor(Resource.Color.green));
						break;
					case EConnectionState.Broadcasting:
						state.SetColorFilter(GetColor(Resource.Color.light_blue));
						break;
					case EConnectionState.Connecting:
						state.SetColorFilter(GetColor(Resource.Color.yellow));
						break;
					case EConnectionState.Disconnected:
					default:
						if (sensor.device.isNearby) {
							state.SetColorFilter(GetColor(Resource.Color.yellow));
						} else {
							state.SetColorFilter(GetColor(Resource.Color.red));
						}
						break;
				}

				measurement.Text = sensor.ToFormattedString(false);
				unit.Text = sensor.unit.ToString();
				workbench.Visibility = (ion.currentWorkbench.ContainsSensor(sensor)) ? ViewStates.Visible : ViewStates.Invisible;
				analyzer.Visibility = (ion.currentAnalyzer.HasSensor(sensor)) ? ViewStates.Visible : ViewStates.Invisible;
      }
		}

    private Color GetColor(int resource) {
      return ItemView.Context.Resources.GetColor(resource);
    }

    private void OnSensorStateChangeEvent(Sensor s) {
      handler.Post(Invalidate);
    }

    private static View CreateViewFrom(ViewGroup parent) {
      var ret = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_holder_grid_gauge_device_sensor, parent, false);
      return ret;
    }
  }
}
