namespace ION.Droid.Dialog {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Util;
  using Android.Preferences;
  using Android.Widget;

  /// <summary>
  /// A simple dialog that will allow the user to enter a number and return it to the called.
  /// </summary>
  public class NumberEntryDialog {

    /// <summary>
    /// The handler that will be notified when a number is selected.
    /// </summary>
    public EventHandler<double> handler;

    public string title;

    public string message;

    public string hint;

    public double initialValue;

    /// <summary>
    /// Shows the dialog.
    /// </summary>
    /// <param name="context">Context.</param>
    public AlertDialog Show(Context context) {
      var adb = new IONAlertDialog(context);
      adb.SetTitle(title);
      adb.SetMessage(message);

      var entry = new EditText(context);
      entry.Hint = hint;
      entry.Text = initialValue + "";
      entry.InputType = Android.Text.InputTypes.NumberFlagDecimal;

      adb.SetView(entry);

      AlertDialog ret = null;

      adb.SetNegativeButton(context.GetString(Resource.String.cancel), (obj, args) => {
        var dialog = obj as Android.App.Dialog;
        dialog.Dismiss();
      });

      adb.SetPositiveButton(context.GetString(Resource.String.ok_done), (obj, args) => {
        var dialog = obj as Android.App.Dialog;

        try {
          var number = double.Parse(entry.Text);
          if (handler != null) {
            handler(ret, number);
          }
          dialog.Dismiss();
        } catch (Exception e) {
					Toast.MakeText(context, Resource.String.please_enter_valid_number, ToastLength.Long).Show();
        }
      });

      ret = adb.Create();

      ret.Show();
      return ret;
    }
  }
}

