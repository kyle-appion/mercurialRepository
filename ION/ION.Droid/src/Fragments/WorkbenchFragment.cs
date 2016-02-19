namespace ION.Droid.Fragments {

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
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Views;
  using ION.Droid.Widgets.Adapters.Workbench;
  using ION.Droid.Widgets.RecyclerViews;

  public class WorkbenchFragment : IONFragment, IOnStartDragListener, IOnStartSwipeListener {

    /// <summary>
    /// The activity request code that will tell us when we return from the device
    /// manager activity.
    /// </summary>
    private const int REQUEST_SENSOR = 1;
    private const int REQUEST_SHOW_PTCHART = 2;
    private const int REQUEST_SHOW_SUPERHEAT_SUBCOOL = 3;

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

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
    }

    /// <Docs>The LayoutInflater object that can be used to inflate
    ///  any views in the fragment,</Docs>
    /// <param name="savedInstanceState">If non-null, this fragment is being re-constructed
    ///  from a previous saved state as given here.</param>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create view event.
    /// </summary>
    /// <param name="inflater">Inflater.</param>
    /// <param name="container">Container.</param>
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var c = inflater.Context;
      var ret = inflater.Inflate(Resource.Layout.fragment_workbench, container, false);

      list = ret.FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(Activity));

      return ret;
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);
      SetHasOptionsMenu(true);
      AddFlags(EFlags.AllowScreenshot);

      if (workbench == null) {
        workbench = ion.currentWorkbench;
      }

      adapter = new WorkbenchAdapter(ion, Resources);
      adapter.dragListener = this;
      adapter.swipeListener = this;
      adapter.SetWorkbench(ion.currentWorkbench, OnAddViewer);
      list.SetAdapter(adapter);
      adapter.onManifoldClicked += (manifold) => {
        ShowManifoldContextDialog(manifold);
      };

      adapter.onSensorPropertyClicked += (manifold, sensorProperty) => {
        var sensor = sensorProperty.sensor;


        if (sensorProperty is PTChartSensorProperty) {
          var pt = ((PTChartSensorProperty)sensorProperty);
          var i = new Intent(Activity, typeof(PTChartActivity));
          i.SetAction(Intent.ActionPick);
          i.PutExtra(PTChartActivity.EXTRA_SENSOR, sensor.ToParcelable());
          StartActivityForResult(i, REQUEST_SHOW_PTCHART);
        } else if (sensorProperty is SuperheatSubcoolSensorProperty) {
          var sp = sensorProperty as SuperheatSubcoolSensorProperty;
          var i = new Intent(Activity, typeof(SuperheatSubcoolActivity));
          i.SetAction(Intent.ActionPick);

          switch (sensor.type) {
            case ESensorType.Pressure:
              i.PutExtra(SuperheatSubcoolActivity.EXTRA_PRESSURE_SENSOR, sensor.ToParcelable());
              if (manifold.secondarySensor != null) {
                i.PutExtra(SuperheatSubcoolActivity.EXTRA_TEMPERATURE_SENSOR, sensor.ToParcelable());
              }
              break;
            case ESensorType.Temperature:
              i.PutExtra(SuperheatSubcoolActivity.EXTRA_TEMPERATURE_SENSOR, sensor.ToParcelable());
              if (manifold.secondarySensor != null) {
                i.PutExtra(SuperheatSubcoolActivity.EXTRA_PRESSURE_SENSOR, sensor.ToParcelable());
              }
              break;
            default:
              var msg = "Cannot start SuperheatSubcoolActivity: sensor is not valid {" + sensor.type + "}";
              Log.E(this, msg);
              Alert(msg);
              break;
          }

          StartActivityForResult(i, REQUEST_SHOW_SUPERHEAT_SUBCOOL);
        }
      };

