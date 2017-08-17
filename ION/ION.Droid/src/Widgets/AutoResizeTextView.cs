namespace ION.Droid.Widgets {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Graphics;
	using Android.Runtime;
	using Android.Support.V4.Widget;
	using Android.Text;
	using Android.Util;
	using Android.Widget;

	/**
	 * http://stackoverflow.com/questions/16017165/auto-fit-textview-for-android/21851239
	 */
	public class AutoResizeTextView : TypefaceTextView {
		/**
		 *
		 * @param suggestedSize
		 *            Size of text to be tested
		 * @param availableSpace
		 *            available space in which text must fit
		 * @return an integer < 0 if after applying {@code suggestedSize} to
		 *         text, it takes less space than {@code availableSpace}, > 0
		 *         otherwise
		 */
		private delegate int SizeTester(int suggestedSize, RectF availableSpace);

		private const int NO_LINE_LIMIT = -1;
    
    private RectF mTextRect = new RectF();
    private RectF mAvailableSpaceRect;
    private SparseIntArray mTextCachedSizes;
    private TextPaint mPaint;
    private float mMaxTextSize;
    private float mSpacingMult = 1.0f;
    private float mSpacingAdd = 0.0f;
    private float mMinTextSize = 20;
    private int mWidthLimit;

    private bool mEnableSizeCache = true;
    private bool mInitializedDimens;
    
    /// <summary>
    /// The new width. Used in reducing the text view's ascent and descent.
    /// </summary>
    private int _newWidth;
    /// <summary>
    /// The new height. Used in reducing the text view's ascent and descent.
    /// </summary>
    private int _newHeight;
    

		public AutoResizeTextView(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) {
		}

		public AutoResizeTextView(Context context) : base(context) {
			Initialize();
		}

		public AutoResizeTextView(Context context, IAttributeSet attrs) : base(context, attrs) {
			Initialize();
		}

		public AutoResizeTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) {
			Initialize();
		}
/*
    // OVERRIDDEN TO REDUCE THE TEXT VIEW's ASCENT AND DECENT PADDING MAKING THE TEXT CLOSER TO THE BOUNDS OF THE VIEW
    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
      if (_newHeight == 0) {
        var bounds = new Rect();
        var paint = new Paint();
        paint.TextSize = TextSize;
        paint.GetTextBounds(Text, 0, Text.Length, bounds);
        _newWidth = bounds.Width();
        _newHeight = (int)(bounds.Height() - 2 * paint.Descent());
        ScrollY = (int)paint.Descent();
      }
      
      SetMeasuredDimension(_newWidth, _newHeight);
    }
*/

		private void Initialize() {
			mPaint = new TextPaint(Paint);
      ////////////
      // todo ahodder@appioninc.com: uncomment this to allow max text sizes
      ////////////
//			mMaxTextSize = TextSize;
      mMaxTextSize = 250;
			mAvailableSpaceRect = new RectF();
			mTextCachedSizes = new SparseIntArray();
			if (MaxLines == 0) {
				// no value was assigned during construction
				SetMaxLines(NO_LINE_LIMIT);
			}
		}

		public override float TextSize {
			get {
				return base.TextSize;
			}
			set {
				mMaxTextSize = value;
				mTextCachedSizes.Clear();
				AdjustTextSize();
			}
		}

		public override void SetMaxLines(int maxlines) {
			base.SetMaxLines(maxlines);
			AdjustTextSize();
		}

		public override void SetSingleLine() {
			base.SetSingleLine();
			SetMaxLines(1);
			AdjustTextSize();
		}

		public override void SetSingleLine(bool singleLine) {
			base.SetSingleLine(singleLine);

			if (singleLine) {
				SetMaxLines(1);
			} else {
				SetMaxLines(NO_LINE_LIMIT);
			}
			AdjustTextSize();
		}

		public override void SetLines(int lines) {
			base.SetLines(lines);
			SetMaxLines(lines);
			AdjustTextSize();
		}

		public override void SetTextSize(ComplexUnitType unit, float size) {
			var r = Context.Resources;

			mMaxTextSize = TypedValue.ApplyDimension(unit, size, r.DisplayMetrics);
			mTextCachedSizes.Clear();
			AdjustTextSize();
		}

		public override void SetLineSpacing(float add, float mult) {
			base.SetLineSpacing(add, mult);
			mSpacingMult = mult;
			mSpacingAdd = add;
		}

		/**
		   * Set the lower text size limit and invalidate the view
		   *
		   * @param minTextSize
		   */
		public void SetMinTextSize(float minTextSize) {
			mMinTextSize = minTextSize;
			AdjustTextSize();
		}

		private void AdjustTextSize() {
			if (!mInitializedDimens) {
				return;
			}
			int startSize = (int)mMinTextSize;
			int heightLimit = MeasuredHeight - this.CompoundPaddingBottom - CompoundPaddingTop;
			mWidthLimit = MeasuredWidth - CompoundPaddingLeft - CompoundPaddingRight;
			mAvailableSpaceRect.Right = mWidthLimit;
			mAvailableSpaceRect.Bottom = heightLimit;
			var pixels = EfficientTextSizeSearch(startSize, (int)mMaxTextSize, InternalSizeTester, mAvailableSpaceRect);
			base.SetTextSize(ComplexUnitType.Px, pixels);
		}

		private int InternalSizeTester(int suggestedSize, RectF availableSpace) {
			mPaint.TextSize = suggestedSize;
			var singleline = MaxLines == 1;
			if (singleline) {
				mTextRect.Bottom = mPaint.FontSpacing;
				mTextRect.Right = mPaint.MeasureText(Text);
			} else {
				StaticLayout layout = new StaticLayout(new Java.Lang.String(Text), mPaint, mWidthLimit, Layout.Alignment.AlignNormal, mSpacingMult, mSpacingAdd, true);
				// return early if we have more lines
				if (MaxLines != NO_LINE_LIMIT && layout.LineCount > MaxLines) {
					return 1;
				}
				mTextRect.Bottom = layout.Height;
				int maxWidth = -1;
				for (int i = 0; i < layout.LineCount; i++) {
					if (maxWidth < layout.GetLineWidth(i)) {
						maxWidth = (int)layout.GetLineWidth(i);
					}
				}
				mTextRect.Right = maxWidth;
			}

			mTextRect.OffsetTo(0, 0);
			if (availableSpace.Contains(mTextRect)) {
				// may be too small, don't worry we will find the best match
				return -1;
			} else {
				// too big
				return 1;
			}
		}

		/**
		 * Enables or disables size caching, enabling it will improve performance
		 * where you are animating a value inside TextView. This stores the font
		 * size against getText().length() Be careful though while enabling it as 0
		 * takes more space than 1 on some fonts and so on.
		 *
		 * @param enable
		 *            enable font size caching
		 */
		public void EnableSizeCache(bool enable) {
			mEnableSizeCache = enable;
			mTextCachedSizes.Clear();
			AdjustTextSize();
		}

		private int EfficientTextSizeSearch(int start, int end, SizeTester sizeTester, RectF availableSpace) {
			if (!mEnableSizeCache) {
				return binarySearch(start, end, sizeTester, availableSpace);
			}
			int key = Text.Length;
			int size = mTextCachedSizes.Get(key);
			if (size != 0) {
				return size;
			}
			size = binarySearch(start, end, sizeTester, availableSpace);
			mTextCachedSizes.Put(key, size);
			return size;
		}

		private static int binarySearch(int start, int end, SizeTester sizeTester, RectF availableSpace) {
			int lastBest = start;
			int lo = start;
			int hi = end - 1;
			int mid;
			while (lo <= hi) {
				mid = (int)((uint)(lo + hi)) >> 1;
				int midValCmp = sizeTester(mid, availableSpace);
				if (midValCmp < 0) {
					lastBest = lo;
					lo = mid + 1;
				} else if (midValCmp > 0) {
					hi = mid - 1;
					lastBest = hi;
				} else {
					return mid;
				}
			}
			// make sure to return last best
			// this is what should always be returned
			return lastBest;
		}

		protected override void OnTextChanged(Java.Lang.ICharSequence text, int start, int lengthBefore, int lengthAfter) {
			base.OnTextChanged(text, start, lengthBefore, lengthAfter);
			AdjustTextSize();
		}

		protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
			mInitializedDimens = true;
			mTextCachedSizes.Clear();
			base.OnSizeChanged(w, h, oldw, oldh);
			if (w != oldw || h != oldh) {
				AdjustTextSize();
			}
		}

		private string getTransformedText() {
			if (Text != null) {
				var transformationMethod = TransformationMethod;
				if (transformationMethod != null) {
					Text = transformationMethod.GetTransformationFormatted(new Java.Lang.String(Text), this).ToString();
				}
			}
			return Text;
		}
	}
}
