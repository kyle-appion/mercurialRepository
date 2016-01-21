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
    /// A delegate that is called when a list option is selected.s
    /// </summary>
    /// <param name="optionId"></param>
    public delegate void OnOptionSelected(int optionId);
    

    /// <summary>
    /// The callback that is notified when a list item is selected.
    /// </summary>
    public OnOptionSelected onOptionSelected { get; set; }

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
      onOptionSelected(content[position].id);
    }

    /// <summary>
    /// Adds a new row to the dialog.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public ListDialogBuilder AddItem(string title, int id) {
      content.Add(new Row(title, id));
      return this;
    }

    /// <summary>
    /// Adds a new row to the dialog. The id of the row will be the strinf res.
    /// </summary>
    /// <param name="stringRes"></param>
    /// <returns></returns>
    public ListDialogBuilder AddItem(int stringRes) {
      content.Add(new Row(Context.GetString(stringRes), stringRes));
      return this;
    }

    /// <summary>
    /// Adds a new row to the dialog.
    /// </summary>
    /// <param name="stringRes"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public ListDialogBuilder AddItem(int stringRes, int id) {
      content.Add(new Row(Context.GetString(stringRes), id));
      return this;
    }

    /// <summary>
    /// Contains the necessary information to identify a row.
    /// </summary>
    internal struct Row {
      public string title { get; set; }
      public int id { get; set; }

      public Row(string title, int id) : this() {
        this.title = title;
        this.id = id;
      }
    }
  }
}