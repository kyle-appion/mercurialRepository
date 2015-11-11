namespace ION.Droid.Widgets.Adapters {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Views.Animations;
  using Android.Widget;

  using ION.Core.Connections;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Sensors;

  public class WorkbenchAdapter : BaseAdapter {

    // Overridden from BaseAdapter
    public override int Count {
      get {
        return content.Count;
      }
    }

    /// <summary>
    /// The list of manifold that are to be displayed by the adapter.
    /// </summary>
    /// <value>The content.</value>
    public List<Manifold> content {
      get;
      set;
    }

    /// <summary>
    /// The indexer that will return the manifold at the given position.
    /// </summary>
    /// <param name="index">Index.</param>
    public Manifold this[int index] {
      get { 
        return content[index];
      }
    }

    /// <summary>
    /// The dictionary that will maintain a cache of sensor property views for the adapter.
    /// </summary>
    private Dictionary<Type, View> sensorPropertyViewCache = new Dictionary<Type, View>();

    public WorkbenchAdapter() {
    }

    // Overridden from BaseAdapter
    public override Java.Lang.Object GetItem(int position) {
      return null;
    }

    // Overridden from BaseAdapter
    public override long GetItemId(int position) {
      return position;
    }

    // Overridden from BaseAdapter
    public override View GetView(int position, View convert, ViewGroup parent) {
      var manifold = content[position];
      var c = parent.Context;
      var vv = convert as ViewerView;

      if (vv == null) {
        vv = new ViewerView(c, this);
      }

      vv.manifold = manifold;

      return vv;
    }

    /// <summary>
    /// The actual view that the adapter will return to the workench's list view.
    /// </summary>
    internal class ViewerView : LinearLayout {
      public Manifold manifold { 
        get {
          return __manifold;
        }
        set {
          __manifold = value;
        }
      } Manifold __manifold;

      private WorkbenchAdapter adapter { get; set; }
      private View manifoldView { get; set; }
      private List<View> propertyViews { get; set; }

      public ViewerView(Context context, WorkbenchAdapter adapter) : base(context) {
        this.adapter = adapter;
      }

      /// <summary>
      /// Gets (creating if necessary) a new manifold view (from Resources.Layout.manifold)
      /// </summary>
      /// <returns>The manifold view.</returns>
      /// <param name="convert">Convert.</param>
      private View GetManifoldView(View convert) {
        ManifoldViewHolder vh;

        if (convert == null) {
          convert = LayoutInflater.From(Context).Inflate(Resource.Layout.manifold, this, false);
          convert.Tag = vh = new ManifoldViewHolder();

          vh.title = convert.FindViewById<TextView>(Resource.Id.title);
          vh.measurement = convert.FindViewById<TextView>(Resource.Id.measurement);
          vh.status = convert.FindViewById<TextView>(Resource.Id.status);
          vh.unit = convert.FindViewById<TextView>(Resource.Id.unit);

          vh.battery = convert.FindViewById<ImageView>(Resource.Id.battery);
          vh.connection = convert.FindViewById<ImageView>(Resource.Id.connection);
          vh.icon = convert.FindViewById<ImageView>(Resource.Id.icon);
          vh.alarm = convert.FindViewById<ImageView>(Resource.Id.alarm);
        } else {
          vh = (ManifoldViewHolder)convert.Tag;
        }

        var sensor = manifold.primarySensor;

        if (sensor is GaugeDeviceSensor) {
          var gd = sensor as GaugeDeviceSensor;
          vh.title.Text = gd.device.serialNumber.deviceModel.GetTypeString();

          UpdateManifoldBatteryIcon(gd, convert, vh);
          UpdateConnectionIcon(gd, convert, vh);

          if (gd.device.isConnected) {
            vh.measurement.SetTextColor(new Color(Context.GetColor(Resource.Color.black)));
            vh.status.Visibility = ViewStates.Invisible;
          } else {
            vh.measurement.SetTextColor(new Color(Context.GetColor(Resource.Color.gray)));
            vh.status.Visibility = ViewStates.Visible;
          }
        } else {
          vh.title.Text = sensor.type.GetTypeString() + ": " + sensor.name;

          vh.battery.Visibility = ViewStates.Invisible;
          vh.connection.Visibility = ViewStates.Invisible;
          vh.icon.SetImageResource(Resource.Drawable.ic_action_edit);
          vh.measurement.SetTextColor(new Color(Context.GetColor(Resource.Color.gray)));
          vh.status.Visibility = ViewStates.Invisible;
        }

        UpdateAlarm(sensor, convert, vh);
        vh.measurement.Text = sensor.ToFormattedString();
        vh.unit.Text = sensor.unit.ToString();

        return convert;
      }

      /// <summary>
      /// Updates the battery icon for the view holder.
      /// </summary>
      /// <param name="sensor">Sensor.</param>
      /// <param name="view">View.</param>
      /// <param name="vh">Vh.</param>
      private void UpdateManifoldBatteryIcon(GaugeDeviceSensor sensor, View view, ManifoldViewHolder vh) {
        var percent = sensor.device.battery;

        if (vh.lastBatteryLevel == percent) {
          return; // We don't need to update the battery
        }

        vh.battery.Visibility = ViewStates.Visible;

        if (percent >= 100) {
          vh.battery.SetImageResource(Resource.Drawable.ic_battery_horiz_100);
        } else if (percent >= 75) {
          vh.battery.SetImageResource(Resource.Drawable.ic_battery_horiz_75);
        } else if (percent >= 50) {
          vh.battery.SetImageResource(Resource.Drawable.ic_battery_horiz_50);
        } else if (percent >= 25) {
          vh.battery.SetImageResource(Resource.Drawable.ic_battery_horiz_25);
        } else if (percent >= 0) {
          vh.battery.SetImageResource(Resource.Drawable.ic_battery_horiz_empty);
        } else {
          vh.battery.Visibility = ViewStates.Invisible;
        }

        vh.lastBatteryLevel = percent;
      }

      /// <summary>
      /// Updates the connection icon for the view holder.
      /// </summary>
      /// <param name="sensor">Sensor.</param>
      /// <param name="view">View.</param>
      /// <param name="vh">Vh.</param>
      private void UpdateConnectionIcon(GaugeDeviceSensor sensor, View view, ManifoldViewHolder vh) {
        if (sensor.device.isConnected) {
          vh.connection.SetBackgroundResource(Resource.Drawable.np_rounded_rect_green);
          vh.connection.SetImageResource(Resource.Drawable.ic_bluetooth_c3_paired);
        } else {
          vh.connection.SetBackgroundResource(Resource.Drawable.np_rounded_rect_red);
          vh.connection.SetImageResource(Resource.Drawable.ic_bluetooth_c3_disconnected);
          if (EConnectionState.Connecting == sensor.device.connection.connectionState && vh.connection.Animation != null) {
            vh.connection.StartAnimation(AnimationUtils.LoadAnimation(Context, Resource.Animation.blink_slow));
          } else {
            vh.connection.ClearAnimation();
          }
        }
      }

      /// <summary>
      /// Updates the alarm icon for the view holder.
      /// </summary>
      /// <param name="sensor">Sensor.</param>
      private void UpdateAlarm(Sensor sensor, View view, ManifoldViewHolder vh) {
      }
    }

    /// <summary>
    /// An android pattern "ViewHolder" that can be used for multiple Viewer views.
    /// </summary>
    internal class ManifoldViewHolder : Java.Lang.Object {
      public TextView title, measurement, status, unit;
      public ImageView battery, connection, icon, alarm;

      public int lastBatteryLevel;
    }
  }
}

