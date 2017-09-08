namespace ION.Droid.Fragments._Workbench {

	using System;

	using Android.Content;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	public class AddViewerRecord : RecordAdapter.IRecord {
		// Implemented from Record.Adapter.IRecord
		public int viewType { get { return (int)WorkbenchAdapter.EViewType.Add; } }

		public Action onClick;

		public AddViewerRecord(Action onClick) {
			this.onClick = onClick;
		}
	}

	public class AddViewerViewHolder : RecyclerView.ViewHolder {
		public AddViewerRecord record;

		public AddViewerViewHolder(ViewGroup parent) : base(Create(parent)) {
			var text = ItemView.FindViewById<TextView>(Resource.Id.text);
			text.SetText(Resource.String.workbench_add_viewer);
			var v = ItemView.FindViewById(Resource.Id.add);
			v.SetOnClickListener(new ViewClickAction((view) => {
				if (record != null && record.onClick != null) {
					record.onClick();
				}
			}));
		}

		private static View Create(ViewGroup parent) {
			var ret = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_add, parent, false);
			return ret;
		}
	}
}
