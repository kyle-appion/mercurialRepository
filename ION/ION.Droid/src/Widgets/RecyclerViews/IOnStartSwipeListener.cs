namespace ION.Droid.Widgets.RecyclerViews {
  
  using System;

  using Android.Support.V7.Widget;

  /// <summary>
  /// Listener for manual initiation of a swipe.
  /// </summary>
  public interface IOnStartSwipeListener {
    /// <summary>
    /// Called when a view is requesting a start of swipe event.
    /// </summary>
    /// <param name="viewHolder">View holder.</param>
    void OnStartSwipe(RecyclerView.ViewHolder viewHolder);
  }
}

