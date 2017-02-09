namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;

	using Android.Graphics;
	using Android.Graphics.Drawables;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;

	using ION.Droid.Widgets.RecyclerViews;

  public class WorkbenchDragDecoration : ItemTouchHelper.SimpleCallback {

    /// <summary>
    /// The workbench adapter that we are decorating.
    /// </summary>
    private WorkbenchAdapter adapter;
    /// <summary>
    /// Whether or not the workbench has been expansion saved.
    /// </summary>
    private bool isSaved;

		private Drawable background;

		public WorkbenchDragDecoration(WorkbenchAdapter adapter) : base(ItemTouchHelper.Up | ItemTouchHelper.Down, ItemTouchHelper.Left) {
     	this.adapter = adapter;
			background = new ColorDrawable(Color.Transparent);
    }

		public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState) {
			base.OnSelectedChanged(viewHolder, actionState);
			if ((ItemTouchHelper.ActionStateDrag & actionState) == ItemTouchHelper.ActionStateDrag) {
				if (viewHolder.ItemViewType == (int)EViewType.Manifold) {
					adapter.SaveManifoldExpansionState();
					isSaved = true;
				}
			}
		}

		/// <summary>
		/// Gets the swipe dirs.
		/// </summary>
		/// <returns>The swipe dirs.</returns>
		/// <param name="recyclerView">Recycler view.</param>
		/// <param name="viewHolder">View holder.</param>
		public override int GetSwipeDirs(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			return 0;
/*
			var record = adapter[viewHolder.AdapterPosition];

			if (adapter.HasPendingAction(viewHolder.AdapterPosition)) {
				return ItemTouchHelper.Right;
			} else if (adapter.IsViewHolderSwipable(record, viewHolder as SwipableViewHolder, viewHolder.AdapterPosition)) {
				return base.GetSwipeDirs(recyclerView, viewHolder);
			} else {
				return 0;
			}
*/
		}

		/// <summary>
		/// Clears the view.
		/// </summary>
		/// <param name="recyclerView">Recycler view.</param>
		/// <param name="viewHolder">View holder.</param>
		public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			base.ClearView(recyclerView, viewHolder);

			if (isSaved) {
				adapter.RestoreManifoldExpansionState();
			}
			isSaved = false;
		}


    /// <summary>
    /// Raises the move event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="target">Target.</param>
    public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
      var sourceRecord = adapter.GetRecordAt(viewHolder.AdapterPosition);
      var targetRecord = adapter.GetRecordAt(target.AdapterPosition);

      if (sourceRecord is ManifoldRecord && targetRecord is ManifoldRecord) {
        return PerformManifoldMove(recyclerView, sourceRecord as ManifoldRecord, targetRecord as ManifoldRecord);
      } else if (sourceRecord is SensorPropertyRecord && targetRecord is SensorPropertyRecord) {
        return PerformSensorPropertyMove(recyclerView, sourceRecord as SensorPropertyRecord, targetRecord as SensorPropertyRecord);
      } else {
        return true;
      }
    }

    /// <summary>
    /// Raises the swiped event.
    /// </summary>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="direction">Direction.</param>
    public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
			/*
			var swipePosition = viewHolder.AdapterPosition;

			if (adapter.HasPendingAction(viewHolder.AdapterPosition)) {
				adapter.CancelSwipeAction(swipePosition);
			} else {
				adapter.PerformSwipeAction(swipePosition);
			}
			*/
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

    /// <summary>
    /// Performs a manifold record swap.
    /// </summary>
    /// <returns><c>true</c>, if manifold move was performed, <c>false</c> otherwise.</returns>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    private bool PerformManifoldMove(RecyclerView recyclerView, ManifoldRecord source, ManifoldRecord target) {
      adapter.CollapseManifold(source.item);
      adapter.CollapseManifold(target.item);
      var workbench = adapter.workbench;
      workbench.Swap(workbench.IndexOf(source.item), workbench.IndexOf(target.item));

      return true;
    }

    /// <summary>
    /// Performs a sensor property swap.
    /// </summary>
    /// <returns><c>true</c>, if manifold move was performed, <c>false</c> otherwise.</returns>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    private bool PerformSensorPropertyMove(RecyclerView recyclerView, SensorPropertyRecord source, SensorPropertyRecord target) {
      if (source.manifold.Equals(target.manifold)) {
        var m = source.manifold;
        var first = m.IndexOfSensorProperty(source.sensorProperty);
        var second = m.IndexOfSensorProperty(target.sensorProperty);
        m.SwapSensorProperties(first, second);
        return true;
      } else {
        return true;
      }
    }
  }

}

