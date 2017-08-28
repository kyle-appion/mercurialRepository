namespace ION.Droid.Fragments._Workbench {

	using System;

	using Android.Content;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Connections;
	using ION.Core.Content;
	using ION.Core.Devices;

	using ION.Droid.Devices;
	using ION.Droid.Util;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class ManifoldRecord : RecordAdapter.IRecord {
		// Implemented from Record.Adapter.IRecord
		public int viewType { get { return (int)WorkbenchAdapter.EViewType.Manifold; } }

		public Manifold manifold { get; set; }
		public bool isExpanded { get; set; }

		public ManifoldRecord(Manifold manifold) {
			this.manifold = manifold;
      this.isExpanded = false;
		}
	}

	public class ManifoldViewHolder : SwipeRecyclerView.ViewHolder {
		public ManifoldRecord record {
			get {
				return __record;
			}
			set {
				Unbind();
				__record = value;
				if (__record != null) {
					__record.manifold.onManifoldEvent += OnManifoldEvent;
					Invalidate();
				}
			}
		} ManifoldRecord __record;

		public Action<ManifoldRecord> onSerialNumberClicked;

		private BitmapCache cache;

		private TextView name;
		private TextView status;
		private TextView measurement;
		private TextView unit;

    private View alarm;

		private ImageView battery;
		private ImageView connection;
		private ImageView icon;

		private ProgressBar progress;

		private int lastBattery;

		public ManifoldViewHolder(SwipeRecyclerView parent, BitmapCache cache, Workbench workbench) : base(parent, Resource.Layout.list_item_viewer, Resource.Layout.list_item_button) {
			this.cache = cache;

      name = foreground.FindViewById<TextView>(Resource.Id.name);
			status = foreground.FindViewById<TextView>(Resource.Id.status);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
      measurement.Tag = new Java.Lang.String("Measurement");
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);

      alarm = foreground.FindViewById(Resource.Id.alarm);

			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			battery = foreground.FindViewById<ImageView>(Resource.Id.battery);
			connection = foreground.FindViewById<ImageView>(Resource.Id.connection);

			progress = foreground.FindViewById<ProgressBar>(Resource.Id.progress);

			lastBattery = -1;

			var button = background as TextView;
			button.SetText(Resource.String.remove);
			button.SetOnClickListener(new ViewClickAction((view) => {
				workbench.Remove(record.manifold);
			}));
		}

		public override void Unbind() {
			if (record != null) {
				record.manifold.onManifoldEvent -= OnManifoldEvent;
			}
		}

		/// <summary>
		/// Invalidates the view within the template.
		/// </summary>
		public void Invalidate() {
			if (record.manifold == null) {
				// TODO ahodder@appioninc.com: Implement a real invalidate for a null manifold.
				return;
			}
			var c = foreground.Context;
			var s = record.manifold.primarySensor;

			measurement.Text = s.ToFormattedString(false);
			unit.Text = s.unit.ToString();

			var gds = s as GaugeDeviceSensor;
			var d = gds?.device;

			var ion = AppState.context;
			if (ion.alarmManager.HostHasEnabledAlarms(s)) {
				alarm.Visibility = ViewStates.Visible;
			} else {
				alarm.Visibility = ViewStates.Invisible;
			}

			if (d != null) {
				name.Text = d.serialNumber.deviceModel.GetTypeString() + ": " + s.name;

				progress.Visibility = ViewStates.Invisible;
				connection.Visibility = ViewStates.Visible;
				switch (d.connection.connectionState) {
          case EConnectionState.Connected: {
            measurement.SetTextColor(Resource.Color.black.AsResourceColor(c));
						unit.SetTextColor(Resource.Color.black.AsResourceColor(c));

						connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_connected));
						status.Text = c.GetString(Resource.String.connected);
						status.SetTextColor(Resource.Color.green.AsResourceColor(c));
					  break;
          } // EConnectionState.Connected
          case EConnectionState.Broadcasting: {
            measurement.SetTextColor(Resource.Color.black.AsResourceColor(c));

            connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
            status.Text = c.GetString(Resource.String.long_range_mode);
						status.SetTextColor(Resource.Color.light_blue.AsResourceColor(c));
					  break;
          } // EConnectionState.Broadcasting
          case EConnectionState.Disconnected: {
						measurement.SetTextColor(Resource.Color.gray.AsResourceColor(c));
						unit.SetTextColor(Resource.Color.gray.AsResourceColor(c));

						connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
						status.Text = c.GetString(Resource.String.disconnected);
						status.SetTextColor(Resource.Color.red.AsResourceColor(c));
					  break;
          } // EConnectionState.Disconnected
          case EConnectionState.Connecting: {
            goto case EConnectionState.Resolving;
          } // EConnectionState.Connecting:
          case EConnectionState.Resolving: {
						progress.Visibility = ViewStates.Visible;
						connection.Visibility = ViewStates.Invisible;
					  break;
          } // EConnectionState.Resolving
				}

				icon.SetImageBitmap(cache.GetBitmap(d.GetDeviceIcon()));

				status.Visibility = ViewStates.Visible;
				icon.Visibility = ViewStates.Visible;
			} else {
				name.Text = s.type.GetSensorTypeName() + ": " + s.name;

				status.Visibility = ViewStates.Invisible;
				connection.Visibility = ViewStates.Invisible;
				icon.Visibility = ViewStates.Invisible;
				progress.Visibility = ViewStates.Invisible;
			}

			InvalidateBattery(d);
		}

		private void InvalidateBattery(GaugeDevice device) {
			if (device != null) {
				var bat = device.battery;
        if (!device.isConnected) {
          lastBattery = -1;
          battery.Visibility = ViewStates.Invisible;
        } else if (bat != lastBattery) {
					battery.Visibility = ViewStates.Visible;
					if (bat >= 100) {
						battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_100));
					} else if (bat >= 75) {
						battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_75));
					} else if (bat >= 50) {
						battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_50));
					} else if (bat >= 25) {
						battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_25));
					} else {
						battery.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_battery_horiz_empty));
					}
          lastBattery = bat;
				}
			} else {
				battery.Visibility = ViewStates.Invisible;
			}
		}

		private void OnManifoldEvent(ManifoldEvent e) {
			switch (e.type) {
				case ManifoldEvent.EType.Invalidated:
					Invalidate();
					break;
				case ManifoldEvent.EType.SensorPropertyAdded:
				case ManifoldEvent.EType.SensorPropertyRemoved:
					break;
			}
		}
	}
}
