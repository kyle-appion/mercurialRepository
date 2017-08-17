using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;

using ION.Core.Database;

using ION.Droid.Views;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {
  public class ReportSessionAdapter : RecordAdapter {

    public Action<ReportSessionRecord> onSessionCheckChanged;
    public Action<ReportJobHeaderRecord> onJobRowClicked;

    private SwipeRecyclerView list;
    private DividerItemDecoration divider;

    public ReportSessionAdapter(SwipeRecyclerView list) {
      this.list = list;
    }

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
      divider = new DividerItemDecoration(recyclerView.Context, RecyclerView.Vertical);
      recyclerView.AddItemDecoration(divider);
    }

    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
      recyclerView.RemoveItemDecoration(divider);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      switch ((EViewType)viewType) {
        case EViewType.Header: {
          var vh = new ReportJobHeaderViewHolder(list, parent);
          vh.actionClick = () => {
            if (onJobRowClicked != null) {
              onJobRowClicked(vh.record);
            }
          };
          return vh;
        }
        case EViewType.Session: {
          var vh = new ReportSessionViewHolder(list, (sr) => {
            if (onSessionCheckChanged != null) {
              onSessionCheckChanged(sr);
            }
          }, parent);
          return vh;
        }
        default:
          throw new Exception("Unknown view type {" + viewType + "}");
      }
    }

    public List<SessionRow> GetCheckedSessions() {
      var ret = new List<SessionRow>();

      foreach (var record in records) {
        var r = record as ReportSessionRecord;
        if (r == null || !r.isChecked) continue;

        ret.Add(r.data);
      }

      return ret;
    }

    public void SetCheckStatusForSessionRow(int id, bool status) {
      for (int i = 0; i < ItemCount; i++) {
        var r = records[i] as ReportSessionRecord;
        if (r == null || id != r.data._id) continue;

        r.isChecked = status;
        NotifyItemChanged(i);
      }
    }

		public void SetCheckStatus(IEnumerable<SessionRow> sessions, bool status) {
			foreach (var session in sessions) {
        var r = FindRecordForSession(session);
        if (r != null) {
          r.isChecked = status;
        }
			}
      
      NotifyDataSetChanged();
		}

		public ReportSessionRecord FindRecordForSession(SessionRow session) {
      foreach (var record in records) {
        var r = record as ReportSessionRecord;
        if (r == null || !r.data.Equals(session)) continue;

        return r;
      }

      return null;
		}
  }

  /// <summary>
  /// The enumeration that will describe the types of view present in a record adapter.
  /// </summary>
  public enum EViewType {
		Header,
		Session,
  }
}
