﻿namespace ION.Droid.Activity.Report {

  using System;

  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Views;

  using Appion.Commons.Util;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.Xamarin.Android;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Report.DataLogs;
  using ION.Core.Sensors;

  using ION.Droid.Sensors;

  public class DetailedGraphRenderer : IGraphRenderer {

    private Context context;
    private IION ion;

    private PlotView plot;
    private PlotModel model;
    private LinearAxis xAxis;
    private LinearAxis yAxis;

    public DetailedGraphRenderer(Context context, IION ion) {
      this.context = context;
      this.ion = ion;
      plot = new PlotView(context);
      model = new PlotModel();

      xAxis = new LinearAxis() {
        Position = AxisPosition.Bottom,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 3,
        MaximumPadding = 3,
        AxisTickToLabelDistance = 1,
        AxislineColor = OxyColors.Black,
        AxislineStyle = LineStyle.Solid,
        MinorGridlineStyle = LineStyle.None,
        MajorGridlineStyle = LineStyle.Solid,
        AxisTitleDistance = 5,
        TickStyle = TickStyle.Outside,
        TextColor = OxyColors.Black,
        FontSize = 7,
        Angle = 90,
    };

      yAxis = new LinearAxis() {
        Position = AxisPosition.Left,
        IsAxisVisible = true,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 3,
        MaximumPadding = 3,
        AxisTitleDistance = 20,
        AxislineColor = OxyColors.Black,
        AxislineStyle = LineStyle.Solid,
        MinorGridlineStyle = LineStyle.None,
        MajorGridlineStyle = LineStyle.Solid,
        TickStyle = TickStyle.Outside,
        TextColor = OxyColors.Black,
        FontSize = 7,
      };

      model.Axes.Add(xAxis);
      model.Axes.Add(yAxis);

      model.Background = OxyColors.Transparent;
      model.DefaultFontSize = 0;

      plot.Model = model;
    }

    // Implemented for IGraphRenderer
    public void Render(Canvas canvas, DateIndexLookup dil, SensorDataLogResults results) {
      var sensor = results.sensor;
      var serial = sensor.device.serialNumber;
      var indices = dil.count;
      var series = results.AsLineSeries(dil);

      model.Subtitle = serial.ToString() + "(" + sensor.type.GetTypeString() + ")";

      xAxis.Minimum = 0;
      xAxis.Maximum = indices - 1;
      xAxis.MajorStep = (xAxis.Maximum - xAxis.Minimum) / Math.Min(10, indices);
      xAxis.LabelFormatter = (arg) => {
        var date = dil.GetDateTimeFromIndex((int)arg);
        return date.ToLongTimeString();
      };

      yAxis.Minimum = results.minimum.amount;
      yAxis.Maximum = results.maximum.amount;
      yAxis.MajorStep = (yAxis.Maximum - yAxis.Minimum) / 5;
      if (yAxis.MajorStep == 0) {
        yAxis.MajorStep = 1;
      }
      yAxis.LabelFormatter = (arg) => {
        var su = sensor.unit.standardUnit;
        return SensorUtils.ToFormattedString(su.OfScalar(arg).ConvertTo(sensor.unit));
      };
      yAxis.Title = sensor.unit.ToString();

      model.Series.Clear();
      foreach (var s in series) {
        var colors = sensor.GetColorForSensor(context);
        s.Color = OxyColor.FromUInt32((uint)colors.Item1.ToArgb());
        model.Series.Add(s);
      }

      model.InvalidatePlot(true);

      var widthSpec = View.MeasureSpec.MakeMeasureSpec(800, MeasureSpecMode.Exactly);
      var heightSpec = View.MeasureSpec.MakeMeasureSpec(400, MeasureSpecMode.Exactly);
      plot.Measure(widthSpec, heightSpec);
      plot.Layout(0, 0, plot.MeasuredWidth, plot.MeasuredHeight);
      plot.Draw(canvas);

      if (plot.Model.GetLastPlotException() != null) {
        throw plot.Model.GetLastPlotException();
      }
    }
  }
}