//      itemTouchHelper = new ItemTouchHelper(new SimpleItemTouchHelperCallback(adapter));
//      itemTouchHelper.AttachToRecyclerView(list);
    }

    /// <Docs>The integer request code originally supplied to
    ///  startActivityForResult(), allowing you to identify who this
    ///  result came from.</Docs>
    /// <param name="data">An Intent, which can return result data to the caller
    ///  (various data can be attached to Intent "extras").</param>
    /// <summary>
    /// Raises the activity result event.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    public override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      switch (requestCode) {
        case REQUEST_SENSOR:
          if (data != null && data.HasExtra(DeviceManagerActivity.EXTRA_SENSOR)) {
            var sp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
            var sensor = sp.Get(ion);
            workbench.AddSensor(sensor);
          }
          break;
        case REQUEST_SHOW_PTCHART:
          if (data != null && data.HasExtra(PTChartActivity.EXTRA_SENSOR)) {
            var u = data.GetIntExtra(PTChartActivity.EXTRA_RETURN_UNIT, -1);
            if (u != -1) {
              // TODO ahodder@appioninc.com: Assign the unit.
//              Alert("Please change the unit for the pt chart");
            }
          }
          break;
        default:
          base.OnActivityResult(requestCode, resultCode, data);
          break;
      }
    }

    /// <summary>
    /// Called when a view is requesting a start of a drag.
    /// </summary>
    /// <param name="viewHolder">The holder of the view to drag.</param>
    public void OnStartDrag(RecyclerView.ViewHolder viewHolder) {
      itemTouchHelper.StartDrag(viewHolder);
    }

    /// <summary>
    /// Called when a view is requesting a start of swipe event.
    /// </summary>
    /// <param name="viewHolder">View holder.</param>
    public void OnStartSwipe(RecyclerView.ViewHolder viewHolder) {
      itemTouchHelper.StartSwipe(viewHolder);
    }

    /// <summary>
    /// Called when the adapter's footer is called.
    /// </summary>
    private void OnAddViewer() {
      var i = new Intent(Activity, typeof(DeviceManagerActivity));
      i.SetAction(Intent.ActionPick);
      StartActivityForResult(i, REQUEST_SENSOR);
    }

    /// <summary>
    /// Shows a context dialog for a manifold. This will present all the options that are available for
    /// the manifold.
    /// </summary>
    private void ShowManifoldContextDialog(Manifold manifold) {
      var ldb = new ListDialogBuilder(Activity);

      var dgs = manifold.primarySensor as GaugeDeviceSensor;

      if (dgs != null && !dgs.device.isConnected) {
        ldb.AddItem(Resource.String.connect, () => {
          dgs.device.connection.Connect();
        });
      }

      ldb.AddItem(Resource.String.rename, () => {
        Toast.MakeText(Activity, "Rename up in this bitch", ToastLength.Short).Show();
      });


      ldb.AddItem(Resource.String.workbench_add_viewer_sub, () => {
        ShowAddSubviewDialog(manifold);
      });

      ldb.AddItem(Resource.String.alarm, () => {
        Toast.MakeText(Activity, "Show some alarms up in dis bitch", ToastLength.Short).Show();
      });

      if (dgs != null && dgs.device.isConnected) {
        ldb.AddItem(Resource.String.disconnect, () => {
          dgs.device.connection.Disconnect();
        });
      }

      ldb.AddItem(Resource.String.remove, () => {
        workbench.Remove(manifold);
      });

      ldb.Show();
    }

    /// <summary>
    /// Shows the add subview dialog.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void ShowAddSubviewDialog(Manifold manifold) {
      Func<int, int, string> format = delegate (int full, int abrv) {
        return GetString(full) + " (" + GetString(abrv) + ")";
      };

      var ldb = new ListDialogBuilder(Activity);

      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_alt, Resource.String.workbench_alt_abrv), () => {
          manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_roc, Resource.String.workbench_roc_abrv), () => {
          manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_min, Resource.String.workbench_min_abrv), () => {
          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_max, Resource.String.workbench_max_abrv), () => {
          manifold.AddSensorProperty(new MaxSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_hold, Resource.String.workbench_hold_abrv), () => {
          manifold.AddSensorProperty(new HoldSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_timer, Resource.String.workbench_timer_abrv), () => {
          manifold.AddSensorProperty(new TimerSensorProperty(manifold.primarySensor));
        });
      }

      if (ESensorType.Pressure == manifold.primarySensor.type || ESensorType.Temperature == manifold.primarySensor.type) {
        if (!manifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))) {
          ldb.AddItem(format(Resource.String.workbench_ptchart, Resource.String.fluid_pt_abrv), () => {
            manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
          });
        }

        if (!manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))) {
          ldb.AddItem(format(Resource.String.workbench_shsc, Resource.String.workbench_shsc_abrv), () => {
            manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
          });
        }
      }

      ldb.Show();
    }
  }
}

