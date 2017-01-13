namespace ION.Droid.Widgets.Analyzer {

	using System;

  using Android.Content;
  using Android.Graphics;
	using Android.OS;
  using Android.Util;
  using Android.Views;
  using Android.Views.Animations;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Dialog;
  using ION.Droid.Util;
  using ION.Droid.Views;

  /// <summary>
  /// This view is the primary display widget for an analyzer data structure.
  /// </summary>
  public class AnalyzerView : ViewGroup {
    /// <summary>
    /// The delegate that is called when a sensor mount within the analyzer view is clicked.
    /// </summary>
    public delegate void OnSensorMountClicked(AnalyzerView view, Analyzer analyzer, int index);
    /// <summary>
    /// The delegate that is called when a sensor mount within the analyzer view is long clicked.
    /// </summary>
    public delegate void OnSensorMountLongClicked(AnalyzerView view, Analyzer analyzer, int index);
    /// <summary>
    /// The delegate that is called when a manifold is clicked.
    /// </summary>
    public delegate void OnManifoldClicked(AnalyzerView view, Analyzer analyzer, Analyzer.ESide side);
    /// <summary>
    /// The delegate that is called when a sensor property is clicked in a manifold on the analyzer.
    /// </summary>
    public delegate void OnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty);

    /// <summary>
    /// The event that is called when a sensor mount is clicked.
    /// </summary>
    public event OnSensorMountClicked onSensorMountClicked;
    /// <summary>
    /// The event that is called when a sensor mount is long clicked.
    /// </summary>
    public event OnSensorMountLongClicked onSensorMountLongClicked;
    /// <summary>
    /// The event that is called when a manifold is clicked.
    /// </summary>
    public event OnManifoldClicked onManifoldClicked;
    /// <summary>
    /// Called when a sensor property is clicked.
    /// </summary>
    public event OnSensorPropertyClicked onSensorPropertyClicked;

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
    /// The rectangle that will maintain the bounds for the low side manifold.
    /// </summary>
    private RectF lowSideRect = new RectF();
    /// <summary>
    /// The rectangle that will maintian the bounds for the high side manifold.
    /// </summary>
    private RectF highSideRect = new RectF();
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
          __analyzer = new Analyzer(AppState.context);
        }

        __analyzer.onAnalyzerEvent += OnAnalyzerEvent;

        if (sensorMounts != null) {
          for (int i = 0; i < sensorMounts.Length; i++) {
            sensorMounts[i].sensor = null;
          }
        }

        RefreshContent();
      }
    } Analyzer __analyzer;
    /// <summary>
    /// The manifold template that will maintain the low side manifold view.
    /// </summary>
    private AnalyzerManifoldViewTemplate lowSideManifoldTemplate;
    /// <summary>
    /// The manifold template that will maintain the high side manifold view.
    /// </summary>
    private AnalyzerManifoldViewTemplate highSideManifoldTemplate;

    public AnalyzerView(Context context) : this(context, null, 0) {
    }

    public AnalyzerView(Context context, IAttributeSet attrs) : this(context, attrs, 0) {
    }

    public AnalyzerView(Context context, IAttributeSet attrs, int style) : base(context, attrs, 0) {
      cache = new BitmapCache(context.Resources);

      var li = LayoutInflater.From(context);
      lowSideManifoldView = li.Inflate(Resource.Layout.analyzer_manifold_viewer, this, false);
      highSideManifoldView = li.Inflate(Resource.Layout.analyzer_manifold_viewer, this, false);

      lowSideManifoldTemplate = new AnalyzerManifoldViewTemplate(lowSideManifoldView, cache,
			                                                           Resource.String.analyzer_drag_to_set_low,
			                                                           Resource.Drawable.xml_low_side_background, OnSensorPropertyClick);
      lowSideManifoldView.SetOnDragListener(new ManifoldDragListener(this, lowSideManifoldTemplate, Analyzer.ESide.Low));
      lowSideManifoldView.SetOnClickListener(new ViewClickAction((v) => {
        NotifyManifoldClicked(Analyzer.ESide.Low);
      }));

      highSideManifoldTemplate = new AnalyzerManifoldViewTemplate(highSideManifoldView, cache,
			                                                            Resource.String.analyzer_drag_to_set_high,
			                                                            Resource.Drawable.xml_high_side_background, OnSensorPropertyClick);
      highSideManifoldView.SetOnDragListener(new ManifoldDragListener(this, highSideManifoldTemplate, Analyzer.ESide.High));
      highSideManifoldView.SetOnClickListener(new ViewClickAction((v) => {
        NotifyManifoldClicked(Analyzer.ESide.High);
      }));

      analyzer = new Analyzer(AppState.context);

      SetWillNotDraw(false);
      Focusable = false;
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
      float hw = width / 2; // Half the measured width
      float hh = height / 2; // Half the measured height

      iconSize = ICON_SIZE * height; // The desired size (width and height) of the icon.
      padding = iconSize / 5; // the padding betwen views.

      systemBounds.Set(winset, hinset, width - winset, sh - hinset); // Set the measurements of the system bounds.
      lowSideRect.Set(padding, hh + padding, hw - padding, height - padding); // Set the bounding area for the low side manifold.
      highSideRect.Set(hw + padding, hh + padding, width - padding, height - padding); // Set the bounds area for the high side manifold.

      float his = iconSize / 2; // Half the icon size.
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

			InitTraces();
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

			if (expansionIcon != null) {
				expansionIcon.Recycle();
			}
      Bitmap tmp = cache.GetBitmap(Resource.Drawable.ic_expansionchamber);
			if (tmp.IsRecycled) {
				cache.Remove(Resource.Drawable.ic_expansionchamber);
				tmp = cache.GetBitmap(Resource.Drawable.ic_expansionchamber);
			}
      expansionIcon = Bitmap.CreateScaledBitmap(tmp, s, s, false);
			tmp.Recycle();

			if (compressorIcon != null) {
				compressorIcon.Recycle();
			}
      tmp = cache.GetBitmap(Resource.Drawable.ic_compressor);
			if (tmp.IsRecycled) {
				cache.Remove(Resource.Drawable.ic_compressor);
				tmp = cache.GetBitmap(Resource.Drawable.ic_compressor);
			}
      compressorIcon = Bitmap.CreateScaledBitmap(cache.GetBitmap(Resource.Drawable.ic_compressor), s, s, false);
			tmp.Recycle();
    }

    /// <Docs>This is called when the view is attached to a window.</Docs>
    /// <summary>
    /// Raises the attached to window event.
    /// </summary>
    protected override void OnAttachedToWindow() {
      base.OnAttachedToWindow();

      lowSideManifoldTemplate.Bind(analyzer.lowSideManifold);
      highSideManifoldTemplate.Bind(analyzer.highSideManifold);
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

      lowSideManifoldTemplate.Unbind();
      highSideManifoldTemplate.Unbind();
/*
      if (expansionIcon != null) {
        expansionIcon.Recycle();
      }

      if (compressorIcon != null) {
        compressorIcon.Recycle();
      }
*/
    }

		protected override void Dispose(bool disposing) {
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
			try {
				DoDraw(canvas);
			} catch (Exception e) {
				Appion.Commons.Util.Log.E(this, "Failed to draw canvas.", e);
			}
		}

		private void DoDraw(Canvas canvas) {	
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

      LayoutViewToRect(lowSideManifoldView, lowSideRect);
      LayoutViewToRect(highSideManifoldView, highSideRect);
    }

    /// <summary>
    /// Starts dragging the sensor mount at the given index. If the analyzer does not have a sensor at the given index,
    /// then the drag attempt will return false.
    /// </summary>
    /// <returns><c>true</c>, if dragging sensor mount was started, <c>false</c> otherwise.</returns>
    /// <param name="index">Index.</param>
    public bool StartDraggingSensorMount(int index) {
      if (!analyzer.HasSensorAt(index)) {
        return false;
      }

      var sensor = analyzer[index];

      var clipDataItem = new ClipData.Item("");
      var clipData = new ClipData("", new string[] { "" }, clipDataItem);
      var dragState = new DragState(index, sensor);

      draggedView = sensorMounts[index].root;
      //draggedView.SetOnDragListener(new SensorMountDragListener(this));
      draggedView.StartDrag(clipData, new DragShadowBuilder(draggedView), new DragState(index, sensor), 0);

      return true;
    }

    /// <summary>
    /// Animates (and sets) the swapping of two sensor mounts.
    /// </summary>
    /// <description>
    /// If a sensor mount is dragged to an empty sensor mount location on the same analyzer side or two sensor mounts
    /// are not attached to either analyzer viewer, then the swap will perform noramlly with not hiccup. However, if
    /// both or either are attached to a manifold, then we will have to ask the user for permission.
    /// </description>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public void SwapSensorMounts(int first, int second) {
			Appion.Commons.Util.Log.D(this, "SWAPPING SENSOR MOUNTS: {" + first + ", " + second + "}");
      if (CanSensorsSwapSafely(first, second)) {
        AnimateSensorMountSwap(first, second);
      } else {
				IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_replace_manifold_sensor, () => {
					AnimateSensorMountSwap(first, second);
				});
      }
    }

		/// <summary>
		/// This is a more loss version of the analyzer's method. This allows sensors to swap even if attached to a manifold
		/// so long as they both do not have secondary sensors.
		/// </summary>
		/// <returns><c>true</c>, if sensor swap safely was caned, <c>false</c> otherwise.</returns>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		private bool CanSensorsSwapSafely(int first, int second) {
			var fs = Analyzer.ESide.Low;
			var ss = Analyzer.ESide.Low;

			analyzer.GetSideOfIndex(first, out fs);
			analyzer.GetSideOfIndex(second, out ss);

			if (fs == ss) {
				// this is a completely safe swap
				return true;
			} else {
				// This swap needs a little more attention to be deemed safe.
				var fm = analyzer.GetManifoldFromSide(fs);
				var sm = analyzer.GetManifoldFromSide(ss);

				if (fm == null && sm == null) {
					// Both of the manifolds are null, this is a safe swap.
					return true;
				} else if (fm == null) {
					// The first manifold is null. This swap is only safe if the second's secondary sensor is null.
					return sm.secondarySensor == null;
				} else if (sm == null) {
					// The second manifold is null. This swap is only safe if the first's secondary sensor is null.
					return fm.secondarySensor == null;
				} else if (fm.secondarySensor == null && sm.secondarySensor == null) {
					// Both of the manifold's secondary sensors are null. This is a safe swap.
					return true;
				} else {
					// Either one of the manifolds has a secondary sensor. This will break the analyzer if the swap is allowed.
					return false;
				}
			}
		}

    /// <summary>
    /// Performs uncheck animation of two sensor mounts.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    private void AnimateSensorMountSwap(int first, int second) {
      var v1 = sensorMounts[first].root;
      var v2 = sensorMounts[second].root;

      int v1x, v1y;
      CalculateSensorMountXYCoordinates(first, out v1x, out v1y);

      int v2x, v2y;
      CalculateSensorMountXYCoordinates(second, out v2x, out v2y);

      v1.StartAnimation(NewTranslationAnimation(v1x, v1y, v2x, v2y));
      v2.StartAnimation(NewTranslationAnimation(v2x, v2y, v1x, v1y));

      PostDelayed(() => {
        analyzer.SwapSensors(first, second, true);
      }, 350);
    }

    /// <summary>
    /// Creates a new animation.
    /// </summary>
    /// <returns>The translation animation.</returns>
    /// <param name="sx">Sx.</param>
    /// <param name="sy">Sy.</param>
    /// <param name="dx">Dx.</param>
    private Animation NewTranslationAnimation(int sx, int sy, int dx, int dy) {
      var ret = new TranslateAnimation(0, dx - sx, 0, dy - sy);
      ret.Duration = 300;
      ret.FillAfter = true;
      return ret;
    }

    /// <summary>
    /// Sets the manifold sensor.
    /// </summary>
    /// <param name="side">Side of the analyzer to add the sensor.</param>
    /// <param name="sensor">The sensor to set the manifold's primary sensor to.</param>
		private void SetManifoldSensor(Analyzer.ESide destSide, Sensor sensor) {
			Analyzer.ESide startSide = Analyzer.ESide.Low;
			// Attempt to get the analyzer side that the sensor is on.
			if (analyzer.HasSensor(sensor) && !analyzer.GetSideOfSensor(sensor, out startSide)) {
				Appion.Commons.Util.Log.E(this, "Failed to get side of sensor in AnalyzerView#SetManifoldSensor(Analyzer.ESide, Sensor)");
				Toast.MakeText(Context, Resource.String.errror_unknown, ToastLength.Long).Show();
				// The sensor was awkwardly present in the analyzer, but we couldn't find it anywhere. Lets remove it just to
				// be safe.
				if (!analyzer.RemoveSensor(sensor)) {
					// TODO ahodder@appioninc.com: This should never happen. If it does, we need to discover why.
					Appion.Commons.Util.Log.E(this, "Failed to remove ghost sensor from analyzer.");
				}
			}

			if (startSide == destSide) {
				CheckSameSideSetManifold(destSide, sensor);
			} else {
				CheckOppositeSideSetManifold(destSide, sensor);
			}
		}

		private void CheckSameSideSetManifold(Analyzer.ESide destSide, Sensor sensor) {
			var manifold = analyzer.GetManifoldFromSide(destSide);

			if (manifold == null) {
				if (sensor.type == ESensorType.Temperature) {
					Toast.MakeText(Context, Resource.String.analyzer_require_pressure_primary, ToastLength.Long).Show();
				} else {
					analyzer.SetManifold(destSide, sensor);
				}
			} else {
				if (manifold.ContainsSensor(sensor)) {
					// The sensor was dragged into its own manifold.
					Toast.MakeText(Context, Resource.String.analyzer_sensor_already_in_viewer, ToastLength.Long).Show();
				} else if (manifold.secondarySensor == null) {
					if (manifold.WillAcceptSecondarySensor(sensor)) {
						manifold.SetSecondarySensor(sensor);
					} else {
						IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_replace_manifold_sensor, () => {
							analyzer.SetManifold(destSide, sensor);
						});
					}
				} else {
					if (manifold.WillAcceptSecondarySensor(sensor)) {
						IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_replace_manifold_sensor, () => {
							manifold.SetSecondarySensor(sensor);
						});
					} else {
						IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_replace_manifold_sensor, () => {
							analyzer.SetManifold(destSide, sensor);
						});
					}
				}
			}
		}

		private void CheckOppositeSideSetManifold(Analyzer.ESide destSide, Sensor sensor) {
			var destManifold = analyzer.GetManifoldFromSide(destSide);
			var curManifold = analyzer.GetManifoldFromSide(destSide.Opposite());

			// Not matter the outcome, a sensor is moving into the dest side. If the side can't support the new sensor, then
			// we have to error out.
			if (analyzer.IsSideFull(destSide)) {
				// TODO ahodder@appioninc.com: Localize
				var msg = string.Format(Context.GetString(Resource.String.analyzer_side_full_1sarg), destSide);
				Toast.MakeText(Context, msg, ToastLength.Long).Show();
				return;
			}

			if (destManifold == null) {
				if (sensor.type == ESensorType.Temperature) {
					Toast.MakeText(Context, Resource.String.analyzer_require_pressure_primary, ToastLength.Long).Show();
				} else if (analyzer.IsSensorAttachedToManifold(sensor)) {
					// This swap is done when a sensor mount is dragged into an opposing empty manifold.
					// All sensor properties should be kept.
					IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_swap_linked_manifolds, () => {
						var si = analyzer.IndexOfSensor(sensor);
						var di = analyzer.NextEmptySensorIndex(destSide);
						AnimateSensorMountSwap(si, di);
						curManifold.SetSecondarySensor(null);
						var sm = analyzer.GetManifoldFromSide(destSide.Opposite());
						analyzer.SetManifold(destSide, sm);
					});
				} else {
					// The sensor is free to move into the new side.
					var si = analyzer.IndexOfSensor(sensor);
					var di = analyzer.NextEmptySensorIndex(destSide);
					AnimateSensorMountSwap(si, di);
					analyzer.SetManifold(destSide, sensor);
				}
			} else {
        if (destManifold.ContainsSensor(sensor)) {
          Toast.MakeText(Context, Resource.String.analyzer_sensor_already_in_viewer, ToastLength.Long).Show();
        } else if (destManifold.WillAcceptSecondarySensor(sensor)) {
          IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_swap_linked_manifolds, () => {
            var si = analyzer.IndexOfSensor(sensor);
            var di = analyzer.NextEmptySensorIndex(destSide);
            AnimateSensorMountSwap(si, di);
            destManifold.SetSecondarySensor(sensor);
          });
        } else {
          if (sensor.type == ESensorType.Temperature) {
            Toast.MakeText(Context, Resource.String.analyzer_require_pressure_primary, ToastLength.Long).Show();
          } else {
						// This swap is done when a sensor mount is dragged into a opposing full manifold.
						// The swap will remove all subviews.
						IONAlertDialog.ShowDialog(Context, Resource.String.analyzer_complete_swap, Resource.String.analyzer_replace_manifold_sensor, () => {
              var si = analyzer.IndexOfSensor(sensor);
							var di = analyzer.NextEmptySensorIndex(destSide);
							AnimateSensorMountSwap(si, di);
							analyzer.SetManifold(destSide, sensor);
            });
          }
        }
			}
		}

    /// <summary>
    /// Refreshes the content views of the analyzer view.
    /// </summary>
    private void RefreshContent() {
      RemoveAllViews();

      sensorMounts = new SensorMount[analyzer.analyzerSize];

      for (int i = 0; i < sensorMounts.Length; i++) {
        var sm = new SensorMount(Context, analyzer);
        sm.sensor = analyzer[i];
        sensorMounts[i] = sm;
        var root = sm.root;

        AddView(root, new LayoutParams(i));
        root.SetOnDragListener(new SensorMountDragListener(this, sm));

        root.SetOnClickListener(new ViewClickAction((v) => {
          var lp = root.LayoutParameters as AnalyzerView.LayoutParams;
          NotifySensorMountClicked(lp.index);
        }));

        root.SetOnLongClickListener(new ViewLongClickAction((v) => {
          var lp = root.LayoutParameters as AnalyzerView.LayoutParams;
          NotifySensorMountLongClicked(lp.index);
          v.PlaySoundEffect(SoundEffects.Click);
        }));
      }

      AddView(lowSideManifoldView, new LayoutParams(LayoutParams.LOW_SIDE_VIEWER));
      AddView(highSideManifoldView, new LayoutParams(LayoutParams.HIGH_SIDE_VIEWER));

			lowSideManifoldTemplate.Bind(analyzer.lowSideManifold);
			highSideManifoldTemplate.Bind(analyzer.highSideManifold);
    }

    /// <summary>
    /// Lays out the view to the given rectangle dimensions.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="rect">Rect.</param>
    private void LayoutViewToRect(View view, RectF rect) {
      view.Layout((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
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
      switch (analyzerEvent.type) {
        case AnalyzerEvent.EType.Added:
          RefreshContent();
          break;
        case AnalyzerEvent.EType.Swapped:
          // TODO ahodder@appioninc.com: We can be smarter about this.
          RefreshContent();
          break;
        case AnalyzerEvent.EType.Removed:
          RefreshContent();
          break;
        case AnalyzerEvent.EType.ManifoldAdded:
          goto case AnalyzerEvent.EType.ManifoldRemoved;
        case AnalyzerEvent.EType.ManifoldRemoved:
          switch (analyzerEvent.side) {
            case Analyzer.ESide.Low:
              lowSideManifoldTemplate.Bind(analyzer.lowSideManifold);
              RefreshContent();
              break;
            case Analyzer.ESide.High:
              highSideManifoldTemplate.Bind(analyzer.highSideManifold);
              RefreshContent();
              break;
          }
          break;
      }
    }

    /// <summary>
    /// Called when a sensor property is clicked.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    /// <param name="sensorProperty">Sensor property.</param>
    private void OnSensorPropertyClick(Manifold manifold, ISensorProperty sensorProperty) {
      if (onSensorPropertyClicked != null) {
        onSensorPropertyClicked(manifold, sensorProperty);
      }
    }

    /// <summary>
    /// Notifies the on sensor mount clicked event handler that a sensor mount was clicked.
    /// </summary>
    /// <param name="index">Index.</param>
    private void NotifySensorMountClicked(int index) {
      if (onSensorMountClicked != null) {
        onSensorMountClicked(this, analyzer, index);
      }
    }

    /// <summary>
    /// Notifies the on sensor mount clicked event handler that a sensor mount was long clicked.
    /// </summary>
    /// <param name="index">Index.</param>
    private void NotifySensorMountLongClicked(int index) {
      if (onSensorMountLongClicked != null) {
        onSensorMountLongClicked(this, analyzer, index);
      }
    }

    /// <summary>
    /// Notifies the on manifold clicked event handler that a manifold as clicked.
    /// </summary>
    /// <param name="side">Side.</param>
    private void NotifyManifoldClicked(Analyzer.ESide side) {
      if (onManifoldClicked != null) {
        onManifoldClicked(this, analyzer, side);
      }
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

      public LayoutParams(int index) : base(0, 0){
        this.index = index;
      }
    }

    /// <summary>
    /// The listener that will respond to the sensor mount being dragged.
    /// </summary>
    private class SensorMountDragListener : Java.Lang.Object, IOnDragListener {
      private AnalyzerView analyzer;
      private SensorMount sensorMount;
      private bool dropped;

      public SensorMountDragListener(AnalyzerView view, SensorMount sensorMount) {
        this.analyzer = view;
        this.sensorMount = sensorMount;
        this.dropped = false;
      }
      /// <summary>
      /// Raises the drag event.
      /// </summary>
      /// <param name="v">The triggering view NOT the dragged view.</param>
      /// <param name="e">E.</param>
      public bool OnDrag(View v, DragEvent e) {
        var dragState = e.LocalState as DragState;

        switch (e.Action) {
          case DragAction.Started:
            if (sensorMount.root == v) {
              analyzer.draggedView.Visibility = ViewStates.Invisible;
              Appion.Commons.Util.Log.D(this, "Drag Started");
              dropped = false;
              return true;
            } else {
              Appion.Commons.Util.Log.D(this, "Drag NOT Started");
              return false;
            }

          case DragAction.Entered:
            Appion.Commons.Util.Log.D(this, "Drag Entered");
            return true;

          case DragAction.Exited:
            Appion.Commons.Util.Log.D(this, "Drag Exited");
            return true;

          case DragAction.Location:
            return true;

          case DragAction.Drop:
            Appion.Commons.Util.Log.D(this, "Drag Dropped");
            var lp = v.LayoutParameters as LayoutParams;

						if (lp != null && analyzer.draggedView != null) {
							Appion.Commons.Util.Log.D(this, "SensorMounts Swapping");
							analyzer.draggedView.Visibility = ViewStates.Visible;
							analyzer.SwapSensorMounts(dragState.index, lp.index);
              dropped = true;
							analyzer.draggedView = null;
              return true;
            } else {
              Appion.Commons.Util.Log.D(this, "SensorMounts NOT Swapping");
              return true;
            }

          case DragAction.Ended:
            Appion.Commons.Util.Log.D(this, "Drag Ended");
            if (!dropped) {
							

 //             analyzer.RefreshContent();
            }
            return true;

          default:
            Appion.Commons.Util.Log.D(this, "Drag Defaulted");
            return true;
        }
      }
    }

    /// <summary>
    /// The listener that will respond to the sensor mount being dragged.
    /// </summary>
		private class ManifoldDragListener : Java.Lang.Object, IOnDragListener {
      private AnalyzerView analyzer;
      private AnalyzerManifoldViewTemplate template;
      private Analyzer.ESide side;
			private Handler handler;

      public ManifoldDragListener(AnalyzerView analyzer, AnalyzerManifoldViewTemplate template, Analyzer.ESide side) {
        this.analyzer = analyzer;
        this.template = template;
        this.side = side;
				handler = new Handler();
      }

      /// <summary>
      /// Raises the drag event.
      /// </summary>
      /// <param name="v">V.</param>
      /// <param name="e">E.</param>
      public bool OnDrag(View v, DragEvent e) {
        var dragState = e.LocalState as DragState;

        switch (e.Action) {
          case DragAction.Started:
            Appion.Commons.Util.Log.D(this, "Drag Started");
            return true;

          case DragAction.Entered:
            Appion.Commons.Util.Log.D(this, "Drag Entered");
            return true;

          case DragAction.Exited:
            Appion.Commons.Util.Log.D(this, "Drag Exited");
            return true;

          case DragAction.Location:
            return true;

          case DragAction.Drop:
            Appion.Commons.Util.Log.D(this, "Drag Dropped");
            analyzer.SetManifoldSensor(side, dragState.sensor);
            return true;

          case DragAction.Ended:
            Appion.Commons.Util.Log.D(this, "Drag Ended");
						handler.PostDelayed(() => {
							analyzer.RefreshContent();
						}, ANIMATION_DURATION);
            return true;

          default:
            Appion.Commons.Util.Log.D(this, "Drag Defaulted");
            return true;
        }
      }
    }

    /// <summary>
    /// The state of the view at the start of a drag event.
    /// </summary>
    private class DragState : Java.Lang.Object {
      public int index;
      public Sensor sensor;

      public DragState(int index, Sensor sensor) {
        this.index = index;
        this.sensor = sensor;
      }
    }
  }
}
