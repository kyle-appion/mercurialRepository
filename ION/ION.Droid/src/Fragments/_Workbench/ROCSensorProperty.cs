namespace ION.Droid.Fragments._Workbench {

	using System;
  using System.Collections.Generic;

	using Android.OS;
	using Android.Widget;
  using Android.Views;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

  using Appion.Commons.Math;
	using Appion.Commons.Measure;
  using Appion.Commons.Util;

	using ION.Core.Content;
	using ION.Core.Devices;
  using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
  using ION.Droid.Widgets;
	using ION.Droid.Widgets.RecyclerViews;

	public class ROCSensorPropertyRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
		public ROCSensorPropertyRecord(Manifold manifold, RateOfChangeSensorProperty sp) : base(manifold, sp, WorkbenchAdapter.EViewType.ROC) {
		}
	}

	public class ROCSensorPropertyViewHolder : SensorPropertyViewHolder<RateOfChangeSensorProperty> {
		private BitmapCache cache;
		private PlotView plot;
		private TextView title;
		private ImageView icon;
		private TextView measurement;
		private TextView unit;

    private RocWidgetManager rocManager;

		private Handler handler;
		private bool isRunning;

		public ROCSensorPropertyViewHolder(SwipeRecyclerView recyclerView, BitmapCache cache) : base(recyclerView, Resource.Layout.subview_graph_large) {
			this.cache = cache;
			plot = this.foreground.FindViewById<PlotView>(Resource.Id.graph);
			handler = new Handler(HandleMessage);
			title = foreground.FindViewById<TextView>(Resource.Id.title);
			icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
			measurement = foreground.FindViewById<TextView>(Resource.Id.measurement);
			unit = foreground.FindViewById<TextView>(Resource.Id.unit);
		}

		public override void Bind() {
			base.Bind();
			isRunning = true;

      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      rocManager = new RocWidgetManager(roc.manifold, plot, false);
      rocManager.Initialize();

			handler.SendEmptyMessageDelayed(0, 500);
		}

		public override void Unbind() {
			base.Unbind();
			isRunning = false;
			handler.RemoveCallbacksAndMessages(null);
		}

		public override void Invalidate() {
			base.Invalidate();

			var c = ItemView.Context;

			title.Text = record.sp.GetLocalizedStringAbreviation(c);

      InvalidatePrimary();
      rocManager.Invalidate();

			var device = (record.manifold.primarySensor as GaugeDeviceSensor)?.device;
			if (device != null && device.isConnected) {
        plot.Visibility = ViewStates.Visible;
				rocManager.Invalidate();
			} else {
				measurement.Text = "";
				plot.Visibility = ViewStates.Invisible;
			}
		}

    private void InvalidatePrimary() {
      var roc = record.manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();

      if (roc == null) {
        return;
      }

      var averageChange = roc.GetPrimaryAverageRateOfChange();
      var c = title.Context;

      var amount = Math.Abs(averageChange.magnitude);
      if (amount == 0) {
        measurement.Text = c.GetString(Resource.String.stable);
        unit.Visibility = ViewStates.Invisible;
      } else {
        var dmax = record.sp.sensor.maxMeasurement.amount / 10;
        if (amount > dmax) {
          measurement.Text = "> " + SensorUtils.ToFormattedString(averageChange.unit.OfScalar(dmax));
        } else {
          measurement.Text = SensorUtils.ToFormattedString(averageChange.unit.OfScalar(amount));
        }
        unit.Visibility = ViewStates.Visible;
        unit.Text = c.GetString(Resource.String.time_minute_abrv);
      }

      var dir = Math.Sign(averageChange.magnitude);
      if (averageChange.magnitude == 0) {
        icon.Visibility = ViewStates.Invisible;
      } else if (dir == 1) {
        icon.Visibility = ViewStates.Visible;
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trendup));
      } else {
        icon.Visibility = ViewStates.Visible;
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trenddown));
      }
    }

		private void HandleMessage(Message msg) {
			Invalidate();
			if (isRunning) {
				handler.SendEmptyMessageDelayed(0, (long)record.sp.interval.TotalMilliseconds);
			}
		}
	}
}
