namespace ION.Droid.Widgets.RecyclerViews {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  /// <summary>
  /// A LinearLayoutManager that will force items to be scolled neatly to the top when over shot.
  /// </summary>
  public class SmoothItemScrollLinearLayoutManager : LinearLayoutManager {

    /// <summary>
    /// The context for the layout manager.
    /// </summary>
    public Context context { get; private set; }
    /// <summary>
    /// Whether or not the layout manager will allow scrolling.
    /// </summary>
    /// <value><c>true</c> if allow scrolling; otherwise, <c>false</c>.</value>
    public bool allowScrolling { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ION.Droid.Widgets.RecyclerViews.SmoothItemScrollLinearLayoutManager"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    public SmoothItemScrollLinearLayoutManager(Context context) : base(context, LinearLayoutManager.Vertical, false) {
      this.context = context;
    }

    /// <summary>
    /// Determines whether this instance can scroll vertically.
    /// </summary>
    /// <returns><c>true</c> if this instance can scroll vertically; otherwise, <c>false</c>.</returns>
    public override bool CanScrollVertically() {
      return allowScrolling && base.CanScrollVertically();
    }

    /// <summary>
    /// Determines whether this instance can scroll horizontally.
    /// </summary>
    /// <returns><c>true</c> if this instance can scroll horizontally; otherwise, <c>false</c>.</returns>
    public override bool CanScrollHorizontally() {
      return allowScrolling && base.CanScrollHorizontally();
    }
/*

    /// <summary>
    /// Smooths the scroll to position.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="state">State.</param>
    /// <param name="position">Position.</param>
    public override void SmoothScrollToPosition(RecyclerView recyclerView, RecyclerView.State state, int position) {
      var ss = new TopSnappedSmoothScroller(this);
      ss.TargetPosition = position;
      StartSmoothScroll(ss);
    }

    private class TopSnappedSmoothScroller : LinearSmoothScroller {
      private SmoothItemScrollLinearLayoutManager llm;
      /// <summary>
      /// Initializes a new instance of the
      /// <see cref="ION.Droid.Widgets.RecyclerViews.SmoothItemScrollLinearLayoutManager+TopSnappedSmoothScroller"/> class.
      /// </summary>
      /// <param name="context">Context.</param>
      public TopSnappedSmoothScroller(SmoothItemScrollLinearLayoutManager llm) : base(llm.context) {
        this.llm = llm;
      }

      /// <summary>
      /// Computes the scroll vector for position.
      /// </summary>
      /// <returns>The scroll vector for position.</returns>
      /// <param name="targetPosition">Target position.</param>
      public override Android.Graphics.PointF ComputeScrollVectorForPosition(int targetPosition) {
        return llm.ComputeScrollVectorForPosition(targetPosition);
      }

      /// <summary>
      /// Gets the vertical snap preference.
      /// </summary>
      /// <returns>The vertical snap preference.</returns>
      public override int GetVerticalSnapPreference() {
        return LinearSmoothScroller.SnapToStart;
      }
    }
*/
  }
}

