namespace ION.Droid.Widgets.Adapters.Navigation {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Util;

  /// <summary>
  /// The adapter that will provide navigation item views to the navigation list.
  /// </summary>
  public sealed class NavigationAdapter : BaseAdapter {
    // Overridden from BaseAdapter
    public override int Count { get { return content.Count; } }

    /// <summary>
    /// The content that the adapter is providing the the navigation list.
    /// </summary>
    /// <value>The content.</value>
    public List<NavigationItem> content { get; set; }
    /// <summary>
    /// The indexer that will return a NavigationItem from the adapter's content.
    /// </summary>
    /// <param name="index">Index.</param>
    public NavigationItem this[int index] {
      get {
        return content[index];
      }
    }

    /// <summary>
    /// The cache that will store bitmaps.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache { get; set; }

    /// <summary>
    /// Creates a new NavigationAdapter. The content provided to this adapter can contain
    /// items that have mixed hiddenness; the adapter will got through and ensure that 
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="content">Content.</param>
    public NavigationAdapter(List<NavigationItem> content, BitmapCache cache) {
			this.content = Expand(content);
      this.cache = cache;
    }

    // Overridden from BaseAdapter
    public override Java.Lang.Object GetItem(int position) {
      return null;
    }

    // Overridden from BaseAdapter
    public override long GetItemId(int position) {
      return this[position].id;
    }

    // Overridden from BaseAdapter
    public override View GetView(int position, View convert, ViewGroup parent) {
      var item = content[position];

      if (item is NavigationCategory) {
        return GetCategoryView(item as NavigationCategory, position, convert, parent);
      } else if (item is NavigationIconItem) {
        return GetIconView(item as NavigationIconItem, position, convert, parent);
      } else {
        throw new ArgumentException("Cannot create NavigationItem view for: " + item.GetType().Name);
      }
    }

    /// <summary>
    /// Gets the category view.
    /// </summary>
    /// <returns>The category view.</returns>
    /// <param name="postition">Postition.</param>
    /// <param name="convert">Convert.</param>
    /// <param name="parent">Parent.</param>
    private View GetCategoryView(NavigationCategory category, int postition, View convert, ViewGroup parent) {
      var res = parent.Context.Resources;
      CategoryViewHolder vh = convert?.Tag as CategoryViewHolder;

      if (convert == null || vh == null) {
        convert = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.navigation_category, parent, false);
        convert.Tag = vh = new CategoryViewHolder();

        vh.title = convert.FindViewById<TextView>(Resource.Id.title);
        vh.title.SetTextSize(Android.Util.ComplexUnitType.Px, res.GetDimension(Resource.Dimension.text_size_medium));
        vh.title.SetTextColor(res.GetColor(Resource.Color.gray));
      } else {
        vh = (CategoryViewHolder)convert.Tag;
      }

      vh.title.Text = category.title;

      return convert;
    }

    /// <summary>
    /// Gets the icon view.
    /// </summary>
    /// <returns>The icon view.</returns>
    /// <param name="item">Item.</param>
    /// <param name="position">Position.</param>
    /// <param name="convert">Convert.</param>
    /// <param name="parent">Parent.</param>
    private View GetIconView(NavigationIconItem item, int position, View convert, ViewGroup parent) {
      var res = parent.Context.Resources;
      IconViewHolder vh = convert?.Tag as IconViewHolder;

      if (convert == null || vh == null) {
        convert = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.navigation_icon_item, parent, false);
        convert.Tag = vh = new IconViewHolder();

        vh.title = convert.FindViewById<TextView>(Resource.Id.title);
        vh.title.SetTextSize(Android.Util.ComplexUnitType.Px, res.GetDimension(Resource.Dimension.text_size_large));
        vh.title.SetTextColor(res.GetColor(Resource.Color.white));

        vh.icon = convert.FindViewById<ImageView>(Resource.Id.icon);
      } else {
        vh = (IconViewHolder)convert.Tag;
      }

      vh.title.Text = item.title;
      vh.icon.SetImageBitmap(cache.GetBitmap(item.icon));
      vh.icon.SetColorFilter(new Color(res.GetColor(Resource.Color.gray)), PorterDuff.Mode.SrcAtop);

      return convert;
    }

    /// <summary>
    /// Removes this hidden items from the list.
    /// </summary>
    /// <param name="items">Items.</param>
		private List<NavigationItem> Expand(List<NavigationItem> items) {
      var ret = new List<NavigationItem>();

      foreach (var item in items) {
        if (item is NavigationCategory) {
          var cat = item as NavigationCategory;
//          if (!item.hidden) {
            ret.Add(item);
            var list = new List<NavigationItem>();
            list.AddRange(cat.items);
            ret.AddRange(Expand(list));
//          }
        } else {
//          if (!item.hidden) {
            ret.Add(item);
//          }
        }
      }

      return ret;
    }

    /// <summary>
    /// Root view holder for a navigation item.
    /// </summary>
    private class ViewHolder : Java.Lang.Object {
      public TextView title;
    }

    /// <summary>
    /// The view holder that will holds view for the NavigationCategory.
    /// </summary>
    private class CategoryViewHolder : ViewHolder {
    }

    /// <summary>
    /// The view holder that will hold views for the NavigationIconItem.
    /// </summary>
    private class IconViewHolder : ViewHolder {
      public ImageView icon;
    }
  }
}

