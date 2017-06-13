namespace ION.Droid.Activity.DeviceManager {

	using System;

	using Android.Views;
	using Android.Widget;

	using ION.Droid.Widgets.RecyclerViews;

	/// <summary>
	/// A record that is used to provide empty space.
	/// </summary>
	public class SpaceRecord : RecordAdapter.IRecord {
		public int viewType { get { return (int)EViewType.Space; } }
	}

	public class SpaceViewHolder : RecordAdapter.RecordViewHolder {
		public SpaceViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_space) {
		}
	}
}
