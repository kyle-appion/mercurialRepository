namespace ION.Droid.Widgets.Adapters {

  using System;
  using System.Collections.Generic;

  using Android.Content;
  using Android.Graphics;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Util;
  using ION.Droid.Views;

  public class DeviceManagerAdapter : BaseAdapter {

    /// <summary>
    /// The event that is called when group's options button is clicked.
    /// </summary>
    public event EventHandler<GroupItem> onGroupOptionsClicked;
    /// <summary>
    /// The event that is called when 
    /// </summary>
    public event EventHandler<DeviceItem> onDeviceConnectClicked;
    /// <summary>
    /// The event that is called when a sensor's add button is clicked.
    /// </summary>
    public event EventHandler<Sensor> onSensorAddClicked;

    private const int GROUP_VIEW = 1;
    private const int DEVICE_VIEW = 2;

    // Overridden from BaseAdapter
    public override int Count {
      get {
        return items.Count;
      }
    }

    /// <summary>
    /// Queries an item at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    public IItem this[int index] {
      get {
        return items[index];
      }
    }

    /// <summary>
    /// The internal list of items that are present in the adapter
    /// </summary>
    private List<IItem> items = new List<IItem>();

    /// <summary>
    /// The bitmap cache that will 
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache { get; set; }

    public DeviceManagerAdapter(Context context, List<IItem> items) {
      cache = new BitmapCache(context.Resources);
      items.AddRange(items);
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
    public override int GetItemViewType(int position) {
      var item = this[position];

      if (item is GroupItem) {
        return GROUP_VIEW;
      } else if (item is DeviceItem) {
        return DEVICE_VIEW;
      } else {
        throw new Exception("Failed to find view type for position: " + position);
      }
    }

    // Overridden from BaseAdapter
    public override View GetView(int position, View convert, ViewGroup parent) {
      var type = GetItemViewType(position);
      switch (type) {
        case GROUP_VIEW:
          return GetGroupView(position, this[position] as GroupItem, convert, parent);
        case DEVICE_VIEW:
          return GetDeviceView(position, this[position] as DeviceItem, convert, parent);
        default:
          throw new Exception("Cannot GetView: unknown view type: " + type);
      }
    }

    /// <summary>
    /// Builds a group list view.
    /// </summary>
    /// <returns>The group view.</returns>
    /// <param name="position">Position.</param>
    /// <param name="group">Group.</param>
    /// <param name="parent">Parent.</param>
    private View GetGroupView(int position, GroupItem group, View convert, ViewGroup parent) {
      var c = parent.Context;
      var vh = convert?.Tag as GroupViewHolder;

      if (vh == null) {
        convert = LayoutInflater.From(c).Inflate(Resource.Layout.device_manager_group, parent, false);
        convert.Tag = vh = new GroupViewHolder();

        vh.counter = convert.FindViewById<TextView>(Resource.Id.counter);
        vh.title = convert.FindViewById<TextView>(Resource.Id.title);
        vh.background = convert;
        vh.options = convert.FindViewById<TextView>(Resource.Id.icon);
      }

      vh.options.SetOnClickListener(new ViewClickAction((view) => {
        NotifyOnGroupOptionsClicked(group);
      }));

      vh.counter.Text = group.count + "";
      vh.title.Text = group.name;
      vh.background.SetBackgroundColor(new Color(group.argbColor));

      return convert;
    }

    /// <summary>
    /// Builds a device list view.
    /// </summary>
    /// <returns>The device view.</returns>
    /// <param name="position">Position.</param>
    /// <param name="device">Device.</param>
    /// <param name="parent">Parent.</param>
    private View GetDeviceView(int position, DeviceItem deviceItem, View convert, ViewGroup parent) {
      var c = parent.Context;
      var vh = convert?.Tag as DeviceViewHolder;

      if (vh == null) {
        convert = LayoutInflater.From(c).Inflate(Resource.Layout.device_manager_device, parent, false);
        convert.Tag = vh = new DeviceViewHolder();

        vh.name = convert.FindViewById<TextView>(Resource.Id.name);
        vh.type = convert.FindViewById<TextView>(Resource.Id.type);
        vh.icon = convert.FindViewById<ImageView>(Resource.Id.icon);
        vh.arrow = convert.FindViewById<ImageView>(Resource.Id.arrow);
        vh.status = convert.FindViewById<ImageView>(Resource.Id.status);
        vh.connecting = convert.FindViewById<ProgressBar>(Resource.Id.loading);
        vh.connect = convert.FindViewById(Resource.Id.connect);
      }

      var device = deviceItem.device as GaugeDevice;

      vh.connect.SetOnClickListener(new ViewClickAction((view) => {
        NotifyOnDeviceConnectClicked(deviceItem);
      }));

      vh.name.Text = device.name;
      vh.type.Text = device.GetDeviceProductName();
      vh.icon.SetImageResource(cache.GetBitmap(device.GetDeviceIcon()));
      // TODO ahodder@appioninc.com: Switch arrow
      switch (device.connection.connectionState) {
        case EConnectionState.Resolving:
          goto case EConnectionState.Connecting;
        case EConnectionState.Connecting:
          vh.status.Visibility = ViewStates.Invisible;
          vh.connecting.Visibility = ViewStates.Visible;
          break;
        case EConnectionState.Broadcasting:
          goto case EConnectionState.Connected;
        case EConnectionState.Connected:
          vh.status.Visibility = ViewStates.Visible;
          vh.status.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_connected));
          vh.connecting.Visibility = ViewStates.Invisible;
          break;
        default:
          vh.status.Visibility = ViewStates.Visible;
          vh.status.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
          vh.connecting.Visibility = ViewStates.Invisible;
          break;
      }

      return convert;
    }

    private void NotifyOnGroupOptionsClicked(GroupItem group) {
      if (onGroupOptionsClicked != null) {
        onGroupOptionsClicked(this, group);
      }
    }

    private void NotifyOnDeviceConnectClicked(DeviceItem device) {
      if (this.onDeviceConnectClicked != null) {
        onDeviceConnectClicked(this, device);
      }
    }
  }

  /// <summary>
  /// The view holder that will hold the quick references for the created group view's content.
  /// </summary>
  internal class GroupViewHolder : Java.Lang.Object {
    public TextView counter, title;
    public View background, options;
  }

  internal class DeviceViewHolder : Java.Lang.Object {
    public TextView name, type;
    public ImageView icon, arrow, status;
    public View connect;
    public ProgressBar connecting;
  }

  public interface IItem {
  }

  public class GroupItem : IItem {
    public string name { get; set; }
    public int argbColor { get; set; }
    public int count { get; set; }

    public GroupItem(string name, int argbColor, int count) {
      this.name = name;
      this.argbColor = argbColor;
      this.count = count;
    }
  }

  public class DeviceItem : IItem {
    public IDevice device { get; set; }

    public DeviceItem(IDevice device) {
      this.device = device;
    }
  }
}

