namespace ION.Droid.Fragment {

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

  using ION.Droid.Widgets.Adapters;

  public class WorkbenchFragment : IONFragment {

    /// <summary>
    /// The listview that will display the Viewers (manifolds) for workbench fragment.
    /// </summary>
    /// <value>The list view.</value>
    public ListView listView { get; set; }

    /// <summary>
    /// The adapter that will provide the list view with views.
    /// </summary>
    /// <value>The adapter.</value>
    public WorkbenchAdapter adapter { get; set; }

    // Overridden from Fragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
    }

    // Overridden from OnCreateView
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_workbench, container, false);

      listView = ret.FindViewById<ListView>(Android.Resource.Id.List);

      return ret;
    }

    // Overridden from Fragment
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      adapter = new WorkbenchAdapter();
      adapter.content = new List<Manifold>();
      listView.Adapter = adapter;
    }
  }
}

