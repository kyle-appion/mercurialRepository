﻿﻿using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

using Appion.Commons.Measure;

using ION.Core.App;
using ION.Core.Report.DataLogs;
using ION.Core.Sensors;

using ION.Droid.Devices;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {

  public class ReportGraphRecord : RecordAdapter.Record<SensorDataLogResults> {
    public override int viewType { get { return 0; } }

    public DateIndexLookup dil { get; private set; }
    public bool detailView { get; set; }
    public bool isChecked { get; set; }

    public Scalar lowest { get; private set; }
		public Scalar highest { get; private set; }
		public Scalar average { get; private set; }

		public ReportGraphRecord(DateIndexLookup dil, SensorDataLogResults results) : base(results) {
      this.dil = dil;
      detailView = true;
      ApplyDateRange(DateTime.FromFileTimeUtc(0), DateTime.Now);
    }

    public void ApplyDateRange(DateTime start, DateTime end) {
      var su = data.sensor.unit.standardUnit;
      var low = su.OfScalar(double.MaxValue);
      var high = su.OfScalar(double.MinValue);
      var avg = 0.0;
      var cnt = 0;

      foreach (var id in data.sessionIds) {
        foreach (var m in data[id]) {
          if (m.recordedDate.BoundedBy(start, end)) {
            if (m.measurement < low) {
              low = m.measurement;
            }
            if (m.measurement > high) {
              high = m.measurement;
            }
            avg += m.measurement.ConvertTo(su).amount;
            cnt++;
          }
        }
      }

      if (cnt > 0) {
        lowest = low;
        highest = high;
        average = su.OfScalar(avg / cnt);
      } else {
        lowest = su.OfScalar(0);
				highest = su.OfScalar(0);
				average = su.OfScalar(0);
			}
    }
  }

  public class ReportGraphViewHolder : RecordAdapter.RecordViewHolder<ReportGraphRecord> {
    public Context context { get { return ItemView.Context; } }

    private int desiredHeight;

    private ReportGraphAdapter _adapter;
    private Overlay _overlay;

    private View _titleContainer;
    private View _toggleContainer;
    private View _detailContainer;
    private View _graphContainer;

    private TextView _titleView;
    private TextView _typeView;
    private TextView _lowMeasurementView;
    private TextView _lowMeasurementUnitView;
    private TextView _highMeasurementView;
    private TextView _highMeasurementUnitView;
		private TextView _avgMeasurementView;
    private TextView _avgMeasurementUnitView;

    private PlotView _graphView;

    private CheckBox _checkView;

    private PlotModel _model;
    private LinearAxis _xAxis;
    private LinearAxis _yAxis;

    public ReportGraphViewHolder(ReportGraphAdapter adapter, ViewGroup parent) : base(parent, Resource.Layout.view_holder_report_graph) {
      _adapter = adapter;
      _toggleContainer = ItemView.FindViewById(Resource.Id.view);
      _overlay = new Overlay(_toggleContainer);
      _toggleContainer.Overlay.Add(_overlay);
      

      desiredHeight = parent.Height / 3;
      // Inflate title views
      _titleContainer = ItemView.FindViewById(Resource.Id.title);
      _titleView = _titleContainer.FindViewById<TextView>(Resource.Id.text);
      _typeView = _titleContainer.FindViewById<TextView>(Resource.Id.type);
      
      // Inflate detail views
      _detailContainer = ItemView.FindViewById(Resource.Id.content);
      // Inflate lowest container
      var lowest = ItemView.FindViewById(Resource.Id.lowest);
      _lowMeasurementView = lowest.FindViewById<TextView>(Resource.Id.measurement);
      _lowMeasurementUnitView = lowest.FindViewById<TextView>(Resource.Id.unit);
      // Inflate highest container
      var highest = ItemView.FindViewById(Resource.Id.highest);
      _highMeasurementView = highest.FindViewById<TextView>(Resource.Id.measurement);
      _highMeasurementUnitView = highest.FindViewById<TextView>(Resource.Id.unit);
      // Inflate average container
      var average = ItemView.FindViewById(Resource.Id.average);
      _avgMeasurementView = average.FindViewById<TextView>(Resource.Id.measurement);
      _avgMeasurementUnitView = average.FindViewById<TextView>(Resource.Id.unit);

      // Inflate graph views
      _graphContainer = ItemView.FindViewById(Resource.Id.graph);
      _graphView = _graphContainer.FindViewById<PlotView>(Resource.Id.content);

      // Inflate the Check area
      _checkView = ItemView.FindViewById<CheckBox>(Resource.Id.check);
      var checkParent = _checkView.Parent as ViewGroup;
      checkParent.Click += (sender, e) => {
        record.isChecked = !record.isChecked;
        _checkView.Checked = record.isChecked;
      };
    }

    public override void Bind() {
      ItemView.LayoutParameters.Height = desiredHeight;

      var sensor = record.data.sensor;
      var colors = sensor.GetColorForSensor(context);

      // Build the model
			_model = new PlotModel() {
				Background = OxyColors.Transparent,
				DefaultFontSize = 0,
				Padding = new OxyThickness(0),
        PlotAreaBorderThickness = new OxyThickness(0),
			};

			// Find the bounds of the graph and build the series
      Scalar low = sensor.unit.OfScalar(double.MaxValue);
			Scalar high = sensor.unit.OfScalar(double.MinValue);

			foreach (var session in record.data.sessionIds) {
        var ls = new LineSeries() {
          StrokeThickness = 1,
          Color = OxyColor.FromUInt32((uint)colors.Item1.ToArgb()),
          MarkerType = MarkerType.Circle,
          MarkerSize = 0,
          MarkerStroke = OxyColors.Transparent,
          MarkerStrokeThickness = 0,
        };

				var su = sensor.unit.standardUnit;
				foreach (var log in record.data[session]) {
          var converted = log.measurement.ConvertTo(su);

          if (converted < low) {
            low = converted;
					}
					if (converted > high) {
            high = converted;
					}
          var dp = new DataPoint(record.dil[log.recordedDate], converted.amount);
          ls.Points.Add(dp);
				}

        // Add the series to the model
        _model.Series.Add(ls);
			}

      var span = high.amount - low.amount;
      var padding = span * 0.05;
      if (padding < 1) padding = 1;

			// Create the axes
      _xAxis = new LinearAxis() {
        Position = AxisPosition.Bottom,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 3,
        MaximumPadding = 3,
        Minimum = 0,
        Maximum = record.dil.count,
			};

			_yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
        Minimum = low.amount - padding,
        Maximum = high.amount + padding,
			};

      // Finish setting up the model
			_model.Axes.Add(_xAxis);
			_model.Axes.Add(_yAxis);

      // Register the model to the graph view
      _graphView.Model = _model;


      // Prepare the locked view content
      _titleContainer.SetBackgroundColor(colors.Item1);
      _titleView.Text = sensor.device.serialNumber.deviceModel.GetTypeString() + ": " + sensor.name;
      _typeView.Text = sensor.type.GetSensorTypeName();
      _titleView.SetTextColor(colors.Item2);
      _typeView.SetTextColor(colors.Item2);
    }

    public override void Invalidate() {
      var prefs = AppState.context.preferences.units;

      if (!record.lowest.Equals(default(Scalar))) {
        var low = prefs.ToDefaultUnit(record.lowest);
        _lowMeasurementView.Text = SensorUtils.ToFormattedString(low);
        _lowMeasurementUnitView.Text = low.unit.ToString();
      }

      if (!record.highest.Equals(default(Scalar))) {
        var high = prefs.ToDefaultUnit(record.highest);
        _highMeasurementView.Text = SensorUtils.ToFormattedString(high);
        _highMeasurementUnitView.Text = high.unit.ToString();
      }

      if (!record.average.Equals(default(Scalar))) {
        var average = prefs.ToDefaultUnit(record.average);
        _avgMeasurementView.Text = SensorUtils.ToFormattedString(average);
        _avgMeasurementUnitView.Text = average.unit.ToString();
      }

      _checkView.Checked = record.isChecked;
      if (record.detailView) {
        _detailContainer.Visibility = ViewStates.Visible;
        _graphContainer.Visibility = ViewStates.Invisible;
      } else {
				_detailContainer.Visibility = ViewStates.Invisible;
				_graphContainer.Visibility = ViewStates.Visible;
				_model.InvalidatePlot(true);
			}

      _overlay.leftPercent = _adapter.leftPercent;
      _overlay.rightPercent = _adapter.rightPercent;
      
      _toggleContainer.Invalidate();
    }
    
		private class Overlay : Drawable {
			// Overridden from Drawable
			public override int Opacity { get { return (int)Format.Translucent; } }

			/// <summary>
			/// The percent from the left of the view that the left handle has been dragged.
			/// </summary>
			/// <value>The left percent.</value>
			public float leftPercent { get; set; }
			/// <summary>
			/// The percent from the left of the view that the right handle has been dragged.
			/// </summary>
			/// <value>The left percent.</value>
			public float rightPercent { get; set; }

			/// <summary>
			/// The view that we are drawing our overlay into.
			/// </summary>
			private View _overlayView;

			/// <summary>
			/// The paint that is used to render the overlay.
			/// </summary>
			private Paint _drawPaint;
			/// <summary>
			/// The rect that is used as a flyweight to render the two overlay sides.
			/// </summary>
			private readonly Rect _drawRect = new Rect();

			public Overlay(View overlayView) {
				_overlayView = overlayView;

				// Initialize the draw paint
				_drawPaint = new Paint();
				_drawPaint.Color = new Color(0x30777777);
				_drawPaint.SetStyle(Paint.Style.Fill);
			}

			// Overridden from Drawable
			public override void Draw(Canvas canvas) {
				// Prepare the draw rect
				_drawRect.Top = 0;
				_drawRect.Bottom = _overlayView.MeasuredHeight;

				// Draw the left side
				_drawRect.Left = _overlayView.PaddingLeft;
				_drawRect.Right = (int)(leftPercent * _overlayView.Width);
				canvas.DrawRect(_drawRect, _drawPaint);

				// Draw the right side
        _drawRect.Left = (int)(rightPercent * _overlayView.Width);
				_drawRect.Right = _overlayView.MeasuredWidth - _overlayView.PaddingRight;
				canvas.DrawRect(_drawRect, _drawPaint);
			}

			// Overridden form Drawable
			public override void SetAlpha(int alpha) {
			}

			// Overridden from Drawable
			public override void SetColorFilter(ColorFilter colorFilter) {
			}
		}
  }
}
