namespace ION.Droid.Widgets.Adapters {

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
  /// RootView is a view that is used by the expandable content adapters. It
  /// maintains some state about the child row allowing for simpler expansion
  /// and collapse of the content.
  /// </summary>
  public class RootView : LinearLayout {
    /// <summary>
    /// The last known position within the list view that the root view occupies.
    /// </summary>
    public int position { get; set; }

    /// <summary>
    /// The layout that will hold the title layout.
    /// </summary>
    /// <remarks>
    /// Note: the id of this layout will ALWAYS be set to Resource.Id.title on set.
    /// </remarks>
    public FrameLayout titleLayout {
      get {
        return __titleLayout;
      }
      set {
        __titleLayout = value;
        __titleLayout.Id = Resource.Id.title;
      }
    } FrameLayout __titleLayout;
    /// <summary>
    /// The layout that will hold the content layout.
    /// </summary>
    /// <remarks>
    /// Note: the id of this layout will ALWAYS be set to Resource.Id.content on set.
    /// </remarks>
    public FrameLayout contentLayout {
      get {
        return __contentLayout;
      }
      set {
        __contentLayout = value;
        __contentLayout.Id = Resource.Id.content;
      }
    } FrameLayout __contentLayout;

    /// <summary>
    /// The actual title view.
    /// </summary>
    public View titleView {
      get {
        return __titleView;
      }
      set {
        titleLayout.RemoveAllViews();
        titleLayout.AddView(__titleView = value);
      }
    } View __titleView;
    /// <summary>
    /// The actual content view.
    /// </summary>
    public View contentView {
      get {
        return __contentView;
      }
      set {
        contentLayout.RemoveAllViews();
        contentLayout.AddView(__contentView = value);
      }
    } View __contentView;

    /// <summary>
    /// Creates a new empty RootView.
    /// </summary>
    /// <param name="context"></param>
    public RootView(Context context) : base(context) {
      Orientation = Orientation.Vertical;

      AddView(titleLayout = new FrameLayout(context));
      AddView(contentLayout = new FrameLayout(context));
    }
  }
}