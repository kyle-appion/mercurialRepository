namespace ION.Droid.Widgets.RecyclerViews {

	using System;

	using Android.Content;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Support.V13.View;
	using Android.Util;
	using Android.Views;
	using Android.Views.Animations;

	using L = Appion.Commons.Util.Log;


	public class SwipeRecyclerView : RecyclerView {

		private enum ETouchState {
			None,
			X,
			Y,
		}

		/// <summary>
		/// The enumeration of the directions that are allowed for swiping.
		/// </summary>
		public enum EDirection {
			Left = 1,
			Right = -1,
		}

		/// <summary>
		/// The event that is notified when a swipe action is started.
		/// </summary>
		public event Action<SwipeRecyclerView, int> onSwipeStart;
		/// <summary>
		/// The event that is notified when a swipe action is completed.
		/// </summary>
		public event Action<SwipeRecyclerView, int> onSwipeEnd;

		/// <summary>
		/// The interpolator that will animate the closing of a row.
		/// </summary>
		/// <value>The close interpolator.</value>
		public IInterpolator closeInterpolator { get; set; }
		/// <summary>
		/// The interpolator that will animate the opening of a row.
		/// </summary>
		/// <value>The open interpolator.</value>
		public IInterpolator openInterpolator { get; set; }
		/// <summary>
		/// The direction to swipe.
		/// </summary>
		/// <value>The swipe direction.</value>
		public EDirection swipeDirection { get; set; }
		/// <summary>
		/// The duration that a swipe animation will last.
		/// </summary>
		/// <value>The duration of the animation.</value>
		public int animationDuration { get; set; }

		/// <summary>
		/// The current touch state for the recycler view.
		/// </summary>
		private ETouchState touchState;
		private float downX, downY;
		private int touchPosition;

		private SwipeMenuLayout touchView;
		private ViewConfiguration viewConfig;
		private DateTime startClickTime;
		private float dx, dy;

		public SwipeRecyclerView(Context context) : this(context, null, 0) {
		}

		public SwipeRecyclerView(Context context, IAttributeSet atts) : this(context, null, 0) {
		}

		public SwipeRecyclerView(Context context, IAttributeSet attrs, int style) : base(context, attrs, style) {
			var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.SwipeRecyclerView, 0, style);
			animationDuration = a.GetInt(Resource.Styleable.SwipeRecyclerView_animationDuration, 300);
			a.Recycle();
			Init();
		}

		/// <summary>
		/// Initializes the recycler view.
		/// </summary>
		private void Init() {
			SetLayoutManager(new LinearLayoutManager(Context));
			touchState = ETouchState.None;
			viewConfig = ViewConfiguration.Get(Context);
			openInterpolator = new BounceInterpolator();
			closeInterpolator = new BounceInterpolator();
			swipeDirection = EDirection.Left;
		}

		public override bool OnInterceptTouchEvent(MotionEvent ev) {
			if (ev.Action != MotionEventActions.Down && touchView == null) {
				return base.OnInterceptTouchEvent(ev);
			}

			switch (ev.Action) {
				case MotionEventActions.Down:
					dx = dy = 0.0f;
					startClickTime = DateTime.Now;
					int oldPos = touchPosition;
					downX = ev.GetX();
					downY = ev.GetY();

					touchState = ETouchState.None;
					touchPosition = GetChildAdapterPosition(FindChildViewUnder(downX, downY));

					if (touchPosition == oldPos && touchView != null && touchView.isOpen) {
						touchState = ETouchState.X;
						touchView.OnSwipe(ev);
					}

					// Find the touched child view
					View view = null;
					var vh = FindViewHolderForAdapterPosition(touchPosition);
					if (vh != null) {
						view = vh.ItemView;
					}

					if (touchPosition != oldPos && touchView != null && touchView.isOpen) {
						touchView.Close();
						touchView = null;
						var cancelEvent = MotionEvent.Obtain(ev);
						cancelEvent.Action = MotionEventActions.Cancel;
						base.OnTouchEvent(cancelEvent);
						L.D(this, "Returning true");
						return true;
					}

					touchView = view as SwipeMenuLayout;
					if (touchView != null) {
						touchView.recyclerView = this;
						touchView.OnSwipe(ev);
					}
				break; // MotionEventActions.Down

				case MotionEventActions.Move:
					dx = Math.Abs(ev.GetX() - downX);
					dy = Math.Abs(ev.GetY() - downY);

					if (touchState == ETouchState.X && touchView.isSwipeEnabled) {
						touchView.OnSwipe(ev);
						ev.Action = MotionEventActions.Cancel;
						base.OnTouchEvent(ev);
					} else if (touchState == ETouchState.None && touchView.isSwipeEnabled) {
						if (Math.Abs(dy) > viewConfig.ScaledTouchSlop) {
							touchState = ETouchState.Y;
						} else if (dx > viewConfig.ScaledTouchSlop) {
							touchState = ETouchState.X;
							NotifySwipeStart(touchPosition);
						}
					}
				break; // MotionEventActions.Move

				case MotionEventActions.Up:
					bool isCloseOnUpEvent = false;

					if (touchState == ETouchState.X && touchView.isSwipeEnabled) {
						isCloseOnUpEvent = !touchView.OnSwipe(ev);
						NotifySwipeEnd(touchPosition);

						if (!touchView.isOpen) {
							touchPosition = -1;
							touchView = null;
						}

						ev.Action = MotionEventActions.Cancel;
						base.OnTouchEvent(ev);
					}

					var clickDuration = DateTime.Now - startClickTime;
					var isOutOfTime = clickDuration > TimeSpan.FromMilliseconds(ViewConfiguration.LongPressTimeout);
					var isOutX = dx > viewConfig.ScaledTouchSlop;
					var isOutY = dy > viewConfig.ScaledTouchSlop;
					if (isOutOfTime || isOutX || isOutY) {
						return true;
					} else {
						var evX = ev.GetX();
						var evY = ev.GetY();

						var upView = FindChildViewUnder(evX, evY) as SwipeMenuLayout;
						if (upView != null) {
							var x = evX - upView.Left;
							var y = evY - upView.Top;
							var mv = upView.background;
							var tx = mv.TranslationY;
							var ty = mv.TranslationX;
							if (! (x >= mv.Left + tx && x <= mv.Right + tx &&
							       y >= mv.Top + ty && y <= mv.Bottom + ty) &&
							    isCloseOnUpEvent) {
								return true;
							}
						}
					}

				break; // MotionEventActions.Up;

				case MotionEventActions.Cancel:
					if (touchView != null && touchView.isSwipeEnabled) {
						ev.Action = MotionEventActions.Up;
						touchView.OnSwipe(ev);
					}
				break; // MotionEventActions.Cancel
			}

			return base.OnInterceptTouchEvent(ev);
		}

		/// <summary>
		/// Animates the hidden menu for the given row, if it has one.
		/// </summary>
		/// <param name="position">Position.</param>
		public void SmoothOpenMenu(int position) {
			var lm = GetLayoutManager();
			var v = lm.FindViewByPosition(position) as SwipeMenuLayout; 
			if (v != null) {
				touchPosition = position;

				if (touchView != null && touchView.isOpen) {
					touchView.Close();
				}

				touchView = v;
				touchView.Open();
			}
		}

		public void SmoothCloseMenu() {
			if (touchView != null && touchView.isOpen) {
				touchView.Close();
			}
		}

		/// <summary>
		/// Notifies the onSwipeStart event.
		/// </summary>
		/// <param name="position">Position.</param>
		private void NotifySwipeStart(int position) {
			if (onSwipeStart != null) {
				onSwipeStart(this, position);
			}
		}

		/// <summary>
		/// Notifies the onSwipeEnd event.
		/// </summary>
		/// <param name="position">Position.</param>
		private void NotifySwipeEnd(int position) {
			if (onSwipeEnd != null) {
				onSwipeEnd(this, position);
			}
		}

		public new class ViewHolder : RecyclerView.ViewHolder {
			public SwipeRecyclerView recyclerView { get; private set; }
			public SwipeMenuLayout layout { get; private set; }
			public View foreground { get; private set; }
			public View background { get; private set; }

			public ViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout, int backgroundLayout, bool swipeEnabled=true) : base(Create(recyclerView, foregroundLayout, backgroundLayout)) {
				this.recyclerView = recyclerView;
				layout = ItemView as SwipeMenuLayout;
				layout.recyclerView = recyclerView;
				layout.Init();
				foreground = ItemView.FindViewById(Resource.Id.swipe_recycler_view_foreground);
				background = ItemView.FindViewById(Resource.Id.swipe_recycler_view_background);
				layout.isSwipeEnabled = swipeEnabled;
			}

			/// <summary>
			/// Called by an adapter to unbind the view holder from any events that it register to.
			/// </summary>
			public virtual void Unbind() {
			}

			private static View Create(ViewGroup parent, int foregroundLayout, int backgroundLayout) {
				var ret = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.swipe_menu_layout, parent, false) as SwipeMenuLayout;

				var foreground = LayoutInflater.From(parent.Context).Inflate(foregroundLayout, ret, false);
				foreground.Id = Resource.Id.swipe_recycler_view_foreground;

				var background = LayoutInflater.From(parent.Context).Inflate(backgroundLayout, ret, false);
				background.Id = Resource.Id.swipe_recycler_view_background;

				ret.AddView(foreground);
				ret.AddView(background);

				return ret;
			}
		}
	}
}
