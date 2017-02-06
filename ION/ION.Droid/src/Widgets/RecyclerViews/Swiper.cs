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
	public class Swiper : RecyclerView.SimpleOnItemTouchListener {

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
		private HashSet<int> activeAnimations = new HashSet<int>();
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

		private Handler handler = new Handler();

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

			/**
         * This will ensure that this SwipeableRecyclerViewTouchListener is paused during list view scrolling.
         * If a scroll listener is already assigned, the caller should still pass scroll changes through
         * to this listener.
         */
			recyclerView.SetOnScrollListener(new ScrollListener(this));
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
							downX = motionEvent.RawX;
							downY = motionEvent.RawY;
							swipedDownPosition = recyclerView.GetChildAdapterPosition(swipedDownView);
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
//				             .Alpha(1)
				             .SetDuration(ANIMATION_FAST);
							foregroundView.Animate()
				             .TranslationX(/*dismissRight ? viewWidth :*/ -viewWidth)
				             .SetDuration(ANIMATION_FAST)
				             .SetListener(new AnimatorListenerActionAdapter() {
												onAnimationEnd = (obj) => {
													DoSwipe(pdd);
//													PerformDismiss(downView, downPosition);
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
						if (velocityTracker == null || paused || activeAnimations.Contains(swipedDownPosition)) {
							break;
						}

						velocityTracker.AddMovement(motionEvent);
						float deltaX = motionEvent.RawX - downX;
						float deltaY = motionEvent.RawY - downY;

						if (deltaX > 0) {
							if (this.pendingDismissAnimations.ContainsKey(this.swipedDownPosition)) {
								pendingDismissAnimations.Remove(swipedDownPosition);
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
//							backgroundView.Alpha = 1 - System.Math.Max(0f, System.Math.Min(1f, 1f - System.Math.Abs(deltaX) / viewWidth));
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
				return;
			}
			pendingDismissAnimations.Add(data.position, data);
			handler.PostDelayed(() => {
				if (pendingDismissAnimations.ContainsValue(data)) {
					pendingDismissAnimations.Remove(data.position);
					activeAnimations.Add(data.position);
					CloseView(data);
				}
			}, PENDING_ACTION_DELAY);
		}

		/// <summary>
		/// Closes an expanded row view.
		/// </summary>
		private void CloseView(PendingDismissData data) {
			ValueAnimator animator = ValueAnimator.OfInt(-data.scrollWidth, 0);
			animator.SetDuration(ANIMATION_FAST);

			// Animate the view back to its initial position
			animator.AddUpdateListener(new AnimatorUpdateActionAdapter() {
				onAnimationUpdate = (obj) => {
					var w = ((Integer) obj.AnimatedValue).IntValue();
					data.foregroundView.TranslationX = w;
				},
			});

			animator.AddListener(new AnimatorListenerActionAdapter() {
				onAnimationEnd = (obj) => {
					data.backgroundView.Visibility = ViewStates.Gone;
					activeAnimations.Remove(data.position);
				},
			});

			animator.Start();
		}

/*
		private void PerformDismiss(View dismissView, int dismissPosition) {
			// Animate the dismissed list item to zero-height and fire the dismiss callback when
			// all dismissed list item animations have completed. This triggers layout on each animation
			// frame; in the future we may want to do something smarter and more performant.

			View backgroundView = dismissView.FindViewById(backgroundId);
			ViewGroup.LayoutParams lp = dismissView.LayoutParameters;
			int originalHeight = dismissView.Height;
			bool[] deleteAble = {true};

			ValueAnimator animator = ValueAnimator.OfInt(originalHeight, 1);
			animator.SetDuration(ANIMATION_FAST);

			animator.AddListener(new AnimatorListenerActionAdapter() {
				onAnimationEnd = (obj) => {
					--dismissAnimationRefCount;

					if (dismissAnimationRefCount > 0) return;

					dismissAnimationRefCount = 0;
					// No active animations, process all pending dismisses.
					// Sort by descending position
					pendingDismisses.Sort();

					int[] dismissPositions = new int[pendingDismisses.Count];
					for (int i = pendingDismisses.Count - 1; i >= 0; i--) {
						dismissPositions[i] = pendingDismisses[i].position;
					}
//					swipeListener.OnDismissedBySwipe(recyclerView, dismissPositions);

					// Reset mDownPosition to avoid MotionEventActions.UP trying to start a dismiss
					// animation with a stale position
					swipedDownPosition = ListView.InvalidPosition;

					ViewGroup.LayoutParams lp2;
					foreach (PendingDismissData pendingDismiss in pendingDismisses) {
						// Reset view presentation
						pendingDismiss.view.FindViewById(foregroundId).TranslationX = 0;
						lp2 = pendingDismiss.view.LayoutParameters;
						lp2.Height = originalHeight;
						pendingDismiss.view.LayoutParameters = lp;
					}

					// Send a cancel event
					long time = SystemClock.UptimeMillis();
					MotionEvent cancelEvent = MotionEvent.Obtain(time, time, MotionEventActions.Cancel, 0, 0, 0);
					recyclerView.DispatchTouchEvent(cancelEvent);

					pendingDismisses.Clear();
				},
			});

			// Animate the dismissed list item to zero-height
			animator.AddUpdateListener(new AnimatorUpdateActionAdapter() {
				onAnimationUpdate = (obj) => {
					lp.Height = ((Integer) obj.AnimatedValue).IntValue();
					dismissView.LayoutParameters = lp;
				},
			});

			PendingDismissData pendingDismissData = new PendingDismissData(dismissPosition, dismissView);
			pendingDismisses.Add(pendingDismissData);

			//fade out background view
			backgroundView.Animate()
			                .Alpha(0).SetDuration(ANIMATION_WAIT)
			                .SetListener(new AnimatorListenerActionAdapter() {
												onAnimationEnd = (obj) => {
													if (deleteAble[0]) animator.Start();
												},
											});

			//cancel animate when click(actually touch) background view.
			backgroundView.SetOnTouchListener(new ViewActionTouchListener() {
				onTouch = (v, e) => {
					switch (e.Action) {
						case MotionEventActions.Down:
							deleteAble[0] = false;
							--dismissAnimationRefCount;
							pendingDismisses.Remove(pendingDismissData);
							backgroundView.PlaySoundEffect(0);
							backgroundView.SetOnTouchListener(null);
							break;
					}
					return false;
				},
			});
		}
*/

		/**
     * The callback interface used by {@link SwipeableRecyclerViewTouchListener} to inform its client
     * about a swipe of one or more list item positions.
     */
		public interface ISwipeListener {
			/**
         * Called to determine whether the given position can be swiped.
         */
			bool CanSwipe(int position);

			/**
         * Called when the item has been dismissed by swiping to the left.
         *
         * @param recyclerView           The originating {@link android.support.v7.widget.RecyclerView}.
         * @param reverseSortedPositions An array of positions to dismiss, sorted in descending
         *                               order for convenience.
         */
//			void OnDismissedBySwipe(RecyclerView recyclerView, int[] reverseSortedPositions);
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
