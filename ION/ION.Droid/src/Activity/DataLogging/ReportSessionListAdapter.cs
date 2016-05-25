namespace ION.Droid.Activity.DataLogging {

  using System;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  using ION.Droid.Widgets.RecyclerViews;

  public class ReportSessionListAdapter : SwipableRecyclerViewAdapter {
    /// <summary>
    /// The delegate that will be notified when a sensor is clicked.
    /// </summary>
//    public delegate void OnSessionCheckChanged(SessionRow sessionRow, bool isChecked);
    /// <summary>
    /// The event  that will be notified of sensor return clicks.
    /// </summary>
//    public event OnSessionCheckChanged onSessionChecked;

    public ReportSessionListAdapter() {
    }

    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return records[position].viewType;
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var vt = (EViewType)viewType;
      switch (vt) {
        case EViewType.Session:
          return new SessionViewHolder(parent);
        default:
          throw new Exception("Cannot create view holder for: " + vt);
          
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var r = records[position];
      switch ((EViewType)r.viewType) {
        case EViewType.Session:
          ((SessionViewHolder)holder).BindTo(holder as SessionRecord);
          break;
      }
    }

    /// <summary>
    /// Sets the sessions that will be displayed by the adapter.
    /// </summary>
    /// <param name="sessions">Sessions.</param>
    public void SetSessions(IEnumerable<SessionRow> sessions) {
      records.Clear();
      foreach (var s in sessions) {
        records.Add(new SessionRecord(s));
      }
    }

    /// <summary>
    /// Queries an enumeration of the checked rows.
    /// </summary>
    /// <returns>The checkes sessions rows.</returns>
    public IEnumerable<SessionRow> GetCheckesSessionsRows() {
      var ret = new List<SessionRow>();

      foreach (var r in records) {
        var sr = r as SessionRecord;
        if (sr != null) {
          ret.Add(sr.sessionRow);
        }
      }

      return ret;
    }
  }


}

