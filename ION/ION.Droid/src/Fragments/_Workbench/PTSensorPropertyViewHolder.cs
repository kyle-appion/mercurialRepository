namespace ION.Droid.Fragments._Workbench {

	using Android.Graphics;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class PTSensorPropertyRecord : SensorPropertyRecord<PTChartSensorProperty> {
		public PTSensorPropertyRecord(Manifold manifold, PTChartSensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.PT) { 
		}
	}


	public class PTSensorPropertyViewHolder : SensorPropertyViewHolder<PTChartSensorProperty> {
		private TextView title;
		private TextView fluid;
		private TextView measurement;
		private TextView unit;

		public PTSensorPropertyViewHolder(SwipeRecyclerView parent) : base(parent, Resource.Layout.subview_fluid_large) {
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			fluid = foreground.FindViewById<TextView>(Resource.Id.fluid);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);
		}

		public override void Invalidate() {
			base.Invalidate();
			if (record == null) {
				return;
			}

			var c = foreground.Context;
			var pt = record.manifold.ptChart;

			if (pt.fluid.mixture) {
				switch (pt.state) {
					case Fluid.EState.Bubble:
						title.Text = c.GetString(Resource.String.fluid_bubble_abrv);
					break;
					case Fluid.EState.Dew:
						title.Text = foreground.Context.GetString(Resource.String.fluid_dew_abrv);
					break;
					default:
					break;
				}
			} else {
				title.Text = c.GetString(Resource.String.fluid_pt_abrv);
			}
			fluid.Text = record.manifold.ptChart.fluid.name;
			fluid.SetBackgroundColor(new Color(record.manifold.ptChart.fluid.color));

			switch (record.sp.sensor.type) {
				case ESensorType.Pressure:
					measurement.Text = SensorUtils.ToFormattedString(record.sp.modifiedMeasurement);
				break;
				case ESensorType.Temperature:
					measurement.Text = SensorUtils.ToFormattedString(record.sp.modifiedMeasurement);
				break;
			}

			unit.Text = record.sp.unit.ToString();
		}
	}
}
