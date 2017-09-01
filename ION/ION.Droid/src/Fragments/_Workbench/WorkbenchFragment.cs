namespace ION.Droid.Fragments._Workbench {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
  using ION.Core.Sensors.Properties;

  using Activity;
  using Activity.Grid;
	using App;
  using ION.Droid.Content;
  using Dialog;
  using Sensors;
	using Widgets.RecyclerViews;

  public class WorkbenchFragment : IONFragment {
    /// <summary>
    /// The activity request code that will tell us when we return from the device
    /// manager activity.
    /// </summary>
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
      try {
        SetHasOptionsMenu(true);
        AddFlags(EFlags.AllowScreenshot | EFlags.StartRecording);
      } catch (Exception e) {
        Log.E(this, "Something failed when starting the workbench fragment", e);
      }
    }

		// Overridden from Fragment
    public override void OnResume() {
      base.OnResume();

			if (workbench == null || workbench != ion.currentWorkbench) {
				workbench = ion.currentWorkbench;
			}

			if (workbench == null) {
				workbench = ion.LoadWorkbenchAsync().Result;
			}

			adapter = new WorkbenchAdapter(OnAddViewer, workbench, ion is RemoteION);
			list.SetAdapter(adapter);

      adapter.NotifyDataSetChanged();
			adapter.onSensorPropertyClicked += OnOnSensorPropertyClicked;
			adapter.onManifoldClicked += OnManifoldClicked;
    }

		// Overridden from Fragment
		public override void OnPause() {
			base.OnPause();

			adapter.onSensorPropertyClicked -= OnOnSensorPropertyClicked;
			adapter.onManifoldClicked -= OnManifoldClicked;
		}

		// Overridden from Fragment
    public override void OnDestroy() {
      base.OnDestroy();

      list.SetAdapter(null);
			if (adapter != null) {
				adapter.Dispose();
			}
    }

		// Overridden from Fragment
    public override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			if (resultCode == Result.Canceled) {
				base.OnActivityResult(requestCode, resultCode, data);
				return;
			}
      switch (requestCode) {
        case REQUEST_SHOW_PTCHART:
          if (data != null && data.HasExtra(PTChartActivity.EXTRA_SENSOR)) {
            var u = data.GetIntExtra(PTChartActivity.EXTRA_RETURN_UNIT, -1);
            if (u != -1) {
              // TODO ahodder@appioninc.com: Assign the unit.
							Log.E(this, "Invalid unit on activity result for ptchart");
							Alert(GetString(Resource.String.ptchart_error_change_unit));
            }
          }
          break;

				case REQUEST_SHOW_SUPERHEAT_SUBCOOL:
					if (data != null) {
						if (data.HasExtra(SuperheatSubcoolActivity.EXTRA_PRESSURE_SENSOR)) {
							
						}
					}
					break;
        default:
          base.OnActivityResult(requestCode, resultCode, data);
          break;
      }

			adapter.NotifyDataSetChanged();
    }

		private void OnManifoldClicked(Manifold manifold) {
			ShowManifoldContextDialog(manifold);
		}

    /// <summary>
    /// Called when the adapter's footer is called.
    /// </summary>
    private void OnAddViewer() {
      var i = new Intent(Activity, typeof(DeviceGridActivity));
			StartActivity(i);
    }

		private void OnOnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty) {
			var sensor = sensorProperty.sensor;

			if (sensorProperty is AlternateUnitSensorProperty) {
				var asp = sensorProperty as AlternateUnitSensorProperty;
				UnitDialog.Create(Activity, sensor.supportedUnits, (obj, u) => {
					asp.unit = u;
				}).Show();
			} else if (sensorProperty is PTChartSensorProperty) {
				var i = new Intent(Activity, typeof(PTChartActivity));
				i.SetAction(Intent.ActionPick);
				i.PutExtra(PTChartActivity.EXTRA_WORKBENCH_MANIFOLD, workbench.IndexOf(manifold));
				if (ion is RemoteION) {
					StartActivity(i);
				} else {
					StartActivityForResult(i, REQUEST_SHOW_PTCHART);
				}
			} else if (sensorProperty is SuperheatSubcoolSensorProperty) {
				var i = new Intent(Activity, typeof(SuperheatSubcoolActivity));
				i.SetAction(Intent.ActionPick);
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_WORKBENCH_MANIFOLD, workbench.IndexOf(manifold));
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_NAME, manifold.ptChart.fluid.name);
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_STATE, (int)manifold.ptChart.state);
				if (ion is RemoteION) {
					StartActivity(i);
				} else {
					StartActivityForResult(i, REQUEST_SHOW_SUPERHEAT_SUBCOOL);
				}
			} else if (sensorProperty is RateOfChangeSensorProperty) {
        var ps = manifold.primarySensor as GaugeDeviceSensor;
        if (ps != null && !ps.device.isConnected) {
          Toast.MakeText(Activity, Resource.String.devices_error_connect_for_roc, ToastLength.Long).Show();
        } else {
          var i = new Intent(Activity, typeof(RoCActivity));
          i.PutExtra(RoCActivity.EXTRA_MANIFOLD, new WorkbenchManifoldParcelable(workbench.IndexOf(manifold)));
          StartActivity(i);
        }
			}
		}

    /// <summary>
    /// Shows a context dialog for a manifold. This will present all the options that are available for
    /// the manifold.
    /// </summary>
    private void ShowManifoldContextDialog(Manifold manifold) {
			if (!workbench.isEditable) {
				return;
			}

      new ManifoldContextDialog.Builder(Activity, ion, manifold)
                               .AddCustomAction(Resource.String.workbench_remove, () => { ion.currentWorkbench.Remove(manifold); })
                               .Build().Show();
    }

		private void ShowChangeUnitDialog(GaugeDeviceSensor sensor) {
			var ldb = new ListDialogBuilder(Activity);
			ldb.SetTitle(Resource.String.pick_unit);

			foreach (var unit in sensor.supportedUnits) {
				if (!unit.Equals(sensor.unit)) {
					ldb.AddItem(unit.ToString(), () => {
						var device = sensor.device;
						var p = device.protocol as IGaugeProtocol;
						device.connection.Write(p.CreateSetUnitCommand(device.IndexOfSensor(sensor) + 1, sensor.type, unit));
					});
				}
			}

			ldb.Show();
		}
  }
}

