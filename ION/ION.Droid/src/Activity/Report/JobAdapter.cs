namespace ION.Droid.Activity.Report {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Views;

  using ION.Droid.Widgets.Adapters;
  using ION.Droid.Widgets.RecyclerViews;

  public class JobAdapter : SwipableRecyclerViewAdapter {

    public JobAdapter() {
    }

    public override long GetItemId(int position) {
      return records[position].viewType;
    }

    public override SwipableViewHolder OnCreateSwipableViewHolder(Android.Views.ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Job:
          return new JobViewHolder(parent, Resource.Layout.list_item_job);
        case EViewType.Session:
          return new SessionViewHolder(parent, Resource.Layout.list_item_session);
        default:
          throw new Exception("Cannot create view holder for " + (EViewType)viewType);
      }
    }

    public override void OnBindViewHolder(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder vh, int position) {
      vh.record = record;
    }

    public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
      return false;
    }

    public override Action GetViewHolderSwipeAction(int index) {
      return null;
    }

    /// <summary>
    /// Sets the jobs content for the adapter.
    /// </summary>
    /// <returns>The jobs.</returns>
    /// <param name="jobs">Jobs.</param>
    public void SetJobs(IEnumerable<JobRecord> jobs) {
      records.Clear();

      foreach (var j in jobs) {
        records.Add(j);
        records.AddRange(j.sessions);
      }

      NotifyDataSetChanged();
    }
  }
}

