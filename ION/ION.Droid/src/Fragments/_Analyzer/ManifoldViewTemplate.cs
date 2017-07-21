namespace ION.Droid.Fragments._Analyzer {

  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Connections;
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

    public View toggle { get; private set; }

    public TextView title { get; private set; }
    public TextView measurement { get; private set; }
    public TextView alarm { get; private set; }
    public TextView status { get; private set; }
    public TextView unit { get; private set; }
    public TextView serialNumber { get; private set; }

    public ImageView battery { get; private set; }
    public ImageView connection { get; private set; }
    public ImageView icon { get; private set; }
    public ImageView arrow { get; private set; }

    public ProgressBar progress { get; private set; }

    private int lastBattery;
    
		public ManifoldViewTemplate(View view, BitmapCache cache) : base(view) {
      this.cache = cache;

			toggle = view.FindViewById(Resource.Id.toggle);

			title = view.FindViewById<TextView>(Resource.Id.name);
			measurement = view.FindViewById<TextView>(Resource.Id.measurement);
			alarm = view.FindViewById<TextView>(Resource.Id.alarm);
			status = view.FindViewById<TextView>(Resource.Id.status);
			unit = view.FindViewById<TextView>(Resource.Id.unit);
			serialNumber = view.FindViewById<TextView>(Resource.Id.device_serial_number);

			battery = view.FindViewById<ImageView>(Resource.Id.battery);
			connection = view.FindViewById<ImageView>(Resource.Id.connection);
			icon = view.FindViewById<ImageView>(Resource.Id.icon);
			arrow = view.FindViewById<ImageView>(Resource.Id.arrow);

			progress = view.FindViewById<ProgressBar>(Resource.Id.progress);

			lastBattery = -1;
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(Manifold t) {
      item.onManifoldEvent += OnManifoldEvent;
      Invalidate();
		}

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected override void OnUnbind() {
      if (item != null) {
        item.onManifoldEvent -= OnManifoldEvent;
      }
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      if (item == null) {
        // TODO ahodder@appioninc.com: Implement a real invalidate for a null manifold.
        return;
      }
      var c = parentView.Context;
      var s = item.primarySensor;

      measurement.Text = s.ToFormattedString(false);
      unit.Text = s.unit.ToString();

      var gds = s as GaugeDeviceSensor;
      var d = gds?.device;

      var ion = AppState.context;
      if (ion.alarmManager.HostHasEnabledAlarms(s)) {
        alarm.Visibility = ViewStates.Visible;
      } else {
        alarm.Visibility = ViewStates.Invisible;
      }

      if (d != null) {
        title.Text = d.serialNumber.deviceModel.GetTypeString() + ": " + s.name;
        if (serialNumber != null) {
          serialNumber.Text = gds.device.serialNumber.ToString();
        }

        progress.Visibility = ViewStates.Invisible;
        connection.Visibility = ViewStates.Visible;
        switch (d.connection.connectionState) {
          case EConnectionState.Connected:
            measurement.SetTextColor(Resource.Color.black.AsResourceColor(c));
            unit.SetTextColor(Resource.Color.black.AsResourceColor(c));

            connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_connected));
            status.Text = c.GetString(Resource.String.connected);
            status.SetTextColor(Resource.Color.green.AsResourceColor(c));
            break;
          case EConnectionState.Broadcasting:
            measurement.SetTextColor(Resource.Color.light_blue.AsResourceColor(c));
            unit.SetTextColor(Resource.Color.light_blue.AsResourceColor(c));

            connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_broadcast));
            status.Text = c.GetString(Resource.String.disconnected);
            status.SetTextColor(Resource.Color.light_blue.AsResourceColor(c));
            break;
          case EConnectionState.Disconnected:
            measurement.SetTextColor(Resource.Color.gray.AsResourceColor(c));
            unit.SetTextColor(Resource.Color.gray.AsResourceColor(c));

            connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
            status.Text = c.GetString(Resource.String.disconnected);
            status.SetTextColor(Resource.Color.red.AsResourceColor(c));
            break;
          case EConnectionState.Connecting:
            goto case EConnectionState.Resolving;
          case EConnectionState.Resolving:
            progress.Visibility = ViewStates.Visible;
            connection.Visibility = ViewStates.Invisible;
            break;
        }

        icon.SetImageBitmap(cache.GetBitmap(d.GetDeviceIcon()));

        status.Visibility = ViewStates.Visible;
        icon.Visibility = ViewStates.Visible;
      } else {
        title.Text = s.type.GetSensorTypeName() + ": " + s.name;

        status.Visibility = ViewStates.Invisible;
        connection.Visibility = ViewStates.Invisible;
        icon.Visibility = ViewStates.Invisible;
        progress.Visibility = ViewStates.Invisible;
      }

			if (serialNumber != null) {
				if (gds != null) {
					serialNumber.Text = gds.device.name + "'s Subviews";
				} else {
					serialNumber.Text = item.primarySensor.name + "'s Subviews";
				}
			}

      InvalidateBattery(d);
    }

    public void MarkAsExpanded(bool expanded) {
      if (expanded) {
        arrow.SetImageResource(Resource.Drawable.ic_arrow_upwhite);
      } else {
        arrow.SetImageResource(Resource.Drawable.ic_arrow_downwhite);
      }
    }

    private void InvalidateBattery(GaugeDevice device) {
      if (battery == null) {
        return;
      }

      if (device != null) {
        var bat = device.battery;
        if (device.isConnected && lastBattery != bat) {
					battery.Visibility = ViewStates.Visible;
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
          lastBattery = bat;
        } else {
          battery.Visibility = ViewStates.Invisible;
          lastBattery = -1;
        }
      } else {
        battery.Visibility = ViewStates.Invisible;
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
        case ManifoldEvent.EType.SecondarySensorAdded:
        	goto case ManifoldEvent.EType.Invalidated;
        case ManifoldEvent.EType.SecondarySensorRemoved:
        	goto case ManifoldEvent.EType.Invalidated;
        default:
          Log.E(this, "Cannot handle manifold event: " + manifoldEvent.type);
					break;
      }
    }
  }
}

