namespace ION.Droid.Widgets.RecyclerViews {

  using System;
  using Android.Views;
  using Android.Support.V4.View;
  using Android.Support.V7.Widget;


  public class TouchListenerHelper : Java.Lang.Object, View.IOnTouchListener, GestureDetector.IOnGestureListener {
    private const int DISTANCE_THRESHOLD = 100;
    private const int VELOCITY_THRESHOLD = 100;

    public IOnStartDragListener dragStartListener { get; set; }
    public IOnStartSwipeListener swipeStartListener { get; set; }

    private RecyclerView.ViewHolder itemHolder;

    /// <summary>
    /// The gesture detector that will determin the type of gestures as they occur.
    /// </summary>
    /// <value>The gesture.</value>
    private GestureDetector gesture { get; set; }

    public TouchListenerHelper(RecyclerView.ViewHolder holder) {
      itemHolder = holder;
      gesture = new GestureDetector(this);
    }

    /// <summary>
    /// Raises the touch event.
    /// </summary>
    /// <param name="v">V.</param>
    /// <param name="e">E.</param>
    public bool OnTouch(View v, MotionEvent e) {
      return gesture.OnTouchEvent(e);
    }

    /// <summary>
    /// Raises the down event.
    /// </summary>
    /// <param name="e">E.</param>
    public bool OnDown(MotionEvent e) {
      return false;
    }

    /// <summary>
    /// Raises the fling event.
    /// </summary>
    /// <param name="e1">E1.</param>
    /// <param name="e2">E2.</param>
    /// <param name="velocityx">Velocityx.</param>
    /// <param name="velocityy">Velocityy.</param>
    public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityx, float velocityy) {
      return false;
    }

    /// <summary>
    /// Raises the long press event.
    /// </summary>
    /// <param name="e">E.</param>
    public void OnLongPress(MotionEvent e) {
    }

    /// <summary>
    /// Raises the scroll event.
    /// </summary>
    /// <param name="e1">E1.</param>
    /// <param name="e2">E2.</param>
    public bool OnScroll(MotionEvent e1, MotionEvent e2, float velocityx, float velocityy) {
      var dx = Math.Abs(e2.GetX() - e1.GetX());
      var dy = Math.Abs(e2.GetY() - e1.GetY());

      if (dx > dy && dx > DISTANCE_THRESHOLD && dx > VELOCITY_THRESHOLD) {
        NotifyOfSwipe();
        return true;
      } else if (dy > dx && dy > DISTANCE_THRESHOLD && dy > VELOCITY_THRESHOLD) {
        NotifyOfDrag();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Raises the show press event.
    /// </summary>
    /// <param name="e">E.</param>
    public void OnShowPress(MotionEvent e) {
    }

    /// <summary>
    /// Raises the single tap up event.
    /// </summary>
    /// <param name="e">E.</param>
    public bool OnSingleTapUp(MotionEvent e) {
      return false;
    }

    private void NotifyOfDrag() {
      if (dragStartListener != null) {
        dragStartListener.OnStartDrag(itemHolder);
      }
    }

    private void NotifyOfSwipe() {
      if (swipeStartListener != null) {
        swipeStartListener.OnStartSwipe(itemHolder);
      }
    }
  }
}

