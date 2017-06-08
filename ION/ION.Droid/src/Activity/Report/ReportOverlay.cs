namespace ION.Droid.Activity.Report {

  using System;

  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Views;

	public class ReportOverlay : Drawable {
    /// <summary>
    /// The event that is used to notify listeners that the overlay threw a drag event.
    /// </summary>
    public event Action OnOverlayDragEvent;
		/// <summary>
		/// Gets the opacity.
		/// </summary>
		/// <value>The opacity.</value>
		public override int Opacity {
			get {
        return (int)Format.Translucent;
			}
		}

    /// <summary>
    /// The current scroll percent for the left handle.
    /// </summary>
    /// <value>The percent.</value>
    public float leftPercent { get { return leftX / (float)parent.MeasuredWidth; } }
		/// <summary>
		/// The current scroll percent for the right handle.
		/// </summary>
		/// <value>The percent.</value>
		public float rightPercent { get { return ((float)parent.MeasuredWidth - rightX) / (float)parent.MeasuredWidth; } }
    /// <summary>
    /// The paint that the drawable will be painted with.
    /// </summary>
    /// <value>The paint.</value>
    public Paint paint { get; set; }

    /// <summary>
    /// The view that we are overlaying this drawable onto.
    /// </summary>
    private View overlayView;
    /// <summary>
    /// The parent view that the self view is contained in.
    /// </summary>
    private View parent;
    /// <summary>
    /// The left handle.
    /// </summary>
    /// <value>The self.</value>
    public View left;
    /// <summary>
    /// The right handle.
    /// </summary>
    private View right;
		/// <summary>
		/// The current relative x coordinate (within the handle view's parent) that the left handle is.
		/// </summary>
		/// <value>The x.</value>
    private int leftX { get { return (int)(left.GetX()); } }
		/// <summary>
		/// That last touch x for the left handle view.
		/// </summary>
		/// <value>The last x.</value>
    private float lastLeftX { get; set; }
		/// <summary>
		/// The current relative x coordinate (within the handle view's parent) that the right handle is.
		/// </summary>
		/// <value>The x.</value>
    private int rightX { get { return (int)(right.GetX() + right.MeasuredWidth); } }
		/// <summary>
		/// That last touch x for the right handle view.
		/// </summary>
		/// <value>The last x.</value>
    private float lastRightX { get; set; }
    /// <summary>
    /// The bounds of the drawable.
    /// </summary>
    private Rect bounds;
    /// <summary>
    /// Whether or not we have performed an initial initialization for the views.
    /// </summary>
    private bool initialized;

    public ReportOverlay(View overlayView, View parent, View left, View right, Color color) {
      this.overlayView = overlayView;
      this.parent = parent;
      this.left = left;
      this.right = right;

			left.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Down:
            lastLeftX = e.Event.GetX();
						break;

					case MotionEventActions.Move:
            var xcoord = left.GetX() + (e.Event.GetX() - lastLeftX);
						// Clamp the x coordinate to within the bounds of its parent.
						xcoord = Math.Max(xcoord, 0);
						xcoord = Math.Min(xcoord, right.GetX() - left.MeasuredWidth);
						left.SetX(xcoord);
						left.Invalidate();
						overlayView.Invalidate();
            NotifyDrag();
						break;
				}
			};

			right.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Down:
						lastRightX = e.Event.GetX();
						break;

					case MotionEventActions.Move:
            var xcoord = right.GetX() + (e.Event.GetX() - lastRightX);
						// Clamp the x coordinate to within the bounds of its parent.
				    xcoord = Math.Max(xcoord, left.GetX() + right.MeasuredWidth);
						xcoord = Math.Min(xcoord, parent.MeasuredWidth - right.MeasuredWidth);
						right.SetX(xcoord);
						right.Invalidate();
						overlayView.Invalidate();
            NotifyDrag();
						break;
				}
			};

      paint = new Paint();
      paint.Color = color;
      paint.SetStyle(Paint.Style.Fill);
      bounds = new Rect();
    }

    /// <Docs>The canvas to draw into</Docs>
    /// <remarks>Draw in its bounds (set via setBounds) respecting optional effects such
    ///  as alpha (set via setAlpha) and color filter (set via setColorFilter).</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Draw the specified canvas.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    public override void Draw(Canvas canvas) {
			var width = parent.MeasuredWidth;
      var height = parent.MeasuredHeight;

      if (!initialized && height > 0) {
				left.SetY(parent.MeasuredHeight - left.MeasuredHeight);
				left.SetX(0);

				right.SetY(parent.MeasuredHeight - right.MeasuredHeight);
        right.SetX(parent.MeasuredWidth - right.MeasuredWidth);

				initialized = true;
      }

			bounds.Top = 0;
			bounds.Bottom = canvas.Height;

			// Draw left overlay
			bounds.Left = 0;
			bounds.Right = leftX;
      canvas.DrawRect(bounds, paint);

      // Draw right overlay
      bounds.Left = rightX;
			bounds.Right = width;
      canvas.DrawRect(bounds, paint);
    }

    /// <Docs>To be added.</Docs>
    /// <remarks>Specify an alpha value for the drawable. 0 means fully transparent, and
    ///  255 means fully opaque.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Sets the alpha.
    /// </summary>
    /// <param name="alpha">Alpha.</param>
    public override void SetAlpha(int alpha) {
      paint.Alpha = alpha;
    }

    /// <summary>
    /// Sets the color filter.
    /// </summary>
    /// <param name="colorFilter">Color filter.</param>
    public override void SetColorFilter(ColorFilter colorFilter) {
      paint.SetColorFilter(colorFilter);
    }

    private void NotifyDrag() {
      if (OnOverlayDragEvent != null) {
        OnOverlayDragEvent();
      }
    }

    /// <summary>
    /// Calculates the number of indices that cannot be selected based on the sizes of the given report overlays.
    /// </summary>
    /// <returns>The not mans indeces.</returns>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    public int CalculateNoMansIndices(DateIndexLookup dil) {
      return (int)((left.MeasuredWidth + right.MeasuredWidth) / (float)parent.MeasuredWidth) * dil.dateSpan;
    }

    public void UpdateHandlesTo(DateIndexLookup dil, DateTime leftDate, DateTime rightDate) {
      var w = (float)parent.MeasuredWidth;
      var mdw = left.MeasuredWidth;

      var leftIndex = dil.IndexOfDate(leftDate);
			var rightIndex = dil.IndexOfDate(rightDate);

      var span = (float)dil.dateSpan - 1;

      var lpx = (leftIndex / span) * w;
      var rpx = (rightIndex / span) * w;

      if (rpx - lpx < mdw) {
        rpx = lpx + mdw;
      }

			lpx = Math.Max(0, lpx);
      rpx = Math.Min(w - right.MeasuredWidth, rpx);

			left.SetX(lpx);
			right.SetX(rpx);

			left.Invalidate();
      right.Invalidate();
      InvalidateSelf();
    }
  }
}