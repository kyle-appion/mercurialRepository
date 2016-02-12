﻿namespace ION.Droid {

  using System;

  using Android.Content;
  using Android.Graphics;
  using Android.Text;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  /// <summary>
  /// This view is the primary display widget for an analyzer data structure.
  /// </summary>
  public class AnalyzerView : ViewGroup {

    /// <summary>
    /// The font that will be used for immdiate text views within the view group.
    /// </summary>
    private const string REGULAR_FONT = "fonts/Roboto_Condensed/RobotoCondensed-Regular.ttf";
    /// <summary>
    /// The font that will be used for immdiate text views within the view group.
    /// </summary>
    private const string BOLD_FONT = "fonts/Droid_Sans/DroidSans-Bold.ttf";

    /// <summary>
    /// The bounds of the system squiggle.
    /// </summary>
    private const float SB_HEIGHT = 0.5f;
    /// <summary>
    /// The inset for the screen edge for the system squiggle.
    /// </summary>
    private const float INSET = 0.075f;
    /// <summary>
    /// The width that the squiggle that will approach the center of the screen.
    /// </summary>
    private const float SQUIGGLE_WIDTH = 0.2f;
    /// <summary>
    /// The overall height of the squiggle section.
    /// </summary>
    private const float SQUIGGLE_HEIGHT = 0.3f;
    /// <summary>
    /// The stoke with of the system squiggle.
    /// </summary>
    private const float SQUIGGLE_STROKE = 0.03f;
    /// <summary>
    /// percentage of a sensor mount that will hang over the system squiggle bounds.
    /// </summary>
    private const float OVERHANG = 0.3f;
    /// <summary>
    /// The height of a sensor mount view.
    /// </summary>
    private const float SENSOR_MOUNT_HEIGHT = 1 / 3.0f;
    /// <summary>
    /// The size of the analyzer icons (ie. compressor, evaporator).
    /// </summary>
    private const float ICON_SIZE = 0.05f;

    /// <summary>
    /// The text height for the title text view in the sensor mount.
    /// </summary>
    private const float TEXT_TITLE = 0.19f;
    /// <summary>
    /// The text height for the measurement text view in the sensor mount.
    /// </summary>
    private const float TEXT_MEASUREMENT = 0.255f;
    /// <summary>
    /// The text height for the unit text view in the sensor mount.
    /// </summary>
    private const float TEXT_UNIT = 0.19f;

    /// <summary>
    /// The length of a swap animation.
    /// </summary>
    private const long ANIMATION_DURATION = 350;

    /// <summary>
    /// The rectangle that will maintain the bounds of the system.
    /// </summary>
    private RectF systemBounds = new RectF();
    /// <summary>
    /// The measured size of the icons within the analyzer view.
    /// </summary>
    private float iconSize;
    /// <summary>
    /// The measured padding between sensor mounts.
    /// </summary>
    private float padding;
    /// <summary>
    /// The measured width of a sensor mount.
    /// </summary>
    private float sensorMountWidth;
    /// <summary>
    /// The measured height of a sensor mount.
    /// </summary>
    private float sensorMountHeight;
    /// <summary>
    /// The width of the manifold viewer views.
    /// </summary>
    private float viewerWidth;
    /// <summary>
    /// The height of the manifold viewer views.
    /// </summary>
    private float viewerHeight;
    /// <summary>
    /// The path that is used to render the low side of the system.
    /// </summary>
    private Path lowSideSystemPath;
    /// <summary>
    /// The path that is used to render the high side of the system.
    /// </summary>
    private Path highSideSystemPath;
    /// <summary>
    /// The paint that is used to stylize the low side of the system.
    /// </summary>
    private Paint lowSidePaint;
    /// <summary>
    /// The paint that is used to stylize the high side of the system.
    /// </summary>
    private Paint highSidePaint;
    /// <summary>
    /// The bitmap cache that will store bitmaps for the analyzer view.
    /// </summary>
    private BitmapCache cache;

    /// <summary>
    /// The view that the user is actively dragging.
    /// </summary>
    private View draggedView;
    /// <summary>
    /// The view that the user dropped the dragged view into.
    /// </summary>
    private View droppedView;

    /// <summary>
    /// The view that represents the low side manifold.
    /// </summary>
    private View lowSideManifoldView;
    /// <summary>
    /// The view that represents the high side manifold.
    /// </summary>
    private View highSideManifoldView;
    /// <summary>
    /// The bitmap that will hold the scaled expansion valve icon.
    /// </summary>
    private Bitmap expansionIcon;
    /// <summary>
    /// The bitmap that will hold the scaled compressor icon.
    /// </summary>
    private Bitmap compressorIcon;

    /// <summary>
    /// The sensor mounts that are present in the analyzer view.
    /// </summary>
    private SensorMount[] sensorMounts;

    /// <summary>
    /// The analyzer that this view is drawing. Defaults to an empty analyzer.
    /// </summary>
    public Analyzer analyzer {
      get {
        return __analyzer;
      }
      set {
        if (__analyzer != null) {
          __analyzer.onAnalyzerEvent -= OnAnalyzerEvent;
        }

        __analyzer = value;

        if (__analyzer == null) {
          __analyzer = new Analyzer();
        }

        __analyzer.onAnalyzerEvent += OnAnalyzerEvent;

        if (sensorMounts != null) {
          for (int i = 0; i < sensorMounts.Length; i++) {
            sensorMounts[i].sensor = null;
          }
        }

        RemoveAllViews();

        sensorMounts = new SensorMount[analyzer.analyzerSize];

        for (int i = 0; i < sensorMounts.Length; i++) {
          sensorMounts[i] = new SensorMount(Context);
          sensorMounts[i].root.LayoutParameters = new LayoutParams(i);
          AddView(sensorMounts[i].root);
        }

        AddView(lowSideManifoldView, new LayoutParams(LayoutParams.LOW_SIDE_VIEWER));
        AddView(highSideManifoldView, new LayoutParams(LayoutParams.HIGH_SIDE_VIEWER));
      }
    } Analyzer __analyzer;
    /// <summary>
    /// The manifold template that will maintain the low side manifold view.
    /// </summary>
    private ManifoldViewTemplate lowSideManifoldTemplate;
    /// <summary>
    /// The manifold template that will maintain the high side manifold view.
    /// </summary>
    private ManifoldViewTemplate highSideManifoldTemplate;

    public AnalyzerView(Context context) : this(context, null, 0) {
    }

    public AnalyzerView(Context context, IAttributeSet attrs) : this(context, attrs, 0) {
    }

    public AnalyzerView(Context context, IAttributeSet attrs, int style) : base(context, attrs, 0) {
      cache = new BitmapCache(context.Resources);

      var li = LayoutInflater.From(context);
      lowSideManifoldView = li.Inflate(Resource.Layout.analyzer_manifold_viewer, this, false);
      highSideManifoldView = li.Inflate(Resource.Layout.analyzer_manifold_viewer, this, false);

      lowSideManifoldTemplate = new ManifoldViewTemplate(lowSideManifoldView, cache);
      highSideManifoldTemplate = new ManifoldViewTemplate(highSideManifoldView, cache);

      analyzer = new Analyzer();

      SetWillNotDraw(false);
      HapticFeedbackEnabled = true;
      SoundEffectsEnabled = true;
    }

    /// <summary>
    /// Raises the measure event.
    /// </summary>
    /// <param name="widthMeasureSpec">Width measure spec.</param>
    /// <param name="heightMeasureSpec">Height measure spec.</param>
    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
      int width = MeasureSpec.GetSize(widthMeasureSpec);
      int height = MeasureSpec.GetSize(heightMeasureSpec);

      SetMeasuredDimension(MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                           MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly));

      // Calculate our view and drawing measurements
      float sh = SB_HEIGHT * height; // The System trace height.
      float hinset = INSET * height; // The inset from the top and bottom of the system bounds.
      float winset = INSET * width; // The inset from the left and right of the system bounds.

      systemBounds.Set(winset, hinset, width - winset, sh - hinset); // Set the measurements of the system bounds.

      iconSize = ICON_SIZE * height; // The desired size (width and height) of the icon.
      padding = iconSize / 5; // the padding betwen views.

      float his = iconSize / 2; // Half the icon size.
      float hw = (systemBounds.Left + systemBounds.Right) / 2; // Half system width, and center of system bounds.
      int depth = analyzer.sensorsPerSide / 2; // The number of sensors that are to be place on either side of the icons.

      float usedSpace = his + ((depth + 1) * padding); // The amount of space that has been used for sensor mounting.
      sensorMountWidth = (hw - usedSpace) / depth; // The desired width of a sensor mount.
      sensorMountHeight = SENSOR_MOUNT_HEIGHT * systemBounds.Height(); // The desired height of a sensor mount.
      viewerWidth = (width / 2) - (padding * 1.5f); // Sets the desired width of the viewer.
      viewerHeight = (height / 2) - (padding * 2); // Sets the desired height of the viewer.

      for (int i = 0; i < analyzer.analyzerSize; i++) {
        MeasureChild(GetChildAt(i), widthMeasureSpec, heightMeasureSpec);
      }

      MeasureChild(lowSideManifoldView, widthMeasureSpec, heightMeasureSpec);
      MeasureChild(highSideManifoldView, widthMeasureSpec, heightMeasureSpec);
    }

    /// <Docs>The child to measure</Docs>
    /// <param name="parentHeightMeasureSpec">The height requirements for this view</param>
    /// <remarks>Ask one of the children of this view to measure itself, taking into
    ///  account both the MeasureSpec requirements for this view and its padding.
    ///  The heavy lifting is done in getChildMeasureSpec.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Measures the child.
    /// </summary>
    /// <param name="child">Child.</param>
    /// <param name="parentWidthMeasureSpec">Parent width measure spec.</param>
    protected override void MeasureChild(View child, int parentWidthMeasureSpec, int parentHeightMeasureSpec) {
      if (child == lowSideManifoldView || child == highSideManifoldView) {
        int viewerWidthSpec = MeasureSpec.MakeMeasureSpec((int)viewerWidth, MeasureSpecMode.Exactly);
        int viewerHeightSpec = MeasureSpec.MakeMeasureSpec((int)viewerHeight, MeasureSpecMode.Exactly);

        child.Measure(MeasureSpec.MakeMeasureSpec((int)viewerWidthSpec, MeasureSpecMode.Exactly),
          MeasureSpec.MakeMeasureSpec((int)viewerHeightSpec, MeasureSpecMode.Exactly));
      } else {
        child.Measure(MeasureSpec.MakeMeasureSpec((int)sensorMountWidth, MeasureSpecMode.Exactly),
          MeasureSpec.MakeMeasureSpec((int)sensorMountHeight, MeasureSpecMode.Exactly));
      }

      var lp = child.LayoutParameters as AnalyzerView.LayoutParams;

      lp.Width = child.MeasuredWidth;
      lp.Height = child.MeasuredHeight;

      child.LayoutParameters = lp;
    }

    /// <Docs>Current width of this view.</Docs>
    /// <param name="oldw">Old width of this view.</param>
    /// <summary>
    /// This is called during layout when the size of this view has changed.
    /// </summary>
    /// <para tool="javadoc-to-mdoc">This is called during layout when the size of this view has changed. If
    ///  you were just added to the view hierarchy, you're called with the old
    ///  values of 0.</para>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    /// <param name="oldh">Oldh.</param>
    protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
      base.OnSizeChanged(w, h, oldw, oldh);

      var s = (int)iconSize;

      Bitmap tmp = cache.GetBitmap(Resource.Drawable.ic_expansionchamber);
      expansionIcon = Bitmap.CreateScaledBitmap(tmp, s, s, false);

      tmp = cache.GetBitmap(Resource.Drawable.ic_compressor);
      compressorIcon = Bitmap.CreateScaledBitmap(cache.GetBitmap(Resource.Drawable.ic_compressor), s, s, false);

      InitTraces();
    }

    /// <Docs>This is called when the view is attached to a window.</Docs>
    /// <summary>
    /// Raises the attached to window event.
    /// </summary>
    protected override void OnAttachedToWindow() {
      base.OnAttachedToWindow();
    }

    /// <Docs>This is called when the view is detached from a window.</Docs>
    /// <para tool="javadoc-to-mdoc">This is called when the view is detached from a window. At this point it
    ///  no longer has a surface for drawing.</para>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the detached from window event.
    /// </summary>
    protected override void OnDetachedFromWindow() {
      base.OnDetachedFromWindow();

      if (expansionIcon != null) {
        expansionIcon.Recycle();
      }

      if (compressorIcon != null) {
        compressorIcon.Recycle();
      }
    }

    /// <Docs>the canvas on which the background will be drawn</Docs>
    /// <remarks>Implement this to do your drawing.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the draw event.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    protected override void OnDraw(Canvas canvas) {
      canvas.DrawPath(lowSideSystemPath, lowSidePaint);
      canvas.DrawPath(highSideSystemPath, highSidePaint);

      var x = (systemBounds.Right + systemBounds.Left) / 2;
      var his = iconSize / 2;

      canvas.DrawBitmap(expansionIcon, x - his, systemBounds.Top - his, null);
      canvas.DrawBitmap(compressorIcon, x - his, systemBounds.Bottom - his, null);
    }

    /// <Docs>This is a new size or position for this view</Docs>
    /// <param name="top">Top position, relative to parent</param>
    /// <param name="bottom">Bottom position, relative to parent</param>
    /// <remarks>Called from layout when this view should
    ///  assign a size and position to each of its children.
    /// 
    ///  Derived classes with children should override
    ///  this method and call layout on each of
    ///  their children.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <summary>
    /// Raises the layout event.
    /// </summary>
    /// <param name="changed">If set to <c>true</c> changed.</param>
    /// <param name="l">L.</param>
    /// <param name="t">T.</param>
    /// <param name="r">The red component.</param>
    /// <param name="b">The blue component.</param>
    protected override void OnLayout(bool changed, int l, int t, int r, int b) {
      for (int i = 0; i < sensorMounts.Length; i++) {
        var v = GetChildAt(i);
        int x, y;

        var lp = (LayoutParams)v.LayoutParameters;
        CalculateSensorMountXYCoordinates(i, out x, out y);

        v.Layout(x, y, x + lp.Width, y + lp.Height);
      }
    }

    /// <summary>
    /// Calculates the upper left coordinate of a sensor mount given a particular index.
    /// </summary>
    /// <param name="index">Index.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    private void CalculateSensorMountXYCoordinates(int index, out int x, out int y) {
      var hw = (systemBounds.Left + systemBounds.Right) / 2;
      var his = iconSize / 2;
      var depth = index % (analyzer.sensorsPerSide / 2);

      var offset = his + (padding * (depth + 1)) + (sensorMountWidth * depth);

      if (index >= analyzer.sensorsPerSide) {
        x = (int)(hw + offset);
      } else {
        x = (int)((hw - offset) - sensorMountWidth);
      }

      if (index % analyzer.sensorsPerSide >= analyzer.sensorsPerSide / 2) {
        y = (int)(systemBounds.Top - (sensorMountHeight * OVERHANG));
      } else {
        y = (int)(systemBounds.Bottom - (sensorMountHeight * (1 - OVERHANG)));
      }
    }

    /// <summary>
    /// Initializes the system bounds traces.
    /// </summary>
    private void InitTraces() {
      // A squiggle is the little "coil" representation on either side of the analyzer trace.
      var low = new Path();
      var high = new Path();
      // Pull out the system bounds for quick reference.
      var l = systemBounds.Left;
      var r = systemBounds.Right;
      var t = systemBounds.Top;
      var b = systemBounds.Bottom;
      var h = systemBounds.Height();
      var w = systemBounds.Width();
      // Calculate the measurements that are used in pathing the system trace.
      var hw = (l + r) / 2; // Half width of the system bounds, also the center x of the bounds.
      var sh = SQUIGGLE_HEIGHT * h; // The height of the squiggle.
      var vh = t + (h - sh) / 2; // The vertical distance that is drawn up and down to meet the squiggle in the middle.
      var sh_p = sh / 3; // Distance of a single one of the squiggle's three vertical movements.
      var sh_p1 = vh + sh_p; // The total distance from the top (or bottom) of one vertical movement and one squiggle movement.
      var sh_p2 = sh_p1 + sh_p; // The total distance from the top (or bottom) of on vertical movement and two squiggle movements.
      var sh_p3 = sh_p2 + sh_p; // The total distance from the top (or bottom) of on vertical movement and three squiggle movements.
      var sw = SQUIGGLE_WIDTH * w; // The width of the squiggle (how far into the bounds it will go).
      var sw_l = l + sw; // The distance from the left side of the view to the squiggles total inset.
      var sw_r = r - sw; // The distance from the right side of the view to the squiggle total inset.

      // NOTE: The arrows indicate which direction the path is moving. All motion is either horizontal or vertical.

      // Plot the left trace. This trace will essentially look like an 'E' with an extra squiggle bar.
      low.MoveTo(hw, t); // Move to and start the path in the upper middle of the system bounds.
      low.LineTo(l, t); // <
      low.LineTo(l, vh); // V
      low.LineTo(sw_l, vh); // >
      low.LineTo(sw_l, sh_p1); // V
      low.LineTo(l, sh_p1); // <
      low.LineTo(l, sh_p2); // V
      low.LineTo(sw_l, sh_p2); // >
      low.LineTo(sw_l, sh_p3); // V
      low.LineTo(l, sh_p3); // <
      low.LineTo(l, b); // V
      low.LineTo(hw, b); // >

      // Plot the right trace. This trace will essentially look like a '3' with an extra squiggle bar.
      high.MoveTo(hw, t); // Move to and start the path in the upper middle of the system bounds.
      high.LineTo(r, t); // >
      high.LineTo(r, vh); // V
      high.LineTo(sw_r, vh); // <
      high.LineTo(sw_r, sh_p1); // V
      high.LineTo(r, sh_p1); // >
      high.LineTo(r, sh_p2); // V
      high.LineTo(sw_r, sh_p2); // <
      high.LineTo(sw_r, sh_p3); // V
      high.LineTo(r, sh_p3); // >
      high.LineTo(r, b); // V
      high.LineTo(hw, b); // <

      var stroke = SQUIGGLE_STROKE * w;

      lowSideSystemPath = low;
      lowSidePaint = CreateSystemPaint(stroke, new Color(unchecked((int)0xff0000ff)), new Color(unchecked((int)0xff7777ff)));

      highSideSystemPath = high;
      highSidePaint = CreateSystemPaint(stroke, new Color(unchecked((int)0xffff0000)), new Color(unchecked((int)0xffff7777)));
    }

    /// <summary>
    /// Creates a new system paint.
    /// </summary>
    /// <param name="strokeWidth">The stroke width to assign to the paint.</param>
    /// <param name="fromColor">The start color of the paint.</param>
    /// <param name="toColor">The color that the paint will gradient to.</param>
    private Paint CreateSystemPaint(float strokeWidth, Color fromColor, Color toColor) {
      var hw = (systemBounds.Left + systemBounds.Right) / 2;

      var p = new Paint();
      p.AntiAlias = true;
      p.StrokeWidth = strokeWidth;
      p.SetStyle(Paint.Style.Stroke);
      p.StrokeJoin = Paint.Join.Round;
      p.StrokeCap = Paint.Cap.Round;
      p.SetPathEffect(new CornerPathEffect(10));
      p.SetShader(new LinearGradient(hw, 0, hw, systemBounds.Bottom, fromColor, toColor, Shader.TileMode.Mirror));

      return p;
    }

    /// <summary>
    /// Called when the analyzer throws a new event.
    /// </summary>
    /// <param name="analyzerEvent">Analyzer event.</param>
    private void OnAnalyzerEvent(AnalyzerEvent analyzerEvent) {
    }

    /// <summary>
    /// The analyzer's specific layout parameter class.
    /// </summary>
    public class LayoutParams : ViewGroup.LayoutParams {
      public const int NOT_SET = -1;
      public const int LOW_SIDE_VIEWER = -2;
      public const int HIGH_SIDE_VIEWER = -3;

      /// <summary>
      /// The index of the layout params.
      /// </summary>
      public int index;

/*
      public LayoutParams() : base(0, 0) {
        this.index = NOT_SET;
      }
*/

      public LayoutParams(int index) : base(0, 0){
        this.index = index;
      }
    }
  }
}
