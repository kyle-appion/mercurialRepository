namespace ION.IOS.ViewController.Analyzer {

	using System;
	using System.Collections.Generic;

	using CoreGraphics;
	using UIKit;

	using Appion.Commons.Util;

	using ION.IOS.Util;

  public class AnalyzerDisplayView : UIView {

    /// <summary>
    /// The last known size frame for the view. Invalidation will automatically
    /// happen if the current frame is not the same as the last known frame.
    /// </summary>
    private CGRect __lastFrame;
    /// <summary>
    /// The path that is used to draw the low side analyzer trace.
    /// </summary>
    private CGPath __lowPressureTrace;
    /// <summary>
    /// The path that is used to draw the high side analyzer trace.
    /// </summary>
    private CGPath __highPressureTrace;

    public AnalyzerDisplayView(CGRect frame) : base(frame) {
//      BackgroundColor = new UIColor(Colors.GOLD);
      Invalidate();
    }

    // Overridden from UIView
    public override void Draw(CGRect rect) {
      Log.D(this, rect.ToString());

      using (var context = UIGraphics.GetCurrentContext()) {
        DrawTrace(context);
      }
    }

    /// <summary>
    /// Invalidates the view. It will be redrawn.
    /// </summary>
    public void Invalidate() {
      Log.D(this, "Frame: " + Frame.ToString());
      Log.D(this, "Bounds: " + Bounds.ToString());
      var w = Frame.Width; // View width
      var h = Frame.Height; // View height
      var rect = new Rect(w * 0.1f, h * 0.1f, w * 0.9f, h * 0.9f);
      __lowPressureTrace = MakeTrace(rect, -1);
      __highPressureTrace = MakeTrace(rect, 1);
    }

    /// <summary>
    /// Draws the analyzer traces.
    /// </summary>
    private void DrawTrace(CGContext context/*, CGPath path, CGColor startColor, CGColor endColor*/) {
      context.SetStrokeColor(Colors.GOLD);
      context.SetLineWidth(5);
      context.AddPath(__lowPressureTrace);
      context.DrawPath(CGPathDrawingMode.Stroke);

//      context.AddPath(__highPressureTrace);
//      context.DrawPath(CGPathDrawingMode.Stroke);
    }

    /*
    private void InitTraces() {
      // We want at least a 10% pixel border around the lines to allow for
      // other widgets to be drawn

      
      var ah = h * 0.8f; // Analyzer trace height

      var hw = w / 2; // Half view width
      var hh = h / 2; // Half view height
      var si = hw / 4; // Squiggle inset
      var sh = ah / 4; // Squiggle height (the height of the squiggle relative to ah)
      var shd = ah / 3; // The height of the 180 degree squiggle turns
      var lh = (ah - sh) / 2; // Height of lines above and below the squiggle


      // Low side pressure trace
      // Start in the upper middle, work left, then down and finally right
      var lpLine = new CGPoint[] {
        new CGPoint(hw, at), // Upper Middle
        new CGPoint(al, at), // <
        new CGPoint(al, at + lh), // V
        new CGPoint(al + si, at + lh), // >
        new CGPoint(al + si, at + lh + shd), // V
        new CGPoint(al, at + lh + shd), // <
        new CGPoint(al, at + lh + shd * 2), // V
        new CGPoint(al + si, at + lh + shd * 2), // >
        new CGPoint(al + si, at + lh + shd * 3), // V 
        new CGPoint(al, at + lh + shd * 3), // <
        new CGPoint(al, ab), // V
        new CGPoint(hw, ab), // Lower Middle
      };

      // High size pressure trace
      // Start in the upper middle, work right, then down, and finally left
      var hpLine = new CGPoint[] {
        new CGPoint(hw, at), // Upper Middle
        new CGPoint(ar, at), // >
        new CGPoint(ar, at + lh), // V
        new CGPoint(ar - si, 0),
      };
    }
    */

    /// <summary>
    /// Creates the points needed for half of an analyzer trace.
    /// </summary>
    /// <param name="traceBounds">The Trace bounds. Note: this must be the bounds of the entire analyzer trace.</param>
    /// <param name="direction">The direction of the trace. 1 means the trace will be open on the right, -1 means the
    /// trace will be open on the left. Non |1| values will yield unexpected results.</param>
    /// <param name="squiggleCount">The number of completed 180 degree insets within the trace.</param>
    /// <param name="squiggleWidth">Squiggle width.</param>
    /// <param name="squiggleHeight">Squiggle height.</param>
    private CGPath MakeTrace(Rect traceBounds, int direction, int squiggleCount = 2, float squiggleWidth = 0.3f, float squiggleHeight = 0.4f) {
      var hw = (traceBounds.left + traceBounds.right) / 2;
      var home = hw + (hw * direction);
      var lh = (traceBounds.height * squiggleHeight) / 2;
      var si = (traceBounds.width * squiggleWidth) * (direction * -1);
      var sph = (traceBounds.height * squiggleHeight) / (squiggleCount + 1);
      var scl1 = squiggleCount - 1;

      var points = new List<CGPoint>();

      points.Add(new CGPoint(hw, traceBounds.top)); // Start in the upper middle of the bounds
      points.Add(new CGPoint(home, traceBounds.top)); // Move to the home edge
      points.Add(new CGPoint(home, traceBounds.top + lh)); // Move down to the squiggle section

      // Place the squiggles
      var y = traceBounds.top + lh;
      for (int i = 0; i < squiggleCount; i++) {
        // Places one complete squiggle
        points.Add(new CGPoint(si, y));
        // Move y down
        y += sph;
        points.Add(new CGPoint(si, y));
        points.Add(new CGPoint(home, y));

        if (i < scl1) {
          // Move y down a smidge  so that the squiggles don't over lap
          // This is only done if we still have more squiggle to place.
          y += sph;
          points.Add(new CGPoint(home, y));
        }
      }

      points.Add(new CGPoint(home, traceBounds.bottom)); // Move to the bottom of the bounds
      points.Add(new CGPoint(hw, traceBounds.bottom)); // Move bave to the center


      var ret = new CGPath();
      ret.AddLines(points.ToArray());

      return ret;
    }
  }

  internal struct Rect {
    public nfloat left, top, right, bottom;

    public nfloat width { 
      get { 
        return right - left;
      }
    }

    public nfloat height {
      get {
        return bottom - top;
      }
    }

    public Rect(nfloat left, nfloat top, nfloat right, nfloat bottom) : this() {
      this.left = left;
      this.top = top;
      this.right = right;
      this.bottom = bottom;
    }
  }
}

