namespace ION.Droid.Fragments._Workbench {

	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class SpaceRecord : RecordAdapter.IRecord {
		// Implemented form IRecord
		public int viewType { get { return (int)WorkbenchAdapter.EViewType.Space; } }
	}

	public class SpaceViewHolder : RecyclerView.ViewHolder {
		public SpaceViewHolder(ViewGroup parent) : base(Create(parent)) {
		}

		private static View Create(ViewGroup parent) {
			var ret = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_space, parent, false);
			return ret;
		}
	}
}
