namespace ION.Droid.Fragments._Workbench {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;

	using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  // Using ION.Droid
  using Activity;
  using Activity.DeviceManager;
	using App;
  using Dialog;
  using Sensors;

  public class WorkbenchFragment : IONFragment {

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

		/// <summary>
		/// Whether or not the workbench is editable.
		/// </summary>
		/// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
		public bool isEditable { get { return ion is AndroidION; } }

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
      AddFlags(EFlags.AllowScreenshot | EFlags.StartRecording);

#if DEBUG
			if (ion == null) {
				Log.E(this, "ION was null at WorkbenchFragment.OnActivityCreated");
				StartActivity(new Intent(Activity, typeof(MainActivity)));
				Activity.Finish();
				return;
			}
#endif
		}


		// Overridden from Fragment
    public override void OnResume() {
      base.OnResume();

			if (workbench == null) {
				workbench = ion.currentWorkbench;
			}

			if (workbench == null || workbench != ion.currentWorkbench) {
				workbench = ion.currentWorkbench = new Workbench(ion);
				Log.E(this, "Failed to load previous workbench. Defaulting to a new empty one");
			}
			workbench.onWorkbenchEvent += OnWorkbenchEvent;

			adapter = new WorkbenchAdapter(OnAddViewer, isEditable);
			adapter.workbench = ion.currentWorkbench;
			list.SetAdapter(adapter);

      adapter.NotifyDataSetChanged();
			adapter.onSensorPropertyClicked += OnOnSensorPropertyClicked;
			adapter.onManifoldClicked += OnManifoldClicked;
    }

		// Overridden from Fragment
		public override void OnPause() {
			base.OnPause();

			workbench.onWorkbenchEvent -= OnWorkbenchEvent;
			adapter.onSensorPropertyClicked -= OnOnSensorPropertyClicked;
			adapter.onManifoldClicked -= OnManifoldClicked;
		}

		// Overridden from Fragment
    public override void OnDestroy() {
      base.OnDestroy();
      if (workbench != null) {
        workbench.onWorkbenchEvent -= OnWorkbenchEvent;
      }
    }

