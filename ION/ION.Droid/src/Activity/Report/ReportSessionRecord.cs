using System;
using System.Linq;
using System.Threading.Tasks;

using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.App;
using ION.Core.Database;

using ION.Droid.Util;
using ION.Droid.Views;
using ION.Droid.Widgets.RecyclerViews;

﻿namespace ION.Droid.Activity.Report {
  public class ReportSessionRecord : RecordAdapter.Record<SessionRow> {
    // Implemented for RecordAdapter.Record
    public override int viewType { get { return (int)EViewType.Session; } }

    public DateTime dateRecorded { get { return data.sessionStart; } }
    public TimeSpan duration { get { return data.sessionEnd - data.sessionStart; } }
    public int sensorCount { get; private set;  }

    public bool isChecked { get; set; }

    private ReportSessionRecord(SessionRow row) : base(row) {
    }

    public static Task<ReportSessionRecord> NewAsync(IION ion, SessionRow row) {
      return Task.Factory.StartNew(() => {
        var count = ion.database.Table<SensorMeasurementRow>()
                       .Where(x => x.frn_SID == row._id)
                       .GroupBy(x => new { x.serialNumber, x.sensorIndex })
                       .Select(x => new { x.Key.serialNumber, x.Key.sensorIndex }).Count();
        return new ReportSessionRecord(row) {
          sensorCount = count,
        };
      });
    }
  }

  public class ReportSessionViewHolder : RecordAdapter.SwipeRecordViewHolder<ReportSessionRecord> {
    private TextView recorded;
    private TextView duration;
    private TextView sensors;
    private CheckBox check;
    private ReportSessionAdapter.ISessionActions actions;

    public ReportSessionViewHolder(ViewGroup parent, SwipeRecyclerView list, ReportSessionAdapter.ISessionActions actions) : base(list, Resource.Layout.view_holder_report_session, Resource.Layout.view_delete) {
      recorded = foreground.FindViewById<TextView>(Resource.Id.report_create_date);
      duration = foreground.FindViewById<TextView>(Resource.Id.report_duration);
      sensors = foreground.FindViewById<TextView>(Resource.Id.report_devices_used);
      check = foreground.FindViewById<CheckBox>(Resource.Id.checkbox);
      
      this.actions = actions;

      ((ViewGroup)recorded.Parent.Parent).SetOnClickListener(new ViewClickAction((v) => {
			  record.isChecked = !record.isChecked;
			  check.Checked = record.isChecked;
        actions.OnCheckChanged(record);
      }));
      
      background.Click += (sender, e) => {
        actions.OnDeleteClicked(record);
      };
      check.Enabled = false;
    }

    public override void Invalidate() {
      recorded.Text = record.dateRecorded.ToLocalTime().ToShortDateString() + " | " + record.dateRecorded.ToLocalTime().ToShortTimeString();
      duration.Text = record.duration.ToFriendlyString(recorded.Context);
      sensors.Text = record.sensorCount + "";
			check.Checked = record.isChecked;
		}
  }
}
