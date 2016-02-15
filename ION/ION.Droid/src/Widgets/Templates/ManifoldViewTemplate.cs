namespace ION.Droid.Widgets.Templates {

  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Devices;

  using ION.Droid.Devices;
  using ION.Droid.Util;

  /// <summary>
  /// A ManifoldViewTemplate is a view template that will mangage the content of a manifold within a view. As all
  /// manifold views operate in the same fashion, this template will be used for all instances of viewers.
  /// </summary>
  /// <code>
  /// ManifoldViewTemplate requires the following contract:
  ///   View            | id
  /// ------------------+---------------------------------
  ///   TextView        | Resource.Id.name
  ///   TextView        | Resource.Id.measurement
  ///   TextView        | Resource.Id.alarm
  ///   TextView        | Resource.Id.status
  ///   TextView        | Resource.Id.unit
  ///   ImageView       | Resource.Id.battery
  ///   ImageView       | Resource.Id.connection
  ///   ImageView       | Resource.Id.icon
  /// </code>
  public class ManifoldViewTemplate : ViewTemplate<Manifold> {

    public BitmapCache cache { get; private set; }

    public TextView title { get; private set; }
    public TextView measurement { get; private set; }
    public TextView alarm { get; private set; }
    public TextView status { get; private set; }
    public TextView unit { get; private set; }

    public ImageView battery { get; private set; }
    public ImageView connection { get; private set; }
    public ImageView icon { get; private set; }

    public Manifold manifold { get; private set; }

    private int lastBattery;
    
    public ManifoldViewTemplate(View view, BitmapCache cache) : base(view) {
      this.cache = cache;

      title = view.FindViewById<TextView>(Resource.Id.name);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
      alarm = view.FindViewById<TextView>(Resource.Id.alarm);
      status = view.FindViewById<TextView>(Resource.Id.status);
      unit = view.FindViewById<TextView>(Resource.Id.unit);

      battery = view.FindViewById<ImageView>(Resource.Id.battery);
      connection = view.FindViewById<ImageView>(Resource.Id.connection);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);

      lastBattery = -1;
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(Manifold t) {
      manifold = t;

      manifold.onManifoldEvent += OnManifoldEvent;

      Invalidate();
    }

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected override void OnUnbind() {
      if (manifold != null) {
        manifold.onManifoldEvent -= OnManifoldEvent;
      }
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      if (manifold == null) {
        // TODO ahodder@appioninc.com: Implement a real invalidate for a null manifold.
        return;
      }
      var c = parentView.Context;
      var s = manifold.primarySensor;

      measurement.Text = s.ToFormattedString(false);
      unit.Text = s.unit.ToString();

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

      // Update Battery
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

    private void OnManifoldEvent(ManifoldEvent manifoldEvent) {
      switch (manifoldEvent.type) {
        case ManifoldEvent.EType.SensorPropertyAdded:
          break;
        case ManifoldEvent.EType.SensorPropertyRemoved:
          break;
        case ManifoldEvent.EType.SensorPropertySwapped:
          break;
        case ManifoldEvent.EType.Invalidated:
          Invalidate();
          break;
        default:
          throw new Exception("Cannot handle manifold event: " + manifoldEvent.type);
      }
      Invalidate();
    }
  }
}

