namespace ION.Droid.Widgets.Adapters.Viewer {

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

  using ION.Core.Content;

  public class WorkbenchAdapter : BaseAdapter {

    /// <summary>
    /// The list of manifold that are to be displayed by the adapter.
    /// </summary>
    /// <value>The content.</value>
    public List<Manifold> content {
      get;
      set;
    }

    /// <summary>
    /// The dictionary that will maintain a cache of sensor property views for the adapter.
    /// </summary>
    private Dictionary<Type, View> sensorPropertyViewCache = new Dictionary<Type, View>();

    public WorkbenchAdapter() {
    }

    // Overridden from BaseAdapter
    public override View GetView(int position, View convert, ViewGroup parent) {
      var manifold = content[position];
      var c = parent.Context;
      var vv = convert as ViewerView;

      if (vv == null) {
        vv = new ViewerView(c, this);
      }

      vv.manifold = manifold;

      return vv;
    }

    /// <summary>
    /// The actual view that the adapter will return to the workench's list view.
    /// </summary>
    internal class ViewerView : LinearLayout {
      public Manifold manifold { 
        get {
          return __manifold;
        }
        set {
          __manifold = value;
        }
      } View __manifold;

      private WorkbenchAdapter adapter { get; set; }
      private View manifoldView { get; set; }
      private List<View> propertyViews { get; set; }

      public ViewerView(Context context, WorkbenchAdapter adapter) : base(context) {
        this.adapter = adapter;
      }

      /// <summary>
      /// Gets (creating if necessary) a new manifold view (from Resources.Layout.manifold)
      /// </summary>
      /// <returns>The manifold view.</returns>
      /// <param name="convert">Convert.</param>
      private View GetManifoldView(View convert) {
        
      }
    }
  }
}

