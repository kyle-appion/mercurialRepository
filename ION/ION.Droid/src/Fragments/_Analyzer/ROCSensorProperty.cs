namespace ION.Droid.Fragments._Analyzer {

	using System;

	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class ROCSensorPropertyRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
		public ROCSensorPropertyRecord(Manifold manifold, RateOfChangeSensorProperty sp) : base(manifold, sp, SubviewAdapter.EViewType.ROC){ 
		}
	}

	public class ROCSensorPropertyViewHolder : SensorPropertyViewHolder<RateOfChangeSensorProperty> {
		private const int MSG_INVALIDATE = 1;

		private BitmapCache cache;
		private TextView title;
		private ImageView icon;
		private TextView measurement;

		private Handler handler;

		public ROCSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_measurement_small) {
			this.cache = cache;
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);

			handler = new Handler(HandleMessage);
		}

		public override void Invalidate() {
			base.Invalidate();
			if (record == null) {
				return;
			}

			var c = title.Context;
			var sp = record.sp;

			title.Text = record.sp.GetLocalizedStringAbreviation(c);

			if (sp.isStable) {
				measurement.Text = c.GetString(Resource.String.stable);
				icon.Visibility = ViewStates.Invisible;
			} else {
				var mod = sp.modifiedMeasurement.amount;

				if (mod < 0) {
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trenddown));
				} else {
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trendup));
				}

				mod = Math.Abs(mod);

				var dmax = sp.sensor.maxMeasurement.amount / 10;

				if (System.Math.Abs(mod) >= dmax) {
					measurement.Text = ">" + (int)dmax;
				} else { 
					measurement.Text = "" + (int)mod;
				}

				icon.Visibility = ViewStates.Visible;
			}
		}

		/// <summary>
		/// Informs the view template that it should unbind itself from its data source.
		/// </summary>
		public override void Unbind() {
			base.Unbind();
			handler.RemoveMessages(MSG_INVALIDATE);
		}

		/// <summary>
		/// Handles the handler message that will update the template.
		/// </summary>
		/// <param name="message">Message.</param>
		private void HandleMessage(Message message) {
			Invalidate();
			if (record != null) {
				handler.SendEmptyMessageDelayed(MSG_INVALIDATE, 333);
			}
		}
	}
}