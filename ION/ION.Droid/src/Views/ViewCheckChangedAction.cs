namespace ION.Droid {

  using System;

  using Android.Widget;

  /// <summary>
  /// A simple wrapper class that will wrap an CheckedChangedListener into a delegatable object.
  /// </summary>
  public class ViewCheckChangedAction : Java.Lang.Object, Android.Widget.CompoundButton.IOnCheckedChangeListener {
    public delegate void OnCheckedChangedAction(CompoundButton button, bool isChecked);

    private OnCheckedChangedAction action;

    public ViewCheckChangedAction(OnCheckedChangedAction action) {
      this.action = action;
    }

    /// <summary>
    /// Raises the checked changed event.
    /// </summary>
    /// <param name="button">Button.</param>
    /// <param name="isChecked">Is checked.</param>
    public void OnCheckedChanged(CompoundButton button, bool isChecked) {
      action(button, isChecked);
    }
  }
}

