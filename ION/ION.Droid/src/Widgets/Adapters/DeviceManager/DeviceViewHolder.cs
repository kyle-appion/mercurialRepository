namespace ION.Droid.Widgets.Adapters.DeviceManager {

  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Connections;
  using ION.Core.Devices;

  using ION.Droid.Devices;
  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  /// <summary>
  /// A record that holds a gauge device.
  /// </summary>
  public class DeviceRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType {
      get {
        return (int)EViewType.IDevice;
      }
    }

    public IDevice device { get; set; }
    public bool isExpandable { get { return true; } }
    public bool isExpanded { get; set; }

    public DeviceRecord(IDevice device) {
      this.device = device;
    }
  }

  /// <summary>
  /// A view holder that will present a gauge device record.
  /// </summary>
  public class DeviceViewHolder : DMViewHolder {
    /// <summary>
    /// The record that is being presented
    /// </summary>
    /// <value>The record.</value>
    private DeviceRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.device.onDeviceEvent -= OnDeviceEvent;
        }

        __record = value;

        if (__record != null) {
          __record.device.onDeviceEvent += OnDeviceEvent;
          Invalidate();
        }
      }
    } DeviceRecord __record;
    /// <summary>
    /// The cache that will cache bitmaps for reuse.
    /// </summary>
    private BitmapCache cache;

    private ImageView icon { get; set; }
    private TextView type { get; set; }
    private TextView name { get; set; }
    private ImageView arrow { get; set; }
    private View connect { get; set; }
    private ImageView status { get; set; }
    private ProgressBar progress { get; set; }

    public DeviceViewHolder(ViewGroup parent, BitmapCache cache) : base(parent, Resource.Layout.list_item_device_manager_device) {
      this.cache = cache;
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      type = view.FindViewById<TextView>(Resource.Id.type);
      name = view.FindViewById<TextView>(Resource.Id.name);
      arrow = view.FindViewById<ImageView>(Resource.Id.arrow);
      connect = view.FindViewById(Resource.Id.connect);
      status = view.FindViewById<ImageView>(Resource.Id.status);
      progress = view.FindViewById<ProgressBar>(Resource.Id.loading);

      connect.SetOnClickListener(new ViewClickAction((v) => {
        switch (record.device.connection.connectionState) {
          case EConnectionState.Disconnected:
            record.device.connection.ConnectAsync();
            break;
          default:
            record.device.connection.Disconnect();
            break;
        }
      }));
    }

    public void BindTo(DeviceRecord record) {
      this.record = record;
    }

    public override void Unbind() {
      this.record = null;
    }

    /// <summary>
    /// Invalidates the 
    /// </summary>
    private void Invalidate() {
      var device = record.device;

      icon.SetImageBitmap(cache.GetBitmap(device.GetDeviceIcon()));
      type.Text = device.GetDeviceProductName();
      var nameString = device.name;
#if DEBUG
      nameString += " {Pv: " + device.protocol.version + "}"; 
#endif
      name.Text = device.name;
      if (record.isExpanded) {
        arrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_downblack));
      } else {
        arrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_upblack));
      }

      status.Visibility = ViewStates.Visible;
      switch (device.connection.connectionState) {
        case EConnectionState.Connected:
          progress.Visibility = ViewStates.Gone;
          status.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_paired));
          break;
        case EConnectionState.Broadcasting:
          progress.Visibility = ViewStates.Gone;
          status.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_broadcast));
          break;
        case EConnectionState.Connecting:
          goto case EConnectionState.Resolving;
        case EConnectionState.Resolving:
          progress.Visibility = ViewStates.Visible;
          status.Visibility = ViewStates.Gone;
          break;
        case EConnectionState.Disconnected:
          progress.Visibility = ViewStates.Gone;
          status.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_disconnected));
          break;
      }
    }

    private void OnDeviceEvent(DeviceEvent deviceEvent) {
      Invalidate();
    }
  }
}

