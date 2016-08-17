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

  public class DeviceSectionRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get { return (int)EViewType.Section; } }
    public bool isExpandable { get { return false; } }
    public bool isExpanded { get; set; }
    public Section section { get; set; }

    public DeviceSectionRecord(Section section) {
      this.section = section;
    }
  }

  public class DeviceSectionViewHolder : DMViewHolder {

		private DeviceSectionRecord deviceRecord;
    private TextView counter { get; set; }
    private TextView title { get; set; }
		private ImageView icon { get; set; }

    public DeviceSectionViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_device_manager_group) {
      counter = view.FindViewById<TextView>(Resource.Id.counter);
      title = view.FindViewById<TextView>(Resource.Id.title);
			icon = view.FindViewById<ImageView>(Resource.Id.icon);
    }

    public void BindTo(DeviceSectionRecord record) {
      deviceRecord = record;
			icon.SetOnClickListener(new ViewClickAction((view) => {
				record.section.actions();
			}));
      Invalidate();
    }

    public override void Unbind() {
      deviceRecord = null;
    }

    public void Invalidate() {
      view.SetBackgroundColor(new Android.Graphics.Color(view.Context.Resources.GetColor(deviceRecord.section.color)));
      counter.Text = "" + deviceRecord.section.devices.Count;
      title.SetText(deviceRecord.section.name);
    }
  }
}