		// Overridden from Fragment
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
      var i = new Intent(Activity, typeof(DeviceManagerActivity));
			i.SetAction(Intent.ActionPick);
			StartActivityForResult(i, REQUEST_SENSOR);
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
				StartActivityForResult(i, REQUEST_SHOW_PTCHART);
			} else if (sensorProperty is SuperheatSubcoolSensorProperty) {
				var i = new Intent(Activity, typeof(SuperheatSubcoolActivity));
				i.SetAction(Intent.ActionPick);
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_WORKBENCH_MANIFOLD, workbench.IndexOf(manifold));
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_NAME, manifold.ptChart.fluid.name);
				i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_STATE, (int)manifold.ptChart.state);
				StartActivityForResult(i, REQUEST_SHOW_SUPERHEAT_SUBCOOL);
			}
		}

    /// <summary>
    /// Called when a manifold event is thrown by the manifold.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    private void OnWorkbenchEvent(WorkbenchEvent workbenchEvent) {
      if (IsVisible) {
        switch (workbenchEvent.type) {
          case WorkbenchEvent.EType.Added:
            goto case WorkbenchEvent.EType.Swapped;
          case WorkbenchEvent.EType.Removed:
            goto case WorkbenchEvent.EType.Swapped;
          case WorkbenchEvent.EType.Swapped:
            ion.SaveWorkbenchAsync();
            break;
          case WorkbenchEvent.EType.ManifoldEvent:
						OnManifoldEvent(workbenchEvent.manifoldEvent);
            break;
        }
      }
    }

		private void OnManifoldEvent(ManifoldEvent manifoldEvent) {
			switch (manifoldEvent.type) {
				case ManifoldEvent.EType.SensorPropertyAdded:
//					adapter.UpdateManifoldSubview(manifoldEvent.manifold, Math.Max(manifoldEvent.index - 1, 0));
					goto case ManifoldEvent.EType.SensorPropertySwapped;
				case ManifoldEvent.EType.SensorPropertyRemoved:
//					adapter.UpdateManifoldSubview(manifoldEvent.manifold, manifoldEvent.manifold.sensorPropertyCount - 1);
					goto case ManifoldEvent.EType.SensorPropertySwapped;
				case ManifoldEvent.EType.SensorPropertySwapped:
					ion.SaveWorkbenchAsync();
				break;
			}
		}

		private void OnWorkbenchChanged(Workbench wb) {
			if (this.workbench != null) {
				this.workbench.onWorkbenchEvent -= OnWorkbenchEvent;
			}

			this.workbench = wb;

			if (this.workbench != null) {
				this.workbench.onWorkbenchEvent += OnWorkbenchEvent;
				this.adapter.workbench = wb;
			}
		}

    /// <summary>
    /// Shows a context dialog for a manifold. This will present all the options that are available for
    /// the manifold.
    /// </summary>
    private void ShowManifoldContextDialog(Manifold manifold) {
      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(string.Format(GetString(Resource.String.devices_actions_1arg), manifold.primarySensor.name));

      var dgs = manifold.primarySensor as GaugeDeviceSensor;

			if (dgs != null) {
				if (!dgs.device.isConnected) {
					ldb.AddItem(Resource.String.reconnect, () => {
						dgs.device.connection.ConnectAsync();
					});
				}
			}

      ldb.AddItem(Resource.String.rename, () => {
        new RenameDialog(manifold.primarySensor).Show(Activity);
      });

			if (dgs.device.isConnected) {
				ldb.AddItem(GetString(Resource.String.remote_change_unit), () => {
					var device = dgs.device;

					if (device.sensorCount > 1) {
						var d = new ListDialogBuilder(Activity);
						d.SetTitle(Resource.String.select_a_sensor);

						for (int i = 0; i < device.sensorCount; i++) {
							var sensor = device[i];
							d.AddItem(i + ": " + sensor.type.GetTypeString(), () => {
								ShowChangeUnitDialog(sensor);
							});
						}

						d.Show();
					} else {
						ShowChangeUnitDialog(device.sensors[0]);
					}
				});
			}

      ldb.AddItem(Resource.String.workbench_add_viewer_sub, () => {
        ShowAddSubviewDialog(manifold);
      });

      ldb.AddItem(Resource.String.alarm, () => {
        var i = new Intent(Activity, typeof(SensorAlarmActivity));
        i.PutExtra(SensorAlarmActivity.EXTRA_SENSOR, manifold.primarySensor.ToParcelable());
        StartActivity(i);
      });

      if (dgs != null && dgs.device.isConnected) {
        ldb.AddItem(Resource.String.disconnect, () => {
          dgs.device.connection.Disconnect();
        });
      }

      ldb.AddItem(Resource.String.workbench_remove, () => {
        workbench.Remove(manifold);
      });

      ldb.Show();
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

    /// <summary>
    /// Shows the add subview dialog.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void ShowAddSubviewDialog(Manifold manifold) {
      Func<int, int, string> format = delegate (int full, int abrv) {
        return GetString(full) + " (" + GetString(abrv) + ")";
      };

      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(GetString(Resource.String.manifold_add_subview));
			ldb.SetTitle(Resource.String.pick_unit);

			if ((manifold.primarySensor.type == ESensorType.Pressure ||
			    manifold.primarySensor.type == ESensorType.Temperature) && manifold.secondarySensor != null) {
				if (!manifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
					var t = manifold.secondarySensor.type;
					var type = t.GetTypeString();
					var abrv = t.GetTypeAbreviationString();
					ldb.AddItem(String.Format(GetString(Resource.String.workbench_linked_sensor_2sarg), type, abrv), () => {
						manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
					});
				}
			}

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

			ldb.AddItem(Resource.String.workbench_add_all, () => {
        AddAllSubviews(manifold);
      });

      ldb.Show();
    }

    /// <summary>
    /// Attempts to add all of the subviews to the manifold, as long as they aren't already present.
    /// </summary>
    private void AddAllSubviews(Manifold manifold) {
			if (manifold.primarySensor.type == ESensorType.Pressure || manifold.primarySensor.type == ESensorType.Temperature) {
				if (!manifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
					manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
				}
			}

      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold.primarySensor));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold.primarySensor));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        manifold.AddSensorProperty(new MaxSensorProperty(manifold.primarySensor));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
        manifold.AddSensorProperty(new HoldSensorProperty(manifold.primarySensor));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        manifold.AddSensorProperty(new TimerSensorProperty(manifold.primarySensor));
      }

      if (ESensorType.Pressure == manifold.primarySensor.type || ESensorType.Temperature == manifold.primarySensor.type) {
        if (!manifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))) {
          manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
        }

        if (!manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))) {
          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
        }
      }
//			adapter.ExpandManifold(manifold);
    }
  }
}

