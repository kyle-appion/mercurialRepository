namespace BluetoothTesting {

  using System;

  using Android.Views;

  /// <summary>
  /// A simple wrapper class that will wrap an Android.View.OnClickListener such
  /// that we can pass an action to this class an not worry about implementation.
  /// </summary>
  public class ViewClickAction : Java.Lang.Object, View.IOnClickListener {

    public delegate void OnClickAction(View view);

    private OnClickAction action { get; set; }


    public ViewClickAction(OnClickAction action) {
      this.action = action;
    }

    // Overridden from View.IOnClickListener
    public void OnClick(View view) {
      if (action != null) {
          action(view);
      }
    }
  }
}

