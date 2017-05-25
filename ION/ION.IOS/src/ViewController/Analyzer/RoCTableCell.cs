using System;
using UIKit;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Xamarin.iOS;
using OxyPlot.Axes;
using OxyPlot.Series;
using ION.Core.Sensors.Properties;
using ION.Core.Content;
using ION.Core.Sensors;
using Appion.Commons.Measure;
using ION.Core.Devices;
using System.Threading.Tasks;
using Foundation;

namespace ION.IOS.ViewController.Analyzer {
  
  public partial class RoCTableCell : UITableViewCell {
    
    public UILabel cellHeader;
    public UILabel cellReading;
    public UIImageView cellImage;
    public UIView graphView;
		public bool isConnected = false;
		public bool isUpdating = false;

		public RateOfChangeSensorProperty cellRoc;
    public Manifold cellManifold;

		public PlotView plotView;
		public LinearAxis BAX;
		public LinearAxis LAX;
		public LinearAxis RAX;

		public LineSeries primarySeries;
		public LineSeries secondarySeries;
		public OxyColor primaryColor;
		public OxyColor secondaryColor;

    public RoCTableCell(IntPtr handle) {
     
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellRoc = lhSensor.roc;
      cellManifold = lhSensor.manifold;

      cellRoc.onSensorPropertyChanged += OnSensorPropertyChanged;

			primaryColor = OxyColors.Blue;
			secondaryColor = OxyColors.Red;

			if (cellManifold.primarySensor.type == ESensorType.Temperature) {
				primaryColor = OxyColors.Red;
				secondaryColor = OxyColors.Blue;
				Console.WriteLine("Creating roc plotmodel for temperature sensor " + cellManifold.primarySensor.name);
			} else if (cellManifold.primarySensor.type == ESensorType.Vacuum) {
				primaryColor = OxyColors.Maroon;
				Console.WriteLine("Creating roc plotmodel for vacuum sensor " + cellManifold.primarySensor.name);
			} else {
				Console.WriteLine("Creating roc plotmodel for pressure sensor " + cellManifold.primarySensor.name);
			}
      Console.WriteLine("Cell height: " + this.Bounds.Height);
      cellHeader = new UILabel(new CGRect(0, 0, tableRect.Width, 36));
      cellImage = new UIImageView(new CGRect(0, 36, .15 * tableRect.Width, 36));
      graphView = new UIView(new CGRect(0, 72, tableRect.Width, 72));
      graphView.BackgroundColor = UIColor.Clear;

			///SETUP THE TRENDING GRAPH
			if (plotView == null) {
				plotView = new PlotView(new CGRect(0, 0, graphView.Bounds.Width, graphView.Bounds.Height)) {
					Model = CreatePlotModel(),
					BackgroundColor = UIColor.Clear,
				};

				plotView.Layer.BorderWidth = 1f;
				plotView.UserInteractionEnabled = false;
				graphView.AddSubview(plotView);
			}

      cellHeader.Text = "TREND";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      cellReading = lhSensor.rocReading;
      cellReading.Text = Util.Strings.Workbench.Viewer.ROC_STABLE;
      cellReading.TextAlignment = UITextAlignment.Right;
      cellReading.Font = UIFont.FromName("Helvetica-Bold", 18f);
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.Layer.BorderColor = UIColor.Black.CGColor;

      cellImage = lhSensor.rocImage;
      cellImage.Layer.BorderColor = UIColor.Black.CGColor;
      cellImage.Layer.BorderWidth = 1f;

      this.AddSubview(cellHeader);
      this.AddSubview(cellReading);
      this.AddSubview(cellImage);
			this.AddSubview(graphView);
      updateCellGraph();
		}   


		private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {

			if (!isConnected && !isUpdating) {
				if (plotView != null) {
					plotView.Hidden = true;
				}
        isUpdating = true;
				updateCellGraph();
			}
		}

