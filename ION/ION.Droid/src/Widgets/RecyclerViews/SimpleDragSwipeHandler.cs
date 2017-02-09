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
	public class SimpleDragSwipeHandler : ItemTouchHelper.Callback, Handler.ICallback {

		/// <summary>
		/// The time in milliseconds that a swiped view will close itself.
		/// </summary>
		private const int AUTO_CLOSE_TIME = 3000;

		// Overridden from ItemTouchHelper.Callback
		public override bool IsLongPressDragEnabled { get { return callback.allowDragging; } }
		// Overridden from ItemTouchHelper.Callback
		public override bool IsItemViewSwipeEnabled { get { return callback.allowSwiping; } }

		private Handler handler;
		private HashSet<int> pendingCloses = new HashSet<int>();
		private bool isDragging;

		private RecyclerView recyclerView;
		private ICallback callback;

		public SimpleDragSwipeHandler(RecyclerView recyclerView, ICallback callback) {
			this.recyclerView = recyclerView;
			this.callback = callback;

			handler = new Handler(this);
		}

		// Overridden from ItemTouchHelper.Callback
		public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			var vh = viewHolder as SwipableViewHolder;

			int drag = 0;
			int swipe = 0;

			Log.D(this, "AllowDragging: " + callback.allowDragging + ", draggable: " + callback.IsDraggable(viewHolder));
			if (callback.allowDragging && callback.IsDraggable(viewHolder)) {
				drag = ItemTouchHelper.Up | ItemTouchHelper.Down;
			}


/*
			if (allowDrag && vh.isDraggable) {
				drag = MakeFlag(ItemTouchHelper.ActionStateIdle, ItemTouchHelper.Up | ItemTouchHelper.Down) |
				MakeFlag(ItemTouchHelper.ActionStateDrag, ItemTouchHelper.Up | ItemTouchHelper.Down);
			}
*/

			Log.D(this, "AllowSwiping: " + callback.allowSwiping + ", swipable: " + callback.IsSwipable(viewHolder.AdapterPosition));
			if (callback.allowSwiping && callback.IsSwipable(viewHolder.AdapterPosition)) {
				swipe = ItemTouchHelper.Left;
			}

			return MakeMovementFlags(drag, swipe);
		}

		// Overridden from ItemTouchHelper.Callback
		public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
