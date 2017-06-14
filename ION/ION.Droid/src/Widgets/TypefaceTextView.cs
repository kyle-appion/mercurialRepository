namespace ION.Droid.Widgets {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Runtime;
  using Android.Text;
	using Android.Util;
  using Android.Widget;

	/// <summary>
	/// A text view that will allow for fonts to be set via xml. Also allows for automatic text resizing. The text resizing
	/// was stolen from: https://stackoverflow.com/a/5535672/480691
	/// </summary>
	[Register("ION.Droid.Widgets.TypeTextView")]
  public class TypefaceTextView : TextView {
    /// <summary>
    /// The cache of typefaces.
    /// </summary>
    private static Dictionary<string, Typeface> TYPEFACES = new Dictionary<string, Typeface>();
    /// <summary>
    /// The path to the default typeface for the text view.
    /// </summary>
    private const string DEFAULT_TYPEFACE = "fonts/DroidSans.ttf";

    /// <summary>
    /// The minimum text size that the view will display.
    /// </summary>
    private const float MIN_TEXT_SIZE = 8;

    /// <summary>
    /// Whether or not the text size should automatically adjust to maximize its size.
    /// </summary>
    public bool autoTextSize { get; set; }

    /// <summary>
    /// Whether or not the text view needs to resize the text.
    /// </summary>
    private bool needsResize;
    /// <summary>
    /// The temporary lower bounds on the starting text size.
    /// </summary>
    private float minTextSize {
      get {
        return __minTextSize;
      }
      set {
        __maxTextSize = value;
        RequestLayout();
        Invalidate();
      }
    } float __minTextSize = MIN_TEXT_SIZE;
    /// <summary>
    /// The temporary upper bounds on the starting text size.
    /// </summary>
    private float maxTextSize {
      get {
        return __maxTextSize;
      }
      set {
        __maxTextSize = value;
        RequestLayout();
        Invalidate();
      }
    } float __maxTextSize = 0;
    /// <summary>
    /// The text view line spacing multiplier.
    /// </summary>
    private float spacingMultiplier = 1.0f;
    /// <summary>
    /// The additional line spacing.
    /// </summary>
    private float spacingAdd = 0.0f;

    
    public TypefaceTextView(Context context) : this(context, null, 0) {
    }

    public TypefaceTextView(Context context, IAttributeSet attrs) : this(context, attrs, 0) {
    }

    public TypefaceTextView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) {
      // Prevent view preview from throwing a shit fit.
      if (IsInEditMode) {
        return;
      }

      var ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.TypefaceTextView);
      if (ta != null) {
        var typeface = (ETypeface)ta.GetInt(Resource.Styleable.TypefaceTextView_typeface, (int)ETypeface.Unknown);
        string path = null;
        switch (typeface) {
          case ETypeface.DroidSans: path = "fonts/DroidSans.ttf"; break;
					case ETypeface.DroidSansBold: path = "fonts/DroidSans_Bold.ttf"; break;
				}

        if (path == null) {
          path = DEFAULT_TYPEFACE;
        }

        SetFontFromAssets(path);

        autoTextSize = ta.GetBoolean(Resource.Styleable.TypefaceTextView_autoSizeText, false);

        ta.Recycle();
      }
    }

    protected override void OnTextChanged(Java.Lang.ICharSequence text, int start, int lengthBefore, int lengthAfter) {
//      base.OnTextChanged(text, start, lengthBefore, lengthAfter);
      needsResize = true;
      ResetTextSize();
    }

    protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
