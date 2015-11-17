namespace ION.Droid.Views {

  using System;

  using Android.Views;

  /// <summary>
  /// A simple wrapper class that will wrap an Android.View.OnLongClickListener such
  /// that we can pass an action to this class an not worry about implementation.
  /// </summary>
  public class ViewLongClickAction : Java.Lang.Object, View.IOnLongClickListener {

    public delegate void OnLongClickAction(View view);

    private OnLongClickAction action { get; set; }


    public ViewLongClickAction(OnLongClickAction action) {
      this.action = action;
    }

    // Overridden from View.IOnLongClickListener
    public bool OnLongClick(View view) {
      if (action != null) {
        action(view);
        return true;
      }
      return false;
    }
  }
}

