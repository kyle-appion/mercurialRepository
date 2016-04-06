namespace ION.Droid.Widgets.RecyclerViews {

  using System;

  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;

  public class SwipeableRecyclerViewTouchHelper : ItemTouchHelper.SimpleCallback {
    public SwipeableRecyclerViewTouchHelper() {
    }

    /// <summary>
    /// Raises the move event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="target">Target.</param>
    public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
      return false;
    }

    /// <summary>
    /// Gets the swipe dirs.
    /// </summary>
    /// <returns>The swipe dirs.</returns>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    public override int GetSwipeDirs(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
      var adapter = recyclerView.GetAdapter() as SwipeableRecyclerViewTouchHelper;
      if (adapter != null && adapter.IsPendingRemoval(viewHolder.AdapterPosition)) {
        return 0;
      } else {
        return base.GetSwipeDirs(recyclerView, viewHolder);
      }
    }
  }
}

