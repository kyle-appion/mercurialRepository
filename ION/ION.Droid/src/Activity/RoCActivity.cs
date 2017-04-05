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
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;
	using ROCFlags = ION.Core.Sensors.Properties.RateOfChangeSensorProperty.EFlags;

	using ION.Droid.Content;
	using ION.Droid.Devices;

	[Activity(Label="US RoC", ScreenOrientation=ScreenOrientation.Portrait)]
	public class RoCActivity : IONActivity {
		/// <summary>
		/// The extra that pulls a ManifoldParcelable from the launching intent.
		/// </summary>
		public const string EXTRA_MANIFOLD = "ION.Droid.Activity.Extra.MANIFOLD";

		private PlotView plot;
		private Manifold manifold;
		private RateOfChangeSensorProperty roc;

		private View content1;
		private View content2;
		private View content3;

		private ImageView icon1;
		private ImageView icon2;
		private ImageView icon3;

		private CheckBox check1;
		private CheckBox check2;
		private CheckBox check3;

		private PlotModel model;
		private LinearAxis xAxis;
		private LinearAxis primaryAxis;
		private LinearAxis secondaryAxis;
		private LinearAxis tertiaryAxis;

		private LineSeries primarySeries;
		private LineSeries secondarySeries;
		private LineSeries tertiarySeries;

		private Handler handler;

		// Overridden from IONActivity
		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			SetContentView(Resource.Layout.activity_roc);

			plot = FindViewById<PlotView>(Resource.Id.graph);

			content1 = FindViewById(Resource.Id._1);
			content2 = FindViewById(Resource.Id._2);
			content3 = FindViewById(Resource.Id._3);

			icon1 = content1.FindViewById<ImageView>(Resource.Id.icon);
			icon2 = content2.FindViewById<ImageView>(Resource.Id.icon);
			icon3 = content3.FindViewById<ImageView>(Resource.Id.icon);

			check1 = content1.FindViewById<CheckBox>(Resource.Id.checkbox);
			check2 = content2.FindViewById<CheckBox>(Resource.Id.checkbox);
			check3 = content3.FindViewById<CheckBox>(Resource.Id.checkbox);

			LoadManifold();
			InitViews();

			check1.CheckedChange += (obj, args) => {
				primarySeries.IsVisible = roc.ToggleFlags(ROCFlags.ShowPrimary);
			};

			check2.CheckedChange += (obj, args) => {
				secondarySeries.IsVisible = roc.ToggleFlags(ROCFlags.ShowSecondary);
			};

			check3.CheckedChange += (obj, args) => {
				tertiarySeries.IsVisible = roc.ToggleFlags(ROCFlags.ShowTertiary);
			};

			handler = new Handler(OnHandleMessage);
		}

		// Overridden from Activity
		protected override void OnResume() {
			base.OnResume();
			handler.SendEmptyMessageDelayed(0, 100);
		}

		// Overridden from Activity
		protected override void OnPause() {
			base.OnPause();
			handler.RemoveCallbacksAndMessages(null);
		}

		// Overridden from Activity
		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);          
			}
		}

		private void Invalidate() {
			var device = (manifold.primarySensor as GaugeDeviceSensor)?.device;
			if (device == null || device.isConnected) {
				InvalidatePrimary();
				InvalidateSecondary();
				InvalidateTertiary();

				plot.InvalidatePlot();
				model.InvalidatePlot(true);
			}
		}

		private void InvalidatePrimary() {
			var averageChange = roc.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(2), TimeSpan.FromMinutes(1));

			var primaryMinMax = roc.GetPrimaryMinMax();
			double diff = primaryMinMax.diff / 10;
			if (diff == 0) {
				diff = 1;
			}
			primaryAxis.Minimum = primaryMinMax.min.amount - diff;
			primaryAxis.Maximum = primaryMinMax.max.amount + diff;

			primarySeries.Points.Clear();
			var primaryBuffer = roc.primarySensorPoints;
			foreach (var pp in primaryBuffer) {
				var t = roc.window - (primaryBuffer[0].date - pp.date);
				primarySeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void InvalidateSecondary() {
			if (manifold.secondarySensor == null) {
				return;
			}

			var minMax = roc.GetSecondaryMinMax();
			double diff = minMax.diff / 10;
			if (diff == 0) {
				diff = 1;
			}

			secondaryAxis.Minimum = minMax.min.amount - diff;
			secondaryAxis.Maximum = minMax.max.amount + diff;

			secondarySeries.Points.Clear();
			var secondaryBuffer = roc.secondarySensorPoints;
			foreach (var pp in secondaryBuffer) {
				var t = roc.window - (secondaryBuffer[0].date - pp.date);
				secondarySeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void InvalidateTertiary() {
			if (!roc.hasTertiaryPoints) {
				return;
			}

			var minMax = roc.GetTertiaryMinMax();
			double diff = minMax.diff / 10;
			if (diff == 0) {
				diff = 1;
			}

			tertiaryAxis.Minimum = minMax.min.amount - diff;
			tertiaryAxis.Maximum = minMax.max.amount + diff;

			tertiarySeries.Points.Clear();
			var tertiaryBuffer = roc.tertiaryPoints;
			foreach (var pp in tertiaryBuffer) {
				var t = roc.window - (tertiaryBuffer[0].date - pp.date);
				tertiarySeries.Points.Add(new DataPoint(t.TotalMilliseconds, pp.measurement));
			}
		}

		private void OnHandleMessage(Message message) {
			Invalidate();
			handler.SendEmptyMessageDelayed(0, 100);
		}

		private void InitViews() {
			primarySeries.IsVisible = check1.Checked = roc.HasFlag(ROCFlags.ShowPrimary);
			secondarySeries.IsVisible = check2.Checked = roc.HasFlag(ROCFlags.ShowSecondary);
			tertiarySeries.IsVisible = check3.Checked = roc.HasFlag(ROCFlags.ShowTertiary);

			check1.Text = manifold.primarySensor.type.GetSensorTypeName();

			if (manifold.secondarySensor != null) {
				check2.Text = manifold.secondarySensor.type.GetSensorTypeName();
			}

			if (roc.hasTertiaryPoints) {
				var amount = roc.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(2), TimeSpan.FromMinutes(1)).amount;
				var shsc = manifold.GetSensorPropertyOfType<SuperheatSubcoolSensorProperty>();
				if (manifold.ptChart.fluid.mixture) {
					switch (manifold.ptChart.state) {
						case Fluid.EState.Bubble: {
							check3.SetText(Resource.String.fluid_sc_abrv);
							break;
						} // Fluid.EState.Bubble
						case Fluid.EState.Dew: {
							check3.SetText(Resource.String.fluid_sh_abrv);
							break;
						} // Fluid.EState.Dew
					}
				} else {
					if (amount < 0) {
						check3.SetText(Resource.String.fluid_sc_abrv);
					} else if (amount > 0) {
						check3.SetText(Resource.String.fluid_sh_abrv);
					} else {
						check3.SetText(Resource.String.fluid_sh_sc);
					}
				}
			}
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

			roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
			if (roc == null) {
				Error("How did you get here if the manifold doesn't have a RateOfChangeSensorProperty");
				Finish();
				return;
			}

			// Initialize the plot
			model = new PlotModel() {
				Padding = new OxyThickness(3),
			};

			xAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = roc.window.TotalMilliseconds,

				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			var baseUnit = manifold.primarySensor.unit.standardUnit;
			primaryAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "first",
			};

			secondaryAxis = new LinearAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "second",
			};

			tertiaryAxis = new LinearAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "third",
			};

			primarySeries = new LineSeries() {
				Color = OxyColors.Red,
				StrokeThickness = 1,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "first",
			};

			secondarySeries = new LineSeries() {
				StrokeThickness = 1,
				Color = OxyColors.Blue,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "second",
			};

			tertiarySeries = new LineSeries() {
				Color = OxyColors.Green,
				StrokeThickness = 1,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "third",
			};


			model.Axes.Add(xAxis);
			model.Axes.Add(primaryAxis);
			model.Axes.Add(secondaryAxis);
			model.Axes.Add(tertiaryAxis);
			model.Series.Add(primarySeries);
			model.Series.Add(secondarySeries);
			model.Series.Add(tertiarySeries);
			model.DefaultFontSize = 0;
			model.PlotAreaBorderThickness = new OxyThickness(1, 1, 1, 1);
			plot.Model = model;
		}
	}
}
