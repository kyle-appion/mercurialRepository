using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace ION.Droid.Dialog {
  public class IONAlertDialog : AlertDialog.Builder, IDialogInterfaceOnCancelListener {
    /// <summary>
    /// A delegate that is called when the dialog is canceled.
    /// </summary>
    public delegate void OnCanceled();

    /// <summary>
    /// The callback that is notified when the dialog is canceled.
    /// </summary>
    public OnCanceled onCanceled { get; set; }

    /// <summary>
    /// Whether or not the title should be truncated.
    /// </summary>
    public bool truncate { get; set; }

    public IONAlertDialog(Context context) : this(context, "") {
      // Nope
    }

    public IONAlertDialog(Context context, int titleRes) : this(context, context.GetString(titleRes)) {
      // Nope
    }

    public IONAlertDialog(Context context, string title) : base(context) {
      this.SetTitle(title);
    }

    // Overridden from IAlertDialog.Builder
    public override AlertDialog Create() {
      View v = LayoutInflater.From(Context).Inflate(Resource.Layout.dialog_title, null, false);

      TextView titleView = v.FindViewById<TextView>(Resource.Id.title);
      View closeView = v.FindViewById(Resource.Id.close);

      if (truncate) {
        titleView.SetSingleLine(true);
        titleView.Ellipsize = TextUtils.TruncateAt.End;
      }

      SetCustomTitle(v);

      AlertDialog ret = base.Create();

      closeView.SetOnClickListener(new CloseDialogAction(ret));

      WindowManagerLayoutParams lp = new WindowManagerLayoutParams();
      lp.CopyFrom(ret.Window.Attributes);
      lp.Width = ViewGroup.LayoutParams.MatchParent;

      return ret;
    }

    // Overridden from IDialogInterfaceOnCancelListener
    public virtual void OnCancel(IDialogInterface dialog) {
      if (onCanceled != null) {
        onCanceled();
      }
    }

    internal class CloseDialogAction : Java.Lang.Object, View.IOnClickListener {
      public AlertDialog dialog { get; set; }

      public CloseDialogAction(AlertDialog dialog) {
        this.dialog = dialog;
      }

      // Overridden from View.IOnClickListener
      public void OnClick(View view) {
        dialog.Cancel();
      }
    } // End IONAlertDialog.CloseDialogAction
  } // End IONAlertDialog

  /// <summary>
  /// A simple class that will wrap an action in a C# action in a java object.
  /// </summary>
  public class ActionDialogClickListener : Java.Lang.Object, IDialogInterfaceOnClickListener {
    internal Action action { get; set; }

    public ActionDialogClickListener(Action action) {
      this.action = action;
    }

    // Overridden from IDialogInterfaceOnClickListener
    public void OnClick(IDialogInterface dialog, int i) {
      action();
    }
  }
}