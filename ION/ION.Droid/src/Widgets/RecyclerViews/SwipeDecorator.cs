namespace ION.Droid.Widgets.RecyclerViews {

  using System;

  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;

  public class SwipeDecorator : ItemTouchHelper.SimpleCallback {

    private SwipableRecyclerViewAdapter adapter;
    private Drawable background;

    public SwipeDecorator(SwipableRecyclerViewAdapter adapter) : this(adapter, Color.Red) {
    }

    public SwipeDecorator(SwipableRecyclerViewAdapter adapter, Color color) : base(0, ItemTouchHelper.Left) {
      this.adapter = adapter;
      background = new ColorDrawable(color);
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
      var record = adapter[viewHolder.AdapterPosition];

      if (adapter.HasPendingAction(viewHolder.AdapterPosition)) {
        return ItemTouchHelper.Right;
      } else if (adapter.IsViewHolderSwipable(record, viewHolder as SwipableViewHolder, viewHolder.AdapterPosition)) {
        return base.GetSwipeDirs(recyclerView, viewHolder);
      } else {
        return 0;
      }
/*
      if (adapter != null && adapter.HasPendingAction(viewHolder.AdapterPosition) ||
        !adapter.IsViewHolderSwipable(record, viewHolder as SwipableViewHolder, viewHolder.AdapterPosition)) {
        return 0;
      } else {
        return base.GetSwipeDirs(recyclerView, viewHolder);
      }
*/
    }

    /// <summary>
    /// Raises the swiped event.
    /// </summary>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="direction">Direction.</param>
    public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
      var swipePosition = viewHolder.AdapterPosition;

      if (adapter.HasPendingAction(viewHolder.AdapterPosition)) {
        adapter.CancelSwipeAction(swipePosition);
      } else {
        adapter.PerformSwipeAction(swipePosition);
      }
    }

    /// <summary>
    /// Raises the child draw event.
    /// </summary>
    /// <param name="cValue">C value.</param>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="dX">D x.</param>
    /// <param name="dY">D y.</param>
    /// <param name="actionState">Action state.</param>
    /// <param name="isCurrentlyActive">If set to <c>true</c> is currently active.</param>
    public override void OnChildDraw(Canvas canvas, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive) {
      var item = viewHolder.ItemView;

      if (viewHolder.AdapterPosition == -1) {
        return;
      }

      background.SetBounds(item.Right + (int)dX, item.Top, item.Right, item.Bottom);
      background.Draw(canvas);

      base.OnChildDraw(canvas, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
    }
  }
}

