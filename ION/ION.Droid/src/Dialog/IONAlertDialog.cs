namespace ION.Droid.Dialog {

	using System;

	using Android.App;
	using Android.Content;
	using Android.Text;
	using Android.Views;
	using Android.Widget;

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

    private string title { get; set; }

    private bool isCancelable;

    public IONAlertDialog(Context context) : this(context, "") {
      // Nope
    }

    public IONAlertDialog(Context context, int titleRes) : this(context, context.GetString(titleRes)) {
      // Nope
    }

    public IONAlertDialog(Context context, string title) : base(context) {
      this.title = title;
      SetCancelable(true);
    }

    /// <summary>
    /// Sets the title.
    /// </summary>
    /// <returns>The title.</returns>
    /// <param name="title">Title.</param>
    public override AlertDialog.Builder SetTitle(Java.Lang.ICharSequence title) {
      this.title = title.ToString();
      return this;
    }

    /// <summary>
    /// Sets the title.
    /// </summary>
    /// <returns>The title.</returns>
    /// <param name="titleId">Title identifier.</param>
    public override AlertDialog.Builder SetTitle(int titleId) {
      title = Context.GetString(titleId);
      return this;
    }

    /// <summary>
    /// Sets the cancelable.
    /// </summary>
    /// <returns>The cancelable.</returns>
    public override AlertDialog.Builder SetCancelable(bool cancelable) {
      base.SetCancelable(cancelable);
      this.isCancelable = cancelable;
      return this;
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
      titleView.Text = title;

      AlertDialog ret = base.Create();

      closeView.SetOnClickListener(new CloseDialogAction(ret));
      closeView.Visibility = (isCancelable) ? ViewStates.Visible : ViewStates.Gone;

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

		/// <summary>
		/// A quick builder that will show a dialog and assign action callbacks to the postive and negative buttons.
		/// </summary>
		/// <returns>The dialog.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="positive">Positive.</param>
		/// <param name="negative">Negative.</param>
		public static Dialog ShowDialog(Context context, int title, int message, Action positive, Action negative = null) {
			return ShowDialog(context, context.GetString(title), context.GetString(message), positive, negative);
		}

		public static Dialog ShowDialog(Context context, string title, string message, Action positive, Action negative = null) {
			var adb = new IONAlertDialog(context, title);
			adb.SetMessage(message);

			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {
				(sender as Dialog).Dismiss();
				if (negative != null) {
					negative();
				}
			});

			adb.SetPositiveButton(Resource.String.ok, (sender, e) => {
				(sender as Dialog).Dismiss();
				positive();
			});

			return adb.Show();
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