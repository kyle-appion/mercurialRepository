namespace ION.Droid.Activity.Grid {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Devices;

  using ION.Droid.Widgets.RecyclerViews;

  public class DeviceGridAdapter : RecyclerView.Adapter {
    public override int ItemCount {
      get {
        return sensors.Count;
      }
    }

    public GaugeDeviceSensor this[int index] {
      get {
        return sensors[index];
      }
    }

    private List<GaugeDeviceSensor> sensors = new List<GaugeDeviceSensor>();

    private IION ion;
    private IDeviceManager dm;

    private DividerItemDecoration divider;

    public DeviceGridAdapter(IION ion) {
      this.ion = ion;
      dm = ion.deviceManager;
      foreach (var device in dm.devices) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          sensors.AddRange(gd.sensors);
        }
      }
    }

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);

      divider = new DividerItemDecoration(recyclerView.Context, GridLayoutManager.Horizontal);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return new DeviceViewHolder(parent);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var vh = holder as DeviceViewHolder;
      vh.Bind(ion, this[position]);
    }
  }
}
