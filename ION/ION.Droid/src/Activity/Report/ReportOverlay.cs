namespace ION.Droid.Report {

  using System;

  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Views;
  
	public class ReportOverlay : Drawable {
    public float percent {
      get {
        if (plotWidth == 0) {
          return (align == EAlign.Left) ? 0 : 1;
        } else {
          return (align == EAlign.Left) ? (width / (float)plotWidth) : 1 - (width / (float)plotWidth);
        }
      }
    }
    /// <summary>
    /// That last touch x for the drawable.
    /// </summary>
    /// <value>The last x.</value>
    public float lastX { get; set; }
    /// <summary>
    /// The width of the defining plot.
    /// </summary>
    /// <value>The width of the plot.</value>
    public int plotWidth { get; set; }
    /// <summary>
    /// The width of the overlay.
    /// </summary>
    /// <value>The width.</value>
    public int width { get; set; }
    /// <summary>
    /// The paint that the drawable will be painted with.
    /// </summary>
    /// <value>The paint.</value>
    public Paint paint { get; set; }
    /// <summary>
    /// How to align the drawable.
    /// </summary>
    private EAlign align;
    /// <summary>
    /// The bounds of the drawable.
    /// </summary>
    private Rect bounds;

    public ReportOverlay(View parent, View self, EAlign align, Color color, int width) {
      this.align = align;
      this.width = width;

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
      switch (align) {
        case EAlign.Left:
          bounds.Left = 0;
          bounds.Right = bounds.Left + width;
          break;
        case EAlign.Right:
          bounds.Right = plotWidth;
          bounds.Left = plotWidth - width;
          break;
      }
      bounds.Top = 0;
      bounds.Bottom = canvas.Height;

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

    /// <summary>
    /// Gets the opacity.
    /// </summary>
    /// <value>The opacity.</value>
    public override int Opacity {
      get {
        return (int)Format.Opaque;
      }
    }

    public enum EAlign {
      Left,
      Right,
    }
  }
}