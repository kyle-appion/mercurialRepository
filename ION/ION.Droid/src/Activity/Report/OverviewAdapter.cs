using ION.Droid.Activity.Report;
namespace ION.Droid {

	using System;
	using System.Collections.Generic;

	using Android.Support.V7.Widget;
	using Android.Views;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;

	using ION.Droid.Widgets.RecyclerViews;

	public class OverviewAdapter : RecordAdapter {

		public OverviewAdapter() {
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			return new OverviewViewHolder(parent);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			var vh = holder as OverviewViewHolder;
			vh.data = records[position] as Record;
		}

    public void SetLogs(List<SensorReportEncapsulation> encaps) {
			records.Clear();

			foreach (var encap in encaps) {
				records.Add(new SensorOverviewRecord(encap));
			}

			NotifyDataSetChanged();
		}
	}
}

