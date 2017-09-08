namespace ION.Droid.Fragments._Analyzer {

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

	public class TimerSensorPropertyRecord : SensorPropertyRecord<TimerSensorProperty> {
		public TimerSensorPropertyRecord(Manifold manifold, TimerSensorProperty sp) : base(manifold, sp, SubviewAdapter.EViewType.Timer) { 
		}
	}

	public class TimerSensorPropertyViewHolder : SensorPropertyViewHolder<TimerSensorProperty> {
		private const int MSG_INVALIDATE = 1;

		private BitmapCache cache { get; set; }
		private TextView title { get; set; }
		private ImageView icon { get; set; }
		private ImageView play { get; set; }
		private View divider { get; set; }
		private TextView measurement { get; set; }

		private Handler handler;
		private bool wasStarted;

		public TimerSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_timer_small) {
			this.cache = cache;
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			play = foreground.FindViewById<ImageView>(Resource.Id.play);
			divider = foreground.FindViewById(Resource.Id.view);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);

			handler = new Handler(HandleMessage);

			icon.SetOnClickListener(new ViewClickAction((v) => {
				if (record != null && record.sp.supportedReset) {
					record.sp.Reset();
				}
			}));

			play.SetOnClickListener(new ViewClickAction((v) => {
				if (record != null) {
					record.sp.Toggle();
				}
			}));

			play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_play));
			wasStarted = false;
		}

		public override void Invalidate() {
			base.Invalidate();
			if (record == null) {
				return;
			}
			var c = foreground.Context;
			title.Text = record.sp.GetLocalizedStringAbreviation(c);
			var sp = record.sp;

			if (sp.supportedReset) {
				icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_refresh));
				divider.Visibility = ViewStates.Visible;
			} else {
				icon.Visibility = ViewStates.Gone;
				divider.Visibility = ViewStates.Gone;
			}

			if (sp.isStarted) {
				play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_pause));
			} else {
				play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_play));
			}

			wasStarted = sp.isStarted;

			if (sp.ellapsedTime.Hours > 0) {
				measurement.Text = sp.ellapsedTime.ToString("h'h 'm'm 's's'");
			} else {
				measurement.Text = sp.ellapsedTime.ToString("m'm 's's'");
			}
		}

		public override void Bind() {
			base.Bind();
			handler.SendEmptyMessageDelayed(MSG_INVALIDATE, 333);
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