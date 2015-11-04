namespace ION.Droid.Widgets.Adapters.Navigation {

  using System;

  using Android.Content;
  using Android.Graphics.Drawables;

  /// <summary>
  /// A simple container for items that will navigation the user to various parts
  /// of the app upon their action being fired.
  /// </summary>
  public class NavigationItem {
    /// <summary>
    /// The value that is used to identify this explicity item within a navigation
    /// list.
    /// </summary>
    /// <value>The identifier.</value>
    public int id { get; private set; }
    /// <summary>
    /// The action that will be fired when the item is clicked.
    /// </summary>
    /// <value>The action.</value>
    public Action action { get; set; }
    /// <summary>
    /// The icon for the navigation item.
    /// </summary>
    /// <value>The icon.</value>
    public Drawable icon { get; set; }
    /// <summary>
    /// The title of the navigation item.
    /// </summary>
    /// <value>The title.</value>
    public string title { get; set; }

    public NavigationItem(int id) {
      this.id = id;
    }

    public NavigationItem(Context context, int id, int icon, int title, Action action) 
      : this(id, context.GetDrawable(icon), title, action) {
    }

    public NavigationItem(int id, Drawable icon, string title, Action action) {
      this.id = id;
      this.icon = icon;
      this.title = title;
      this.action = action;
    }
  }
}

