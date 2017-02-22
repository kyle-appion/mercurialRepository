namespace ION.Droid.Fragments._Workbench {

	using System;

	using Android.Graphics;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public class SHSCSensorPropertyRecord : SensorPropertyRecord<SuperheatSubcoolSensorProperty> {
		public SHSCSensorPropertyRecord(Manifold manifold, SuperheatSubcoolSensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.SHSC) { 
		}
	}

	public class SHSCSensorPropertyViewHolder : SensorPropertyViewHolder<SuperheatSubcoolSensorProperty> {
		private TextView title;
		private TextView fluid;
		private TextView measurement;
		private TextView unit;

		public SHSCSensorPropertyViewHolder(SwipeRecyclerView parent) : base(parent, Resource.Layout.subview_fluid_large) {
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
			Func<int, string> GetString = c.GetString;
			var pt = record.manifold.ptChart;

			var amount = record.sp.modifiedMeasurement.amount;
			var u = record.sp.modifiedMeasurement.unit;

			if (pt.fluid.mixture) {
				switch (pt.state) {
					case Fluid.EState.Bubble: {
							title.Text = GetString(Resource.String.fluid_sc_abrv);
					} break; // Fluid.EState.Bubble

					case Fluid.EState.Dew: {
							title.Text = GetString(Resource.String.fluid_sh_abrv);
					} break; // Fluid.EState.Dew
				}
				//				measurement.Text = SensorUtils.ToFormattedString(Math.Abs(amount), unit);
			} else {
				if (amount < 0) {
					title.Text = GetString(Resource.String.fluid_sc_abrv);
				} else if (amount > 0) {
					title.Text = GetString(Resource.String.fluid_sh_abrv);
				} else {
					title.Text = GetString(Resource.String.fluid_sh_sc);
				}
				measurement.Text = SensorUtils.ToFormattedString(Math.Abs(amount), u);
			}

			fluid.Text = pt.fluid.name;
			fluid.SetBackgroundColor(new Color(pt.fluid.color));

			if (record.manifold.secondarySensor == null) {
				measurement.Text = c.GetString(Resource.String.fluid_setup);
				unit.Text = "";
			} else {
				measurement.Text = SensorUtils.ToFormattedString(Math.Abs(amount), u);
				unit.Text = record.sp.temperatureSensor.unit.ToString();
			}
		}
	}
}