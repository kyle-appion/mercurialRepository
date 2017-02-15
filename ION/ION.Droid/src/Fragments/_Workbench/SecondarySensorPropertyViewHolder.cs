namespace ION.Droid.Fragments._Workbench {

	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class SecondarySensorPropertyRecord : SensorPropertyRecord<SecondarySensorProperty> {
		public SecondarySensorPropertyRecord(Manifold manifold, SecondarySensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.Secondary){ 
		}
	}

	public class SecondarySensorPropertyViewHolder : SensorPropertyViewHolder<SecondarySensorProperty> {
		private TextView title { get; set; }
		private ImageView icon { get; set; }
		private View divider { get; set; }
		private TextView measurement { get; set; }
		private TextView unit { get; set; }

		public SecondarySensorPropertyViewHolder(SwipeRecyclerView recyclerView) : base(recyclerView, Resource.Layout.subview_measurement_large) {
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			divider = foreground.FindViewById(Resource.Id.view);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);

			icon.SetOnClickListener(new ViewClickAction((v) => {
				if (record != null && record.sp.supportedReset) {
					record.sp.Reset();
				}
			}));
		}

		public override void Invalidate() {
			base.Invalidate();
			if (record == null) {
				return;
			}
			var c = foreground.Context;
			var sp = record.sp;

			title.Text = sp.GetLocalizedStringAbreviation(c);

			icon.Visibility = ViewStates.Invisible;
			divider.Visibility = ViewStates.Invisible;
			unit.Text = "";

			if (sp.hasSecondarySensor) {
				title.Text = record.manifold.secondarySensor.type.GetTypeAbreviationString();
				measurement.Text = sp.sensor.ToFormattedString(false);
				unit.Text = sp.modifiedMeasurement.unit.ToString();
			} else {
				title.Text = c.GetString(Resource.String.link).ToUpper();
				measurement.Text = c.GetString(Resource.String.link_none);
			}
		}
	}
}