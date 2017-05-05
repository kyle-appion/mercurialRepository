namespace ION.Droid.Fragments._Analyzer {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Views;
	using Android.Widget;

	using L = Appion.Commons.Util.Log;

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

  public class AnalyzerFragment : IONFragment {
    /// <summary>
    /// The mask that is used to check request values.
    /// </summary>
    private const int MASK_REQUEST = unchecked((int)0xff000000);
    /// <summary>
    /// The mask that is used to chack payload values.
    /// </summary>
    private const int MASK_REQUEST_PAYLOAD = unchecked((int)0x000000ff);
    /// <summary>
    /// The mask that will get the lower nibble of the second least significant byte.
    /// </summary>
    private const int MASK_SIDE = unchecked((int)0x00000f00);
    /// <summary>
    /// The constant value indicating that the fragment requested a sensor for a sensor mount. The first byte of this
    /// request will be the index of the sensor.
    /// </summary>
    private const int REQUEST_SENSOR_MOUNT_SENSOR = unchecked((int)0x01000000);
    private const int REQUEST_SHOW_PTCHART = unchecked((int)0x02000000);
    private const int REQUEST_SHOW_SUPERHEAT_SUBCOOL = unchecked((int)0x03000000);
    private const int REQUEST_MANIFOLD_ON_SIDE = unchecked((int)0x04000000);
    /// <summary>
    /// The constant that indicates a low side manifold.
    /// </summary>
    private const int LOW_SIDE_MANIFOLD = -1;
    /// <summary>
    /// The constant that indicates a high side manifold.
    /// </summary>
    private const int HIGH_SIDE_MANIFOLD = -2;

    /// <summary>
    /// The view that will display the application's analyzer.
    /// </summary>
    private AnalyzerView analyzerView;
    /// <summary>
    /// The analyzer that is being displayed.
    /// </summary>
    private Analyzer analyzer;

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
      analyzerView = new AnalyzerView(inflater.Context);

      analyzerView.onSensorMountClicked += OnSensorMountClicked;
      analyzerView.onSensorMountLongClicked += OnSensorMountLongClicked;
      analyzerView.onManifoldClicked += OnManifoldClicked;
      analyzerView.onSensorPropertyClicked += OnSensorPropertyClicked;

      return analyzerView;
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

      analyzer = ion.currentAnalyzer;
      if (analyzer == null) {
        analyzer = new Analyzer(ion);
      }
      analyzerView.analyzer = analyzer;
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
      if (Result.Ok != resultCode) {
        return;
      }

      var request = requestCode & MASK_REQUEST;

      switch (request) {
				case REQUEST_SENSOR_MOUNT_SENSOR: {
						var index = requestCode & MASK_REQUEST_PAYLOAD;
						var sp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
						analyzer.PutSensor(index, sp.Get(ion));
				} break;

				case REQUEST_SHOW_SUPERHEAT_SUBCOOL: {
					var side = (Analyzer.ESide)unchecked((requestCode & MASK_SIDE) >> 8);

					var manifold = analyzer.GetManifoldFromSide(side);

					if (analyzer.HasSensor(manifold.secondarySensor)) {
						// TODO ahodder@appioninc.com: Is this right?
						Analyzer.ESide ss;
						if (!analyzer.GetSideOfSensor(manifold.secondarySensor, out ss)) {
							Appion.Commons.Util.Log.E(this, "Failed to get side of sensor");
							return;
						}

						// The user returned from the SHSC activity with a sensor.
						if (ss != side) {
							// The sensor that the user returned with is not on the side of the manifold.
							if (analyzer.IsSideFull(side)) {
								// We cannot swap the side of the sensor; the side is full.
								L.E(this, "Tried to add sensor from opposite side to this side.");
								Toast.MakeText(Activity, Resource.String.analyzer_error_failed_to_link_sensors, ToastLength.Long).Show();
								manifold.SetSecondarySensor(null);
							} else {
								analyzer.SwapSensors(analyzer.IndexOfSensor(manifold.secondarySensor), analyzer.NextEmptySensorIndex(side), true);
							}
						} else if (!analyzer.HasSensor(manifold.secondarySensor) && analyzer.IsSideFull(side)) {
							Toast.MakeText(Activity, string.Format(GetString(Resource.String.analyzer_side_full_1sarg), side), ToastLength.Long).Show();
							manifold.SetSecondarySensor(null);
						} else {
							if (!analyzer.IsSensorOnSide(manifold.secondarySensor, side)) {
								var si = analyzer.IndexOfSensor(manifold.secondarySensor);
								var di = analyzer.NextEmptySensorIndex(side);
//								analyzerView.SwapSensorMounts(di, si);
								analyzer.SwapSensors(di, si, true);

							}
						}
					} else {
						analyzer.AddSensorToSide(side, manifold.secondarySensor);
					}
				} break;

				case REQUEST_MANIFOLD_ON_SIDE: {
          var mside = (Analyzer.ESide)unchecked((requestCode & MASK_SIDE) >> 8);
          var msp = data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR) as SensorParcelable;
          var s = msp.Get(ion);

          TrySetManifold(mside, s);
				} break;
        default:
          L.D(this, "Unknown request: " + request);
          break;
      }
    }

		public override void OnResume() {
			base.OnResume();
			analyzerView.analyzer = analyzer;
			analyzer.onAnalyzerEvent += OnAnalyzerEvent;
		}

		public override void OnPause() {
			base.OnPause();
			analyzer.onAnalyzerEvent -= OnAnalyzerEvent;
		}

    /// <summary>
    /// Creates an encoded sensor mount request value.
    /// </summary>
    /// <returns>The sensor mount request.</returns>
    /// <param name="sensorMountIndex">Sensor mount index.</param>
    private int EncodeSensorMountRequest(int sensorMountIndex) {
      return REQUEST_SENSOR_MOUNT_SENSOR | (MASK_REQUEST_PAYLOAD & sensorMountIndex);
    }

    /// <summary>
    /// Creates an encoded superheat subcool request value.
    /// </summary>
    /// <returns>The superheat subcool request.</returns>
    /// <param name="side">Side.</param>
    private int EncodeSuperheatSubcoolRequest(Analyzer.ESide side) {
      return REQUEST_SHOW_SUPERHEAT_SUBCOOL | (MASK_SIDE & ((int)side << 8));
    }

    /// <summary>
    /// Creates an encoded manifold sensor request value.
    /// </summary>
    /// <returns>The manifold side request.</returns>
    /// <param name="side">Side.</param>
    private int EncodeManifoldSideRequest(Analyzer.ESide side) {
      return REQUEST_MANIFOLD_ON_SIDE | (MASK_SIDE & ((int)side << 8));
    }

    /// <summary>
    /// Attempts to set the manifold at the given side.
    /// </summary>
    /// <returns><c>true</c>, if set manifold was tryed, <c>false</c> otherwise.</returns>
    /// <param name="side">Side.</param>
    /// <param name="sensor">Sensor.</param>
    private bool TrySetManifold(Analyzer.ESide side, Sensor sensor) {
			if (analyzer.HasSensor(sensor)) { // The analyzer already contains the sensor. We will need to ensure that the integrity of the analyzer is not broken
				if (analyzer.IsSensorOnSide(sensor, side)) { // The sensor is already on the propert side, we can set the manifold.
					analyzer.SetManifold(side, sensor);
				} else { 
					if (analyzer.IsSideFull(side)) { // We cannot move the sensor to the proper side. Toast a message
						Toast.MakeText(Activity, string.Format(GetString(Resource.String.analyzer_cannot_set_manifold_side_full_1sarg), side.ToLocalizedString(Activity)), ToastLength.Long).Show();
					} else { // Perform a sensor swap and set the manifold
						analyzer.SwapSensors(analyzer.IndexOfSensor(sensor), analyzer.NextEmptySensorIndex(side), true);
						analyzer.SetManifold(side, sensor);
					}
				}
			} else { // The analyzer does not contain the sensor 
				if (analyzer.IsSideFull(side)) {
					Toast.MakeText(Activity, string.Format(GetString(Resource.String.analyzer_cannot_set_manifold_side_full_1sarg), side.ToLocalizedString(Activity)), ToastLength.Long).Show();
				} else {
					analyzer.AddSensorToSide(side, sensor);
					analyzer.SetManifold(side, sensor);
				}
			}

			return true;
    }

    /// <summary>
    /// Shows a context dialog for a manifold. This will present all the options that are available for
    /// the manifold.
    /// </summary>
    private void ShowManifoldContextDialog(Manifold manifold) {
      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(string.Format(GetString(Resource.String.devices_actions_1arg), manifold.primarySensor.name));

      var dgs = manifold.primarySensor as GaugeDeviceSensor;

      if (dgs != null && !dgs.device.isConnected) {
        ldb.AddItem(Resource.String.reconnect, () => {
          dgs.device.connection.Connect();
        });
      }

      ldb.AddItem(Resource.String.rename, () => {
				ldb.AddItem(Resource.String.rename, () => {
					if (manifold.primarySensor is GaugeDeviceSensor) {
						var gds = manifold.primarySensor as GaugeDeviceSensor;
						new RenameDialog(gds.device).Show(Activity);
					} else {
						new RenameDialog(manifold.primarySensor).Show(Activity);
					}
				});
      });

			if (dgs != null && dgs.device.isConnected) {
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

      ldb.AddItem(Resource.String.remove, () => {
        analyzer.RemoveManifold(manifold);
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

			if (manifold.secondarySensor != null) {
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
          manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_roc, Resource.String.workbench_roc_abrv), () => {
					manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_min, Resource.String.workbench_min_abrv), () => {
          manifold.AddSensorProperty(new MinSensorProperty(manifold));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_max, Resource.String.workbench_max_abrv), () => {
          manifold.AddSensorProperty(new MaxSensorProperty(manifold));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_hold, Resource.String.workbench_hold_abrv), () => {
          manifold.AddSensorProperty(new HoldSensorProperty(manifold));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_timer, Resource.String.workbench_timer_abrv), () => {
          manifold.AddSensorProperty(new TimerSensorProperty(manifold));
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
      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        manifold.AddSensorProperty(new MinSensorProperty(manifold));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        manifold.AddSensorProperty(new MaxSensorProperty(manifold));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
        manifold.AddSensorProperty(new HoldSensorProperty(manifold));
      }

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        manifold.AddSensorProperty(new TimerSensorProperty(manifold));
      }

      if (ESensorType.Pressure == manifold.primarySensor.type || ESensorType.Temperature == manifold.primarySensor.type) {
        if (!manifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))) {
          manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
        }

        if (!manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))) {
          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
        }
      }
    }

    /// <summary>
    /// Shows a dialog that will add a sensor to a sensor mount.
    /// </summary>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void ShowAddFromDialog(Analyzer analyzer, int index) {
      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(Resource.String.analyzer_add_from);
      ldb.AddItem(Resource.String.device_manager, () => {
        var i = new Intent(Activity, typeof(DeviceManagerActivity));
        i.SetAction(Intent.ActionPick);
        StartActivityForResult(i, EncodeSensorMountRequest(index));
      });
			ldb.AddItem(Resource.String.sensor_create_manual_entry, () => {
				var d = new ManualSensorCreateDialog(Activity, SensorUtils.GetSensorTypeUnitMapping()).Show((sensor) => {
					analyzer.PutSensor(index, sensor, false);
				});
			});
      ldb.Show();
    }

    /// <summary>
    /// Shows a dialog that will add a sensor to a manifold of the given side.
    /// </summary>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void ShowAddFromDialog(Analyzer analyzer, Analyzer.ESide side) {
			var ldb = new ListDialogBuilder(Activity);
			ldb.SetTitle(Resource.String.analyzer_add_from);
			ldb.AddItem(Resource.String.device_manager, () => {
				var i = new Intent(Activity, typeof(DeviceManagerActivity));
				i.SetAction(Intent.ActionPick);
				StartActivityForResult(i, EncodeManifoldSideRequest(side));
			});
			ldb.AddItem(Resource.String.sensor_create_manual_entry, () => {
				if (analyzer.IsSideFull(side)) {
					Toast.MakeText(Activity, string.Format(GetString(Resource.String.analyzer_cannot_set_manifold_side_full_1sarg), side.ToLocalizedString(Activity)), ToastLength.Long).Show();
				} else {
					var d = new ManualSensorCreateDialog(Activity, SensorUtils.GetSensorTypeUnitMapping()).Show((sensor) => {
						analyzer.PutSensor(analyzer.NextEmptySensorIndex(side), sensor, false);
						analyzer.SetManifold(side, sensor);
					});
				}
			});
			ldb.Show();
    }

		private void OnAnalyzerEvent(AnalyzerEvent ae) {
			switch (ae.type) {
				case AnalyzerEvent.EType.Added:
					ion.SaveAnalyzerAsync();
					break;
				case AnalyzerEvent.EType.ManifoldAdded:
					ion.SaveAnalyzerAsync();
					break;
				case AnalyzerEvent.EType.ManifoldRemoved:
					ion.SaveAnalyzerAsync();
					break;
				case AnalyzerEvent.EType.Removed:
					ion.SaveAnalyzerAsync();
					break;
				case AnalyzerEvent.EType.Swapped:
					ion.SaveAnalyzerAsync();
					break;
			}
		}

    /// <summary>
    /// Called when a sensor mount is clicked in the analyzer.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void OnSensorMountClicked(AnalyzerView view, Analyzer analyzer, int index) {
      if (analyzer.HasSensorAt(index)) {
        var side = Analyzer.ESide.Low;
        analyzer.GetSideOfIndex(index, out side);
        new ViewerDialog(this.Activity, analyzer, analyzer[index], side).Show();
      } else {
				if (!analyzer.isEditable) {
					return;
				}
        ShowAddFromDialog(analyzer, index);
      }
    }

    /// <summary>
    /// Called when a sensor mount is clicked in the analyzer.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void OnSensorMountLongClicked(AnalyzerView view, Analyzer analyzer, int index) {
			if (!analyzer.isEditable) {
				return;
			}
      if (analyzer.HasSensorAt(index)) {
        analyzerView.StartDraggingSensorMount(index);
      } else {
        ShowAddFromDialog(analyzer, index);
      }
      L.D(this, "Analyzer view callback sensor mount long clicked at index: " + index);
    }

    /// <summary>
    /// Called when a manifold is clicked in the analyzer view.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="side">Side.</param>
    private void OnManifoldClicked(AnalyzerView view, Analyzer analyzer, Analyzer.ESide side) {
			if (!analyzer.isEditable) {
				return;
			}

      var manifold = analyzer.GetManifoldFromSide(side);

      if (manifold != null) {
        ShowManifoldContextDialog(manifold);
      } else {
        ShowAddFromDialog(analyzer, side);
      }
    }

    /// <summary>
    /// Called when a sensor property is clicked in the analyzer view. Note: either the low or high side may be clicked
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    /// <param name="sensorProperty">Sensor property.</param>
    private void OnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty) {
      var sensor = sensorProperty.sensor;

      if (sensorProperty is AlternateUnitSensorProperty) {
        var asp = sensorProperty as AlternateUnitSensorProperty;
        UnitDialog.Create(Activity, sensor.supportedUnits, (obj, u) => {
          asp.unit = u;
        }).Show();
      } else if (sensorProperty is PTChartSensorProperty) {
				ViewInPtChartActivity(manifold, sensorProperty);
      } else if (sensorProperty is SuperheatSubcoolSensorProperty) {
        ViewInSuperheatSubcoolActivity(manifold, sensorProperty);
      }
    }

		private void ViewInPtChartActivity(Manifold manifold, ISensorProperty sensorProperty) {
			var side = Analyzer.ESide.Low;

			if (!analyzer.GetSideOfManifold(manifold, out side)) {
				Error(GetString(Resource.String.analyzer_error_failed_to_launch_ptchart_missing_manifold));
				return;
			}

			var i = new Intent(Activity, typeof(PTChartActivity));
			i.SetAction(Intent.ActionPick);
			i.PutExtra(PTChartActivity.EXTRA_LOCK_FLUID, true);
			i.PutExtra(PTChartActivity.EXTRA_FLUID_NAME, manifold.ptChart.fluid.name);
			i.PutExtra(PTChartActivity.EXTRA_FLUID_STATE, (int)analyzer.SideAsFluidState(side));
			i.PutExtra(PTChartActivity.EXTRA_ANALYZER_MANIFOLD, (int)side);
			if (ion is RemoteION) {
				StartActivity(i);
			} else {
				StartActivityForResult(i, REQUEST_SHOW_PTCHART);
			}
		}

    /// <summary>
    /// Views the in superheat subcool activity.
    /// </summary>
    /// <param name="">.</param>
    private void ViewInSuperheatSubcoolActivity(Manifold manifold, ISensorProperty sensorProperty) {
      var side = Analyzer.ESide.Low;

      if (!analyzer.GetSideOfManifold(manifold, out side)) {
				Error(GetString(Resource.String.analyzer_error_failed_to_launch_shsc_missing_manifold));
        return;
      }

      // If the manifold does not have a secondary sensor, then we will need to query whether or not the analyzer has
      // space to accept the returned sensor. If it does not, then we cannot open the activity safely.
      if (manifold.secondarySensor == null && !analyzer.CanAddSensorToSide(side)) {
        var adb = new IONAlertDialog(Activity, Resource.String.error);
				adb.SetMessage(string.Format(GetString(Resource.String.analyzer_error_failed_to_launch_shsc_analyzer_full_1sarg), side.ToLocalizedString(Activity)));

        adb.SetNegativeButton(Resource.String.ok, (obj, args) => {
          var dialog = obj as Android.App.Dialog;
          dialog.Dismiss();
        });

        adb.Show();
        return;
      }

      var sensor = sensorProperty.sensor;
      var i = new Intent(Activity, typeof(SuperheatSubcoolActivity));
      i.SetAction(Intent.ActionPick);
      i.PutExtra(SuperheatSubcoolActivity.EXTRA_LOCK_FLUID, true);
      i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_NAME, manifold.ptChart.fluid.name);
      i.PutExtra(SuperheatSubcoolActivity.EXTRA_FLUID_STATE, (int)analyzer.SideAsFluidState(side));

      switch (sensor.type) {
        case ESensorType.Pressure:
					i.PutExtra(SuperheatSubcoolActivity.EXTRA_ANALYZER_MANIFOLD, (int)side);
					if (ion is RemoteION) {
						StartActivity(i);
					} else {
          	StartActivityForResult(i, EncodeSuperheatSubcoolRequest(side));
					}
          break;
        case ESensorType.Temperature:
					i.PutExtra(SuperheatSubcoolActivity.EXTRA_ANALYZER_MANIFOLD, (int)side);
					if (ion is RemoteION) {
						StartActivity(i);
					} else {
          	StartActivityForResult(i, EncodeSuperheatSubcoolRequest(side));
					}
          break;
        default:
					var msg = string.Format(GetString(Resource.String.analyzer_error_invalid_sensor_type), sensor.type.GetTypeString());
          L.E(this, msg);
          Alert(msg);
          break;
      }
    }
  }

	internal static class AnalyzerExtensions {
		public static string ToLocalizedString(this Analyzer.ESide side, Context context) {
			switch (side) {
				case Analyzer.ESide.Low:
					return context.GetString(Resource.String.analyzer_side_low);
				case Analyzer.ESide.High:
					return context.GetString(Resource.String.analyzer_side_high);
				default:
					return context.GetString(Resource.String.unknown);
			}
		}
	}
}

