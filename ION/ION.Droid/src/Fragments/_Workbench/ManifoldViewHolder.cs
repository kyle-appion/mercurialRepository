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

		private TextView title;
		private TextView measurement;
		private TextView alarm;
		private TextView status;
		private TextView unit;
		private TextView serialNumber;

		private ImageView battery;
		private ImageView connection;
		private ImageView icon;
		private ImageView arrow;

		private ProgressBar progress;

		private int lastBattery;

		public ManifoldViewHolder(SwipeRecyclerView parent, BitmapCache cache, Workbench workbench) : base(parent, Resource.Layout.viewer_large, Resource.Layout.list_item_button) {
			this.cache = cache;

			title = foreground.FindViewById<TextView>(Resource.Id.name);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
			alarm = foreground.FindViewById<TextView>(Resource.Id.alarm);
			status = foreground.FindViewById<TextView>(Resource.Id.status);
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);
			serialNumber = foreground.FindViewById<TextView>(Resource.Id.device_serial_number);

			battery = foreground.FindViewById<ImageView>(Resource.Id.battery);
			connection = foreground.FindViewById<ImageView>(Resource.Id.connection);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			arrow = foreground.FindViewById<ImageView>(Resource.Id.arrow);

			progress = foreground.FindViewById<ProgressBar>(Resource.Id.progress);

			lastBattery = -1;

			serialNumber.SetOnClickListener(new ViewClickAction((view) => {
				if (onSerialNumberClicked != null) {
					onSerialNumberClicked(record);
				}
			}));

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
				title.Text = d.serialNumber.deviceModel.GetTypeString() + ": " + s.name;
				if (serialNumber != null) {
					serialNumber.Text = gds.device.serialNumber.ToString();
				}

				progress.Visibility = ViewStates.Invisible;
				connection.Visibility = ViewStates.Visible;
				switch (d.connection.connectionState) {
          case EConnectionState.Connected: {
						measurement.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.black)));
						unit.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.black)));

						connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_connected));
						status.Text = c.GetString(Resource.String.connected);
						status.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.green)));
					  break;
          } // EConnectionState.Connected
          case EConnectionState.Broadcasting: {
						measurement.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.light_blue)));
						unit.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.light_blue)));

						connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_c3_broadcast));
						status.Text = c.GetString(Resource.String.disconnected);
						status.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.light_blue)));
					  break;
          } // EConnectionState.Broadcasting
          case EConnectionState.Disconnected: {
						measurement.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.gray)));
						unit.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.gray)));

						connection.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_bluetooth_disconnected));
						status.Text = c.GetString(Resource.String.disconnected);
						status.SetTextColor(new Android.Graphics.Color(c.Resources.GetColor(Resource.Color.red)));
					  break;
          } // EConnectionState.Disconnected
          case EConnectionState.Connecting: {
					  goto case EConnectionState.Disconnected;
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
				title.Text = s.type.GetSensorTypeName() + ": " + s.name;

				status.Visibility = ViewStates.Invisible;
				connection.Visibility = ViewStates.Invisible;
				icon.Visibility = ViewStates.Invisible;
				progress.Visibility = ViewStates.Invisible;
			}

			if (serialNumber != null) {
				if (gds != null) {
					serialNumber.Text = gds.device.name + "'s Subviews";
				} else {
					serialNumber.Text = record.manifold.primarySensor.name + "'s Subviews";
				}

				if (this.record.manifold.secondarySensor != null) {
					serialNumber.Text +=  " (" + c.GetString(Resource.String.workbench_linked) + ")";
				}
			}

			InvalidateBattery(d);
			MarkAsExpanded(record.isExpanded);
		}

		public void MarkAsExpanded(bool expanded) {
			if (record.manifold.sensorPropertyCount <= 0) {
				arrow.Visibility = ViewStates.Invisible;
			} else {
				arrow.Visibility = ViewStates.Visible;
				if (expanded) {
					arrow.SetImageResource(Resource.Drawable.ic_arrow_upwhite);
				} else {
					arrow.SetImageResource(Resource.Drawable.ic_arrow_downwhite);
				}
			}
		}

		private void InvalidateBattery(GaugeDevice device) {
			if (battery == null) {
				return;
			}

			if (device != null) {
				var bat = device.battery;
				if (device.isConnected) {
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
				} else {
					battery.Visibility = ViewStates.Invisible;
					lastBattery = -1;
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
//					Invalidate();
					break;
			}
		}
	}
}
