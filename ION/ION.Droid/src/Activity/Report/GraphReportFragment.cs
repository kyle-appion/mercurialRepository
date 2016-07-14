namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	using OxyPlot.Xamarin.Android;

	using ION.Core.Database;

	// Using ION.Droid
	using Fragments;
	using Widgets.RecyclerViews;

	public class GraphReportFragment : IONFragment {

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_report_graph, container, false);

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);
		}

		public override void OnResume() {
			base.OnResume();
		}

		private class SessionRecord : SwipableRecyclerViewAdapter.IRecord {
			public int viewType { get { return 0; } }
			public SessionRow row;
			public bool isChecked;

			public SessionRecord(SessionRow row) {
				this.row = row;
			}
		}

		private class SessionViewHolder : SwipableViewHolder<SessionRecord> {
			private PlotView graph;
			private TextView title;
			private TextView check;
			private TextView serialNumber;

			public SessionViewHolder(ViewGroup parent, int resource) : base(parent, resource) {
				graph = view.FindViewById<PlotView>(Resource.Id.content);
				title = view.FindViewById<TextView>(Resource.Id.title);
				check = view.FindViewById<TextView>(Resource.Id.check);
				serialNumber = view.FindViewById<TextView>(Resource.Id.device_serial_number);
			}
		}

		private class SessionGraphAdapter : SwipableRecyclerViewAdapter {
			public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
				return new SessionViewHolder(parent, Resource.Layout.list_item_data_log_graph);
			}

			public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
				return false;
			}

			public override Action GetViewHolderSwipeAction(int index) {
				return null;
			}

			public void SetGraphs(IEnumerable<SessionRecord> sessions) { 
				records.Clear();

				records.AddRange(sessions);

				NotifyDataSetChanged();
			}
		}
	}
}

