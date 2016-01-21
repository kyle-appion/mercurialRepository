namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Util;

  class ViewerRecord : IRecord {
    // Overridden from IRecord
    public ViewType viewType {
      get {
        return ViewType.Viewer;
      }
    }

    public Manifold manifold { get; set; }

    public ViewerRecord(Manifold manifold) {
      this.manifold = manifold;
    }
  }

  class ViewerViewHolder : WorkbenchViewHolder<ViewerRecord> {
    private WorkbenchAdapter adapter { get; set; }
    private BitmapCache cache { get; set; }

    private ViewerRecord record { get; set; }
    
    private TextView title { get; set; }
    private TextView measurement { get; set; }
    private TextView alarm { get; set; }
    private TextView status { get; set; }
    private TextView unit { get; set; }

    private ImageView battery { get; set; }
    private ImageView connection { get; set; }
    private ImageView icon { get; set; }

    private int lastBattery { get; set; }

    public ViewerViewHolder(WorkbenchAdapter adapter, BitmapCache cache, View view) : base(view) {
      this.adapter = adapter;
      this.cache = cache;
      lastBattery = -1;

      title = view.FindViewById<TextView>(Resource.Id.name);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
      alarm = view.FindViewById<TextView>(Resource.Id.alarm);
      status = view.FindViewById<TextView>(Resource.Id.status);
      unit = view.FindViewById<TextView>(Resource.Id.unit);

      battery = view.FindViewById<ImageView>(Resource.Id.battery);
      connection = view.FindViewById<ImageView>(Resource.Id.connection);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
    }

    // Overridden from ViewerRecord
    public override void BindTo(ViewerRecord record) {
      this.record = record;

      record.manifold.onManifoldChanged += OnManifoldChanged;

      var c = ItemView.Context;
      var s = record.manifold.primarySensor;

      var gds = s as GaugeDeviceSensor;
      var d = gds?.device;
      if (d != null) {
        title.Text = d.serialNumber.deviceModel.GetModelCode() + ": " + s.name;

        if (d.isConnected) {
          measurement.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.black)));
          unit.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.black)));

          connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_connected));
          status.Text = c.GetString(Resource.String.connected);
          status.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.green)));
        } else {
          measurement.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.gray)));
          unit.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.gray)));

          connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
          status.Text = c.GetString(Resource.String.disconnected);
          status.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.red)));
        }

        icon.SetImageBitmap(cache.GetBitmap(d.GetDeviceIcon()));

        status.Visibility = ViewStates.Visible;

        battery.Visibility = ViewStates.Visible;
        connection.Visibility = ViewStates.Visible;
        icon.Visibility = ViewStates.Visible;
      } else {
        status.Visibility = ViewStates.Invisible;

        battery.Visibility = ViewStates.Invisible;
        connection.Visibility = ViewStates.Invisible;
        icon.Visibility = ViewStates.Invisible;
      }

      Invalidate();
    }

    // Overridden from ViewerRecord
    public override void Unbind() {
      record.manifold.onManifoldChanged -= OnManifoldChanged;
    }

    private void Invalidate() {
      var c = ItemView.Context;
      var s = record.manifold.primarySensor;

      measurement.Text = s.ToFormattedString();
      unit.Text = s.unit.ToString();

      var gds = s as GaugeDeviceSensor;
      var d = gds?.device;
      UpdateBattery(gds);
    }

    private void UpdateBattery(GaugeDeviceSensor sensor) {
      var d = sensor.device;

      var bat = d.battery;
      if (d.isConnected) {
        if (bat >= 100) {
          battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_100));
        } else if (bat >= 75) {
          battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_75));
        } else if (bat >= 50) {
          battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_50));
        } else if (bat >= 25) {
          battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_25));
        } else {
          battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_empty));
        }
      } else {
        battery.Visibility = ViewStates.Invisible;
        lastBattery = -1;
      }
    }

    private void OnManifoldChanged(Manifold manifold) {
      Invalidate();
    }
  }
}

