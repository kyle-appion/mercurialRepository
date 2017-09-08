namespace ION.Droid.Dialog {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;


  using ION.Droid.Sensors;
  
	public class ManifoldContextDialog {

    private Builder builder;
    private Context context { get { return builder.context; } }
    private Manifold manifold { get { return builder.manifold; } }

    private List<Action> actions = new List<Action>();

    private ManifoldContextDialog(Builder builder) {
      this.builder = builder;
    }

    public AlertDialog Show() {
      var adb = new ListDialogBuilder(builder.context);
      adb.SetTitle(builder.manifold.primarySensor.name + " " + builder.context.GetString(Resource.String.options));

      BuildActionList(adb);


      return adb.Show();
    }

    private void BuildActionList(ListDialogBuilder adb) {
      AddConnectOptions(adb);
      AddSubviewOptions(adb);
      AddAlarmOptions(adb);
      AddCustomOptions(adb);
    }

    private void AddConnectOptions(ListDialogBuilder adb) {
			if (builder.showConnectOptions) {
				var gds = manifold.primarySensor as GaugeDeviceSensor;
				if (gds != null) {
          if (gds.device.connection.connectionState == EConnectionState.Disconnected) {
            adb.AddItem(Resource.String.reconnect, () => {
      				gds.device.connection.Connect();
						});
					} else {
						adb.AddItem(Resource.String.disconnect, () => {
							gds.device.connection.Disconnect();
						});
					}
				}
			}
    }

    private void AddSubviewOptions(ListDialogBuilder adb) {
      if (builder.showSubviewOptions) {
        adb.AddItem(Resource.String.manifold_calculation_subviews, () => {
          ShowSubviewDialog();
        });
      }
    }

    private void AddAlarmOptions(ListDialogBuilder adb) {
      if (builder.showAlarmsOptions) {
        adb.AddItem(Resource.String.alarm, () => {
          Toast.MakeText(builder.context, "Showing alarms is fun", ToastLength.Long).Show();
        });
      }
    }

    private void AddCustomOptions(ListDialogBuilder adb) {
      foreach (var option in builder.customOptions) {
        adb.AddItem(option.title, option.action);
      }
    }

    private void ShowSubviewDialog() {
			Func<int, int, string> format = delegate (int full, int abrv) {
				return context.GetString(full) + " (" + context.GetString(abrv) + ")";
			};

			var ldb = new ListDialogBuilder(context);
			ldb.SetTitle(context.GetString(Resource.String.manifold_add_subview));
			ldb.SetTitle(Resource.String.pick_unit);

			if ((manifold.primarySensor.type == ESensorType.Pressure ||
			  manifold.primarySensor.type == ESensorType.Temperature) && manifold.secondarySensor != null) {
				if (!manifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
					var t = manifold.secondarySensor.type;
					var type = t.GetTypeString();
					var abrv = t.GetTypeAbreviationString();
					ldb.AddItem(String.Format(context.GetString(Resource.String.workbench_linked_sensor_2sarg), type, abrv), () => {
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
					manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold, builder.ion.preferences.device.trendInterval));
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
				AddAllSubviews();
			});

			ldb.Show();
    }

		/// <summary>
		/// Attempts to add all of the subviews to the manifold, as long as they aren't already present.
		/// </summary>
		private void AddAllSubviews() {
			if (manifold.primarySensor.type == ESensorType.Pressure || manifold.primarySensor.type == ESensorType.Temperature) {
				if (!manifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
					manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
				}
			}

			if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
				manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold));
			}

			if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
				manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold, builder.ion.preferences.device.trendInterval));
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
    /// The builder class that will allow customization of a ManifoldContextDialog.
    /// </summary>
    public class Builder {
      public Context context { get; private set; }
			public IION ion { get; private set; }
			public Manifold manifold { get; private set; }

      public bool showConnectOptions { get; set; }
      public bool showSubviewOptions { get; set; }
      public bool showAlarmsOptions { get; set; }
      internal List<Option> customOptions = new List<Option>();

      public Builder(Context context, IION ion, Manifold manifold) {
        this.context = context;
        this.ion = ion;
        this.manifold = manifold;

        showConnectOptions = true;
        showSubviewOptions = true;
        showAlarmsOptions = true;
      }

      /// <summary>
      /// Sets whether or not the dialog will show connect / disconnect (based on the primary device's current
      /// connection status) within the dialog.
      /// </summary>
      /// <returns>The connect options.</returns>
      /// <param name="showConnectOptions">If set to <c>true</c> show connect options.</param>
      public Builder SetConnectOptions(bool showConnectOptions) {
        this.showConnectOptions = showConnectOptions;
        return this;
      }

      /// <summary>
      /// Sets whether or not the dialog will show an option for to select 
      /// </summary>
      /// <returns>The subview options.</returns>
      /// <param name="showSubviewOptions">If set to <c>true</c> show subview options.</param>
      public Builder ShowSubviewOptions(bool showSubviewOptions) {
        this.showSubviewOptions = showSubviewOptions;
        return this;
      }

      /// <summary>
      /// Sets whether or not the dialog will show an option to show the alarm management for the manifold.
      /// </summary>
      /// <returns>The alarm options.</returns>
      /// <param name="showAlarmOptions">If set to <c>true</c> show alarm options.</param>
      public Builder ShowAlarmOptions(bool showAlarmOptions) {
        this.showAlarmsOptions = showAlarmOptions;
        return this;
      }

			/// <summary>
			/// Adds a custom action to the builder.
			/// </summary>
			/// <returns>The custom action.</returns>
			/// <param name="titleRes">Title.</param>
			/// <param name="action">Action.</param>
      public Builder AddCustomAction(int titleRes, Action action) {
        customOptions.Add(new Option(context.GetString(titleRes), action));
				return this;
			}

      /// <summary>
      /// Adds a custom action to the builder.
      /// </summary>
      /// <returns>The custom action.</returns>
      /// <param name="title">Title.</param>
      /// <param name="action">Action.</param>
      public Builder AddCustomAction(string title, Action action) {
        customOptions.Add(new Option(title, action));
        return this;
      }

      /// <summary>
      /// Build this instance.
      /// </summary>
      /// <returns>The build.</returns>
      public ManifoldContextDialog Build() {
        return new ManifoldContextDialog(this);
      }
    }

    internal class Option {
      public string title { get; }
      public Action action { get; }

      public Option(string res, Action action) {
        this.title = res;
        this.action = action;
      }
    }
  }
}
