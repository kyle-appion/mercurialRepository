namespace ION.Droid.Fragments._Workbench {

	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class SimpleSensorPropertyRecord : SensorPropertyRecord<ISensorProperty> {
		public SimpleSensorPropertyRecord(Manifold manifold, ISensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.Simple) { 
		}
	}

	public class SimpleSensorPropertyViewHolder : SensorPropertyViewHolder<ISensorProperty> {
		private BitmapCache cache { get; set; }
		private TextView title { get; set; }
		private ImageView icon { get; set; }
		private View divider { get; set; }
		private TextView measurement { get; set; }
		private TextView unit { get; set; }

		public SimpleSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_measurement_large) {
			this.cache = cache;
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

			title.Text = record.sp.GetLocalizedStringAbreviation(foreground.Context);

			if (record.sp.supportedReset) {
				icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_refresh));
				divider.Visibility = ViewStates.Visible;
			} else {
				icon.Visibility = ViewStates.Invisible;
				divider.Visibility = ViewStates.Invisible;
			}

			measurement.Text = SensorUtils.ToFormattedString(record.sp.modifiedMeasurement, false);
			unit.Text = record.sp.modifiedMeasurement.unit.ToString();
		}
	}
}
