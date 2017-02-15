namespace ION.Droid.Widgets.RecyclerViews {

	using System;

	using Android.Content;
	using Android.Support.V7.Widget;
	using Android.Support.V13.View;
	using Android.OS;
	using Android.Util; 
	using Android.Views;
	using Android.Views.Animations;
	using Android.Widget;

	using L = Appion.Commons.Util.Log;

	public class SwipeMenuLayout : FrameLayout, GestureDetector.IOnGestureListener, Handler.ICallback {

		private const int MSG_CLOSE = 1;
		private const long PENDING_CLOSE_DELAY = 2500;

		public SwipeRecyclerView recyclerView { 
			get {
				return __recyclerView;
			}
			internal set {
				__recyclerView = value;
				Init();
			}
		} SwipeRecyclerView __recyclerView;
		public bool isOpen { get; private set; }

		public bool isSwipeEnabled { get; set; }

		public Scroller openScroller;
		public Scroller closeScroller;

		public View foreground { get; internal set; }
		public View background { get; internal set; }
		private ViewConfiguration viewConfig;
		private GestureDetector gestureDetector;
		private bool isFling;
		private float baseX;
		private float downX;
		private Handler handler;

		public SwipeMenuLayout(Context context) : this(context, null, 0) {
		}

		public SwipeMenuLayout(Context context, IAttributeSet atts) : this(context, null, 0) {
		}

		public SwipeMenuLayout(Context context, IAttributeSet attrs, int style) : base(context, attrs, style) {
		}

		/// <summary>
		/// Initializes the recycler view.
		/// </summary>
		public void Init() {
			handler = new Handler(this);
			Clickable = true;

			foreground = FindViewById(Resource.Id.swipe_recycler_view_foreground);
			if (foreground == null) {
				throw new Exception("Attempted to create a " + typeof(SwipeMenuLayout).Name + " without a foreground view");
			}

			background = FindViewById(Resource.Id.swipe_recycler_view_background);
			if (background == null) {
				throw new Exception("Attempted to create a " + typeof(SwipeMenuLayout).Name + " without a background view");
			}

			viewConfig = ViewConfiguration.Get(Context);

			isOpen = false;

			gestureDetector = new GestureDetector(Context, this);
			openScroller = new Scroller(Context, recyclerView.openInterpolator);
			closeScroller = new Scroller(Context, recyclerView.closeInterpolator);
		}

		public override void ComputeScroll() {
			if (isOpen) {
				if (openScroller.ComputeScrollOffset()) {
					Swipe(openScroller.CurrX * (int)recyclerView.swipeDirection);
					PostInvalidate();
				}
			} else {
				if (closeScroller.ComputeScrollOffset()) {
					Swipe((int)((baseX - closeScroller.CurrX) * (int)recyclerView.swipeDirection));
					PostInvalidate();
				}
			}
		}

		protected override void OnLayout(bool changed, int left, int top, int right, int bottom) {
			var lp = foreground.LayoutParameters as LayoutParams;
			var lgap = PaddingLeft + lp.LeftMargin;
			var tgap = PaddingTop + lp.TopMargin;

			foreground.Layout(lgap, tgap, lgap + foreground.MeasuredWidthAndState, tgap + foreground.MeasuredHeightAndState);

			lp = background.LayoutParameters as LayoutParams;
			tgap = PaddingTop + lp.TopMargin;
			if (recyclerView.swipeDirection == SwipeRecyclerView.EDirection.Left) {
				background.Layout(MeasuredWidth, tgap, MeasuredWidth + background.MeasuredWidthAndState, tgap + background.MeasuredHeightAndState);
			} else {
				background.Layout(-background.MeasuredWidthAndState, tgap, 0, tgap + background.MeasuredHeightAndState);
			}
		}

		// Implemented from GestureDetector.IGestureListener
		public bool OnDown(MotionEvent e) {
			isFling = false;
			return true;
		}

		public bool OnFling(MotionEvent e1, MotionEvent e2, float vx, float vy) {
			if (vx > viewConfig.ScaledMinimumFlingVelocity || vy > viewConfig.ScaledMinimumFlingVelocity) {
				isFling = true;
			}
			return isFling;
		}

		// Implemented from GestureDetector.IGestureListener
		public void OnLongPress(MotionEvent e) {
		}

		// Implemented from GestureDetector.IGestureListener
		public bool OnScroll(MotionEvent e1, MotionEvent e2, float dx, float dy) {
			return false;
		}

		// Implemented from GestureDetector.IGestureListener
		public void OnShowPress(MotionEvent e) {
		}

		// Implemented from GestureDetector.IGestureListener
		public bool OnSingleTapUp(MotionEvent e) {
			return false;
		}

		public bool OnSwipe(MotionEvent e) {
			gestureDetector.OnTouchEvent(e);
			switch (e.Action) {
				case MotionEventActions.Down:
					downX = e.GetX();
					isFling = false;
				break; // MotionEventActions.Down

				case MotionEventActions.Move:
					int dis = (int)(downX - e.GetX());
					if (isOpen) {
						dis += background.Width * (int)recyclerView.swipeDirection;
					}
					Swipe(dis);
				break; // MotionEventActions.Move

				case MotionEventActions.Up:
					if ((isFling || Math.Abs(downX - e.GetX()) > (background.Width / 3)) && Math.Sign(downX - e.GetX()) == (int)recyclerView.swipeDirection) {
						Open();
					} else {
						Close();
						return false;
					}
				break; // MotionEventActions.Up
			}

			return true;
		}

		public void Open() {
			isOpen = true;
			if (recyclerView.swipeDirection == SwipeRecyclerView.EDirection.Left) {
				openScroller.StartScroll(-foreground.Left, 0, background.Width, 0, recyclerView.animationDuration);
			} else {
				openScroller.StartScroll(foreground.Left, 0, background.Width, 0, recyclerView.animationDuration);
			}
			PostInvalidate();
			handler.SendEmptyMessageDelayed(MSG_CLOSE, PENDING_CLOSE_DELAY);
		}

		public void Close() {
			isOpen = false;
			if (recyclerView.swipeDirection == SwipeRecyclerView.EDirection.Left) {
				baseX = -foreground.Left;
				closeScroller.StartScroll(0, 0, background.Width, 0, recyclerView.animationDuration);
			} else {
				baseX = foreground.Left;
				closeScroller.StartScroll(0, 0, background.Width, 0, recyclerView.animationDuration);
			}
			PostInvalidate();
			handler.RemoveMessages(MSG_CLOSE);
		}

		// Implemented from Handler.ICallback
		public bool HandleMessage(Message msg) {
			switch (msg.What) {
				case MSG_CLOSE:
					if (isOpen) {
						Close();
					}
				return true; // MSG_CLOSE
			}

			return false;
		}

		private void Swipe(int distance) {
			if (Math.Sign(distance) != (int)recyclerView.swipeDirection) {
				distance = 0;
			} else if (Math.Abs(distance) > background.Width) {
				distance = background.Width * (int)recyclerView.swipeDirection;
				isOpen = true;
			}

			var lp = foreground.LayoutParameters as FrameLayout.LayoutParams;
			int gap = PaddingLeft + lp.LeftMargin;
			foreground.Layout(gap - distance, foreground.Top, gap + foreground.MeasuredWidthAndState - distance, foreground.Bottom);

			if (recyclerView.swipeDirection == SwipeRecyclerView.EDirection.Left) {
				background.Layout(MeasuredWidth - distance, background.Top, MeasuredWidth + background.MeasuredWidthAndState - distance, background.Bottom);
			} else {
				background.Layout(-background.MeasuredWidthAndState - distance, background.Top, -distance, background.Bottom);
			}
		}
	}
}
