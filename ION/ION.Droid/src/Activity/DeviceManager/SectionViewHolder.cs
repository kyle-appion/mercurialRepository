namespace ION.Droid.Activity.DeviceManager {

	using System;

	using Android.Views;
	using Android.Widget;

	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	public class SectionRecord : RecordAdapter.Record<Section> {
		public override int viewType { get { return (int)EViewType.Section; } }

		public Action clickAction;

		public SectionRecord(Section section) : base(section) {
		}
	}

	public class SectionViewHolder : RecordAdapter.RecordViewHolder<SectionRecord> {
		private TextView counter { get; set; }
		private TextView title { get; set; }
		private ImageView icon { get; set; }

		public SectionViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_device_manager_group) {
			counter = ItemView.FindViewById<TextView>(Resource.Id.counter);
			title = ItemView.FindViewById<TextView>(Resource.Id.title);
			icon = ItemView.FindViewById<ImageView>(Resource.Id.icon);

			icon.SetOnClickListener(new ViewClickAction((view) => {
				if (record != null && record.clickAction != null) {
					record.clickAction();
				}
			}));
		}

		public override void Invalidate() {
			var s = record.data;
			ItemView.SetBackgroundColor(s.section.Color(ItemView.Context));
			counter.Text = "" + s.count;
			title.Text = s.section.ToLocalizedString(ItemView.Context);
		}
	}
}

