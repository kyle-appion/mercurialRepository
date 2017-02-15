namespace ION.Droid.Widgets.RecyclerViews {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content.Res;
	using Android.Graphics;
	using Android.Support.V7.Widget;
	using Android.Support.V7.Widget.Helper;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Droid.Animations;

	/// <summary>
	/// A touch helper that is used to drag items around in a recycler view using a drag handle.
	/// </summary>
	public class SimpleDragSwipeHandler : ItemTouchHelper.Callback {

		// Overridden from ItemTouchHelper.Callback
		public override bool IsLongPressDragEnabled { get { return false; } }
		// Overridden from ItemTouchHelper.Callback
		public override bool IsItemViewSwipeEnabled { get { return true; } }

		private Handler handler;
		private HashSet<int> pendingCloses = new HashSet<int>();
		private bool isSwiping;
		private bool isDragging;

		private RecyclerView recyclerView;
		private ICallback callback;

		public SimpleDragSwipeHandler(RecyclerView recyclerView, ICallback callback) {
			this.recyclerView = recyclerView;
			this.callback = callback;
		}

		// Overridden from ItemTouchHelper.Callback
		public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			var vh = viewHolder as SwipableViewHolder;

			int swipe = 0;

			if (callback.IsSwipable(viewHolder.AdapterPosition)) {
				swipe = ItemTouchHelper.Left;
			}

			return MakeMovementFlags(0, swipe);
		}

		// Overridden from ItemTouchHelper.Callback
		public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
			return false;
		}

		// Overridden from ItemTouchHelper.Callback
		public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
			Log.D(this, "OnSwiped(RecyclerView.ViewHolder: " + viewHolder + ", direction: " + direction + ")");
			callback.OnRecordSwiped(viewHolder.AdapterPosition);
		}

		// Overridden from ItemTouchHelper.Callback
		public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState) {
			base.OnSelectedChanged(viewHolder, actionState);
			Log.D(this, "OnSelectedChanged(RecyclerView.ViewHolder: " + viewHolder + ", actionState: " + actionState + ")");
		}

		// Overridden from ItemTouchHelper.Callback
		public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			base.ClearView(recyclerView, viewHolder);
			Log.D(this, "ClearView(RecyclerView: " + recyclerView + ", RecyclerView.ViewHolder: " + viewHolder + ")");
		}
		// Overridden from ItemTouchHelper.Callback
		public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive) {
			base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
		}


		public interface ICallback {
			bool IsSwipable(int position);
			void OnRecordSwiped(int position);
		}
	}
}
