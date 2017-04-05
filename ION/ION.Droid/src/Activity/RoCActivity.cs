namespace ION.Droid.Activity {

	using System;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.OS;
	using Android.Text;
	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using Appion.Commons.Util;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Content;

	[Activity(Label="US RoC", ScreenOrientation=ScreenOrientation.Portrait)]
	public class RoCActivity : IONActivity {
		/// <summary>
		/// The extra that pulls a ManifoldParcelable from the launching intent.
		/// </summary>
		public const string EXTRA_MANIFOLD = "ION.Droid.Activity.Extra.MANIFOLD";

		private PlotView plot;

		private View content1;
		private View content2;
		private View content3;

		private ImageView icon1;
		private ImageView icon2;
		private ImageView icon3;

		private CheckBox check1;
		private CheckBox check2;
		private CheckBox check3;

		private Manifold manifold;
		private PlotModel model;
		private LineSeries mainSeries;
		private LinearAxis axis1;
		private LinearAxis axis2;

		private Handler handler;

		// Overridden from IONActivity
		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);
			SetContentView(Resource.Layout.activity_roc);

			plot = FindViewById<PlotView>(Resource.Id.graph);

			content1 = FindViewById(Resource.Id._1);
			content2 = FindViewById(Resource.Id._2);
			content3 = FindViewById(Resource.Id._3);

			icon1 = FindViewById<ImageView>(Resource.Id.icon);
			icon2 = FindViewById<ImageView>(Resource.Id.icon);
			icon3 = FindViewById<ImageView>(Resource.Id.icon);

			check1 = FindViewById<CheckBox>(Resource.Id.checkbox);
			check2 = FindViewById<CheckBox>(Resource.Id.checkbox);
			check3 = FindViewById<CheckBox>(Resource.Id.checkbox);

			LoadManifold();

			handler = new Handler(OnHandleMessage);
		}

		protected override void OnResume() {
			base.OnResume();
			handler.SendEmptyMessageDelayed(0, 100);
		}

		protected override void OnPause() {
			base.OnPause();
			handler.RemoveCallbacksAndMessages(null);
		}

		public void Invalidate() {
			InvalidatePrimary();
			
			plot.InvalidatePlot();
			model.ResetAllAxes();
		}

		private void InvalidatePrimary() {
			if (!check1.Checked) {
				return;
			}

			var sp = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
			var roc = sp.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
			var amount = Math.Abs(roc.amount);

			var minMax = sp.GetPrimaryMinMax();
			double diff = (minMax.diff) / 10;
			if (diff == 0) {
				diff = 1;
			}
			axis1.Minimum = minMax.min.amount - diff;
			axis1.Maximum = minMax.max.amount + diff;


			mainSeries.Points.Clear();
			var buffer = sp.primarySensorPoints;
			foreach (var pp in buffer) {
				var t = sp.window - (buffer[0].date - pp.date);
				mainSeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void OnHandleMessage(Message message) {
			Invalidate();
			handler.SendEmptyMessageDelayed(0, 100);
		}

		private void LoadManifold() {
			var mp = Intent.GetParcelableExtra(EXTRA_MANIFOLD) as ManifoldParcelable;
			if (mp == null) {
				// TODO ahodder@appioninc.com: Localize
				Error("US Manifold was not passed to activity");
				Finish();
				return;
			}

			manifold = mp.Get(ion);
			if (manifold == null) {
				// TODO ahodder@appioninc.com: Localize
				Error("US Manifold did not load from parcelable");
				Finish();
				return;
			}

			var roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
			if (roc == null) {
				Error("How did you get here if the manifold doesn't have a RateOfChangeSensorProperty");
				Finish();
				return;
			}

			model = new PlotModel() {
				Padding = new OxyThickness(3),
			};

			var timeAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = roc.window.TotalMilliseconds,

				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			mainSeries = new LineSeries() {
				StrokeThickness = 1,
				MarkerType = MarkerType.Circle,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
			};

			model.Series.Add(mainSeries);
			model.Axes.Add(timeAxis);
			// Load the primary sensor axis
			model.Axes.Add(axis1 = CreateAxisForSensor(roc, manifold.primarySensor, content1));

			// Load the secondary sensor axis
			if (manifold.secondarySensor != null) {
				model.Axes.Add(axis2 = CreateAxisForSensor(roc, manifold.secondarySensor, content2));
			}

			plot.Model = model;
		}

		private LinearAxis CreateAxisForSensor(RateOfChangeSensorProperty roc, Sensor sensor, View content) {
			var baseUnit = sensor.unit.standardUnit;

			var ret = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = sensor.minMeasurement.ConvertTo(baseUnit).amount,
				Maximum = sensor.maxMeasurement.ConvertTo(baseUnit).amount,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
			};

			return ret;
		}
	}
}
