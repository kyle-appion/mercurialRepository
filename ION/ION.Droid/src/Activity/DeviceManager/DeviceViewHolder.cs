namespace ION.Droid.Activity.DeviceManager {

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
	public class DeviceRecord : RecordAdapter.Record<IDevice> {
    public override int viewType { get { return (int)EViewType.IDevice; } }

    public bool isExpandable { get { return true; } }
		public bool isExpanded;
		public Action<IDevice> onDeleteClicked;

		public DeviceRecord(IDevice device, Action<IDevice> onDeleteClicked) : base(device) {
			this.onDeleteClicked = onDeleteClicked;
    }
  }

  /// <summary>
  /// A view holder that will present a gauge device record.
  /// </summary>
	public class DeviceViewHolder : RecordAdapter.SwipeRecordViewHolder<DeviceRecord> {
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

		public DeviceViewHolder(SwipeRecyclerView rv, BitmapCache cache, Action<IDevice> onConnectClick) : base(rv, Resource.Layout.list_item_device_manager_device, Resource.Layout.list_item_button) {
      this.cache = cache;
      icon = ItemView.FindViewById<ImageView>(Resource.Id.icon);
			type = ItemView.FindViewById<TextView>(Resource.Id.type);
			name = ItemView.FindViewById<TextView>(Resource.Id.name);
			arrow = ItemView.FindViewById<ImageView>(Resource.Id.arrow);
			connect = ItemView.FindViewById(Resource.Id.connect);
			status = ItemView.FindViewById<ImageView>(Resource.Id.status);
			progress = ItemView.FindViewById<ProgressBar>(Resource.Id.loading);

      connect.SetOnClickListener(new ViewClickAction((v) => {
        if (onConnectClick != null) {
          onConnectClick(record.data);
        }
      }));

			var button = background as TextView;
			button.SetText(Resource.String.remove);
			button.SetOnClickListener(new ViewClickAction((view) => {
				if (record != null && record.onDeleteClicked != null) {
					record.onDeleteClicked(record.data);
				}
			}));
    }

    public override void Bind() {
			base.Bind();
			record.data.onDeviceEvent += OnDeviceEvent;
    }

    public override void Unbind() {
			if (record != null) {
				record.data.onDeviceEvent -= OnDeviceEvent;
			}
    }

    /// <summary>
    /// Invalidates the 
    /// </summary>
    public override void Invalidate() {
      var device = record.data;

      icon.SetImageBitmap(cache.GetBitmap(device.GetDeviceIcon()));
      type.Text = device.GetDeviceProductName();
      var nameString = device.name;
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

