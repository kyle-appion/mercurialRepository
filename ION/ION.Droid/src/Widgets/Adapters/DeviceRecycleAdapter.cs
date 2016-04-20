namespace ION.Droid.Widgets.Adapters {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Content.Res;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Filters;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Dialog;
  using ION.Droid.Devices;
  using ION.Droid.Sensors;
  using ION.Droid.Util;
  using ION.Droid.Views;

  public class DeviceRecycleAdapter : IONRecyclerViewAdapter {
    [Flags]
    private enum Actions {
      ConnectAll          = 1 << 0,
      DisconnectAll       = 1 << 1,
      ForgetAll           = 1 << 2,
      AddAllToWorkbench   = 1 << 3,
    }

    /// <summary>
    /// The delegate that will be notified when a sensor is clicked.
    /// </summary>
    public delegate void OnSensorReturnClicked(GaugeDeviceSensor sensor, int position);


    /// <summary>
    /// The event that will be notified of sensor return clicks.
    /// </summary>
    public event OnSensorReturnClicked onSensorReturnClicked;

    /// <summary>
    /// The filter that will filter the devices that are shown by the adapter.
    /// </summary>
    public IFilter<IDevice> deviceFilter;
    /// <summary>
    /// The filter that will filter the sensors that are shown by the adapter.
    /// </summary>
    public IFilter<Sensor> sensorFilter;

    /// <summary>
    /// The current ION instance.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The context for the adapter.
    /// </summary>
    private Context context;
    /// <summary>
    /// The application resources.
    /// </summary>
    /// <value>The resources.</value>
    private Resources resources { get; set; }
    /// <summary>
    /// The cache that will contain all of the bitmaps for the adapter.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache { get; set; }

    /// <summary>
    /// The records that are contained within the adapter.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();

    // Overridden from RecyclerView.Adapter
    public override int ItemCount {
      get {
        return records.Count;
      }
    }

    public DeviceRecycleAdapter(Context context) {
      this.context = context;
      resources = context.Resources;
      ion = AppState.context;
      cache = new BitmapCache(resources);
    }

    // Overridden from RecyclerView.Adapter
    public override int GetItemViewType(int position) {
      var record = records[position];

      return (int)record.viewType;
    }

    // Overridden from RecyclerView.Adapter
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Category:
          return new CategoryViewHolder(this, li.Inflate(Resource.Layout.list_item_device_manager_group, parent, false));
        case EViewType.Device:
          return new GaugeDeviceViewHolder(this, li.Inflate(Resource.Layout.list_item_device_manager_device, parent, false), cache);
        case EViewType.Sensor:
          return new SensorViewHolder(this, li.Inflate(Resource.Layout.list_item_device_manager_sensor, parent, false), cache);
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    // Overridden from RecyclerView.Adapter
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var viewType = GetItemViewType(position);

      switch ((EViewType)viewType) {
        case EViewType.Category:
          var cr = records[position] as CategoryRecord;
          if (cr != null) {
            (holder as CategoryViewHolder)?.BindTo(cr);
          }
          break;
        case EViewType.Device:
          var gr = records[position] as GaugeDeviceRecord;
          if (gr != null) {
            (holder as GaugeDeviceViewHolder)?.BindTo(gr, OnGaugeDeviceClicked);
          }
          break;
        case EViewType.Sensor:
          var sr = records[position] as SensorRecord;
          if (sr != null) {
            (holder as SensorViewHolder)?.BindTo(sr, OnSensorRecordClicked);
          }
          break;
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    // Overridden from RecyclerView.Adapter
    public override void OnViewDetachedFromWindow(Java.Lang.Object holder) {
      base.OnViewDetachedFromWindow(holder);

      var record = holder as DeviceViewHolder;
      record?.OnUnbind();
    }

    /// <summary>
    /// Sets the content of the adapter.
    /// </summary>
    /// <param name="devices">Devices.</param>
    public void SetDevices(List<IDevice> devices) {
      var connected = new List<IDevice>();
      var broadcasting = new List<IDevice>();
      var newDevices = new List<IDevice>();
      var available = new List<IDevice>();
      var disconnected = new List<IDevice>();

      foreach (var device in devices) {
        if (!deviceFilter.Matches(device)) {
          continue;
        }

        var con = device.connection;
        switch (con.connectionState) {
          case EConnectionState.Connected:
            connected.Add(device);
            break;
          case EConnectionState.Broadcasting:
            broadcasting.Add(device);
            break;
          case EConnectionState.Connecting:
            goto case EConnectionState.Resolving;
          case EConnectionState.Resolving:
            available.Add(device);
            break;
          default:
            if (device.isNearby) {
              if (ion.deviceManager.IsDeviceKnown(device)) {
                available.Add(device);
              } else {
                newDevices.Add(device);
              }
            } else {
              disconnected.Add(device);
            }
            break;
        }
      }

      // Add the devices to the record list.
      records.Clear();

      // Add the connected devices.
      if (connected.Count > 0) {
        AddGaugeDevicesToRecords(records, resources.GetString(Resource.String.connected), Resource.Color.green, connected, () => {
          BuildBatchOptionsDialog(Actions.DisconnectAll | Actions.ForgetAll | Actions.AddAllToWorkbench,
            Resource.String.device_manager_batch_connected_actions,
            connected);
        });
      }

      // Add the broadcasting devices.
      if (broadcasting.Count > 0) {
        AddGaugeDevicesToRecords(records, resources.GetString(Resource.String.long_range_mode), Resource.Color.light_blue, broadcasting, () => {
          BuildBatchOptionsDialog(Actions.AddAllToWorkbench,
            Resource.String.device_manager_batch_long_range_actions,
            broadcasting);
        });
      }

      // Add the new devices.
      if (newDevices.Count > 0) {
        AddGaugeDevicesToRecords(records, resources.GetString(Resource.String.device_manager_new_devices_found), Resource.Color.light_gray, newDevices, () => {
          BuildBatchOptionsDialog(Actions.ConnectAll,
            Resource.String.device_manager_batch_new_device_actions,
            newDevices);
        });
      }

      // Add the available devices.
      if (available.Count > 0) {
        AddGaugeDevicesToRecords(records, resources.GetString(Resource.String.available), Resource.Color.yellow, available, () => {
          BuildBatchOptionsDialog(Actions.ConnectAll | Actions.ForgetAll | Actions.AddAllToWorkbench,
            Resource.String.device_manager_batch_available_actions,
            available);
        });
      }

      // Add the disconnected devices.
      if (disconnected.Count > 0) {
        AddGaugeDevicesToRecords(records, resources.GetString(Resource.String.disconnected), Resource.Color.red, disconnected, () => {
          BuildBatchOptionsDialog(Actions.ConnectAll | Actions.ForgetAll | Actions.AddAllToWorkbench,
            Resource.String.device_manager_batch_disconnected_actions,
            disconnected);
        });
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Adds the gauge devices to records.
    /// </summary>
    /// <param name="records">Records.</param>
    /// <param name="device">Device.</param>
    private void AddGaugeDevicesToRecords(List<IRecord> records, string title, int color, List<IDevice> devices, Action action) {
      var r = new CategoryRecord() {
        counter = 0,
        title = title,
        color = new Android.Graphics.Color(resources.GetColor(color)),
        action = action,
      };

      records.Add(r);

      foreach (var device in devices) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          r.counter += 1;
          records.Add(new GaugeDeviceRecord() {
            device = device as GaugeDevice,
            expanded = false,
          });

//          AddGaugeDeviceSensorRecords(records, device as GaugeDevice);
        }
      }
    }

    /// <summary>
    /// Builds a list of sensor records from the given device.
    /// </summary>
    /// <returns>The gauge device sensor records.</returns>
    /// <param name="device">Device.</param>
    private void AddGaugeDeviceSensorRecords(List<IRecord> records, GaugeDeviceRecord deviceRecord, int offset=-1) {
      var device = deviceRecord.device;
      var sensors = new List<GaugeDeviceSensor>();

      foreach (var sensor in device.sensors) {
        if (sensorFilter.Matches(sensor)) {
          sensors.Add(sensor);
        }
      }

      deviceRecord.sensorsAttachedCount = sensors.Count;


      if (offset == -1) {
        foreach (var s in sensors) {
          records.Add(new SensorRecord() {
            sensor = s,
          });
        }
      } else {
        foreach (var s in sensors) {
          records.Insert(offset, new SensorRecord() {
            sensor = s,
          });
        }
      }
    }

    /// <summary>
    /// Called when a gauge device's background is clicked.
    /// </summary>
    /// <param name="pos">Position.</param>
    /// <param name="gr">Gr.</param>
    private void OnGaugeDeviceClicked(int pos, GaugeDeviceRecord gr) {
      var start = pos + 1;

      if (gr.expanded) {
        records.RemoveRange(start, gr.sensorsAttachedCount);
        NotifyItemRangeRemoved(start, gr.sensorsAttachedCount);        
      } else {
        AddGaugeDeviceSensorRecords(records, gr, start);
        NotifyItemRangeInserted(start, gr.sensorsAttachedCount);
      }

      gr.expanded = !gr.expanded;
      NotifyItemChanged(start);
    }

    /// <summary>
    /// Called when a sensor's "Add" button is clicked.
    /// </summary>
    /// <param name="pos">Position.</param>
    /// <param name="sr">Sr.</param>
    private void OnSensorRecordClicked(int pos, SensorRecord sr) {
      onSensorReturnClicked(sr.sensor, pos);
    }

    /// <summary>
    /// Builds an options dialog for a device state category.
    /// </summary>
    /// <param name="actions">Actions.</param>
    /// <param name="title">Title.</param>
    /// <param name="devices">Devices.</param>
    private void BuildBatchOptionsDialog(Actions actions, int title, IEnumerable<IDevice> devices) {
      var ldb = new ListDialogBuilder(context);
      ldb.SetTitle(title);

      for (int i = 1; i <= 32; i++) {
        if ((i & (int)actions) == i) {
          switch ((Actions)i) {
            case Actions.ConnectAll:
              ldb.AddItem(Resource.String.connect_all, () => {
                foreach (var device in devices) {
                  device.connection.Connect();
                }
              });
              break;

            case Actions.DisconnectAll:
              ldb.AddItem(Resource.String.disconnect_all, () => {
                foreach (var device in devices) {
                  device.connection.Disconnect();
                }
              });
              break;

            case Actions.ForgetAll:
              ldb.AddItem(Resource.String.forget_all, () => {
                foreach (var device in devices) {
                  ion.deviceManager.DeleteDevice(device.serialNumber);
                }
              });
              break;

            case Actions.AddAllToWorkbench:
              ldb.AddItem(Resource.String.device_manager_add_all_to_workbench, () => {
                foreach (var device in devices) {
                  var gd = device as GaugeDevice;
                  if (gd != null) {
                    foreach (var sensor in gd.sensors) {
                      ion.currentWorkbench.AddSensor(sensor);
                    }
                  }
                }
              });
              break;
          }
        }
      }

      ldb.Show();
    }

    /// <summary>
    /// The types of views (and implicitly, view holders) that are in the adapter.
    /// </summary>
    public enum EViewType {
      Category,
      Device,
      Sensor,
    }

    /// <summary>
    /// The enumeration of possible device states.
    /// </summary>
    public enum State {
      Connected,
      Broadcasting,
      NewDevices,
      Available,
      Disconnected,
    }

    interface IRecord {
      EViewType viewType { get; }
    }

    class CategoryRecord : IRecord {
      // Overridden from IRecord
      public EViewType viewType {
        get {
          return EViewType.Category;
        }
      }        

      public int counter { get; set; }
      public string title { get; set; }
      public Android.Graphics.Color color { get; set; }
      public Action action { get; set; }
    }

    class GaugeDeviceRecord : IRecord {
      // Overridden from IRecord
      public EViewType viewType {
        get {
          return EViewType.Device;
        }
      }

      public GaugeDevice device;
      public int sensorsAttachedCount;
      public bool expanded;
    }

    class SensorRecord : IRecord {
      // Overridden from IRecord
      public EViewType viewType {
        get {
          return EViewType.Sensor;
        }
      }

      public GaugeDeviceSensor sensor { get; set; }
    }

    abstract class DeviceViewHolder : RecyclerView.ViewHolder {

      public DeviceViewHolder(View view) : base(view) {
      }

      public abstract void OnUnbind();
    }

    class CategoryViewHolder : DeviceViewHolder {

      private CategoryRecord record { get; set; }

      private DeviceRecycleAdapter adapter { get; set; }
      private TextView counter { get; set; }
      private TextView title { get; set; }

      public CategoryViewHolder(DeviceRecycleAdapter adapter, View view) : base(view) {
        this.adapter = adapter;
        counter = view.FindViewById<TextView>(Resource.Id.counter);
        title = view.FindViewById<TextView>(Resource.Id.title);
        view.FindViewById(Resource.Id.icon).SetOnClickListener(new ViewClickAction((v) => {
          if (record != null) {
            record.action();
          }
        }));
      }

      public void BindTo(CategoryRecord record) {
        this.record = record;
        Invalidate();
      }

      public void Invalidate() {
        var res = ItemView.Context.Resources;

        ItemView.SetBackgroundColor(record.color);

        counter.Text = record.counter + "";
        title.Text = record.title;
      }

      // Overridden from DeviceViewHolder
      public override void OnUnbind() {
      }
    }


    /// <summary>
    /// The view holder that will provide the view for a gauge device.
    /// </summary>
    class GaugeDeviceViewHolder : DeviceViewHolder {

      public delegate void OnDeviceViewBackgoundClicked(int pos, GaugeDeviceRecord gr);

      /// <summary>
      /// The cache that will hold bitmaps for resuse.
      /// </summary>
      /// <value>The cache.</value>
      private BitmapCache cache { get; set; }
      /// <summary>
      /// The device that the view holder will present
      /// </summary>
      /// <value>The device.</value>
      private GaugeDeviceRecord record { get; set; }

      private OnDeviceViewBackgoundClicked onBackgroundClicked { get; set; }

      private DeviceRecycleAdapter adapter { get; set; }
      private ImageView icon { get; set; }
      private TextView type { get; set; }
      private TextView name { get; set; }
      private ImageView arrow { get; set; }
      private View connect { get; set; }
      private ImageView status { get; set; }
      private ProgressBar progress { get; set; }


      public GaugeDeviceViewHolder(DeviceRecycleAdapter adapter, View view, BitmapCache cache) : base(view) {
        this.adapter = adapter;
        this.cache = cache;

        view.SetOnClickListener(new ViewClickAction((v) => {
          onBackgroundClicked(AdapterPosition, record);
        }));

        icon = view.FindViewById<ImageView>(Resource.Id.icon);
        type = view.FindViewById<TextView>(Resource.Id.type);
        name = view.FindViewById<TextView>(Resource.Id.name);
        arrow = view.FindViewById<ImageView>(Resource.Id.arrow);
        connect = view.FindViewById(Resource.Id.connect);
        status = view.FindViewById<ImageView>(Resource.Id.status);
        progress = view.FindViewById<ProgressBar>(Resource.Id.loading);

        connect.SetOnClickListener(new ViewClickAction((v) => {
          var device = record.device;
          switch (device.connection.connectionState) {
            case EConnectionState.Disconnected:
              device.connection.Connect();
              break;
            default:
              device.connection.Disconnect();
              break;
          }
        }));
      }

      /// <summary>
      /// Binds the view holder to the given device.
      /// </summary>
      public void BindTo(GaugeDeviceRecord record, OnDeviceViewBackgoundClicked onBackgroundClicked) {
        OnUnbind();
        this.record = record;
        this.record.device.onDeviceEvent += OnDeviceEvent;
        this.onBackgroundClicked = onBackgroundClicked;
        Invalidate();
      }

      /// <summary>
      /// Updates the views that are in the view holder.
      /// </summary>
      public void Invalidate() {
        var device = record.device;

        icon.SetImageBitmap(cache.GetBitmap(device.GetDeviceIcon()));
        type.Text = device.GetDeviceProductName();
        name.Text = device.name + "{Pv: " + device.protocol.version + "}";
        if (record.expanded) {
          arrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_downblack));
        } else {
          arrow.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_upblack));
        }

        var state = device.connection.connectionState;
        status.Visibility = ViewStates.Visible;
        switch (state) {
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

      // Overridden from DeviceViewHolder
      public override void OnUnbind() {
        if (record != null) {
          record.device.onDeviceEvent -= OnDeviceEvent;
        }
      }

      /// <summary>
      /// Called when the view holder receives a new device event.
      /// </summary>
      /// <param name="deviceEvent">Device event.</param>
      private void OnDeviceEvent(DeviceEvent deviceEvent) {
        Invalidate();
      }
    }

    /// <summary>
    /// The view holder that will provide the view for a gauge sensor.
    /// </summary>
    class SensorViewHolder : DeviceViewHolder {

      public delegate void OnSensorAddButtonClicked(int pos, SensorRecord sr);

      /// <summary>
      /// The cache that will hold bitmaps for resuse.
      /// </summary>
      /// <value>The cache.</value>
      private BitmapCache cache { get; set; }
      /// <summary>
      /// The sensor that will be displayed by the view holder.
      /// </summary>
      private SensorRecord record { get; set; }

      private OnSensorAddButtonClicked onClicked { get; set; }

      private DeviceRecycleAdapter adapter { get; set; }
      private ImageView icon { get; set; }
      private TextView type { get; set; }
      private TextView measurement { get; set; }
      private ImageButton add { get; set; }

      public SensorViewHolder(DeviceRecycleAdapter adapter, View view, BitmapCache cache) : base(view) {
        this.adapter = adapter;
        this.cache = cache;

        icon = view.FindViewById<ImageView>(Resource.Id.icon);
        type = view.FindViewById<TextView>(Resource.Id.type);
        measurement = view.FindViewById<TextView>(Resource.Id.measurement);
        add = view.FindViewById<ImageButton>(Resource.Id.add);
        add.SetOnClickListener(new ViewClickAction((v) => {
          onClicked(AdapterPosition, record);
        }));
      }

      /// <summary>
      /// Binds the view holder to the given sensor.
      /// </summary>
      public void BindTo(SensorRecord record, OnSensorAddButtonClicked onClicked) {
        OnUnbind();
        this.record = record;
        this.record.sensor.onSensorStateChangedEvent += OnSensorEvent;
        this.onClicked = onClicked;
        Invalidate();
      }

      /// <summary>
      /// Updates the views that are in the view holder.
      /// </summary>
      public void Invalidate() {
        var sensor = record.sensor;
        type.Text = sensor.type.GetTypeString();
        measurement.Text = sensor.ToFormattedString(true);
      }

      // Overridden from DeviceViewHolder
      public override void OnUnbind() {
        if (this.record != null) {
          this.record.sensor.onSensorStateChangedEvent -= OnSensorEvent;
        }
      }

      /// <summary>
      /// Called when the the sensor changes.
      /// </summary>
      /// <param name="sensor">Sensor.</param>
      private void OnSensorEvent(Sensor sensor) {
        Invalidate();
      }
    }
  }
}
