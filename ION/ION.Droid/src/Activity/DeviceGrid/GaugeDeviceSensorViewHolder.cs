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

  public class GaugeDeviceSensorViewHolder : RecyclerView.ViewHolder {

    public IION ion { get; private set; }
    public GaugeDeviceSensor sensor { get; private set; }

    private TextView type;
    private TextView measurement;
    private TextView usage;
    private TextView unit;
    private ImageView state;
    private ImageView workbench;
    private ImageView analyzer;

    private View view;
    private View container;
    private View content;

    private View bluetooth;

    public GaugeDeviceSensorViewHolder(ViewGroup parent) : base(CreateViewFrom(parent)) {
      type = ItemView.FindViewById<TextView>(Resource.Id.type);
			measurement = ItemView.FindViewById<TextView>(Resource.Id.measurement);
			usage = ItemView.FindViewById<TextView>(Resource.Id.text);
			unit = ItemView.FindViewById<TextView>(Resource.Id.unit);
			state = ItemView.FindViewById<ImageView>(Resource.Id.status);
      workbench = ItemView.FindViewById<ImageView>(Resource.Id.workbench);
      analyzer = ItemView.FindViewById<ImageView>(Resource.Id.analyzer);

      view = ItemView.FindViewById(Resource.Id.view);
      container = ItemView.FindViewById(Resource.Id.container);
      content = ItemView.FindViewById(Resource.Id.content);
      bluetooth = ItemView.FindViewById(Resource.Id.connect);
		}

    public void Bind(IION ion, GaugeDeviceSensor gds) {
      this.ion = ion;
      this.sensor = gds;

      Invalidate();
    }

    private void Invalidate() {
      if (sensor == null) {
        view.Visibility = ViewStates.Gone;
        return;
      } else {
        view.Visibility = ViewStates.Visible;
			}

      container.Visibility = ViewStates.Visible;

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
            container.Visibility = ViewStates.Gone;
						state.SetColorFilter(GetColor(Resource.Color.red));
            this.content.Visibility = ViewStates.Gone;
            content.RequestLayout();
					}
					break;
			}
	    
			usage.Visibility = ViewStates.Gone;
      type.Text = sensor.name;//sensor.device.serialNumber.deviceModel.GetTypeString();
			measurement.Text = sensor.ToFormattedString(false);
			unit.Text = sensor.unit.ToString();
			workbench.Visibility = (ion.currentWorkbench.ContainsSensor(sensor)) ? ViewStates.Visible : ViewStates.Invisible;
			analyzer.Visibility = (ion.currentAnalyzer.HasSensor(sensor)) ? ViewStates.Visible : ViewStates.Invisible;
		}

    private Color GetColor(int resource) {
      return ItemView.Context.Resources.GetColor(resource);
    }

    private static View CreateViewFrom(ViewGroup parent) {
      var ret = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_holder_grid_gauge_device_sensor, parent, false);
      return ret;
    }
  }
}
