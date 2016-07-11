namespace ION.Droid.Activity.Report {

  using System;

  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Widgets.RecyclerViews;

  public class SessionAdapter : SwipableRecyclerViewAdapter{
    public SessionAdapter() {
    }

    public override long GetItemId(int position) {
      return records[position].viewType;
    }

    public override SwipableViewHolder OnCreateSwipableViewHolder(Android.Views.ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Session:
          return new SessionViewHolder(parent, Resource.Layout.list_item_session);
        default:
          throw new Exception("Cannot create view holder for " + (EViewType)viewType);
      }
    }

    public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
      return true;
    }

    public override Action GetViewHolderSwipeAction(int index) {
      return () => {
        Toast.MakeText(recyclerView.Context, "Implement me!", ToastLength.Short).Show();
      };
    }

    /// <summary>
    /// Sets the sessions content for the adapter.
    /// </summary>
    /// <returns>The sessions.</returns>
    /// <param name="sessions">Sessions.</param>
    public void SetSessions(IEnumerable<SessionRecord> sessions) {
      records.Clear();

      records.AddRange(sessions);

      NotifyDataSetChanged();
    }
  }
}

