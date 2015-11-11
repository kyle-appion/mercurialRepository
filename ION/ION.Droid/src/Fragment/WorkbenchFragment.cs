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

  using ION.Droid.Activity;
  using ION.Droid.Widgets.Adapters;

  public class WorkbenchFragment : IONFragment, View.IOnClickListener {

    /// <summary>
    /// The activity request code that will tell us when we return from the device
    /// manager activity.
    /// </summary>
    private const int REQUEST_SENSOR = 1;

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
      var c = inflater.Context;
      var ret = inflater.Inflate(Resource.Layout.fragment_workbench, container, false);

      listView = ret.FindViewById<ListView>(Android.Resource.Id.List);
      var addNewViewerButton = new Button(c);
      addNewViewerButton.Id = Resource.Id.add;
      addNewViewerButton.Text = c.GetString(Resource.String.workbench_add_viewer);
      addNewViewerButton.SetOnClickListener(this);
      listView.AddFooterView(addNewViewerButton);

      return ret;
    }

    // Overridden from Fragment
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      adapter = new WorkbenchAdapter();
      adapter.content = new List<Manifold>();
      listView.Adapter = adapter;
    }

    public override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      switch (requestCode) {
        case REQUEST_SENSOR:
          break;
        default:
          base.OnActivityResult(requestCode, resultCode, data);
          break;
      }
    }

    // Overridden from View.IOnClickListener
    public void OnClick(View view) {
      if (Resource.Id.add == view.Id) {
        var i = new Intent(Activity, typeof(DeviceManagerActivity));
        i.SetAction(Intent.ActionPick);
        StartActivityForResult(i, REQUEST_SENSOR);
      }
    }
  }
}

