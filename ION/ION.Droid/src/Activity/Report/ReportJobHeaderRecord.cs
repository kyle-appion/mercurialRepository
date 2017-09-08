using System;
using System.Collections.Generic;
using System.Linq;

using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.App;
using ION.Core.Database;

using ION.Droid.Util;
using ION.Droid.Views;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {
  public class ReportJobHeaderRecord : RecordAdapter.Record<JobRow> {
    // Implemented for RecordAdapter.Record
    public override int viewType { get { return (int)EViewType.Header; } }

    public IEnumerable<SessionRow> sessions { get; private set; }

	  public ReportJobHeaderRecord(JobRow jobRow, IEnumerable<SessionRow> sessions) : base(jobRow) {
      this.sessions = sessions;
    }
  }

  public class ReportJobHeaderViewHolder : RecordAdapter.RecordViewHolder<ReportJobHeaderRecord> {
    public Action actionClick;
    private TextView title;

    public ReportJobHeaderViewHolder(SwipeRecyclerView list, ViewGroup parent) :  base(list, Resource.Layout.view_holder_report_job_header) {
      title = ItemView.FindViewById<TextView>(Resource.Id.title);
      title.Click += (sender, e) => {
			  actionClick();
		  };
      ItemView.FindViewById(Resource.Id.icon).Click += (sender, e) => {
        actionClick();
      };
    }

    public override void Invalidate() {
      if (record.data._id == 0) {
        title.Text = record.data.jobName;
      } else {
        title.Text = record.data._id.ToString("##") + "- " + record.data.jobName;
      }
    }
  }
}
