namespace ION.Droid.Widgets {

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

  /// <summary>
  /// Useful extensions for list view.
  /// </summary>
  public class ListViewExtensions {

    /// <summary>
    /// Refreshes a single row in the list view.
    /// </summary>
    /// <param name="listView">List view.</param>
    /// <param name="row">Row.</param>
    public static void RefreshRow(this ListView listView, int row) {
      listView.Adapter.GetView(row, listView.GetChildAt(row - listView.FirstVisiblePosition), listView);
    }
  }
}