//			Log.D(this, "OnMove(RecyclerView: " + recyclerView + ", RecyclerView.ViewHolder: " + viewHolder + ", RecyclerView.ViewHolder: " + target + ")");
			if (callback.WillAcceptDrop(viewHolder, target)) {
//				Log.D(this, "Returning true");
				return true;
			} else {
//				Log.D(this, "Returning false");
				return false;
			}
		}

		// Overridden from ItemTouchHelper.Callback
		public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
			Log.D(this, "OnSwiped(RecyclerView.ViewHolder: " + viewHolder + ", direction: " + direction + ")");
			PostMessage(viewHolder.AdapterPosition);
		}

		// Overridden from ItemTouchHelper.Callback
		public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState) {
			Log.D(this, "OnSelectedChanged(RecyclerView.ViewHolder: " + viewHolder + ", actionState: " + actionState + ")");


			var vh = viewHolder as SwipableViewHolder;

			if (actionState == ItemTouchHelper.ActionStateSwipe) {
				vh.button.Visibility = ViewStates.Visible;
			} else if (actionState == ItemTouchHelper.ActionStateDrag) {
				callback.StartingDrag(viewHolder);
				isDragging = true;
			}
		}

		// Overridden from ItemTouchHelper.Callback
		public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
			base.ClearView(recyclerView, viewHolder);
			Log.D(this, "ClearView(RecyclerView: " + recyclerView + ", RecyclerView.ViewHolder: " + viewHolder + ")");

			if (isDragging) {
				callback.EndingDrag(viewHolder);
				isDragging = false;
			}

			if (pendingCloses.Contains(viewHolder.AdapterPosition)) {
				CloseView(viewHolder.AdapterPosition);
			}
		}

		// Overridden from ItemTouchHelper.Callback
		public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive) {
//			Log.D(this, "OnChildDraw(dx: " + dX + " dy: " + dY + " actionState: " + actionState + " isCurrentlyActive: " + isCurrentlyActive + ")");
			if (actionState == ItemTouchHelper.ActionStateSwipe) {
				var vh = viewHolder as SwipableViewHolder;

				if (!pendingCloses.Contains(viewHolder.AdapterPosition)) {
					var view = vh.content;

					if (dX < -vh.button.Width) {
						dX = -vh.button.Width;
					}

					view.TranslationX = dX;
				}
			} else {
				base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
			}
		}

		// Implemented from Handler.ICallback
		public bool HandleMessage(Message msg) {
			CloseView(msg.What);
			return true;
		}

		private void PostMessage(int position) {
			handler.SendMessageDelayed(handler.ObtainMessage(position), AUTO_CLOSE_TIME);
		}

		/// <summary>
		/// Animates the closing of a swiped view holder.
		/// </summary>
		/// <param name="position">Position.</param>
		private void CloseView(int position) {
			Log.D(this, "CloseView(position: " + position + ")");
			var vh = recyclerView.FindViewHolderForAdapterPosition(position) as SwipableViewHolder;

			handler.RemoveMessages(position);

			vh.content.Animate()
			  .TranslationX(0)
			  .SetDuration(300)
			  .SetListener(new AnimatorListenerActionAdapter() {
					onAnimationEnd = (obj) => {
						vh.button.Visibility = ViewStates.Gone;
						vh.content.TranslationX = 0;
						recyclerView.GetAdapter().NotifyItemChanged(position);
					},
				})
			  .Start();
		}

		public interface ICallback {
			/// <summary>
			/// Whether or not the callback allows dragging.
			/// </summary>
			/// <value><c>true</c> if allows dragging; otherwise, <c>false</c>.</value>
			bool allowDragging { get; }
			/// <summary>
			/// Whether or not the callback allows swiping.
			/// </summary>
			/// <value><c>true</c> if allow swiping; otherwise, <c>false</c>.</value>
			bool allowSwiping { get; }

			/// <summary>
			/// Queries whether or not the given position is swipable.
			/// </summary>
			/// <returns><c>true</c>, if swipable was ised, <c>false</c> otherwise.</returns>
			/// <param name="position">Position.</param>
			bool IsSwipable(int position);
			/// <summary>
			/// Queries whether or not the given view holder is draggable.
			/// </summary>
			/// <returns><c>true</c>, if draggable was ised, <c>false</c> otherwise.</returns>
			/// <param name="viewHolder">View holder.</param>
			/// <param name="dropTarget">Drop target.</param>
			bool IsDraggable(RecyclerView.ViewHolder viewHolder);
			/// <summary>
			/// Called when a drag is started.
			/// </summary>
			/// <param name="viewHolder">View holder.</param>
			void StartingDrag(RecyclerView.ViewHolder viewHolder);
			/// <summary>
			/// Called when a drag is ended.
			/// </summary>
			/// <param name="viewHolder">View holder.</param>
			void EndingDrag(RecyclerView.ViewHolder viewHolder);
			/// <summary>
			/// Queries whether or not the callback will accept the given drop.
			/// </summary>
			/// <returns><c>true</c>, if accept drop was willed, <c>false</c> otherwise.</returns>
			/// <param name="viewHolder">View holder.</param>
			/// <param name="dropTarget">Drop target.</param>
			bool WillAcceptDrop(RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder dropTarget);

			/// <summary>
			/// Notifies the callback that two records at the given positions have been swapped by a drag.
			/// </summary>
			/// <param name="i1">I1.</param>
			/// <param name="i2">I2.</param>
			void OnRecordsSwapped(int i1, int i2);
		}
	}
}
