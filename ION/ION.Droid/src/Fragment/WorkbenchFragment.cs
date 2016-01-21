namespace ION.Droid.Fragment {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;

  using ION.Droid.Activity;
  using ION.Droid.Sensors;
  using ION.Droid.Widgets.Adapters.Workbench;
  using ION.Droid.Widgets.RecyclerViews;

  public class WorkbenchFragment : IONFragment, IOnStartDragListener {

    /// <summary>
    /// The activity request code that will tell us when we return from the device
    /// manager activity.
    /// </summary>
    private const int REQUEST_SENSOR = 1;

    /// <summary>
    /// The workbench that the fragment is currently working with.
    /// </summary>
    /// <value>The workbench.</value>
    public Workbench workbench { get; set; }

    /// <summary>
    /// The listview that will display the Viewers (manifolds) for workbench fragment.
    /// </summary>
    /// <value>The list view.</value>
    private RecyclerView list { get; set; }
    /// <summary>
    /// The item touch helper that will assist in resolving RecyclerView touches.
    /// </summary>
    /// <value>The item touch helper.</value>
    private ItemTouchHelper itemTouchHelper { get; set; }



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

      list = ret.FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(Activity));

      return ret;
    }

    // Overridden from Fragment
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      if (workbench == null) {
        workbench = ion.currentWorkbench;
      }

      adapter = new WorkbenchAdapter(ion, Resources, this);
      adapter.SetWorkbench(null, OnAddViewer);
      list.SetAdapter(adapter);

      itemTouchHelper = new ItemTouchHelper(new SimpleItemTouchHelperCallback(adapter));
      itemTouchHelper.AttachToRecyclerView(list);
    }

    // Overridden from Fragment
    public override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      switch (requestCode) {
        case REQUEST_SENSOR:
          var sp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
          var sensor = sp.Get(ion);
          workbench.AddSensor(sensor);
          adapter.AddSensor(sensor);
          break;
        default:
          base.OnActivityResult(requestCode, resultCode, data);
          break;
      }
    }

    // Overridden from IOnStartDragListener
    public void OnStartDrag(RecyclerView.ViewHolder viewHolder) {
      itemTouchHelper.StartDrag(viewHolder);
    }

    /// <summary>
    /// Called when the adapter's footer is called.
    /// </summary>
    private void OnAddViewer() {
      var i = new Intent(Activity, typeof(DeviceManagerActivity));
      i.SetAction(Intent.ActionPick);
      StartActivityForResult(i, REQUEST_SENSOR);
    }
  }
}