//      base.OnSizeChanged(w, h, oldw, oldh);
      if (w != oldw || h != oldh) {
        needsResize = true;
      }
    }

    public override void SetTextSize(ComplexUnitType unit, float size) {
      base.SetTextSize(unit, size);
    }

    public override void SetLineSpacing(float add, float mult) {
      base.SetLineSpacing(add, mult);
      spacingMultiplier = mult;
      spacingAdd = add;
    }

    protected override void OnLayout(bool changed, int left, int top, int right, int bottom) {
      if (changed || needsResize) {
        var widthLimit = (right - left) - CompoundPaddingLeft - CompoundPaddingRight;
        var heightLimit = (bottom - top) - CompoundPaddingBottom - CompoundPaddingTop;
        ResizeText(widthLimit, heightLimit);
      }
      base.OnLayout(changed, left, top, right, bottom);
    }

    public void ResetTextSize() {
      if (autoTextSize && TextSize > 0) {
        base.SetTextSize(ComplexUnitType.Px, TextSize);
        maxTextSize = TextSize;
      }
    }

    /// <summary>
    /// Sets the typeface for the view based on the given asset path.
    /// </summary>
    /// <param name="assetPath">Asset path.</param>
    private void SetFontFromAssets(string assetPath) {
      try {
        if (assetPath != null) {
          Typeface t = null;

          if (TYPEFACES.ContainsKey(assetPath)) {
            t = TYPEFACES[assetPath];
          } else {
            try {
              t = Typeface.CreateFromAsset(Application.Context.Assets, assetPath);
              TYPEFACES[assetPath] = t;
            } catch (Exception e) {
              Appion.Commons.Util.Log.E(this, "Failed to load typeface: " + assetPath, e);
              t = Typeface.Default;
            }
          }

          Typeface = t;
        }
      } catch (Exception e) {
        Appion.Commons.Util.Log.E(this, "Failed to set typeface", e);
      }
    }

    /// <summary>
    /// Resizes the text to fit comfortably within the full view.
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    private void ResizeText(int width, int height) {
      if (Text == null || Text.Length == 0 || width <= 0 ||height <= 0 || TextSize == 0) {
        return;
      }

      if (TransformationMethod != null) {
        Text = TransformationMethod.GetTransformationFormatted(new Java.Lang.String(Text), this).ToString();
      }

      var paint = Paint;
      var oldSize = paint.TextSize;
      // If there is a max text size set, use the lesser of that and the default text size
      var targetTextSize = maxTextSize > 0 ? Math.Min(TextSize, maxTextSize) : TextSize;

      // Get the required text height
      var textHeight = GetTextHeight(Text, paint, width, targetTextSize);

      // Until we either fit within out text view or we have reached our min text size, incrementally try smaller sizes
      while (textHeight > height && targetTextSize > minTextSize) {
        targetTextSize = Math.Max(targetTextSize - 1, minTextSize);
        textHeight = GetTextHeight(Text, paint, width, targetTextSize);
      }
/*
WE DONT USE ELLIPSES

    // If we had reached our minimum text size and still don't fit, append an ellipsis
    if (mAddEllipsis && targetTextSize == mMinTextSize && textHeight > height) {
      // Draw using a static layout
      // modified: use a copy of TextPaint for measuring
      TextPaint paint = new TextPaint(textPaint);
      // Draw using a static layout
      StaticLayout layout = new StaticLayout(text, paint, width, Alignment.ALIGN_NORMAL, mSpacingMult, mSpacingAdd, false);
      // Check that we have a least one line of rendered text
      if (layout.getLineCount() > 0) {
        // Since the line at the specific vertical position would be cut off,
        // we must trim up to the previous line
        int lastLine = layout.getLineForVertical(height) - 1;
        // If the text would not even fit on a single line, clear it
        if (lastLine < 0) {
          setText("");
        }
        // Otherwise, trim to the previous line and add an ellipsis
        else {
          int start = layout.getLineStart(lastLine);
          int end = layout.getLineEnd(lastLine);
          float lineWidth = layout.getLineWidth(lastLine);
          float ellipseWidth = textPaint.measureText(mEllipsis);

          // Trim characters off until we have enough room to draw the ellipsis
          while (width < lineWidth + ellipseWidth) {
            lineWidth = textPaint.measureText(text.subSequence(start, --end + 1).toString());
          }
          setText(text.subSequence(0, end) + mEllipsis);
        }
      }
*/

      // Some devices try to auto adjust line spacing, so force default line spacing and invalidate the layout as a side effect
      SetTextSize(ComplexUnitType.Px, targetTextSize);
      SetLineSpacing(spacingAdd, spacingMultiplier);

      needsResize = false;
    }

    /// <summary>
    /// Measure the height of the text off screen.
    /// </summary>
    /// <returns>The text height.</returns>
    /// <param name="text">Text.</param>
    /// <param name="paint">Paint.</param>
    /// <param name="width">Width.</param>
    /// <param name="textSize">Text size.</param>
    private int GetTextHeight(string text, TextPaint paint, int width, float textSize) {
      // Make a clone of the paint so we don't fuck up the current view
      var p = new TextPaint(paint);
      p.TextSize = textSize;
      var layout = new StaticLayout(new Java.Lang.String(text), p, width, Layout.Alignment.AlignNormal, spacingMultiplier, spacingAdd, true);
      return layout.Height;
    }

    [Flags]
    public enum ETypeface {
      Unknown = -1,
      DroidSans = 1,
      DroidSansBold = 2,
    }
  }
}

