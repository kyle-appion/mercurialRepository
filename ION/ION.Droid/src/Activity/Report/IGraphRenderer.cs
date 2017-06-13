namespace ION.Droid.Activity.Report {

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
	using ION.Core.Sensors;

	using ION.Droid.Sensors;

  /// <summary>
  /// Represents an entity that will render a graph to a "floating view" (not added to view heirarchy).
  /// </summary>
	public interface IGraphRenderer {
    void Render(Canvas canvas, SensorReportEncapsulation encap);
  }
}
