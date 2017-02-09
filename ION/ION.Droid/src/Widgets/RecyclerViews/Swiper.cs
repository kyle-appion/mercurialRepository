namespace ION.Droid.Widgets.RecyclerViews {

	using System;
	using System.Collections.Generic;

	using Android.Animation;
	using Android.Content;
	using Android.Graphics;
	using Android.OS;
	using Android.Support.V7.RecyclerView;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using Java.Lang;

	using Appion.Commons.Util;

	using ION.Droid.Animations;
	using ION.Droid.Views;

	/// <summary>
	/// Stolen from: 
	/// https://github.com/heruoxin/Clip-Stack/blob/master/app/src/main/java/com/catchingnow/tinyclipboardmanager/SwipeableRecyclerViewTouchListener.java
	/// </summary>
	public class Swiper : RecyclerView.SimpleOnItemTouchListener, Handler.ICallback {

		private const long ANIMATION_FAST = 300;
		private const long ANIMATION_WAIT = 2200;
		private const long PENDING_ACTION_DELAY = 2500;

		// Cached ViewConfiguration and system-wide constant values
		private int slop;
		private int minFlingVelocity;
		private int maxFlingVelocity;

		// Fixed properties
		private RecyclerView recyclerView;
		private ISwipeListener swipeListener;
		private int viewWidth = 1; // 1 and not 0 to prevent dividing by zero

		// Transient properties
		private Dictionary<int, PendingDismissData> pendingDismissAnimations = new Dictionary<int, PendingDismissData>();
		private int dismissAnimationRefCount = 0;
		private float downX;
		private float downY;
		private bool swiping;
		private int swipingSlop;
		private VelocityTracker velocityTracker;
		private int swipedDownPosition;
		private View swipedDownView;
		private bool paused;
		private float finalDelta;

		// Foreground view (to be swiped)
		// background view (to show)
		private View foregroundView;
		private View backgroundView;

		//view ID
		private int foregroundId;
		private int backgroundId;

		private Handler handler;

		/**
     * Constructs a new swipe touch listener for the given {@link android.support.v7.widget.RecyclerView}
     *
     * @param recyclerView The recycler view whose items should be dismissable by swiping.
     * @param listener     The listener for the swipe events.
     */
		public Swiper(RecyclerView recyclerView, int fgID, int BgID, ISwipeListener listener) {
			foregroundId = fgID;
			backgroundId = BgID;
			ViewConfiguration vc = ViewConfiguration.Get(recyclerView.Context);
			slop = vc.ScaledTouchSlop;
			minFlingVelocity = vc.ScaledMinimumFlingVelocity * 16;
			maxFlingVelocity = vc.ScaledMaximumFlingVelocity;
			this.recyclerView = recyclerView;
			swipeListener = listener;
			handler = new Handler(this);

			/**
         * This will ensure that this SwipeableRecyclerViewTouchListener is paused during list view scrolling.
         * If a scroll listener is already assigned, the caller should still pass scroll changes through
         * to this listener.
         */
			recyclerView.SetOnScrollListener(new ScrollListener(this));
		}

		public bool HandleMessage(Message msg) {
			if (pendingDismissAnimations.ContainsKey(msg.What)) {
				CloseView(pendingDismissAnimations[msg.What]);
			}
			return true;
		}

		/**
     * Enables or disables (pauses or resumes) watching for swipe-to-dismiss gestures.
     *
     * @param enabled Whether or not to watch for gestures.
     */
		public void SetEnabled(bool enabled) {
			paused = !enabled;
		}

		public override bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent motionEvent) {
			return HandleTouchEvent(motionEvent);
		}

		public override void OnTouchEvent(RecyclerView rv, MotionEvent motionEvent) {
			HandleTouchEvent(motionEvent);
		}

		private bool HandleTouchEvent(MotionEvent motionEvent) {
			switch (motionEvent.Action) {
				case MotionEventActions.Down: {
						if (paused) {
							Log.D(this, "Breaking because paused");
							break;
						}

						// Find the child view that was touched (perform a hit test)
						Rect rect = new Rect();
						int childCount = recyclerView.ChildCount;
						int[] listViewCoords = new int[2];
						recyclerView.GetLocationOnScreen(listViewCoords);
						int x = (int) motionEvent.RawX - listViewCoords[0];
						int y = (int) motionEvent.RawY - listViewCoords[1];
						View child;
						for (int i = 0; i < childCount; i++) {
							child = recyclerView.GetChildAt(i);
							child.GetHitRect(rect);
							if (rect.Contains(x, y)) {
								swipedDownView = child;
								break;
							}
						}

						if (swipedDownView != null) {
							swipedDownPosition = recyclerView.GetChildAdapterPosition(swipedDownView);

							if (!swipeListener.CanSwipe(swipedDownPosition)) {
								return false;
							}

							downX = motionEvent.RawX;
							downY = motionEvent.RawY;
							velocityTracker = VelocityTracker.Obtain();
							velocityTracker.AddMovement(motionEvent);
							foregroundView = swipedDownView.FindViewById(foregroundId);
							backgroundView = swipedDownView.FindViewById(backgroundId);
						}
						break;
					}

				case MotionEventActions.Cancel: {
						if (velocityTracker == null) {
							break;
						}

						if (swipedDownView != null && swiping) {
							// cancel
							foregroundView.Animate()
							              .TranslationX(0)
							              .SetDuration(ANIMATION_FAST)
							              .SetListener(null);
						}
						velocityTracker.Recycle();
						velocityTracker = null;
						downX = 0;
						downY = 0;
						swipedDownView = null;
						swipedDownPosition = ListView.InvalidPosition;
						swiping = false;
						backgroundView = null;
						break;
					}

				case MotionEventActions.Up: {
						if (velocityTracker == null) {
							break;
						}

						finalDelta = motionEvent.RawX - downX;
						velocityTracker.AddMovement(motionEvent);
						velocityTracker.ComputeCurrentVelocity(1000);
						float velocityX = velocityTracker.XVelocity;
						float absVelocityX = System.Math.Abs(velocityX);
						float absVelocityY = System.Math.Abs(velocityTracker.YVelocity);
						bool dismiss = false;
						bool dismissRight = false;
						if (System.Math.Abs(finalDelta) > viewWidth / 2 && swiping) {
							dismiss = true;
							dismissRight = finalDelta > 0;
						} else if (minFlingVelocity <= absVelocityX && absVelocityX <= maxFlingVelocity
						           && absVelocityY < absVelocityX && swiping) {
							// dismiss only if flinging in the same direction as dragging
							dismiss = (velocityX < 0) == (finalDelta < 0);
							dismissRight = velocityTracker.XVelocity > 0;
						}
						if (dismiss && swipedDownPosition != ListView.InvalidPosition && swipeListener.CanSwipe(swipedDownPosition)) {
							// dismiss
							View downView = swipedDownView; // mDownView gets null'd before animation ends
							int downPosition = swipedDownPosition;
							++dismissAnimationRefCount;
							var pdd = new PendingDismissData(downPosition, foregroundView, backgroundView, viewWidth);
							backgroundView.Animate()
							              .SetDuration(ANIMATION_FAST);
							foregroundView.Animate()
							              .TranslationX(/*dismissRight ? viewWidth :*/ -viewWidth)
							              .SetDuration(ANIMATION_FAST)
							              .SetListener(new AnimatorListenerActionAdapter() {
								onAnimationEnd = (obj) => {
									DoSwipe(pdd);
								},
							});
						} else {
							// cancel
							foregroundView.Animate()
							              .TranslationX(0)
							              .SetDuration(ANIMATION_FAST)
							              .SetListener(null);
						}
						velocityTracker.Recycle();
						velocityTracker = null;
						downX = 0;
						downY = 0;
						swipedDownView = null;
						swipedDownPosition = ListView.InvalidPosition;
						swiping = false;
						backgroundView = null;
						break;
					}

				case MotionEventActions.Move: {
						if (velocityTracker == null || paused || pendingDismissAnimations.ContainsKey(swipedDownPosition)) {
							break;
						}

						velocityTracker.AddMovement(motionEvent);
						float deltaX = motionEvent.RawX - downX;
						float deltaY = motionEvent.RawY - downY;

						if (deltaX > 0) {
							if (pendingDismissAnimations.ContainsKey(this.swipedDownPosition)) {
								CloseView(new PendingDismissData(swipedDownPosition, foregroundView, backgroundView, viewWidth));
							}
							break;
						}
						if (!swiping && System.Math.Abs(deltaX) > slop && System.Math.Abs(deltaY) < System.Math.Abs(deltaX) / 2) {
							swiping = true;
							swipingSlop = (deltaX > 0 ? slop : -slop);
						}

						if (swiping) {
							if (backgroundView == null) {
								backgroundView = swipedDownView.FindViewById(backgroundId);
								// This is what dictates how far we scroll
							}
							backgroundView.Visibility = ViewStates.Visible;
							viewWidth = backgroundView.Width;
							foregroundView.TranslationX = deltaX - swipingSlop;
							if (foregroundView.TranslationX < -backgroundView.Width) {
								foregroundView.TranslationX = -backgroundView.Width;
							}
							return true;
						}
						break;
					}
			}

			return false;
		}

		/// <summary>
		/// Called when the user performs a swipe action. This posts a "dismiss" action into a heap that is removed after a
		/// timeout, resulting in the swipe being undone, or when the user performs the action.
		/// </summary>
		/// <param name="swipePosition">Swipe position.</param>
		private void DoSwipe(PendingDismissData data) {
			if (pendingDismissAnimations.ContainsKey(data.position)) {
				// Prevent the same item from spamming close.
				Log.D(this, "Preventing the spamming of close at: " + data.position);
				return;
			}
			data.backgroundView.Visibility = ViewStates.Visible;
			pendingDismissAnimations.Add(data.position, data);
			Log.D(this, "Adding: " + data.position + " to close queue");
			handler.SendMessageDelayed(handler.ObtainMessage(data.position), PENDING_ACTION_DELAY); 
			/*
			handler.PostDelayed(() => {
				if (pendingDismissAnimations.ContainsKey(data.position)) {
					CloseView(data);
				} else {
					Log.D(this, "Failed to close item at position: " + data.position);
				}
			}, PENDING_ACTION_DELAY);
*/
		}

		/// <summary>
		/// Closes an expanded row view.
		/// </summary>
		private void CloseView(PendingDismissData data) {
			handler.RemoveMessages(data.position);
			pendingDismissAnimations.Remove(data.position);
			data.foregroundView.Animate()
			    .TranslationX(0)
			    .SetListener(new AnimatorListenerActionAdapter() {
				onAnimationEnd = (obj) => {
					data.backgroundView.Visibility = ViewStates.Invisible;
				},
			})
			    .Start();
			Log.D(this, "Removed: " + data.position + ". There are " + pendingDismissAnimations.Count + " items left to close");
		}

		/**
     * The callback interface used by {@link SwipeableRecyclerViewTouchListener} to inform its client
     * about a swipe of one or more list item positions.
     */
		public interface ISwipeListener {
			/**
         * Called to determine whether the given position can be swiped.
         */
			bool CanSwipe(int position);
		}

		class ScrollListener : RecyclerView.OnScrollListener {
			private Swiper swiper;

			public ScrollListener(Swiper swiper) {
				this.swiper = swiper;
			}

			public override void OnScrollStateChanged(RecyclerView recyclerView, int newState) {
				swiper.SetEnabled(newState != RecyclerView.ScrollStateDragging);
			}

			public override void OnScrolled(RecyclerView recyclerView, int dx, int dy) {
			}
		}

		class PendingDismissData : IComparable<PendingDismissData> {
			public int position;
			public View foregroundView;
			public View backgroundView;
			public int scrollWidth;

			public PendingDismissData(int position, View fore, View back, int width) {
				this.position = position;
				this.foregroundView = fore;
				this.backgroundView = back;
				this.scrollWidth = width;
			}

			public int CompareTo(PendingDismissData other) {
				// Sort by descending position
				return other.position - position;
			}
		}
	}
}
