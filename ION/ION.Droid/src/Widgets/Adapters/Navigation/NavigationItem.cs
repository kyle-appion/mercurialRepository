namespace ION.Droid.Widgets.Adapters.Navigation {

  using System;
  using System.Collections.Generic;


  using Android.Content;
  using Android.Graphics.Drawables;

  /// <summary>
  /// An interface that defines the contract for an item that may be used in the
  /// navigation adapter.
  /// </summary>
  public interface NavigationItem {
    /// <summary>
    /// The value that is used to identify this navigation item. Note: while uniqueness
    /// of this id is not enforced, it is recommended that the id be as close to unique
    /// for a given category tree as possible. 
    /// </summary>
    /// <value>The identifier.</value>
    int id { get; }
    /// <summary>
    /// The title of the navigation item.
    /// </summary>
    /// <value>The title.</value>
    string title { get; set; }
    /// <summary>
    /// The action that will be fired when the item is clicked.
    /// </summary>
    /// <value>The action.</value>
    Action action { get; set; }
    /// <summary>
    /// Whether or not the NavigationItem is hidden (should be viewed) in the navigation
    /// drawer.
    /// </summary>
    /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
    bool hidden { get; set; }
  }

  public class BaseNavigationItem : NavigationItem {
    // Overridden from NavigationItem
    public int id { get; set; }
    // Overridden from NavitationItem
    public string title { get; set; }
    // Overridden from NavigationItem
    public Action action { get; set; }
    // Overridden from NavigationItem
    public bool hidden { get; set; }


    public BaseNavigationItem() {
      hidden = false;
    }

    public BaseNavigationItem(int id, string title, Action action) {
      this.id = id;
      this.title = title;
      this.action = action;
      this.hidden = false;
    }
  }

  /// <summary>
  /// A container that will hold a number of navigation items.
  /// </summary>
  public class NavigationCategory : BaseNavigationItem {
    /// <summary>
    /// The list of navigation items that are held within this category.
    /// </summary>
    /// <value>The items.</value>
    public NavigationItem[] items { get; set; }
    /// <summary>
    /// The indexer that will retrieve a single navigation item from the category.
    /// </summary>
    /// <param name="index">Index.</param>
    public NavigationItem this[int index] {
      get {
        return items[index];
      }
    }

    public NavigationCategory() {
    }

    public NavigationCategory(int id, string title, params NavigationItem[] items) : this(id, title, null, items) {
    }

    public NavigationCategory(int id, string title, Action action, params NavigationItem[] items) : base(id, title, action) {
      this.items = items;
    }

    /// <summary>
    /// Finds the NavigationItem with the given id.
    /// </summary>
    /// <returns>The item by identifier.</returns>
    /// <param name="id">Identifier.</param>
    public NavigationItem FindItemById(int id) {
      if (this.id == id) {
        return this;
      } else {
        foreach (var item in items) {
          if (item is NavigationCategory) {
            var cat = (NavigationCategory)item;
            return cat.FindItemById(id);
          } else {
            if (item.id == id) {
              return item;
            }
          }
        }
      }

      return null;
    }
  }

  /// <summary>
  /// A simple container for items that will navigation the user to various parts
  /// of the app upon their action being fired.
  /// </summary>
  public class NavigationIconItem : BaseNavigationItem {
    /// <summary>
    /// The icon for the navigation item.
    /// </summary>
    /// <value>The icon.</value>
    public int icon { get; set; }

    public NavigationIconItem() {
    }

    public NavigationIconItem(int id, string title, Action action) : base(id, title, action) {
    }
  }
}