		public async void updateCellGraph() {
      isUpdating = true;
			if (plotView == null) {
				return;
			}

			var device = (cellManifold.primarySensor as GaugeDeviceSensor)?.device;
			isConnected = device.isConnected;

			if (device == null || device.isConnected) {
				InvalidatePrimary();
				if (cellManifold.secondarySensor != null && !(cellManifold.secondarySensor is ManualSensor)) {
					InvalidateSecondary();
				}
				InvalidateTime();

				InvokeOnMainThread(() => {
					plotView.InvalidatePlot();
					plotView.Model.InvalidatePlot(true);
				});
			}

			await Task.Delay(TimeSpan.FromMilliseconds(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_trending_interval")));
			if (isConnected) {
				plotView.Hidden = false;
				updateCellGraph();
			} else {
        isUpdating = false;
				plotView.Hidden = true;
			}
		}

		private void InvalidateTime() {
			var axis = BAX;

			if (cellRoc == null) {
				return;
			}

			var points = cellRoc.primarySensorPoints;

			if (points.Count <= 0) {
				Console.WriteLine("Failed to invalidate time: points.count was " + points.Count);
				return;
			}
			var startTime = points[0];
			var endTime = points[points.Count - 1].date;

			axis.Minimum = (startTime.date - cellRoc.window).ToFileTime() - 1000000;
			axis.Maximum = startTime.date.ToFileTime() + 1000000;

			axis.MajorStep = (long)((cellRoc.window.TotalMilliseconds * 1e4) / 2);
			axis.PlotModel.PlotMargins = new OxyThickness(-7, -7, -7, 3);

			axis.MinorStep = axis.MajorStep / 5;
			axis.AxislineStyle = LineStyle.Solid;
			axis.AxislineThickness = 1;
		}

		private void InvalidatePrimary() {
			if (cellRoc == null) {
				return;
			}

			var minMax = cellRoc.GetPrimaryMinMax();
			var rangeAmount = cellRoc.manifold.primarySensor.maxMeasurement.ConvertTo(cellRoc.manifold.primarySensor.unit.standardUnit).amount * .05;
			var sensorRange = new Scalar(cellRoc.manifold.primarySensor.unit.standardUnit, rangeAmount);
			//////VACUUM READINGS WILL HAVE 3 TIERS
			/// ATM-> 15K microns(2000 Pa) = 10,000 BUFFER
			/// 15K -> 1K microns(134 Pa) = 500 BUFFER
			/// 1K -> 1 micron = 50 BUFFER
			if (cellManifold.primarySensor.type == ESensorType.Vacuum) {
				////set minimum
				/// set maximum
				/// set range
			}
			var sensorUnit = cellRoc.manifold.primarySensor.unit;
			var sensorMin = cellRoc.manifold.primarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			UpdateAxis(LAX, minMax.min, minMax.max, sensorRange, sensorUnit, sensorMin);
			var primaryBuffer = cellRoc.primarySensorPoints;
			var l = primaryBuffer.Count;
			// Resize the points list
			// Trim down to size
			while (primarySeries.Points.Count > l) {
				primarySeries.Points.RemoveAt(primarySeries.Points.Count - 1);
			}
			// Add any missing items
			while (primarySeries.Points.Count < l) {
				primarySeries.Points.Add(new DataPoint());
			}

			for (int i = 0; i < primaryBuffer.Count; i++) {
				var p = primaryBuffer[i];
				primarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), p.measurement);
			}
		}

		private void InvalidateSecondary() {
			if (cellManifold.secondarySensor == null) {
				return;
			}

			if (cellRoc == null) {
				return;
			}

			var minMax = cellRoc.GetSecondaryMinMax();
			var rangeAmount = cellRoc.manifold.secondarySensor.maxMeasurement.ConvertTo(cellRoc.manifold.secondarySensor.unit.standardUnit).amount * .05;
			var sensorRange = new Scalar(cellRoc.manifold.secondarySensor.unit.standardUnit, rangeAmount);
			var sensorUnit = cellRoc.manifold.secondarySensor.unit;
			var sensorMin = cellRoc.manifold.secondarySensor.minMeasurement.ConvertTo(sensorUnit.standardUnit);

			UpdateAxis(RAX, minMax.min, minMax.max, sensorRange, sensorUnit, sensorMin);

			var secondaryBuffer = cellRoc.secondarySensorPoints;
			var l = secondaryBuffer.Count;
			// Resize the points list
			// Trim down to size
			while (secondarySeries.Points.Count > l) {
				secondarySeries.Points.RemoveAt(secondarySeries.Points.Count - 1);
			}
			// Add any missing items
			while (secondarySeries.Points.Count < l) {
				secondarySeries.Points.Add(new DataPoint());
			}

			for (int i = 0; i < secondaryBuffer.Count; i++) {
				var p = secondaryBuffer[i];
				secondarySeries.Points[i] = new DataPoint(p.date.ToFileTime(), p.measurement);
			}
		}
		/// <summary>
		/// Updates the axis to the given state for RoC based measurements.
		/// </summary>
		/// <param name="axis">Axis.</param>
		private void UpdateAxis(LinearAxis axis, Scalar min, Scalar max, Scalar range, Unit u, Scalar sensorMin) {
			if (max.amount < sensorMin.amount) {
				return;
			}
			if (min.amount - (range.amount / 2) < sensorMin.amount) {
				axis.Minimum = sensorMin.amount;
			} else {
				axis.Minimum = min.amount - (range.amount / 2);
				var diffScalar = new Scalar(u.standardUnit, (min.amount - (range.amount / 2)));
			}
			if (max.amount + (range.amount / 2) < sensorMin.amount + range.amount) {
				axis.Maximum = sensorMin.amount + range.amount;
				var diffScalar = new Scalar(u.standardUnit, sensorMin.amount + range.amount);
			} else {
				axis.Maximum = max.amount + (range.amount / 2);
				var diffScalar = new Scalar(u.standardUnit, (max.amount + (range.amount / 2)));
			}
			primarySeries.Color = primaryColor;
			secondarySeries.Color = secondaryColor;

			axis.MinimumPadding = 0.25;
			axis.MaximumPadding = 0.25;
			axis.AxislineStyle = LineStyle.Solid;
			axis.AxislineThickness = 1;
		}

