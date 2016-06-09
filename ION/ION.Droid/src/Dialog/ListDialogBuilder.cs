using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ION.Droid.Dialog {
  /// <summary>
  /// ListDialogBuilder is a class that helps in the creation of ION themed list
  /// dialogs.
  /// </summary>
  public class ListDialogBuilder : IONAlertDialog, IDialogInterfaceOnClickListener {
    /// <summary>
    /// The list of rows that are present in the dialog.
    /// </summary>
    private List<Row> content { get; set; }

    public ListDialogBuilder(Context context) : base(context) {
      content = new List<Row>();
    }

    // Overridden from IONAlertDialog
    public override AlertDialog Show() {
      string[] texts = new string[content.Count];

      for (int i = 0; i < texts.Length; i++) {
        texts[i] = content[i].title;
      }

      SetItems(texts, this);
      SetCancelable(true);

      return base.Show();
    }

    // Overridden from IDialogInterfaceOnClickListener
    public void OnClick(IDialogInterface dialog, int position) {
      var action = content[position].action;
      if (action != null) {
        action();
      }
      dialog.Dismiss();
    }

    /// <summary>
    /// Adds a new row to the dialog.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public ListDialogBuilder AddItem(string title, Action action) {
      content.Add(new Row(title, action));
      return this;
    }

    /// <summary>
    /// Adds a new row to the dialog. The id of the row will be the strinf res.
    /// </summary>
    /// <param name="stringRes"></param>
    /// <returns></returns>
    public ListDialogBuilder AddItem(int stringRes, Action action) {
      content.Add(new Row(Context.GetString(stringRes), action));
      return this;
    }

    /// <summary>
    /// Contains the necessary information to identify a row.
    /// </summary>
    internal struct Row {
      public string title { get; set; }
      public Action action { get; set; }

      public Row(string title, Action action) : this() {
        this.title = title;
        this.action = action;
      }
    }
  }
}