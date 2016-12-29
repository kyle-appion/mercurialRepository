namespace ION.Droid.Widgets.Templates {

	using Android.Views;
	using Android.Widget;

	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Sensors.Properties;
	using ION.Droid.Views;
	using ION.Droid.Sensors;


	/// <summary>
	/// A template for a generic sensor property subview.
	/// </summary>
	/// <code>
	/// MeasurementSubviewTemplate requires the following contract:
	///   View            | id
	/// ------------------+-----------------------------
	///   TextView        | Resource.Id.title
	///   ImageView       | Resource.Id.icon
	///   View            | Resource.Id.view
	///   TextView        | Resource.Id.measurement
	/// </code>
	public class SecondarySensorSubviewTemplate : SubviewTemplate<SecondarySensorProperty> {
		private TextView title { get; set; }
		private ImageView icon { get; set; }
		private View divider { get; set; }
		private TextView measurement { get; set; }
		private TextView unit { get; set; }

		public SecondarySensorSubviewTemplate(View view) : base(view) {
			title = view.FindViewById<TextView>(Resource.Id.title);
			icon = view.FindViewById<ImageView>(Resource.Id.icon);
			divider = view.FindViewById(Resource.Id.view);
			measurement = view.FindViewById<TextView>(Resource.Id.measurement);
			unit = view.FindViewById<TextView>(Resource.Id.unit);

			icon.SetOnClickListener(new ViewClickAction((v) => {
				if (item != null && item.supportedReset) {
					item.Reset();
				}
			}));
		}

		/// <summary>
		/// Binds the view template to the given data.
		/// </summary>
		/// <param name="t">T.</param>
		protected override void OnBind(SecondarySensorProperty t) {
			t.onSensorPropertyChanged += OnSensorPropertyChanged;
		}

		/// <summary>
		/// Invalidates the view within the template.
		/// </summary>
		public override void Invalidate() {
			var c = parentView.Context;

			title.Text = item.GetLocalizedStringAbreviation(parentView.Context);

			icon.Visibility = ViewStates.Invisible;
			divider.Visibility = ViewStates.Invisible;
			unit.Text = "";

			if (item.hasSecondarySensor) {
				title.Text = item.manifold.secondarySensor.type.GetTypeAbreviationString();
				measurement.Text = item.sensor.ToFormattedString(false);
				unit.Text = item.modifiedMeasurement.unit.ToString();
			} else {
				title.Text = c.GetString(Resource.String.link).ToUpper();
				measurement.Text = c.GetString(Resource.String.link_none);
			}
		}

		/// <summary>
		/// Informs the view template that it should unbind itself from its data source.
		/// </summary>
		protected override void OnUnbind() {
			item.onSensorPropertyChanged -= OnSensorPropertyChanged;
		}

		private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
			Invalidate();
		}
	}
}