		/// <summary>
		/// Sets up the graph for live trending
		/// </summary>
		/// <returns>The plot model.</returns>
		public PlotModel CreatePlotModel() {
			var model = new PlotModel();

			BAX = new LinearAxis() {
				Position = AxisPosition.Bottom,
				Minimum = DateTime.Now.AddSeconds(-30).ToFileTime() - 100000,
				Maximum = DateTime.Now.ToFileTime() - 100000,
				IsAxisVisible = true,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "time",
				LabelFormatter = (arg) => {
					var d = DateTime.FromFileTime((long)arg);
					return d.Hour.ToString("00") + ":" + d.Minute.ToString("00") + ":" + d.Second.ToString("00");

				},
				Font = model.DefaultFont,
				FontSize = 10,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,
				TickStyle = TickStyle.None,
			};

			var baseUnit =cellManifold.primarySensor.unit.standardUnit;
			LAX = new LinearAxis() {
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "first",
				LabelFormatter = (arg) => {
					var u = cellManifold.primarySensor.unit;
					var p = SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
					return p;
				},
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,
				TickStyle = TickStyle.None,
			};

			RAX = new LinearAxis() {
				Position = AxisPosition.Right,
				Minimum = 0,
				Maximum = 100,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				Key = "second",
				LabelFormatter = (arg) => {
					if (cellManifold.secondarySensor != null) {
						var u = cellManifold.secondarySensor.unit;
						return SensorUtils.ToFormattedString(u.standardUnit.OfScalar(arg).ConvertTo(u), true);
					} else {
						return "";
					}
				},
				Font = model.DefaultFont,
				FontSize = 15,
				TextColor = OxyColors.Black,
				AxislineThickness = 0,
				AxislineStyle = LineStyle.None,
				MajorGridlineStyle = LineStyle.None,
				MinorGridlineStyle = LineStyle.None,
				AxisTickToLabelDistance = 0.0,
				TickStyle = TickStyle.None,
			};

			primarySeries = new LineSeries() {
				StrokeThickness = 1,
				Color = primaryColor,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "first",
			};

			secondarySeries = new LineSeries() {
				StrokeThickness = 1,
				Color = secondaryColor,
				MarkerType = MarkerType.None,
				MarkerSize = 0,
				MarkerStroke = OxyColors.Transparent,
				MarkerStrokeThickness = 0,
				YAxisKey = "second",
			};
			model.Background = OxyColors.Transparent;

			model.PlotMargins = new OxyThickness(-7, -7, -7, -5);
			model.TitlePadding = 0.0;

			model.PlotType = PlotType.XY;
			model.Axes.Add(BAX);
			model.Axes.Add(LAX);
			model.Axes.Add(RAX);
			model.Series.Add(primarySeries);
			model.Series.Add(secondarySeries);

			model.PlotAreaBorderThickness = new OxyThickness(0);
			model.PlotAreaBorderColor = OxyColors.Transparent;

			return model;
		}

  }
}

